// Decompiled with JetBrains decompiler
// Type: PX.SM.SimpleWikiPage
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
public class SimpleWikiPage : WikiPage
{
  [PXDBString(InputMask = "", IsUnicode = true)]
  [PXUIField(DisplayName = "Name", Visibility = PXUIVisibility.SelectorVisible)]
  public override 
  #nullable disable
  string Name
  {
    get => base.Name;
    set => base.Name = value;
  }

  [PXDefault]
  [PXString(InputMask = "", IsUnicode = true)]
  [PXUIField(DisplayName = "Name", Visibility = PXUIVisibility.SelectorVisible)]
  [PXSelector(typeof (SimpleWikiPage.title))]
  public override string Title
  {
    get => base.Title;
    set => base.Title = value;
  }

  public new abstract class pageID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  SimpleWikiPage.pageID>
  {
  }

  public new abstract class wikiID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  SimpleWikiPage.wikiID>
  {
  }

  public new abstract class statusID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SimpleWikiPage.statusID>
  {
  }

  public new abstract class name : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SimpleWikiPage.name>
  {
  }

  public new abstract class title : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SimpleWikiPage.title>
  {
  }

  public new abstract class parentUID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  SimpleWikiPage.parentUID>
  {
  }

  public new abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    SimpleWikiPage.lastModifiedByID>
  {
  }
}
