// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.FSContractPeriodFilter
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CM;
using System;

#nullable enable
namespace PX.Objects.FS;

[Serializable]
public class FSContractPeriodFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXString(3, IsFixed = true)]
  [ListField_ContractPeriod_Actions.ListAtrribute]
  [PXUIField(DisplayName = "Actions")]
  public virtual 
  #nullable disable
  string Actions { get; set; }

  [PXInt]
  [PXUIField(DisplayName = "Billing Period")]
  [FSSelectorContractBillingPeriod]
  [PXDefault(typeof (Search<FSContractPeriod.contractPeriodID, Where2<Where<Current<FSContractPeriodFilter.actions>, Equal<ListField_ContractPeriod_Actions.ModifyBillingPeriod>, And<FSContractPeriod.serviceContractID, Equal<Current<FSServiceContract.serviceContractID>>, And<FSContractPeriod.status, Equal<ListField_Status_ContractPeriod.Inactive>>>>, Or<Where<Current<FSContractPeriodFilter.actions>, Equal<ListField_ContractPeriod_Actions.SearchBillingPeriod>, And<FSContractPeriod.serviceContractID, Equal<Current<FSServiceContract.serviceContractID>>, And<FSContractPeriod.status, Equal<ListField_Status_ContractPeriod.Active>>>>>>, OrderBy<Desc<FSContractPeriod.startPeriodDate>>>))]
  public virtual int? ContractPeriodID { get; set; }

  [PXBaseCury]
  [PXUIField(DisplayName = "Contract Total", IsReadOnly = true)]
  public virtual Decimal? StandardizedBillingTotal { get; set; }

  [PXString]
  [PXUIField(DisplayName = "Reference Nbr.", IsReadOnly = true)]
  public virtual string PostDocRefNbr { get; set; }

  public abstract class actions : ListField_ContractPeriod_Actions
  {
  }

  public abstract class contractPeriodID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSContractPeriodFilter.contractPeriodID>
  {
  }

  public abstract class standardizedBillingTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FSContractPeriodFilter.standardizedBillingTotal>
  {
  }

  public abstract class postDocRefNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSContractPeriodFilter.postDocRefNbr>
  {
  }
}
