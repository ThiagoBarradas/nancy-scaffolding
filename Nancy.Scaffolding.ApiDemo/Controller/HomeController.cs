using Nancy.Scaffolding.ApiDemo.Models.Request;
using Nancy.Scaffolding.Docs.Attributes;
using Nancy.Scaffolding.Mappers;
using Nancy.Scaffolding.Modules;
using Nancy.Serilog.Simple;
using Nancy.Swagger.Annotations.Attributes;
using System;
using System.Threading;
using WebApi.Models.Response;

namespace Nancy.Scaffolding.ApiDemo.Controller
{
    public class HomeController : BaseModule
    {
        public AdditionalInfo AdditionalInfo { get; set; }

        public Test Test { get; set; }

        public HomeController(AdditionalInfo additionalInfo, Test test)
        {
            this.Test = test;
            this.AdditionalInfo = additionalInfo;
            this.Get("users/{id}", args => this.CreateUser(), name: nameof(CreateUser));
        }

        [DescribeJsonResource(typeof(HomeController), nameof(CreateUser))]
        [PathContent("id")]
        [QueryContent("page", typeof(int))]
        [BodyContent(typeof(CreateUserRequest))]
        [HeaderContent("Authorization")]
        [SwaggerResponse(HttpStatusCode.OK, Model = typeof(GetUserResponse))]
        [SwaggerResponse(HttpStatusCode.BadRequest, Model = typeof(ErrorsResponse))]
        public object CreateUser()
        {
            var form = this.BindFromForm<CreateUserRequest>();
            var request = this.BindFromBody<CreateUserRequest>();
            var query = this.BindFromQuery<CreateUserRequest>();

            this.AdditionalInfo.Data.Add("AccountId", query.FirstName);
            this.AdditionalInfo.Data.Add("MerchantId", query.LastName);

            while (query.FirstName == "acc_123")
            {
                Thread.Sleep(1000);
                Console.WriteLine("Query {0}", query.FirstName);
                Console.WriteLine("AccountId {0}", this.AdditionalInfo.Data["AccountId"]);
                Console.WriteLine("MerchantId {0}", this.AdditionalInfo.Data["MerchantId"]);
            }

            Test.Testing();

            //this.ValidateRequest(form);
            //this.ValidateRequest(request);
            //this.ValidateRequest(query);

            var response = new ApiResponse { StatusCode = System.Net.HttpStatusCode.OK };

            response.Content = GlobalMapper.Map<GetUserResponse>(request);

            return this.CreateJsonResponse(response);
        }
    }

    public class Test
    {
        public AdditionalInfo AdditionalInfo { get; set; }

        public Test(AdditionalInfo additionalInfo)
        {
            this.AdditionalInfo = additionalInfo;
        }

        public void Testing()
        {
            Console.WriteLine("Testing");
            Console.WriteLine(AdditionalInfo.Data["AccountId"]);
            Console.WriteLine(AdditionalInfo.Data["MerchantId"]);
        }
    }
}
