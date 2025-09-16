// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.INAdjustmentEntrySplit
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CS;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.IN;

public class INAdjustmentEntrySplit : PXGraphExtension<INAdjustmentEntry>
{
  public PXSetup<CommonSetup> setup;
  public PXAction<INRegister> split;

  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.inventory>();

  public virtual void Initialize()
  {
    ((PXGraphExtension) this).Initialize();
    ((PXGraph) this.Base).OnBeforePersist += new Action<PXGraph>(this.BaseOnBeforePersist);
  }

  [PXButton(CommitChanges = true, VisibleOnDataSource = false)]
  [PXUIField]
  public virtual IEnumerable Split(PXAdapter adapter)
  {
    if (((PXSelectBase<INTran>) this.Base.transactions).Current != null)
    {
      INTran newLine = this.SplitTransaction(((PXSelectBase<INTran>) this.Base.transactions).Current);
      if (newLine != null)
      {
        INTran inTran1 = newLine;
        int? sortOrder = inTran1.SortOrder;
        inTran1.SortOrder = sortOrder.HasValue ? new int?(sortOrder.GetValueOrDefault() + 1) : new int?();
        foreach (PXResult<INTran> pxResult in ((PXSelectBase<INTran>) this.Base.transactions).Select(Array.Empty<object>()))
        {
          INTran inTran2 = PXResult<INTran>.op_Implicit(pxResult);
          int? nullable1 = inTran2.SortOrder;
          sortOrder = newLine.SortOrder;
          if (nullable1.GetValueOrDefault() >= sortOrder.GetValueOrDefault() & nullable1.HasValue & sortOrder.HasValue)
          {
            PXCache cache = ((PXSelectBase) this.Base.transactions).Cache;
            INTran inTran3 = inTran2;
            sortOrder = inTran2.SortOrder;
            int? nullable2;
            if (!sortOrder.HasValue)
            {
              nullable1 = new int?();
              nullable2 = nullable1;
            }
            else
              nullable2 = new int?(sortOrder.GetValueOrDefault() + 1);
            // ISSUE: variable of a boxed type
            __Boxed<int?> local = (ValueType) nullable2;
            cache.SetValue<INTran.sortOrder>((object) inTran3, (object) local);
            GraphHelper.MarkUpdated(((PXSelectBase) this.Base.transactions).Cache, (object) inTran2, true);
          }
        }
        this.InsertNewSplit(newLine);
      }
    }
    return adapter.Get();
  }

  public virtual INTran SplitTransaction(INTran source)
  {
    INTran copy = (INTran) ((PXSelectBase) this.Base.transactions).Cache.CreateCopy((object) source);
    copy.LineNbr = new int?();
    copy.NoteID = new Guid?();
    copy.TranCost = new Decimal?();
    return copy;
  }

  public virtual INTran InsertNewSplit(INTran newLine)
  {
    if (string.IsNullOrEmpty(newLine.LotSerialNbr))
      return ((PXSelectBase<INTran>) this.Base.transactions).Insert(newLine);
    using (this.Base.LineSplittingExt.SuppressedModeScope(true))
      return ((PXSelectBase<INTran>) this.Base.transactions).Insert(newLine);
  }

  protected virtual void _(Events.RowSelected<INRegister> e)
  {
    if (e.Row == null)
      return;
    bool flag = !string.IsNullOrEmpty(e.Row.PIID);
    if (flag)
      ((PXSelectBase) this.Base.transactions).Cache.AllowDelete = ((PXSelectBase) this.Base.transactions).Cache.AllowUpdate;
    ((PXAction) this.split).SetVisible(flag);
    ((PXAction) this.split).SetEnabled(flag);
  }

  protected virtual void _(Events.RowSelected<INTran> e)
  {
    PXUIFieldAttribute.SetEnabled<INTran.qty>(((Events.Event<PXRowSelectedEventArgs, Events.RowSelected<INTran>>) e).Cache, (object) e.Row, true);
  }

  protected virtual void _(Events.RowDeleting<INTran> e)
  {
    if (string.IsNullOrEmpty(e.Row.PIID) || ((PXSelectBase) this.Base.adjustment).Cache.GetStatus((object) ((PXSelectBase<INRegister>) this.Base.adjustment).Current) == 3)
      return;
    e.Cancel = ((Events.Event<PXRowDeletingEventArgs, Events.RowDeleting<INTran>>) e).Cache.GetStatus((object) e.Row) != 2 && ((Events.Event<PXRowDeletingEventArgs, Events.RowDeleting<INTran>>) e).Cache.GetStatus((object) e.Row) != 4;
  }

  protected virtual void BaseOnBeforePersist(PXGraph obj)
  {
    if (string.IsNullOrEmpty(((PXSelectBase<INRegister>) this.Base.adjustment).Current?.PIID))
      return;
    this.ValidateQtyVariance();
  }

  protected virtual void ValidateQtyVariance()
  {
    bool flag = false;
    foreach (IGrouping<int?, INTran> source in GraphHelper.RowCast<INTran>((IEnumerable) ((PXSelectBase<INTran>) this.Base.transactions).Select(Array.Empty<object>())).ToList<INTran>().GroupBy<INTran, int?>((Func<INTran, int?>) (x => x.PILineNbr)).Where<IGrouping<int?, INTran>>((Func<IGrouping<int?, INTran>, bool>) (g => g.Count<INTran>() > 1)))
    {
      Decimal d = source.Sum<INTran>((Func<INTran, Decimal>) (x => x.Qty.GetValueOrDefault()));
      INPIDetail inpiDetail = INPIDetail.PK.Find((PXGraph) this.Base, ((PXSelectBase<INRegister>) this.Base.adjustment).Current.PIID, source.Key);
      Decimal? varQty = inpiDetail.VarQty;
      Decimal num = d;
      if (!(varQty.GetValueOrDefault() == num & varQty.HasValue))
      {
        flag = true;
        foreach (INTran inTran1 in (IEnumerable<INTran>) source)
        {
          PXCache cache = ((PXSelectBase) this.Base.transactions).Cache;
          INTran inTran2 = inTran1;
          // ISSUE: variable of a boxed type
          __Boxed<Decimal?> qty = (ValueType) inTran1.Qty;
          object[] objArray = new object[3]
          {
            (object) inTran1.PILineNbr,
            (object) Decimal.Round(d, (int) (((PXSelectBase<CommonSetup>) this.setup).Current.DecPlQty ?? (short) 2)),
            null
          };
          varQty = inpiDetail.VarQty;
          objArray[2] = (object) Decimal.Round(varQty.GetValueOrDefault(), (int) (((PXSelectBase<CommonSetup>) this.setup).Current.DecPlQty ?? (short) 2));
          PXSetPropertyException propertyException = new PXSetPropertyException("The total quantity of the inventory adjustment line must be equal the variance quantity of the line {0} of the physical inventory document {1}.", objArray);
          cache.RaiseExceptionHandling<INTran.qty>((object) inTran2, (object) qty, (Exception) propertyException);
        }
      }
    }
    if (flag)
      throw new PXException("Inserting 'IN adjustment line' record raised at least one error. Please review the errors.");
  }
}
