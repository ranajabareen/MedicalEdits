using System;
using System.Collections.Generic;

namespace ProviderApps.Core.Schemas.MedicalEdits.CustomExceptions
{
    public class MedicalEditsAPIException : Exception
    {
        public string Response { get; private set; }
        public string MedicalEditsExceptionMessage { get; private set; }
        public int StatusCode { get; private set; }

        public Dictionary<string, IEnumerable<string>> Headers { get; private set; }

        public MedicalEditsAPIException(string message, int statusCode, string response, Dictionary<string, IEnumerable<string>> headers, Exception innerException) :
            base(message +  "\nResponse: \n" + response.Substring(0, response.Length >= 512 ? 512 : response.Length), innerException)
        {
            Response = response;
            MedicalEditsExceptionMessage = message;
            Headers = headers;
            StatusCode = statusCode;
        }

        public override string ToString()
        {
            return string.Format("HTTP Response: \n\n{0}\n\n{1}", Response, base.ToString());
        }
    }
}
