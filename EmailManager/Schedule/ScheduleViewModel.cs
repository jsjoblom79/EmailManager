using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EmailManager.Models;
using EmailManager.Services;

namespace EmailManager.Schedule
{
    public class ScheduleViewModel: BindableBase
    {
        public ScheduleViewModel()
        {
            CurrentSchedule = new Models.Schedule();
            CreateSchedule = new RelayCommand<Models.Schedule>(OnCreateSchedule);
            Frequencies = new ObservableCollection<Models.Frequency>
            {
                new Frequency{ Id=1, Name = "One Time"},
                new Frequency { Id = 2, Name = "Repeat" }
            };

        }

        private Models.Schedule _currentSchedule;

        private string _scheduleName;

        private string _scheduleStartDate;

        private string _scheduleStartTime;

        private string _frequency;

        private string _scheduleEndDate;

        private string _nextRun;

        private ObservableCollection<Models.Frequency> _frequencies;

        public ObservableCollection<Models.Frequency> Frequencies
        {
            get { return _frequencies; }
            set { SetProperty(ref _frequencies, value); }
        }

        public string NextRun
        {
            get { return _nextRun; }
            set { SetProperty(ref _nextRun, value);
                CurrentSchedule.NextRunDate = value.ToDate();
                PropChng();
            }
        }

        public string ScheduleEndDate
        {
            get { return _scheduleEndDate; }
            set { SetProperty(ref _scheduleEndDate, value);
                CurrentSchedule.ScheduleEndDate = value.ToDate();
                PropChng();
            }
        }

        public string Frequency
        {
            get { return _frequency; }
            set { SetProperty(ref _frequency, value);
                CurrentSchedule.Frequency = value;
                PropChng();
            }
        }

        public string ScheduleStartTime
        {
            get { return _scheduleStartTime; }
            set { SetProperty(ref _scheduleStartTime, value);
                CurrentSchedule.ScheduleTime = value.ToTime();
                if (ScheduleStartDate != null)
                {
                    NextRun = $"{ScheduleStartDate} {ScheduleStartTime}";
                }
                PropChng();
            }
        }

        public string ScheduleStartDate
        {
            get { return _scheduleStartDate; }
            set { SetProperty(ref _scheduleStartDate, value);
                ScheduleEndDate = value;
                OnPropertyChanged("ScheduleEndDate");
                CurrentSchedule.ScheduleStartDate = value.ToDate();
                PropChng();
            }
        }


        public string ScheduleName
        {
            get { return _scheduleName; }
            set { SetProperty(ref _scheduleName, value);
                CurrentSchedule.ScheduleName = _scheduleName;
                PropChng();
            }
        }

        public Models.Schedule CurrentSchedule
        {
            get { return _currentSchedule;  }
            set { SetProperty(ref _currentSchedule, value);}
        }


        public RelayCommand<Models.Schedule> CreateSchedule { get; private set; }

        private void PropChng()
        {
            OnPropertyChanged("CurrentSchedule");
        }
        private void OnCreateSchedule(Models.Schedule schedule)
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
            if (!path.Equals(string.Empty))
            {
                scheduler.CreateSchedule(schedule, path);
            }
           
        }
    }
}
