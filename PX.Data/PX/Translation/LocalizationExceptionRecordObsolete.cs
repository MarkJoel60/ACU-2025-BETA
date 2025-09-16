// Decompiled with JetBrains decompiler
// Type: PX.Translation.LocalizationExceptionRecordObsolete
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Translation;

/// <exclude />
[Serializable]
public class LocalizationExceptionRecordObsolete : LocalizationExceptionRecord
{
  /// <exclude />
  public new abstract class id : 
    BqlType<IBqlString, string>.Field<
    #nullable disable
    LocalizationExceptionRecordObsolete.id>
  {
  }

  /// <exclude />
  public new abstract class neutralValue : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    LocalizationExceptionRecordObsolete.neutralValue>
  {
  }

  /// <exclude />
  public new abstract class isNotLocalized : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    LocalizationExceptionRecordObsolete.isNotLocalized>
  {
  }

  /// <exclude />
  public new abstract class idRes : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    LocalizationExceptionRecordObsolete.idRes>
  {
  }

  /// <exclude />
  public new abstract class resKey : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    LocalizationExceptionRecordObsolete.resKey>
  {
  }
}
