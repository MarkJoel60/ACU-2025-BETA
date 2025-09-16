// Decompiled with JetBrains decompiler
// Type: PX.Objects.CN.Subcontracts.CR.GraphExtensions.CrEmailActivityMaintExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.EP;
using PX.Objects.CN.Subcontracts.CR.Helpers;
using PX.Objects.CR;
using PX.Objects.CS;

#nullable disable
namespace PX.Objects.CN.Subcontracts.CR.GraphExtensions;

public class CrEmailActivityMaintExt : PXGraphExtension<CREmailActivityMaint>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.construction>();

  protected virtual void CrSmEmail_RowSelected(
    PXCache cache,
    PXRowSelectedEventArgs args,
    PXRowSelected baseHandler)
  {
    baseHandler.Invoke(cache, args);
    if (!(args.Row is CRSMEmail row))
      return;
    this.UpdateEntityDescriptionIfRequired(row);
  }

  private void UpdateEntityDescriptionIfRequired(CRSMEmail email)
  {
    string description = SubcontractEntityDescriptionHelper.GetDescription((CRActivity) email, (PXGraph) this.Base);
    if (description == null)
      return;
    email.EntityDescription = CacheUtility.GetErrorDescription(email.Exception) + description;
  }
}
