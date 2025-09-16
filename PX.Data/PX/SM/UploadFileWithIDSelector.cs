// Decompiled with JetBrains decompiler
// Type: PX.SM.UploadFileWithIDSelector
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.SM;

[PXPrimaryGraph(typeof (WikiFileMaintenance))]
[PXCacheName("File")]
[Serializable]
public class UploadFileWithIDSelector : UploadFile
{
  protected bool? _Selected = new bool?(false);
  private Guid? _SelectedWikiID;
  protected Guid? _SelectedPageID;
  protected short? _AccessRights;
  protected 
  #nullable disable
  string _ExternalLink;
  protected string _WikiLink;

  [PXBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Selected")]
  public bool? Selected
  {
    get => this._Selected;
    set => this._Selected = value;
  }

  [PXDBGuid(false, IsKey = true)]
  [PXUIField(DisplayName = "File", Enabled = false)]
  [PXSelector(typeof (UploadFile.fileID), SubstituteKey = typeof (UploadFile.name))]
  public override Guid? FileID
  {
    get => this._FileID;
    set => this._FileID = value;
  }

  [PXDBGuid(false)]
  public override Guid? PrimaryPageID
  {
    get => this._PrimaryPageID;
    set => this._PrimaryPageID = value;
  }

  [PXDBString]
  [PXSelector(typeof (SiteMap.screenID))]
  [PXUIField(DisplayName = "Primary Screen")]
  public override string PrimaryScreenID
  {
    get => this._PrimaryScreenID;
    set => this._PrimaryScreenID = value;
  }

  [PXGuid]
  [PXWikiSelector(SubstituteKey = typeof (WikiDescriptor.name))]
  [PXUIField(DisplayName = "Wiki")]
  public Guid? SelectedWikiID
  {
    get => this._SelectedWikiID;
    set => this._SelectedWikiID = value;
  }

  [PXGuid]
  [PXUIField(DisplayName = "Primary Page")]
  [PXSelector(typeof (WikiPage.pageID), SubstituteKey = typeof (WikiPage.name), DescriptionField = typeof (WikiPage.title))]
  public virtual Guid? SelectedPageID
  {
    get => this._SelectedPageID;
    set => this._SelectedPageID = value;
  }

  [PXShort]
  [PXDefault(0)]
  public virtual short? AccessRights
  {
    get => this._AccessRights;
    set => this._AccessRights = value;
  }

  [PXBool]
  internal bool? IsPublicChanged { get; set; }

  [PXString(InputMask = "", IsUnicode = true)]
  [PXUIField(DisplayName = "URL")]
  public virtual string ExternalLink
  {
    get => this._ExternalLink;
    set => this._ExternalLink = value;
  }

  [PXString(InputMask = "", IsUnicode = true)]
  [PXUIField(DisplayName = "URL for Wiki")]
  public virtual string WikiLink
  {
    get => this._WikiLink;
    set => this._WikiLink = value;
  }

  public abstract class selected : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  UploadFileWithIDSelector.selected>
  {
  }

  public new abstract class fileID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  UploadFileWithIDSelector.fileID>
  {
  }

  public new abstract class primaryPageID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    UploadFileWithIDSelector.primaryPageID>
  {
  }

  public new abstract class primaryScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    UploadFileWithIDSelector.primaryScreenID>
  {
  }

  public abstract class selectedWikiID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    UploadFileWithIDSelector.selectedWikiID>
  {
  }

  public abstract class selectedPageID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    UploadFileWithIDSelector.selectedPageID>
  {
  }

  public abstract class accessRights : 
    BqlType<
    #nullable enable
    IBqlShort, short>.Field<
    #nullable disable
    UploadFileWithIDSelector.accessRights>
  {
  }

  public abstract class isPublicChanged
  {
  }

  public abstract class externalLink : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    UploadFileWithIDSelector.externalLink>
  {
  }

  public abstract class wikiLink : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    UploadFileWithIDSelector.wikiLink>
  {
  }
}
