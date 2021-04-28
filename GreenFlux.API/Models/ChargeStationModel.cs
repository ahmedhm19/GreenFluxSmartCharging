using AutoMapper;
using GreenFlux.Model;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace GreenFlux.API.Models
{
    public class ChargeStationModel
    {

        public long Id { get; set; }

        public long GroupId { get; set; }

        [Required]
        [StringLength(300)]
        public string Name { get; set; }

        public float MaxCurrentSum
        { 
            get
            {
                return Connectors.Sum(c => c.MaxCurrent);
            }
        }

        public List<ConnectorModel> Connectors { get; set; } = new List<ConnectorModel>();

    }




    public class ChargeStationProfile : Profile
    {

        public ChargeStationProfile()
        {
            CreateMap<ChargeStation, ChargeStationModel>()
               .ReverseMap()
               .ForMember(e => e.Connectors, e => e.Ignore());//Don't map Connectors when mapping from ChargeStationModel to ChargeStation

        }


    }




}
