// Decompiled with JetBrains decompiler
// Type: PX.SM.UPHistory
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.SM;

[PXCacheName("Update History")]
[Serializable]
public class UPHistory : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected int? _UpdateID;
  protected 
  #nullable disable
  string _Host;
  protected System.DateTime? _Started;
  protected System.DateTime? _Finished;

  [PXUIField(DisplayName = "Maintenance ID", Enabled = false)]
  [PXDBIdentity(IsKey = true)]
  public virtual int? UpdateID
  {
    get => this._UpdateID;
    set => this._UpdateID = value;
  }

  [PXUIField(DisplayName = "Host", Enabled = false)]
  [PXDBString(255 /*0xFF*/)]
  [PXDefault("")]
  public virtual string Host
  {
    get => this._Host;
    set => this._Host = value;
  }

  [PXUIField(DisplayName = "Started On", Enabled = false)]
  [PXDBDate(InputMask = "g", PreserveTime = true, UseTimeZone = false)]
  public virtual System.DateTime? Started
  {
    get => this._Started;
    set => this._Started = value;
  }

  [PXUIField(DisplayName = "Finished On", Enabled = false)]
  [PXDBDate(InputMask = "g", PreserveTime = true, UseTimeZone = false)]
  public virtual System.DateTime? Finished
  {
    get => this._Finished;
    set => this._Finished = value;
  }

  public abstract class updateID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  UPHistory.updateID>
  {
  }

  public abstract class host : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  UPHistory.host>
  {
  }

  public abstract class started : BqlType<
  #nullable enable
  IBqlDateTime, System.DateTime>.Field<
  #nullable disable
  UPHistory.started>
  {
  }

  public abstract class finished : BqlType<
  #nullable enable
  IBqlDateTime, System.DateTime>.Field<
  #nullable disable
  UPHistory.finished>
  {
  }
}
