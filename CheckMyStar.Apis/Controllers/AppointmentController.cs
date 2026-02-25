using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using CheckMyStar.Apis.Services.Abstractions;
using CheckMyStar.Bll.Requests;

namespace CheckMyStar.Apis.Controllers
{
    /// <summary>
    /// Defines API endpoints for appointment operations.
    /// </summary>
    /// <param name="appointmentService">Appointment service</param>
    [ApiController]
    [Route("api/[controller]")]
    public class AppointmentController(IAppointmentService appointmentService) : ControllerBase
    {
        /// <summary>
        /// Retrieves the next available identifier for a new appointment.
        /// </summary>
        /// <param name="ct">A cancellation token that can be used to cancel the operation.</param>
        /// <returns>An <see cref="IActionResult"/> containing the next available appointment identifier.</returns>
        [HttpGet("getnextidentifier")]
        [Authorize(Roles = "Administrator, Inspector")]
        public async Task<IActionResult> GetNextIdentifier(CancellationToken ct)
        {
            var appointment = await appointmentService.GetNextIdentifier(ct);

            return Ok(appointment);
        }

        /// <summary>
        /// Retrieves the appointment associated with a specific folder.
        /// </summary>
        /// <param name="request">The request containing the folder identifier.</param>
        /// <param name="ct">A cancellation token that can be used to cancel the operation.</param>
        /// <returns>An <see cref="IActionResult"/> containing the appointment details.</returns>
        [HttpGet("getappointmentbyfolder")]
        [Authorize(Roles = "Administrator, Inspector")]
        public async Task<IActionResult> GetAppointmentByFolder([FromQuery] AppointmentGetByFolderRequest request, CancellationToken ct)
        {
            var appointment = await appointmentService.GetAppointmentByFolder(request, ct);

            return Ok(appointment);
        }

        /// <summary>
        /// Creates a new appointment and associates it with a folder.
        /// </summary>
        /// <remarks>
        /// Creates an appointment with all required fields. The identifier and created date 
        /// are generated automatically. The appointment is then associated with the specified folder
        /// by updating the Folder.AppointmentIdentifier.
        /// </remarks>
        /// <param name="request">The appointment details including date, location, notes, and folder identifier. Must not be null.</param>
        /// <param name="ct">A cancellation token that can be used to cancel the operation.</param>
        /// <returns>An <see cref="IActionResult"/> containing the created appointment with its generated identifier.</returns>
        [HttpPost("addappointment")]
        [Authorize(Roles = "Administrator, Inspector")]
        public async Task<IActionResult> AddAppointment([FromBody] AppointmentSaveRequest request, CancellationToken ct)
        {
            var appointment = await appointmentService.AddAppointment(request, ct);

            return Ok(appointment);
        }

        /// <summary>
        /// Updates an existing appointment.
        /// </summary>
        /// <remarks>
        /// Updates an appointment with all required fields. The identifier must correspond to an 
        /// existing appointment. The created date is preserved.
        /// </remarks>
        /// <param name="request">The appointment details including date, location, and notes. Must not be null and must include a valid identifier.</param>
        /// <param name="ct">A cancellation token that can be used to cancel the operation.</param>
        /// <returns>An <see cref="IActionResult"/> containing the updated appointment.</returns>
        [HttpPut("updateappointment")]
        [Authorize(Roles = "Administrator, Inspector")]
        public async Task<IActionResult> UpdateAppointment([FromBody] AppointmentSaveRequest request, CancellationToken ct)
        {
            var appointment = await appointmentService.UpdateAppointment(request, ct);

            return Ok(appointment);
        }

        /// <summary>
        /// Deletes an appointment and removes its association from the related folder.
        /// </summary>
        /// <remarks>
        /// This method requires the caller to have either the Administrator or Inspector role.
        /// The operation deletes the appointment and sets the Folder.AppointmentIdentifier to NULL.
        /// The operation is performed asynchronously and returns an appropriate response based on the result.
        /// </remarks>
        /// <param name="request">The request containing the appointment identifier and folder identifier.</param>
        /// <param name="ct">A cancellation token that can be used to cancel the delete operation.</param>
        /// <returns>An IActionResult that indicates the outcome of the delete operation.</returns>
        [HttpDelete("deleteappointment")]
        [Authorize(Roles = "Administrator, Inspector")]
        public async Task<IActionResult> DeleteAppointment([FromQuery] AppointmentDeleteRequest request, CancellationToken ct)
        {
            var result = await appointmentService.DeleteAppointment(request, ct);

            return Ok(result);
        }
    }
}
