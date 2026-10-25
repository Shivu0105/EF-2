using EF_2.Model;
using EF_2.Repository;
using Microsoft.EntityFrameworkCore;


namespace EF_2.Services
{
    public class CarService
    {
        private readonly CarDB _carDB;
        public CarService(CarDB carDB)
        {
            _carDB = carDB;
        }

        public async Task<List<Cars>> GetCarsAsync()
        {
            return await _carDB.CarsTable.ToListAsync();
        }

        public async Task<string> AddCars(Cars entity)
        {
            try
            {
                _carDB.CarsTable.Add(entity);
                await _carDB.SaveChangesAsync();
                return "Data Saved Successfully";
            }
            catch (Exception ex)
            {
                return $"Error occurred while saving data: {ex.Message}";
            }
           
        }

        public async Task<string> DeleteCar(int Id)
        {
            try
            {
                var entity = await _carDB.CarsTable.FindAsync(Id);
                if (entity == null)
                {
                    return "Car not found";
                }
                _carDB.CarsTable.Remove(entity);
                await _carDB.SaveChangesAsync();
                return "Car deleted successfully";

            }
            catch (Exception ex)
            {
                return $"Error occurred while deleting data: {ex.Message}";
            }
        }

        public async Task<string> UpdateCar(int Id, Cars entity)
        {
            try
            {
                var existingCar = await _carDB.CarsTable.FindAsync(Id);
                if (existingCar == null)
                {
                    return "Car not found";
                }
                existingCar.Name = entity.Name;
                existingCar.Model = entity.Model;
                existingCar.Color = entity.Color;
                existingCar.Year = entity.Year;

                _carDB.CarsTable.Update(existingCar);
                await _carDB.SaveChangesAsync();
                return "Car updated successfully";
            }
            catch (Exception ex)
            {
                return $"Error occurred while updating data: {ex.Message}";
            }
        }
    }
}
