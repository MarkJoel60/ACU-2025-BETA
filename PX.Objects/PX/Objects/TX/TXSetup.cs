// Decompiled with JetBrains decompiler
// Type: PX.Objects.TX.TXSetup
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CS;
using PX.Objects.GL;
using System;

#nullable enable
namespace PX.Objects.TX;

/// <summary>
/// Provides access to the preferences of the Taxes module.
/// Can be edited by user through the Tax Preferences (TX103000) form.
/// </summary>
[PXPrimaryGraph(typeof (TXSetupMaint))]
[PXCacheName("Tax Preferences")]
[Serializable]
public class TXSetup : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected 
  #nullable disable
  byte[] _tstamp;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected DateTime? _LastModifiedDateTime;

  /// <summary>The numbering sequence used for Tax Adjustments.</summary>
  /// <value>
  /// This field is a link to a <see cref="T:PX.Objects.CS.Numbering" /> record.
  /// </value>
  [PXDBString(10, IsUnicode = true)]
  [PXDefault("APBILL")]
  [PXSelector(typeof (Numbering.numberingID), DescriptionField = typeof (Numbering.descr))]
  [PXUIField]
  public virtual string TaxAdjustmentNumberingID { get; set; }

  /// <summary>
  /// An expense account to book positive discrepancy by the credit side.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.GL.Account.AccountID" /> field.
  /// </value>
  [PXDefault]
  [Account(null)]
  [PXUIRequired(typeof (FeatureInstalled<FeaturesSet.netGrossEntryMode>))]
  public virtual int? TaxRoundingGainAcctID { get; set; }

  /// <summary>
  /// A subaccount to book positive discrepancy by the credit side. Visible if Subaccounts feature is activated.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.GL.Sub.SubID" /> field.
  /// </value>
  [PXDefault]
  [SubAccount(typeof (TXSetup.taxRoundingGainAcctID))]
  [PXUIRequired(typeof (FeatureInstalled<FeaturesSet.netGrossEntryMode>))]
  public virtual int? TaxRoundingGainSubID { get; set; }

  /// <summary>
  /// An expense account to book negative discrepancy by the debit side.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.GL.Account.AccountID" /> field.
  /// </value>
  [PXDefault]
  [Account(null)]
  [PXUIRequired(typeof (FeatureInstalled<FeaturesSet.netGrossEntryMode>))]
  public virtual int? TaxRoundingLossAcctID { get; set; }

  /// <summary>
  /// A subaccount to book negative discrepancy by the debit side. Visible if Subaccounts feature is activated.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.GL.Sub.SubID" /> field.
  /// </value>
  [PXDefault]
  [SubAccount(typeof (TXSetup.taxRoundingLossAcctID))]
  [PXUIRequired(typeof (FeatureInstalled<FeaturesSet.netGrossEntryMode>))]
  public virtual int? TaxRoundingLossSubID { get; set; }

  /// <summary>
  /// An external exemption certificate provider. The respective box is available if the Exemption Certificate Management feature is enabled.
  /// </summary>
  [PXDBString(15, IsUnicode = true)]
  [PXUIField]
  [ECMPluginSelector]
  public virtual string ECMProvider { get; set; }

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
    public class TaxRoundingGainAccount : 
      PrimaryKeyOf<PX.Objects.GL.Account>.By<PX.Objects.GL.Account.accountID>.ForeignKeyOf<TXSetup>.By<TXSetup.taxRoundingGainAcctID>
    {
    }

    public class TaxRoundingGainSubaccount : 
      PrimaryKeyOf<PX.Objects.GL.Sub>.By<PX.Objects.GL.Sub.subID>.ForeignKeyOf<TXSetup>.By<TXSetup.taxRoundingGainSubID>
    {
    }

    public class TaxRoundingLossAccount : 
      PrimaryKeyOf<PX.Objects.GL.Account>.By<PX.Objects.GL.Account.accountID>.ForeignKeyOf<TXSetup>.By<TXSetup.taxRoundingLossAcctID>
    {
    }

    public class TaxRoundingLossSubaccount : 
      PrimaryKeyOf<PX.Objects.GL.Sub>.By<PX.Objects.GL.Sub.subID>.ForeignKeyOf<TXSetup>.By<TXSetup.taxRoundingLossSubID>
    {
    }

    public class TaxAdjustmentNumberingSequence : 
      PrimaryKeyOf<Numbering>.By<Numbering.numberingID>.ForeignKeyOf<TXSetup>.By<TXSetup.taxAdjustmentNumberingID>
    {
    }
  }

  public abstract class taxAdjustmentNumberingID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    TXSetup.taxAdjustmentNumberingID>
  {
  }

  public abstract class taxRoundingGainAcctID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    TXSetup.taxRoundingGainAcctID>
  {
  }

  public abstract class taxRoundingGainSubID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    TXSetup.taxRoundingGainSubID>
  {
  }

  public abstract class taxRoundingLossAcctID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    TXSetup.taxRoundingLossAcctID>
  {
  }

  public abstract class taxRoundingLossSubID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    TXSetup.taxRoundingLossSubID>
  {
  }

  public abstract class eCMProvider : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  TXSetup.eCMProvider>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  TXSetup.Tstamp>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  TXSetup.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    TXSetup.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    TXSetup.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  TXSetup.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    TXSetup.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    TXSetup.lastModifiedDateTime>
  {
  }
}
