using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpTest
{
    public class WorkDayCalculator : IWorkDayCalculator
    {
        public DateTime Calculate(DateTime startDate, int dayCount, WeekEnd[] weekEnds)
        {
            
            dayCount = Math.Max(dayCount, 1);
            //Could thow an exception in case of zero or negative days count
            //if(dayCount <= 0)
            //{
                //throw new ArgumentException($"{nameof(dayCount)} argument value less then zero. Provided value: {dayCount}");
            //}

            var weekendDays = weekEnds?
                .SelectMany(w => Enumerable.Range(0, (w.EndDate - w.StartDate).Days + 1).Select(offset => w.StartDate.AddDays(offset)))
                .ToHashSet() ?? new HashSet<DateTime>();

            return Enumerable.Range(0, int.MaxValue)
                .Select(offset => startDate.AddDays(offset))
                .Where(day => !weekendDays.Contains(day))
                .Take(dayCount)
                .Last();
        }
    }
}
