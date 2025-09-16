// Decompiled with JetBrains decompiler
// Type: PX.Objects.TX.ExpenseClaimEntryTaxCalcModeExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.EP;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.TX;

public class ExpenseClaimEntryTaxCalcModeExt : 
  TaxCalculationModeExtension<ExpenseClaimEntry, EPExpenseClaim>
{
  protected override IEnumerable<Tax> GetTaxes()
  {
    return GraphHelper.RowCast<Tax>((IEnumerable) ((PXSelectBase<EPTaxAggregate>) this.Base.Taxes).Select(Array.Empty<object>()));
  }

  public override void _(Events.RowPersisting<EntityWithTaxCalcMode> e)
  {
    if (this.SkipValidation || this.Taxes.Count<Tax>() == 0)
      return;
    PXSetPropertyException propertyException = (PXSetPropertyException) null;
    foreach (PXResult<EPExpenseClaimDetails> pxResult in ((PXSelectBase<EPExpenseClaimDetails>) this.Base.ExpenseClaimDetails).Select(Array.Empty<object>()))
    {
      EPExpenseClaimDetails expenseClaimDetails = PXResult<EPExpenseClaimDetails>.op_Implicit(pxResult);
      try
      {
        TaxCalculationModeExtension<ExpenseClaimEntry, EPExpenseClaim>.VerifyTransactions(expenseClaimDetails.TaxCalcMode, GraphHelper.RowCast<Tax>((IEnumerable) ((PXSelectBase<EPTaxTran>) this.Base.Tax_Rows).Select(new object[1]
        {
          (object) expenseClaimDetails.ClaimDetailID
        })));
      }
      catch (PXException ex)
      {
        propertyException = new PXSetPropertyException(((Exception) ex).Message);
        ((PXSelectBase) this.Base.ExpenseClaimDetails).Cache.RaiseExceptionHandling<EPExpenseClaimDetails.curyTaxTotal>((object) expenseClaimDetails, (object) expenseClaimDetails.CuryTaxTotal, (Exception) propertyException);
      }
    }
    if (propertyException != null)
      throw propertyException;
  }

  public virtual void _(Events.RowUpdated<EPExpenseClaimDetails> e)
  {
    EPExpenseClaimDetails row = e.Row;
    EPExpenseClaimDetails oldRow = e.OldRow;
    if (((Events.Event<PXRowUpdatedEventArgs, Events.RowUpdated<EPExpenseClaimDetails>>) e).Cache.ObjectsEqual<EPExpenseClaimDetails.refNbr, EPExpenseClaimDetails.taxCategoryID, EPExpenseClaimDetails.taxCalcMode, EPExpenseClaimDetails.taxZoneID>((object) e.Row, (object) e.OldRow))
      return;
    try
    {
      TaxCalculationModeExtension<ExpenseClaimEntry, EPExpenseClaim>.VerifyTransactions(row.TaxCalcMode, GraphHelper.RowCast<Tax>((IEnumerable) ((PXSelectBase<EPTaxTran>) this.Base.Tax_Rows).Select(new object[1]
      {
        (object) row.ClaimDetailID
      })));
    }
    catch (PXException ex)
    {
      ((Events.Event<PXRowUpdatedEventArgs, Events.RowUpdated<EPExpenseClaimDetails>>) e).Cache.RaiseExceptionHandling<EPExpenseClaimDetails.curyTaxTotal>((object) row, (object) row.CuryTaxTotal, (Exception) new PXSetPropertyException(((Exception) ex).Message));
    }
  }
}
