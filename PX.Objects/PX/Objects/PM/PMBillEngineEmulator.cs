// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.PMBillEngineEmulator
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.AR;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

#nullable disable
namespace PX.Objects.PM;

public class PMBillEngineEmulator : PMBillEngine
{
  public override ARInvoiceEntry InvoiceEntry
  {
    get
    {
      if (this.invoiceEntry == null)
      {
        this.invoiceEntry = (ARInvoiceEntry) PXGraph.CreateInstance<ARInvoiceEntryEmulator>();
        ((PXGraph) this.invoiceEntry).FieldVerifying.AddHandler<ARTran.taskID>(PMBillEngineEmulator.\u003C\u003Ec.\u003C\u003E9__1_0 ?? (PMBillEngineEmulator.\u003C\u003Ec.\u003C\u003E9__1_0 = new PXFieldVerifying((object) PMBillEngineEmulator.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003Cget_InvoiceEntry\u003Eb__1_0))));
      }
      return this.invoiceEntry;
    }
  }

  public override RegisterEntry PMEntry
  {
    get
    {
      if (this.pmRegisterEntry == null)
      {
        this.pmRegisterEntry = (RegisterEntry) PXGraph.CreateInstance<RegisterEntryEmulator>();
        ((PXGraph) this.pmRegisterEntry).FieldVerifying.AddHandler<PMTran.projectID>(PMBillEngineEmulator.\u003C\u003Ec.\u003C\u003E9__3_0 ?? (PMBillEngineEmulator.\u003C\u003Ec.\u003C\u003E9__3_0 = new PXFieldVerifying((object) PMBillEngineEmulator.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003Cget_PMEntry\u003Eb__3_0))));
        ((PXGraph) this.pmRegisterEntry).FieldVerifying.AddHandler<PMTran.taskID>(PMBillEngineEmulator.\u003C\u003Ec.\u003C\u003E9__3_1 ?? (PMBillEngineEmulator.\u003C\u003Ec.\u003C\u003E9__3_1 = new PXFieldVerifying((object) PMBillEngineEmulator.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003Cget_PMEntry\u003Eb__3_1))));
        ((PXGraph) this.pmRegisterEntry).FieldVerifying.AddHandler<PMTran.inventoryID>(PMBillEngineEmulator.\u003C\u003Ec.\u003C\u003E9__3_2 ?? (PMBillEngineEmulator.\u003C\u003Ec.\u003C\u003E9__3_2 = new PXFieldVerifying((object) PMBillEngineEmulator.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003Cget_PMEntry\u003Eb__3_2))));
      }
      return this.pmRegisterEntry;
    }
  }

  public override List<PMTran> SelectBillingBase(
    int? projectID,
    int? taskID,
    int? accountGroupID,
    bool includeNonBillable)
  {
    List<PMTran> pmTranList = new List<PMTran>();
    foreach (PMTran pmTran in ((PXSelectBase) this.Transactions).Cache.Cached)
    {
      int? nullable1 = pmTran.ProjectID;
      int? nullable2 = projectID;
      if (nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue)
      {
        nullable2 = pmTran.TaskID;
        nullable1 = taskID;
        if (nullable2.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable2.HasValue == nullable1.HasValue)
        {
          nullable1 = pmTran.AccountGroupID;
          nullable2 = accountGroupID;
          if (nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue)
          {
            bool? nullable3 = pmTran.Billed;
            if (!nullable3.GetValueOrDefault())
            {
              nullable3 = pmTran.ExcludedFromBilling;
              if (!nullable3.GetValueOrDefault())
              {
                nullable3 = pmTran.Released;
                if (nullable3.GetValueOrDefault() && pmTran.TranType != "AR")
                  pmTranList.Add(pmTran);
              }
            }
          }
        }
      }
    }
    return pmTranList;
  }

  public override List<PMTask> SelectBillableTasks(PMProject project)
  {
    List<PMTask> pmTaskList = new List<PMTask>();
    foreach (PXResult<PMTask> pxResult in ((PXSelectBase<PMTask>) new PXSelect<PMTask, Where<PMTask.projectID, Equal<Required<PMTask.projectID>>, And<PMTask.billingID, IsNotNull>>>((PXGraph) this)).Select(new object[1]
    {
      (object) project.ContractID
    }))
    {
      PMTask pmTask = PXResult<PMTask>.op_Implicit(pxResult);
      pmTaskList.Add(pmTask);
    }
    return pmTaskList;
  }

  public override void AutoReleaseCreatedDocuments(
    PMProject project,
    PMBillEngine.BillingResult result,
    PMRegister wipReversalDoc)
  {
  }

  protected override PMProject SelectProjectByID(int? projectID)
  {
    PMProject pmProject = base.SelectProjectByID(projectID);
    pmProject.CreateProforma = new bool?(false);
    return pmProject;
  }

  public override string GetInvoiceKey(string proformaTag, PMBillingRule rule)
  {
    return base.GetInvoiceKey("P", (PMBillingRule) null);
  }

  public override string GenerateProformaTag(PMProject project, PMTask task) => "P";

  [ExcludeFromCodeCoverage]
  public virtual void Persist()
  {
  }

  [ExcludeFromCodeCoverage]
  public virtual int Persist(Type cacheType, PXDBOperation operation) => 1;
}
