using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Excel = Microsoft.Office.Interop.Excel;
using EmailManager.Models;
using System.Windows.Forms;

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

        public bool GetRecipients(string path)
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
                    EmailAddress = ((string)xlWrkSht.Cells[row, "D"].Text).IsValidEmail()
                };
                list.Add(recipient);
            }

            xlWrkBk.Close();
            xlApp.Quit();
           return SplitRecipientList(list.Where(r => r.EmailAddress != null).ToList());
        }

        private bool SplitRecipientList(List<Models.Recipient> recipients)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("USE CampaignManager \n GO");
            sb.AppendLine("INSERT INTO [dbo].[Recipients] \n VALUES");
            int totalGroupSize = 0;
            if(recipients.Count > 3000)
            {
                totalGroupSize = recipients.Count / 3;
            }
            else
            {
                totalGroupSize = recipients.Count;
            }
            
            var partitions = recipients.Partition(totalGroupSize);
            int counter = 1;
            string folderPath = string.Empty;

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.InitialDirectory = @"\\nmslfiles\SourceCode\POB\";
            sfd.FileName = "";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                folderPath = Path.GetDirectoryName(sfd.FileName);
            }
            foreach (var part in partitions)
            {
                File.WriteAllText($@"{folderPath}\{counter}_Recipient.sql", WritePartitionList(part));
                counter++;
            }
            return true;
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
