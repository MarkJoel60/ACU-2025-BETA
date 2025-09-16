// Decompiled with JetBrains decompiler
// Type: PX.SM.NewAnswerInfo
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.SM;

[PXCacheName("Change Answer")]
[Serializable]
public class NewAnswerInfo : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBString(255 /*0xFF*/, IsUnicode = true)]
  [PXUIField(DisplayName = "New Question")]
  public virtual 
  #nullable disable
  string PasswordQuestion { get; set; }

  [PXDBString(255 /*0xFF*/, IsUnicode = true)]
  [PXDefault]
  [PXUIField(DisplayName = "New Answer")]
  public virtual string PasswordAnswer { get; set; }

  [PXDBString(128 /*0x80*/, IsUnicode = true)]
  [PXDefault]
  [PXUIField(DisplayName = "Password")]
  public virtual string Password { get; set; }

  public abstract class passwordQuestion : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    NewAnswerInfo.passwordQuestion>
  {
  }

  public abstract class passwordAnswer : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    NewAnswerInfo.passwordAnswer>
  {
  }

  public abstract class password : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  NewAnswerInfo.password>
  {
  }
}
