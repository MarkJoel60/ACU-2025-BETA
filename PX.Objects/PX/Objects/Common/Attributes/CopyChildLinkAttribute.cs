// Decompiled with JetBrains decompiler
// Type: PX.Objects.Common.Attributes.CopyChildLinkAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

#nullable disable
namespace PX.Objects.Common.Attributes;

public class CopyChildLinkAttribute : PXUnboundFormulaAttribute
{
  protected Type _counterField;
  protected List<Type> _linkChildKeys;
  protected List<Type> _linkParentKeys;
  protected Type _parentType;
  protected Type _amountField;

  public CopyChildLinkAttribute(
    Type counterField,
    Type amountField,
    Type[] linkChildKeys,
    Type[] linkParentKeys)
    : base(typeof (IIf<,,>).MakeGenericType(typeof (Where<,>).MakeGenericType(amountField, typeof (Equal<decimal0>)), typeof (Zero), typeof (One)), typeof (SumCalc<>).MakeGenericType(counterField))
  {
    this._counterField = counterField ?? throw new ArgumentNullException(nameof (counterField));
    this._amountField = amountField ?? throw new ArgumentNullException(nameof (amountField));
    this._linkChildKeys = (linkChildKeys != null ? ((IEnumerable<Type>) linkChildKeys).ToList<Type>() : (List<Type>) null) ?? throw new ArgumentNullException(nameof (linkChildKeys));
    this._linkParentKeys = (linkParentKeys != null ? ((IEnumerable<Type>) linkParentKeys).ToList<Type>() : (List<Type>) null) ?? throw new ArgumentNullException(nameof (linkParentKeys));
    if (this._linkChildKeys.Count == 0 || this._linkParentKeys.Count == 0 || this._linkChildKeys.Count != this._linkParentKeys.Count)
      throw new ArgumentOutOfRangeException(nameof (linkParentKeys));
    this._parentType = typeof (IBqlField).IsAssignableFrom(this._counterField) ? BqlCommand.GetItemType(this._counterField) : throw new PXArgumentException(nameof (_counterField), "Invalid field: '{0}'", new object[1]
    {
      (object) this._counterField.Name
    });
  }

  public virtual void CacheAttached(PXCache sender)
  {
    base.CacheAttached(sender);
    this.ForcePersistParent(sender);
  }

  protected virtual void ForcePersistParent(PXCache sender)
  {
    if (sender.Graph.Views.Caches.Contains(this._parentType))
      return;
    sender.Graph.Views.Caches.Add(this._parentType);
  }

  public virtual void RowInserted(PXCache sender, PXRowInsertedEventArgs e)
  {
    base.RowInserted(sender, e);
    if (e.Row == null)
      return;
    this.OnRowInserted(sender, e.Row);
  }

  public virtual void RowDeleted(PXCache sender, PXRowDeletedEventArgs e)
  {
    base.RowDeleted(sender, e);
    if (e.Row == null)
      return;
    this.OnRowDeleted(sender, e.Row);
  }

  public virtual void RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    base.RowUpdated(sender, e);
    if (e.Row == null)
      return;
    this.OnRowUpdated(sender, e.Row, e.OldRow);
  }

  protected virtual void OnRowInserted(PXCache sender, object row)
  {
    if (this.IsAmountZero(sender, row))
      return;
    object obj = PXParentAttribute.SelectParent(sender, row, this._parentType);
    if (obj == null)
      return;
    int? counterValue = this.GetCounterValue(sender.Graph, obj);
    if (counterValue.GetValueOrDefault() == 1)
    {
      object[] childLinkKeys = this.GetChildLinkKeys(sender, row);
      this.SetParentLinkKeys(sender, row, childLinkKeys, obj);
    }
    else
    {
      int? nullable = counterValue;
      int num = 1;
      if (nullable.GetValueOrDefault() > num & nullable.HasValue)
      {
        object[] childLinkKeys = this.GetChildLinkKeys(sender, row);
        object[] parentLinkKeys = this.GetParentLinkKeys(sender, row, obj);
        object[] second = parentLinkKeys;
        if (((IEnumerable<object>) childLinkKeys).SequenceEqual<object>((IEnumerable<object>) second) || !((IEnumerable<object>) parentLinkKeys).Any<object>((Func<object, bool>) (k => k != null)))
          return;
        object[] array = ((IEnumerable<object>) parentLinkKeys).Select<object, object>((Func<object, object>) (k => (object) null)).ToArray<object>();
        this.SetParentLinkKeys(sender, row, array, obj);
      }
      else
        this.OnWrongCounter(sender, row, counterValue, nameof (OnRowInserted));
    }
  }

  protected virtual void OnRowDeleted(PXCache sender, object row)
  {
    if (this.IsAmountZero(sender, row) || this.IsParentDeleted(sender, row))
      return;
    object obj = PXParentAttribute.SelectParent(sender, row, this._parentType);
    if (obj == null)
      return;
    int? counterValue = this.GetCounterValue(sender.Graph, obj);
    int? nullable = counterValue;
    int num1 = 0;
    if (nullable.GetValueOrDefault() == num1 & nullable.HasValue)
    {
      object[] array = this._linkParentKeys.Select<Type, object>((Func<Type, object>) (k => (object) null)).ToArray<object>();
      this.SetParentLinkKeys(sender, row, array, obj);
    }
    else
    {
      nullable = counterValue;
      int num2 = 0;
      if (nullable.GetValueOrDefault() > num2 & nullable.HasValue)
      {
        object[] childLinkKeys = this.GetChildLinkKeys(sender, row);
        object[] parentLinkKeys = this.GetParentLinkKeys(sender, row, obj);
        object[] second = parentLinkKeys;
        if (((IEnumerable<object>) childLinkKeys).SequenceEqual<object>((IEnumerable<object>) second) && !((IEnumerable<object>) parentLinkKeys).Any<object>((Func<object, bool>) (k => k == null)))
          return;
        this.VerifyAllChildren(sender, obj);
      }
      else
        this.OnWrongCounter(sender, row, counterValue, nameof (OnRowDeleted));
    }
  }

  protected virtual void OnRowUpdated(PXCache sender, object row, object oldRow)
  {
    if (oldRow != null)
    {
      if (this.IsAmountZero(sender, row) && this.IsAmountZero(sender, oldRow))
        return;
      if (this.IsAmountZero(sender, row) != this.IsAmountZero(sender, oldRow))
      {
        this.OnRowDeleted(sender, oldRow);
        this.OnRowInserted(sender, row);
      }
      else
      {
        object[] childLinkKeys1 = this.GetChildLinkKeys(sender, row);
        object obj1 = PXParentAttribute.SelectParent(sender, row, this._parentType);
        if (obj1 == null)
          return;
        int? counterValue = this.GetCounterValue(sender.Graph, obj1);
        if (counterValue.GetValueOrDefault() == 1)
        {
          this.SetParentLinkKeys(sender, row, childLinkKeys1, obj1);
        }
        else
        {
          int? nullable = counterValue;
          int num = 1;
          if (nullable.GetValueOrDefault() > num & nullable.HasValue)
          {
            object[] childLinkKeys2 = this.GetChildLinkKeys(sender, oldRow);
            if (((IEnumerable<object>) childLinkKeys1).SequenceEqual<object>((IEnumerable<object>) childLinkKeys2))
              return;
            object obj2 = PXParentAttribute.SelectParent(sender, oldRow, this._parentType);
            if (obj1 != obj2)
            {
              this.OnRowDeleted(sender, oldRow);
              this.OnRowInserted(sender, row);
            }
            else
            {
              object[] parentLinkKeys = this.GetParentLinkKeys(sender, row, obj1);
              if (((IEnumerable<object>) parentLinkKeys).SequenceEqual<object>((IEnumerable<object>) childLinkKeys2) && ((IEnumerable<object>) parentLinkKeys).All<object>((Func<object, bool>) (k => k != null)))
              {
                object[] array = this._linkParentKeys.Select<Type, object>((Func<Type, object>) (k => (object) null)).ToArray<object>();
                this.SetParentLinkKeys(sender, row, array, obj1);
              }
              else
                this.VerifyAllChildren(sender, obj1);
            }
          }
          else
            this.OnWrongCounter(sender, row, counterValue, nameof (OnRowUpdated));
        }
      }
    }
    else
      this.VerifyAllChildren(sender, (object) null);
  }

  protected virtual bool IsAmountZero(PXCache cache, object row)
  {
    return ((Decimal?) cache.GetValue(row, this._amountField.Name)).GetValueOrDefault() == 0M;
  }

  protected virtual int? GetCounterValue(PXGraph graph, object row)
  {
    return (int?) graph.Caches[this._parentType].GetValue(row, this._counterField.Name);
  }

  protected virtual object[] GetChildLinkKeys(PXCache cache, object row)
  {
    return this._linkChildKeys.Select<Type, object>((Func<Type, object>) (k => cache.GetValue(row, k.Name))).ToArray<object>();
  }

  protected virtual object[] GetParentLinkKeys(
    PXCache childCache,
    object childRow,
    out object parentRow)
  {
    parentRow = PXParentAttribute.SelectParent(childCache, childRow, this._parentType);
    return this.GetParentLinkKeys(childCache, childRow, parentRow);
  }

  protected virtual object[] GetParentLinkKeys(
    PXCache childCache,
    object childRow,
    object parentRow)
  {
    PXCache parentCache = childCache.Graph.Caches[this._parentType];
    return this._linkParentKeys.Select<Type, object>((Func<Type, object>) (k => parentCache.GetValue(parentRow, k.Name))).ToArray<object>();
  }

  protected virtual object SetParentLinkKeys(
    PXCache childCache,
    object childRow,
    object[] values,
    object parentRow)
  {
    PXCache cach = childCache.Graph.Caches[this._parentType];
    object copy = cach.CreateCopy(parentRow);
    bool flag = false;
    foreach (var data in this._linkParentKeys.Zip((IEnumerable<object>) values, (k, v) => new
    {
      Key = k,
      Value = v
    }))
    {
      if (!object.Equals(cach.GetValue(copy, data.Key.Name), data.Value))
      {
        cach.SetValueExt(copy, data.Key.Name, data.Value);
        flag = true;
      }
    }
    return flag ? cach.Update(copy) : parentRow;
  }

  protected virtual bool IsParentDeleted(PXCache childCache, object childRow)
  {
    return PXParentAttribute.SelectParent(childCache, childRow, this._parentType) == null;
  }

  protected virtual int VerifyAllChildren(PXCache childCache, object parentRow)
  {
    object[] first = (object[]) null;
    bool flag = false;
    int num = 0;
    foreach (object selectChild in PXParentAttribute.SelectChildren(childCache, parentRow, this._parentType))
    {
      if (!this.IsAmountZero(childCache, selectChild))
      {
        object[] childLinkKeys = this.GetChildLinkKeys(childCache, selectChild);
        if (first != null && !((IEnumerable<object>) first).SequenceEqual<object>((IEnumerable<object>) childLinkKeys))
        {
          flag = false;
          break;
        }
        flag = true;
        first = childLinkKeys;
        ++num;
      }
    }
    object[] values = flag ? first : this._linkParentKeys.Select<Type, object>((Func<Type, object>) (k => (object) null)).ToArray<object>();
    this.SetParentLinkKeys(childCache, (object) null, values, parentRow);
    return num;
  }

  protected virtual void OnWrongCounter(
    PXCache cache,
    object row,
    int? currentValue,
    [CallerMemberName] string memberName = null)
  {
    PXCache cach = cache.Graph.Caches[this._parentType];
    object parentRow = PXParentAttribute.SelectParent(cache, row, this._parentType);
    int num = this.VerifyAllChildren(cache, parentRow);
    PXTrace.WriteError($"CopyChildLinkAttribute.{memberName ?? nameof (OnWrongCounter)}: {PXLocalizer.LocalizeFormat("An error occurred during processing of the field {0} value {1} {2}.", new object[3]
    {
      (object) this._counterField.Name,
      (object) currentValue,
      (object) num
    })}");
    object copy = cach.CreateCopy(parentRow);
    cach.SetValueExt(parentRow, this._counterField.Name, (object) num);
    cach.Update(copy);
  }
}
