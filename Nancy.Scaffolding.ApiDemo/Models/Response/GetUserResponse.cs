using Newtonsoft.Json;

namespace Nancy.Scaffolding.ApiDemo.Models.Request
{
    public class GetUserResponse
    {
        public string FirstName { get; set; }

        [JsonProperty("teste")]
        public string LastName { get; set; }

        [JsonIgnore]
        public string Email { get; set; }

        public string Password { get; set; }
    }
}
