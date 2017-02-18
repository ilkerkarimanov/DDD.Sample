using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DDD.Core.Domain
{
    public abstract class Identity : IEquatable<Identity>, IIdentity
    {
        public Identity()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public Identity(string id)
        {
            this.Id = id;
        }

        public string Id { get; set; }

        public bool Equals(Identity id)
        {
            if (object.ReferenceEquals(this, id)) return true;
            if (object.ReferenceEquals(null, id)) return false;
            return this.Id.Equals(id.Id);
        }

        public override bool Equals(object anotherObject)
        {
            return Equals(anotherObject as Identity);
        }

        public override int GetHashCode()
        {
            return (this.GetType().GetHashCode() * 907) + this.Id.GetHashCode();
        }

        public override string ToString()
        {
            return this.GetType().Name + " [Id=" + Id + "]";
        }

        public static bool operator ==(Identity a, Identity b)
        {
            // If both are null, or both are same instance, return true.
            if (System.Object.ReferenceEquals(a, b))
            {
                return true;
            }

            // If one is null, but not both, return false.
            if (((object)a == null) || ((object)b == null))
            {
                return false;
            }
            return a.Equals(b);
        }

        public static bool operator !=(Identity a, Identity b)
        {
            return !(a == b);
        }
    }

    public interface IIdentity
    {
        string Id { get; set; }
    }
}
