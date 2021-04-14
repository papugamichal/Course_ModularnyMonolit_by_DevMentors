﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Confab.Shared.Abstraction.Kernel.Types
{
    public abstract class TypeId : IEquatable<TypeId>
    {
        public Guid Value { get; }

        protected TypeId(Guid value)
            => Value = value;

        public bool IsEmpty() => Value == Guid.Empty;

        public bool Equals(TypeId other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Value.Equals(other.Value);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((TypeId)obj);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }


        public static implicit operator Guid(TypeId typeId) => typeId.Value;

        public static bool operator ==(TypeId left, TypeId right)
        {
            if (ReferenceEquals(left, right))
            {
                return true;
            }

            if (left is not null && right is not null)
            {
                return left.Equals(right);
            }

            return false;
        }

        public static bool operator !=(TypeId left, TypeId right)
        {
            return !(left == right);
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}
