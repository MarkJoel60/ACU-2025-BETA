// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.UpdateDiscountProcess
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.AR;

public class UpdateDiscountProcess : PXGraph<UpdateDiscountProcess>
{
  public virtual void UpdateDiscount(ARUpdateDiscounts.SelectedItem item, DateTime? filterDate)
  {
    this.UpdateDiscount(item.DiscountID, item.DiscountSequenceID, filterDate);
  }

  public virtual void UpdateDiscount(
    string discountID,
    string discountSequenceID,
    DateTime? filterDate)
  {
    using (new PXConnectionScope())
    {
      using (PXTransactionScope transactionScope = new PXTransactionScope())
      {
        foreach (PXResult<DiscountSequenceDetail> pxResult in PXSelectBase<DiscountSequenceDetail, PXSelect<DiscountSequenceDetail, Where<DiscountSequenceDetail.discountID, Equal<Required<DiscountSequenceDetail.discountID>>, And<DiscountSequenceDetail.discountSequenceID, Equal<Required<DiscountSequenceDetail.discountSequenceID>>, And<DiscountSequenceDetail.isLast, Equal<False>>>>>.Config>.Select((PXGraph) this, new object[2]
        {
          (object) discountID,
          (object) discountSequenceID
        }))
        {
          DiscountSequenceDetail discountSequenceDetail = PXResult<DiscountSequenceDetail>.op_Implicit(pxResult);
          DateTime? pendingDate = discountSequenceDetail.PendingDate;
          if (pendingDate.HasValue)
          {
            pendingDate = discountSequenceDetail.PendingDate;
            if (pendingDate.Value <= filterDate.Value)
            {
              if (!PXDatabase.Update<DiscountSequenceDetail>(new PXDataFieldParam[17]
              {
                (PXDataFieldParam) new PXDataFieldAssign("IsActive", (PXDbType) 2, (object) discountSequenceDetail.IsActive),
                (PXDataFieldParam) new PXDataFieldAssign("Amount", (PXDbType) 5, (object) discountSequenceDetail.Amount),
                (PXDataFieldParam) new PXDataFieldAssign("AmountTo", (PXDbType) 5, (object) discountSequenceDetail.AmountTo),
                (PXDataFieldParam) new PXDataFieldAssign("Quantity", (PXDbType) 5, (object) discountSequenceDetail.Quantity),
                (PXDataFieldParam) new PXDataFieldAssign("QuantityTo", (PXDbType) 5, (object) discountSequenceDetail.QuantityTo),
                (PXDataFieldParam) new PXDataFieldAssign("Discount", (PXDbType) 5, (object) discountSequenceDetail.Discount),
                (PXDataFieldParam) new PXDataFieldAssign("FreeItemQty", (PXDbType) 5, (object) discountSequenceDetail.FreeItemQty),
                (PXDataFieldParam) new PXDataFieldAssign("LastDate", (PXDbType) 4, (object) discountSequenceDetail.PendingDate),
                (PXDataFieldParam) new PXDataFieldAssign("PendingAmount", (PXDbType) 5, (object) null),
                (PXDataFieldParam) new PXDataFieldAssign("PendingQuantity", (PXDbType) 5, (object) 0M),
                (PXDataFieldParam) new PXDataFieldAssign("PendingDiscount", (PXDbType) 5, (object) null),
                (PXDataFieldParam) new PXDataFieldAssign("PendingFreeItemQty", (PXDbType) 5, (object) 0M),
                (PXDataFieldParam) new PXDataFieldAssign("PendingDate", (PXDbType) 4, (object) null),
                (PXDataFieldParam) new PXDataFieldRestrict("DiscountID", (PXDbType) 12, (object) discountSequenceDetail.DiscountID),
                (PXDataFieldParam) new PXDataFieldRestrict("DiscountSequenceID", (PXDbType) 12, (object) discountSequenceDetail.DiscountSequenceID),
                (PXDataFieldParam) new PXDataFieldRestrict("LineNbr", (PXDbType) 8, (object) discountSequenceDetail.LineNbr),
                (PXDataFieldParam) new PXDataFieldRestrict("IsLast", (PXDbType) 2, (object) 1)
              }))
                PXDatabase.Insert<DiscountSequenceDetail>(new PXDataFieldAssign[23]
                {
                  new PXDataFieldAssign("DiscountID", (PXDbType) 12, (object) discountSequenceDetail.DiscountID),
                  new PXDataFieldAssign("DiscountSequenceID", (PXDbType) 12, (object) discountSequenceDetail.DiscountSequenceID),
                  new PXDataFieldAssign("LineNbr", (PXDbType) 8, (object) discountSequenceDetail.LineNbr),
                  new PXDataFieldAssign("IsActive", (PXDbType) 2, (object) discountSequenceDetail.IsActive),
                  new PXDataFieldAssign("IsLast", (PXDbType) 2, (object) 1),
                  new PXDataFieldAssign("Amount", (PXDbType) 5, (object) discountSequenceDetail.Amount),
                  new PXDataFieldAssign("AmountTo", (PXDbType) 5, (object) discountSequenceDetail.AmountTo),
                  new PXDataFieldAssign("Quantity", (PXDbType) 5, (object) discountSequenceDetail.Quantity),
                  new PXDataFieldAssign("QuantityTo", (PXDbType) 5, (object) discountSequenceDetail.QuantityTo),
                  new PXDataFieldAssign("Discount", (PXDbType) 5, (object) discountSequenceDetail.Discount),
                  new PXDataFieldAssign("FreeItemQty", (PXDbType) 5, (object) discountSequenceDetail.FreeItemQty),
                  new PXDataFieldAssign("LastDate", (PXDbType) 4, (object) discountSequenceDetail.PendingDate),
                  new PXDataFieldAssign("PendingAmount", (PXDbType) 5, (object) null),
                  new PXDataFieldAssign("PendingQuantity", (PXDbType) 5, (object) 0M),
                  new PXDataFieldAssign("PendingDiscount", (PXDbType) 5, (object) null),
                  new PXDataFieldAssign("PendingFreeItemQty", (PXDbType) 5, (object) 0M),
                  new PXDataFieldAssign("PendingDate", (PXDbType) 4, (object) null),
                  new PXDataFieldAssign("CreatedByID", (PXDbType) 14, new int?(16 /*0x10*/), (object) discountSequenceDetail.CreatedByID),
                  new PXDataFieldAssign("CreatedByScreenID", (PXDbType) 3, new int?(8), (object) discountSequenceDetail.CreatedByScreenID),
                  new PXDataFieldAssign("CreatedDateTime", (PXDbType) 4, new int?(8), (object) discountSequenceDetail.CreatedDateTime),
                  new PXDataFieldAssign("LastModifiedByID", (PXDbType) 14, new int?(16 /*0x10*/), (object) discountSequenceDetail.LastModifiedByID),
                  new PXDataFieldAssign("LastModifiedByScreenID", (PXDbType) 3, new int?(8), (object) discountSequenceDetail.LastModifiedByScreenID),
                  new PXDataFieldAssign("LastModifiedDateTime", (PXDbType) 4, new int?(8), (object) discountSequenceDetail.LastModifiedDateTime)
                });
              PXDatabase.Update<DiscountSequenceDetail>(new PXDataFieldParam[14]
              {
                (PXDataFieldParam) new PXDataFieldAssign("Amount", (PXDbType) 5, (object) discountSequenceDetail.PendingAmount),
                (PXDataFieldParam) new PXDataFieldAssign("Quantity", (PXDbType) 5, (object) discountSequenceDetail.PendingQuantity),
                (PXDataFieldParam) new PXDataFieldAssign("Discount", (PXDbType) 5, (object) discountSequenceDetail.PendingDiscount),
                (PXDataFieldParam) new PXDataFieldAssign("FreeItemQty", (PXDbType) 5, (object) discountSequenceDetail.PendingFreeItemQty),
                (PXDataFieldParam) new PXDataFieldAssign("LastDate", (PXDbType) 4, (object) discountSequenceDetail.PendingDate),
                (PXDataFieldParam) new PXDataFieldAssign("PendingAmount", (PXDbType) 5, (object) null),
                (PXDataFieldParam) new PXDataFieldAssign("PendingQuantity", (PXDbType) 5, (object) 0M),
                (PXDataFieldParam) new PXDataFieldAssign("PendingDiscount", (PXDbType) 5, (object) null),
                (PXDataFieldParam) new PXDataFieldAssign("PendingFreeItemQty", (PXDbType) 5, (object) 0M),
                (PXDataFieldParam) new PXDataFieldAssign("PendingDate", (PXDbType) 4, (object) null),
                (PXDataFieldParam) new PXDataFieldRestrict("DiscountID", (PXDbType) 12, (object) discountSequenceDetail.DiscountID),
                (PXDataFieldParam) new PXDataFieldRestrict("DiscountSequenceID", (PXDbType) 12, (object) discountSequenceDetail.DiscountSequenceID),
                (PXDataFieldParam) new PXDataFieldRestrict("LineNbr", (PXDbType) 8, (object) discountSequenceDetail.LineNbr),
                (PXDataFieldParam) new PXDataFieldRestrict("IsLast", (PXDbType) 2, (object) 0)
              });
            }
          }
        }
        foreach (PXResult<DiscountSequenceDetail> pxResult in PXSelectBase<DiscountSequenceDetail, PXSelectReadonly<DiscountSequenceDetail, Where<DiscountSequenceDetail.discountID, Equal<Required<DiscountSequenceDetail.discountID>>, And<DiscountSequenceDetail.discountSequenceID, Equal<Required<DiscountSequenceDetail.discountSequenceID>>>>>.Config>.Select((PXGraph) this, new object[2]
        {
          (object) discountID,
          (object) discountSequenceID
        }))
        {
          DiscountSequenceDetail discountSequenceDetail1 = PXResult<DiscountSequenceDetail>.op_Implicit(pxResult);
          DiscountSequenceDetail discountSequenceDetail2 = PXResultset<DiscountSequenceDetail>.op_Implicit(PXSelectBase<DiscountSequenceDetail, PXSelectReadonly<DiscountSequenceDetail, Where<DiscountSequenceDetail.discountID, Equal<Required<DiscountSequenceDetail.discountID>>, And<DiscountSequenceDetail.discountSequenceID, Equal<Required<DiscountSequenceDetail.discountSequenceID>>, And<DiscountSequenceDetail.amount, Greater<Required<DiscountSequenceDetail.amount>>, And<DiscountSequenceDetail.isLast, Equal<Required<DiscountSequenceDetail.isLast>>, And<DiscountSequenceDetail.isActive, Equal<True>>>>>>, OrderBy<Asc<DiscountSequenceDetail.amount>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, new object[4]
          {
            (object) discountID,
            (object) discountSequenceID,
            (object) discountSequenceDetail1.Amount,
            (object) discountSequenceDetail1.IsLast
          }));
          DiscountSequenceDetail discountSequenceDetail3 = PXResultset<DiscountSequenceDetail>.op_Implicit(PXSelectBase<DiscountSequenceDetail, PXSelectReadonly<DiscountSequenceDetail, Where<DiscountSequenceDetail.discountID, Equal<Required<DiscountSequenceDetail.discountID>>, And<DiscountSequenceDetail.discountSequenceID, Equal<Required<DiscountSequenceDetail.discountSequenceID>>, And<DiscountSequenceDetail.quantity, Greater<Required<DiscountSequenceDetail.quantity>>, And<DiscountSequenceDetail.isLast, Equal<Required<DiscountSequenceDetail.isLast>>, And<DiscountSequenceDetail.isActive, Equal<True>>>>>>, OrderBy<Asc<DiscountSequenceDetail.quantity>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, new object[4]
          {
            (object) discountID,
            (object) discountSequenceID,
            (object) discountSequenceDetail1.Quantity,
            (object) discountSequenceDetail1.IsLast
          }));
          PXDatabase.Update<DiscountSequenceDetail>(new PXDataFieldParam[3]
          {
            (PXDataFieldParam) new PXDataFieldAssign("AmountTo", (PXDbType) 5, (object) (discountSequenceDetail2 == null ? new Decimal?() : discountSequenceDetail2.Amount)),
            (PXDataFieldParam) new PXDataFieldAssign("QuantityTo", (PXDbType) 5, (object) (discountSequenceDetail3 == null ? new Decimal?() : discountSequenceDetail3.Quantity)),
            (PXDataFieldParam) new PXDataFieldRestrict("DiscountDetailsID", (PXDbType) 8, (object) discountSequenceDetail1.DiscountDetailsID)
          });
        }
        DiscountSequence discountSequence = PXResultset<DiscountSequence>.op_Implicit(PXSelectBase<DiscountSequence, PXSelect<DiscountSequence, Where<DiscountSequence.discountID, Equal<Required<DiscountSequence.discountID>>, And<DiscountSequence.discountSequenceID, Equal<Required<DiscountSequence.discountSequenceID>>>>>.Config>.Select((PXGraph) this, new object[2]
        {
          (object) discountID,
          (object) discountSequenceID
        }));
        DateTime? startDate;
        if (discountSequence != null)
        {
          startDate = discountSequence.StartDate;
          if (startDate.HasValue && discountSequence.PendingFreeItemID.HasValue)
          {
            PXDatabase.Update<DiscountSequence>(new PXDataFieldParam[6]
            {
              (PXDataFieldParam) new PXDataFieldAssign("LastFreeItemID", (PXDbType) 200, (object) "FreeItemID"),
              (PXDataFieldParam) new PXDataFieldAssign("FreeItemID", (PXDbType) 200, (object) "PendingFreeItemID"),
              (PXDataFieldParam) new PXDataFieldAssign("PendingFreeItemID", (PXDbType) 8, (object) null),
              (PXDataFieldParam) new PXDataFieldAssign("UpdateDate", (PXDbType) 4, (object) filterDate.Value),
              (PXDataFieldParam) new PXDataFieldRestrict("DiscountID", (PXDbType) 22, (object) discountID),
              (PXDataFieldParam) new PXDataFieldRestrict("DiscountSequenceID", (PXDbType) 22, (object) discountSequenceID)
            });
            goto label_26;
          }
        }
        if (discountSequence != null)
        {
          startDate = discountSequence.StartDate;
          if (startDate.HasValue)
            PXDatabase.Update<DiscountSequence>(new PXDataFieldParam[3]
            {
              (PXDataFieldParam) new PXDataFieldAssign("UpdateDate", (PXDbType) 4, (object) filterDate.Value),
              (PXDataFieldParam) new PXDataFieldRestrict("DiscountID", (PXDbType) 22, (object) discountID),
              (PXDataFieldParam) new PXDataFieldRestrict("DiscountSequenceID", (PXDbType) 22, (object) discountSequenceID)
            });
        }
label_26:
        transactionScope.Complete();
      }
    }
  }
}
