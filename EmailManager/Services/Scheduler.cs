using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Excel = Microsoft.Office.Interop.Excel;
using EmailManager.Models;

namespace EmailManager.Services
{
    public class Scheduler: IScheduler
    {
        public Scheduler()
        {

        }

        public void CreateCampaign(Models.Campaign campaign, string path)
        {
            File.WriteAllText(path, campaign.ToString());
        }

        public void CreateSchedule(Models.Schedule schedule, string path)
        {
            File.WriteAllText(path, schedule.ToString());
        }

        public void GetRecipients(string path)
        {
            Excel.Application xlApp = new Excel.Application();
            Excel.Workbook xlWrkBk = xlApp.Workbooks.Open(path);
            Excel.Worksheet xlWrkSht = xlWrkBk.Sheets["Sheet1"];
            xlApp.DisplayAlerts = false;
            xlWrkSht.Columns.AutoFit();
            int rows = xlWrkSht.UsedRange.Rows.Count;
            List<Models.Recipient> list = new List<Models.Recipient>();
            for (int row = 2; row < rows+1; row++)
            {
                Models.Recipient recipient = new Models.Recipient
                {
                    FirstName = ((string)xlWrkSht.Cells[row, "B"].Text).Clean(),
                    LastName = ((string)xlWrkSht.Cells[row, "C"].Text).Clean(),
                    EmailAddress = ((string)xlWrkSht.Cells[row, "D"].Text).Clean()
                };
                list.Add(recipient);
            }

            xlWrkBk.Close();
            xlApp.Quit();
            SplitRecipientList(list);
        }

        private void SplitRecipientList(List<Models.Recipient> recipients)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("USE CampaignManager \n GO");
            sb.AppendLine("INSERT INTO [dbo].[Recipients] \n VALUES");

            int totalGroupSize = recipients.Count / 3;
            var partitions = recipients.Partition(totalGroupSize);
            int counter = 1;
            foreach (var part in partitions)
            {
                File.WriteAllText($@"\\nmslfiles\SourceCode\POB\24415\{counter}_Recipient_sql.sql", WritePartitionList(part));
                counter++;
            }
        }

        private string WritePartitionList(List<Models.Recipient> lists)
        {
            string mainstring = string.Empty;

            var partitions = lists.Partition(1000);
            foreach (var list in partitions)
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("USE CampaignManager \n GO");
                sb.AppendLine("INSERT INTO [dbo].[Recipients] \n VALUES");
                foreach (Models.Recipient recipient in list)
                {
                    if(list.Count -1 == list.IndexOf(recipient))
                    {
                        sb.AppendLine($"{recipient}\n GO");
                    }
                    else 
                    {
                        sb.AppendLine($"{recipient},");
                    }
                        
                }

                mainstring += sb.ToString();
            }

            return mainstring;
        }
    }
}
