using System;
using System.ComponentModel;
using System.Windows;
using SnapshotManager.UI;
using SnapshotManager.UI.Views;

namespace SnapshotManager.App;

public partial class App : Application
{
    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        var uiFactory = new SnapshotManagerUiFactory();
        var uiContent = uiFactory.GetSettingsUi();

        var shell = new ShellWrapper { DataContext = uiContent };
        shell.Closing += OnWindowClosing;
        shell.Show();
    }

    private void OnWindowClosing(object sender, CancelEventArgs e)
    {
        Console.WriteLine("ICH BIN HIER ZU ENDE");
    }
}