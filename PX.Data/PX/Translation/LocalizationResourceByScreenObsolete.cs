// Decompiled with JetBrains decompiler
// Type: PX.Translation.LocalizationResourceByScreenObsolete
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Translation;

[Serializable]
public class LocalizationResourceByScreenObsolete : LocalizationResourceByScreen
{
  public new abstract class screenID : 
    BqlType<IBqlString, string>.Field<
    #nullable disable
    LocalizationResourceByScreenObsolete.screenID>
  {
  }

  public new abstract class idValue : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    LocalizationResourceByScreenObsolete.idValue>
  {
  }

  public new abstract class idRes : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    LocalizationResourceByScreenObsolete.idRes>
  {
  }
}
