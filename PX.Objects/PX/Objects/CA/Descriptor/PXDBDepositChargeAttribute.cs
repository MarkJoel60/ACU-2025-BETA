// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.Descriptor.PXDBDepositChargeAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.CA.Descriptor;

public class PXDBDepositChargeAttribute(int length) : PXDBStringAttribute(length)
{
  public virtual void FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
  {
    base.FieldVerifying(sender, e);
    CADepositCharge row = e.Row as CADepositCharge;
    string str = e.NewValue?.ToString();
    if (row == null)
      return;
    CADepositCharge caDepositCharge = (CADepositCharge) null;
    if (((PXEventSubscriberAttribute) this)._FieldName == "EntryTypeID")
      caDepositCharge = PXResultset<CADepositCharge>.op_Implicit(PXSelectBase<CADepositCharge, PXSelect<CADepositCharge, Where<CADepositCharge.tranType, Equal<Required<CADepositCharge.tranType>>, And<CADepositCharge.refNbr, Equal<Required<CADepositCharge.refNbr>>, And<CADepositCharge.entryTypeID, Equal<Required<CADepositCharge.entryTypeID>>, And<CADepositCharge.paymentMethodID, Equal<Required<CADepositCharge.paymentMethodID>>>>>>>.Config>.Select(sender.Graph, new object[4]
      {
        (object) row?.TranType,
        (object) row?.RefNbr,
        (object) str,
        (object) row?.PaymentMethodID
      }));
    else if (((PXEventSubscriberAttribute) this)._FieldName == "PaymentMethodID")
      caDepositCharge = PXResultset<CADepositCharge>.op_Implicit(PXSelectBase<CADepositCharge, PXSelect<CADepositCharge, Where<CADepositCharge.tranType, Equal<Required<CADepositCharge.tranType>>, And<CADepositCharge.refNbr, Equal<Required<CADepositCharge.refNbr>>, And<CADepositCharge.entryTypeID, Equal<Required<CADepositCharge.entryTypeID>>, And<CADepositCharge.paymentMethodID, Equal<Required<CADepositCharge.paymentMethodID>>>>>>>.Config>.Select(sender.Graph, new object[4]
      {
        (object) row?.TranType,
        (object) row?.RefNbr,
        (object) row?.EntryTypeID,
        (object) str
      }));
    PXEntryStatus status = sender.GetStatus((object) row);
    if (caDepositCharge != null && (status == 2 || status == 1))
    {
      sender.RaiseExceptionHandling<CADepositCharge.entryTypeID>((object) row, (object) row.EntryTypeID, (Exception) new PXSetPropertyException("The charge with the same Entry Type and Payment Method already exists.", (PXErrorLevel) 4));
      sender.RaiseExceptionHandling<CADepositCharge.paymentMethodID>((object) row, (object) row.PaymentMethodID, (Exception) new PXSetPropertyException("The charge with the same Entry Type and Payment Method already exists.", (PXErrorLevel) 4));
      throw new PXSetPropertyException("The charge with the same Entry Type and Payment Method already exists.", (PXErrorLevel) 4);
    }
  }
}
