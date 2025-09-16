// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.ARUpdateDiscounts
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.Common.Discount;
using PX.Objects.GL;
using System;
using System.Collections;
using System.Collections.Generic;

#nullable enable
namespace PX.Objects.AR;

[TableAndChartDashboardType]
public class ARUpdateDiscounts : PXGraph<
#nullable disable
ARUpdateDiscounts>
{
  public PXCancel<ARUpdateDiscounts.ItemFilter> Cancel;
  public PXFilter<ARUpdateDiscounts.ItemFilter> Filter;
  [PXFilterable(new Type[] {})]
  public PXFilteredProcessing<ARUpdateDiscounts.SelectedItem, ARUpdateDiscounts.ItemFilter> Items;

  public virtual IEnumerable items()
  {
    ARUpdateDiscounts arUpdateDiscounts = this;
    if (((PXSelectBase<ARUpdateDiscounts.ItemFilter>) arUpdateDiscounts.Filter).Current != null)
    {
      bool found = false;
      foreach (ARUpdateDiscounts.SelectedItem selectedItem in ((PXSelectBase) arUpdateDiscounts.Items).Cache.Inserted)
      {
        found = true;
        yield return (object) selectedItem;
      }
      if (!found)
      {
        List<string> added = new List<string>();
        foreach (PXResult<DiscountSequence> pxResult in PXSelectBase<DiscountSequence, PXSelect<DiscountSequence, Where<DiscountSequence.startDate, LessEqual<Current<ARUpdateDiscounts.ItemFilter.pendingDiscountDate>>, And<DiscountSequence.isPromotion, Equal<False>, And<DiscountSequence.isActive, Equal<True>>>>>.Config>.Select((PXGraph) arUpdateDiscounts, Array.Empty<object>()))
        {
          DiscountSequence discountSequence = PXResult<DiscountSequence>.op_Implicit(pxResult);
          added.Add($"{discountSequence.DiscountID}.{discountSequence.DiscountSequenceID}");
          yield return (object) ((PXSelectBase<ARUpdateDiscounts.SelectedItem>) arUpdateDiscounts.Items).Insert(new ARUpdateDiscounts.SelectedItem()
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
        foreach (PXResult<DiscountDetail> pxResult in PXSelectBase<DiscountDetail, PXSelectGroupBy<DiscountDetail, Where<DiscountDetail.startDate, LessEqual<Current<ARUpdateDiscounts.ItemFilter.pendingDiscountDate>>>, Aggregate<GroupBy<DiscountDetail.discountID, GroupBy<DiscountDetail.discountSequenceID>>>>.Config>.Select((PXGraph) arUpdateDiscounts, Array.Empty<object>()))
        {
          DiscountDetail discountDetail = PXResult<DiscountDetail>.op_Implicit(pxResult);
          if (!added.Contains($"{discountDetail.DiscountID}.{discountDetail.DiscountSequenceID}"))
          {
            DiscountSequence discountSequence = PXResultset<DiscountSequence>.op_Implicit(PXSelectBase<DiscountSequence, PXSelect<DiscountSequence, Where<DiscountSequence.discountID, Equal<Required<DiscountSequence.discountID>>, And<DiscountSequence.discountSequenceID, Equal<Required<DiscountSequence.discountSequenceID>>, And<DiscountSequence.isActive, Equal<True>>>>>.Config>.Select((PXGraph) arUpdateDiscounts, new object[2]
            {
              (object) discountDetail.DiscountID,
              (object) discountDetail.DiscountSequenceID
            }));
            if (discountSequence != null)
            {
              bool? isPromotion = discountSequence.IsPromotion;
              bool flag = false;
              if (isPromotion.GetValueOrDefault() == flag & isPromotion.HasValue)
                yield return (object) ((PXSelectBase<ARUpdateDiscounts.SelectedItem>) arUpdateDiscounts.Items).Insert(new ARUpdateDiscounts.SelectedItem()
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
        ((PXSelectBase) arUpdateDiscounts.Items).Cache.IsDirty = false;
      }
    }
  }

  public ARUpdateDiscounts()
  {
    ((PXProcessingBase<ARUpdateDiscounts.SelectedItem>) this.Items).SetSelected<ARUpdateDiscounts.SelectedItem.selected>();
    ((PXProcessing<ARUpdateDiscounts.SelectedItem>) this.Items).SetProcessCaption("Process");
    ((PXProcessing<ARUpdateDiscounts.SelectedItem>) this.Items).SetProcessAllCaption("Process All");
  }

  protected virtual void ItemFilter_RowUpdated(PXCache cache, PXRowUpdatedEventArgs e)
  {
    ((PXSelectBase) this.Items).Cache.Clear();
  }

  protected virtual void ItemFilter_RowSelected(PXCache cache, PXRowSelectedEventArgs e)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    ARUpdateDiscounts.\u003C\u003Ec__DisplayClass6_0 cDisplayClass60 = new ARUpdateDiscounts.\u003C\u003Ec__DisplayClass6_0();
    ARUpdateDiscounts.ItemFilter current = ((PXSelectBase<ARUpdateDiscounts.ItemFilter>) this.Filter).Current;
    // ISSUE: reference to a compiler-generated field
    cDisplayClass60.date = ((PXSelectBase<ARUpdateDiscounts.ItemFilter>) this.Filter).Current.PendingDiscountDate;
    // ISSUE: method pointer
    ((PXProcessingBase<ARUpdateDiscounts.SelectedItem>) this.Items).SetProcessDelegate<UpdateDiscountProcess>(new PXProcessingBase<ARUpdateDiscounts.SelectedItem>.ProcessItemDelegate<UpdateDiscountProcess>((object) cDisplayClass60, __methodptr(\u003CItemFilter_RowSelected\u003Eb__0)));
  }

  public static void UpdateDiscount(
    UpdateDiscountProcess graph,
    ARUpdateDiscounts.SelectedItem item,
    DateTime? filterDate)
  {
    graph.UpdateDiscount(item, filterDate);
  }

  public static void UpdateDiscount(
    string discountID,
    string discountSequenceID,
    DateTime? filterDate)
  {
    PXGraph.CreateInstance<UpdateDiscountProcess>().UpdateDiscount(discountID, discountSequenceID, filterDate);
  }

  [Serializable]
  public class ItemFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    protected DateTime? _PendingDiscountDate;

    [PXDBDate]
    [PXDefault(typeof (AccessInfo.businessDate))]
    [PXUIField(DisplayName = "Update Discounts with Pending Date Before")]
    public virtual DateTime? PendingDiscountDate
    {
      get => this._PendingDiscountDate;
      set => this._PendingDiscountDate = value;
    }

    public abstract class pendingDiscountDate : 
      BqlType<
      #nullable enable
      IBqlDateTime, DateTime>.Field<
      #nullable disable
      ARUpdateDiscounts.ItemFilter.pendingDiscountDate>
    {
    }
  }

  [Serializable]
  public class SelectedItem : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    protected bool? _Selected = new bool?(false);
    protected string _DiscountID;
    protected string _DiscountSequenceID;
    protected string _Description;
    protected string _DiscountedFor;
    protected string _BreakBy;
    protected bool? _IsPromotion;
    protected bool? _IsActive;
    protected DateTime? _StartDate;
    protected DateTime? _EndDate;

    [PXBool]
    [PXUIField(DisplayName = "Selected")]
    public virtual bool? Selected
    {
      get => this._Selected;
      set => this._Selected = value;
    }

    [PXDBString(10, IsUnicode = true, IsKey = true)]
    [PXUIField]
    public virtual string DiscountID
    {
      get => this._DiscountID;
      set => this._DiscountID = value;
    }

    [PXDBString(10, IsUnicode = true, IsKey = true)]
    [PXUIField]
    public virtual string DiscountSequenceID
    {
      get => this._DiscountSequenceID;
      set => this._DiscountSequenceID = value;
    }

    [PXDBString(250, IsUnicode = true)]
    [PXUIField]
    public virtual string Description
    {
      get => this._Description;
      set => this._Description = value;
    }

    [PXDBString(1, IsFixed = true)]
    [PXDefault("P")]
    [DiscountOption.List]
    [PXUIField]
    public virtual string DiscountedFor
    {
      get => this._DiscountedFor;
      set => this._DiscountedFor = value;
    }

    [PXDBString(1, IsFixed = true)]
    [PXDefault("A")]
    [BreakdownType.List]
    [PXUIField]
    public virtual string BreakBy
    {
      get => this._BreakBy;
      set => this._BreakBy = value;
    }

    [PXDBBool]
    [PXDefault(false)]
    [PXUIField]
    public virtual bool? IsPromotion
    {
      get => this._IsPromotion;
      set => this._IsPromotion = value;
    }

    [PXDBBool]
    [PXDefault(true)]
    [PXUIField]
    public virtual bool? IsActive
    {
      get => this._IsActive;
      set => this._IsActive = value;
    }

    [PXDBDate]
    [PXDefault]
    [PXUIField]
    public virtual DateTime? StartDate
    {
      get => this._StartDate;
      set => this._StartDate = value;
    }

    [PXDBDate]
    [PXUIField]
    public virtual DateTime? EndDate
    {
      get => this._EndDate;
      set => this._EndDate = value;
    }

    public abstract class selected : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      ARUpdateDiscounts.SelectedItem.selected>
    {
    }

    public abstract class discountID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ARUpdateDiscounts.SelectedItem.discountID>
    {
    }

    public abstract class discountSequenceID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ARUpdateDiscounts.SelectedItem.discountSequenceID>
    {
    }

    public abstract class description : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ARUpdateDiscounts.SelectedItem.description>
    {
    }

    public abstract class discountedFor : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ARUpdateDiscounts.SelectedItem.discountedFor>
    {
    }

    public abstract class breakBy : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ARUpdateDiscounts.SelectedItem.breakBy>
    {
    }

    public abstract class isPromotion : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      ARUpdateDiscounts.SelectedItem.isPromotion>
    {
    }

    public abstract class isActive : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      ARUpdateDiscounts.SelectedItem.isActive>
    {
    }

    public abstract class startDate : 
      BqlType<
      #nullable enable
      IBqlDateTime, DateTime>.Field<
      #nullable disable
      ARUpdateDiscounts.SelectedItem.startDate>
    {
    }

    public abstract class endDate : 
      BqlType<
      #nullable enable
      IBqlDateTime, DateTime>.Field<
      #nullable disable
      ARUpdateDiscounts.SelectedItem.endDate>
    {
    }
  }
}
