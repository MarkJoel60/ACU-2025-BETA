// Decompiled with JetBrains decompiler
// Type: PX.SM.PasswordsInfo
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.SM;

[PXCacheName("Change Password")]
[Serializable]
public class PasswordsInfo : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBString(128 /*0x80*/, IsUnicode = true)]
  [PXDefault]
  [PXUIField(DisplayName = "Old Password")]
  public virtual 
  #nullable disable
  string OldPassword { get; set; }

  [PXDBString(128 /*0x80*/, IsUnicode = true)]
  [PXDefault]
  [PXUIField(DisplayName = "New Password")]
  public virtual string NewPassword { get; set; }

  [PXDBString(128 /*0x80*/, IsUnicode = true)]
  [PXDefault]
  [PXUIField(DisplayName = "Confirm Password")]
  public virtual string ConfirmPassword { get; set; }

  public abstract class oldPassword : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PasswordsInfo.oldPassword>
  {
  }

  public abstract class newPassword : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PasswordsInfo.newPassword>
  {
  }

  public abstract class confirmPassword : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PasswordsInfo.confirmPassword>
  {
  }
}
