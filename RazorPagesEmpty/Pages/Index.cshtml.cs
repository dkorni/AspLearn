using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorPagesEmpty.Pages
{
    public class IndexModel : PageModel
    {
        public string Name;

        public string Age;

        public void OnGet(string name, string age)
        {
            Name = name;
            Age = age;
        }
    }
}
