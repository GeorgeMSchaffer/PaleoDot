namespace Backend.Mapper;
using AutoMapper;
using Backend.Models;
using Backend.Models.DTOs;
public class OccurrenceProfile : Profile
{
    public OccurrenceProfile()
    {
        CreateMap<Occurrence, OccurrenceDTO>();
        // CreateMap<OccurrenceDTO, Occurrence>();
        //
        // CreateMap<Occurrence, OccurrenceJsonDTO>().ForMember(occ => occ.accepted_name, opt => opt.MapFrom(dto => dto.AcceptedName))
        //     .ForMember(dto => dto.accepted_no, opt => opt.MapFrom(o => o.AcceptedNo))
        //     .ForMember(dto => dto.accepted_rank, opt => opt.MapFrom(o => o.AcceptedRank))
        //     .ForMember(dto => dto.cc, opt => opt.MapFrom(o => o.Cc))
        //     .ForMember(dto => dto.class_name, opt => opt.MapFrom(o => o.Class))
        //     .ForMember(dto => dto.collection_no, opt => opt.MapFrom(o => o.CollectionNo))
        //     .ForMember(dto => dto.early_interval, opt => opt.MapFrom(o => o.EarlyInterval))
        //     .ForMember(dto => dto.late_interval, opt => opt.MapFrom(o => o.LateInterval))
        //     .ForMember(dto => dto.family, opt => opt.MapFrom(o => o.Family))
        //     .ForMember(dto => dto.genus, opt => opt.MapFrom(o => o.Genus))
        //     .ForMember(dto => dto.geogscale, opt => opt.MapFrom(dto => dto.Geogscale))
        //     .ForMember(dto => dto.identified_name, opt => opt.MapFrom(o => o.IdentifiedName))
        //     .ForMember(dto => dto.identified_no, opt => opt.MapFrom(o => o.IdentifiedNo))
        //     .ForMember(dto => dto.identified_rank, opt => opt.MapFrom(o => o.IdentifiedRank))
        //     .ForMember(dto => dto.latlng_basis, opt => opt.MapFrom(o => o.LatlngBasis))
        //     .ForMember(dto => dto.latlng_precision, opt => opt.MapFrom(o => o.LatlngPrecision))
        //     .ForMember(dto => dto.min_ma, opt => opt.MapFrom(o => o.MinMya))
        //     .ForMember(dto => dto.max_ma, opt => opt.MapFrom(o => o.MaxMya))
        //     .ForMember(dto => dto.order, opt => opt.MapFrom(o => o.Order))
        //     .ForMember(dto => dto.phylum, opt => opt.MapFrom(o => o.Phylum))
        //     .ForMember(dto => dto.record_type, opt => opt.MapFrom(o => o.RecordType))
        //     .ForMember(dto => dto.reference_no, opt => opt.MapFrom(o => o.ReferenceNo));
    }
}