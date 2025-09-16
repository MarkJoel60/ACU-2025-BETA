// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.APInvoiceEntryMultipleBaseCurrencies
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CM;
using PX.Objects.Common;
using PX.Objects.CR;
using PX.Objects.GL;
using System;

#nullable disable
namespace PX.Objects.AP;

public class APInvoiceEntryMultipleBaseCurrencies : PXGraphExtension<APInvoiceEntry>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<PX.Objects.CS.FeaturesSet.multipleBaseCurrencies>();

  protected virtual void _(PX.Data.Events.FieldUpdated<APInvoice.branchID> e)
  {
    BranchBaseAttribute.VerifyFieldInPXCache<APTran, APTran.branchID>((PXGraph) this.Base, this.Base.Transactions.Select());
  }

  [PXOverride]
  public virtual void Persist(System.Action persist)
  {
    BranchBaseAttribute.VerifyFieldInPXCache<APTran, APTran.branchID>((PXGraph) this.Base, this.Base.Transactions.Select());
    persist();
  }

  [PXMergeAttributes(Method = MergeMethod.Merge)]
  [PXRestrictor(typeof (Where<Current2<APInvoiceMultipleBaseCurrenciesRestriction.branchBaseCuryID>, IsNull, Or<Vendor.baseCuryID, IsNull, Or<Vendor.baseCuryID, Equal<Current2<APInvoiceMultipleBaseCurrenciesRestriction.branchBaseCuryID>>>>>), null, new System.Type[] {})]
  protected virtual void _(PX.Data.Events.CacheAttached<APInvoice.vendorID> e)
  {
  }

  protected virtual void _(PX.Data.Events.FieldVerifying<APInvoice.branchID> e)
  {
    if (e.NewValue == null)
      return;
    PX.Objects.GL.Branch branch = PXSelectorAttribute.Select<APInvoice.branchID>(e.Cache, e.Row, (object) (int) e.NewValue) as PX.Objects.GL.Branch;
    string str = (string) PXFormulaAttribute.Evaluate<APInvoiceMultipleBaseCurrenciesRestriction.vendorBaseCuryID>(e.Cache, e.Row);
    if (str != null && branch != null && branch.BaseCuryID != str)
    {
      e.NewValue = (object) branch.BranchCD;
      BAccountR baccountR = PXSelectorAttribute.Select<APInvoice.vendorID>(e.Cache, e.Row) as BAccountR;
      throw new PXSetPropertyException("The branch base currency differs from the base currency of the {0} entity associated with the {1} account.", new object[2]
      {
        (object) PXOrgAccess.GetCD(baccountR.VOrgBAccountID),
        (object) baccountR.AcctCD
      });
    }
  }

  protected virtual void _(PX.Data.Events.RowUpdated<APInvoice> e)
  {
    PX.Objects.GL.Branch branch = PXSelectorAttribute.Select<APInvoice.branchID>(e.Cache, (object) e.Row, (object) e.Row.BranchID) as PX.Objects.GL.Branch;
    if (!(e.Cache.GetValueExt<APInvoiceMultipleBaseCurrenciesRestriction.vendorBaseCuryID>((object) e.Row) is PXFieldState valueExt) || valueExt.Value == null || branch == null || !(branch.BaseCuryID != valueExt.ToString()))
      return;
    e.Row.BranchID = new int?();
  }

  protected virtual void _(PX.Data.Events.RowSelected<APInvoice> e)
  {
    APInvoice row = e.Row;
    if (row == null)
      return;
    PX.Objects.CM.CurrencyInfo currencyInfo = CurrencyInfoAttribute.GetCurrencyInfo<APInvoice.curyInfoID>(e.Cache, (object) row);
    if (currencyInfo == null)
      return;
    if ((e.Cache.GetStateExt<APInvoice.docDate>((object) row) as PXFieldState).ErrorLevel <= PXErrorLevel.Warning)
      e.Cache.RaiseExceptionHandling<APInvoice.docDate>((object) row, (object) row.DocDate, (Exception) null);
    if ((currencyInfo != null ? (!currencyInfo.CuryRate.HasValue ? 1 : 0) : 1) == 0)
    {
      if (currencyInfo == null)
        return;
      Decimal? curyRate = currencyInfo.CuryRate;
      Decimal num = 0.0M;
      if (!(curyRate.GetValueOrDefault() == num & curyRate.HasValue))
        return;
    }
    e.Cache.RaiseExceptionHandling<APInvoice.docDate>((object) row, (object) row.DocDate, (Exception) new PXSetPropertyException("Currency Rate is not defined.", PXErrorLevel.Warning));
  }
}
