using CarsManager.Web.Models;

namespace CarsManager.Web.Services
{
    public interface ICarsService
    {
        Task<IEnumerable<Car>> GetCars(); // Get all cars.
        Task<Car?> GetCarById(int id); // Get a car by Id.
        Task<bool> UpdateCar(int id, Car updatedCar); // Update method
        Task<bool> DeleteCar(int id); // Delete method
        Task<Car> AddCar(Car newCar); // Add method
    }
}
