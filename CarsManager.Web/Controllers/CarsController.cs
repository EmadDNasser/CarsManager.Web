using CarsManager.Web.Models;
using CitiesManager.WebAPI.DatabaseContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarsManager.Web.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {

        private readonly ApplicationDbContext _carcontext;

        public CarsController(ApplicationDbContext carcontext)
        {
            _carcontext = carcontext;
        }

        // GET: api/Cars
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Car>>> GetCars()
        {
            var cars = await _carcontext.Cars.ToListAsync();
            if (!cars.Any())
            {
                return NotFound("No cars available."); //HTTP 404
            }
            return Ok(cars);
        }

        // GET: api/Cars/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Car>> GetCar(int id)
        {
            var car = await _carcontext.Cars.FindAsync(id);

            if (car == null)
            {
                return NotFound($"Car with ID {id} not found."); //HTTP 404
            }

            return Ok(car);
        }

        // POST: api/Cars
        [HttpPost]
        public async Task<ActionResult<Car>> PostCar([Bind(nameof(Car.Id), nameof(Car.Make),nameof(Car.Year),nameof(Car.Model))] Car car)
        {
            if (_carcontext.Cars == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Cars'  is null.");
            }
            _carcontext.Cars.Add(car);
            await _carcontext.SaveChangesAsync();

            return CreatedAtAction("GetCar", new { id = car.Id }, car); //api/Cars/2
        }


        // PUT: api/Cars/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCar(int id, [Bind (nameof(Car.Id), nameof(Car.Make), nameof(Car.Year), nameof(Car.Model))] Car car)
        {
            if (id != car.Id) 
            {
                return BadRequest("Invalid car data."); //HTTP 400
            }

            var result = await _carcontext.Cars.FindAsync(id);
            
            if (result == null)
            {
                return NotFound($"Car with ID {id} not found."); //HTTP 400
            }

            result.Year = car.Year;
            result.Model = car.Model;
            result.Make = car.Make;

            try
            {
                await _carcontext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CarExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent(); // 204 No Content (successful update)
        }

        // DELETE: api/Cars/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCar(int id)
        {
            var car = await _carcontext.Cars.FindAsync(id);
            if (car == null)
            {
                return NotFound(); //HTTP 404
            }

            _carcontext.Cars.Remove(car);
            await _carcontext.SaveChangesAsync();

            return NoContent(); //HTTP 200
        }

        private bool CarExists(int id)
        {
            return (_carcontext.Cars?.Any(e => e.Id == id)).GetValueOrDefault();
        }

    }
}
