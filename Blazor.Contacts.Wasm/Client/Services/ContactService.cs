﻿using Blazor.Contacts.Wasm.Shared;
using System.Net.Http.Json;

namespace Blazor.Contacts.Wasm.Client.Services
{
    public class ContactService : IContactService
    {
        private readonly HttpClient _httpClient;

        public ContactService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task DeleteContact(int id)
        {
            await _httpClient.DeleteAsync($"api/contacts/{id}");
        }

        public async Task<IEnumerable<Contact>> GetAll()
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<Contact>>("api/contacts");
        }

        public async Task<Contact> GetContact(int id)
        {
            return await _httpClient.GetFromJsonAsync<Contact>($"api/contacts/{id}");
        }

        public async Task SaveContact(Contact contact)
        {
            if(contact.Id == 0)
            {
                await _httpClient.PostAsJsonAsync<Contact>($"api/contacts", contact);
            }
            else
            {
                await _httpClient.PutAsJsonAsync<Contact>($"api/contacts/{contact.Id}", contact);
            }
        }
    }
}
