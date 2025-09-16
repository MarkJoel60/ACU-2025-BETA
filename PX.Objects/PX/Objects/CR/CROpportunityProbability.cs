// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.CROpportunityProbability
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.CR;

[PXCacheName("Opportunity Probability")]
[Serializable]
public class CROpportunityProbability : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected 
  #nullable disable
  string _CreatedByScreenID;
  protected Guid? _CreatedByID;
  protected DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected DateTime? _LastModifiedDateTime;
  protected byte[] _tstamp;

  [PXDBString(2, IsKey = true)]
  [PXDefault]
  [PXUIField(DisplayName = "Stage ID")]
  public virtual string StageCode { get; set; }

  [PXDBInt(MinValue = 0, MaxValue = 100)]
  [PXDefault(0)]
  [PXUIField(DisplayName = "Probability")]
  public virtual int? Probability { get; set; }

  [PXDBLocalizableString(50, IsUnicode = true)]
  [PXDefault]
  [PXUIField(DisplayName = "Name")]
  public virtual string Name { get; set; }

  [PXDBInt]
  [PXDefault(0)]
  [PXUIField(DisplayName = "Sort Order")]
  public virtual int? SortOrder { get; set; }

  [PXBool]
  [PXUIField(DisplayName = "Active")]
  [PXDefault(true)]
  public virtual bool? IsActive { get; set; }

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

  [PXNote]
  public virtual Guid? NoteID { get; set; }

  [PXDBTimestamp]
  public virtual byte[] tstamp
  {
    get => this._tstamp;
    set => this._tstamp = value;
  }

  public abstract class stageCode : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CROpportunityProbability.stageCode>
  {
  }

  public abstract class probability : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    CROpportunityProbability.probability>
  {
  }

  public abstract class name : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CROpportunityProbability.name>
  {
  }

  public abstract class sortOrder : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CROpportunityProbability.sortOrder>
  {
  }

  public abstract class isActive : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CROpportunityProbability.isActive>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CROpportunityProbability.createdByScreenID>
  {
  }

  public abstract class createdByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    CROpportunityProbability.createdByID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CROpportunityProbability.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    CROpportunityProbability.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CROpportunityProbability.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CROpportunityProbability.lastModifiedDateTime>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  CROpportunityProbability.noteID>
  {
  }

  public abstract class Tstamp : 
    BqlType<
    #nullable enable
    IBqlByteArray, byte[]>.Field<
    #nullable disable
    CROpportunityProbability.Tstamp>
  {
  }
}
