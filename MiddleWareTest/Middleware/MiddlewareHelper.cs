using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;

namespace MiddleWareTest.Middleware
{
    // https://github.com/elmahio/Elmah.Io.Functions.Isolated/blob/main/src/Elmah.Io.Functions.Isolated/FunctionContextExtensions.cs
    internal static class MiddlewareHelper
    {
        // Thank you https://github.com/Azure/azure-functions-dotnet-worker/issues/414#issuecomment-828810635
        public static HttpRequestData GetHttpRequestData(this FunctionContext functionContext)
        {
            try
            {
                var keyValuePair =
                    functionContext.Features.SingleOrDefault(f => f.Key.Name == "IFunctionBindingsFeature");
                if (keyValuePair.Equals(default(KeyValuePair<Type, object>))) return null;
                var functionBindingsFeature = keyValuePair.Value;
                var type = functionBindingsFeature.GetType();
                var inputData =
                    type.GetProperties().Single(p => p.Name == "InputData").GetValue(functionBindingsFeature) as
                        IReadOnlyDictionary<string, object>;
                return inputData?.Values.SingleOrDefault(o => o is HttpRequestData) as HttpRequestData;
            }
            catch
            {
                return null;
            }
        }

        // Thank you https://github.com/Azure/azure-functions-dotnet-worker/issues/414#issuecomment-872818004
        public static HttpResponseData GetHttpResponseData(this FunctionContext functionContext)
        {
            try
            {
                var request = functionContext.GetHttpRequestData();
                if (request == null) return null;
                var response = HttpResponseData.CreateResponse(request);
                var keyValuePair =
                    functionContext.Features.FirstOrDefault(f => f.Key.Name == "IFunctionBindingsFeature");
                if (keyValuePair.Equals(default(KeyValuePair<Type, object>))) return null;
                var functionBindingsFeature = keyValuePair.Value;
                var pinfo = functionBindingsFeature.GetType().GetProperty("InvocationResult");
                pinfo?.SetValue(functionBindingsFeature, response);
                return response;
            }
            catch
            {
                return null;
            }
        }

        internal static void InvokeResult(this FunctionContext context, HttpResponseData response)
        {
            var keyValuePair = context.Features.SingleOrDefault(f => f.Key.Name == "IFunctionBindingsFeature");
            var functionBindingsFeature = keyValuePair.Value;
            var type = functionBindingsFeature.GetType();
            var result = type.GetProperties().Single(p => p.Name == "InvocationResult");
            result.SetValue(functionBindingsFeature, response);
        }
    }
}