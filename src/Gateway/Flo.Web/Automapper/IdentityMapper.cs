using AutoMapper;
using Flo.Proto;
using Flo.Web.DTOs.Identity;

namespace Flo.Web.Automapper;

public class IdentityMapper : Profile
{
    public  IdentityMapper()
    {
        CreateMap<LoginRequestDto, LoginRequest>();
    }
}