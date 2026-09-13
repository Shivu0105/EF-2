using EF_2.Model;
using Microsoft.EntityFrameworkCore;

namespace EF_2.Repository
{
    public class CarDB : DbContext
    {
        public CarDB(DbContextOptions<CarDB> options) : base(options)
        {

        }

        public DbSet <Cars> CarsTable { get; set; }

    }
}
