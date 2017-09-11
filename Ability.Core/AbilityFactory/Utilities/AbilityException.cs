using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ability.Core.AbilityFactory.Utilities
{
    using System.Collections;

    [Serializable]
    internal class AbilityException : Exception
    {
        public AbilityException()
            : base()
        {
        }

        public AbilityException(string message)
            : base(message)
        {
        }

        public AbilityException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected AbilityException(
            System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context)
            : base(info, context)
        {
        }

        public override IDictionary Data { get; } = new Dictionary<string, object>();
    }
}
