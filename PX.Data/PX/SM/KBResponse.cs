// Decompiled with JetBrains decompiler
// Type: PX.SM.KBResponse
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
public class KBResponse : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected 
  #nullable disable
  string _CreatedByScreenID;
  protected Guid? _CreatedByID;
  protected System.DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected System.DateTime? _LastModifiedDateTime;
  protected byte[] _tstamp;

  [PXDBIdentity(IsKey = true)]
  [PXUIField(Visible = false)]
  public virtual int? ResponseID { get; set; }

  [PXDBGuid(false)]
  [PXUIField(DisplayName = "Article", Enabled = false)]
  [PXSelector(typeof (Search<WikiPage.pageID>), DescriptionField = typeof (WikiPage.name))]
  public virtual Guid? PageID { get; set; }

  [PXDBGuid(false)]
  [PXUIField(DisplayName = "User ID", Enabled = false)]
  public virtual Guid? UserID { get; set; }

  [PXDBInt]
  [PXDefault(1)]
  [PXUIField(DisplayName = "Revision", Enabled = false)]
  public virtual int? RevisionID { get; set; }

  [PXDBDate(PreserveTime = true)]
  [PXUIField(DisplayName = "Date", Enabled = false)]
  [CRNowDefault]
  public virtual System.DateTime? Date { get; set; }

  [PXDBString(IsUnicode = true)]
  [PXUIField(DisplayName = "Summary")]
  public virtual string Summary { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "Mark")]
  public virtual int? NewMark { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "Mark")]
  public virtual int? OldMark { get; set; }

  [PXDBCreatedByScreenID]
  public virtual string CreatedByScreenID
  {
    get => this._CreatedByScreenID;
    set => this._CreatedByScreenID = value;
  }

  [PXDBCreatedByID]
  public virtual Guid? CreatedByID
  {
    get => this._CreatedByID;
    set => this._CreatedByID = value;
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
  public virtual byte[] tstamp
  {
    get => this._tstamp;
    set => this._tstamp = value;
  }

  public abstract class responseID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  KBResponse.responseID>
  {
  }

  public abstract class pageID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  KBResponse.pageID>
  {
  }

  public abstract class userID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  KBResponse.userID>
  {
  }

  public abstract class revisionID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  KBResponse.revisionID>
  {
  }

  public abstract class date : BqlType<
  #nullable enable
  IBqlDateTime, System.DateTime>.Field<
  #nullable disable
  KBResponse.date>
  {
  }

  public abstract class summary : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  KBResponse.summary>
  {
  }

  public abstract class newMark : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  KBResponse.newMark>
  {
  }

  public abstract class oldMark : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  KBResponse.oldMark>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    KBResponse.createdByScreenID>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  KBResponse.createdByID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    KBResponse.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  KBResponse.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    KBResponse.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    KBResponse.lastModifiedDateTime>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  KBResponse.Tstamp>
  {
  }
}
