// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.MultiCurrency.CABankTransactionsMaintMultiCurrency
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using CommonServiceLocator;
using PX.Data;
using PX.Objects.CM.Extensions;
using PX.Objects.CS;
using System;

#nullable disable
namespace PX.Objects.CA.MultiCurrency;

public sealed class CABankTransactionsMaintMultiCurrency : 
  CABankTransactionsBaseMultiCurrency<CABankTransactionsMaint>
{
  private string CurrentModule = "CA";

  protected override string Module => this.CurrentModule;

  protected override PXSelectBase[] GetChildren()
  {
    return new PXSelectBase[7]
    {
      (PXSelectBase) this.Base.Details,
      (PXSelectBase) this.Base.TranSplit,
      (PXSelectBase) this.Base.Adjustments,
      (PXSelectBase) this.Base.caTran,
      (PXSelectBase) this.Base.TranMatch,
      (PXSelectBase) this.Base.Taxes,
      (PXSelectBase) this.Base.TaxTrans
    };
  }

  public PX.Objects.CM.Extensions.CurrencyInfo CreateCurrencyInfo()
  {
    PX.Objects.CM.Extensions.CurrencyInfo info = (PX.Objects.CM.Extensions.CurrencyInfo) ((PXSelectBase) this.currencyinfo).Cache.Insert((object) new PX.Objects.CM.Extensions.CurrencyInfo());
    this.defaultCurrencyRate(((PXSelectBase) this.currencyinfo).Cache, info, true, false);
    return info;
  }

  protected void _(
    Events.FieldUpdated<CABankTran, CABankTran.origModule> e)
  {
    if (e.NewValue == null)
      return;
    this.CurrentModule = e.Row.OrigModule;
    this.SourceFieldUpdated<CABankTran.curyInfoID, CABankTran.curyID, CABankTran.tranDate>(((Events.Event<PXFieldUpdatedEventArgs, Events.FieldUpdated<CABankTran, CABankTran.origModule>>) e).Cache, (IBqlTable) e.Row);
  }

  protected override void _(
    Events.FieldDefaulting<PX.Objects.CM.Extensions.CurrencyInfo, PX.Objects.CM.Extensions.CurrencyInfo.curyRateTypeID> e)
  {
    if (!PXAccess.FeatureInstalled<FeaturesSet.multicurrency>())
      return;
    PX.Objects.Extensions.MultiCurrency.CurySource curySource = this.CurrentSourceSelect();
    if (!string.IsNullOrEmpty(curySource?.CuryRateTypeID))
    {
      ((Events.FieldDefaultingBase<Events.FieldDefaulting<PX.Objects.CM.Extensions.CurrencyInfo, PX.Objects.CM.Extensions.CurrencyInfo.curyRateTypeID>, PX.Objects.CM.Extensions.CurrencyInfo, object>) e).NewValue = (object) curySource.CuryRateTypeID;
      ((Events.FieldDefaultingBase<Events.FieldDefaulting<PX.Objects.CM.Extensions.CurrencyInfo, PX.Objects.CM.Extensions.CurrencyInfo.curyRateTypeID>>) e).Cancel = true;
    }
    else
    {
      if (e.Row == null || string.IsNullOrEmpty(((PXSelectBase<CABankTran>) this.Base.Details).Current?.OrigModule))
        return;
      ((Events.FieldDefaultingBase<Events.FieldDefaulting<PX.Objects.CM.Extensions.CurrencyInfo, PX.Objects.CM.Extensions.CurrencyInfo.curyRateTypeID>, PX.Objects.CM.Extensions.CurrencyInfo, object>) e).NewValue = (object) ServiceLocator.Current.GetInstance<Func<PXGraph, IPXCurrencyService>>()((PXGraph) this.Base).DefaultRateTypeID(((PXSelectBase<CABankTran>) this.Base.Details).Current?.OrigModule);
      ((Events.FieldDefaultingBase<Events.FieldDefaulting<PX.Objects.CM.Extensions.CurrencyInfo, PX.Objects.CM.Extensions.CurrencyInfo.curyRateTypeID>>) e).Cancel = true;
    }
  }

  protected void _(Events.RowUpdating<CABankTranDetail> e, PXRowUpdating baseMethod)
  {
    this.UpdateNewTranDetailCuryTranAmtOrCuryUnitPrice(((Events.Event<PXRowUpdatingEventArgs, Events.RowUpdating<CABankTranDetail>>) e).Cache, (ICATranDetail) e.Row, (ICATranDetail) e.NewRow);
    baseMethod.Invoke(((Events.Event<PXRowUpdatingEventArgs, Events.RowUpdating<CABankTranDetail>>) e).Cache, ((Events.Event<PXRowUpdatingEventArgs, Events.RowUpdating<CABankTranDetail>>) e).Args);
  }

  protected void _(Events.RowInserting<CABankTranAdjustment> e)
  {
    this.InsertNewCurrencyInfoWithoutCuryID<CABankTranAdjustment.adjdCuryInfoID>(((Events.Event<PXRowInsertingEventArgs, Events.RowInserting<CABankTranAdjustment>>) e).Cache, (object) e.Row);
    this.InsertNewCurrencyInfoWithoutCuryID<CABankTranAdjustment.adjgCuryInfoID>(((Events.Event<PXRowInsertingEventArgs, Events.RowInserting<CABankTranAdjustment>>) e).Cache, (object) e.Row);
  }

  protected void _(
    Events.FieldUpdated<CABankTranAdjustment.adjdCuryRate> e)
  {
    CABankTranAdjustment row = (CABankTranAdjustment) e.Row;
    PX.Objects.CM.Extensions.CurrencyInfo currencyInfo1 = PXResultset<PX.Objects.CM.Extensions.CurrencyInfo>.op_Implicit(PXSelectBase<PX.Objects.CM.Extensions.CurrencyInfo, PXSelect<PX.Objects.CM.Extensions.CurrencyInfo, Where<PX.Objects.CM.Extensions.CurrencyInfo.curyInfoID, Equal<Current<CABankTranAdjustment.adjgCuryInfoID>>>>.Config>.SelectSingleBound((PXGraph) this.Base, new object[1]
    {
      e.Row
    }, Array.Empty<object>()));
    PX.Objects.CM.Extensions.CurrencyInfo info = PXResultset<PX.Objects.CM.Extensions.CurrencyInfo>.op_Implicit(PXSelectBase<PX.Objects.CM.Extensions.CurrencyInfo, PXSelect<PX.Objects.CM.Extensions.CurrencyInfo, Where<PX.Objects.CM.Extensions.CurrencyInfo.curyInfoID, Equal<Current<CABankTranAdjustment.adjdCuryInfoID>>>>.Config>.SelectSingleBound((PXGraph) this.Base, new object[1]
    {
      e.Row
    }, Array.Empty<object>()));
    Decimal num1 = row.CuryAdjgAmt.Value;
    Decimal num2 = row.CuryAdjgDiscAmt.Value;
    Decimal? nullable1;
    if (string.Equals(currencyInfo1.CuryID, info.CuryID))
    {
      nullable1 = row.AdjdCuryRate;
      Decimal num3 = 1M;
      if (!(nullable1.GetValueOrDefault() == num3 & nullable1.HasValue))
      {
        row.AdjdCuryRate = new Decimal?(1M);
        info.CuryEffDate = ((PXSelectBase<CABankTran>) this.Base.DetailsForPaymentCreation).Current.TranDate;
        this.defaultCurrencyRate(((Events.Event<PXFieldUpdatedEventArgs, Events.FieldUpdated<CABankTranAdjustment.adjdCuryRate>>) e).Cache, info, true, false);
        goto label_16;
      }
    }
    if (string.Equals(info.CuryID, info.BaseCuryID))
    {
      CABankTranAdjustment bankTranAdjustment = row;
      Decimal? nullable2;
      if (!(currencyInfo1.CuryMultDiv == "M"))
      {
        nullable2 = currencyInfo1.CuryRate;
      }
      else
      {
        Decimal num4 = (Decimal) 1;
        nullable1 = currencyInfo1.CuryRate;
        nullable2 = nullable1.HasValue ? new Decimal?(num4 / nullable1.GetValueOrDefault()) : new Decimal?();
      }
      bankTranAdjustment.AdjdCuryRate = nullable2;
    }
    else
    {
      info.CuryRate = row.AdjdCuryRate;
      PX.Objects.CM.Extensions.CurrencyInfo currencyInfo2 = info;
      nullable1 = row.AdjdCuryRate;
      Decimal? nullable3 = new Decimal?(Math.Round(1M / nullable1.Value, 8, MidpointRounding.AwayFromZero));
      currencyInfo2.RecipRate = nullable3;
      info.CuryMultDiv = "M";
      PX.Objects.CM.Extensions.CurrencyInfo currencyInfo3 = info;
      nullable1 = row.CuryAdjdAmt;
      Decimal curyval1 = nullable1.Value;
      num1 = currencyInfo3.CuryConvBase(curyval1);
      PX.Objects.CM.Extensions.CurrencyInfo currencyInfo4 = info;
      nullable1 = row.CuryAdjdDiscAmt;
      Decimal curyval2 = nullable1.Value;
      num2 = currencyInfo4.CuryConvBase(curyval2);
      PX.Objects.CM.Extensions.CurrencyInfo currencyInfo5 = info;
      nullable1 = row.CuryAdjdAmt;
      Decimal num5 = nullable1.Value;
      nullable1 = row.CuryAdjdDiscAmt;
      Decimal num6 = nullable1.Value;
      Decimal curyval3 = num5 + num6;
      Decimal num7 = currencyInfo5.CuryConvBase(curyval3);
      PX.Objects.CM.Extensions.CurrencyInfo currencyInfo6 = info;
      nullable1 = row.AdjdCuryRate;
      Decimal num8 = nullable1.Value;
      Decimal num9;
      if (!(currencyInfo1.CuryMultDiv == "M"))
      {
        nullable1 = currencyInfo1.CuryRate;
        num9 = 1M / nullable1.Value;
      }
      else
      {
        nullable1 = currencyInfo1.CuryRate;
        num9 = nullable1.Value;
      }
      Decimal? nullable4 = new Decimal?(Math.Round(num8 * num9, 8, MidpointRounding.AwayFromZero));
      currencyInfo6.CuryRate = nullable4;
      PX.Objects.CM.Extensions.CurrencyInfo currencyInfo7 = info;
      Decimal num10;
      if (!(currencyInfo1.CuryMultDiv == "M"))
      {
        nullable1 = currencyInfo1.CuryRate;
        num10 = nullable1.Value;
      }
      else
      {
        nullable1 = currencyInfo1.CuryRate;
        num10 = 1M / nullable1.Value;
      }
      nullable1 = row.AdjdCuryRate;
      Decimal num11 = nullable1.Value;
      Decimal? nullable5 = new Decimal?(Math.Round(num10 / num11, 8, MidpointRounding.AwayFromZero));
      currencyInfo7.RecipRate = nullable5;
      if (num1 + num2 != num7)
        num2 += num7 - num2 - num1;
    }
label_16:
    GraphHelper.MarkUpdated(((PXGraph) this.Base).Caches[typeof (PX.Objects.CM.Extensions.CurrencyInfo)], (object) info);
    Decimal num12 = num1;
    nullable1 = row.CuryAdjgAmt;
    Decimal num13 = nullable1.Value;
    if (num12 != num13)
      ((Events.Event<PXFieldUpdatedEventArgs, Events.FieldUpdated<CABankTranAdjustment.adjdCuryRate>>) e).Cache.SetValue<CABankTranAdjustment.curyAdjgAmt>(e.Row, (object) num1);
    Decimal num14 = num2;
    nullable1 = row.CuryAdjgDiscAmt;
    Decimal num15 = nullable1.Value;
    if (num14 != num15)
      ((Events.Event<PXFieldUpdatedEventArgs, Events.FieldUpdated<CABankTranAdjustment.adjdCuryRate>>) e).Cache.SetValue<CABankTranAdjustment.curyAdjgDiscAmt>(e.Row, (object) num2);
    this.Base.UpdateBalance((CABankTranAdjustment) e.Row, true);
  }

  protected void _(
    Events.FieldSelecting<CABankTranAdjustment, CABankTranAdjustment.adjdCuryID> e)
  {
    ((Events.FieldSelectingBase<Events.FieldSelecting<CABankTranAdjustment, CABankTranAdjustment.adjdCuryID>>) e).ReturnValue = (object) this.CuryIDFieldSelecting<CABankTranAdjustment.adjdCuryInfoID>(((Events.Event<PXFieldSelectingEventArgs, Events.FieldSelecting<CABankTranAdjustment, CABankTranAdjustment.adjdCuryID>>) e).Cache, (object) e.Row);
  }
}
