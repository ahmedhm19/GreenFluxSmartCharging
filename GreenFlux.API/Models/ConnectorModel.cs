using AutoMapper;
using GreenFlux.Model;
using System;
using System.ComponentModel.DataAnnotations;

namespace GreenFlux.API.Models
{
    public class ConnectorModel
    {

        public byte CK_Id { get; set; }
        public long ChargeStationId { get; set; }

        [Range(float.Epsilon, float.PositiveInfinity)]
        public float MaxCurrent { get; set; }

    }


    public class ConnectorProfile : Profile
    {

        public ConnectorProfile()
        {
            CreateMap<Connector, ConnectorModel>().ReverseMap();
        }


    }



}
