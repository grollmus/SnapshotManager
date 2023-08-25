using SnapshotManager.Core;
using SnapshotManager.UI.ViewModels;
using SnapshotManager.UI.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace SnapshotManager.UI
{
    public class UiFactory
    {
        public ContentControl GetSnapshotManagerWindow(SnapshotManagerTypes snapshotManagerType)
        {
            switch (snapshotManagerType)
            {
                case SnapshotManagerTypes.Settings:
                    return new SettingsView { DataContext = new SettingsViewModel() };

                default:
                    return null;
            }
        }
    }
}
