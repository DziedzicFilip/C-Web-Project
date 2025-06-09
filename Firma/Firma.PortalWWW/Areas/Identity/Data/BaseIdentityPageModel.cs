using Microsoft.AspNetCore.Mvc.RazorPages;
using Firma.Data.Data;
using System.Linq;

public class BaseIdentityPageModel : PageModel
{
    protected readonly FirmaContext _context;

    public BaseIdentityPageModel(FirmaContext context)
    {
        _context = context;
    }

    public void LoadCommonData()
    {
        ViewData["ModelStrony"] = _context.Strona.OrderBy(s => s.Pozycja).ToList();
        ViewData["ModelBaner"] = _context.Baner.ToList();
        ViewData["ModelLinki"] = _context.PrzydatneLinki.ToList();
        ViewData["ModelOnas"] = _context.Onas.ToList();
        ViewData["ModelKontakt"] = _context.Kontakt.ToList();
    }
}
