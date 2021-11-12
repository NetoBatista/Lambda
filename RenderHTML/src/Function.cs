using System.Net;
using System.Text.Json;
using Amazon.Lambda.Core;
using RazorEngine;
using RenderHTML.Model;
using RazorEngine.Templating;
using System;
using Newtonsoft.Json.Linq;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace RenderHTML
{
    public class Function
    {
        /// <param name="request"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public Output FunctionHandler(RenderInput request, ILambdaContext context)
        {
            if (request == null)
            {
                return new Output
                {
                    status_code = (int)HttpStatusCode.BadRequest,
                };
            }

            if (string.IsNullOrEmpty(request.html))
            {
                return new Output
                {
                    status_code = (int)HttpStatusCode.BadRequest,
                    response = "Html not found"
                };
            }

            if (string.IsNullOrEmpty(request.model))
            {
                return new Output
                {
                    status_code = (int)HttpStatusCode.BadRequest,
                    response = "Model not found"
                };
            }

            try
            {
                var response = Engine.Razor.RunCompile(request.html, Guid.NewGuid().ToString(), null, JObject.Parse(request.model));
                return new Output
                {
                    status_code = (int)HttpStatusCode.OK,
                    response = response.ToString()
                };

            }
            catch (Exception ex)
            {
                return new Output
                {
                    status_code = (int)HttpStatusCode.BadRequest,
                    response = ex.Message
                };
            }
        }
    }
}
