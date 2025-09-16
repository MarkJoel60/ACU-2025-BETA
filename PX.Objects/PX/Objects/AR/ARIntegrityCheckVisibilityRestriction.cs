// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.ARIntegrityCheckVisibilityRestriction
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CS;
using System;
using System.Collections;

#nullable disable
namespace PX.Objects.AR;

public class ARIntegrityCheckVisibilityRestriction : PXGraphExtension<ARIntegrityCheck>
{
  public PXFilteredProcessing<Customer, ARIntegrityCheckFilter, Where<Customer.cOrgBAccountID, RestrictByUserBranches<Current<AccessInfo.userName>>, And<Match<Current<AccessInfo.userName>>>>> ARCustomerList;
  public PXSelect<Customer, Where<Customer.customerClassID, Equal<Current<ARIntegrityCheckFilter.customerClassID>>, And<Customer.cOrgBAccountID, RestrictByUserBranches<Current<AccessInfo.userName>>, And<Match<Current<AccessInfo.userName>>>>>> Customer_ClassID;

  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.visibilityRestriction>();

  [PXMergeAttributes]
  [RestrictCustomerClassByUserBranches]
  protected virtual void ARIntegrityCheckFilter_CustomerClassID_CacheAttached(PXCache sender)
  {
  }

  protected IEnumerable arcustomerlist()
  {
    return ((PXSelectBase<ARIntegrityCheckFilter>) this.Base.Filter).Current != null && ((PXSelectBase<ARIntegrityCheckFilter>) this.Base.Filter).Current.CustomerClassID != null ? (IEnumerable) ((PXSelectBase<Customer>) this.Customer_ClassID).Select(Array.Empty<object>()) : (IEnumerable) null;
  }
}
