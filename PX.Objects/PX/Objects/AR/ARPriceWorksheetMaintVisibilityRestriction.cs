// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.ARPriceWorksheetMaintVisibilityRestriction
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CR;
using PX.Objects.CS;
using System;

#nullable disable
namespace PX.Objects.AR;

public class ARPriceWorksheetMaintVisibilityRestriction : PXGraphExtension<ARPriceWorksheetMaint>
{
  [Obsolete("This item has been deprecated and will be removed in Acumatica ERP 2024 R1.")]
  public PXSelect<PX.Objects.CR.BAccount, Where<PX.Objects.CR.BAccount.cOrgBAccountID, RestrictByUserBranches<Current<AccessInfo.userName>>, And<Where<PX.Objects.CR.BAccount.type, Equal<BAccountType.customerType>, Or<PX.Objects.CR.BAccount.type, Equal<BAccountType.combinedType>>>>>> CustomerCode;

  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.visibilityRestriction>();

  public virtual void Initialize()
  {
    ((PXGraphExtension) this).Initialize();
    ((PXSelectBase<ARPriceWorksheetDetail>) this.Base.Details).WhereAnd<Where<PX.Objects.CR.BAccount.cOrgBAccountID, RestrictByUserBranches<Current<AccessInfo.userName>>, Or<ARPriceWorksheetDetail.customerID, IsNull>>>();
  }
}
