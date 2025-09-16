// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.LocationAPPaymentInfo
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CA;
using PX.Objects.CR;
using PX.Objects.GL;
using System;

#nullable enable
namespace PX.Objects.AP;

[PXProjection(typeof (Select2<PX.Objects.CR.Standalone.Location, InnerJoin<BAccountR, On<BAccountR.bAccountID, Equal<PX.Objects.CR.Standalone.Location.bAccountID>>>>), Persistent = false)]
[PXCacheName("Location Payment Settings")]
[Serializable]
public class LocationAPPaymentInfo : 
  PXBqlTable,
  IBqlTable,
  IBqlTableSystemDataStorage,
  IPaymentTypeDetailMaster
{
  protected int? _BAccountID;
  protected int? _LocationID;
  protected int? _VDefAddressID;
  protected int? _VDefContactID;
  protected int? _VPaymentInfoLocationID;
  protected 
  #nullable disable
  string _VPaymentMethodID;
  protected int? _VCashAccountID;
  protected short? _VPaymentLeadTime;
  protected bool? _VSeparateCheck;
  protected int? _VPaymentByType;
  protected bool? _IsRemitAddressSameAsMain;
  protected bool? _IsRemitContactSameAsMain;
  protected int? _VRemitAddressID;
  protected int? _VRemitContactID;
  protected int? _VAPAccountLocationID;
  protected int? _VAPAccountID;
  protected int? _VAPSubID;

  [PXDBInt(BqlField = typeof (PX.Objects.CR.Standalone.Location.bAccountID), IsKey = true)]
  public virtual int? BAccountID
  {
    get => this._BAccountID;
    set => this._BAccountID = value;
  }

  [PXDBInt(BqlField = typeof (PX.Objects.CR.Standalone.Location.locationID), IsKey = true)]
  public virtual int? LocationID
  {
    get => this._LocationID;
    set => this._LocationID = value;
  }

  [PXDBInt(BqlField = typeof (BAccountR.defAddressID))]
  [PXDefault(typeof (Select<PX.Objects.CR.BAccount, Where<PX.Objects.CR.BAccount.bAccountID, Equal<Current<LocationAPPaymentInfo.bAccountID>>>>), SourceField = typeof (PX.Objects.CR.BAccount.defAddressID))]
  public virtual int? VDefAddressID
  {
    get => this._VDefAddressID;
    set => this._VDefAddressID = value;
  }

  [PXDBInt(BqlField = typeof (BAccountR.defContactID))]
  [PXDefault(typeof (Select<PX.Objects.CR.BAccount, Where<PX.Objects.CR.BAccount.bAccountID, Equal<Current<LocationAPPaymentInfo.bAccountID>>>>), SourceField = typeof (PX.Objects.CR.BAccount.defContactID))]
  public virtual int? VDefContactID
  {
    get => this._VDefContactID;
    set => this._VDefContactID = value;
  }

  [PXDBInt(BqlField = typeof (PX.Objects.CR.Standalone.Location.vPaymentInfoLocationID))]
  public virtual int? VPaymentInfoLocationID
  {
    get => this._VPaymentInfoLocationID;
    set => this._VPaymentInfoLocationID = value;
  }

  /// <inheritdoc cref="P:PX.Objects.CR.Standalone.Location.VPaymentMethodID" />
  [PXDBString(10, IsUnicode = true, BqlField = typeof (PX.Objects.CR.Standalone.Location.vPaymentMethodID))]
  [PXUIField(DisplayName = "Payment Method")]
  [PXSelector(typeof (Search<PX.Objects.CA.PaymentMethod.paymentMethodID, Where<PX.Objects.CA.PaymentMethod.useForAP, Equal<True>, And<PX.Objects.CA.PaymentMethod.isActive, Equal<True>>>>), DescriptionField = typeof (PX.Objects.CA.PaymentMethod.descr))]
  [PXDefault(PersistingCheck = PXPersistingCheck.Nothing)]
  [PXRestrictor(typeof (Where<PX.Objects.CA.PaymentMethod.paymentType, NotEqual<PaymentMethodType.externalPaymentProcessor>, PX.Data.Or<Where<PX.Objects.CA.PaymentMethod.paymentType, Equal<PaymentMethodType.externalPaymentProcessor>, PX.Data.And<FeatureInstalled<PX.Objects.CS.FeaturesSet.paymentProcessor>>>>>), "Payment Method '{0}' is not configured to print checks.", new System.Type[] {typeof (PX.Objects.CA.PaymentMethod.paymentMethodID)})]
  public virtual string VPaymentMethodID
  {
    get => this._VPaymentMethodID;
    set => this._VPaymentMethodID = value;
  }

  [CashAccount(typeof (Search2<PX.Objects.CA.CashAccount.cashAccountID, InnerJoin<PaymentMethodAccount, On<PaymentMethodAccount.cashAccountID, Equal<PX.Objects.CA.CashAccount.cashAccountID>>>, Where2<Match<Current<AccessInfo.userName>>, And<PX.Objects.CA.CashAccount.clearingAccount, Equal<False>, And<PaymentMethodAccount.paymentMethodID, Equal<Current<LocationAPPaymentInfo.vPaymentMethodID>>, And<PaymentMethodAccount.useForAP, Equal<True>>>>>>), BqlField = typeof (PX.Objects.CR.Standalone.Location.vCashAccountID), Visibility = PXUIVisibility.Visible)]
  [PXDefault(PersistingCheck = PXPersistingCheck.Nothing)]
  public virtual int? VCashAccountID
  {
    get => this._VCashAccountID;
    set => this._VCashAccountID = value;
  }

  [PXDBShort(BqlField = typeof (PX.Objects.CR.Standalone.Location.vPaymentLeadTime), MinValue = -3660, MaxValue = 3660)]
  [PXDefault(0)]
  [PXUIField(DisplayName = "Payment Lead Time (Days)")]
  public short? VPaymentLeadTime
  {
    get => this._VPaymentLeadTime;
    set => this._VPaymentLeadTime = value;
  }

  [PXDBBool(BqlField = typeof (PX.Objects.CR.Standalone.Location.vSeparateCheck))]
  [PXUIField(DisplayName = "Pay Separately")]
  [PXDefault(false)]
  public virtual bool? VSeparateCheck
  {
    get => this._VSeparateCheck;
    set => this._VSeparateCheck = value;
  }

  [PXDBInt(BqlField = typeof (PX.Objects.CR.Standalone.Location.vPaymentByType))]
  [PXDefault(0)]
  [APPaymentBy.List]
  [PXUIField(DisplayName = "Payment By")]
  public int? VPaymentByType
  {
    get => this._VPaymentByType;
    set => this._VPaymentByType = value;
  }

  [PXBool]
  [PXUIField(DisplayName = "Override")]
  [Obsolete("Use Location.OverrideRemitAddress instead")]
  public virtual bool? OverrideRemitAddress { get; set; }

  [Obsolete("Use Location.OverrideRemitAddress instead")]
  [PXBool]
  [PXUIField(DisplayName = "Same as Main")]
  public virtual bool? IsRemitAddressSameAsMain
  {
    get
    {
      if (!this.OverrideRemitAddress.HasValue)
        return new bool?();
      bool? overrideRemitAddress = this.OverrideRemitAddress;
      return !overrideRemitAddress.HasValue ? new bool?() : new bool?(!overrideRemitAddress.GetValueOrDefault());
    }
  }

  [PXBool]
  [PXUIField(DisplayName = "Override")]
  public virtual bool? OverrideRemitContact { get; set; }

  [Obsolete("Use OverrideRemitContact instead")]
  [PXBool]
  [PXUIField(DisplayName = "Same as Main")]
  public virtual bool? IsRemitContactSameAsMain
  {
    get
    {
      if (!this.OverrideRemitContact.HasValue)
        return new bool?();
      bool? overrideRemitContact = this.OverrideRemitContact;
      return !overrideRemitContact.HasValue ? new bool?() : new bool?(!overrideRemitContact.GetValueOrDefault());
    }
  }

  [PXDBInt(BqlField = typeof (PX.Objects.CR.Standalone.Location.vRemitAddressID))]
  [Obsolete("Use Location.VRemitAddressID instead")]
  public virtual int? VRemitAddressID
  {
    get => this._VRemitAddressID;
    set => this._VRemitAddressID = value;
  }

  [PXDBInt(BqlField = typeof (PX.Objects.CR.Standalone.Location.vRemitContactID))]
  [Obsolete("Use Location.VRemitContactID instead")]
  public virtual int? VRemitContactID
  {
    get => this._VRemitContactID;
    set => this._VRemitContactID = value;
  }

  [PXDBInt(BqlField = typeof (PX.Objects.CR.Standalone.Location.vAPAccountLocationID))]
  public virtual int? VAPAccountLocationID
  {
    get => this._VAPAccountLocationID;
    set => this._VAPAccountLocationID = value;
  }

  [Account(BqlField = typeof (PX.Objects.CR.Standalone.Location.vAPAccountID), DisplayName = "AP Account", DescriptionField = typeof (PX.Objects.GL.Account.description))]
  public virtual int? VAPAccountID
  {
    get => this._VAPAccountID;
    set => this._VAPAccountID = value;
  }

  [SubAccount(typeof (LocationAPAccountSub.vAPAccountID), BqlField = typeof (PX.Objects.CR.Standalone.Location.vAPSubID), DisplayName = "AP Sub.", DescriptionField = typeof (PX.Objects.GL.Sub.description))]
  public virtual int? VAPSubID
  {
    get => this._VAPSubID;
    set => this._VAPSubID = value;
  }

  public abstract class bAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  LocationAPPaymentInfo.bAccountID>
  {
  }

  public abstract class locationID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  LocationAPPaymentInfo.locationID>
  {
  }

  public abstract class vDefAddressID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    LocationAPPaymentInfo.vDefAddressID>
  {
  }

  public abstract class vDefContactID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    LocationAPPaymentInfo.vDefContactID>
  {
  }

  public abstract class vPaymentInfoLocationID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    LocationAPPaymentInfo.vPaymentInfoLocationID>
  {
  }

  public abstract class vPaymentMethodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    LocationAPPaymentInfo.vPaymentMethodID>
  {
  }

  public abstract class vCashAccountID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    LocationAPPaymentInfo.vCashAccountID>
  {
  }

  public abstract class vPaymentLeadTime : 
    BqlType<
    #nullable enable
    IBqlShort, short>.Field<
    #nullable disable
    LocationAPPaymentInfo.vPaymentLeadTime>
  {
  }

  public abstract class vSeparateCheck : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    LocationAPPaymentInfo.vSeparateCheck>
  {
  }

  public abstract class vPaymentByType : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    LocationAPPaymentInfo.vPaymentByType>
  {
  }

  [Obsolete("Use Location.overrideRemitAddress instead")]
  public abstract class overrideRemitAddress : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    LocationAPPaymentInfo.overrideRemitAddress>
  {
  }

  [Obsolete("Use Location.overrideRemitAddress instead")]
  public abstract class isRemitAddressSameAsMain : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    LocationAPPaymentInfo.isRemitAddressSameAsMain>
  {
  }

  [Obsolete("Use Location.OverrideRemitContact instead")]
  public abstract class overrideRemitContact : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    LocationAPPaymentInfo.overrideRemitContact>
  {
  }

  [Obsolete("Use Location.OverrideRemitContact instead")]
  public abstract class isRemitContactSameAsMain : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    LocationAPPaymentInfo.isRemitContactSameAsMain>
  {
  }

  [Obsolete("Use Location.vRemitAddressID instead")]
  public abstract class vRemitAddressID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    LocationAPPaymentInfo.vRemitAddressID>
  {
  }

  [Obsolete("Use Location.vRemitContactID instead")]
  public abstract class vRemitContactID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    LocationAPPaymentInfo.vRemitContactID>
  {
  }

  public abstract class vAPAccountLocationID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    LocationAPPaymentInfo.vAPAccountLocationID>
  {
  }

  public abstract class vAPAccountID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    LocationAPPaymentInfo.vAPAccountID>
  {
  }

  public abstract class vAPSubID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  LocationAPPaymentInfo.vAPSubID>
  {
  }
}
