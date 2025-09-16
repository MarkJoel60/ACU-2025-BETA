// Decompiled with JetBrains decompiler
// Type: PX.Objects.DR.DRProjection
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;
using System.Collections;
using System.Collections.Generic;

#nullable enable
namespace PX.Objects.DR;

public class DRProjection : PXGraph<
#nullable disable
DRProjection>
{
  public PXCancel<DRProjection.ScheduleProjectionFilter> Cancel;
  public PXFilter<DRProjection.ScheduleProjectionFilter> Filter;
  public PXFilteredProcessing<DRScheduleDetail, DRProjection.ScheduleProjectionFilter> Items;
  public PXSetup<DRSetup> Setup;

  protected virtual IEnumerable items()
  {
    DRProjection.ScheduleProjectionFilter current = ((PXSelectBase<DRProjection.ScheduleProjectionFilter>) this.Filter).Current;
    PXSelectBase<DRScheduleDetail> pxSelectBase = (PXSelectBase<DRScheduleDetail>) new PXSelectJoin<DRScheduleDetail, InnerJoin<DRSchedule, On<DRScheduleDetail.scheduleID, Equal<DRSchedule.scheduleID>>, InnerJoin<DRDeferredCode, On<DRDeferredCode.deferredCodeID, Equal<DRScheduleDetail.defCode>>>>, Where<DRDeferredCode.method, Equal<DeferredMethodType.cashReceipt>>>((PXGraph) this);
    if (!string.IsNullOrEmpty(current.DeferredCode))
      pxSelectBase.WhereAnd<Where<DRScheduleDetail.defCode, Equal<Current<DRProjection.ScheduleProjectionFilter.deferredCode>>>>();
    return (IEnumerable) pxSelectBase.Select(Array.Empty<object>());
  }

  public DRProjection()
  {
    DRSetup current = ((PXSelectBase<DRSetup>) this.Setup).Current;
    ((PXProcessingBase<DRScheduleDetail>) this.Items).SetSelected<DRScheduleDetail.selected>();
  }

  protected virtual void ScheduleProjectionFilter_RowUpdated(PXCache cache, PXRowUpdatedEventArgs e)
  {
    ((PXSelectBase) this.Items).Cache.Clear();
  }

  protected virtual void ScheduleProjectionFilter_RowSelected(
    PXCache cache,
    PXRowSelectedEventArgs e)
  {
    DRProjection.ScheduleProjectionFilter current = ((PXSelectBase<DRProjection.ScheduleProjectionFilter>) this.Filter).Current;
    // ISSUE: method pointer
    ((PXProcessingBase<DRScheduleDetail>) this.Items).SetProcessDelegate(new PXProcessingBase<DRScheduleDetail>.ProcessListDelegate((object) null, __methodptr(RunProjection)));
  }

  public static void RunProjection(List<DRScheduleDetail> items)
  {
  }

  [Serializable]
  public class ScheduleProjectionFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    protected string _DeferredCode;

    [PXDBString(10, IsUnicode = true, InputMask = ">aaaaaaaaaa")]
    [PXUIField(DisplayName = "Deferral Code")]
    [PXSelector(typeof (Search<DRDeferredCode.deferredCodeID, Where<DRDeferredCode.method, Equal<DeferredMethodType.cashReceipt>>>))]
    public virtual string DeferredCode
    {
      get => this._DeferredCode;
      set => this._DeferredCode = value;
    }

    public abstract class deferredCode : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      DRProjection.ScheduleProjectionFilter.deferredCode>
    {
    }
  }
}
