using System;

namespace Domain
{
    public class GameException : Exception
    {
        public int Code { get; private set; }


        public GameException(int code, string message) : base(message)
        {
            Code = code;
        }

        public GameException() : this(500, "Internal Server Error") { }

    }
}