// Decompiled with JetBrains decompiler
// Type: PX.Objects.DR.DraftScheduleMaintVisibilityRestriction
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.AP;
using PX.Objects.AR;
using PX.Objects.CR;
using PX.Objects.CS;
using System;
using System.Collections.Generic;
using System.ComponentModel;

#nullable enable
namespace PX.Objects.DR;

public class DraftScheduleMaintVisibilityRestriction : PXGraphExtension<
#nullable disable
DraftScheduleMaint>
{
  [PXHidden]
  public PXSetup<BAccountR>.Where<BqlOperand<
  #nullable enable
  BAccountR.bAccountID, IBqlInt>.IsEqual<
  #nullable disable
  BqlField<
  #nullable enable
  DRSchedule.bAccountID, IBqlInt>.FromCurrent>> _dummyBAccountR;

  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.visibilityRestriction>();

  public virtual void Initialize()
  {
    ((PXGraphExtension) this).Initialize();
    ((PXSelectBase<DRSchedule>) this.Base.Schedule).Join<LeftJoin<BAccountR, On<BAccountR.bAccountID, Equal<DRSchedule.bAccountID>>>>();
    ((PXSelectBase<DRSchedule>) this.Base.Schedule).WhereAnd<Where<BAccountR.cOrgBAccountID, RestrictByUserBranches<Current<AccessInfo.userName>>>>();
    ((PXSelectBase<DRSchedule>) this.Base.Schedule).WhereAnd<Where<BAccountR.vOrgBAccountID, RestrictByUserBranches<Current<AccessInfo.userName>>>>();
  }

  [PXMergeAttributes]
  [PXSelector(typeof (Search2<DRSchedule.scheduleNbr, LeftJoin<BAccountR, On<DRSchedule.bAccountID, Equal<BAccountR.bAccountID>>>>), new System.Type[] {typeof (DRSchedule.scheduleNbr), typeof (DRSchedule.documentTypeEx), typeof (DRSchedule.refNbr), typeof (DRSchedule.bAccountID)})]
  [RestrictCustomerByUserBranches(typeof (BAccountR.cOrgBAccountID))]
  [RestrictVendorByUserBranches(typeof (BAccountR.vOrgBAccountID))]
  public void _(
  #nullable disable
  PX.Data.Events.CacheAttached<DRSchedule.scheduleNbr> e)
  {
  }

  [PXMergeAttributes]
  [RestrictBranchByCustomer(typeof (DRSchedule.bAccountID), typeof (BAccountR.cOrgBAccountID))]
  [RestrictBranchByVendor(typeof (DRSchedule.bAccountID), typeof (BAccountR.vOrgBAccountID))]
  public void _(PX.Data.Events.CacheAttached<DRScheduleDetail.branchID> e)
  {
  }

  [PXOverride]
  public void DRScheduleDetail_BranchID_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e,
    DraftScheduleMaintVisibilityRestriction.DRScheduleDetail_BranchID_FieldDefaultingDelegate baseMethod)
  {
    baseMethod(sender, e);
    if (!(((PXSelectBase) this.Base.Schedule).Cache.Current is DRSchedule current))
      return;
    bool flag = false;
    PXAccess.MasterCollection.Branch branch = PXAccess.GetBranch((int?) e.NewValue);
    PX.Objects.CR.BAccount baccount = (PX.Objects.CR.BAccount) PXResultset<BAccountR>.op_Implicit(PXSelectBase<BAccountR, PXSelect<BAccountR, Where<BAccountR.bAccountID, Equal<Current<DRSchedule.bAccountID>>>>.Config>.Select((PXGraph) this.Base, Array.Empty<object>()));
    int num = 0;
    if (baccount.Type == "VE")
      num = ((int?) baccount?.VOrgBAccountID).GetValueOrDefault();
    else if (baccount.Type == "CU")
      num = ((int?) baccount?.COrgBAccountID).GetValueOrDefault();
    if (num == 0)
    {
      flag = true;
    }
    else
    {
      foreach (int parent in (IEnumerable<int>) RestrictByOrganization<IBqlParameter>.GetParents(num))
      {
        if (parent.Equals((object) branch?.BAccountID))
          flag = true;
      }
    }
    if (flag || current.RefNbr != null)
      return;
    e.NewValue = (object) null;
    ((CancelEventArgs) e).Cancel = true;
  }

  public void _(PX.Data.Events.RowUpdating<DRSchedule> e)
  {
    DRSchedule newRow = e.NewRow;
    if (((PX.Data.Events.Event<PXRowUpdatingEventArgs, PX.Data.Events.RowUpdating<DRSchedule>>) e).Cache.ObjectsEqual<DRSchedule.bAccountID>((object) e.Row, (object) e.NewRow))
      return;
    PXCache pxCache = (PXCache) GraphHelper.Caches<DRScheduleDetail>((PXGraph) this.Base);
    foreach (PXResult<DRScheduleDetail, DRSchedule> pxResult in ((PXSelectBase<DRScheduleDetail>) this.Base.Components).Select(Array.Empty<object>()))
    {
      DRScheduleDetail drScheduleDetail = PXResult<DRScheduleDetail, DRSchedule>.op_Implicit(pxResult);
      drScheduleDetail.BAccountID = newRow.BAccountID;
      object branchId = (object) drScheduleDetail.BranchID;
      try
      {
        pxCache.RaiseFieldVerifying<DRScheduleDetail.branchID>((object) newRow, ref branchId);
      }
      catch (PXSetPropertyException ex)
      {
        BAccountR baccountR = PXResultset<BAccountR>.op_Implicit(((PXSelectBase<BAccountR>) this._dummyBAccountR).Select(Array.Empty<object>()));
        ((PX.Data.Events.Event<PXRowUpdatingEventArgs, PX.Data.Events.RowUpdating<DRSchedule>>) e).Cache.RaiseExceptionHandling<DRSchedule.bAccountID>((object) newRow, (object) baccountR?.AcctCD, (Exception) ex);
      }
    }
  }

  public void _(PX.Data.Events.RowPersisting<DRSchedule> e)
  {
    DRSchedule row = e.Row;
    PXCache pxCache = (PXCache) GraphHelper.Caches<DRScheduleDetail>((PXGraph) this.Base);
    foreach (PXResult<DRScheduleDetail, DRSchedule> pxResult in ((PXSelectBase<DRScheduleDetail>) this.Base.Components).Select(Array.Empty<object>()))
    {
      DRScheduleDetail drScheduleDetail = PXResult<DRScheduleDetail, DRSchedule>.op_Implicit(pxResult);
      object branchId = (object) drScheduleDetail.BranchID;
      try
      {
        pxCache.RaiseFieldVerifying<DRScheduleDetail.branchID>((object) drScheduleDetail, ref branchId);
      }
      catch (PXSetPropertyException ex)
      {
        BAccountR baccountR = PXResultset<BAccountR>.op_Implicit(((PXSelectBase<BAccountR>) this._dummyBAccountR).Select(Array.Empty<object>()));
        ((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<DRSchedule>>) e).Cache.RaiseExceptionHandling<DRSchedule.bAccountID>((object) row, (object) baccountR?.AcctCD, (Exception) ex);
      }
    }
  }

  public void _(PX.Data.Events.FieldUpdated<DRScheduleDetail.branchID> e)
  {
    DRScheduleDetail row = (DRScheduleDetail) e.Row;
    PXCache pxCache = (PXCache) GraphHelper.Caches<DRScheduleTran>((PXGraph) this.Base);
    foreach (PXResult<DRScheduleTran> pxResult in ((PXSelectBase<DRScheduleTran>) this.Base.Transactions).Select(Array.Empty<object>()))
    {
      DRScheduleTran drScheduleTran = PXResult<DRScheduleTran>.op_Implicit(pxResult);
      pxCache.SetValue<DRScheduleTran.branchID>((object) drScheduleTran, (object) row.BranchID);
      GraphHelper.MarkUpdated(pxCache, (object) drScheduleTran);
    }
  }

  public delegate void DRScheduleDetail_BranchID_FieldDefaultingDelegate(
    PXCache sender,
    PXFieldDefaultingEventArgs e);
}
