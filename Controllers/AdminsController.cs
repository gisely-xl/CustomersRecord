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
    [Route("api/[controller]")]
    [ApiController]
    public class AdminsControllers : ControllerBase
    {
        private readonly IAdminRepository _adminRepository;
        private readonly TokenService _tokenService;
       
        public AdminsControllers(IAdminRepository adminRepository, TokenService tokenService)
        {
            _adminRepository = adminRepository;
            _tokenService = tokenService;
        }

        // GET api/<AdminControllers>/5
        /// <summary>
        /// Generate a token for Admin
        /// </summary>
        /// <remarks>
        /// Example:
        /// 
        ///     name = Gisely
        ///     password = 123xl
        ///     
        /// </remarks>
        /// <param name="name"></param>
        /// <param name="password"></param>
        /// <response code = '200'>Success</response>
        /// <response code = '400'>Bad Request: Exception catched</response>
        /// <response code = '404'>Not found: Admin not found</response>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet("{name}")]
        [ProducesResponseType(typeof(Admin), 200)]
        [ProducesErrorResponseType(typeof(BadRequestResult))]
        public IActionResult Get(string name, string password)
        {
            try
            {
              
                var admin = _adminRepository.GetAdmin(name, password);
               
                //checks if admin exists
                if (admin == null)
                {

                    return NotFound(new { message = $"Admin '{name}' not found" });
                }


                //cover the password
                var pass = admin.Password.Length;
                admin.Password = "";

                for (int i = 0; i < pass; i++)
                {
                    admin.Password += "*";
                }
                var token = _tokenService.GenerateToken(admin);
                //return token and admin
                return Accepted(new
                {
                    admin = admin,
                    token = token
                }); ;
            }
            catch (Exception ex)
            {
               
                return BadRequest(ex.Message);
            }

        }
    }
}

