// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.ARDunningLetterDetail
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CM;
using PX.Objects.CS;
using System;

#nullable enable
namespace PX.Objects.AR;

/// <summary>List of invoises in Dunning Letter</summary>
[PXCacheName("Dunning Letter Detail")]
[Serializable]
public class ARDunningLetterDetail : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBInt(IsKey = true)]
  [PXDBDefault(typeof (ARDunningLetter.dunningLetterID))]
  [PXUIField(Enabled = false)]
  [PXParent(typeof (Select<ARDunningLetter, Where<ARDunningLetter.dunningLetterID, Equal<Current<ARDunningLetterDetail.dunningLetterID>>>>))]
  public virtual int? DunningLetterID { get; set; }

  [PXDBString(3, IsFixed = true, IsKey = true)]
  [ARDocType.List]
  [PXUIField(DisplayName = "Type")]
  [PXDefault]
  public virtual 
  #nullable disable
  string DocType { get; set; }

  /// <summary>
  /// The type of the document for printing, which is used in reports.
  /// </summary>
  /// <value>
  /// The field can have one of the values described in <see cref="T:PX.Objects.AR.ARDocType.PrintListAttribute" />.
  /// </value>
  [PXString(3, IsFixed = true)]
  [ARDocType.PrintList]
  [PXUIField]
  public virtual string PrintDocType
  {
    [PXDependsOnFields(new Type[] {typeof (ARDunningLetterDetail.docType)})] get => this.DocType;
  }

  [PXDBString(15, IsUnicode = true, IsKey = true, InputMask = ">CCCCCCCCCCCCCCC")]
  [PXUIField(DisplayName = "Reference Nbr.")]
  [PXDefault]
  public virtual string RefNbr { get; set; }

  [PXDefault]
  [PXUIField(DisplayName = "Customer", IsReadOnly = true, Visible = false)]
  [Customer(DescriptionField = typeof (Customer.acctName))]
  public virtual int? BAccountID { get; set; }

  [PXDefault]
  [PXDBInt]
  public virtual int? DunningLetterBAccountID { get; set; }

  [PXDBDate]
  [PXDefault(TypeCode.DateTime, "01/01/1900")]
  public virtual DateTime? DocDate { get; set; }

  [PXDBDate]
  [PXUIField(DisplayName = "Due Date")]
  public virtual DateTime? DueDate { get; set; }

  [PXDBCury(typeof (ARDunningLetterDetail.curyID))]
  public virtual Decimal? CuryOrigDocAmt { get; set; }

  [PXDBCury(typeof (ARDunningLetterDetail.curyID))]
  public virtual Decimal? CuryDocBal { get; set; }

  [PXDBString(5, IsUnicode = true)]
  [PXDefault("")]
  [PXSelector(typeof (PX.Objects.CM.Currency.curyID), CacheGlobal = true)]
  [PXUIField(DisplayName = "Currency ID")]
  public virtual string CuryID { get; set; }

  [PXDBBaseCury(null, null)]
  [PXUIField(DisplayName = "Original Document Amount")]
  public virtual Decimal? OrigDocAmt { get; set; }

  [PXDBBaseCury(null, null)]
  [PXUIField(DisplayName = "Outstanding Balance")]
  public virtual Decimal? DocBal { get; set; }

  [PXDBBool]
  [PXDefault(true)]
  public virtual bool? Overdue { get; set; }

  [PXBaseCury]
  [PXDBCalced(typeof (Switch<Case<Where<ARDunningLetterDetail.overdue, Equal<True>>, ARDunningLetterDetail.docBal>, decimal0>), typeof (Decimal))]
  [PXUIField(DisplayName = "Overdue Balance")]
  public virtual Decimal? OverdueBal { get; set; }

  [PXDBBool]
  [PXDBDefault(typeof (ARDunningLetter.voided))]
  public virtual bool? Voided { get; set; }

  [PXDBBool]
  [PXDBDefault(typeof (ARDunningLetter.released))]
  public virtual bool? Released { get; set; }

  [PXDBTimestamp]
  public virtual byte[] tstamp { get; set; }

  [PXDBInt]
  [PXDefault(0)]
  [PXUIField(DisplayName = "Dunning Level")]
  public virtual int? DunningLetterLevel { get; set; }

  public class PK : 
    PrimaryKeyOf<ARDunningLetterDetail>.By<ARDunningLetterDetail.dunningLetterID, ARDunningLetterDetail.docType, ARDunningLetterDetail.refNbr>
  {
    public static ARDunningLetterDetail Find(
      PXGraph graph,
      int? dunningLetterID,
      string docType,
      string refNbr,
      PKFindOptions options = 0)
    {
      return (ARDunningLetterDetail) PrimaryKeyOf<ARDunningLetterDetail>.By<ARDunningLetterDetail.dunningLetterID, ARDunningLetterDetail.docType, ARDunningLetterDetail.refNbr>.FindBy(graph, (object) dunningLetterID, (object) docType, (object) refNbr, options);
    }
  }

  public static class FK
  {
    public class Customer : 
      PrimaryKeyOf<Customer>.By<Customer.bAccountID>.ForeignKeyOf<ARDunningLetterDetail>.By<ARDunningLetterDetail.bAccountID>
    {
    }
  }

  public abstract class dunningLetterID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    ARDunningLetterDetail.dunningLetterID>
  {
  }

  public abstract class docType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARDunningLetterDetail.docType>
  {
  }

  public abstract class printDocType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARDunningLetterDetail.printDocType>
  {
  }

  public abstract class refNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARDunningLetterDetail.refNbr>
  {
  }

  public abstract class bAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARDunningLetterDetail.bAccountID>
  {
  }

  public abstract class dunningLetterBAccountID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    ARDunningLetterDetail.dunningLetterBAccountID>
  {
  }

  public abstract class docDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    ARDunningLetterDetail.docDate>
  {
  }

  public abstract class dueDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    ARDunningLetterDetail.dueDate>
  {
  }

  public abstract class curyOrigDocAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARDunningLetterDetail.curyOrigDocAmt>
  {
  }

  public abstract class curyDocBal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARDunningLetterDetail.curyDocBal>
  {
  }

  public abstract class curyID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARDunningLetterDetail.curyID>
  {
  }

  public abstract class origDocAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARDunningLetterDetail.origDocAmt>
  {
  }

  public abstract class docBal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARDunningLetterDetail.docBal>
  {
  }

  public abstract class overdue : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARDunningLetterDetail.overdue>
  {
  }

  public abstract class overdueBal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARDunningLetterDetail.overdueBal>
  {
  }

  public abstract class voided : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARDunningLetterDetail.voided>
  {
  }

  public abstract class released : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARDunningLetterDetail.released>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  ARDunningLetterDetail.Tstamp>
  {
  }

  public abstract class dunningLetterLevel : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    ARDunningLetterDetail.dunningLetterLevel>
  {
  }
}
