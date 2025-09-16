// Decompiled with JetBrains decompiler
// Type: PX.SM.EulaStatus
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.SM;

[Serializable]
public class EulaStatus : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected Guid? _PKID;
  protected 
  #nullable disable
  string _IPAddress;
  protected System.DateTime? _Date;

  [PXDBGuidMaintainDeleted]
  [PXDefault]
  [PXUIField(Visibility = PXUIVisibility.Invisible)]
  public virtual Guid? PKID
  {
    get => this._PKID;
    set => this._PKID = value;
  }

  [PXDBString(50)]
  [PXUIField(DisplayName = "IP address")]
  public virtual string IPAddress
  {
    get => this._IPAddress;
    set => this._IPAddress = value;
  }

  [PXDBDate(InputMask = "g", PreserveTime = true, UseTimeZone = true)]
  [PXUIField(DisplayName = "Date")]
  public virtual System.DateTime? Date
  {
    get => this._Date;
    set => this._Date = value;
  }

  public abstract class pKID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  EulaStatus.pKID>
  {
  }

  public abstract class iPAddress : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  EulaStatus.iPAddress>
  {
  }

  public abstract class date : BqlType<
  #nullable enable
  IBqlDateTime, System.DateTime>.Field<
  #nullable disable
  EulaStatus.date>
  {
  }
}
