using Application.DTOs.Users;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mapping.UserProfiles
{
    public class UserSignUpProfile: Profile
    {
        public UserSignUpProfile()
        {
            CreateMap<UserSignUpDto, UserInfo>()
                .ForMember(dest => dest.Salt, opt => opt.Ignore())
                .ForMember(dest => dest.HashedPassword, opt => opt.Ignore());
        }
    }
}
