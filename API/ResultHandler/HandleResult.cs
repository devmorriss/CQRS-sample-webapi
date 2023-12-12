using Application.Core;

namespace API.ResultHandler
{
    public static class HandleResult
    {
        public static IResult MyHandleResult<T>(Result<T> result)
        {
            if (result == null) return TypedResults.NotFound();
            if (result.IsSuccess && result.Value != null)
                return TypedResults.Ok(result.Value);

            if (result.IsSuccess && result.Value == null)
                return TypedResults.NotFound();

            return TypedResults.BadRequest(result.Error);
        }
    }
}