using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TestWebApi.Models;

namespace TestWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpGet]
        public async Task<UserResponse> GetAsync()
        {
            return await Task.FromResult(new UserResponse
            {
                Id = 1,
                Name = "Tom",
                Code = 1024
            });
        }

        [HttpPost]
        public async Task<UserResponse> CreateAsync(UserCreateRequest req)
        {
            return await Task.FromResult(new UserResponse
            {
                Id = req.Id,
                Name = req.Name,
                Code = req.Code,
            });
        }

        [HttpPatch]
        public async Task<UserResponse> UpdateAsync(UserUpdateRequest req)
        {
            var user = new UserResponse
            {
                Id = 7777,
                Name = "John",
                Code = 4989,
            };

            return await Task.FromResult(req.Update(user));
        }

        [HttpPatch("nested")]
        public async Task<PremiumUserResponse> UpdateAsync(PremiumUserRequest req)
        {
            var user = new PremiumUserResponse
            {
                Id = 8888,
                Name = "Tom",
                Code = 5858,
                Nested = new NestedResponse
                {
                    Id = 1,
                    Name = "Jerry"
                }
            };

            return await Task.FromResult(req.Update(user));
        }
    }

    public class PremiumUserRequest : UserUpdateRequest
    {
        public NestedResponse Nested { get; set; }

    }

    public class NestedResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class PremiumUserResponse : UserResponse
    {
        public NestedResponse Nested { get; set; }
    }
}