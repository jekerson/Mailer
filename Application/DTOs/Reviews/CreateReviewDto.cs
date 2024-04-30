using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Reviews
{
    public record CreateReviewDto(
        int UserId,
        int SendingId,
        int Rating,
        string Comment);
}
