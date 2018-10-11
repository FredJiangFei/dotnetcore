using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPagesContacts.Data;

namespace MyWebApp.Pages.Contact
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

        [BindProperty]
        // [Required(ErrorMessage = "Color is required")]
        public string Color { get; set; }


        [TempData]
        public string Message { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _db.Customers.Add(Customer);
            Message = $"Customer {Customer.Name} added";
            await _db.SaveChangesAsync();

            return RedirectToPage("./Index");
        }

        public async Task<IActionResult> OnPostJoinListAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _db.Customers.Add(Customer);
            Message = $"Customer {Customer.Name} added";
            await _db.SaveChangesAsync();
            return RedirectToPage("./Index");
        }

        public async Task<IActionResult> OnPostJoinListUCAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            Customer.Name = Customer.Name?.ToUpper();
            return await OnPostJoinListAsync();
        }
    }
}