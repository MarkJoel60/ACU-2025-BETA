// Decompiled with JetBrains decompiler
// Type: PX.Translation.LocalizationRecordObsolete
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
[PXVirtual]
[Serializable]
public class LocalizationRecordObsolete : LocalizationRecord
{
  /// <exclude />
  public new abstract class id : BqlType<IBqlString, string>.Field<
  #nullable disable
  LocalizationRecordObsolete.id>
  {
  }

  public new abstract class neutralValue : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    LocalizationRecordObsolete.neutralValue>
  {
  }

  /// <exclude />
  public new abstract class isNotLocalized : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    LocalizationRecordObsolete.isNotLocalized>
  {
  }
}
