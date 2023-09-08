using SnapshotManager.UI.ViewModels;
using SnapshotManager.UI.Views;

namespace SnapshotManager.UI;

public class SnapshotManagerUiFactory
{
    public IUiContent GetFullUi()
    {
        return new MasterView { DataContext = new MasterViewModel() };
    }

    public IUiContent GetSettingsUi()
    {
        return new SettingsView { DataContext = new SettingsViewModel() };
    }
}