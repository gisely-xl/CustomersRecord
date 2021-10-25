using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CustomersRec.APIrest.Data;
using CustomersRec.APIrest.Models;
using CustomersRec.APIrest.Models.RepositoryInterface;
using CustomersRec.APIrest.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;

namespace CustomersRec.APIrest.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IClientRepository _clientRepository;
        private readonly RecordContext _context;

        public ClientController(IClientRepository clientRepository, RecordContext context)
        {
            _clientRepository = clientRepository;
            _context = context;
        }

        // GET: api/<ClientController>
        /// <summary>
        /// List all clients on database.
        /// </summary>
        /// <response code = '200'>Success</response>
        /// <response code = '400'>Bad Resquest: Exception catched</response>
        /// <response code = '404'>Not found: Clients not found</response>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesErrorResponseType(typeof(BadRequestObjectResult))]
        public IActionResult Get()
        {
            try
            {
                var clients = _clientRepository.GetClients();
                if (clients == null)
                    return NotFound("Theres no Clients registered");
                return Ok(clients);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET api/<ClientController>/5
        /// <summary>
        /// Show a client by Id
        /// </summary>
        /// <remarks>
        /// Example:
        ///     Id = 1005
        /// </remarks>
        /// <param name="id">Client Id</param>
        /// <response code = '200'>Success</response>
        /// <response code = '400'>Bad Request: Exception catched</response>
        /// <response code = '404'>Not found: Client of this Id not found</response>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Client), 200)]
        [ProducesErrorResponseType(typeof(BadRequestObjectResult))]
        public IActionResult Get(int id)
        {
            try
            {
                var client = _clientRepository.GetClient(id);

                if (client == null)
                    return NotFound(new { meassage = $"Client {id} not found" });

                return Ok(_clientRepository.GetClient(id));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // POST api/<ClientController>
        /// <summary>
        /// Create a new client.
        /// </summary>
        /// <remarks>
        /// Example:
        ///
        ///     POST 
        ///     {
        ///        "name": "Gisely",
        ///        "contact": "88996464849",
        ///        "cep": "62031-222"
        ///      } 
        /// </remarks>
        /// <param name="client">Client to be created</param>
        /// <response code = '201'>Success</response>
        /// <response code = '400'>Bad Request: Read the message</response>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(Client), 200)]
        [ProducesErrorResponseType(typeof(BadRequestObjectResult))]
        public async Task<IActionResult> Post([FromBody] ClientDb client)
        {
            try
            {
                _context.Clients.Add(client);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(Get), new { id = client.Id }, client);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // PUT api/<ClientController>/5
        /// <summary>
        /// Update client
        /// </summary>
        /// <remarks>
        /// Example:
        ///
        ///     PUT
        ///     {  
        ///        "id": 2010
        ///        "name": "GISELY",
        ///        "contact": "88996464849",
        ///        "cep": "60010-040"
        ///      } 
        /// </remarks>
        /// <param name="id">Client Id</param>
        /// <param name="client"> Client body to Update</param>
        /// <response code = '200'> Succes</response>
        /// <response code = '400'> Bad Request: Exception catched </response>
        /// <response code = '404'> Not found: Client Id not found</response>
        /// <returns>Client updated</returns>
        [HttpPut]
        [ProducesResponseType(typeof(ClientDb), 200)]
        [ProducesErrorResponseType(typeof(BadRequestObjectResult))]
        public async Task<IActionResult> Put(int id, [FromBody] ClientDb client)
        {
            try
            {
                if (id != client.Id)
                    return BadRequest();

                _context.Entry(client).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HeroExists(client.Id))
                    {
                        return NotFound($"'{client.Name}' not found");
                    }
                    else
                    {
                        throw;
                    }
                }

                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        // DELETE api/<ClientController>/5
        /// <summary>
        /// Delete client by id
        /// </summary>
        /// <param name="id"> to Delete</param>
        /// <response code = '204'>Succes</response>
        /// <response code = '400'>Bad Request: Exception catched</response>
        /// <response code = '404'>Not found: Client not found to delete</response>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesErrorResponseType(typeof(BadRequestObjectResult))]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var client = await _context.Clients.FindAsync(id);
                if (client == null)
                {
                    return NotFound($"'{client.Name}' not found");
                }

                _context.Clients.Remove(client);
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }
        private bool HeroExists(int id)
        {
            return _context.Clients.Any(e => e.Id == id);
        }
    }
}

