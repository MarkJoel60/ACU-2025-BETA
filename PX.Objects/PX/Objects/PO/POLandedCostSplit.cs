// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.POLandedCostSplit
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.AP;
using PX.Objects.CM.Extensions;
using System;

#nullable enable
namespace PX.Objects.PO;

[PXHidden]
[Serializable]
public class POLandedCostSplit : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  /// <summary>The type of the landed cost receipt line.</summary>
  /// <value>
  /// The field is determined by the type of the parent <see cref="T:PX.Objects.PO.POLandedCostDoc">document</see>.
  /// For the list of possible values see <see cref="P:PX.Objects.PO.POLandedCostDoc.DocType" />.
  /// </value>
  [APDocType.List]
  [PXDBString(1, IsKey = true, IsFixed = true)]
  [PXDBDefault(typeof (POLandedCostDoc.docType))]
  public virtual 
  #nullable disable
  string DocType { get; set; }

  /// <summary>
  /// Reference number of the parent <see cref="T:PX.Objects.PO.POLandedCostDoc">document</see>.
  /// </summary>
  [PXDBString(15, IsUnicode = true, IsKey = true)]
  [PXDBDefault(typeof (POLandedCostDoc.refNbr))]
  [PXParent(typeof (POLandedCostSplit.FK.LandedCostDocument))]
  public virtual string RefNbr { get; set; }

  /// <summary>The number of the transaction line in the document.</summary>
  /// <value>
  /// Note that the sequence of line numbers of the transactions belonging to a single document may include gaps.
  /// </value>
  [PXDBInt(IsKey = true)]
  public virtual int? DetailLineNbr { get; set; }

  /// <summary>The number of the transaction line in the document.</summary>
  /// <value>
  /// Note that the sequence of line numbers of the transactions belonging to a single document may include gaps.
  /// </value>
  [PXDBInt(IsKey = true)]
  public virtual int? ReceiptLineNbr { get; set; }

  [PXDBLong]
  [CurrencyInfo(typeof (POLandedCostDoc.curyInfoID))]
  public virtual long? CuryInfoID { get; set; }

  [PXDBCurrency(typeof (POLandedCostSplit.curyInfoID), typeof (POLandedCostSplit.lineAmt), BaseCalc = false)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryLineAmt { get; set; }

  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? LineAmt { get; set; }

  [PXDBCreatedByID]
  public virtual Guid? CreatedByID { get; set; }

  [PXDBCreatedByScreenID]
  public virtual string CreatedByScreenID { get; set; }

  [PXDBCreatedDateTime]
  public virtual DateTime? CreatedDateTime { get; set; }

  [PXDBLastModifiedByID]
  public virtual Guid? LastModifiedByID { get; set; }

  [PXDBLastModifiedByScreenID]
  public virtual string LastModifiedByScreenID { get; set; }

  [PXDBLastModifiedDateTime]
  public virtual DateTime? LastModifiedDateTime { get; set; }

  [PXDBTimestamp]
  public virtual byte[] tstamp { get; set; }

  [PXNote]
  public virtual Guid? NoteID { get; set; }

  public class PK : 
    PrimaryKeyOf<POLandedCostSplit>.By<POLandedCostSplit.docType, POLandedCostSplit.refNbr, POLandedCostSplit.receiptLineNbr, POLandedCostSplit.detailLineNbr>
  {
    public static POLandedCostSplit Find(
      PXGraph graph,
      string docType,
      string refNbr,
      int? receiptLineNbr,
      int? detailLineNbr,
      PKFindOptions options = 0)
    {
      return (POLandedCostSplit) PrimaryKeyOf<POLandedCostSplit>.By<POLandedCostSplit.docType, POLandedCostSplit.refNbr, POLandedCostSplit.receiptLineNbr, POLandedCostSplit.detailLineNbr>.FindBy(graph, (object) docType, (object) refNbr, (object) receiptLineNbr, (object) detailLineNbr, options);
    }
  }

  public static class FK
  {
    public class LandedCostDocument : 
      PrimaryKeyOf<POLandedCostDoc>.By<POLandedCostDoc.docType, POLandedCostDoc.refNbr>.ForeignKeyOf<POLandedCostSplit>.By<POLandedCostSplit.docType, POLandedCostSplit.refNbr>
    {
    }

    public class LandedCostReceiptLine : 
      PrimaryKeyOf<POLandedCostReceiptLine>.By<POLandedCostReceiptLine.docType, POLandedCostReceiptLine.refNbr, POLandedCostReceiptLine.lineNbr>.ForeignKeyOf<POLandedCostSplit>.By<POLandedCostSplit.docType, POLandedCostSplit.refNbr, POLandedCostSplit.receiptLineNbr>
    {
    }

    public class LandedCostDetail : 
      PrimaryKeyOf<POLandedCostDetail>.By<POLandedCostDetail.docType, POLandedCostDetail.refNbr, POLandedCostDetail.lineNbr>.ForeignKeyOf<POLandedCostSplit>.By<POLandedCostSplit.docType, POLandedCostSplit.refNbr, POLandedCostSplit.detailLineNbr>
    {
    }

    public class CurrencyInfo : 
      PrimaryKeyOf<PX.Objects.CM.CurrencyInfo>.By<PX.Objects.CM.CurrencyInfo.curyInfoID>.ForeignKeyOf<POLandedCostSplit>.By<POLandedCostSplit.curyInfoID>
    {
    }
  }

  public abstract class docType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POLandedCostSplit.docType>
  {
  }

  public abstract class refNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POLandedCostSplit.refNbr>
  {
  }

  public abstract class detailLineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POLandedCostSplit.detailLineNbr>
  {
  }

  public abstract class receiptLineNbr : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    POLandedCostSplit.receiptLineNbr>
  {
  }

  public abstract class curyInfoID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  POLandedCostSplit.curyInfoID>
  {
  }

  public abstract class curyLineAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POLandedCostSplit.curyLineAmt>
  {
  }

  public abstract class lineAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  POLandedCostSplit.lineAmt>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  POLandedCostSplit.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POLandedCostSplit.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    POLandedCostSplit.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    POLandedCostSplit.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POLandedCostSplit.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    POLandedCostSplit.lastModifiedDateTime>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  POLandedCostSplit.Tstamp>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  POLandedCostSplit.noteID>
  {
  }
}
