// Decompiled with JetBrains decompiler
// Type: PX.SM.WikiPageSimple
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.SM;

[Serializable]
public class WikiPageSimple : WikiPage
{
  [PXString(InputMask = "")]
  public override 
  #nullable disable
  string Content
  {
    get => this._Content;
    set => this._Content = value;
  }

  public new abstract class wikiID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  WikiPageSimple.wikiID>
  {
  }

  public new abstract class pageID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  WikiPageSimple.pageID>
  {
  }

  public new abstract class name : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  WikiPageSimple.name>
  {
  }

  public new abstract class folder : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  WikiPageSimple.folder>
  {
  }

  public new abstract class title : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  WikiPageSimple.title>
  {
  }

  public new abstract class parentUID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  WikiPageSimple.parentUID>
  {
  }

  public new abstract class statusID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  WikiPageSimple.statusID>
  {
  }
}
