// Decompiled with JetBrains decompiler
// Type: PX.SM.UPLock
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;
using PX.DbServices.Model;
using System;

#nullable enable
namespace PX.SM;

public class UPLock : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected int? _DatabaseID;
  protected 
  #nullable disable
  string _Host;
  protected System.DateTime? _Date;
  protected string _Purpose;

  [PXDBInt(IsKey = true)]
  public virtual int? DatabaseID
  {
    get => this._DatabaseID;
    set => this._DatabaseID = value;
  }

  [PXDBString]
  public virtual string Host
  {
    get => this._Host;
    set => this._Host = value;
  }

  [PXUIField(DisplayName = "Lockout at", Enabled = true)]
  [PXDBDateAndTime(PreserveTime = true, InputMask = "g")]
  public virtual System.DateTime? Date
  {
    get => this._Date;
    set => this._Date = value;
  }

  [PXDBString]
  [PXUIField(DisplayName = "Reason", Enabled = true)]
  public virtual string Purpose
  {
    get => this._Purpose;
    set => this._Purpose = value;
  }

  public virtual PXLockoutReason? Reason
  {
    get
    {
      int? databaseId = this.DatabaseID;
      return !databaseId.HasValue ? new PXLockoutReason?() : new PXLockoutReason?((PXLockoutReason) databaseId.GetValueOrDefault());
    }
    set
    {
      PXLockoutReason? nullable = value;
      this.DatabaseID = nullable.HasValue ? new int?((int) nullable.GetValueOrDefault()) : new int?();
    }
  }

  public UPLock()
  {
  }

  public UPLock(PXLockoutReason reason, string host, System.DateTime? dateTime, string purpose)
    : this()
  {
    this.Reason = new PXLockoutReason?(reason);
    this._Host = host;
    this._Date = dateTime;
    this._Purpose = purpose;
  }

  public static UPLock SelectSingle(PXLockoutReason reason)
  {
    PXDataRecord pxDataRecord = PXDatabase.SelectSingle<UPLock>((PXDataField) new PXDataField<UPLock.host>(), (PXDataField) new PXDataField<UPLock.date>(), (PXDataField) new PXDataField<UPLock.purpose>(), (PXDataField) new PXDataFieldValue<UPLock.databaseID>((object) (int) reason));
    if (pxDataRecord == null)
      return (UPLock) null;
    try
    {
      return new UPLock()
      {
        Host = pxDataRecord.GetString(0),
        Date = pxDataRecord.GetDateTime(1),
        Purpose = pxDataRecord.GetString(2),
        DatabaseID = new int?((int) reason)
      };
    }
    finally
    {
      ((IDisposable) pxDataRecord).Dispose();
    }
  }

  public static void Delete(PXLockoutReason reason)
  {
    PXDatabase.Delete<UPLock>((PXDataFieldRestrict) new PXDataFieldRestrict<UPLock.databaseID>((object) (int) reason));
  }

  public abstract class databaseID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  UPLock.databaseID>
  {
  }

  public abstract class host : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  UPLock.host>
  {
  }

  public abstract class date : BqlType<
  #nullable enable
  IBqlDateTime, System.DateTime>.Field<
  #nullable disable
  UPLock.date>
  {
  }

  public abstract class purpose : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  UPLock.purpose>
  {
  }
}
