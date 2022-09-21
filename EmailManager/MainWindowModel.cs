using EmailManager.Campaign;
using EmailManager.Main;
using EmailManager.Recipient;
using EmailManager.Schedule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailManager
{
    public class MainWindowModel: BindableBase
    {
        private MainMenuViewModel _mainMenu = new MainMenuViewModel();
        private ScheduleViewModel _scheduleView = new ScheduleViewModel();
        private RecipientsViewModel _recipientView = new RecipientsViewModel();
        private CampaignViewModel _campaignView = new CampaignViewModel();

        private BindableBase _currentViewModel;

        public BindableBase CurrentView
        {
            get { return _currentViewModel; }
            set { SetProperty(ref _currentViewModel, value); }
        }

        public MainWindowModel()
        {
            CurrentView = _mainMenu;
            Navigate = new RelayCommand<string>(OnNavigate);
        }


        public RelayCommand<string> Navigate { get; private set; }

        private void OnNavigate(string destination)
        {
            switch (destination)
            {
                case "schedule":
                    CurrentView = _scheduleView;
                    break;
                case "recipients":
                    CurrentView = _recipientView;
                    break;
                case "campaign":
                    CurrentView = _campaignView;
                    break;
                default:
                    CurrentView = _mainMenu;
                    break;
            }
        }
    }
}
