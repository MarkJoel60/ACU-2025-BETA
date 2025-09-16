// Decompiled with JetBrains decompiler
// Type: PX.SM.WikiPageCurrentLanguage
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.BQL;
using System;

#nullable enable
namespace PX.SM;

[Serializable]
public class WikiPageCurrentLanguage : WikiPageLanguage
{
  public new abstract class pageID : BqlType<IBqlGuid, Guid>.Field<
  #nullable disable
  WikiPageCurrentLanguage.pageID>
  {
  }

  public new abstract class title : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  WikiPageCurrentLanguage.title>
  {
  }

  public new abstract class summary : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    WikiPageCurrentLanguage.summary>
  {
  }

  public new abstract class keywords : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    WikiPageCurrentLanguage.keywords>
  {
  }

  public new abstract class language : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    WikiPageCurrentLanguage.language>
  {
  }

  public new abstract class lastRevisionID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    WikiPageCurrentLanguage.lastRevisionID>
  {
  }

  public new abstract class lastPublishedID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    WikiPageCurrentLanguage.lastPublishedID>
  {
  }

  public new abstract class lastPublishedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    WikiPageCurrentLanguage.lastPublishedDateTime>
  {
  }
}
