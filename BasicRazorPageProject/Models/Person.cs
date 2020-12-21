using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace BasicRazorPageProject.Models
{
    public class Person
    {
        public string Name { get; set; }

        public int Age { get; set; }

        public int Id { get; set; }
    }
}
