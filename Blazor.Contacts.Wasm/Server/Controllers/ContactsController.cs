using Blazor.Contacts.Wasm.Repositories;
using Blazor.Contacts.Wasm.Shared;
using Microsoft.AspNetCore.Mvc;

namespace Blazor.Contacts.Wasm.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private readonly IContactRepository _contactRepository;

        public ContactsController(IContactRepository contactRepository)
        {
            _contactRepository = contactRepository;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Contact contact)
        {
            if (contact == null)
            {
                return BadRequest();
            }

            if (string.IsNullOrWhiteSpace(contact.FirstName))
            {
                ModelState.AddModelError("FirstName", "First name can't be empty");
            }

            if (string.IsNullOrWhiteSpace(contact.LastName))
            {
                ModelState.AddModelError("LastName", "Last name can't be empty");
            }

            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _contactRepository.InsertContact(contact);

            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromRoute] int id, [FromBody] Contact contact)
        {
            if (contact == null)
            {
                return BadRequest();
            }

            if (string.IsNullOrWhiteSpace(contact.FirstName))
            {
                ModelState.AddModelError("FirstName", "First name can't be empty");
            }

            if (string.IsNullOrWhiteSpace(contact.LastName))
            {
                ModelState.AddModelError("LastName", "Last name can't be empty");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _contactRepository.UpdateContact(contact);

            return NoContent();
        }

        [HttpGet]
        public async Task<IEnumerable<Contact>> Get()
        {
            return await _contactRepository.GetAllContacts();
        }

        [HttpGet("{id}")]
        public async Task<Contact> Get([FromRoute] int id)
        {
            return await _contactRepository.GetContactById(id);
        }

        [HttpDelete("{id}")]
        public async Task Delete([FromRoute] int id)
        {
            await _contactRepository.DeleteContact(id);
        }
    }
}
