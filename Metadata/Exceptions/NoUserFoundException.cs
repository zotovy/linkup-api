using System;

namespace Metadata.Exceptions {
    public class NoUserFoundException : Exception {
        public NoUserFoundException() : base() { }
        public NoUserFoundException(string message) : base(message) { }
        public NoUserFoundException(string message, Exception inner) : base(message, inner) { }
        public NoUserFoundException(int id) : base($"No user found with id={id}") { }

        // A constructor is needed for serialization when an
        // exception propagates from a remoting server to the client.
        protected NoUserFoundException(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}