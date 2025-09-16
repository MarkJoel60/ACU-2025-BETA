// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.EquipmentTimecardPrimary
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.EP;

public class EquipmentTimecardPrimary : PXGraph<
#nullable disable
EquipmentTimecardPrimary>
{
  [PXFilterable(new Type[] {})]
  public PXSelectJoin<EPEquipmentTimeCard, LeftJoin<EPEquipmentTimeCardSpentTotals, On<EPEquipmentTimeCardSpentTotals.timeCardCD, Equal<EPEquipmentTimeCard.timeCardCD>>, LeftJoin<EPEquipmentTimeCardBillableTotals, On<EPEquipmentTimeCardBillableTotals.timeCardCD, Equal<EPEquipmentTimeCard.timeCardCD>>, LeftJoin<EquipmentTimecardPrimary.EPEquipmentTimeCardEx, On<EPEquipmentTimeCard.timeCardCD, Equal<EquipmentTimecardPrimary.EPEquipmentTimeCardEx.origTimeCardCD>>>>>, Where<EquipmentTimecardPrimary.EPEquipmentTimeCardEx.timeCardCD, IsNull>, OrderBy<Desc<EPEquipmentTimeCard.timeCardCD>>> Items;
  public PXAction<EPEquipmentTimeCard> createDetail;
  public PXAction<EPEquipmentTimeCard> updateDetail;
  public PXAction<EPEquipmentTimeCard> deleteDetail;

  public EquipmentTimecardPrimary()
  {
    ((PXSelectBase) this.Items).AllowDelete = false;
    ((PXSelectBase) this.Items).AllowInsert = false;
    ((PXSelectBase) this.Items).AllowUpdate = false;
  }

  [PXUIField(DisplayName = "Create")]
  [PXButton(Tooltip = "Add New Timecard", ImageKey = "AddNew")]
  [PXEntryScreenRights(typeof (EPTimeCard), "Insert")]
  protected virtual void CreateDetail()
  {
    using (new PXPreserveScope())
    {
      EquipmentTimeCardMaint instance = (EquipmentTimeCardMaint) PXGraph.CreateInstance(typeof (EquipmentTimeCardMaint));
      ((PXGraph) instance).Clear((PXClearOption) 3);
      ((PXSelectBase<EPEquipmentTimeCard>) instance.Document).Insert();
      PXRedirectHelper.TryRedirect((PXGraph) instance, (PXRedirectHelper.WindowMode) 4);
    }
  }

  [PXUIField(DisplayName = "Update")]
  [PXButton(Tooltip = "Edit Timecard", ImageKey = "RecordEdit")]
  protected virtual void UpdateDetail()
  {
    if (((PXSelectBase<EPEquipmentTimeCard>) this.Items).Current == null)
      return;
    PXRedirectHelper.TryRedirect((PXGraph) this, (object) ((PXSelectBase<EPEquipmentTimeCard>) this.Items).Current, (PXRedirectHelper.WindowMode) 4);
  }

  [PXUIField(DisplayName = "Delete")]
  [PXDeleteButton(Tooltip = "Delete Timecard")]
  [PXEntryScreenRights(typeof (EPEquipmentTimeCard))]
  protected void DeleteDetail()
  {
    if (((PXSelectBase<EPEquipmentTimeCard>) this.Items).Current == null)
      return;
    using (new PXPreserveScope())
    {
      EquipmentTimeCardMaint instance = (EquipmentTimeCardMaint) PXGraph.CreateInstance(typeof (EquipmentTimeCardMaint));
      ((PXGraph) instance).Clear((PXClearOption) 3);
      ((PXSelectBase<EPEquipmentTimeCard>) instance.Document).Current = PXResultset<EPEquipmentTimeCard>.op_Implicit(((PXSelectBase<EPEquipmentTimeCard>) instance.Document).Search<EPEquipmentTimeCard.timeCardCD>((object) ((PXSelectBase<EPEquipmentTimeCard>) this.Items).Current.TimeCardCD, Array.Empty<object>()));
      ((PXAction) instance.Delete).Press();
    }
  }

  public class EPEquipmentTimeCardEx : EPEquipmentTimeCard
  {
    public new abstract class timeCardCD : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      EquipmentTimecardPrimary.EPEquipmentTimeCardEx.timeCardCD>
    {
    }

    public new abstract class origTimeCardCD : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      EquipmentTimecardPrimary.EPEquipmentTimeCardEx.origTimeCardCD>
    {
    }
  }
}
