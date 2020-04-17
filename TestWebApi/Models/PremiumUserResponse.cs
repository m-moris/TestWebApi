namespace TestWebApi.Models
{
    public class PremiumUserResponse : UserResponse
    {
        public NestedResponse Nested { get; set; }
    }
}