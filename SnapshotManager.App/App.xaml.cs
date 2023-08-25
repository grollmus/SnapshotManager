using System;
using System.ComponentModel;
using System.Windows;
using SnapshotManager.UI.ViewModels;

namespace SnapshotManager.App;

public partial class App : Application
{
    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);
        
        var content = new MasterViewModel();

        var window = new Shell { DataContext = content };
        window.Closing += OnWindowClosing;
        window.Show();
    }

    private void OnWindowClosing(object sender, CancelEventArgs e)
    {
        Console.WriteLine("ICH BIN HIER ZU ENDE");
    }
}