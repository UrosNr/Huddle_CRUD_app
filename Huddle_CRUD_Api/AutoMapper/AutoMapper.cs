using AutoMapper;
using Huddle_CRUD.Core.Models;
using Huddle_CRUD.Core.ViewModels;

namespace Huddle_CRUD_Api.AutoMapper
{
    public class AutoMapper:Profile
    {
        public AutoMapper()
        {
            CreateMap<TeamModel, TeamVm>()
                .ForMember(d => d.InitialName, opt => opt.MapFrom(s => s.Name));
        }
    }
}
