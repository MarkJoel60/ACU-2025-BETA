// Decompiled with JetBrains decompiler
// Type: PX.SM.WikiRevision
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.SM;

[Serializable]
public class WikiRevision : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected Guid? _PageID;
  protected 
  #nullable disable
  string _Language;
  protected int? _PageRevisionID;
  protected string _Content;
  protected string _PlainText;
  protected Guid? _ApprovalByID;
  protected System.DateTime? _ApprovalDateTime;
  protected bool _Published;
  protected Guid? _CreatedByID;
  protected System.DateTime? _CreatedDateTime;
  protected bool? _Selected;
  protected bool? _SelectedDest;
  protected Guid? _UID;

  [PXDBGuid(false, IsKey = true)]
  public virtual Guid? PageID
  {
    get => this._PageID;
    set => this._PageID = value;
  }

  [PXDBString(IsKey = true)]
  [PXUIField(DisplayName = "Language", Visibility = PXUIVisibility.SelectorVisible)]
  public virtual string Language
  {
    get => this._Language;
    set => this._Language = value;
  }

  [PXDBInt(IsKey = true)]
  [PXDefault(0)]
  [PXUIField(DisplayName = "Version ID")]
  public virtual int? PageRevisionID
  {
    get => this._PageRevisionID;
    set => this._PageRevisionID = value;
  }

  [PXDBString(IsUnicode = true)]
  [PXUIField(DisplayName = "Content")]
  public virtual string Content
  {
    get => this._Content;
    set => this._Content = value;
  }

  [PXDBString(IsUnicode = true)]
  [PXUIField(DisplayName = "Plain Text")]
  public virtual string PlainText
  {
    get => this._PlainText;
    set => this._PlainText = value;
  }

  [PXDBGuid(false)]
  [PXSelector(typeof (Users.pKID), SubstituteKey = typeof (Users.username), DescriptionField = typeof (Users.displayName), CacheGlobal = true)]
  [PXUIField(DisplayName = "Approval By", Enabled = false)]
  public virtual Guid? ApprovalByID
  {
    get => this._ApprovalByID;
    set => this._ApprovalByID = value;
  }

  [PXDBDate(InputMask = "g", PreserveTime = true)]
  [PXUIField(DisplayName = "Approval Time")]
  public virtual System.DateTime? ApprovalDateTime
  {
    get => this._ApprovalDateTime;
    set => this._ApprovalDateTime = value;
  }

  [PXBool]
  [PXUIField(DisplayName = "Published")]
  public virtual bool Published
  {
    [PXDependsOnFields(new System.Type[] {typeof (WikiRevision.approvalByID)})] get
    {
      return this.ApprovalByID.HasValue;
    }
  }

  [PXDBCreatedByID]
  [PXUIField(DisplayName = "Created by", Visible = true, Enabled = false)]
  public virtual Guid? CreatedByID
  {
    get => this._CreatedByID;
    set => this._CreatedByID = value;
  }

  [PXDBCreatedDateTime(PreserveTime = true)]
  [PXUIField(DisplayName = "Creation Time", Visible = true, Enabled = false)]
  public virtual System.DateTime? CreatedDateTime
  {
    get => this._CreatedDateTime;
    set => this._CreatedDateTime = value;
  }

  [PXBool]
  [PXUIField(DisplayName = "Source version", Visibility = PXUIVisibility.Visible)]
  public virtual bool? Selected
  {
    get => this._Selected;
    set => this._Selected = value;
  }

  [PXBool]
  [PXUIField(DisplayName = "Compare To", Visibility = PXUIVisibility.Visible)]
  public virtual bool? SelectedDest
  {
    get => this._SelectedDest;
    set => this._SelectedDest = value;
  }

  [PXDBGuid(false)]
  public virtual Guid? UID
  {
    get => this._UID;
    set => this._UID = value;
  }

  public abstract class pageID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  WikiRevision.pageID>
  {
  }

  public abstract class language : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  WikiRevision.language>
  {
  }

  public abstract class pageRevisionID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  WikiRevision.pageRevisionID>
  {
  }

  public abstract class content : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  WikiRevision.content>
  {
  }

  public abstract class plainText : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  WikiRevision.plainText>
  {
  }

  public abstract class approvalByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  WikiRevision.approvalByID>
  {
  }

  public abstract class approvalDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    WikiRevision.approvalDateTime>
  {
  }

  public abstract class published : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  WikiRevision.published>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  WikiRevision.createdByID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    WikiRevision.createdDateTime>
  {
  }

  public abstract class selected : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  WikiRevision.selected>
  {
  }

  public abstract class selectedDest : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  WikiRevision.selectedDest>
  {
  }

  /// <summary>This is used for full-text indexing.</summary>
  public abstract class uID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  WikiRevision.uID>
  {
  }
}
