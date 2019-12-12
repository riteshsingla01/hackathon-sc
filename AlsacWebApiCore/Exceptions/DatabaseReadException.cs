using System;

namespace AlsacWebApiCore.Exceptions
{
    public class DatabaseReadException : Exception
    {
        public DatabaseReadException()
        {
        }

        public DatabaseReadException(string message): base(message)
        {
        }

        public DatabaseReadException(string message, Exception inner): base(message, inner)
        {
        }
    }
}
