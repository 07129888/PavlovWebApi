using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PavlovWebApi.Storage
{
    [System.Serializable]
    public class IncorrectCustomerDataException : System.Exception
    {
        public IncorrectCustomerDataException() { }
        public IncorrectCustomerDataException(string message) : base(message) { }
        public IncorrectCustomerDataException(string message, System.Exception inner) : base(message, inner) { }
        protected IncorrectCustomerDataException(
            System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
