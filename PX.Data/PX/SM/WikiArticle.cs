// Decompiled with JetBrains decompiler
// Type: PX.SM.WikiArticle
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.SM;

[PXTable(new System.Type[] {typeof (WikiPage.pageID)})]
[Serializable]
public class WikiArticle : WikiPage
{
  [PXDBGuid(false)]
  [PXUIField(DisplayName = "Parent Folder")]
  [PXSelector(typeof (Search<WikiPageSimple.pageID, Where<WikiPageSimple.wikiID, Equal<Current<WikiPage.wikiID>>, Or<WikiPageSimple.pageID, Equal<Current<WikiPage.wikiID>>>>>), SubstituteKey = typeof (WikiPageSimple.name))]
  public override Guid? ParentUID
  {
    get => this._ParentUID;
    set => this._ParentUID = value;
  }

  [PXDBInt]
  [PXDefault(10)]
  public override int? ArticleType
  {
    get => this._ArticleType;
    set => this._ArticleType = value;
  }

  [PXNote]
  public override Guid? NoteID
  {
    get => this._NoteID;
    set => this._NoteID = value;
  }

  public new abstract class wikiID : BqlType<IBqlGuid, Guid>.Field<
  #nullable disable
  WikiArticle.wikiID>
  {
  }

  public new abstract class pageID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  WikiArticle.pageID>
  {
  }

  public new abstract class parentUID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  WikiArticle.parentUID>
  {
  }

  public new abstract class articleType : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  WikiArticle.articleType>
  {
  }

  public new abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  WikiArticle.noteID>
  {
  }
}
