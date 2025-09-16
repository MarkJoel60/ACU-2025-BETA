// Decompiled with JetBrains decompiler
// Type: PX.Objects.DR.DRSchedulePrimary
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.AP;
using PX.Objects.AR;
using System;
using System.Collections;
using System.Linq;

#nullable disable
namespace PX.Objects.DR;

public class DRSchedulePrimary : PXGraph<DRSchedulePrimary>
{
  [PXFilterable(new Type[] {})]
  public PXSelect<DRSchedule> Items;
  public PXSetup<DRSetup> Setup;
  public PXAction<DRSchedule> create;
  public PXAction<DRSchedule> viewDoc;
  public PXAction<DRSchedule> viewSchedule;

  public DRSchedulePrimary()
  {
    DRSetup current = ((PXSelectBase<DRSetup>) this.Setup).Current;
  }

  [PXUIField(DisplayName = "")]
  [PXButton]
  [PXEntryScreenRights(typeof (DRSchedule), "Insert")]
  protected virtual void Create()
  {
    using (new PXPreserveScope())
    {
      DraftScheduleMaint instance = PXGraph.CreateInstance<DraftScheduleMaint>();
      ((PXGraph) instance).Clear((PXClearOption) 3);
      ((PXSelectBase<DRSchedule>) instance.Schedule).Insert();
      ((PXSelectBase) instance.Schedule).Cache.IsDirty = false;
      PXRedirectHelper.TryRedirect((PXGraph) instance, (PXRedirectHelper.WindowMode) 4);
    }
  }

  [PXUIField(DisplayName = "")]
  [PXButton]
  public virtual IEnumerable ViewDoc(PXAdapter adapter)
  {
    if (((PXSelectBase<DRSchedule>) this.Items).Current != null)
      DRRedirectHelper.NavigateToOriginalDocument((PXGraph) this, ((PXSelectBase<DRSchedule>) this.Items).Current);
    return adapter.Get();
  }

  [PXUIField(DisplayName = "")]
  [PXButton]
  public virtual IEnumerable ViewSchedule(PXAdapter adapter)
  {
    if (((PXSelectBase<DRSchedule>) this.Items).Current != null)
      PXRedirectHelper.TryRedirect(((PXGraph) this).Caches[typeof (DRSchedule)], (object) ((PXSelectBase<DRSchedule>) this.Items).Current, nameof (ViewSchedule), (PXRedirectHelper.WindowMode) 0);
    return adapter.Get();
  }

  protected virtual void DRSchedule_RowSelecting(PXCache sender, PXRowSelectingEventArgs e)
  {
    if (!(e.Row is DRSchedule row))
      return;
    if (row.IsDraft.GetValueOrDefault())
    {
      row.Status = "D";
    }
    else
    {
      using (new PXConnectionScope())
      {
        PXResultset<DRScheduleDetail> pxResultset = PXSelectBase<DRScheduleDetail, PXSelect<DRScheduleDetail, Where<DRScheduleDetail.scheduleID, Equal<Required<DRSchedule.scheduleID>>>>.Config>.Select((PXGraph) this, new object[1]
        {
          (object) row.ScheduleID
        });
        row.Status = GraphHelper.RowCast<DRScheduleDetail>((IEnumerable) pxResultset).Any<DRScheduleDetail>((Func<DRScheduleDetail, bool>) (d => d.IsOpen.GetValueOrDefault())) ? "O" : "C";
      }
    }
  }

  protected virtual void DRSchedule_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    if (!(e.Row is DRSchedule row))
      return;
    row.DocumentType = DRScheduleDocumentType.BuildDocumentType(row.Module, row.DocType);
    if (row.Module == "AR")
    {
      ARTran arTran = PXResultset<ARTran>.op_Implicit(PXSelectBase<ARTran, PXSelect<ARTran, Where<ARTran.tranType, Equal<Current<DRSchedule.docType>>, And<ARTran.refNbr, Equal<Current<DRSchedule.refNbr>>, And<ARTran.lineNbr, Equal<Current<DRSchedule.lineNbr>>>>>>.Config>.Select((PXGraph) this, Array.Empty<object>()));
      if (arTran != null)
        row.OrigLineAmt = arTran.TranAmt;
    }
    else
    {
      APTran apTran = PXResultset<APTran>.op_Implicit(PXSelectBase<APTran, PXSelect<APTran, Where<APTran.tranType, Equal<Current<DRSchedule.docType>>, And<APTran.refNbr, Equal<Current<DRSchedule.refNbr>>, And<APTran.lineNbr, Equal<Current<DRSchedule.lineNbr>>>>>>.Config>.Select((PXGraph) this, Array.Empty<object>()));
      if (apTran != null)
        row.OrigLineAmt = apTran.TranAmt;
    }
    PXUIFieldAttribute.SetVisible<DRSchedule.origLineAmt>(sender, (object) row, !row.IsCustom.GetValueOrDefault());
  }
}
