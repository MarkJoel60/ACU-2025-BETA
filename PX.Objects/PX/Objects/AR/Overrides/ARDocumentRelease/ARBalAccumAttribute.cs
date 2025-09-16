// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.Overrides.ARDocumentRelease.ARBalAccumAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.AR.Overrides.ARDocumentRelease;

public class ARBalAccumAttribute : PXAccumulatorAttribute
{
  public ARBalAccumAttribute()
  {
    this._SingleRecord = true;
    ((PXDBInterceptorAttribute) this).PersistOrder = (PersistOrder) 10;
  }

  protected virtual bool PrepareInsert(PXCache sender, object row, PXAccumulatorCollection columns)
  {
    if (!base.PrepareInsert(sender, row, columns))
      return false;
    ARBalances arBalances = (ARBalances) row;
    columns.Update<ARBalances.lastInvoiceDate>((object) arBalances.LastInvoiceDate, (PXDataFieldAssign.AssignBehavior) 2);
    columns.Update<ARBalances.numberInvoicePaid>((object) arBalances.NumberInvoicePaid, (PXDataFieldAssign.AssignBehavior) 1);
    columns.Update<ARBalances.paidInvoiceDays>((object) arBalances.PaidInvoiceDays, (PXDataFieldAssign.AssignBehavior) 1);
    columns.Update<ARBalances.lastModifiedByID>((object) arBalances.LastModifiedByID, (PXDataFieldAssign.AssignBehavior) 0);
    columns.Update<ARBalances.lastModifiedByScreenID>((object) arBalances.LastModifiedByScreenID, (PXDataFieldAssign.AssignBehavior) 0);
    columns.Update<ARBalances.lastModifiedDateTime>((object) arBalances.LastModifiedDateTime, (PXDataFieldAssign.AssignBehavior) 0);
    DateTime? nullable1 = arBalances.LastDocDate;
    if (nullable1.HasValue)
      columns.Update<ARBalances.lastDocDate>((object) arBalances.LastDocDate, (PXDataFieldAssign.AssignBehavior) 2);
    if (arBalances.StatementRequired.GetValueOrDefault())
      columns.Update<ARBalances.statementRequired>((object) arBalances.StatementRequired, (PXDataFieldAssign.AssignBehavior) 0);
    nullable1 = arBalances.LastInvoiceDate;
    if (!nullable1.HasValue && !arBalances.NumberInvoicePaid.HasValue && !arBalances.PaidInvoiceDays.HasValue && arBalances.CuryID == null)
    {
      Decimal? nullable2 = arBalances.CurrentBal;
      Decimal num1 = 0M;
      if (nullable2.GetValueOrDefault() == num1 & nullable2.HasValue)
      {
        nullable2 = arBalances.UnreleasedBal;
        Decimal num2 = 0M;
        if (nullable2.GetValueOrDefault() == num2 & nullable2.HasValue)
        {
          nullable2 = arBalances.TotalOpenOrders;
          Decimal num3 = 0M;
          if (nullable2.GetValueOrDefault() == num3 & nullable2.HasValue)
          {
            nullable2 = arBalances.TotalPrepayments;
            Decimal num4 = 0M;
            if (nullable2.GetValueOrDefault() == num4 & nullable2.HasValue)
            {
              nullable2 = arBalances.TotalQuotations;
              Decimal num5 = 0M;
              if (nullable2.GetValueOrDefault() == num5 & nullable2.HasValue)
              {
                nullable2 = arBalances.TotalShipped;
                Decimal num6 = 0M;
                if (nullable2.GetValueOrDefault() == num6 & nullable2.HasValue)
                {
                  nullable1 = arBalances.LastDocDate;
                  if (!nullable1.HasValue)
                    return arBalances.StatementRequired.GetValueOrDefault();
                }
              }
            }
          }
        }
      }
    }
    return true;
  }
}
