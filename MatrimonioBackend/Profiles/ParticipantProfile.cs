using AutoMapper;
using MatrimonioBackend.DTOs.Participant;
using MatrimonioBackend.DTOs.User;
using MatrimonioBackend.Models;

namespace MatrimonioBackend.Profiles
{
    public class ParticipantProfile : Profile
    {

        public ParticipantProfile()
        {
            CreateMap<ParticipantUpdateDTO, Participant>();
            CreateMap<Participant, ParticipantReadDTO>().ReverseMap();
        }

    }
}
