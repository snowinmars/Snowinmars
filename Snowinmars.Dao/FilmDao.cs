using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Snowinmars.Common;
using Snowinmars.Dao.Interfaces;
using Snowinmars.Entities;

namespace Snowinmars.Dao
{
	public class FilmDao : IFilmDao
	{
		public void Create(Film film)
		{
			using (var sqlConnection = new SqlConnection(Constant.ConnectionString))
			{
				AddFilm(film, sqlConnection);
				AddFilmAuthorConnections(film, sqlConnection);
			}
		}

		private static void AddFilmAuthorConnections(Film film, SqlConnection sqlConnection)
		{
			var command = new SqlCommand(LocalConst.FilmAuthor.InsertCommand, sqlConnection);

			command.Parameters.AddWithValue(LocalConst.FilmAuthor.Column.FilmId, LocalCommon.ConvertToDbValue(film.Id));

			// Here I reusing SqlParameter to improve everything :3 Idk, is it true. Read about it? Todo.
			var authorIdParameter = new SqlParameter(LocalConst.BookAuthor.Parameter.AuthorId, SqlDbType.UniqueIdentifier);
			command.Parameters.Add(authorIdParameter);

			sqlConnection.Open();

			foreach (var authorId in film.AuthorIds)
			{
				command.Parameters[1].Value = authorId;

				command.ExecuteNonQuery();
			}

			sqlConnection.Close();
		}

		private void AddFilm(Film film, SqlConnection sqlConnection)
		{
			var command = new SqlCommand(LocalConst.Film.InsertCommand, sqlConnection);

			var id = film.Id;
			var title = film.Title;
			var year = film.Year;
			var kinopoiskUrl = film.KinopoiskUrl;
			var description = film.Description;
			var isSyncronized = film.IsSynchronized;

			command.Parameters.AddWithValue(LocalConst.Film.Parameter.Id, id);
			command.Parameters.AddWithValue(LocalConst.Film.Parameter.Title, title);
			command.Parameters.AddWithValue(LocalConst.Film.Parameter.Year, year);
			command.Parameters.AddWithValue(LocalConst.Film.Parameter.KinopoiskUrl, kinopoiskUrl);
			command.Parameters.AddWithValue(LocalConst.Film.Parameter.Description, description);
			command.Parameters.AddWithValue(LocalConst.Film.Parameter.IsSynchronized, isSyncronized);

			sqlConnection.Open();
			command.ExecuteNonQuery();
			sqlConnection.Close();
		}

		public Film Get(Guid id)
		{
			using (var sqlConnection = new SqlConnection(Constant.ConnectionString))
			{
				var command = new SqlCommand(LocalConst.Film.SelectCommand, sqlConnection);

				command.Parameters.AddWithValue(LocalConst.Film.Parameter.Id, LocalCommon.ConvertToDbValue(id));

				sqlConnection.Open();
				var reader = command.ExecuteReader();

				if (!reader.Read())
				{
					throw new ObjectNotFoundException();
				}

				Film film = LocalCommon.MapFilm(reader);

				sqlConnection.Close();

				//////////////////////
				//// I suppose that reusing SqlCommand will improve perfomance. I read few about it. I have to know it better. Todo?

				command.CommandText = LocalConst.FilmAuthor.SelectByFilmCommand;

				command.Parameters.Clear();
				command.Parameters.AddWithValue(LocalConst.FilmAuthor.Column.FilmId, LocalCommon.ConvertToDbValue(film.Id));

				sqlConnection.Open();
				reader = command.ExecuteReader();

				while (reader.Read())
				{
					Guid authorId = (Guid)reader[LocalConst.FilmAuthor.Column.AuthorId];

					film.AuthorIds.Add(authorId);
				}

				sqlConnection.Close();

				return film;
			}
		}

		public void Remove(Guid id)
		{
			using (var sqlConnection = new SqlConnection(Constant.ConnectionString))
			{
				var command = new SqlCommand(LocalConst.Film.DeleteCommand, sqlConnection);

				command.Parameters.AddWithValue(LocalConst.Book.Column.Id, LocalCommon.ConvertToDbValue(id));

				sqlConnection.Open();
				command.ExecuteNonQuery();
				sqlConnection.Close();
			}
		}

		public void Update(Film film)
		{
			using (var sqlConnection = new SqlConnection(Constant.ConnectionString))
			{
				var command = new SqlCommand(LocalConst.Film.UpdateCommand, sqlConnection);

				var id = film.Id;
				var title = film.Title;
				var year = film.Year;
				var kinopoiskUrl = film.KinopoiskUrl;
				var description = film.Description;
				var isSyncronized = film.IsSynchronized;

				command.Parameters.AddWithValue(LocalConst.Film.Parameter.Id, id);
				command.Parameters.AddWithValue(LocalConst.Film.Parameter.Title, title);
				command.Parameters.AddWithValue(LocalConst.Film.Parameter.Year, year);
				command.Parameters.AddWithValue(LocalConst.Film.Parameter.KinopoiskUrl, kinopoiskUrl);
				command.Parameters.AddWithValue(LocalConst.Film.Parameter.KinopoiskUrl, description);
				command.Parameters.AddWithValue(LocalConst.Film.Parameter.IsSynchronized, isSyncronized);

				sqlConnection.Open();
				command.ExecuteNonQuery();
				sqlConnection.Close();
			}
		}

		public IEnumerable<Film> Get(Expression<Func<Film, bool>> filter)
		{
			var films = this.GetAll().ToList();

			return films;
		}

		private IEnumerable<Film> GetAll()
		{
			using (var sqlConnection = new SqlConnection(Constant.ConnectionString))
			{
				var command = new SqlCommand(LocalConst.Film.SelectAllCommand, sqlConnection);

				sqlConnection.Open();
				var reader = command.ExecuteReader();
				var films = LocalCommon.MapFilms(reader);
				sqlConnection.Close();

				return films;
			}
		}
	}
}
