// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.DAC.Unbound.SOOrderRelatedReturnsSPResultLine
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.AR;
using PX.Objects.CS;
using PX.Objects.IN;
using System;

#nullable enable
namespace PX.Objects.SO.DAC.Unbound;

/// <exclude />
/// <summary>
/// The DAC that represent the result line of Return Documents Related to Sales Order side panel inquiry of the sales orders form.
/// </summary>
[PXCacheName("Return Documents by Line")]
public class SOOrderRelatedReturnsSPResultLine : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  /// <exclude />
  [PXString(2, IsFixed = true, IsKey = true)]
  [PXUIField(DisplayName = "Order Type")]
  public virtual 
  #nullable disable
  string OrderType { get; set; }

  /// <exclude />
  [PXString(15, IsUnicode = true, IsKey = true)]
  [PXUIField(DisplayName = "Order Nbr.")]
  public virtual string OrderNbr { get; set; }

  /// <exclude />
  [PXInt(IsKey = true)]
  [PXUIField(DisplayName = "Line Nbr.", Visible = false)]
  public virtual int? LineNbr { get; set; }

  /// <exclude />
  [PXString(2, IsFixed = true, IsKey = true)]
  public virtual string ReturnOrderType { get; set; }

  /// <exclude />
  [PXString(15, IsUnicode = true, IsKey = true)]
  public virtual string ReturnOrderNbr { get; set; }

  /// <exclude />
  [PXString(2, IsFixed = true)]
  public virtual string ReturnLineType { get; set; }

  /// <exclude />
  [PXString(2, IsFixed = true)]
  [PXUIField(DisplayName = "Return Order Type", Visible = false)]
  public virtual string DisplayReturnOrderType { get; set; }

  /// <exclude />
  [PXString(15, IsUnicode = true)]
  [PXUIField(DisplayName = "Return Order Nbr.")]
  [PXSelector(typeof (Search<PX.Objects.SO.SOOrder.orderNbr, Where<PX.Objects.SO.SOOrder.orderType, Equal<Current<SOOrderRelatedReturnsSPResultLine.displayReturnOrderType>>>>))]
  public virtual string DisplayReturnOrderNbr { get; set; }

  /// <exclude />
  [PXString(3, IsKey = true, IsFixed = true)]
  public virtual string ReturnInvoiceType { get; set; }

  /// <exclude />
  [PXString(15, IsUnicode = true, IsKey = true)]
  public virtual string ReturnInvoiceNbr { get; set; }

  /// <exclude />
  [PXString(3, IsFixed = true)]
  [ARDocType.List]
  [PXUIField(DisplayName = "Return Invoice Type", Visible = false)]
  public virtual string DisplayReturnInvoiceType { get; set; }

  /// <exclude />
  [PXString(15, IsUnicode = true)]
  [PXSelector(typeof (Search<PX.Objects.SO.SOInvoice.refNbr, Where<PX.Objects.SO.SOInvoice.docType, Equal<Current<SOOrderRelatedReturnsSPResultLine.displayReturnInvoiceType>>>>))]
  [PXUIField(DisplayName = "Return Invoice Nbr.")]
  public virtual string DisplayReturnInvoiceNbr { get; set; }

  /// <exclude />
  [AnyInventory]
  public virtual int? InventoryID { get; set; }

  /// <exclude />
  [PXString(6, IsUnicode = true)]
  [PXUIField(DisplayName = "Base UOM")]
  public virtual string BaseUnit { get; set; }

  /// <exclude />
  [PXDecimal(typeof (Search<CommonSetup.decPlQty>))]
  [PXUIField(DisplayName = "Qty. on Return")]
  public virtual Decimal? ReturnedQty { get; set; }

  public abstract class orderType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOOrderRelatedReturnsSPResultLine.orderType>
  {
  }

  public abstract class orderNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOOrderRelatedReturnsSPResultLine.orderNbr>
  {
  }

  public abstract class lineNbr : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    SOOrderRelatedReturnsSPResultLine.lineNbr>
  {
  }

  public abstract class returnOrderType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOOrderRelatedReturnsSPResultLine.returnOrderType>
  {
  }

  public abstract class returnOrderNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOOrderRelatedReturnsSPResultLine.returnOrderNbr>
  {
  }

  public abstract class returnLineType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOOrderRelatedReturnsSPResultLine.returnLineType>
  {
  }

  public abstract class displayReturnOrderType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOOrderRelatedReturnsSPResultLine.displayReturnOrderType>
  {
  }

  public abstract class displayReturnOrderNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOOrderRelatedReturnsSPResultLine.displayReturnOrderNbr>
  {
  }

  public abstract class returnInvoiceType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOOrderRelatedReturnsSPResultLine.returnInvoiceType>
  {
  }

  public abstract class returnInvoiceNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOOrderRelatedReturnsSPResultLine.returnInvoiceNbr>
  {
  }

  public abstract class displayReturnInvoiceType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOOrderRelatedReturnsSPResultLine.displayReturnInvoiceType>
  {
  }

  public abstract class displayReturnInvoiceNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOOrderRelatedReturnsSPResultLine.displayReturnInvoiceNbr>
  {
  }

  public abstract class inventoryID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    SOOrderRelatedReturnsSPResultLine.inventoryID>
  {
  }

  public abstract class baseUnit : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOOrderRelatedReturnsSPResultLine.baseUnit>
  {
  }

  public abstract class returnedQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOOrderRelatedReturnsSPResultLine.returnedQty>
  {
  }
}
