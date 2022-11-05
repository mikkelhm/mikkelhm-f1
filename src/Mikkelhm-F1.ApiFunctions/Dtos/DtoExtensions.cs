using Mikkelhm_F1.Domain;

namespace Mikkelhm_F1.ApiFunctions.Dtos;

public static class DtoExtensions
{
    public static MetaInformationDto ToDto(this MetaInformation domain)
    {
        return new MetaInformationDto()
        {
            CreatedDateUtc = domain.CreatedDateUtc
        };
    }

    public static WikipediaInformationDto ToDto(this WikipediaInformation domain)
    {
        return new WikipediaInformationDto()
        {
            Link = domain.Link
        };
    }

    public static LocationDto ToDto(this Location domain)
    {
        return new LocationDto()
        {
            Country = domain.Country,
            Latitude = domain.Latitude,
            Longitude = domain.Longitude,
            Locality = domain.Locality
        };
    }

    public static SeasonDto ToDto(this Season domain)
    {
        return new SeasonDto()
        {
            Year = domain.Year,
            WikipediaInformation = domain.WikipediaInformation.ToDto(),
            MetaInformation = domain.MetaInformation.ToDto()
        };
    }

    public static RaceDto ToDto(this Race domain)
    {
        return new RaceDto()
        {
            Year = domain.Year,
            CircuitId = domain.CircuitId,
            RaceDate = domain.RaceDate,
            WikipediaInformation = domain.WikipediaInformation.ToDto(),
            MetaInformation = domain.MetaInformation.ToDto()
        };
    }

    public static DriverDto ToDto(this Driver domain)
    {
        return new DriverDto()
        {
            DriverId = domain.DriverId,
            GivenName = domain.GivenName,
            FamilyName = domain.FamilyName,
            DateOfBirth = domain.DateOfBirth,
            Nationality = domain.Nationality,
            WikipediaInformation = domain.WikipediaInformation.ToDto(),
            MetaInformation = domain.MetaInformation.ToDto()
        };
    }

    public static CircuitDto ToDto(this Circuit domain)
    {
        return new CircuitDto()
        {
            CircuitId = domain.CircuitId,
            CircuitName = domain.CircuitName,
            Location = domain.Location.ToDto(),
            WikipediaInformation = domain.WikipediaInformation.ToDto(),
            MetaInformation = domain.MetaInformation.ToDto()
        };
    }
}