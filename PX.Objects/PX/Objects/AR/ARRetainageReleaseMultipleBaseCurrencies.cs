// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.ARRetainageReleaseMultipleBaseCurrencies
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CS;
using System.Collections;

#nullable disable
namespace PX.Objects.AR;

public class ARRetainageReleaseMultipleBaseCurrencies : PXGraphExtension<ARRetainageRelease>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.multipleBaseCurrencies>();

  [PXOverride]
  public IEnumerable documentList(
    ARRetainageReleaseMultipleBaseCurrencies.documentListDelegate baseMethod)
  {
    return !((PXSelectBase<ARRetainageFilter>) this.Base.Filter).Current.OrgBAccountID.HasValue ? (IEnumerable) null : baseMethod();
  }

  public delegate IEnumerable documentListDelegate();
}
