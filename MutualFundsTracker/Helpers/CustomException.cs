using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MutualFundsTracker.Helpers
{
    [Serializable]
    public class CustomException : Exception
    {
        public CustomException() { }

        public CustomException(string message)
            : base(message) { }

        public CustomException(string message, Exception inner)
            : base(message, inner) { }
    }
}
