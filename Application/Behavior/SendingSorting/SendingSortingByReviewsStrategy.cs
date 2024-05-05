using Application.DTOs.Sendings;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Behavior.SendingSorting
{
    public class SendingSortingByReviewsStrategy : ISendingSortingStrategy
    {
        public IEnumerable<Sending> SortSendings(IEnumerable<Sending> sendings)
        {
            return sendings.OrderByDescending(s => s.SendingScores.First().ReviewScore);
        }
    }

}
