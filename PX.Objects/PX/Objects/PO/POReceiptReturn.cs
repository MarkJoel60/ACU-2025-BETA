// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.POReceiptReturn
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.AP;
using PX.Objects.CS;
using PX.Objects.IN;
using System;

#nullable enable
namespace PX.Objects.PO;

[PXProjection(typeof (Select<POReceipt>), Persistent = false)]
[PXHidden]
[Serializable]
public class POReceiptReturn : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected 
  #nullable disable
  string _CuryID;

  [PXBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Selected")]
  public virtual bool? Selected { get; set; }

  [PXDBString(2, IsFixed = true, IsKey = true, BqlField = typeof (POReceipt.receiptType))]
  [POReceiptType.List]
  [PXUIField(DisplayName = "Type", Enabled = false)]
  public virtual string ReceiptType { get; set; }

  [PXDBString(15, IsUnicode = true, IsKey = true, InputMask = "", BqlField = typeof (POReceipt.receiptNbr))]
  [POReceiptType.RefNbr(typeof (Search<POReceipt.receiptNbr, Where<POReceipt.receiptType, Equal<BqlField<POReceiptReturn.receiptType, IBqlString>.FromCurrent>>>), Filterable = true)]
  [PXUIField(DisplayName = "Receipt Nbr.", Enabled = false)]
  public virtual string ReceiptNbr { get; set; }

  [VendorActive(Enabled = false, DescriptionField = typeof (PX.Objects.AP.Vendor.acctName), CacheGlobal = true, Filterable = true, BqlField = typeof (POReceipt.vendorID))]
  public virtual int? VendorID { get; set; }

  [LocationID(typeof (Where<PX.Objects.CR.Location.bAccountID, Equal<Current<POReceipt.vendorID>>>), DescriptionField = typeof (PX.Objects.CR.Location.descr), Enabled = false, BqlField = typeof (POReceipt.vendorLocationID))]
  public virtual int? VendorLocationID { get; set; }

  [PXDBString(5, IsUnicode = true, InputMask = ">LLLLL", BqlField = typeof (POReceipt.curyID))]
  [PXUIField]
  public virtual string CuryID { get; set; }

  [PXDBDate(BqlField = typeof (POReceipt.receiptDate))]
  [PXUIField(DisplayName = "Date", Enabled = false)]
  public virtual DateTime? ReceiptDate { get; set; }

  [PXDBQuantity(BqlField = typeof (POReceipt.orderQty))]
  [PXUIField(DisplayName = "Total Qty.", Enabled = false)]
  public virtual Decimal? OrderQty { get; set; }

  [PXDBBool(BqlField = typeof (POReceipt.released))]
  [PXUIField(DisplayName = "Released")]
  public virtual bool? Released { get; set; }

  public abstract class selected : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  POReceiptReturn.selected>
  {
  }

  public abstract class receiptType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POReceiptReturn.receiptType>
  {
  }

  public abstract class receiptNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POReceiptReturn.receiptNbr>
  {
  }

  public abstract class vendorID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POReceiptReturn.vendorID>
  {
  }

  public abstract class vendorLocationID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    POReceiptReturn.vendorLocationID>
  {
  }

  public abstract class curyID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POReceiptReturn.curyID>
  {
  }

  public abstract class receiptDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    POReceiptReturn.receiptDate>
  {
  }

  public abstract class orderQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  POReceiptReturn.orderQty>
  {
  }

  public abstract class released : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  POReceiptReturn.released>
  {
  }
}
