using AutoMapper;
using EmployeeCRUD.Models.DTO;
using EmployeeCRUD.Models.Entities;

namespace EmployeeCRUD.Mappings
{
    public class AutoMapperProfiles: Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Employee,EmployeesDTO>().ReverseMap();
            CreateMap<EmployeesDTO, Employee>().ReverseMap();
            CreateMap<AddEmployeesDTO, Employee>().ReverseMap();
            CreateMap<Employee, AddEmployeesDTO>().ReverseMap();

            CreateMap<UpdateEmployeesDTO, Employee>().ReverseMap();
            //CreateMap<Employee, UpdateEmployeesDTO>().ReverseMap();
        }
    }
}
