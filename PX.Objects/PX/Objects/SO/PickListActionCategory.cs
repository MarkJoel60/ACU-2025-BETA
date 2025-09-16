// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.PickListActionCategory
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data.WorkflowAPI;
using PX.Objects.Common;
using System;

#nullable disable
namespace PX.Objects.SO;

public static class PickListActionCategory
{
  public const string ID = "Pick List";

  public static BoundedTo<SOShipmentEntry, SOShipment>.ActionCategory.IConfigured Get(
    WorkflowContext<SOShipmentEntry, SOShipment> context)
  {
    return context.Categories.Get("Pick List") ?? context.Categories.CreateNew("Pick List", (Func<BoundedTo<SOShipmentEntry, SOShipment>.ActionCategory.IAllowOptionalConfigCategory, BoundedTo<SOShipmentEntry, SOShipment>.ActionCategory.IConfigured>) (category => (BoundedTo<SOShipmentEntry, SOShipment>.ActionCategory.IConfigured) category.DisplayName("Pick List").PlaceAfter(CommonActionCategories.Get<SOShipmentEntry, SOShipment>(context).Processing))).Apply<BoundedTo<SOShipmentEntry, SOShipment>.ActionCategory.IConfigured>((Action<BoundedTo<SOShipmentEntry, SOShipment>.ActionCategory.IConfigured>) (category => context.UpdateScreenConfigurationFor((Func<BoundedTo<SOShipmentEntry, SOShipment>.ScreenConfiguration.ConfiguratorScreen, BoundedTo<SOShipmentEntry, SOShipment>.ScreenConfiguration.ConfiguratorScreen>) (screen => screen.WithCategories((Action<BoundedTo<SOShipmentEntry, SOShipment>.ActionCategory.ContainerAdjusterCategories>) (categories => categories.Add(category)))))));
  }

  [PXLocalizable]
  public static class DisplayNames
  {
    public const string Value = "Pick List";
  }
}
