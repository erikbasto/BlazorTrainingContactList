using Blazor.Contacts.Wasm.Shared;
using Dapper;
using System.Data;

namespace Blazor.Contacts.Wasm.Repositories
{
    public class ContactRepository : IContactRepository
    {
        private readonly IDbConnection _connection;

        public ContactRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task DeleteContact(int id)
        {
            var sql = @"DELETE FROM Contacts WHERE Id = @Id";
            var result =  await _connection.ExecuteAsync(sql, new { Id = id });
        }

        public async Task<IEnumerable<Contact>> GetAllContacts()
        {
            var sql = @"SELECT Id,FirstName,LastName,Phone,Address FROM Contacts";
            return await _connection.QueryAsync<Contact>(sql, new { });
        }

        public async Task<Contact> GetContactById(int id)
        {
            var sql = @"SELECT Id,FirstName,LastName,Phone,Address FROM Contacts WHERE Id = @Id";
            return await _connection.QueryFirstOrDefaultAsync<Contact>(sql, new { Id = id });
        }

        public async Task<bool> InsertContact(Contact contact)
        {
            try
            {
                var sql = @"INSERT INTO Contacts(FirstName, LastName, Phone, Address) VALUES (@FirstName, @LastName, @Phone, @Address)";
                var result = await _connection.ExecuteAsync(sql, new {
                    contact.FirstName,
                    contact.LastName,
                    contact.Phone,
                    contact.Address
                });

                return result > 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> UpdateContact(Contact contact)
        {
            try
            {
                var sql = @"UPDATE Contacts SET FirstName=@FirstName, LastName= @LastName, Phone=@Phone, Address= @Address WHERE Id = @Id";
                var result = await _connection.ExecuteAsync(sql, new
                {
                    contact.FirstName,
                    contact.LastName,
                    contact.Phone,
                    contact.Address,
                    contact.Id
                });

                return result > 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
