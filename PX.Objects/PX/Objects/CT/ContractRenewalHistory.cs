// Decompiled with JetBrains decompiler
// Type: PX.Objects.CT.ContractRenewalHistory
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.AR;
using System;

#nullable enable
namespace PX.Objects.CT;

[PXCacheName("Contract Renewal History")]
[Serializable]
public class ContractRenewalHistory : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected int? _ContractID;
  protected DateTime? _RenewalDate;
  protected 
  #nullable disable
  string _Status;
  protected int? _ChildContractID;
  protected string _DiscountID;
  protected byte[] _tstamp;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected DateTime? _LastModifiedDateTime;

  [PXParent(typeof (Select<Contract, Where<Contract.contractID, Equal<Current<ContractRenewalHistory.contractID>>>>))]
  [PXDBDefault(typeof (Contract.contractID))]
  [PXDBInt(IsKey = true)]
  public virtual int? ContractID
  {
    get => this._ContractID;
    set => this._ContractID = value;
  }

  [PXDBInt(MinValue = 1, IsKey = true)]
  [PXDefault(typeof (Contract.revID))]
  public virtual int? RevID { get; set; }

  [PXDBDate]
  [PXDefault]
  [PXUIField(DisplayName = "Renewal Date")]
  public virtual DateTime? RenewalDate
  {
    get => this._RenewalDate;
    set => this._RenewalDate = value;
  }

  [PXDBDate]
  [PXDefault(typeof (AccessInfo.businessDate))]
  public virtual DateTime? ActionBusinessDate { get; set; }

  [PXDBString(1, IsFixed = true)]
  [Contract.status.List]
  [PXDefault("D")]
  [PXUIField]
  public virtual string Status
  {
    get => this._Status;
    set => this._Status = value;
  }

  [PXDBString(1, IsFixed = true)]
  [ContractAction.List]
  [PXDefault("N")]
  [PXUIField(DisplayName = "Action")]
  public virtual string Action { get; set; }

  [PXDBInt]
  [PXSelector(typeof (Contract.contractID), SubstituteKey = typeof (Contract.contractCD))]
  [PXUIField(DisplayName = "Related Contract")]
  public virtual int? ChildContractID
  {
    get => this._ChildContractID;
    set => this._ChildContractID = value;
  }

  [PXDBDate]
  public virtual DateTime? ExpireDate { get; set; }

  [PXDBDate]
  public virtual DateTime? EffectiveFrom { get; set; }

  [PXDBDate]
  public virtual DateTime? ActivationDate { get; set; }

  [PXDBDate]
  public virtual DateTime? StartDate { get; set; }

  [PXDBDate]
  public virtual DateTime? NextDate { get; set; }

  [PXDBDate]
  public virtual DateTime? LastDate { get; set; }

  [PXDBDate]
  public virtual DateTime? StartBilling { get; set; }

  [PXDBDate]
  public virtual DateTime? TerminationDate { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? IsActive { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? IsCompleted { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? IsCancelled { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? IsPendingUpdate { get; set; }

  [PXDate]
  [PXUIField(DisplayName = "Date")]
  [PXFormula(typeof (Switch<Case<Where<ContractRenewalHistory.action, Equal<ContractAction.setup>>, ContractRenewalHistory.startDate, Case<Where<ContractRenewalHistory.action, Equal<ContractAction.activate>, Or<ContractRenewalHistory.action, Equal<ContractAction.setupAndActivate>>>, Switch<Case<Where<ContractRenewalHistory.effectiveFrom, Greater<ContractRenewalHistory.activationDate>>, ContractRenewalHistory.effectiveFrom>, ContractRenewalHistory.activationDate>, Case<Where<ContractRenewalHistory.action, Equal<ContractAction.bill>>, ContractRenewalHistory.lastDate, Case<Where<ContractRenewalHistory.action, Equal<ContractAction.renew>, Or<ContractRenewalHistory.action, Equal<ContractAction.create>>>, ContractRenewalHistory.renewalDate, Case<Where<ContractRenewalHistory.action, Equal<ContractAction.terminate>>, ContractRenewalHistory.terminationDate, Case<Where<ContractRenewalHistory.action, Equal<ContractAction.upgrade>>, ContractRenewalHistory.actionBusinessDate>>>>>>>))]
  public virtual DateTime? Date { get; set; }

  [PXDBString(10, IsUnicode = true)]
  public virtual string DiscountID
  {
    get => this._DiscountID;
    set => this._DiscountID = value;
  }

  [PXDBTimestamp]
  public virtual byte[] tstamp
  {
    get => this._tstamp;
    set => this._tstamp = value;
  }

  [PXDBCreatedByID(DisplayName = "User")]
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

  [PXUIField(DisplayName = "Modified Time")]
  [PXDBLastModifiedDateTime]
  public virtual DateTime? LastModifiedDateTime
  {
    get => this._LastModifiedDateTime;
    set => this._LastModifiedDateTime = value;
  }

  public class PK : 
    PrimaryKeyOf<ContractRenewalHistory>.By<ContractRenewalHistory.contractID, ContractRenewalHistory.revID>
  {
    public static ContractRenewalHistory Find(
      PXGraph graph,
      int? contractID,
      int? revID,
      PKFindOptions options = 0)
    {
      return (ContractRenewalHistory) PrimaryKeyOf<ContractRenewalHistory>.By<ContractRenewalHistory.contractID, ContractRenewalHistory.revID>.FindBy(graph, (object) contractID, (object) revID, options);
    }
  }

  public static class FK
  {
    public class Contract : 
      PrimaryKeyOf<Contract>.By<Contract.contractID>.ForeignKeyOf<ContractRenewalHistory>.By<ContractRenewalHistory.contractID>
    {
    }

    public class Discount : 
      PrimaryKeyOf<ARDiscount>.By<ARDiscount.discountID>.ForeignKeyOf<ContractRenewalHistory>.By<ContractRenewalHistory.discountID>
    {
    }
  }

  public abstract class contractID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ContractRenewalHistory.contractID>
  {
  }

  public abstract class revID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ContractRenewalHistory.revID>
  {
  }

  public abstract class renewalDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    ContractRenewalHistory.renewalDate>
  {
  }

  public abstract class actionBusinessDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    ContractRenewalHistory.actionBusinessDate>
  {
  }

  public abstract class status : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ContractRenewalHistory.status>
  {
  }

  public abstract class action : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ContractRenewalHistory.action>
  {
  }

  public abstract class childContractID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    ContractRenewalHistory.childContractID>
  {
  }

  public abstract class expireDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    ContractRenewalHistory.expireDate>
  {
  }

  public abstract class effectiveFrom : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    ContractRenewalHistory.effectiveFrom>
  {
  }

  public abstract class activationDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    ContractRenewalHistory.activationDate>
  {
  }

  public abstract class startDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    ContractRenewalHistory.startDate>
  {
  }

  public abstract class nextDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    ContractRenewalHistory.nextDate>
  {
  }

  public abstract class lastDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    ContractRenewalHistory.lastDate>
  {
  }

  public abstract class startBilling : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    ContractRenewalHistory.startBilling>
  {
  }

  public abstract class terminationDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    ContractRenewalHistory.terminationDate>
  {
  }

  public abstract class isActive : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ContractRenewalHistory.isActive>
  {
  }

  public abstract class isCompleted : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    ContractRenewalHistory.isCompleted>
  {
  }

  public abstract class isCancelled : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    ContractRenewalHistory.isCancelled>
  {
  }

  public abstract class isPendingUpdate : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    ContractRenewalHistory.isPendingUpdate>
  {
  }

  public abstract class date : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  ContractRenewalHistory.date>
  {
  }

  public abstract class discountID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ContractRenewalHistory.discountID>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  ContractRenewalHistory.Tstamp>
  {
  }

  public abstract class createdByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    ContractRenewalHistory.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ContractRenewalHistory.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    ContractRenewalHistory.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    ContractRenewalHistory.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ContractRenewalHistory.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    ContractRenewalHistory.lastModifiedDateTime>
  {
  }
}
