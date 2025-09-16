// Decompiled with JetBrains decompiler
// Type: PX.Objects.CS.Carrier
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CS.Attributes;
using PX.Objects.GL;
using System;

#nullable enable
namespace PX.Objects.CS;

/// <summary>
/// Represents a Carrier used by the company for shipping goods or a company's own
/// shipping option.
/// The records of this type are created and edited through the Ship Via Codes (CS207500) form
/// (which corresponds to the <see cref="T:PX.Objects.CS.CarrierMaint" /> graph).
/// If the <see cref="P:PX.Objects.CS.FeaturesSet.CarrierIntegration" /> feature is enabled, the corresponding settings and plugins
/// are defined through the Carriers (CS207700) form
/// (which corresponds to the <see cref="T:PX.Objects.CS.CarrierPluginMaint" /> graph).
/// </summary>
[PXPrimaryGraph(new Type[] {typeof (CarrierMaint)}, new Type[] {typeof (Select<Carrier, Where<Carrier.carrierID, Equal<Current<Carrier.carrierID>>>>)})]
[PXCacheName]
[Serializable]
public class Carrier : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected 
  #nullable disable
  string _CarrierID;
  protected string _CalcMethod;
  protected string _Description;
  protected string _TaxCategoryID;
  protected string _CalendarID;
  protected int? _FreightSalesAcctID;
  protected int? _FreightSalesSubID;
  protected int? _FreightExpenseAcctID;
  protected int? _FreightExpenseSubID;
  protected Decimal? _BaseRate;
  protected bool? _IsExternal;
  protected string _CarrierPluginID;
  protected string _PluginMethod;
  protected bool? _ConfirmationRequired;
  protected bool? _PackageRequired;
  protected bool? _IsCommonCarrier;
  protected bool? _ReturnLabel;
  protected bool? _ValidatePackedQty;
  protected bool? _IsExternalShippingApplication;
  protected string _ShippingApplicationType;
  protected Guid? _NoteID;
  protected byte[] _tstamp;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected DateTime? _LastModifiedDateTime;

  /// <summary>
  /// Key field.
  /// A unique code of a non-integrated carrier, a method of the integrated carrier or a shipping option of the company.
  /// </summary>
  [PXDBString(15, IsUnicode = true, IsKey = true, InputMask = ">aaaaaaaaaaaaaaa")]
  [PXDefault]
  [PXUIField]
  [PXSelector(typeof (Search<Carrier.carrierID>), CacheGlobal = true)]
  public virtual string CarrierID
  {
    get => this._CarrierID;
    set => this._CarrierID = value;
  }

  /// <summary>
  /// The method used to calculate freight charges using the rate breakdown specified in the related <see cref="T:PX.Objects.CS.FreightRate" /> records.
  /// </summary>
  /// <value>
  /// Allowed values are:
  /// <c>"P"</c> - Per Unit,
  /// <c>"N"</c> - Net (flat rates),
  /// <c>"M"</c> - Manual.
  /// Defaults to Manual (<c>"M"</c>).
  /// </value>
  [PXDBString(1, IsFixed = true)]
  [PXDefault("M")]
  [CarrierCalcMethod.List]
  [PXUIField(DisplayName = "Calculation Method")]
  public virtual string CalcMethod
  {
    get => this._CalcMethod;
    set => this._CalcMethod = value;
  }

  /// <summary>The description of the carrier or shipping option.</summary>
  [PXDBString(60, IsUnicode = true)]
  [PXDefault]
  [PXUIField]
  public virtual string Description
  {
    get => this._Description;
    set => this._Description = value;
  }

  /// <summary>
  /// A Boolean value that indicates whether the ship via code is active and can be used in documents.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Active")]
  public virtual bool? IsActive { get; set; }

  /// <summary>The delivery type of the ship via code.</summary>
  /// <value>
  /// Allowed values are:
  /// <list type="bullet">
  /// <item> <term><c>N</c></term> <description>N/A</description> </item>
  /// <item> <term><c>P</c></term> <description>Parcel</description> </item>
  /// <item> <term><c>L</c></term> <description>LTL/FTL</description> </item>
  /// </list>
  /// </value>
  [PXDBString(1, IsFixed = true)]
  [CarrierDeliveryTypeAttribute.List]
  [PXDefault("N")]
  [PXUIField(DisplayName = "Delivery Type")]
  public virtual string DeliveryType { get; set; }

  /// <summary>
  /// Identifier of the <see cref="T:PX.Objects.TX.TaxCategory">Tax Category</see> to be applied to the freight amount
  /// when goods are shipped with this shipping option.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.TX.TaxCategory.TaxCategoryID" /> field.
  /// </value>
  [PXDBString(15, IsUnicode = true)]
  [PXUIField(DisplayName = "Tax Category")]
  [PXSelector(typeof (PX.Objects.TX.TaxCategory.taxCategoryID), DescriptionField = typeof (PX.Objects.TX.TaxCategory.descr))]
  [PXRestrictor(typeof (Where<PX.Objects.TX.TaxCategory.active, Equal<True>>), "Tax Category '{0}' is inactive", new Type[] {typeof (PX.Objects.TX.TaxCategory.taxCategoryID)})]
  public virtual string TaxCategoryID
  {
    get => this._TaxCategoryID;
    set => this._TaxCategoryID = value;
  }

  /// <summary>
  /// The <see cref="T:PX.Objects.CS.CSCalendar">Calendar</see> associated with the carrier, which reflects its work hours and the days when it ships the goods.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.CS.CSCalendar.CalendarID" /> field.
  /// </value>
  [PXDBString(10, IsUnicode = true)]
  [PXUIField(DisplayName = "Calendar")]
  [PXSelector(typeof (Search<CSCalendar.calendarID>), DescriptionField = typeof (CSCalendar.description))]
  public virtual string CalendarID
  {
    get => this._CalendarID;
    set => this._CalendarID = value;
  }

  /// <summary>
  /// Identifier of the General Ledger income <see cref="T:PX.Objects.GL.Account" />, that is used to record the freight charges to be paid to the company.
  /// </summary>
  [Account]
  [PXDefault]
  [PXForeignReference(typeof (Field<Carrier.freightSalesAcctID>.IsRelatedTo<PX.Objects.GL.Account.accountID>))]
  public virtual int? FreightSalesAcctID
  {
    get => this._FreightSalesAcctID;
    set => this._FreightSalesAcctID = value;
  }

  /// <summary>
  /// Identifier of the General Ledger <see cref="T:PX.Objects.GL.Sub">Subaccount</see>, that is used to record the freight charges to be paid to the company.
  /// </summary>
  [SubAccount(typeof (Carrier.freightSalesAcctID))]
  [PXDefault]
  [PXForeignReference(typeof (Field<Carrier.freightSalesSubID>.IsRelatedTo<PX.Objects.GL.Sub.subID>))]
  public virtual int? FreightSalesSubID
  {
    get => this._FreightSalesSubID;
    set => this._FreightSalesSubID = value;
  }

  /// <summary>
  /// Identifier of the General Ledger expense <see cref="T:PX.Objects.GL.Account" />, that is used to record the freight charges to be paid to the Carrier.
  /// </summary>
  [Account]
  [PXDefault]
  [PXForeignReference(typeof (Field<Carrier.freightExpenseAcctID>.IsRelatedTo<PX.Objects.GL.Account.accountID>))]
  public virtual int? FreightExpenseAcctID
  {
    get => this._FreightExpenseAcctID;
    set => this._FreightExpenseAcctID = value;
  }

  /// <summary>
  /// Identifier of the General Ledger <see cref="T:PX.Objects.GL.Sub">Subaccount</see>, that is used to record the freight charges to be paid to the Carrier.
  /// </summary>
  [SubAccount(typeof (Carrier.freightExpenseAcctID))]
  [PXDefault]
  [PXForeignReference(typeof (Field<Carrier.freightExpenseSubID>.IsRelatedTo<PX.Objects.GL.Sub.subID>))]
  public virtual int? FreightExpenseSubID
  {
    get => this._FreightExpenseSubID;
    set => this._FreightExpenseSubID = value;
  }

  /// <summary>
  /// The flat-rate charge, which is added to the freight amount calculated according to the related <see cref="T:PX.Objects.CS.FreightRate" /> records.
  /// This field is relevan only if <see cref="P:PX.Objects.CS.Carrier.IsExternal" /> is set to <c>false</c>.
  /// </summary>
  /// <value>
  /// Defaults to <c>0.0</c>.
  /// </value>
  [PXDBDecimal(typeof (Search<CommonSetup.decPlPrcCst>))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Base Rate")]
  public virtual Decimal? BaseRate
  {
    get => this._BaseRate;
    set => this._BaseRate = value;
  }

  /// <summary>
  /// When set to <c>true</c>, indicates that the system must use a plugin, specified in the <see cref="P:PX.Objects.CS.Carrier.CarrierPluginID" /> field,
  /// to provide integration with the carrier for this shipping option.
  /// This field is relevant only if the <see cref="P:PX.Objects.CS.FeaturesSet.CarrierIntegration" /> feature is enabled.
  /// </summary>
  /// <value>
  /// Defaults to <c>false</c>.
  /// </value>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "External Plug-in")]
  public virtual bool? IsExternal
  {
    get => this._IsExternal;
    set => this._IsExternal = value;
  }

  /// <summary>
  /// Identifier of the <see cref="T:PX.Objects.CS.CarrierPlugin" /> used to provide integration with the carrier for this shipping option.
  /// This field is relevant only if <see cref="P:PX.Objects.CS.Carrier.IsExternal" /> is set to <c>true</c>.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.CS.CarrierPlugin.CarrierPluginID" /> field.
  /// </value>
  [PXUIField(DisplayName = "Carrier")]
  [PXActiveCarrierPluginSelector(typeof (Search<CarrierPlugin.carrierPluginID, Where<CarrierPlugin.pluginTypeName, IsNotNull>>), CacheGlobal = true)]
  public virtual string CarrierPluginID
  {
    get => this._CarrierPluginID;
    set => this._CarrierPluginID = value;
  }

  /// <summary>
  /// The code of the <see cref="!:CarrierPluginMethod">Service Method</see> of the <see cref="P:PX.Objects.CS.Carrier.CarrierPluginID">Carrier Plugin</see>
  /// that is used to provide integration with the carrier for this shipping option.
  /// This field is relevant only if <see cref="P:PX.Objects.CS.Carrier.IsExternal" /> is set to <c>true</c>.
  /// </summary>
  [PXDBString(100)]
  [PXUIField(DisplayName = "Service Method")]
  [CarrierMethodSelector]
  public virtual string PluginMethod
  {
    get => this._PluginMethod;
    set => this._PluginMethod = value;
  }

  /// <summary>
  /// When set to <c>true</c>, indicates that to confirm the <see cref="T:PX.Objects.SO.SOShipment">shipment</see> the system will require
  /// confirmation for each <see cref="T:PX.Objects.SO.SOPackageDetail">package</see> used for shipment with this shipment option.
  /// This field is relevant only if <see cref="P:PX.Objects.CS.Carrier.IsExternal" /> is set to <c>true</c>.
  /// </summary>
  /// <value>
  /// Defaults to <c>true</c>.
  /// </value>
  [PXDBBool]
  [PXDefault(true)]
  [PXUIField]
  public virtual bool? ConfirmationRequired
  {
    get => this._ConfirmationRequired;
    set => this._ConfirmationRequired = value;
  }

  /// <summary>
  /// When set to <c>true</c>, indicates that at least one <see cref="T:PX.Objects.SO.SOPackageDetail">package</see> must be specified
  /// to create (and confirm) a <see cref="T:PX.Objects.SO.SOShipment">shipment</see> with this shipment option.
  /// This field is relevant only if <see cref="P:PX.Objects.CS.Carrier.IsExternal" /> is set to <c>true</c>.
  /// </summary>
  /// <value>
  /// Defaults to <c>true</c>.
  /// </value>
  [PXDBBool]
  [PXDefault(true)]
  [PXUIField]
  public virtual bool? PackageRequired
  {
    get => this._PackageRequired;
    set => this._PackageRequired = value;
  }

  /// <summary>
  /// Indicates whether the carrier is a common carrier.
  /// Because common carriers deliver goods from a company branch to the customer location that is
  /// a selling point, the value of this field affects the set of taxes that applies to the corresponding invoice.
  /// </summary>
  /// <value>
  /// When set to <c>true</c>, the carrier is considered common and hence the taxes
  /// from the <see cref="!:AR.Customer.TaxZoneID">customer's tax zone</see> are applied.
  /// Otherwise, the system applies the taxes from the <see cref="T:PX.Objects.TX.TaxZone">taz zone</see> associated with the selling branch.
  /// </value>
  [PXDBBool]
  [PXDefault(true)]
  [PXUIField]
  public virtual bool? IsCommonCarrier
  {
    get => this._IsCommonCarrier;
    set => this._IsCommonCarrier = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Generate Return Label Automatically")]
  public virtual bool? ReturnLabel
  {
    get => this._ReturnLabel;
    set => this._ReturnLabel = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Validate Packed Quantities on Shipment Confirmation")]
  public virtual bool? ValidatePackedQty
  {
    get => this._ValidatePackedQty;
    set => this._ValidatePackedQty = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Use External Shipping Application")]
  public virtual bool? IsExternalShippingApplication
  {
    get => this._IsExternalShippingApplication;
    set => this._IsExternalShippingApplication = value;
  }

  [PXDBString(IsFixed = true, IsUnicode = true)]
  [ShippingApplicationTypes.List]
  [PXUIField(DisplayName = "Shipping Application")]
  public virtual string ShippingApplicationType
  {
    get => this._ShippingApplicationType;
    set => this._ShippingApplicationType = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Calculate Freight on Returns")]
  public virtual bool? CalcFreightOnReturn { get; set; }

  /// <summary>
  /// Identifier of the <see cref="T:PX.Data.Note">Note</see> object, associated with the document.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Data.Note.NoteID">Note.NoteID</see> field.
  /// </value>
  [PXNote]
  public virtual Guid? NoteID
  {
    get => this._NoteID;
    set => this._NoteID = value;
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

  public class PK : PrimaryKeyOf<Carrier>.By<Carrier.carrierID>
  {
    public static Carrier Find(PXGraph graph, string carrierID, PKFindOptions options = 0)
    {
      return (Carrier) PrimaryKeyOf<Carrier>.By<Carrier.carrierID>.FindBy(graph, (object) carrierID, options);
    }
  }

  public abstract class carrierID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Carrier.carrierID>
  {
  }

  public abstract class calcMethod : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Carrier.calcMethod>
  {
  }

  public abstract class description : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Carrier.description>
  {
  }

  public abstract class isActive : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  Carrier.isActive>
  {
  }

  public abstract class deliveryType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Carrier.deliveryType>
  {
  }

  public abstract class taxCategoryID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Carrier.taxCategoryID>
  {
  }

  public abstract class calendarID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Carrier.calendarID>
  {
  }

  public abstract class freightSalesAcctID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Carrier.freightSalesAcctID>
  {
  }

  public abstract class freightSalesSubID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Carrier.freightSalesSubID>
  {
  }

  public abstract class freightExpenseAcctID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    Carrier.freightExpenseAcctID>
  {
  }

  public abstract class freightExpenseSubID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    Carrier.freightExpenseSubID>
  {
  }

  public abstract class baseRate : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  Carrier.baseRate>
  {
  }

  public abstract class isExternal : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  Carrier.isExternal>
  {
  }

  public abstract class carrierPluginID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Carrier.carrierPluginID>
  {
  }

  public abstract class pluginMethod : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Carrier.pluginMethod>
  {
  }

  public abstract class confirmationRequired : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    Carrier.confirmationRequired>
  {
  }

  public abstract class packageRequired : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  Carrier.packageRequired>
  {
  }

  public abstract class isCommonCarrier : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  Carrier.isCommonCarrier>
  {
  }

  public abstract class returnLabel : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  Carrier.returnLabel>
  {
  }

  public abstract class validatePackedQty : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  Carrier.validatePackedQty>
  {
  }

  public abstract class isExternalShippingApplication : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    Carrier.isExternalShippingApplication>
  {
  }

  public abstract class shippingApplicationType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    Carrier.shippingApplicationType>
  {
  }

  public abstract class calcFreightOnReturn : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    Carrier.calcFreightOnReturn>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  Carrier.noteID>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  Carrier.Tstamp>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  Carrier.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    Carrier.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    Carrier.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  Carrier.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    Carrier.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    Carrier.lastModifiedDateTime>
  {
  }
}
