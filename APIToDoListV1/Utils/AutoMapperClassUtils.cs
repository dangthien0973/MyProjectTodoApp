
using APIToDoListV1.Entities;
using AutoMapper;
using Model;
using Model.UserModel;

namespace APIToDoListV1.Utils
{
    public class AutoMapperClassUtils : Profile
    {
        public AutoMapperClassUtils()
        {
            CreateMap<Todo,TodoDto>();
            CreateMap<User, UserDTO>();
        }
    }
}
