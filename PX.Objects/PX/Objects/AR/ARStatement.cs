// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.ARStatement
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
/// The primary entity representing a customer statement in the system.
/// It contains general statement information (including aged customer due
/// and overdue balances on the statement date) and serves as a master entity
/// for all <see cref="T:PX.Objects.AR.ARStatementDetail">document details</see>/&gt; that are displayed in the statement.
/// </summary>
[PXPrimaryGraph(typeof (ARStatementUpdate))]
[PXCacheName("AR Statement")]
[Serializable]
public class ARStatement : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected int? _BranchID;
  protected 
  #nullable disable
  string _CuryID;
  protected int? _CustomerID;
  protected DateTime? _StatementDate;
  protected DateTime? _PrevStatementDate;
  protected string _StatementCycleId;
  protected string _StatementType;
  protected Decimal? _BegBalance;
  protected Decimal? _CuryBegBalance;
  protected Decimal? _EndBalance;
  protected Decimal? _CuryEndBalance;
  protected Decimal? _AgeBalance00;
  protected Decimal? _CuryAgeBalance00;
  protected Decimal? _AgeBalance01;
  protected Decimal? _CuryAgeBalance01;
  protected Decimal? _AgeBalance02;
  protected Decimal? _CuryAgeBalance02;
  protected Decimal? _AgeBalance03;
  protected Decimal? _CuryAgeBalance03;
  protected Decimal? _AgeBalance04;
  protected Decimal? _CuryAgeBalance04;
  protected bool? _DontPrint;
  protected bool? _Printed;
  protected bool? _DontEmail;
  protected bool? _Emailed;

  [Branch(null, null, true, true, true, IsKey = true)]
  public virtual int? BranchID
  {
    get => this._BranchID;
    set => this._BranchID = value;
  }

  [PXDBString(5, IsUnicode = true, IsKey = true)]
  [PXDefault]
  [PXSelector(typeof (PX.Objects.CM.Currency.curyID), CacheGlobal = true)]
  [PXUIField(DisplayName = "Currency ID")]
  public virtual string CuryID
  {
    get => this._CuryID;
    set => this._CuryID = value;
  }

  [Customer(IsKey = true)]
  [PXDefault]
  [PXUIField(DisplayName = "Customer", IsReadOnly = true)]
  public virtual int? CustomerID
  {
    get => this._CustomerID;
    set => this._CustomerID = value;
  }

  [PXDBInt]
  [PXDefault]
  public virtual int? StatementCustomerID { get; set; }

  [PXDBDate(IsKey = true)]
  [PXDefault]
  [PXUIField(DisplayName = "Statement Date")]
  [PXSelector(typeof (Search4<ARStatement.statementDate, Aggregate<GroupBy<ARStatement.statementDate>>>))]
  public virtual DateTime? StatementDate
  {
    get => this._StatementDate;
    set => this._StatementDate = value;
  }

  [PXDBDate]
  [PXUIField(DisplayName = "Last Statement Date")]
  public virtual DateTime? PrevStatementDate
  {
    get => this._PrevStatementDate;
    set => this._PrevStatementDate = value;
  }

  [PXDBString(10, IsUnicode = true)]
  [PXDefault("")]
  [PXUIField(DisplayName = "Statement Cycle ID")]
  [PXSelector(typeof (ARStatementCycle.statementCycleId))]
  [PXForeignReference(typeof (Field<ARStatement.statementCycleId>.IsRelatedTo<ARStatementCycle.statementCycleId>))]
  public virtual string StatementCycleId
  {
    get => this._StatementCycleId;
    set => this._StatementCycleId = value;
  }

  [PXDBString(1, IsFixed = true)]
  [PXUIField(DisplayName = "Statement Type")]
  public virtual string StatementType
  {
    get => this._StatementType;
    set => this._StatementType = value;
  }

  [PXDBBaseCury(null, null)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Beg. Balance")]
  public virtual Decimal? BegBalance
  {
    get => this._BegBalance;
    set => this._BegBalance = value;
  }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Curr. Beg. Balance")]
  public virtual Decimal? CuryBegBalance
  {
    get => this._CuryBegBalance;
    set => this._CuryBegBalance = value;
  }

  [PXDBBaseCury(null, null)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? EndBalance
  {
    get => this._EndBalance;
    set => this._EndBalance = value;
  }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryEndBalance
  {
    get => this._CuryEndBalance;
    set => this._CuryEndBalance = value;
  }

  [PXDBBaseCury(null, null)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Age00 Balance")]
  public virtual Decimal? AgeBalance00
  {
    get => this._AgeBalance00;
    set => this._AgeBalance00 = value;
  }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Cury. Age00 Balance")]
  public virtual Decimal? CuryAgeBalance00
  {
    get => this._CuryAgeBalance00;
    set => this._CuryAgeBalance00 = value;
  }

  [PXDBBaseCury(null, null)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Age01 Balance")]
  public virtual Decimal? AgeBalance01
  {
    get => this._AgeBalance01;
    set => this._AgeBalance01 = value;
  }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Cury. Age01 Balance")]
  public virtual Decimal? CuryAgeBalance01
  {
    get => this._CuryAgeBalance01;
    set => this._CuryAgeBalance01 = value;
  }

  [PXDBBaseCury(null, null)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Age02 Balance")]
  public virtual Decimal? AgeBalance02
  {
    get => this._AgeBalance02;
    set => this._AgeBalance02 = value;
  }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Cury. Age02 Balance")]
  public virtual Decimal? CuryAgeBalance02
  {
    get => this._CuryAgeBalance02;
    set => this._CuryAgeBalance02 = value;
  }

  [PXDBBaseCury(null, null)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Cury. Age03 Balance")]
  public virtual Decimal? AgeBalance03
  {
    get => this._AgeBalance03;
    set => this._AgeBalance03 = value;
  }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Cury. Age03 Balance")]
  public virtual Decimal? CuryAgeBalance03
  {
    get => this._CuryAgeBalance03;
    set => this._CuryAgeBalance03 = value;
  }

  [PXDBBaseCury(null, null)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Age04 Balance")]
  public virtual Decimal? AgeBalance04
  {
    get => this._AgeBalance04;
    set => this._AgeBalance04 = value;
  }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Cury. Age04 Balance")]
  public virtual Decimal? CuryAgeBalance04
  {
    get => this._CuryAgeBalance04;
    set => this._CuryAgeBalance04 = value;
  }

  [PXDBShort]
  public virtual short? AgeDays00 { get; set; }

  [PXDBShort]
  public virtual short? AgeDays01 { get; set; }

  [PXDBShort]
  public virtual short? AgeDays02 { get; set; }

  [PXDBShort]
  public virtual short? AgeDays03 { get; set; }

  [PXDBShort]
  [Obsolete("This field has been deprecated and will be removed in Acumatica ERP 2018R2.")]
  public virtual short? AgeDays04 { get; set; }

  /// <summary>
  /// The description of the current aging period, which incorporates
  /// documents that are no more than <see cref="P:PX.Objects.AR.ARStatement.AgeDays00" /> days
  /// past due.
  /// </summary>
  [PXDBString(60, IsUnicode = true)]
  [PXUIField]
  public virtual string AgeBucketCurrentDescription { get; set; }

  /// <summary>
  /// The description of the first aging period, which incorporates documents
  /// that are from 1 to <see cref="P:PX.Objects.AR.ARStatement.AgeDays01" /> days past due.
  /// </summary>
  [PXDBString(60, IsUnicode = true)]
  [PXUIField]
  public virtual string AgeBucket01Description { get; set; }

  /// <summary>
  /// The description of the second aging period, which incorporates documents
  /// that are from <see cref="P:PX.Objects.AR.ARStatement.AgeDays01" /> + 1 to <see cref="P:PX.Objects.AR.ARStatement.AgeDays02" />
  /// days past due.
  /// </summary>
  [PXDBString(60, IsUnicode = true)]
  [PXUIField]
  public virtual string AgeBucket02Description { get; set; }

  /// <summary>
  /// The description of the third aging period that incorporates documents
  /// that are from <see cref="P:PX.Objects.AR.ARStatement.AgeDays02" /> + 1 to <see cref="P:PX.Objects.AR.ARStatement.AgeDays03" />
  /// days past due.
  /// </summary>
  [PXDBString(60, IsUnicode = true)]
  [PXUIField]
  public virtual string AgeBucket03Description { get; set; }

  /// <summary>
  /// The description of the last aging period that incorporates documents
  /// that are over <see cref="P:PX.Objects.AR.ARStatement.AgeDays03" /> days past due.
  /// </summary>
  [PXDBString(60, IsUnicode = true)]
  [PXUIField]
  public virtual string AgeBucket04Description { get; set; }

  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Don't Print")]
  public virtual bool? DontPrint
  {
    get => this._DontPrint;
    set => this._DontPrint = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Printed")]
  public virtual bool? Printed
  {
    get => this._Printed;
    set => this._Printed = value;
  }

  [PXDBShort]
  [PXDefault(TypeCode.Int16, "0")]
  [PXUIField(DisplayName = "Previously Printed Count")]
  public virtual short? PrevPrintedCnt { get; set; }

  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Don't Email")]
  public virtual bool? DontEmail
  {
    get => this._DontEmail;
    set => this._DontEmail = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Emailed")]
  public virtual bool? Emailed
  {
    get => this._Emailed;
    set => this._Emailed = value;
  }

  [PXBool]
  public virtual bool? Processed { get; set; }

  [PXDBShort]
  [PXDefault(TypeCode.Int16, "0")]
  [PXUIField(DisplayName = "Previously Emailed Count")]
  public virtual short? PrevEmailedCnt { get; set; }

  /// <summary>
  /// Gets or sets a value indicating whether the current
  /// customer statement has been generated on demand as
  /// opposed to by schedule.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "On-Demand Statement")]
  public virtual bool? OnDemand { get; set; }

  /// <summary>
  /// The name of the locale in which the statement
  /// has been generated.
  /// </summary>
  [PXDBString(10, IsUnicode = true)]
  [PXUIField(DisplayName = "Locale")]
  public virtual string LocaleName { get; set; }

  [PXNote(DescriptionField = typeof (ARStatement.statementDate))]
  public virtual Guid? NoteID { get; set; }

  [PXDBTimestamp]
  public virtual byte[] tstamp { get; set; }

  [PXDBCreatedDateTime]
  public virtual DateTime? CreatedDateTime { get; set; }

  [PXDBCreatedByScreenID]
  public virtual string CreatedByScreenID { get; set; }

  [PXDBCreatedByID]
  public virtual Guid? CreatedByID { get; set; }

  [PXDBLastModifiedByID]
  public virtual Guid? LastModifiedByID { get; set; }

  [PXDBLastModifiedByScreenID]
  public virtual string LastModifiedByScreenID { get; set; }

  [PXDBLastModifiedDateTime]
  public virtual DateTime? LastModifiedDateTime { get; set; }

  [PXBool]
  [PXDependsOnFields(new Type[] {typeof (ARStatement.customerID), typeof (ARStatement.statementCustomerID)})]
  public bool IsParentCustomerStatement
  {
    get
    {
      int? customerId = this.CustomerID;
      int? statementCustomerId = this.StatementCustomerID;
      return customerId.GetValueOrDefault() == statementCustomerId.GetValueOrDefault() & customerId.HasValue == statementCustomerId.HasValue;
    }
  }

  /// <exclude />
  public class PK : 
    PrimaryKeyOf<ARStatement>.By<ARStatement.branchID, ARStatement.customerID, ARStatement.curyID, ARStatement.statementDate>
  {
    public static ARStatement Find(
      PXGraph graph,
      int? branchID,
      int? customerID,
      string curyID,
      DateTime? statementDate,
      PKFindOptions options = 0)
    {
      return (ARStatement) PrimaryKeyOf<ARStatement>.By<ARStatement.branchID, ARStatement.customerID, ARStatement.curyID, ARStatement.statementDate>.FindBy(graph, (object) branchID, (object) customerID, (object) curyID, (object) statementDate, options);
    }
  }

  public static class FK
  {
    public class Branch : 
      PrimaryKeyOf<PX.Objects.GL.Branch>.By<PX.Objects.GL.Branch.branchID>.ForeignKeyOf<ARStatement>.By<ARStatement.branchID>
    {
    }

    public class Customer : 
      PrimaryKeyOf<Customer>.By<Customer.bAccountID>.ForeignKeyOf<ARStatement>.By<ARStatement.customerID>
    {
    }

    public class Currency : 
      PrimaryKeyOf<PX.Objects.CM.Currency>.By<PX.Objects.CM.Currency.curyID>.ForeignKeyOf<ARStatement>.By<ARStatement.curyID>
    {
    }

    public class StatementCustomer : 
      PrimaryKeyOf<Customer>.By<Customer.bAccountID>.ForeignKeyOf<ARStatement>.By<ARStatement.statementCustomerID>
    {
    }

    public class StatementCycle : 
      PrimaryKeyOf<ARStatementCycle>.By<ARStatementCycle.statementCycleId>.ForeignKeyOf<ARStatement>.By<ARStatement.statementCycleId>
    {
    }
  }

  public abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARStatement.branchID>
  {
  }

  public abstract class curyID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARStatement.curyID>
  {
  }

  public abstract class customerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARStatement.customerID>
  {
  }

  public abstract class statementCustomerID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    ARStatement.statementCustomerID>
  {
  }

  public abstract class statementDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    ARStatement.statementDate>
  {
  }

  public abstract class prevStatementDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    ARStatement.prevStatementDate>
  {
  }

  public abstract class statementCycleId : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARStatement.statementCycleId>
  {
  }

  public abstract class statementType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARStatement.statementType>
  {
  }

  public abstract class begBalance : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARStatement.begBalance>
  {
  }

  public abstract class curyBegBalance : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARStatement.curyBegBalance>
  {
  }

  public abstract class endBalance : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARStatement.endBalance>
  {
  }

  public abstract class curyEndBalance : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARStatement.curyEndBalance>
  {
  }

  public abstract class ageBalance00 : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARStatement.ageBalance00>
  {
  }

  public abstract class curyAgeBalance00 : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARStatement.curyAgeBalance00>
  {
  }

  public abstract class ageBalance01 : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARStatement.ageBalance01>
  {
  }

  public abstract class curyAgeBalance01 : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARStatement.curyAgeBalance01>
  {
  }

  public abstract class ageBalance02 : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARStatement.ageBalance02>
  {
  }

  public abstract class curyAgeBalance02 : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARStatement.curyAgeBalance02>
  {
  }

  public abstract class ageBalance03 : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARStatement.ageBalance03>
  {
  }

  public abstract class curyAgeBalance03 : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARStatement.curyAgeBalance03>
  {
  }

  public abstract class ageBalance04 : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARStatement.ageBalance04>
  {
  }

  public abstract class curyAgeBalance04 : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARStatement.curyAgeBalance04>
  {
  }

  public abstract class ageDays00 : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  ARStatement.ageDays00>
  {
  }

  public abstract class ageDays01 : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  ARStatement.ageDays01>
  {
  }

  public abstract class ageDays02 : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  ARStatement.ageDays02>
  {
  }

  public abstract class ageDays03 : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  ARStatement.ageDays03>
  {
  }

  [Obsolete("This field has been deprecated and will be removed in Acumatica ERP 2018R2.")]
  public abstract class ageDays04 : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  ARStatement.ageDays04>
  {
  }

  public abstract class ageBucketCurrentDescription : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARStatement.ageBucketCurrentDescription>
  {
  }

  public abstract class ageBucket01Description : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARStatement.ageBucket01Description>
  {
  }

  public abstract class ageBucket02Description : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARStatement.ageBucket02Description>
  {
  }

  public abstract class ageBucket03Description : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARStatement.ageBucket03Description>
  {
  }

  public abstract class ageBucket04Description : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARStatement.ageBucket04Description>
  {
  }

  public abstract class dontPrint : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARStatement.dontPrint>
  {
  }

  public abstract class printed : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARStatement.printed>
  {
  }

  public abstract class prevPrintedCnt : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  ARStatement.prevPrintedCnt>
  {
  }

  public abstract class dontEmail : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARStatement.dontEmail>
  {
  }

  public abstract class emailed : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARStatement.emailed>
  {
  }

  public abstract class processed : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARStatement.processed>
  {
  }

  public abstract class prevEmailedCnt : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  ARStatement.prevEmailedCnt>
  {
  }

  public abstract class onDemand : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARStatement.onDemand>
  {
  }

  public abstract class localeName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARStatement.localeName>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  ARStatement.noteID>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  ARStatement.Tstamp>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    ARStatement.createdDateTime>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARStatement.createdByScreenID>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  ARStatement.createdByID>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    ARStatement.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARStatement.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    ARStatement.lastModifiedDateTime>
  {
  }

  public abstract class isParentCustomerStatement : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    ARStatement.isParentCustomerStatement>
  {
  }
}
