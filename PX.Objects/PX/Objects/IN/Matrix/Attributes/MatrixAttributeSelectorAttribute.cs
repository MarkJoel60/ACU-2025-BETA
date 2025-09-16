// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.Matrix.Attributes.MatrixAttributeSelectorAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using System;
using System.Collections.Generic;

#nullable enable
namespace PX.Objects.IN.Matrix.Attributes;

public class MatrixAttributeSelectorAttribute : 
  PXSelectorAttribute,
  IPXFieldUpdatedSubscriber,
  IPXRowPersistingSubscriber
{
  public const 
  #nullable disable
  string DummyAttributeName = "~MX~DUMMY~";
  public const string DummyAttributeValue = "Value";
  protected Type _secondField;
  protected bool _allowTheSameValue;

  public MatrixAttributeSelectorAttribute(
    Type type,
    Type secondField,
    bool allowTheSameValue,
    params Type[] fieldList)
    : base(type, fieldList)
  {
    this._secondField = secondField;
    this._allowTheSameValue = allowTheSameValue;
  }

  public virtual void FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
  {
    if (e.NewValue as string == "~MX~DUMMY~")
      return;
    if (e.Row != null && e.NewValue == null && EnumerableExtensions.IsNotIn<string>(sender.GetValue(e.Row, this._secondField.Name) as string, (string) null, "~MX~DUMMY~") && this.IsValueListEmpty(PXSelectorAttribute.SelectAll(sender, ((PXEventSubscriberAttribute) this)._FieldName, e.Row)))
      e.NewValue = (object) "~MX~DUMMY~";
    else
      base.FieldVerifying(sender, e);
  }

  public virtual void FieldSelecting(PXCache sender, PXFieldSelectingEventArgs e)
  {
    base.FieldSelecting(sender, e);
    if (!(e.ReturnValue as string == "~MX~DUMMY~"))
      return;
    e.ReturnValue = (object) null;
  }

  public virtual void FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs args)
  {
    if (args.Row == null)
      return;
    string oldValue = (string) args.OldValue;
    string fieldValue = (string) sender.GetValue(args.Row, ((PXEventSubscriberAttribute) this)._FieldName);
    string secondFieldValue = (string) sender.GetValue(args.Row, this._secondField.Name);
    this.ReplaceSecondFieldValueWithOldValueIfCurrentValueIsTheSame(sender, args.Row, oldValue, fieldValue, ref secondFieldValue);
    this.SetDummyAttribute(sender, args.Row, fieldValue, secondFieldValue);
  }

  protected virtual void ReplaceSecondFieldValueWithOldValueIfCurrentValueIsTheSame(
    PXCache sender,
    object row,
    string fieldOldValue,
    string fieldValue,
    ref string secondFieldValue)
  {
    if (string.Equals(fieldOldValue, fieldValue, StringComparison.OrdinalIgnoreCase) || !string.Equals(secondFieldValue, fieldValue, StringComparison.OrdinalIgnoreCase) || secondFieldValue == null)
      return;
    sender.SetValueExt(row, this._secondField.Name, (object) fieldOldValue);
    secondFieldValue = fieldOldValue;
  }

  protected virtual void SetDummyAttribute(
    PXCache cache,
    object row,
    string fieldValue,
    string secondFieldValue)
  {
    if (EnumerableExtensions.IsNotIn<string>(fieldValue, (string) null, "~MX~DUMMY~") && secondFieldValue == null)
    {
      if (!this.IsValueListEmpty(PXSelectorAttribute.SelectAll(cache, this._secondField.Name, row)))
        return;
      cache.SetValueExt(row, this._secondField.Name, (object) "~MX~DUMMY~");
    }
    else
    {
      if (fieldValue != null || !(secondFieldValue == "~MX~DUMMY~"))
        return;
      cache.SetValueExt(row, this._secondField.Name, (object) null);
    }
  }

  protected virtual bool IsValueListEmpty(List<object> values)
  {
    if (values.Count <= 0 && !this._allowTheSameValue)
      return true;
    return values.Count <= 1 && this._allowTheSameValue;
  }

  public virtual void RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    int num = (e.Operation & 3) == 2 ? 1 : 0;
    bool flag = (e.Operation & 3) == 1;
    if (num == 0 && !flag || !(sender.GetValue(e.Row, ((PXEventSubscriberAttribute) this)._FieldName) as string == "~MX~DUMMY~") || this.IsValueListEmpty(PXSelectorAttribute.SelectAll(sender, ((PXEventSubscriberAttribute) this)._FieldName, e.Row)))
      return;
    if (sender.RaiseExceptionHandling(((PXEventSubscriberAttribute) this)._FieldName, e.Row, (object) null, (Exception) new PXSetPropertyKeepPreviousException(PXMessages.LocalizeFormat("'{0}' cannot be empty.", new object[1]
    {
      (object) ((PXEventSubscriberAttribute) this)._FieldName
    }))))
      throw new PXRowPersistingException(((PXEventSubscriberAttribute) this)._FieldName, (object) null, "'{0}' cannot be empty.", new object[1]
      {
        (object) ((PXEventSubscriberAttribute) this)._FieldName
      });
  }

  public virtual void RefreshDummyValue(PXCache cache, object row)
  {
    string str1 = cache.GetValue(row, ((PXEventSubscriberAttribute) this)._FieldName) as string;
    string str2 = cache.GetValue(row, this._secondField.Name) as string;
    switch (str1)
    {
      case "~MX~DUMMY~":
        if (this.IsValueListEmpty(PXSelectorAttribute.SelectAll(cache, ((PXEventSubscriberAttribute) this)._FieldName, row)))
          break;
        cache.SetValueExt(row, ((PXEventSubscriberAttribute) this)._FieldName, (object) null);
        break;
      case null:
        if (!EnumerableExtensions.IsNotIn<string>(str2, "~MX~DUMMY~", (string) null) || !this.IsValueListEmpty(PXSelectorAttribute.SelectAll(cache, ((PXEventSubscriberAttribute) this)._FieldName, row)))
          break;
        cache.SetValueExt(row, ((PXEventSubscriberAttribute) this)._FieldName, (object) "~MX~DUMMY~");
        break;
    }
  }

  public class dummyAttributeName : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    MatrixAttributeSelectorAttribute.dummyAttributeName>
  {
    public dummyAttributeName()
      : base("~MX~DUMMY~")
    {
    }
  }
}
