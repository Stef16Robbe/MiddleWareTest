using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using MiddleWareTest.Domain;

namespace MiddleWareTest.Functions
{
    public class UserFunctions
    {
        [Function("GetUserById")]
        [Authorized(new UserType[] {UserType.Admin, UserType.Owner})]
        public HttpResponseData GetUserById([HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequestData req,
            FunctionContext executionContext)
        {
            var logger = executionContext.GetLogger("UserFunctions");
            logger.LogInformation("C# HTTP trigger function processed a request.");
            var service = new UserService();


            var authAttribute = Helpers.GetAuthorizedAttributes<UserFunctions>(nameof(GetUserById));
            // passed down to us by our middleware
            if (!AuthorizationService.UserAuthorized(authAttribute.AllowedUserTypes, executionContext.Items["Email"].ToString())) throw new UnauthorizedGoogleException();

            var response = req.CreateResponse(HttpStatusCode.OK);
            response.Headers.Add("Content-Type", "text/plain; charset=utf-8");

            response.WriteString("Welcome to Azure Functions!");

            return response;
        }
    }

    public static class AuthorizationService
    {
        // sample service, the idea is that you check for Authorization in the servicelayer,
        // using the Attributes on each function
        public static bool UserAuthorized(UserType[] allowedUserTypes, string currentEmail)
        {
            Console.WriteLine(allowedUserTypes.ToString());
            var currentUser = UserRepository.GetCurrentUser(currentEmail);
            // normally you'd get the user type from current user obj
            var allowedUserTypesList = allowedUserTypes.ToList();
            // TODO:
            // Checking if isowner of object to retrieve...
            

            return allowedUserTypesList.Any(ut => ut.Equals(currentUser));
        }
    }

    public class UserService
    {
        
        public string GetUserById()
        {
            
            return "";
        }
    }

    public static class UserRepository
    {
        public static string GetCurrentUser(string email)
        {
            // TODO:
            // in reality, here's where you grab the current user by their google email from your DB...

            return "";
        }
    }
}
