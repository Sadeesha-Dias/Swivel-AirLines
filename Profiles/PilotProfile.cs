using AutoMapper;
using Swivel_AirLines.DTO.IncomingDTO;
using Swivel_AirLines.Models;

namespace Swivel_AirLines.Profiles
{
    public class PilotProfile : Profile
    {
        public PilotProfile() 
        {
            //Referencing the mapping from the DTO to the entity / model.
            //Source :- PilotIncomingDto | Destination :- Pilots

            CreateMap<PilotIncomingDto, Pilots>()
                .ForMember(
                    destination => destination.Id,
                    option => option.MapFrom(src => Guid.NewGuid())) // --> Auto generating the Id for pilot and auto mapping it to the Pilots model.

                .ForMember(
                    destination => destination.FirstName,
                    option => option.MapFrom(src => src.FirstName))

                .ForMember(
                    destination => destination.LastName,
                    option => option.MapFrom(src => src.LastName))

                .ForMember(
                    destination => destination.PilotLisenceNumber,
                    option => option.MapFrom(src => src.PilotLisenceNumber))

                .ForMember(
                    destination => destination.Status,
                    option => option.MapFrom(src => 1))

                .ForMember(
                    destination => destination.FlyingHours,
                    option => option.MapFrom(src => src.FlyingHours))

                .ForMember(
                    destination => destination.DateCreated,
                    option => option.MapFrom(src => DateTime.UtcNow))

                .ForMember(
                    destination => destination.DateUpdated,
                    option => option.MapFrom(src => DateTime.UtcNow));


        }
    }
}
