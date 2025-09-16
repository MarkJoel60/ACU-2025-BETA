// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.POReceiptLineSigned
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.IN;
using System;

#nullable enable
namespace PX.Objects.PO;

[PXProjection(typeof (Select<POReceiptLine>), Persistent = false)]
[PXHidden]
[Serializable]
public class POReceiptLineSigned : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBString(2, IsFixed = true, IsKey = true, BqlField = typeof (POReceiptLine.receiptType))]
  public virtual 
  #nullable disable
  string ReceiptType { get; set; }

  [PXDBString(15, IsUnicode = true, IsKey = true, InputMask = "", BqlField = typeof (POReceiptLine.receiptNbr))]
  public virtual string ReceiptNbr { get; set; }

  [PXDBInt(IsKey = true, BqlField = typeof (POReceiptLine.lineNbr))]
  public virtual int? LineNbr { get; set; }

  [PXDBString(2, IsFixed = true, BqlField = typeof (POReceiptLine.pOType))]
  public virtual string POType { get; set; }

  [PXDBString(15, IsUnicode = true, BqlField = typeof (POReceiptLine.pONbr))]
  public virtual string PONbr { get; set; }

  [PXDBInt(BqlField = typeof (POReceiptLine.pOLineNbr))]
  public virtual int? POLineNbr { get; set; }

  [PXQuantity]
  [PXDBCalced(typeof (Mult<POReceiptLine.baseReceiptQty, POReceiptLine.invtMult>), typeof (Decimal))]
  public virtual Decimal? SignedBaseReceiptQty { get; set; }

  public abstract class receiptType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POReceiptLineSigned.receiptType>
  {
  }

  public abstract class receiptNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POReceiptLineSigned.receiptNbr>
  {
  }

  public abstract class lineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POReceiptLineSigned.lineNbr>
  {
  }

  public abstract class pOType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POReceiptLineSigned.pOType>
  {
  }

  public abstract class pONbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POReceiptLineSigned.pONbr>
  {
  }

  public abstract class pOLineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POReceiptLineSigned.pOLineNbr>
  {
  }

  public abstract class signedBaseReceiptQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POReceiptLineSigned.signedBaseReceiptQty>
  {
  }
}
