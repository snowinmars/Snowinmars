using System;

namespace Snowinmars.Entities
{
    public class Warning : Entity
    {
        public Warning(Guid entityId)
        {
            this.EntityId = entityId;
        }

        public Guid EntityId { get; set; }

        public string Message { get; set; }

        public override bool Equals(object obj)
        {
            Warning w = obj as Warning;

            if (w == null)
            {
                return false;
            }

            return this.Equals(w);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (this.EntityId.GetHashCode() * 397) ^
                    (this.Message?.GetHashCode() ?? 0);
            }
        }

        public override string ToString()
        {
            return this.Message;
        }

        protected bool Equals(Warning other)
        {
            return this.EntityId.Equals(other.EntityId) &&
                string.Equals(this.Message, other.Message);
        }
    }
}