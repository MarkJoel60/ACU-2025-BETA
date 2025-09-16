// Decompiled with JetBrains decompiler
// Type: PX.Objects.CM.Extensions.Currency
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.EP;
using PX.Objects.CS;
using PX.Objects.GL;
using System;

#nullable enable
namespace PX.Objects.CM.Extensions;

/// <summary>
/// Stores financial settings associated with currencies, thus complementing the <see cref="T:PX.Objects.CM.Extensions.CurrencyList" /> DAC type.
/// While <see cref="T:PX.Objects.CM.Extensions.CurrencyList" /> holds only general information, such as code and precision, the <see cref="T:PX.Objects.CM.Extensions.Currency" /> DAC provides information
/// on all accounts and subaccounts associated with a particular currency, such as the Realized Gain and Loss account and subaccount.
/// The <see cref="T:PX.Objects.CM.Extensions.Currency" /> DAC also exposes fields with general currency information (such as <see cref="P:PX.Objects.CM.Extensions.Currency.Description" />),
/// which are mapped to the corresponding fields in the <see cref="T:PX.Objects.CM.Extensions.CurrencyList" /> DAC by means of <see cref="T:PX.Data.PXDBScalarAttribute" />.
/// The records of this type (as well as the <see cref="T:PX.Objects.CM.Extensions.CurrencyList" /> records) are edited on the Currencies (CM202000) form (which corresponds to the <see cref="T:PX.Objects.CM.CurrencyMaint" /> graph).
/// </summary>
[PXPrimaryGraph(new Type[] {typeof (CurrencyMaint)}, new Type[] {typeof (Select<Currency, Where<Currency.curyID, Equal<Current<Currency.curyID>>>>)})]
[PXCacheName("Currency")]
[Serializable]
public class Currency : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage, IPXCurrency
{
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
  public virtual 
  #nullable disable
  string CuryID { get; set; }

  /// <summary>
  /// Identifier of the Realized Gain <see cref="T:PX.Objects.GL.Account" /> associated with the currency.
  /// Required field.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.GL.Account.AccountID" /> field.
  /// </value>
  [PXDefault]
  [Account(null)]
  public virtual int? RealGainAcctID { get; set; }

  /// <summary>
  /// Identifier of the Realized Gain <see cref="T:PX.Objects.GL.Sub">Subaccount</see> associated with the currency.
  /// Required field.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.GL.Sub.SubID" /> field.
  /// </value>
  [PXDefault]
  [SubAccount(typeof (Currency.realGainAcctID))]
  public virtual int? RealGainSubID { get; set; }

  /// <summary>
  /// Identifier of the Realized Loss <see cref="T:PX.Objects.GL.Account" /> associated with the currency.
  /// Required field.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.GL.Account.AccountID" /> field.
  /// </value>
  [PXDefault]
  [Account(null)]
  public virtual int? RealLossAcctID { get; set; }

  /// <summary>
  /// Identifier of the Realized Loss <see cref="T:PX.Objects.GL.Sub">Subaccount</see> associated with the currency.
  /// Required field.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.GL.Sub.SubID" /> field.
  /// </value>
  [PXDefault]
  [SubAccount(typeof (Currency.realLossAcctID))]
  public virtual int? RealLossSubID { get; set; }

  /// <summary>
  /// Identifier of the Revaluation Gain <see cref="T:PX.Objects.GL.Account" /> associated with the currency.
  /// Required field.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.GL.Account.AccountID" /> field.
  /// </value>
  [PXDefault]
  [Account(null)]
  public virtual int? RevalGainAcctID { get; set; }

  /// <summary>
  /// Identifier of the Revaluation Gain <see cref="T:PX.Objects.GL.Sub">Subaccount</see> associated with the currency.
  /// Required field.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.GL.Sub.SubID" /> field.
  /// </value>
  [PXDefault]
  [SubAccount(typeof (Currency.revalGainAcctID))]
  public virtual int? RevalGainSubID { get; set; }

  /// <summary>
  /// Identifier of the Revaluation Loss <see cref="T:PX.Objects.GL.Account" /> associated with the currency.
  /// Required field.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.GL.Account.AccountID" /> field.
  /// </value>
  [PXDefault]
  [Account(null)]
  public virtual int? RevalLossAcctID { get; set; }

  /// <summary>
  /// Identifier of the Revaluation Loss <see cref="T:PX.Objects.GL.Sub">Subaccount</see> associated with the currency.
  /// Required field.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.GL.Sub.SubID" /> field.
  /// </value>
  [PXDefault]
  [SubAccount(typeof (Currency.revalLossAcctID))]
  public virtual int? RevalLossSubID { get; set; }

  /// <summary>
  /// Identifier of the Accounts Receivable Provisioning <see cref="T:PX.Objects.GL.Account" /> associated with the currency.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.GL.Account.AccountID" /> field.
  /// </value>
  [Account(null, DisplayName = "AR Provisioning Account", DescriptionField = typeof (PX.Objects.GL.Account.description))]
  public virtual int? ARProvAcctID { get; set; }

  /// <summary>
  /// Identifier of the Accounts Receivable Provisioning <see cref="T:PX.Objects.GL.Sub">Subaccount</see> associated with the currency.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.GL.Sub.SubID" /> field.
  /// </value>
  [SubAccount(typeof (Currency.aRProvAcctID), DescriptionField = typeof (PX.Objects.GL.Sub.description), DisplayName = "AR Provisioning Subaccount")]
  public virtual int? ARProvSubID { get; set; }

  /// <summary>
  /// Identifier of the Accounts Payable Provisioning <see cref="T:PX.Objects.GL.Account" /> associated with the currency.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.GL.Account.AccountID" /> field.
  /// </value>
  [Account(null, DisplayName = "AP Provisioning Account", DescriptionField = typeof (PX.Objects.GL.Account.description))]
  public virtual int? APProvAcctID { get; set; }

  /// <summary>
  /// Identifier of the Accounts Payable Provisioning <see cref="T:PX.Objects.GL.Sub">Subaccount</see> associated with the currency.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.GL.Sub.SubID" /> field.
  /// </value>
  [SubAccount(typeof (Currency.aPProvAcctID), DescriptionField = typeof (PX.Objects.GL.Sub.description), DisplayName = "AP Provisioning Subaccount")]
  public virtual int? APProvSubID { get; set; }

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
  public virtual int? TranslationGainAcctID { get; set; }

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
  public virtual int? TranslationGainSubID { get; set; }

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
  public virtual int? TranslationLossAcctID { get; set; }

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
  public virtual int? TranslationLossSubID { get; set; }

  /// <summary>
  /// Identifier of the Unrealized Gain <see cref="T:PX.Objects.GL.Account" /> associated with the currency.
  /// Required field.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.GL.Account.AccountID" /> field.
  /// </value>
  [PXDefault]
  [Account(null)]
  public virtual int? UnrealizedGainAcctID { get; set; }

  /// <summary>
  /// Identifier of the Unrealized Gain <see cref="T:PX.Objects.GL.Sub">Subaccount</see> associated with the currency.
  /// Required field.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.GL.Sub.SubID" /> field.
  /// </value>
  [PXDefault]
  [SubAccount(typeof (Currency.unrealizedGainAcctID))]
  public virtual int? UnrealizedGainSubID { get; set; }

  /// <summary>
  /// Identifier of the Unrealized Loss <see cref="T:PX.Objects.GL.Account" /> associated with the currency.
  /// Required field.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.GL.Account.AccountID" /> field.
  /// </value>
  [PXDefault]
  [Account(null)]
  public virtual int? UnrealizedLossAcctID { get; set; }

  /// <summary>
  /// Identifier of the Unrealized Loss <see cref="T:PX.Objects.GL.Sub">Subaccount</see> associated with the currency.
  /// Required field.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.GL.Sub.SubID" /> field.
  /// </value>
  [PXDefault]
  [SubAccount(typeof (Currency.unrealizedLossAcctID))]
  public virtual int? UnrealizedLossSubID { get; set; }

  /// <summary>
  /// Identifier of the Rounding Gain <see cref="T:PX.Objects.GL.Account" /> associated with the currency.
  /// Required field.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.GL.Account.AccountID" /> field.
  /// </value>
  [PXDefault]
  [Account(null)]
  public virtual int? RoundingGainAcctID { get; set; }

  /// <summary>
  /// Identifier of the Rounding Gain <see cref="T:PX.Objects.GL.Sub">Subaccount</see> associated with the currency.
  /// Required field.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.GL.Sub.SubID" /> field.
  /// </value>
  [PXDefault]
  [SubAccount(typeof (Currency.roundingGainAcctID))]
  public virtual int? RoundingGainSubID { get; set; }

  /// <summary>
  /// Identifier of the Rounding Loss <see cref="T:PX.Objects.GL.Account" /> associated with the currency.
  /// Required field.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.GL.Account.AccountID" /> field.
  /// </value>
  [PXDefault]
  [Account(null)]
  public virtual int? RoundingLossAcctID { get; set; }

  /// <summary>
  /// Identifier of the Rounding Loss <see cref="T:PX.Objects.GL.Sub">Subaccount</see> associated with the currency.
  /// Required field.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.GL.Sub.SubID" /> field.
  /// </value>
  [PXDefault]
  [SubAccount(typeof (Currency.roundingLossAcctID))]
  public virtual int? RoundingLossSubID { get; set; }

  /// <summary>The user-defined description of the currency.</summary>
  [PXString(IsUnicode = true)]
  [PXDBScalar(typeof (Search<CurrencyList.description, Where<CurrencyList.curyID, Equal<Currency.curyID>>>))]
  [PXUIField]
  public virtual string Description { get; set; }

  /// <summary>The symbol of the currency.</summary>
  [PXString(IsUnicode = true)]
  [PXDBScalar(typeof (Search<CurrencyList.curySymbol, Where<CurrencyList.curyID, Equal<Currency.curyID>>>))]
  [PXUIField(DisplayName = "Currency Symbol")]
  public virtual string CurySymbol { get; set; }

  /// <summary>The caption (the name) of the currency.</summary>
  [PXString(IsUnicode = true)]
  [PXDBScalar(typeof (Search<CurrencyList.curyCaption, Where<CurrencyList.curyID, Equal<Currency.curyID>>>))]
  [PXUIField(DisplayName = "Currency Caption")]
  public virtual string CuryCaption { get; set; }

  /// <summary>
  /// The number of digits after the decimal point used in operations with the currency.
  /// </summary>
  /// <value>Minimum allowed value is 0, maximum - 4.</value>
  [PXShort(MinValue = 0, MaxValue = 4)]
  [PXDBScalar(typeof (Search<CurrencyList.decimalPlaces, Where<CurrencyList.curyID, Equal<Currency.curyID>>>))]
  [PXUIField(DisplayName = "Decimal Precision")]
  public virtual short? DecimalPlaces { get; set; }

  /// <summary>
  /// Identifier of the <see cref="T:PX.Data.Note">Note</see> object, associated with the document.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Data.Note.NoteID">Note.NoteID</see> field.
  /// </value>
  [PXNote(DescriptionField = typeof (Currency.curyID))]
  public virtual Guid? NoteID { get; set; }

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
