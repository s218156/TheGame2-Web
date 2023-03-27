namespace TheGame2_Library.Exceptions
{
    public class TheGameWebException : Exception
    {
        public string ExceptionCode;
        public TheGameWebException(string message) : base(message)
        {

        }
        public TheGameWebException(string message, Exception inner) : base(message, inner)
        {

        }
        public TheGameWebException(string code, string message) : base(message)
        {
            this.ExceptionCode = code;
        }
    }
}
