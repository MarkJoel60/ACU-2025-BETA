// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.SOOrderInvoicesSPFilter
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.Objects.SO;

/// <summary>
/// The DAC that is used as a filter in invoices for sales orders side panel inquiry of the sales orders form.
/// </summary>
[PXCacheName("SO Order Invoices Filter")]
public class SOOrderInvoicesSPFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  /// <summary>The type of the sales order.</summary>
  [PXDBString(2, IsFixed = true)]
  [PXUIField(DisplayName = "Order Type")]
  [PXSelector(typeof (Search<SOOrderType.orderType, Where<BqlOperand<SOOrderType.behavior, IBqlString>.IsNotIn<SOBehavior.tR, SOBehavior.qT>>>))]
  public virtual 
  #nullable disable
  string OrderType { get; set; }

  /// <summary>The number of the sales order.</summary>
  [PXDBString(15, IsUnicode = true)]
  [PXUIField(DisplayName = "Order Nbr.")]
  [PXSelector(typeof (Search<SOOrder.orderNbr, Where<SOOrder.orderType, Equal<Current<SOOrderInvoicesSPFilter.orderType>>>>))]
  public virtual string OrderNbr { get; set; }

  public abstract class orderType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOOrderInvoicesSPFilter.orderType>
  {
  }

  public abstract class orderNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOOrderInvoicesSPFilter.orderNbr>
  {
  }
}
