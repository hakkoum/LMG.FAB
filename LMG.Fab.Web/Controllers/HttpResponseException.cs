using System;
using System.Runtime.Serialization;

namespace LMG.Fab.Web.Controllers
{
    [Serializable]
    internal class HttpResponseException : Exception
    {
        private object response;

        public HttpResponseException()
        {
        }

        public HttpResponseException(object response)
        {
            this.response = response;
        }

        public HttpResponseException(string message) : base(message)
        {
        }

        public HttpResponseException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected HttpResponseException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}