// Decompiled with JetBrains decompiler
// Type: PX.SM.FilesFilter
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
public class FilesFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected 
  #nullable disable
  string _DocName;
  protected System.DateTime? _DateCreatedFrom;
  protected System.DateTime? _DateCreatedTo;
  protected Guid? _AddedBy;
  protected string _ScreenID;
  protected Guid? _CheckedOutBy;

  [PXDBString(IsUnicode = true)]
  [PXUIField(DisplayName = "File Name Contains")]
  public virtual string DocName
  {
    get => this._DocName;
    set => this._DocName = value;
  }

  [PXDBDate]
  [PXUIField(DisplayName = "Added From")]
  public virtual System.DateTime? DateCreatedFrom
  {
    get => this._DateCreatedFrom;
    set => this._DateCreatedFrom = value;
  }

  [PXDBDate]
  [PXUIField(DisplayName = "To")]
  public virtual System.DateTime? DateCreatedTo
  {
    get => this._DateCreatedTo;
    set => this._DateCreatedTo = value;
  }

  [PXDBGuid(false)]
  [PXUIField(DisplayName = "Added By")]
  [PXSelector(typeof (Users.pKID), SubstituteKey = typeof (Users.username))]
  public Guid? AddedBy
  {
    get => this._AddedBy;
    set => this._AddedBy = value;
  }

  [PXDBString(8, IsFixed = true, InputMask = "CC.CC.CC.CC")]
  [PXUIField(DisplayName = "Screen ID", Visibility = PXUIVisibility.SelectorVisible)]
  [PXSiteMapNodeSelector(typeof (UploadFile), typeof (UploadFile.primaryScreenID), typeof (UploadFile.name))]
  public string ScreenID
  {
    get => this._ScreenID;
    set => this._ScreenID = value;
  }

  [PXBool]
  [PXUIField(DisplayName = "Show Unassigned Files")]
  [PXDefault(false)]
  public bool? ShowUnassignedFiles { get; set; }

  [PXDBGuid(false)]
  [PXUIField(DisplayName = "Checked Out By")]
  [PXSelector(typeof (Users.pKID), SubstituteKey = typeof (Users.username))]
  public virtual Guid? CheckedOutBy
  {
    get => this._CheckedOutBy;
    set => this._CheckedOutBy = value;
  }

  public abstract class docName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FilesFilter.docName>
  {
  }

  public abstract class dateCreatedFrom : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    FilesFilter.dateCreatedFrom>
  {
  }

  public abstract class dateCreatedTo : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    FilesFilter.dateCreatedTo>
  {
  }

  public abstract class addedBy : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  FilesFilter.addedBy>
  {
  }

  public abstract class screenID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FilesFilter.screenID>
  {
  }

  public abstract class showUnassignedFiles : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FilesFilter.showUnassignedFiles>
  {
  }

  public abstract class checkedOutBy : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  FilesFilter.checkedOutBy>
  {
  }
}
