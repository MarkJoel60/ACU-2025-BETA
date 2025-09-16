// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.GLTrialBalanceImportMap
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Data.EP;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CM;
using PX.Objects.CS;
using PX.Objects.GL.Attributes;
using PX.Objects.GL.DAC;
using PX.Objects.GL.FinPeriods;
using System;

#nullable enable
namespace PX.Objects.GL;

[PXPrimaryGraph(typeof (JournalEntryImport))]
[PXCacheName("Trial Balance Import")]
[Serializable]
public class GLTrialBalanceImportMap : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected 
  #nullable disable
  string _Number;
  protected string _BatchNbr;
  protected DateTime? _ImportDate;
  protected string _FinPeriodID;
  protected string _Description;
  protected int? _LedgerID;
  protected bool? _IsHold;
  protected string _Status;
  protected Decimal? _CreditTotalBalance;
  protected Decimal? _DebitTotalBalance;
  protected Decimal? _TotalBalance;
  protected int? _LineCntr;
  protected byte[] _tstamp;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected DateTime? _LastModifiedDateTime;

  [PXDBString(15, IsUnicode = true, IsKey = true, InputMask = ">CCCCCCCCCCCCCCC")]
  [PXUIField]
  [PXSelector(typeof (GLTrialBalanceImportMap.number), new Type[] {typeof (GLTrialBalanceImportMap.number), typeof (GLTrialBalanceImportMap.ledgerID), typeof (GLTrialBalanceImportMap.status), typeof (GLTrialBalanceImportMap.orgBAccountID), typeof (GLTrialBalanceImportMap.finPeriodID), typeof (GLTrialBalanceImportMap.importDate), typeof (GLTrialBalanceImportMap.totalBalance), typeof (GLTrialBalanceImportMap.batchNbr)})]
  [AutoNumber(typeof (GLSetup.tBImportNumberingID), typeof (GLTrialBalanceImportMap.importDate))]
  [PXFieldDescription]
  public virtual string Number
  {
    get => this._Number;
    set => this._Number = value;
  }

  /// <summary>
  /// A reference to the <see cref="T:PX.Objects.CR.BAccount" />
  /// An integer identifier of the organizational entity.
  /// BAccountID of the Organization if OrganizationType != OrganizationTypes.WithBranchesBalancing
  /// BAccountID of the Branch if OrganizationType = OrganizationTypes.WithBranchesBalancing
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.CR.BAccount.BAccountID" /> field.
  /// </value>
  [OrganizationTree(typeof (GLTrialBalanceImportMap.organizationID), null, typeof (TrialBalanceImportOrganizationTreeSelect), true)]
  [PXUIVisible(typeof (Where2<FeatureInstalled<FeaturesSet.branch>, Or<FeatureInstalled<FeaturesSet.multiCompany>>>))]
  public int? OrgBAccountID { get; set; }

  /// <summary>
  /// Organization ID corresponds to the value of <see cref="P:PX.Objects.GL.GLTrialBalanceImportMap.OrgBAccountID" /> field.
  /// </summary>
  [PXInt]
  [OrganizationIdByBAccount(typeof (GLTrialBalanceImportMap.orgBAccountID))]
  public int? OrganizationID { get; set; }

  [PXDBString(15, IsUnicode = true)]
  [PXSelector(typeof (Search<Batch.batchNbr, Where<Batch.module, Equal<BatchModule.moduleGL>>>))]
  [PXUIField]
  public virtual string BatchNbr
  {
    get => this._BatchNbr;
    set => this._BatchNbr = value;
  }

  [PXDBDate]
  [PXDefault(typeof (AccessInfo.businessDate))]
  [PXUIField]
  public virtual DateTime? ImportDate
  {
    get => this._ImportDate;
    set => this._ImportDate = value;
  }

  [OpenPeriod(null, typeof (GLTrialBalanceImportMap.importDate), null, null, typeof (GLTrialBalanceImportMap.organizationID), null, null, true, false, false, false, true, FinPeriodSelectorAttribute.SelectionModesWithRestrictions.Undefined, null, null, true)]
  [PXDefault]
  [PXUIField]
  public virtual string FinPeriodID
  {
    get => this._FinPeriodID;
    set => this._FinPeriodID = value;
  }

  public virtual string BegFinPeriod
  {
    [PXDependsOnFields(new Type[] {typeof (GLTrialBalanceImportMap.finPeriodID)})] get
    {
      return this._FinPeriodID != null ? FinPeriodUtils.FiscalYear(this._FinPeriodID) + "01" : (string) null;
    }
  }

  [PXDBString(IsUnicode = true)]
  [PXUIField]
  public virtual string Description
  {
    get => this._Description;
    set => this._Description = value;
  }

  [PXDBInt]
  [PXDefault(typeof (SearchFor<Ledger.ledgerID>.In<SelectFromBase<Ledger, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<OrganizationLedgerLink>.On<BqlOperand<OrganizationLedgerLink.ledgerID, IBqlInt>.IsEqual<Ledger.ledgerID>>>, FbqlJoins.Left<PX.Objects.GL.DAC.Organization>.On<BqlOperand<PX.Objects.GL.DAC.Organization.organizationID, IBqlInt>.IsEqual<OrganizationLedgerLink.organizationID>>>, FbqlJoins.Left<Branch>.On<BqlOperand<Branch.organizationID, IBqlInt>.IsEqual<OrganizationLedgerLink.organizationID>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Ledger.balanceType, Equal<LedgerBalanceType.actual>>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.GL.DAC.Organization.organizationType, Equal<OrganizationTypes.withBranchesBalancing>>>>>.And<BqlOperand<Branch.bAccountID, IBqlInt>.IsEqual<BqlField<GLTrialBalanceImportMap.orgBAccountID, IBqlInt>.FromCurrent>>>>>.Or<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.GL.DAC.Organization.organizationType, NotEqual<OrganizationTypes.withBranchesBalancing>>>>>.And<BqlOperand<PX.Objects.GL.DAC.Organization.bAccountID, IBqlInt>.IsEqual<BqlField<GLTrialBalanceImportMap.orgBAccountID, IBqlInt>.FromCurrent>>>>>.Aggregate<To<GroupBy<Ledger.ledgerID>>>>))]
  [PXUIField]
  [PXSelector(typeof (SearchFor<Ledger.ledgerID>.In<SelectFromBase<Ledger, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<OrganizationLedgerLink>.On<BqlOperand<OrganizationLedgerLink.ledgerID, IBqlInt>.IsEqual<Ledger.ledgerID>>>, FbqlJoins.Left<PX.Objects.GL.DAC.Organization>.On<BqlOperand<PX.Objects.GL.DAC.Organization.organizationID, IBqlInt>.IsEqual<OrganizationLedgerLink.organizationID>>>, FbqlJoins.Left<Branch>.On<BqlOperand<Branch.organizationID, IBqlInt>.IsEqual<OrganizationLedgerLink.organizationID>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Ledger.balanceType, NotEqual<LedgerBalanceType.budget>>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.GL.DAC.Organization.organizationType, Equal<OrganizationTypes.withBranchesBalancing>>>>>.And<BqlOperand<Branch.bAccountID, IBqlInt>.IsEqual<BqlField<GLTrialBalanceImportMap.orgBAccountID, IBqlInt>.FromCurrent>>>>>.Or<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.GL.DAC.Organization.organizationType, NotEqual<OrganizationTypes.withBranchesBalancing>>>>>.And<BqlOperand<PX.Objects.GL.DAC.Organization.bAccountID, IBqlInt>.IsEqual<BqlField<GLTrialBalanceImportMap.orgBAccountID, IBqlInt>.FromCurrent>>>>>.Aggregate<To<GroupBy<Ledger.ledgerID>>>>), SubstituteKey = typeof (Ledger.ledgerCD), CacheGlobal = true, DescriptionField = typeof (Ledger.descr))]
  public virtual int? LedgerID
  {
    get => this._LedgerID;
    set => this._LedgerID = value;
  }

  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Hold")]
  public virtual bool? IsHold
  {
    get => this._IsHold;
    set => this._IsHold = value;
  }

  [PXDBString]
  [TrialBalanceImportMapStatus]
  [PXDefault("H")]
  [PXUIField(DisplayName = "Status", Enabled = false)]
  public virtual string Status
  {
    get => this._Status;
    set => this._Status = value;
  }

  [PXBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "", Visible = false)]
  [PXDependsOnFields(new Type[] {typeof (GLTrialBalanceImportMap.status)})]
  public virtual bool? IsEditable => new bool?(this._Status != "R");

  [PXDBBaseCury(typeof (GLTrialBalanceImportMap.ledgerID))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Credit Total", Enabled = false)]
  [PXFormula(typeof (Mult<Add<GLTrialBalanceImportMap.liabilityTotal, GLTrialBalanceImportMap.incomeTotal>, Switch<Case<Where<Current<GLSetup.trialBalanceSign>, Equal<GLSetup.trialBalanceSign.normal>>, decimal1>, decimal_1>>))]
  public virtual Decimal? CreditTotalBalance
  {
    get => this._CreditTotalBalance;
    set => this._CreditTotalBalance = value;
  }

  [PXDBBaseCury(typeof (GLTrialBalanceImportMap.ledgerID))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Debit Total", Enabled = false)]
  [PXFormula(typeof (Add<GLTrialBalanceImportMap.assetTotal, GLTrialBalanceImportMap.expenseTotal>))]
  public virtual Decimal? DebitTotalBalance
  {
    get => this._DebitTotalBalance;
    set => this._DebitTotalBalance = value;
  }

  /// <summary>
  /// Total amount of details that have Account type = Liability
  /// </summary>
  [PXDBBaseCury(typeof (GLTrialBalanceImportMap.ledgerID))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Liability Total", Enabled = false)]
  public virtual Decimal? LiabilityTotal { get; set; }

  /// <summary>
  /// Total amount of details that have Account type = Income
  /// </summary>
  [PXDBBaseCury(typeof (GLTrialBalanceImportMap.ledgerID))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Income Total", Enabled = false)]
  public virtual Decimal? IncomeTotal { get; set; }

  /// <summary>
  /// Total amount of details that have Account type = Asset
  /// </summary>
  [PXDBBaseCury(typeof (GLTrialBalanceImportMap.ledgerID))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Asset Total", Enabled = false)]
  public virtual Decimal? AssetTotal { get; set; }

  /// <summary>
  /// Total amount of details that have Account type = Expense
  /// </summary>
  [PXDBBaseCury(typeof (GLTrialBalanceImportMap.ledgerID))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Expense Total", Enabled = false)]
  public virtual Decimal? ExpenseTotal { get; set; }

  [PXDBBaseCury(typeof (GLTrialBalanceImportMap.ledgerID))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Control Total", Enabled = false)]
  public virtual Decimal? TotalBalance
  {
    get => this._TotalBalance;
    set => this._TotalBalance = value;
  }

  [PXDBInt]
  [PXDefault(0)]
  public virtual int? LineCntr
  {
    get => this._LineCntr;
    set => this._LineCntr = value;
  }

  [PXNote(DescriptionField = typeof (GLTrialBalanceImportMap.number))]
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
  [PXUIField(DisplayName = "Created On", Enabled = false, IsReadOnly = true)]
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
  [PXUIField(DisplayName = "Last Modified On", Enabled = false, IsReadOnly = true)]
  public virtual DateTime? LastModifiedDateTime
  {
    get => this._LastModifiedDateTime;
    set => this._LastModifiedDateTime = value;
  }

  public class PK : PrimaryKeyOf<GLTrialBalanceImportMap>.By<GLTrialBalanceImportMap.number>
  {
    public static GLTrialBalanceImportMap Find(PXGraph graph, string number, PKFindOptions options = 0)
    {
      return (GLTrialBalanceImportMap) PrimaryKeyOf<GLTrialBalanceImportMap>.By<GLTrialBalanceImportMap.number>.FindBy(graph, (object) number, options);
    }
  }

  public static class FK
  {
    public class Ledger : 
      PrimaryKeyOf<Ledger>.By<Ledger.ledgerID>.ForeignKeyOf<GLTrialBalanceImportMap>.By<GLTrialBalanceImportMap.ledgerID>
    {
    }
  }

  public abstract class number : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  GLTrialBalanceImportMap.number>
  {
  }

  public abstract class orgBAccountID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    GLTrialBalanceImportMap.orgBAccountID>
  {
  }

  public abstract class organizationID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    GLTrialBalanceImportMap.organizationID>
  {
  }

  public abstract class batchNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    GLTrialBalanceImportMap.batchNbr>
  {
  }

  public abstract class importDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    GLTrialBalanceImportMap.importDate>
  {
  }

  public abstract class finPeriodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    GLTrialBalanceImportMap.finPeriodID>
  {
  }

  public abstract class begFinPeriod : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    GLTrialBalanceImportMap.begFinPeriod>
  {
  }

  public abstract class description : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    GLTrialBalanceImportMap.description>
  {
  }

  public abstract class ledgerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  GLTrialBalanceImportMap.ledgerID>
  {
  }

  public abstract class isHold : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  GLTrialBalanceImportMap.isHold>
  {
  }

  public abstract class status : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  GLTrialBalanceImportMap.status>
  {
  }

  public abstract class isEditable : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    GLTrialBalanceImportMap.isEditable>
  {
  }

  public abstract class creditTotalBalance : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    GLTrialBalanceImportMap.creditTotalBalance>
  {
  }

  public abstract class debitTotalBalance : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    GLTrialBalanceImportMap.debitTotalBalance>
  {
  }

  public abstract class liabilityTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    GLTrialBalanceImportMap.liabilityTotal>
  {
  }

  public abstract class incomeTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    GLTrialBalanceImportMap.incomeTotal>
  {
  }

  public abstract class assetTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    GLTrialBalanceImportMap.assetTotal>
  {
  }

  public abstract class expenseTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    GLTrialBalanceImportMap.expenseTotal>
  {
  }

  public abstract class totalBalance : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    GLTrialBalanceImportMap.totalBalance>
  {
  }

  public abstract class lineCntr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  GLTrialBalanceImportMap.lineCntr>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  GLTrialBalanceImportMap.noteID>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  GLTrialBalanceImportMap.Tstamp>
  {
  }

  public abstract class createdByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    GLTrialBalanceImportMap.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    GLTrialBalanceImportMap.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    GLTrialBalanceImportMap.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    GLTrialBalanceImportMap.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    GLTrialBalanceImportMap.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    GLTrialBalanceImportMap.lastModifiedDateTime>
  {
  }
}
