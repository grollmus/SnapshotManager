using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using SnapshotManager.Core.Reflection;

namespace SnapshotManager.UI.Mvvm
{
    public class ViewModelBase : INotifyPropertyChanged, IDisposable
    {
        [Display(AutoGenerateField = false)]
        public bool IsDisposed { get; private set; }

        protected void OnPropertyChanged<T>(Expression<Func<T>> propertyExpression)
        {
            OnPropertyChanged(ReflectionHelper.GetPropertyName(propertyExpression));
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected void RaisePropertyChanged<T>(Expression<Func<T>> propertyExpression)
        {
            OnPropertyChanged(ReflectionHelper.GetPropertyName(propertyExpression));
        }

        protected virtual void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public virtual void Dispose()
        {
            IsDisposed = true;
        }

        public virtual void Refresh()
        {
            if (IsDisposed)
                throw new ObjectDisposedException($"The instance of the class '{GetType()}' is already disposed.");
        }
    }
}