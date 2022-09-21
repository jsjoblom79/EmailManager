using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmailManager.Services;
using System.Windows.Forms;


namespace EmailManager.Recipient
{
    public class RecipientsViewModel: BindableBase
    {
        public RecipientsViewModel()
        {
            ProcessFile = new RelayCommand<string>(OnProcessFile);
            Browse = new RelayCommand(OnBrowse);
        }


        private string _filePath;
        
        public string  FilePath
        {
            get { return _filePath; }
            set { SetProperty(ref _filePath, value); }
        }


        public RelayCommand<string> ProcessFile { get; private set; }

        public RelayCommand Browse { get; private set; }

        private void OnBrowse()
        {
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.InitialDirectory = @"\\nmslfiles\SourceCode\POB\";
                dialog.Filter = "All files (*.*)|*.*";

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    FilePath = dialog.FileName;
                    OnPropertyChanged("FilePath");
                }
            }
           
        }
        private async void OnProcessFile(string path)
        {
            Scheduler scheduler = new Scheduler();
            await Task.Run(() => scheduler.GetRecipients(path));
        }

    }
}
