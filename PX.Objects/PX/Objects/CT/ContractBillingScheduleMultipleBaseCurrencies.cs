// Decompiled with JetBrains decompiler
// Type: PX.Objects.CT.ContractBillingScheduleMultipleBaseCurrencies
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CS;

#nullable enable
namespace PX.Objects.CT;

public sealed class ContractBillingScheduleMultipleBaseCurrencies : 
  PXCacheExtension<
  #nullable disable
  ContractBillingSchedule>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.multipleBaseCurrencies>();

  [PXInt]
  [PXFormula(typeof (Selector<ContractBillingSchedule.accountID, PX.Objects.AR.Customer.cOrgBAccountID>))]
  public int? COrgBAccountID { get; set; }

  [PXString]
  [PXFormula(typeof (Selector<ContractBillingSchedule.accountID, PX.Objects.AR.Customer.baseCuryID>))]
  public string CustomerBaseCuryID { get; set; }

  public abstract class cOrgBAccountID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ContractBillingScheduleMultipleBaseCurrencies.cOrgBAccountID>
  {
  }

  public abstract class customerBaseCuryID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ContractBillingScheduleMultipleBaseCurrencies.customerBaseCuryID>
  {
  }
}
