using SnapshotManager.UI.Mvvm;
using System;

namespace SnapshotManager.UI.ViewModels;

public class SettingsViewModel : ViewModelBase
{
    private string _message;

    internal SettingsViewModel()
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
        Random rnd = new Random();
        int number = rnd.Next();

        Message = $"Hallo ich bin da ({number})";
    }
}