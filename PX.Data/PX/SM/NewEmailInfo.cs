// Decompiled with JetBrains decompiler
// Type: PX.SM.NewEmailInfo
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.SM;

[PXCacheName("Change Email")]
[Serializable]
public class NewEmailInfo : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBEmail]
  [PXDefault]
  [PXUIField(DisplayName = "New Email")]
  public virtual 
  #nullable disable
  string Email { get; set; }

  [PXDBString(128 /*0x80*/, IsUnicode = true)]
  [PXDefault]
  [PXUIField(DisplayName = "Password")]
  public virtual string Password { get; set; }

  public abstract class email : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  NewEmailInfo.email>
  {
  }

  public abstract class password : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  NewEmailInfo.password>
  {
  }
}
