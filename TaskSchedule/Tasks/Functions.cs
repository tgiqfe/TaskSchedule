using System.Text;

namespace TaskSchedule.Tasks
{
    internal class Functions
    {
        /// <summary>
        /// PnYnMnDTnHnMnS形式で返す (年と月は対象外)
        /// </summary>
        /// <param name="ts"></param>
        /// <returns></returns>
        public static string ToText(TimeSpan ts)
        {
            var sb = new StringBuilder();
            if (ts.Days > 0)
            {
                sb.Append($"{ts.Days}D");
            }
            if (ts.Hours > 0 || ts.Minutes > 0 || ts.Seconds > 0)
            {
                sb.Append("T");
            }
            if (ts.Hours > 0)
            {
                sb.Append($"{ts.Hours}H");
            }
            if (ts.Minutes > 0)
            {
                sb.Append($"{ts.Minutes}M");
            }
            if (ts.Seconds > 0)
            {
                sb.Append($"{ts.Seconds}S");
            }

            return sb.ToString();
        }

        public static string ToText(TimeSpan? ts)
        {
            return ToText((TimeSpan)ts);
        }

        public static string ToText(DateTime dt)
        {
            return dt.ToString("yyyy-MM-ddTHH:mm:ss");
        }

        public static string ToText(DateTime? dt)
        {
            return ToText((DateTime)dt);
        }
    }
}
