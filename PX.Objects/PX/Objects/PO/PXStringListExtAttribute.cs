// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.PXStringListExtAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;

#nullable disable
namespace PX.Objects.PO;

/// <summary>
/// Extension of the string PXStringList attribute, which allows to define list <br />
/// of hidden(possible) values and their lables. Usually, this list must be wider, then list of <br />
/// enabled values - which mean that UI control may display more values then user is allowed to select in it<br />
/// </summary>
public class PXStringListExtAttribute : PXStringListAttribute
{
  protected string[] _HiddenValues;
  protected string[] _HiddenLabels;
  protected string[] _HiddenLabelsLocal;

  /// <summary>Ctor</summary>
  /// <param name="allowedValues">List of the string values, that user can select in the UI</param>
  /// <param name="allowedLabels">List of the labels for these values</param>
  /// <param name="hiddenValues">List of the string values, that may appear in the list. Normally, it must contain all the values from allowedList </param>
  /// <param name="hiddenLabels">List of the labels for these values</param>
  public PXStringListExtAttribute(
    string[] allowedValues,
    string[] allowedLabels,
    string[] hiddenValues,
    string[] hiddenLabels)
    : base(allowedValues, allowedLabels)
  {
    this._HiddenValues = hiddenValues;
    this._HiddenLabels = hiddenLabels;
    this._HiddenLabelsLocal = (string[]) null;
    this._ExclusiveValues = false;
  }

  protected PXStringListExtAttribute(
    Tuple<string, string>[] allowedPairs,
    Tuple<string, string>[] hiddenPairs)
    : this(((IEnumerable<Tuple<string, string>>) allowedPairs).Select<Tuple<string, string>, string>((Func<Tuple<string, string>, string>) (t => t.Item1)).ToArray<string>(), ((IEnumerable<Tuple<string, string>>) allowedPairs).Select<Tuple<string, string>, string>((Func<Tuple<string, string>, string>) (t => t.Item2)).ToArray<string>(), ((IEnumerable<Tuple<string, string>>) hiddenPairs).Select<Tuple<string, string>, string>((Func<Tuple<string, string>, string>) (t => t.Item1)).ToArray<string>(), ((IEnumerable<Tuple<string, string>>) hiddenPairs).Select<Tuple<string, string>, string>((Func<Tuple<string, string>, string>) (t => t.Item2)).ToArray<string>())
  {
  }

  public virtual void CacheAttached(PXCache sender)
  {
    base.CacheAttached(sender);
    PXGraph.FieldUpdatingEvents fieldUpdating = sender.Graph.FieldUpdating;
    Type itemType = sender.GetItemType();
    string fieldName = ((PXEventSubscriberAttribute) this)._FieldName;
    PXStringListExtAttribute listExtAttribute = this;
    // ISSUE: virtual method pointer
    PXFieldUpdating pxFieldUpdating = new PXFieldUpdating((object) listExtAttribute, __vmethodptr(listExtAttribute, OnFieldUpdating));
    fieldUpdating.AddHandler(itemType, fieldName, pxFieldUpdating);
  }

  public virtual void FieldSelecting(PXCache sender, PXFieldSelectingEventArgs e)
  {
    base.FieldSelecting(sender, e);
    if (this._HiddenLabelsLocal == null)
    {
      if (!CultureInfo.InvariantCulture.Equals((object) Thread.CurrentThread.CurrentCulture) && this._HiddenLabels != null && this._HiddenValues != null)
      {
        this._HiddenLabelsLocal = new string[this._HiddenLabels.Length];
        this._HiddenLabels.CopyTo((Array) this._HiddenLabelsLocal, 0);
        if (((PXEventSubscriberAttribute) this)._BqlTable != (Type) null)
        {
          for (int index = 0; index < this._HiddenLabels.Length; ++index)
          {
            string str1 = $"{PXUIFieldAttribute.GetNeutralDisplayName(sender, ((PXEventSubscriberAttribute) this)._FieldName)} -> {this._HiddenLabels[index]}";
            string str2 = PXLocalizer.Localize(str1, ((PXEventSubscriberAttribute) this)._BqlTable.FullName);
            if (!string.IsNullOrEmpty(str2) && str2 != str1)
              this._HiddenLabelsLocal[index] = str2;
          }
        }
      }
      else
        this._HiddenLabelsLocal = this._HiddenLabels;
    }
    if (e.Row == null || e.ReturnValue == null || this.IndexAllowedValue((string) e.ReturnValue) >= 0)
      return;
    int index1 = this.IndexValue((string) e.ReturnValue);
    if (index1 < 0)
      return;
    e.ReturnValue = this._HiddenLabelsLocal != null ? (object) this._HiddenLabelsLocal[index1] : (object) this._HiddenLabels[index1];
  }

  protected int IndexAllowedValue(string value)
  {
    if (this._AllowedValues != null)
    {
      for (int index = 0; index < this._AllowedValues.Length; ++index)
      {
        if (string.Compare(this._AllowedValues[index], value, true) == 0)
          return index;
      }
    }
    return -1;
  }

  protected int IndexValue(string value)
  {
    if (this._HiddenValues != null)
    {
      for (int index = 0; index < this._HiddenValues.Length; ++index)
      {
        if (string.Compare(this._HiddenValues[index], value, true) == 0)
          return index;
      }
    }
    return -1;
  }

  protected string SearchValueByName(string name)
  {
    if (this._HiddenValues != null)
    {
      for (int index = 0; index < this._HiddenValues.Length; ++index)
      {
        if (this._HiddenLabelsLocal != null && string.Compare(this._HiddenLabelsLocal[index], name, true) == 0 || this._HiddenLabels != null && string.Compare(this._HiddenLabels[index], name, true) == 0)
          return this._HiddenValues[index];
      }
    }
    return (string) null;
  }

  protected virtual void OnFieldUpdating(PXCache sender, PXFieldUpdatingEventArgs e)
  {
    if (e.NewValue == null || this.IndexValue((string) e.NewValue) != -1)
      return;
    e.NewValue = (object) this.SearchValueByName((string) e.NewValue);
  }
}
