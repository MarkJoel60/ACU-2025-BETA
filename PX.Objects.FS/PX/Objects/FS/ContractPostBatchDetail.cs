// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.ContractPostBatchDetail
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.EP;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CS;
using System;

#nullable enable
namespace PX.Objects.FS;

[PXProjection(typeof (Select2<FSContractPostDoc, InnerJoin<FSServiceContract, On<FSServiceContract.serviceContractID, Equal<FSContractPostDoc.serviceContractID>>, LeftJoin<PX.Objects.AR.Customer, On<PX.Objects.AR.Customer.bAccountID, Equal<FSServiceContract.billCustomerID>>>>>))]
[Serializable]
public class ContractPostBatchDetail : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBInt(IsKey = true, BqlField = typeof (FSContractPostDoc.contractPostDocID))]
  [PXUIField(DisplayName = "Contract Post Doc. ID")]
  public virtual int? ContractPostDocID { get; set; }

  [PXDBInt(IsKey = true, BqlField = typeof (FSContractPostDoc.contractPostBatchID))]
  [PXUIField(DisplayName = "Contract Post Batch ID")]
  public virtual int? ContractPostBatchID { get; set; }

  [PXDBString(2, IsFixed = true, InputMask = ">aa", BqlField = typeof (FSContractPostDoc.postedTO))]
  [PXUIField(DisplayName = "Posted to")]
  public virtual 
  #nullable disable
  string PostedTO { get; set; }

  [PXDBString(15, BqlField = typeof (FSContractPostDoc.postRefNbr))]
  [PXUIField(DisplayName = "Document Nbr.", Enabled = false)]
  public virtual string PostRefNbr { get; set; }

  [PXDBString(3, BqlField = typeof (FSContractPostDoc.postDocType))]
  [PXUIField(DisplayName = "Document Type", Enabled = false)]
  public virtual string PostDocType { get; set; }

  [PXDBString(15, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC", BqlField = typeof (FSServiceContract.refNbr))]
  [PXUIField]
  [PXSelector(typeof (Search<FSServiceContract.refNbr>))]
  public virtual string ContractRefNbr { get; set; }

  [PXDBString(15, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC", BqlField = typeof (FSServiceContract.customerContractNbr))]
  [PXUIField]
  public virtual string CustomerContractNbr { get; set; }

  [PXDBInt(BqlField = typeof (FSServiceContract.serviceContractID))]
  public virtual int? ServiceContractID { get; set; }

  [PXDBInt(BqlField = typeof (FSServiceContract.billCustomerID))]
  [PXUIField(DisplayName = "Billing Customer ID")]
  [FSSelectorBAccountTypeCustomerOrCombined]
  public virtual int? BillCustomerID { get; set; }

  [PXDBString(60, IsUnicode = true, BqlField = typeof (PX.Objects.AR.Customer.acctName))]
  [PXFieldDescription]
  [PXUIField]
  public virtual string AcctName { get; set; }

  [LocationID(typeof (Where<PX.Objects.CR.Location.bAccountID, Equal<Current<ContractPostBatchDetail.billCustomerID>>>), BqlField = typeof (FSServiceContract.billLocationID), DescriptionField = typeof (PX.Objects.CR.Location.descr), DisplayName = "Billing Location", DirtyRead = true)]
  public virtual int? BillLocationID { get; set; }

  [PXUIField(DisplayName = "Start Date")]
  [PXDBDate(BqlField = typeof (FSServiceContract.startDate))]
  public virtual DateTime? StartDate { get; set; }

  [PXDBDate(BqlField = typeof (FSServiceContract.nextBillingInvoiceDate))]
  [PXUIField(DisplayName = "Next Billing Date", Enabled = false)]
  public virtual DateTime? NextBillingInvoiceDate { get; set; }

  [PXDBInt(BqlField = typeof (FSServiceContract.branchID))]
  [PXUIField(DisplayName = "Branch")]
  [PXSelector(typeof (PX.Objects.GL.Branch.branchID), SubstituteKey = typeof (PX.Objects.GL.Branch.branchCD), DescriptionField = typeof (PX.Objects.GL.Branch.acctName))]
  public virtual int? BranchID { get; set; }

  [PXDBInt(BqlField = typeof (FSServiceContract.branchLocationID))]
  [PXUIField(DisplayName = "Branch Location")]
  [PXSelector(typeof (Search<FSBranchLocation.branchLocationID, Where<FSBranchLocation.branchID, Equal<Current<ContractPostBatchDetail.branchID>>>>), SubstituteKey = typeof (FSBranchLocation.branchLocationCD), DescriptionField = typeof (FSBranchLocation.descr))]
  public virtual int? BranchLocationID { get; set; }

  [PXDBString(255 /*0xFF*/, IsUnicode = true, BqlField = typeof (FSServiceContract.docDesc))]
  [PXUIField(DisplayName = "Description")]
  public virtual string DocDesc { get; set; }

  public class PK : 
    PrimaryKeyOf<ContractPostBatchDetail>.By<ContractPostBatchDetail.contractPostDocID, ContractPostBatchDetail.contractPostBatchID>
  {
    public static ContractPostBatchDetail Find(
      PXGraph graph,
      int? contractPostDocID,
      int? contractPostBatchID,
      PKFindOptions options = 0)
    {
      return (ContractPostBatchDetail) PrimaryKeyOf<ContractPostBatchDetail>.By<ContractPostBatchDetail.contractPostDocID, ContractPostBatchDetail.contractPostBatchID>.FindBy(graph, (object) contractPostDocID, (object) contractPostBatchID, options);
    }
  }

  public static class FK
  {
    public class ContractPostDocument : 
      PrimaryKeyOf<FSContractPostDoc>.By<FSContractPostDoc.contractPostDocID>.ForeignKeyOf<ContractPostBatchDetail>.By<ContractPostBatchDetail.contractPostDocID>
    {
    }

    public class ContractPostBatch : 
      PrimaryKeyOf<FSContractPostBatch>.By<FSContractPostBatch.contractPostBatchID>.ForeignKeyOf<ContractPostBatchDetail>.By<ContractPostBatchDetail.contractPostBatchID>
    {
    }

    public class BillCustomer : 
      PrimaryKeyOf<PX.Objects.AR.Customer>.By<PX.Objects.AR.Customer.bAccountID>.ForeignKeyOf<ContractPostBatchDetail>.By<ContractPostBatchDetail.billCustomerID>
    {
    }

    public class BillCustomerLocation : 
      PrimaryKeyOf<PX.Objects.CR.Location>.By<PX.Objects.CR.Location.bAccountID, PX.Objects.CR.Location.locationID>.ForeignKeyOf<ContractPostBatchDetail>.By<ContractPostBatchDetail.billCustomerID, ContractPostBatchDetail.billLocationID>
    {
    }

    public class Branch : 
      PrimaryKeyOf<PX.Objects.GL.Branch>.By<PX.Objects.GL.Branch.branchID>.ForeignKeyOf<ContractPostBatchDetail>.By<ContractPostBatchDetail.branchID>
    {
    }

    public class BranchLocation : 
      PrimaryKeyOf<FSBranchLocation>.By<FSBranchLocation.branchLocationID>.ForeignKeyOf<ContractPostBatchDetail>.By<ContractPostBatchDetail.branchLocationID>
    {
    }

    public class ServiceContract : 
      PrimaryKeyOf<FSServiceContract>.By<FSServiceContract.serviceContractID>.ForeignKeyOf<ContractPostBatchDetail>.By<ContractPostBatchDetail.serviceContractID>
    {
    }
  }

  public abstract class contractPostDocID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    ContractPostBatchDetail.contractPostDocID>
  {
  }

  public abstract class contractPostBatchID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    ContractPostBatchDetail.contractPostBatchID>
  {
  }

  public abstract class postedTO : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ContractPostBatchDetail.postedTO>
  {
  }

  public abstract class postRefNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ContractPostBatchDetail.postRefNbr>
  {
  }

  public abstract class postDocType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ContractPostBatchDetail.postDocType>
  {
  }

  public abstract class serviceContractID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    ContractPostBatchDetail.serviceContractID>
  {
  }

  public abstract class billCustomerID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    ContractPostBatchDetail.billCustomerID>
  {
  }

  public abstract class billLocationID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    ContractPostBatchDetail.billLocationID>
  {
  }

  public abstract class startDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    ContractPostBatchDetail.startDate>
  {
  }

  public abstract class nextBillingInvoiceDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    ContractPostBatchDetail.nextBillingInvoiceDate>
  {
  }

  public abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ContractPostBatchDetail.branchID>
  {
  }

  public abstract class branchLocationID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    ContractPostBatchDetail.branchLocationID>
  {
  }

  public abstract class docDesc : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ContractPostBatchDetail.docDesc>
  {
  }
}
