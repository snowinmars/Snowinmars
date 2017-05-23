using Snowinmars.Common;
using Snowinmars.Entities;
using Snowinmars.Ui.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Snowinmars.Ui.Controllers
{
    internal static class ControllerHelper
    {
        internal static JsonResult GetFailJsonResult()
        {
            return new JsonResult { Data = new { success = false } };
        }

        internal static JsonResult GetSuccessJsonResult(object data)
        {
            return new JsonResult {Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet};
        }

        internal static BookModel Map(Book book)
        {
            return new BookModel
            {
                Id = book.Id,
                PageCount = book.PageCount,
                Title = book.Title,
                Year = book.Year,
                AuthorModelIds = book.AuthorIds.ToList(),
                AuthorShortcuts = book.AuthorShortcuts.ToList(),
                AdditionalInfo = book.AdditionalInfo,
                Bookshelf = book.Bookshelf,
                FlibustaUrl = book.FlibustaUrl,
                LibRusEcUrl = book.LibRusEcUrl,
                LiveLibUrl = book.LiveLibUrl,
                Owner = book.Owner,
                MustInformAboutWarnings = book.MustInformAboutWarnings,
                IsSynchronized = book.IsSynchronized,
            };
        }

        internal static Book Map(BookModel bookModel)
        {
            var book = new Book(bookModel.Title, bookModel.PageCount)
            {
                Year = bookModel.Year,
                AdditionalInfo = ControllerHelper.Trim(bookModel.AdditionalInfo),
                Bookshelf = ControllerHelper.Trim(bookModel.Bookshelf),
                FlibustaUrl = ControllerHelper.Trim(bookModel.FlibustaUrl),
                LibRusEcUrl = ControllerHelper.Trim(bookModel.LibRusEcUrl),
                LiveLibUrl = ControllerHelper.Trim(bookModel.LiveLibUrl),
                MustInformAboutWarnings = bookModel.MustInformAboutWarnings,
                IsSynchronized = bookModel.IsSynchronized,
            };

            ControllerHelper.SetOwner(bookModel.Owner, book);
            ControllerHelper.SetId(bookModel, book);
            ControllerHelper.SetAuthorIds(bookModel.AuthorModelIds, book.AuthorIds);
            ControllerHelper.SetAuthorShortcuts(bookModel.AuthorShortcuts, book.AuthorShortcuts);

            return book;
        }

        internal static AuthorModel Map(Author author)
        {
            return new AuthorModel
            {
                Id = author.Id,
                GivenName = author.GivenName,
                FullMiddleName = author.FullMiddleName,
                FamilyName = author.FamilyName,
                Shortcut = author.Shortcut,
                IsSynchronized = author.IsSynchronized,
                MustInformAboutWarnings = author.MustInformAboutWarnings,
                PseudonymGivenName = author.Pseudonym?.GivenName ?? "",
                PseudonymFullMiddleName = author.Pseudonym?.FullMiddleName ?? "",
                PseudonymFamilyName = author.Pseudonym?.FamilyName ?? "",
            };
        }

        internal static Author Map(AuthorModel authorModel)
        {
            var author = new Author(authorModel.Shortcut)
            {
                GivenName = ControllerHelper.Trim(authorModel.GivenName),
                FullMiddleName = ControllerHelper.Trim(authorModel.FullMiddleName),
                FamilyName = ControllerHelper.Trim(authorModel.FamilyName),
                IsSynchronized = authorModel.IsSynchronized,
                MustInformAboutWarnings = authorModel.MustInformAboutWarnings,
                Pseudonym = ControllerHelper.MapPseudonym(authorModel),
            };

            ControllerHelper.SetId(authorModel, author);

            return author;
        }

        internal static UpdateUserModel Map(User user)
        {
            return new UpdateUserModel()
            {
                Id = user.Id,
                Username = user.Username,
                Roles = user.Roles,
                Email = user.Email,
                Language = user.Language,
                IsSynchronized = user.IsSynchronized,
            };
        }

        internal static User Map(CreateUserModel userModel)
        {
            var user = new User(userModel.Username)
            {
                Email = ControllerHelper.Trim(userModel.Email),
                Roles = userModel.Roles,
                Language = userModel.Language,
            };

            ControllerHelper.SetId(userModel, user);

            return user;
        }

        internal static User Map(UpdateUserModel userModel)
        {
            var username = string.IsNullOrWhiteSpace(userModel.Username) ? "" : userModel.Username;

            var user = new User(username)
            {
                Email = ControllerHelper.Trim(userModel.Email),
                Roles = userModel.Roles,
                Language = userModel.Language,
            };

            ControllerHelper.SetId(userModel, user);

            return user;
        }
        internal static Pseudonym MapPseudonym(AuthorModel authorModel)
        {
            return new Pseudonym
            {
                GivenName = ControllerHelper.Trim(authorModel.PseudonymGivenName),
                FullMiddleName = ControllerHelper.Trim(authorModel.PseudonymFullMiddleName),
                FamilyName = ControllerHelper.Trim(authorModel.PseudonymFamilyName),
            };
        }

        internal static string Trim(string str) => str?.Trim() ?? "";

        private static void SetAuthorIds(IEnumerable<Guid> authorModelIds, ICollection<Guid> container)
        {
            if (authorModelIds != null && authorModelIds.Any())
            {
                container.AddRange(authorModelIds);
            }
        }

        private static void SetAuthorShortcuts(IEnumerable<string> authorShortcuts, ICollection<string> container)
        {
            if (authorShortcuts != null && authorShortcuts.Any())
            {
                container.AddRange(authorShortcuts);
            }
        }

        private static void SetId(EntityModel model, Entity user)
        {
            if (model.Id != Guid.Empty)
            {
                user.Id = model.Id;
            }
        }

        private static void SetOwner(string owner, Book book)
        {
            if (string.IsNullOrWhiteSpace(owner))
            {
                book.Owner = System.Web.HttpContext.Current.User.Identity.Name;
            }
            else
            {
                book.Owner = ControllerHelper.Trim(owner);
            }
        }

        public static IEnumerable<UpdateUserModel> Map(IEnumerable<User> users)
        {
            return users.Select(ControllerHelper.Map).ToList();
        }
    }

    public static class BinaryConverter
    {
        public static BitArray ToBinary(this int numeral)
        {
            return new BitArray(new[] { numeral });
        }

        public static int ToNumeral(this BitArray binary)
        {
            if (binary == null)
                throw new ArgumentNullException("binary");
            if (binary.Length > 32)
                throw new ArgumentException("must be at most 32 bits long");

            var result = new int[1];
            binary.CopyTo(result, 0);
            return result[0];
        }

        public static int ToInt32(this bool[] source)
        {
            int result = 0;
            // This assumes the array never contains more than 8 elements!
            // Loop through the array
            for (var i = 0; i < 32; i++)
            {
                bool b = source[i];
                // if the element is 'true' set the bit at that position

                if (b)
                {
                    result |= 1 << i;
                }
            }

            return result;
        }


        public static bool[] ToBoolArray(this int numeral)
        {
            return numeral.ToBinary().Cast<bool>().ToArray();
        }

        public static UserRoles Promote(this UserRoles userRoles)
        {
            int maxUserRolesValue = BinaryConverter.GetMaxValue(typeof(UserRoles));

            bool[] array = ((int)userRoles).ToBoolArray();

            int lastIndex = GetEnumLength(array);

            if (lastIndex == 0)
            {
                // you can't unban with promotion
                return UserRoles.Banned;
            }

            array[lastIndex] = true; // promote

            int int32 = array.ToInt32();

            if (int32 > maxUserRolesValue)
            {
                int32 = maxUserRolesValue;
            }

            return (UserRoles)int32;
        }

        private static int GetMaxValue(Type enumType)
        {
            int maxEnumValue = Enum.GetValues(enumType).Cast<int>().Max();

            var attributes = enumType.GetCustomAttributes(typeof(FlagsAttribute), false);

            if (attributes.Any())
            {
                // Geometric progression
                int n = (int)Math.Log(maxEnumValue, 2) + 1;
                int q = 2;

                return (int)((1 - Math.Pow(q, n)) / (1 - q));
            }

            return maxEnumValue;
        }

        public static UserRoles Demote(this UserRoles userRoles)
        {
            int maxUserRolesValue = BinaryConverter.GetMaxValue(typeof(UserRoles));

            bool[] array = ((int)userRoles).ToBoolArray();

            int lastIndex = BinaryConverter.GetEnumLength(array);

            if (lastIndex == 0)
            {
                return UserRoles.Banned;
            }

            // you can't demote less then user status
            if (lastIndex == 1)
            {
                return UserRoles.User;
            }

            array[lastIndex - 1] = false; // demote

            int int32 = array.ToInt32();

            if (int32 > maxUserRolesValue)
            {
                int32 = maxUserRolesValue;
            }

            return (UserRoles)int32;
        }

        private static int GetEnumLength(bool[] array)
        {
            int lastIndex = 0;

            for (int i = 0; i < array.Length; i++)
            {
                if (!array[i])
                {
                    lastIndex = i;
                    break;
                }
            }

            return lastIndex;
        }
    }

}