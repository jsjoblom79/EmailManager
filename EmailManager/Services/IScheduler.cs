using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailManager.Services
{
    public interface IScheduler
    {
        void CreateSchedule(Models.Schedule schedule, string path);
        void CreateCampaign(Models.Campaign campaign, string path);
        void GetRecipients(string path);
    }
}
