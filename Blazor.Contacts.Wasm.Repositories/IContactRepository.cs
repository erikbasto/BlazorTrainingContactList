using Blazor.Contacts.Wasm.Shared;

namespace Blazor.Contacts.Wasm.Repositories
{
    public interface IContactRepository
    {
        Task<bool> InsertContact(Contact contact);
        Task<bool> UpdateContact(Contact contact);
        Task DeleteContact(int id);
        Task<IEnumerable<Contact>> GetAllContacts();
        Task<Contact> GetContactById(int id);
    }
}
