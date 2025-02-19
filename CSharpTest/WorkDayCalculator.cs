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
            
            dayCount = Math.Max(dayCount - 1, 0);
            //Could thow an exception in case of zero or negative days count
            //if(dayCount <= 0)
            //{
                //throw new ArgumentException($"{nameof(dayCount)} argument value less then zero. Provided value: {dayCount}");
            //}

            var endDate = startDate.AddDays(dayCount);
            var intersectedWeekends = weekEnds?
                .Where(we => we.StartDate <= endDate && we.EndDate >= startDate)
                .ToList() ?? new List<WeekEnd>();

            var totalDaysSpent = intersectedWeekends
                .Select(iw => ((iw.StartDate <= startDate && iw.EndDate >= startDate) ? iw.EndDate - startDate : iw.EndDate - iw.StartDate).Add(TimeSpan.FromDays(1)))
                .Aggregate(TimeSpan.Zero, (summ, timeSpanItem) => summ.Add(timeSpanItem));

            totalDaysSpent = totalDaysSpent.Add(TimeSpan.FromDays(dayCount));

            var result = startDate.Add(totalDaysSpent);
            return result;
        }
    }
}
