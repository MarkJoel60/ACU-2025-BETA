// Decompiled with JetBrains decompiler
// Type: PX.Api.SYMapping
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.SM;
using System;

#nullable enable
namespace PX.Api;

[PXPrimaryGraph(new System.Type[] {typeof (SYImportMaint), typeof (SYExportMaint)}, new System.Type[] {typeof (Where<SYMapping.mappingType, Equal<SYMapping.mappingType.typeImport>>), typeof (Where<SYMapping.mappingType, Equal<SYMapping.mappingType.typeExport>>)})]
[PXCacheName("Mapping")]
public class SYMapping : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage, ISentByEvent
{
  internal const 
  #nullable disable
  string EventSubscriberType = "IMPT";

  [PXDBBool]
  [PXUIField(DisplayName = "Simple Scenario", Enabled = false)]
  public bool? IsSimpleMapping { get; set; }

  [PXBool]
  [PXDefault(false, PersistingCheck = PXPersistingCheck.Nothing)]
  [PXUIField(DisplayName = "Selected")]
  public bool? Selected { get; set; }

  [PXDBGuid(false)]
  [PXDefault]
  [PXUIField(DisplayName = "Mapping ID")]
  [PXReferentialIntegrityCheck]
  public Guid? MappingID { get; set; }

  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Active")]
  public virtual bool? IsActive { get; set; }

  [PXDBString(128 /*0x80*/, IsKey = true, IsUnicode = true, InputMask = "")]
  [PXDefault]
  [PXUIField(DisplayName = "Name", Visibility = PXUIVisibility.SelectorVisible)]
  [PXSYMappingSelector]
  public virtual string Name { get; set; }

  [PXString(IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Site Map Title")]
  public string SitemapTitle { get; set; }

  [PXString(IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Site Map ScreenID", Visible = false)]
  public string SitemapScreenId { get; set; }

  [PXDBGuid(false)]
  [PXSelector(typeof (Search<SYMapping.mappingID, Where<SYMapping.mappingType, NotEqual<Current<SYMapping.mappingType>>>>), new System.Type[] {typeof (SYMapping.name), typeof (SYMapping.screenID)}, DescriptionField = typeof (SYMapping.name))]
  [PXUIField(DisplayName = "Inverse Mapping ID", Visible = false)]
  public virtual Guid? InverseMappingID { get; set; }

  [PXDBString(8, IsFixed = true, InputMask = "CC.CC.CC.CC")]
  [PXDefault]
  [PXUIField(DisplayName = "Screen Name", Visibility = PXUIVisibility.SelectorVisible)]
  [PXSiteMapNodeSelector]
  public virtual string ScreenID { get; set; }

  [PXDBString(1, IsFixed = true)]
  [SYMapping.mappingType.StringList]
  [PXDefault]
  public virtual string MappingType { get; set; }

  [PXDBByte]
  [PXUIField(DisplayName = "Detail Export Mode")]
  [PXDefault(0)]
  [SYMapping.repeatingOption.IntList]
  public virtual byte? RepeatingData { get; set; }

  [PXDBString(128 /*0x80*/)]
  [PXDefault]
  public virtual string GraphName { get; set; }

  [PXDBString(128 /*0x80*/)]
  [PXDefault]
  public virtual string ViewName { get; set; }

  [PXDBString(128 /*0x80*/)]
  public virtual string GridViewName { get; set; }

  [PXDBGuid(false)]
  [PXUIField(DisplayName = "Provider")]
  [PXDefault]
  [PXSelector(typeof (Search<SYProvider.providerID, Where<SYProvider.isActive, Equal<PX.Data.True>>>), SubstituteKey = typeof (SYProvider.name))]
  public virtual Guid? ProviderID { get; set; }

  [PXDBString(128 /*0x80*/, IsUnicode = true, InputMask = "")]
  [PXDefault]
  [PXUIField(DisplayName = "Provider Object")]
  [PXSelector(typeof (Search<SYProviderObject.name, Where<SYProviderObject.providerID, Equal<Current<SYMapping.providerID>>, And<SYProviderObject.isActive, Equal<PX.Data.True>>>>))]
  public virtual string ProviderObject { get; set; }

  [PXDBString(1, IsFixed = true)]
  [PX.Api.SyncType.StringList]
  [PXUIField(DisplayName = "Sync Type")]
  [PXDefault("F")]
  public virtual string SyncType { get; set; }

  [PXDBString(1, IsFixed = true)]
  [PXUIField(DisplayName = "Status", Enabled = false)]
  [MappingStatus.StringList]
  [PXDefault("N")]
  public virtual string Status { get; set; }

  [PXDBShort]
  [PXDefault(0)]
  public virtual short? FieldCntr { get; set; }

  [PXDBShort]
  [PXDefault(0)]
  public virtual short? FieldOrderCntr { get; set; }

  [PXDBInt]
  [PXDefault(0)]
  public virtual int? DataCntr { get; set; }

  [PXDBShort]
  [PXDefault(0)]
  public virtual short? ImportConditionCntr { get; set; }

  [PXDBShort]
  [PXDefault(0)]
  public virtual short? ConditionCntr { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "Number of Records", Enabled = false)]
  [PXDefault(0)]
  public virtual int? NbrRecords { get; set; }

  [PXDBDate(PreserveTime = true, UseSmallDateTime = false, UseTimeZone = true, DisplayMask = "g", InputMask = "g")]
  [PXUIField(DisplayName = "Prepared On", Enabled = false)]
  public virtual System.DateTime? PreparedOn { get; set; }

  [PXDBDate(PreserveTime = true, UseSmallDateTime = false, UseTimeZone = true, DisplayMask = "g", InputMask = "g")]
  [PXUIField(DisplayName = "Completed On", Enabled = false)]
  public virtual System.DateTime? CompletedOn { get; set; }

  [PXDBString(4000, IsUnicode = true)]
  public virtual string ImportTimeStamp { get; set; }

  [PXDBDate(PreserveTime = true, UseTimeZone = false, UseSmallDateTime = false, DisplayMask = "g", InputMask = "g")]
  public virtual System.DateTime? ExportTimeStamp { get; set; }

  [PXDBDate(PreserveTime = true, UseTimeZone = true, UseSmallDateTime = false, DisplayMask = "g", InputMask = "g")]
  public virtual System.DateTime? ExportTimeStampUtc { get; set; }

  [PXSYMappingNote]
  public virtual Guid? NoteID { get; set; }

  [PXDBString(6)]
  [PXCultureSelector]
  [PXUIField(DisplayName = "Format Locale")]
  public virtual string FormatLocale { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Export Only Mapped Fields")]
  public virtual bool? IsExportOnlyMappingFields { get; set; }

  [PXDBCreatedByID]
  public virtual Guid? CreatedByID { get; set; }

  [PXDBCreatedByScreenID]
  public virtual string CreatedByScreenID { get; set; }

  [PXDBCreatedDateTime]
  public virtual System.DateTime? CreatedDateTime { get; set; }

  [PXDBLastModifiedByID]
  public virtual Guid? LastModifiedByID { get; set; }

  [PXDBLastModifiedByScreenID]
  public virtual string LastModifiedByScreenID { get; set; }

  [PXDBLastModifiedDateTime]
  public virtual System.DateTime? LastModifiedDateTime { get; set; }

  [PXDBTimestamp]
  public virtual byte[] TStamp { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Parallel Processing")]
  public bool? ProcessInParallel { get; set; }

  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Break on Error")]
  public bool? BreakOnError { get; set; }

  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Break on Incorrect Target")]
  public bool? BreakOnTarget { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Skip Headers")]
  public bool? SkipHeaders { get; set; }

  [PXDBGuid(false)]
  public Guid? SitemapID { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Discard Previous Result")]
  public bool? DiscardResult { get; set; }

  [PXInternalUseOnly]
  string ISentByEvent.SubscriberType
  {
    get => "IMPT";
    set
    {
    }
  }

  [PXBool]
  [PXUIField(Visible = false)]
  public virtual bool? ShowCreatedByEventsTabExpr => new bool?(false);

  Guid? ISentByEvent.GetHandlerId() => this.MappingID;

  public class PK : PrimaryKeyOf<SYMapping>.By<SYMapping.mappingID>
  {
    public static SYMapping Find(PXGraph graph, Guid? mappingID, PKFindOptions options = PKFindOptions.None)
    {
      return PrimaryKeyOf<SYMapping>.By<SYMapping.mappingID>.FindBy(graph, (object) mappingID, options);
    }
  }

  public static class FK
  {
    public class SiteMap : 
      PrimaryKeyOf<PX.SM.SiteMap>.By<PX.SM.SiteMap.screenID>.ForeignKeyOf<SYMapping>.By<SYMapping.screenID>
    {
    }

    public class PortalMap : 
      PrimaryKeyOf<PX.SM.PortalMap>.By<PX.SM.PortalMap.screenID>.ForeignKeyOf<SYMapping>.By<SYMapping.screenID>
    {
    }

    public class Provider : 
      PrimaryKeyOf<SYProvider>.By<SYProvider.providerID>.ForeignKeyOf<SYMapping>.By<SYMapping.providerID>
    {
    }

    public class Locale : 
      PrimaryKeyOf<PX.SM.Locale>.By<PX.SM.Locale.localeName>.ForeignKeyOf<SYMapping>.By<SYMapping.formatLocale>
    {
    }
  }

  public abstract class isSimpleMapping : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SYMapping.isSimpleMapping>
  {
  }

  public abstract class selected : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SYMapping.selected>
  {
  }

  public abstract class mappingID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  SYMapping.mappingID>
  {
  }

  public abstract class isActive : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SYMapping.isActive>
  {
  }

  public abstract class name : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SYMapping.name>
  {
  }

  public abstract class sitemapTitle : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SYMapping.sitemapTitle>
  {
  }

  public abstract class sitemapScreenId : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SYMapping.sitemapScreenId>
  {
  }

  public abstract class inverseMappingID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  SYMapping.inverseMappingID>
  {
  }

  public abstract class screenID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SYMapping.screenID>
  {
  }

  public abstract class mappingType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SYMapping.mappingType>
  {
    internal static readonly string[] Values = new string[2]
    {
      "I",
      "E"
    };
    internal static readonly string[] Labels = new string[2]
    {
      nameof (Import),
      nameof (Export)
    };
    internal const string Import = "I";
    internal const string Export = "E";

    internal static class UI
    {
      internal const string Import = "Import";
      internal const string Export = "Export";
    }

    internal class StringListAttribute : PXStringListAttribute
    {
      internal StringListAttribute()
        : base(SYMapping.mappingType.Values, SYMapping.mappingType.Labels)
      {
      }
    }

    public interface IMappingConst : IConstant<string>, IConstant, IBqlOperand
    {
    }

    public class mappingConst : 
      BqlType<
      #nullable enable
      IBqlString, string>.Constant<
      #nullable disable
      SYMapping.mappingType.mappingConst>,
      SYMapping.mappingType.IMappingConst,
      IConstant<string>,
      IConstant,
      IBqlOperand
    {
      public mappingConst()
        : base("")
      {
      }
    }

    public class typeImport : 
      BqlType<
      #nullable enable
      IBqlString, string>.Constant<
      #nullable disable
      SYMapping.mappingType.typeImport>,
      SYMapping.mappingType.IMappingConst,
      IConstant<string>,
      IConstant,
      IBqlOperand
    {
      public typeImport()
        : base("I")
      {
      }
    }

    public class typeExport : 
      BqlType<
      #nullable enable
      IBqlString, string>.Constant<
      #nullable disable
      SYMapping.mappingType.typeExport>,
      SYMapping.mappingType.IMappingConst,
      IConstant<string>,
      IConstant,
      IBqlOperand
    {
      public typeExport()
        : base("E")
      {
      }
    }
  }

  public enum RepeatingOption
  {
    Primary,
    All,
    None,
  }

  internal static class repeatingOption
  {
    public class primary : BqlType<
    #nullable enable
    IBqlByte, byte>.Constant<
    #nullable disable
    SYMapping.repeatingOption.primary>
    {
      public primary()
        : base((byte) 0)
      {
      }
    }

    public class all : BqlType<
    #nullable enable
    IBqlByte, byte>.Constant<
    #nullable disable
    SYMapping.repeatingOption.all>
    {
      public all()
        : base((byte) 1)
      {
      }
    }

    public class none : BqlType<
    #nullable enable
    IBqlByte, byte>.Constant<
    #nullable disable
    SYMapping.repeatingOption.none>
    {
      public none()
        : base((byte) 2)
      {
      }
    }

    public class IntListAttribute : PXIntListAttribute
    {
      public IntListAttribute()
        : base(new int[3]{ 0, 1, 2 }, new string[3]
        {
          "Repeat Only Summary Fields",
          "Repeat All Fields",
          "Do Not Repeat Fields"
        })
      {
      }
    }
  }

  public abstract class repeatingData : BqlType<
  #nullable enable
  IBqlByte, byte>.Field<
  #nullable disable
  SYMapping.repeatingData>
  {
  }

  public abstract class graphName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SYMapping.graphName>
  {
  }

  public abstract class viewName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SYMapping.viewName>
  {
  }

  public abstract class gridViewName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SYMapping.gridViewName>
  {
  }

  public abstract class providerID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  SYMapping.providerID>
  {
  }

  public abstract class providerObject : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SYMapping.providerObject>
  {
  }

  public abstract class syncType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SYMapping.syncType>
  {
  }

  public abstract class status : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SYMapping.status>
  {
  }

  public abstract class fieldCntr : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  SYMapping.fieldCntr>
  {
  }

  public abstract class fieldOrderCntr : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  SYMapping.fieldOrderCntr>
  {
  }

  public abstract class dataCntr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SYMapping.dataCntr>
  {
  }

  public abstract class importConditionCntr : 
    BqlType<
    #nullable enable
    IBqlShort, short>.Field<
    #nullable disable
    SYMapping.importConditionCntr>
  {
  }

  public abstract class conditionCntr : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  SYMapping.conditionCntr>
  {
  }

  public abstract class nbrRecords : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SYMapping.nbrRecords>
  {
  }

  public abstract class preparedOn : BqlType<
  #nullable enable
  IBqlDateTime, System.DateTime>.Field<
  #nullable disable
  SYMapping.preparedOn>
  {
  }

  public abstract class completedOn : BqlType<
  #nullable enable
  IBqlDateTime, System.DateTime>.Field<
  #nullable disable
  SYMapping.completedOn>
  {
  }

  public abstract class importTimeStamp : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SYMapping.importTimeStamp>
  {
  }

  public abstract class exportTimeStamp : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    SYMapping.exportTimeStamp>
  {
  }

  public abstract class exportTimeStampUtc : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    SYMapping.exportTimeStampUtc>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  SYMapping.noteID>
  {
  }

  public abstract class formatLocale : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SYMapping.formatLocale>
  {
  }

  public abstract class isExportOnlyMappingFields : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SYMapping.isExportOnlyMappingFields>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  SYMapping.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SYMapping.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    SYMapping.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  SYMapping.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SYMapping.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    SYMapping.lastModifiedDateTime>
  {
  }

  public abstract class tStamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  SYMapping.tStamp>
  {
  }

  public abstract class processInParallel : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SYMapping.processInParallel>
  {
  }

  public abstract class breakOnError : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SYMapping.breakOnError>
  {
  }

  public abstract class breakOnTarget : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SYMapping.breakOnTarget>
  {
  }

  public abstract class skipHeaders : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SYMapping.skipHeaders>
  {
  }

  public abstract class sitemapID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  SYMapping.sitemapID>
  {
  }

  public abstract class discardResult : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SYMapping.discardResult>
  {
  }

  /// <summary> A service field. It is used to hide "Executed By Events" tab on SM206025 page. </summary>
  public abstract class showCreatedByEventsTabExpr : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SYMapping.showCreatedByEventsTabExpr>
  {
  }
}
