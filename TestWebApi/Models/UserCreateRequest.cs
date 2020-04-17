using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace TestWebApi.Models
{
    /// <summary>
    /// 作成
    /// </summary>
    public class UserCreateRequest
    {
        [Required, JsonProperty(Required = Required.Always)]
        public int Id { get; set; }

        [Required, MaxLength(10)]
        public string Name { get; set; }

        [Required]
        public int Code { get; set; }
    }
}