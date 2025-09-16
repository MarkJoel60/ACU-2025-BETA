// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.GLBudgetLineDetail
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using System;

#nullable enable
namespace PX.Objects.GL;

/// <summary>
/// Stores the amount budgeted for a particular <see cref="P:PX.Objects.GL.GLBudgetLineDetail.FinPeriodID">financial period</see> under a certain article of a budget (see <see cref="T:PX.Objects.GL.GLBudgetLine" />).
/// The record is related to the master budget article via the (<see cref="P:PX.Objects.GL.GLBudgetLineDetail.BranchID" />, <see cref="P:PX.Objects.GL.GLBudgetLineDetail.LedgerID" />, <see cref="P:PX.Objects.GL.GLBudgetLineDetail.FinYear" />, <see cref="P:PX.Objects.GL.GLBudgetLineDetail.GroupID" />) tuple.
/// The compound key includes all of the above fields and the <see cref="P:PX.Objects.GL.GLBudgetLineDetail.FinPeriodID" /> field.
/// </summary>
[PXCacheName("GL Budget Line Detail")]
[Serializable]
public class GLBudgetLineDetail : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected Guid? _GroupID;
  protected Decimal? _ReleasedAmount;

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.GL.Branch">branch</see> to which the budget article belongs.
  /// This field is a part of the compound key.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.GL.Branch.BranchID" /> field.
  /// </value>
  [PXDBInt(IsKey = true)]
  [PXDefault]
  public virtual int? BranchID { get; set; }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.GL.Ledger">ledger</see> to which the budget article belongs.
  /// This field is a part of the compound key.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.GL.Ledger.LedgerID" /> field.
  /// </value>
  [PXDBInt(IsKey = true)]
  [PXDefault]
  public virtual int? LedgerID { get; set; }

  /// <summary>
  /// The <see cref="P:PX.Objects.GL.GLBudgetLineDetail.FinYear">financial year</see> to which the budget article belongs.
  /// This field is a part of the compound key.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="!:FinYear.year" /> field.
  /// </value>
  [PXDBString(4, IsKey = true, IsFixed = true)]
  [PXDefault]
  public virtual 
  #nullable disable
  string FinYear { get; set; }

  /// <summary>
  /// The identifier of the budget article.
  /// This field is a part of the compound key.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.GL.GLBudgetLine.GroupID" /> field of the master record.
  /// </value>
  [PXDBGuid(false, IsKey = true)]
  [PXDefault]
  [PXParent(typeof (Select<GLBudgetLine, Where<GLBudgetLine.groupID, Equal<Current<GLBudgetLineDetail.groupID>>>>))]
  [PXUIField]
  public virtual Guid? GroupID
  {
    get => this._GroupID;
    set => this._GroupID = value;
  }

  /// <summary>
  /// The identifier of the GL <see cref="T:PX.Objects.GL.Account">account</see> of the budget article.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.GL.Account.AccountID" /> field.
  /// The value of this field is inherited from the <see cref="P:PX.Objects.GL.GLBudgetLine.AccountID" /> field of the master record.
  /// </value>
  [PXDBInt]
  [PXDefault]
  public virtual int? AccountID { get; set; }

  /// <summary>
  /// The identifier of the GL <see cref="T:PX.Objects.GL.Sub">subaccount</see> of the budget article.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.GL.Sub.SubID" /> field.
  /// The value of this field is inherited from the <see cref="P:PX.Objects.GL.GLBudgetLine.SubID" /> field of the master record.
  /// </value>
  [PXDefault]
  [SubAccount]
  public virtual int? SubID { get; set; }

  /// <summary>
  /// The identifier of the <see cref="!:PX.Objects.GL.Obsolete.FinPeriod">financial period</see>, which is represented by the record.
  /// This field is a part of the compound key.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="!:PX.Objects.GL.Obsolete.FinPeriod.FinPeriodID" /> field.
  /// </value>
  [PXDBString(6, IsKey = true)]
  [PXDefault]
  public virtual string FinPeriodID { get; set; }

  /// <summary>
  /// The amount allocated to the specified <see cref="P:PX.Objects.GL.GLBudgetLineDetail.FinPeriodID">financial period</see>.
  /// </summary>
  /// <value>
  /// The value is specified by a user or assigned by one of the procedures that distribute budgeted amounts.
  /// The values of these fields of detail lines are summed up to the <see cref="P:PX.Objects.GL.GLBudgetLine.Amount" /> field of the master record.
  /// </value>
  [PXDBDecimal(typeof (Search2<PX.Objects.CM.Currency.decimalPlaces, InnerJoin<Ledger, On<Ledger.baseCuryID, Equal<PX.Objects.CM.Currency.curyID>>>, Where<Ledger.ledgerID, Equal<Current<GLBudgetLineDetail.ledgerID>>>>))]
  [PXUIField(DisplayName = "Budget Amount")]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXFormula(null, typeof (SumCalc<GLBudgetLine.allocatedAmount>))]
  public virtual Decimal? Amount { get; set; }

  /// <summary>
  /// The currently released amount for the specified <see cref="P:PX.Objects.GL.GLBudgetLineDetail.FinPeriodID">financial period</see>.
  /// </summary>
  /// <value>
  /// This field is updated with the value of the <see cref="P:PX.Objects.GL.GLBudgetLineDetail.Amount" /> field upon release of the corresponding budget article.
  /// The difference between the value of this field and the <see cref="P:PX.Objects.GL.GLBudgetLineDetail.Amount" /> field shows the difference
  /// between the current state of the article and the corresponding figures in the budget ledger.
  /// </value>
  [PXDBDecimal(typeof (Search2<PX.Objects.CM.Currency.decimalPlaces, InnerJoin<Ledger, On<Ledger.baseCuryID, Equal<PX.Objects.CM.Currency.curyID>>>, Where<Ledger.ledgerID, Equal<Current<GLBudgetLineDetail.ledgerID>>>>))]
  [PXUIField(DisplayName = "Released Amount", Enabled = false)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? ReleasedAmount
  {
    get => this._ReleasedAmount;
    set => this._ReleasedAmount = value;
  }

  [PXDBTimestamp]
  public virtual byte[] tstamp { get; set; }

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

  public class PK : 
    PrimaryKeyOf<GLBudgetLineDetail>.By<GLBudgetLineDetail.branchID, GLBudgetLineDetail.ledgerID, GLBudgetLineDetail.groupID, GLBudgetLineDetail.finYear, GLBudgetLineDetail.finPeriodID>
  {
    public static GLBudgetLineDetail Find(
      PXGraph graph,
      int? branchID,
      int? ledgerID,
      Guid? groupID,
      string finYear,
      string finPeriodID,
      PKFindOptions options = 0)
    {
      return (GLBudgetLineDetail) PrimaryKeyOf<GLBudgetLineDetail>.By<GLBudgetLineDetail.branchID, GLBudgetLineDetail.ledgerID, GLBudgetLineDetail.groupID, GLBudgetLineDetail.finYear, GLBudgetLineDetail.finPeriodID>.FindBy(graph, (object) branchID, (object) ledgerID, (object) groupID, (object) finYear, (object) finPeriodID, options);
    }
  }

  public static class FK
  {
    public class Branch : 
      PrimaryKeyOf<Branch>.By<Branch.branchID>.ForeignKeyOf<GLBudgetLineDetail>.By<GLBudgetLineDetail.branchID>
    {
    }

    public class Ledger : 
      PrimaryKeyOf<Ledger>.By<Ledger.ledgerID>.ForeignKeyOf<GLBudgetLineDetail>.By<GLBudgetLineDetail.ledgerID>
    {
    }

    public class Account : 
      PrimaryKeyOf<Account>.By<Account.accountID>.ForeignKeyOf<GLBudgetLineDetail>.By<GLBudgetLineDetail.accountID>
    {
    }

    public class Subaccount : 
      PrimaryKeyOf<Sub>.By<Sub.subID>.ForeignKeyOf<GLBudgetLineDetail>.By<GLBudgetLineDetail.subID>
    {
    }
  }

  public abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  GLBudgetLineDetail.branchID>
  {
  }

  public abstract class ledgerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  GLBudgetLineDetail.ledgerID>
  {
  }

  public abstract class finYear : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  GLBudgetLineDetail.finYear>
  {
  }

  public abstract class groupID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  GLBudgetLineDetail.groupID>
  {
  }

  public abstract class accountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  GLBudgetLineDetail.accountID>
  {
  }

  public abstract class subID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  GLBudgetLineDetail.subID>
  {
  }

  public abstract class finPeriodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    GLBudgetLineDetail.finPeriodID>
  {
  }

  public abstract class amount : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  GLBudgetLineDetail.amount>
  {
  }

  public abstract class releasedAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    GLBudgetLineDetail.releasedAmount>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  GLBudgetLineDetail.Tstamp>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  GLBudgetLineDetail.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    GLBudgetLineDetail.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    GLBudgetLineDetail.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    GLBudgetLineDetail.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    GLBudgetLineDetail.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    GLBudgetLineDetail.lastModifiedDateTime>
  {
  }
}
