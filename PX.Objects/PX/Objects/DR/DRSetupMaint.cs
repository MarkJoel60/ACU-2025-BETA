// Decompiled with JetBrains decompiler
// Type: PX.Objects.DR.DRSetupMaint
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CS;

#nullable disable
namespace PX.Objects.DR;

public class DRSetupMaint : PXGraph<DRSetupMaint>
{
  public PXSelect<DRSetup> DRSetupRecord;
  public PXSave<DRSetup> Save;
  public PXCancel<DRSetup> Cancel;

  protected virtual void DRSetup_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    if (!(e.Row is DRSetup row))
      return;
    bool flag1 = PXAccess.FeatureInstalled<FeaturesSet.multicurrency>();
    bool flag2 = PXAccess.FeatureInstalled<FeaturesSet.aSC606>();
    PXUIFieldAttribute.SetVisible<DRSetup.useFairValuePricesInBaseCurrency>(sender, (object) row, flag1 & flag2);
    PXUIFieldAttribute.SetVisible<DRSetup.suspenseAccountID>(sender, (object) row, flag2);
    PXUIFieldAttribute.SetVisible<DRSetup.suspenseSubID>(sender, (object) row, flag2);
    PXUIFieldAttribute.SetVisible<DRSetup.recognizeAdjustmentsInPreviousPeriods>(sender, (object) row, flag2);
  }
}
