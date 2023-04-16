using System.Runtime.Serialization;

namespace PruebaApiThales.Exceptions
{
    public class DataAccessException : Exception
    {
        public DataAccessException(string message) : base(message)
        {
        }

        protected DataAccessException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
