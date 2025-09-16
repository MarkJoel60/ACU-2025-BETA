// Decompiled with JetBrains decompiler
// Type: PX.Objects.CS.PXNotificationFormatAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

#nullable disable
namespace PX.Objects.CS;

public class PXNotificationFormatAttribute : PXEventSubscriberAttribute
{
  protected readonly Type report;
  protected readonly Type template;
  protected readonly Type where;
  protected PXView view;

  public PXNotificationFormatAttribute(Type report, Type template)
    : this(report, template, (Type) null)
  {
  }

  public PXNotificationFormatAttribute(Type report, Type template, Type where)
  {
    this.report = report;
    this.template = template;
    this.where = where;
    if (BqlCommand.GetItemType(report) != BqlCommand.GetItemType(template))
      throw new PXArgumentException((string) null, "Template and report must be defined in same class.");
  }

  public virtual void CacheAttached(PXCache sender)
  {
    base.CacheAttached(sender);
    PXGraph.RowUpdatingEvents rowUpdating = sender.Graph.RowUpdating;
    Type itemType1 = sender.GetItemType();
    PXNotificationFormatAttribute notificationFormatAttribute1 = this;
    // ISSUE: virtual method pointer
    PXRowUpdating pxRowUpdating = new PXRowUpdating((object) notificationFormatAttribute1, __vmethodptr(notificationFormatAttribute1, OnRowUpdating));
    rowUpdating.AddHandler(itemType1, pxRowUpdating);
    if (BqlCommand.GetItemType(this.report).IsAssignableFrom(sender.GetItemType()))
    {
      PXGraph.RowUpdatedEvents rowUpdated = sender.Graph.RowUpdated;
      Type itemType2 = sender.GetItemType();
      PXNotificationFormatAttribute notificationFormatAttribute2 = this;
      // ISSUE: virtual method pointer
      PXRowUpdated pxRowUpdated = new PXRowUpdated((object) notificationFormatAttribute2, __vmethodptr(notificationFormatAttribute2, OnRowUpdated));
      rowUpdated.AddHandler(itemType2, pxRowUpdated);
    }
    else
    {
      if (!(this.where != (Type) null))
        return;
      Type[] sourceArray = BqlCommand.Decompose(this.where);
      if (typeof (NotificationRecipient).IsAssignableFrom(sender.GetItemType()))
      {
        for (int index = 0; index < sourceArray.Length; ++index)
        {
          if (typeof (IBqlField).IsAssignableFrom(sourceArray[index]) && BqlCommand.GetItemType(sourceArray[index]) == typeof (NotificationRecipient))
            sourceArray[index] = sender.GetItemType().GetNestedType(sourceArray[index].Name);
        }
      }
      Type[] destinationArray = new Type[sourceArray.Length + 2];
      destinationArray[0] = typeof (Select<,>);
      destinationArray[1] = sender.GetItemType();
      Array.Copy((Array) sourceArray, 0, (Array) destinationArray, 2, sourceArray.Length);
      this.view = new PXView(sender.Graph, false, BqlCommand.CreateInstance(new Type[1]
      {
        BqlCommand.Compose(destinationArray)
      }));
      PXGraph.RowUpdatedEvents rowUpdated = sender.Graph.RowUpdated;
      Type itemType3 = BqlCommand.GetItemType(this.report);
      PXNotificationFormatAttribute notificationFormatAttribute3 = this;
      // ISSUE: virtual method pointer
      PXRowUpdated pxRowUpdated = new PXRowUpdated((object) notificationFormatAttribute3, __vmethodptr(notificationFormatAttribute3, OnRowUpdated));
      rowUpdated.AddHandler(itemType3, pxRowUpdated);
    }
  }

  protected virtual void OnRowUpdating(PXCache cache, PXRowUpdatingEventArgs e)
  {
    string str1 = (string) cache.GetValue(e.NewRow, this._FieldOrdinal);
    string str2 = (string) cache.GetValue(e.Row, this._FieldOrdinal);
    if (str1 == str2)
      return;
    if (this.GetSource(cache, e.NewRow, this.report) == null ? this.GetSource(cache, e.NewRow, this.template) == null || PXNotificationFormatAttribute.ValidateFormat(NotificationFormat.TemplateList, str1) : PXNotificationFormatAttribute.ValidateFormat(NotificationFormat.ReportList, str1))
      return;
    PXSetPropertyException propertyException = new PXSetPropertyException("Specified format isn't supported for this type notification.");
    cache.RaiseExceptionHandling(this._FieldName, e.NewRow, (object) str1, (Exception) propertyException);
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void OnRowUpdated(PXCache cache, PXRowUpdatedEventArgs e)
  {
    NotificationFormat.ListAttribute list = (NotificationFormat.ListAttribute) null;
    object objA1 = cache.GetValue(e.Row, this.template.Name);
    object objA2 = cache.GetValue(e.Row, this.report.Name);
    string str = "H";
    if (!object.Equals(objA1, cache.GetValue(e.OldRow, this.template.Name)) && objA1 != null && objA2 == null)
      list = NotificationFormat.TemplateList;
    else if (!object.Equals(objA2, cache.GetValue(e.OldRow, this.report.Name)) && objA2 != null)
    {
      list = NotificationFormat.ReportList;
      str = "P";
    }
    if (list == null)
      return;
    if (this.view == null)
    {
      if (PXNotificationFormatAttribute.ValidateFormat(list, (string) cache.GetValue(e.Row, this._FieldOrdinal)))
        return;
      cache.RaiseExceptionHandling(this._FieldName, e.Row, (object) null, (Exception) null);
      cache.SetValue(e.Row, this._FieldName, (object) str);
    }
    else
    {
      PXCache cache1 = this.view.Cache;
      foreach (object obj in this.view.SelectMultiBound(new object[1]
      {
        e.Row
      }, (object[]) null))
      {
        if (!PXNotificationFormatAttribute.ValidateFormat(list, (string) cache1.GetValue(obj, this._FieldOrdinal)))
        {
          object copy = cache1.CreateCopy(obj);
          cache.SetValue(e.Row, this._FieldName, (object) str);
          cache1.Update(copy);
        }
      }
    }
  }

  private static bool ValidateFormat(NotificationFormat.ListAttribute list, string value)
  {
    return ((IEnumerable<string>) list.AllowedValues).Any<string>((Func<string, bool>) (e => e == value));
  }

  private object GetSource(PXCache sender, object row, Type sourceType)
  {
    return sender.GetItemType() == BqlCommand.GetItemType(sourceType) ? sender.GetValue(row, sourceType.Name) : sender.Graph.Caches[BqlCommand.GetItemType(sourceType)].GetValue(sender.Graph.Caches[BqlCommand.GetItemType(sourceType)].Current, sourceType.Name);
  }

  public static string ValidBodyFormat(string format)
  {
    return !PXNotificationFormatAttribute.ValidateFormat(NotificationFormat.TemplateList, format) ? "H" : format;
  }
}
