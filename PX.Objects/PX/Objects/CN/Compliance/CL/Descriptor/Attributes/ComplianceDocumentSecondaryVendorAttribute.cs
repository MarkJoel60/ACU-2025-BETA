// Decompiled with JetBrains decompiler
// Type: PX.Objects.CN.Compliance.CL.Descriptor.Attributes.ComplianceDocumentSecondaryVendorAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.AP;
using PX.Objects.CN.Compliance.CL.DAC;

#nullable disable
namespace PX.Objects.CN.Compliance.CL.Descriptor.Attributes;

public class ComplianceDocumentSecondaryVendorAttribute : 
  PXEventSubscriberAttribute,
  IPXFieldUpdatedSubscriber
{
  public void FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    if (!(e.Row is ComplianceDocument row))
      return;
    string vendorName = this.GetVendorName(row.SecondaryVendorID, sender.Graph);
    sender.SetValue<ComplianceDocument.secondaryVendorName>((object) row, (object) vendorName);
  }

  private string GetVendorName(int? vendorId, PXGraph senderGraph)
  {
    if (!vendorId.HasValue)
      return (string) null;
    return ((PXSelectBase<Vendor>) new PXSelect<Vendor, Where<Vendor.bAccountID, Equal<Required<Vendor.bAccountID>>>>(senderGraph)).SelectSingle(new object[1]
    {
      (object) vendorId
    })?.AcctName;
  }
}
