// Decompiled with JetBrains decompiler
// Type: PX.Objects.CM.Currency
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.EP;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CS;
using PX.Objects.GL;
using System;

#nullable enable
namespace PX.Objects.CM;

/// <summary>
/// Stores financial settings associated with currencies, thus complementing the <see cref="T:PX.Objects.CM.CurrencyList" /> DAC type.
/// While <see cref="T:PX.Objects.CM.CurrencyList" /> holds only general information, such as code and precision, the <see cref="T:PX.Objects.CM.Currency" /> DAC provides information
/// on all accounts and subaccounts associated with a particular currency, such as the Realized Gain and Loss account and subaccount.
/// The <see cref="T:PX.Objects.CM.Currency" /> DAC also exposes fields with general currency information (such as <see cref="P:PX.Objects.CM.Currency.Description" />),
/// which are mapped to the corresponding fields in the <see cref="T:PX.Objects.CM.CurrencyList" /> DAC by means of <see cref="T:PX.Data.PXDBScalarAttribute" />.
/// The records of this type (as well as the <see cref="T:PX.Objects.CM.CurrencyList" /> records) are edited on the Currencies (CM202000) form (which corresponds to the <see cref="T:PX.Objects.CM.CurrencyMaint" /> graph).
/// </summary>
[PXPrimaryGraph(new Type[] {typeof (CurrencyMaint)}, new Type[] {typeof (Select<Currency, Where<Currency.curyID, Equal<Current<Currency.curyID>>>>)})]
[PXCacheName("Currency")]
[Serializable]
public class Currency : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected 
  #nullable disable
  string _CuryID;
  protected int? _RealGainAcctID;
  protected int? _RealGainSubID;
  protected int? _RealLossAcctID;
  protected int? _RealLossSubID;
  protected int? _RevalGainAcctID;
  protected int? _RevalGainSubID;
  protected int? _RevalLossAcctID;
  protected int? _RevalLossSubID;
  protected int? _ARProvAcctID;
  protected int? _ARProvSubID;
  protected int? _APProvAcctID;
  protected int? _APProvSubID;
  protected int? _TranslationGainAcctID;
  protected int? _TranslationGainSubID;
  protected int? _TranslationLossAcctID;
  protected int? _TranslationLossSubID;
  protected int? _UnrealizedGainAcctID;
  protected int? _UnrealizedGainSubID;
  protected int? _UnrealizedLossAcctID;
  protected int? _UnrealizedLossSubID;
  protected int? _RoundingGainAcctID;
  protected int? _RoundingGainSubID;
  protected int? _RoundingLossAcctID;
  protected int? _RoundingLossSubID;
  protected string _Description;
  protected string _CurySymbol;
  protected string _CuryCaption;
  protected short? _DecimalPlaces;
  protected long? _CuryInfoID;
  protected long? _CuryInfoBaseID;
  protected Decimal? _RoundingLimit;
  protected byte[] _tstamp;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected DateTime? _LastModifiedDateTime;

  /// <summary>
  /// Key field.
  /// Unique identifier of the currency.
  /// </summary>
  [PXDBString(5, IsUnicode = true, IsKey = true, InputMask = ">LLLLL")]
  [PXDBDefault(typeof (CurrencyList.curyID))]
  [PXUIField]
  [PXSelector(typeof (Search<CurrencyList.curyID>), CacheGlobal = true)]
  [PXParent(typeof (Select<CurrencyList, Where<CurrencyList.curyID, Equal<Current<Currency.curyID>>>>))]
  [PXFieldDescription]
  public virtual string CuryID
  {
    get => this._CuryID;
    set => this._CuryID = value;
  }

  /// <summary>
  /// Identifier of the Realized Gain <see cref="T:PX.Objects.GL.Account" /> associated with the currency.
  /// Required field.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.GL.Account.AccountID" /> field.
  /// </value>
  [PXDefault]
  [Account(null)]
  [PXForeignReference(typeof (Field<Currency.realGainAcctID>.IsRelatedTo<PX.Objects.GL.Account.accountID>))]
  public virtual int? RealGainAcctID
  {
    get => this._RealGainAcctID;
    set => this._RealGainAcctID = value;
  }

  /// <summary>
  /// Identifier of the Realized Gain <see cref="T:PX.Objects.GL.Sub">Subaccount</see> associated with the currency.
  /// Required field.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.GL.Sub.SubID" /> field.
  /// </value>
  [PXDefault]
  [SubAccount(typeof (Currency.realGainAcctID))]
  [PXForeignReference(typeof (Field<Currency.realGainSubID>.IsRelatedTo<PX.Objects.GL.Sub.subID>))]
  public virtual int? RealGainSubID
  {
    get => this._RealGainSubID;
    set => this._RealGainSubID = value;
  }

  /// <summary>
  /// Identifier of the Realized Loss <see cref="T:PX.Objects.GL.Account" /> associated with the currency.
  /// Required field.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.GL.Account.AccountID" /> field.
  /// </value>
  [PXDefault]
  [Account(null)]
  [PXForeignReference(typeof (Field<Currency.realLossAcctID>.IsRelatedTo<PX.Objects.GL.Account.accountID>))]
  public virtual int? RealLossAcctID
  {
    get => this._RealLossAcctID;
    set => this._RealLossAcctID = value;
  }

  /// <summary>
  /// Identifier of the Realized Loss <see cref="T:PX.Objects.GL.Sub">Subaccount</see> associated with the currency.
  /// Required field.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.GL.Sub.SubID" /> field.
  /// </value>
  [PXDefault]
  [SubAccount(typeof (Currency.realLossAcctID))]
  [PXForeignReference(typeof (Field<Currency.realLossSubID>.IsRelatedTo<PX.Objects.GL.Sub.subID>))]
  public virtual int? RealLossSubID
  {
    get => this._RealLossSubID;
    set => this._RealLossSubID = value;
  }

  /// <summary>
  /// Identifier of the Revaluation Gain <see cref="T:PX.Objects.GL.Account" /> associated with the currency.
  /// Required field.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.GL.Account.AccountID" /> field.
  /// </value>
  [PXDefault]
  [Account(null)]
  [PXForeignReference(typeof (Field<Currency.revalGainAcctID>.IsRelatedTo<PX.Objects.GL.Account.accountID>))]
  public virtual int? RevalGainAcctID
  {
    get => this._RevalGainAcctID;
    set => this._RevalGainAcctID = value;
  }

  /// <summary>
  /// Identifier of the Revaluation Gain <see cref="T:PX.Objects.GL.Sub">Subaccount</see> associated with the currency.
  /// Required field.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.GL.Sub.SubID" /> field.
  /// </value>
  [PXDefault]
  [SubAccount(typeof (Currency.revalGainAcctID))]
  [PXForeignReference(typeof (Field<Currency.revalGainSubID>.IsRelatedTo<PX.Objects.GL.Sub.subID>))]
  public virtual int? RevalGainSubID
  {
    get => this._RevalGainSubID;
    set => this._RevalGainSubID = value;
  }

  /// <summary>
  /// Identifier of the Revaluation Loss <see cref="T:PX.Objects.GL.Account" /> associated with the currency.
  /// Required field.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.GL.Account.AccountID" /> field.
  /// </value>
  [PXDefault]
  [Account(null)]
  [PXForeignReference(typeof (Field<Currency.revalLossAcctID>.IsRelatedTo<PX.Objects.GL.Account.accountID>))]
  public virtual int? RevalLossAcctID
  {
    get => this._RevalLossAcctID;
    set => this._RevalLossAcctID = value;
  }

  /// <summary>
  /// Identifier of the Revaluation Loss <see cref="T:PX.Objects.GL.Sub">Subaccount</see> associated with the currency.
  /// Required field.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.GL.Sub.SubID" /> field.
  /// </value>
  [PXDefault]
  [SubAccount(typeof (Currency.revalLossAcctID))]
  [PXForeignReference(typeof (Field<Currency.revalLossSubID>.IsRelatedTo<PX.Objects.GL.Sub.subID>))]
  public virtual int? RevalLossSubID
  {
    get => this._RevalLossSubID;
    set => this._RevalLossSubID = value;
  }

  /// <summary>
  /// Identifier of the Accounts Receivable Provisioning <see cref="T:PX.Objects.GL.Account" /> associated with the currency.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.GL.Account.AccountID" /> field.
  /// </value>
  [Account(null, DisplayName = "AR Provisioning Account", DescriptionField = typeof (PX.Objects.GL.Account.description))]
  [PXForeignReference(typeof (Field<Currency.aRProvAcctID>.IsRelatedTo<PX.Objects.GL.Account.accountID>))]
  public virtual int? ARProvAcctID
  {
    get => this._ARProvAcctID;
    set => this._ARProvAcctID = value;
  }

  /// <summary>
  /// Identifier of the Accounts Receivable Provisioning <see cref="T:PX.Objects.GL.Sub">Subaccount</see> associated with the currency.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.GL.Sub.SubID" /> field.
  /// </value>
  [SubAccount(typeof (Currency.aRProvAcctID), DescriptionField = typeof (PX.Objects.GL.Sub.description), DisplayName = "AR Provisioning Subaccount")]
  [PXForeignReference(typeof (Field<Currency.aRProvSubID>.IsRelatedTo<PX.Objects.GL.Sub.subID>))]
  public virtual int? ARProvSubID
  {
    get => this._ARProvSubID;
    set => this._ARProvSubID = value;
  }

  /// <summary>
  /// Identifier of the Accounts Payable Provisioning <see cref="T:PX.Objects.GL.Account" /> associated with the currency.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.GL.Account.AccountID" /> field.
  /// </value>
  [Account(null, DisplayName = "AP Provisioning Account", DescriptionField = typeof (PX.Objects.GL.Account.description))]
  [PXForeignReference(typeof (Field<Currency.aPProvAcctID>.IsRelatedTo<PX.Objects.GL.Account.accountID>))]
  public virtual int? APProvAcctID
  {
    get => this._APProvAcctID;
    set => this._APProvAcctID = value;
  }

  /// <summary>
  /// Identifier of the Accounts Payable Provisioning <see cref="T:PX.Objects.GL.Sub">Subaccount</see> associated with the currency.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.GL.Sub.SubID" /> field.
  /// </value>
  [SubAccount(typeof (Currency.aPProvAcctID), DescriptionField = typeof (PX.Objects.GL.Sub.description), DisplayName = "AP Provisioning Subaccount")]
  [PXForeignReference(typeof (Field<Currency.aPProvSubID>.IsRelatedTo<PX.Objects.GL.Sub.subID>))]
  public virtual int? APProvSubID
  {
    get => this._APProvSubID;
    set => this._APProvSubID = value;
  }

  /// <summary>
  /// Identifier of the Translation Gain <see cref="T:PX.Objects.GL.Account" /> associated with the currency.
  /// Required field.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.GL.Account.AccountID" /> field.
  /// </value>
  [PXDefault]
  [PXUIRequired(typeof (FeatureInstalled<FeaturesSet.finStatementCurTranslation>))]
  [Account(null)]
  [PXForeignReference(typeof (Field<Currency.translationGainAcctID>.IsRelatedTo<PX.Objects.GL.Account.accountID>))]
  public virtual int? TranslationGainAcctID
  {
    get => this._TranslationGainAcctID;
    set => this._TranslationGainAcctID = value;
  }

  /// <summary>
  /// Identifier of the Translation Gain <see cref="T:PX.Objects.GL.Sub">Subaccount</see> associated with the currency.
  /// Required field.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.GL.Sub.SubID" /> field.
  /// </value>
  [PXDefault]
  [PXUIRequired(typeof (FeatureInstalled<FeaturesSet.finStatementCurTranslation>))]
  [SubAccount(typeof (Currency.translationGainAcctID))]
  [PXForeignReference(typeof (Field<Currency.translationGainSubID>.IsRelatedTo<PX.Objects.GL.Sub.subID>))]
  public virtual int? TranslationGainSubID
  {
    get => this._TranslationGainSubID;
    set => this._TranslationGainSubID = value;
  }

  /// <summary>
  /// Identifier of the Translation Loss <see cref="T:PX.Objects.GL.Account" /> associated with the currency.
  /// Required field.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.GL.Account.AccountID" /> field.
  /// </value>
  [PXDefault]
  [PXUIRequired(typeof (FeatureInstalled<FeaturesSet.finStatementCurTranslation>))]
  [Account(null)]
  [PXForeignReference(typeof (Field<Currency.translationLossAcctID>.IsRelatedTo<PX.Objects.GL.Account.accountID>))]
  public virtual int? TranslationLossAcctID
  {
    get => this._TranslationLossAcctID;
    set => this._TranslationLossAcctID = value;
  }

  /// <summary>
  /// Identifier of the Translation Loss <see cref="T:PX.Objects.GL.Sub">Subaccount</see> associated with the currency.
  /// Required field.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.GL.Sub.SubID" /> field.
  /// </value>
  [PXDefault]
  [PXUIRequired(typeof (FeatureInstalled<FeaturesSet.finStatementCurTranslation>))]
  [SubAccount(typeof (Currency.translationLossAcctID))]
  [PXForeignReference(typeof (Field<Currency.translationLossSubID>.IsRelatedTo<PX.Objects.GL.Sub.subID>))]
  public virtual int? TranslationLossSubID
  {
    get => this._TranslationLossSubID;
    set => this._TranslationLossSubID = value;
  }

  /// <summary>
  /// Identifier of the Unrealized Gain <see cref="T:PX.Objects.GL.Account" /> associated with the currency.
  /// Required field.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.GL.Account.AccountID" /> field.
  /// </value>
  [PXDefault]
  [Account(null)]
  [PXForeignReference(typeof (Field<Currency.unrealizedGainAcctID>.IsRelatedTo<PX.Objects.GL.Account.accountID>))]
  public virtual int? UnrealizedGainAcctID
  {
    get => this._UnrealizedGainAcctID;
    set => this._UnrealizedGainAcctID = value;
  }

  /// <summary>
  /// Identifier of the Unrealized Gain <see cref="T:PX.Objects.GL.Sub">Subaccount</see> associated with the currency.
  /// Required field.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.GL.Sub.SubID" /> field.
  /// </value>
  [PXDefault]
  [SubAccount(typeof (Currency.unrealizedGainAcctID))]
  [PXForeignReference(typeof (Field<Currency.unrealizedGainSubID>.IsRelatedTo<PX.Objects.GL.Sub.subID>))]
  public virtual int? UnrealizedGainSubID
  {
    get => this._UnrealizedGainSubID;
    set => this._UnrealizedGainSubID = value;
  }

  /// <summary>
  /// Identifier of the Unrealized Loss <see cref="T:PX.Objects.GL.Account" /> associated with the currency.
  /// Required field.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.GL.Account.AccountID" /> field.
  /// </value>
  [PXDefault]
  [Account(null)]
  [PXForeignReference(typeof (Field<Currency.unrealizedLossAcctID>.IsRelatedTo<PX.Objects.GL.Account.accountID>))]
  public virtual int? UnrealizedLossAcctID
  {
    get => this._UnrealizedLossAcctID;
    set => this._UnrealizedLossAcctID = value;
  }

  /// <summary>
  /// Identifier of the Unrealized Loss <see cref="T:PX.Objects.GL.Sub">Subaccount</see> associated with the currency.
  /// Required field.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.GL.Sub.SubID" /> field.
  /// </value>
  [PXDefault]
  [SubAccount(typeof (Currency.unrealizedLossAcctID))]
  [PXForeignReference(typeof (Field<Currency.unrealizedLossSubID>.IsRelatedTo<PX.Objects.GL.Sub.subID>))]
  public virtual int? UnrealizedLossSubID
  {
    get => this._UnrealizedLossSubID;
    set => this._UnrealizedLossSubID = value;
  }

  /// <summary>
  /// Identifier of the Rounding Gain <see cref="T:PX.Objects.GL.Account" /> associated with the currency.
  /// Required field.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.GL.Account.AccountID" /> field.
  /// </value>
  [PXDefault]
  [Account(null)]
  [PXForeignReference(typeof (Field<Currency.roundingGainAcctID>.IsRelatedTo<PX.Objects.GL.Account.accountID>))]
  public virtual int? RoundingGainAcctID
  {
    get => this._RoundingGainAcctID;
    set => this._RoundingGainAcctID = value;
  }

  /// <summary>
  /// Identifier of the Rounding Gain <see cref="T:PX.Objects.GL.Sub">Subaccount</see> associated with the currency.
  /// Required field.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.GL.Sub.SubID" /> field.
  /// </value>
  [PXDefault]
  [SubAccount(typeof (Currency.roundingGainAcctID))]
  [PXForeignReference(typeof (Field<Currency.roundingGainSubID>.IsRelatedTo<PX.Objects.GL.Sub.subID>))]
  public virtual int? RoundingGainSubID
  {
    get => this._RoundingGainSubID;
    set => this._RoundingGainSubID = value;
  }

  /// <summary>
  /// Identifier of the Rounding Loss <see cref="T:PX.Objects.GL.Account" /> associated with the currency.
  /// Required field.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.GL.Account.AccountID" /> field.
  /// </value>
  [PXDefault]
  [Account(null)]
  [PXForeignReference(typeof (Field<Currency.roundingLossAcctID>.IsRelatedTo<PX.Objects.GL.Account.accountID>))]
  public virtual int? RoundingLossAcctID
  {
    get => this._RoundingLossAcctID;
    set => this._RoundingLossAcctID = value;
  }

  /// <summary>
  /// Identifier of the Rounding Loss <see cref="T:PX.Objects.GL.Sub">Subaccount</see> associated with the currency.
  /// Required field.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.GL.Sub.SubID" /> field.
  /// </value>
  [PXDefault]
  [SubAccount(typeof (Currency.roundingLossAcctID))]
  [PXForeignReference(typeof (Field<Currency.roundingLossSubID>.IsRelatedTo<PX.Objects.GL.Sub.subID>))]
  public virtual int? RoundingLossSubID
  {
    get => this._RoundingLossSubID;
    set => this._RoundingLossSubID = value;
  }

  /// <summary>The user-defined description of the currency.</summary>
  [PXString(IsUnicode = true)]
  [PXDBScalar(typeof (Search<CurrencyList.description, Where<CurrencyList.curyID, Equal<Currency.curyID>>>))]
  [PXUIField]
  public virtual string Description
  {
    get => this._Description;
    set => this._Description = value;
  }

  /// <summary>The symbol of the currency.</summary>
  [PXString(IsUnicode = true)]
  [PXDBScalar(typeof (Search<CurrencyList.curySymbol, Where<CurrencyList.curyID, Equal<Currency.curyID>>>))]
  [PXUIField(DisplayName = "Currency Symbol")]
  public virtual string CurySymbol
  {
    get => this._CurySymbol;
    set => this._CurySymbol = value;
  }

  /// <summary>The caption (the name) of the currency.</summary>
  [PXString(IsUnicode = true)]
  [PXDBScalar(typeof (Search<CurrencyList.curyCaption, Where<CurrencyList.curyID, Equal<Currency.curyID>>>))]
  [PXUIField(DisplayName = "Currency Caption")]
  public virtual string CuryCaption
  {
    get => this._CuryCaption;
    set => this._CuryCaption = value;
  }

  /// <summary>
  /// The number of digits after the decimal point used in operations with the currency.
  /// </summary>
  /// <value>Minimum allowed value is 0, maximum - 4.</value>
  [PXShort(MinValue = 0, MaxValue = 4)]
  [PXDBScalar(typeof (Search<CurrencyList.decimalPlaces, Where<CurrencyList.curyID, Equal<Currency.curyID>>>))]
  [PXUIField(DisplayName = "Decimal Precision")]
  public virtual short? DecimalPlaces
  {
    get => this._DecimalPlaces;
    set => this._DecimalPlaces = value;
  }

  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Use AR Preferences Settings")]
  public virtual bool? UseARPreferencesSettings { get; set; }

  [PXDBDecimalString(2)]
  [InvoicePrecision.List]
  [PXDefault(TypeCode.Decimal, "0.05")]
  [PXUIField(DisplayName = "Rounding Precision")]
  public virtual Decimal? ARInvoicePrecision { get; set; }

  [PXDBString(1, IsFixed = true)]
  [PXDefault("N")]
  [PXUIField(DisplayName = "Rounding Rule for Invoices")]
  [InvoiceRounding.List]
  public virtual string ARInvoiceRounding { get; set; }

  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Use AP Preferences Settings")]
  public virtual bool? UseAPPreferencesSettings { get; set; }

  [PXDBDecimalString(2)]
  [InvoicePrecision.List]
  [PXDefault(TypeCode.Decimal, "0.05")]
  [PXUIField(DisplayName = "Rounding Precision")]
  public virtual Decimal? APInvoicePrecision { get; set; }

  [PXDBString(1, IsFixed = true)]
  [PXDefault("N")]
  [PXUIField(DisplayName = "Rounding Rule for Bills")]
  [InvoiceRounding.List]
  public virtual string APInvoiceRounding { get; set; }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.CM.CurrencyInfo">CurrencyInfo</see> object associated with the currency.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="!:CurrencyInfoID" /> field.
  /// </value>
  [PXDBLong]
  public virtual long? CuryInfoID
  {
    get => this._CuryInfoID;
    set => this._CuryInfoID = value;
  }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.CM.CurrencyInfo">CurrencyInfo</see> object associated with the currency.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="!:CurrencyInfoID" /> field.
  /// </value>
  [PXDBLong]
  public virtual long? CuryInfoBaseID
  {
    get => this._CuryInfoBaseID;
    set => this._CuryInfoBaseID = value;
  }

  [PXDBDecimal(2)]
  [PXDefault(TypeCode.Decimal, "0")]
  [PXUIField(DisplayName = "Rounding Limit")]
  public virtual Decimal? RoundingLimit
  {
    get => this._RoundingLimit;
    set => this._RoundingLimit = value;
  }

  /// <summary>
  /// Identifier of the <see cref="T:PX.Data.Note">Note</see> object, associated with the document.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Data.Note.NoteID">Note.NoteID</see> field.
  /// </value>
  [PXNote(DescriptionField = typeof (Currency.curyID))]
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

  public class PK : PrimaryKeyOf<Currency>.By<Currency.curyID>
  {
    public static Currency Find(PXGraph graph, string curyID, PKFindOptions options = 0)
    {
      return (Currency) PrimaryKeyOf<Currency>.By<Currency.curyID>.FindBy(graph, (object) curyID, options);
    }
  }

  public static class FK
  {
    public class RealGainAccount : 
      PrimaryKeyOf<PX.Objects.GL.Account>.By<PX.Objects.GL.Account.accountID>.ForeignKeyOf<Currency>.By<Currency.realGainAcctID>
    {
    }

    public class RealGainSubaccount : 
      PrimaryKeyOf<PX.Objects.GL.Sub>.By<PX.Objects.GL.Sub.subID>.ForeignKeyOf<Currency>.By<Currency.realGainSubID>
    {
    }

    public class RealLossAccount : 
      PrimaryKeyOf<PX.Objects.GL.Account>.By<PX.Objects.GL.Account.accountID>.ForeignKeyOf<Currency>.By<Currency.realLossAcctID>
    {
    }

    public class RealLossSubaccount : 
      PrimaryKeyOf<PX.Objects.GL.Sub>.By<PX.Objects.GL.Sub.subID>.ForeignKeyOf<Currency>.By<Currency.realLossSubID>
    {
    }

    public class RevalGainAccount : 
      PrimaryKeyOf<PX.Objects.GL.Account>.By<PX.Objects.GL.Account.accountID>.ForeignKeyOf<Currency>.By<Currency.revalGainAcctID>
    {
    }

    public class RevalGainSubaccount : 
      PrimaryKeyOf<PX.Objects.GL.Sub>.By<PX.Objects.GL.Sub.subID>.ForeignKeyOf<Currency>.By<Currency.revalGainSubID>
    {
    }

    public class RevalLossAccount : 
      PrimaryKeyOf<PX.Objects.GL.Account>.By<PX.Objects.GL.Account.accountID>.ForeignKeyOf<Currency>.By<Currency.revalLossAcctID>
    {
    }

    public class RevalLossSubaccount : 
      PrimaryKeyOf<PX.Objects.GL.Sub>.By<PX.Objects.GL.Sub.subID>.ForeignKeyOf<Currency>.By<Currency.revalLossSubID>
    {
    }

    public class APProvisioningAccount : 
      PrimaryKeyOf<PX.Objects.GL.Account>.By<PX.Objects.GL.Account.accountID>.ForeignKeyOf<Currency>.By<Currency.aPProvAcctID>
    {
    }

    public class APProvisioningSubaccount : 
      PrimaryKeyOf<PX.Objects.GL.Sub>.By<PX.Objects.GL.Sub.subID>.ForeignKeyOf<Currency>.By<Currency.aPProvSubID>
    {
    }

    public class TranslationGainAccount : 
      PrimaryKeyOf<PX.Objects.GL.Account>.By<PX.Objects.GL.Account.accountID>.ForeignKeyOf<Currency>.By<Currency.translationGainAcctID>
    {
    }

    public class TranslationGainSubaccount : 
      PrimaryKeyOf<PX.Objects.GL.Sub>.By<PX.Objects.GL.Sub.subID>.ForeignKeyOf<Currency>.By<Currency.translationGainSubID>
    {
    }

    public class TranslationLossAccount : 
      PrimaryKeyOf<PX.Objects.GL.Account>.By<PX.Objects.GL.Account.accountID>.ForeignKeyOf<Currency>.By<Currency.translationLossAcctID>
    {
    }

    public class TranslationLossSubaccount : 
      PrimaryKeyOf<PX.Objects.GL.Sub>.By<PX.Objects.GL.Sub.subID>.ForeignKeyOf<Currency>.By<Currency.translationLossSubID>
    {
    }

    public class UnrealizedGainAccount : 
      PrimaryKeyOf<PX.Objects.GL.Account>.By<PX.Objects.GL.Account.accountID>.ForeignKeyOf<Currency>.By<Currency.unrealizedGainAcctID>
    {
    }

    public class UnrealizedGainSubaccount : 
      PrimaryKeyOf<PX.Objects.GL.Sub>.By<PX.Objects.GL.Sub.subID>.ForeignKeyOf<Currency>.By<Currency.unrealizedGainSubID>
    {
    }

    public class RoundingGainAccount : 
      PrimaryKeyOf<PX.Objects.GL.Account>.By<PX.Objects.GL.Account.accountID>.ForeignKeyOf<Currency>.By<Currency.roundingGainAcctID>
    {
    }

    public class RoundingGainSubaccount : 
      PrimaryKeyOf<PX.Objects.GL.Sub>.By<PX.Objects.GL.Sub.subID>.ForeignKeyOf<Currency>.By<Currency.roundingGainSubID>
    {
    }

    public class RoundingLossAccount : 
      PrimaryKeyOf<PX.Objects.GL.Account>.By<PX.Objects.GL.Account.accountID>.ForeignKeyOf<Currency>.By<Currency.roundingLossAcctID>
    {
    }

    public class RoundingLossSubaccount : 
      PrimaryKeyOf<PX.Objects.GL.Sub>.By<PX.Objects.GL.Sub.subID>.ForeignKeyOf<Currency>.By<Currency.roundingLossSubID>
    {
    }
  }

  public abstract class curyID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Currency.curyID>
  {
  }

  public abstract class realGainAcctID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Currency.realGainAcctID>
  {
  }

  public abstract class realGainSubID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Currency.realGainSubID>
  {
  }

  public abstract class realLossAcctID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Currency.realLossAcctID>
  {
  }

  public abstract class realLossSubID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Currency.realLossSubID>
  {
  }

  public abstract class revalGainAcctID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Currency.revalGainAcctID>
  {
  }

  public abstract class revalGainSubID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Currency.revalGainSubID>
  {
  }

  public abstract class revalLossAcctID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Currency.revalLossAcctID>
  {
  }

  public abstract class revalLossSubID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Currency.revalLossSubID>
  {
  }

  public abstract class aRProvAcctID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Currency.aRProvAcctID>
  {
  }

  public abstract class aRProvSubID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Currency.aRProvSubID>
  {
  }

  public abstract class aPProvAcctID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Currency.aPProvAcctID>
  {
  }

  public abstract class aPProvSubID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Currency.aPProvSubID>
  {
  }

  public abstract class translationGainAcctID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    Currency.translationGainAcctID>
  {
  }

  public abstract class translationGainSubID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    Currency.translationGainSubID>
  {
  }

  public abstract class translationLossAcctID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    Currency.translationLossAcctID>
  {
  }

  public abstract class translationLossSubID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    Currency.translationLossSubID>
  {
  }

  public abstract class unrealizedGainAcctID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    Currency.unrealizedGainAcctID>
  {
  }

  public abstract class unrealizedGainSubID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    Currency.unrealizedGainSubID>
  {
  }

  public abstract class unrealizedLossAcctID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    Currency.unrealizedLossAcctID>
  {
  }

  public abstract class unrealizedLossSubID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    Currency.unrealizedLossSubID>
  {
  }

  public abstract class roundingGainAcctID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Currency.roundingGainAcctID>
  {
  }

  public abstract class roundingGainSubID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Currency.roundingGainSubID>
  {
  }

  public abstract class roundingLossAcctID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Currency.roundingLossAcctID>
  {
  }

  public abstract class roundingLossSubID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Currency.roundingLossSubID>
  {
  }

  public abstract class description : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Currency.description>
  {
  }

  public abstract class curySymbol : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Currency.curySymbol>
  {
  }

  public abstract class curyCaption : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Currency.curyCaption>
  {
  }

  public abstract class decimalPlaces : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  Currency.decimalPlaces>
  {
  }

  public abstract class useARPreferencesSettings : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    Currency.useARPreferencesSettings>
  {
  }

  public abstract class aRInvoicePrecision : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    Currency.aRInvoicePrecision>
  {
  }

  public abstract class aRInvoiceRounding : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    Currency.aRInvoiceRounding>
  {
  }

  public abstract class useAPPreferencesSettings : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    Currency.useAPPreferencesSettings>
  {
  }

  public abstract class aPInvoicePrecision : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    Currency.aPInvoicePrecision>
  {
  }

  public abstract class aPInvoiceRounding : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    Currency.aPInvoiceRounding>
  {
  }

  public abstract class curyInfoID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  Currency.curyInfoID>
  {
  }

  public abstract class curyInfoBaseID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  Currency.curyInfoID>
  {
  }

  public abstract class roundingLimit : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  Currency.roundingLimit>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  Currency.noteID>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  Currency.Tstamp>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  Currency.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    Currency.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    Currency.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  Currency.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    Currency.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    Currency.lastModifiedDateTime>
  {
  }
}
