using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using TestWebApi.Patch;

namespace TestWebApi.Models
{
    /// <summary>
    /// 部分更新
    /// </summary>
    public class UserUpdateRequest : IPatch
    {
        public int Id { get; set; }

        [MaxLength(10)]
        public string Name { get; set; }

        public int Code { get; set; }

        [JsonIgnore]
        public string Raw { get; set; }

        public T Update<T>(T dest)
        {
            return PatchHelper.Update(Raw, this, dest);
        }
    }
}