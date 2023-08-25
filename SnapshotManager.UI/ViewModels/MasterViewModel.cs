using SnapshotManager.UI.Mvvm;

namespace SnapshotManager.UI.ViewModels
{
    public class MasterViewModel : ViewModelBase
    {
        private string _message;

        public MasterViewModel()
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
            return true;
        }

        private void OnOpenFileCommandExecute()
        {
            Message = "Hallo ich bin da";
        }
    }
}