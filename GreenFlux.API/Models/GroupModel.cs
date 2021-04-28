using AutoMapper;
using GreenFlux.Model;
using System;
using System.ComponentModel.DataAnnotations;

namespace GreenFlux.API.Models
{
    public class GroupModel
    {

        public long Id { get; set; }

        [Required]
        [StringLength(300)]
        public string Name { get; set; }

        [Range(float.Epsilon, float.PositiveInfinity)]
        public float Capacity { get; set; }

    }


    public class GroupProfile : Profile
    {

        public GroupProfile()
        {
            CreateMap<Group, GroupModel>()
               .ReverseMap();
        }


    }

}
