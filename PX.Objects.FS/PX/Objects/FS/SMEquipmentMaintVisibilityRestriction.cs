// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.SMEquipmentMaintVisibilityRestriction
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Objects.AR;
using PX.Objects.CS;

#nullable disable
namespace PX.Objects.FS;

public class SMEquipmentMaintVisibilityRestriction : PXGraphExtension<SMEquipmentMaint>
{
  public PXSetup<PX.Objects.CR.BAccount, Where<PX.Objects.CR.BAccount.bAccountID, Equal<Current<FSEquipment.ownerID>>>> currentBAccount;

  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.visibilityRestriction>();

  [PXMergeAttributes]
  [RestrictBranchByCustomer(typeof (FSEquipment.ownerID), typeof (PX.Objects.CR.BAccount.cOrgBAccountID), ResetBranch = true)]
  public void _(PX.Data.Events.CacheAttached<FSEquipment.branchID> e)
  {
  }
}
