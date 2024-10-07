namespace Backend.Mapper;
using AutoMapper;
using Backend.Models;
public class IntervalProfile : Profile
{
    public IntervalProfile()
    {
        CreateMap<Interval, IntervalDTO>();
        CreateMap<IntervalDTO, Interval>();
        // CreateMap<Interval,IntervalJsonDTO>()
        //     .ForMember(dto => dto., opt => opt.MapFrom(i => i.IntervalNo))
        //     .ForMember(dto => dto.record_type, opt => opt.MapFrom(i => i.RecordType))
        //     .ForMember(dto => dto.scale_no, opt => opt.MapFrom(i => i.ScaleNo))
        //     .ForMember(dto => dto.interval_name, opt => opt.MapFrom(i => i.IntervalName))
        //     .ForMember(dto => dto.type, opt => opt.MapFrom(i => i.RecordType))
        //     .ForMember(dto => dto.color, opt => opt.MapFrom(i => i.Color))
        //     .ForMember(dto => dto.t_age, opt => opt.MapFrom(i => i.StartMYA))
        //     .ForMember(dto => dto.b_age, opt => opt.MapFrom(i => i.EndMYA));
    }
}