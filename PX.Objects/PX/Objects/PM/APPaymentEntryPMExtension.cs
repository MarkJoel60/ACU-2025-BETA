// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.APPaymentEntryPMExtension
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.AP;
using PX.Objects.CS;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace PX.Objects.PM;

public class APPaymentEntryPMExtension : PXGraphExtension<APPaymentEntry>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.projectAccounting>();

  [PXOverride]
  public virtual IEnumerable adjustments(Func<IEnumerable> baseViewDelegate)
  {
    APPaymentEntryPMExtension entryPmExtension = this;
    foreach (object obj in baseViewDelegate())
    {
      PX.Objects.AP.APInvoice apInvoice = PXResult.Unwrap<PX.Objects.AP.APInvoice>(obj);
      APTran apTran = PXResult.Unwrap<APTran>(obj);
      if (apInvoice != null && apTran != null)
      {
        int? nullable = apTran.ProjectID;
        if (!apInvoice.PaymentsByLinesAllowed.GetValueOrDefault())
          nullable = apInvoice.HasMultipleProjects.GetValueOrDefault() ? new int?() : apInvoice.ProjectID;
        PX.Objects.AP.APAdjust apAdjust = PXResult.Unwrap<PX.Objects.AP.APAdjust>(obj);
        ((PXSelectBase) entryPmExtension.Base.Adjustments).Cache.SetValue<APAdjustPMExtension.paymentProjectID>((object) apAdjust, (object) nullable);
      }
      yield return obj;
    }
  }

  [PXOverride]
  public virtual IEnumerable appost(Func<IEnumerable> baseViewDelegate)
  {
    APPaymentEntryPMExtension entryPmExtension = this;
    foreach (object obj in baseViewDelegate())
    {
      PX.Objects.AP.Standalone.APRegister apRegister = PXResult.Unwrap<PX.Objects.AP.Standalone.APRegister>(obj);
      APTran apTran = PXResult.Unwrap<APTran>(obj);
      if (apRegister != null && apTran != null)
      {
        int? nullable = apTran.ProjectID;
        if (!apRegister.PaymentsByLinesAllowed.GetValueOrDefault())
          nullable = apRegister.HasMultipleProjects.GetValueOrDefault() ? new int?() : apRegister.ProjectID;
        APTranPostBal apTranPostBal = PXResult.Unwrap<APTranPostBal>(obj);
        ((PXSelectBase) entryPmExtension.Base.APPost).Cache.SetValue<APTranPostBalPMExtension.paymentProjectID>((object) apTranPostBal, (object) nullable);
      }
      yield return obj;
    }
  }

  protected virtual void _(PX.Data.Events.RowInserting<PX.Objects.AP.APAdjust> e)
  {
    PX.Objects.AP.APAdjust row = e.Row;
    int? nullable1;
    int num1;
    if (row == null)
    {
      num1 = 1;
    }
    else
    {
      nullable1 = row.AdjdLineNbr;
      int num2 = 0;
      num1 = !(nullable1.GetValueOrDefault() == num2 & nullable1.HasValue) ? 1 : 0;
    }
    if (num1 != 0)
      return;
    PX.Objects.AP.APInvoice apInvoice = PX.Objects.AP.APInvoice.PK.Find((PXGraph) this.Base, row.AdjdDocType, row.AdjdRefNbr);
    bool? nullable2;
    int num3;
    if (apInvoice == null)
    {
      num3 = 1;
    }
    else
    {
      nullable2 = apInvoice.PaymentsByLinesAllowed;
      bool flag = false;
      num3 = !(nullable2.GetValueOrDefault() == flag & nullable2.HasValue) ? 1 : 0;
    }
    if (num3 != 0)
      return;
    PXCache cache = ((PXSelectBase) this.Base.Adjustments).Cache;
    PX.Objects.AP.APAdjust apAdjust = row;
    nullable2 = apInvoice.HasMultipleProjects;
    int? nullable3;
    if (!nullable2.GetValueOrDefault())
    {
      nullable3 = apInvoice.ProjectID;
    }
    else
    {
      nullable1 = new int?();
      nullable3 = nullable1;
    }
    // ISSUE: variable of a boxed type
    __Boxed<int?> local = (ValueType) nullable3;
    cache.SetValue<APAdjustPMExtension.paymentProjectID>((object) apAdjust, (object) local);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PX.Objects.AP.APAdjust, PX.Objects.AP.APAdjust.adjdLineNbr> e)
  {
    PX.Objects.AP.APAdjust row = e.Row;
    if (row == null)
      return;
    int? adjdLineNbr = row.AdjdLineNbr;
    int num = 0;
    if (!(adjdLineNbr.GetValueOrDefault() > num & adjdLineNbr.HasValue) || !(PXSelectorAttribute.Select<PX.Objects.AP.APAdjust.adjdLineNbr>(((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PX.Objects.AP.APAdjust, PX.Objects.AP.APAdjust.adjdLineNbr>>) e).Cache, (object) row) is APTran apTran))
      return;
    ((PXSelectBase) this.Base.Adjustments).Cache.SetValue<APAdjustPMExtension.paymentProjectID>((object) row, (object) apTran.ProjectID);
  }

  protected virtual PXFilterRow[]? RemoveFilterByProjectID(PXFilterRow[]? filters)
  {
    return filters == null ? (PXFilterRow[]) null : ((IEnumerable<PXFilterRow>) filters).Where<PXFilterRow>((Func<PXFilterRow, bool>) (f => !string.Equals(f.DataField, "paymentProjectID", StringComparison.OrdinalIgnoreCase))).ToArray<PXFilterRow>();
  }

  [PXOverride]
  public virtual PXFilterRow[]? SelectAdjustmentsFilters(
    PXFilterRow[]? filters,
    Func<PXFilterRow[]?, PXFilterRow[]?> baseSelect)
  {
    return this.RemoveFilterByProjectID(baseSelect(filters));
  }

  [PXOverride]
  public virtual PXFilterRow[]? SelectAPPostFilters(
    PXFilterRow[]? filters,
    Func<PXFilterRow[]?, PXFilterRow[]?> baseSelect)
  {
    return this.RemoveFilterByProjectID(baseSelect(filters));
  }
}
