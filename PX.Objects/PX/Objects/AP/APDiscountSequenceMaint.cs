// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.APDiscountSequenceMaint
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.AR;
using PX.Objects.Common.Discount;
using PX.Objects.IN;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

#nullable enable
namespace PX.Objects.AP;

public class APDiscountSequenceMaint : PXGraph<
#nullable disable
APDiscountSequenceMaint>
{
  public PXSelect<APDiscountSequenceMaint.APDiscountEx, Where<APDiscountSequenceMaint.APDiscountEx.discountID, Equal<Current<VendorDiscountSequence.discountID>>>> Discount;
  public PXSelect<VendorDiscountSequence> Sequence;
  public PXSelect<DiscountDetail, Where<DiscountDetail.discountID, Equal<Current<VendorDiscountSequence.discountID>>, And<DiscountDetail.discountSequenceID, Equal<Current<VendorDiscountSequence.discountSequenceID>>>>, OrderBy<Asc<DiscountDetail.quantity, Asc<DiscountDetail.amount>>>> Details;
  public PXSelect<DiscountSequenceDetail, Where<DiscountSequenceDetail.discountID, Equal<Current<VendorDiscountSequence.discountID>>, And<DiscountSequenceDetail.discountSequenceID, Equal<Current<VendorDiscountSequence.discountSequenceID>>>>, OrderBy<Asc<DiscountSequenceDetail.quantity, Asc<DiscountSequenceDetail.amount>>>> SequenceDetails;
  [PXImport(typeof (VendorDiscountSequence))]
  public PXSelectJoin<DiscountItem, InnerJoin<PX.Objects.IN.InventoryItem, On<PX.Objects.IN.InventoryItem.inventoryID, Equal<DiscountItem.inventoryID>>>, Where<DiscountItem.discountID, Equal<Current<VendorDiscountSequence.discountID>>, And<DiscountItem.discountSequenceID, Equal<Current<VendorDiscountSequence.discountSequenceID>>>>> Items;
  public PXSelectJoin<DiscountInventoryPriceClass, InnerJoin<INPriceClass, On<DiscountInventoryPriceClass.inventoryPriceClassID, Equal<INPriceClass.priceClassID>>>, Where<DiscountInventoryPriceClass.discountID, Equal<Current<VendorDiscountSequence.discountID>>, And<DiscountInventoryPriceClass.discountSequenceID, Equal<Current<VendorDiscountSequence.discountSequenceID>>>>> InventoryPriceClasses;
  [PXImport(typeof (VendorDiscountSequence))]
  public PXSelectJoin<APDiscountLocation, InnerJoin<PX.Objects.CR.Location, On<PX.Objects.CR.Location.locationID, Equal<APDiscountLocation.locationID>>>, Where<APDiscountLocation.discountID, Equal<Current<VendorDiscountSequence.discountID>>, And<APDiscountLocation.discountSequenceID, Equal<Current<VendorDiscountSequence.discountSequenceID>>, And<APDiscountLocation.vendorID, Equal<Current<VendorDiscountSequence.vendorID>>>>>> Locations;
  public PXSelect<APDiscountVendor> Vendors;
  public PXFilter<APDiscountSequenceMaint.UpdateSettingsFilter> UpdateSettings;
  public PXSave<VendorDiscountSequence> Save;
  public PXAction<VendorDiscountSequence> cancel;
  public PXInsert<VendorDiscountSequence> Insert;
  public PXDelete<VendorDiscountSequence> Delete;
  public PXFirst<VendorDiscountSequence> First;
  public PXPrevious<VendorDiscountSequence> Prev;
  public PXNext<VendorDiscountSequence> Next;
  public PXLast<VendorDiscountSequence> Last;
  public PXAction<VendorDiscountSequence> updateDiscounts;

  public APDiscountSequenceMaint()
  {
    PXDBDefaultAttribute.SetDefaultForInsert<PX.Objects.AR.DiscountSequence.discountSequenceID>(this.Sequence.Cache, (object) null, false);
  }

  [PXCancelButton]
  [PXUIField(DisplayName = "Cancel", MapEnableRights = PXCacheRights.Select)]
  protected virtual IEnumerable Cancel(PXAdapter a)
  {
    APDiscountSequenceMaint graph = this;
    VendorDiscountSequence discountSequence1 = (VendorDiscountSequence) null;
    string str1 = (string) null;
    string str2 = (string) null;
    string str3 = (string) null;
    if (a.Searches != null)
    {
      if (a.Searches.Length != 0)
        str3 = (string) a.Searches[0];
      if (a.Searches.Length > 1)
        str1 = (string) a.Searches[1];
      if (a.Searches.Length > 2)
        str2 = (string) a.Searches[2];
    }
    if ((APDiscount) PXSelectBase<APDiscount, PXSelectJoin<APDiscount, InnerJoin<Vendor, On<Vendor.bAccountID, Equal<APDiscount.bAccountID>>>, Where<Vendor.acctCD, Equal<Required<Vendor.acctCD>>, And<APDiscount.discountID, Equal<Required<APDiscount.discountID>>>>>.Config>.Select((PXGraph) graph, (object) str3, (object) str1) == null)
    {
      if (a.Searches != null && a.Searches.Length > 1)
      {
        a.Searches[1] = (object) null;
        str1 = (string) null;
      }
      if (a.Searches != null && a.Searches.Length > 2)
        a.Searches[2] = (object) null;
    }
    VendorDiscountSequence discountSequence2 = (VendorDiscountSequence) PXSelectBase<VendorDiscountSequence, PXSelect<VendorDiscountSequence, Where<VendorDiscountSequence.discountSequenceID, Equal<Required<VendorDiscountSequence.discountSequenceID>>, And<VendorDiscountSequence.discountID, Equal<Required<VendorDiscountSequence.discountID>>>>>.Config>.Select((PXGraph) graph, (object) str2, (object) str1);
    APDiscount apDiscount = (APDiscount) PXSelectBase<APDiscount, PXSelect<APDiscount, Where<APDiscount.discountID, Equal<Required<APDiscount.discountID>>>>.Config>.Select((PXGraph) graph, (object) str1);
    bool flag1 = false;
    if (discountSequence2 == null)
    {
      if (a.Searches != null && a.Searches.Length > 2)
        a.Searches[2] = (object) null;
      if (str1 != null)
        flag1 = true;
    }
    if (graph.Discount.Current != null && graph.Discount.Current.DiscountID != str1)
      str2 = (string) null;
    foreach (VendorDiscountSequence discountSequence3 in new PXCancel<VendorDiscountSequence>((PXGraph) graph, nameof (Cancel)).Press(a))
      discountSequence1 = discountSequence3;
    if (flag1)
    {
      graph.Sequence.Cache.Remove((object) discountSequence1);
      VendorDiscountSequence discountSequence4 = new VendorDiscountSequence();
      discountSequence4.DiscountID = str1;
      Vendor vendor = (Vendor) PXSelectBase<Vendor, PXSelect<Vendor, Where<Vendor.acctCD, Equal<Required<Vendor.acctCD>>>>.Config>.SelectWindowed((PXGraph) graph, 0, 1, (object) str3);
      discountSequence4.VendorID = vendor.BAccountID;
      if (apDiscount != null)
        discountSequence4.Description = apDiscount.Description;
      discountSequence1 = graph.Sequence.Insert(discountSequence4);
      graph.Sequence.Cache.IsDirty = false;
      if (apDiscount != null)
      {
        bool? isAutoNumber = apDiscount.IsAutoNumber;
        bool flag2 = false;
        if (isAutoNumber.GetValueOrDefault() == flag2 & isAutoNumber.HasValue)
          discountSequence1.DiscountSequenceID = str2;
        else
          discountSequence1.DiscountSequenceID = PXMessages.LocalizeNoPrefix(" <NEW>");
        graph.Sequence.Cache.Normalize();
      }
    }
    if (discountSequence2 != null && discountSequence2.Description != null)
      discountSequence1.Description = discountSequence2.Description;
    yield return (object) discountSequence1;
  }

  [PXUIField(DisplayName = "Update Discounts", MapEnableRights = PXCacheRights.Update, MapViewRights = PXCacheRights.Select, Enabled = true)]
  [PXProcessButton]
  public virtual IEnumerable UpdateDiscounts(PXAdapter adapter)
  {
    if (this.Sequence.Current != null)
    {
      WebDialogResult webDialogResult = this.UpdateSettings.AskExt();
      if (webDialogResult == WebDialogResult.OK || this.IsContractBasedAPI && webDialogResult == WebDialogResult.Yes)
      {
        this.Save.Press();
        ARUpdateDiscounts.UpdateDiscount(this.Sequence.Current.DiscountID, this.Sequence.Current.DiscountSequenceID, this.UpdateSettings.Current.FilterDate);
        this.Sequence.Current.tstamp = PXDatabase.SelectTimeStamp();
        this.Save.Press();
        this.SelectTimeStamp();
        this.Details.Cache.Clear();
        this.Details.Cache.ClearQueryCacheObsolete();
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
    if (this.Sequence.Current != null && this.Sequence.Current.StartDate.HasValue)
      e.NewValue = (object) this.Sequence.Current.StartDate;
    else
      e.NewValue = (object) this.Accessinfo.BusinessDate;
  }

  protected virtual void VendorDiscountSequence_IsPromotion_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    if (!(e.Row is VendorDiscountSequence row))
      return;
    if (row.IsPromotion.GetValueOrDefault())
    {
      row.PendingFreeItemID = new int?();
      row.LastFreeItemID = new int?();
    }
    else
      row.EndDate = new System.DateTime?();
  }

  protected virtual void VendorDiscountSequence_DiscountedFor_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    PXUIFieldAttribute.SetEnabled<VendorDiscountSequence.prorate>(sender, e.Row, ((PX.Objects.AR.DiscountSequence) e.Row).DiscountedFor == "A");
  }

  protected virtual void VendorDiscountSequence_DiscountID_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    VendorDiscountSequence row = e.Row as VendorDiscountSequence;
    APDiscount apDiscount = (APDiscount) PXSelectBase<APDiscount, PXSelect<APDiscount, Where<APDiscount.discountID, Equal<Required<APDiscount.discountID>>>>.Config>.Select((PXGraph) this, (object) row.DiscountID);
    if (row == null || apDiscount == null || !(apDiscount.Type == "D"))
      return;
    row.BreakBy = "A";
  }

  protected virtual void DiscountDetail_Amount_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    if (!(e.Row is DiscountDetail row) || !this.Sequence.Current.IsPromotion.GetValueOrDefault())
      return;
    PXResult<DiscountDetail> pxResult1 = (PXResult<DiscountDetail>) PXSelectBase<DiscountDetail, PXSelect<DiscountDetail, Where<DiscountDetail.discountID, Equal<Current<VendorDiscountSequence.discountID>>, And<DiscountDetail.discountSequenceID, Equal<Current<VendorDiscountSequence.discountSequenceID>>, And<DiscountDetail.amount, Less<Required<DiscountDetail.amount>>, And<DiscountDetail.isActive, Equal<True>>>>>, OrderBy<Desc<DiscountDetail.amount>>>.Config>.SelectSingleBound((PXGraph) this, (object[]) null, (object) row.Amount);
    if (pxResult1 != null)
    {
      DiscountDetail discountDetail = (DiscountDetail) pxResult1;
      discountDetail.AmountTo = row.Amount;
      this.Details.Update(discountDetail);
    }
    PXResult<DiscountDetail> pxResult2 = (PXResult<DiscountDetail>) PXSelectBase<DiscountDetail, PXSelect<DiscountDetail, Where<DiscountDetail.discountID, Equal<Current<VendorDiscountSequence.discountID>>, And<DiscountDetail.discountSequenceID, Equal<Current<VendorDiscountSequence.discountSequenceID>>, And<DiscountDetail.amount, Greater<Required<DiscountDetail.amount>>, And<DiscountDetail.isActive, Equal<True>>>>>, OrderBy<Asc<DiscountDetail.amount>>>.Config>.SelectSingleBound((PXGraph) this, (object[]) null, (object) row.Amount);
    if (pxResult2 == null)
    {
      row.AmountTo = new Decimal?();
    }
    else
    {
      DiscountDetail discountDetail = (DiscountDetail) pxResult2;
      row.AmountTo = discountDetail.Amount;
    }
  }

  protected virtual void DiscountDetail_Quantity_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    if (!(e.Row is DiscountDetail row) || !this.Sequence.Current.IsPromotion.GetValueOrDefault())
      return;
    PXResult<DiscountDetail> pxResult1 = (PXResult<DiscountDetail>) PXSelectBase<DiscountDetail, PXSelect<DiscountDetail, Where<DiscountDetail.discountID, Equal<Current<VendorDiscountSequence.discountID>>, And<DiscountDetail.discountSequenceID, Equal<Current<VendorDiscountSequence.discountSequenceID>>, And<DiscountDetail.quantity, Less<Required<DiscountDetail.quantity>>, And<DiscountDetail.isActive, Equal<True>>>>>, OrderBy<Desc<DiscountDetail.quantity>>>.Config>.SelectSingleBound((PXGraph) this, (object[]) null, (object) row.Quantity);
    if (pxResult1 != null)
    {
      DiscountDetail discountDetail = (DiscountDetail) pxResult1;
      discountDetail.QuantityTo = row.Quantity;
      this.Details.Update(discountDetail);
    }
    PXResult<DiscountDetail> pxResult2 = (PXResult<DiscountDetail>) PXSelectBase<DiscountDetail, PXSelect<DiscountDetail, Where<DiscountDetail.discountID, Equal<Current<VendorDiscountSequence.discountID>>, And<DiscountDetail.discountSequenceID, Equal<Current<VendorDiscountSequence.discountSequenceID>>, And<DiscountDetail.quantity, Greater<Required<DiscountDetail.quantity>>, And<DiscountDetail.isActive, Equal<True>>>>>, OrderBy<Asc<DiscountDetail.quantity>>>.Config>.SelectSingleBound((PXGraph) this, (object[]) null, (object) row.Quantity);
    if (pxResult2 == null)
    {
      row.QuantityTo = new Decimal?();
    }
    else
    {
      DiscountDetail discountDetail = (DiscountDetail) pxResult2;
      row.QuantityTo = discountDetail.Quantity;
    }
  }

  protected virtual void VendorDiscountSequence_RowSelected(
    PXCache sender,
    PXRowSelectedEventArgs e)
  {
    if (!(e.Row is VendorDiscountSequence row))
      return;
    PXCache cache1 = this.Details.Cache;
    PXCache cache2 = this.Details.Cache;
    PXCache cache3 = this.Details.Cache;
    int? vendorId = row.VendorID;
    int num1;
    bool flag1 = (num1 = !vendorId.HasValue || row.DiscountID == null ? 0 : (row.DiscountSequenceID != null ? 1 : 0)) != 0;
    cache3.AllowDelete = num1 != 0;
    int num2;
    bool flag2 = (num2 = flag1 ? 1 : 0) != 0;
    cache2.AllowUpdate = num2 != 0;
    int num3 = flag2 ? 1 : 0;
    cache1.AllowInsert = num3 != 0;
    PXCache cache4 = this.Items.Cache;
    PXCache cache5 = this.Items.Cache;
    PXCache cache6 = this.Items.Cache;
    vendorId = row.VendorID;
    int num4;
    bool flag3 = (num4 = !vendorId.HasValue || row.DiscountID == null ? 0 : (row.DiscountSequenceID != null ? 1 : 0)) != 0;
    cache6.AllowDelete = num4 != 0;
    int num5;
    bool flag4 = (num5 = flag3 ? 1 : 0) != 0;
    cache5.AllowUpdate = num5 != 0;
    int num6 = flag4 ? 1 : 0;
    cache4.AllowInsert = num6 != 0;
    PXCache cache7 = this.InventoryPriceClasses.Cache;
    PXCache cache8 = this.InventoryPriceClasses.Cache;
    PXCache cache9 = this.InventoryPriceClasses.Cache;
    vendorId = row.VendorID;
    int num7;
    bool flag5 = (num7 = !vendorId.HasValue || row.DiscountID == null ? 0 : (row.DiscountSequenceID != null ? 1 : 0)) != 0;
    cache9.AllowDelete = num7 != 0;
    int num8;
    bool flag6 = (num8 = flag5 ? 1 : 0) != 0;
    cache8.AllowUpdate = num8 != 0;
    int num9 = flag6 ? 1 : 0;
    cache7.AllowInsert = num9 != 0;
    PXCache cache10 = this.Locations.Cache;
    PXCache cache11 = this.Locations.Cache;
    PXCache cache12 = this.Locations.Cache;
    vendorId = row.VendorID;
    int num10;
    bool flag7 = (num10 = !vendorId.HasValue || row.DiscountID == null ? 0 : (row.DiscountSequenceID != null ? 1 : 0)) != 0;
    cache12.AllowDelete = num10 != 0;
    int num11;
    bool flag8 = (num11 = flag7 ? 1 : 0) != 0;
    cache11.AllowUpdate = num11 != 0;
    int num12 = flag8 ? 1 : 0;
    cache10.AllowInsert = num12 != 0;
    APDiscount discount = (APDiscount) PXSelectBase<APDiscount, PXSelect<APDiscount, Where<APDiscount.discountID, Equal<Required<APDiscount.discountID>>>>.Config>.Select((PXGraph) this, (object) row.DiscountID);
    this.SetControlsState(sender, row, discount);
    this.SetGridColumnsState(discount);
    Dictionary<string, string> valueLabelDic = new DiscountOption.ListAttribute().ValueLabelDic;
    valueLabelDic.Remove("F");
    PXStringListAttribute.SetList<VendorDiscountSequence.discountedFor>(sender, (object) row, valueLabelDic.Keys.ToArray<string>(), valueLabelDic.Values.ToArray<string>());
  }

  protected virtual void VendorDiscountSequence_RowPersisting(
    PXCache sender,
    PXRowPersistingEventArgs e)
  {
    bool? nullable1;
    if (e.Row is VendorDiscountSequence row1)
    {
      nullable1 = row1.IsPromotion;
      System.DateTime? nullable2;
      if (nullable1.GetValueOrDefault())
      {
        nullable2 = row1.EndDate;
        if (!nullable2.HasValue && sender.RaiseExceptionHandling<VendorDiscountSequence.endDate>(e.Row, (object) null, (Exception) new PXSetPropertyException("'{0}' cannot be empty.")))
          throw new PXRowPersistingException(typeof (VendorDiscountSequence.endDate).Name, (object) null, "'{0}' cannot be empty.");
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
            System.DateTime? startDate = row1.StartDate;
            if ((nullable2.HasValue & startDate.HasValue ? (nullable2.GetValueOrDefault() < startDate.GetValueOrDefault() ? 1 : 0) : 0) != 0 && sender.RaiseExceptionHandling<PX.Objects.AR.DiscountSequence.endDate>(e.Row, (object) row1.EndDate, (Exception) new PXSetPropertyException("The Expiration Date should not be earlier than the Effective Date.")))
              throw new PXRowPersistingException(typeof (PX.Objects.AR.DiscountSequence.endDate).Name, (object) row1.EndDate, "The Expiration Date should not be earlier than the Effective Date.");
          }
        }
      }
    }
    if ((e.Operation & PXDBOperation.Delete) != PXDBOperation.Insert)
      return;
    APDiscount current = (APDiscount) this.Discount.Current;
    if (current == null)
      return;
    PXCache cache = sender;
    object row2 = e.Row;
    nullable1 = current.IsAutoNumber;
    int num = nullable1.GetValueOrDefault() ? 1 : 0;
    PXDBDefaultAttribute.SetDefaultForInsert<VendorDiscountSequence.discountSequenceID>(cache, row2, num != 0);
  }

  protected virtual void VendorDiscountSequence_DiscountSequenceID_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    if (e.Row == null)
      return;
    APDiscount apDiscount = (APDiscount) PXSelectBase<APDiscount, PXSelect<APDiscount, Where<APDiscount.discountID, Equal<Required<VendorDiscountSequence.discountID>>>>.Config>.Select((PXGraph) this, (object) ((PX.Objects.AR.DiscountSequence) e.Row).DiscountID);
    if (apDiscount == null || !apDiscount.IsAutoNumber.GetValueOrDefault())
      return;
    e.NewValue = (object) PXMessages.LocalizeNoPrefix(" <NEW>");
    e.Cancel = true;
  }

  protected virtual void DiscountDetail_RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    if (!(e.Row is DiscountDetail row))
      return;
    if (!sender.ObjectsEqual<DiscountDetail.isActive>(e.Row, e.OldRow))
    {
      PXResult<DiscountSequenceDetail> pxResult = (PXResult<DiscountSequenceDetail>) PXSelectBase<DiscountSequenceDetail, PXSelect<DiscountSequenceDetail, Where<DiscountSequenceDetail.discountID, Equal<Current<VendorDiscountSequence.discountID>>, And<DiscountSequenceDetail.discountSequenceID, Equal<Current<VendorDiscountSequence.discountSequenceID>>, And<DiscountSequenceDetail.discountDetailsID, NotEqual<Required<DiscountSequenceDetail.discountDetailsID>>, And<DiscountSequenceDetail.lineNbr, Equal<Required<DiscountSequenceDetail.lineNbr>>, And<DiscountSequenceDetail.isLast, Equal<True>>>>>>, OrderBy<Asc<DiscountSequenceDetail.quantity>>>.Config>.SelectSingleBound((PXGraph) this, (object[]) null, (object) row.DiscountDetailsID, (object) row.LineNbr);
      if (pxResult != null)
      {
        ((DiscountSequenceDetail) pxResult).IsActive = row.IsActive;
        this.SequenceDetails.Update((DiscountSequenceDetail) pxResult);
      }
      if (this.Sequence.Current.BreakBy == "Q")
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
              this.Details.Update(detailLineQuantity2);
            }
            else
            {
              detailLineQuantity2.QuantityTo = new Decimal?();
              this.Details.Update(detailLineQuantity2);
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
            this.Details.Update(detailLineQuantity2);
          }
          else
          {
            detailLineQuantity2.QuantityTo = detailLineQuantity1.Quantity;
            this.Details.Update(detailLineQuantity2);
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
              this.Details.Update(detailLineAmount2);
            }
            else
            {
              detailLineAmount2.AmountTo = new Decimal?();
              this.Details.Update(detailLineAmount2);
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
            this.Details.Update(detailLineAmount2);
          }
          else
          {
            detailLineAmount2.AmountTo = detailLineAmount1.Amount;
            this.Details.Update(detailLineAmount2);
          }
        }
        else
          row.AmountTo = detailLineAmount1.Amount;
      }
    }
    if (sender.ObjectsEqual<DiscountDetail.pendingQuantity, DiscountDetail.pendingAmount, DiscountDetail.pendingDiscountPercent, DiscountDetail.pendingFreeItemQty>(e.Row, e.OldRow))
      return;
    System.DateTime? startDate = row.StartDate;
    if (startDate.HasValue)
      return;
    if (this.Sequence.Current != null)
    {
      startDate = this.Sequence.Current.StartDate;
      if (startDate.HasValue)
      {
        row.StartDate = this.Sequence.Current.StartDate;
        return;
      }
    }
    row.StartDate = this.Accessinfo.BusinessDate;
  }

  protected virtual void DiscountDetail_RowDeleting(PXCache sender, PXRowDeletingEventArgs e)
  {
    if (!(e.Row is DiscountDetail row))
      return;
    PXResult<DiscountSequenceDetail> pxResult = (PXResult<DiscountSequenceDetail>) PXSelectBase<DiscountSequenceDetail, PXSelect<DiscountSequenceDetail, Where<DiscountSequenceDetail.discountID, Equal<Current<VendorDiscountSequence.discountID>>, And<DiscountSequenceDetail.discountSequenceID, Equal<Current<VendorDiscountSequence.discountSequenceID>>, And<DiscountSequenceDetail.discountDetailsID, NotEqual<Required<DiscountSequenceDetail.discountDetailsID>>, And<DiscountSequenceDetail.lineNbr, Equal<Required<DiscountSequenceDetail.lineNbr>>, And<DiscountSequenceDetail.isLast, Equal<True>>>>>>, OrderBy<Asc<DiscountSequenceDetail.quantity>>>.Config>.SelectSingleBound((PXGraph) this, (object[]) null, (object) row.DiscountDetailsID, (object) row.LineNbr);
    if (pxResult == null)
      return;
    this.SequenceDetails.Delete((DiscountSequenceDetail) pxResult);
  }

  protected virtual void DiscountDetail_RowDeleted(PXCache sender, PXRowDeletedEventArgs e)
  {
    if (!(e.Row is DiscountDetail row))
      return;
    if (this.Sequence.Current.BreakBy == "Q")
    {
      DiscountDetail detailLineQuantity1 = this.GetNextDiscountDetailLineQuantity(row);
      DiscountDetail detailLineQuantity2 = this.GetPrevDiscountDetailLineQuantity(row);
      if (detailLineQuantity2 == null)
        return;
      if (detailLineQuantity1 == null)
      {
        detailLineQuantity2.QuantityTo = new Decimal?();
        this.Details.Update(detailLineQuantity2);
      }
      else
      {
        detailLineQuantity2.QuantityTo = detailLineQuantity1.Quantity;
        this.Details.Update(detailLineQuantity2);
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
        this.Details.Update(detailLineAmount2);
      }
      else
      {
        detailLineAmount2.AmountTo = detailLineAmount1.Amount;
        this.Details.Update(detailLineAmount2);
      }
    }
  }

  public virtual DiscountDetail GetNextDiscountDetailLineQuantity(DiscountDetail currentLine)
  {
    return (DiscountDetail) PXSelectBase<DiscountDetail, PXSelect<DiscountDetail, Where<DiscountDetail.discountID, Equal<Current<VendorDiscountSequence.discountID>>, And<DiscountDetail.discountSequenceID, Equal<Current<VendorDiscountSequence.discountSequenceID>>, And<DiscountDetail.quantity, Greater<Required<DiscountDetail.quantity>>, And<DiscountDetail.isActive, Equal<True>>>>>, OrderBy<Asc<DiscountDetail.quantity>>>.Config>.SelectSingleBound((PXGraph) this, (object[]) null, (object) currentLine.Quantity);
  }

  public virtual DiscountDetail GetPrevDiscountDetailLineQuantity(DiscountDetail currentLine)
  {
    return (DiscountDetail) PXSelectBase<DiscountDetail, PXSelect<DiscountDetail, Where<DiscountDetail.discountID, Equal<Current<VendorDiscountSequence.discountID>>, And<DiscountDetail.discountSequenceID, Equal<Current<VendorDiscountSequence.discountSequenceID>>, And<DiscountDetail.quantity, Less<Required<DiscountDetail.quantity>>, And<DiscountDetail.isActive, Equal<True>>>>>, OrderBy<Desc<DiscountDetail.quantity>>>.Config>.SelectSingleBound((PXGraph) this, (object[]) null, (object) currentLine.Quantity);
  }

  public virtual DiscountDetail GetNextDiscountDetailLineAmount(DiscountDetail currentLine)
  {
    return (DiscountDetail) PXSelectBase<DiscountDetail, PXSelect<DiscountDetail, Where<DiscountDetail.discountID, Equal<Current<VendorDiscountSequence.discountID>>, And<DiscountDetail.discountSequenceID, Equal<Current<VendorDiscountSequence.discountSequenceID>>, And<DiscountDetail.amount, Greater<Required<DiscountDetail.amount>>, And<DiscountDetail.isActive, Equal<True>>>>>, OrderBy<Asc<DiscountDetail.amount>>>.Config>.SelectSingleBound((PXGraph) this, (object[]) null, (object) currentLine.Amount);
  }

  public virtual DiscountDetail GetPrevDiscountDetailLineAmount(DiscountDetail currentLine)
  {
    return (DiscountDetail) PXSelectBase<DiscountDetail, PXSelect<DiscountDetail, Where<DiscountDetail.discountID, Equal<Current<VendorDiscountSequence.discountID>>, And<DiscountDetail.discountSequenceID, Equal<Current<VendorDiscountSequence.discountSequenceID>>, And<DiscountDetail.amount, Less<Required<DiscountDetail.amount>>, And<DiscountDetail.isActive, Equal<True>>>>>, OrderBy<Desc<DiscountDetail.amount>>>.Config>.SelectSingleBound((PXGraph) this, (object[]) null, (object) currentLine.Amount);
  }

  private bool RunVerification()
  {
    if (this.Discount.Current != null && this.Sequence.Current != null && this.Sequence.Current.IsActive.GetValueOrDefault())
    {
      switch (this.Discount.Current.ApplicableTo)
      {
        case "UN":
          return this.VerifyUnconditional();
        case "VI":
          return this.VerifyItem();
        case "VP":
          return this.VerifyInventoryPriceClass();
        case "VL":
          return this.VerifyLocation();
        case "LI":
          return this.VerifyCombination_Location_Inventory();
      }
    }
    return true;
  }

  private bool VerifyUnconditional()
  {
    bool flag = true;
    if (!this.IsUncoditionalValid())
    {
      flag = false;
      this.Sequence.Cache.RaiseExceptionHandling<VendorDiscountSequence.discountSequenceID>((object) this.Sequence.Current, (object) this.Sequence.Current.DiscountSequenceID, (Exception) new PXSetPropertyException("Unconditional discounts cannot have active overlapping sequences.", PXErrorLevel.Error));
    }
    return flag;
  }

  private bool IsUncoditionalValid()
  {
    if (this.Sequence.Current.IsPromotion.GetValueOrDefault())
    {
      if ((VendorDiscountSequence) PXSelectBase<VendorDiscountSequence, PXSelectReadonly<VendorDiscountSequence, Where<VendorDiscountSequence.isActive, Equal<True>, And<VendorDiscountSequence.discountID, Equal<Current<VendorDiscountSequence.discountID>>, And<VendorDiscountSequence.discountSequenceID, NotEqual<Current<VendorDiscountSequence.discountSequenceID>>, And<VendorDiscountSequence.isPromotion, Equal<True>, PX.Data.And<Where2<Where<VendorDiscountSequence.startDate, Between<Current<VendorDiscountSequence.startDate>, Current<VendorDiscountSequence.endDate>>>, Or<VendorDiscountSequence.endDate, Between<Current<VendorDiscountSequence.startDate>, Current<VendorDiscountSequence.endDate>>>>>>>>>>.Config>.SelectWindowed((PXGraph) this, 0, 1) != null)
        return false;
    }
    else if ((VendorDiscountSequence) PXSelectBase<VendorDiscountSequence, PXSelectReadonly<VendorDiscountSequence, Where<VendorDiscountSequence.isActive, Equal<True>, And<VendorDiscountSequence.discountID, Equal<Current<VendorDiscountSequence.discountID>>, And<VendorDiscountSequence.discountSequenceID, NotEqual<Current<VendorDiscountSequence.discountSequenceID>>, And<VendorDiscountSequence.isPromotion, Equal<False>>>>>>.Config>.SelectWindowed((PXGraph) this, 0, 1) != null)
      return false;
    return true;
  }

  private bool VerifyItem()
  {
    bool flag = true;
    foreach (PXResult<DiscountItem, PX.Objects.IN.InventoryItem> row in this.Items.Select())
    {
      if (this.Items.Cache.GetStatus((object) (DiscountItem) row) != PXEntryStatus.Deleted && !this.VerifyItem(((DiscountItem) row).InventoryID))
      {
        flag = false;
        this.Items.Cache.RaiseExceptionHandling<DiscountItem.inventoryID>((object) (DiscountItem) row, (object) ((PX.Objects.IN.InventoryItem) row).InventoryCD, (Exception) new PXSetPropertyException("Same Item cannot be listed more than once. Same item cannot belong to two or more active discount sequences of the same discount code", PXErrorLevel.Error));
      }
    }
    return flag;
  }

  private bool VerifyItem(int? inventoryID)
  {
    if (this.Sequence.Current.IsPromotion.GetValueOrDefault())
    {
      if ((DiscountItem) PXSelectBase<DiscountItem, PXSelectReadonly2<DiscountItem, InnerJoin<VendorDiscountSequence, On<DiscountItem.discountID, Equal<VendorDiscountSequence.discountID>, And<DiscountItem.discountSequenceID, Equal<VendorDiscountSequence.discountSequenceID>, And<VendorDiscountSequence.discountSequenceID, NotEqual<Current<VendorDiscountSequence.discountSequenceID>>>>>>, Where<DiscountItem.discountID, Equal<Current<VendorDiscountSequence.discountID>>, And<DiscountItem.inventoryID, Equal<Required<DiscountItem.inventoryID>>, And<VendorDiscountSequence.isActive, Equal<True>, And<VendorDiscountSequence.isPromotion, Equal<True>, PX.Data.And<Where2<Where<VendorDiscountSequence.startDate, Between<Current<VendorDiscountSequence.startDate>, Current<VendorDiscountSequence.endDate>>>, Or<VendorDiscountSequence.endDate, Between<Current<VendorDiscountSequence.startDate>, Current<VendorDiscountSequence.endDate>>>>>>>>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, (object) inventoryID) != null)
        return false;
    }
    else if ((DiscountItem) PXSelectBase<DiscountItem, PXSelectReadonly2<DiscountItem, InnerJoin<VendorDiscountSequence, On<DiscountItem.discountID, Equal<VendorDiscountSequence.discountID>, And<DiscountItem.discountSequenceID, Equal<VendorDiscountSequence.discountSequenceID>, And<VendorDiscountSequence.discountSequenceID, NotEqual<Current<VendorDiscountSequence.discountSequenceID>>>>>>, Where<DiscountItem.discountID, Equal<Current<VendorDiscountSequence.discountID>>, And<DiscountItem.inventoryID, Equal<Required<DiscountItem.inventoryID>>, And<VendorDiscountSequence.isActive, Equal<True>, And<VendorDiscountSequence.isPromotion, Equal<False>>>>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, (object) inventoryID) != null)
      return false;
    return true;
  }

  private bool VerifyInventoryPriceClass()
  {
    bool flag = true;
    foreach (PXResult<DiscountInventoryPriceClass> pxResult in this.InventoryPriceClasses.Select())
    {
      DiscountInventoryPriceClass row = (DiscountInventoryPriceClass) pxResult;
      if (this.InventoryPriceClasses.Cache.GetStatus((object) row) != PXEntryStatus.Deleted && !this.VerifyInventoryPriceClass(row.InventoryPriceClassID))
      {
        flag = false;
        this.InventoryPriceClasses.Cache.RaiseExceptionHandling<DiscountInventoryPriceClass.inventoryPriceClassID>((object) row, (object) null, (Exception) new PXSetPropertyException("Same Item cannot be listed more than once. Same item cannot belong to two or more active discount sequences of the same discount code", PXErrorLevel.Error));
      }
    }
    return flag;
  }

  private bool VerifyInventoryPriceClass(string priceClassID)
  {
    if (this.Sequence.Current.IsPromotion.GetValueOrDefault())
    {
      if ((DiscountInventoryPriceClass) PXSelectBase<DiscountInventoryPriceClass, PXSelectReadonly2<DiscountInventoryPriceClass, InnerJoin<VendorDiscountSequence, On<DiscountInventoryPriceClass.discountID, Equal<VendorDiscountSequence.discountID>, And<DiscountInventoryPriceClass.discountSequenceID, Equal<VendorDiscountSequence.discountSequenceID>, And<VendorDiscountSequence.discountSequenceID, NotEqual<Current<VendorDiscountSequence.discountSequenceID>>>>>>, Where<DiscountInventoryPriceClass.discountID, Equal<Current<VendorDiscountSequence.discountID>>, And<DiscountInventoryPriceClass.inventoryPriceClassID, Equal<Required<DiscountInventoryPriceClass.inventoryPriceClassID>>, And<VendorDiscountSequence.isActive, Equal<True>, And<VendorDiscountSequence.isPromotion, Equal<True>, PX.Data.And<Where2<Where<VendorDiscountSequence.startDate, Between<Current<VendorDiscountSequence.startDate>, Current<VendorDiscountSequence.endDate>>>, Or<VendorDiscountSequence.endDate, Between<Current<VendorDiscountSequence.startDate>, Current<VendorDiscountSequence.endDate>>>>>>>>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, (object) priceClassID) != null)
        return false;
    }
    else if ((DiscountInventoryPriceClass) PXSelectBase<DiscountInventoryPriceClass, PXSelectReadonly2<DiscountInventoryPriceClass, InnerJoin<VendorDiscountSequence, On<DiscountInventoryPriceClass.discountID, Equal<VendorDiscountSequence.discountID>, And<DiscountInventoryPriceClass.discountSequenceID, Equal<VendorDiscountSequence.discountSequenceID>, And<VendorDiscountSequence.discountSequenceID, NotEqual<Current<VendorDiscountSequence.discountSequenceID>>>>>>, Where<DiscountInventoryPriceClass.discountID, Equal<Current<VendorDiscountSequence.discountID>>, And<DiscountInventoryPriceClass.inventoryPriceClassID, Equal<Required<DiscountInventoryPriceClass.inventoryPriceClassID>>, And<VendorDiscountSequence.isActive, Equal<True>, And<VendorDiscountSequence.isPromotion, Equal<False>>>>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, (object) priceClassID) != null)
      return false;
    return true;
  }

  private bool VerifyLocation()
  {
    bool flag = true;
    foreach (PXResult<APDiscountLocation, PX.Objects.CR.Location> row in this.Locations.Select())
    {
      if (this.Locations.Cache.GetStatus((object) (APDiscountLocation) row) != PXEntryStatus.Deleted && !this.VerifyLocation(((APDiscountLocation) row).LocationID))
      {
        flag = false;
        this.Locations.Cache.RaiseExceptionHandling<APDiscountLocation.locationID>((object) (APDiscountLocation) row, (object) ((PX.Objects.CR.Location) row).LocationID, (Exception) new PXSetPropertyException("Same Item cannot be listed more than once. Same item cannot belong to two or more active discount sequences of the same discount code", PXErrorLevel.Error));
      }
    }
    return flag;
  }

  private bool VerifyLocation(int? locationID)
  {
    if (this.Sequence.Current.IsPromotion.GetValueOrDefault())
    {
      if ((APDiscountLocation) PXSelectBase<APDiscountLocation, PXSelectReadonly2<APDiscountLocation, InnerJoin<VendorDiscountSequence, On<APDiscountLocation.discountID, Equal<VendorDiscountSequence.discountID>, And<APDiscountLocation.discountSequenceID, Equal<VendorDiscountSequence.discountSequenceID>, And<VendorDiscountSequence.discountSequenceID, NotEqual<Current<VendorDiscountSequence.discountSequenceID>>>>>>, Where<APDiscountLocation.discountID, Equal<Current<VendorDiscountSequence.discountID>>, And<APDiscountLocation.vendorID, Equal<Current<VendorDiscountSequence.vendorID>>, And<APDiscountLocation.locationID, Equal<Required<APDiscountLocation.locationID>>, And<VendorDiscountSequence.isActive, Equal<True>, And<VendorDiscountSequence.isPromotion, Equal<True>, PX.Data.And<Where2<Where<VendorDiscountSequence.startDate, Between<Current<VendorDiscountSequence.startDate>, Current<VendorDiscountSequence.endDate>>>, Or<VendorDiscountSequence.endDate, Between<Current<VendorDiscountSequence.startDate>, Current<VendorDiscountSequence.endDate>>>>>>>>>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, (object) locationID) != null)
        return false;
    }
    else if ((APDiscountLocation) PXSelectBase<APDiscountLocation, PXSelectReadonly2<APDiscountLocation, InnerJoin<VendorDiscountSequence, On<APDiscountLocation.discountID, Equal<VendorDiscountSequence.discountID>, And<APDiscountLocation.discountSequenceID, Equal<VendorDiscountSequence.discountSequenceID>, And<VendorDiscountSequence.discountSequenceID, NotEqual<Current<VendorDiscountSequence.discountSequenceID>>>>>>, Where<APDiscountLocation.discountID, Equal<Current<VendorDiscountSequence.discountID>>, And<APDiscountLocation.vendorID, Equal<Current<VendorDiscountSequence.vendorID>>, And<APDiscountLocation.locationID, Equal<Required<APDiscountLocation.locationID>>, And<VendorDiscountSequence.isActive, Equal<True>, And<VendorDiscountSequence.isPromotion, Equal<False>>>>>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, (object) locationID) != null)
      return false;
    return true;
  }

  private bool VerifyCombination_Location_Inventory()
  {
    bool flag = true;
    foreach (PXResult<DiscountItem, PX.Objects.IN.InventoryItem> row1 in this.Items.Select())
    {
      if (this.Items.Cache.GetStatus((object) (DiscountItem) row1) != PXEntryStatus.Deleted)
      {
        foreach (PXResult<APDiscountLocation, PX.Objects.CR.Location> row2 in this.Locations.Select())
        {
          if (this.Locations.Cache.GetStatus((object) (APDiscountLocation) row2) != PXEntryStatus.Deleted && !this.VerifyCombination_Location_Inventory(((APDiscountLocation) row2).LocationID, ((APDiscountLocation) row2).VendorID, ((DiscountItem) row1).InventoryID))
          {
            flag = false;
            this.Locations.Cache.RaiseExceptionHandling<APDiscountLocation.locationID>((object) (APDiscountLocation) row2, (object) ((PX.Objects.CR.Location) row2).LocationCD, (Exception) new PXSetPropertyException("Same Item cannot be listed more than once. Same item cannot belong to two or more active discount sequences of the same discount code", PXErrorLevel.Error));
            this.Items.Cache.RaiseExceptionHandling<DiscountItem.inventoryID>((object) (DiscountItem) row1, (object) ((PX.Objects.IN.InventoryItem) row1).InventoryCD, (Exception) new PXSetPropertyException("Same Item cannot be listed more than once. Same item cannot belong to two or more active discount sequences of the same discount code", PXErrorLevel.Error));
          }
        }
      }
    }
    return flag;
  }

  private bool VerifyCombination_Location_Inventory(int? locationID, int? vendorID, int? itemID)
  {
    if (this.Sequence.Current.IsPromotion.GetValueOrDefault())
    {
      if ((APDiscountLocation) PXSelectBase<APDiscountLocation, PXSelectReadonly2<APDiscountLocation, InnerJoin<DiscountItem, On<APDiscountLocation.discountID, Equal<DiscountItem.discountID>, And<APDiscountLocation.discountSequenceID, Equal<DiscountItem.discountSequenceID>>>, InnerJoin<PX.Objects.AR.DiscountSequence, On<APDiscountLocation.discountID, Equal<PX.Objects.AR.DiscountSequence.discountID>, And<APDiscountLocation.discountSequenceID, Equal<PX.Objects.AR.DiscountSequence.discountSequenceID>, And<PX.Objects.AR.DiscountSequence.discountSequenceID, NotEqual<Current<PX.Objects.AR.DiscountSequence.discountSequenceID>>>>>, InnerJoin<APDiscountVendor, On<APDiscountLocation.vendorID, Equal<APDiscountVendor.vendorID>, And<APDiscountLocation.discountID, Equal<APDiscountVendor.discountID>, And<APDiscountLocation.discountSequenceID, Equal<APDiscountVendor.discountSequenceID>>>>>>>, Where<APDiscountLocation.locationID, Equal<Required<APDiscountLocation.locationID>>, And<APDiscountVendor.vendorID, Equal<Required<APDiscountVendor.vendorID>>, And<DiscountItem.inventoryID, Equal<Required<DiscountItem.inventoryID>>, And<PX.Objects.AR.DiscountSequence.isActive, Equal<True>, And<PX.Objects.AR.DiscountSequence.discountID, Equal<Current<PX.Objects.AR.DiscountSequence.discountID>>, And<PX.Objects.AR.DiscountSequence.isPromotion, Equal<True>, PX.Data.And<Where2<Where<PX.Objects.AR.DiscountSequence.startDate, Between<Current<PX.Objects.AR.DiscountSequence.startDate>, Current<PX.Objects.AR.DiscountSequence.endDate>>>, Or<PX.Objects.AR.DiscountSequence.endDate, Between<Current<PX.Objects.AR.DiscountSequence.startDate>, Current<PX.Objects.AR.DiscountSequence.endDate>>>>>>>>>>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, (object) locationID, (object) vendorID, (object) itemID) != null)
        return false;
    }
    else if ((APDiscountLocation) PXSelectBase<APDiscountLocation, PXSelectReadonly2<APDiscountLocation, InnerJoin<DiscountItem, On<APDiscountLocation.discountID, Equal<DiscountItem.discountID>, And<APDiscountLocation.discountSequenceID, Equal<DiscountItem.discountSequenceID>>>, InnerJoin<PX.Objects.AR.DiscountSequence, On<APDiscountLocation.discountID, Equal<PX.Objects.AR.DiscountSequence.discountID>, And<APDiscountLocation.discountSequenceID, Equal<PX.Objects.AR.DiscountSequence.discountSequenceID>, And<PX.Objects.AR.DiscountSequence.discountSequenceID, NotEqual<Current<PX.Objects.AR.DiscountSequence.discountSequenceID>>>>>, InnerJoin<APDiscountVendor, On<APDiscountLocation.vendorID, Equal<APDiscountVendor.vendorID>, And<APDiscountLocation.discountID, Equal<APDiscountVendor.discountID>, And<APDiscountLocation.discountSequenceID, Equal<APDiscountVendor.discountSequenceID>>>>>>>, Where<APDiscountLocation.locationID, Equal<Required<APDiscountLocation.locationID>>, And<APDiscountVendor.vendorID, Equal<Required<APDiscountVendor.vendorID>>, And<DiscountItem.inventoryID, Equal<Required<DiscountItem.inventoryID>>, And<PX.Objects.AR.DiscountSequence.isActive, Equal<True>, And<PX.Objects.AR.DiscountSequence.discountID, Equal<Current<PX.Objects.AR.DiscountSequence.discountID>>, And<PX.Objects.AR.DiscountSequence.discountID, Equal<Current<PX.Objects.AR.DiscountSequence.discountID>>, And<PX.Objects.AR.DiscountSequence.isPromotion, Equal<False>>>>>>>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, (object) locationID, (object) vendorID, (object) itemID) != null)
      return false;
    return true;
  }

  private void SetControlsState(PXCache sender, VendorDiscountSequence row, APDiscount discount)
  {
    if (row == null)
      return;
    PXAction<VendorDiscountSequence> updateDiscounts = this.updateDiscounts;
    bool? nullable;
    int num1;
    if (sender.GetStatus((object) row) == PXEntryStatus.Inserted && discount != null)
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
    updateDiscounts.SetEnabled(num1 != 0);
    PXCache cache1 = sender;
    VendorDiscountSequence data1 = row;
    nullable = row.IsPromotion;
    int num2 = nullable.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetEnabled<VendorDiscountSequence.endDate>(cache1, (object) data1, num2 != 0);
    PXCache cache2 = sender;
    nullable = row.IsPromotion;
    int num3 = nullable.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetRequired<VendorDiscountSequence.endDate>(cache2, num3 != 0);
    PXCache cache3 = sender;
    VendorDiscountSequence data2 = row;
    nullable = row.IsPromotion;
    int num4 = nullable.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetEnabled<VendorDiscountSequence.startDate>(cache3, (object) data2, num4 != 0);
    PXCache cache4 = sender;
    nullable = row.IsPromotion;
    int num5 = nullable.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetRequired<VendorDiscountSequence.startDate>(cache4, num5 != 0);
    PXCache cache5 = sender;
    VendorDiscountSequence data3 = row;
    nullable = row.IsPromotion;
    int num6 = nullable.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetVisible<VendorDiscountSequence.endDate>(cache5, (object) data3, num6 != 0);
    PXCache cache6 = sender;
    VendorDiscountSequence data4 = row;
    nullable = row.IsPromotion;
    int num7 = nullable.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetVisible<VendorDiscountSequence.startDate>(cache6, (object) data4, num7 != 0);
    PXUIFieldAttribute.SetEnabled<VendorDiscountSequence.prorate>(sender, (object) row, row.DiscountedFor == "A");
    PXUIFieldAttribute.SetEnabled<VendorDiscountSequence.breakBy>(sender, (object) row, discount != null && (discount.Type == "G" || discount.Type == "L"));
  }

  private void SetGridColumnsState(APDiscount discount)
  {
    if (this.Sequence.Current == null)
      return;
    PXUIFieldAttribute.SetVisible<DiscountDetail.amountTo>(this.Details.Cache, (object) null, true);
    PXUIFieldAttribute.SetVisible<DiscountDetail.amount>(this.Details.Cache, (object) null, true);
    PXUIFieldAttribute.SetVisible<DiscountDetail.pendingAmount>(this.Details.Cache, (object) null, true);
    PXUIFieldAttribute.SetVisible<DiscountDetail.lastAmount>(this.Details.Cache, (object) null, true);
    PXUIFieldAttribute.SetVisible<DiscountDetail.quantityTo>(this.Details.Cache, (object) null, true);
    PXUIFieldAttribute.SetVisible<DiscountDetail.quantity>(this.Details.Cache, (object) null, true);
    PXUIFieldAttribute.SetVisible<DiscountDetail.pendingQuantity>(this.Details.Cache, (object) null, true);
    PXUIFieldAttribute.SetVisible<DiscountDetail.lastQuantity>(this.Details.Cache, (object) null, true);
    PXUIFieldAttribute.SetVisible<DiscountDetail.discount>(this.Details.Cache, (object) null, true);
    PXUIFieldAttribute.SetVisible<DiscountDetail.lastDiscount>(this.Details.Cache, (object) null, true);
    PXUIFieldAttribute.SetVisible<DiscountDetail.pendingDiscount>(this.Details.Cache, (object) null, true);
    PXUIFieldAttribute.SetVisible<DiscountDetail.discountPercent>(this.Details.Cache, (object) null, true);
    PXUIFieldAttribute.SetVisible<DiscountDetail.lastDiscountPercent>(this.Details.Cache, (object) null, true);
    PXUIFieldAttribute.SetVisible<DiscountDetail.pendingDiscountPercent>(this.Details.Cache, (object) null, true);
    PXUIFieldAttribute.SetVisible<DiscountDetail.freeItemQty>(this.Details.Cache, (object) null, true);
    PXUIFieldAttribute.SetVisible<DiscountDetail.pendingFreeItemQty>(this.Details.Cache, (object) null, true);
    PXUIFieldAttribute.SetVisible<DiscountDetail.lastFreeItemQty>(this.Details.Cache, (object) null, true);
    PXUIFieldAttribute.SetVisible<DiscountDetail.startDate>(this.Details.Cache, (object) null, true);
    PXUIFieldAttribute.SetVisible<DiscountDetail.lastDate>(this.Details.Cache, (object) null, true);
    PXUIFieldAttribute.SetVisible<DiscountItem.amount>(this.Items.Cache, (object) null, true);
    PXUIFieldAttribute.SetVisible<DiscountItem.quantity>(this.Items.Cache, (object) null, true);
    PXUIFieldAttribute.SetVisible<DiscountItem.uOM>(this.Items.Cache, (object) null, true);
    if (this.Sequence.Current.DiscountedFor != "F")
    {
      PXUIFieldAttribute.SetVisible<DiscountDetail.freeItemQty>(this.Details.Cache, (object) null, false);
      PXUIFieldAttribute.SetVisible<DiscountDetail.lastFreeItemQty>(this.Details.Cache, (object) null, false);
      PXUIFieldAttribute.SetVisible<DiscountDetail.pendingFreeItemQty>(this.Details.Cache, (object) null, false);
    }
    if (this.Sequence.Current.BreakBy == "Q")
    {
      PXUIFieldAttribute.SetVisible<DiscountDetail.amount>(this.Details.Cache, (object) null, false);
      PXUIFieldAttribute.SetVisible<DiscountDetail.amountTo>(this.Details.Cache, (object) null, false);
      PXUIFieldAttribute.SetVisible<DiscountDetail.pendingAmount>(this.Details.Cache, (object) null, false);
      PXUIFieldAttribute.SetVisible<DiscountDetail.lastAmount>(this.Details.Cache, (object) null, false);
      PXUIFieldAttribute.SetVisible<DiscountItem.amount>(this.Items.Cache, (object) null, false);
    }
    else
    {
      PXUIFieldAttribute.SetVisible<DiscountDetail.quantity>(this.Details.Cache, (object) null, false);
      PXUIFieldAttribute.SetVisible<DiscountDetail.quantityTo>(this.Details.Cache, (object) null, false);
      PXUIFieldAttribute.SetVisible<DiscountDetail.pendingQuantity>(this.Details.Cache, (object) null, false);
      PXUIFieldAttribute.SetVisible<DiscountDetail.lastQuantity>(this.Details.Cache, (object) null, false);
      PXUIFieldAttribute.SetVisible<DiscountItem.quantity>(this.Items.Cache, (object) null, false);
      PXUIFieldAttribute.SetVisible<DiscountItem.uOM>(this.Items.Cache, (object) null, false);
    }
    bool? isPromotion = this.Sequence.Current.IsPromotion;
    if (isPromotion.GetValueOrDefault())
    {
      PXUIFieldAttribute.SetVisible<DiscountDetail.lastAmount>(this.Details.Cache, (object) null, false);
      PXUIFieldAttribute.SetVisible<DiscountDetail.lastDiscount>(this.Details.Cache, (object) null, false);
      PXUIFieldAttribute.SetVisible<DiscountDetail.lastDiscountPercent>(this.Details.Cache, (object) null, false);
      PXUIFieldAttribute.SetVisible<DiscountDetail.lastFreeItemQty>(this.Details.Cache, (object) null, false);
      PXUIFieldAttribute.SetVisible<DiscountDetail.lastQuantity>(this.Details.Cache, (object) null, false);
      PXUIFieldAttribute.SetVisible<DiscountDetail.pendingAmount>(this.Details.Cache, (object) null, false);
      PXUIFieldAttribute.SetVisible<DiscountDetail.pendingDiscount>(this.Details.Cache, (object) null, false);
      PXUIFieldAttribute.SetVisible<DiscountDetail.pendingDiscountPercent>(this.Details.Cache, (object) null, false);
      PXUIFieldAttribute.SetVisible<DiscountDetail.pendingFreeItemQty>(this.Details.Cache, (object) null, false);
      PXUIFieldAttribute.SetVisible<DiscountDetail.pendingQuantity>(this.Details.Cache, (object) null, false);
      PXUIFieldAttribute.SetVisible<DiscountDetail.startDate>(this.Details.Cache, (object) null, false);
      PXUIFieldAttribute.SetVisible<DiscountDetail.lastDate>(this.Details.Cache, (object) null, false);
    }
    if (this.Sequence.Current.DiscountedFor == "A")
    {
      PXUIFieldAttribute.SetVisible<DiscountDetail.discountPercent>(this.Details.Cache, (object) null, false);
      PXUIFieldAttribute.SetVisible<DiscountDetail.lastDiscountPercent>(this.Details.Cache, (object) null, false);
      PXUIFieldAttribute.SetVisible<DiscountDetail.pendingDiscountPercent>(this.Details.Cache, (object) null, false);
    }
    if (this.Sequence.Current.DiscountedFor == "P")
    {
      PXUIFieldAttribute.SetVisible<DiscountDetail.discount>(this.Details.Cache, (object) null, false);
      PXUIFieldAttribute.SetVisible<DiscountDetail.lastDiscount>(this.Details.Cache, (object) null, false);
      PXUIFieldAttribute.SetVisible<DiscountDetail.pendingDiscount>(this.Details.Cache, (object) null, false);
    }
    if (this.Sequence.Current.DiscountedFor == "F")
    {
      PXUIFieldAttribute.SetVisible<DiscountDetail.discount>(this.Details.Cache, (object) null, false);
      PXUIFieldAttribute.SetVisible<DiscountDetail.lastDiscount>(this.Details.Cache, (object) null, false);
      PXUIFieldAttribute.SetVisible<DiscountDetail.pendingDiscount>(this.Details.Cache, (object) null, false);
      PXUIFieldAttribute.SetVisible<DiscountDetail.discountPercent>(this.Details.Cache, (object) null, false);
      PXUIFieldAttribute.SetVisible<DiscountDetail.lastDiscountPercent>(this.Details.Cache, (object) null, false);
      PXUIFieldAttribute.SetVisible<DiscountDetail.pendingDiscountPercent>(this.Details.Cache, (object) null, false);
    }
    isPromotion = this.Sequence.Current.IsPromotion;
    if (!isPromotion.GetValueOrDefault())
      return;
    PXUIFieldAttribute.SetEnabled<DiscountDetail.amount>(this.Details.Cache, (object) null, true);
    PXUIFieldAttribute.SetEnabled<DiscountDetail.quantity>(this.Details.Cache, (object) null, true);
    PXUIFieldAttribute.SetEnabled<DiscountDetail.discount>(this.Details.Cache, (object) null, true);
    PXUIFieldAttribute.SetEnabled<DiscountDetail.discountPercent>(this.Details.Cache, (object) null, true);
    PXUIFieldAttribute.SetEnabled<DiscountDetail.freeItemQty>(this.Details.Cache, (object) null, true);
  }

  private static string IncNumber(string str, int count)
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

  public override void Persist()
  {
    if (!this.RunVerification())
      throw new PXException("One or more validations failed for the given discount sequence. Please fix the errors and try again.");
    APDiscountSequenceMaint.APDiscountEx apDiscountEx = (APDiscountSequenceMaint.APDiscountEx) PXSelectBase<APDiscountSequenceMaint.APDiscountEx, PXSelect<APDiscountSequenceMaint.APDiscountEx, Where<APDiscountSequenceMaint.APDiscountEx.discountID, Equal<Current<VendorDiscountSequence.discountID>>>>.Config>.Select((PXGraph) this);
    if (apDiscountEx != null && this.Sequence.Current != null && apDiscountEx.IsAutoNumber.GetValueOrDefault() && this.Sequence.Cache.GetStatus((object) this.Sequence.Current) == PXEntryStatus.Inserted)
    {
      string str = string.IsNullOrEmpty(apDiscountEx.LastNumber) ? "0000000000" : apDiscountEx.LastNumber;
      if (!char.IsDigit(str[str.Length - 1]))
        str = $"{str.Substring(1, 6)}0000";
      apDiscountEx.LastNumber = APDiscountSequenceMaint.IncNumber(str, 1);
      this.Discount.Update(apDiscountEx);
    }
    if ((APDiscountVendor) PXSelectBase<APDiscountVendor, PXSelect<APDiscountVendor, Where<APDiscountVendor.discountID, Equal<Current<VendorDiscountSequence.discountID>>, And<APDiscountVendor.discountSequenceID, Equal<Current<VendorDiscountSequence.discountSequenceID>>, And<APDiscountVendor.vendorID, Equal<Current<VendorDiscountSequence.vendorID>>>>>>.Config>.Select((PXGraph) this) == null && this.Sequence.Current != null)
      this.Vendors.Update(new APDiscountVendor()
      {
        VendorID = this.Sequence.Current.VendorID,
        DiscountID = this.Sequence.Current.DiscountID,
        DiscountSequenceID = this.Sequence.Current.DiscountSequenceID
      });
    base.Persist();
  }

  [PXHidden]
  [Serializable]
  public class APDiscountEx : APDiscount
  {
    [PXBool]
    [PXUIField(Visibility = PXUIVisibility.Visible)]
    public virtual bool? ShowListOfItems
    {
      [PXDependsOnFields(new System.Type[] {typeof (APDiscount.applicableTo)})] get
      {
        return new bool?(this.ApplicableTo == "VI" || this.ApplicableTo == "LI");
      }
      set
      {
      }
    }

    [PXBool]
    [PXUIField(Visibility = PXUIVisibility.Visible)]
    public virtual bool? ShowLocations
    {
      [PXDependsOnFields(new System.Type[] {typeof (APDiscount.applicableTo)})] get
      {
        return new bool?(this.ApplicableTo == "VL" || this.ApplicableTo == "LI");
      }
      set
      {
      }
    }

    [PXBool]
    [PXUIField(Visibility = PXUIVisibility.Visible)]
    public virtual bool? ShowInventoryPriceClass
    {
      [PXDependsOnFields(new System.Type[] {typeof (APDiscount.applicableTo)})] get
      {
        return new bool?(this.ApplicableTo == "VP");
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
      APDiscountSequenceMaint.APDiscountEx.discountID>
    {
    }

    public abstract class showListOfItems : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      APDiscountSequenceMaint.APDiscountEx.showListOfItems>
    {
    }

    public abstract class showLocations : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      APDiscountSequenceMaint.APDiscountEx.showLocations>
    {
    }

    public abstract class showInventoryPriceClass : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      APDiscountSequenceMaint.APDiscountEx.showInventoryPriceClass>
    {
    }
  }

  [Serializable]
  public class UpdateSettingsFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    protected System.DateTime? _FilterDate;

    [PXDefault(typeof (AccessInfo.businessDate))]
    [PXDate]
    [PXUIField(DisplayName = "Filter Date", Required = true)]
    public virtual System.DateTime? FilterDate
    {
      get => this._FilterDate;
      set => this._FilterDate = value;
    }

    public abstract class filterDate : 
      BqlType<
      #nullable enable
      IBqlDateTime, System.DateTime>.Field<
      #nullable disable
      APDiscountSequenceMaint.UpdateSettingsFilter.filterDate>
    {
    }
  }
}
