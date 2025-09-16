// Decompiled with JetBrains decompiler
// Type: PX.SM.Instance
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace PX.SM;

[PXCacheName("Application")]
[Serializable]
public class Instance : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected 
  #nullable disable
  string _InstallationID;
  protected string _DatabaseInfo;
  protected System.DateTime? _Date;

  [PXDBString(64 /*0x40*/, IsKey = true)]
  public virtual string InstallationID
  {
    get => this._InstallationID;
    set => this._InstallationID = value;
  }

  [PXDBString]
  public virtual string DatabaseInfo
  {
    get => this._DatabaseInfo;
    set => this._DatabaseInfo = value;
  }

  [PXDBDate(InputMask = "g", PreserveTime = true, UseTimeZone = false)]
  public virtual System.DateTime? Date
  {
    get => this._Date;
    set => this._Date = value;
  }

  public Instance()
  {
  }

  public Instance(string installationID, string databaseInfo, System.DateTime? date)
    : this()
  {
    this._InstallationID = installationID;
    this._DatabaseInfo = databaseInfo;
    this._Date = date;
  }

  public static IEnumerable<Instance> SelectMulti()
  {
    return PXDatabase.SelectMulti<Instance>((PXDataField) new PXDataField<Instance.installationID>(), (PXDataField) new PXDataField<Instance.databaseInfo>(), (PXDataField) new PXDataField<Instance.date>()).Select<PXDataRecord, Instance>((Func<PXDataRecord, Instance>) (dr => new Instance()
    {
      InstallationID = dr.GetString(0),
      DatabaseInfo = dr.GetString(1),
      Date = dr.GetDateTime(2)
    }));
  }

  public abstract class installationID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Instance.installationID>
  {
  }

  public abstract class databaseInfo : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Instance.databaseInfo>
  {
  }

  public abstract class date : BqlType<
  #nullable enable
  IBqlDateTime, System.DateTime>.Field<
  #nullable disable
  Instance.date>
  {
  }
}
