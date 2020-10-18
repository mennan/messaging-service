using System;
using MessagingService.Service;

namespace MessagingService.Api
{
    public interface IJwtHelper
    {
        string GetJwtSecret();
    }
    
    public class JwtHelper : IJwtHelper
    {
        public string GetJwtSecret()
        {
            return Environment.GetEnvironmentVariable(Constants.JwtSecretEnvironmentVariableName);
        }
    }
}