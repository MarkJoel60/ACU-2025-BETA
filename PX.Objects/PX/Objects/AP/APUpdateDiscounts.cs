// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.APUpdateDiscounts
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.AR;
using PX.Objects.GL;
using System;
using System.Collections;
using System.Collections.Generic;

#nullable enable
namespace PX.Objects.AP;

[TableAndChartDashboardType]
public class APUpdateDiscounts : PXGraph<
#nullable disable
APUpdateDiscounts>
{
  public PXCancel<APUpdateDiscounts.ItemFilter> Cancel;
  public PXFilter<APUpdateDiscounts.ItemFilter> Filter;
  [PXFilterable(new System.Type[] {})]
  public PXFilteredProcessing<ARUpdateDiscounts.SelectedItem, APUpdateDiscounts.ItemFilter> Items;
  public PXSelectJoin<VendorDiscountSequence, LeftJoin<Vendor, On<Vendor.bAccountID, Equal<VendorDiscountSequence.vendorID>>>, Where<VendorDiscountSequence.startDate, LessEqual<Current<APUpdateDiscounts.ItemFilter.pendingDiscountDate>>, And<VendorDiscountSequence.vendorID, Equal<Current<APUpdateDiscounts.ItemFilter.vendorID>>, And<VendorDiscountSequence.isPromotion, Equal<False>, And<VendorDiscountSequence.isActive, Equal<True>>>>>> NonPromotionSequences;
  public PXSelectJoin<VendorDiscountSequence, LeftJoin<Vendor, On<Vendor.bAccountID, Equal<VendorDiscountSequence.vendorID>>>, Where<VendorDiscountSequence.discountID, Equal<Required<VendorDiscountSequence.discountID>>, And<VendorDiscountSequence.discountSequenceID, Equal<Required<VendorDiscountSequence.discountSequenceID>>, And<VendorDiscountSequence.isActive, Equal<True>>>>> Sequences;

  public virtual IEnumerable items()
  {
    APUpdateDiscounts graph = this;
    if (graph.Filter.Current != null)
    {
      bool found = false;
      foreach (ARUpdateDiscounts.SelectedItem selectedItem in graph.Items.Cache.Inserted)
      {
        found = true;
        yield return (object) selectedItem;
      }
      if (!found)
      {
        List<string> added = new List<string>();
        foreach (PXResult<VendorDiscountSequence, Vendor> pxResult in graph.NonPromotionSequences.Select())
        {
          VendorDiscountSequence discountSequence = (VendorDiscountSequence) pxResult;
          added.Add($"{discountSequence.DiscountID}.{discountSequence.DiscountSequenceID}");
          yield return (object) graph.Items.Insert(new ARUpdateDiscounts.SelectedItem()
          {
            DiscountID = discountSequence.DiscountID,
            DiscountSequenceID = discountSequence.DiscountSequenceID,
            Description = discountSequence.Description,
            DiscountedFor = discountSequence.DiscountedFor,
            BreakBy = discountSequence.BreakBy,
            IsPromotion = discountSequence.IsPromotion,
            IsActive = discountSequence.IsActive,
            StartDate = discountSequence.StartDate,
            EndDate = discountSequence.UpdateDate
          });
        }
        foreach (PXResult<DiscountDetail> pxResult in PXSelectBase<DiscountDetail, PXSelectGroupBy<DiscountDetail, Where<DiscountDetail.startDate, LessEqual<Current<APUpdateDiscounts.ItemFilter.pendingDiscountDate>>>, Aggregate<GroupBy<DiscountDetail.discountID, GroupBy<DiscountDetail.discountSequenceID>>>>.Config>.Select((PXGraph) graph))
        {
          DiscountDetail discountDetail = (DiscountDetail) pxResult;
          if (!added.Contains($"{discountDetail.DiscountID}.{discountDetail.DiscountSequenceID}"))
          {
            VendorDiscountSequence discountSequence = (VendorDiscountSequence) graph.Sequences.Select((object) discountDetail.DiscountID, (object) discountDetail.DiscountSequenceID);
            if (discountSequence != null)
            {
              bool? isPromotion = discountSequence.IsPromotion;
              bool flag = false;
              if (isPromotion.GetValueOrDefault() == flag & isPromotion.HasValue)
                yield return (object) graph.Items.Insert(new ARUpdateDiscounts.SelectedItem()
                {
                  DiscountID = discountSequence.DiscountID,
                  DiscountSequenceID = discountSequence.DiscountSequenceID,
                  Description = discountSequence.Description,
                  DiscountedFor = discountSequence.DiscountedFor,
                  BreakBy = discountSequence.BreakBy,
                  IsPromotion = discountSequence.IsPromotion,
                  IsActive = discountSequence.IsActive,
                  StartDate = discountSequence.StartDate,
                  EndDate = discountSequence.UpdateDate
                });
            }
          }
        }
        graph.Items.Cache.IsDirty = false;
      }
    }
  }

  public APUpdateDiscounts()
  {
    this.Items.SetSelected<ARUpdateDiscounts.SelectedItem.selected>();
    this.Items.SetProcessCaption("Process");
    this.Items.SetProcessAllCaption("Process All");
  }

  protected virtual void ItemFilter_RowUpdated(PXCache cache, PXRowUpdatedEventArgs e)
  {
    this.Items.Cache.Clear();
  }

  protected virtual void ItemFilter_RowSelected(PXCache cache, PXRowSelectedEventArgs e)
  {
    APUpdateDiscounts.ItemFilter current = this.Filter.Current;
    System.DateTime? date = this.Filter.Current.PendingDiscountDate;
    this.Items.SetProcessDelegate<UpdateDiscountProcess>((PXProcessingBase<ARUpdateDiscounts.SelectedItem>.ProcessItemDelegate<UpdateDiscountProcess>) ((graph, item) => APUpdateDiscounts.UpdateDiscount(graph, item, date)));
  }

  public static void UpdateDiscount(
    UpdateDiscountProcess graph,
    ARUpdateDiscounts.SelectedItem item,
    System.DateTime? filterDate)
  {
    graph.UpdateDiscount(item, filterDate);
  }

  public static void UpdateDiscount(
    string discountID,
    string discountSequenceID,
    System.DateTime? filterDate)
  {
    PXGraph.CreateInstance<UpdateDiscountProcess>().UpdateDiscount(discountID, discountSequenceID, filterDate);
  }

  [Serializable]
  public class ItemFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    protected int? _VendorID;
    protected System.DateTime? _PendingDiscountDate;

    [Vendor]
    public virtual int? VendorID
    {
      get => this._VendorID;
      set => this._VendorID = value;
    }

    [PXDBDate]
    [PXDefault(typeof (AccessInfo.businessDate))]
    [PXUIField(DisplayName = "Update Discounts with Pending Date Before")]
    public virtual System.DateTime? PendingDiscountDate
    {
      get => this._PendingDiscountDate;
      set => this._PendingDiscountDate = value;
    }

    public abstract class vendorID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      APUpdateDiscounts.ItemFilter.vendorID>
    {
    }

    public abstract class pendingDiscountDate : 
      BqlType<
      #nullable enable
      IBqlDateTime, System.DateTime>.Field<
      #nullable disable
      APUpdateDiscounts.ItemFilter.pendingDiscountDate>
    {
    }
  }
}
