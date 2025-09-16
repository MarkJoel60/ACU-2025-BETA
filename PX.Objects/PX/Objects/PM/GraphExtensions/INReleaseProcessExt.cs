// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.GraphExtensions.INReleaseProcessExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CS;
using PX.Objects.GL;
using PX.Objects.IN;
using PX.Objects.IN.InventoryRelease;
using System;

#nullable disable
namespace PX.Objects.PM.GraphExtensions;

public class INReleaseProcessExt : PXGraphExtension<INReleaseProcess>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.projectAccounting>();

  [PXOverride]
  public virtual GLTran InsertGLSalesDebit(
    JournalEntry je,
    GLTran tran,
    INReleaseProcess.GLTranInsertionContext context,
    Func<JournalEntry, GLTran, INReleaseProcess.GLTranInsertionContext, GLTran> baseMethod)
  {
    if (context?.INTran != null && this.IsProjectAccount(tran.AccountID))
    {
      tran.ProjectID = context.INTran.ProjectID;
      tran.TaskID = context.INTran.TaskID;
      if (context.INTran.CostCodeID.HasValue)
        tran.CostCodeID = context.INTran.CostCodeID;
    }
    return baseMethod(je, tran, context);
  }

  [PXOverride]
  public virtual GLTran InsertGLSalesCredit(
    JournalEntry je,
    GLTran tran,
    INReleaseProcess.GLTranInsertionContext context,
    Func<JournalEntry, GLTran, INReleaseProcess.GLTranInsertionContext, GLTran> baseMethod)
  {
    if (context?.INTran != null && this.IsProjectAccount(tran.AccountID))
    {
      tran.ProjectID = context.INTran.ProjectID;
      tran.TaskID = context.INTran.TaskID;
      if (context.INTran.CostCodeID.HasValue)
        tran.CostCodeID = context.INTran.CostCodeID;
    }
    return baseMethod(je, tran, context);
  }

  [PXOverride]
  public virtual GLTran InsertGLNonStockCostDebit(
    JournalEntry je,
    GLTran tran,
    INReleaseProcess.GLTranInsertionContext context,
    Func<JournalEntry, GLTran, INReleaseProcess.GLTranInsertionContext, GLTran> baseMethod)
  {
    if ((context != null ? (!((bool?) context.INTran?.AccrueCost).GetValueOrDefault() ? 1 : 0) : 1) != 0 && this.IsProjectAccount(tran.AccountID))
    {
      tran.ProjectID = context.INTran.ProjectID;
      tran.TaskID = context.INTran.TaskID;
      if (context.INTran.CostCodeID.HasValue)
        tran.CostCodeID = context.INTran.CostCodeID;
    }
    return baseMethod(je, tran, context);
  }

  [PXOverride]
  public virtual GLTran InsertGLNonStockCostCredit(
    JournalEntry je,
    GLTran tran,
    INReleaseProcess.GLTranInsertionContext context,
    Func<JournalEntry, GLTran, INReleaseProcess.GLTranInsertionContext, GLTran> baseMethod)
  {
    if (context?.INTran != null && this.IsProjectAccount(tran.AccountID))
    {
      tran.ProjectID = context.INTran.ProjectID;
      tran.TaskID = (int?) context.INTran?.TaskID;
      INTran inTran = context.INTran;
      if ((inTran != null ? (inTran.CostCodeID.HasValue ? 1 : 0) : 0) != 0)
        tran.CostCodeID = context.INTran.CostCodeID;
    }
    return baseMethod(je, tran, context);
  }

  [PXOverride]
  public virtual GLTran InsertGLCostsDebit(
    JournalEntry je,
    GLTran tran,
    INReleaseProcess.GLTranInsertionContext context,
    Func<JournalEntry, GLTran, INReleaseProcess.GLTranInsertionContext, GLTran> baseMethod)
  {
    if (context?.INTran != null && this.IsProjectAccount(tran.AccountID))
    {
      if (this.Base.IsOneStepTransfer())
      {
        this.InsertGLCostsDebitForOneStepTransfer(je, tran, context);
      }
      else
      {
        GLTran glTran1 = tran;
        int? nullable1 = (int?) context.INTran?.ProjectID;
        int? nullable2 = nullable1 ?? tran.ProjectID;
        glTran1.ProjectID = nullable2;
        GLTran glTran2 = tran;
        INTran inTran1 = context.INTran;
        int? nullable3;
        if (inTran1 == null)
        {
          nullable1 = new int?();
          nullable3 = nullable1;
        }
        else
          nullable3 = inTran1.TaskID;
        glTran2.TaskID = nullable3;
        INTran inTran2 = context.INTran;
        int num;
        if (inTran2 == null)
        {
          num = 0;
        }
        else
        {
          nullable1 = inTran2.CostCodeID;
          num = nullable1.HasValue ? 1 : 0;
        }
        if (num != 0)
          tran.CostCodeID = context.INTran.CostCodeID;
      }
    }
    return baseMethod(je, tran, context);
  }

  private void InsertGLCostsDebitForOneStepTransfer(
    JournalEntry je,
    GLTran tran,
    INReleaseProcess.GLTranInsertionContext context)
  {
    short? invtMult = context.TranCost.InvtMult;
    if ((invtMult.HasValue ? new int?((int) invtMult.GetValueOrDefault()) : new int?()).GetValueOrDefault() == -1)
    {
      tran.ProjectID = context.INTran.ProjectID;
      tran.TaskID = context.INTran.TaskID;
      if (!context.INTran.CostCodeID.HasValue)
        return;
      tran.CostCodeID = context.INTran.CostCodeID;
    }
    else
    {
      tran.ProjectID = context.INTran.ToProjectID ?? context.INTran.ProjectID;
      GLTran glTran = tran;
      int? nullable1 = context.INTran.ToTaskID;
      int? nullable2 = nullable1 ?? context.INTran.TaskID;
      glTran.TaskID = nullable2;
      nullable1 = context.INTran.CostCodeID;
      if (!nullable1.HasValue)
        return;
      tran.CostCodeID = context.INTran.CostCodeID;
    }
  }

  [PXOverride]
  public virtual GLTran InsertGLCostsCredit(
    JournalEntry je,
    GLTran tran,
    INReleaseProcess.GLTranInsertionContext context,
    Func<JournalEntry, GLTran, INReleaseProcess.GLTranInsertionContext, GLTran> baseMethod)
  {
    if (context?.INTran != null && this.IsProjectAccount(tran.AccountID))
    {
      tran.ProjectID = context.INTran.ProjectID;
      tran.TaskID = context.INTran.TaskID;
      if (context.INTran.CostCodeID.HasValue)
        tran.CostCodeID = context.INTran.CostCodeID;
    }
    return baseMethod(je, tran, context);
  }

  [PXOverride]
  public virtual GLTran InsertGLCostsOversold(
    JournalEntry je,
    GLTran tran,
    INReleaseProcess.GLTranInsertionContext context,
    Func<JournalEntry, GLTran, INReleaseProcess.GLTranInsertionContext, GLTran> baseMethod)
  {
    if (context.TranCost != null && context.TranCost.TranType == "TRX")
    {
      GLTran glTran1 = tran;
      int? nullable1 = context.TranCost.COGSAcctID;
      int? nullable2 = !nullable1.HasValue ? ProjectDefaultAttribute.NonProject() : context.INTran.ProjectID;
      glTran1.ProjectID = nullable2;
      GLTran glTran2 = tran;
      nullable1 = context.TranCost.COGSAcctID;
      int? nullable3;
      if (nullable1.HasValue)
      {
        nullable3 = context.INTran.TaskID;
      }
      else
      {
        nullable1 = new int?();
        nullable3 = nullable1;
      }
      glTran2.TaskID = nullable3;
      nullable1 = context.INTran.CostCodeID;
      if (nullable1.HasValue)
        tran.CostCodeID = context.INTran.CostCodeID;
    }
    else if (context?.INTran != null && this.IsProjectAccount(tran.AccountID))
    {
      tran.ProjectID = context.INTran.ProjectID;
      tran.TaskID = context.INTran.TaskID;
      if (context.INTran.CostCodeID.HasValue)
        tran.CostCodeID = context.INTran.CostCodeID;
    }
    return baseMethod(je, tran, context);
  }

  [PXOverride]
  public virtual GLTran InsertGLCostsVarianceCredit(
    JournalEntry je,
    GLTran tran,
    INReleaseProcess.GLTranInsertionContext context,
    Func<JournalEntry, GLTran, INReleaseProcess.GLTranInsertionContext, GLTran> baseMethod)
  {
    if (context?.INTran != null && this.IsProjectAccount(tran.AccountID))
    {
      tran.ProjectID = context.INTran.ProjectID;
      tran.TaskID = context.INTran.TaskID;
      if (context.INTran.CostCodeID.HasValue)
        tran.CostCodeID = context.INTran.CostCodeID;
    }
    return baseMethod(je, tran, context);
  }

  [PXOverride]
  public virtual GLTran InsertGLCostsVarianceDebit(
    JournalEntry je,
    GLTran tran,
    INReleaseProcess.GLTranInsertionContext context,
    Func<JournalEntry, GLTran, INReleaseProcess.GLTranInsertionContext, GLTran> baseMethod)
  {
    if (context?.INTran != null && this.IsProjectAccount(tran.AccountID))
    {
      tran.ProjectID = context.INTran.ProjectID;
      tran.TaskID = context.INTran.TaskID;
      if (context.INTran.CostCodeID.HasValue)
        tran.CostCodeID = context.INTran.CostCodeID;
    }
    return baseMethod(je, tran, context);
  }

  private void WriteLinkedCostsDebitTarget(
    GLTran tran,
    INReleaseProcess.GLTranInsertionContext context)
  {
    int? nullable1 = new int?();
    int? nullable2;
    int? nullable3;
    if (context.Location != null)
    {
      int? projectId = context.Location.ProjectID;
      if (projectId.HasValue)
      {
        nullable2 = context.Location.ProjectID;
        nullable1 = context.Location.TaskID;
        if (!nullable1.HasValue)
        {
          projectId = context.Location.ProjectID;
          nullable3 = context.INTran.ProjectID;
          if (projectId.GetValueOrDefault() == nullable3.GetValueOrDefault() & projectId.HasValue == nullable3.HasValue)
          {
            nullable1 = context.INTran.TaskID;
            goto label_8;
          }
          PMTask pmTask = PXResultset<PMTask>.op_Implicit(PXSelectBase<PMTask, PXSelect<PMTask, Where<PMTask.projectID, Equal<Required<PMTask.projectID>>, And<PMTask.visibleInIN, Equal<True>, And<PMTask.isActive, Equal<True>>>>>.Config>.Select((PXGraph) this.Base, new object[1]
          {
            (object) context.Location.ProjectID
          }));
          if (pmTask != null)
          {
            nullable1 = pmTask.TaskID;
            goto label_8;
          }
          goto label_8;
        }
        goto label_8;
      }
    }
    nullable2 = ProjectDefaultAttribute.NonProject();
label_8:
    if (context.TranCost.TranType == "ADJ" || context.TranCost.TranType == "TRX")
    {
      tran.ProjectID = nullable2;
      tran.TaskID = nullable1;
    }
    else
    {
      GLTran glTran1 = tran;
      nullable3 = context.INTran.ProjectID;
      int? nullable4 = nullable3 ?? nullable2;
      glTran1.ProjectID = nullable4;
      GLTran glTran2 = tran;
      nullable3 = context.INTran.TaskID;
      int? nullable5 = nullable3 ?? nullable1;
      glTran2.TaskID = nullable5;
    }
    nullable3 = context.INTran.CostCodeID;
    if (!nullable3.HasValue)
      return;
    tran.CostCodeID = context.INTran.CostCodeID;
  }

  private void WriteValuatedCostsDebitTarget(
    GLTran tran,
    INReleaseProcess.GLTranInsertionContext context)
  {
    tran.ProjectID = context.INTran.ProjectID;
    tran.TaskID = context.INTran.TaskID;
    if (!context.INTran.CostCodeID.HasValue)
      return;
    tran.CostCodeID = context.INTran.CostCodeID;
  }

  public bool IsProjectAccount(int? accountID)
  {
    PX.Objects.GL.Account account = PX.Objects.GL.Account.PK.Find((PXGraph) this.Base, accountID);
    return account != null && account.AccountGroupID.HasValue;
  }
}
