// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.ARFinChargesMaint
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;
using System.ComponentModel;

#nullable disable
namespace PX.Objects.AR;

public class ARFinChargesMaint : PXGraph<ARFinChargesMaint, ARFinCharge>
{
  public PXSelect<ARFinCharge> ARFinChargesList;
  public PXSelect<ARFinChargePercent, Where<ARFinChargePercent.finChargeID, Equal<Current<ARFinCharge.finChargeID>>>, OrderBy<Asc<ARFinChargePercent.beginDate>>> PercentList;
  public PXSetup<PX.Objects.GL.GLSetup> GLSetup;

  public ARFinChargesMaint()
  {
    PX.Objects.GL.GLSetup current = ((PXSelectBase<PX.Objects.GL.GLSetup>) this.GLSetup).Current;
  }

  protected virtual void ARFinCharge_RowPersisting(PXCache cache, PXRowPersistingEventArgs e)
  {
    ARFinCharge row = (ARFinCharge) e.Row;
    PXCache pxCache1 = cache;
    ARFinCharge arFinCharge1 = row;
    Decimal? feeAmount = row.FeeAmount;
    Decimal num1 = 0M;
    int num2 = !(feeAmount.GetValueOrDefault() == num1 & feeAmount.HasValue) ? 1 : 2;
    PXDefaultAttribute.SetPersistingCheck<ARFinCharge.feeAccountID>(pxCache1, (object) arFinCharge1, (PXPersistingCheck) num2);
    PXCache pxCache2 = cache;
    ARFinCharge arFinCharge2 = row;
    Decimal? nullable1 = row.FeeAmount;
    Decimal num3 = 0M;
    int num4 = !(nullable1.GetValueOrDefault() == num3 & nullable1.HasValue) ? 1 : 2;
    PXDefaultAttribute.SetPersistingCheck<ARFinCharge.feeSubID>(pxCache2, (object) arFinCharge2, (PXPersistingCheck) num4);
    bool? nullable2 = row.PercentFlag;
    if (nullable2.GetValueOrDefault())
    {
      if (((PXSelectBase<ARFinChargePercent>) this.PercentList).Select(Array.Empty<object>()).Count >= 1 || e.Operation == 3)
        return;
      cache.RaiseExceptionHandling<ARFinCharge.chargingMethod>((object) row, (object) row.ChargingMethod, (Exception) new PXSetPropertyException("For the selected charging method, at least one percent rate must be specified in the table below.", (PXErrorLevel) 4));
    }
    else
    {
      nullable1 = row.MinFinChargeAmount;
      Decimal num5 = 0M;
      if (!(nullable1.GetValueOrDefault() == num5 & nullable1.HasValue))
      {
        nullable2 = row.MinFinChargeFlag;
        bool flag = false;
        if (!(nullable2.GetValueOrDefault() == flag & nullable2.HasValue))
          return;
      }
      cache.RaiseExceptionHandling<ARFinCharge.fixedAmount>((object) row, (object) row.FixedAmount, (Exception) new PXSetPropertyException("With 0.00 amount specified, the system will not calculate charges for overdue documents. To initiate calculation of overdue charges, specify the fixed amount greater than 0.00.", (PXErrorLevel) 3));
    }
  }

  protected virtual void ARFinCharge_RowInserting(PXCache cache, PXRowInsertingEventArgs e)
  {
    if (PXResultset<ARFinCharge>.op_Implicit(PXSelectBase<ARFinCharge, PXSelect<ARFinCharge, Where<ARFinCharge.finChargeID, Equal<Required<ARFinCharge.finChargeID>>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, new object[1]
    {
      (object) ((ARFinCharge) e.Row).FinChargeID
    })) == null)
      return;
    cache.RaiseExceptionHandling<ARFinCharge.finChargeID>(e.Row, (object) ((ARFinCharge) e.Row).FinChargeID, (Exception) new PXException("Record already exists"));
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void ARFinCharge_RowSelected(PXCache cache, PXRowSelectedEventArgs e)
  {
    if (!(e.Row is ARFinCharge row))
      return;
    Decimal? feeAmount = row.FeeAmount;
    int num1;
    if (feeAmount.HasValue)
    {
      feeAmount = row.FeeAmount;
      Decimal num2 = 0M;
      num1 = !(feeAmount.GetValueOrDefault() == num2 & feeAmount.HasValue) ? 1 : 0;
    }
    else
      num1 = 0;
    bool flag = num1 != 0;
    PXUIFieldAttribute.SetEnabled<ARFinCharge.feeAccountID>(cache, (object) row, flag);
    PXUIFieldAttribute.SetEnabled<ARFinCharge.feeSubID>(cache, (object) row, flag);
    PXUIFieldAttribute.SetEnabled<ARFinCharge.feeDesc>(cache, (object) row, flag);
    PXCache pxCache1 = cache;
    ARFinCharge arFinCharge1 = row;
    int? chargingMethod = row.ChargingMethod;
    int num3 = chargingMethod.GetValueOrDefault() == 1 ? 1 : 0;
    PXUIFieldAttribute.SetVisible<ARFinCharge.fixedAmount>(pxCache1, (object) arFinCharge1, num3 != 0);
    PXCache pxCache2 = cache;
    ARFinCharge arFinCharge2 = row;
    chargingMethod = row.ChargingMethod;
    int num4 = chargingMethod.GetValueOrDefault() == 2 ? 1 : 0;
    PXUIFieldAttribute.SetVisible<ARFinCharge.lineThreshold>(pxCache2, (object) arFinCharge2, num4 != 0);
    PXCache pxCache3 = cache;
    ARFinCharge arFinCharge3 = row;
    chargingMethod = row.ChargingMethod;
    int num5 = chargingMethod.GetValueOrDefault() == 3 ? 1 : 0;
    PXUIFieldAttribute.SetVisible<ARFinCharge.minFinChargeAmount>(pxCache3, (object) arFinCharge3, num5 != 0);
    ((PXSelectBase) this.PercentList).AllowSelect = row.PercentFlag.GetValueOrDefault();
    ((PXSelectBase) this.PercentList).AllowInsert = row.PercentFlag.GetValueOrDefault();
    ((PXSelectBase) this.PercentList).AllowUpdate = row.PercentFlag.GetValueOrDefault();
    ((PXSelectBase) this.PercentList).AllowDelete = row.PercentFlag.GetValueOrDefault();
  }

  protected virtual void ARFinChargePercent_RowPersisting(
    PXCache sender,
    PXRowPersistingEventArgs e)
  {
    if (!(e.Row is ARFinChargePercent row))
      return;
    Decimal? finChargePercent = row.FinChargePercent;
    Decimal num = 0M;
    if (!(finChargePercent.GetValueOrDefault() < num & finChargePercent.HasValue))
      return;
    sender.RaiseExceptionHandling<ARFinChargePercent.finChargePercent>((object) row, (object) row.FinChargePercent, (Exception) new PXSetPropertyException<ARFinChargePercent.finChargePercent>("Incorrect value. The value to be entered must be greater than or equal to {0}.", new object[1]
    {
      (object) 0.ToString()
    }));
  }
}
