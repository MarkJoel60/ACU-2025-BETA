// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.DAC.Projections.ARTranForDirectInvoice
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
namespace PX.Objects.SO.DAC.Projections;

[PXProjection(typeof (Select2<PX.Objects.AR.ARTran, LeftJoin<INTran, On<INTran.FK.ARTran>>, Where<PX.Objects.AR.ARTran.released, Equal<boolTrue>, And<Where2<Where<INTran.released, Equal<True>, And<INTran.qty, Greater<decimal0>, And<INTran.tranType, In3<INTranType.issue, INTranType.debitMemo, INTranType.invoice>>>>, Or<Where<INTran.released, IsNull, And<PX.Objects.AR.ARTran.lineType, In3<SOLineType.miscCharge, SOLineType.nonInventory>, And<Where2<Where<PX.Objects.AR.ARTran.qty, Greater<decimal0>, And<PX.Objects.AR.ARTran.tranType, In3<ARDocType.debitMemo, ARDocType.cashSale, ARDocType.invoice>>>, Or<Where<PX.Objects.AR.ARTran.qty, Less<decimal0>, And<PX.Objects.AR.ARTran.tranType, In3<ARDocType.creditMemo, ARDocType.cashReturn>>>>>>>>>>>>, OrderBy<Desc<PX.Objects.AR.ARTran.refNbr>>>), Persistent = false)]
[PXCacheName("AR Transactions")]
[Serializable]
public class ARTranForDirectInvoice : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Selected")]
  public virtual bool? Selected { get; set; }

  [PXDBString(3, IsKey = true, IsFixed = true, BqlField = typeof (PX.Objects.AR.ARTran.tranType))]
  [PXUIField(DisplayName = "Doc. Type", Enabled = false)]
  [ARDocType.List]
  public virtual 
  #nullable disable
  string TranType { get; set; }

  [PXDBString(15, IsUnicode = true, IsKey = true, BqlField = typeof (PX.Objects.AR.ARTran.refNbr))]
  [PXUIField(DisplayName = "Reference Nbr.", Enabled = false)]
  public virtual string RefNbr { get; set; }

  [PXDBInt(IsKey = true, BqlField = typeof (PX.Objects.AR.ARTran.lineNbr))]
  [PXUIField(DisplayName = "Line Nbr.", Enabled = false)]
  public virtual int? LineNbr { get; set; }

  [PXDBString(2, IsFixed = true, BqlField = typeof (PX.Objects.AR.ARTran.lineType))]
  [PXUIField(DisplayName = "Line Type", Enabled = false)]
  public virtual string LineType { get; set; }

  [Customer(Enabled = false, BqlField = typeof (PX.Objects.AR.ARTran.customerID))]
  public virtual int? CustomerID { get; set; }

  [PXDBDate(BqlField = typeof (PX.Objects.AR.ARTran.tranDate))]
  [PXUIField(DisplayName = "Doc. Date", Enabled = false)]
  public virtual DateTime? TranDate { get; set; }

  [AnyInventory(Enabled = false, BqlField = typeof (PX.Objects.AR.ARTran.inventoryID))]
  public virtual int? InventoryID { get; set; }

  [INUnit(typeof (PX.Objects.AR.ARTran.inventoryID), Enabled = false, BqlField = typeof (PX.Objects.AR.ARTran.uOM))]
  public virtual string UOM { get; set; }

  [PXDBDecimal(BqlField = typeof (PX.Objects.AR.ARTran.qty))]
  [PXUIField(DisplayName = "Qty", Enabled = false)]
  public virtual Decimal? Qty { get; set; }

  [PXDBDecimal(BqlField = typeof (PX.Objects.AR.ARTran.curyUnitPrice))]
  [PXUIField(DisplayName = "Unit Price", Enabled = false)]
  public virtual Decimal? CuryUnitPrice { get; set; }

  [PXDBBool(BqlField = typeof (PX.Objects.AR.ARTran.released))]
  public virtual bool? Released { get; set; }

  [PXDBInt(BqlField = typeof (PX.Objects.AR.ARTran.subItemID))]
  public virtual int? SubItemID { get; set; }

  [PXDBInt(BqlField = typeof (PX.Objects.AR.ARTran.siteID))]
  public virtual int? SiteID { get; set; }

  [PXDBInt(BqlField = typeof (PX.Objects.AR.ARTran.locationID))]
  public virtual int? LocationID { get; set; }

  [PXDBString(100, IsUnicode = true, InputMask = "", BqlField = typeof (PX.Objects.AR.ARTran.lotSerialNbr))]
  public virtual string LotSerialNbr { get; set; }

  [PXDBDate(InputMask = "d", DisplayMask = "d", BqlField = typeof (PX.Objects.AR.ARTran.expireDate))]
  public virtual DateTime? ExpireDate { get; set; }

  public abstract class selected : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARTranForDirectInvoice.selected>
  {
  }

  public abstract class tranType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARTranForDirectInvoice.tranType>
  {
  }

  public abstract class refNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARTranForDirectInvoice.refNbr>
  {
  }

  public abstract class lineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARTranForDirectInvoice.lineNbr>
  {
  }

  public abstract class lineType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARTranForDirectInvoice.lineType>
  {
  }

  public abstract class customerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARTranForDirectInvoice.customerID>
  {
  }

  public abstract class tranDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    ARTranForDirectInvoice.tranDate>
  {
  }

  public abstract class inventoryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARTranForDirectInvoice.inventoryID>
  {
  }

  public abstract class uOM : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARTranForDirectInvoice.uOM>
  {
  }

  public abstract class qty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARTranForDirectInvoice.qty>
  {
  }

  public abstract class curyUnitPrice : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARTranForDirectInvoice.curyUnitPrice>
  {
  }

  public abstract class released : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARTranForDirectInvoice.released>
  {
  }

  public abstract class subItemID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARTranForDirectInvoice.subItemID>
  {
  }

  public abstract class siteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARTranForDirectInvoice.siteID>
  {
  }

  public abstract class locationID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARTranForDirectInvoice.locationID>
  {
  }

  public abstract class lotSerialNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARTranForDirectInvoice.lotSerialNbr>
  {
  }

  public abstract class expireDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    ARTranForDirectInvoice.expireDate>
  {
  }
}
