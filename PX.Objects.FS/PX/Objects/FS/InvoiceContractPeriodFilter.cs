// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.InvoiceContractPeriodFilter
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.AR;
using PX.Objects.GL;
using System;

#nullable enable
namespace PX.Objects.FS;

[Serializable]
public class InvoiceContractPeriodFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXInt]
  [PXDefault(typeof (AccessInfo.branchID))]
  [PXUIField(DisplayName = "Branch")]
  public virtual int? BranchID { get; set; }

  [PXInt]
  [PXUIField(DisplayName = "Billing Customer")]
  [FSSelectorCustomer]
  public virtual int? CustomerID { get; set; }

  [PXDate]
  [PXDefault(typeof (AccessInfo.businessDate))]
  [PXUIField(DisplayName = "Up to Date")]
  public virtual DateTime? UpToDate { get; set; }

  [PXDate]
  [PXFormula(typeof (InvoiceContractPeriodFilter.upToDate))]
  [PXUIField(DisplayName = "Billing Date")]
  public virtual DateTime? InvoiceDate { get; set; }

  [AROpenPeriod(typeof (InvoiceContractPeriodFilter.invoiceDate), typeof (InvoiceContractPeriodFilter.branchID), null, null, null, null, true, false, FinPeriodSelectorAttribute.SelectionModesWithRestrictions.Undefined, null, null)]
  [PXUIField]
  public virtual 
  #nullable disable
  string InvoiceFinPeriodID { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "Service Contract ID")]
  [PXSelector(typeof (Search<FSServiceContract.serviceContractID, Where2<Where<FSServiceContract.recordType, Equal<ListField_RecordType_ContractSchedule.ServiceContract>, Or<FSServiceContract.recordType, Equal<ListField_RecordType_ContractSchedule.RouteServiceContract>>>, And<Where<Current<InvoiceContractPeriodFilter.customerID>, IsNull, Or<FSServiceContract.billCustomerID, Equal<Current<InvoiceContractPeriodFilter.customerID>>>>>>>), SubstituteKey = typeof (FSServiceContract.refNbr))]
  public virtual int? ServiceContractID { get; set; }

  public abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  InvoiceContractPeriodFilter.branchID>
  {
  }

  public abstract class customerID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    InvoiceContractPeriodFilter.customerID>
  {
  }

  public abstract class upToDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    InvoiceContractPeriodFilter.upToDate>
  {
  }

  public abstract class invoiceDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    InvoiceContractPeriodFilter.invoiceDate>
  {
  }

  public abstract class invoiceFinPeriodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    InvoiceContractPeriodFilter.invoiceFinPeriodID>
  {
  }

  public abstract class serviceContractID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    InvoiceContractPeriodFilter.serviceContractID>
  {
  }
}
