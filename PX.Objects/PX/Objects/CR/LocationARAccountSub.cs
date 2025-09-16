// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.LocationARAccountSub
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
namespace PX.Objects.CR;

[PXProjection(typeof (Select<PX.Objects.CR.Standalone.Location>), Persistent = false)]
[PXCacheName("Location GL Accounts")]
[Serializable]
public class LocationARAccountSub : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected int? _BAccountID;
  protected int? _LocationID;
  protected int? _CARAccountLocationID;
  protected int? _CARAccountID;
  protected int? _CARSubID;

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

  [PXDBInt(BqlField = typeof (PX.Objects.CR.Standalone.Location.cARAccountLocationID))]
  public virtual int? CARAccountLocationID
  {
    get => this._CARAccountLocationID;
    set => this._CARAccountLocationID = value;
  }

  [Account(BqlField = typeof (PX.Objects.CR.Standalone.Location.cARAccountID), DisplayName = "AR Account", DescriptionField = typeof (Account.description), Required = true, ControlAccountForModule = "AR")]
  public virtual int? CARAccountID
  {
    get => this._CARAccountID;
    set => this._CARAccountID = value;
  }

  [SubAccount(typeof (LocationARAccountSub.cARAccountID), BqlField = typeof (PX.Objects.CR.Standalone.Location.cARSubID), DisplayName = "AR Sub.", DescriptionField = typeof (Sub.description), Required = true)]
  [PXReferentialIntegrityCheck]
  [PXForeignReference(typeof (Field<LocationARAccountSub.cARSubID>.IsRelatedTo<Sub.subID>))]
  public virtual int? CARSubID
  {
    get => this._CARSubID;
    set => this._CARSubID = value;
  }

  [Account(BqlField = typeof (PX.Objects.CR.Standalone.Location.cRetainageAcctID), DisplayName = "Retainage Receivable Account", DescriptionField = typeof (Account.description), Required = true, ControlAccountForModule = "AR")]
  public virtual int? CRetainageAcctID { get; set; }

  [SubAccount(typeof (LocationARAccountSub.cRetainageAcctID), BqlField = typeof (PX.Objects.CR.Standalone.Location.cRetainageSubID), DisplayName = "Retainage Receivable Sub.", DescriptionField = typeof (Sub.description), Required = true)]
  [PXReferentialIntegrityCheck]
  [PXForeignReference(typeof (Field<LocationARAccountSub.cRetainageSubID>.IsRelatedTo<Sub.subID>))]
  public virtual int? CRetainageSubID { get; set; }

  public abstract class bAccountID : BqlType<IBqlInt, int>.Field<
  #nullable disable
  LocationARAccountSub.bAccountID>
  {
  }

  public abstract class locationID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  LocationARAccountSub.locationID>
  {
  }

  public abstract class cARAccountLocationID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    LocationARAccountSub.cARAccountLocationID>
  {
  }

  public abstract class cARAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  LocationARAccountSub.cARAccountID>
  {
  }

  public abstract class cARSubID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  LocationARAccountSub.cARSubID>
  {
  }

  public abstract class cRetainageAcctID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    LocationARAccountSub.cRetainageAcctID>
  {
  }

  public abstract class cRetainageSubID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    LocationARAccountSub.cRetainageSubID>
  {
  }
}
