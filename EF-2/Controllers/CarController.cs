using EF_2.Model;
using EF_2.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EF_2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarController : ControllerBase
    {
        private readonly CarDB _car;
        public CarController(CarDB car)
        {
            _car = car;
        }

        [HttpGet]
        public async Task<IActionResult> getCars()
        {
            try
            {
                var carData = await _car.CarsTable.ToListAsync();
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
                _car.CarsTable.Add(entity);
                await _car.SaveChangesAsync();

                return Ok("Data Saved Successfully");
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
                var entity = await _car.CarsTable.FindAsync(Id);
                if (entity == null)
                {
                    return NotFound("Car not found");
                }

                _car.CarsTable.Remove(entity);
                await _car.SaveChangesAsync();

                return Ok("Data Deleted Successfully");
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
                var existingCar = await _car.CarsTable.FindAsync(Id);
                if (existingCar == null)
                {
                    return NotFound("Car not found");
                }
                existingCar.Name = entity.Name;
                existingCar.Model = entity.Model;
                existingCar.Color = entity.Color;
                existingCar.Year = entity.Year;

                _car.CarsTable.Update(existingCar);
                await _car.SaveChangesAsync();
                return Ok("Data Updated Successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
