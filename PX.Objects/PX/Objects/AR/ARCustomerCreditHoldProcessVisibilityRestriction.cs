// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.ARCustomerCreditHoldProcessVisibilityRestriction
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CS;
using System;
using System.Data.SqlTypes;

#nullable disable
namespace PX.Objects.AR;

public class ARCustomerCreditHoldProcessVisibilityRestriction : 
  PXGraphExtension<ARCustomerCreditHoldProcess>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.visibilityRestriction>();

  [PXOverride]
  public PXResultset<Customer> GetCustomersToProcess(
    ARCustomerCreditHoldProcess.CreditHoldParameters header,
    ARCustomerCreditHoldProcessVisibilityRestriction.GetCustomersToProcessDelegate baseMethod)
  {
    int? action = header.Action;
    if (action.HasValue)
    {
      switch (action.GetValueOrDefault())
      {
        case 0:
          ARCustomerCreditHoldProcess creditHoldProcess = this.Base;
          object[] objArray = new object[2];
          DateTime? nullable = header.BeginDate;
          SqlDateTime sqlDateTime;
          DateTime valueOrDefault1;
          if (!nullable.HasValue)
          {
            sqlDateTime = SqlDateTime.MinValue;
            valueOrDefault1 = sqlDateTime.Value;
          }
          else
            valueOrDefault1 = nullable.GetValueOrDefault();
          objArray[0] = (object) valueOrDefault1;
          nullable = header.EndDate;
          DateTime valueOrDefault2;
          if (!nullable.HasValue)
          {
            sqlDateTime = SqlDateTime.MaxValue;
            valueOrDefault2 = sqlDateTime.Value;
          }
          else
            valueOrDefault2 = nullable.GetValueOrDefault();
          objArray[1] = (object) valueOrDefault2;
          return PXSelectBase<Customer, PXSelectJoin<Customer, InnerJoin<ARDunningLetter, On<Customer.bAccountID, Equal<ARDunningLetter.bAccountID>, And<ARDunningLetter.lastLevel, Equal<True>, And<ARDunningLetter.released, Equal<True>, And<ARDunningLetter.voided, NotEqual<True>>>>>>, Where<ARDunningLetter.dunningLetterDate, Between<Required<ARDunningLetter.dunningLetterDate>, Required<ARDunningLetter.dunningLetterDate>>, And<Customer.cOrgBAccountID, RestrictByUserBranches<Current<AccessInfo.userName>>, And<BqlOperand<Customer.status, IBqlString>.IsIn<CustomerStatus.active, CustomerStatus.oneTime>>>>, OrderBy<Asc<ARDunningLetter.bAccountID>>>.Config>.Select((PXGraph) creditHoldProcess, objArray);
        case 1:
          PXSelectBase<Customer> pxSelectBase = (PXSelectBase<Customer>) new PXSelectJoin<Customer, LeftJoin<ARDunningLetter, On<Customer.bAccountID, Equal<ARDunningLetter.bAccountID>, And<ARDunningLetter.lastLevel, Equal<True>, And<ARDunningLetter.released, Equal<True>, And<ARDunningLetter.voided, NotEqual<True>>>>>>, Where<Customer.cOrgBAccountID, RestrictByUserBranches<Current<AccessInfo.userName>>, And<BqlOperand<Customer.status, IBqlString>.IsEqual<CustomerStatus.creditHold>>>>((PXGraph) this.Base);
          if (PXAccess.FeatureInstalled<FeaturesSet.parentChildAccount>())
            pxSelectBase.WhereAnd<Where<Customer.bAccountID, Equal<Customer.sharedCreditCustomerID>>>();
          return pxSelectBase.Select(Array.Empty<object>());
      }
    }
    return new PXResultset<Customer>();
  }

  public delegate PXResultset<Customer> GetCustomersToProcessDelegate(
    ARCustomerCreditHoldProcess.CreditHoldParameters header);
}
