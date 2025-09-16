// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.DAC.Unbound.SOOrderRelatedReturnsSPFilter
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CS;
using System;

#nullable enable
namespace PX.Objects.SO.DAC.Unbound;

/// <summary>
/// The DAC that is used as a filter in Return Documents Related to Sales Order side panel inquiry of the sales orders form.
/// </summary>
[PXCacheName("Return Documents Related to Sales Order Filter")]
public class SOOrderRelatedReturnsSPFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  /// <summary>The type of the sales order.</summary>
  [PXDBString(2, IsFixed = true)]
  [PXUIField(DisplayName = "Order Type")]
  [PXSelector(typeof (Search<SOOrderType.orderType, Where<BqlOperand<SOOrderType.behavior, IBqlString>.IsIn<SOBehavior.sO, SOBehavior.iN, SOBehavior.mO, SOBehavior.rM>>>))]
  public virtual 
  #nullable disable
  string OrderType { get; set; }

  /// <summary>The number of the sales order.</summary>
  [PXDBString(15, IsUnicode = true)]
  [PXUIField(DisplayName = "Order Nbr.")]
  [PXSelector(typeof (Search<SOOrder.orderNbr, Where<SOOrder.orderType, Equal<Current<SOOrderRelatedReturnsSPFilter.orderType>>>>))]
  public virtual string OrderNbr { get; set; }

  /// <exclude />
  [PXDecimal(typeof (Search<CommonSetup.decPlQty>))]
  [PXUIField(DisplayName = "Qty. on Shipments", Enabled = false)]
  public virtual Decimal? ShippedQty { get; set; }

  /// <exclude />
  [PXDecimal(typeof (Search<CommonSetup.decPlQty>))]
  [PXUIField(DisplayName = "Qty. on Returns", Enabled = false)]
  public virtual Decimal? ReturnedQty { get; set; }

  public abstract class orderType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOOrderRelatedReturnsSPFilter.orderType>
  {
  }

  public abstract class orderNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOOrderRelatedReturnsSPFilter.orderNbr>
  {
  }

  public abstract class shippedQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOOrderRelatedReturnsSPFilter.shippedQty>
  {
  }

  public abstract class returnedQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOOrderRelatedReturnsSPFilter.returnedQty>
  {
  }
}
