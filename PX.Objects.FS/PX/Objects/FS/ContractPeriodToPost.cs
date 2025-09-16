// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.ContractPeriodToPost
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.AR;
using PX.Objects.CS;
using System;

#nullable enable
namespace PX.Objects.FS;

[PXProjection(typeof (Select5<FSServiceContract, InnerJoin<FSContractPeriod, On<FSContractPeriod.serviceContractID, Equal<FSServiceContract.serviceContractID>>, InnerJoin<FSContractPeriodDet, On<FSContractPeriodDet.contractPeriodID, Equal<FSContractPeriod.contractPeriodID>>>>, Where<FSContractPeriod.invoiced, Equal<False>, And<FSServiceContract.status, Equal<ListField_Status_ServiceContract.Active>, And2<Where<FSServiceContract.billingType, Equal<ListField.ServiceContractBillingType.standardizedBillings>, Or<FSServiceContract.billingType, Equal<ListField.ServiceContractBillingType.fixedRateAsPerformedBillings>, Or<FSServiceContract.billingType, Equal<ListField.ServiceContractBillingType.fixedRateBillings>>>>, And<Where<FSContractPeriod.status, Equal<ListField_Status_ContractPeriod.Active>, Or<FSContractPeriod.status, Equal<ListField_Status_ContractPeriod.Pending>>>>>>>, Aggregate<GroupBy<FSServiceContract.serviceContractID, GroupBy<FSServiceContract.noteID, GroupBy<FSContractPeriod.contractPeriodID>>>>>))]
[PXGroupMask(typeof (InnerJoinSingleTable<PX.Objects.AR.Customer, On<PX.Objects.AR.Customer.bAccountID, Equal<ContractPeriodToPost.billCustomerID>, And<Match<PX.Objects.AR.Customer, Current<AccessInfo.userName>>>>>))]
[Serializable]
public class ContractPeriodToPost : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBInt(BqlField = typeof (FSServiceContract.customerID))]
  public virtual int? CustomerID { get; set; }

  [PXDBString(BqlField = typeof (FSServiceContract.refNbr))]
  [PXUIField]
  [PXSelector(typeof (Search<FSServiceContract.refNbr, Where<FSServiceContract.customerID, Equal<Current<ContractPeriodToPost.customerID>>>>))]
  public virtual 
  #nullable disable
  string RefNbr { get; set; }

  [PXDBString(15, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC", BqlField = typeof (FSServiceContract.customerContractNbr))]
  [PXUIField]
  [FSSelectorCustomerContractNbrAttribute(typeof (ListField_RecordType_ContractSchedule.ServiceContract), typeof (ContractPeriodToPost.customerID))]
  public virtual string CustomerContractNbr { get; set; }

  [PXDBInt(BqlField = typeof (FSServiceContract.serviceContractID), IsKey = true)]
  public virtual int? ServiceContractID { get; set; }

  [PXDBInt(BqlField = typeof (FSServiceContract.billCustomerID))]
  [PXDefault]
  [PXUIField(DisplayName = "Billing Customer", Enabled = false)]
  [FSSelectorBAccountTypeCustomerOrCombined]
  [PXRestrictor(typeof (Where<PX.Objects.AR.Customer.status, IsNull, Or<PX.Objects.AR.Customer.status, Equal<CustomerStatus.active>, Or<PX.Objects.AR.Customer.status, Equal<CustomerStatus.oneTime>>>>), "The customer status is '{0}'.", new System.Type[] {typeof (PX.Objects.AR.Customer.status)})]
  public virtual int? BillCustomerID { get; set; }

  [LocationID(typeof (Where<PX.Objects.CR.Location.bAccountID, Equal<Current<ContractPeriodToPost.billCustomerID>>>), BqlField = typeof (FSServiceContract.billLocationID), DescriptionField = typeof (PX.Objects.CR.Location.descr), DirtyRead = true, DisplayName = "Billing Location", Enabled = false)]
  public virtual int? BillLocationID { get; set; }

  [PXDBInt(BqlField = typeof (FSServiceContract.branchID))]
  [PXUIField(DisplayName = "Branch")]
  [PXSelector(typeof (PX.Objects.GL.Branch.branchID), DescriptionField = typeof (PX.Objects.GL.Branch.acctName), SubstituteKey = typeof (PX.Objects.GL.Branch.branchCD))]
  public virtual int? BranchID { get; set; }

  [PXDBInt(BqlField = typeof (FSServiceContract.branchLocationID))]
  [PXUIField(DisplayName = "Branch Location")]
  [PXSelector(typeof (Search<FSBranchLocation.branchLocationID, Where<FSBranchLocation.branchID, Equal<Current<ContractPeriodToPost.branchID>>>>), DescriptionField = typeof (FSBranchLocation.descr), SubstituteKey = typeof (FSBranchLocation.branchLocationCD))]
  public virtual int? BranchLocationID { get; set; }

  [PXDBString(4, IsUnicode = true, BqlField = typeof (FSServiceContract.billingType))]
  [ListField.ServiceContractBillingType.List]
  [PXUIField(DisplayName = "Billing Type")]
  public virtual string BillingType { get; set; }

  [PXDBDate(BqlField = typeof (FSServiceContract.startDate))]
  [PXUIField(DisplayName = "Start Date")]
  public virtual DateTime? StartDate { get; set; }

  [PXDBString(BqlField = typeof (FSContractPeriod.status))]
  [ListField_Status_ContractPeriod.List]
  [PXUIField]
  public virtual string Status { get; set; }

  [PXDBString(BqlField = typeof (FSServiceContract.docDesc))]
  [PXUIField(DisplayName = "Description")]
  public virtual string DocDesc { get; set; }

  [PXDBDate(BqlField = typeof (FSContractPeriod.endPeriodDate))]
  public virtual DateTime? EndPeriodDate { get; set; }

  [PXDBDate(BqlField = typeof (FSContractPeriod.startPeriodDate))]
  public virtual DateTime? StartPeriodDate { get; set; }

  [PXDBDate(BqlField = typeof (FSServiceContract.nextBillingInvoiceDate))]
  [PXUIField(DisplayName = "Next Billing Date", Enabled = false)]
  public virtual DateTime? NextBillingInvoiceDate { get; set; }

  [PXDBInt(BqlField = typeof (FSContractPeriod.contractPeriodID), IsKey = true)]
  public virtual int? ContractPeriodID { get; set; }

  [PXInt]
  [PXUIField(DisplayName = "Batch Nbr.", Enabled = false)]
  [PXSelector(typeof (Search<FSContractPostBatch.contractPostBatchID>), SubstituteKey = typeof (FSContractPostBatch.contractPostBatchNbr))]
  public virtual int? ContractPostBatchID { get; set; }

  [PXBool]
  [PXUIField(DisplayName = "Selected")]
  public virtual bool? Selected { get; set; }

  [PXString]
  [PXUIField(DisplayName = "Billing Period", Enabled = false, IsReadOnly = true)]
  public virtual string BillingPeriod
  {
    get
    {
      return this.StartPeriodDate.HasValue && this.EndPeriodDate.HasValue ? $"{this.StartPeriodDate.Value.ToString("d")} - {this.EndPeriodDate.Value.ToString("d")}" : string.Empty;
    }
  }

  public class PK : 
    PrimaryKeyOf<ContractPeriodToPost>.By<ContractPeriodToPost.serviceContractID, ContractPeriodToPost.contractPeriodID>
  {
    public static ContractPeriodToPost Find(
      PXGraph graph,
      int? serviceContractID,
      int? contractPeriodID,
      PKFindOptions options = 0)
    {
      return (ContractPeriodToPost) PrimaryKeyOf<ContractPeriodToPost>.By<ContractPeriodToPost.serviceContractID, ContractPeriodToPost.contractPeriodID>.FindBy(graph, (object) serviceContractID, (object) contractPeriodID, options);
    }
  }

  public static class FK
  {
    public class Customer : 
      PrimaryKeyOf<PX.Objects.AR.Customer>.By<PX.Objects.AR.Customer.bAccountID>.ForeignKeyOf<ContractPeriodToPost>.By<ContractPeriodToPost.customerID>
    {
    }

    public class ServiceContract : 
      PrimaryKeyOf<FSServiceContract>.By<FSServiceContract.refNbr>.ForeignKeyOf<ContractPeriodToPost>.By<ContractPeriodToPost.refNbr>
    {
    }

    public class BillCustomer : 
      PrimaryKeyOf<PX.Objects.AR.Customer>.By<PX.Objects.AR.Customer.bAccountID>.ForeignKeyOf<ContractPeriodToPost>.By<ContractPeriodToPost.billCustomerID>
    {
    }

    public class BillCustomerLocation : 
      PrimaryKeyOf<PX.Objects.CR.Location>.By<PX.Objects.CR.Location.bAccountID, PX.Objects.CR.Location.locationID>.ForeignKeyOf<ContractPeriodToPost>.By<ContractPeriodToPost.billCustomerID, ContractPeriodToPost.billLocationID>
    {
    }

    public class Branch : 
      PrimaryKeyOf<PX.Objects.GL.Branch>.By<PX.Objects.GL.Branch.branchID>.ForeignKeyOf<ContractPeriodToPost>.By<ContractPeriodToPost.branchID>
    {
    }

    public class BranchLocation : 
      PrimaryKeyOf<FSBranchLocation>.By<FSBranchLocation.branchLocationID>.ForeignKeyOf<ContractPeriodToPost>.By<ContractPeriodToPost.branchLocationID>
    {
    }

    public class ContractPeriod : 
      PrimaryKeyOf<FSContractPeriod>.By<FSContractPeriod.contractPeriodID>.ForeignKeyOf<ContractPeriodToPost>.By<ContractPeriodToPost.contractPeriodID>
    {
    }

    public class ContractPostBatch : 
      PrimaryKeyOf<FSContractPostBatch>.By<FSContractPostBatch.contractPostBatchID>.ForeignKeyOf<ContractPeriodToPost>.By<ContractPeriodToPost.contractPostBatchID>
    {
    }
  }

  public abstract class customerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ContractPeriodToPost.customerID>
  {
  }

  public abstract class refNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ContractPeriodToPost.refNbr>
  {
  }

  public abstract class customerContractNbr : IBqlField, IBqlOperand
  {
  }

  public abstract class serviceContractID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    ContractPeriodToPost.serviceContractID>
  {
  }

  public abstract class billCustomerID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    ContractPeriodToPost.billCustomerID>
  {
  }

  public abstract class billLocationID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    ContractPeriodToPost.billLocationID>
  {
  }

  public abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ContractPeriodToPost.branchID>
  {
  }

  public abstract class branchLocationID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    ContractPeriodToPost.branchLocationID>
  {
  }

  public abstract class billingType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ContractPeriodToPost.billingType>
  {
    public abstract class Values : ListField.ServiceContractBillingType
    {
    }
  }

  public abstract class startDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    ContractPeriodToPost.startDate>
  {
  }

  public abstract class status : ListField_Status_ContractPeriod
  {
  }

  public abstract class docDesc : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ContractPeriodToPost.docDesc>
  {
  }

  public abstract class endPeriodDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    ContractPeriodToPost.endPeriodDate>
  {
  }

  public abstract class startPeriodDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    ContractPeriodToPost.startPeriodDate>
  {
  }

  public abstract class nextBillingInvoiceDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    ContractPeriodToPost.nextBillingInvoiceDate>
  {
  }

  public abstract class contractPeriodID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    ContractPeriodToPost.contractPeriodID>
  {
  }

  public abstract class contractPostBatchID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    ContractPeriodToPost.contractPostBatchID>
  {
  }

  public abstract class selected : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ContractPeriodToPost.selected>
  {
  }

  public abstract class billingPeriod : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ContractPeriodToPost.billingPeriod>
  {
  }
}
