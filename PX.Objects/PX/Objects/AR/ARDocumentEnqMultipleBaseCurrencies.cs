// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.ARDocumentEnqMultipleBaseCurrencies
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CS;
using PX.SP;
using System.Collections;

#nullable disable
namespace PX.Objects.AR;

public class ARDocumentEnqMultipleBaseCurrencies : PXGraphExtension<ARDocumentEnq>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.multipleBaseCurrencies>();

  [PXOverride]
  public IEnumerable documents(
    ARDocumentEnqMultipleBaseCurrencies.documentsDelegate baseMethod)
  {
    return !((PXSelectBase<ARDocumentEnq.ARDocumentFilter>) this.Base.Filter).Current.OrgBAccountID.HasValue && !PortalHelper.IsPortalContext((PortalContexts) 3) ? (IEnumerable) new PXDelegateResult() : baseMethod();
  }

  public delegate IEnumerable documentsDelegate();
}
