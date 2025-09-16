// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.GraphExtensions.ShowPickListPopup
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.IN;
using System;
using System.Collections;

#nullable enable
namespace PX.Objects.SO.GraphExtensions;

public static class ShowPickListPopup
{
  public abstract class On<TGraph, TPrimary>
    where TGraph : 
    #nullable disable
    PXGraph
    where TPrimary : class, IBqlTable, new()
  {
    public abstract class FilteredBy<TWhere> : PXGraphExtension<TGraph> where TWhere : IBqlWhere, new()
    {
      public FbqlSelect<SelectFromBase<SOPickingJob, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<SOPicker>.On<SOPickingJob.FK.Picker>>, FbqlJoins.Inner<SOPickingWorksheet>.On<SOPicker.FK.Worksheet>>>.Where<TWhere>, SOPickingJob>.View PickListHeader;
      public FbqlSelect<SelectFromBase<SOPickerListEntry, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<SOPicker>.On<SOPickerListEntry.FK.Picker>>, FbqlJoins.Inner<SOPickingWorksheet>.On<SOPicker.FK.Worksheet>>, FbqlJoins.Inner<SOPickingJob>.On<SOPickingJob.FK.Picker>>, FbqlJoins.Inner<INLocation>.On<SOPickerListEntry.FK.Location>>, FbqlJoins.Inner<PX.Objects.IN.InventoryItem>.On<SOPickerListEntry.FK.InventoryItem>>>.Where<TWhere>.Order<By<BqlField<
      #nullable enable
      INLocation.pathPriority, IBqlInt>.Asc, 
      #nullable disable
      BqlField<
      #nullable enable
      INLocation.locationCD, IBqlString>.Asc, 
      #nullable disable
      BqlField<
      #nullable enable
      PX.Objects.IN.InventoryItem.inventoryCD, IBqlString>.Asc, 
      #nullable disable
      BqlField<
      #nullable enable
      SOPickerListEntry.lotSerialNbr, IBqlString>.Asc>>, 
      #nullable disable
      SOPickerListEntry>.View PickListEntries;
      public PXAction<TPrimary> ShowPickList;
      public PXAction<TPrimary> DeletePickList;
      public PXAction<TPrimary> ViewPickListSource;

      protected virtual bool IsPickListExternalViewMode => true;

      protected virtual bool CanDeletePickList(TPrimary primaryRow)
      {
        return !this.IsPickListExternalViewMode;
      }

      protected virtual void PerformPickListDeletion() => throw new NotImplementedException();

      protected virtual void _(PX.Data.Events.RowSelected<TPrimary> e)
      {
        ((PXAction) this.DeletePickList).SetVisible(!this.IsPickListExternalViewMode);
        ((PXAction) this.DeletePickList).SetEnabled(this.CanDeletePickList(e.Row));
        ((PXAction) this.ViewPickListSource).SetVisible(this.IsPickListExternalViewMode);
        ((PXAction) this.ViewPickListSource).SetEnabled(this.IsPickListExternalViewMode);
        ((PXAction) this.ShowPickList).SetEnabled((object) e.Row != null && ((PXSelectBase<SOPickerListEntry>) this.PickListEntries).SelectSingle(Array.Empty<object>()) != null);
      }

      [PXButton(CommitChanges = true)]
      [PXUIField]
      protected virtual void showPickList()
      {
        ((PXSelectBase<SOPickerListEntry>) this.PickListEntries).AskExt();
      }

      [PXButton]
      [PXUIField(DisplayName = "Delete Pick List")]
      protected virtual IEnumerable deletePickList(PXAdapter adapter)
      {
        this.PerformPickListDeletion();
        return adapter.Get();
      }

      [PXButton(CommitChanges = true, DisplayOnMainToolbar = false)]
      [PXUIField]
      protected virtual void viewPickListSource()
      {
        SOPickingWorksheet parent = (SOPickingWorksheet) PrimaryKeyOf<SOPickingWorksheet>.By<SOPickingWorksheet.worksheetNbr>.ForeignKeyOf<SOPickerListEntry>.By<SOPickerListEntry.worksheetNbr>.FindParent((PXGraph) this.Base, (SOPickerListEntry.worksheetNbr) ((PXSelectBase<SOPickerListEntry>) this.PickListEntries).Current, (PKFindOptions) 0);
        if (parent == null)
          return;
        if (parent.WorksheetType == "SS")
        {
          SOShipmentEntry instance = PXGraph.CreateInstance<SOShipmentEntry>();
          ((PXSelectBase<PX.Objects.SO.SOShipment>) instance.Document).Current = PXResultset<PX.Objects.SO.SOShipment>.op_Implicit(((PXSelectBase<PX.Objects.SO.SOShipment>) instance.Document).Search<PX.Objects.SO.SOShipment.shipmentNbr>((object) parent.SingleShipmentNbr, Array.Empty<object>()));
          throw new PXRedirectRequiredException((PXGraph) instance, true, "");
        }
        SOPickingWorksheetReview instance1 = PXGraph.CreateInstance<SOPickingWorksheetReview>();
        ((PXSelectBase<SOPickingWorksheet>) instance1.worksheet).Current = PXResultset<SOPickingWorksheet>.op_Implicit(((PXSelectBase<SOPickingWorksheet>) instance1.worksheet).Search<SOPickingWorksheet.worksheetNbr>((object) parent.WorksheetNbr, Array.Empty<object>()));
        throw new PXRedirectRequiredException((PXGraph) instance1, true, "");
      }
    }
  }

  [PXLocalizable]
  public abstract class Msg
  {
    public const string DeleteConfirmation = "The current Pick List record will be deleted.";
  }
}
