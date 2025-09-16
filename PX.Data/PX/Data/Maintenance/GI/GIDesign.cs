// Decompiled with JetBrains decompiler
// Type: PX.Data.Maintenance.GI.GIDesign
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.BQL;
using PX.Data.GenericInquiry;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Data.Search;
using System;

#nullable enable
namespace PX.Data.Maintenance.GI;

/// <exclude />
[PXPrimaryGraph(typeof (GenericInquiryDesigner))]
[PXCacheName("Generic Inquiry")]
public class GIDesign : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  public const 
  #nullable disable
  string PrimaryScreenID_DBColumn = "PrimaryScreenIDNew";

  [PXDBGuid(false)]
  [PXDefault]
  [PXReferentialIntegrityCheck]
  public Guid? DesignID { get; set; }

  [PXDBString(IsKey = true, IsUnicode = true, InputMask = "")]
  [PXDefault]
  [PXUIField(DisplayName = "Inquiry Title", Visibility = PXUIVisibility.SelectorVisible)]
  [PXSelector(typeof (GIDesign.name), new System.Type[] {typeof (GIDesign.name), typeof (GIDesign.exposeViaOData), typeof (GIDesign.sitemapSelectorTitle)})]
  public string Name { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "Select Top")]
  public int? SelectTop { get; set; }

  [PXDBInt(MinValue = 1, MaxValue = 20)]
  [PXDefault(3)]
  [PXUIField(DisplayName = "Arrange Parameters in")]
  public int? FilterColCount { get; set; }

  [PXDBInt(MinValue = 0, MaxValue = 1000)]
  [PXDefault(0, PersistingCheck = PXPersistingCheck.Nothing)]
  [PXUIField(DisplayName = "Records per Page")]
  public int? PageSize { get; set; }

  [PXDBInt(MinValue = 0)]
  [PXDefault(0, PersistingCheck = PXPersistingCheck.Nothing)]
  [PXUIField(DisplayName = "Export Top")]
  public int? ExportTop { get; set; }

  [PXDBString(8, IsFixed = true, InputMask = "CC.CC.CC.CC", DatabaseFieldName = "PrimaryScreenIDNew")]
  [PXUIField(DisplayName = "Entry Screen")]
  [PXSiteMapNodeSelector]
  [PXForeignReference(typeof (GIDesign.FK.SiteMap))]
  [PXForeignReference(typeof (GIDesign.FK.PortalMap))]
  public string PrimaryScreenID { get; set; }

  [PXBool]
  [PXUIField(DisplayName = "Replace Entry Screen with this Inquiry in Menu")]
  public bool? ReplacePrimaryScreen { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Enable New Record Creation")]
  public bool? NewRecordCreationEnabled { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Enable Mass Record Deletion")]
  public bool? MassDeleteEnabled { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Auto-Confirm Custom Delete Confirmations")]
  public bool? AutoConfirmDelete { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Enable Mass Record Update")]
  public bool? MassRecordsUpdateEnabled { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Enable Mass Actions on Records")]
  public bool? MassActionsOnRecordsEnabled { get; set; }

  [PXDBBool]
  [PXUIField(DisplayName = "Expose via OData")]
  [PXDefault(false)]
  public bool? ExposeViaOData { get; set; }

  [PXDBBool]
  [PXUIField(DisplayName = "Expose to the Mobile Application")]
  [PXDefault(false)]
  public bool? ExposeViaMobile { get; set; }

  [PXDBString(IsUnicode = true)]
  [PXDefault("$<None>", PersistingCheck = PXPersistingCheck.Nothing)]
  [PXUIField(DisplayName = "Attach Notes To", Visibility = PXUIVisibility.SelectorVisible)]
  [PXStringList(new string[] {"$<None>"}, new string[] {"Not Applicable"})]
  public string NotesAndFilesTable { get; set; }

  [PXFormulaEditor(DisplayName = "Row Style", IsDBField = true)]
  [PXFormulaEditor.AddOperators]
  [PXFormulaEditor.AddFunctions]
  [PXFormulaEditor.AddStyles]
  public string RowStyleFormula { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Show Deleted Records")]
  public bool? ShowDeletedRecords { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Show Archived Records")]
  public bool? ShowArchivedRecords { get; set; }

  [PXNote]
  [PXSearchable(65535 /*0xFFFF*/, "Generic Inquiry: {0}", new System.Type[] {typeof (GIDesign.name), typeof (GIDesign.designID)}, new System.Type[] {typeof (PX.SM.SiteMap.title), typeof (PX.SM.SiteMap.screenID)}, typeof (ForeignDAC<PX.SM.SiteMap>.GetFields<PX.SM.SiteMap.title, PX.SM.SiteMap.screenID>.WithParameter<GIDesign.designID>.AndQuery<Select2<PX.SM.SiteMap, InnerJoin<GIDesign, On<PX.SM.SiteMap.url, Equal<Add<GIDesign.GIUrl.giUrlID, ConvertToStr<GIDesign.designID>>>, Or<PX.SM.SiteMap.url, Equal<Add<GIDesign.GIUrl.giUrlName, GIDesign.name>>>>>, Where<GIDesign.designID, Equal<Required<GIDesign.designID>>>>>), Line1Format = "{0}", Line1Fields = new System.Type[] {typeof (PX.SM.SiteMap.title)}, Line2Format = "{0}", Line2Fields = new System.Type[] {typeof (PX.SM.SiteMap.screenID)}, SelectForFastIndexing = typeof (Select2<GIDesign, InnerJoin<PX.SM.SiteMap, On<PX.SM.SiteMap.url, Equal<Add<GIDesign.GIUrl.giUrlID, ConvertToStr<GIDesign.designID>>>, Or<PX.SM.SiteMap.url, Equal<Add<GIDesign.GIUrl.giUrlName, GIDesign.name>>>>>>))]
  public virtual Guid? NoteID { get; set; }

  [PXDBCreatedByID]
  [PXUIField(DisplayName = "Created By", Enabled = false)]
  public Guid? CreatedByID { get; set; }

  [PXDBCreatedByScreenID]
  public string CreatedByScreenID { get; set; }

  [PXDBCreatedDateTime]
  [PXUIField(DisplayName = "Creation Date", Enabled = false)]
  public System.DateTime? CreatedDateTime { get; set; }

  [PXDBLastModifiedByID]
  public Guid? LastModifiedByID { get; set; }

  [PXDBLastModifiedByScreenID]
  public string LastModifiedByScreenID { get; set; }

  [PXDBLastModifiedDateTime]
  public System.DateTime? LastModifiedDateTime { get; set; }

  [PXString(IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Site Map Title", Enabled = false)]
  public string SitemapTitle { get; set; }

  [PXString(IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Site Map Title")]
  public string SitemapSelectorTitle => GenericInquiryHelpers.FindSiteMapNodeByGi(this)?.Title;

  [PXString(8, IsFixed = true, InputMask = "CC.CC.CC.CC")]
  [PXUIField(DisplayName = "Screen ID", Enabled = false)]
  public string SitemapScreenID { get; set; }

  [PXString]
  [PXUIField(DisplayName = "Default Sort Order", Enabled = false, Visible = false)]
  public string DefaultSortOrder { get; set; }

  public class PK : PrimaryKeyOf<GIDesign>.By<GIDesign.designID>
  {
    public static GIDesign Find(PXGraph graph, Guid? designID, PKFindOptions options = PKFindOptions.None)
    {
      return PrimaryKeyOf<GIDesign>.By<GIDesign.designID>.FindBy(graph, (object) designID, options);
    }
  }

  public class UK : PrimaryKeyOf<GIDesign>.By<GIDesign.name>
  {
    public static GIDesign Find(PXGraph graph, string name, PKFindOptions options = PKFindOptions.None)
    {
      return PrimaryKeyOf<GIDesign>.By<GIDesign.name>.FindBy(graph, (object) name, options);
    }
  }

  public static class FK
  {
    public class SiteMap : 
      PrimaryKeyOf<PX.SM.SiteMap>.By<PX.SM.SiteMap.screenID>.ForeignKeyOf<GIDesign>.By<GIDesign.primaryScreenID>
    {
    }

    public class PortalMap : 
      PrimaryKeyOf<PX.SM.PortalMap>.By<PX.SM.PortalMap.screenID>.ForeignKeyOf<GIDesign>.By<GIDesign.primaryScreenID>
    {
    }
  }

  /// <exclude />
  public abstract class designID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  GIDesign.designID>
  {
  }

  /// <exclude />
  public abstract class name : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  GIDesign.name>
  {
  }

  /// <exclude />
  public abstract class selectTop : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  GIDesign.selectTop>
  {
  }

  /// <exclude />
  public abstract class filterColCount : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  GIDesign.filterColCount>
  {
  }

  /// <exclude />
  public abstract class pageSize : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  GIDesign.pageSize>
  {
  }

  /// <exclude />
  public abstract class exportTop : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  GIDesign.exportTop>
  {
  }

  /// <exclude />
  public abstract class primaryScreenID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  GIDesign.primaryScreenID>
  {
  }

  /// <exclude />
  public abstract class replacePrimaryScreen : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    GIDesign.replacePrimaryScreen>
  {
  }

  /// <exclude />
  public abstract class newRecordCreationEnabled : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    GIDesign.newRecordCreationEnabled>
  {
  }

  /// <exclude />
  public abstract class massDeleteEnabled : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  GIDesign.massDeleteEnabled>
  {
  }

  /// <exclude />
  public abstract class autoConfirmDelete : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  GIDesign.autoConfirmDelete>
  {
  }

  /// <exclude />
  public abstract class massRecordsUpdateEnabled : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    GIDesign.massRecordsUpdateEnabled>
  {
  }

  /// <exclude />
  public abstract class massActionsOnRecordsEnabled : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    GIDesign.massActionsOnRecordsEnabled>
  {
  }

  /// <exclude />
  public abstract class exposeViaOData : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  GIDesign.exposeViaOData>
  {
  }

  /// <exclude />
  public abstract class exposeViaMobile : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  GIDesign.exposeViaMobile>
  {
  }

  /// <exclude />
  public abstract class notesAndFilesTable : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    GIDesign.notesAndFilesTable>
  {
  }

  /// <exclude />
  public abstract class rowStyleFormula : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  GIDesign.rowStyleFormula>
  {
  }

  public abstract class showDeletedRecords : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    GIDesign.showDeletedRecords>
  {
  }

  public abstract class showArchivedRecords : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    GIDesign.showArchivedRecords>
  {
  }

  /// <exclude />
  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  GIDesign.noteID>
  {
  }

  public class GIUrl
  {
    public class giUrlID : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    GIDesign.GIUrl.giUrlID>
    {
      public giUrlID()
        : base("~/GenericInquiry/GenericInquiry.aspx?id=")
      {
      }
    }

    public class giUrlName : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    GIDesign.GIUrl.giUrlName>
    {
      public giUrlName()
        : base("~/GenericInquiry/GenericInquiry.aspx?name=")
      {
      }
    }
  }

  /// <exclude />
  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  GIDesign.createdByID>
  {
  }

  /// <exclude />
  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    GIDesign.createdByScreenID>
  {
  }

  /// <exclude />
  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    GIDesign.createdDateTime>
  {
  }

  /// <exclude />
  public abstract class lastModifiedByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  GIDesign.lastModifiedByID>
  {
  }

  /// <exclude />
  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    GIDesign.lastModifiedByScreenID>
  {
  }

  /// <exclude />
  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    GIDesign.lastModifiedDateTime>
  {
  }

  public abstract class sitemapTitle : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  GIDesign.sitemapTitle>
  {
  }

  /// <exclude />
  public abstract class sitemapSelectorTitle : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    GIDesign.sitemapSelectorTitle>
  {
  }

  /// <exclude />
  public abstract class sitemapScreenID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  GIDesign.sitemapScreenID>
  {
  }

  /// <exclude />
  public abstract class defaultSortOrder : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    GIDesign.defaultSortOrder>
  {
  }
}
