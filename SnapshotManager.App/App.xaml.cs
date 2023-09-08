using System;
using System.ComponentModel;
using System.Windows;
using SnapshotManager.UI;

namespace SnapshotManager.App;

public partial class App : Application
{
    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        var uiFactory = new SnapshotManagerUiFactory();
        var uiContent = uiFactory.GetFullUi();

        var window = new Shell { DataContext = uiContent };
        window.Closing += OnWindowClosing;
        window.Show();
    }

    private void OnWindowClosing(object sender, CancelEventArgs e)
    {
        Console.WriteLine("ICH BIN HIER ZU ENDE");
    }
}