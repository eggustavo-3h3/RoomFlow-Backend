using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using RoomFlowApi.Configurations;
using RoomFlowApi.Context;
using RoomFlowApi.Domain;
using RoomFlowApi.Domain.Base;
using RoomFlowApi.Domain.DTO.Aula;
using RoomFlowApi.Domain.DTO.Curso;
using RoomFlowApi.Domain.DTO.Disciplina;
using RoomFlowApi.Domain.DTO.Login;
using RoomFlowApi.Domain.DTO.Sala;
using RoomFlowApi.Domain.DTO.Turma;
using RoomFlowApi.Domain.DTO.Usuario;
using RoomFlowApi.Domain.Util;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;



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
builder.Services.AddAuthorization();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthentication();
app.UseAuthorization();

app.UseHttpsRedirection();

app.UseCors(x => x
    .AllowAnyHeader()
    .AllowAnyMethod()
    .AllowAnyOrigin()
    );
#region Controller Sala
app.MapPost("sala/adicionar", (RoomFlowContext context, SalaAdicionarDTO SalaAdicionarDTO) =>
{
    var sala = new Sala
    {
        Id = Guid.NewGuid(),
        Descricao = SalaAdicionarDTO.Descricao,
        StatusSala = SalaAdicionarDTO.StatusSala,
        TipoSala = SalaAdicionarDTO.TipoSala,
        NumeroSala = SalaAdicionarDTO.NumeroSala
    };

    context.SalaSet.Add(sala);
    context.SaveChanges();
    return Results.Created("Created", "Sala Cadastrada com Sucesso!");
    
})
    .RequireAuthorization()
    .WithTags("Sala");

app.MapGet("sala/listar", (RoomFlowContext context) =>
{
    var listasalaDto = context.SalaSet.Select(p => new SalaListarDTO
    {
        Id = p.Id,
        Descricao = p.Descricao,
        StatusSala = p.StatusSala,
        TipoSala = p.TipoSala,
        NumeroSala = p.NumeroSala
    }).AsEnumerable();

    return Results.Ok(listasalaDto);
})
    .WithTags("Sala");


app.MapPut("sala/atualizar", (RoomFlowContext context, SalaAtualizarDTO salaDto) =>
{
    var sala = context.SalaSet.Find(salaDto.Id);
    if (sala is null)
        return Results.BadRequest(new BaseResponse("Sala não Encontrada"));

    sala.Descricao = salaDto.Descricao;
    sala.StatusSala = salaDto.StatusSala;
    sala.TipoSala = salaDto.TipoSala;
    sala.NumeroSala = salaDto.NumeroSala;
    context.SaveChanges();

    return Results.Ok(new BaseResponse("Sala Atualizada com Sucesso!"));
})
       .RequireAuthorization()
    .WithTags("Sala");

app.MapDelete("sala/remover/{id:guid}", (RoomFlowContext context, Guid id) =>
{
    var sala = context.SalaSet.Find(id);
    if (sala is null)
        return Results.BadRequest(new BaseResponse("Sala não Encontrada"));

    context.SalaSet.Remove(sala);
    context.SaveChanges();

    return Results.Ok(new BaseResponse("Sala Removida com Sucesso!"));
    
})
       .RequireAuthorization()
    .WithTags("Sala");

app.MapGet("sala/listar/{id:guid}", (RoomFlowContext context, Guid id ) =>
{
    var sala = context.SalaSet.Find(id);

    if (sala is null)
        return Results.BadRequest(new BaseResponse("Sala não Encontrada"));

    var salaDto = new SalaListarDTO
    {
        Id = sala.Id,
        Descricao = sala.Descricao,
        StatusSala = sala.StatusSala,
        TipoSala = sala.TipoSala,
        NumeroSala = sala.NumeroSala,
    };

    return Results.Ok(salaDto);

}).RequireAuthorization()
    .WithTags("Sala");

#endregion

#region controller disciplina
app.MapPost("disciplina/adicionar", (RoomFlowContext context, DisciplinaAdcionarDTO disciplinaDto) =>
{
    var disciplina = new Disciplina
    {
        Id = Guid.NewGuid(),
        Nome = disciplinaDto.Nome,
        Descricao = disciplinaDto.Descricao,
    };

    context.DisciplinaSet.Add(disciplina);
    context.SaveChanges();

    return Results.Created("Created", "Disciplina Cadastrada com Sucesso!");
})
    .RequireAuthorization()
    .WithTags("Disciplina");

app.MapGet("disciplina/listar", (RoomFlowContext context) =>
{
   var listaDisciplina = context.DisciplinaSet
        .Select(p => new 
        {
            Id = p.Id,
            Nome = p.Nome,
            Descricao = p.Descricao,
        })
        .AsEnumerable();


    return Results.Ok(listaDisciplina);
})
    .RequireAuthorization()
    .WithTags("Disciplina");

app.MapPut("disciplina/atualizar", (RoomFlowContext context, DisciplinaAtualizarDTO disciplinaDto) =>
    {
        var disciplina = context.DisciplinaSet.Find(disciplinaDto.Id);
        if (disciplina is null)
            return Results.BadRequest(new BaseResponse("Disciplina não Encontrada"));

        disciplina.Nome = disciplinaDto.Nome;
        disciplina.Descricao = disciplinaDto.Descricao;
        context.SaveChanges();
        return Results.Ok("Disciplina Atualizada com Sucesso!");
    })
    .RequireAuthorization()
    .WithTags("Disciplina");


app.MapDelete("disciplina/remover/{id:guid}", (RoomFlowContext context, Guid id) =>
    {
        var disciplina = context.DisciplinaSet.Find(id);
        context.DisciplinaSet.Remove(disciplina);
        context.SaveChanges();
        return Results.Ok("Disciplina Removida com Sucesso!");
    })
    .RequireAuthorization()
    .WithTags("Disciplina");

app.MapGet("disciplina/listar/{id:guid}", (RoomFlowContext context, Guid id) =>
{
    var disciplina = context.DisciplinaSet.Find(id);

    if(disciplina is null)
    {
        return Results.BadRequest(new BaseResponse("Disciplina não encontrada"));
    }

    var disciplinaDto = new DisciplinaListarDTO
    {
        Id = disciplina.Id,
        Nome = disciplina.Nome,
        Descricao = disciplina.Descricao,
    };

    return Results.Ok(disciplinaDto);

}).RequireAuthorization()
    .WithTags("Disciplina");



#endregion

#region Controller Turma
app.MapPost("turma/adicionar", (RoomFlowContext context, TurmaAdicionarDTO turmaDto) =>
{
    var turma = new Turma
    {
        Id = Guid.NewGuid(),
        Descricao = turmaDto.Descricao,
        CursoId = turmaDto.CursoId,
    };

    context.TurmaSet.Add(turma);

    context.SaveChanges();
    return Results.Created("Created", "Turma Cadastrada com Sucesso!");
})
    .RequireAuthorization()
    .WithTags("Turma");

app.MapGet("turma/listar", (RoomFlowContext context) =>
{
    var listaturma = context.TurmaSet.Select(p => new
    {
        Id = p.Id,
        Descricao = p.Descricao,
        Curso = p.Curso,
    })
    .AsEnumerable();
    return Results.Ok(listaturma.AsEnumerable());
})
    .RequireAuthorization()
    .WithTags("Turma");

app.MapPut("turma/atualizar", (RoomFlowContext context, TurmaAtualizarDTO turmaDto) =>
{
    var turma = context.TurmaSet.Find(turmaDto.Id);
    if (turma is null)
        return Results.BadRequest(new BaseResponse("Turma não Encontrada"));

    turma.Descricao = turmaDto.Descricao;
    turma.CursoId = turmaDto.CursoId;


    context.SaveChanges();
    return Results.Ok("Turma Atualizada com Sucesso!");
})
    .RequireAuthorization()
    .WithTags("Turma");

app.MapDelete("turma/remover/{id:guid}", (RoomFlowContext context, Guid id) =>
{
    var turma = context.TurmaSet.Find(id);
    context.TurmaSet.Remove(turma);
    context.SaveChanges();
    return Results.Ok("Turma Removida com Sucesso!");
})
    .RequireAuthorization()
    .WithTags("Turma");

app.MapGet("turma/listar/{id:guid}", (RoomFlowContext context, Guid id) =>
{
    var turma = context.TurmaSet.Find(id);

    if (turma is null)
    {
        return Results.BadRequest(new BaseResponse("Turma não encontrada"));
    }


    var turmaDto = new TurmaListarDTO
    {
        Id = turma.Id,
        Descricao = turma.Descricao,
        CursoId = turma.CursoId,
    };

    return Results.Ok(turmaDto);

}).RequireAuthorization()
    .WithTags("Turma");

#endregion

#region controller cursos
app.MapPost("curso/adicionar", (RoomFlowContext context, CursoAdicionarDTO cursoDto) =>
{
    var curso = new Curso
    {
        Id = Guid.NewGuid(),
        Nome = cursoDto.Nome,
        Periodo = cursoDto.Periodo,
    };

    context.CursoSet.Add(curso);
    context.SaveChanges();
    return Results.Created("Created", "Curso Cadastrada com Sucesso!");
})
    .RequireAuthorization()
    .WithTags("Curso");

app.MapGet("curso/listar", (RoomFlowContext context) =>
{
    var listacurso = context.CursoSet.Select(p => new
    {
        Id = p.Id,
        Nome = p.Nome,
        Periodo = p.Periodo,
    }).AsEnumerable();

    return Results.Ok(listacurso.AsEnumerable());
})
    .RequireAuthorization()
    .WithTags("Curso");

app.MapPut("curso/atualizar", (RoomFlowContext context, CursoAtualizarDTO cursoDto) =>
    {
        var curso = context.CursoSet.Find(cursoDto.Id);
        if (curso is null)
            return Results.BadRequest(new BaseResponse("Curso não Encontrado"));
        curso.Nome = cursoDto.Nome;
        curso.Periodo = cursoDto.Periodo;
        context.SaveChanges();
        return Results.Ok("Curso Atualizado com Sucesso!");
    }).RequireAuthorization()
    .WithTags("Curso");

app.MapDelete("curso/remover/{id:guid}", (RoomFlowContext context, Guid id) =>
    {
        var curso = context.CursoSet.Find(id);
        context.CursoSet.Remove(curso);
        context.SaveChanges();
        return Results.Ok("Curso Removido com Sucesso!");
    })
    .RequireAuthorization()
    .WithTags("Curso");

app.MapGet("curso/listar/{id:guid}", (RoomFlowContext context , Guid id) =>
{
    var curso = context.CursoSet.Find(id);

    if(curso is null)
    {
        return Results.BadRequest(new BaseResponse("Curso não encontrado"));
    }


    var cursoDto = new CursoListarDTO
    {
        Id = curso.Id,
        Nome = curso.Nome,
        Periodo = curso.Periodo
    };

    return Results.Ok(cursoDto);

}).RequireAuthorization()
    .WithTags("Curso");

#endregion

#region controller usuario
app.MapPost("usuario/adicionar", (RoomFlowContext context, UsuarioAdicionarDTO usuarioDto) =>
{
    var usuario = new Usuario
    {
        Nome = usuarioDto.Nome,
        Senha = usuarioDto.Senha.ToMD5(),
        Perfil = usuarioDto.Perfil,
        Login = usuarioDto.Login,
        Status = usuarioDto.Status,
    };

    context.UsuarioSet.Add(usuario);
    context.SaveChanges();
    return Results.Created("Created", "Usuario Cadastrada com Sucesso!");
})
    .RequireAuthorization()
    .WithTags("Usuário");

app.MapGet("usuario/listar", (RoomFlowContext context) =>
{
    var listaUsuarios = context.UsuarioSet
        .Select(p => new
        {
            Id = p.Id,
            Login = p.Login,
            Nome = p.Nome,
            Perfil = p.Perfil,
            Status = p.Status
        })
        .ToList();

    return Results.Ok(listaUsuarios);
})
.RequireAuthorization()
.WithTags("Usuário");

app.MapGet("usuario/listar-ativos", (RoomFlowContext context) =>
{
    var ListaUsuariosAtivos = context.UsuarioSet
        .Where(p => ((int)p.Status) == 1) 
        .Select(p => new
        {
            Id = p.Id,
            Login = p.Login,
            Nome = p.Nome,
            Perfil = p.Perfil,
        }).ToList();
    return Results.Ok(ListaUsuariosAtivos.AsEnumerable());
})
.RequireAuthorization()
.WithTags("Usuário");

app.MapPut("usuario/atualizar", (RoomFlowContext context, UsuarioAtualizarDTO usuarioDto) =>
{
    var usuario = context.UsuarioSet.Find(usuarioDto.Id);
    if (usuario is null)
        return Results.BadRequest(new BaseResponse("Usuario não Encontrado"));
    usuario.Nome = usuarioDto.Nome;
    usuario.Senha = usuarioDto.Senha;
    usuario.Perfil = usuarioDto.Perfil;
    usuario.Status = usuarioDto.Status;
    context.SaveChanges();
    return Results.Ok("Usuario Atualizada com Sucesso!");
})
    .RequireAuthorization()
    .WithTags("Usuário");

app.MapDelete("usuario/remover/{id:guid}", (RoomFlowContext context, Guid id) =>
    {

        var usuario = context.UsuarioSet.Find(id);
        context.UsuarioSet.Remove(usuario);
        context.SaveChanges();
        return Results.Ok("Usuario Removido com Sucesso!");
    })
    .RequireAuthorization()
    .WithTags("Usuário");

app.MapGet("usuario/listar/{id:guid}", (RoomFlowContext context, Guid id) =>
{
    var usuario = context.UsuarioSet.Find(id);

    if (usuario is null)
    {
        return Results.BadRequest(new BaseResponse("Usuário não encontrado"));
    }


    var usuarioDto = new UsuarioListarDTO
    {
        Id = usuario.Id,
        Nome = usuario.Nome,
        Login = usuario.Login,
        Senha = usuario.Senha,
        Perfil = usuario.Perfil,
        Status = usuario.Status,
    };

    return Results.Ok(usuarioDto);

}).RequireAuthorization()
    .WithTags("Usuário");

#endregion

#region controller aula
app.MapPost("aula/adicionar", async (RoomFlowContext context, AulaAdicionarDto aulaDto) =>
{
    var aulaExistente = await context.AulaSet.FirstOrDefaultAsync(a =>
        a.SalaId == aulaDto.SalaId &&
        a.Bloco == aulaDto.Bloco && 
        a.Data.Date == aulaDto.Data.Date); 

    if (aulaExistente != null)
    {
        return Results.Conflict("Já existe uma aula cadastrada para a mesma sala, bloco e data.");
    }

    var aula = new Aula
    {
        Id = Guid.NewGuid(),
        DisciplinaId = aulaDto.DisciplinaId,
        SalaId = aulaDto.SalaId,
        TurmaId = aulaDto.TurmaId,
        Data = aulaDto.Data,
        ProfessorId = aulaDto.ProfessorId,
        Bloco = aulaDto.Bloco 
    };

    context.AulaSet.Add(aula);
    await context.SaveChangesAsync(); 

    return Results.Created($"/aula/{aula.Disciplina}", "Aula Cadastrada com Sucesso!");
})
.RequireAuthorization()
.WithTags("Aula");

app.MapGet("aula/listar", (RoomFlowContext context) =>
{
    var listaula = context.AulaSet.Select(p => new
    {
        Id = p.Id,
        UsuarioNome = p.Usuario.Nome,
        DisciplinaNome = p.Disciplina.Nome,
        SalaNome = p.Sala.Descricao,
        TurmaDescricao = p.Turma.Descricao,
        Data = p.Data,
        
    }).ToList();
    return Results.Ok(listaula.AsEnumerable());

})
    
    .WithTags("Aula");

app.MapPut("aula/atualizar", (RoomFlowContext context, AulaAtualizarDto aulaDto) =>
    {
        var aula = context.AulaSet.Find(aulaDto.Id);

        if (aula is null)
            return Results.BadRequest(new BaseResponse("Aula não Encontrada"));

        aula.DisciplinaId = aulaDto.DisciplinaId;
        aula.SalaId = aulaDto.SalaId;
        aula.TurmaId = aulaDto.TurmaId;
        aula.Data = aulaDto.Data;
        aula.ProfessorId = aulaDto.professorId;
        context.SaveChanges();
        return Results.Ok("Aula Atualizada com Sucesso!");
    })
    .RequireAuthorization()
    .WithTags("Aula");

app.MapDelete("aula/remover/{id:guid}", (RoomFlowContext context, Guid id) =>
    {
        
        var aula = context.AulaSet.Find(id);
        context.AulaSet.Remove(aula);
        context.SaveChanges();
        return Results.Ok("Aula Removida com Sucesso!");
    })
    .RequireAuthorization()
    .WithTags("Aula");

app.MapPost("aula/gerador", (RoomFlowContext context, AulaAdicionarDto aulaDto) =>

{
    var aulas = new List<Aula>();

    DateTime dataAtual = aulaDto.DataInicio;

    while (dataAtual.DayOfWeek != aulaDto.DiaSemana)
    {
        dataAtual = dataAtual.AddDays(1);
    }

    while(dataAtual <= aulaDto.DataFim)
    {
        var aula = new Aula
        {
            Id = Guid.NewGuid(),
            DisciplinaId = aulaDto.DisciplinaId,
            SalaId = aulaDto.SalaId,
            TurmaId = aulaDto.TurmaId,
            Data = dataAtual,
            ProfessorId = aulaDto.ProfessorId,
            Bloco = aulaDto.Bloco,
            CursoId = aulaDto.CursoId
            

        };

        aulas.Add(aula);
        dataAtual = dataAtual.AddDays(7);

    }
    context.AulaSet.AddRange(aulas);
    context.SaveChanges();

    return Results.Created("Created", $"{aulas.Count} Aula(s) cadastrada(s) com sucesso!");


})
    .WithTags("Aula");

app.MapGet("aula/listar/{id:guid}", (RoomFlowContext context, Guid id) =>
{
    var aula = context.AulaSet.Find(id);

    if (aula is null)
    {
        return Results.BadRequest(new BaseResponse("Aula não encontrada"));
    }


    var aulaDto = context.AulaSet.Select(p => new
    {
        Id = p.Id,
        UsuarioNome = p.Usuario.Nome,
        DisciplinaNome = p.Disciplina.Nome,
        SalaNome = p.Sala.Descricao,
        TurmaDescricao = p.Turma.Descricao,
        Data = p.Data,

    }).ToList();
    return Results.Ok(aulaDto.AsEnumerable());

    return Results.Ok(aulaDto);

}).RequireAuthorization()
    .WithTags("Aula");


#endregion

#region Controller Autenticar
app.MapPost
    ("autenticar", (RoomFlowContext context,
                    LoginDto loginDto) =>
    {

        var usuario = context.UsuarioSet.Where(u => u.Login == loginDto.Login && u.Senha == loginDto.Senha.ToMD5()).FirstOrDefault();

        if (usuario != null)
        {
            var claims = new[]
            {
                new Claim("Nome", usuario.Nome),
                new Claim("Login", usuario.Login),
                new Claim("Perfil", usuario.Perfil.ToString()),
            };

            //Recebe uma instância da Classe
            //   SymmetricSecurityKey armazenando a
            //   chave de criptografia
            //   usada na criação do Token
            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes("{4ea4267e-eeae-4a10-8a05-8c237c13cb55}")
            );

            //Recebe um objeto do tipo SigninCredentials
            //  contendo a chave de criptografia e o algoritimo
            //  de seguran�a empregados na geração de assinaturas
            //  digitais para tokens
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "room.flow",
                audience: "room.flow",
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds
            );

            return Results.Ok(
                new JwtSecurityTokenHandler()
                .WriteToken(token));
        }

        return Results.BadRequest(new BaseResponse("Usuário ou Senha Inválidos"));
    });

#endregion

#region Controller AlterarSenha
app.MapPut("usuario/alterarsenha", async (RoomFlowContext context, AlterarSenhaDTO alterarSenhaDto) =>
{
    if (alterarSenhaDto.NovaSenha != alterarSenhaDto.ConfirmarNovaSenha )
    {
        return Results.BadRequest(new { mensagem = "A nova senha e a confirmação não coincidem." });
    }
    
    var usuario = await context.UsuarioSet.FindAsync(alterarSenhaDto.Login);

    if (usuario is null)
    {
        return Results.NotFound(new { mensagem = "Usuário não encontrado." });
    }
    if (usuario.Senha != alterarSenhaDto.Senha.ToMD5())
    {
        return Results.BadRequest(new { mensagem = "Senha incorreta." });
    }
    var senha = alterarSenhaDto.NovaSenha.ToMD5();
    usuario.Senha = senha;
    await context.SaveChangesAsync();
    return Results.Ok(new { mensagem = "Senha alterada com sucesso." });


})
    .RequireAuthorization()
    .WithTags("Usuário");



#endregion

app.Run();