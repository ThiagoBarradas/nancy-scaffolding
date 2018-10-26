using Nancy.Extensions;
using Nancy.IO;
using Nancy.ModelBinding;
using Nancy.Validation;
using Newtonsoft.Json;
using PackUtils;
using System.Linq;
using WebApi.Models.Exceptions;
using WebApi.Models.Response;

namespace Nancy.Scaffolding.Modules
{
    public abstract class BaseModule : NancyModule
    {
        public BaseModule() : base(GetModulePath()) { }

        public BaseModule(string path) : base(GetModulePath(path)) { }

        public static JsonSerializerSettings JsonSerializerSettings { get; set; } = new JsonSerializerSettings();

        public object CreateJsonResponse(ApiResponse response)
        {
            HttpStatusCode statusCode = response.StatusCode.ConvertToEnum<HttpStatusCode>();
            Response nancyResponse = Response.AsJson(response.Content, statusCode);

            if (response.Headers != null)
            {
                foreach (var header in response.Headers)
                {
                    nancyResponse.Headers[header.Key] = header.Value;
                }
            }

            return nancyResponse;
        }

        public void ValidateSignatureFromHeaderWithContent(string secretKey, string headerName)
        {
            var signature = this.Request.Headers[headerName].FirstOrDefault() ?? string.Empty;
            var content = RequestStream.FromStream(this.Request.Body).AsString() ?? string.Empty;

            var result = SignatureUtility.ValidateSignature(signature, secretKey, content);

            if (result == false)
            {
                throw new UnauthorizedException();
            }
        }

        public void ValidateRequest<TRequest>(TRequest request) where TRequest : class, new()
        {
            var validationResult = this.Validate(request);

            if (validationResult.IsValid == false)
            {
                ErrorsResponse errors = this.CastModelValidationResultToErrorsResponse(validationResult);
                throw new BadRequestException(errors);
            }
        }

        private ErrorsResponse CastModelValidationResultToErrorsResponse(ModelValidationResult validationResult)
        {
            ErrorsResponse errorsResponse = new ErrorsResponse();

            foreach (var errorPerProperty in validationResult.Errors)
            {
                foreach (var errorDetail in errorPerProperty.Value)
                {
                    errorsResponse.AddError(errorDetail, errorPerProperty.Key);
                }
            }

            return errorsResponse;
        }

        public TRequest BindFromBody<TRequest>() where TRequest : class, new()
        {
            var bindingConfig = new BindingConfig
            {
                BodyOnly = true
            };

            return this.Bind<TRequest>(bindingConfig);
        }

        public TRequest BindFromQuery<TRequest>() where TRequest : class, new()
        {
            if (this.Request?.Query == null)
            {
                return default(TRequest);
            }

            var jsonString = JsonConvert.SerializeObject(this.Request.Query.ToDictionary(), JsonSerializerSettings);

            return JsonConvert.DeserializeObject<TRequest>(jsonString, JsonSerializerSettings);
        }

        public TRequest BindFromForm<TRequest>() where TRequest : class, new()
        {
            if (this.Request?.Form == null)
            {
                return default(TRequest);
            }

            var jsonString = JsonConvert.SerializeObject(this.Request.Form.ToDictionary(), JsonSerializerSettings);

            return JsonConvert.DeserializeObject<TRequest>(jsonString, JsonSerializerSettings);
        }

        public TRequest BindFromPath<TRequest>() where TRequest : class, new()
        {
            if (this.Context.Parameters == null)
            {
                return default(TRequest);
            }

            var jsonString = JsonConvert.SerializeObject(this.Context.Parameters.ToDictionary(), JsonSerializerSettings);

            return JsonConvert.DeserializeObject<TRequest>(jsonString, JsonSerializerSettings);
        }

        public static string GetModulePath(string path = null)
        {
            var globalPrefix = Api.ApiSettings.PathPrefix?.Trim('/');
            var currentPath = path?.Trim('/');

            var finalPath = "";

            if (string.IsNullOrEmpty(globalPrefix) == false)
            {
                finalPath += $"/{globalPrefix}";
            }

            if (string.IsNullOrEmpty(currentPath) == false)
            {
                finalPath += $"/{currentPath}";
            }

            return finalPath;
        }
    }
}
