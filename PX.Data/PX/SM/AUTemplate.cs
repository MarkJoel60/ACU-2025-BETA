// Decompiled with JetBrains decompiler
// Type: PX.SM.AUTemplate
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.SM;

/// <exclude />
[Serializable]
public class AUTemplate : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected 
  #nullable disable
  string _ScreenID;
  protected int? _TemplateID;
  protected string _Description;
  protected Guid? _NoteID;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected System.DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected System.DateTime? _LastModifiedDateTime;
  protected byte[] _TStamp;

  [PXDBString(8, IsKey = false, IsFixed = true, InputMask = "CC.CC.CC.CC")]
  [PXDefault]
  [PXUIField(DisplayName = "Screen ID", Enabled = false, Visibility = PXUIVisibility.SelectorVisible)]
  public virtual string ScreenID
  {
    get => this._ScreenID;
    set => this._ScreenID = value;
  }

  [PXString(IsKey = false, InputMask = "")]
  [PXUIField(DisplayName = "Graph")]
  public string Graph
  {
    [PXDependsOnFields(new System.Type[] {typeof (AUTemplate.screenID)})] get
    {
      if (Str.IsNullOrEmpty(this.ScreenID))
        return "";
      PXSiteMapNode mapNodeByScreenId = PXSiteMap.Provider.FindSiteMapNodeByScreenID(this.ScreenID);
      return mapNodeByScreenId != null ? mapNodeByScreenId.GraphType : "";
    }
  }

  [PXDBIdentity(IsKey = true)]
  [PXUIField(DisplayName = "Template ID")]
  [PXSelector(typeof (AUTemplate.templateID), DescriptionField = typeof (AUTemplate.description))]
  public virtual int? TemplateID
  {
    get => this._TemplateID;
    set => this._TemplateID = value;
  }

  [PXDBString(128 /*0x80*/, IsUnicode = true)]
  [PXDefault]
  [PXUIField(DisplayName = "Description", Visibility = PXUIVisibility.SelectorVisible)]
  public virtual string Description
  {
    get => this._Description;
    set => this._Description = value;
  }

  [PXNote]
  public virtual Guid? NoteID
  {
    get => this._NoteID;
    set => this._NoteID = value;
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

  [PXDBTimestamp]
  public virtual byte[] TStamp
  {
    get => this._TStamp;
    set => this._TStamp = value;
  }

  /// <exclude />
  public abstract class screenID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AUTemplate.screenID>
  {
  }

  /// <exclude />
  public abstract class graph : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AUTemplate.graph>
  {
  }

  /// <exclude />
  public abstract class templateID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  AUTemplate.templateID>
  {
  }

  /// <exclude />
  public abstract class description : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AUTemplate.description>
  {
  }

  /// <exclude />
  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  AUTemplate.noteID>
  {
  }

  /// <exclude />
  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  AUTemplate.createdByID>
  {
  }

  /// <exclude />
  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AUTemplate.createdByScreenID>
  {
  }

  /// <exclude />
  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    AUTemplate.createdDateTime>
  {
  }

  /// <exclude />
  public abstract class lastModifiedByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  AUTemplate.lastModifiedByID>
  {
  }

  /// <exclude />
  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AUTemplate.lastModifiedByScreenID>
  {
  }

  /// <exclude />
  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    AUTemplate.lastModifiedDateTime>
  {
  }

  /// <exclude />
  public abstract class tStamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  AUTemplate.tStamp>
  {
  }
}
