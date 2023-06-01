using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using RateMyProfessors.Models;
using Microsoft.AspNetCore.Hosting;
using System.Diagnostics.Metrics;

namespace RateMyProfessors.Services
{
    public class JsonFileProfessorService
    {
        private JsonWriterOptions newJsonWriterOptions;

        public JsonFileProfessorService(IWebHostEnvironment webHostEnvironment)
        {
            WebHostEnvironment = webHostEnvironment;
        }

        public IWebHostEnvironment WebHostEnvironment { get; }

        private string JsonFileName
        {
            get
            {
                return Path.Combine(WebHostEnvironment.WebRootPath, "data", "professors.json");
            }

        }

        // can for each over which is where the name comes from
        public IEnumerable<Professor> GetProfessors()
          
        {
            using (var jsonFileReader = File.OpenText(JsonFileName))
            {
                return JsonSerializer.Deserialize<Professor[]>(jsonFileReader.ReadToEnd(),
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });

            }
        }

        public void AddRating(string professorId, int rating)
        {
            var professors = GetProfessors();

            //LINQ
            var query = professors.First(x => x.Id == professorId);

            if (query.Ratings == null)
            {
                query.Ratings = new int[] { rating };
            }
            else
            {
                var ratings = query.Ratings.ToList();
                ratings.Add(rating);
                query.Ratings = ratings.ToArray();

            }

            using (var outputStream = File.OpenWrite(JsonFileName))
            {
                JsonSerializer.Serialize<IEnumerable<Professor>>(
                    new Utf8JsonWriter(outputStream, new JsonWriterOptions
                    {
                        SkipValidation = true,
                        Indented = true
                    }),

                    professors
               );
            }


        }

        public void AddComment(string professorId, string comment)
        {
            var professors = GetProfessors();

            //LINQ
            var query = professors.First(x => x.Id == professorId);

            if (query.Comments == null)
            {
                query.Comments = new string[] {comment};
            }
            else
            {
                var comments = query.Comments.ToList();
                comments.Add(comment);
                query.Comments = comments.ToArray();

            }

            using (var outputStream = File.OpenWrite(JsonFileName))
            {
                JsonSerializer.Serialize<IEnumerable<Professor>>(
                    new Utf8JsonWriter(outputStream, new JsonWriterOptions
                    {
                        SkipValidation = true,
                        Indented = true
                    }),

                    professors
               );
            }
        }
    }
}