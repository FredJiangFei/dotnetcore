using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPagesContacts.Data;

namespace MyWebApp.Pages
{
    public class CreateModel : PageModel
    {
       private readonly AppDbContext _db;

        public CreateModel(AppDbContext db)
        {
            _db = db;
        }

        [BindProperty] // model binding
        // [BindProperty(SupportsGet = true)]
        // Binding reduces code by using the same property to render form fields (<input asp-for="Customer.Name" />) and accept the input.
        public Customer Customer { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _db.Customers.Add(Customer);
            await _db.SaveChangesAsync();

            return RedirectToPage("/Index");
        }
    }
}