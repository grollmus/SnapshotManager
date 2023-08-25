using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace SnapshotManager.UI.Mvvm;

public class ViewModelBase : INotifyPropertyChanged, IDisposable
{
    [Display(AutoGenerateField = false)]
    public bool IsDisposed { get; private set; }

    private static string GetPropertyName<T>(Expression<Func<T>> propertyExpression)
    {
        // Retreive lambda body
        if (!(propertyExpression.Body is MemberExpression memberExpression))
            throw new ArgumentException("propertyExpression should be a member expression");

        var propertyInfo = memberExpression.Member as PropertyInfo;
        if (null == propertyInfo)
            throw new ArgumentException("propertyExpression does not use a property");

        return propertyInfo.Name;
    }

    protected void OnPropertyChanged<T>(Expression<Func<T>> propertyExpression)
    {
        OnPropertyChanged(GetPropertyName(propertyExpression));
    }

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    protected void RaisePropertyChanged<T>(Expression<Func<T>> propertyExpression)
    {
        OnPropertyChanged(GetPropertyName(propertyExpression));
    }

    protected virtual void RaisePropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public virtual void Refresh()
    {
        if (IsDisposed)
            throw new ObjectDisposedException($"The instance of the class '{GetType()}' is already disposed.");
    }

    public virtual void Dispose()
    {
        IsDisposed = true;
    }

    public event PropertyChangedEventHandler PropertyChanged;
}