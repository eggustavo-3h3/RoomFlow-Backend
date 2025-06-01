using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using RoomFlowApi.Domain.Base;
using RoomFlowApi.Domain.DTOs.Aula;
using RoomFlowApi.Domain.DTOs.Curso;
using RoomFlowApi.Domain.DTOs.Disciplina;
using RoomFlowApi.Domain.DTOs.Login;
using RoomFlowApi.Domain.DTOs.Sala;
using RoomFlowApi.Domain.DTOs.Turma;
using RoomFlowApi.Domain.DTOs.Usuario;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using RoomFlowApi.Domain.Entities;
using RoomFlowApi.Infra.Data.Context;
using RoomFlowApi.Infra.Email;
using RoomFlowApi.Domain.DTOs.AlterarSenha;
using RoomFlowApi.Domain.DTOs.ResetSenha;
using RoomFlowApi.Domain.Enumerators;
using RoomFlowApi.Domain.Extensions;
using RoomFlowApi.Domain.DTOs.Signup;
using RoomFlowApi.Domain.DTOs.Mapa;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http.Json;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(config =>
{
    config.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "RoomFlow API",
        Version = "v1",
        Description = "API para gerenciamento de Salas da Etec"
    });

    config.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = @"<b>JWT Autorização</b> <br/> 
                          Digite 'Bearer' [espaço] e em seguida colar seu token na caixa de texto abaixo.
                          <br/> <br/>
                          <b>Exemplo:</b> 'bearer 123456abcdefg...'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    config.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header
            },
            new List<string>()
        }
    });
});

builder.Services.AddDbContext<RoomFlowContext>();

builder.Services.AddAuthentication(
    JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = 
        new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "room.flow",
            ValidAudience = "room.flow",
            IssuerSigningKey = 
            new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(
                "{4ea4267e-eeae-4a10-8a05-8c237c13cb55}"))
        };
    });

builder.Services.AddAuthorizationBuilder()
    .AddPolicy("Administrador", policy => policy.RequireRole("Administrador"))
    .AddPolicy("Professor", policy => policy.RequireRole("Professor", "Administrador"));

builder.Services.Configure<JsonOptions>(options =>
{
    options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthentication();
app.UseAuthorization();

app.UseHttpsRedirection();

app.UseCors(cp => cp
        .AllowAnyHeader()
        .AllowAnyMethod()
        .AllowAnyOrigin()
);

#region Cursos

app.MapGet("curso/listar", (RoomFlowContext context) =>
{
    var cursos = context.CursoSet.Select(p => new CursoListarDto
    {
        Id = p.Id,
        Nome = p.Nome,
        Periodo = p.Periodo,
    }).AsEnumerable();

    return Results.Ok(cursos);
}).WithTags("Curso");

app.MapGet("curso/obter/{id:guid}", (RoomFlowContext context, Guid id) =>
{
    var curso = context.CursoSet.Find(id);

    if (curso is null)
        return Results.BadRequest(new BaseResponse("Curso não encontrado"));

    var cursoDto = new CursoListarDto
    {
        Id = curso.Id,
        Nome = curso.Nome,
        Periodo = curso.Periodo
    };

    return Results.Ok(cursoDto);
}).WithTags("Curso");

app.MapPost("curso/adicionar", (RoomFlowContext context, CursoAdicionarDto cursoDto) =>
{
    var resultado = new CursoAdicionarValidatorDto().Validate(cursoDto);

    if (!resultado.IsValid)
        return Results.BadRequest(resultado.Errors.Select(error => error.ErrorMessage));

    var curso = new Curso
    {
        Id = Guid.NewGuid(),
        Nome = cursoDto.Nome,
        Periodo = cursoDto.Periodo,
    };

    context.CursoSet.Add(curso);
    context.SaveChanges();

    return Results.Created("Created", "Curso Cadastrada com Sucesso!");
}).RequireAuthorization("Administrador").WithTags("Curso");

app.MapPut("curso/atualizar", (RoomFlowContext context, CursoAtualizarDto cursoDto) =>
{
    var resultado = new CursoAtualizarValidatorDto().Validate(cursoDto);

    if (!resultado.IsValid)
        return Results.BadRequest(resultado.Errors.Select(error => error.ErrorMessage));

    var curso = context.CursoSet.Find(cursoDto.Id);
    if (curso is null)
        return Results.NotFound(new BaseResponse("Curso não encontrado."));

    curso.Nome = cursoDto.Nome;
    curso.Periodo = cursoDto.Periodo;
    context.SaveChanges();

    return Results.Ok("Curso Atualizado com Sucesso!");
}).RequireAuthorization().WithTags("Curso");

app.MapDelete("curso/remover/{id:guid}", (RoomFlowContext context, Guid id) =>
{
    var curso = context.CursoSet.Find(id);
    if (curso is null)
        return Results.BadRequest(new BaseResponse("Curso não encontrado."));

    context.CursoSet.Remove(curso);
    context.SaveChanges();

    return Results.Ok("Curso Removido com Sucesso!");
}).RequireAuthorization().WithTags("Curso");

#endregion

#region Disciplina

app.MapGet("disciplina/listar", (RoomFlowContext context) =>
{
    var disciplinas = context.DisciplinaSet.Select(p => new DisciplinaListarDto
    {
        Id = p.Id,
        Nome = p.Nome,
        Descricao = p.Descricao,
    }).AsEnumerable();

    return Results.Ok(disciplinas);
}).WithTags("Disciplina");

app.MapGet("disciplina/obter/{id:guid}", (RoomFlowContext context, Guid id) =>
{
    var disciplina = context.DisciplinaSet.Find(id);

    if (disciplina is null)
        return Results.BadRequest(new BaseResponse("Disciplina não encontrada"));

    var disciplinaDto = new DisciplinaListarDto
    {
        Id = disciplina.Id,
        Nome = disciplina.Nome,
        Descricao = disciplina.Descricao,
    };

    return Results.Ok(disciplinaDto);
}).WithTags("Disciplina");

app.MapPost("disciplina/adicionar", (RoomFlowContext context, DisciplinaAdicionarDto disciplinaDto) =>
{
    var resultado = new DisciplinaAdicionarValidatorDto().Validate(disciplinaDto);

    if (!resultado.IsValid)
        return Results.BadRequest(resultado.Errors.Select(error => error.ErrorMessage));

    var disciplina = new Disciplina
    {
        Id = Guid.NewGuid(),
        Nome = disciplinaDto.Nome,
        Descricao = disciplinaDto.Descricao,
    };

    context.DisciplinaSet.Add(disciplina);
    context.SaveChanges();

    return Results.Created("Created", "Disciplina Cadastrada com Sucesso!");
}).RequireAuthorization().WithTags("Disciplina");

app.MapPut("disciplina/atualizar", (RoomFlowContext context, DisciplinaAtualizarDto disciplinaDto) =>
    {
        var resultado = new DisciplinaAtualizarValidatorDto().Validate(disciplinaDto);

        if (!resultado.IsValid)
            return Results.BadRequest(resultado.Errors.Select(error => error.ErrorMessage));

        var disciplina = context.DisciplinaSet.Find(disciplinaDto.Id);
        if (disciplina is null)
            return Results.NotFound(new BaseResponse("Disciplina não encontrada."));

        disciplina.Nome = disciplinaDto.Nome;
        disciplina.Descricao = disciplinaDto.Descricao;
        context.SaveChanges();

        return Results.Ok("Disciplina Atualizada com Sucesso!");
    }).RequireAuthorization().WithTags("Disciplina");

app.MapDelete("disciplina/remover/{id:guid}", (RoomFlowContext context, Guid id) =>
{
    var disciplina = context.DisciplinaSet.Find(id);
    if (disciplina is null)
        return Results.BadRequest(new BaseResponse("Disciplina não encontrada."));

    context.DisciplinaSet.Remove(disciplina);
    context.SaveChanges();
    return Results.Ok("Disciplina Removida com Sucesso!");
}).RequireAuthorization().WithTags("Disciplina");

#endregion

#region Sala

app.MapGet("sala/listar", (RoomFlowContext context) =>
{
    var salas = context.SalaSet.Select(p => new SalaListarDto
    {
        Id = p.Id,
        Descricao = p.Descricao,
        StatusSala = p.StatusSala,
        TipoSala = p.TipoSala,
        NumeroSala = p.NumeroSala
    }).AsEnumerable();

    return Results.Ok(salas);
}).WithTags("Sala");

app.MapGet("sala/obter/{id:guid}", (RoomFlowContext context, Guid id) =>
{
    var sala = context.SalaSet.Find(id);

    if (sala is null)
        return Results.BadRequest(new BaseResponse("Sala não Encontrada"));

    var salaDto = new SalaListarDto
    {
        Id = sala.Id,
        Descricao = sala.Descricao,
        StatusSala = sala.StatusSala,
        TipoSala = sala.TipoSala,
        NumeroSala = sala.NumeroSala,
    };

    return Results.Ok(salaDto);
}).WithTags("Sala");

app.MapPost("sala/adicionar", (RoomFlowContext context, SalaAdicionarDto salaAdicionarDto) =>
{
    var resultado = new SalaAdicionarValidatorDto().Validate(salaAdicionarDto);

    if (!resultado.IsValid)
        return Results.BadRequest(resultado.Errors.Select(error => error.ErrorMessage));

    var sala = new Sala
    {
        Id = Guid.NewGuid(),
        NumeroSala = salaAdicionarDto.NumeroSala,
        Descricao = salaAdicionarDto.Descricao,
        StatusSala = salaAdicionarDto.StatusSala,
        TipoSala = salaAdicionarDto.TipoSala,
        FlagExibirNumeroSala = salaAdicionarDto.FlagExibirNumeroSala
    };

    context.SalaSet.Add(sala);
    context.SaveChanges();

    return Results.Created("Created", "Sala Cadastrada com Sucesso!");
}).RequireAuthorization().WithTags("Sala");

app.MapPut("sala/atualizar", (RoomFlowContext context, SalaAtualizarDto salaAtualizarDto) =>
{
    var resultado = new SalaAtualizarValidatorDto().Validate(salaAtualizarDto);

    if (!resultado.IsValid)
        return Results.BadRequest(resultado.Errors.Select(error => error.ErrorMessage));

    var sala = context.SalaSet.Find(salaAtualizarDto.Id);
    if (sala is null)
        return Results.NotFound(new BaseResponse("Sala não Encontrada."));

    sala.NumeroSala = salaAtualizarDto.NumeroSala;
    sala.Descricao = salaAtualizarDto.Descricao;
    sala.StatusSala = salaAtualizarDto.StatusSala;
    sala.TipoSala = salaAtualizarDto.TipoSala;
    sala.FlagExibirNumeroSala = salaAtualizarDto.FlagExibirNumeroSala;

    context.SaveChanges();

    return Results.Ok(new BaseResponse("Sala Atualizada com Sucesso!"));
}).RequireAuthorization().WithTags("Sala");

app.MapDelete("sala/remover/{id:guid}", (RoomFlowContext context, Guid id) =>
{
    var sala = context.SalaSet.Find(id);
    if (sala is null)
        return Results.BadRequest(new BaseResponse("Sala não Encontrada"));

    context.SalaSet.Remove(sala);
    context.SaveChanges();

    return Results.Ok(new BaseResponse("Sala Removida com Sucesso!"));
    
}).RequireAuthorization().WithTags("Sala");

#endregion

#region Turma

app.MapGet("turma/listar", (RoomFlowContext context) =>
{
    var turmas = context.TurmaSet.Include(p => p.Curso).Select(p => new TurmaListarDto()
    {
        Id = p.CursoId,
        Descricao = p.Descricao,
        Curso = new CursoListarDto
        {
            Id = p.Curso.Id,
            Nome = p.Curso.Nome,
            Periodo = p.Curso.Periodo
        }
    }).AsEnumerable();

    return Results.Ok(turmas);
}).WithTags("Turma");

app.MapGet("turma/obter/{id:guid}", (RoomFlowContext context, Guid id) =>
{
    var turma = context.TurmaSet.Include(p => p.Curso).FirstOrDefault(p => p.Id == id);
    if (turma is null)
        return Results.BadRequest(new BaseResponse("Turma não encontrada."));

    var turmaDto = new TurmaListarDto
    {
        Id = turma.CursoId,
        Descricao = turma.Descricao,
        Curso = new CursoListarDto
        {
            Id = turma.Curso.Id,
            Nome = turma.Curso.Nome,
            Periodo = turma.Curso.Periodo
        }
    };

    return Results.Ok(turmaDto);
}).WithTags("Turma");

app.MapPost("turma/adicionar", (RoomFlowContext context, TurmaAdicionarDto turmaDto) =>
{
    var resultado = new TurmaAdicionarValidatorDto().Validate(turmaDto);
    if (!resultado.IsValid)
        return Results.BadRequest(resultado.Errors.Select(error => error.ErrorMessage));

    var turma = new Turma
    {
        Id = Guid.NewGuid(),
        Descricao = turmaDto.Descricao,
        CursoId = turmaDto.CursoId,
    };

    context.TurmaSet.Add(turma);
    context.SaveChanges();

    return Results.Created("Created", "Turma Cadastrada com Sucesso!");
}).RequireAuthorization().WithTags("Turma");

app.MapPut("turma/atualizar", (RoomFlowContext context, TurmaAtualizarDto turmaDto) =>
{
    var resultado = new TurmaAtualizarValidatorDto().Validate(turmaDto);
    if (!resultado.IsValid)
        return Results.BadRequest(resultado.Errors.Select(error => error.ErrorMessage));

    var turma = context.TurmaSet.Find(turmaDto.Id);
    if (turma is null)
        return Results.NotFound(new BaseResponse("Turma não encontrada."));

    turma.Descricao = turmaDto.Descricao;
    turma.CursoId = turmaDto.CursoId;

    context.SaveChanges();
    
    return Results.Ok("Turma Atualizada com Sucesso!");
}).RequireAuthorization().WithTags("Turma");

app.MapDelete("turma/remover/{id:guid}", (RoomFlowContext context, Guid id) =>
{
    var turma = context.TurmaSet.Find(id);
    if (turma is null)
        return Results.NotFound(new BaseResponse("Turma não encontrada para remoção."));

    context.TurmaSet.Remove(turma);
    
    context.SaveChanges();

    return Results.Ok("Turma Removida com Sucesso!");
}).RequireAuthorization().WithTags("Turma");

#endregion

#region Aula

app.MapGet("aula/listar", (RoomFlowContext context) =>
{
    var aulas = context.AulaSet
        .Include(p => p.Disciplina)
        .Include(p => p.Sala)
        .Include(p => p.TurmaId)
        .Include(p => p.ProfessorId)
        .Include(p => p.Curso)
        .Include(aula => aula.Professor)
        .Include(aula => aula.Turma)
        .ThenInclude(turma => turma.Curso)
        .Select(p => new AulaListarDto
        {
            Id = p.Id,
            Bloco = p.Bloco,
            Disciplina = new DisciplinaListarDto
            {
                Id = p.Disciplina.Id,
                Nome = p.Disciplina.Nome,
                Descricao = p.Disciplina.Descricao
            },
            Sala = new SalaListarDto
            {
                Id = p.Sala.Id,
                NumeroSala = p.Sala.NumeroSala,
                Descricao = p.Sala.Descricao,
                StatusSala = p.Sala.StatusSala,
                TipoSala = p.Sala.TipoSala,
                FlagExibirNumeroSala = p.Sala.FlagExibirNumeroSala
            },
            Turma = new TurmaListarDto
            {
                Id = p.Turma.Id,
                Descricao = p.Turma.Descricao,
                Curso = new CursoListarDto
                {
                    Id = p.Turma.Curso.Id,
                    Nome = p.Turma.Curso.Nome,
                    Periodo = p.Turma.Curso.Periodo
                }
            },
            Data = p.Data,
            Professor = new UsuarioListarDto
            {
                Id = p.ProfessorId,
                Nome = p.Professor.Nome,
                Login = p.Professor.Login,
                Perfil = p.Professor.Perfil,
                Status = p.Professor.Status
            },
            Curso = new CursoListarDto
            {
                Id = p.CursoId,
                Nome = p.Curso.Nome,
                Periodo = p.Curso.Periodo
            }
        }).AsEnumerable();

    return Results.Ok(aulas);
}).WithTags("Aula");

app.MapGet("aula/obter{id:guid}", (RoomFlowContext context, Guid id) =>
{
    var aula = context.AulaSet
        .Include(p => p.Disciplina)
        .Include(p => p.Sala)
        .Include(p => p.TurmaId)
        .Include(p => p.ProfessorId)
        .Include(p => p.Curso)
        .Include(aula => aula.Professor)
        .Include(aula => aula.Turma)
        .ThenInclude(turma => turma.Curso)
        .FirstOrDefault(p => p.Id == id);

    if (aula is null)
        return Results.BadRequest(new BaseResponse("Aula não encontrada."));

    var aulaDto = new AulaListarDto
    {
        Id = aula.Id,
        Bloco = aula.Bloco,
        Disciplina = new DisciplinaListarDto
        {
            Id = aula.Disciplina.Id,
            Nome = aula.Disciplina.Nome,
            Descricao = aula.Disciplina.Descricao
        },
        Sala = new SalaListarDto
        {
            Id = aula.Sala.Id,
            NumeroSala = aula.Sala.NumeroSala,
            Descricao = aula.Sala.Descricao,
            StatusSala = aula.Sala.StatusSala,
            TipoSala = aula.Sala.TipoSala,
            FlagExibirNumeroSala = aula.Sala.FlagExibirNumeroSala
        },
        Turma = new TurmaListarDto
        {
            Id = aula.Turma.Id,
            Descricao = aula.Turma.Descricao,
            Curso = new CursoListarDto
            {
                Id = aula.Turma.Curso.Id,
                Nome = aula.Turma.Curso.Nome,
                Periodo = aula.Turma.Curso.Periodo
            }
        },
        Data = aula.Data,
        Professor = new UsuarioListarDto
        {
            Id = aula.ProfessorId,
            Nome = aula.Professor.Nome,
            Login = aula.Professor.Login,
            Perfil = aula.Professor.Perfil,
            Status = aula.Professor.Status
        },
        Curso = new CursoListarDto
        {
            Id = aula.CursoId,
            Nome = aula.Curso.Nome,
            Periodo = aula.Curso.Periodo
        }
    };

    return Results.Ok(aulaDto);
}).WithTags("Aula");

app.MapPost("aula/adicionar", async (RoomFlowContext context, AulaAdicionarDto aulaAdicionarDto) =>
{
    var resultado = new AulaAdicionarValidatorDto().Validate(aulaAdicionarDto);
    if (!resultado.IsValid)
        return Results.BadRequest(resultado.Errors.Select(error => error.ErrorMessage));

    var aulaExistente = await context.AulaSet.FirstOrDefaultAsync(a =>
        a.SalaId == aulaAdicionarDto.SalaId &&
        a.Bloco == aulaAdicionarDto.Bloco &&
        a.Data.Date == aulaAdicionarDto.Data.Date);

    if (aulaExistente != null)
        return Results.Conflict("Já existe uma aula cadastrada para a mesma sala, bloco e data.");

    var disciplina = context.DisciplinaSet.Find(aulaAdicionarDto.DisciplinaId);
    if (disciplina is null)
        return Results.BadRequest(new BaseResponse("Disciplina não encontrada."));

    var sala = context.SalaSet.Find(aulaAdicionarDto.SalaId);
    if (sala is null)
        return Results.BadRequest(new BaseResponse("Sala não encontrada."));

    var turma = context.TurmaSet.Find(aulaAdicionarDto.TurmaId);
    if (turma is null)
        return Results.BadRequest(new BaseResponse("Turma não encontrada."));

    var professor = context.UsuarioSet.Find(aulaAdicionarDto.ProfessorId);
    if (professor is null)
        return Results.BadRequest(new BaseResponse("Professor não encontrado."));

    var curso = context.CursoSet.Find(aulaAdicionarDto.CursoId);
    if (curso is null)
        return Results.BadRequest(new BaseResponse("Curso não encontrado."));
    
    var aula = new Aula
    {
        Id = Guid.NewGuid(),
        Bloco = aulaAdicionarDto.Bloco,
        DisciplinaId = aulaAdicionarDto.DisciplinaId,
        SalaId = aulaAdicionarDto.SalaId,
        TurmaId = aulaAdicionarDto.TurmaId,
        Data = aulaAdicionarDto.Data,
        ProfessorId = aulaAdicionarDto.ProfessorId,
        CursoId = aulaAdicionarDto.CursoId
    };

    context.AulaSet.Add(aula);
    await context.SaveChangesAsync();

    return Results.Created("Created", "Aula Cadastrada com Sucesso!");
}).RequireAuthorization().WithTags("Aula");

app.MapPut("aula/atualizar", (RoomFlowContext context, AulaAtualizarDto aulaAtualizarDto) =>
{
    var resultado = new AulaAtualizarValidatorDto().Validate(aulaAtualizarDto);
    if (!resultado.IsValid)
        return Results.BadRequest(resultado.Errors.Select(error => error.ErrorMessage));

    var aula = context.AulaSet.Find(aulaAtualizarDto.Id);
    if (aula is null)
        return Results.NotFound(new BaseResponse("Aula não encontrada."));

    var disciplina = context.DisciplinaSet.Find(aulaAtualizarDto.DisciplinaId);
    if (disciplina is null)
        return Results.BadRequest(new BaseResponse("Disciplina não encontrada."));

    var sala = context.SalaSet.Find(aulaAtualizarDto.SalaId);
    if (sala is null)
        return Results.BadRequest(new BaseResponse("Sala não encontrada."));

    var turma = context.TurmaSet.Find(aulaAtualizarDto.TurmaId);
    if (turma is null)
        return Results.BadRequest(new BaseResponse("Turma não encontrada."));

    var professor = context.UsuarioSet.Find(aulaAtualizarDto.ProfessorId);
    if (professor is null)
        return Results.BadRequest(new BaseResponse("Professor não encontrado."));

    var curso = context.CursoSet.Find(aulaAtualizarDto.CursoId);
    if (curso is null)
        return Results.BadRequest(new BaseResponse("Curso não encontrado."));

    aula.Bloco = aulaAtualizarDto.Bloco;
    aula.DisciplinaId = aulaAtualizarDto.DisciplinaId;
    aula.SalaId = aulaAtualizarDto.SalaId;
    aula.TurmaId = aulaAtualizarDto.TurmaId;
    aula.Data = aulaAtualizarDto.Data;
    aula.ProfessorId = aulaAtualizarDto.ProfessorId;
    aula.CursoId = aulaAtualizarDto.CursoId;
    
    context.SaveChanges();

    return Results.Ok("Aula Atualizada com Sucesso!");
}).RequireAuthorization().WithTags("Aula");

app.MapDelete("aula/remover/{id:guid}", (RoomFlowContext context, Guid id) =>
{
    var aula = context.AulaSet.Find(id);
    if (aula is null)
        return Results.NotFound(new BaseResponse("Aula não encontrada."));

    context.AulaSet.Remove(aula);

    context.SaveChanges();

    return Results.Ok("Aula Removida com Sucesso!");
}).RequireAuthorization().WithTags("Aula");

app.MapPost("aula/gerador", (RoomFlowContext context, AulaAdicionarDto aulaAdicionarDto) =>
{
    var resultado = new AulaAdicionarValidatorDto().Validate(aulaAdicionarDto);

    if (!resultado.IsValid)
        return Results.BadRequest(resultado.Errors.Select(error => error.ErrorMessage));

    var sala = context.SalaSet.Find(aulaAdicionarDto.SalaId);
    if (sala is null)
        return Results.BadRequest(new BaseResponse("Sala não encontrada."));

    var turma = context.TurmaSet.Find(aulaAdicionarDto.TurmaId);
    if (turma is null)
        return Results.BadRequest(new BaseResponse("Turma não encontrada."));

    var professor = context.UsuarioSet.Find(aulaAdicionarDto.ProfessorId);
    if (professor is null)
        return Results.BadRequest(new BaseResponse("Professor não encontrado."));

    var curso = context.CursoSet.Find(aulaAdicionarDto.CursoId);
    if (curso is null)
        return Results.BadRequest(new BaseResponse("Curso não encontrado."));

    var aulas = new List<Aula>();

    var dataAtual = aulaAdicionarDto.DataInicio;

    while (dataAtual.DayOfWeek != aulaAdicionarDto.DiaSemana)
    {
        dataAtual = dataAtual.AddDays(1);
    }

    while (dataAtual <= aulaAdicionarDto.DataFim)
    {
        var aula = new Aula
        {
            Id = Guid.NewGuid(),
            DisciplinaId = aulaAdicionarDto.DisciplinaId,
            SalaId = aulaAdicionarDto.SalaId,
            TurmaId = aulaAdicionarDto.TurmaId,
            Data = dataAtual,
            ProfessorId = aulaAdicionarDto.ProfessorId,
            Bloco = aulaAdicionarDto.Bloco,
            CursoId = aulaAdicionarDto.CursoId
        };

        aulas.Add(aula);
        dataAtual = dataAtual.AddDays(7);
    }

    context.AulaSet.AddRange(aulas);
    context.SaveChanges();

    return Results.Created("Created", $"{aulas.Count} Aula(s) cadastrada(s) com sucesso!");
}).RequireAuthorization().WithTags("Aula");

#endregion

#region Usuário

app.MapGet("usuario/listar", (RoomFlowContext context) =>
{
    var usuarios = context.UsuarioSet.Select(p => new UsuarioListarDto
    {
        Id = p.Id,
        Login = p.Login,
        Nome = p.Nome,
        Perfil = p.Perfil,
        Status = p.Status
    }).AsEnumerable();

    return Results.Ok(usuarios);
}).WithTags("Usuário");

app.MapGet("usuario/listar-pendentes", (RoomFlowContext context) =>
{
    var usuarios = context.UsuarioSet.Where(p => p.Status == EnumStatusUsuario.Pendente).Select(p => new UsuarioListarDto
    {
        Id = p.Id,
        Login = p.Login,
        Nome = p.Nome,
        Perfil = p.Perfil,
        Status = p.Status
    }).AsEnumerable();

    return Results.Ok(usuarios);
}).WithTags("Usuário");

app.MapGet("usuario/listar-ativos", (RoomFlowContext context) =>
{
    var usuarios = context.UsuarioSet.Where(p => p.Status == EnumStatusUsuario.Ativo).Select(p => new UsuarioListarDto
    {
        Id = p.Id,
        Login = p.Login,
        Nome = p.Nome,
        Perfil = p.Perfil,
        Status = p.Status
    }).AsEnumerable();

    return Results.Ok(usuarios);
}).WithTags("Usuário");

app.MapGet("usuario/obter/{id:guid}", (RoomFlowContext context, Guid id) =>
{
    var usuario = context.UsuarioSet.FirstOrDefault(p => p.Id == id);
    if (usuario is null)
        return Results.BadRequest(new BaseResponse("Usuário não encontrado."));

    var usuarioDto = new UsuarioListarDto
    {
        Id = usuario.Id,
        Login = usuario.Login,
        Nome = usuario.Nome,
        Perfil = usuario.Perfil,
        Status = usuario.Status
    };

    return Results.Ok(usuarioDto);
}).WithTags("Usuário");

app.MapPost("usuario/adicionar", (RoomFlowContext context, UsuarioAdicionarDto usuarioDto) =>
{
    var resultado = new UsuarioAdicionarValidatorDto().Validate(usuarioDto);
    if (!resultado.IsValid)
        return Results.BadRequest(resultado.Errors.Select(error => error.ErrorMessage));

    var usuario = new Usuario
    {
        Nome = usuarioDto.Nome,
        Senha = usuarioDto.Senha.EncryptPassword(),
        Perfil = usuarioDto.Perfil,
        Login = usuarioDto.Login,
    };

    context.UsuarioSet.Add(usuario);
    context.SaveChanges();

    return Results.Created("Created", "Usuario Cadastrada com Sucesso!");
}).RequireAuthorization().WithTags("Usuário");

app.MapPut("usuario/atualizar", (RoomFlowContext context, UsuarioAtualizarDto usuarioDto) =>
{
    var resultado = new UsuarioAtualizarValidatorDto().Validate(usuarioDto);

    if (!resultado.IsValid)
        return Results.BadRequest(resultado.Errors.Select(error => error.ErrorMessage));

    var usuario = context.UsuarioSet.Find(usuarioDto.Id);
    if (usuario == null)
        return Results.NotFound(new BaseResponse("Usuário não encontrado."));

    usuario.Nome = usuarioDto.Nome;
    usuario.Perfil = usuarioDto.Perfil;

    context.SaveChanges();

    return Results.Ok("Usuario Atualizada com Sucesso!");
}).RequireAuthorization().WithTags("Usuário");

app.MapPut("usuario/ativar/{id:guid}", (RoomFlowContext context, Guid id) =>
{
    var usuario = context.UsuarioSet.Find(id);
    if (usuario == null)
        return Results.NotFound(new BaseResponse("Usuário não encontrado."));

    usuario.Status = EnumStatusUsuario.Ativo;

    context.SaveChanges();

    return Results.Ok("Usuário Ativado com Sucesso!");
}).RequireAuthorization().WithTags("Usuário");

app.MapPut("usuario/inativar/{id:guid}", (RoomFlowContext context, Guid id) =>
{
    var usuario = context.UsuarioSet.Find(id);
    if (usuario == null)
        return Results.NotFound(new BaseResponse("Usuário não encontrado."));

    usuario.Status = EnumStatusUsuario.Inativo;

    context.SaveChanges();

    return Results.Ok("Usuário Ativado com Sucesso!");
}).RequireAuthorization().WithTags("Usuário");

app.MapDelete("usuario/remover/{id:guid}", (RoomFlowContext context, Guid id) =>
{
    var usuario = context.UsuarioSet.Find(id);
    if (usuario is null)
        return Results.BadRequest(new BaseResponse("Usuário não encontrado."));

    context.UsuarioSet.Remove(usuario);

    context.SaveChanges();

    return Results.Ok("Usuario Removido com Sucesso!");
}).RequireAuthorization().WithTags("Usuário");

#endregion

#region Segurança

app.MapPost("autenticar", (RoomFlowContext context, LoginDto loginDto) =>
{
    var usuario = context.UsuarioSet.FirstOrDefault(u => u.Login == loginDto.Login && u.Senha == loginDto.Senha.EncryptPassword());
    if (usuario == null)
        return Results.BadRequest(new BaseResponse("Usuário ou Senha Inválidos"));

    var claims = new[]
    {
        new Claim("Id", usuario.Id.ToString()),
        new Claim("Nome", usuario.Nome),
        new Claim("Login", usuario.Login),
        new Claim("Perfil", ((int)usuario.Perfil).ToString())
    };

    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("{4ea4267e-eeae-4a10-8a05-8c237c13cb55}"));

    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

    var token = new JwtSecurityToken(
        issuer: "room.flow",
        audience: "room.flow",
        claims: claims,
        expires: DateTime.Now.AddDays(1),
        signingCredentials: creds
    );

    return Results.Ok(new JwtSecurityTokenHandler().WriteToken(token));
}).WithTags("Segurança");

app.MapPost("gerar-chave-reset-senha", (RoomFlowContext context, GerarResetSenhaDto gerarResetSenhaDto) =>
{
    var resultado = new GerarResetSenhaDtoValidator().Validate(gerarResetSenhaDto);
    if (!resultado.IsValid)
        return Results.BadRequest(resultado.Errors.Select(error => error.ErrorMessage));

    var startup = context.UsuarioSet.FirstOrDefault(p => p.Login == gerarResetSenhaDto.Email);

    if (startup is not null)
    {
        startup.ChaveResetSenha = Guid.NewGuid();
        context.UsuarioSet.Update(startup);
        context.SaveChanges();

        var emailService = new EmailService();
        var enviarEmailResponse = emailService.EnviarEmail(gerarResetSenhaDto.Email, "Reset de Senha", $"https://url-front/reset-senha/{startup.ChaveResetSenha}", true);
        if (!enviarEmailResponse.Sucesso)
            return Results.BadRequest(new BaseResponse("Erro ao enviar o e-mail: " + enviarEmailResponse.Mensagem));
    }

    return Results.Ok(new BaseResponse("Se o e-mail informado estiver correto, você receberá as instruções por e-mail."));
}).WithTags("Segurança");

app.MapPut("resetar-senha", (RoomFlowContext context, ResetSenhaDto resetSenhaDto) =>
{
    var resultado = new ResetSenhaDtoValidator().Validate(resetSenhaDto);
    if (!resultado.IsValid)
        return Results.BadRequest(resultado.Errors.Select(error => error.ErrorMessage));

    var startup = context.UsuarioSet.FirstOrDefault(p => p.ChaveResetSenha == resetSenhaDto.ChaveResetSenha);

    if (startup is null)
        return Results.BadRequest(new BaseResponse("Chave de reset de senha inválida."));

    startup.Senha = resetSenhaDto.NovaSenha.EncryptPassword();
    startup.ChaveResetSenha = null;
    context.UsuarioSet.Update(startup);
    context.SaveChanges();

    return Results.Ok(new BaseResponse("Senha alterada com sucesso."));
}).WithTags("Segurança");

app.MapPut("alterar-senha", (RoomFlowContext context, ClaimsPrincipal claims, AlterarSenhaDto alterarSenhaDto) =>
{
    var resultado = new AlterarSenhaDtoValidator().Validate(alterarSenhaDto);
    if (!resultado.IsValid)
        return Results.BadRequest(resultado.Errors.Select(error => error.ErrorMessage));

    var userIdClaim = claims.FindFirst("Id")?.Value;
    if (userIdClaim == null)
        return Results.Unauthorized();

    var userId = Guid.Parse(userIdClaim);
    var startup = context.UsuarioSet.FirstOrDefault(p => p.Id == userId);
    if (startup == null)
        return Results.NotFound(new BaseResponse("Usuário não encontrado."));

    startup.Senha = alterarSenhaDto.NovaSenha.EncryptPassword();
    context.UsuarioSet.Update(startup);
    context.SaveChanges();

    return Results.Ok(new BaseResponse("Senha alterada com sucesso."));
}).WithTags("Segurança");

#endregion

#region Signup

app.MapPost("signup", (RoomFlowContext context, SignupDto signupDto) =>
{
    var resultado = new SignupDtoValidator().Validate(signupDto);
    if (!resultado.IsValid)
        return Results.BadRequest(resultado.Errors.Select(error => error.ErrorMessage));

    var usuario = context.UsuarioSet.FirstOrDefault(u => u.Perfil == EnumPerfilUsuario.Administrador);
    if (usuario is not null)
        return Results.BadRequest(new BaseResponse("Já existe um usuário administrador cadastrado."));

    usuario = new Usuario
    {
        Id = Guid.NewGuid(),
        Nome = signupDto.Nome,
        Login = signupDto.Login,
        Senha = signupDto.Senha.EncryptPassword(),
        Perfil = EnumPerfilUsuario.Administrador,
        Status = EnumStatusUsuario.Ativo
    };

    context.UsuarioSet.Add(usuario);
    context.SaveChanges();

    return Results.Created("Created", new BaseResponse("Usuário Administrador cadastrado com sucesso!"));
}).WithTags("Signup");

#endregion

#region Mapa

app.MapGet("mapa/listar", (RoomFlowContext context) =>
{
    var salas = context.SalaSet.Select(s => new MapaSalaDto
    {
        SalaId = s.Id,
        NumeroSala = s.NumeroSala,
        Descricao = s.Descricao,
        TipoSala = s.TipoSala,
        StatusSala = s.StatusSala,
        FlagExibirNumeroSala = s.FlagExibirNumeroSala
    });

    salas.Where(p => p.StatusSala == EnumStatusSala.Ocupada).ToList().ForEach(sala =>
    {
        var aula = context.AulaSet.FirstOrDefault(a => a.SalaId == sala.SalaId && a.Data.Date == DateTime.Now.Date);

        if (aula is not null)
        {
            var disciplina = context.DisciplinaSet.FirstOrDefault(d => d.Id == aula.DisciplinaId);
            var professor = context.UsuarioSet.FirstOrDefault(u => u.Id == aula.ProfessorId);
            var turma = context.TurmaSet.FirstOrDefault(t => t.Id == aula.TurmaId);
            
            Curso? curso = null;
            if (turma is not null)
                curso = context.CursoSet.FirstOrDefault(c => c.Id == turma.CursoId);

            sala.Aula = new MapaAulaDto
            {
                Disciplina = disciplina != null ? new MapaDisciplinaDto
                {
                    Nome = disciplina.Nome,
                    Descricao = disciplina.Descricao
                } : null,
                Professor = professor != null ? new MapaProfessorDto
                {
                    Nome = professor.Nome
                } : null,
                Turma = turma != null ? new MapaTurmaDto
                {
                    Descricao = turma.Descricao
                } : null,
                Curso = curso != null ? new MapaCursoDto
                {
                    Nome = curso.Nome,
                    Periodo = curso.Periodo
                } : null
            };
        }
    });
    
    return Results.Ok(salas);
}).WithTags("Mapa");

#endregion

app.Run();