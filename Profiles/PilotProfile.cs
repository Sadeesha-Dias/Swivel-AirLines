using AutoMapper;
using Swivel_AirLines.DTO.IncomingDTO;
using Swivel_AirLines.DTO.OutgoingDTO;
using Swivel_AirLines.Models;

namespace Swivel_AirLines.Profiles
{
    public class PilotProfile : Profile
    {
        public PilotProfile() 
        {
            #region Incoming request DTO mapping
            //Referencing the mapping from the DTO to the entity / model.
            //Source :- PilotIncomingDto | Destination :- Pilots

            CreateMap<PilotIncomingDto, Pilots>()
                .ForMember(
                    destination => destination.Id,
                    option => option.MapFrom(src => Guid.NewGuid())) // --> Auto generating the Id for pilot and auto mapping it to the Pilots model.

                .ForMember(
                    destination => destination.FirstName,
                    option => option.MapFrom(src => src.FirstName)) // mapping First Name

                .ForMember(
                    destination => destination.LastName,
                    option => option.MapFrom(src => src.LastName)) // mapping Last Name

                .ForMember(
                    destination => destination.PilotLisenceNumber,
                    option => option.MapFrom(src => src.PilotLisenceNumber)) // mapping PilotLisenceNumber

                .ForMember(
                    destination => destination.Status,
                    option => option.MapFrom(src => 1)) // mapping Status

                .ForMember(
                    destination => destination.FlyingHours,
                    option => option.MapFrom(src => src.FlyingHours)) // mapping FlyingHours

                .ForMember(
                    destination => destination.DateCreated,
                    option => option.MapFrom(src => DateTime.UtcNow)) // mapping DateCreated

                .ForMember(
                    destination => destination.DateUpdated,
                    option => option.MapFrom(src => DateTime.UtcNow)); // mapping DateUpdated
            #endregion

            #region Outgoing response DTO mapping
            //Referencing the mapping from the entity / model to DTO.
            //Source :- Pilots | Destination :- PilotOutgoingDto
            CreateMap<Pilots, PilotOutgoingDto>()
                .ForMember(
                    dest => dest.Id,
                    opt => opt.MapFrom(c => c.Id)) // mapping "Id"

                .ForMember(
                    dest => dest.FullName,
                    opt => opt.MapFrom(c => $"{c.FirstName} {c.LastName}")) // concatinating & mapping "FirstName" and "LastName" into "FullName"

                .ForMember(
                    dest => dest.PilotLisenceNumber,
                    opt => opt.MapFrom(c => c.PilotLisenceNumber)) // mapping PilotLisenceNumber

                .ForMember(
                    dest => dest.FlyingHours,
                    opt => opt.MapFrom(c => c.FlyingHours)); // mapping FlyingHours

            #endregion


        }
    }
}
