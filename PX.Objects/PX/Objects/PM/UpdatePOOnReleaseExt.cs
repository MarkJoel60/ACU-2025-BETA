// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.UpdatePOOnReleaseExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.AP;
using PX.Objects.CS;
using PX.Objects.PO.GraphExtensions.APReleaseProcessExt;
using System;

#nullable disable
namespace PX.Objects.PM;

public class UpdatePOOnReleaseExt : 
  PXGraphExtension<UpdatePOOnRelease, APReleaseProcess.MultiCurrency, APReleaseProcess>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.projectAccounting>();

  [PXOverride]
  public virtual PX.Objects.IN.INRegister CreatePPVAdjustment(
    PX.Objects.AP.APInvoice apdoc,
    Func<PX.Objects.AP.APInvoice, PX.Objects.IN.INRegister> baseCreatePPVAdjustment)
  {
    using (new SkipDefaultingFromLocationScope())
      return baseCreatePPVAdjustment(apdoc);
  }

  [PXOverride]
  public virtual void SetProjectForPPVTransaction(
    PX.Objects.GL.GLTran ppvTran,
    PX.Objects.AP.APTran tran,
    PX.Objects.PO.POReceiptLine rctLine,
    Action<PX.Objects.GL.GLTran, PX.Objects.AP.APTran, PX.Objects.PO.POReceiptLine> baseMethod)
  {
    if (this.IsProjectAccount(ppvTran.AccountID))
    {
      ppvTran.ProjectID = tran.ProjectID;
      ppvTran.TaskID = tran.TaskID;
      ppvTran.CostCodeID = tran.CostCodeID;
    }
    else if (tran.TaskID.HasValue)
    {
      PX.Objects.GL.Account account = PX.Objects.GL.Account.PK.Find((PXGraph) ((PXGraphExtension<APReleaseProcess>) this).Base, ppvTran.AccountID);
      PMProject pmProject = PMProject.PK.Find((PXGraph) ((PXGraphExtension<APReleaseProcess>) this).Base, tran.ProjectID);
      throw new PXException("The document cannot be released because the {0} line is associated with the {1} project and the Purchase Price Variance account ({2}) specified in this line is not associated with any account group.", new object[3]
      {
        (object) tran.LineNbr,
        (object) pmProject?.ContractCD.Trim(),
        (object) account?.AccountCD.Trim()
      });
    }
  }

  public bool IsProjectAccount(int? accountID)
  {
    PX.Objects.GL.Account account = PX.Objects.GL.Account.PK.Find((PXGraph) ((PXGraphExtension<APReleaseProcess>) this).Base, accountID);
    return account != null && account.AccountGroupID.HasValue;
  }
}
