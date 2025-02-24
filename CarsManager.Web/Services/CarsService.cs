using CarsManager.Web.Models;

namespace CarsManager.Web.Services
{
    public class CarsService : ICarsService
    {
        private readonly List<Car> _cars = new()
        {
            new Car { Id = 1, Make = "Toyota", Model = "Camry", Year = 2022 },
            new Car { Id = 2, Make = "Honda", Model = "Civic", Year = 2023 },
            new Car { Id = 3, Make = "Ford", Model = "Mustang", Year = 2021 }
        };

        public async Task<IEnumerable<Car>> GetCars()
        {
            return await Task.FromResult(_cars);
        }

        public async Task<Car?> GetCarById(int id)
        {
            var car = _cars.FirstOrDefault(c => c.Id == id);
            return await Task.FromResult(car); // Wrap the result inside a completed Task
        }

        public async Task<bool> UpdateCar(int id, Car updatedCar)
        {
            var car = _cars.FirstOrDefault(c => c.Id == id);
            if (car == null)
                return await Task.FromResult(false);

            car.Make = updatedCar.Make;
            car.Model = updatedCar.Model;
            car.Year = updatedCar.Year;

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteCar(int id)
        {
            var car = _cars.FirstOrDefault(c => c.Id == id);
            if (car == null)
                return await Task.FromResult(false);

            _cars.Remove(car);
            return await Task.FromResult(true);
        }

        public async Task<Car> AddCar(Car newCar)
        {
            newCar.Id = _cars.Max(c => c.Id) + 1; // Generate new ID
            _cars.Add(newCar);
            return await Task.FromResult(newCar);
        }
    }
}
