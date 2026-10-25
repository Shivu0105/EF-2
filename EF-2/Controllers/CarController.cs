using EF_2.Model;
using EF_2.Repository;
using EF_2.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EF_2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarController : ControllerBase
    {
        private readonly CarService _carService;
        public CarController(CarService car)
        {
            _carService = car;
        }

        [HttpGet]
        public async Task<IActionResult> getCars()
        {
            try
            {
                var carData = await _carService.GetCarsAsync();
                return Ok(carData);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> createCar([FromBody] Cars entity)
        {
            try
            {
                var result = await _carService.AddCars(entity);
                return Ok(result);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCar(int Id)
        {
            try
            {
                var result = await _carService.DeleteCar(Id);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCars(int Id, [FromBody] Cars entity)
        {
            try
            {
                var result = await _carService.UpdateCar(Id, entity);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
