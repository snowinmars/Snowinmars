using System;

namespace Snowinmars.Entities
{
    public class Author : Entity
    {
        private static readonly Author EmptyAuthor;

        static Author()
        {
            Author.EmptyAuthor = new Author("crunch")
            {
                Shortcut = "",
            };
        }

        public Author(string shortcut)
        {
            if (string.IsNullOrWhiteSpace(shortcut))
            {
                throw new ArgumentException("Shortcut can't be empty");
            }

            this.Shortcut = shortcut;

            this.GivenName = "";
            this.FullMiddleName = "";
            this.FamilyName = "";

            this.Pseudonym = new Pseudonym()
            {
                GivenName = "",
                FullMiddleName = "",
                FamilyName = "",
            };
        }

        private Author() : this("")
        {
        }

        public static Author Empty => Author.EmptyAuthor;
        public string FamilyName { get; set; }
        public string FullMiddleName { get; set; }
        public string GivenName { get; set; }
        public Pseudonym Pseudonym { get; set; }
        public string Shortcut { get; set; }
    }
}