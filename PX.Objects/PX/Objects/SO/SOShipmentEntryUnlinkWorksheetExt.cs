// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.SOShipmentEntryUnlinkWorksheetExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Data.WorkflowAPI;
using PX.Objects.CS;
using PX.Objects.SO.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

#nullable disable
namespace PX.Objects.SO;

public class SOShipmentEntryUnlinkWorksheetExt : PXGraphExtension<SOShipmentEntry>
{
  public PXAction<SOShipment> UnlinkFromWorksheet;

  public static bool IsActive()
  {
    return PXAccess.FeatureInstalled<FeaturesSet.wMSPaperlessPicking>() || PXAccess.FeatureInstalled<FeaturesSet.wMSAdvancedPicking>();
  }

  [PXButton(CommitChanges = true)]
  [PXUIField(DisplayName = "Remove from Worksheet")]
  protected virtual IEnumerable unlinkFromWorksheet(PXAdapter adapter)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    SOShipmentEntryUnlinkWorksheetExt.\u003C\u003Ec__DisplayClass1_0 cDisplayClass10 = new SOShipmentEntryUnlinkWorksheetExt.\u003C\u003Ec__DisplayClass1_0();
    ((PXAction) this.Base.Save).Press();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass10.shipmentNbr = ((PXSelectBase<SOShipment>) this.Base.Document).Current.ShipmentNbr;
    // ISSUE: method pointer
    PXLongOperation.StartOperation((PXGraph) this.Base, new PXToggleAsyncDelegate((object) cDisplayClass10, __methodptr(\u003CunlinkFromWorksheet\u003Eb__0)));
    return adapter.Get();
  }

  public virtual void Unlink(string shipmentNbr)
  {
    ((PXSelectBase<SOShipment>) this.Base.Document).Current = PXResultset<SOShipment>.op_Implicit(((PXSelectBase<SOShipment>) this.Base.Document).Search<SOShipment.shipmentNbr>((object) shipmentNbr, Array.Empty<object>()));
    using (PXTransactionScope transactionScope = new PXTransactionScope())
    {
      this.ClearPickingData(((PXSelectBase<SOShipment>) this.Base.Document).Current);
      this.UnlinkShipment(((PXSelectBase<SOShipment>) this.Base.Document).Current);
      transactionScope.Complete();
    }
  }

  protected virtual void ClearPickingData(SOShipment shipment)
  {
    if (shipment == null)
      return;
    Decimal? pickedQty = shipment.PickedQty;
    Decimal num = 0M;
    if (!(pickedQty.GetValueOrDefault() > num & pickedQty.HasValue))
      return;
    ((PXSelectBase<SOShipment>) this.Base.Document).Current = PXResultset<SOShipment>.op_Implicit(((PXSelectBase<SOShipment>) this.Base.Document).Search<SOShipment.shipmentNbr>((object) shipment.ShipmentNbr, Array.Empty<object>()));
    this.Base.CartSupportExt?.RemoveItemsFromCart();
    PXSelectBase<SOShipLine> transactions = (PXSelectBase<SOShipLine>) this.Base.Transactions;
    PXSelectBase<SOShipLineSplit> splits = (PXSelectBase<SOShipLineSplit>) this.Base.splits;
    foreach (PXResult<SOShipLine> pxResult1 in transactions.Select(Array.Empty<object>()))
    {
      SOShipLine soShipLine = PXResult<SOShipLine>.op_Implicit(pxResult1);
      transactions.Current = PXResultset<SOShipLine>.op_Implicit(transactions.Search<SOShipLine.shipmentNbr, SOShipLine.lineNbr>((object) ((PXSelectBase<SOShipment>) this.Base.Document).Current.ShipmentNbr, (object) soShipLine.LineNbr, Array.Empty<object>()));
      foreach (PXResult<SOShipLineSplit> pxResult2 in splits.Select(Array.Empty<object>()))
      {
        SOShipLineSplit soShipLineSplit = PXResult<SOShipLineSplit>.op_Implicit(pxResult2);
        splits.Current = PXResultset<SOShipLineSplit>.op_Implicit(splits.Search<SOShipLineSplit.shipmentNbr, SOShipLineSplit.lineNbr, SOShipLineSplit.splitLineNbr>((object) ((PXSelectBase<SOShipment>) this.Base.Document).Current.ShipmentNbr, (object) soShipLine.LineNbr, (object) soShipLineSplit.SplitLineNbr, Array.Empty<object>()));
        splits.SetValueExt<SOShipLineSplit.pickedQty>(splits.Current, (object) 0M);
        splits.SetValueExt<SOShipLineSplit.basePickedQty>(splits.Current, (object) 0M);
        splits.UpdateCurrent();
      }
      transactions.Current.BasePickedQty = new Decimal?(0M);
      transactions.Current.PickedQty = new Decimal?(0M);
      GraphHelper.MarkUpdated(((PXSelectBase) transactions).Cache, (object) transactions.Current, true);
    }
    ((PXSelectBase<SOShipment>) this.Base.Document).SetValueExt<SOShipment.picked>(((PXSelectBase<SOShipment>) this.Base.Document).Current, (object) false);
    ((PXSelectBase<SOShipment>) this.Base.Document).SetValueExt<SOShipment.pickedQty>(((PXSelectBase<SOShipment>) this.Base.Document).Current, (object) 0M);
    ((PXSelectBase<SOShipment>) this.Base.Document).SetValueExt<SOShipment.pickedViaWorksheet>(((PXSelectBase<SOShipment>) this.Base.Document).Current, (object) false);
    ((PXSelectBase<SOShipment>) this.Base.Document).UpdateCurrent();
    ((PXAction) this.Base.Save).Press();
  }

  protected virtual void UnlinkShipment(SOShipment shipment)
  {
    if (shipment == null || shipment.CurrentWorksheetNbr == null)
      return;
    SOPickingWorksheetReview instance = PXGraph.CreateInstance<SOPickingWorksheetReview>();
    ((PXGraph) instance).SelectTimeStamp();
    SOPickingWorksheet worksheet = SOPickingWorksheet.PK.Find((PXGraph) instance, shipment.CurrentWorksheetNbr, (PKFindOptions) 1);
    if (worksheet == null)
      return;
    ((PXSelectBase<SOPickingWorksheet>) instance.worksheet).Current = worksheet;
    if (worksheet.WorksheetType == "SS")
    {
      ((PXSelectBase<SOPickingWorksheet>) instance.worksheet).Delete(worksheet);
    }
    else
    {
      if (worksheet.WorksheetType == "WV")
      {
        SOPickerToShipmentLink topFirst = PXSelectBase<SOPickerToShipmentLink, PXViewOf<SOPickerToShipmentLink>.BasedOn<SelectFromBase<SOPickerToShipmentLink, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<SOPickerToShipmentLink.worksheetNbr, Equal<P.AsString>>>>>.And<BqlOperand<SOPickerToShipmentLink.shipmentNbr, IBqlString>.IsEqual<P.AsString>>>>.Config>.Select((PXGraph) instance, new object[2]
        {
          (object) worksheet.WorksheetNbr,
          (object) shipment.ShipmentNbr
        }).TopFirst;
        if (topFirst != null)
        {
          ((PXSelectBase<SOPickerToShipmentLink>) instance.pickerShipments).Delete(topFirst);
          GraphHelper.EnsureCachePersistence<SOPickerToShipmentLink>((PXGraph) instance);
        }
      }
      ((PXSelectBase<SOShipment>) instance.shipments).Current = PXResultset<SOShipment>.op_Implicit(((PXSelectBase<SOShipment>) instance.shipments).Search<SOShipment.shipmentNbr>((object) ((PXSelectBase<SOShipment>) this.Base.Document).Current.ShipmentNbr, Array.Empty<object>()));
      ((PXSelectBase<SOShipment>) instance.shipments).SetValueExt<SOShipment.currentWorksheetNbr>(((PXSelectBase<SOShipment>) instance.shipments).Current, (object) null);
      ((PXSelectBase<SOShipment>) instance.shipments).UpdateCurrent();
      this.Base.TryCompleteWorksheet((PXGraph) instance, worksheet);
    }
    ((PXAction) instance.Save).Press();
  }

  public virtual bool CanUnlinkWorksheetFrom(SOShipment shipment)
  {
    int num1;
    if (shipment != null)
    {
      bool? confirmed = shipment.Confirmed;
      bool flag = false;
      if (confirmed.GetValueOrDefault() == flag & confirmed.HasValue)
      {
        num1 = shipment.CurrentWorksheetNbr != null ? 1 : 0;
        goto label_4;
      }
    }
    num1 = 0;
label_4:
    bool flag1 = num1 != 0;
    if (flag1)
    {
      SOPickingWorksheet pickingWorksheet = SOPickingWorksheet.PK.Find((PXGraph) this.Base, shipment.CurrentWorksheetNbr);
      int num2;
      if (flag1 && pickingWorksheet != null && (EnumerableExtensions.IsIn<string>(pickingWorksheet.Status, "P", "C", "L") || pickingWorksheet.WorksheetType == "SS" && pickingWorksheet.Status != "I"))
      {
        if (pickingWorksheet.WorksheetType == "SS")
        {
          SOPickPackShipSetup pickPackShipSetup = SOPickPackShipSetup.PK.Find((PXGraph) this.Base, ((PXGraph) this.Base).Accessinfo.BranchID);
          if ((pickPackShipSetup != null ? (pickPackShipSetup.IsPackOnly ? 1 : 0) : 0) != 0)
          {
            num2 = 1;
            goto label_11;
          }
        }
        num2 = !((IEnumerable<PXResult<SOShipLineSplit>>) PXSelectBase<SOShipLineSplit, PXViewOf<SOShipLineSplit>.BasedOn<SelectFromBase<SOShipLineSplit, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<SOShipLineSplit.shipmentNbr, Equal<P.AsString>>>>>.And<BqlOperand<SOShipLineSplit.packedQty, IBqlDecimal>.IsNotEqual<decimal0>>>>.Config>.Select((PXGraph) this.Base, new object[1]
        {
          (object) shipment.ShipmentNbr
        })).AsEnumerable<PXResult<SOShipLineSplit>>().Any<PXResult<SOShipLineSplit>>() ? 1 : 0;
      }
      else
        num2 = 0;
label_11:
      flag1 = num2 != 0;
    }
    return flag1;
  }

  protected virtual void _(Events.RowSelected<SOShipment> e)
  {
    ((PXAction) this.UnlinkFromWorksheet).SetEnabled(EnumerableExtensions.IsIn<string>(e.Row.With<SOShipment, SOPickingWorksheet>((Func<SOShipment, SOPickingWorksheet>) (sh => SOPickingWorksheet.PK.Find((PXGraph) this.Base, sh.CurrentWorksheetNbr))).With<SOPickingWorksheet, string>((Func<SOPickingWorksheet, string>) (ws => ws.WorksheetType)), "WV", "BT") && this.CanUnlinkWorksheetFrom(e.Row));
    ((PXAction) this.UnlinkFromWorksheet).SetVisible(PXAccess.FeatureInstalled<FeaturesSet.wMSAdvancedPicking>());
  }

  /// Overrides <see cref="M:PX.Objects.SO.SOShipmentEntry.CorrectShipment(PX.Objects.SO.Models.CorrectShipmentArgs)" />
  [PXOverride]
  public virtual void CorrectShipment(
    CorrectShipmentArgs args,
    Action<CorrectShipmentArgs> base_CorrectShipment)
  {
    if (!string.IsNullOrEmpty(args.Shipment?.CurrentWorksheetNbr))
      this.Unlink(args.Shipment.ShipmentNbr);
    base_CorrectShipment(args);
  }

  public class WorkflowChanges : PXGraphExtension<SOShipmentEntry_Workflow, SOShipmentEntry>
  {
    public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.wMSAdvancedPicking>();

    public virtual void Configure(PXScreenConfiguration config)
    {
      SOShipmentEntryUnlinkWorksheetExt.WorkflowChanges.Configure(config.GetScreenConfigurationContext<SOShipmentEntry, SOShipment>());
    }

    protected static void Configure(
      WorkflowContext<SOShipmentEntry, SOShipment> context)
    {
      if (!SOShipmentEntryUnlinkWorksheetExt.WorkflowChanges.IsActive())
        return;
      BoundedTo<SOShipmentEntry, SOShipment>.ActionCategory.IConfigured pickListCategory = PickListActionCategory.Get(context);
      context.UpdateScreenConfigurationFor((Func<BoundedTo<SOShipmentEntry, SOShipment>.ScreenConfiguration.ConfiguratorScreen, BoundedTo<SOShipmentEntry, SOShipment>.ScreenConfiguration.ConfiguratorScreen>) (screen => screen.WithActions((Action<BoundedTo<SOShipmentEntry, SOShipment>.ActionDefinition.ContainerAdjusterActions>) (actions => actions.Add<SOShipmentEntryUnlinkWorksheetExt>((Expression<Func<SOShipmentEntryUnlinkWorksheetExt, PXAction<SOShipment>>>) (g => g.UnlinkFromWorksheet), (Func<BoundedTo<SOShipmentEntry, SOShipment>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<SOShipmentEntry, SOShipment>.ActionDefinition.IConfigured>) (a => (BoundedTo<SOShipmentEntry, SOShipment>.ActionDefinition.IConfigured) a.WithCategory(pickListCategory)))))));
    }
  }
}
