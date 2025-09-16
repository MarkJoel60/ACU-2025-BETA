// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.POReceiptLine2
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.Objects.PO;

[PXBreakInheritance]
[PXHidden]
public class POReceiptLine2 : POReceiptLine
{
  public new abstract class receiptType : 
    BqlType<IBqlString, string>.Field<
    #nullable disable
    POReceiptLine2.receiptType>
  {
  }

  public new abstract class receiptNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POReceiptLine2.receiptNbr>
  {
  }

  public new abstract class lineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POReceiptLine2.lineNbr>
  {
  }

  public new abstract class origReceiptNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POReceiptLine2.origReceiptNbr>
  {
  }

  public new abstract class origReceiptLineNbr : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    POReceiptLine2.origReceiptLineNbr>
  {
  }

  public new abstract class origReceiptLineType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POReceiptLine2.origReceiptLineType>
  {
  }

  public new abstract class intercompanyShipmentLineNbr : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    POReceiptLine2.intercompanyShipmentLineNbr>
  {
  }

  public new abstract class pONbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POReceiptLine2.pONbr>
  {
  }

  public new abstract class pOType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POReceiptLine2.pOType>
  {
  }

  public new abstract class pOLineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POReceiptLine2.pOLineNbr>
  {
  }
}
