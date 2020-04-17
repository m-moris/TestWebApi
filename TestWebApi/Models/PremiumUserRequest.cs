namespace TestWebApi.Models
{
    public class PremiumUserRequest : UserUpdateRequest
    {
        public NestedResponse Nested { get; set; }

    }
}