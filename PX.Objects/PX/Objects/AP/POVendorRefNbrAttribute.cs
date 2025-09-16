// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.POVendorRefNbrAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.AP;

public class POVendorRefNbrAttribute : BaseVendorRefNbrAttribute
{
  public POVendorRefNbrAttribute()
    : base(typeof (PX.Objects.PO.POReceipt.vendorID))
  {
  }

  protected override bool IsIgnored(PXCache sender, object row)
  {
    PX.Objects.PO.POReceipt poReceipt = (PX.Objects.PO.POReceipt) row;
    if (!(poReceipt.ReceiptType == "RX"))
    {
      bool? nullable = poReceipt.Released;
      if (!nullable.GetValueOrDefault())
      {
        nullable = poReceipt.AutoCreateInvoice;
        if (nullable.GetValueOrDefault())
          return base.IsIgnored(sender, row);
      }
    }
    return true;
  }

  protected override BaseVendorRefNbrAttribute.EntityKey GetEntityKey(PXCache sender, object row)
  {
    return new BaseVendorRefNbrAttribute.EntityKey()
    {
      _DetailID = this.DETAIL_DUMMY,
      _MasterID = this.GetMasterNoteId(typeof (PX.Objects.PO.POReceipt), typeof (PX.Objects.PO.POReceipt.noteID), row)
    };
  }

  public override Guid? GetSiblingID(PXCache sender, object row)
  {
    return (Guid?) sender.GetValue<PX.Objects.PO.POReceipt.noteID>(row);
  }
}
