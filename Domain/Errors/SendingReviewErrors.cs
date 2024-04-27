using Domain.Abstraction;

namespace Domain.Errors
{
    public static class SendingReviewErrors
    {
        public static Error SendingReviewNotFoundById(int id) =>
            Error.NotFound("SendingReview.NotFoundById", $"Sending review with ID {id} was not found.");

        public static Error SendingReviewAlreadyExist(int sendingId, int reviewId) =>
            Error.Conflict("SendingReview.AlreadyExist", $"Sending review with Sending ID {sendingId} and Review ID {reviewId} already exists.");
    }
}
