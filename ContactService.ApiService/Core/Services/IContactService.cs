
using ContactsApi.Models;

public interface IContactService
{
	Task<IEnumerable<VCardDto>> GetAllContactsAsync();
	Task<VCardDto?> GetContactByIdAsync(Guid id);
	Task<VCardDto> CreateContactAsync(CreateVCardRequest request);
	Task<VCardDto?> UpdateContactAsync(Guid id, UpdateVCardRequest request);
	Task<bool> DeleteContactAsync(Guid id);
	Task<IEnumerable<VCardDto>> SearchContactsAsync(string searchTerm);
}
