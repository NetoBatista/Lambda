using System.Net;
using Amazon.Lambda.Core;
using SendMail.Model;
using System;
using Amazon.Lambda.APIGatewayEvents;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Text.Json;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace SendMail
{
    public class Function
    {
        /// <param name="request"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public APIGatewayProxyResponse FunctionHandler(APIGatewayProxyRequest request, ILambdaContext context)
        {
            if (string.IsNullOrEmpty(request.Body))
            {
                return new APIGatewayProxyResponse
                {
                    Body = "Body not found",
                    StatusCode = 400
                };
            }

            var input = JsonSerializer.Deserialize<Input>(request.Body);

            if (string.IsNullOrEmpty(input.title))
            {
                return new APIGatewayProxyResponse
                {
                    Body = "Title not found",
                    StatusCode = 400
                };
            }

            if (string.IsNullOrEmpty(input.to))
            {
                return new APIGatewayProxyResponse
                {
                    Body = "To email not found",
                    StatusCode = 400
                };
            }

            if (string.IsNullOrEmpty(input.message))
            {
                return new APIGatewayProxyResponse
                {
                    Body = "Message not found",
                    StatusCode = 400
                };
            }


            try
            {
                SendMail(input);
                return new APIGatewayProxyResponse
                {
                    Body = "Email sent",
                    StatusCode = 200
                };

            }
            catch (Exception ex)
            {
                return new APIGatewayProxyResponse
                {
                    Body = ex.Message,
                    StatusCode = 500
                };
            }
        }

        private void SendMail(Input input)
        {
            string email = Environment.GetEnvironmentVariable("EMAIL");
            string port = Environment.GetEnvironmentVariable("PORT");
            string username = Environment.GetEnvironmentVariable("USERNAME");
            string password = Environment.GetEnvironmentVariable("PASSWORD");
            string provider = Environment.GetEnvironmentVariable("PROVIDER");

            MailMessage mensagemEmail = new MailMessage(email, input.to, input.title, input.message);
            mensagemEmail.IsBodyHtml = input.isHtml;

            SmtpClient smptClient = new SmtpClient();
            smptClient.Port = int.Parse(port);
            smptClient.Host = provider;
            smptClient.UseDefaultCredentials = false;
            smptClient.EnableSsl = true;

            smptClient.Credentials = new NetworkCredential(username, password);

            smptClient.Send(mensagemEmail);
        }
    }
}
