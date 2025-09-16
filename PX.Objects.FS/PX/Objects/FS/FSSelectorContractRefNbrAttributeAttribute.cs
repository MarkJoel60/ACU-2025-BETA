// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.FSSelectorContractRefNbrAttributeAttribute
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;

#nullable disable
namespace PX.Objects.FS;

public class FSSelectorContractRefNbrAttributeAttribute(
  System.Type serviceContract_RecordType,
  System.Type TWhere) : PXSelectorAttribute(((IBqlTemplate) BqlTemplate.OfCommand<Search2<FSServiceContract.refNbr, LeftJoin<PX.Objects.AR.Customer, On<PX.Objects.AR.Customer.bAccountID, Equal<FSServiceContract.customerID>>, LeftJoin<PX.Objects.CR.Address, On<PX.Objects.CR.Address.bAccountID, Equal<PX.Objects.AR.Customer.bAccountID>, And<PX.Objects.CR.Address.addressID, Equal<PX.Objects.AR.Customer.defAddressID>>>>>, Where<FSServiceContract.recordType, Equal<BqlPlaceholder.A>, And2<Where<PX.Objects.AR.Customer.bAccountID, IsNull, Or<Match<PX.Objects.AR.Customer, Current<AccessInfo.userName>>>>, And<BqlPlaceholder.B>>>, OrderBy<Desc<FSServiceContract.refNbr>>>>.Replace<BqlPlaceholder.A>(serviceContract_RecordType).Replace<BqlPlaceholder.B>(TWhere)).ToType(), new System.Type[9]
{
  typeof (FSServiceContract.refNbr),
  typeof (FSServiceContract.customerContractNbr),
  typeof (FSServiceContract.customerID),
  typeof (PX.Objects.AR.Customer.acctName),
  typeof (FSServiceContract.status),
  typeof (FSServiceContract.customerLocationID),
  typeof (PX.Objects.CR.Address.addressLine1),
  typeof (PX.Objects.CR.Address.city),
  typeof (PX.Objects.CR.Address.state)
})
{
  public FSSelectorContractRefNbrAttributeAttribute(System.Type serviceContract_RecordType)
    : this(serviceContract_RecordType, typeof (Where<True, Equal<True>>))
  {
  }
}
