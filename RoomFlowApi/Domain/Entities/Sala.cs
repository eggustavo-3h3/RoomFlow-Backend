﻿using RoomFlowApi.Domain.Enumerators;

namespace RoomFlowApi.Domain.Entities
{
    public class Sala
    {
        public Guid Id { get; set; }
        public string? Descricao { get; set; } = string.Empty;

        public EnumStatusSala StatusSala {  get; set; }
        
        public EnumTipoSala TipoSala { get; set; }
        public int? NumeroSala { get; set; }

    }
}
