// Decompiled with JetBrains decompiler
// Type: PX.Objects.CS.ShipTermsMaint
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;
using System.ComponentModel;

#nullable disable
namespace PX.Objects.CS;

public class ShipTermsMaint : PXGraph<CarrierMaint, ShipTerms>
{
  public PXSelect<ShipTerms> ShipTermsCurrent;
  public PXSelect<PX.Objects.CS.ShipTermsDetail, Where<PX.Objects.CS.ShipTermsDetail.shipTermsID, Equal<Current<ShipTerms.shipTermsID>>>> ShipTermsDetail;

  protected virtual void ShipTermsDetail_RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    PX.Objects.CS.ShipTermsDetail row = (PX.Objects.CS.ShipTermsDetail) e.Row;
    Decimal? breakAmount = row.BreakAmount;
    Decimal num1 = 0M;
    if (breakAmount.GetValueOrDefault() < num1 & breakAmount.HasValue)
    {
      if (sender.RaiseExceptionHandling<PX.Objects.CS.ShipTermsDetail.breakAmount>(e.Row, (object) null, (Exception) new PXSetPropertyException("'{0}' should be positive.", new object[1]
      {
        (object) "[breakAmount]"
      })))
        throw new PXRowPersistingException(typeof (PX.Objects.CS.ShipTermsDetail.breakAmount).Name, (object) null, "'{0}' should be positive.", new object[1]
        {
          (object) "breakAmount"
        });
      ((CancelEventArgs) e).Cancel = true;
    }
    Decimal? nullable = row.FreightCostPercent;
    Decimal num2 = 0M;
    if (nullable.GetValueOrDefault() < num2 & nullable.HasValue)
    {
      if (sender.RaiseExceptionHandling<PX.Objects.CS.ShipTermsDetail.freightCostPercent>(e.Row, (object) null, (Exception) new PXSetPropertyException("'{0}' should not be negative.", new object[1]
      {
        (object) "[freightCostPercent]"
      })))
        throw new PXRowPersistingException(typeof (PX.Objects.CS.ShipTermsDetail.freightCostPercent).Name, (object) null, "'{0}' should not be negative.", new object[1]
        {
          (object) "freightCostPercent"
        });
      ((CancelEventArgs) e).Cancel = true;
    }
    nullable = row.InvoiceAmountPercent;
    Decimal num3 = 0M;
    if (!(nullable.GetValueOrDefault() < num3 & nullable.HasValue))
      return;
    if (sender.RaiseExceptionHandling<PX.Objects.CS.ShipTermsDetail.invoiceAmountPercent>(e.Row, (object) null, (Exception) new PXSetPropertyException("'{0}' should not be negative.", new object[1]
    {
      (object) "[invoiceAmountPercent]"
    })))
      throw new PXRowPersistingException(typeof (PX.Objects.CS.ShipTermsDetail.invoiceAmountPercent).Name, (object) null, "'{0}' should not be negative.", new object[1]
      {
        (object) "invoiceAmountPercent"
      });
    ((CancelEventArgs) e).Cancel = true;
  }
}
