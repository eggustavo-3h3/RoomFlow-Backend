﻿namespace RoomFlowApi.Domain.DTO.Disciplina
{
    public class DisciplinaAtualizarDTO
    {
        public Guid Id   { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
    }
}

