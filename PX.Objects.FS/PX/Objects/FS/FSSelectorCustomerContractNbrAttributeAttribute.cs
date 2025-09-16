// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.FSSelectorCustomerContractNbrAttributeAttribute
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;

#nullable disable
namespace PX.Objects.FS;

public class FSSelectorCustomerContractNbrAttributeAttribute(
  System.Type serviceContract_RecordType,
  System.Type CurrentCustomer,
  System.Type Where) : PXSelectorAttribute(BqlCommand.Compose(new System.Type[22]
{
  typeof (Search2<,,,>),
  typeof (FSServiceContract.customerContractNbr),
  typeof (LeftJoin<,,>),
  typeof (PX.Objects.AR.Customer),
  typeof (On<PX.Objects.AR.Customer.bAccountID, Equal<FSServiceContract.customerID>>),
  typeof (LeftJoin<,>),
  typeof (PX.Objects.CR.Address),
  typeof (On<PX.Objects.CR.Address.bAccountID, Equal<PX.Objects.AR.Customer.bAccountID>, And<PX.Objects.CR.Address.addressID, Equal<PX.Objects.AR.Customer.defAddressID>>>),
  typeof (Where2<,>),
  typeof (Where<,,>),
  typeof (FSServiceContract.customerID),
  typeof (Equal<>),
  typeof (Current<>),
  CurrentCustomer,
  typeof (And<,,>),
  typeof (FSServiceContract.recordType),
  typeof (Equal<>),
  serviceContract_RecordType,
  typeof (And<Where<PX.Objects.AR.Customer.bAccountID, IsNull, Or<Match<PX.Objects.AR.Customer, Current<AccessInfo.userName>>>>>),
  typeof (And<>),
  Where,
  typeof (OrderBy<Desc<FSServiceContract.customerContractNbr>>)
}), new System.Type[8]
{
  typeof (FSServiceContract.refNbr),
  typeof (FSServiceContract.customerID),
  typeof (PX.Objects.AR.Customer.acctName),
  typeof (FSServiceContract.status),
  typeof (FSServiceContract.customerLocationID),
  typeof (PX.Objects.CR.Address.addressLine1),
  typeof (PX.Objects.CR.Address.city),
  typeof (PX.Objects.CR.Address.state)
})
{
  public FSSelectorCustomerContractNbrAttributeAttribute(
    System.Type serviceContract_RecordType,
    System.Type CurrentCustomer)
    : this(serviceContract_RecordType, CurrentCustomer, typeof (Where<True, Equal<True>>))
  {
  }
}
