// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.CRCaseReference
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.CR;

/// <exclude />
[PXCacheName("Case Reference")]
[Serializable]
public class CRCaseReference : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected 
  #nullable disable
  byte[] _tstamp;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected DateTime? _LastModifiedDateTime;

  [PXDBString(15, IsKey = true, InputMask = "CCCCCCCCCCCCCCC")]
  [PXDBDefault(typeof (CRCase.caseCD))]
  [PXUIField(Visible = false)]
  public virtual string ParentCaseCD { get; set; }

  [PXDBString(15, IsKey = true, InputMask = "CCCCCCCCCCCCCCC")]
  [PXDefault]
  [PXUIField(DisplayName = "Case ID")]
  [PXSelector(typeof (Search2<CRCase.caseCD, LeftJoin<BAccount, On<BAccount.bAccountID, Equal<CRCase.customerID>>>, Where<BAccount.bAccountID, IsNull, Or<Match<BAccount, Current<AccessInfo.userName>>>>, OrderBy<Desc<CRCase.caseCD>>>), DescriptionField = typeof (CRCase.subject))]
  public virtual string ChildCaseCD { get; set; }

  [PXDBString(1, IsFixed = true)]
  [PXUIField(DisplayName = "Relation Type", Required = true)]
  [CaseRelationType]
  [PXDefault("C")]
  public virtual string RelationType { get; set; }

  [PXDBTimestamp]
  public virtual byte[] tstamp
  {
    get => this._tstamp;
    set => this._tstamp = value;
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

  public abstract class parentCaseCD : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CRCaseReference.parentCaseCD>
  {
  }

  public abstract class childCaseCD : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CRCaseReference.childCaseCD>
  {
  }

  public abstract class relationType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CRCaseReference.relationType>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  CRCaseReference.Tstamp>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  CRCaseReference.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CRCaseReference.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CRCaseReference.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    CRCaseReference.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CRCaseReference.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CRCaseReference.lastModifiedDateTime>
  {
  }
}
