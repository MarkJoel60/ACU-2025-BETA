// Decompiled with JetBrains decompiler
// Type: PX.Data.SQLTree.ProjectionReference
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Collections.Generic;
using System.Reflection;

#nullable disable
namespace PX.Data.SQLTree;

internal class ProjectionReference : ProjectionItem
{
  private List<MethodInfo> _setters;
  private List<MethodInfo> _getters;
  private PXGraph _lastGraph;
  private PXCache _cache;

  public ProjectionReference(System.Type dac) => this.type_ = dac;

  protected virtual IEnumerable<PropertyInfo> GetDacProperties(System.Type dac)
  {
    return (IEnumerable<PropertyInfo>) dac.GetProperties();
  }

  private void EnsureSettersAndGetters(System.Type dac)
  {
    if (this._setters != null && this._getters != null)
      return;
    this._setters = new List<MethodInfo>();
    this._getters = new List<MethodInfo>();
    foreach (PropertyInfo dacProperty in this.GetDacProperties(dac))
    {
      bool flag = false;
      foreach (object customAttribute in dacProperty.GetCustomAttributes(true))
      {
        if (typeof (PXDBFieldAttribute).IsAssignableFrom(customAttribute.GetType()) || typeof (PXDBCalcedAttribute).IsAssignableFrom(customAttribute.GetType()) || typeof (PXDBCreatedByIDAttribute).IsAssignableFrom(customAttribute.GetType()))
        {
          flag = true;
          break;
        }
      }
      if (flag)
      {
        this._setters.Add(dacProperty.SetMethod);
        this._getters.Add(dacProperty.GetMethod);
      }
    }
  }

  private object GetValue(PXDataRecord data, ref int position)
  {
    this.EnsureSettersAndGetters(this.type_);
    object instance = Activator.CreateInstance(this.type_);
    foreach (MethodInfo setter in this._setters)
    {
      object obj1 = ProjectionReference.adoptParameter(setter, data.GetValue(position));
      ++position;
      object obj2 = instance;
      object[] parameters = new object[1]{ obj1 };
      setter.Invoke(obj2, parameters);
    }
    return instance;
  }

  protected static byte[] TimeStampFromString(System.DateTime dateWithMicrosex)
  {
    byte[] destinationArray = new byte[8];
    uint num1 = (uint) (dateWithMicrosex.Year << 20) | (uint) (dateWithMicrosex.Month << 16 /*0x10*/) | (uint) (dateWithMicrosex.Day << 8) | (uint) dateWithMicrosex.Hour;
    int num2 = dateWithMicrosex.Minute << 26 | dateWithMicrosex.Second << 20 | dateWithMicrosex.Millisecond * 1000;
    Array.Copy((Array) BitConverter.GetBytes(num1), 0, (Array) destinationArray, 4, 4);
    Array.Copy((Array) BitConverter.GetBytes((uint) num2), 0, (Array) destinationArray, 0, 4);
    Array.Reverse((Array) destinationArray);
    return destinationArray;
  }

  protected static object adoptParameter(MethodInfo setter, object parameter)
  {
    if (setter == (MethodInfo) null)
      return parameter;
    System.Type parameterType = setter.GetParameters()[0].ParameterType;
    System.Type underlyingType = Nullable.GetUnderlyingType(parameterType);
    if (parameter != null)
    {
      if (parameterType == typeof (bool) || underlyingType == typeof (bool))
        parameter = !(underlyingType != (System.Type) null) ? Convert.ChangeType(parameter, parameterType) : (parameter == null ? parameter : Convert.ChangeType(parameter, underlyingType));
      else if (parameterType == typeof (byte[]) && parameter.GetType() == typeof (System.DateTime))
        parameter = (object) ProjectionReference.TimeStampFromString((System.DateTime) parameter);
      else if (underlyingType != (System.Type) null && underlyingType.IsPrimitive)
        parameter = Convert.ChangeType(parameter, underlyingType);
      else if (parameterType.IsPrimitive)
        parameter = Convert.ChangeType(parameter, parameterType);
      else if (parameterType == typeof (string))
        parameter = (object) parameter.ToString();
    }
    return parameter;
  }

  internal override object GetValue(PXDataRecord data, ref int position, MergeCacheContext context)
  {
    if (context == null || !typeof (IBqlTable).IsAssignableFrom(this.type_))
      return this.GetValue(data, ref position);
    if (this._cache == null || this._lastGraph != context.Graph)
    {
      this._cache = context.Graph.Caches[this.type_];
      this._lastGraph = context.Graph;
    }
    try
    {
      if (context.CreateNew)
        return this.type_ == context.DacTypeToMerge ? context.MergedDac : this._cache.CreateInstance();
      bool isReadOnly = this.type_ != context.DacTypeToMerge;
      bool wasUpdated;
      object obj = context.CreateItem(this._cache, data, ref position, isReadOnly, out wasUpdated);
      if (!isReadOnly && obj == null)
        context.Status = MergeCacheStatus.Skip;
      else if (wasUpdated)
        context.Status = MergeCacheStatus.Updated;
      if (this.type_ == context.DacTypeToMerge)
        context.MergedDac = obj;
      return obj;
    }
    catch (Exception ex)
    {
      PXTrace.WriteError(ex.Message);
      context.Status = MergeCacheStatus.Skip;
      return (object) null;
    }
  }

  protected override object CloneValueInternal(object value, CloneContext context)
  {
    if (this._cache != null && this._cache.GetItemType() == value.GetType())
      return this._cache.CreateCopy(value);
    this.EnsureSettersAndGetters(this.type_);
    object instance = Activator.CreateInstance(this.type_);
    for (int index = 0; index < this._setters.Count; ++index)
    {
      MethodInfo setter = this._setters[index];
      object obj1 = this._getters[index].Invoke(value, new object[0]);
      object obj2 = instance;
      object[] parameters = new object[1]{ obj1 };
      setter.Invoke(obj2, parameters);
    }
    return instance;
  }

  public override string ToString() => this.type_.Name;
}
