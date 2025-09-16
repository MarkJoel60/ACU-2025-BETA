// Decompiled with JetBrains decompiler
// Type: PX.SM.UserLocaleFormat
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.SM;

/// <exclude />
[Serializable]
public class UserLocaleFormat : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected Guid? _UserID;
  protected 
  #nullable disable
  string _LocaleName;

  [PXDBGuid(false, IsKey = true)]
  [PXDefault]
  public virtual Guid? UserID
  {
    get => this._UserID;
    set => this._UserID = value;
  }

  [PXDBString(10, IsKey = true, IsUnicode = true, InputMask = "")]
  [PXDefault]
  public virtual string LocaleName
  {
    get => this._LocaleName;
    set => this._LocaleName = value;
  }

  [PXDBInt]
  [PXDBChildIdentity(typeof (LocaleFormat.formatID))]
  public virtual int? FormatID { get; set; }

  public abstract class userID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  UserLocaleFormat.userID>
  {
  }

  /// <exclude />
  public abstract class localeName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  UserLocaleFormat.localeName>
  {
  }

  /// <exclude />
  public abstract class formatID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  UserLocaleFormat.formatID>
  {
  }
}
