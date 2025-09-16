// Decompiled with JetBrains decompiler
// Type: PX.Data.Update.UPSpeedCheck
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Data.Update;

[Serializable]
public class UPSpeedCheck : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXUIField(DisplayName = "Screen")]
  [PXString(8, InputMask = "CC.CC.CC.CC")]
  [PXDefault]
  public virtual 
  #nullable disable
  string Screen { get; set; }

  [PXUIField(DisplayName = "Action")]
  [PXString(64 /*0x40*/)]
  [PXDefault]
  public virtual string Action { get; set; }

  [PXUIField(DisplayName = "Request Time Span", Enabled = false)]
  [PXInt]
  public virtual int? Interval { get; set; }

  [PXUIField(DisplayName = "Users Count", Enabled = false)]
  [PXInt]
  public virtual int? UsersCount { get; set; }

  public abstract class screen : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  UPSpeedCheck.screen>
  {
  }

  public abstract class action : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  UPSpeedCheck.action>
  {
  }

  public abstract class interval : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  UPSpeedCheck.interval>
  {
  }

  public abstract class usersCount : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  UPSpeedCheck.usersCount>
  {
  }
}
