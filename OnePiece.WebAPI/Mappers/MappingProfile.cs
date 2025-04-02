using System.Text.Json;
using AutoMapper;
using OnePiece.Core.Entities;
using OnePiece.WebAPI.Models;

namespace OnePiece.WebAPI.Mappers;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Ánh xạ từ Entity sang DTO
        CreateMap<HuntRequest,TreasureHunt>().ForMember(dest => dest.MatrixJson,
            opt => opt.MapFrom<MatrixToJsonResolver>());

        // Ánh xạ từ DTO sang Entity
        // CreateMap<TreasureHuntDto, TreasureHuntEntity>();
    }

    private class MatrixToJsonResolver : IValueResolver<HuntRequest, TreasureHunt, string>
    {
        public string Resolve(HuntRequest source, TreasureHunt destination, string destMember, ResolutionContext context)
        {
            return JsonSerializer.Serialize(source.Matrix); // Serialize outside expression tree
        }
    }
}