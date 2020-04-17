namespace TestWebApi.Models
{
    public interface IPatch
    {
        public string Raw { get; set; }
        public T Update<T>(T dest);
    }
}