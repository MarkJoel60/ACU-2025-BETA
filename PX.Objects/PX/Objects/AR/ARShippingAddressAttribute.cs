// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.ARShippingAddressAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CS;
using System;

#nullable disable
namespace PX.Objects.AR;

public class ARShippingAddressAttribute(Type SelectType) : AddressAttribute(typeof (ARShippingAddress.addressID), typeof (ARShippingAddress.isDefaultBillAddress), SelectType)
{
  public override void CacheAttached(PXCache sender)
  {
    base.CacheAttached(sender);
    PXGraph.FieldVerifyingEvents fieldVerifying = sender.Graph.FieldVerifying;
    ARShippingAddressAttribute addressAttribute = this;
    // ISSUE: virtual method pointer
    PXFieldVerifying pxFieldVerifying = new PXFieldVerifying((object) addressAttribute, __vmethodptr(addressAttribute, Record_Override_FieldVerifying));
    fieldVerifying.AddHandler<ARShippingAddress.overrideAddress>(pxFieldVerifying);
  }

  public override void DefaultRecord(PXCache sender, object DocumentRow, object Row)
  {
    this.DefaultAddress<ARShippingAddress, ARShippingAddress.addressID>(sender, DocumentRow, Row);
  }

  public override void CopyRecord(
    PXCache sender,
    object DocumentRow,
    object SourceRow,
    bool clone)
  {
    this.CopyAddress<ARShippingAddress, ARShippingAddress.addressID>(sender, DocumentRow, SourceRow, clone);
  }

  public override void Record_IsDefault_FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
  {
  }

  public virtual void Record_Override_FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
  {
    PXFieldVerifyingEventArgs verifyingEventArgs1 = e;
    object obj1;
    if (e.NewValue != null)
    {
      bool? newValue = (bool?) e.NewValue;
      bool flag = false;
      obj1 = (object) (newValue.GetValueOrDefault() == flag & newValue.HasValue);
    }
    else
      obj1 = e.NewValue;
    verifyingEventArgs1.NewValue = obj1;
    try
    {
      this.Address_IsDefaultAddress_FieldVerifying<ARShippingAddress>(sender, e);
    }
    finally
    {
      PXFieldVerifyingEventArgs verifyingEventArgs2 = e;
      object obj2;
      if (e.NewValue != null)
      {
        bool? newValue = (bool?) e.NewValue;
        bool flag = false;
        obj2 = (object) (newValue.GetValueOrDefault() == flag & newValue.HasValue);
      }
      else
        obj2 = e.NewValue;
      verifyingEventArgs2.NewValue = obj2;
    }
  }

  protected override void Record_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    base.Record_RowSelected(sender, e);
    if (e.Row == null)
      return;
    PXUIFieldAttribute.SetEnabled<ARShippingAddress.overrideAddress>(sender, e.Row, sender.AllowUpdate);
    PXUIFieldAttribute.SetEnabled<ARShippingAddress.isValidated>(sender, e.Row, false);
  }
}
