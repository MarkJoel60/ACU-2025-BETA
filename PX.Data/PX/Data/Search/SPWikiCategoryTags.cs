// Decompiled with JetBrains decompiler
// Type: PX.Data.Search.SPWikiCategoryTags
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Data.Search;

[Serializable]
public class SPWikiCategoryTags : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected 
  #nullable disable
  string _CategoryID;
  protected Guid? _PageID;
  protected string _PageName;
  protected string _PageTitle;
  protected byte[] _tstamp;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected System.DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected System.DateTime? _LastModifiedDateTime;

  [PXUIField(DisplayName = "Category ID", Visibility = PXUIVisibility.SelectorVisible)]
  [PXDBString(16 /*0x10*/, IsKey = true)]
  [PXDefault]
  [PXSelector(typeof (PX.Data.Search<SPWikiCategory.categoryID>))]
  public string CategoryID
  {
    get => this._CategoryID;
    set => this._CategoryID = value;
  }

  [PXDBGuid(false, IsKey = true)]
  [PXUIField(Visibility = PXUIVisibility.SelectorVisible)]
  public virtual Guid? PageID
  {
    get => this._PageID;
    set => this._PageID = value;
  }

  [PXDBString]
  [PXUIField(Visibility = PXUIVisibility.SelectorVisible, DisplayName = "Article ID")]
  public virtual string PageName
  {
    get => this._PageName;
    set => this._PageName = value;
  }

  [PXDBString]
  [PXUIField(Visibility = PXUIVisibility.SelectorVisible, Enabled = false, DisplayName = "Name")]
  public virtual string PageTitle
  {
    get => this._PageTitle;
    set => this._PageTitle = value;
  }

  [PXDBTimestamp]
  public virtual byte[] tstamp
  {
    get => this._tstamp;
    set => this._tstamp = value;
  }

  [PXDBCreatedByID]
  public virtual Guid? CreatedByID
  {
    get => this._CreatedByID;
    set => this._CreatedByID = value;
  }

  [PXDBCreatedByScreenID]
  public virtual string CreatedByScreenID
  {
    get => this._CreatedByScreenID;
    set => this._CreatedByScreenID = value;
  }

  [PXDBCreatedDateTime]
  public virtual System.DateTime? CreatedDateTime
  {
    get => this._CreatedDateTime;
    set => this._CreatedDateTime = value;
  }

  [PXDBLastModifiedByID]
  public virtual Guid? LastModifiedByID
  {
    get => this._LastModifiedByID;
    set => this._LastModifiedByID = value;
  }

  [PXDBLastModifiedByScreenID]
  public virtual string LastModifiedByScreenID
  {
    get => this._LastModifiedByScreenID;
    set => this._LastModifiedByScreenID = value;
  }

  [PXDBLastModifiedDateTime]
  public virtual System.DateTime? LastModifiedDateTime
  {
    get => this._LastModifiedDateTime;
    set => this._LastModifiedDateTime = value;
  }

  public abstract class categoryID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SPWikiCategoryTags.categoryID>
  {
  }

  public abstract class pageID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  SPWikiCategoryTags.pageID>
  {
  }

  public abstract class pageName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SPWikiCategoryTags.pageName>
  {
  }

  public abstract class pageTitle : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SPWikiCategoryTags.pageTitle>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  SPWikiCategoryTags.Tstamp>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  SPWikiCategoryTags.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SPWikiCategoryTags.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    SPWikiCategoryTags.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    SPWikiCategoryTags.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SPWikiCategoryTags.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    SPWikiCategoryTags.lastModifiedDateTime>
  {
  }
}
