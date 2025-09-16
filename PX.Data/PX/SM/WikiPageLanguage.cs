// Decompiled with JetBrains decompiler
// Type: PX.SM.WikiPageLanguage
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
public class WikiPageLanguage : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected Guid? _PageID;
  protected 
  #nullable disable
  string _Title;
  protected string _Summary;
  protected string _Keywords;
  protected string _Language;
  protected int? _LastRevisionID;
  protected int? _LastPublishedID;
  protected System.DateTime? _LastPublishedDateTime;

  [PXDBGuid(false, IsKey = true)]
  public virtual Guid? PageID
  {
    get => this._PageID;
    set => this._PageID = value;
  }

  [PXDefault]
  [PXDBString(InputMask = "", IsUnicode = true)]
  [PXUIField(DisplayName = "Name", Visibility = PXUIVisibility.SelectorVisible)]
  public virtual string Title
  {
    get => this._Title;
    set => this._Title = value;
  }

  [PXDBString(InputMask = "", IsUnicode = true)]
  [PXUIField(DisplayName = "Summary")]
  public virtual string Summary
  {
    get => this._Summary;
    set => this._Summary = value;
  }

  [PXDBString(InputMask = "", IsUnicode = true)]
  [PXUIField(DisplayName = "Keywords")]
  public virtual string Keywords
  {
    get => this._Keywords;
    set => this._Keywords = value;
  }

  [PXDBString(IsKey = true, InputMask = "")]
  [PXUIField(DisplayName = "Language", Visibility = PXUIVisibility.SelectorVisible)]
  public virtual string Language
  {
    get => this._Language;
    set => this._Language = value;
  }

  [PXDBInt]
  [PXDefault(0)]
  [PXUIField(DisplayName = "Version ID")]
  public virtual int? LastRevisionID
  {
    get => this._LastRevisionID;
    set => this._LastRevisionID = value;
  }

  [PXDBInt]
  [PXDefault(0)]
  [PXUIField(DisplayName = "Published ID")]
  public virtual int? LastPublishedID
  {
    get => this._LastPublishedID;
    set => this._LastPublishedID = value;
  }

  [PXDBDateAndTime(PreserveTime = true)]
  [PXUIField(DisplayName = "Creation Time", Visible = true, Enabled = false)]
  public virtual System.DateTime? LastPublishedDateTime
  {
    get => this._LastPublishedDateTime;
    set => this._LastPublishedDateTime = value;
  }

  public abstract class pageID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  WikiPageLanguage.pageID>
  {
  }

  public abstract class title : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  WikiPageLanguage.title>
  {
  }

  public abstract class summary : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  WikiPageLanguage.summary>
  {
  }

  public abstract class keywords : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  WikiPageLanguage.keywords>
  {
  }

  public abstract class language : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  WikiPageLanguage.language>
  {
  }

  public abstract class lastRevisionID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  WikiPageLanguage.lastRevisionID>
  {
  }

  public abstract class lastPublishedID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    WikiPageLanguage.lastPublishedID>
  {
  }

  public abstract class lastPublishedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    WikiPageLanguage.lastPublishedDateTime>
  {
  }
}
