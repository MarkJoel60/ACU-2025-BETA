// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.ARScheduleMaintExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.AR;
using PX.Objects.CS;
using PX.Objects.SO;
using System;

#nullable disable
namespace PX.Objects.PM;

public class ARScheduleMaintExt : PXGraphExtension<ARScheduleMaint>
{
  public PXSelect<PMBudgetAccum> Budget;
  [PXViewName("AR Transactions")]
  [PXImport(typeof (PX.Objects.AR.ARInvoice))]
  public PXOrderedSelect<PX.Objects.AR.ARInvoice, ARTran, Where<ARTran.tranType, Equal<Current<PX.Objects.AR.ARInvoice.docType>>, And<ARTran.refNbr, Equal<Current<PX.Objects.AR.ARInvoice.refNbr>>, And<Where<ARTran.lineType, IsNull, Or<ARTran.lineType, NotEqual<SOLineType.discount>>>>>>, OrderBy<Asc<ARTran.tranType, Asc<ARTran.refNbr, Asc<ARTran.sortOrder, Asc<ARTran.lineNbr>>>>>> Transactions;

  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.projectAccounting>();

  [PXOverride]
  public void AddDocumentToSchedule(PX.Objects.AR.ARRegister documentAsRegister)
  {
    this.ReduceDraftProjectBalance(documentAsRegister.RefNbr);
  }

  private void ReduceDraftProjectBalance(string refNbr)
  {
    PXResultset<PMProject, ARTran> pxResultset = PXSelectBase<PMProject, PXSelectJoin<PMProject, InnerJoin<ARTran, On<PMProject.contractID, Equal<ARTran.projectID>>>, Where<ARTran.refNbr, Equal<Required<ARTran.refNbr>>>>.Config>.Select<PXResultset<PMProject, ARTran>>((PXGraph) this.Base, new object[1]
    {
      (object) refNbr
    });
    PMProject pmProject = PXResultset<PMProject, ARTran>.op_Implicit(pxResultset);
    ARTran line = PXResultset<PMProject, ARTran>.op_Implicit(pxResultset);
    if (pmProject == null || pmProject.NonProject.GetValueOrDefault() || !line.TaskID.HasValue)
      return;
    PMBudgetAccum pmBudgetAccum1 = ((PXSelectBase<PMBudgetAccum>) this.Budget).Insert(this.GetTargetBudget(this.GetProjectedAccountGroup(line), line));
    if (pmProject.CuryID == pmProject.BillingCuryID)
    {
      PMBudgetAccum pmBudgetAccum2 = pmBudgetAccum1;
      Decimal? nullable1 = pmBudgetAccum2.CuryInvoicedAmount;
      Decimal num1 = line.CuryTranAmt.GetValueOrDefault() + line.CuryRetainageAmt.GetValueOrDefault();
      pmBudgetAccum2.CuryInvoicedAmount = nullable1.HasValue ? new Decimal?(nullable1.GetValueOrDefault() - num1) : new Decimal?();
      PMBudgetAccum pmBudgetAccum3 = pmBudgetAccum1;
      nullable1 = pmBudgetAccum3.InvoicedAmount;
      Decimal? nullable2 = line.TranAmt;
      Decimal valueOrDefault1 = nullable2.GetValueOrDefault();
      nullable2 = line.RetainageAmt;
      Decimal valueOrDefault2 = nullable2.GetValueOrDefault();
      Decimal num2 = valueOrDefault1 + valueOrDefault2;
      Decimal? nullable3;
      if (!nullable1.HasValue)
      {
        nullable2 = new Decimal?();
        nullable3 = nullable2;
      }
      else
        nullable3 = new Decimal?(nullable1.GetValueOrDefault() - num2);
      pmBudgetAccum3.InvoicedAmount = nullable3;
    }
    else
    {
      PMBudgetAccum pmBudgetAccum4 = pmBudgetAccum1;
      Decimal? nullable4 = pmBudgetAccum4.CuryInvoicedAmount;
      Decimal num3 = line.TranAmt.GetValueOrDefault() + line.RetainageAmt.GetValueOrDefault();
      pmBudgetAccum4.CuryInvoicedAmount = nullable4.HasValue ? new Decimal?(nullable4.GetValueOrDefault() - num3) : new Decimal?();
      PMBudgetAccum pmBudgetAccum5 = pmBudgetAccum1;
      nullable4 = pmBudgetAccum5.InvoicedAmount;
      Decimal? nullable5 = line.TranAmt;
      Decimal valueOrDefault3 = nullable5.GetValueOrDefault();
      nullable5 = line.RetainageAmt;
      Decimal valueOrDefault4 = nullable5.GetValueOrDefault();
      Decimal num4 = valueOrDefault3 + valueOrDefault4;
      Decimal? nullable6;
      if (!nullable4.HasValue)
      {
        nullable5 = new Decimal?();
        nullable6 = nullable5;
      }
      else
        nullable6 = new Decimal?(nullable4.GetValueOrDefault() - num4);
      pmBudgetAccum5.InvoicedAmount = nullable6;
    }
  }

  private PMBudgetAccum GetTargetBudget(int? accountGroupID, ARTran line)
  {
    PMAccountGroup ag = PMAccountGroup.PK.Find((PXGraph) this.Base, accountGroupID);
    PMProject project = PMProject.PK.Find((PXGraph) this.Base, line.ProjectID);
    PX.Objects.PM.Lite.PMBudget pmBudget = new BudgetService((PXGraph) this.Base).SelectProjectBalance(ag, project, line.TaskID, line.InventoryID, line.CostCodeID, out bool _);
    PMBudgetAccum targetBudget = new PMBudgetAccum();
    targetBudget.Type = pmBudget.Type;
    targetBudget.ProjectID = pmBudget.ProjectID;
    targetBudget.ProjectTaskID = pmBudget.TaskID;
    targetBudget.AccountGroupID = pmBudget.AccountGroupID;
    targetBudget.InventoryID = pmBudget.InventoryID;
    targetBudget.CostCodeID = pmBudget.CostCodeID;
    targetBudget.UOM = pmBudget.UOM;
    targetBudget.Description = pmBudget.Description;
    targetBudget.CuryInfoID = project.CuryInfoID;
    return targetBudget;
  }

  private int? GetProjectedAccountGroup(ARTran line)
  {
    int? projectedAccountGroup = new int?();
    if (line.AccountID.HasValue && PXSelectorAttribute.Select<ARTran.accountID>(((PXSelectBase) this.Transactions).Cache, (object) line, (object) line.AccountID) is PX.Objects.GL.Account account)
    {
      if (!account.AccountGroupID.HasValue)
        throw new PXException("Revenue Account {0} is not mapped to Account Group.", new object[1]
        {
          (object) account.AccountCD
        });
      projectedAccountGroup = account.AccountGroupID;
    }
    return projectedAccountGroup;
  }
}
