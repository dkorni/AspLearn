using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorPagesEmpty.Pages
{
    public class ExchangeModel : PageModel
    {
        public string Message;
        private readonly decimal currentRate = 64.1m;

        public void OnGet()
        {
            Message = "Введите сумму";
        }

        public void OnPost(int? sum)
        {
            if (sum.Value == null || sum.Value < 1000)
            {
                Message = "Icorrect sum";
            }
            else
            {
                var result = sum / currentRate;
                Message = $"Rub {sum} = $ {result}";
            }
        }
    }
}
