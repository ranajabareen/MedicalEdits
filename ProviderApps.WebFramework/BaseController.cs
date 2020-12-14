using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using ProviderApps.Core.Classes;
using ProviderApps.WebFramework.Models;

namespace ProviderApps.WebFramework
{
    [ApiController]
    [Produces("application/json")]
    [Route("api/[Controller]")]
    [Authorize]
    public abstract class BaseController : ControllerBase
    {
        [NonAction]
        public async Task<IActionResult> GetResponse<T>(ProcessResult<T> processResult)
        {
            var responseWrapper = new ResponseWrapper()
            {
                Data = processResult.DataResult,
            };

            if (!string.IsNullOrEmpty(processResult.Message))
            {
                responseWrapper.Messages = new List<UIMessage>() { new UIMessage() { Body = processResult.Message } };
            }

            switch (processResult.StatusCode)
            {
                case HttpStatusCode.OK:
                    return await Task.FromResult(Ok(responseWrapper));
                case HttpStatusCode.Created:
                    return await Task.FromResult(StatusCode(StatusCodes.Status201Created, responseWrapper));
                case HttpStatusCode.NoContent:
                    return await Task.FromResult(StatusCode(StatusCodes.Status204NoContent, responseWrapper));
                case HttpStatusCode.BadRequest:
                    {

                        // check if there is  ModelError
                        if (processResult.Errors != null && processResult.Errors.Count > 0)
                        {
                            foreach (var error in processResult.Errors)
                            {
                                responseWrapper.Messages.Add(new UIMessage() { Body = error });
                            }
                        }

                        if (processResult.GetModelErrors().Count > 0)
                        {
                            foreach (KeyValuePair<string, string> error in processResult.GetModelErrors())
                            {
                                responseWrapper.Messages.Add(new UIMessage() { Body = error.Value, Key = error.Key });
                            }
                        }

                        return await Task.FromResult(base.BadRequest(responseWrapper));
                    }

                case HttpStatusCode.Unauthorized:
                    return await Task.FromResult(base.Unauthorized(responseWrapper));
                case HttpStatusCode.PaymentRequired:
                    break;
                case HttpStatusCode.Forbidden:
                    return await Task.FromResult(base.Forbid(processResult.Message));
                case HttpStatusCode.NotFound:
                    return await Task.FromResult(base.NotFound(responseWrapper));
                case HttpStatusCode.MethodNotAllowed:
                    return await Task.FromResult(StatusCode(StatusCodes.Status405MethodNotAllowed, responseWrapper));
                case HttpStatusCode.RequestTimeout:
                    return await Task.FromResult(StatusCode(StatusCodes.Status408RequestTimeout, responseWrapper));

                case HttpStatusCode.Conflict:
                    return await Task.FromResult(base.Conflict(processResult.Message));
   
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return await Task.FromResult(BadRequest(responseWrapper));
        }    
    }
}
