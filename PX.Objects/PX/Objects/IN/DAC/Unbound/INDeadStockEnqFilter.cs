// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.DAC.Unbound.INDeadStockEnqFilter
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Objects.CS;
using System;

#nullable enable
namespace PX.Objects.IN.DAC.Unbound;

[PXCacheName("IN Dead Stock Enquiry Filter")]
public class INDeadStockEnqFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [Site(DescriptionField = typeof (INSite.descr), DisplayName = "Warehouse", Required = true)]
  public virtual int? SiteID { get; set; }

  [PXInt]
  [PXUIField]
  [PXDimensionSelector("INITEMCLASS", typeof (Search<INItemClass.itemClassID>), typeof (INItemClass.itemClassCD), DescriptionField = typeof (INItemClass.descr), CacheGlobal = true)]
  public virtual int? ItemClassID { get; set; }

  [AnyInventory(typeof (Search<PX.Objects.IN.InventoryItem.inventoryID, Where<PX.Objects.IN.InventoryItem.stkItem, NotEqual<boolFalse>, And<Where<Match<Current<AccessInfo.userName>>>>>>), typeof (PX.Objects.IN.InventoryItem.inventoryCD), typeof (PX.Objects.IN.InventoryItem.descr))]
  public virtual int? InventoryID { get; set; }

  [PXString]
  [INDeadStockEnqFilter.selectBy.List]
  [PXDefault("Days")]
  [PXUIField(DisplayName = "Select by", Required = true)]
  public virtual 
  #nullable disable
  string SelectBy { get; set; }

  [PXInt]
  [PXUIField(DisplayName = "In Stock For")]
  [PXDefault(30)]
  [PXUIVisible(typeof (Where<INDeadStockEnqFilter.selectBy, Equal<INDeadStockEnqFilter.selectBy.days>>))]
  public virtual int? InStockDays { get; set; }

  [PXDate]
  [PXUIField(DisplayName = "In Stock Since")]
  [PXUIVisible(typeof (Where<INDeadStockEnqFilter.selectBy, Equal<INDeadStockEnqFilter.selectBy.date>>))]
  public virtual DateTime? InStockSince { get; set; }

  [PXInt]
  [PXUIField(DisplayName = "No Sales For")]
  [PXUIVisible(typeof (Where<INDeadStockEnqFilter.selectBy, Equal<INDeadStockEnqFilter.selectBy.days>>))]
  public virtual int? NoSalesDays { get; set; }

  [PXDate]
  [PXUIField(DisplayName = "No Sales Since")]
  [PXUIVisible(typeof (Where<INDeadStockEnqFilter.selectBy, Equal<INDeadStockEnqFilter.selectBy.date>>))]
  public virtual DateTime? NoSalesSince { get; set; }

  public abstract class siteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INDeadStockEnqFilter.siteID>
  {
  }

  public abstract class itemClassID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INDeadStockEnqFilter.itemClassID>
  {
  }

  public abstract class inventoryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INDeadStockEnqFilter.inventoryID>
  {
  }

  public abstract class selectBy : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INDeadStockEnqFilter.selectBy>
  {
    public const string Days = "Days";
    public const string Date = "Date";

    public class days : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    INDeadStockEnqFilter.selectBy.days>
    {
      public days()
        : base("Days")
      {
      }
    }

    public class date : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    INDeadStockEnqFilter.selectBy.days>
    {
      public date()
        : base("Date")
      {
      }
    }

    [PXLocalizable]
    public class Messages
    {
      public const string Days = "Days";
      public const string Date = "Date";
    }

    public class ListAttribute : PXStringListAttribute
    {
      public ListAttribute()
        : base(new string[2]{ "Days", "Date" }, new string[2]
        {
          "Days",
          "Date"
        })
      {
      }
    }
  }

  public abstract class inStockDays : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INDeadStockEnqFilter.inStockDays>
  {
  }

  public abstract class inStockSince : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    INDeadStockEnqFilter.inStockSince>
  {
  }

  public abstract class noSalesDays : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INDeadStockEnqFilter.noSalesDays>
  {
  }

  public abstract class noSalesSince : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    INDeadStockEnqFilter.noSalesSince>
  {
  }
}
