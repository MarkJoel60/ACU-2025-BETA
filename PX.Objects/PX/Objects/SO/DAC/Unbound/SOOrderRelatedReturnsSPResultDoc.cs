// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.DAC.Unbound.SOOrderRelatedReturnsSPResultDoc
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.AP;

#nullable enable
namespace PX.Objects.SO.DAC.Unbound;

/// <summary>
/// The DAC that represent the result line of Return Documents Related to Sales Order side panel inquiry of the sales orders form.
/// </summary>
[PXCacheName("Related Return Documents")]
public class SOOrderRelatedReturnsSPResultDoc : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  /// <exclude />
  [PXDBInt(IsKey = true)]
  [PXUIField]
  public virtual int? GridLineNbr { get; set; }

  /// <exclude />
  [PXString(2, IsFixed = true)]
  [PXUIField(DisplayName = "Order Type")]
  public virtual 
  #nullable disable
  string OrderType { get; set; }

  /// <exclude />
  [PXString(15, IsUnicode = true)]
  [PXUIField(DisplayName = "Order Nbr.")]
  public virtual string OrderNbr { get; set; }

  /// <exclude />
  [PXString(2, IsFixed = true)]
  [PXUIField(DisplayName = "Return Order Type", Visible = false)]
  public virtual string ReturnOrderType { get; set; }

  /// <exclude />
  [PXString(15, IsUnicode = true)]
  [PXUIField(DisplayName = "Return Order Nbr.")]
  [PXSelector(typeof (Search<PX.Objects.SO.SOOrder.orderNbr, Where<PX.Objects.SO.SOOrder.orderType, Equal<Current<SOOrderRelatedReturnsSPResultDoc.returnOrderType>>>>))]
  public virtual string ReturnOrderNbr { get; set; }

  /// <exclude />
  [PXString(3, IsFixed = true)]
  [PX.Objects.AR.ARDocType.List]
  [PXUIField(DisplayName = "Return Invoice Type", Visible = false)]
  public virtual string ReturnInvoiceType { get; set; }

  /// <exclude />
  [PXString(15, IsUnicode = true)]
  [PXSelector(typeof (Search<PX.Objects.SO.SOInvoice.refNbr, Where<PX.Objects.SO.SOInvoice.docType, Equal<Current<SOOrderRelatedReturnsSPResultDoc.returnInvoiceType>>>>))]
  [PXUIField(DisplayName = "Return Invoice Nbr.")]
  public virtual string ReturnInvoiceNbr { get; set; }

  /// <exclude />
  [PXDBString(1, IsFixed = true)]
  [SOShipmentType.List]
  [PXUIField(DisplayName = "Shipment Type")]
  public virtual string ShipmentType { get; set; }

  /// <exclude />
  [PXDBString(15, IsUnicode = true)]
  [PXSelector(typeof (Search<PX.Objects.SO.Navigate.SOOrderShipment.shipmentNbr, Where<PX.Objects.SO.Navigate.SOOrderShipment.orderType, Equal<Current<SOOrderRelatedReturnsSPResultDoc.returnOrderType>>, And<PX.Objects.SO.Navigate.SOOrderShipment.orderNbr, Equal<Current<SOOrderRelatedReturnsSPResultDoc.returnOrderNbr>>, And<PX.Objects.SO.Navigate.SOOrderShipment.shipmentType, Equal<Current<SOOrderRelatedReturnsSPResultDoc.shipmentType>>>>>>))]
  [PXUIField(DisplayName = "Shipment Nbr.")]
  public virtual string ShipmentNbr { get; set; }

  /// <exclude />
  [PXString(3, IsFixed = true)]
  [PX.Objects.AR.ARDocType.List]
  [PXUIField(DisplayName = "AR Doc. Type")]
  public virtual string ARDocType { get; set; }

  /// <exclude />
  [PXString(15, IsUnicode = true)]
  [PXSelector(typeof (Search<PX.Objects.SO.SOInvoice.refNbr, Where<PX.Objects.SO.SOInvoice.docType, Equal<Current<SOOrderRelatedReturnsSPResultDoc.aRDocType>>>>))]
  [PXUIField(DisplayName = "AR Ref. Nbr.")]
  public virtual string ARRefNbr { get; set; }

  /// <exclude />
  [PXString(3, IsFixed = true)]
  [APInvoiceType.List]
  [PXUIField(DisplayName = "AP Doc. Type")]
  public virtual string APDocType { get; set; }

  /// <exclude />
  [PXDBString(15, IsUnicode = true)]
  [PXSelector(typeof (Search<APInvoice.refNbr, Where<APInvoice.docType, Equal<Current<SOOrderRelatedReturnsSPResultDoc.aPDocType>>>>))]
  [PXUIField(DisplayName = "AP Ref. Nbr.")]
  public virtual string APRefNbr { get; set; }

  public abstract class gridLineNbr : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    SOOrderRelatedReturnsSPResultDoc.gridLineNbr>
  {
  }

  public abstract class orderType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOOrderRelatedReturnsSPResultDoc.orderType>
  {
  }

  public abstract class orderNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOOrderRelatedReturnsSPResultDoc.orderNbr>
  {
  }

  public abstract class returnOrderType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOOrderRelatedReturnsSPResultDoc.returnOrderType>
  {
  }

  public abstract class returnOrderNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOOrderRelatedReturnsSPResultDoc.returnOrderNbr>
  {
  }

  public abstract class returnInvoiceType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOOrderRelatedReturnsSPResultDoc.returnInvoiceType>
  {
  }

  public abstract class returnInvoiceNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOOrderRelatedReturnsSPResultDoc.returnInvoiceNbr>
  {
  }

  public abstract class shipmentType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOOrderRelatedReturnsSPResultDoc.shipmentType>
  {
  }

  public abstract class shipmentNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOOrderRelatedReturnsSPResultDoc.shipmentNbr>
  {
  }

  public abstract class aRDocType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOOrderRelatedReturnsSPResultDoc.aRDocType>
  {
  }

  public abstract class aRRefNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOOrderRelatedReturnsSPResultDoc.aRRefNbr>
  {
  }

  public abstract class aPDocType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOOrderRelatedReturnsSPResultDoc.aPDocType>
  {
  }

  public abstract class aPRefNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOOrderRelatedReturnsSPResultDoc.aPRefNbr>
  {
  }
}
