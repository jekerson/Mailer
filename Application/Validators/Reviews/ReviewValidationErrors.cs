using Domain.Abstraction;

namespace Application.Validators.Reviews
{
    public static class ReviewValidationErrors
    {
        public static Error InvalidUserId => Error.Validation(
            "CreateReview.InvalidUserId",
            "The provided user ID is invalid.");

        public static Error InvalidSendingId => Error.Validation(
            "CreateReview.InvalidSendingId",
            "The provided sending ID is invalid.");
        public static Error InvalidRating => Error.Validation(
            "CreateReview.InvalidRating",
            "The rating must be between 1 and 5.");

        public static Error CommentRequired => Error.Validation(
            "CreateReview.CommentRequired",
            "The comment is required.");

    }
}
