// Decompiled with JetBrains decompiler
// Type: PX.Data.RichTextEdit.WikiPage2
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.BQL;
using PX.SM;
using System;

#nullable enable
namespace PX.Data.RichTextEdit;

[PXProjection(typeof (Select2<WikiPage, InnerJoin<WikiPageLanguage, On<WikiPage.pageID, Equal<WikiPageLanguage.pageID>>>, Where<WikiPageLanguage.language, Equal<LocaleParam>>>), Persistent = false)]
[Serializable]
public class WikiPage2 : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected Guid? _PageID;
  protected 
  #nullable disable
  string _Title;
  protected Guid? _ParentUID;
  protected System.DateTime? _CreatedDateTime;

  [PXDBGuid(false, IsKey = true, BqlField = typeof (WikiPage.pageID))]
  [PXUIField(Visibility = PXUIVisibility.SelectorVisible)]
  public virtual Guid? PageID
  {
    get => this._PageID;
    set => this._PageID = value;
  }

  [PXDefault]
  [PXDBString(InputMask = "", IsUnicode = true, BqlField = typeof (WikiPageLanguage.title))]
  [PXUIField(DisplayName = "Name", Visibility = PXUIVisibility.SelectorVisible)]
  public virtual string Title
  {
    get => this._Title;
    set => this._Title = value;
  }

  [PXDBGuid(false, BqlField = typeof (WikiPage.parentUID))]
  [PXUIField(DisplayName = "Parent Folder")]
  public virtual Guid? ParentUID
  {
    get => this._ParentUID;
    set => this._ParentUID = value;
  }

  [PXDBCreatedDateTime(BqlField = typeof (WikiPage.createdDateTime))]
  [PXUIField(DisplayName = "Created at", Visible = true, Enabled = false)]
  public virtual System.DateTime? CreatedDateTime
  {
    get => this._CreatedDateTime;
    set => this._CreatedDateTime = value;
  }

  public abstract class pageID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  WikiPage2.pageID>
  {
  }

  public abstract class title : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  WikiPage2.title>
  {
  }

  public abstract class parentUID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  WikiPage2.parentUID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    WikiPage2.createdDateTime>
  {
  }
}
