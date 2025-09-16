// Decompiled with JetBrains decompiler
// Type: PX.SM.SiteMapCurrentUserEntry
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.SM;

/// <exclude />
[Serializable]
public class SiteMapCurrentUserEntry : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected 
  #nullable disable
  string _Title;
  protected string _Url;
  protected string _ScreenID;
  protected int? _OrderIndex;
  private string _graphType;

  [PXString(255 /*0xFF*/, IsUnicode = true)]
  [PXUIField(DisplayName = "Title")]
  public virtual string Title
  {
    get => this._Title;
    set => this._Title = value;
  }

  [PXString(512 /*0x0200*/)]
  [PXUIField(DisplayName = "URL")]
  public virtual string Url
  {
    get => this._Url;
    set => this._Url = value;
  }

  [PXString(8)]
  public virtual string ScreenID
  {
    get => this._ScreenID;
    set => this._ScreenID = value;
  }

  [PXInt]
  public virtual int? OrderIndex
  {
    get => this._OrderIndex;
    set => this._OrderIndex = value;
  }

  [PXString(255 /*0xFF*/)]
  [PXUIField(DisplayName = "Graph Type")]
  public virtual string GraphType
  {
    [PXDependsOnFields(new System.Type[] {typeof (SiteMapCurrentUserEntry.url)})] get
    {
      return this._graphType ?? (this._graphType = PXPageIndexingService.GetGraphType(this.Url));
    }
  }

  [PXInt]
  [PXIntList(typeof (PXCacheRights), true)]
  [PXUIField(DisplayName = "Access Rights")]
  public virtual int? AccessRights { get; set; }

  /// <exclude />
  public abstract class title : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SiteMapCurrentUserEntry.title>
  {
  }

  public abstract class url : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SiteMapCurrentUserEntry.url>
  {
  }

  /// <exclude />
  public abstract class screenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SiteMapCurrentUserEntry.screenID>
  {
  }

  /// <exclude />
  public abstract class orderIndex : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SiteMapCurrentUserEntry.orderIndex>
  {
  }

  /// <exclude />
  public abstract class graphType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SiteMapCurrentUserEntry.graphType>
  {
  }

  /// <exclude />
  public abstract class accessRights : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    SiteMapCurrentUserEntry.accessRights>
  {
  }
}
