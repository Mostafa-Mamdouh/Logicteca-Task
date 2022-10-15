using AutoMapper;
using Core.Entities;
using System.Data;

namespace API.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            #region Client
            CreateMap<DataRow, Product>()
                .ForMember(d => d.PartSKU, o => o.MapFrom(s => s["Part SKU"]))
                .ForMember(d => d.Band, o => o.MapFrom(s => s["Band #"]))
                .ForMember(d => d.Manufacturer, o => o.MapFrom(s => s["Manufacturer"]))
                .ForMember(d => d.CategoryCode, o => o.MapFrom(s => s["Category Code"]))
                .ForMember(d => d.ListPrice, o => o.MapFrom(s => s["List Price"]))
                .ForMember(d => d.MinimumDiscount, o => o.MapFrom(s => s["Minimum Discount"]))
                .ForMember(d => d.DiscountedPrice, o => o.MapFrom(s => s["Discounted Price"]));

           
            #endregion
   
        }
    }
}