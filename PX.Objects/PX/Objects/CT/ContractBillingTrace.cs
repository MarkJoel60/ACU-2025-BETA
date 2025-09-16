// Decompiled with JetBrains decompiler
// Type: PX.Objects.CT.ContractBillingTrace
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using System;

#nullable enable
namespace PX.Objects.CT;

[PXCacheName("Contract Billing Trace")]
[Serializable]
public class ContractBillingTrace : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected int? _ContractID;
  protected int? _RecordID;
  protected 
  #nullable disable
  string _DocType;
  protected string _RefNbr;
  protected byte[] _tstamp;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected DateTime? _LastModifiedDateTime;

  [PXDBInt(IsKey = true)]
  [PXDefault]
  public virtual int? ContractID
  {
    get => this._ContractID;
    set => this._ContractID = value;
  }

  [PXDBIdentity(IsKey = true)]
  public virtual int? RecordID
  {
    get => this._RecordID;
    set => this._RecordID = value;
  }

  [PXDBString(3, IsKey = true, IsFixed = true)]
  [PXDefault]
  public virtual string DocType
  {
    get => this._DocType;
    set => this._DocType = value;
  }

  [PXDBString(15, IsUnicode = true, IsKey = true, InputMask = "")]
  [PXDefault]
  public virtual string RefNbr
  {
    get => this._RefNbr;
    set => this._RefNbr = value;
  }

  [PXDBDate]
  public virtual DateTime? NextDate { get; set; }

  [PXDBDate]
  public virtual DateTime? LastDate { get; set; }

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

  public class PK : 
    PrimaryKeyOf<ContractBillingTrace>.By<ContractBillingTrace.contractID, ContractBillingTrace.recordID>
  {
    public static ContractBillingTrace Find(
      PXGraph graph,
      int? contractID,
      int? recordID,
      PKFindOptions options = 0)
    {
      return (ContractBillingTrace) PrimaryKeyOf<ContractBillingTrace>.By<ContractBillingTrace.contractID, ContractBillingTrace.recordID>.FindBy(graph, (object) contractID, (object) recordID, options);
    }
  }

  public static class FK
  {
    public class Contract : 
      PrimaryKeyOf<Contract>.By<Contract.contractID>.ForeignKeyOf<ContractBillingTrace>.By<ContractBillingTrace.contractID>
    {
    }
  }

  public abstract class contractID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ContractBillingTrace.contractID>
  {
  }

  public abstract class recordID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ContractBillingTrace.recordID>
  {
  }

  public abstract class docType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ContractBillingTrace.docType>
  {
  }

  public abstract class refNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ContractBillingTrace.refNbr>
  {
  }

  public abstract class nextDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    ContractBillingTrace.nextDate>
  {
  }

  public abstract class lastDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    ContractBillingTrace.lastDate>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  ContractBillingTrace.Tstamp>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  ContractBillingTrace.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ContractBillingTrace.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    ContractBillingTrace.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    ContractBillingTrace.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ContractBillingTrace.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    ContractBillingTrace.lastModifiedDateTime>
  {
  }
}
