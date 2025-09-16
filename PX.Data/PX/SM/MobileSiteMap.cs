// Decompiled with JetBrains decompiler
// Type: PX.SM.MobileSiteMap
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
[PXCacheName("Mobile Site Map")]
public class MobileSiteMap : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected Guid? _CreatedByID;
  protected 
  #nullable disable
  string _CreatedByScreenID;
  protected System.DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected System.DateTime? _LastModifiedDateTime;

  [PXDBString(8, InputMask = ">CC.CC.CC.CC", IsKey = true)]
  [PXUIField(Visibility = PXUIVisibility.SelectorVisible, DisplayName = "Screen ID")]
  public virtual string ScreenID { get; set; }

  [PXDBText(IsUnicode = true)]
  [PXUIField(Visibility = PXUIVisibility.SelectorVisible, DisplayName = "Script")]
  public string Script { get; set; }

  [PXDBString(10, IsUnicode = false, IsKey = true)]
  [PXUIField(Visibility = PXUIVisibility.SelectorVisible, DisplayName = "Type")]
  public string Type { get; set; }

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

  public class PK : PrimaryKeyOf<MobileSiteMap>.By<MobileSiteMap.screenID, MobileSiteMap.type>
  {
    public static MobileSiteMap Find(
      PXGraph graph,
      string screenID,
      string type,
      PKFindOptions options = PKFindOptions.None)
    {
      return PrimaryKeyOf<MobileSiteMap>.By<MobileSiteMap.screenID, MobileSiteMap.type>.FindBy(graph, (object) screenID, (object) type, options);
    }
  }

  /// <exclude />
  public abstract class screenID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  MobileSiteMap.screenID>
  {
  }

  /// <exclude />
  public abstract class script : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  MobileSiteMap.script>
  {
  }

  /// <exclude />
  public abstract class type : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  MobileSiteMap.type>
  {
  }

  /// <exclude />
  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  MobileSiteMap.createdByID>
  {
  }

  /// <exclude />
  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    MobileSiteMap.createdByScreenID>
  {
  }

  /// <exclude />
  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    MobileSiteMap.createdDateTime>
  {
  }

  /// <exclude />
  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    MobileSiteMap.lastModifiedByID>
  {
  }

  /// <exclude />
  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    MobileSiteMap.lastModifiedByScreenID>
  {
  }

  /// <exclude />
  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    MobileSiteMap.lastModifiedDateTime>
  {
  }
}
