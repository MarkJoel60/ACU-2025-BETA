// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.APReleaseProcessExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.AP;
using PX.Objects.CS;
using PX.Objects.DR;
using PX.Objects.GL;
using PX.Objects.PO;

#nullable disable
namespace PX.Objects.PM;

public class APReleaseProcessExt : CommitmentTracking<APReleaseProcess>
{
  public new static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.projectAccounting>();

  [PXOverride]
  public virtual void ReleaseInvoiceTransactionPostProcessing(
    JournalEntry je,
    PX.Objects.AP.APInvoice apdoc,
    PXResult<PX.Objects.AP.APTran, APTax, PX.Objects.TX.Tax, DRDeferredCode, LandedCostCode, PX.Objects.IN.InventoryItem, APTaxTran> r,
    PX.Objects.GL.GLTran tran)
  {
    PX.Objects.AP.APTran tran1 = PXResult<PX.Objects.AP.APTran, APTax, PX.Objects.TX.Tax, DRDeferredCode, LandedCostCode, PX.Objects.IN.InventoryItem, APTaxTran>.op_Implicit(r);
    if (!this.CopyProjectFromAPTran(apdoc, tran1))
      return;
    tran.ProjectID = tran1.ProjectID;
    tran.TaskID = tran1.TaskID;
    tran.CostCodeID = tran1.CostCodeID;
  }

  protected virtual bool CopyProjectFromAPTran(PX.Objects.AP.APInvoice doc, PX.Objects.AP.APTran tran)
  {
    if (doc.IsChildRetainageDocument())
      return false;
    PX.Objects.GL.Account account = PX.Objects.GL.Account.PK.Find((PXGraph) this.Base, tran.AccountID);
    return account == null || account.AccountGroupID.HasValue;
  }
}
