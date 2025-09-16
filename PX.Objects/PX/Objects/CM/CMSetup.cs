// Decompiled with JetBrains decompiler
// Type: PX.Objects.CM.CMSetup
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CS;
using System;

#nullable enable
namespace PX.Objects.CM;

/// <summary>
/// Stores the settings of the Currency Management module.
/// The system holds one record of this type per company. The record is edited on the
/// Currency Management Preferences (CM101000) form, which corresponds to the <see cref="T:PX.Objects.CM.CMSetupMaint" /> graph.
/// </summary>
[PXPrimaryGraph(typeof (CMSetupMaint))]
[PXCacheName("Currency Management Preferences")]
[Serializable]
public class CMSetup : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected 
  #nullable disable
  string _BatchNumberingID;
  protected string _ExtRefNbrNumberingID;
  protected bool? _APCuryOverride;
  protected string _APRateTypeDflt;
  protected bool? _APRateTypeOverride;
  protected string _APRateTypeReval;
  protected bool? _ARCuryOverride;
  protected string _ARRateTypeDflt;
  protected bool? _ARRateTypeOverride;
  protected string _ARRateTypeReval;
  protected string _CARateTypeDflt;
  protected string _GLRateTypeDflt;
  protected string _GLRateTypeReval;
  protected Decimal? _RateVariance;
  protected bool? _RateVarianceWarn;
  protected bool? _AutoPostOption;
  protected string _TranslDefId;
  protected short? _RetainPeriodsNumber;
  protected string _TranslNumberingID;
  protected string _GainLossSubMask;
  protected byte[] _tstamp;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected DateTime? _LastModifiedDateTime;

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.CS.Numbering">Numbering Sequence</see> used to assign <see cref="!:Btach.BatchNbr">Btach Numbers</see>
  /// to the batches originating from the Currency Mangement module.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.CS.Numbering.NumberingID" /> field.
  /// </value>
  [PXDBString(10, IsUnicode = true)]
  [PXDefault("BATCH")]
  [PXSelector(typeof (Numbering.numberingID), DescriptionField = typeof (Numbering.descr))]
  [PXUIField]
  public virtual string BatchNumberingID
  {
    get => this._BatchNumberingID;
    set => this._BatchNumberingID = value;
  }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.CS.Numbering">Numbering Sequence</see> used to assign
  /// <see cref="P:PX.Objects.GL.GLTran.RefNbr">reference numbers</see> to the transactions generated during the process
  /// of revaluation of AP and AR accounts.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.CS.Numbering.NumberingID" /> field.
  /// </value>
  [PXDBString(10, IsUnicode = true)]
  [PXSelector(typeof (Numbering.numberingID), DescriptionField = typeof (Numbering.descr))]
  [PXUIField]
  public virtual string ExtRefNbrNumberingID
  {
    get => this._ExtRefNbrNumberingID;
    set => this._ExtRefNbrNumberingID = value;
  }

  /// <summary>
  /// When set to <c>true</c>, indicates that the system will allow to enter AP documents in the currency different from
  /// the <see cref="!:Vendor.CuryID">currency of the vendor</see>.
  /// The value of this field is used as a default for the <see cref="!:VendorClass.AllowOverrideCury" /> field of the
  /// <see cref="!:APSetup.DfltVendorClassID">default Vendor Class</see>. Its value in turn affects the values of the same fied of
  /// other vendor classes.
  /// </summary>
  /// <value>
  /// Defaults to <c>false</c>.
  /// </value>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Allow Vendor CurrencyID Override")]
  public virtual bool? APCuryOverride
  {
    get => this._APCuryOverride;
    set => this._APCuryOverride = value;
  }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.CM.CurrencyRateType">Rate Type</see> used for Accounts Payable documents by default.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.CM.CurrencyRateType.CuryRateTypeID" /> field.
  /// </value>
  [PXDBString(6, IsUnicode = true)]
  [PXDefault(typeof (Search<CurrencyRateType.curyRateTypeID>))]
  [PXForeignReference(typeof (Field<CMSetup.aPRateTypeDflt>.IsRelatedTo<CurrencyRateType.curyRateTypeID>))]
  [PXSelector(typeof (CurrencyRateType.curyRateTypeID), DescriptionField = typeof (CurrencyRateType.descr))]
  [PXUIField(DisplayName = "AP Rate Type")]
  public virtual string APRateTypeDflt
  {
    get => this._APRateTypeDflt;
    set => this._APRateTypeDflt = value;
  }

  /// <summary>
  /// When set to <c>true</c>, indicates that for AP documents the system allows to specify the rate type different from the <see cref="P:PX.Objects.CM.CMSetup.APRateTypeDflt" />.
  /// Similarly to the <see cref="P:PX.Objects.CM.CMSetup.APCuryOverride" /> field, this field affects actual vendors and documents through the <see cref="!:VendorClass.AllowOverrideRate" /> field of
  /// the <see cref="!:APSetup.DfltVendorClass" />.
  /// </summary>
  /// <value>
  /// Defaults to <c>false</c>.
  /// </value>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Allow Vendor Rate Type Override")]
  public virtual bool? APRateTypeOverride
  {
    get => this._APRateTypeOverride;
    set => this._APRateTypeOverride = value;
  }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.CM.CurrencyRateType">Rate Type</see> used for revaluation of Accounts Payable.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.CM.CurrencyRateType.CuryRateTypeID" /> field.
  /// </value>
  [PXDBString(6, IsUnicode = true)]
  [PXForeignReference(typeof (Field<CMSetup.aPRateTypeReval>.IsRelatedTo<CurrencyRateType.curyRateTypeID>))]
  [PXSelector(typeof (CurrencyRateType.curyRateTypeID), DescriptionField = typeof (CurrencyRateType.descr))]
  [PXUIField(DisplayName = "AP Revaluation Rate Type")]
  public virtual string APRateTypeReval
  {
    get => this._APRateTypeReval;
    set => this._APRateTypeReval = value;
  }

  /// <summary>
  /// When set to <c>true</c>, indicates that the system will allow to enter AR documents in the currency different from
  /// the <see cref="!:Customer.CuryID">currency of the customer</see>.
  /// The value of this field is used as a default for the <see cref="!:CustomerClass.AllowOverrideCury" /> field of the
  /// <see cref="!:ARSetup.DfltCustomerClassID">default Customer Class</see>. Its value in turn affects the values of the same fied of
  /// other customer classes.
  /// </summary>
  /// <value>
  /// Defaults to <c>false</c>.
  /// </value>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Allow Customer Currency ID Override")]
  public virtual bool? ARCuryOverride
  {
    get => this._ARCuryOverride;
    set => this._ARCuryOverride = value;
  }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.CM.CurrencyRateType">Rate Type</see> used for Accounts Receivable documents by default.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.CM.CurrencyRateType.CuryRateTypeID" /> field.
  /// </value>
  [PXDBString(6, IsUnicode = true)]
  [PXDefault(typeof (Search<CurrencyRateType.curyRateTypeID>))]
  [PXForeignReference(typeof (Field<CMSetup.aRRateTypeDflt>.IsRelatedTo<CurrencyRateType.curyRateTypeID>))]
  [PXSelector(typeof (CurrencyRateType.curyRateTypeID), DescriptionField = typeof (CurrencyRateType.descr))]
  [PXUIField(DisplayName = "AR Rate Type")]
  public virtual string ARRateTypeDflt
  {
    get => this._ARRateTypeDflt;
    set => this._ARRateTypeDflt = value;
  }

  /// <summary>
  /// Obsolete field.
  /// Replace by the <see cref="P:PX.Objects.AR.ARSetup.DefaultRateTypeID">ARSetup.DefaultRateTypeID</see> in the <see cref="T:PX.Objects.AR.ARSetup">AR Preferences</see>.
  /// </summary>
  [PXDBString(6, IsUnicode = true)]
  [PXDefault(typeof (Search<CurrencyRateType.curyRateTypeID>))]
  [PXForeignReference(typeof (Field<CMSetup.aRRateTypePrc>.IsRelatedTo<CurrencyRateType.curyRateTypeID>))]
  [PXSelector(typeof (CurrencyRateType.curyRateTypeID))]
  [PXUIField(DisplayName = "Sales Price Rate Type ")]
  [Obsolete("This field has been deprecated and will be removed in Acumatica ERP 2017R2.")]
  public virtual string ARRateTypePrc { get; set; }

  /// <summary>
  /// When set to <c>true</c>, indicates that for AR documents the system allows to specify the rate type different from the <see cref="P:PX.Objects.CM.CMSetup.ARRateTypeDflt" />.
  /// Similarly to the <see cref="P:PX.Objects.CM.CMSetup.ARCuryOverride" />, this field affects actual customers and documents through the <see cref="!:CustomerClass.AllowOverrideRate" /> field of
  /// the <see cref="!:ARSetup.DfltCustomerClass" />.
  /// </summary>
  /// <value>
  /// Defaults to <c>false</c>.
  /// </value>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Allow Customer Rate Type Override")]
  public virtual bool? ARRateTypeOverride
  {
    get => this._ARRateTypeOverride;
    set => this._ARRateTypeOverride = value;
  }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.CM.CurrencyRateType">Rate Type</see> used for revaluation of Accounts Receivable.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.CM.CurrencyRateType.CuryRateTypeID" /> field.
  /// </value>
  [PXDBString(6, IsUnicode = true)]
  [PXForeignReference(typeof (Field<CMSetup.aRRateTypeReval>.IsRelatedTo<CurrencyRateType.curyRateTypeID>))]
  [PXSelector(typeof (CurrencyRateType.curyRateTypeID), DescriptionField = typeof (CurrencyRateType.descr))]
  [PXUIField(DisplayName = "AR Revaluation Rate Type")]
  public virtual string ARRateTypeReval
  {
    get => this._ARRateTypeReval;
    set => this._ARRateTypeReval = value;
  }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.CM.CurrencyRateType">Rate Type</see> used for Cash Management documents by default.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.CM.CurrencyRateType.CuryRateTypeID" /> field.
  /// </value>
  [PXDBString(6, IsUnicode = true)]
  [PXDefault(typeof (Search<CurrencyRateType.curyRateTypeID>))]
  [PXForeignReference(typeof (Field<CMSetup.cARateTypeDflt>.IsRelatedTo<CurrencyRateType.curyRateTypeID>))]
  [PXSelector(typeof (CurrencyRateType.curyRateTypeID), DescriptionField = typeof (CurrencyRateType.descr))]
  [PXUIField(DisplayName = "CA Rate Type")]
  public virtual string CARateTypeDflt
  {
    get => this._CARateTypeDflt;
    set => this._CARateTypeDflt = value;
  }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.CM.CurrencyRateType">Rate Type</see> used for General Ledger transactions and for translations by default.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.CM.CurrencyRateType.CuryRateTypeID" /> field.
  /// </value>
  [PXDBString(6, IsUnicode = true)]
  [PXDefault(typeof (Search<CurrencyRateType.curyRateTypeID>))]
  [PXForeignReference(typeof (Field<CMSetup.gLRateTypeDflt>.IsRelatedTo<CurrencyRateType.curyRateTypeID>))]
  [PXSelector(typeof (CurrencyRateType.curyRateTypeID), DescriptionField = typeof (CurrencyRateType.descr))]
  [PXUIField(DisplayName = "GL Rate Type")]
  public virtual string GLRateTypeDflt
  {
    get => this._GLRateTypeDflt;
    set => this._GLRateTypeDflt = value;
  }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.CM.CurrencyRateType">Rate Type</see> used by default for revaluations performed for currency denominated accounts.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.CM.CurrencyRateType.CuryRateTypeID" /> field.
  /// </value>
  [PXDBString(6, IsUnicode = true)]
  [PXForeignReference(typeof (Field<CMSetup.gLRateTypeReval>.IsRelatedTo<CurrencyRateType.curyRateTypeID>))]
  [PXSelector(typeof (CurrencyRateType.curyRateTypeID), DescriptionField = typeof (CurrencyRateType.descr))]
  [PXUIField(DisplayName = "GL Revaluation Rate Type")]
  public virtual string GLRateTypeReval
  {
    get => this._GLRateTypeReval;
    set => this._GLRateTypeReval = value;
  }

  [PXDBString(6, IsUnicode = true)]
  [PXDefault(typeof (Search<CurrencyRateType.curyRateTypeID>))]
  [PXForeignReference(typeof (Field<CMSetup.pMRateTypeDflt>.IsRelatedTo<CurrencyRateType.curyRateTypeID>))]
  [PXSelector(typeof (CurrencyRateType.curyRateTypeID), DescriptionField = typeof (CurrencyRateType.descr))]
  [PXUIField(DisplayName = "PM Rate Type")]
  public virtual string PMRateTypeDflt { get; set; }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.CM.CurrencyRateType">Rate Type</see> used for the shipment freight by default.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.CM.CurrencyRateType.CuryRateTypeID" /> field.
  /// </value>
  [PXDBString(6, IsUnicode = true)]
  [PXDefault(typeof (Search<CurrencyRateType.curyRateTypeID>))]
  [PXForeignReference(typeof (Field<CMSetup.sOFreightRateTypeDflt>.IsRelatedTo<CurrencyRateType.curyRateTypeID>))]
  [PXSelector(typeof (CurrencyRateType.curyRateTypeID), DescriptionField = typeof (CurrencyRateType.descr))]
  [PXUIField(DisplayName = "SO Freight Rate Type")]
  public virtual string SOFreightRateTypeDflt { get; set; }

  /// <summary>
  /// The maximum rate variance allowed for manually entered exchange rates expressed as a percent.
  /// If the currency rate entered by user differs from the applicable currency rate stored in the system by more than
  /// the specified percent of the latter, and the <see cref="P:PX.Objects.CM.CMSetup.RateVarianceWarn" /> field is set to <c>true</c>
  /// the system will display a warning.
  /// </summary>
  /// <value>
  /// Defaults to <c>0.0</c>, which means that the checks for rate variance are disabled.
  /// </value>
  [PXDBDecimal(0)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Rate Variance Allowed, %")]
  public virtual Decimal? RateVariance
  {
    get => this._RateVariance;
    set => this._RateVariance = value;
  }

  /// <summary>
  /// When set to <c>true</c> indicates that the system should display warnings about exceeding
  /// the <see cref="P:PX.Objects.CM.CMSetup.RateVariance">allowed rate variance</see> for manually entered currency rates.
  /// </summary>
  /// <value>
  /// Defaults to <c>false</c>.
  /// </value>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Warn About Rate Variance")]
  public virtual bool? RateVarianceWarn
  {
    get => this._RateVarianceWarn;
    set => this._RateVarianceWarn = value;
  }

  /// <summary>
  /// When set to <c>true</c>, indicates that the batches originating from the Currency Management module
  /// will be automatically posted to the General Ledger on release.
  /// </summary>
  /// <value>
  /// Defaults to <c>false</c>.
  /// </value>
  [PXDBBool]
  [PXDefault(true)]
  [PXUIField]
  public virtual bool? AutoPostOption
  {
    get => this._AutoPostOption;
    set => this._AutoPostOption = value;
  }

  /// <summary>
  /// A Boolean value that indicates (if set to <see langword="true" />) that the AR prepayment balance should be taken into account during revaluation of AR accounts.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Revalue AR Prepayment Balance")]
  public virtual bool? RevalueARPrepaymentsOption { get; set; }

  /// <summary>
  /// A Boolean value that indicates (if set to <see langword="true" />) that the AP prepayment balance should be taken into account during revaluation of AP accounts.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Revalue AP Prepayment Balance")]
  public virtual bool? RevalueAPPrepaymentsOption { get; set; }

  /// <summary>
  /// The identifier of the default <see cref="!:TransDef">type of translations</see> performed in the system.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.CM.TranslDef.TranslDefId" /> field.
  /// </value>
  [PXDBString(10, IsUnicode = true)]
  [PXUIField(DisplayName = "Default Translation ID")]
  [PXSelector(typeof (TranslDef.translDefId), DescriptionField = typeof (TranslDef.description))]
  public virtual string TranslDefId
  {
    get => this._TranslDefId;
    set => this._TranslDefId = value;
  }

  /// <summary>
  /// Defines for how many periods the translation records should be stored in the system.
  /// </summary>
  /// <value>
  /// Defaults to <c>99</c>.
  /// </value>
  [PXDBShort]
  [PXDefault(99)]
  [PXUIField(DisplayName = "Keep History For Periods")]
  public virtual short? RetainPeriodsNumber
  {
    get => this._RetainPeriodsNumber;
    set => this._RetainPeriodsNumber = value;
  }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.CS.Numbering">Numbering Sequence</see> used to assign the
  /// <see cref="P:PX.Objects.CM.TranslationHistory.ReferenceNbr">reference numbers</see> to the
  /// <see cref="T:PX.Objects.CM.TranslationHistory">Translation History</see> records.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.CS.Numbering.NumberingID" /> field.
  /// Defaults to <c>"TRANSLAT"</c>.
  /// </value>
  [PXDBString(10, IsUnicode = true)]
  [PXDefault("TRANSLAT")]
  [PXUIField(DisplayName = "Translation Numbering Sequence")]
  [PXSelector(typeof (Numbering.numberingID), DescriptionField = typeof (Numbering.descr))]
  public virtual string TranslNumberingID
  {
    get => this._TranslNumberingID;
    set => this._TranslNumberingID = value;
  }

  /// <summary>
  /// The subaccount mask that defines the rules of composing subaccounts for recording realized and unrealized gains and losses
  /// resulting from rounding and translations.
  /// </summary>
  /// <value>
  /// For the rules related to setting the value of this field, see the documentation for the Currency Management Preferences (CM101000) form.
  /// </value>
  [PXDefault]
  [GainLossSubAccountMask(DisplayName = "Combine Gain/Loss Sub. from")]
  public virtual string GainLossSubMask
  {
    get => this._GainLossSubMask;
    set => this._GainLossSubMask = value;
  }

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

  public static class FK
  {
    public class BatchNumberingSequence : 
      PrimaryKeyOf<Numbering>.By<Numbering.numberingID>.ForeignKeyOf<CMSetup>.By<CMSetup.batchNumberingID>
    {
    }

    public class BatchRefNumberNumberingSequence : 
      PrimaryKeyOf<Numbering>.By<Numbering.numberingID>.ForeignKeyOf<CMSetup>.By<CMSetup.extRefNbrNumberingID>
    {
    }

    public class TranslationNumberingSequence : 
      PrimaryKeyOf<Numbering>.By<Numbering.numberingID>.ForeignKeyOf<CMSetup>.By<CMSetup.translNumberingID>
    {
    }

    public class DefaultTranslation : 
      PrimaryKeyOf<TranslDef>.By<TranslDef.translDefId>.ForeignKeyOf<CMSetup>.By<CMSetup.translDefId>
    {
    }
  }

  public abstract class batchNumberingID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CMSetup.batchNumberingID>
  {
  }

  public abstract class extRefNbrNumberingID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CMSetup.extRefNbrNumberingID>
  {
  }

  public abstract class aPCuryOverride : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CMSetup.aPCuryOverride>
  {
  }

  public abstract class aPRateTypeDflt : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CMSetup.aPRateTypeDflt>
  {
  }

  public abstract class aPRateTypeOverride : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    CMSetup.aPRateTypeOverride>
  {
  }

  public abstract class aPRateTypeReval : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CMSetup.aPRateTypeReval>
  {
  }

  public abstract class aRCuryOverride : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CMSetup.aRCuryOverride>
  {
  }

  public abstract class aRRateTypeDflt : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CMSetup.aRRateTypeDflt>
  {
  }

  [Obsolete("This field has been deprecated and will be removed in Acumatica ERP 2017R2.")]
  public abstract class aRRateTypePrc : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CMSetup.aRRateTypePrc>
  {
  }

  public abstract class aRRateTypeOverride : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    CMSetup.aRRateTypeOverride>
  {
  }

  public abstract class aRRateTypeReval : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CMSetup.aRRateTypeReval>
  {
  }

  public abstract class cARateTypeDflt : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CMSetup.cARateTypeDflt>
  {
  }

  public abstract class gLRateTypeDflt : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CMSetup.gLRateTypeDflt>
  {
  }

  public abstract class gLRateTypeReval : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CMSetup.gLRateTypeReval>
  {
  }

  public abstract class pMRateTypeDflt : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CMSetup.pMRateTypeDflt>
  {
  }

  public abstract class sOFreightRateTypeDflt : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CMSetup.sOFreightRateTypeDflt>
  {
  }

  public abstract class rateVariance : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  CMSetup.rateVariance>
  {
  }

  public abstract class rateVarianceWarn : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CMSetup.rateVarianceWarn>
  {
  }

  public abstract class autoPostOption : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CMSetup.autoPostOption>
  {
  }

  public abstract class revalueARPrepaymentsOption : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    CMSetup.revalueARPrepaymentsOption>
  {
  }

  public abstract class revalueAPPrepaymentsOption : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    CMSetup.revalueAPPrepaymentsOption>
  {
  }

  public abstract class translDefId : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CMSetup.translDefId>
  {
  }

  public abstract class retainPeriodsNumber : 
    BqlType<
    #nullable enable
    IBqlShort, short>.Field<
    #nullable disable
    CMSetup.retainPeriodsNumber>
  {
  }

  public abstract class translNumberingID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CMSetup.translNumberingID>
  {
  }

  public abstract class gainLossSubMask : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CMSetup.gainLossSubMask>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  CMSetup.Tstamp>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  CMSetup.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CMSetup.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CMSetup.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  CMSetup.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CMSetup.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CMSetup.lastModifiedDateTime>
  {
  }
}
