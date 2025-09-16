// Decompiled with JetBrains decompiler
// Type: PX.Translation.LocalizationResourceForObsolete
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Translation;

[Serializable]
public class LocalizationResourceForObsolete : LocalizationResource
{
  public new abstract class idValue : 
    BqlType<IBqlString, string>.Field<
    #nullable disable
    LocalizationResourceForObsolete.idValue>
  {
  }

  public new abstract class id : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    LocalizationResourceForObsolete.id>
  {
  }

  public new abstract class resType : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    LocalizationResourceForObsolete.resType>
  {
  }
}
