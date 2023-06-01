using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RateMyProfessors.Models;
using RateMyProfessors.Services;

namespace RateMyProfessors.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProfessorsController : ControllerBase
    {
        public ProfessorsController(JsonFileProfessorService professorService)
        {
            this.ProfessorService = professorService;
        }

        public JsonFileProfessorService ProfessorService { get; }

        [HttpGet]

        public IEnumerable<Professor> Get()
        {
            return ProfessorService.GetProfessors();
        }

        public IEnumerable<Professor> Search(string searchTerm)
        {
            return ProfessorService.GetProfessors();
        }
       
        //[HttpPatch] "[FromBody]"
        [Route("Rate")]
        [Route("Comment")]
        [HttpGet]

        public ActionResult Get([FromQuery] string ProfessorId,
                                [FromQuery] int Rating)
        {
            ProfessorService.AddRating(ProfessorId, Rating);
            return Ok();
        }

        public ActionResult Get([FromQuery] string ProfessorId,
                                [FromQuery] string Comments)
        {
            ProfessorService.AddComment(ProfessorId, Comments);
            return Ok();
        }


        //public async Task<IActionResult> Index(string id)
        //{
        //    if (id == null)
        //    {
        //        return Problem("Search box is null.");
        //    }

        //    var professors = from p in id
        //                 select p;

        //    if (!String.IsNullOrEmpty(id))
        //    {
        //        professors = professors.Where(r => r.SearchID!.Contains(id));
        //    }

        //    return View(await professors.ToListAsync());
        //}

    }
}