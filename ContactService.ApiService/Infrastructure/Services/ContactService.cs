using ContactsApi.Data;
using ContactsApi.Models;
using Microsoft.EntityFrameworkCore;

namespace ContactsApi.Services;

public class ContactServiceImpl : IContactService
{
	private readonly ContactsDbContext _context;

	public ContactServiceImpl(ContactsDbContext context)
	{
		_context = context;
	}

	public async Task<IEnumerable<VCardDto>> GetAllContactsAsync()
	{
		var contacts = await _context.VCards.Include(v => v.Telephones).ToListAsync();

		return contacts.Select(MapToDto);
	}

	public async Task<VCardDto?> GetContactByIdAsync(Guid id)
	{
		var contact = await _context
			.VCards.Include(v => v.Telephones)
			.FirstOrDefaultAsync(v => v.Id == id);

		return contact == null ? null : MapToDto(contact);
	}

	public async Task<VCardDto> CreateContactAsync(CreateVCardRequest request)
	{
		var vcard = new VCard
		{
			Language =
				request.Language != null
					? new Language
					{
						Value = request.Language.Value,
						Preference = request.Language.Preference,
					}
					: null,

			Organization =
				request.Organization != null
					? new Organization
					{
						Name = request.Organization.Name,
						Type = request.Organization.Type,
					}
					: null,

			Address =
				request.Address != null
					? new Address
					{
						Type = request.Address.Type,
						PoBox = request.Address.PoBox,
						ExtendedAddress = request.Address.ExtendedAddress,
						StreetAddress = request.Address.StreetAddress,
						Locality = request.Address.Locality,
						Region = request.Address.Region,
						PostalCode = request.Address.PostalCode,
						Country = request.Address.Country,
					}
					: null,

			Email =
				request.Email != null
					? new Email { Type = request.Email.Type, Address = request.Email.Address }
					: null,

			Geography =
				request.Geography != null
					? new Geography
					{
						Type = request.Geography.Type,
						Latitude = request.Geography.Latitude,
						Longitude = request.Geography.Longitude,
					}
					: null,

			PublicKey =
				request.PublicKey != null
					? new PublicKey
					{
						Type = request.PublicKey.Type,
						ValueType = request.PublicKey.ValueType,
						Uri = request.PublicKey.Uri,
					}
					: null,

			Timezone = request.Timezone,

			Url =
				request.Url != null
					? new Url { Type = request.Url.Type, Address = request.Url.Address }
					: null,
		};

		_context.VCards.Add(vcard);
		await _context.SaveChangesAsync();

		// Add telephones if provided
		if (request.Telephones?.Any() == true)
		{
			foreach (var telephoneDto in request.Telephones)
			{
				var telephone = new Telephone
				{
					VCardId = vcard.Id,
					Value = telephoneDto.Value,
					ValueType = telephoneDto.ValueType,
					Extension = telephoneDto.Extension,
					Preference = telephoneDto.Preference,
					TypeList = telephoneDto.Type ?? new List<string>(),
				};

				_context.Telephones.Add(telephone);
			}

			await _context.SaveChangesAsync();
		}

		// Reload with telephones
		var createdContact = await _context
			.VCards.Include(v => v.Telephones)
			.FirstAsync(v => v.Id == vcard.Id);

		return MapToDto(createdContact);
	}

	public async Task<VCardDto?> UpdateContactAsync(Guid id, UpdateVCardRequest request)
	{
		var vcard = await _context
			.VCards.Include(v => v.Telephones)
			.FirstOrDefaultAsync(v => v.Id == id);

		if (vcard == null)
			return null;

		// Update properties
		vcard.Language =
			request.Language != null
				? new Language
				{
					Value = request.Language.Value,
					Preference = request.Language.Preference,
				}
				: null;

		vcard.Organization =
			request.Organization != null
				? new Organization
				{
					Name = request.Organization.Name,
					Type = request.Organization.Type,
				}
				: null;

		vcard.Address =
			request.Address != null
				? new Address
				{
					Type = request.Address.Type,
					PoBox = request.Address.PoBox,
					ExtendedAddress = request.Address.ExtendedAddress,
					StreetAddress = request.Address.StreetAddress,
					Locality = request.Address.Locality,
					Region = request.Address.Region,
					PostalCode = request.Address.PostalCode,
					Country = request.Address.Country,
				}
				: null;

		vcard.Email =
			request.Email != null
				? new Email { Type = request.Email.Type, Address = request.Email.Address }
				: null;

		vcard.Geography =
			request.Geography != null
				? new Geography
				{
					Type = request.Geography.Type,
					Latitude = request.Geography.Latitude,
					Longitude = request.Geography.Longitude,
				}
				: null;

		vcard.PublicKey =
			request.PublicKey != null
				? new PublicKey
				{
					Type = request.PublicKey.Type,
					ValueType = request.PublicKey.ValueType,
					Uri = request.PublicKey.Uri,
				}
				: null;

		vcard.Timezone = request.Timezone;

		vcard.Url =
			request.Url != null
				? new Url { Type = request.Url.Type, Address = request.Url.Address }
				: null;

		vcard.UpdatedAt = DateTime.UtcNow;

		// Update telephones - remove existing and add new ones
		_context.Telephones.RemoveRange(vcard.Telephones);

		if (request.Telephones?.Any() == true)
		{
			foreach (var telephoneDto in request.Telephones)
			{
				var telephone = new Telephone
				{
					VCardId = vcard.Id,
					Value = telephoneDto.Value,
					ValueType = telephoneDto.ValueType,
					Extension = telephoneDto.Extension,
					Preference = telephoneDto.Preference,
					TypeList = telephoneDto.Type ?? new List<string>(),
				};

				vcard.Telephones.Add(telephone);
			}
		}

		await _context.SaveChangesAsync();

		return MapToDto(vcard);
	}

	public async Task<bool> DeleteContactAsync(Guid id)
	{
		var vcard = await _context.VCards.FindAsync(id);

		if (vcard == null)
			return false;

		_context.VCards.Remove(vcard);
		await _context.SaveChangesAsync();

		return true;
	}

	public async Task<IEnumerable<VCardDto>> SearchContactsAsync(string searchTerm)
	{
		var query = _context.VCards.Include(v => v.Telephones).AsQueryable();

		if (!string.IsNullOrWhiteSpace(searchTerm))
		{
			query = query.Where(v =>
				(v.Organization != null && v.Organization.Name!.Contains(searchTerm))
				|| (v.Email != null && v.Email.Address!.Contains(searchTerm))
				|| (
					v.Address != null
					&& (
						v.Address.StreetAddress!.Contains(searchTerm)
						|| v.Address.Locality!.Contains(searchTerm)
						|| v.Address.Country!.Contains(searchTerm)
					)
				)
				|| v.Telephones.Any(t => t.Value!.Contains(searchTerm))
			);
		}

		var contacts = await query.ToListAsync();
		return contacts.Select(MapToDto);
	}

	private static VCardDto MapToDto(VCard vcard)
	{
		return new VCardDto(
			vcard.Id,
			vcard.Language != null
				? new LanguageDto(vcard.Language.Value, vcard.Language.Preference)
				: null,
			vcard.Organization != null
				? new OrganizationDto(vcard.Organization.Name, vcard.Organization.Type)
				: null,
			vcard.Address != null
				? new AddressDto(
					vcard.Address.Type,
					vcard.Address.PoBox,
					vcard.Address.ExtendedAddress,
					vcard.Address.StreetAddress,
					vcard.Address.Locality,
					vcard.Address.Region,
					vcard.Address.PostalCode,
					vcard.Address.Country
				)
				: null,
			vcard
				.Telephones.Select(t => new TelephoneDto(
					t.Id,
					t.Value,
					t.ValueType,
					t.TypeList,
					t.Preference,
					t.Extension
				))
				.ToList(),
			vcard.Email != null ? new EmailDto(vcard.Email.Type, vcard.Email.Address) : null,
			vcard.Geography != null
				? new GeographyDto(
					vcard.Geography.Type,
					vcard.Geography.Latitude,
					vcard.Geography.Longitude
				)
				: null,
			vcard.PublicKey != null
				? new PublicKeyDto(
					vcard.PublicKey.Type,
					vcard.PublicKey.ValueType,
					vcard.PublicKey.Uri
				)
				: null,
			vcard.Timezone,
			vcard.Url != null ? new UrlDto(vcard.Url.Type, vcard.Url.Address) : null
		);
	}
}
