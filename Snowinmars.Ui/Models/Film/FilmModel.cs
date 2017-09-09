using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Snowinmars.Entities;
using Snowinmars.Ui.App_LocalResources;

namespace Snowinmars.Ui.Models.Film
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

		[Display(Name = "FilmModel_Name", ResourceType = typeof(Global))]
		public string Name { get; set; }

		[Display(Name = "FilmModel_Year", ResourceType = typeof(Global))]
		public int Year { get; set; }

		[Display(Name = "FilmModel_Authors", ResourceType = typeof(Global))]
		public ICollection<Guid> AuthorIds { get; set; }

		public static FilmModel Empty => FilmModel.EmptyFilm;
	}
}