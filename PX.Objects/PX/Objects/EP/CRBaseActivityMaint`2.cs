// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.CRBaseActivityMaint`2
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.AP;
using PX.Objects.CR;
using System;
using System.Linq;

#nullable disable
namespace PX.Objects.EP;

public class CRBaseActivityMaint<TGraph, TMaster> : PXGraph<TGraph>, IActivityMaint
  where TGraph : PXGraph
  where TMaster : CRActivity, new()
{
  [PXHidden]
  public PXSelect<PX.Objects.CR.BAccount> BaseBAccount;
  [PXHidden]
  public PXSelect<VendorR> BaseVendor;
  [PXHidden]
  public PXSelect<PX.Objects.AR.Customer> BaseCustomer;
  [PXHidden]
  public PXSelect<EPEmployee> BaseEmployee;
  [PXHidden]
  public PXSelect<EPView> EPViews;
  [PXHidden]
  public PXSelect<CRActivityStatistics> Stats;
  public PXSave<TMaster> Save;
  public PXCancel<TMaster> Cancel;
  public PXInsert<TMaster> Insert;

  public CRBaseActivityMaint()
  {
    ((PXGraph) this).Views.Caches.Remove(typeof (CRActivityStatistics));
    ((PXGraph) this).Views.Caches.Add(typeof (CRActivityStatistics));
  }

  protected virtual void _(PX.Data.Events.RowSelected<TMaster> e)
  {
    if ((object) e.Row == null)
      return;
    this.AdjustProvidesCaseSolutionState(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<TMaster>>) e).Cache, e.Row);
  }

  public virtual void AdjustProvidesCaseSolutionState(PXCache cache, TMaster row)
  {
    bool isVisible = false;
    bool isEnabled = true;
    if (row.RefNoteIDType == typeof (CRCase).FullName)
    {
      Guid? nullable = row.RefNoteID;
      if (nullable.HasValue)
      {
        Guid valueOrDefault1 = nullable.GetValueOrDefault();
        nullable = row.NoteID;
        if (nullable.HasValue)
        {
          Guid valueOrDefault2 = nullable.GetValueOrDefault();
          CRCase crCase = PXResultset<CRCase>.op_Implicit(PXSelectBase<CRCase, PXViewOf<CRCase>.BasedOn<SelectFromBase<CRCase, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<CRCase.noteID, IBqlGuid>.IsEqual<P.AsGuid>>>.Config>.Select((PXGraph) this, new object[1]
          {
            (object) valueOrDefault1
          }));
          if (crCase != null)
          {
            bool? solutionsInActivities = (bool?) CRCaseClass.PK.Find((PXGraph) this, crCase.CaseClassID)?.TrackSolutionsInActivities;
            if (solutionsInActivities.HasValue && solutionsInActivities.GetValueOrDefault())
              isVisible = true;
            nullable = crCase.SolutionActivityNoteID;
            Guid guid = valueOrDefault2;
            if ((nullable.HasValue ? (nullable.GetValueOrDefault() == guid ? 1 : 0) : 0) != 0)
              isEnabled = false;
          }
        }
      }
    }
    PXCacheEx.AdjustUI(cache, (object) row).For("providesCaseSolution", (Action<PXUIFieldAttribute>) (ui =>
    {
      ui.Visible = isVisible;
      ui.Enabled = isEnabled;
    }));
  }

  public virtual void CancelRow(CRActivity row)
  {
  }

  public virtual void CompleteRow(CRActivity row)
  {
  }

  protected virtual void MarkAs(PXCache cache, CRActivity row, int? contactID, int status)
  {
    if (((PXGraph) this).IsImport || !row.NoteID.HasValue || !contactID.HasValue)
      return;
    FbqlSelect<SelectFromBase<EPView, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<EPView.noteID, Equal<P.AsGuid>>>>>.And<BqlOperand<EPView.contactID, IBqlInt>.IsEqual<P.AsInt>>>, EPView>.View view = new FbqlSelect<SelectFromBase<EPView, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<EPView.noteID, Equal<P.AsGuid>>>>>.And<BqlOperand<EPView.contactID, IBqlInt>.IsEqual<P.AsInt>>>, EPView>.View((PXGraph) this);
    EPView epView = PXResult<EPView>.op_Implicit(((IQueryable<PXResult<EPView>>) ((PXSelectBase<EPView>) view).Select(new object[2]
    {
      (object) row.NoteID,
      (object) contactID
    })).FirstOrDefault<PXResult<EPView>>());
    bool isDirty = ((PXSelectBase) this.EPViews).Cache.IsDirty;
    if (epView == null)
    {
      object obj = ((PXSelectBase) this.EPViews).Cache.Insert((object) new EPView()
      {
        NoteID = row.NoteID,
        ContactID = contactID,
        Status = new int?(status)
      });
      ((PXSelectBase) this.EPViews).Cache.PersistInserted(obj);
      ((PXSelectBase) view).View.Clear();
      ((PXSelectBase) this.EPViews).Cache.SetStatus(obj, (PXEntryStatus) 0);
      ((PXSelectBase) this.EPViews).Cache.Current = (object) null;
    }
    else
    {
      int num = status;
      int? status1 = epView.Status;
      int valueOrDefault = status1.GetValueOrDefault();
      if (!(num == valueOrDefault & status1.HasValue))
      {
        epView.Status = new int?(status);
        ((PXSelectBase) this.EPViews).Cache.PersistUpdated((object) epView);
      }
    }
    ((PXSelectBase) this.EPViews).Cache.IsDirty = isDirty;
  }
}
