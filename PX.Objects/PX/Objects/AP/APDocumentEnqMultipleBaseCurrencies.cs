// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.APDocumentEnqMultipleBaseCurrencies
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System.Collections;

#nullable disable
namespace PX.Objects.AP;

public class APDocumentEnqMultipleBaseCurrencies : PXGraphExtension<APDocumentEnq>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<PX.Objects.CS.FeaturesSet.multipleBaseCurrencies>();

  [PXOverride]
  public IEnumerable documents(
    APDocumentEnqMultipleBaseCurrencies.documentsDelegate baseMethod)
  {
    return !this.Base.Filter.Current.OrgBAccountID.HasValue ? (IEnumerable) new PXDelegateResult() : baseMethod();
  }

  public delegate IEnumerable documentsDelegate();
}
