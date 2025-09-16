// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.CCPaymentProcessing.ExpirationDateAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;
using System.Globalization;

#nullable disable
namespace PX.Objects.AR.CCPaymentProcessing;

public class ExpirationDateAttribute : PXEventSubscriberAttribute, IPXFieldUpdatingSubscriber
{
  public virtual void CacheAttached(PXCache sender)
  {
    base.CacheAttached(sender);
    // ISSUE: method pointer
    sender.Graph.FieldSelecting.AddHandler(sender.GetItemType(), this._FieldName, new PXFieldSelecting((object) this, __methodptr(FieldSelectingHandler)));
  }

  public void FieldSelectingHandler(PXCache sender, PXFieldSelectingEventArgs e)
  {
    DateTime? returnValue = e.ReturnValue as DateTime?;
    if (!returnValue.HasValue)
      return;
    e.ReturnValue = (object) returnValue.Value.AddMonths(-1);
  }

  public void FieldUpdating(PXCache sender, PXFieldUpdatingEventArgs e)
  {
    DateTime? nullable = e.NewValue as DateTime?;
    DateTime result;
    if (!nullable.HasValue && e.NewValue != null && DateTime.TryParseExact(e.NewValue.ToString(), "MM/yy", (IFormatProvider) null, DateTimeStyles.None, out result))
      nullable = new DateTime?(result);
    if (!nullable.HasValue)
      return;
    e.NewValue = (object) nullable.Value.AddMonths(1);
  }
}
