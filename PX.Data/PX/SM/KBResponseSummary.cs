// Decompiled with JetBrains decompiler
// Type: PX.SM.KBResponseSummary
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
public class KBResponseSummary : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
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

  [PXDBGuid(false, IsKey = true)]
  [PXUIField(DisplayName = "Article", Enabled = false)]
  [PXSelector(typeof (Search<WikiPage.pageID>), DescriptionField = typeof (WikiPage.name))]
  public virtual Guid? PageID { get; set; }

  [PXDefault(0)]
  [PXDBInt]
  [PXUIField(DisplayName = "Mark Count", Required = true)]
  public virtual int? Markcount { get; set; }

  [PXDefault(0)]
  [PXDBInt]
  [PXUIField(DisplayName = "Mark Summary", Required = true)]
  public virtual int? Marksummary { get; set; }

  [PXDefault(0)]
  [PXDBInt]
  [PXUIField(DisplayName = "Views", Required = true)]
  public virtual int? Views { get; set; }

  [PXDBDouble]
  [PXUIField(DisplayName = "Average Rate", Required = true)]
  public virtual double? AvRate { get; set; }

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

  public abstract class pageID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  KBResponseSummary.pageID>
  {
  }

  public abstract class markcount : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  KBResponseSummary.markcount>
  {
  }

  public abstract class marksummary : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  KBResponseSummary.marksummary>
  {
  }

  public abstract class views : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  KBResponseSummary.views>
  {
  }

  public abstract class avRate : BqlType<
  #nullable enable
  IBqlDouble, double>.Field<
  #nullable disable
  KBResponseSummary.avRate>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    KBResponseSummary.createdByScreenID>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  KBResponseSummary.createdByID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    KBResponseSummary.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    KBResponseSummary.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    KBResponseSummary.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    KBResponseSummary.lastModifiedDateTime>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  KBResponseSummary.Tstamp>
  {
  }
}
