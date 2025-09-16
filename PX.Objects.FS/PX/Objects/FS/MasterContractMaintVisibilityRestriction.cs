// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.MasterContractMaintVisibilityRestriction
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Objects.AR;
using PX.Objects.CS;

#nullable disable
namespace PX.Objects.FS;

public class MasterContractMaintVisibilityRestriction : PXGraphExtension<MasterContractMaint>
{
  public PXSetup<PX.Objects.CR.BAccount, Where<PX.Objects.CR.BAccount.bAccountID, Equal<Current<FSMasterContract.customerID>>>> currentBAccount;

  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.visibilityRestriction>();

  [PXMergeAttributes]
  [RestrictBranchByCustomer(typeof (FSMasterContract.customerID), typeof (PX.Objects.CR.BAccount.cOrgBAccountID), ResetBranch = true)]
  public virtual void FSMasterContract_BranchID_CacheAttached(PXCache sender)
  {
  }

  [PXMergeAttributes]
  [RestrictCustomerByUserBranches]
  public virtual void FSMasterContract_CustomerID_CacheAttached(PXCache sender)
  {
  }
}
