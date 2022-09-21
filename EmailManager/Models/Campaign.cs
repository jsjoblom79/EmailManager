using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailManager.Models
{
    public class Campaign
    {
        public string CampaignName { get; set; }
        public string CampaignContent { get; set; }
        public string CampaignType { get; set; }
        public int ScheduleId { get; set; }
        public string BodyFormat { get; set; }
        public string ReplyTo { get; set; }
        public string FromAddress { get; set; }
        public string Subject { get; set; }
        public string Importance { get; set; }
        public string Sensitivity { get; set; }
        public string BypassEmailSend { get; set; }


        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("USE [CampaignManager]");
            sb.AppendLine("GO");
            sb.AppendLine("INSERT INTO [dbo].[Campaign]");
            sb.AppendLine("([CampaignName]");
            sb.AppendLine(",[CampaignContent]");
            sb.AppendLine(",[CampaignType]");
            sb.AppendLine(",[ScheduleId]");
            sb.AppendLine(",[BodyFormat]");
            sb.AppendLine(",[ReplyTo]");
            sb.AppendLine(",[FromAddress]");
            sb.AppendLine(",[Subject]");
            sb.AppendLine(",[Importance]");
            sb.AppendLine(",[Sensitivity]");
            sb.AppendLine(",[BypassEmailSend])");
            sb.AppendLine("VALUES");
            sb.AppendLine($"('{CampaignName}'");
            sb.AppendLine($",'{CampaignContent}'");
            sb.AppendLine($",'Email'");
            sb.AppendLine($",ScheduleID");
            sb.AppendLine($",'HTML'");
            sb.AppendLine($",'{ReplyTo}'");
            sb.AppendLine($",'{FromAddress}'");
            sb.AppendLine($",'{Subject}'");
            sb.AppendLine($",'{Importance}'");
            sb.AppendLine($",'{Sensitivity}'");
            sb.AppendLine($",'{BypassEmailSend}')");

            return sb.ToString();
        }
    }

    
}
