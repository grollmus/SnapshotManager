using SnapshotManager.UI.Mvvm;

namespace SnapshotManager.UI.ViewModels;

internal class MasterViewModel : ViewModelBase
{
    private SettingsViewModel _settingsArea;
    private SnapshotsViewerViewModel _snapshotsViewer;

    public MasterViewModel()
    {
        SnapshotsViewer = new SnapshotsViewerViewModel();
        SettingsArea = new SettingsViewModel();
    }

    public SettingsViewModel SettingsArea
    {
        get => _settingsArea;
        set
        {
            _settingsArea = value;
            OnPropertyChanged();
        }
    }

    public SnapshotsViewerViewModel SnapshotsViewer
    {
        get => _snapshotsViewer;
        set
        {
            _snapshotsViewer = value;
            OnPropertyChanged();
        }
    }
}