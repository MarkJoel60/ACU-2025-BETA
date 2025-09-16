// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.EPView
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.EP;

[PXCacheName("Activity View Status")]
[Serializable]
public class EPView : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected Guid? _NoteID;
  protected int? _ContactID;
  protected int? _Status;
  protected bool? _Read;
  protected Guid? _CreatedByID;
  protected 
  #nullable disable
  string _CreatedByScreenID;
  protected DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected DateTime? _LastModifiedDateTime;

  [PXDBGuid(false, IsKey = true)]
  public virtual Guid? NoteID
  {
    get => this._NoteID;
    set => this._NoteID = value;
  }

  [PXDBInt(IsKey = true)]
  public virtual int? ContactID
  {
    get => this._ContactID;
    set => this._ContactID = value;
  }

  [PXDBInt]
  [PXDefault(0)]
  [EPViewStatus]
  [PXUIField(DisplayName = "Status")]
  public virtual int? Status
  {
    get => this._Status;
    set => this._Status = value;
  }

  [PXBool]
  [PXDefault(false)]
  [PXDBCalced(typeof (Switch<Case<Where<EPView.status, Equal<EPViewStatusAttribute.Viewed>>, True>, False>), typeof (bool))]
  [PXUIField(DisplayName = "Read")]
  public virtual bool? Read
  {
    get => this._Read;
    set => this._Read = value;
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
  public virtual DateTime? CreatedDateTime
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
  public virtual DateTime? LastModifiedDateTime
  {
    get => this._LastModifiedDateTime;
    set => this._LastModifiedDateTime = value;
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  EPView.noteID>
  {
  }

  public abstract class contactID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPView.contactID>
  {
  }

  public abstract class status : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPView.status>
  {
  }

  public abstract class read : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  EPView.read>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  EPView.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    EPView.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    EPView.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  EPView.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    EPView.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    EPView.lastModifiedDateTime>
  {
  }
}
