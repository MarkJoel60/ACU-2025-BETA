// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.MasterContractMaint
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;

#nullable disable
namespace PX.Objects.FS;

public class MasterContractMaint : PXGraph<MasterContractMaint, FSMasterContract>
{
  public PXSelect<PX.Objects.CR.BAccount> BAccountRecords;
  public PXSelectJoin<FSMasterContract, LeftJoin<PX.Objects.CR.BAccount, On<PX.Objects.CR.BAccount.bAccountID, Equal<FSMasterContract.customerID>>, LeftJoin<PX.Objects.AR.Customer, On<PX.Objects.AR.Customer.bAccountID, Equal<FSMasterContract.customerID>>>>, Where<PX.Objects.AR.Customer.bAccountID, IsNull, Or<Match<PX.Objects.AR.Customer, Current<AccessInfo.userName>>>>> MasterContracts;

  public MasterContractMaint()
  {
    PXUIFieldAttribute.SetDisplayName<PX.Objects.CR.BAccount.acctName>(((PXSelectBase) this.BAccountRecords).Cache, "Customer Name");
    PXUIFieldAttribute.SetRequired<PX.Objects.CR.BAccount.acctName>(((PXSelectBase) this.BAccountRecords).Cache, false);
  }
}
