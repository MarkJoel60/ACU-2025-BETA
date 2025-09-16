// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.SalesPersonMaintVisibilityRestriction
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CS;

#nullable disable
namespace PX.Objects.AR;

public class SalesPersonMaintVisibilityRestriction : PXGraphExtension<SalesPersonMaint>
{
  public PXSelectJoin<CustSalesPeople, InnerJoin<Customer, On<Customer.bAccountID, Equal<CustSalesPeople.bAccountID>>>, Where<CustSalesPeople.salesPersonID, Equal<Current<SalesPerson.salesPersonID>>, And<Customer.cOrgBAccountID, RestrictByUserBranches<Current<AccessInfo.userName>>, And<Match<Customer, Current<AccessInfo.userName>>>>>> SPCustomers;

  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.visibilityRestriction>();
}
