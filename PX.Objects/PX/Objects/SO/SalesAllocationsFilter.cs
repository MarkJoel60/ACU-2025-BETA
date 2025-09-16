// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.SalesAllocationsFilter
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.AR;
using PX.Objects.Common.Bql;
using PX.Objects.CR;
using PX.Objects.IN;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace PX.Objects.SO;

/// <exclude />
[PXCacheName("Sales Allocations Filter")]
public class SalesAllocationsFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [SalesAllocationsFilter.action.List]
  [PXDBString(7)]
  [PXUIField(DisplayName = "Action", Required = true)]
  [PXDefault("None")]
  public virtual 
  #nullable disable
  string Action { get; set; }

  [SalesAllocationsFilter.selectBy.List]
  [PXDBString(11)]
  [PXUIField(DisplayName = "Select By", Required = true)]
  [PXDefault("ShipOn")]
  public virtual string SelectBy { get; set; }

  [PXDBDate]
  [PXUIField(DisplayName = "Start Date")]
  public virtual DateTime? StartDate { get; set; }

  [PXDBDate]
  [PXDefault(typeof (AccessInfo.businessDate))]
  [PXUIField(DisplayName = "End Date")]
  public virtual DateTime? EndDate { get; set; }

  [Site(Required = true)]
  public virtual int? SiteID { get; set; }

  [PXDBString]
  [PXStringList(MultiSelect = true)]
  [PXUIField(DisplayName = "Order Type")]
  public virtual string OrderType { get; set; }

  [PXDBString]
  [SalesAllocationsFilter.orderStatus.ListAttribute.WithExpired]
  [PXUIField(DisplayName = "Order Status")]
  public virtual string OrderStatus { get; set; }

  [PXDBShort]
  [PXUIField(DisplayName = "Order Priority")]
  public virtual short? Priority { get; set; }

  [PXDBString(15, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC")]
  [PXUIField(DisplayName = "Order Nbr.")]
  [PXSelector(typeof (FbqlSelect<SelectFromBase<SOOrder, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<SOOrderType>.On<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<SOOrderType.orderType, Equal<SOOrder.orderType>>>>, And<BqlOperand<SOOrderType.behavior, IBqlString>.IsInSequence<SalesAllocationsFilter.orderType.behaviorList>>>>.And<BqlOperand<SOOrderType.active, IBqlBool>.IsEqual<True>>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<SOOrder.status, In3<AllowedValues<SalesAllocationsFilter.orderStatus>>>>>, And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Current<SalesAllocationsFilter.priority>, IsNull>>>>.Or<BqlOperand<SOOrder.priority, IBqlShort>.IsEqual<BqlField<SalesAllocationsFilter.priority, IBqlShort>.FromCurrent>>>>, And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Current<SalesAllocationsFilter.orderType>, IsNull>>>>.Or<BqlOperand<SOOrder.orderType, IBqlString>.IsInSequence<CurrentSelectedValues<SalesAllocationsFilter.orderType>>>>>, And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Current<SalesAllocationsFilter.orderStatus>, IsNull>>>>.Or<BqlOperand<SOOrder.status, IBqlString>.IsInSequence<CurrentSelectedValues<SalesAllocationsFilter.orderStatus>>>>>, And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Current<SalesAllocationsFilter.customerID>, IsNull>>>>.Or<BqlOperand<SOOrder.customerID, IBqlInt>.IsEqual<BqlField<SalesAllocationsFilter.customerID, IBqlInt>.FromCurrent>>>>>.And<Exists<SelectFromBase<SOOrderTypeOperation, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<SOOrderTypeOperation.orderType, Equal<SOOrderType.orderType>>>>, And<BqlOperand<SOOrderTypeOperation.operation, IBqlString>.IsEqual<SOOperation.issue>>>>.And<BqlOperand<SOOrderTypeOperation.active, IBqlBool>.IsEqual<True>>>>>>, SOOrder>.SearchFor<SOOrder.orderNbr>))]
  [PXFormula(typeof (Default<SalesAllocationsFilter.action>))]
  [PXFormula(typeof (Default<SalesAllocationsFilter.priority, SalesAllocationsFilter.orderType, SalesAllocationsFilter.orderStatus, SalesAllocationsFilter.customerID>))]
  public virtual string OrderNbr { get; set; }

  [PXDBString(10, IsUnicode = true)]
  [PXSelector(typeof (FbqlSelect<SelectFromBase<CustomerClass, TypeArrayOf<IFbqlJoin>.Empty>, CustomerClass>.SearchFor<CustomerClass.customerClassID>), DescriptionField = typeof (CustomerClass.descr), CacheGlobal = true)]
  [PXUIField(DisplayName = "Customer Class")]
  public virtual string CustomerClassID { get; set; }

  [CustomerActive(typeof (Search<BAccountR.bAccountID, Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Current<SalesAllocationsFilter.customerClassID>, IsNull>>>>.Or<BqlOperand<PX.Objects.AR.Customer.customerClassID, IBqlString>.IsEqual<BqlField<SalesAllocationsFilter.customerClassID, IBqlString>.FromCurrent>>>>))]
  [PXFormula(typeof (Default<SalesAllocationsFilter.customerClassID>))]
  public virtual int? CustomerID { get; set; }

  [StockItem]
  [PXRestrictor(typeof (Where<PX.Objects.IN.InventoryItem.itemStatus, NotEqual<InventoryItemStatus.noSales>>), "The inventory item is {0}.", new System.Type[] {typeof (PX.Objects.IN.InventoryItem.itemStatus)}, ShowWarning = true)]
  public virtual int? InventoryID { get; set; }

  public abstract class action : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SalesAllocationsFilter.action>
  {
    public const string None = "None";
    public const string AllocateSalesOrders = "Alloc";
    public const string DeallocateSalesOrders = "Dealloc";

    public class ListAttribute : PXStringListAttribute
    {
      public ListAttribute()
        : base(new (string, string)[3]
        {
          ("None", "<SELECT>"),
          ("Alloc", "Allocate Sales Orders"),
          ("Dealloc", "Deallocate Sales Orders")
        })
      {
      }
    }

    public class allocateSalesOrders : 
      BqlType<
      #nullable enable
      IBqlString, string>.Constant<
      #nullable disable
      SalesAllocationsFilter.action.allocateSalesOrders>
    {
      public allocateSalesOrders()
        : base("Alloc")
      {
      }
    }

    public class deallocateSalesOrders : 
      BqlType<
      #nullable enable
      IBqlString, string>.Constant<
      #nullable disable
      SalesAllocationsFilter.action.deallocateSalesOrders>
    {
      public deallocateSalesOrders()
        : base("Dealloc")
      {
      }
    }
  }

  public abstract class selectBy : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SalesAllocationsFilter.selectBy>
  {
    public const string OrderDate = "OrderDate";
    public const string CancelBy = "CancelBy";
    public const string LineRequestedOn = "RequestedOn";
    public const string LineShipOn = "ShipOn";

    public class ListAttribute : PXStringListAttribute
    {
      public ListAttribute()
        : base(new (string, string)[4]
        {
          ("OrderDate", "Order Date"),
          ("RequestedOn", "Line Requested On"),
          ("ShipOn", "Line Ship On"),
          ("CancelBy", "Cancel By")
        })
      {
      }
    }

    public class orderDate : 
      BqlType<
      #nullable enable
      IBqlString, string>.Constant<
      #nullable disable
      SalesAllocationsFilter.selectBy.orderDate>
    {
      public orderDate()
        : base("OrderDate")
      {
      }
    }

    public class cancelBy : 
      BqlType<
      #nullable enable
      IBqlString, string>.Constant<
      #nullable disable
      SalesAllocationsFilter.selectBy.cancelBy>
    {
      public cancelBy()
        : base("CancelBy")
      {
      }
    }

    public class lineRequestedOn : 
      BqlType<
      #nullable enable
      IBqlString, string>.Constant<
      #nullable disable
      SalesAllocationsFilter.selectBy.lineRequestedOn>
    {
      public lineRequestedOn()
        : base("RequestedOn")
      {
      }
    }

    public class lineShipOn : 
      BqlType<
      #nullable enable
      IBqlString, string>.Constant<
      #nullable disable
      SalesAllocationsFilter.selectBy.lineShipOn>
    {
      public lineShipOn()
        : base("ShipOn")
      {
      }
    }
  }

  public abstract class startDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    SalesAllocationsFilter.startDate>
  {
  }

  public abstract class endDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    SalesAllocationsFilter.endDate>
  {
  }

  public abstract class siteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SalesAllocationsFilter.siteID>
  {
  }

  public abstract class orderType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SalesAllocationsFilter.orderType>
  {
    public class behaviorList : 
      SetOf.Strings.FilledWith<SOBehavior.bL, SOBehavior.sO, SOBehavior.tR, SOBehavior.rM>
    {
    }
  }

  public abstract class orderStatus : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SalesAllocationsFilter.orderStatus>
  {
    public class ListAttribute : PXStringListAttribute
    {
      private static (string, string)[] BaseValues = new (string, string)[6]
      {
        ("H", "On Hold"),
        ("R", "Credit Hold"),
        ("A", "Awaiting Payment"),
        ("E", "Pending Processing"),
        ("N", "Open"),
        ("B", "Back Order")
      };

      public ListAttribute()
        : base(SalesAllocationsFilter.orderStatus.ListAttribute.BaseValues)
      {
        this.MultiSelect = true;
      }

      protected ListAttribute(params (string, string)[] additionalValues)
        : base(((IEnumerable<(string, string)>) SalesAllocationsFilter.orderStatus.ListAttribute.BaseValues).Concat<(string, string)>((IEnumerable<(string, string)>) additionalValues).ToArray<(string, string)>())
      {
        this.MultiSelect = true;
      }

      public class WithExpiredAttribute : SalesAllocationsFilter.orderStatus.ListAttribute
      {
        public WithExpiredAttribute()
          : base(("D", "Expired"))
        {
        }
      }
    }

    public class list : 
      SetOf.Strings.FilledWith<SOOrderStatus.hold, SOOrderStatus.creditHold, SOOrderStatus.awaitingPayment, SOOrderStatus.pendingProcessing, SOOrderStatus.open, SOOrderStatus.backOrder>
    {
      public class withExpired : 
        SetOf.Strings.FilledWith<SOOrderStatus.hold, SOOrderStatus.creditHold, SOOrderStatus.awaitingPayment, SOOrderStatus.pendingProcessing, SOOrderStatus.open, SOOrderStatus.backOrder, SOOrderStatus.expired>
      {
      }
    }
  }

  public abstract class priority : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  SalesAllocationsFilter.priority>
  {
  }

  public abstract class orderNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SalesAllocationsFilter.orderNbr>
  {
  }

  public abstract class customerClassID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SalesAllocationsFilter.customerClassID>
  {
  }

  public abstract class customerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SalesAllocationsFilter.customerID>
  {
  }

  public abstract class inventoryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SalesAllocationsFilter.inventoryID>
  {
  }
}
