using AutoMapper;
using People.Domain.Users.Entities;
using People.UseCases.Common.Dtos.Position;
using People.UseCases.Positions.Common.Dtos;

namespace People.UseCases.Positions.Common;

/// <summary>
/// Mapping profile for position entity.
/// </summary>
public class PositionMappingProfile : Profile
{
    /// <summary>
    /// Mapping profile constructor.
    /// </summary>
    public PositionMappingProfile()
    {
        CreateMap<Position, PositionWithRelationsDto>();
        CreateMap<Position, PositionDto>();
        CreateMap<PositionWithRelationsDto, PositionDto>();
    }
}
