using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using SnapshotManager.Core.Exceptions;
using ArgumentNullException = SnapshotManager.Core.Exceptions.ArgumentNullException;

namespace SnapshotManager.Core.Reflection;

public static class ReflectionHelper
{
    /// <summary>
    /// Prüft rekursiv, ob ein Typ eine Member-Variable (Field) besitzt.
    /// </summary>
    /// <param name="type">Der Typ, in dem die Member-Variable gesucht wird.</param>
    /// <param name="fieldName">Name der Member-Variable (Field).</param>
    /// <returns>true, wenn eine Member-Variable mit dem Namen fieldName gefunden wurde.</returns>
    public static bool ContainsField(Type type, string fieldName)
    {
        var fields = type.GetFields();
        if (fields.Any(field => field.Name == fieldName))
        {
            return true;
        }

        var nestedTypes = type.GetNestedTypes();
        return nestedTypes.Any(nestedType => ContainsField(nestedType, fieldName));
    }

    /// <summary>
    /// Prüft rekursiv, ob ein Typ ein Property besitzt.
    /// </summary>
    /// <param name="type">Der Typ, in dem das Property gesucht wird.</param>
    /// <param name="propertyName">Name des Property.</param>
    /// <returns>true, wenn ein Property mit dem Namen propertyName gefunden wurde.</returns>
    public static bool ContainsProperty(Type type, string propertyName)
    {
        var properties = type.GetProperties();
        if (properties.Any(property => property.Name == propertyName))
        {
            return true;
        }

        var nestedTypes = type.GetNestedTypes();
        return nestedTypes.Any(nestedType => ContainsField(nestedType, propertyName));
    }

    /// <summary>
    /// Führt die Methode eines von Fields des Typs TField aus.
    /// Es BindingFlags werden NonPublic und Instance verwendet.
    /// </summary>
    /// <typeparam name="TField">Typ der Fields.</typeparam>
    /// <param name="obj">Objekt, dessen Fields-Methode aufgerufen werden soll.</param>
    /// <param name="methodName"></param>
    public static void ExecuteMethodOfFields<TField>(object obj, string methodName)
    {
        ExecuteMethodOfFields<TField>(obj, BindingFlags.NonPublic | BindingFlags.Instance, methodName, null);
    }

    /// <summary>
    /// Führt die Methode eines von Fields des Typs TField aus.
    /// </summary>
    /// <typeparam name="TField">Typ der Fields.</typeparam>
    /// <param name="obj">Objekt, dessen Fields-Methode aufgerufen werden soll.</param>
    /// <param name="flags">Die BindingFlags zur Auswahl der Fiels. <see cref="BindingFlags"/></param>
    /// <param name="methodName"></param>
    public static void ExecuteMethodOfFields<TField>(object obj, BindingFlags flags, string methodName)
    {
        ExecuteMethodOfFields<TField>(obj, flags, methodName, null);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TField"></typeparam>
    /// <param name="obj"></param>
    /// <param name="flags"></param>
    /// <param name="methodName"></param>
    /// <param name="methodArguments"></param>
    public static void ExecuteMethodOfFields<TField>(object obj, BindingFlags flags, string methodName, object[] methodArguments)
    {
        var method = typeof(TField).GetMethod(methodName);
        var fields = GetFieldInstances<TField>(obj, flags);
        foreach (var field in fields)
        {
            try
            {
                method.Invoke(field, methodArguments);
            }
            catch (Exception ex)
            {
                throw new MethodInvokeException(method, ex);
            }

        }
    }

    /// <summary>
    /// Gibt die Instanzen der Fields eines Objekts zurück.
    /// </summary>
    /// <param name="obj">Objekt, dessen Fields zurück gegeben werden soll.</param>
    /// <param name="flags">BindingFlags zur Selektion der Fields.</param>
    /// <returns>Liste von Field-Instanzen.</returns>
    public static IEnumerable<object> GetFieldInstances(object obj, BindingFlags flags)
    {
        var type = obj.GetType();
        var fieldInfos = type.GetFields(flags);
        return fieldInfos.Select(fieldInfo => fieldInfo.GetValue(obj));
    }

    /// <summary>
    /// Gibt die Instanzen der Fields vom Typ T eines Objekts zurück.
    /// </summary>
    /// <typeparam name="T">Typ der Fields.</typeparam>
    /// <param name="obj">Objekt, dessen Fields zurück gegeben werden soll.</param>
    /// <param name="flags">BindingFlags zur Selektion der Fields.</param>
    /// <returns>Liste von Field-Instanzen vom Typ T.</returns>
    public static IEnumerable<T> GetFieldInstances<T>(object obj, BindingFlags flags)
    {
        var type = obj.GetType();
        var fieldType = typeof(T);
        var fieldInfos = type.GetFields(flags);
        foreach (var fieldInfo in fieldInfos)
        {
            if (fieldInfo.FieldType == fieldType)
                yield return (T)fieldInfo.GetValue(obj);
        }
    }

    /// <summary>
    /// Gibt den Namen eines Properties zurück.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="propertyExpression"></param>
    /// <returns></returns>
    public static string GetPropertyName<T>(Expression<Func<T>> propertyExpression)
    {
        // Retreive lambda body
        if (!(propertyExpression.Body is MemberExpression memberExpression))
            throw new ArgumentException("propertyExpression should be a member expression");

        var propertyInfo = memberExpression.Member as PropertyInfo;
        if(null == propertyInfo)
            throw new ArgumentException("propertyExpression does not use a property");

        return propertyInfo.Name;
    }

    /// <summary>
    /// Gibt den Wert eines Properties zurück.
    /// </summary>
    /// <param name="obj">Das Objekt, dessen Wert des Properties zurückgegeben werden soll.</param>
    /// <param name="propertyName">Name des Properties.</param>
    /// <param name="throwExceptions">Wenn false, dann wird keine Exception geworfen, falls die Property nicht vorhanden ist.</param>
    /// <returns></returns>
    public static object GetPropertyValue(object obj, string propertyName, bool throwExceptions = true)
    {
        ArgumentNullException.ThrowIfNull(obj);

        if (string.IsNullOrEmpty(propertyName))
            throw new ArgumentNullException("propertyName");

        var propertyInfo = obj.GetType().GetProperty(propertyName);
        if (!throwExceptions && null == propertyInfo)
            return null;

        return propertyInfo.GetValue(obj, null);
    }

    /// <summary>
    /// Gibt den Wert eines Properties zurück.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="obj">Das Objekt, dessen Wert des Properties zurückgegeben werden soll.</param>
    /// <param name="propertyName">Name des Properties.</param>
    /// <param name="throwExceptions">Wenn false, dann wird keine Exception geworfen, falls die Property nicht vorhanden ist.</param>
    /// <returns></returns>
    public static T GetPropertyValue<T>(object obj, string propertyName, bool throwExceptions = true)
    {
        return (T) GetPropertyValue(obj, propertyName, throwExceptions);
    }

    /// <summary>
    /// Gibt den Wert eines Properties zurück.
    /// </summary>
    /// <param name="propertyExpression">Property, dessen Wert zurückgegeben werden soll.</param>
    /// <param name="throwExceptions">Wenn false, dann wird keine Exception geworfen, falls die Property nicht vorhanden ist.</param>
    /// <returns>Wert des Propertys.</returns>
    public static object GetPropertyValue<T>(Expression<Func<T>> propertyExpression, bool throwExceptions = true)
    {
        // Retreive lambda body
        if (!(propertyExpression.Body is MemberExpression body))
            throw new ArgumentException("'propertyExpression' should be a member expression");

        // Extract the right part (after "=>")
        if (!(body.Expression is ConstantExpression constantExpression))
            throw new ArgumentException("'propertyExpression' body should be a constant expression");

        var propertyInfo = constantExpression.Value.GetType().GetProperty(body.Member.Name);
        if (!throwExceptions && null == propertyInfo)
            return null;

        return propertyInfo.GetValue(constantExpression.Value, null);
    }

    /// <summary>
    /// Gibt den Wert eines Properties zurück.
    /// </summary>
    /// <typeparam name="T">Typ des Propertys.</typeparam>
    /// <param name="propertyExpression">Property, dessen Wert zurückgegeben werden soll.</param>
    /// <param name="throwExceptions">Wenn false, dann wird keine Exception geworfen, falls die Property nicht vorhanden ist.</param>
    /// <returns>Wert des Propertys.</returns>
    public static T GetPropertyValue<T>(Expression<Func<object>> propertyExpression, bool throwExceptions = true)
    {
        return (T)GetPropertyValue(propertyExpression);
    }

    /// <summary>
    /// Prüft, ob es sich um einen numerischen Typ handelt.
    /// </summary>
    /// <param name="value"></param>
    /// <returns>Gibt true zurück, falls numerisch.</returns>
    public static bool IsNumeric(object value)
    {
        if (value is byte ||
            value is decimal ||
            value is double ||
            value is short ||
            value is int ||
            value is long ||
            value is float ||
            value is sbyte ||
            value is ushort ||
            value is uint ||
            value is ulong)
            return true;

        return false;
    }

    /// <summary>
    /// Setzt den Wert des selektierten Properties.
    /// </summary>
    /// <typeparam name="T">Typ des Objekts, dessen Property gesetzt werden soll.</typeparam>
    /// <typeparam name="TProperty">Typ des Properties.</typeparam>
    /// <param name="propertySelector">Auswahl des Properties.</param>
    /// <param name="value">Wert der gesetzt werden soll.</param>
    /// <param name="throwExceptions"></param>
    public static void SetProperty<T, TProperty>(Expression<Func<T>> propertySelector, TProperty value, bool throwExceptions = true)
    {
        // Retreive lambda body
        if (!(propertySelector.Body is MemberExpression body))
            throw new ArgumentException("'propertyExpression' should be a member expression");

        // Extract the right part (after "=>")
        if (!(body.Expression is ConstantExpression constantExpression))
            throw new ArgumentException("'propertyExpression' body should be a constant expression");

        var propertyInfo = constantExpression.Value.GetType().GetProperty(body.Member.Name);
        if (!throwExceptions && null == propertyInfo)
            return;

        propertyInfo.SetValue(constantExpression.Value, value, null);
    }
}