// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.ARStatementDetail
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CM;
using PX.Objects.GL;
using System;

#nullable enable
namespace PX.Objects.AR;

/// <summary>
/// A document that has been included in a <see cref="T:PX.Objects.AR.ARStatement">customer statement</see>.
/// The entities of this type are created on the Prepare Statements (AR503000) processing form and
/// can be seen on the Customer Statement (AR641500) and Customer Statement MC (AR642000) reports.
/// </summary>
[PXCacheName("AR Statement Detail")]
[Serializable]
public class ARStatementDetail : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  /// <summary>
  /// The integer identifier of the <see cref="T:PX.Objects.GL.Branch" /> of the <see cref="T:PX.Objects.AR.ARStatement">
  /// Customer Statement</see>, to which the detail belongs. This field is part of the compound
  /// key of the statement detail, and part of the foreign key referencing the
  /// <see cref="T:PX.Objects.AR.ARStatement">Customer Statement</see> record.
  /// Corresponds to the <see cref="P:PX.Objects.AR.ARStatement.BranchID" /> field.
  /// </summary>
  [Branch(null, null, true, true, true)]
  public virtual int? BranchID { get; set; }

  /// <summary>
  /// The integer identifier of the <see cref="T:PX.Objects.AR.Customer" /> of the <see cref="T:PX.Objects.AR.ARStatement">
  /// Customer Statement</see>, to which the detail belongs. This field is part of the compound
  /// key of the statement detail, and part of the foreign key referencing the
  /// <see cref="T:PX.Objects.AR.ARStatement">Customer Statement</see> record.
  /// Corresponds to the <see cref="P:PX.Objects.AR.ARStatement.CustomerID" /> field.
  /// </summary>
  [PXDBInt(IsKey = true)]
  [PXDefault(typeof (ARStatement.customerID))]
  [PXUIField(DisplayName = "Customer ID")]
  [PXParent(typeof (Select<ARStatement, Where<ARStatement.customerID, Equal<Current<ARStatementDetail.customerID>>, And<ARStatement.statementDate, Equal<Current<ARStatementDetail.statementDate>>, And<ARStatement.curyID, Equal<Current<ARStatementDetail.curyID>>>>>>))]
  public virtual int? CustomerID { get; set; }

  /// <summary>
  /// The date of the <see cref="T:PX.Objects.AR.ARStatement">Customer Statement</see>, to which
  /// the detail belongs. This field is part of the compound key of the statement
  /// detail, and part of the foreign key referencing the <see cref="T:PX.Objects.AR.ARStatement">
  /// Customer Statement</see> record.
  /// Corresponds to the <see cref="P:PX.Objects.AR.ARStatement.StatementDate" /> field.
  /// </summary>
  [PXDBDate(IsKey = true)]
  [PXDefault(typeof (ARStatement.statementDate))]
  [PXUIField(DisplayName = "Statement Date")]
  public virtual DateTime? StatementDate { get; set; }

  /// <summary>
  /// The currency of the <see cref="T:PX.Objects.AR.ARStatement">Customer Statement</see>, to
  /// which the detail belongs. This field is part of the compound key of the
  /// statement detail, and part of the foreign key referencing the <see cref="T:PX.Objects.AR.ARStatement">
  /// Customer Statement</see> record.
  /// Corresponds to the <see cref="P:PX.Objects.AR.ARStatement.StatementDate" /> field.
  /// </summary>
  [PXDBString(5, IsUnicode = true, IsKey = true)]
  [PXDefault(typeof (ARStatement.curyID))]
  [PXSelector(typeof (PX.Objects.CM.Currency.curyID), CacheGlobal = true)]
  [PXUIField(DisplayName = "Currency ID")]
  public virtual 
  #nullable disable
  string CuryID { get; set; }

  /// <summary>
  /// The type of the document, for which the statement detail is created.
  /// This field is part of the compound key of the statement detail,
  /// and part of the foreign key referencing the <see cref="T:PX.Objects.AR.ARRegister" />
  /// document. Corresponds to the <see cref="P:PX.Objects.AR.ARRegister.DocType" /> field.
  /// </summary>
  [PXDBString(3, IsKey = true, IsFixed = true)]
  [PXDefault]
  [PXUIField(DisplayName = "DocType")]
  public virtual string DocType { get; set; }

  /// <summary>
  /// The reference number of the document, for which the statement
  /// detail is created. This field is part of the compound key of
  /// the statement detail, and part of the foreign key referencing
  /// the <see cref="T:PX.Objects.AR.ARRegister" /> document. Corresponds to the
  /// <see cref="P:PX.Objects.AR.ARRegister.RefNbr" /> field.
  /// </summary>
  [PXDBString(15, IsUnicode = true, IsKey = true)]
  [PXDefault]
  [PXUIField(DisplayName = "Ref. Nbr.")]
  public virtual string RefNbr { get; set; }

  /// <summary>
  /// Indicates the balance, in base currency, that the document
  /// has on the statement date.
  /// </summary>
  [PXDBDecimal(4)]
  [PXUIField(DisplayName = "Doc. Balance")]
  public virtual Decimal? DocBalance { get; set; }

  /// <summary>
  /// Indicates the balance, in document currency, that the document
  /// has on the statement date.
  /// </summary>
  [PXDBDecimal(4)]
  [PXUIField(DisplayName = "Cury. Doc. Balance")]
  public virtual Decimal? CuryDocBalance { get; set; }

  /// <summary>The type of the customer statement.</summary>
  [PXDBString(1, IsFixed = true)]
  [PXUIField(DisplayName = "Statement Type")]
  public virtual string StatementType { get; set; }

  /// <summary>
  /// The beginning balance of the customer statement in the base currency. Only for the Balance Brought Forvard type.
  /// </summary>
  [PXDBBaseCury(null, null)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Beg. Balance")]
  public virtual Decimal? BegBalance { get; set; }

  /// <summary>
  /// The beginning balance of the customer statement in the foreign currency. Only for the Balance Brought Forvard type.
  /// </summary>
  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Curr. Beg. Balance")]
  public virtual Decimal? CuryBegBalance { get; set; }

  /// <summary>
  /// The customer statement's balance of the age bucket 00 in the base currency.
  /// </summary>
  [PXDBBaseCury(null, null)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Age00 Balance")]
  public virtual Decimal? AgeBalance00 { get; set; }

  /// <summary>
  /// The customer statement's balance of the age bucket 00 in the foreign currency.
  /// </summary>
  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Cury. Age00 Balance")]
  public virtual Decimal? CuryAgeBalance00 { get; set; }

  /// <summary>
  /// The customer statement's balance of the age bucket 01 in the base currency.
  /// </summary>
  [PXDBBaseCury(null, null)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Age01 Balance")]
  public virtual Decimal? AgeBalance01 { get; set; }

  /// <summary>
  /// The customer statement's balance of the age bucket 01 in the foreign currency.
  /// </summary>
  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Cury. Age01 Balance")]
  public virtual Decimal? CuryAgeBalance01 { get; set; }

  /// <summary>
  /// The customer statement's balance of the age bucket 02 in the base currency.
  /// </summary>
  [PXDBBaseCury(null, null)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Age02 Balance")]
  public virtual Decimal? AgeBalance02 { get; set; }

  /// <summary>
  /// The customer statement's balance of the age bucket 03 in the foreign currency.
  /// </summary>
  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Cury. Age02 Balance")]
  public virtual Decimal? CuryAgeBalance02 { get; set; }

  /// <summary>
  /// The customer statement's balance of the age bucket 03 in the base currency.
  /// </summary>
  [PXDBBaseCury(null, null)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Cury. Age03 Balance")]
  public virtual Decimal? AgeBalance03 { get; set; }

  /// <summary>
  /// The customer statement's balance of the age bucket 03 in the foreign currency.
  /// </summary>
  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Cury. Age03 Balance")]
  public virtual Decimal? CuryAgeBalance03 { get; set; }

  /// <summary>
  /// The customer statement's balance of the age bucket 04 in the base currency.
  /// </summary>
  [PXDBBaseCury(null, null)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Age04 Balance")]
  public virtual Decimal? AgeBalance04 { get; set; }

  /// <summary>
  /// The customer statement's balance of the age bucket 04 in the foreign currency.
  /// </summary>
  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Cury. Age04 Balance")]
  public virtual Decimal? CuryAgeBalance04 { get; set; }

  /// <summary>
  /// If set to <c>true</c>, indicates that the document
  /// is open on the statement date.
  /// </summary>
  [PXDBBool]
  [PXDefault(true)]
  public virtual bool? IsOpen { get; set; }

  [PXDBGuid(false, IsKey = true)]
  public virtual Guid? RefNoteID { get; set; }

  [PXDBInt]
  public virtual int? TranPostID { get; set; }

  public class PK : 
    PrimaryKeyOf<ARStatementDetail>.By<ARStatementDetail.branchID, ARStatementDetail.customerID, ARStatementDetail.curyID, ARStatementDetail.statementDate, ARStatementDetail.docType, ARStatementDetail.refNbr, ARStatementDetail.refNoteID>
  {
    public static ARStatementDetail Find(
      PXGraph graph,
      int? branchID,
      int? customerID,
      string curyID,
      DateTime? statementDate,
      string docType,
      string refNbr,
      Guid? RefNoteID,
      PKFindOptions options = 0)
    {
      return (ARStatementDetail) PrimaryKeyOf<ARStatementDetail>.By<ARStatementDetail.branchID, ARStatementDetail.customerID, ARStatementDetail.curyID, ARStatementDetail.statementDate, ARStatementDetail.docType, ARStatementDetail.refNbr, ARStatementDetail.refNoteID>.FindBy(graph, (object) branchID, (object) customerID, (object) curyID, (object) statementDate, (object) docType, (object) refNbr, (object) RefNoteID, options);
    }
  }

  public static class FK
  {
    public class Branch : 
      PrimaryKeyOf<PX.Objects.GL.Branch>.By<PX.Objects.GL.Branch.branchID>.ForeignKeyOf<ARStatementDetail>.By<ARStatementDetail.branchID>
    {
    }

    public class Customer : 
      PrimaryKeyOf<Customer>.By<Customer.bAccountID>.ForeignKeyOf<ARStatementDetail>.By<ARStatementDetail.customerID>
    {
    }

    public class Currency : 
      PrimaryKeyOf<PX.Objects.CM.Currency>.By<PX.Objects.CM.Currency.curyID>.ForeignKeyOf<ARStatementDetail>.By<ARStatementDetail.curyID>
    {
    }

    public class Statement : 
      PrimaryKeyOf<ARStatement>.By<ARStatement.branchID, ARStatement.customerID, ARStatement.curyID, ARStatement.statementDate>.ForeignKeyOf<ARStatementDetail>.By<ARStatementDetail.branchID, ARStatementDetail.customerID, ARStatementDetail.curyID, ARStatementDetail.statementDate>
    {
    }
  }

  public abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARStatementDetail.branchID>
  {
  }

  public abstract class customerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARStatementDetail.customerID>
  {
  }

  public abstract class statementDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    ARStatementDetail.statementDate>
  {
  }

  public abstract class curyID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARStatementDetail.curyID>
  {
  }

  public abstract class docType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARStatementDetail.docType>
  {
  }

  public abstract class refNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARStatementDetail.refNbr>
  {
  }

  public abstract class docBalance : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARStatementDetail.docBalance>
  {
  }

  public abstract class curyDocBalance : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARStatementDetail.curyDocBalance>
  {
  }

  public abstract class statementType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARStatementDetail.statementType>
  {
  }

  public abstract class begBalance : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARStatementDetail.begBalance>
  {
  }

  public abstract class curyBegBalance : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARStatementDetail.curyBegBalance>
  {
  }

  public abstract class ageBalance00 : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARStatementDetail.ageBalance00>
  {
  }

  public abstract class curyAgeBalance00 : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARStatementDetail.curyAgeBalance00>
  {
  }

  public abstract class ageBalance01 : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARStatementDetail.ageBalance01>
  {
  }

  public abstract class curyAgeBalance01 : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARStatementDetail.curyAgeBalance01>
  {
  }

  public abstract class ageBalance02 : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARStatementDetail.ageBalance02>
  {
  }

  public abstract class curyAgeBalance02 : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARStatementDetail.curyAgeBalance02>
  {
  }

  public abstract class ageBalance03 : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARStatementDetail.ageBalance03>
  {
  }

  public abstract class curyAgeBalance03 : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARStatementDetail.curyAgeBalance03>
  {
  }

  public abstract class ageBalance04 : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARStatementDetail.ageBalance04>
  {
  }

  public abstract class curyAgeBalance04 : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARStatementDetail.curyAgeBalance04>
  {
  }

  public abstract class isOpen : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARStatementDetail.isOpen>
  {
  }

  public abstract class refNoteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARStatementDetail.refNoteID>
  {
  }

  public abstract class tranPostID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARStatementDetail.tranPostID>
  {
  }
}
