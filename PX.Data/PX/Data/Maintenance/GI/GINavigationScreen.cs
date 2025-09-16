// Decompiled with JetBrains decompiler
// Type: PX.Data.Maintenance.GI.GINavigationScreen
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.SM;
using System;

#nullable enable
namespace PX.Data.Maintenance.GI;

/// <exclude />
[PXCacheName("Generic Inquiry Navigation Screen")]
public class GINavigationScreen : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage, ISortOrder
{
  [PXDBGuid(false, IsKey = true)]
  [PXDefault(typeof (GIDesign.designID))]
  [PXParent(typeof (Select<GIDesign, Where<GIDesign.designID, Equal<Current<GINavigationScreen.designID>>>>))]
  public Guid? DesignID { get; set; }

  [PXDefault]
  [PXDBString(2048 /*0x0800*/, IsUnicode = true)]
  [PXSiteMapNodeSelector]
  [PXUIField(DisplayName = "Link")]
  public virtual 
  #nullable disable
  string Link { get; set; }

  [PXDBInt(IsKey = true)]
  [PXDefault]
  [PXLineNbr(typeof (GIDesign))]
  public virtual int? LineNbr { get; set; }

  [PXUIField(DisplayName = "Sort Order", Visible = false, Enabled = false)]
  [PXDBInt]
  public virtual int? SortOrder { get; set; }

  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Active")]
  public bool? IsActive { get; set; }

  [PXString(255 /*0xFF*/, IsUnicode = true)]
  [PXUIField(DisplayName = "Title")]
  [PXDependsOnFields(new System.Type[] {typeof (GINavigationScreen.link)})]
  public virtual string Title
  {
    get
    {
      return this.Link != null ? PXSiteMap.Provider.FindSiteMapNodeByScreenIDUnsecure(this.Link).With<PXSiteMapNode, string>((Func<PXSiteMapNode, string>) (_ => _.Title)) : (string) null;
    }
  }

  [PXDefault("S")]
  [PXWindowMode]
  [PXUIField(DisplayName = "Window Mode")]
  [PXDBString(1)]
  public virtual string WindowMode { get; set; }

  [PXDBString(255 /*0xFF*/, IsUnicode = true)]
  [PXUIField(DisplayName = "Icon")]
  [PXUIVisible(typeof (Where<GINavigationScreen.windowMode, Equal<PXWindowModeAttribute.layer>>))]
  [PXScalableIconsList]
  public string Icon { get; set; }

  [PXDBLocalizableString(255 /*0xFF*/, IsUnicode = true)]
  [PXUIField(DisplayName = "Custom Title")]
  [PXUIVisible(typeof (Where<GINavigationScreen.windowMode, Equal<PXWindowModeAttribute.layer>>))]
  public virtual string CustomTitle { get; set; }

  [PXNote]
  public virtual Guid? NoteID { get; set; }

  [PXDBCreatedByID]
  [PXUIField(DisplayName = "Created By", Enabled = false, Visible = false)]
  public Guid? CreatedByID { get; set; }

  [PXDBCreatedByScreenID]
  public string CreatedByScreenID { get; set; }

  [PXDBCreatedDateTime]
  [PXUIField(DisplayName = "Creation Date", Enabled = false, Visible = false)]
  public System.DateTime? CreatedDateTime { get; set; }

  [PXDBLastModifiedByID]
  public Guid? LastModifiedByID { get; set; }

  [PXDBLastModifiedByScreenID]
  public string LastModifiedByScreenID { get; set; }

  [PXDBLastModifiedDateTime]
  public System.DateTime? LastModifiedDateTime { get; set; }

  public class PK : 
    PrimaryKeyOf<GINavigationScreen>.By<GINavigationScreen.designID, GINavigationScreen.lineNbr>
  {
    public static GINavigationScreen Find(
      PXGraph graph,
      Guid? designID,
      int? lineNbr,
      PKFindOptions options = PKFindOptions.None)
    {
      return PrimaryKeyOf<GINavigationScreen>.By<GINavigationScreen.designID, GINavigationScreen.lineNbr>.FindBy(graph, (object) designID, (object) lineNbr, options);
    }
  }

  public static class FK
  {
    public class Design : 
      PrimaryKeyOf<GIDesign>.By<GIDesign.designID>.ForeignKeyOf<GINavigationScreen>.By<GINavigationScreen.designID>
    {
    }
  }

  /// <exclude />
  public abstract class designID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  GINavigationScreen.designID>
  {
  }

  /// <exclude />
  public abstract class link : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  GINavigationScreen.link>
  {
  }

  /// <exclude />
  public abstract class lineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  GINavigationScreen.lineNbr>
  {
  }

  public abstract class sortOrder : IBqlField, IBqlOperand
  {
  }

  /// <exclude />
  public abstract class isActive : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  GINavigationScreen.isActive>
  {
  }

  /// <exclude />
  public abstract class title : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  GINavigationScreen.title>
  {
  }

  public abstract class windowMode : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  GINavigationScreen.windowMode>
  {
  }

  public abstract class icon : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  GINavigationScreen.icon>
  {
  }

  public abstract class customTitle : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    GINavigationScreen.customTitle>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  GINavigationScreen.noteID>
  {
  }

  /// <exclude />
  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  GINavigationScreen.createdByID>
  {
  }

  /// <exclude />
  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    GINavigationScreen.createdByScreenID>
  {
  }

  /// <exclude />
  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    GINavigationScreen.createdDateTime>
  {
  }

  /// <exclude />
  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    GINavigationScreen.lastModifiedByID>
  {
  }

  /// <exclude />
  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    GINavigationScreen.lastModifiedByScreenID>
  {
  }

  /// <exclude />
  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    GINavigationScreen.lastModifiedDateTime>
  {
  }
}
