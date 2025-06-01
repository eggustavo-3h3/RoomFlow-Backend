using RoomFlowApi.Domain.Enumerators;

namespace RoomFlowApi.Domain.DTOs.Mapa;

public class MapaSalaDto
{
    public Guid SalaId { get; set; }
    public int NumeroSala { get; set; }
    public string Descricao { get; set; } = string.Empty;
    public EnumStatusSala StatusSala { get; set; }
    public EnumTipoSala TipoSala { get; set; }
    public bool FlagExibirNumeroSala { get; set; }
    public MapaAulaDto? Aula { get; set; } = null;
}

public class MapaAulaDto
{
    public MapaDisciplinaDto? Disciplina { get; set; }
    public MapaCursoDto? Curso { get; set; }
    public MapaTurmaDto? Turma { get; set; }
    public MapaProfessorDto? Professor { get; set; }
    public DateTime Data { get; set; }
    public DayOfWeek DiaSemana { get; set; }
    public DateTime DataInicio { get; set; }
    public DateTime DataFim { get; set; }
    public EnumBloco Bloco { get; set; }
}

public class MapaDisciplinaDto
{
    public string Nome { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
}

public class MapaCursoDto
{
    public string Nome { get; set; } = string.Empty;
    public EnumPeriodo Periodo { get; set; }
}

public class MapaTurmaDto
{
    public string Descricao { get; set; } = string.Empty;
}

public class MapaProfessorDto
{
    public string Nome { get; set; } = string.Empty;
}