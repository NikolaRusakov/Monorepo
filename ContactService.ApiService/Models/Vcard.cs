using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContactsApi.Models;

public class VCard
{
	[Key]
	public Guid Id { get; set; } = Guid.NewGuid();

	public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
	public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

	// Language
	public Language? Language { get; set; }

	// Organization
	public Organization? Organization { get; set; }

	// Address
	public Address? Address { get; set; }

	// Telephones (one-to-many relationship)
	public List<Telephone> Telephones { get; set; } = new();

	// Email
	public Email? Email { get; set; }

	// Geography
	public Geography? Geography { get; set; }

	// Public Key
	public PublicKey? PublicKey { get; set; }

	// Timezone
	public string? Timezone { get; set; }

	// URL
	public Url? Url { get; set; }
}

[ComplexType]
public class Language
{
	public string? Value { get; set; }
	public int? Preference { get; set; }
}

[ComplexType]
public class Organization
{
	public string? Name { get; set; }
	public string? Type { get; set; }
}

[ComplexType]
public class Address
{
	public string? Type { get; set; }
	public string? PoBox { get; set; }
	public string? ExtendedAddress { get; set; }
	public string? StreetAddress { get; set; }
	public string? Locality { get; set; }
	public string? Region { get; set; }
	public string? PostalCode { get; set; }
	public string? Country { get; set; }
}

public class Telephone
{
	[Key]
	public Guid Id { get; set; } = Guid.NewGuid();

	[ForeignKey("VCard")]
	public Guid VCardId { get; set; }

	public VCard VCard { get; set; } = null!;

	public string? Value { get; set; }
	public string? ValueType { get; set; }
	public string? Extension { get; set; }
	public int? Preference { get; set; }

	// Store types as JSON string for simplicity
	public string? Types { get; set; }

	[NotMapped]
	public List<string> TypeList
	{
		get =>
			string.IsNullOrEmpty(Types)
				? new List<string>()
				: System.Text.Json.JsonSerializer.Deserialize<List<string>>(Types)
				?? new List<string>();
		set => Types = System.Text.Json.JsonSerializer.Serialize(value);
	}
}

[ComplexType]
public class Email
{
	public string? Type { get; set; }
	public string? Address { get; set; }
}

[ComplexType]
public class Geography
{
	public string? Type { get; set; }
	public double? Latitude { get; set; }
	public double? Longitude { get; set; }
}

[ComplexType]
public class PublicKey
{
	public string? Type { get; set; }
	public string? ValueType { get; set; }
	public string? Uri { get; set; }
}

[ComplexType]
public class Url
{
	public string? Type { get; set; }
	public string? Address { get; set; }
}

// DTOs for API
public record VCardDto(
	Guid? Id,
	LanguageDto? Language,
	OrganizationDto? Organization,
	AddressDto? Address,
	List<TelephoneDto>? Telephones,
	EmailDto? Email,
	GeographyDto? Geography,
	PublicKeyDto? PublicKey,
	string? Timezone,
	UrlDto? Url
);

public record LanguageDto(string? Value, int? Preference);

public record OrganizationDto(string? Name, string? Type);

public record AddressDto(
	string? Type,
	string? PoBox,
	string? ExtendedAddress,
	string? StreetAddress,
	string? Locality,
	string? Region,
	string? PostalCode,
	string? Country
);

public record TelephoneDto(
	Guid? Id,
	string? Value,
	string? ValueType,
	List<string>? Type,
	int? Preference,
	string? Extension
);

public record EmailDto(string? Type, string? Address);

public record GeographyDto(string? Type, double? Latitude, double? Longitude);

public record PublicKeyDto(string? Type, string? ValueType, string? Uri);

public record UrlDto(string? Type, string? Address);

public record CreateVCardRequest(
	LanguageDto? Language,
	OrganizationDto? Organization,
	AddressDto? Address,
	List<TelephoneDto>? Telephones,
	EmailDto? Email,
	GeographyDto? Geography,
	PublicKeyDto? PublicKey,
	string? Timezone,
	UrlDto? Url
);

public record UpdateVCardRequest(
	LanguageDto? Language,
	OrganizationDto? Organization,
	AddressDto? Address,
	List<TelephoneDto>? Telephones,
	EmailDto? Email,
	GeographyDto? Geography,
	PublicKeyDto? PublicKey,
	string? Timezone,
	UrlDto? Url
);
