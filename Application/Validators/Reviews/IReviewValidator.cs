using Application.DTOs.Reviews;
using Domain.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validators.Reviews
{
    public interface IReviewValidator
    {
        Task<Result> ValidateAsync(CreateReviewDto createReviewDto);

    }
}
