using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SkysFormsDemo.Data;

namespace RazorAjax3UseCasesEnd.Pages.Person
{
    public class ViewModel : PageModel
    {
        private readonly ApplicationDbContext _dbContext;

        public ViewModel(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public string Name { get; set; }
        public int Id { get; set; }

        public class Car
        {
            public int Id { get; set; }
            public string Vin { get; set; }
            public string Manufacturer { get; set; }
            public string Model { get; set; }
            public string Type { get; set; }
            public string Fuel { get; set; }
            public DateTime BoughtDate { get; set; }
        }
        public List<Car> Cars { get; set; } = new List<Car>();

        public IActionResult OnGetFetchValue(int id)
        {
            // Detta är så klart löjligt!
            // Jag vill bara demonstrera att här gör man sin "dyr" beräkning
            return new JsonResult(new
            {
                value = id * 1000
            });
        }
        public void OnGet(int personId)
        {
            var person = _dbContext.Person
                .Include(e => e.OwnedCars)
                .First(person => person.Id == personId);
            Id = personId;
            Name = person.Name;
            Cars = person.OwnedCars.Select(e => new Car
            {
                BoughtDate = e.BoughtDate,
                Id = e.Id,
                Model = e.Model,
                Fuel = e.Fuel,
                Manufacturer = e.Manufacturer,
                Type = e.Type,
                Vin = e.Vin
            }).ToList();
        }
    }
}
