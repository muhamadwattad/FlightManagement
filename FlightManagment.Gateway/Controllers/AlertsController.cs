using FlightManagement.BusinessLogic.BL;
using FlightManagement.BusinessLogic.BL.DTOs;
using FlightManagement.DataAccessLayer.Entities;
using Microsoft.AspNetCore.Mvc;

namespace FlightManagment.Gateway.Controllers
{
    /// <summary>
    /// Alerts Controller
    /// </summary>
    /// <param name="alertBL"></param>
    [Route("api/[controller]")]
    [ApiController]
    public class AlertsController(AlertBL alertBL) : ControllerBase
    {
        /// <summary>
        /// Gets alrets by UserId
        /// </summary>
        /// <param name="UserId">User's Id</param>
        /// <returns></returns>
        [HttpGet("Get/{UserId}")]
        public async Task<IActionResult> Get(Guid UserId)
        {
            List<Alert> alerts = await alertBL.Get(UserId);
            return Ok(alerts);
        }

        /// <summary>
        /// Creates a new Alert for a user
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPut("Create")]
        public async Task<IActionResult> Create(AlertDTOs.Create dto)
        {
            Guid alertId = await alertBL.CreateAlert(dto);
            return Ok(alertId);
        }

        /// <summary>
        /// Updates an alert for a user
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost("Update")]
        public async Task<IActionResult> Update(AlertDTOs.Update dto)
        {
            Guid alertId = await alertBL.Update(dto);
            return Ok(alertId);
        }

        /// <summary>
        /// Deletes an alert by its Id
        /// </summary>
        /// <param name="Id">Alert's Id</param>
        /// <returns></returns>
        [HttpDelete("Delete/{Id}")]
        public async Task<IActionResult> Delete(Guid Id)
        {
            await alertBL.Delete(Id);
            return Ok("Alert has been deleted!");
        }
    }
}
