using Application.DTOs.Companies;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mapping.StaffProfiles
{
    public class CompanySignUpProfile: Profile
    {
        public CompanySignUpProfile()
        {
            CreateMap<CompanyProfileDto, Company>()
                .ForMember(dest => dest.Salt, opt => opt.Ignore())
                .ForMember(dest => dest.HashedPassword, opt => opt.Ignore());
        }
    }
}
