// Decompiled with JetBrains decompiler
// Type: PX.SM.SiteMap
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using System;

#nullable enable
namespace PX.SM;

/// <exclude />
[PXCacheName("Site Map")]
public class SiteMap : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage, IAuditInfo
{
  protected Guid? _NodeID;
  protected double? _Position;
  protected 
  #nullable disable
  string _Title;
  protected string _Url;
  protected string _UrlBackup;
  protected string _SelectedUI;
  protected string _ScreenID;
  private string _graphType;
  protected Guid? _ParentID;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected System.DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected System.DateTime? _LastModifiedDateTime;

  [PXDBGuid(false, IsKey = true)]
  [PXDefault]
  [PXUIField(DisplayName = "Node ID", Visibility = PXUIVisibility.Invisible, Visible = false)]
  public virtual Guid? NodeID
  {
    get => this._NodeID;
    set => this._NodeID = value;
  }

  [PXDBDouble]
  public virtual double? Position
  {
    get => this._Position;
    set => this._Position = value;
  }

  [PXDBString(255 /*0xFF*/, IsUnicode = true)]
  [PXUIField(Visibility = PXUIVisibility.SelectorVisible, DisplayName = "Title")]
  public virtual string Title
  {
    get => this._Title;
    set => this._Title = value;
  }

  [PXDBString(512 /*0x0200*/)]
  [PXUIField(DisplayName = "URL", Visibility = PXUIVisibility.SelectorVisible)]
  public virtual string Url
  {
    get => this._Url;
    set
    {
      this._Url = value;
      this._graphType = (string) null;
    }
  }

  [PXDBString(512 /*0x0200*/)]
  public virtual string UrlBackup
  {
    get => this._UrlBackup;
    set => this._UrlBackup = value;
  }

  /// <summary>Select</summary>
  [PXDBString(1, IsFixed = true)]
  [PXStringList(new string[] {"T", "E", "D"}, new string[] {"Modern", "Classic", "Default"})]
  [PXDefault("D", PersistingCheck = PXPersistingCheck.Nothing)]
  [PXUIField(DisplayName = "UI", Visibility = PXUIVisibility.SelectorVisible)]
  public virtual string SelectedUI
  {
    get => this._SelectedUI;
    set
    {
      this._SelectedUI = value;
      this._graphType = (string) null;
    }
  }

  [PXDBString(8, InputMask = ">CC.CC.CC.CC")]
  [PXUIField(Visibility = PXUIVisibility.SelectorVisible, DisplayName = "Screen ID")]
  [PXDefault]
  [PXReferentialIntegrityCheck(RowsExcludingCondition = typeof (Search<SiteMap.screenID, Where<SiteMap.screenID, Equal<Current<SiteMap.screenID>>, And<SiteMap.nodeID, NotEqual<Current<SiteMap.nodeID>>>>>))]
  public virtual string ScreenID
  {
    get => this._ScreenID;
    set => this._ScreenID = value;
  }

  [PXString(255 /*0xFF*/)]
  [PXUIField(DisplayName = "Graph Type", Visibility = PXUIVisibility.SelectorVisible, Enabled = false)]
  public virtual string Graphtype
  {
    [PXDependsOnFields(new System.Type[] {typeof (SiteMap.url)})] get
    {
      return this._graphType ?? (this._graphType = PXPageIndexingService.GetGraphType(this.Url));
    }
  }

  [PXDBGuid(false)]
  [PXDefault]
  public virtual Guid? ParentID
  {
    get => this._ParentID;
    set => this._ParentID = value;
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
  [PXUIField(DisplayName = "Last Modified By", Visible = true, Enabled = false)]
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
  [PXUIField(DisplayName = "Last Modified On", Visible = true, Enabled = false)]
  public virtual System.DateTime? LastModifiedDateTime
  {
    get => this._LastModifiedDateTime;
    set => this._LastModifiedDateTime = value;
  }

  public class PK : PrimaryKeyOf<SiteMap>.By<SiteMap.nodeID>
  {
    public static SiteMap Find(PXGraph graph, Guid? nodeID, PKFindOptions options = PKFindOptions.None)
    {
      return PrimaryKeyOf<SiteMap>.By<SiteMap.nodeID>.FindBy(graph, (object) nodeID, options);
    }
  }

  public class UK : PrimaryKeyOf<SiteMap>.By<SiteMap.screenID>
  {
    public static SiteMap Find(PXGraph graph, string screenID, PKFindOptions options = PKFindOptions.None)
    {
      return PrimaryKeyOf<SiteMap>.By<SiteMap.screenID>.FindBy(graph, (object) screenID, options);
    }
  }

  /// <exclude />
  public abstract class nodeID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  SiteMap.nodeID>
  {
  }

  /// <exclude />
  public abstract class position : BqlType<
  #nullable enable
  IBqlDouble, double>.Field<
  #nullable disable
  SiteMap.position>
  {
  }

  /// <exclude />
  public abstract class title : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SiteMap.title>
  {
  }

  /// <exclude />
  public abstract class url : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SiteMap.url>
  {
  }

  /// <exclude />
  public abstract class urlBackup : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SiteMap.urlBackup>
  {
  }

  public abstract class selectedUI : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SiteMap.selectedUI>
  {
  }

  /// <exclude />
  public abstract class screenID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SiteMap.screenID>
  {
  }

  /// <exclude />
  public abstract class graphtype : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SiteMap.graphtype>
  {
  }

  /// <exclude />
  public abstract class parentID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  SiteMap.parentID>
  {
  }

  /// <exclude />
  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  SiteMap.createdByID>
  {
  }

  /// <exclude />
  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SiteMap.createdByScreenID>
  {
  }

  /// <exclude />
  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    SiteMap.createdDateTime>
  {
  }

  /// <exclude />
  public abstract class lastModifiedByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  SiteMap.lastModifiedByID>
  {
  }

  /// <exclude />
  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SiteMap.lastModifiedByScreenID>
  {
  }

  /// <exclude />
  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    SiteMap.lastModifiedDateTime>
  {
  }
}
