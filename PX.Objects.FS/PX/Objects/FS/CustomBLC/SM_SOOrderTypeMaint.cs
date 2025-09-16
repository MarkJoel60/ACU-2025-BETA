// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.CustomBLC.SM_SOOrderTypeMaint
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Objects.CS;
using PX.Objects.SO;

#nullable disable
namespace PX.Objects.FS.CustomBLC;

public class SM_SOOrderTypeMaint : PXGraphExtension<SOOrderTypeMaint>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.serviceManagementModule>();

  protected virtual void SOOrderType_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    if (e.Row == null)
      return;
    bool flag = ((PX.Objects.SO.SOOrderType) e.Row).Behavior == "BL";
    PXUIFieldAttribute.SetVisible<FSxSOOrderType.enableFSIntegration>(sender, e.Row, !flag);
  }
}
