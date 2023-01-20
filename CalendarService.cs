using CapstoneProject.Data;

namespace CapstoneProject.Services
{
    public class CalendarService
    {
        private List<CustomCalendar> Cal = new();
        public Task<List<CustomCalendar>> LoadCalendar(DateTime date)
        {
            CreateCalendar(date);
            return Task.FromResult(Cal);
        }
        private (int, int, int, int) InitializedDate(DateTime date)
        {
            int daysFirstWeek = (int)date.DayOfWeek;
            int daysInMonth = CountDaysInMonth(date);
            int daysLastWeek = LastWeekDays(date);
            int daysPreviousMonth = CountDaysInMonth(date.AddMonths(-1));

            return (daysFirstWeek, daysInMonth, daysLastWeek, daysPreviousMonth);
        }

        private int LastWeekDays(DateTime month)
        {
            int day = (int)month.AddMonths(1)
                .AddDays(-1)
                .DayOfWeek;

            return CountLastWeekDays(day);
        }
        private int CountLastWeekDays(int day)
        {           
            int numDays = day switch
            {
                1 => 5,
                2 => 4,
                3 => 3,
                4 => 2,
                5 => 1,
                6 => 0,
                _ => 6
            };
            return numDays;
        }
        private void CreateCalendar(DateTime date)
        {
            if(Cal.Count > 0)
                Cal.Clear();

            (int days, int daysInMonth, int daysLast, int daysLastM) = InitializedDate(new(date.Year, date.Month, 1));
            FillCalendar(date, days, daysInMonth, daysLast, daysLastM);
        }
        private void FillCalendar(DateTime date, int daysInWeek, int daysInMonth, int daysLastWeek, int daysPreviousMonth)
        {
            PreviousTiles(date.AddMonths(-1), daysInWeek, daysPreviousMonth);
            PresentAndFollowingTiles(date, daysInMonth);
            PresentAndFollowingTiles(date.AddMonths(1), daysLastWeek, true);
        }
        private void PresentAndFollowingTiles(DateTime date, int limit, bool empty = false)
        {
            for (int i = 1; i <= limit; i++)
            {
                Cal.Add(new CustomCalendar() { Month = new(date.Year, date.Month, i), Empty = empty });
            }
        }
        private void PreviousTiles(DateTime date, int limit, int startDate)
        {
            for (int i = 1; i <= limit; i++)
            {
                int minus = limit - i;
                Cal.Add(new CustomCalendar() { Month = new(date.Year, date.Month, startDate - minus), Empty = true });
            }
        }
        private int CountDaysInMonth(DateTime date)
        {
            return DateTime.DaysInMonth(date.Year, date.Month);
        }
    }
}
