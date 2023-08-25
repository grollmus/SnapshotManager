using SnapshotManager.UI.ViewModels;
using SnapshotManager.UI.Views;

namespace SnapshotManager.UI;

public class SnapshotManagerUiFactory
{
    public IUiContent GetSettingsUi()
    {
        return new SettingsView { DataContext = new SettingsViewModel() };
    }
}