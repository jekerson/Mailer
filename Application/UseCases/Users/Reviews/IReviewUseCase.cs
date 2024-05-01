using Application.DTOs.Reviews;
using Domain.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Users.ReviewManagment
{
    public interface IReviewUseCase
    {
        Task<Result> CreateReviewAsync(CreateReviewDto createReviewDto);
        Task<Result> DeleteReviewAsync(DeleteReviewDto deleteReviewDto);
    }
}
