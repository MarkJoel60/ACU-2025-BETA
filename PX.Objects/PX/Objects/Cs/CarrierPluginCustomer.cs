// Decompiled with JetBrains decompiler
// Type: PX.Objects.CS.CarrierPluginCustomer
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.AR;
using System;

#nullable enable
namespace PX.Objects.CS;

[PXCacheName("Carrier Plugin Customer")]
[Serializable]
public class CarrierPluginCustomer : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected 
  #nullable disable
  string _CarrierPluginID;
  protected int? _RecordID;
  protected int? _CustomerID;
  protected int? _CustomerLocationID;
  protected bool? _IsActive;
  protected string _CarrierAccount;
  protected string _PostalCode;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected DateTime? _LastModifiedDateTime;
  protected byte[] _tstamp;

  [PXUIField(DisplayName = "Carrier")]
  [PXSelector(typeof (CarrierPlugin.carrierPluginID))]
  [PXParent(typeof (Select<CarrierPlugin, Where<CarrierPlugin.carrierPluginID, Equal<Current<CarrierPluginCustomer.carrierPluginID>>>>))]
  [PXDBString(15, IsUnicode = true, IsKey = true)]
  [PXDefault(typeof (CarrierPlugin.carrierPluginID))]
  public virtual string CarrierPluginID
  {
    get => this._CarrierPluginID;
    set => this._CarrierPluginID = value;
  }

  [PXDBIdentity(IsKey = true)]
  public virtual int? RecordID
  {
    get => this._RecordID;
    set => this._RecordID = value;
  }

  [Customer(DescriptionField = typeof (Customer.acctName), Filterable = true)]
  [PXUIField(DisplayName = "Customer ID")]
  [PXDefault]
  public virtual int? CustomerID
  {
    get => this._CustomerID;
    set => this._CustomerID = value;
  }

  [PXCheckUnique(new System.Type[] {typeof (CarrierPluginCustomer.carrierPluginID), typeof (CarrierPluginCustomer.customerID)}, IgnoreNulls = false)]
  [LocationID(typeof (Where<PX.Objects.CR.Location.bAccountID, Equal<Current<CarrierPluginCustomer.customerID>>>), DisplayName = "Customer Location", DescriptionField = typeof (PX.Objects.CR.Location.descr))]
  public virtual int? CustomerLocationID
  {
    get => this._CustomerLocationID;
    set => this._CustomerLocationID = value;
  }

  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Active")]
  public virtual bool? IsActive
  {
    get => this._IsActive;
    set => this._IsActive = value;
  }

  [PXDefault]
  [PXDBString]
  [PXUIField(DisplayName = "Carrier Billing Account")]
  public virtual string CarrierAccount
  {
    get => this._CarrierAccount;
    set => this._CarrierAccount = value;
  }

  [PXDefault(typeof (Search2<PX.Objects.CR.Address.postalCode, InnerJoin<PX.Objects.CR.Location, On<PX.Objects.CR.Address.addressID, Equal<PX.Objects.CR.Location.defAddressID>, And<PX.Objects.CR.Location.bAccountID, Equal<Current<CarrierPluginCustomer.customerID>>, And<PX.Objects.CR.Location.locationID, Equal<Current<CarrierPluginCustomer.customerLocationID>>>>>>>))]
  [PXDBString]
  [PXUIField(DisplayName = "Billing Postal Code", Enabled = false)]
  public virtual string PostalCode
  {
    get => this._PostalCode;
    set => this._PostalCode = value;
  }

  [PXDBString(1)]
  [CarrierBillingTypes.List]
  [PXDefault("R")]
  [PXUIField(DisplayName = "Billing Type")]
  public virtual string CarrierBillingType { get; set; }

  /// <summary>The unique two-letter identifier of the Country.</summary>
  /// <value>
  /// The identifiers of the countries are defined by the ISO 3166 standard.
  /// </value>
  [PXDBString(2, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Billing Country", Enabled = false)]
  [PXSelector(typeof (Country.countryID), CacheGlobal = true, DescriptionField = typeof (Country.description))]
  [PXDefault]
  public virtual string CountryID { get; set; }

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

  [PXDBTimestamp]
  public virtual byte[] tstamp
  {
    get => this._tstamp;
    set => this._tstamp = value;
  }

  public abstract class carrierPluginID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CarrierPluginCustomer.carrierPluginID>
  {
  }

  public abstract class recordID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CarrierPluginCustomer.recordID>
  {
  }

  public abstract class customerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CarrierPluginCustomer.customerID>
  {
  }

  public abstract class customerLocationID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    CarrierPluginCustomer.customerLocationID>
  {
  }

  public abstract class isActive : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CarrierPluginCustomer.isActive>
  {
  }

  public abstract class carrierAccount : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CarrierPluginCustomer.carrierAccount>
  {
  }

  public abstract class postalCode : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CarrierPluginCustomer.postalCode>
  {
  }

  public abstract class carrierBillingType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CarrierPluginCustomer.carrierBillingType>
  {
  }

  public abstract class countryID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CarrierPluginCustomer.countryID>
  {
  }

  public abstract class createdByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    CarrierPluginCustomer.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CarrierPluginCustomer.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CarrierPluginCustomer.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    CarrierPluginCustomer.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CarrierPluginCustomer.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CarrierPluginCustomer.lastModifiedDateTime>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  CarrierPluginCustomer.Tstamp>
  {
  }
}
