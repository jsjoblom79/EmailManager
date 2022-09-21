using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailManager.Models
{
    public class Recipient
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int CampaignId { get; set; }
        public string MergeField { get; set; }
        public string GetDate { get; set; }
        public int Neg1 { get; set; }
        public string EmptySpace { get; set; }
        public string EmailAddress { get; set; }
        public string RecipientType { get; set; }

        public override string ToString()
        {
            return $"('{FirstName}','{LastName}',CampaignId,'@@_EMAIL@@={EmailAddress}^@@FIRSTNAME@@={FirstName}^@@LASTNAME@@={LastName}',GetDate(),-1,'','{EmailAddress}','New')";
        }
    }
}
