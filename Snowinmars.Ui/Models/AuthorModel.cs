using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Snowinmars.Entities;

namespace Snowinmars.Ui.Models
{
	public class AuthorModel
	{
		public Guid Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Surname { get; set; }
		public string Shortcut { get; set; }

		public static AuthorModel Map(Author author)
		{
			AuthorModel authorModel = new AuthorModel
			{
				Id = author.Id,
				FirstName = author.FirstName,
				LastName = author.LastName,
				Surname = author.Surname,
				Shortcut = author.Shortcut,
			};

			return authorModel;
		}
	}
}