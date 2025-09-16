// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.POReceiptLinesCount
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.Common.Attributes;

#nullable enable
namespace PX.Objects.PO;

[PXHidden]
[PXProjection(typeof (SelectFromBase<POReceiptLine, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<POReceiptLine.lineType, IBqlString>.IsNotIn<POLineType.service, POLineType.freight>>.AggregateTo<GroupBy<POReceiptLine.receiptType, GroupBy<POReceiptLine.receiptNbr>>>))]
public class POReceiptLinesCount : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBString(2, IsFixed = true, IsKey = true, BqlField = typeof (POReceiptLine.receiptType))]
  public virtual 
  #nullable disable
  string ReceiptType { get; set; }

  [PXDBString(15, IsUnicode = true, IsKey = true, InputMask = "", BqlField = typeof (POReceiptLine.receiptNbr))]
  public virtual string ReceiptNbr { get; set; }

  [PXDBCount]
  public virtual int? LinesCount { get; set; }

  public class PK : 
    PrimaryKeyOf<POReceiptLinesCount>.By<POReceiptLinesCount.receiptType, POReceiptLinesCount.receiptNbr>
  {
    public static POReceiptLinesCount Find(
      PXGraph graph,
      string receiptType,
      string receiptNbr,
      PKFindOptions options = 0)
    {
      return (POReceiptLinesCount) PrimaryKeyOf<POReceiptLinesCount>.By<POReceiptLinesCount.receiptType, POReceiptLinesCount.receiptNbr>.FindBy(graph, (object) receiptType, (object) receiptNbr, options);
    }
  }

  public static class FK
  {
    public class Receipt : 
      PrimaryKeyOf<POReceipt>.By<POReceipt.receiptType, POReceipt.receiptNbr>.ForeignKeyOf<POReceiptLinesCount>.By<POReceiptLinesCount.receiptType, POReceiptLinesCount.receiptNbr>
    {
    }
  }

  public abstract class receiptType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POReceiptLinesCount.receiptType>
  {
  }

  public abstract class receiptNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POReceiptLinesCount.receiptNbr>
  {
  }

  public abstract class linesCount : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POReceiptLinesCount.linesCount>
  {
  }
}
