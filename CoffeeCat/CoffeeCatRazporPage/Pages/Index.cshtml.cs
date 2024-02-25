using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Threading.Tasks;
using Repositories;
using Entities;
public class IndexModel : PageModel
{
  

    public IndexModel(CoffeeCatContext context)
    {
       
    }

   

    public async Task OnGetAsync()
    {
     
    }
}
