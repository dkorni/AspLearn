using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BasicRazorPageProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BasicRazorPageProject.Pages
{
    public class PersonModel : PageModel
    {
        public string Message;

        //[BindProperty]
        //public string Name { get; set; }

        //[BindProperty]
        //public int Age { get; set; }

        //public int Id { get; set; }

        [BindProperty]
        public Person Person { get; set; } = new Person(){Id = 10};

        public void OnGet(int id)
        {
            Message = "Enter data";
            //Id = id;
        }

        //public void OnPost()
        //{
        //    Message = $"Name: {Name} Age: {Age}";
        //}

        public void OnPost()
        {
            Message = $"Name: {Person.Name} Age: {Person.Age}";
        }
    }
}
