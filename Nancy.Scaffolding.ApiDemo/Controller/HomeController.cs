using Nancy.Scaffolding.ApiDemo.Models.Request;
using Nancy.Scaffolding.Docs.Attributes;
using Nancy.Scaffolding.Mappers;
using Nancy.Scaffolding.Modules;
using Nancy.Swagger.Annotations.Attributes;
using WebApi.Models.Response;

namespace Nancy.Scaffolding.ApiDemo.Controller
{
    public class HomeController : BaseModule
    {
        public HomeController() : base()
        {
            this.Post("users/{id}", args => this.CreateUser(), name: nameof(CreateUser));
        }

        [DescribeJsonResource(typeof(HomeController), nameof(CreateUser))]
        [PathContent("id", typeof(int))]
        [QueryContent("page", typeof(int))]
        [BodyContent(typeof(CreateUserRequest))]
        [SwaggerResponse(HttpStatusCode.OK, Model = typeof(GetUserResponse))]
        [SwaggerResponse(HttpStatusCode.BadRequest, Model = typeof(ErrorsResponse))]
        public object CreateUser()
        {
            var form = this.BindFromForm<CreateUserRequest>();
            var request = this.BindFromBody<CreateUserRequest>();
            var query = this.BindFromQuery<CreateUserRequest>();

            this.ValidateRequest(form);
            this.ValidateRequest(request);
            this.ValidateRequest(query);

            var response = new ApiResponse { StatusCode = System.Net.HttpStatusCode.OK };

            response.Content = GlobalMapper.Map<GetUserResponse>(request);

            return this.CreateJsonResponse(response);
        }
    }
}
