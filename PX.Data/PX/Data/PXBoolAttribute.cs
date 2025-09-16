// Decompiled with JetBrains decompiler
// Type: PX.Data.PXBoolAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.SQLTree;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Data;

/// <summary>Indicates a DAC field of <tt>bool?</tt> type that is not
/// mapped to a database column.</summary>
/// <remarks>The attribute is added to the value declaration of a DAC field.
/// The field is not bound to a table column.</remarks>
/// <example>
/// <code>
/// [PXBool()]
/// [PXDefault(false)]
/// public virtual bool? Selected { get; set; }
/// </code>
/// </example>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Parameter)]
[PXAttributeFamily(typeof (PXFieldState))]
public class PXBoolAttribute : 
  PXEventSubscriberAttribute,
  IPXFieldUpdatingSubscriber,
  IPXFieldSelectingSubscriber,
  IPXCommandPreparingSubscriber
{
  protected bool _IsKey;

  /// <summary>Gets or sets the value that indicates whether the field is a
  /// key field.</summary>
  public virtual bool IsKey
  {
    get => this._IsKey;
    set => this._IsKey = value;
  }

  /// <exclude />
  public virtual void FieldUpdating(PXCache sender, PXFieldUpdatingEventArgs e)
  {
    PXBoolAttribute.ConvertValue(e);
  }

  /// <summary>
  /// Converts the <tt>e.NewValue</tt> property of the parameter from string
  /// to boolean and sets <tt>e.NewValue</tt> to the converted value.
  /// </summary>
  /// <param name="e">Event arguments of the <tt>FieldUpdating</tt> event.</param>
  public static void ConvertValue(PXFieldUpdatingEventArgs e)
  {
    e.NewValue = PXBoolAttribute.ConvertValue(e.NewValue);
  }

  public static object ConvertValue(object newValue)
  {
    if (newValue is string)
    {
      bool result;
      if (bool.TryParse((string) newValue, out result))
      {
        newValue = (object) result;
      }
      else
      {
        string str = newValue as string;
        if (!string.IsNullOrEmpty(str))
        {
          switch (str.Trim())
          {
            case "1":
              newValue = (object) true;
              break;
            case "0":
              newValue = (object) false;
              break;
            default:
              newValue = (object) null;
              break;
          }
        }
        else
          newValue = (object) null;
      }
    }
    return newValue;
  }

  /// <exclude />
  public virtual void FieldSelecting(PXCache sender, PXFieldSelectingEventArgs e)
  {
    if (this._AttributeLevel != PXAttributeLevel.Item && !e.IsAltered)
      return;
    e.ReturnState = (object) PXFieldState.CreateInstance(e.ReturnState, typeof (bool), new bool?(this._IsKey), required: new int?(-1), fieldName: this._FieldName);
  }

  /// <exclude />
  public virtual void CommandPreparing(PXCache sender, PXCommandPreparingEventArgs e)
  {
    if ((e.Operation & PXDBOperation.Delete) != PXDBOperation.Select)
      return;
    if (e.Value != null)
    {
      e.BqlTable = this._BqlTable;
      if (e.Expr == null)
        e.Expr = (SQLExpression) new Column(this._FieldName, e.BqlTable);
      e.DataValue = e.Value;
      e.DataLength = new int?(1);
    }
    e.DataType = PXDbType.Bit;
  }

  /// <exclude />
  public override void CacheAttached(PXCache sender)
  {
    if (!this.IsKey)
      return;
    sender.Keys.Add(this._FieldName);
  }

  /// <exclude />
  public static bool CheckSingleRow(PXCache cache, PXView view, object item, string fieldName)
  {
    bool? nullable1 = (bool?) cache.GetValue(item, fieldName);
    bool flag1 = true;
    if (!(nullable1.GetValueOrDefault() == flag1 & nullable1.HasValue))
      return false;
    foreach (object data in cache.Cached)
    {
      bool flag2 = true;
      foreach (string key in (IEnumerable<string>) cache.Keys)
      {
        if (!object.Equals(cache.GetValue(data, key), cache.GetValue(item, key)))
        {
          flag2 = false;
          break;
        }
      }
      if (!flag2)
      {
        bool? nullable2 = (bool?) cache.GetValue(data, fieldName);
        bool flag3 = true;
        if (nullable2.GetValueOrDefault() == flag3 & nullable2.HasValue)
        {
          cache.SetValue(data, fieldName, (object) false);
          cache.Update(data);
        }
      }
    }
    view.RequestRefresh();
    cache.IsDirty = false;
    return true;
  }

  /// <exclude />
  public static bool CheckSingleRow<T>(PXCache cache, PXView view, object item) where T : IBqlField
  {
    return PXBoolAttribute.CheckSingleRow(cache, view, item, typeof (T).Name);
  }
}
