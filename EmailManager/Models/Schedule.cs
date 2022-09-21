using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailManager.Models
{
    public class Schedule
    {
        public DateTime ScheduleStartDate { get; set; }
        public DateTime ScheduleTime { get; set; }
        public string Frequency { get; set; }
        public DateTime ScheduleEndDate { get; set; }
        public DateTime NextRunDate { get; set; }
        public string ScheduleName { get; set; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("USE CampaignManager\nGO");
            sb.AppendLine("INSERT INTO [dbo].[Schedule]");
            sb.AppendLine("([ScheduleStartDate]");
            sb.AppendLine(",[ScheduleTime]");
            sb.AppendLine(",[Frequency]");
            sb.AppendLine(",[ScheduleEndDate]");
            sb.AppendLine(",[NextRunDate]");
            sb.AppendLine(",[ScheduleName])\nVALUES");
            sb.AppendLine($"('{ScheduleStartDate:yyyy-MM-dd}'");
            sb.AppendLine($",'{ScheduleTime:HH:mm:ss}'");
            sb.AppendLine($",'{Frequency}'");
            sb.AppendLine($",'{ScheduleEndDate:yyyy-MM-dd}'");
            sb.AppendLine($",'{NextRunDate:yyyy-MM-dd HH:mm:ss}'");
            sb.AppendLine($",'{ScheduleName}')\nGO");

            return sb.ToString();
        }
    }
}
