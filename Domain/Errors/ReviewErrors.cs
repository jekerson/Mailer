using Domain.Abstraction;

namespace Domain.Errors
{
    public static class ReviewErrors
    {
        public static Error ReviewNotFoundById(int id) =>
            Error.NotFound("Review.NotFoundById", $"Review with ID {id} was not found.");
    }
}
