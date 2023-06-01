using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RateMyProfessors.Models;
using RateMyProfessors.Services;

namespace RateMyProfessors.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        public JsonFileProfessorService ProfessorService;
        public IEnumerable<Professor> Professors { get; private set; }

        public IndexModel(ILogger<IndexModel> logger,
            JsonFileProfessorService professorService)
        {
            _logger = logger;
            ProfessorService = professorService;
        }

        public void OnGet()
        {
            Professors = ProfessorService.GetProfessors();
        }
    }
}