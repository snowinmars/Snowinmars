using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;

namespace Snowinmars.UserSlice.UserEntites
{
	public class Validation
	{
		public static void CheckUsername(string username)
		{
			if (username == null)
			{
				throw new ValidationException("Username is null");
			}

			if (!Regex.IsMatch(username, "[a-zA-Z]*"))
			{
				throw new ValidationException("Username can contains only english letters");
			}

			if (username.Length < Common.Constant.MinUsernameLength)
			{
				throw new ValidationException("Username is too short");
			}

			if (username.Length > Common.Constant.MaxUsernameLength)
			{
				throw new ValidationException("Username is too long");
			}
		}

		public static void Check(ApplicationUser user)
		{
			Validation.Check(user.Id);

			Validation.CheckUsername(user.Username);
			Validation.CheckEmail(user.Email);
		}

		public static void CheckEmail(string email)
		{
			if (email == null)
			{
				throw new ValidationException("User's email is null");
			}

			if (!string.IsNullOrWhiteSpace(email) && !Regex.IsMatch(email, ".*\\@.*\\..*"))
			{
				throw new ValidationException("User's email have wrong format");
			}
		}

		public static void Check(Guid id)
		{
			if (id == Guid.Empty)
			{
				throw new ValidationException("Author's id can't be empty");
			}
		}

		public static void CheckPassword(string password)
		{
			if (password == null)
			{
				throw new ValidationException("User's password is null");
			}

			if (password.Length < 8 ||
			    Validation.top100Passwords.Contains(password))
			{
				throw new UserIsMoronException("Password is in top 100");
			}
		}

		public static void CheckSalt(string salt)
		{
			if (salt == null)
			{
				throw new ValidationException("Salt is null");
			}

			if (salt.Length < 16)
			{
				throw new ValidationException("You can't use this salt: it's too short");
			}
		}

		#region top100Passwords

		private static string[] top100Passwords = new[]
		{
			"123456",
			"12345",
			"password",
			"DEFAULT",
			"123456789",
			"qwerty",
			"12345678",
			"abc123",
			"pussy",
			"1234567",
			"696969",
			"ashley",
			"fuckme",
			"football",
			"baseball",
			"fuckyou",
			"111111",
			"1234567890",
			"ashleymadison",
			"password1",
			"madison",
			"asshole",
			"superman",
			"mustang",
			"harley",
			"654321",
			"123123",
			"hello",
			"monkey",
			"000000",
			"hockey",
			"letmein",
			"11111",
			"soccer",
			"cheater",
			"kazuga",
			"hunter",
			"shadow",
			"michael",
			"121212",
			"666666",
			"iloveyou",
			"qwertyuiop",
			"secret",
			"buster",
			"horny",
			"jordan",
			"hosts",
			"zxcvbnm",
			"asdfghjkl",
			"affair",
			"dragon",
			"987654",
			"liverpool",
			"bigdick",
			"sunshine",
			"yankees",
			"asdfg",
			"freedom",
			"batman",
			"whatever",
			"charlie",
			"fuckoff",
			"money",
			"pepper",
			"jessica",
			"asdfasdf",
			"1qaz2wsx",
			"987654321",
			"andrew",
			"qazwsx",
			"dallas",
			"55555",
			"131313",
			"abcd1234",
			"anthony",
			"steelers",
			"asdfgh",
			"jennifer",
			"killer",
			"cowboys",
			"master",
			"jordan23",
			"robert",
			"maggie",
			"looking",
			"thomas",
			"george",
			"matthew",
			"7777777",
			"amanda",
			"summer",
			"qwert",
			"princess",
			"ranger",
			"william",
			"corvette",
			"jackson",
			"tigger",
			"computer",
		};

		#endregion top100Passwords
	}
}
