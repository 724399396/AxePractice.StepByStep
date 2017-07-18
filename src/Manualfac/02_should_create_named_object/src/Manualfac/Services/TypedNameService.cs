using System;

namespace Manualfac.Services
{
    class TypedNameService : Service, IEquatable<TypedNameService>
    {
        private readonly Type serviceType;
        private readonly string name;

        #region Please modify the following code to pass the test

        /*
         * This class is used as a key for registration by both type and name.
         */

        public TypedNameService(Type serviceType, string name)
        {
            this.serviceType = serviceType;
            this.name = name;
        }

        public bool Equals(TypedNameService other)
        {
            if (other == null) return false;
            if (ReferenceEquals(this, other)) return true;
            return other.serviceType == serviceType && other.name == name;
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (ReferenceEquals(this, obj)) return true;
            var other = obj as TypedNameService;
            return Equals(other);
        }

        public override int GetHashCode()
        {
            return serviceType.GetHashCode() + name.GetHashCode();
        }

        #endregion
    }
}