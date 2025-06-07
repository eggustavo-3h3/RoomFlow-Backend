using RoomFlowApi.Domain.Enumerators;

namespace RoomFlowApi.Domain.DTOs.Mapa;

public class MapaFiltroDto
{
    public DateOnly Data { get; set; }
    public EnumBloco Bloco { get; set; }
}