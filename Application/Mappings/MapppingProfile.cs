using Application.DTO;
using Application.DTO.Payment;
using AutoMapper;
using Domain.Entities;
using static Application.DTO.Auth;
public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Map ProductDto to Product
        CreateMap<ProductDto, Product>()
            .ForMember(dest => dest.ProductVariants, opt => opt.MapFrom(src => src.ProductVariants));

        // Map ProductVariantDto to ProductVariant
        //CreateMap<ProductVariantDto, ProductVariant>()
        //    //.ForMember(dest => dest.ProductVariantSizes, opt => opt.MapFrom(src => src.ProductVariantSizes))
        //    .ForMember(dest => dest.ProductVariantSizes, opt => opt.MapFrom(src => src.ProductVariantSizes))
        //    .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.Image)); // No conversion here

        // Map ProductVariantSizeDto to ProductVariantSize
        CreateMap<ProductVariantSizeDto, ProductVariantSize>()
            .ForMember(dest => dest.SizeId, opt => opt.MapFrom(src => src.SizeId))
            .ForMember(dest => dest.ProductVariantId, opt => opt.MapFrom(src => src.ProductVariantId));

        // Map Product to ProductDto
        CreateMap<Product, ProductDto>()
            .ForMember(dest => dest.ProductVariants, opt => opt.MapFrom(src => src.ProductVariants));

        // Map ProductVariant to ProductVariantDto
        //CreateMap<ProductVariant, ProductVariantDto>()
        //    .ForMember(dest => dest.ProductVariantSizes, opt => opt.MapFrom(src => src.ProductVariantSizes))
        //    .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.Image)); // No conversion here

        // Map ProductVariantSize to ProductVariantSizeDto
        CreateMap<ProductVariantSize, ProductVariantSizeDto>()
            .ForMember(dest => dest.SizeId, opt => opt.MapFrom(src => src.SizeId))
            .ForMember(dest => dest.ProductVariantId, opt => opt.MapFrom(src => src.ProductVariantId));

        // Map Size to SizeDto and vice versa
        CreateMap<Size, SizeDto>().ReverseMap();

        // Map Size to SizeDto and vice versa
        CreateMap<RazorpayPaymentLinkResponse, RazorpayPaymentLinkResponseDTO>().ReverseMap();

        // ReverseMap for other entities
        CreateMap<CustomerDto, Customer>().ReverseMap();
        CreateMap<BrandDto, Brand>().ReverseMap();
        CreateMap<CategoryDto, Category>().ReverseMap();



        // Map ProductDto to Product
        CreateMap<ProductDto, Product>().ReverseMap();

        // Map ProductVariantDto to ProductVariant
        CreateMap<ProductVariantDto, ProductVariant>().ReverseMap();

        // Map ProductVariantSizeDto to ProductVariantSize
        CreateMap<ProductVariantSizeDto, ProductVariantSize>()
            .ForMember(dest => dest.Size, opt => opt.MapFrom(src => src.Size))
            .ReverseMap();

        // Map SizeDto to Size and vice versa
        CreateMap<SizeDto, Size>().ReverseMap();


    }
}



