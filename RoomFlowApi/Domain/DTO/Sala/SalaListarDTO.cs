﻿using RoomFlowApi.Enumerators;

namespace RoomFlowApi.Domain.DTO.Sala
{
    public class SalaListarDTO
    {
        public Guid Id { get; set; }
        public string Descricao { get; set; } = string.Empty;

        public EnumStatusSala statusSala { get; set; }
    }
}
