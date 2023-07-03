using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using Tig.Crebo.Etl.Csv;

namespace Tig.Crebo.Etl.Pages; 

public class IndexModel : PageModel
{
    public List<VerrijkteStudent> VerrijkteStudentenMetGeldigeCREBO { get; set; }
    public List<VerrijkteStudent> VerrijkteStudentenMetOngeldigeCREBO { get; set; }
    
    private readonly StudentenService _studentenService;

    public IndexModel(StudentenService studentenService)
    {
        this._studentenService = studentenService;
    }
    
    public void OnGet()
    {
        if (TempData["VerrijkteStudentenMetGeldigeCREBO"] == null)
        {
            VerrijkteStudentenMetGeldigeCREBO = new List<VerrijkteStudent>();
        }
        else
        {
            VerrijkteStudentenMetGeldigeCREBO =  JsonConvert.DeserializeObject<List<VerrijkteStudent>>(TempData["VerrijkteStudentenMetGeldigeCREBO"].ToString());
        }
        
        if (TempData["VerrijkteStudentenMetOngeldigeCREBO"] == null)
        {
            VerrijkteStudentenMetOngeldigeCREBO = new List<VerrijkteStudent>();
        }
        else
        {
            VerrijkteStudentenMetOngeldigeCREBO =  JsonConvert.DeserializeObject<List<VerrijkteStudent>>(TempData["VerrijkteStudentenMetOngeldigeCREBO"].ToString());
        }
    }
    
    public async Task<IActionResult> OnPostAsync(IFormFile? fileInput)
    {
        if (fileInput is not { Length: > 0 }) return Page();
        
        var studenten = new List<Student>();
        
        using (var streamReader = new StreamReader(fileInput.OpenReadStream()))
        {
            studenten = CsvParser.ParseCsvToListOfObjects<Student>(streamReader.BaseStream);
        }

        var verrijkteStudenten = _studentenService.GetVerrijkteStudenten(studenten);

        TempData["VerrijkteStudentenMetGeldigeCREBO"] = JsonConvert.SerializeObject(verrijkteStudenten.Where(s => s.OpleidingNaam != null).ToList());
        TempData["VerrijkteStudentenMetOngeldigeCREBO"] = JsonConvert.SerializeObject(verrijkteStudenten.Where(s => s.OpleidingNaam == null).ToList());
        
        return RedirectToPage("./Index");
    }
}