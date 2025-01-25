using FlightManagement.BusinessLogic.BL;
using FlightManagement.BusinessLogic.BL.DTOs;
using FlightManagement.DataAccessLayer.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FlightManagment.Gateway.Controllers
{
    /// <summary>
    /// Users Controller
    /// </summary>
    /// <param name="userBL"></param>
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController(UserBL userBL) : ControllerBase
    {

        /// <summary>
        /// Gets Users
        /// </summary>
        /// <returns></returns>
        [HttpGet("Get")]
        public async Task<IActionResult> Get()
        {
            List<User> users = await userBL.Get();
            return Ok(users);
        }

        /// <summary>
        /// Creates a new User
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPut("Create")]
        public async Task<IActionResult> Create(UserDTOs.CreateUser dto)
        {

            Guid id = await userBL.Create(dto);
            return Ok(id);
        }
        /// <summary>
        /// Updates a user
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>

        [HttpPost("Update")]
        public async Task<IActionResult> Update(UserDTOs.UpdateUser dto)
        {
            Guid id = await userBL.Update(dto);
            return Ok(id);
        }

        /// <summary>
        /// Deletes a user
        /// </summary>
        /// <param name="Id">User's Id</param>
        /// <returns></returns>
        [HttpDelete("Delete/{Id}")]
        public async Task<IActionResult> Delete(Guid Id)
        {
            await userBL.Delete(Id);
            return Ok("User has been Deleted");
        }
    }
}
