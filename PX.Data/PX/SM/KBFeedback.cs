// Decompiled with JetBrains decompiler
// Type: PX.SM.KBFeedback
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.SM;

[PXPrimaryGraph(typeof (KBFeedbackMaint))]
[Serializable]
public class KBFeedback : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
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
  [PXUIField]
  public virtual int? FeedbackID { get; set; }

  [PXDBString]
  [FeedBackIsFind]
  [PXUIField(DisplayName = "A user found what he or she had been looking for")]
  public virtual string IsFind { get; set; }

  [PXDBString]
  [FeedBackSatisfaction]
  [PXUIField(DisplayName = "Overall satisfaction with Self-Service Portal")]
  public virtual string Satisfaction { get; set; }

  [PXDBString]
  [PXUIField(DisplayName = "Other user comments and suggestions")]
  public virtual string Summary { get; set; }

  [PXDBGuid(false)]
  [PXUIField(DisplayName = "User ID", Enabled = false)]
  public virtual Guid? UserID { get; set; }

  [PXGuid]
  [PXUIField(DisplayName = "Page ID", Enabled = false)]
  public virtual Guid? PageID { get; set; }

  [PXDBDate(PreserveTime = true)]
  [PXUIField(DisplayName = "Date", Enabled = false)]
  [CRNowDefault]
  public virtual System.DateTime? Date { get; set; }

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

  public abstract class feedbackID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  KBFeedback.feedbackID>
  {
  }

  public abstract class isFind : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  KBFeedback.isFind>
  {
  }

  public abstract class satisfaction : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  KBFeedback.satisfaction>
  {
  }

  public abstract class summary : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  KBFeedback.summary>
  {
  }

  public abstract class userID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  KBFeedback.userID>
  {
  }

  public abstract class pageID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  KBFeedback.pageID>
  {
  }

  public abstract class date : BqlType<
  #nullable enable
  IBqlDateTime, System.DateTime>.Field<
  #nullable disable
  KBFeedback.date>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    KBFeedback.createdByScreenID>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  KBFeedback.createdByID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    KBFeedback.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  KBFeedback.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    KBFeedback.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    KBFeedback.lastModifiedDateTime>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  KBFeedback.Tstamp>
  {
  }
}
