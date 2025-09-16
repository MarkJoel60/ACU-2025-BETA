// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.FSContractPeriod
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CM;
using System;

#nullable enable
namespace PX.Objects.FS;

[Serializable]
public class FSContractPeriod : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  public 
  #nullable disable
  string _BillingPeriod;

  [PXDBInt(IsKey = true)]
  [PXParent(typeof (Select<FSServiceContract, Where<FSServiceContract.serviceContractID, Equal<Current<FSContractPeriod.serviceContractID>>>>))]
  [PXDBDefault(typeof (FSServiceContract.serviceContractID))]
  [PXUIField(DisplayName = "Service Contract ID")]
  public virtual int? ServiceContractID { get; set; }

  [PXDBIdentity(IsKey = true)]
  public virtual int? ContractPeriodID { get; set; }

  [PXDBInt]
  public virtual int? ContractPostDocID { get; set; }

  [PXDBDate]
  [PXDefault]
  [PXUIField(DisplayName = "End Period Date")]
  public virtual DateTime? EndPeriodDate { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Invoiced")]
  public virtual bool? Invoiced { get; set; }

  [PXDBBaseCury(null, null)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Period Total")]
  public virtual Decimal? PeriodTotal { get; set; }

  [PXDBDate]
  [PXDefault]
  [PXUIField(DisplayName = "Start Period Date")]
  public virtual DateTime? StartPeriodDate { get; set; }

  [PXDefault("I")]
  [ListField_Status_ContractPeriod.List]
  [PXDBString(1, IsUnicode = false)]
  public virtual string Status { get; set; }

  [PXDBCreatedByID]
  [PXUIField(DisplayName = "Created By ID")]
  public virtual Guid? CreatedByID { get; set; }

  [PXDBCreatedByScreenID]
  [PXUIField(DisplayName = "Created By Screen ID")]
  public virtual string CreatedByScreenID { get; set; }

  [PXDBCreatedDateTime]
  [PXUIField(DisplayName = "Created DateTime")]
  public virtual DateTime? CreatedDateTime { get; set; }

  [PXDBLastModifiedByID]
  [PXUIField(DisplayName = "Last Modified By ID")]
  public virtual Guid? LastModifiedByID { get; set; }

  [PXDBLastModifiedByScreenID]
  [PXUIField(DisplayName = "Last Modified By Screen ID")]
  public virtual string LastModifiedByScreenID { get; set; }

  [PXDBLastModifiedDateTime]
  [PXUIField(DisplayName = "Last Modified Date Time")]
  public virtual DateTime? LastModifiedDateTime { get; set; }

  [PXDBTimestamp]
  public virtual byte[] tstamp { get; set; }

  [PXString]
  [PXUIField(DisplayName = "Billing Period", IsReadOnly = true, Enabled = false)]
  public virtual string BillingPeriod
  {
    get
    {
      this._BillingPeriod = !this.StartPeriodDate.HasValue || !this.EndPeriodDate.HasValue ? (string) null : $"{this.StartPeriodDate.Value.ToString("d")} - {this.EndPeriodDate.Value.ToString("d")}";
      return this._BillingPeriod;
    }
    set => this._BillingPeriod = value;
  }

  public class PK : 
    PrimaryKeyOf<FSContractPeriod>.By<FSContractPeriod.serviceContractID, FSContractPeriod.contractPeriodID>
  {
    public static FSContractPeriod Find(
      PXGraph graph,
      int? serviceContractID,
      int? contractPeriodID,
      PKFindOptions options = 0)
    {
      return (FSContractPeriod) PrimaryKeyOf<FSContractPeriod>.By<FSContractPeriod.serviceContractID, FSContractPeriod.contractPeriodID>.FindBy(graph, (object) serviceContractID, (object) contractPeriodID, options);
    }
  }

  public class UK : PrimaryKeyOf<FSContractPeriod>.By<FSContractPeriod.contractPeriodID>
  {
    public static FSContractPeriod Find(
      PXGraph graph,
      int? contractPeriodID,
      PKFindOptions options = 0)
    {
      return (FSContractPeriod) PrimaryKeyOf<FSContractPeriod>.By<FSContractPeriod.contractPeriodID>.FindBy(graph, (object) contractPeriodID, options);
    }
  }

  public static class FK
  {
    public class ServiceContract : 
      PrimaryKeyOf<FSServiceContract>.By<FSServiceContract.serviceContractID>.ForeignKeyOf<FSContractPeriod>.By<FSContractPeriod.serviceContractID>
    {
    }
  }

  public abstract class serviceContractID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSContractPeriod.serviceContractID>
  {
  }

  public abstract class contractPeriodID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSContractPeriod.contractPeriodID>
  {
  }

  public abstract class contractPostDocID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSContractPeriod.contractPostDocID>
  {
  }

  public abstract class endPeriodDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FSContractPeriod.endPeriodDate>
  {
  }

  public abstract class invoiced : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FSContractPeriod.invoiced>
  {
  }

  public abstract class periodTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FSContractPeriod.periodTotal>
  {
  }

  public abstract class startPeriodDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FSContractPeriod.startPeriodDate>
  {
  }

  public abstract class status : ListField_Status_ContractPeriod
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  FSContractPeriod.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSContractPeriod.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FSContractPeriod.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    FSContractPeriod.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSContractPeriod.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FSContractPeriod.lastModifiedDateTime>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  FSContractPeriod.Tstamp>
  {
  }

  public abstract class billingPeriod : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSContractPeriod.billingPeriod>
  {
  }
}
