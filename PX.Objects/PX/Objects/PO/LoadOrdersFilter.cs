// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.LoadOrdersFilter
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.GL;
using System;

#nullable enable
namespace PX.Objects.PO;

[PXHidden]
public class LoadOrdersFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [Branch(typeof (LoadOrdersFilter.branchID), null, true, true, true, Required = false)]
  [PXDefault]
  public virtual int? BranchID { get; set; }

  [PXDBDate]
  [PXUIField]
  public virtual DateTime? FromDate { get; set; }

  [PXDBDate]
  [PXUIField]
  public virtual DateTime? ToDate { get; set; }

  [PXDBInt]
  [PXUIField]
  [PXDefault(999)]
  public virtual int? MaxNumberOfDocuments { get; set; }

  [PXDBString(15, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC")]
  [PXDefault]
  [PXUIField(DisplayName = "Start Order Nbr.", Required = false)]
  [PXSelector(typeof (Search<POOrder.orderNbr, Where<POOrder.status, In3<POOrderStatus.open, POOrderStatus.completed>, And<POOrder.orderType, In3<POOrderType.regularOrder, POOrderType.dropShip>>>>), new Type[] {typeof (POOrder.orderNbr), typeof (POOrder.orderDate), typeof (POOrder.status), typeof (POOrder.curyUnprepaidTotal), typeof (POOrder.curyLineTotal), typeof (POOrder.curyID)}, Filterable = true)]
  public virtual 
  #nullable disable
  string StartOrderNbr { get; set; }

  [PXDBString(15, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC")]
  [PXDefault]
  [PXUIField(DisplayName = "End Order Nbr.", Required = false)]
  [PXSelector(typeof (Search<POOrder.orderNbr, Where<POOrder.status, In3<POOrderStatus.open, POOrderStatus.completed>, And<POOrder.orderType, In3<POOrderType.regularOrder, POOrderType.dropShip>>>>), new Type[] {typeof (POOrder.orderNbr), typeof (POOrder.orderDate), typeof (POOrder.status), typeof (POOrder.curyUnprepaidTotal), typeof (POOrder.curyLineTotal), typeof (POOrder.curyID)}, Filterable = true)]
  public virtual string EndOrderNbr { get; set; }

  [PXDBInt]
  [PXIntList(new int[] {1, 2}, new string[] {"Order Date, Order Nbr.", "Order Nbr."})]
  [PXUIField(DisplayName = "Sort Order")]
  [PXDefault(1)]
  public virtual int? OrderBy { get; set; }

  public abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  LoadOrdersFilter.branchID>
  {
  }

  public abstract class fromDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  LoadOrdersFilter.fromDate>
  {
  }

  public abstract class toDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  LoadOrdersFilter.toDate>
  {
  }

  public abstract class maxNumberOfDocuments : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    LoadOrdersFilter.maxNumberOfDocuments>
  {
  }

  public abstract class startOrderNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    LoadOrdersFilter.startOrderNbr>
  {
  }

  public abstract class endOrderNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  LoadOrdersFilter.endOrderNbr>
  {
  }

  public abstract class orderBy : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  LoadOrdersFilter.orderBy>
  {
    public const int ByDate = 1;
    public const int ByNbr = 2;

    public class byDate : BqlType<
    #nullable enable
    IBqlInt, int>.Constant<
    #nullable disable
    LoadOrdersFilter.orderBy.byDate>
    {
      public byDate()
        : base(1)
      {
      }
    }

    public class byNbr : BqlType<
    #nullable enable
    IBqlInt, int>.Constant<
    #nullable disable
    LoadOrdersFilter.orderBy.byNbr>
    {
      public byNbr()
        : base(2)
      {
      }
    }
  }
}
