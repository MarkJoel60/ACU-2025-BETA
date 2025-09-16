// Decompiled with JetBrains decompiler
// Type: PX.Objects.CN.Compliance.CL.Descriptor.Attributes.ComplianceDocumentCustomerAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.AR;
using PX.Objects.CN.Compliance.CL.DAC;

#nullable disable
namespace PX.Objects.CN.Compliance.CL.Descriptor.Attributes;

public class ComplianceDocumentCustomerAttribute : 
  PXEventSubscriberAttribute,
  IPXFieldUpdatedSubscriber
{
  public void FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    if (!(e.Row is ComplianceDocument row))
      return;
    string customerName = this.GetCustomerName(row.CustomerID, sender.Graph);
    sender.SetValue<ComplianceDocument.customerName>((object) row, (object) customerName);
  }

  private string GetCustomerName(int? customerId, PXGraph senderGraph)
  {
    if (!customerId.HasValue)
      return (string) null;
    return ((PXSelectBase<Customer>) new PXSelect<Customer, Where<Customer.bAccountID, Equal<Required<Customer.bAccountID>>>>(senderGraph)).SelectSingle(new object[1]
    {
      (object) customerId
    })?.AcctName;
  }
}
