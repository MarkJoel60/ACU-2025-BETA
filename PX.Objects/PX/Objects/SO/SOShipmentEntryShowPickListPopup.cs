// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.SOShipmentEntryShowPickListPopup
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.WorkflowAPI;
using PX.Objects.CS;
using System;
using System.Linq.Expressions;

#nullable enable
namespace PX.Objects.SO;

public class SOShipmentEntryShowPickListPopup : 
  PX.Objects.SO.GraphExtensions.ShowPickListPopup.On<
  #nullable disable
  SOShipmentEntry, SOShipment>.FilteredBy<Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  SOPickingWorksheet.worksheetType, 
  #nullable disable
  Equal<SOPickingWorksheet.worksheetType.single>>>>>.And<BqlOperand<
  #nullable enable
  SOPickingWorksheet.worksheetNbr, IBqlString>.IsEqual<
  #nullable disable
  BqlField<
  #nullable enable
  SOShipment.currentWorksheetNbr, IBqlString>.FromCurrent>>>>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.wMSPaperlessPicking>();

  protected 
  #nullable disable
  SOShipmentEntryUnlinkWorksheetExt Unlinker
  {
    get => ((PXGraph) this.Base).FindImplementation<SOShipmentEntryUnlinkWorksheetExt>();
  }

  protected override bool IsPickListExternalViewMode => false;

  protected override bool CanDeletePickList(SOShipment primaryRow)
  {
    return base.CanDeletePickList(primaryRow) && this.Unlinker.CanUnlinkWorksheetFrom(primaryRow);
  }

  protected override void PerformPickListDeletion()
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    SOShipmentEntryShowPickListPopup.\u003C\u003Ec__DisplayClass6_0 cDisplayClass60 = new SOShipmentEntryShowPickListPopup.\u003C\u003Ec__DisplayClass6_0();
    ((PXAction) this.Base.Save).Press();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass60.shipmentNbr = ((PXSelectBase<SOShipment>) this.Base.Document).Current.ShipmentNbr;
    // ISSUE: method pointer
    PXLongOperation.StartOperation((PXGraph) this.Base, new PXToggleAsyncDelegate((object) cDisplayClass60, __methodptr(\u003CPerformPickListDeletion\u003Eb__0)));
  }

  public class WorkflowChanges : PXGraphExtension<SOShipmentEntry_Workflow, SOShipmentEntry>
  {
    public static bool IsActive() => SOShipmentEntryShowPickListPopup.IsActive();

    public virtual void Configure(PXScreenConfiguration config)
    {
      SOShipmentEntryShowPickListPopup.WorkflowChanges.Configure(config.GetScreenConfigurationContext<SOShipmentEntry, SOShipment>());
    }

    protected static void Configure(
      WorkflowContext<SOShipmentEntry, SOShipment> context)
    {
      if (!SOShipmentEntryShowPickListPopup.WorkflowChanges.IsActive())
        return;
      BoundedTo<SOShipmentEntry, SOShipment>.ActionCategory.IConfigured pickListCategory = PickListActionCategory.Get(context);
      context.UpdateScreenConfigurationFor((Func<BoundedTo<SOShipmentEntry, SOShipment>.ScreenConfiguration.ConfiguratorScreen, BoundedTo<SOShipmentEntry, SOShipment>.ScreenConfiguration.ConfiguratorScreen>) (screen => screen.WithActions((Action<BoundedTo<SOShipmentEntry, SOShipment>.ActionDefinition.ContainerAdjusterActions>) (actions => actions.Add<SOShipmentEntryShowPickListPopup>((Expression<Func<SOShipmentEntryShowPickListPopup, PXAction<SOShipment>>>) (ex => ex.ShowPickList), (Func<BoundedTo<SOShipmentEntry, SOShipment>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<SOShipmentEntry, SOShipment>.ActionDefinition.IConfigured>) (a => (BoundedTo<SOShipmentEntry, SOShipment>.ActionDefinition.IConfigured) a.WithCategory(pickListCategory)))))));
    }

    public class SortExtensions : 
      SortExtensionsBy<TypeArrayOf<PXGraphExtension<SOShipmentEntry>>.FilledWith<SOShipmentEntryShowPickListPopup.WorkflowChanges, SOShipmentEntryUnlinkWorksheetExt.WorkflowChanges>>
    {
    }
  }
}
