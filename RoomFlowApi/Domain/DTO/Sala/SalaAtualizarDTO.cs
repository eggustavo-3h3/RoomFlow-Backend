﻿using RoomFlowApi.Enumerators;

namespace RoomFlowApi.Domain.DTO.Sala
{
    public class SalaAtualizarDTO
    {
        public Guid Id { get; set; }
        public string Descricao { get; set; } = string.Empty;

        public EnumStatusSala StatusSala { get; set; }
    }
}
