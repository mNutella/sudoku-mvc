using AutoMapper;
using Sudoku.Mvc.Data.Entity;
using Sudoku.Mvc.Data.Model.OutputDto.GameBoardService;

namespace Sudoku.Mvc.Api.Automapper
{
    public class AutomapperProfiles : Profile
    {
        public AutomapperProfiles()
        {
            CreateMap<Board, BoardDataOutput>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.FilledBoard, opt => opt.MapFrom(src => src.FilledBoard))
                .ForMember(dest => dest.EmptyBoard, opt => opt.MapFrom(src => src.EmptyBoard));
        }
    }
}
