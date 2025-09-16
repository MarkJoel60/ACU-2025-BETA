// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.APDataEntryGraph`2
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Api.Models;
using PX.Data;
using PX.Data.WorkflowAPI;
using PX.Objects.Common;
using PX.Objects.CR;
using PX.Objects.GL;
using System.Collections;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.AP;

public class APDataEntryGraph<TGraph, TPrimary> : PXGraph<TGraph, TPrimary>, IVoucherEntry
  where TGraph : PXGraph
  where TPrimary : APRegister, new()
{
  public PXAction<TPrimary> putOnHold;
  [APMigrationModeDependentActionRestriction(false, true, true)]
  public PXAction<TPrimary> releaseFromHold;
  public PXWorkflowEventHandler<TPrimary> OnUpdateStatus;
  public PXSetup<APSetup> apsetup;
  public PXInitializeState<TPrimary> initializeState;
  private readonly FinDocCopyPasteHelper CopyPasteHelper;
  public PXAction<TPrimary> release;
  public PXAction<TPrimary> voidCheck;
  public PXAction<TPrimary> viewBatch;

  [PXButton(CommitChanges = true)]
  [PXUIField(DisplayName = "Hold", MapEnableRights = PXCacheRights.Update, MapViewRights = PXCacheRights.Select)]
  [APMigrationModeDependentActionRestriction(false, true, true)]
  protected virtual IEnumerable PutOnHold(PXAdapter adapter) => adapter.Get();

  [PXButton(CommitChanges = true)]
  [PXUIField(DisplayName = "Remove Hold", MapEnableRights = PXCacheRights.Update, MapViewRights = PXCacheRights.Select)]
  protected virtual IEnumerable ReleaseFromHold(PXAdapter adapter) => adapter.Get();

  public PXAction DeleteButton => (PXAction) this.Delete;

  public APDataEntryGraph()
  {
    this.CopyPasteHelper = new FinDocCopyPasteHelper((PXGraph) this);
    this.FieldDefaulting.AddHandler<BAccountR.type>((PXFieldDefaulting) ((sender, e) =>
    {
      if (e.Row == null)
        return;
      e.NewValue = (object) "VE";
    }));
  }

  [PXUIField(DisplayName = "Release", MapEnableRights = PXCacheRights.Update, MapViewRights = PXCacheRights.Update)]
  [PXProcessButton]
  [APMigrationModeDependentActionRestriction(false, true, true)]
  public virtual IEnumerable Release(PXAdapter adapter) => adapter.Get();

  [PXUIField(DisplayName = "Void", MapEnableRights = PXCacheRights.Update, MapViewRights = PXCacheRights.Update, Visible = false)]
  [PXProcessButton]
  [APMigrationModeDependentActionRestriction(false, true, true)]
  public virtual IEnumerable VoidCheck(PXAdapter adapter) => adapter.Get();

  [PXUIField(DisplayName = "Review Batch", Visible = false, MapEnableRights = PXCacheRights.Select)]
  [PXLookupButton]
  public virtual IEnumerable ViewBatch(PXAdapter adapter)
  {
    foreach (TPrimary primary in adapter.Get<TPrimary>())
    {
      if (!string.IsNullOrEmpty(primary.BatchNbr))
      {
        JournalEntry instance = PXGraph.CreateInstance<JournalEntry>();
        instance.BatchModule.Current = (Batch) PXSelectBase<Batch, PXSelect<Batch, Where<Batch.module, Equal<BatchModule.moduleAP>, And<Batch.batchNbr, Equal<Required<Batch.batchNbr>>>>>.Config>.Select((PXGraph) this, (object) primary.BatchNbr);
        throw new PXRedirectRequiredException((PXGraph) instance, "Current batch record");
      }
    }
    return adapter.Get();
  }

  protected virtual IEnumerable Report(PXAdapter adapter, string reportID)
  {
    using (IEnumerator<TPrimary> enumerator = adapter.Get<TPrimary>().GetEnumerator())
    {
      if (enumerator.MoveNext())
      {
        TPrimary current = enumerator.Current;
        this.Caches[typeof (TPrimary)].MarkUpdated((object) current);
        this.Save.Press();
        throw new PXReportRequiredException(new Dictionary<string, string>()
        {
          ["PeriodFrom"] = (string) null,
          ["PeriodTo"] = (string) null,
          ["OrgBAccountID"] = PXAccess.GetBranchCD(current.BranchID),
          ["DocType"] = current.DocType,
          ["RefNbr"] = current.RefNbr
        }, reportID, nameof (Report));
      }
    }
    return adapter.Get();
  }

  public override void CopyPasteGetScript(
    bool isImportSimple,
    List<Command> script,
    List<Container> containers)
  {
    this.CopyPasteHelper.SetBranchFieldCommandToTheTop(script);
  }

  protected virtual void AssertOnDelete(PX.Data.Events.RowPersisting<TPrimary> e)
  {
    if (e.Cache.GetStatus((object) e.Row) == PXEntryStatus.Deleted && (e.Row.Released.GetValueOrDefault() || e.Row.Voided.GetValueOrDefault()))
      throw new PXInvalidOperationException("The {0} document with the {1} ref. number is released and cannot be deleted.", new object[2]
      {
        (object) e.Row.DocType,
        (object) e.Row.RefNbr
      });
  }
}
