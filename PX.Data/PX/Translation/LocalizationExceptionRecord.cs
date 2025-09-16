// Decompiled with JetBrains decompiler
// Type: PX.Translation.LocalizationExceptionRecord
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Translation;

/// <exclude />
[Serializable]
public class LocalizationExceptionRecord : LocalizationRecordObsolete
{
  [PXUIField(Visible = false)]
  [PXString(32 /*0x20*/, IsKey = true)]
  public virtual 
  #nullable disable
  string IdRes { get; set; }

  [PXString]
  [PXUIField(DisplayName = "Key")]
  public virtual string ResKey { get; set; }

  /// <exclude />
  public new abstract class id : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  LocalizationExceptionRecord.id>
  {
  }

  /// <exclude />
  public new abstract class neutralValue : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    LocalizationExceptionRecord.neutralValue>
  {
  }

  /// <exclude />
  public new abstract class isNotLocalized : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    LocalizationExceptionRecord.isNotLocalized>
  {
  }

  /// <exclude />
  public abstract class idRes : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  LocalizationExceptionRecord.idRes>
  {
  }

  /// <exclude />
  public abstract class resKey : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    LocalizationExceptionRecord.resKey>
  {
  }
}
