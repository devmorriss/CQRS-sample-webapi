namespace Application.Core
{
    public class Result<T>
    {
        public T Value { get; set; }
        public bool IsSuccess { get; set; }
        public string Error { get; set; }
        public static Result<T> Success(T value) => new Result<T> { IsSuccess = true, Value = value };
        public static Result<T> Failure(string message) => new Result<T> { IsSuccess = false, Error = message };
    }
}