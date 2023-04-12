using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace hospital_project.Models.ViewModels
{
    public class DetailsDepartment
    {
        public GenreDto SelectedGenre { get; set; }
        public IEnumerable<AnimeDto> TaggedAnimes { get; set; }
    }
}