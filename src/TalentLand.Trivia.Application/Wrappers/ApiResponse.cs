namespace TalentLand.Trivia.Application.Wrappers
{
    public class ApiResponse<T>
    {
        public ApiResponse() { }

        public ApiResponse(T data)
        {
            Data = data;
        }

        public string Status { get; set; } = "Success";

        public T Data { get; set; } = default!;
    }
}
