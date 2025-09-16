// Decompiled with JetBrains decompiler
// Type: PX.Objects.CT.EPCustomerBillingProcessUsageCurrencyValidation
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.EP;
using PX.Objects.PM;

#nullable disable
namespace PX.Objects.CT;

public class EPCustomerBillingProcessUsageCurrencyValidation : 
  UsageCurrencyValidationBase<EPCustomerBillingProcess>
{
  [PXOverride]
  public PMTran InsertPMTran(
    RegisterEntry pmGraph,
    PXResult<EPExpenseClaimDetails, Contract, PX.Objects.GL.Account> res,
    EPCustomerBillingProcessUsageCurrencyValidation.InsertPMTranDelegate baseMethod)
  {
    this.ValidateContractUsageBaseCurrency((int?) PXResult<EPExpenseClaimDetails, Contract, PX.Objects.GL.Account>.op_Implicit(res)?.ContractID);
    return baseMethod(pmGraph, res);
  }

  public delegate PMTran InsertPMTranDelegate(
    RegisterEntry pmGraph,
    PXResult<EPExpenseClaimDetails, Contract, PX.Objects.GL.Account> res);
}
