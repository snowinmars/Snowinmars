using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Snowinmars.Entities;
using Snowinmars.Ui.App_LocalResources;

namespace Snowinmars.Ui.Models
{
	public class FilmModel : EntityModel
	{
		private static readonly FilmModel EmptyFilm = new FilmModel
		{
			Id = Guid.Empty,
			IsSynchronized = false,
			
		};

		public FilmModel()
		{
			this.AuthorIds = new List<Guid>();
		}

		[Display(Name = "FilmModel_Title", ResourceType = typeof(Global))]
		public string Title { get; set; }

		[Display(Name = "FilmModel_Year", ResourceType = typeof(Global))]
		public int Year { get; set; }

		[Display(Name = "FilmModel_Authors", ResourceType = typeof(Global))]
		public ICollection<Guid> AuthorIds { get; set; }

		[Display(Name = "FilmModel_KinopoiskUrl", ResourceType = typeof(Global))]
		public string KinopoiskUrl { get; set; }

		[Display(Name = "FilmModel_Description", ResourceType = typeof(Global))]
		public string Description { get; set; }

		public static FilmModel Empty => FilmModel.EmptyFilm;
	}
}