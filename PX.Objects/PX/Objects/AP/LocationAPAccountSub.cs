// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.LocationAPAccountSub
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.GL;
using System;

#nullable enable
namespace PX.Objects.AP;

[PXProjection(typeof (Select<PX.Objects.CR.Standalone.Location>), Persistent = false)]
[PXCacheName("Location GL Accounts")]
[Serializable]
public class LocationAPAccountSub : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected int? _BAccountID;
  protected int? _LocationID;
  protected int? _VPaymentInfoLocationID;
  protected 
  #nullable disable
  string _PaymentMethodID;
  protected int? _VCashAccountID;
  protected short? _VPaymentLeadTime;
  protected bool? _VSeparateCheck;
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

  [PXDBInt(BqlField = typeof (PX.Objects.CR.Standalone.Location.vPaymentInfoLocationID))]
  public virtual int? VPaymentInfoLocationID
  {
    get => this._VPaymentInfoLocationID;
    set => this._VPaymentInfoLocationID = value;
  }

  [PXDBString(10, IsUnicode = true, BqlField = typeof (PX.Objects.CR.Standalone.Location.vPaymentMethodID))]
  [PXUIField(DisplayName = "Payment Method")]
  public virtual string VPaymentMethodID
  {
    get => this._PaymentMethodID;
    set => this._PaymentMethodID = value;
  }

  [CashAccount(BqlField = typeof (PX.Objects.CR.Standalone.Location.vCashAccountID), Visibility = PXUIVisibility.Visible)]
  public virtual int? VCashAccountID
  {
    get => this._VCashAccountID;
    set => this._VCashAccountID = value;
  }

  [PXDBShort(BqlField = typeof (PX.Objects.CR.Standalone.Location.vPaymentLeadTime), MinValue = 0, MaxValue = 3660)]
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

  [PXDBInt(BqlField = typeof (PX.Objects.CR.Standalone.Location.vRemitAddressID))]
  public virtual int? VRemitAddressID
  {
    get => this._VRemitAddressID;
    set => this._VRemitAddressID = value;
  }

  [PXDBInt(BqlField = typeof (PX.Objects.CR.Standalone.Location.vRemitContactID))]
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

  [Account(BqlField = typeof (PX.Objects.CR.Standalone.Location.vAPAccountID), DisplayName = "AP Account", DescriptionField = typeof (PX.Objects.GL.Account.description), Required = true, ControlAccountForModule = "AP")]
  public virtual int? VAPAccountID
  {
    get => this._VAPAccountID;
    set => this._VAPAccountID = value;
  }

  [SubAccount(typeof (LocationAPAccountSub.vAPAccountID), BqlField = typeof (PX.Objects.CR.Standalone.Location.vAPSubID), DisplayName = "AP Sub.", DescriptionField = typeof (Sub.description), Required = true)]
  [PXReferentialIntegrityCheck(CheckPoint = CheckPoint.BeforePersisting)]
  [PXForeignReference(typeof (PX.Data.ReferentialIntegrity.Attributes.Field<LocationAPAccountSub.vAPSubID>.IsRelatedTo<Sub.subID>))]
  public virtual int? VAPSubID
  {
    get => this._VAPSubID;
    set => this._VAPSubID = value;
  }

  [Account(BqlField = typeof (PX.Objects.CR.Standalone.Location.vRetainageAcctID), DisplayName = "Retainage Payable Account", DescriptionField = typeof (PX.Objects.GL.Account.description), Required = true, ControlAccountForModule = "AP")]
  public virtual int? VRetainageAcctID { get; set; }

  [SubAccount(typeof (LocationAPAccountSub.vRetainageAcctID), BqlField = typeof (PX.Objects.CR.Standalone.Location.vRetainageSubID), DisplayName = "Retainage Payable Sub.", DescriptionField = typeof (Sub.description), Required = true)]
  [PXReferentialIntegrityCheck(CheckPoint = CheckPoint.BeforePersisting)]
  [PXForeignReference(typeof (PX.Data.ReferentialIntegrity.Attributes.Field<LocationAPAccountSub.vRetainageSubID>.IsRelatedTo<Sub.subID>))]
  public virtual int? VRetainageSubID { get; set; }

  public abstract class bAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  LocationAPAccountSub.bAccountID>
  {
  }

  public abstract class locationID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  LocationAPAccountSub.locationID>
  {
  }

  public abstract class vPaymentInfoLocationID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    LocationAPAccountSub.vPaymentInfoLocationID>
  {
  }

  public abstract class vPaymentMethodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    LocationAPAccountSub.vPaymentMethodID>
  {
  }

  public abstract class vCashAccountID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    LocationAPAccountSub.vCashAccountID>
  {
  }

  public abstract class vPaymentLeadTime : 
    BqlType<
    #nullable enable
    IBqlShort, short>.Field<
    #nullable disable
    LocationAPAccountSub.vPaymentLeadTime>
  {
  }

  public abstract class vSeparateCheck : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    LocationAPAccountSub.vSeparateCheck>
  {
  }

  public abstract class vRemitAddressID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    LocationAPAccountSub.vRemitAddressID>
  {
  }

  public abstract class vRemitContactID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    LocationAPAccountSub.vRemitContactID>
  {
  }

  public abstract class vAPAccountLocationID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    LocationAPAccountSub.vAPAccountLocationID>
  {
  }

  public abstract class vAPAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  LocationAPAccountSub.vAPAccountID>
  {
  }

  public abstract class vAPSubID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  LocationAPAccountSub.vAPSubID>
  {
  }

  public abstract class vRetainageAcctID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    LocationAPAccountSub.vRetainageAcctID>
  {
  }

  public abstract class vRetainageSubID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    LocationAPAccountSub.vRetainageSubID>
  {
  }
}
