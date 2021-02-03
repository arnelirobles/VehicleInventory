using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VehicleInventory.Models;
using VehicleInventory.Service;

namespace VehicleInventory.BusinessLogic
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleController : ControllerBase
    {
        private readonly IVehicleService _vehicleService;

        public VehicleController(IVehicleService vehicleService)
        {
            _vehicleService = vehicleService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Vehicle>>> Get()
        {
            var result = await _vehicleService.GetAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Vehicle>> Get([FromRoute] string id)
        {
            var result = await _vehicleService.GetByIdAsync(id);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] string id)
        {
            try
            {
                await _vehicleService.DeleteAsync(id);

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Put(Vehicle vehicle)
        {
            if (!ModelState.IsValid)
                return BadRequest("Not a valid data");
            try
            {
                await _vehicleService.UpdateAsync(vehicle);

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(Vehicle vehicle)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var result = await _vehicleService.CreateAsync(vehicle);

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}