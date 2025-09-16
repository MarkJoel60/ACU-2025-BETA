// Decompiled with JetBrains decompiler
// Type: PX.Objects.CT.ContractBillingVisibilityRestriction
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CS;

#nullable disable
namespace PX.Objects.CT;

public class ContractBillingVisibilityRestriction : PXGraphExtension<ContractBilling>
{
  public PXFilteredProcessingJoin<Contract, ContractBilling.BillingFilter, InnerJoin<ContractBillingSchedule, On<Contract.contractID, Equal<ContractBillingSchedule.contractID>>, LeftJoin<PX.Objects.AR.Customer, On<Contract.customerID, Equal<PX.Objects.AR.Customer.bAccountID>>>>, Where2<Where<ContractBillingSchedule.nextDate, LessEqual<Current<ContractBilling.BillingFilter.invoiceDate>>, Or<ContractBillingSchedule.type, Equal<BillingType.BillingOnDemand>>>, And<Contract.baseType, Equal<CTPRType.contract>, And<Contract.isCancelled, Equal<False>, And<Contract.isCompleted, Equal<False>, And<Contract.isActive, Equal<True>, And<PX.Objects.AR.Customer.cOrgBAccountID, RestrictByUserBranches<Current<AccessInfo.userName>>, And2<Where<Current<ContractBilling.BillingFilter.templateID>, IsNull, Or<Current<ContractBilling.BillingFilter.templateID>, Equal<Contract.templateID>>>, And2<Where<Current<ContractBilling.BillingFilter.customerClassID>, IsNull, Or<Current<ContractBilling.BillingFilter.customerClassID>, Equal<PX.Objects.AR.Customer.customerClassID>>>, And<Where<Current<ContractBilling.BillingFilter.customerID>, IsNull, Or<Current<ContractBilling.BillingFilter.customerID>, Equal<Contract.customerID>>>>>>>>>>>>> Items;

  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.visibilityRestriction>();
}
