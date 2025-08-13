using AutoMapper;
using OsintCommand.API.Dtos;
using OsintCommand.API.Entities;

namespace OsintCommand.API.Mappings
{
    public class AutoMapperProfiles: Profile
    {
        public AutoMapperProfiles() 
        {

            CreateMap<FakeAccount, FakeAccountDto>().ReverseMap();
            CreateMap<FakeAccount, FakeAccountCreateDto>().ReverseMap();
            CreateMap<FakeAccount, FakeAccountUpdateDto>().ReverseMap().ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<FakeAccount, FakeAccountsImportResultDto>().ReverseMap();
            CreateMap<FakeAccount, ImportFakeAccountDto>().ReverseMap();

            CreateMap<Group, GroupDto>().ReverseMap();
            CreateMap<Friend, FriendDto>().ReverseMap();

            CreateMap<Script, ScriptDto>().ReverseMap();
            CreateMap<Script, ScriptCreateDto>().ReverseMap();
            CreateMap<Script, ScriptUpdateDto>().ReverseMap();
            CreateMap<Script, ScriptDetailDto>().ReverseMap();


            CreateMap<Entities.Action, ActionDto>().ReverseMap();
            CreateMap<Entities.Action, ActionAddRequestDto>().ReverseMap();
            CreateMap<Entities.Action, ActionUpdateRequestDto>().ReverseMap();
        }
    }
}
