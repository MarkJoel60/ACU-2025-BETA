// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.ARDiscountSequenceMaint
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.Common.Discount;
using PX.Objects.IN;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

#nullable enable
namespace PX.Objects.AR;

[Serializable]
public class ARDiscountSequenceMaint : PXGraph<
#nullable disable
ARDiscountSequenceMaint>
{
  public PXSelect<ARDiscountSequenceMaint.ARDiscountEx, Where<ARDiscountSequenceMaint.ARDiscountEx.discountID, Equal<Current<DiscountSequence.discountID>>>> Discount;
  public PXSelectJoin<DiscountSequence, InnerJoin<ARDiscount, On<DiscountSequence.discountID, Equal<ARDiscount.discountID>>>> Sequence;
  public PXSelect<DiscountSequence, Where<DiscountSequence.discountID, Equal<Current<DiscountSequence.discountID>>, And<DiscountSequence.discountSequenceID, Equal<Current<DiscountSequence.discountSequenceID>>>>> CurrentSequence;
  public PXSelect<DiscountDetail, Where<DiscountDetail.discountID, Equal<Current<DiscountSequence.discountID>>, And<DiscountDetail.discountSequenceID, Equal<Current<DiscountSequence.discountSequenceID>>>>, OrderBy<Asc<DiscountDetail.quantity, Asc<DiscountDetail.amount>>>> Details;
  public PXSelect<DiscountSequenceDetail, Where<DiscountSequenceDetail.discountID, Equal<Current<DiscountSequence.discountID>>, And<DiscountSequenceDetail.discountSequenceID, Equal<Current<DiscountSequence.discountSequenceID>>>>, OrderBy<Asc<DiscountSequenceDetail.quantity, Asc<DiscountSequenceDetail.amount>>>> SequenceDetails;
  [PXImport(typeof (DiscountSequence))]
  public PXSelectJoin<DiscountItem, InnerJoin<PX.Objects.IN.InventoryItem, On<DiscountItem.inventoryID, Equal<PX.Objects.IN.InventoryItem.inventoryID>>>, Where<DiscountItem.discountID, Equal<Current<DiscountSequence.discountID>>, And<DiscountItem.discountSequenceID, Equal<Current<DiscountSequence.discountSequenceID>>>>> Items;
  [PXImport(typeof (DiscountSequence))]
  public PXSelectJoin<DiscountCustomer, InnerJoin<Customer, On<DiscountCustomer.customerID, Equal<Customer.bAccountID>>>, Where<DiscountCustomer.discountID, Equal<Current<DiscountSequence.discountID>>, And<DiscountCustomer.discountSequenceID, Equal<Current<DiscountSequence.discountSequenceID>>>>> Customers;
  public PXSelectJoin<DiscountCustomerPriceClass, InnerJoin<ARPriceClass, On<DiscountCustomerPriceClass.customerPriceClassID, Equal<ARPriceClass.priceClassID>>>, Where<DiscountCustomerPriceClass.discountID, Equal<Current<DiscountSequence.discountID>>, And<DiscountCustomerPriceClass.discountSequenceID, Equal<Current<DiscountSequence.discountSequenceID>>>>> CustomerPriceClasses;
  public PXSelectJoin<DiscountInventoryPriceClass, InnerJoin<INPriceClass, On<DiscountInventoryPriceClass.inventoryPriceClassID, Equal<INPriceClass.priceClassID>>>, Where<DiscountInventoryPriceClass.discountID, Equal<Current<DiscountSequence.discountID>>, And<DiscountInventoryPriceClass.discountSequenceID, Equal<Current<DiscountSequence.discountSequenceID>>>>> InventoryPriceClasses;
  public PXSelectJoin<DiscountBranch, InnerJoin<PX.Objects.GL.Branch, On<DiscountBranch.branchID, Equal<PX.Objects.GL.Branch.branchID>>>, Where<DiscountBranch.discountID, Equal<Current<DiscountSequence.discountID>>, And<DiscountBranch.discountSequenceID, Equal<Current<DiscountSequence.discountSequenceID>>>>> Branches;
  public PXSelectJoin<DiscountSite, InnerJoin<INSite, On<DiscountSite.siteID, Equal<INSite.siteID>>>, Where<DiscountSite.discountID, Equal<Current<DiscountSequence.discountID>>, And<DiscountSite.discountSequenceID, Equal<Current<DiscountSequence.discountSequenceID>>>>> Sites;
  public PXFilter<ARDiscountSequenceMaint.UpdateSettingsFilter> UpdateSettings;
  public PXSave<DiscountSequence> Save;
  public PXAction<DiscountSequence> cancel;
  public PXInsert<DiscountSequence> Insert;
  public PXDelete<DiscountSequence> Delete;
  public PXFirst<DiscountSequence> First;
  public PXPrevious<DiscountSequence> Prev;
  public PXNext<DiscountSequence> Next;
  public PXLast<DiscountSequence> Last;
  public PXAction<DiscountSequence> updateDiscounts;

  public ARDiscountSequenceMaint()
  {
    PXDBDefaultAttribute.SetDefaultForInsert<DiscountSequence.discountSequenceID>(((PXSelectBase) this.Sequence).Cache, (object) null, false);
  }

  [PXCancelButton]
  [PXUIField]
  protected virtual IEnumerable Cancel(PXAdapter a)
  {
    ARDiscountSequenceMaint discountSequenceMaint = this;
    DiscountSequence discountSequence1 = (DiscountSequence) null;
    string str1 = (string) null;
    string str2 = (string) null;
    if (a.Searches != null)
    {
      if (a.Searches.Length != 0)
        str1 = (string) a.Searches[0];
      if (a.Searches.Length > 1)
        str2 = (string) a.Searches[1];
    }
    DiscountSequence discountSequence2 = PXResultset<DiscountSequence>.op_Implicit(PXSelectBase<DiscountSequence, PXSelect<DiscountSequence, Where<DiscountSequence.discountSequenceID, Equal<Required<DiscountSequence.discountSequenceID>>, And<DiscountSequence.discountID, Equal<Required<DiscountSequence.discountID>>>>>.Config>.Select((PXGraph) discountSequenceMaint, new object[2]
    {
      (object) str2,
      (object) str1
    }));
    ARDiscount arDiscount = PXResultset<ARDiscount>.op_Implicit(PXSelectBase<ARDiscount, PXSelect<ARDiscount, Where<ARDiscount.discountID, Equal<Required<ARDiscount.discountID>>>>.Config>.Select((PXGraph) discountSequenceMaint, new object[1]
    {
      (object) str1
    }));
    bool flag1 = false;
    if (discountSequence2 == null)
    {
      if (a.Searches != null && a.Searches.Length > 1)
        a.Searches[1] = (object) null;
      flag1 = true;
    }
    if (((PXSelectBase<ARDiscountSequenceMaint.ARDiscountEx>) discountSequenceMaint.Discount).Current != null && ((PXSelectBase<ARDiscountSequenceMaint.ARDiscountEx>) discountSequenceMaint.Discount).Current.DiscountID != str1)
      str2 = (string) null;
    foreach (PXResult<DiscountSequence, ARDiscount> pxResult in ((PXAction) new PXCancel<DiscountSequence>((PXGraph) discountSequenceMaint, nameof (Cancel))).Press(a))
      discountSequence1 = PXResult<DiscountSequence, ARDiscount>.op_Implicit(pxResult);
    if (flag1)
    {
      ((PXSelectBase) discountSequenceMaint.Sequence).Cache.Remove((object) discountSequence1);
      DiscountSequence discountSequence3 = new DiscountSequence();
      discountSequence3.DiscountID = str1;
      if (arDiscount != null)
        discountSequence3.Description = arDiscount.Description;
      discountSequence1 = ((PXSelectBase<DiscountSequence>) discountSequenceMaint.Sequence).Insert(discountSequence3);
      ((PXSelectBase) discountSequenceMaint.Sequence).Cache.IsDirty = false;
      if (arDiscount != null)
      {
        bool? isAutoNumber = arDiscount.IsAutoNumber;
        bool flag2 = false;
        discountSequence1.DiscountSequenceID = !(isAutoNumber.GetValueOrDefault() == flag2 & isAutoNumber.HasValue) ? PXMessages.LocalizeNoPrefix(" <NEW>") : str2;
        ((PXSelectBase) discountSequenceMaint.Sequence).Cache.Normalize();
      }
    }
    yield return (object) discountSequence1;
  }

  [PXUIField]
  [PXProcessButton]
  public virtual IEnumerable UpdateDiscounts(PXAdapter adapter)
  {
    if (((PXSelectBase<DiscountSequence>) this.Sequence).Current != null)
    {
      WebDialogResult webDialogResult = ((PXSelectBase<ARDiscountSequenceMaint.UpdateSettingsFilter>) this.UpdateSettings).AskExt();
      if (webDialogResult == 1 || ((PXGraph) this).IsContractBasedAPI && webDialogResult == 6)
      {
        ((PXAction) this.Save).Press();
        ARUpdateDiscounts.UpdateDiscount(((PXSelectBase<DiscountSequence>) this.Sequence).Current.DiscountID, ((PXSelectBase<DiscountSequence>) this.Sequence).Current.DiscountSequenceID, ((PXSelectBase<ARDiscountSequenceMaint.UpdateSettingsFilter>) this.UpdateSettings).Current.FilterDate);
        ((PXSelectBase<DiscountSequence>) this.Sequence).Current.tstamp = PXDatabase.SelectTimeStamp();
        ((PXAction) this.Save).Press();
        ((PXGraph) this).SelectTimeStamp();
        ((PXSelectBase) this.Details).Cache.Clear();
        ((PXSelectBase) this.Details).Cache.ClearQueryCacheObsolete();
        ((PXSelectBase) this.CurrentSequence).Cache.Clear();
        ((PXSelectBase) this.CurrentSequence).Cache.ClearQueryCacheObsolete();
      }
    }
    return adapter.Get();
  }

  protected virtual void DiscountDetail_StartDate_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    if (!(e.Row is DiscountDetail))
      return;
    if (((PXSelectBase<DiscountSequence>) this.Sequence).Current != null && ((PXSelectBase<DiscountSequence>) this.Sequence).Current.StartDate.HasValue)
      e.NewValue = (object) ((PXSelectBase<DiscountSequence>) this.Sequence).Current.StartDate;
    else
      e.NewValue = (object) ((PXGraph) this).Accessinfo.BusinessDate;
  }

  protected virtual void DiscountSequence_IsPromotion_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    if (!(e.Row is DiscountSequence row))
      return;
    if (row.IsPromotion.GetValueOrDefault())
    {
      row.PendingFreeItemID = new int?();
      row.LastFreeItemID = new int?();
    }
    else
      row.EndDate = new DateTime?();
  }

  protected virtual void DiscountSequence_DiscountID_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    DiscountSequence row = e.Row as DiscountSequence;
    ARDiscount arDiscount = PXResultset<ARDiscount>.op_Implicit(PXSelectBase<ARDiscount, PXSelect<ARDiscount, Where<ARDiscount.discountID, Equal<Required<ARDiscount.discountID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) row.DiscountID
    }));
    if (row == null || arDiscount == null || !(arDiscount.Type == "D"))
      return;
    row.BreakBy = "A";
  }

  protected virtual void DiscountDetail_Amount_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    if (!(e.Row is DiscountDetail row) || !((PXSelectBase<DiscountSequence>) this.Sequence).Current.IsPromotion.GetValueOrDefault())
      return;
    PXResult<DiscountDetail> pxResult1 = PXResultset<DiscountDetail>.op_Implicit(PXSelectBase<DiscountDetail, PXSelect<DiscountDetail, Where<DiscountDetail.discountID, Equal<Current<DiscountSequence.discountID>>, And<DiscountDetail.discountSequenceID, Equal<Current<DiscountSequence.discountSequenceID>>, And<DiscountDetail.amount, Less<Required<DiscountDetail.amount>>, And<DiscountDetail.isActive, Equal<True>>>>>, OrderBy<Desc<DiscountDetail.amount>>>.Config>.SelectSingleBound((PXGraph) this, (object[]) null, new object[1]
    {
      (object) row.Amount
    }));
    if (pxResult1 != null)
    {
      DiscountDetail discountDetail = PXResult<DiscountDetail>.op_Implicit(pxResult1);
      discountDetail.AmountTo = row.Amount;
      ((PXSelectBase<DiscountDetail>) this.Details).Update(discountDetail);
    }
    PXResult<DiscountDetail> pxResult2 = PXResultset<DiscountDetail>.op_Implicit(PXSelectBase<DiscountDetail, PXSelect<DiscountDetail, Where<DiscountDetail.discountID, Equal<Current<DiscountSequence.discountID>>, And<DiscountDetail.discountSequenceID, Equal<Current<DiscountSequence.discountSequenceID>>, And<DiscountDetail.amount, Greater<Required<DiscountDetail.amount>>, And<DiscountDetail.isActive, Equal<True>>>>>, OrderBy<Asc<DiscountDetail.amount>>>.Config>.SelectSingleBound((PXGraph) this, (object[]) null, new object[1]
    {
      (object) row.Amount
    }));
    if (pxResult2 == null)
    {
      row.AmountTo = new Decimal?();
    }
    else
    {
      DiscountDetail discountDetail = PXResult<DiscountDetail>.op_Implicit(pxResult2);
      row.AmountTo = discountDetail.Amount;
    }
  }

  protected virtual void DiscountDetail_Quantity_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    if (!(e.Row is DiscountDetail row) || !((PXSelectBase<DiscountSequence>) this.Sequence).Current.IsPromotion.GetValueOrDefault())
      return;
    PXResult<DiscountDetail> pxResult1 = PXResultset<DiscountDetail>.op_Implicit(PXSelectBase<DiscountDetail, PXSelect<DiscountDetail, Where<DiscountDetail.discountID, Equal<Current<DiscountSequence.discountID>>, And<DiscountDetail.discountSequenceID, Equal<Current<DiscountSequence.discountSequenceID>>, And<DiscountDetail.quantity, Less<Required<DiscountDetail.quantity>>, And<DiscountDetail.isActive, Equal<True>>>>>, OrderBy<Desc<DiscountDetail.quantity>>>.Config>.SelectSingleBound((PXGraph) this, (object[]) null, new object[1]
    {
      (object) row.Quantity
    }));
    if (pxResult1 != null)
    {
      DiscountDetail discountDetail = PXResult<DiscountDetail>.op_Implicit(pxResult1);
      discountDetail.QuantityTo = row.Quantity;
      ((PXSelectBase<DiscountDetail>) this.Details).Update(discountDetail);
    }
    PXResult<DiscountDetail> pxResult2 = PXResultset<DiscountDetail>.op_Implicit(PXSelectBase<DiscountDetail, PXSelect<DiscountDetail, Where<DiscountDetail.discountID, Equal<Current<DiscountSequence.discountID>>, And<DiscountDetail.discountSequenceID, Equal<Current<DiscountSequence.discountSequenceID>>, And<DiscountDetail.quantity, Greater<Required<DiscountDetail.quantity>>, And<DiscountDetail.isActive, Equal<True>>>>>, OrderBy<Asc<DiscountDetail.quantity>>>.Config>.SelectSingleBound((PXGraph) this, (object[]) null, new object[1]
    {
      (object) row.Quantity
    }));
    if (pxResult2 == null)
    {
      row.QuantityTo = new Decimal?();
    }
    else
    {
      DiscountDetail discountDetail = PXResult<DiscountDetail>.op_Implicit(pxResult2);
      row.QuantityTo = discountDetail.Quantity;
    }
  }

  protected virtual void DiscountSequence_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    if (e.Row == null)
      return;
    ARDiscount discount = PXResultset<ARDiscount>.op_Implicit(PXSelectBase<ARDiscount, PXSelect<ARDiscount, Where<ARDiscount.discountID, Equal<Required<ARDiscount.discountID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) ((DiscountSequence) e.Row).DiscountID
    }));
    this.SetControlsState(sender, e.Row as DiscountSequence, discount);
    this.SetGridColumnsState(discount);
    if (discount == null || !(discount.Type != "G"))
      return;
    Dictionary<string, string> valueLabelDic = new DiscountOption.ListAttribute().ValueLabelDic;
    valueLabelDic.Remove("F");
    PXStringListAttribute.SetList<DiscountSequence.discountedFor>(sender, e.Row, valueLabelDic.Keys.ToArray<string>(), valueLabelDic.Values.ToArray<string>());
  }

  protected virtual void DiscountSequence_RowUpdating(PXCache sender, PXRowUpdatingEventArgs e)
  {
    if (!(e.NewRow is DiscountSequence newRow))
      return;
    bool? nullable = newRow.IsActive;
    if (!nullable.GetValueOrDefault() || !(newRow.DiscountedFor == "F") || newRow.FreeItemID.HasValue)
      return;
    newRow.IsActive = new bool?(false);
    nullable = newRow.IsPromotion;
    if (nullable.GetValueOrDefault())
    {
      ((PXSelectBase) this.CurrentSequence).Cache.RaiseExceptionHandling<DiscountSequence.freeItemID>(e.NewRow, (object) newRow.FreeItemID, (Exception) new PXSetPropertyException("Free Item may not be empty. Please select Free Item before activating discount.", (PXErrorLevel) 2));
      ((PXSelectBase) this.CurrentSequence).Cache.RaiseExceptionHandling<DiscountSequence.isActive>(e.NewRow, (object) newRow.IsActive, (Exception) new PXSetPropertyException("Free Item may not be empty. Please select Free Item before activating discount.", (PXErrorLevel) 2));
    }
    else
    {
      ((PXSelectBase) this.CurrentSequence).Cache.RaiseExceptionHandling<DiscountSequence.freeItemID>(e.NewRow, (object) newRow.FreeItemID, (Exception) new PXSetPropertyException("Free Item may not be empty. Please select Pending Free Item and update discount before activating it.", (PXErrorLevel) 2));
      ((PXSelectBase) this.CurrentSequence).Cache.RaiseExceptionHandling<DiscountSequence.isActive>(e.NewRow, (object) newRow.IsActive, (Exception) new PXSetPropertyException("Free Item may not be empty. Please select Pending Free Item and update discount before activating it.", (PXErrorLevel) 2));
    }
  }

  protected virtual void DiscountSequence_RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    bool? nullable1;
    if (e.Row is DiscountSequence row1)
    {
      nullable1 = row1.DiscountSequenceID != null ? row1.IsPromotion : throw new PXRowPersistingException(typeof (DiscountSequence.discountSequenceID).Name, (object) null, "'{0}' cannot be empty.");
      DateTime? nullable2;
      if (nullable1.GetValueOrDefault())
      {
        nullable2 = row1.EndDate;
        if (!nullable2.HasValue && sender.RaiseExceptionHandling<DiscountSequence.endDate>(e.Row, (object) null, (Exception) new PXSetPropertyException("'{0}' cannot be empty.")))
          throw new PXRowPersistingException(typeof (DiscountSequence.endDate).Name, (object) null, "'{0}' cannot be empty.");
      }
      nullable1 = row1.IsPromotion;
      if (nullable1.GetValueOrDefault())
      {
        nullable2 = row1.EndDate;
        if (nullable2.HasValue)
        {
          nullable2 = row1.StartDate;
          if (nullable2.HasValue)
          {
            nullable2 = row1.EndDate;
            DateTime? startDate = row1.StartDate;
            if ((nullable2.HasValue & startDate.HasValue ? (nullable2.GetValueOrDefault() < startDate.GetValueOrDefault() ? 1 : 0) : 0) != 0 && sender.RaiseExceptionHandling<DiscountSequence.endDate>(e.Row, (object) row1.EndDate, (Exception) new PXSetPropertyException("The Expiration Date should not be earlier than the Effective Date.")))
              throw new PXRowPersistingException(typeof (DiscountSequence.endDate).Name, (object) row1.EndDate, "The Expiration Date should not be earlier than the Effective Date.");
          }
        }
      }
      if (row1.DiscountedFor == "F")
      {
        nullable1 = row1.IsActive;
        if (nullable1.GetValueOrDefault() && !row1.FreeItemID.HasValue)
        {
          row1.IsActive = new bool?(false);
          nullable1 = row1.IsPromotion;
          if (nullable1.GetValueOrDefault())
            ((PXSelectBase) this.CurrentSequence).Cache.RaiseExceptionHandling<DiscountSequence.freeItemID>(e.Row, (object) row1.FreeItemID, (Exception) new PXSetPropertyException("Free Item may not be empty. Please select Free Item before activating discount.", (PXErrorLevel) 4));
          else
            ((PXSelectBase) this.CurrentSequence).Cache.RaiseExceptionHandling<DiscountSequence.freeItemID>(e.Row, (object) row1.FreeItemID, (Exception) new PXSetPropertyException("Free Item may not be empty. Please select Pending Free Item and update discount before activating it.", (PXErrorLevel) 4));
        }
      }
    }
    if ((e.Operation & 3) != 2)
      return;
    ARDiscount current = (ARDiscount) ((PXSelectBase<ARDiscountSequenceMaint.ARDiscountEx>) this.Discount).Current;
    if (current == null)
      return;
    PXCache pxCache = sender;
    object row2 = e.Row;
    nullable1 = current.IsAutoNumber;
    int num = nullable1.GetValueOrDefault() ? 1 : 0;
    PXDBDefaultAttribute.SetDefaultForInsert<DiscountSequence.discountSequenceID>(pxCache, row2, num != 0);
  }

  protected virtual void DiscountSequence_DiscountSequenceID_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    if (e.Row == null)
      return;
    ARDiscount arDiscount = PXResultset<ARDiscount>.op_Implicit(PXSelectBase<ARDiscount, PXSelect<ARDiscount, Where<ARDiscount.discountID, Equal<Required<DiscountSequence.discountID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) ((DiscountSequence) e.Row).DiscountID
    }));
    if (arDiscount == null || !arDiscount.IsAutoNumber.GetValueOrDefault())
      return;
    e.NewValue = (object) PXMessages.LocalizeNoPrefix(" <NEW>");
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void DiscountDetail_RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    if (!(e.Row is DiscountDetail row))
      return;
    if (!sender.ObjectsEqual<DiscountDetail.isActive>(e.Row, e.OldRow))
    {
      PXResult<DiscountSequenceDetail> pxResult = PXResultset<DiscountSequenceDetail>.op_Implicit(PXSelectBase<DiscountSequenceDetail, PXSelect<DiscountSequenceDetail, Where<DiscountSequenceDetail.discountID, Equal<Current<DiscountSequence.discountID>>, And<DiscountSequenceDetail.discountSequenceID, Equal<Current<DiscountSequence.discountSequenceID>>, And<DiscountSequenceDetail.discountDetailsID, NotEqual<Required<DiscountSequenceDetail.discountDetailsID>>, And<DiscountSequenceDetail.lineNbr, Equal<Required<DiscountSequenceDetail.lineNbr>>, And<DiscountSequenceDetail.isLast, Equal<True>>>>>>, OrderBy<Asc<DiscountSequenceDetail.quantity>>>.Config>.SelectSingleBound((PXGraph) this, (object[]) null, new object[2]
      {
        (object) row.DiscountDetailsID,
        (object) row.LineNbr
      }));
      if (pxResult != null)
      {
        PXResult<DiscountSequenceDetail>.op_Implicit(pxResult).IsActive = row.IsActive;
        ((PXSelectBase<DiscountSequenceDetail>) this.SequenceDetails).Update(PXResult<DiscountSequenceDetail>.op_Implicit(pxResult));
      }
      if (((PXSelectBase<DiscountSequence>) this.Sequence).Current.BreakBy == "Q")
      {
        DiscountDetail detailLineQuantity1 = this.GetNextDiscountDetailLineQuantity(row);
        DiscountDetail detailLineQuantity2 = this.GetPrevDiscountDetailLineQuantity(row);
        if (detailLineQuantity1 == null)
        {
          if (detailLineQuantity2 != null)
          {
            if (row.IsActive.GetValueOrDefault())
            {
              row.QuantityTo = new Decimal?();
              detailLineQuantity2.QuantityTo = row.Quantity;
              ((PXSelectBase<DiscountDetail>) this.Details).Update(detailLineQuantity2);
            }
            else
            {
              detailLineQuantity2.QuantityTo = new Decimal?();
              ((PXSelectBase<DiscountDetail>) this.Details).Update(detailLineQuantity2);
            }
          }
          row.QuantityTo = new Decimal?();
        }
        else if (detailLineQuantity2 != null)
        {
          if (row.IsActive.GetValueOrDefault())
          {
            detailLineQuantity2.QuantityTo = row.Quantity;
            row.QuantityTo = detailLineQuantity1.Quantity;
            ((PXSelectBase<DiscountDetail>) this.Details).Update(detailLineQuantity2);
          }
          else
          {
            detailLineQuantity2.QuantityTo = detailLineQuantity1.Quantity;
            ((PXSelectBase<DiscountDetail>) this.Details).Update(detailLineQuantity2);
          }
        }
        else
          row.QuantityTo = detailLineQuantity1.Quantity;
      }
      else
      {
        DiscountDetail detailLineAmount1 = this.GetNextDiscountDetailLineAmount(row);
        DiscountDetail detailLineAmount2 = this.GetPrevDiscountDetailLineAmount(row);
        if (detailLineAmount1 == null)
        {
          if (detailLineAmount2 != null)
          {
            if (row.IsActive.GetValueOrDefault())
            {
              row.AmountTo = new Decimal?();
              detailLineAmount2.AmountTo = row.Amount;
              ((PXSelectBase<DiscountDetail>) this.Details).Update(detailLineAmount2);
            }
            else
            {
              detailLineAmount2.AmountTo = new Decimal?();
              ((PXSelectBase<DiscountDetail>) this.Details).Update(detailLineAmount2);
            }
          }
          row.AmountTo = new Decimal?();
        }
        else if (detailLineAmount2 != null)
        {
          if (row.IsActive.GetValueOrDefault())
          {
            detailLineAmount2.AmountTo = row.Amount;
            row.AmountTo = detailLineAmount1.Amount;
            ((PXSelectBase<DiscountDetail>) this.Details).Update(detailLineAmount2);
          }
          else
          {
            detailLineAmount2.AmountTo = detailLineAmount1.Amount;
            ((PXSelectBase<DiscountDetail>) this.Details).Update(detailLineAmount2);
          }
        }
        else
          row.AmountTo = detailLineAmount1.Amount;
      }
    }
    if (sender.ObjectsEqual<DiscountDetail.pendingQuantity, DiscountDetail.pendingAmount, DiscountDetail.pendingDiscountPercent, DiscountDetail.pendingFreeItemQty>(e.Row, e.OldRow))
      return;
    DateTime? startDate = row.StartDate;
    if (startDate.HasValue)
      return;
    if (((PXSelectBase<DiscountSequence>) this.Sequence).Current != null)
    {
      startDate = ((PXSelectBase<DiscountSequence>) this.Sequence).Current.StartDate;
      if (startDate.HasValue)
      {
        row.StartDate = ((PXSelectBase<DiscountSequence>) this.Sequence).Current.StartDate;
        return;
      }
    }
    row.StartDate = ((PXGraph) this).Accessinfo.BusinessDate;
  }

  protected virtual void DiscountDetail_RowDeleting(PXCache sender, PXRowDeletingEventArgs e)
  {
    if (!(e.Row is DiscountDetail row))
      return;
    PXResult<DiscountSequenceDetail> pxResult = PXResultset<DiscountSequenceDetail>.op_Implicit(PXSelectBase<DiscountSequenceDetail, PXSelect<DiscountSequenceDetail, Where<DiscountSequenceDetail.discountID, Equal<Current<DiscountSequence.discountID>>, And<DiscountSequenceDetail.discountSequenceID, Equal<Current<DiscountSequence.discountSequenceID>>, And<DiscountSequenceDetail.discountDetailsID, NotEqual<Required<DiscountSequenceDetail.discountDetailsID>>, And<DiscountSequenceDetail.lineNbr, Equal<Required<DiscountSequenceDetail.lineNbr>>, And<DiscountSequenceDetail.isLast, Equal<True>>>>>>, OrderBy<Asc<DiscountSequenceDetail.quantity>>>.Config>.SelectSingleBound((PXGraph) this, (object[]) null, new object[2]
    {
      (object) row.DiscountDetailsID,
      (object) row.LineNbr
    }));
    if (pxResult == null)
      return;
    ((PXSelectBase<DiscountSequenceDetail>) this.SequenceDetails).Delete(PXResult<DiscountSequenceDetail>.op_Implicit(pxResult));
  }

  protected virtual void DiscountDetail_RowDeleted(PXCache sender, PXRowDeletedEventArgs e)
  {
    if (!(e.Row is DiscountDetail row))
      return;
    if (((PXSelectBase<DiscountSequence>) this.Sequence).Current.BreakBy == "Q")
    {
      DiscountDetail detailLineQuantity1 = this.GetNextDiscountDetailLineQuantity(row);
      DiscountDetail detailLineQuantity2 = this.GetPrevDiscountDetailLineQuantity(row);
      if (detailLineQuantity2 == null)
        return;
      if (detailLineQuantity1 == null)
      {
        detailLineQuantity2.QuantityTo = new Decimal?();
        ((PXSelectBase<DiscountDetail>) this.Details).Update(detailLineQuantity2);
      }
      else
      {
        detailLineQuantity2.QuantityTo = detailLineQuantity1.Quantity;
        ((PXSelectBase<DiscountDetail>) this.Details).Update(detailLineQuantity2);
      }
    }
    else
    {
      DiscountDetail detailLineAmount1 = this.GetNextDiscountDetailLineAmount(row);
      DiscountDetail detailLineAmount2 = this.GetPrevDiscountDetailLineAmount(row);
      if (detailLineAmount2 == null)
        return;
      if (detailLineAmount1 == null)
      {
        detailLineAmount2.AmountTo = new Decimal?();
        ((PXSelectBase<DiscountDetail>) this.Details).Update(detailLineAmount2);
      }
      else
      {
        detailLineAmount2.AmountTo = detailLineAmount1.Amount;
        ((PXSelectBase<DiscountDetail>) this.Details).Update(detailLineAmount2);
      }
    }
  }

  public virtual DiscountDetail GetNextDiscountDetailLineQuantity(DiscountDetail currentLine)
  {
    return PXResultset<DiscountDetail>.op_Implicit(PXSelectBase<DiscountDetail, PXSelect<DiscountDetail, Where<DiscountDetail.discountID, Equal<Current<DiscountSequence.discountID>>, And<DiscountDetail.discountSequenceID, Equal<Current<DiscountSequence.discountSequenceID>>, And<DiscountDetail.quantity, Greater<Required<DiscountDetail.quantity>>, And<DiscountDetail.isActive, Equal<True>>>>>, OrderBy<Asc<DiscountDetail.quantity>>>.Config>.SelectSingleBound((PXGraph) this, (object[]) null, new object[1]
    {
      (object) currentLine.Quantity
    }));
  }

  public virtual DiscountDetail GetPrevDiscountDetailLineQuantity(DiscountDetail currentLine)
  {
    return PXResultset<DiscountDetail>.op_Implicit(PXSelectBase<DiscountDetail, PXSelect<DiscountDetail, Where<DiscountDetail.discountID, Equal<Current<DiscountSequence.discountID>>, And<DiscountDetail.discountSequenceID, Equal<Current<DiscountSequence.discountSequenceID>>, And<DiscountDetail.quantity, Less<Required<DiscountDetail.quantity>>, And<DiscountDetail.isActive, Equal<True>>>>>, OrderBy<Desc<DiscountDetail.quantity>>>.Config>.SelectSingleBound((PXGraph) this, (object[]) null, new object[1]
    {
      (object) currentLine.Quantity
    }));
  }

  public virtual DiscountDetail GetNextDiscountDetailLineAmount(DiscountDetail currentLine)
  {
    return PXResultset<DiscountDetail>.op_Implicit(PXSelectBase<DiscountDetail, PXSelect<DiscountDetail, Where<DiscountDetail.discountID, Equal<Current<DiscountSequence.discountID>>, And<DiscountDetail.discountSequenceID, Equal<Current<DiscountSequence.discountSequenceID>>, And<DiscountDetail.amount, Greater<Required<DiscountDetail.amount>>, And<DiscountDetail.isActive, Equal<True>>>>>, OrderBy<Asc<DiscountDetail.amount>>>.Config>.SelectSingleBound((PXGraph) this, (object[]) null, new object[1]
    {
      (object) currentLine.Amount
    }));
  }

  public virtual DiscountDetail GetPrevDiscountDetailLineAmount(DiscountDetail currentLine)
  {
    return PXResultset<DiscountDetail>.op_Implicit(PXSelectBase<DiscountDetail, PXSelect<DiscountDetail, Where<DiscountDetail.discountID, Equal<Current<DiscountSequence.discountID>>, And<DiscountDetail.discountSequenceID, Equal<Current<DiscountSequence.discountSequenceID>>, And<DiscountDetail.amount, Less<Required<DiscountDetail.amount>>, And<DiscountDetail.isActive, Equal<True>>>>>, OrderBy<Desc<DiscountDetail.amount>>>.Config>.SelectSingleBound((PXGraph) this, (object[]) null, new object[1]
    {
      (object) currentLine.Amount
    }));
  }

  private void SetControlsState(PXCache sender, DiscountSequence row, ARDiscount discount)
  {
    if (row == null)
      return;
    PXAction<DiscountSequence> updateDiscounts = this.updateDiscounts;
    bool? nullable;
    int num1;
    if (sender.GetStatus((object) row) == 2 && discount != null)
    {
      nullable = discount.IsAutoNumber;
      if (nullable.GetValueOrDefault())
      {
        num1 = 0;
        goto label_5;
      }
    }
    nullable = row.IsPromotion;
    num1 = !nullable.GetValueOrDefault() ? 1 : 0;
label_5:
    ((PXAction) updateDiscounts).SetEnabled(num1 != 0);
    PXCache pxCache1 = sender;
    DiscountSequence discountSequence1 = row;
    nullable = row.IsPromotion;
    int num2 = nullable.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetEnabled<DiscountSequence.endDate>(pxCache1, (object) discountSequence1, num2 != 0);
    PXCache pxCache2 = sender;
    nullable = row.IsPromotion;
    int num3 = nullable.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetRequired<DiscountSequence.endDate>(pxCache2, num3 != 0);
    PXCache pxCache3 = sender;
    nullable = row.IsPromotion;
    int num4 = nullable.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetRequired<DiscountSequence.startDate>(pxCache3, num4 != 0);
    PXUIFieldAttribute.SetVisible<DiscountSequence.startDate>(sender, (object) row, true);
    PXCache pxCache4 = sender;
    DiscountSequence discountSequence2 = row;
    nullable = row.IsPromotion;
    int num5 = nullable.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetVisible<DiscountSequence.startDate>(pxCache4, (object) discountSequence2, num5 != 0);
    PXCache pxCache5 = sender;
    DiscountSequence discountSequence3 = row;
    nullable = row.IsPromotion;
    int num6 = nullable.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetVisible<DiscountSequence.endDate>(pxCache5, (object) discountSequence3, num6 != 0);
    PXCache pxCache6 = sender;
    DiscountSequence discountSequence4 = row;
    nullable = row.IsPromotion;
    bool flag = false;
    int num7 = nullable.GetValueOrDefault() == flag & nullable.HasValue ? 1 : 0;
    PXUIFieldAttribute.SetVisible<DiscountSequence.updateDate>(pxCache6, (object) discountSequence4, num7 != 0);
    PXUIFieldAttribute.SetEnabled<DiscountSequence.breakBy>(sender, (object) row, discount != null && (discount.Type == "G" || discount.Type == "L"));
    PXUIFieldAttribute.SetEnabled<DiscountSequence.pendingFreeItemID>(sender, (object) row, row.DiscountedFor == "F" && this.IsFreeItemApplicable(row.DiscountID));
    PXCache pxCache7 = sender;
    DiscountSequence discountSequence5 = row;
    nullable = row.IsPromotion;
    int num8 = nullable.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetEnabled<DiscountSequence.freeItemID>(pxCache7, (object) discountSequence5, num8 != 0);
    PXUIFieldAttribute.SetEnabled<DiscountSequence.prorate>(sender, (object) row, row.DiscountedFor == "F" || row.DiscountedFor == "A");
    PXCache pxCache8 = sender;
    DiscountSequence discountSequence6 = row;
    nullable = row.IsPromotion;
    int num9 = !nullable.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetVisible<DiscountSequence.pendingFreeItemID>(pxCache8, (object) discountSequence6, num9 != 0);
    PXCache pxCache9 = sender;
    DiscountSequence discountSequence7 = row;
    nullable = row.IsPromotion;
    int num10 = !nullable.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetVisible<DiscountSequence.lastFreeItemID>(pxCache9, (object) discountSequence7, num10 != 0);
  }

  private void SetGridColumnsState(ARDiscount discount)
  {
    if (((PXSelectBase<DiscountSequence>) this.Sequence).Current == null)
      return;
    bool valueOrDefault = ((PXSelectBase<DiscountSequence>) this.Sequence).Current.IsPromotion.GetValueOrDefault();
    bool flag1 = ((PXSelectBase<DiscountSequence>) this.Sequence).Current.DiscountedFor == "A";
    bool flag2 = ((PXSelectBase<DiscountSequence>) this.Sequence).Current.DiscountedFor == "P";
    bool flag3 = ((PXSelectBase<DiscountSequence>) this.Sequence).Current.DiscountedFor == "F";
    bool flag4 = ((PXSelectBase<DiscountSequence>) this.Sequence).Current.BreakBy == "A";
    bool flag5 = ((PXSelectBase<DiscountSequence>) this.Sequence).Current.BreakBy == "Q";
    PXUIFieldAttribute.SetVisible<DiscountItem.amount>(((PXSelectBase) this.Items).Cache, (object) null, flag4);
    PXUIFieldAttribute.SetVisible<DiscountItem.quantity>(((PXSelectBase) this.Items).Cache, (object) null, flag5);
    PXUIFieldAttribute.SetVisible<DiscountItem.uOM>(((PXSelectBase) this.Items).Cache, (object) null, flag5);
    PXUIFieldAttribute.SetVisible(((PXSelectBase) this.Details).Cache, (string) null, false);
    PXUIFieldAttribute.SetVisible<DiscountDetail.discount>(((PXSelectBase) this.Details).Cache, (object) null, flag1);
    PXUIFieldAttribute.SetVisible<DiscountDetail.lastDiscount>(((PXSelectBase) this.Details).Cache, (object) null, flag1 && !valueOrDefault);
    PXUIFieldAttribute.SetVisible<DiscountDetail.pendingDiscount>(((PXSelectBase) this.Details).Cache, (object) null, flag1 && !valueOrDefault);
    PXUIFieldAttribute.SetVisible<DiscountDetail.discountPercent>(((PXSelectBase) this.Details).Cache, (object) null, flag2);
    PXUIFieldAttribute.SetVisible<DiscountDetail.lastDiscountPercent>(((PXSelectBase) this.Details).Cache, (object) null, flag2 && !valueOrDefault);
    PXUIFieldAttribute.SetVisible<DiscountDetail.pendingDiscountPercent>(((PXSelectBase) this.Details).Cache, (object) null, flag2 && !valueOrDefault);
    PXUIFieldAttribute.SetVisible<DiscountDetail.freeItemQty>(((PXSelectBase) this.Details).Cache, (object) null, flag3);
    PXUIFieldAttribute.SetVisible<DiscountDetail.lastFreeItemQty>(((PXSelectBase) this.Details).Cache, (object) null, flag3 && !valueOrDefault);
    PXUIFieldAttribute.SetVisible<DiscountDetail.pendingFreeItemQty>(((PXSelectBase) this.Details).Cache, (object) null, flag3 && !valueOrDefault);
    PXUIFieldAttribute.SetVisible<DiscountDetail.amount>(((PXSelectBase) this.Details).Cache, (object) null, flag4);
    PXUIFieldAttribute.SetVisible<DiscountDetail.lastAmount>(((PXSelectBase) this.Details).Cache, (object) null, flag4 && !valueOrDefault);
    PXUIFieldAttribute.SetVisible<DiscountDetail.pendingAmount>(((PXSelectBase) this.Details).Cache, (object) null, flag4 && !valueOrDefault);
    PXUIFieldAttribute.SetVisible<DiscountDetail.quantity>(((PXSelectBase) this.Details).Cache, (object) null, flag5);
    PXUIFieldAttribute.SetVisible<DiscountDetail.lastQuantity>(((PXSelectBase) this.Details).Cache, (object) null, flag5 && !valueOrDefault);
    PXUIFieldAttribute.SetVisible<DiscountDetail.pendingQuantity>(((PXSelectBase) this.Details).Cache, (object) null, flag5 && !valueOrDefault);
    PXUIFieldAttribute.SetVisible<DiscountDetail.startDate>(((PXSelectBase) this.Details).Cache, (object) null, !valueOrDefault);
    PXUIFieldAttribute.SetVisible<DiscountDetail.lastDate>(((PXSelectBase) this.Details).Cache, (object) null, !valueOrDefault);
    PXUIFieldAttribute.SetEnabled(((PXSelectBase) this.Details).Cache, (string) null, false);
    PXUIFieldAttribute.SetEnabled<DiscountDetail.isActive>(((PXSelectBase) this.Details).Cache, (object) null, true);
    PXUIFieldAttribute.SetEnabled<DiscountDetail.discount>(((PXSelectBase) this.Details).Cache, (object) null, flag1 & valueOrDefault);
    PXUIFieldAttribute.SetEnabled<DiscountDetail.pendingDiscount>(((PXSelectBase) this.Details).Cache, (object) null, flag1 && !valueOrDefault);
    PXUIFieldAttribute.SetEnabled<DiscountDetail.discountPercent>(((PXSelectBase) this.Details).Cache, (object) null, flag2 & valueOrDefault);
    PXUIFieldAttribute.SetEnabled<DiscountDetail.pendingDiscountPercent>(((PXSelectBase) this.Details).Cache, (object) null, flag2 && !valueOrDefault);
    PXUIFieldAttribute.SetEnabled<DiscountDetail.freeItemQty>(((PXSelectBase) this.Details).Cache, (object) null, flag3 & valueOrDefault);
    PXUIFieldAttribute.SetEnabled<DiscountDetail.pendingFreeItemQty>(((PXSelectBase) this.Details).Cache, (object) null, flag3 && !valueOrDefault);
    PXUIFieldAttribute.SetEnabled<DiscountDetail.amount>(((PXSelectBase) this.Details).Cache, (object) null, flag4 & valueOrDefault);
    PXUIFieldAttribute.SetEnabled<DiscountDetail.pendingAmount>(((PXSelectBase) this.Details).Cache, (object) null, flag4 && !valueOrDefault);
    PXUIFieldAttribute.SetEnabled<DiscountDetail.quantity>(((PXSelectBase) this.Details).Cache, (object) null, flag5 & valueOrDefault);
    PXUIFieldAttribute.SetEnabled<DiscountDetail.pendingQuantity>(((PXSelectBase) this.Details).Cache, (object) null, flag5 && !valueOrDefault);
    PXUIFieldAttribute.SetEnabled<DiscountDetail.startDate>(((PXSelectBase) this.Details).Cache, (object) null, !valueOrDefault);
  }

  private bool IsFreeItemApplicable(string discountID)
  {
    ARDiscount arDiscount = PXResultset<ARDiscount>.op_Implicit(PXSelectBase<ARDiscount, PXSelect<ARDiscount, Where<ARDiscount.discountID, Equal<Required<ARDiscount.discountID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) discountID
    }));
    return arDiscount == null || arDiscount.Type == "G";
  }

  internal static string IncNumber(string str, int count)
  {
    bool flag = true;
    int num = count;
    StringBuilder stringBuilder = new StringBuilder();
    for (int length = str.Length; length > 0; --length)
    {
      string input = str.Substring(length - 1, 1);
      if (Regex.IsMatch(input, "[^0-9]"))
        flag = false;
      if (flag && Regex.IsMatch(input, "[0-9]"))
      {
        int int16_1 = (int) Convert.ToInt16(input);
        string str1 = Convert.ToString(num);
        int int16_2 = (int) Convert.ToInt16(str1.Substring(str1.Length - 1, 1));
        stringBuilder.Append((int16_1 + int16_2) % 10);
        num = (num - int16_2 + (int16_1 + int16_2 - (int16_1 + int16_2) % 10)) / 10;
        if (num == 0)
          flag = false;
      }
      else
        stringBuilder.Append(input);
    }
    if (num != 0)
      throw new ArithmeticException("");
    char[] charArray = stringBuilder.ToString().ToCharArray();
    Array.Reverse((Array) charArray);
    return new string(charArray);
  }

  public virtual void Persist()
  {
    ARDiscountSequenceMaint.ARDiscountEx arDiscountEx = PXResultset<ARDiscountSequenceMaint.ARDiscountEx>.op_Implicit(PXSelectBase<ARDiscountSequenceMaint.ARDiscountEx, PXSelect<ARDiscountSequenceMaint.ARDiscountEx, Where<ARDiscountSequenceMaint.ARDiscountEx.discountID, Equal<Current<DiscountSequence.discountID>>>>.Config>.Select((PXGraph) this, Array.Empty<object>()));
    if (arDiscountEx != null && ((PXSelectBase<DiscountSequence>) this.Sequence).Current != null && arDiscountEx.IsAutoNumber.GetValueOrDefault() && ((PXSelectBase) this.Sequence).Cache.GetStatus((object) ((PXSelectBase<DiscountSequence>) this.Sequence).Current) == 2)
    {
      string str = string.IsNullOrEmpty(arDiscountEx.LastNumber) ? "0000000000" : arDiscountEx.LastNumber;
      if (!char.IsDigit(str[str.Length - 1]))
        str = $"{str.Substring(1, 6)}0000";
      arDiscountEx.LastNumber = ARDiscountSequenceMaint.IncNumber(str, 1);
      ((PXSelectBase<ARDiscountSequenceMaint.ARDiscountEx>) this.Discount).Update(arDiscountEx);
    }
    ((PXGraph) this).Persist();
  }

  public ARDiscountSequenceMaint.DiscountValidation DiscountValidationExt
  {
    get => ((PXGraph) this).GetExtension<ARDiscountSequenceMaint.DiscountValidation>();
  }

  [Serializable]
  public class ARDiscountEx : ARDiscount
  {
    [PXBool]
    [PXUIField]
    public virtual bool? ShowListOfItems
    {
      [PXDependsOnFields(new Type[] {typeof (ARDiscount.applicableTo)})] get
      {
        return new bool?(this.ApplicableTo == "IN" || this.ApplicableTo == "CI" || this.ApplicableTo == "PI" || this.ApplicableTo == "WI");
      }
      set
      {
      }
    }

    [PXBool]
    [PXUIField]
    public virtual bool? ShowCustomers
    {
      [PXDependsOnFields(new Type[] {typeof (ARDiscount.applicableTo)})] get
      {
        return new bool?(this.ApplicableTo == "CU" || this.ApplicableTo == "CI" || this.ApplicableTo == "CP" || this.ApplicableTo == "WC" || this.ApplicableTo == "CB");
      }
      set
      {
      }
    }

    [PXBool]
    [PXUIField]
    public virtual bool? ShowCustomerPriceClass
    {
      [PXDependsOnFields(new Type[] {typeof (ARDiscount.applicableTo)})] get
      {
        return new bool?(this.ApplicableTo == "CE" || this.ApplicableTo == "PI" || this.ApplicableTo == "PP" || this.ApplicableTo == "WE" || this.ApplicableTo == "PB");
      }
      set
      {
      }
    }

    [PXBool]
    [PXUIField]
    public virtual bool? ShowInventoryPriceClass
    {
      [PXDependsOnFields(new Type[] {typeof (ARDiscount.applicableTo)})] get
      {
        return new bool?(this.ApplicableTo == "IE" || this.ApplicableTo == "CP" || this.ApplicableTo == "PP" || this.ApplicableTo == "WP");
      }
      set
      {
      }
    }

    [PXBool]
    [PXUIField]
    public virtual bool? ShowBranches
    {
      [PXDependsOnFields(new Type[] {typeof (ARDiscount.applicableTo)})] get
      {
        return new bool?(this.ApplicableTo == "BR" || this.ApplicableTo == "CB" || this.ApplicableTo == "PB");
      }
      set
      {
      }
    }

    [PXBool]
    [PXUIField]
    public virtual bool? ShowSites
    {
      [PXDependsOnFields(new Type[] {typeof (ARDiscount.applicableTo)})] get
      {
        return new bool?(this.ApplicableTo == "WH" || this.ApplicableTo == "WC" || this.ApplicableTo == "WE" || this.ApplicableTo == "WI" || this.ApplicableTo == "WP");
      }
      set
      {
      }
    }

    public new abstract class discountID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ARDiscountSequenceMaint.ARDiscountEx.discountID>
    {
    }

    public abstract class showListOfItems : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      ARDiscountSequenceMaint.ARDiscountEx.showListOfItems>
    {
    }

    public abstract class showCustomers : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      ARDiscountSequenceMaint.ARDiscountEx.showCustomers>
    {
    }

    public abstract class showCustomerPriceClass : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      ARDiscountSequenceMaint.ARDiscountEx.showCustomerPriceClass>
    {
    }

    public abstract class showInventoryPriceClass : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      ARDiscountSequenceMaint.ARDiscountEx.showInventoryPriceClass>
    {
    }

    public abstract class showBranches : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      ARDiscountSequenceMaint.ARDiscountEx.showBranches>
    {
    }

    public abstract class showSites : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      ARDiscountSequenceMaint.ARDiscountEx.showSites>
    {
    }
  }

  [Serializable]
  public class UpdateSettingsFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    protected DateTime? _FilterDate;

    [PXDefault(typeof (AccessInfo.businessDate))]
    [PXDate]
    [PXUIField(DisplayName = "Filter Date", Required = true)]
    public virtual DateTime? FilterDate
    {
      get => this._FilterDate;
      set => this._FilterDate = value;
    }

    public abstract class filterDate : 
      BqlType<
      #nullable enable
      IBqlDateTime, DateTime>.Field<
      #nullable disable
      ARDiscountSequenceMaint.UpdateSettingsFilter.filterDate>
    {
    }
  }

  public class DiscountValidation : PXGraphExtension<ARDiscountSequenceMaint>
  {
    public static bool IsActive() => true;

    public virtual IReadOnlyDictionary<string, Func<bool>> DuplicatePredicatesMap { get; }

    public DiscountValidation()
    {
      this.DuplicatePredicatesMap = (IReadOnlyDictionary<string, Func<bool>>) new Dictionary<string, Func<bool>>()
      {
        ["UN"] = new Func<bool>(this.HasUnconditionalDuplicates),
        ["CU"] = new Func<bool>(this.HasCustomerDuplicates),
        ["IN"] = new Func<bool>(this.HasInventoryDuplicates),
        ["CE"] = new Func<bool>(this.HasCustomerPriceClassDuplicates),
        ["IE"] = new Func<bool>(this.HasInventoryPriceClassDuplicates),
        ["CI"] = new Func<bool>(this.HasCustomerAndInventoryComboDuplicates),
        ["CP"] = new Func<bool>(this.HasCustomerAndInventoryPriceClassComboDuplicates),
        ["PI"] = new Func<bool>(this.HasCustomerPriceClassAndInventoryComboDuplicates),
        ["PP"] = new Func<bool>(this.HasCustomerPriceClassAndInventoryPriceClassComboDuplicates),
        ["BR"] = new Func<bool>(this.HasBranchDuplicates),
        ["WH"] = new Func<bool>(this.HasWarehouseDuplicates),
        ["WC"] = new Func<bool>(this.HasWarehouseAndCustomerComboDuplicates),
        ["WE"] = new Func<bool>(this.HasWarehouseAndCustomerPriceClassComboDuplicates),
        ["WI"] = new Func<bool>(this.HasWarehouseAndInventoryComboDuplicates),
        ["WP"] = new Func<bool>(this.HasWarehouseAndInventoryPriceClassComboDuplicates)
      };
    }

    public virtual void Initialize()
    {
      ((PXGraphExtension) this).Initialize();
      ((PXGraph) this.Base).OnBeforeCommit += new Action<PXGraph>(this.OnBeforeCommit);
    }

    private void OnBeforeCommit(PXGraph g)
    {
      if (this.HasDuplicates())
        throw new PXException("One or more validations failed for the given discount sequence. Please fix the errors and try again.");
    }

    public virtual bool HasDuplicates()
    {
      return ((PXSelectBase<ARDiscountSequenceMaint.ARDiscountEx>) this.Base.Discount).Current != null && ((PXSelectBase<DiscountSequence>) this.Base.Sequence).Current != null && ((PXSelectBase<DiscountSequence>) this.Base.Sequence).Current.IsActive.GetValueOrDefault() && this.DuplicatePredicatesMap.ContainsKey(((PXSelectBase<ARDiscountSequenceMaint.ARDiscountEx>) this.Base.Discount).Current.ApplicableTo) && this.DuplicatePredicatesMap[((PXSelectBase<ARDiscountSequenceMaint.ARDiscountEx>) this.Base.Discount).Current.ApplicableTo]();
    }

    private bool HasUnconditionalDuplicates()
    {
      bool flag = false;
      foreach (PXResult<DiscountSequence> pxResult in this.GetDuplicatesSelectCommand().SelectWindowed(0, 1, Array.Empty<object>()))
        flag |= this.Invalidate<DiscountSequence, DiscountSequence.discountSequenceID>((PXSelectBase<DiscountSequence>) this.Base.Sequence, PXResult<DiscountSequence>.op_Implicit(pxResult), "Unconditional discounts cannot have active overlapping sequences.");
      return flag;
    }

    public virtual bool HasCustomerDuplicates()
    {
      bool flag = false;
      foreach (PXResult<DiscountSequence, DiscountCustomer> pxResult in this.GetDuplicatesSelectCommand().With<PXSelectBase<DiscountSequence>, PXSelectBase<DiscountSequence>>(new Func<PXSelectBase<DiscountSequence>, PXSelectBase<DiscountSequence>>(this.ApplyCustomer)).Select(Array.Empty<object>()))
        flag |= this.Invalidate<DiscountCustomer, DiscountCustomer.customerID>((PXSelectBase<DiscountCustomer>) this.Base.Customers, PXResult<DiscountSequence, DiscountCustomer>.op_Implicit(pxResult), "Same Item cannot be listed more than once. Same item cannot belong to two or more active discount sequences of the same discount code");
      return flag;
    }

    public virtual bool HasInventoryDuplicates()
    {
      bool flag = false;
      foreach (PXResult<DiscountSequence, DiscountItem> pxResult in this.GetDuplicatesSelectCommand().With<PXSelectBase<DiscountSequence>, PXSelectBase<DiscountSequence>>(new Func<PXSelectBase<DiscountSequence>, PXSelectBase<DiscountSequence>>(this.ApplyInventory)).Select(Array.Empty<object>()))
        flag |= this.Invalidate<DiscountItem, DiscountItem.inventoryID>((PXSelectBase<DiscountItem>) this.Base.Items, PXResult<DiscountSequence, DiscountItem>.op_Implicit(pxResult), "Same Item cannot be listed more than once. Same item cannot belong to two or more active discount sequences of the same discount code");
      return flag;
    }

    public virtual bool HasCustomerPriceClassDuplicates()
    {
      bool flag = false;
      foreach (PXResult<DiscountSequence, DiscountCustomerPriceClass> pxResult in this.GetDuplicatesSelectCommand().With<PXSelectBase<DiscountSequence>, PXSelectBase<DiscountSequence>>(new Func<PXSelectBase<DiscountSequence>, PXSelectBase<DiscountSequence>>(this.ApplyCustomerPriceClass)).Select(Array.Empty<object>()))
        flag |= this.Invalidate<DiscountCustomerPriceClass, DiscountCustomerPriceClass.customerPriceClassID>((PXSelectBase<DiscountCustomerPriceClass>) this.Base.CustomerPriceClasses, PXResult<DiscountSequence, DiscountCustomerPriceClass>.op_Implicit(pxResult), "Same Item cannot be listed more than once. Same item cannot belong to two or more active discount sequences of the same discount code");
      return flag;
    }

    public virtual bool HasInventoryPriceClassDuplicates()
    {
      bool flag = false;
      foreach (PXResult<DiscountSequence, DiscountInventoryPriceClass> pxResult in this.GetDuplicatesSelectCommand().With<PXSelectBase<DiscountSequence>, PXSelectBase<DiscountSequence>>(new Func<PXSelectBase<DiscountSequence>, PXSelectBase<DiscountSequence>>(this.ApplyInventoryPriceClass)).Select(Array.Empty<object>()))
        flag |= this.Invalidate<DiscountInventoryPriceClass, DiscountInventoryPriceClass.inventoryPriceClassID>((PXSelectBase<DiscountInventoryPriceClass>) this.Base.InventoryPriceClasses, PXResult<DiscountSequence, DiscountInventoryPriceClass>.op_Implicit(pxResult), "Same Item cannot be listed more than once. Same item cannot belong to two or more active discount sequences of the same discount code");
      return flag;
    }

    public virtual bool HasBranchDuplicates()
    {
      bool flag = false;
      foreach (PXResult<DiscountSequence, DiscountBranch> pxResult in this.GetDuplicatesSelectCommand().With<PXSelectBase<DiscountSequence>, PXSelectBase<DiscountSequence>>(new Func<PXSelectBase<DiscountSequence>, PXSelectBase<DiscountSequence>>(this.ApplyBranch)).Select(Array.Empty<object>()))
        flag |= this.Invalidate<DiscountBranch, DiscountBranch.branchID>((PXSelectBase<DiscountBranch>) this.Base.Branches, PXResult<DiscountSequence, DiscountBranch>.op_Implicit(pxResult), "Same Branch cannot be listed more than once. Same branch cannot belong to two or more active discount sequences of the same discount code");
      return flag;
    }

    public virtual bool HasWarehouseDuplicates()
    {
      bool flag = false;
      foreach (PXResult<DiscountSequence, DiscountSite> pxResult in this.GetDuplicatesSelectCommand().With<PXSelectBase<DiscountSequence>, PXSelectBase<DiscountSequence>>(new Func<PXSelectBase<DiscountSequence>, PXSelectBase<DiscountSequence>>(this.ApplyWarehouse)).Select(Array.Empty<object>()))
        flag |= this.Invalidate<DiscountSite, DiscountSite.siteID>((PXSelectBase<DiscountSite>) this.Base.Sites, PXResult<DiscountSequence, DiscountSite>.op_Implicit(pxResult), "Same Warehouse cannot be listed more than once. Same warehouse cannot belong to two or more active discount sequences of the same discount code");
      return flag;
    }

    public virtual bool HasCustomerAndInventoryComboDuplicates()
    {
      bool flag = false;
      foreach (PXResult<DiscountSequence, DiscountCustomer, DiscountItem> pxResult in this.GetDuplicatesSelectCommand().With<PXSelectBase<DiscountSequence>, PXSelectBase<DiscountSequence>>(new Func<PXSelectBase<DiscountSequence>, PXSelectBase<DiscountSequence>>(this.ApplyCustomer)).With<PXSelectBase<DiscountSequence>, PXSelectBase<DiscountSequence>>(new Func<PXSelectBase<DiscountSequence>, PXSelectBase<DiscountSequence>>(this.ApplyInventory)).Select(Array.Empty<object>()))
      {
        flag |= this.Invalidate<DiscountCustomer, DiscountCustomer.customerID>((PXSelectBase<DiscountCustomer>) this.Base.Customers, PXResult<DiscountSequence, DiscountCustomer, DiscountItem>.op_Implicit(pxResult), "Same Item cannot be listed more than once. Same item cannot belong to two or more active discount sequences of the same discount code");
        flag |= this.Invalidate<DiscountItem, DiscountItem.inventoryID>((PXSelectBase<DiscountItem>) this.Base.Items, PXResult<DiscountSequence, DiscountCustomer, DiscountItem>.op_Implicit(pxResult), "Same Item cannot be listed more than once. Same item cannot belong to two or more active discount sequences of the same discount code");
      }
      return flag;
    }

    public virtual bool HasCustomerAndInventoryPriceClassComboDuplicates()
    {
      bool flag = false;
      foreach (PXResult<DiscountSequence, DiscountCustomer, DiscountInventoryPriceClass> pxResult in this.GetDuplicatesSelectCommand().With<PXSelectBase<DiscountSequence>, PXSelectBase<DiscountSequence>>(new Func<PXSelectBase<DiscountSequence>, PXSelectBase<DiscountSequence>>(this.ApplyCustomer)).With<PXSelectBase<DiscountSequence>, PXSelectBase<DiscountSequence>>(new Func<PXSelectBase<DiscountSequence>, PXSelectBase<DiscountSequence>>(this.ApplyInventoryPriceClass)).Select(Array.Empty<object>()))
      {
        flag |= this.Invalidate<DiscountCustomer, DiscountCustomer.customerID>((PXSelectBase<DiscountCustomer>) this.Base.Customers, PXResult<DiscountSequence, DiscountCustomer, DiscountInventoryPriceClass>.op_Implicit(pxResult), "Same Item cannot be listed more than once. Same item cannot belong to two or more active discount sequences of the same discount code");
        flag |= this.Invalidate<DiscountInventoryPriceClass, DiscountInventoryPriceClass.inventoryPriceClassID>((PXSelectBase<DiscountInventoryPriceClass>) this.Base.InventoryPriceClasses, PXResult<DiscountSequence, DiscountCustomer, DiscountInventoryPriceClass>.op_Implicit(pxResult), "Same Item cannot be listed more than once. Same item cannot belong to two or more active discount sequences of the same discount code");
      }
      return flag;
    }

    public virtual bool HasCustomerPriceClassAndInventoryComboDuplicates()
    {
      bool flag = false;
      foreach (PXResult<DiscountSequence, DiscountCustomerPriceClass, DiscountItem> pxResult in this.GetDuplicatesSelectCommand().With<PXSelectBase<DiscountSequence>, PXSelectBase<DiscountSequence>>(new Func<PXSelectBase<DiscountSequence>, PXSelectBase<DiscountSequence>>(this.ApplyCustomerPriceClass)).With<PXSelectBase<DiscountSequence>, PXSelectBase<DiscountSequence>>(new Func<PXSelectBase<DiscountSequence>, PXSelectBase<DiscountSequence>>(this.ApplyInventory)).Select(Array.Empty<object>()))
      {
        flag |= this.Invalidate<DiscountCustomerPriceClass, DiscountCustomerPriceClass.customerPriceClassID>((PXSelectBase<DiscountCustomerPriceClass>) this.Base.CustomerPriceClasses, PXResult<DiscountSequence, DiscountCustomerPriceClass, DiscountItem>.op_Implicit(pxResult), "Same Item cannot be listed more than once. Same item cannot belong to two or more active discount sequences of the same discount code");
        flag |= this.Invalidate<DiscountItem, DiscountItem.inventoryID>((PXSelectBase<DiscountItem>) this.Base.Items, PXResult<DiscountSequence, DiscountCustomerPriceClass, DiscountItem>.op_Implicit(pxResult), "Same Item cannot be listed more than once. Same item cannot belong to two or more active discount sequences of the same discount code");
      }
      return flag;
    }

    public virtual bool HasCustomerPriceClassAndInventoryPriceClassComboDuplicates()
    {
      bool flag = false;
      foreach (PXResult<DiscountSequence, DiscountCustomerPriceClass, DiscountInventoryPriceClass> pxResult in this.GetDuplicatesSelectCommand().With<PXSelectBase<DiscountSequence>, PXSelectBase<DiscountSequence>>(new Func<PXSelectBase<DiscountSequence>, PXSelectBase<DiscountSequence>>(this.ApplyCustomerPriceClass)).With<PXSelectBase<DiscountSequence>, PXSelectBase<DiscountSequence>>(new Func<PXSelectBase<DiscountSequence>, PXSelectBase<DiscountSequence>>(this.ApplyInventoryPriceClass)).Select(Array.Empty<object>()))
      {
        flag |= this.Invalidate<DiscountCustomerPriceClass, DiscountCustomerPriceClass.customerPriceClassID>((PXSelectBase<DiscountCustomerPriceClass>) this.Base.CustomerPriceClasses, PXResult<DiscountSequence, DiscountCustomerPriceClass, DiscountInventoryPriceClass>.op_Implicit(pxResult), "Same Item cannot be listed more than once. Same item cannot belong to two or more active discount sequences of the same discount code");
        flag |= this.Invalidate<DiscountInventoryPriceClass, DiscountInventoryPriceClass.inventoryPriceClassID>((PXSelectBase<DiscountInventoryPriceClass>) this.Base.InventoryPriceClasses, PXResult<DiscountSequence, DiscountCustomerPriceClass, DiscountInventoryPriceClass>.op_Implicit(pxResult), "Same Item cannot be listed more than once. Same item cannot belong to two or more active discount sequences of the same discount code");
      }
      return flag;
    }

    public virtual bool HasWarehouseAndCustomerComboDuplicates()
    {
      bool flag = false;
      foreach (PXResult<DiscountSequence, DiscountSite, DiscountCustomer> pxResult in this.GetDuplicatesSelectCommand().With<PXSelectBase<DiscountSequence>, PXSelectBase<DiscountSequence>>(new Func<PXSelectBase<DiscountSequence>, PXSelectBase<DiscountSequence>>(this.ApplyWarehouse)).With<PXSelectBase<DiscountSequence>, PXSelectBase<DiscountSequence>>(new Func<PXSelectBase<DiscountSequence>, PXSelectBase<DiscountSequence>>(this.ApplyCustomer)).Select(Array.Empty<object>()))
      {
        flag |= this.Invalidate<DiscountSite, DiscountSite.siteID>((PXSelectBase<DiscountSite>) this.Base.Sites, PXResult<DiscountSequence, DiscountSite, DiscountCustomer>.op_Implicit(pxResult), "Same Warehouse cannot be listed more than once. Same warehouse cannot belong to two or more active discount sequences of the same discount code");
        flag |= this.Invalidate<DiscountCustomer, DiscountCustomer.customerID>((PXSelectBase<DiscountCustomer>) this.Base.Customers, PXResult<DiscountSequence, DiscountSite, DiscountCustomer>.op_Implicit(pxResult), "Same Item cannot be listed more than once. Same item cannot belong to two or more active discount sequences of the same discount code");
      }
      return flag;
    }

    public virtual bool HasWarehouseAndCustomerPriceClassComboDuplicates()
    {
      bool flag = false;
      foreach (PXResult<DiscountSequence, DiscountSite, DiscountCustomerPriceClass> pxResult in this.GetDuplicatesSelectCommand().With<PXSelectBase<DiscountSequence>, PXSelectBase<DiscountSequence>>(new Func<PXSelectBase<DiscountSequence>, PXSelectBase<DiscountSequence>>(this.ApplyWarehouse)).With<PXSelectBase<DiscountSequence>, PXSelectBase<DiscountSequence>>(new Func<PXSelectBase<DiscountSequence>, PXSelectBase<DiscountSequence>>(this.ApplyCustomerPriceClass)).Select(Array.Empty<object>()))
      {
        flag |= this.Invalidate<DiscountSite, DiscountSite.siteID>((PXSelectBase<DiscountSite>) this.Base.Sites, PXResult<DiscountSequence, DiscountSite, DiscountCustomerPriceClass>.op_Implicit(pxResult), "Same Warehouse cannot be listed more than once. Same warehouse cannot belong to two or more active discount sequences of the same discount code");
        flag |= this.Invalidate<DiscountCustomerPriceClass, DiscountCustomerPriceClass.customerPriceClassID>((PXSelectBase<DiscountCustomerPriceClass>) this.Base.CustomerPriceClasses, PXResult<DiscountSequence, DiscountSite, DiscountCustomerPriceClass>.op_Implicit(pxResult), "Same Item cannot be listed more than once. Same item cannot belong to two or more active discount sequences of the same discount code");
      }
      return flag;
    }

    public virtual bool HasWarehouseAndInventoryComboDuplicates()
    {
      bool flag = false;
      foreach (PXResult<DiscountSequence, DiscountSite, DiscountItem> pxResult in this.GetDuplicatesSelectCommand().With<PXSelectBase<DiscountSequence>, PXSelectBase<DiscountSequence>>(new Func<PXSelectBase<DiscountSequence>, PXSelectBase<DiscountSequence>>(this.ApplyWarehouse)).With<PXSelectBase<DiscountSequence>, PXSelectBase<DiscountSequence>>(new Func<PXSelectBase<DiscountSequence>, PXSelectBase<DiscountSequence>>(this.ApplyInventory)).Select(Array.Empty<object>()))
      {
        flag |= this.Invalidate<DiscountSite, DiscountSite.siteID>((PXSelectBase<DiscountSite>) this.Base.Sites, PXResult<DiscountSequence, DiscountSite, DiscountItem>.op_Implicit(pxResult), "Same Warehouse cannot be listed more than once. Same warehouse cannot belong to two or more active discount sequences of the same discount code");
        flag |= this.Invalidate<DiscountItem, DiscountItem.inventoryID>((PXSelectBase<DiscountItem>) this.Base.Items, PXResult<DiscountSequence, DiscountSite, DiscountItem>.op_Implicit(pxResult), "Same Item cannot be listed more than once. Same item cannot belong to two or more active discount sequences of the same discount code");
      }
      return flag;
    }

    public virtual bool HasWarehouseAndInventoryPriceClassComboDuplicates()
    {
      bool flag = false;
      foreach (PXResult<DiscountSequence, DiscountSite, DiscountInventoryPriceClass> pxResult in this.GetDuplicatesSelectCommand().With<PXSelectBase<DiscountSequence>, PXSelectBase<DiscountSequence>>(new Func<PXSelectBase<DiscountSequence>, PXSelectBase<DiscountSequence>>(this.ApplyWarehouse)).With<PXSelectBase<DiscountSequence>, PXSelectBase<DiscountSequence>>(new Func<PXSelectBase<DiscountSequence>, PXSelectBase<DiscountSequence>>(this.ApplyInventoryPriceClass)).Select(Array.Empty<object>()))
      {
        flag |= this.Invalidate<DiscountSite, DiscountSite.siteID>((PXSelectBase<DiscountSite>) this.Base.Sites, PXResult<DiscountSequence, DiscountSite, DiscountInventoryPriceClass>.op_Implicit(pxResult), "Same Warehouse cannot be listed more than once. Same warehouse cannot belong to two or more active discount sequences of the same discount code");
        flag |= this.Invalidate<DiscountInventoryPriceClass, DiscountInventoryPriceClass.inventoryPriceClassID>((PXSelectBase<DiscountInventoryPriceClass>) this.Base.InventoryPriceClasses, PXResult<DiscountSequence, DiscountSite, DiscountInventoryPriceClass>.op_Implicit(pxResult), "Same Item cannot be listed more than once. Same item cannot belong to two or more active discount sequences of the same discount code");
      }
      return flag;
    }

    public PXSelectBase<DiscountSequence> GetDuplicatesSelectCommand()
    {
      PXSelectReadonly<DiscountSequence, Where<DiscountSequence.isActive, Equal<True>, And<DiscountSequence.discountID, Equal<Current<DiscountSequence.discountID>>, And<DiscountSequence.discountSequenceID, NotEqual<Current<DiscountSequence.discountSequenceID>>>>>> duplicatesSelectCommand = new PXSelectReadonly<DiscountSequence, Where<DiscountSequence.isActive, Equal<True>, And<DiscountSequence.discountID, Equal<Current<DiscountSequence.discountID>>, And<DiscountSequence.discountSequenceID, NotEqual<Current<DiscountSequence.discountSequenceID>>>>>>((PXGraph) this.Base);
      if (((PXSelectBase<DiscountSequence>) this.Base.Sequence).Current.IsPromotion.GetValueOrDefault())
        ((PXSelectBase<DiscountSequence>) duplicatesSelectCommand).WhereAnd<Where<DiscountSequence.isPromotion, Equal<True>, And<Where2<Where<DiscountSequence.startDate, Between<Current<DiscountSequence.startDate>, Current<DiscountSequence.endDate>>>, Or<DiscountSequence.endDate, Between<Current<DiscountSequence.startDate>, Current<DiscountSequence.endDate>>>>>>>();
      else
        ((PXSelectBase<DiscountSequence>) duplicatesSelectCommand).WhereAnd<Where<DiscountSequence.isPromotion, Equal<False>>>();
      return (PXSelectBase<DiscountSequence>) duplicatesSelectCommand;
    }

    public PXSelectBase<DiscountSequence> ApplyCustomer(PXSelectBase<DiscountSequence> cmd)
    {
      cmd.Join<InnerJoin<DiscountCustomer, On<DiscountCustomer.FK.DiscountSequence>>>();
      cmd.WhereAnd<Where<DiscountCustomer.customerID, In2<Search<DiscountCustomer.customerID, Where<KeysRelation<CompositeKey<Field<DiscountCustomer.discountID>.IsRelatedTo<DiscountSequence.discountID>, Field<DiscountCustomer.discountSequenceID>.IsRelatedTo<DiscountSequence.discountSequenceID>>.WithTablesOf<DiscountSequence, DiscountCustomer>, DiscountSequence, DiscountCustomer>.SameAsCurrent>>>>>();
      return cmd;
    }

    public PXSelectBase<DiscountSequence> ApplyInventory(PXSelectBase<DiscountSequence> cmd)
    {
      cmd.Join<InnerJoin<DiscountItem, On<DiscountItem.FK.DiscountSequence>>>();
      cmd.WhereAnd<Where<DiscountItem.inventoryID, In2<Search<DiscountItem.inventoryID, Where<KeysRelation<CompositeKey<Field<DiscountItem.discountID>.IsRelatedTo<DiscountSequence.discountID>, Field<DiscountItem.discountSequenceID>.IsRelatedTo<DiscountSequence.discountSequenceID>>.WithTablesOf<DiscountSequence, DiscountItem>, DiscountSequence, DiscountItem>.SameAsCurrent>>>>>();
      return cmd;
    }

    public PXSelectBase<DiscountSequence> ApplyBranch(PXSelectBase<DiscountSequence> cmd)
    {
      cmd.Join<InnerJoin<DiscountBranch, On<DiscountBranch.FK.DiscountSequence>>>();
      cmd.WhereAnd<Where<DiscountBranch.branchID, In2<Search<DiscountBranch.branchID, Where<KeysRelation<CompositeKey<Field<DiscountBranch.discountID>.IsRelatedTo<DiscountSequence.discountID>, Field<DiscountBranch.discountSequenceID>.IsRelatedTo<DiscountSequence.discountSequenceID>>.WithTablesOf<DiscountSequence, DiscountBranch>, DiscountSequence, DiscountBranch>.SameAsCurrent>>>>>();
      return cmd;
    }

    public PXSelectBase<DiscountSequence> ApplyWarehouse(PXSelectBase<DiscountSequence> cmd)
    {
      cmd.Join<InnerJoin<DiscountSite, On<DiscountSite.FK.DiscountSequence>>>();
      cmd.WhereAnd<Where<DiscountSite.siteID, In2<Search<DiscountSite.siteID, Where<KeysRelation<CompositeKey<Field<DiscountSite.discountID>.IsRelatedTo<DiscountSequence.discountID>, Field<DiscountSite.discountSequenceID>.IsRelatedTo<DiscountSequence.discountSequenceID>>.WithTablesOf<DiscountSequence, DiscountSite>, DiscountSequence, DiscountSite>.SameAsCurrent>>>>>();
      return cmd;
    }

    public PXSelectBase<DiscountSequence> ApplyCustomerPriceClass(PXSelectBase<DiscountSequence> cmd)
    {
      cmd.Join<InnerJoin<DiscountCustomerPriceClass, On<DiscountCustomerPriceClass.FK.DiscountSequence>>>();
      cmd.WhereAnd<Where<DiscountCustomerPriceClass.customerPriceClassID, In2<Search<DiscountCustomerPriceClass.customerPriceClassID, Where<KeysRelation<CompositeKey<Field<DiscountCustomerPriceClass.discountID>.IsRelatedTo<DiscountSequence.discountID>, Field<DiscountCustomerPriceClass.discountSequenceID>.IsRelatedTo<DiscountSequence.discountSequenceID>>.WithTablesOf<DiscountSequence, DiscountCustomerPriceClass>, DiscountSequence, DiscountCustomerPriceClass>.SameAsCurrent>>>>>();
      return cmd;
    }

    public PXSelectBase<DiscountSequence> ApplyInventoryPriceClass(
      PXSelectBase<DiscountSequence> cmd)
    {
      cmd.Join<InnerJoin<DiscountInventoryPriceClass, On<DiscountInventoryPriceClass.DiscountSequenceFK>>>();
      cmd.WhereAnd<Where<DiscountInventoryPriceClass.inventoryPriceClassID, In2<Search<DiscountInventoryPriceClass.inventoryPriceClassID, Where<KeysRelation<CompositeKey<Field<DiscountInventoryPriceClass.discountID>.IsRelatedTo<DiscountSequence.discountID>, Field<DiscountInventoryPriceClass.discountSequenceID>.IsRelatedTo<DiscountSequence.discountSequenceID>>.WithTablesOf<DiscountSequence, DiscountInventoryPriceClass>, DiscountSequence, DiscountInventoryPriceClass>.SameAsCurrent>>>>>();
      return cmd;
    }

    public bool Invalidate<TEntity, TKeyField>(
      PXSelectBase<TEntity> view,
      TEntity entity,
      string errorMsg)
      where TEntity : class, IBqlTable, new()
      where TKeyField : class, IBqlField
    {
      ((PXSelectBase) view).Cache.SetValue<DiscountSequence.discountSequenceID>((object) entity, (object) ((PXSelectBase<DiscountSequence>) this.Base.Sequence).Current.DiscountSequenceID);
      TEntity entity1 = (TEntity) ((PXSelectBase) view).Cache.Locate((object) entity) ?? GraphHelper.RowCast<TEntity>((IEnumerable) view.Select(Array.Empty<object>())).FirstOrDefault<TEntity>((Func<TEntity, bool>) (r => ((PXSelectBase) view).Cache.ObjectsEqual((object) r, (object) (TEntity) entity)));
      if ((object) entity1 == null || ((PXSelectBase) view).Cache.GetStatus((object) entity1) == 3)
        return false;
      ((PXSelectBase) view).Cache.RaiseExceptionHandling<TKeyField>((object) entity1, (object) PXFieldState.op_Implicit((PXFieldState) ((PXSelectBase) view).Cache.GetStateExt<TKeyField>((object) entity1)), (Exception) new PXSetPropertyException(errorMsg, (PXErrorLevel) 4));
      return true;
    }
  }
}
