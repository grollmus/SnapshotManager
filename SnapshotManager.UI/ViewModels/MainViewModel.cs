using SnapshotManager.UI.Mvvm;

namespace SnapshotManager.UI.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private string _message;

        public MainViewModel()
        {
            CreateCommands();
        }

        public string Message
        {
            get => _message;
            set
            {
                _message = value;
                OnPropertyChanged();
            }
        }

        public DelegateCommand OpenFileCommand { get; set; }

        private void CreateCommands()
        {
            OpenFileCommand = new DelegateCommand(OnOpenFileCommandExecute, OnOpenFileCommandCanExecute);
        }

        private bool OnOpenFileCommandCanExecute()
        {
            return false;
        }

        private void OnOpenFileCommandExecute()
        {
            Message = "Hallo ich bin da";
        }
    }
}