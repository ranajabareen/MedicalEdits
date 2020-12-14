using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace ProviderApps.Core.Classes
{
    /// <summary>
    /// Base Process result.
    /// </summary>
    public abstract class BaseProcessResult
    {
        private Dictionary<string, string> _modelErrors = new Dictionary<string, string>();

        public void AddModelError(string key, string errorMessage)
        {
            if (key == null)
                throw new ArgumentNullException(nameof(key));
            if (errorMessage == null)
                throw new ArgumentNullException(nameof(errorMessage));
            if (!_modelErrors.ContainsKey(key))
            {
                _modelErrors.Add(key, errorMessage);
            }
        }

        public void AddRangeModelError(Dictionary<string, string> errorDictionary)
        {
            if (errorDictionary != null && errorDictionary.Count > 0)
            {
                if (_modelErrors == null)
                {
                    _modelErrors = new Dictionary<string, string>();
                }
                _modelErrors = _modelErrors.Concat(errorDictionary).GroupBy(d => d.Key).ToDictionary(d => d.Key, d => d.First().Value);

            }
        }

        public Dictionary<string, string> GetModelErrors()
        {
            return _modelErrors;
        }
    }

    public class ProcessResult<T> : BaseProcessResult
    {
        public ProcessResult()
        {
            Errors = new List<string>();
        }
        public string Message { get; set; }
        public bool Succeeded { get; set; }

        public HttpStatusCode StatusCode { get; set; }
        public T DataResult { get; set; }
        public List<string> Errors { get; set; }

        public void AddErrorMessage(string message)
        {
            if (Errors == null)
                Errors = new List<string>();
            Errors.Add(message);
        }
    }

    public class ProcessResult : BaseProcessResult
    {
        public string Message { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public List<string> Errors { get; set; }
        public bool Succeeded { get; set; }

        public void AddErrorMessage(string message)
        {
            if (Errors == null)
                Errors = new List<string>();
            Errors.Add(message);
        }
    }
}
