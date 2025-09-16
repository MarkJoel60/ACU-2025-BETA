// Decompiled with JetBrains decompiler
// Type: PX.SM.PreferencesGeneral
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.SM;

[PXPrimaryGraph(typeof (PreferencesGeneralMaint))]
[PXCacheName("General Preferences")]
public class PreferencesGeneral : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected int? _MapViewer;
  protected bool? _GridActionsText;
  protected int? _MaxUploadSize;
  protected 
  #nullable disable
  string _HeaderFont;
  protected short? _HeaderFontSize;
  protected string _HeaderFontColor;
  protected string _HeaderFillColor;
  protected string _HeaderFontType;
  protected string _BodyFont;
  protected short? _BodyFontSize;
  protected string _BodyFontColor;
  protected string _BodyFillColor;
  protected string _BodyFontType;
  protected bool? _Border;
  protected string _BorderColor;
  protected bool? _HiddenSkip;
  protected string _PersonNameFormat;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected System.DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected System.DateTime? _LastModifiedDateTime;

  [PXDBInt]
  [PXUIField(DisplayName = "Map Viewer")]
  [PXIntList(new int[] {0}, new string[] {"Google"})]
  public virtual int? MapViewer
  {
    get => this._MapViewer;
    set => this._MapViewer = value;
  }

  [PXDBBool]
  [PXUIField(DisplayName = "Show Tooltips for Table Toolbar Buttons")]
  public virtual bool? GridActionsText
  {
    get => this._GridActionsText;
    set => this._GridActionsText = value;
  }

  [PXDBInt]
  [PXDefault(7)]
  [PXIntList(new int[] {6, 7}, new string[] {"Contains", "Starts With"})]
  [PXUIField(DisplayName = "Search Condition")]
  public virtual int? GridFastFilterCondition { get; set; }

  [PXDBInt]
  [PXDefault(100)]
  [PXUIField(DisplayName = "Max. Length of Search String")]
  public virtual int? GridFastFilterMaxLength { get; set; }

  [PXDBString(32 /*0x20*/)]
  [PXUIField(DisplayName = "Login Time Zone")]
  [PXTimeZone(true)]
  public virtual string TimeZone { get; set; }

  [PXDBInt]
  [PXDefault(4096 /*0x1000*/)]
  [PXUIField(DisplayName = "Maximum File Upload Size in KB")]
  public virtual int? MaxUploadSize
  {
    get => this._MaxUploadSize;
    set => this._MaxUploadSize = value;
  }

  [PXDBGuid(false)]
  [PXUIField(DisplayName = "Template for External Links")]
  [PXSelector(typeof (WikiPage.pageID), DescriptionField = typeof (WikiPage.title))]
  public virtual Guid? GetLinkTemplate { get; set; }

  [PXDBString(30)]
  [PXUIField(DisplayName = "Font")]
  [PXFontList]
  public virtual string HeaderFont
  {
    get => this._HeaderFont;
    set => this._HeaderFont = value;
  }

  [PXDBShort]
  [PXUIField(DisplayName = "Size")]
  [PXFontSizeList]
  public virtual short? HeaderFontSize
  {
    get => this._HeaderFontSize;
    set => this._HeaderFontSize = value;
  }

  [PXDBString(30)]
  [PXUIField(DisplayName = "Font Color")]
  [PXColorList]
  public virtual string HeaderFontColor
  {
    get => this._HeaderFontColor;
    set => this._HeaderFontColor = value;
  }

  [PXDBString(30)]
  [PXUIField(DisplayName = "Fill Color")]
  [PXColorList]
  public virtual string HeaderFillColor
  {
    get => this._HeaderFillColor;
    set => this._HeaderFillColor = value;
  }

  [PXDBString(1, IsFixed = true)]
  [PXUIField(DisplayName = "Style")]
  [PXStringList(new string[] {"0", "1", "2", "3"}, new string[] {"normal", "bold", "italic", "bold & italic"})]
  public virtual string HeaderFontType
  {
    get => this._HeaderFontType;
    set => this._HeaderFontType = value;
  }

  [PXDBString(30)]
  [PXUIField(DisplayName = "Font")]
  [PXFontList]
  public virtual string BodyFont
  {
    get => this._BodyFont;
    set => this._BodyFont = value;
  }

  [PXDBShort]
  [PXUIField(DisplayName = "Size")]
  [PXFontSizeList]
  public virtual short? BodyFontSize
  {
    get => this._BodyFontSize;
    set => this._BodyFontSize = value;
  }

  [PXDBString(30)]
  [PXUIField(DisplayName = "Font Color")]
  [PXColorList]
  public virtual string BodyFontColor
  {
    get => this._BodyFontColor;
    set => this._BodyFontColor = value;
  }

  [PXDBString(30)]
  [PXUIField(DisplayName = "Fill Color")]
  [PXColorList]
  public virtual string BodyFillColor
  {
    get => this._BodyFillColor;
    set => this._BodyFillColor = value;
  }

  [PXDBString(1, IsFixed = true)]
  [PXUIField(DisplayName = "Style")]
  [PXStringList(new string[] {"0", "1", "2", "3"}, new string[] {"normal", "bold", "italic", "bold & italic"})]
  public virtual string BodyFontType
  {
    get => this._BodyFontType;
    set => this._BodyFontType = value;
  }

  [PXDBBool]
  [PXUIField(DisplayName = "Draw Border")]
  public virtual bool? Border
  {
    get => this._Border;
    set => this._Border = value;
  }

  [PXDBString(30)]
  [PXUIField(DisplayName = "Border Color")]
  [PXColorList]
  public virtual string BorderColor
  {
    get => this._BorderColor;
    set => this._BorderColor = value;
  }

  [PXDBBool]
  [PXUIField(DisplayName = "Skip Hidden Fields")]
  public virtual bool? HiddenSkip
  {
    get => this._HiddenSkip;
    set => this._HiddenSkip = value;
  }

  [PXDBString(50)]
  [PXUIField(DisplayName = "Editor Font")]
  [PXDefault("Arial")]
  [PXFontList]
  public virtual string EditorFontName { get; set; }

  [PXDBInt]
  [PXDefault(2)]
  [PXFontSizeStrList]
  public virtual int? EditorFontSize { get; set; }

  [PXDBBool]
  [PXUIField(DisplayName = "Spell Check")]
  public virtual bool? SpellCheck { get; set; }

  [PXNote]
  [PXUIField(Visible = false, Visibility = PXUIVisibility.Invisible)]
  public virtual Guid? NoteID { get; set; }

  [PXDBString(IsUnicode = true, InputMask = "")]
  [PXStringList(new string[] {null}, new string[] {""})]
  [PXDefault("Default")]
  [PXUIField(DisplayName = "Interface Theme")]
  public string Theme { get; set; }

  [PXString(30, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Primary Color")]
  [PXGeneralThemeVariable(typeof (PreferencesGeneral.theme), "--primary-color")]
  [PXColorList]
  public string PrimaryColor { get; set; }

  [PXString(30, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Background Color")]
  [PXGeneralThemeVariable(typeof (PreferencesGeneral.theme), "--background-color")]
  [PXColorList]
  public string BackgroundColor { get; set; }

  [PXDBGuid(false)]
  [PXUIField(DisplayName = "Home Page")]
  public virtual Guid? HomePage { get; set; }

  [PXDBGuid(false)]
  [PXUIField(DisplayName = "Help On Help")]
  [PXSelector(typeof (WikiPage.pageID), DescriptionField = typeof (WikiPage.title))]
  public virtual Guid? HelpPage { get; set; }

  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Use Online Help System")]
  public virtual bool? UseMLSearch { get; set; }

  [PXDBInt]
  [PXDefault(0)]
  [PreferencesGeneral.MLEventsMode.List]
  public virtual int? DeletingMLEventsMode { get; set; }

  [PXDBInt]
  [PXDefault(90)]
  [PXUIField(DisplayName = "")]
  public virtual int? MLEventsRetentionAge { get; set; }

  [PXDBGuid(false)]
  [PXUIField(DisplayName = "Home Page")]
  public virtual Guid? PortalHomePage { get; set; }

  [PXDBString]
  [PXUIField(DisplayName = "Portal External Access Link")]
  public virtual string PortalExternalAccessLink { get; set; }

  [PXDBString]
  [PXUIField(DisplayName = "Display Name Order")]
  [PXDefault("WESTERN")]
  [PersonNameFormats]
  public virtual string PersonNameFormat
  {
    get => this._PersonNameFormat;
    set => this._PersonNameFormat = value;
  }

  [PXBool]
  [PXDefault(false, PersistingCheck = PXPersistingCheck.Nothing)]
  public virtual bool? NeedUpdatePersonNames { get; set; }

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

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Apply Body Settings to Empty Cells")]
  public virtual bool? ApplyToEmptyCells { get; set; }

  [PXDBString(15, IsUnicode = true)]
  [PXUIField(DisplayName = "Address Lookup Plug-In", Visible = false, Enabled = false)]
  public virtual string AddressLookupPluginID { get; set; }

  [PXDBString(1, IsUnicode = false)]
  [PXStringList(new string[] {"T", "E"}, new string[] {"Modern", "Classic"})]
  [PXUIField(DisplayName = "Default UI")]
  public virtual string DefaultUI { get; set; }

  public abstract class mapViewer : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PreferencesGeneral.mapViewer>
  {
  }

  public abstract class gridActionsText : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    PreferencesGeneral.gridActionsText>
  {
  }

  public abstract class gridFastFilterCondition : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    PreferencesGeneral.gridFastFilterCondition>
  {
  }

  public abstract class gridFastFilterMaxLength : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    PreferencesGeneral.gridFastFilterMaxLength>
  {
  }

  public abstract class timeZone : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PreferencesGeneral.timeZone>
  {
  }

  public abstract class maxUploadSize : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PreferencesGeneral.maxUploadSize>
  {
  }

  public abstract class getLinkTemplate : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    PreferencesGeneral.getLinkTemplate>
  {
  }

  public abstract class headerFont : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PreferencesGeneral.headerFont>
  {
  }

  public abstract class headerFontSize : 
    BqlType<
    #nullable enable
    IBqlShort, short>.Field<
    #nullable disable
    PreferencesGeneral.headerFontSize>
  {
  }

  public abstract class headerFontColor : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PreferencesGeneral.headerFontColor>
  {
  }

  public abstract class headerFillColor : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PreferencesGeneral.headerFillColor>
  {
  }

  public abstract class headerFontType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PreferencesGeneral.headerFontType>
  {
  }

  public abstract class bodyFont : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PreferencesGeneral.bodyFont>
  {
  }

  public abstract class bodyFontSize : 
    BqlType<
    #nullable enable
    IBqlShort, short>.Field<
    #nullable disable
    PreferencesGeneral.bodyFontSize>
  {
  }

  public abstract class bodyFontColor : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PreferencesGeneral.bodyFontColor>
  {
  }

  public abstract class bodyFillColor : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PreferencesGeneral.bodyFillColor>
  {
  }

  public abstract class bodyFontType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PreferencesGeneral.bodyFontType>
  {
  }

  public abstract class border : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  PreferencesGeneral.border>
  {
  }

  public abstract class borderColor : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PreferencesGeneral.borderColor>
  {
  }

  public abstract class hiddenSkip : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  PreferencesGeneral.hiddenSkip>
  {
  }

  public abstract class editorFontName : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PreferencesGeneral.editorFontName>
  {
  }

  public abstract class editorFontSize : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    PreferencesGeneral.editorFontSize>
  {
  }

  public abstract class spellCheck : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  PreferencesGeneral.spellCheck>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  PreferencesGeneral.noteID>
  {
  }

  public abstract class theme : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PreferencesGeneral.theme>
  {
  }

  public abstract class primaryColor : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PreferencesGeneral.primaryColor>
  {
  }

  public abstract class backgroundColor : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PreferencesGeneral.backgroundColor>
  {
  }

  public abstract class homePage : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  PreferencesGeneral.homePage>
  {
  }

  public abstract class helpPage : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  PreferencesGeneral.helpPage>
  {
  }

  public abstract class useMLSearch : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  PreferencesGeneral.useMLSearch>
  {
  }

  public class MLEventsMode
  {
    public const int DeleteAfterDays = 0;
    public const int NeverDelete = 1;

    public class ListAttribute : PXIntListAttribute
    {
      public ListAttribute()
        : base(PXIntListAttribute.Pair(0, "Number of Days to Keep Menu Usage History:"), PXIntListAttribute.Pair(1, "Never Delete Menu Usage History"))
      {
      }
    }

    public class deleteAfterDays : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Constant<
      #nullable disable
      PreferencesGeneral.MLEventsMode.deleteAfterDays>
    {
      public deleteAfterDays()
        : base(0)
      {
      }
    }

    public class neverDelete : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Constant<
      #nullable disable
      PreferencesGeneral.MLEventsMode.neverDelete>
    {
      public neverDelete()
        : base(1)
      {
      }
    }
  }

  public abstract class deletingMLEventsMode : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    PreferencesGeneral.deletingMLEventsMode>
  {
  }

  public abstract class mLEventsRetentionAge : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    PreferencesGeneral.mLEventsRetentionAge>
  {
  }

  public abstract class portalHomePage : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    PreferencesGeneral.portalHomePage>
  {
  }

  public abstract class portalExternalAccessLink : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PreferencesGeneral.portalExternalAccessLink>
  {
  }

  public abstract class personNameFormat : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PreferencesGeneral.personNameFormat>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  PreferencesGeneral.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PreferencesGeneral.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    PreferencesGeneral.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    PreferencesGeneral.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PreferencesGeneral.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    PreferencesGeneral.lastModifiedDateTime>
  {
  }

  public abstract class applyToEmptyCells : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    PreferencesGeneral.applyToEmptyCells>
  {
  }

  public abstract class addressLookupPluginID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PreferencesGeneral.addressLookupPluginID>
  {
  }

  public abstract class defaultUI : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PreferencesGeneral.defaultUI>
  {
  }
}
