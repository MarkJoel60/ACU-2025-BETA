// Decompiled with JetBrains decompiler
// Type: PX.Objects.CT.ContractRevisionByPeriod
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.GL.FinPeriods;
using System;

#nullable enable
namespace PX.Objects.CT;

[PXProjection(typeof (Select5<MasterFinPeriod, LeftJoin<ContractRenewalHistory, On<MasterFinPeriod.startDate, NotEqual<MasterFinPeriod.endDate>, And<MasterFinPeriod.finDate, GreaterEqual<ContractRenewalHistory.effectiveFrom>, And<MasterFinPeriod.finDate, GreaterEqual<ContractRenewalHistory.activationDate>, And<ContractRenewalHistory.status, NotEqual<Contract.status.draft>, And<ContractRenewalHistory.status, NotEqual<Contract.status.inApproval>, And<ContractRenewalHistory.status, NotEqual<Contract.status.inUpgrade>, And<ContractRenewalHistory.status, NotEqual<Contract.status.pendingActivation>, And<ContractRenewalHistory.effectiveFrom, IsNotNull, And<Where<MasterFinPeriod.startDate, LessEqual<ContractRenewalHistory.expireDate>, Or<ContractRenewalHistory.expireDate, IsNull>>>>>>>>>>>, LeftJoin<Contract, On<Where<ContractRenewalHistory.contractID, Equal<Contract.contractID>>>>>, Where<MasterFinPeriod.startDate, LessEqual<Contract.terminationDate>, Or<Contract.terminationDate, IsNull>>, Aggregate<GroupBy<MasterFinPeriod.finDate, GroupBy<ContractRenewalHistory.contractID, Max<ContractRenewalHistory.revID>>>>>))]
[PXCacheName("Contract revision by period")]
[Serializable]
public class ContractRevisionByPeriod : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PX.Objects.GL.FinPeriodID(null, null, null, null, null, null, true, false, null, null, null, true, true, IsKey = true, BqlField = typeof (MasterFinPeriod.finPeriodID))]
  public virtual 
  #nullable disable
  string FinPeriodID { get; set; }

  [PXDBIdentity(IsKey = true, BqlField = typeof (ContractRenewalHistory.contractID))]
  [PXUIField(DisplayName = "Contract ID")]
  public virtual int? ContractID { get; set; }

  [PXDBInt(BqlField = typeof (ContractRenewalHistory.revID))]
  [PXUIField(DisplayName = "Revision Number")]
  public virtual int? RevID { get; set; }

  [PXDBDate(BqlField = typeof (ContractRenewalHistory.activationDate))]
  public virtual DateTime? ActivationDate { get; set; }

  [PXDBDate(BqlField = typeof (ContractRenewalHistory.effectiveFrom))]
  [PXUIField(DisplayName = "Effective From")]
  public virtual DateTime? EffectiveFrom { get; set; }

  [PXDBDate(BqlField = typeof (ContractRenewalHistory.expireDate))]
  [PXUIField(DisplayName = "Expiration Date")]
  public virtual DateTime? ExpireDate { get; set; }

  [PXDBDate(BqlField = typeof (Contract.terminationDate))]
  public virtual DateTime? TerminationDate { get; set; }

  [PXDBDate(BqlField = typeof (MasterFinPeriod.startDate))]
  [PXUIField(DisplayName = "Start Date")]
  public virtual DateTime? StartFinPeriod { get; set; }

  [PXDBDate(BqlField = typeof (MasterFinPeriod.endDate))]
  public virtual DateTime? EndFinPeriod { get; set; }

  [PXInt]
  public virtual int? NewCount
  {
    [PXDependsOnFields(new Type[] {typeof (ContractRevisionByPeriod.effectiveFrom), typeof (ContractRevisionByPeriod.startFinPeriod), typeof (ContractRevisionByPeriod.endFinPeriod)})] get
    {
      DateTime? effectiveFrom = this.EffectiveFrom;
      DateTime? nullable = this.StartFinPeriod;
      if ((effectiveFrom.HasValue & nullable.HasValue ? (effectiveFrom.GetValueOrDefault() >= nullable.GetValueOrDefault() ? 1 : 0) : 0) != 0)
      {
        nullable = this.EffectiveFrom;
        DateTime? endFinPeriod = this.EndFinPeriod;
        if ((nullable.HasValue & endFinPeriod.HasValue ? (nullable.GetValueOrDefault() < endFinPeriod.GetValueOrDefault() ? 1 : 0) : 0) != 0)
          return new int?(1);
      }
      return new int?(0);
    }
    set
    {
    }
  }

  [PXDBInt]
  [PXUIField(DisplayName = "Expired")]
  public virtual int? ExpiredCount
  {
    [PXDependsOnFields(new Type[] {typeof (ContractRevisionByPeriod.terminationDate), typeof (ContractRevisionByPeriod.expireDate), typeof (ContractRevisionByPeriod.startFinPeriod), typeof (ContractRevisionByPeriod.endFinPeriod)})] get
    {
      DateTime? expireDate = this.ExpireDate;
      DateTime? nullable1 = this.StartFinPeriod;
      DateTime? nullable2;
      if ((expireDate.HasValue & nullable1.HasValue ? (expireDate.GetValueOrDefault() >= nullable1.GetValueOrDefault() ? 1 : 0) : 0) != 0)
      {
        nullable1 = this.ExpireDate;
        nullable2 = this.EndFinPeriod;
        if ((nullable1.HasValue & nullable2.HasValue ? (nullable1.GetValueOrDefault() < nullable2.GetValueOrDefault() ? 1 : 0) : 0) != 0)
          goto label_4;
      }
      nullable2 = this.TerminationDate;
      nullable1 = this.StartFinPeriod;
      if ((nullable2.HasValue & nullable1.HasValue ? (nullable2.GetValueOrDefault() >= nullable1.GetValueOrDefault() ? 1 : 0) : 0) != 0)
      {
        nullable1 = this.TerminationDate;
        nullable2 = this.EndFinPeriod;
        if ((nullable1.HasValue & nullable2.HasValue ? (nullable1.GetValueOrDefault() < nullable2.GetValueOrDefault() ? 1 : 0) : 0) != 0)
          goto label_4;
      }
      return new int?(0);
label_4:
      return new int?(1);
    }
    set
    {
    }
  }

  public class PK : 
    PrimaryKeyOf<ContractRevisionByPeriod>.By<ContractRevisionByPeriod.contractID, ContractRevisionByPeriod.revID>
  {
    public static ContractRevisionByPeriod Find(
      PXGraph graph,
      int? contractID,
      int? revID,
      PKFindOptions options = 0)
    {
      return (ContractRevisionByPeriod) PrimaryKeyOf<ContractRevisionByPeriod>.By<ContractRevisionByPeriod.contractID, ContractRevisionByPeriod.revID>.FindBy(graph, (object) contractID, (object) revID, options);
    }
  }

  public static class FK
  {
    public class Contract : 
      PrimaryKeyOf<Contract>.By<Contract.contractID>.ForeignKeyOf<ContractRevisionByPeriod>.By<ContractRevisionByPeriod.contractID>
    {
    }
  }

  public abstract class finPeriodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ContractRevisionByPeriod.finPeriodID>
  {
  }

  public abstract class contractID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ContractRevisionByPeriod.contractID>
  {
  }

  public abstract class revID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ContractRevisionByPeriod.revID>
  {
  }

  public abstract class activationDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    ContractRevisionByPeriod.activationDate>
  {
  }

  public abstract class effectiveFrom : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    ContractRevisionByPeriod.effectiveFrom>
  {
  }

  public abstract class expireDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    ContractRevisionByPeriod.expireDate>
  {
  }

  public abstract class terminationDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    ContractRevisionByPeriod.terminationDate>
  {
  }

  public abstract class startFinPeriod : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    ContractRevisionByPeriod.startFinPeriod>
  {
  }

  public abstract class endFinPeriod : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    ContractRevisionByPeriod.endFinPeriod>
  {
  }

  public abstract class newCount : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ContractRevisionByPeriod.newCount>
  {
  }

  public abstract class expiredCount : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    ContractRevisionByPeriod.expiredCount>
  {
  }
}
