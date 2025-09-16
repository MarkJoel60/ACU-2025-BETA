// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.Attributes.SOOrderPaymentRefAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Objects.AR;
using System;

#nullable disable
namespace PX.Objects.SO.Attributes;

/// <summary>
/// This attribute implements auto-generation of the next check sequential number for Sales Order document<br />
/// according to the settings in the CashAccount and PaymentMethod. <br />
/// </summary>
public class SOOrderPaymentRefAttribute(
  Type cashAccountID,
  Type paymentTypeID,
  Type updateNextNumber) : PaymentRefAttribute(cashAccountID, paymentTypeID, updateNextNumber)
{
  protected virtual bool IsEnabled(PX.Objects.SO.SOOrder order)
  {
    if (order == null)
      return false;
    string arDocType = order.ARDocType;
    return (arDocType != null ? new bool?(EnumerableExtensions.IsIn<string>(arDocType, "CSL", "RCS")) : new bool?()).GetValueOrDefault();
  }

  public override void FieldDefaulting(PXCache sender, PXFieldDefaultingEventArgs e)
  {
    if (!this.IsEnabled(e.Row as PX.Objects.SO.SOOrder))
      return;
    base.FieldDefaulting(sender, e);
  }

  public override void FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
  {
    if (!this.IsEnabled(e.Row as PX.Objects.SO.SOOrder))
      return;
    base.FieldVerifying(sender, e);
  }

  public override void RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    if (!this.IsEnabled(e.Row as PX.Objects.SO.SOOrder))
      return;
    base.RowPersisting(sender, e);
  }

  protected override void DefaultRef(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    if (!this.IsEnabled(e.Row as PX.Objects.SO.SOOrder))
      return;
    base.DefaultRef(sender, e);
  }
}
