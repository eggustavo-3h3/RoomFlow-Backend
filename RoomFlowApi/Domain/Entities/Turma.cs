namespace RoomFlowApi.Domain.Entities
{
    public class Turma
    {
        public Guid Id { get; set; }
        public  string Descricao { get; set; } = string.Empty;
        public Guid CursoId { get; set; }

        #region Propriedades de Navegabilidade

        public Curso? Curso { get; set; }

        #endregion
    }
}
