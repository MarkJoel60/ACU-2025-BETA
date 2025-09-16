// Decompiled with JetBrains decompiler
// Type: PX.SM.LockoutFilter
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.SM;

[PXCacheName("Schedule Maintenance")]
[Serializable]
public class LockoutFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected System.DateTime? _DateTime;
  protected 
  #nullable disable
  string _Reason;
  protected bool? _LockoutAll;

  [PXUIField(DisplayName = "Lock Out At", Enabled = true)]
  [PXDBDateAndTime(PreserveTime = true, InputMask = "g", DisplayNameDate = "Lockout at", DisplayNameTime = "")]
  public virtual System.DateTime? DateTime
  {
    get => this._DateTime;
    set => this._DateTime = value;
  }

  [PXDBText(IsUnicode = true)]
  [PXUIField(DisplayName = "Reason", Enabled = true)]
  public virtual string Reason
  {
    get => this._Reason;
    set => this._Reason = value;
  }

  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Lock Out All Sites", Enabled = true)]
  public bool? LockoutAll
  {
    get => this._LockoutAll;
    set => this._LockoutAll = value;
  }

  public abstract class date : BqlType<
  #nullable enable
  IBqlDateTime, System.DateTime>.Field<
  #nullable disable
  LockoutFilter.date>
  {
  }

  public abstract class reason : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  LockoutFilter.reason>
  {
  }

  public abstract class lockoutAll : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  LockoutFilter.lockoutAll>
  {
  }
}
