// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.EquipmentMaint
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.PM;

#nullable disable
namespace PX.Objects.EP;

public class EquipmentMaint : PXGraph<EquipmentMaint, EPEquipment>
{
  public PXSelect<EPEquipment> Equipment;
  public PXSelect<EPEquipment, Where<EPEquipment.equipmentID, Equal<Current<EPEquipment.equipmentID>>>> EquipmentProperties;
  public PXSelectJoin<EPEquipmentRate, InnerJoin<PMProject, On<PMProject.contractID, Equal<EPEquipmentRate.projectID>>>, Where<EPEquipmentRate.equipmentID, Equal<Current<EPEquipment.equipmentID>>>> Rates;
  [PXViewName("Equipment Answers")]
  public CRAttributeList<EPEquipment> Answers;

  [PXDBIdentity]
  protected virtual void _(PX.Data.Events.CacheAttached<EPEquipment.equipmentID> _)
  {
  }

  [PXDBString(15, IsKey = true, IsUnicode = true)]
  [PXDefault]
  [PXUIField(DisplayName = "Equipment ID")]
  [PXSelector(typeof (Search<EPEquipment.equipmentCD>), new System.Type[] {typeof (EPEquipment.equipmentCD), typeof (EPEquipment.description), typeof (EPEquipment.isActive)})]
  protected virtual void _(PX.Data.Events.CacheAttached<EPEquipment.equipmentCD> _)
  {
  }

  protected virtual void _(PX.Data.Events.RowSelected<EPEquipment> e)
  {
    ((PXSelectBase) this.Rates).AllowSelect = PXAccess.FeatureInstalled<FeaturesSet.projectAccounting>();
  }

  protected virtual void _(PX.Data.Events.RowDeleting<EPEquipment> e)
  {
    if (e.Row == null)
      return;
    EPEquipmentTimeCard equipmentTimeCard = PXResultset<EPEquipmentTimeCard>.op_Implicit(PXSelectBase<EPEquipmentTimeCard, PXSelect<EPEquipmentTimeCard, Where<EPEquipmentTimeCard.equipmentID, Equal<Required<EPEquipmentTimeCard.equipmentID>>>>.Config>.SelectSingleBound((PXGraph) this, (object[]) null, new object[1]
    {
      (object) e.Row.EquipmentID
    }));
    if (equipmentTimeCard != null)
    {
      e.Cancel = true;
      throw new PXException(PXMessages.LocalizeFormatNoPrefix("The {0} equipment cannot be deleted because it has been used in at least one equipment time card: {1}", new object[2]
      {
        (object) e.Row.EquipmentCD?.Trim(),
        (object) equipmentTimeCard.TimeCardCD?.Trim()
      }));
    }
  }

  protected virtual void _(PX.Data.Events.FieldUpdated<EPEquipment.runRateItemID> e)
  {
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<EPEquipment.runRateItemID>>) e).Cache.SetDefaultExt<EPEquipment.runRate>(e.Row);
  }

  protected virtual void _(PX.Data.Events.FieldUpdated<EPEquipment.setupRateItemID> e)
  {
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<EPEquipment.setupRateItemID>>) e).Cache.SetDefaultExt<EPEquipment.setupRate>(e.Row);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<EPEquipment.suspendRateItemID> e)
  {
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<EPEquipment.suspendRateItemID>>) e).Cache.SetDefaultExt<EPEquipment.suspendRate>(e.Row);
  }
}
