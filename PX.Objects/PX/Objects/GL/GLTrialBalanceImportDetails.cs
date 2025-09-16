// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.GLTrialBalanceImportDetails
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CM;
using PX.Objects.CS;
using PX.Objects.GL.DAC;
using System;

#nullable enable
namespace PX.Objects.GL;

[PXCacheName("Trial Balance Import Details")]
[Serializable]
public class GLTrialBalanceImportDetails : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected 
  #nullable disable
  string _MapNumber;
  protected int? _Line;
  protected string _ImportAccountCDError;
  protected string _ImportAccountCD;
  protected int? _MapAccountID;
  protected string _ImportSubAccountCDError;
  protected string _ImportSubAccountCD;
  protected int? _MapSubAccountID;
  protected int? _Status;
  protected Decimal? _YtdBalance;
  protected Decimal? _CuryYtdBalance;
  protected string _Description;
  protected string _AccountType;
  protected byte[] _tstamp;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected DateTime? _LastModifiedDateTime;

  [PXDBString(15, IsUnicode = true, IsKey = true)]
  [PXDBDefault(typeof (GLTrialBalanceImportMap.number))]
  [PXUIField(Visible = false)]
  [PXParent(typeof (Select<GLTrialBalanceImportMap, Where<GLTrialBalanceImportMap.number, Equal<Current<GLTrialBalanceImportDetails.mapNumber>>>>))]
  public virtual string MapNumber
  {
    get => this._MapNumber;
    set => this._MapNumber = value;
  }

  [PXDBInt(IsKey = true)]
  [PXLineNbr(typeof (GLTrialBalanceImportMap.lineCntr))]
  [PXUIField(Visible = false)]
  public virtual int? Line
  {
    get => this._Line;
    set => this._Line = value;
  }

  /// <summary>
  /// The text of the error, which is the result of the validation process
  /// </summary>
  [PXDBString(255 /*0xFF*/, IsUnicode = true)]
  [PXUIField(Visible = false)]
  public virtual string ImportBranchCDError { get; set; }

  /// <summary>
  /// Branch string value, which is the result of the import process
  /// </summary>
  [PXDBString(30, IsUnicode = true)]
  [PXUIField(DisplayName = "Imported Branch", FieldClass = "BRANCH", Required = false)]
  [PXDefault(typeof (SearchFor<Branch.branchCD>.In<SelectFromBase<Branch, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PX.Objects.GL.DAC.Organization>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Branch.organizationID, Equal<PX.Objects.GL.DAC.Organization.organizationID>>>>>.And<BqlOperand<PX.Objects.GL.DAC.Organization.organizationType, IBqlString>.IsNotEqual<OrganizationTypes.withBranchesNotBalancing>>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.GL.DAC.Organization.organizationType, Equal<OrganizationTypes.withBranchesBalancing>>>>>.And<BqlOperand<Branch.bAccountID, IBqlInt>.IsEqual<BqlField<GLTrialBalanceImportMap.orgBAccountID, IBqlInt>.FromCurrent>>>>>.Or<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.GL.DAC.Organization.organizationType, Equal<OrganizationTypes.withoutBranches>>>>>.And<BqlOperand<PX.Objects.GL.DAC.Organization.bAccountID, IBqlInt>.IsEqual<BqlField<GLTrialBalanceImportMap.orgBAccountID, IBqlInt>.FromCurrent>>>>>))]
  [PXDimensionSelector("BRANCH", typeof (Search<Branch.branchCD, Where<Branch.branchID, InsideBranchesOf<Current<GLTrialBalanceImportMap.orgBAccountID>>, And<Branch.active, Equal<True>, And<Where<Branch.bAccountID, RestrictByUserBranches<Current<AccessInfo.userName>>>>>>>))]
  [PersistError(typeof (GLTrialBalanceImportDetails.importBranchCDError))]
  public virtual string ImportBranchCD { get; set; }

  /// <summary>
  /// A reference to the <see cref="T:PX.Objects.GL.Branch" />
  /// An integer identifier of the Branch, which is the result of the validation process
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.GL.Branch.BranchID" /> field.
  /// </value>
  [Branch(null, null, false, true, false, DisplayName = "Mapped Branch", Enabled = false, FieldClass = "BRANCH")]
  public virtual int? MapBranchID { get; set; }

  [PXDBString(255 /*0xFF*/, IsUnicode = true)]
  [PXUIField(Visible = false)]
  public virtual string ImportAccountCDError
  {
    get => this._ImportAccountCDError;
    set => this._ImportAccountCDError = value;
  }

  [PXDBString(10, IsUnicode = true)]
  [PXUIField(DisplayName = "Imported Account", FieldClass = "ACCOUNT")]
  [PXDimensionSelector("ACCOUNT", typeof (Search<Account.accountCD, Where2<Match<Current<AccessInfo.userName>>, And<Account.accountingType, Equal<AccountEntityType.gLAccount>, And<Where<Current<GLSetup.ytdNetIncAccountID>, IsNull, Or<Account.accountID, NotEqual<Current<GLSetup.ytdNetIncAccountID>>>>>>>>), DescriptionField = typeof (Account.description))]
  [PersistError(typeof (GLTrialBalanceImportDetails.importAccountCDError))]
  public virtual string ImportAccountCD
  {
    get => this._ImportAccountCD;
    set => this._ImportAccountCD = value;
  }

  [Account(DisplayName = "Mapped Account", Enabled = false)]
  public virtual int? MapAccountID
  {
    get => this._MapAccountID;
    set => this._MapAccountID = value;
  }

  [PXDBString(255 /*0xFF*/, IsUnicode = true)]
  [PXUIField(Visible = false)]
  public virtual string ImportSubAccountCDError
  {
    get => this._ImportSubAccountCDError;
    set => this._ImportSubAccountCDError = value;
  }

  [PXDBString(30, IsUnicode = true)]
  [PXUIField(DisplayName = "Imported Subaccount", FieldClass = "SUBACCOUNT")]
  [PXDimensionSelector("SUBACCOUNT", typeof (Search<Sub.subCD, Where<Match<Current<AccessInfo.userName>>>>))]
  [PersistError(typeof (GLTrialBalanceImportDetails.importSubAccountCDError))]
  public virtual string ImportSubAccountCD
  {
    get => this._ImportSubAccountCD;
    set => this._ImportSubAccountCD = value;
  }

  [SubAccount(typeof (GLTrialBalanceImportDetails.mapAccountID), DisplayName = "Mapped Subaccount", Enabled = false)]
  public virtual int? MapSubAccountID
  {
    get => this._MapSubAccountID;
    set => this._MapSubAccountID = value;
  }

  [PXBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Selected")]
  public virtual bool? Selected { get; set; } = new bool?(false);

  [PXDBInt]
  [TrialBalanceImportStatus]
  [PXDefault(0)]
  [PXUIField(DisplayName = "Status", Enabled = false)]
  public virtual int? Status
  {
    get => this._Status;
    set => this._Status = value;
  }

  [PXDBBaseCury(typeof (GLTrialBalanceImportMap.ledgerID))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "YTD Balance")]
  [PXUnboundFormula(typeof (Switch<Case<Where<GLTrialBalanceImportDetails.accountType, Equal<PX.Objects.GL.AccountType.liability>>, GLTrialBalanceImportDetails.ytdBalance>, decimal0>), typeof (SumCalc<GLTrialBalanceImportMap.liabilityTotal>))]
  [PXUnboundFormula(typeof (Switch<Case<Where<GLTrialBalanceImportDetails.accountType, Equal<PX.Objects.GL.AccountType.income>>, GLTrialBalanceImportDetails.ytdBalance>, decimal0>), typeof (SumCalc<GLTrialBalanceImportMap.incomeTotal>))]
  [PXUnboundFormula(typeof (Switch<Case<Where<GLTrialBalanceImportDetails.accountType, Equal<PX.Objects.GL.AccountType.asset>>, GLTrialBalanceImportDetails.ytdBalance>, decimal0>), typeof (SumCalc<GLTrialBalanceImportMap.assetTotal>))]
  [PXUnboundFormula(typeof (Switch<Case<Where<GLTrialBalanceImportDetails.accountType, Equal<PX.Objects.GL.AccountType.expense>>, GLTrialBalanceImportDetails.ytdBalance>, decimal0>), typeof (SumCalc<GLTrialBalanceImportMap.expenseTotal>))]
  public virtual Decimal? YtdBalance
  {
    get => this._YtdBalance;
    set => this._YtdBalance = value;
  }

  [PXDBCury(typeof (GLTrialBalanceImportDetails.accountCuryID))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  [PXFormula(typeof (IIf<Where<GLTrialBalanceImportDetails.importAccountCD, IsNotNull, Or<Current<Ledger.ledgerID>, IsNotNull>>, GLTrialBalanceImportDetails.ytdBalance, GLTrialBalanceImportDetails.ytdBalance>))]
  public virtual Decimal? CuryYtdBalance
  {
    get => this._CuryYtdBalance;
    set => this._CuryYtdBalance = value;
  }

  [PXString]
  [PXUIField(DisplayName = "Description", IsReadOnly = true, Enabled = false)]
  [PXFormula(typeof (Selector<GLTrialBalanceImportDetails.mapAccountID, Account.description>))]
  public virtual string Description
  {
    get => this._Description;
    set => this._Description = value;
  }

  [PXString(1)]
  [PX.Objects.GL.AccountType.List]
  [PXUIField(DisplayName = "Type", IsReadOnly = true, Enabled = false)]
  [PXFormula(typeof (Selector<GLTrialBalanceImportDetails.mapAccountID, Account.type>))]
  public virtual string AccountType
  {
    get => this._AccountType;
    set => this._AccountType = value;
  }

  [PXString(5, IsUnicode = true)]
  [PXFormula(typeof (Selector<GLTrialBalanceImportDetails.importAccountCD, Account.curyID>))]
  public virtual string AccountCuryID { get; set; }

  [PXDBTimestamp]
  public virtual byte[] tstamp
  {
    get => this._tstamp;
    set => this._tstamp = value;
  }

  [PXDBCreatedByID]
  [PXUIField(Visible = false, Enabled = false)]
  public virtual Guid? CreatedByID
  {
    get => this._CreatedByID;
    set => this._CreatedByID = value;
  }

  [PXDBCreatedByScreenID]
  [PXUIField(Visible = false, Enabled = false)]
  public virtual string CreatedByScreenID
  {
    get => this._CreatedByScreenID;
    set => this._CreatedByScreenID = value;
  }

  [PXDBCreatedDateTime]
  [PXUIField(Visible = false, Enabled = false)]
  public virtual DateTime? CreatedDateTime
  {
    get => this._CreatedDateTime;
    set => this._CreatedDateTime = value;
  }

  [PXDBLastModifiedByID]
  [PXUIField(Visible = false, Enabled = false)]
  public virtual Guid? LastModifiedByID
  {
    get => this._LastModifiedByID;
    set => this._LastModifiedByID = value;
  }

  [PXDBLastModifiedByScreenID]
  [PXUIField(Visible = false, Enabled = false)]
  public virtual string LastModifiedByScreenID
  {
    get => this._LastModifiedByScreenID;
    set => this._LastModifiedByScreenID = value;
  }

  [PXDBLastModifiedDateTime]
  [PXUIField(Visible = false, Enabled = false)]
  public virtual DateTime? LastModifiedDateTime
  {
    get => this._LastModifiedDateTime;
    set => this._LastModifiedDateTime = value;
  }

  public class PK : 
    PrimaryKeyOf<GLTrialBalanceImportDetails>.By<GLTrialBalanceImportDetails.mapNumber, GLTrialBalanceImportDetails.line>
  {
    public static GLTrialBalanceImportDetails Find(
      PXGraph graph,
      string mapNumber,
      int? line,
      PKFindOptions options = 0)
    {
      return (GLTrialBalanceImportDetails) PrimaryKeyOf<GLTrialBalanceImportDetails>.By<GLTrialBalanceImportDetails.mapNumber, GLTrialBalanceImportDetails.line>.FindBy(graph, (object) mapNumber, (object) line, options);
    }
  }

  public abstract class mapNumber : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    GLTrialBalanceImportDetails.mapNumber>
  {
  }

  public abstract class line : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  GLTrialBalanceImportDetails.line>
  {
  }

  public abstract class importBranchCDError : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    GLTrialBalanceImportDetails.importBranchCDError>
  {
  }

  public abstract class importBranchCD : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    GLTrialBalanceImportDetails.importBranchCD>
  {
  }

  public abstract class mapBranchID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    GLTrialBalanceImportDetails.mapBranchID>
  {
  }

  public abstract class importAccountCDError : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    GLTrialBalanceImportDetails.importAccountCDError>
  {
  }

  public abstract class importAccountCD : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    GLTrialBalanceImportDetails.importAccountCD>
  {
  }

  public abstract class mapAccountID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    GLTrialBalanceImportDetails.mapAccountID>
  {
  }

  public abstract class importSubAccountCDError : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    GLTrialBalanceImportDetails.importSubAccountCDError>
  {
  }

  public abstract class importSubAccountCD : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    GLTrialBalanceImportDetails.importSubAccountCD>
  {
  }

  public abstract class mapSubAccountID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    GLTrialBalanceImportDetails.mapSubAccountID>
  {
  }

  public abstract class selected : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    GLTrialBalanceImportDetails.selected>
  {
  }

  public abstract class status : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  GLTrialBalanceImportDetails.status>
  {
  }

  public abstract class ytdBalance : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    GLTrialBalanceImportDetails.ytdBalance>
  {
  }

  public abstract class curyYtdBalance : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    GLTrialBalanceImportDetails.curyYtdBalance>
  {
  }

  public abstract class description : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    GLTrialBalanceImportDetails.description>
  {
  }

  public abstract class accountType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    GLTrialBalanceImportDetails.accountType>
  {
  }

  public abstract class accountCuryID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    GLTrialBalanceImportDetails.accountCuryID>
  {
  }

  public abstract class Tstamp : 
    BqlType<
    #nullable enable
    IBqlByteArray, byte[]>.Field<
    #nullable disable
    GLTrialBalanceImportDetails.Tstamp>
  {
  }

  public abstract class createdByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    GLTrialBalanceImportDetails.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    GLTrialBalanceImportDetails.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    GLTrialBalanceImportDetails.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    GLTrialBalanceImportDetails.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    GLTrialBalanceImportDetails.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    GLTrialBalanceImportDetails.lastModifiedDateTime>
  {
  }
}
