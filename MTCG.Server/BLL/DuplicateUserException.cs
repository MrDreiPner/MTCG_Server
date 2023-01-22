﻿using System.Runtime.Serialization;

namespace SWE1.MTCG.BLL
{
    [Serializable]
    public class DuplicateUserException : Exception
    {
        public DuplicateUserException()
        {
        }

        public DuplicateUserException(string? message) : base(message)
        {
        }

        public DuplicateUserException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected DuplicateUserException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}