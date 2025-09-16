// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.FSSelectorPPFRServiceContract
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.FS;

public class FSSelectorPPFRServiceContract : PXSelectorAttribute
{
  public FSSelectorPPFRServiceContract(Type currentBillingCustomerID)
    : base(BqlCommand.Compose(new Type[9]
    {
      typeof (Search<,,>),
      typeof (FSServiceContract.serviceContractID),
      typeof (Where<,,>),
      typeof (FSServiceContract.billCustomerID),
      typeof (Equal<>),
      typeof (Current<>),
      currentBillingCustomerID,
      typeof (And<Where<FSServiceContract.status, Equal<ListField_Status_ServiceContract.Active>, And<Where2<Where<FSServiceContract.billingType, Equal<ListField.ServiceContractBillingType.standardizedBillings>>, Or<FSServiceContract.isFixedRateContract, Equal<True>>>>>>),
      typeof (OrderBy<Asc<FSServiceContract.refNbr>>)
    }), new Type[10]
    {
      typeof (FSServiceContract.customerID),
      typeof (FSServiceContract.customerLocationID),
      typeof (FSServiceContract.refNbr),
      typeof (FSServiceContract.customerContractNbr),
      typeof (FSServiceContract.status),
      typeof (FSServiceContract.vendorID),
      typeof (FSServiceContract.sourcePrice),
      typeof (FSServiceContract.billCustomerID),
      typeof (FSServiceContract.billLocationID),
      typeof (FSServiceContract.docDesc)
    })
  {
    this.SubstituteKey = typeof (FSServiceContract.refNbr);
    this.DescriptionField = typeof (FSServiceContract.docDesc);
    this.Filterable = true;
  }

  public FSSelectorPPFRServiceContract(Type currentCustomerId, Type currentLocationId)
    : base(BqlCommand.Compose(new Type[9]
    {
      typeof (Search<,,>),
      typeof (FSServiceContract.serviceContractID),
      typeof (Where<,,>),
      typeof (FSServiceContract.customerID),
      typeof (Equal<>),
      typeof (Current<>),
      currentCustomerId,
      typeof (And<Where<FSServiceContract.status, Equal<ListField_Status_ServiceContract.Active>, And<Where2<Where<FSServiceContract.billingType, Equal<ListField.ServiceContractBillingType.standardizedBillings>>, Or<FSServiceContract.isFixedRateContract, Equal<True>>>>>>),
      typeof (OrderBy<Asc<FSServiceContract.refNbr>>)
    }), new Type[10]
    {
      typeof (FSServiceContract.customerID),
      typeof (FSServiceContract.customerLocationID),
      typeof (FSServiceContract.refNbr),
      typeof (FSServiceContract.customerContractNbr),
      typeof (FSServiceContract.status),
      typeof (FSServiceContract.vendorID),
      typeof (FSServiceContract.sourcePrice),
      typeof (FSServiceContract.billCustomerID),
      typeof (FSServiceContract.billLocationID),
      typeof (FSServiceContract.docDesc)
    })
  {
    this.SubstituteKey = typeof (FSServiceContract.refNbr);
    this.DescriptionField = typeof (FSServiceContract.docDesc);
    this.Filterable = true;
  }
}
