// Decompiled with JetBrains decompiler
// Type: PX.Data.ListEntryPoint
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using System;

#nullable enable
namespace PX.Data;

/// <exclude />
[PXCacheName("List as Entry Point")]
[PXPrimaryGraph(typeof (LEPMaint))]
public class ListEntryPoint : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage, IAuditInfo
{
  [PXDefault]
  [PXDBString(8, IsKey = true, IsFixed = true, InputMask = "CC.CC.CC.CC")]
  [PXUIField(DisplayName = "Entry Screen ID")]
  [PXSiteMapNodeSelector]
  [PXParent(typeof (ListEntryPoint.FK.EntryScreen))]
  [PXParent(typeof (ListEntryPoint.FK.PortalEntryScreen))]
  public 
  #nullable disable
  string EntryScreenID { get; set; }

  [PXDefault]
  [PXDBString(8, IsFixed = true, InputMask = "CC.CC.CC.CC")]
  [PXUnique(ErrorMessage = "This list screen has already been used for another entry screen. Please select another screen.")]
  [PXUIField(DisplayName = "Substitute Screen ID")]
  [PXSiteMapNodeSelector]
  [PXParent(typeof (ListEntryPoint.FK.ListScreen))]
  [PXParent(typeof (ListEntryPoint.FK.PortalListScreen))]
  public string ListScreenID { get; set; }

  [PXDBBool]
  [PXUIField(DisplayName = "Active")]
  [PXDefault(true)]
  public bool? IsActive { get; set; }

  [PXDBCreatedByID]
  public Guid? CreatedByID { get; set; }

  [PXDBCreatedByScreenID]
  public string CreatedByScreenID { get; set; }

  [PXDBCreatedDateTime]
  [PXUIField(DisplayName = "Created On", Enabled = false, IsReadOnly = true)]
  public System.DateTime? CreatedDateTime { get; set; }

  [PXDBLastModifiedByID]
  public Guid? LastModifiedByID { get; set; }

  [PXDBLastModifiedByScreenID]
  public string LastModifiedByScreenID { get; set; }

  [PXDBLastModifiedDateTime]
  [PXUIField(DisplayName = "Last Modified On", Enabled = false, IsReadOnly = true)]
  public System.DateTime? LastModifiedDateTime { get; set; }

  public class PK : PrimaryKeyOf<ListEntryPoint>.By<ListEntryPoint.entryScreenID>
  {
    public static ListEntryPoint Find(PXGraph graph, string entryScreenID, PKFindOptions options = PKFindOptions.None)
    {
      return PrimaryKeyOf<ListEntryPoint>.By<ListEntryPoint.entryScreenID>.FindBy(graph, (object) entryScreenID, options);
    }
  }

  public static class FK
  {
    public class EntryScreen : 
      PrimaryKeyOf<PX.SM.SiteMap>.By<PX.SM.SiteMap.screenID>.ForeignKeyOf<ListEntryPoint>.By<ListEntryPoint.entryScreenID>
    {
    }

    public class ListScreen : 
      PrimaryKeyOf<PX.SM.SiteMap>.By<PX.SM.SiteMap.screenID>.ForeignKeyOf<ListEntryPoint>.By<ListEntryPoint.listScreenID>
    {
    }

    public class PortalEntryScreen : 
      PrimaryKeyOf<PX.SM.PortalMap>.By<PX.SM.PortalMap.screenID>.ForeignKeyOf<ListEntryPoint>.By<ListEntryPoint.entryScreenID>
    {
    }

    public class PortalListScreen : 
      PrimaryKeyOf<PX.SM.PortalMap>.By<PX.SM.PortalMap.screenID>.ForeignKeyOf<ListEntryPoint>.By<ListEntryPoint.listScreenID>
    {
    }
  }

  /// <exclude />
  public abstract class entryScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ListEntryPoint.entryScreenID>
  {
  }

  public abstract class listScreenID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ListEntryPoint.listScreenID>
  {
  }

  /// <exclude />
  public abstract class isActive : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ListEntryPoint.isActive>
  {
  }

  /// <exclude />
  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  ListEntryPoint.createdByID>
  {
  }

  /// <exclude />
  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ListEntryPoint.createdByScreenID>
  {
  }

  /// <exclude />
  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    ListEntryPoint.createdDateTime>
  {
  }

  /// <exclude />
  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    ListEntryPoint.lastModifiedByID>
  {
  }

  /// <exclude />
  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ListEntryPoint.lastModifiedByScreenID>
  {
  }

  /// <exclude />
  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    ListEntryPoint.lastModifiedDateTime>
  {
  }
}
