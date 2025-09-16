// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.PMTranMultiCurrencyPM`1
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CM.Extensions;
using System;

#nullable disable
namespace PX.Objects.PM;

public abstract class PMTranMultiCurrencyPM<TGraph> : PMTranMultiCurrency<TGraph> where TGraph : PXGraph
{
  [PXMergeAttributes]
  [CurrencyInfo(typeof (CurrencyInfo.curyInfoID))]
  protected void _(Events.CacheAttached<PMTran.projectCuryInfoID> e)
  {
  }

  protected override void _(Events.FieldSelecting<PX.Objects.Extensions.MultiCurrency.Document, PX.Objects.Extensions.MultiCurrency.Document.curyID> e)
  {
  }

  protected void _(Events.FieldSelecting<PMTran, PMTran.tranCuryID> e)
  {
    if (!this.Base.Accessinfo.CuryViewState)
      return;
    ((Events.FieldSelectingBase<Events.FieldSelecting<PMTran, PMTran.tranCuryID>>) e).ReturnValue = (object) this.GetCurrencyInfo((long?) e.Row?.BaseCuryInfoID)?.BaseCuryID;
  }

  protected override string Module => "PM";

  protected void _(Events.RowInserting<PMTran> e)
  {
    this.DocumentRowInserting<PMTran.projectCuryInfoID, PMTran.projectCuryID>(((Events.Event<PXRowInsertingEventArgs, Events.RowInserting<PMTran>>) e).Cache, (object) e.Row);
  }

  protected void _(Events.RowUpdating<PMTran> e)
  {
    this.DocumentRowUpdating<PMTran.projectCuryInfoID, PMTran.projectCuryID>(((Events.Event<PXRowUpdatingEventArgs, Events.RowUpdating<PMTran>>) e).Cache, (object) e.NewRow);
  }

  protected virtual void _(
    Events.FieldSelecting<PMTran, PMTran.projectCuryID> e)
  {
    ((Events.FieldSelectingBase<Events.FieldSelecting<PMTran, PMTran.projectCuryID>>) e).ReturnValue = (object) this.GetCurrencyInfo((long?) e.Row?.ProjectCuryInfoID)?.BaseCuryID;
  }

  protected virtual void _(
    Events.FieldSelecting<PMTran, PMTran.projectCuryRate> e)
  {
    ((Events.FieldSelectingBase<Events.FieldSelecting<PMTran, PMTran.projectCuryRate>>) e).ReturnValue = (object) (Decimal?) this.GetCurrencyInfo((long?) e.Row?.ProjectCuryInfoID)?.SampleCuryRate;
  }

  protected virtual void _(
    Events.FieldSelecting<PMTran, PMTran.baseCuryRate> e)
  {
    ((Events.FieldSelectingBase<Events.FieldSelecting<PMTran, PMTran.baseCuryRate>>) e).ReturnValue = (object) (Decimal?) this.GetCurrencyInfo((long?) e.Row?.BaseCuryInfoID)?.SampleCuryRate;
  }

  protected virtual void _(Events.FieldUpdated<PMTran, PMTran.date> e)
  {
    this.DateFieldUpdated<PMTran.projectCuryInfoID, PMTran.date>(((Events.Event<PXFieldUpdatedEventArgs, Events.FieldUpdated<PMTran, PMTran.date>>) e).Cache, (IBqlTable) e.Row);
  }
}
