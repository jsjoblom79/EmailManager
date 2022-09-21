using EmailManager.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EmailManager.Campaign
{
    public class CampaignViewModel : BindableBase
    {
        public CampaignViewModel()
        {
            CurrentCampaign = new Models.Campaign();
            CreateCampaign = new RelayCommand<Models.Campaign>(OnCreateCampaign);
        }


        public RelayCommand<Models.Campaign> CreateCampaign { get; private set; }
        private void OnCreateCampaign(Models.Campaign campaign)
        {
            string path = string.Empty;
            using (SaveFileDialog dialog = new SaveFileDialog())
            {
                dialog.InitialDirectory = @"\\nmslfiles\SourceCode\POB\";
                dialog.Filter = "All Files (*.*)|*.*";

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    path = dialog.FileName;
                }
            }

            Scheduler scheduler = new Scheduler();
            scheduler.CreateCampaign(campaign, path);
        }

        private void UpdateCamp()
        {
            OnPropertyChanged("CurrentCampaign");
        }
        private string _campaignName;

        private string _content;

        private string _to;

        private string _from;

        private string _subject;

        private string _importance;

        private string _sensitivity;

        private string _bypass;

        private Models.Campaign _campaign;

        public Models.Campaign CurrentCampaign
        {
            get { return _campaign; }
            set { SetProperty(ref _campaign, value); }
        }

        public string Bypass
        {
            get { return _bypass; }
            set { SetProperty(ref _bypass, value);
                CurrentCampaign.BypassEmailSend = value;
                UpdateCamp();
            }
        }

        public string Sensitivity
        {
            get { return _sensitivity; }
            set { SetProperty(ref _sensitivity, value);
                CurrentCampaign.Sensitivity = value;
                UpdateCamp();
            }
        }

        public string Importance
        {
            get { return _importance; }
            set { SetProperty(ref _importance, value);
                CurrentCampaign.Importance = value;
                UpdateCamp();
            }
        }

        public string Subject
        {
            get { return _subject; }
            set { SetProperty(ref _subject, value);
                CurrentCampaign.Subject = value;
                UpdateCamp();
            }
        }

        public string From
        {
            get { return _from; }
            set { SetProperty(ref _from, value);
                CurrentCampaign.FromAddress = value;
                UpdateCamp();
            }
        }

        public string To
        {
            get { return _to; }
            set { SetProperty(ref _to, value);
                CurrentCampaign.ReplyTo = value;
                UpdateCamp();
            }
        }

        public string Content
        {
            get { return _content; }
            set { SetProperty(ref _content, value);
                CurrentCampaign.CampaignContent = value;
                UpdateCamp();
            }
        }

        public string CampaignName
        {
            get { return _campaignName; }
            set { SetProperty(ref _campaignName, value);
                CurrentCampaign.CampaignName = value;
                UpdateCamp();
            }
        }

    }
}
