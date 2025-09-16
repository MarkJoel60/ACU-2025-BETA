// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.Account
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.PM;
using PX.SM;
using System;

#nullable enable
namespace PX.Objects.GL;

/// <summary>
/// Represents an account of the General Ledger.
/// The records of this type are edited through the Chart Of Accounts (GL202500) form
/// (which corresponds to the <see cref="T:PX.Objects.GL.AccountMaint" /> graph).
/// </summary>
[PXCacheName("GL Account", CacheGlobal = true)]
[PXPrimaryGraph(typeof (AccountHistoryByYearEnq), Filter = typeof (AccountByYearFilter))]
[Serializable]
public class Account : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage, IIncludable, IRestricted
{
  protected int? _AccountID;
  protected 
  #nullable disable
  string _AccountCD;
  protected string _AccountClassID;
  protected string _Type;
  protected short? _COAOrder;
  protected bool? _Active;
  protected string _Description;
  protected string _PostOption;
  protected bool? _DirectPost;
  protected bool? _NoSubDetail;
  protected bool? _RequireUnits;
  protected string _GLConsolAccountCD;
  protected string _CuryID;
  protected int? _AccountGroupID;
  protected byte[] _GroupMask;
  protected string _RevalCuryRateTypeId;
  protected short? _Box1099;
  protected byte[] _tstamp;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected DateTime? _LastModifiedDateTime;
  protected bool? _IsCashAccount;
  protected bool? _Included;
  protected string _TaxCategoryID;
  public const string Default = "0";

  /// <summary>Unique identifier of the account. Database identity.</summary>
  [PXDBIdentity]
  [PXUIField]
  [PXReferentialIntegrityCheck]
  public virtual int? AccountID
  {
    get => this._AccountID;
    set => this._AccountID = value;
  }

  /// <summary>
  /// Key field.
  /// The user-friendly unique identifier of the account.
  /// </summary>
  [PXDefault]
  [AccountRaw]
  public virtual string AccountCD
  {
    [PXDependsOnFields(new System.Type[] {typeof (Account.accountID)})] get => this._AccountCD;
    set => this._AccountCD = value;
  }

  /// <summary>The type of the Entity. Extensibility point.</summary>
  /// <value>
  /// Possible values are:
  /// <c>"F"</c> - Financial Accounting, Account
  /// </value>
  [PXDBString(1, IsFixed = true)]
  [PXDefault("F")]
  [AccountEntityType.List]
  public string AccountingType { get; set; }

  /// <summary>
  /// Identifier of the <see cref="T:PX.Objects.GL.AccountClass">account class</see>, to which the account is assigned.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.GL.AccountClass.AccountClassID" /> field.
  /// </value>
  [PXDBString(20, IsUnicode = true)]
  [PXUIField]
  [PXSelector(typeof (AccountClass.accountClassID), DescriptionField = typeof (AccountClass.descr))]
  public virtual string AccountClassID
  {
    get => this._AccountClassID;
    set => this._AccountClassID = value;
  }

  /// <summary>The type of the account.</summary>
  /// <value>
  /// Allowed values are:
  /// <c>"A"</c> - Asset,
  /// <c>"L"</c> - Liability,
  /// <c>"I"</c> - Income,
  /// <c>"E"</c> - Expense.
  /// Defaults to the <see cref="P:PX.Objects.GL.AccountClass.Type">type</see>, specified in the <see cref="P:PX.Objects.GL.Account.AccountClassID">account class</see>.
  /// </value>
  [PXDBString(1, IsFixed = true)]
  [PXDefault("A", typeof (Search<AccountClass.type, Where<AccountClass.accountClassID, Equal<Current<Account.accountClassID>>>>))]
  [AccountType.List]
  [PXUIField]
  public virtual string Type
  {
    get => this._Type;
    set => this._Type = value;
  }

  /// <summary>
  /// Acumatica module for witch the Account is control account.
  /// </summary>
  /// <value>
  /// Allowed values are:
  /// <c>"AP"</c> - Accounts Payable,
  /// <c>"AR"</c> - Accounts Receivable,
  /// <c>"TX"</c> - TX,
  /// <c>"FA"</c> - Fixed Assets,
  /// <c>"DR"</c> - Deferred Revenue,
  /// <c>"SO"</c> - Sales Order,
  /// <c>"PO"</c> - Purchase Order,
  /// <c>"IN"</c> - Inventory.
  /// Defaults to the <see cref="P:PX.Objects.GL.AccountClass.Type">type</see>, specified in the <see cref="P:PX.Objects.GL.Account.AccountClassID">account class</see>.
  /// </value>
  [PXDBString(2, IsFixed = true)]
  [PX.Objects.GL.ControlAccountModule.List]
  [PXUIField]
  public virtual string ControlAccountModule { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Allow Manual Entry")]
  public virtual bool? AllowManualEntry { get; set; }

  /// <summary>
  /// The relative order of the account in the chart of accounts.
  /// </summary>
  /// <value>
  /// The value of this field is used to order accounts in the reports of the General Ledger module,
  /// when Custom Order option is selected in the <see cref="P:PX.Objects.GL.GLSetup.COAOrder" /> field of the <see cref="T:PX.Objects.GL.GLSetup">GL Preferences</see>.
  /// </value>
  [PXDBShort(MinValue = 0, MaxValue = 255 /*0xFF*/)]
  [PXDefault(0)]
  [PXUIField]
  public virtual short? COAOrder
  {
    get => this._COAOrder;
    set => this._COAOrder = value;
  }

  /// <summary>Indicates whether the Account is active.</summary>
  [PXDBBool]
  [PXDefault(true)]
  [PXUIField]
  public virtual bool? Active
  {
    get => this._Active;
    set => this._Active = value;
  }

  /// <summary>The description of the account.</summary>
  [PXDBLocalizableString(60, IsUnicode = true)]
  [PXUIField]
  public virtual string Description
  {
    get => this._Description;
    set => this._Description = value;
  }

  /// <summary>
  /// Defines how the transactions created in other modules are posted to this account.
  /// In the scope of the account overrides the <see cref="P:PX.Objects.AP.APSetup.SummaryPost">APSetup.SummaryPost</see>,
  /// <see cref="P:PX.Objects.AR.ARSetup.SummaryPost">ARSetup.SummaryPost</see> and similar settings in other modules.
  /// </summary>
  /// <value>
  /// Allowed values are:
  /// <c>"S"</c> - Summary (transactions amounts are summarized for each account and subaccount pair),
  /// <c>"D"</c> - Details (each transaction from other module results in a <see cref="T:PX.Objects.GL.GLTran">journal transaction</see>.
  /// Defaults to <c>"D"</c> - Detail.
  /// </value>
  [PXDBString(1)]
  [PXDefault("S")]
  [AccountPostOption.List]
  [PXUIField]
  public virtual string PostOption
  {
    get => this._PostOption;
    set => this._PostOption = value;
  }

  /// <summary>Reserved for backward compatibility.</summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField]
  public virtual bool? DirectPost
  {
    get => this._DirectPost;
    set => this._DirectPost = value;
  }

  /// <summary>
  /// If set to <c>true</c>, indicates that the system must set the subaccount to the <see cref="P:PX.Objects.GL.GLSetup.DefaultSubID">default subaccount</see>,
  /// when this account is selected for a document or transaction.
  /// </summary>
  /// <value>
  /// Defaults to <c>false</c>.
  /// </value>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField]
  public virtual bool? NoSubDetail
  {
    get => this._NoSubDetail;
    set => this._NoSubDetail = value;
  }

  /// <summary>
  /// When set to <c>true</c>, indicates that every transaction posted to this account must have
  /// <see cref="P:PX.Objects.GL.GLTran.Qty">Qunatity</see> and <see cref="P:PX.Objects.GL.GLTran.UOM">Units of Measure</see> specified.
  /// </summary>
  /// <value>
  /// Defaults to <c>false</c>.
  /// </value>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField]
  public virtual bool? RequireUnits
  {
    get => this._RequireUnits;
    set => this._RequireUnits = value;
  }

  /// <summary>
  /// The identifier of the external General Ledger account in the chart of accounts of the parent company,
  /// to which the balance of this account will be exported in the process of consolidation.
  /// This field is relevant only if the company is a consolidation unit in the parent company.
  /// </summary>
  [PXDBString(30, IsUnicode = true)]
  [PXUIField]
  [PXSelector(typeof (GLConsolAccount.accountCD), DescriptionField = typeof (GLConsolAccount.description))]
  public virtual string GLConsolAccountCD
  {
    get => this._GLConsolAccountCD;
    set => this._GLConsolAccountCD = value;
  }

  /// <summary>
  /// Identifier of the <see cref="T:PX.Objects.CM.Currency" /> of the account.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="!:Currency.CuryID" /> field.
  /// </value>
  [PXDBString(5, IsUnicode = true)]
  [PXUIField]
  [PXSelector(typeof (PX.Objects.CM.Currency.curyID))]
  public virtual string CuryID
  {
    get => this._CuryID;
    set => this._CuryID = value;
  }

  /// <summary>
  /// Identifier of the <see cref="T:PX.Objects.PM.PMAccountGroup">Account Group</see>, that includes this account.
  /// Used only if the Projects module has been activated.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.PM.PMAccountGroup.GroupID" /> field.
  /// </value>
  [PXDBInt]
  [PXUIField]
  [PXRestrictor(typeof (Where<PMAccountGroup.isActive, Equal<True>>), "The {0} account group is inactive. You can activate it on the Account Groups (PM201000) form.", new System.Type[] {typeof (PMAccountGroup.groupCD)})]
  [PXDimensionSelector("ACCGROUP", typeof (Search<PMAccountGroup.groupID, Where<PMAccountGroup.isActive, Equal<True>, And2<Where2<Where<Current<Account.type>, Equal<AccountType.asset>, Or<Current<Account.type>, Equal<AccountType.liability>>>, And<Where<PMAccountGroup.type, Equal<AccountType.asset>, Or<PMAccountGroup.type, Equal<AccountType.liability>>>>>, Or2<Where<PMAccountGroup.type, Equal<AccountType.expense>, And<Current<Account.type>, In3<AccountType.expense, AccountType.income, AccountType.asset, AccountType.liability>>>, Or<Where<PMAccountGroup.type, Equal<AccountType.income>>>>>>>), typeof (PMAccountGroup.groupCD), new System.Type[] {typeof (PMAccountGroup.groupCD), typeof (PMAccountGroup.description), typeof (PMAccountGroup.type), typeof (PMAccountGroup.isActive)})]
  public virtual int? AccountGroupID
  {
    get => this._AccountGroupID;
    set => this._AccountGroupID = value;
  }

  /// <summary>
  /// The group mask showing which <see cref="T:PX.SM.RelationGroup">restriction groups</see> the Account belongs to.
  /// To learn more about the way restriction groups are managed, see the documentation for the GL Account Access (GL104000) form
  /// (which corresponds to the <see cref="T:PX.Objects.GL.GLAccess" /> graph).
  /// </summary>
  [PXDBGroupMask]
  public virtual byte[] GroupMask
  {
    get => this._GroupMask;
    set => this._GroupMask = value;
  }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.CM.CurrencyRateType">Exchange Rate Type</see>
  /// that is used for the account in the process of revaluation.
  /// This field is required only for the accounts denominated to a foreign currency.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.CM.CurrencyRateType.CuryRateTypeID" /> field.
  /// </value>
  [PXDBString(6, IsUnicode = true)]
  [PXDefault]
  [PXForeignReference(typeof (Field<Account.revalCuryRateTypeId>.IsRelatedTo<PX.Objects.CM.CurrencyRateType.curyRateTypeID>))]
  [PXSelector(typeof (PX.Objects.CM.CurrencyRateType.curyRateTypeID))]
  [PXUIField(DisplayName = "Revaluation Rate Type")]
  public virtual string RevalCuryRateTypeId
  {
    get => this._RevalCuryRateTypeId;
    set => this._RevalCuryRateTypeId = value;
  }

  /// <summary>
  /// The box on the 1099 form associated with this account.
  /// </summary>
  [PXDBShort]
  [PXIntList(new int[] {0}, new string[] {"Undefined"})]
  [PXUIField]
  public virtual short? Box1099
  {
    get => this._Box1099;
    set => this._Box1099 = value;
  }

  /// <summary>
  /// Identifier of the <see cref="T:PX.Data.Note">Note</see> object, associated with the document.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Data.Note.NoteID">Note.NoteID</see> field.
  /// </value>
  [PXNote(DescriptionField = typeof (Account.accountCD))]
  public virtual Guid? NoteID { get; set; }

  [PXDBTimestamp]
  public virtual byte[] tstamp
  {
    get => this._tstamp;
    set => this._tstamp = value;
  }

  [PXDBCreatedByID]
  public virtual Guid? CreatedByID
  {
    get => this._CreatedByID;
    set => this._CreatedByID = value;
  }

  [PXDBCreatedByScreenID]
  public virtual string CreatedByScreenID
  {
    get => this._CreatedByScreenID;
    set => this._CreatedByScreenID = value;
  }

  [PXDBCreatedDateTime]
  public virtual DateTime? CreatedDateTime
  {
    get => this._CreatedDateTime;
    set => this._CreatedDateTime = value;
  }

  [PXDBLastModifiedByID]
  public virtual Guid? LastModifiedByID
  {
    get => this._LastModifiedByID;
    set => this._LastModifiedByID = value;
  }

  [PXDBLastModifiedByScreenID]
  public virtual string LastModifiedByScreenID
  {
    get => this._LastModifiedByScreenID;
    set => this._LastModifiedByScreenID = value;
  }

  [PXDBLastModifiedDateTime]
  public virtual DateTime? LastModifiedDateTime
  {
    get => this._LastModifiedDateTime;
    set => this._LastModifiedDateTime = value;
  }

  /// <summary>
  /// Indicates whether the accounts has on or several <see cref="T:PX.Objects.CA.CashAccount">Cash Accounts</see> associated with it.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField]
  public virtual bool? IsCashAccount
  {
    get => this._IsCashAccount;
    set => this._IsCashAccount = value;
  }

  /// <summary>
  /// The read-only label used for totals of the <see cref="P:PX.Objects.GL.Account.Type">account type</see> in the reports.
  /// </summary>
  /// <value>
  /// The value of this field depends solely on the <see cref="P:PX.Objects.GL.Account.Type">type of the account</see>.
  /// </value>
  [PXString(1)]
  [PXDefault("A")]
  [PXStringList(new string[] {"A", "L", "I", "E"}, new string[] {"Assets Total", "Liability Total", "Income Total", "Expense Total"})]
  [PXUIField]
  public virtual string TypeTotal
  {
    get => this.Type;
    set
    {
    }
  }

  /// <summary>
  /// The user-friendly label indicating whether the account is active. Used in the reports.
  /// </summary>
  /// <value>
  /// The value of this field depends solely on the <see cref="P:PX.Objects.GL.Account.Active" /> flag.
  /// </value>
  [PXInt]
  [PXDefault(1)]
  [PXIntList(new int[] {1, 0}, new string[] {"Yes", "No"})]
  [PXUIField]
  public virtual int? ReadableActive
  {
    [PXDependsOnFields(new System.Type[] {typeof (Account.active)})] get
    {
      return new int?(!this.Active.HasValue || !this.Active.Value ? 0 : 1);
    }
    set
    {
    }
  }

  /// <summary>Obsolete field.</summary>
  [PXBool]
  [Obsolete("This field has been deprecated and will be removed in Acumatica ERP 2017R2.")]
  public virtual bool? TransactionsForGivenCurrencyExists { get; set; }

  /// <summary>
  /// An unbound field used in the User Interface to include the Account into a <see cref="T:PX.SM.RelationGroup">restriction group</see>.
  /// To learn more about the way restriction groups are managed, see the documentation for the GL Account Access (GL104000) form
  /// (which corresponds to the <see cref="T:PX.Objects.GL.GLAccess" /> graph).
  /// </summary>
  [PXUnboundDefault(false)]
  [PXBool]
  [PXUIField(DisplayName = "Included")]
  public virtual bool? Included
  {
    get => this._Included;
    set => this._Included = value;
  }

  /// <summary>
  /// Identifier of the <see cref="T:PX.Objects.TX.TaxCategory">Tax Category</see> associated with the account.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.TX.TaxCategory.TaxCategoryID" /> field.
  /// </value>
  [PXDBString(15, IsUnicode = true)]
  [PXUIField]
  [PXSelector(typeof (PX.Objects.TX.TaxCategory.taxCategoryID), DescriptionField = typeof (PX.Objects.TX.TaxCategory.descr))]
  [PXRestrictor(typeof (Where<PX.Objects.TX.TaxCategory.active, Equal<True>>), "Tax Category '{0}' is inactive", new System.Type[] {typeof (PX.Objects.TX.TaxCategory.taxCategoryID)})]
  public virtual string TaxCategoryID
  {
    get => this._TaxCategoryID;
    set => this._TaxCategoryID = value;
  }

  public class PK : PrimaryKeyOf<Account>.By<Account.accountID>
  {
    public static Account Find(PXGraph graph, int? accountID, PKFindOptions options = 0)
    {
      return (Account) PrimaryKeyOf<Account>.By<Account.accountID>.FindBy(graph, (object) accountID, options);
    }
  }

  public class UK : PrimaryKeyOf<Account>.By<Account.accountCD>
  {
    public static Account Find(PXGraph graph, string accountCD, PKFindOptions options = 0)
    {
      return (Account) PrimaryKeyOf<Account>.By<Account.accountCD>.FindBy(graph, (object) accountCD, options);
    }
  }

  public static class FK
  {
    public class AccountClass : 
      PrimaryKeyOf<AccountClass>.By<AccountClass.accountClassID>.ForeignKeyOf<Account>.By<Account.accountClassID>
    {
    }

    public class Currency : 
      PrimaryKeyOf<PX.Objects.CM.Currency>.By<PX.Objects.CM.Currency.curyID>.ForeignKeyOf<Account>.By<Account.curyID>
    {
    }

    public class CurrencyRateType : 
      PrimaryKeyOf<PX.Objects.CM.CurrencyRateType>.By<PX.Objects.CM.CurrencyRateType.curyRateTypeID>.ForeignKeyOf<Account>.By<Account.revalCuryRateTypeId>
    {
    }

    public class TaxCategory : 
      PrimaryKeyOf<PX.Objects.TX.TaxCategory>.By<PX.Objects.TX.TaxCategory.taxCategoryID>.ForeignKeyOf<Account>.By<Account.taxCategoryID>
    {
    }

    public class ConsolidationAccount : 
      PrimaryKeyOf<Account>.By<Account.accountCD>.ForeignKeyOf<Account>.By<Account.gLConsolAccountCD>
    {
    }
  }

  public abstract class accountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Account.accountID>
  {
  }

  public abstract class accountCD : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Account.accountCD>
  {
  }

  public abstract class accountingType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Account.accountingType>
  {
  }

  public abstract class accountClassID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Account.accountClassID>
  {
  }

  public abstract class type : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Account.type>
  {
  }

  public abstract class controlAccountModule : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    Account.controlAccountModule>
  {
  }

  public abstract class allowManualEntry : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  Account.allowManualEntry>
  {
  }

  public abstract class cOAOrder : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  Account.cOAOrder>
  {
  }

  public abstract class active : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  Account.active>
  {
  }

  public abstract class description : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Account.description>
  {
  }

  public abstract class postOption : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Account.postOption>
  {
  }

  public abstract class directPost : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  Account.directPost>
  {
  }

  public abstract class noSubDetail : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  Account.noSubDetail>
  {
  }

  public abstract class requireUnits : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  Account.requireUnits>
  {
  }

  public abstract class gLConsolAccountCD : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    Account.gLConsolAccountCD>
  {
  }

  public abstract class curyID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Account.curyID>
  {
  }

  public abstract class accountGroupID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Account.accountGroupID>
  {
  }

  public abstract class groupMask : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  Account.groupMask>
  {
  }

  public abstract class revalCuryRateTypeId : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    Account.revalCuryRateTypeId>
  {
  }

  public abstract class box1099 : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  Account.box1099>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  Account.noteID>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  Account.Tstamp>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  Account.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    Account.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    Account.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  Account.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    Account.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    Account.lastModifiedDateTime>
  {
  }

  public abstract class isCashAccount : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  Account.isCashAccount>
  {
  }

  public abstract class typeTotal : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Account.typeTotal>
  {
  }

  public abstract class readableActive : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Account.readableActive>
  {
  }

  [Obsolete("This field has been deprecated and will be removed in Acumatica ERP 2017R2.")]
  public abstract class transactionsForGivenCurrencyExists : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    Account.transactionsForGivenCurrencyExists>
  {
  }

  public abstract class included : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  Account.included>
  {
  }

  public abstract class taxCategoryID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Account.taxCategoryID>
  {
  }

  public class main : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  Account.main>
  {
    public main()
      : base("0")
    {
    }
  }
}
