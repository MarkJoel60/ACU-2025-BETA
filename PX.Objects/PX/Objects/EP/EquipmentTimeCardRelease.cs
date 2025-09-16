// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.EquipmentTimeCardRelease
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.GL;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.EP;

[TableDashboardType]
[Serializable]
public class EquipmentTimeCardRelease : PXGraph<EquipmentTimeCardRelease>
{
  [PXViewName("Equipment Time Card")]
  [PXFilterable(new Type[] {})]
  public PXProcessingJoin<EPEquipmentTimeCard, LeftJoin<EPApproval, On<EPApproval.refNoteID, Equal<EPEquipmentTimeCard.noteID>>>, Where<EPEquipmentTimeCard.isApproved, Equal<True>, And<EPEquipmentTimeCard.isReleased, Equal<False>>>, OrderBy<Asc<EPEquipmentTimeCard.timeCardCD>>> FilteredItems;
  public PXSetup<EPSetup> Setup;
  public PXCancel<EPEquipmentTimeCard> Cancel;

  [PXInt]
  [PXUIField(DisplayName = "Setup", Enabled = false)]
  protected virtual void EPEquipmentTimeCard_TimeSetupCalc_CacheAttached(PXCache sender)
  {
  }

  [PXInt]
  [PXUIField(DisplayName = "Billable Setup", Enabled = false)]
  protected virtual void EPEquipmentTimeCard_TimeBillableSetupCalc_CacheAttached(PXCache sender)
  {
  }

  public EquipmentTimeCardRelease()
  {
    ((PXProcessing<EPEquipmentTimeCard>) this.FilteredItems).SetProcessCaption("Release");
    ((PXProcessing<EPEquipmentTimeCard>) this.FilteredItems).SetProcessAllCaption("Release All");
    ((PXProcessingBase<EPEquipmentTimeCard>) this.FilteredItems).SetSelected<EPTimeCard.selected>();
    // ISSUE: method pointer
    ((PXProcessingBase<EPEquipmentTimeCard>) this.FilteredItems).SetProcessDelegate(new PXProcessingBase<EPEquipmentTimeCard>.ProcessListDelegate((object) null, __methodptr(Release)));
    ((PXGraph) this).Actions.Move("Process", nameof (Cancel));
  }

  protected virtual void EPEquipmentTimeCard_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    if (!(e.Row is EPEquipmentTimeCard row))
      return;
    this.RecalculateTotals(row);
  }

  protected virtual void RecalculateTotals(EPEquipmentTimeCard timecard)
  {
    if (timecard == null)
      throw new ArgumentNullException();
    PXSelectBase<EPEquipmentDetail> pxSelectBase = (PXSelectBase<EPEquipmentDetail>) new PXSelect<EPEquipmentDetail, Where<EPEquipmentDetail.timeCardCD, Equal<Required<EPEquipmentTimeCard.timeCardCD>>>>((PXGraph) this);
    int num1 = 0;
    int num2 = 0;
    int num3 = 0;
    int num4 = 0;
    int num5 = 0;
    int num6 = 0;
    foreach (PXResult<EPEquipmentDetail> pxResult in pxSelectBase.Select(new object[1]
    {
      (object) timecard.TimeCardCD
    }))
    {
      EPEquipmentDetail epEquipmentDetail = PXResult<EPEquipmentDetail>.op_Implicit(pxResult);
      int num7 = num1;
      int? nullable = epEquipmentDetail.SetupTime;
      int valueOrDefault1 = nullable.GetValueOrDefault();
      num1 = num7 + valueOrDefault1;
      int num8 = num2;
      nullable = epEquipmentDetail.RunTime;
      int valueOrDefault2 = nullable.GetValueOrDefault();
      num2 = num8 + valueOrDefault2;
      int num9 = num3;
      nullable = epEquipmentDetail.SuspendTime;
      int valueOrDefault3 = nullable.GetValueOrDefault();
      num3 = num9 + valueOrDefault3;
      if (epEquipmentDetail.IsBillable.GetValueOrDefault())
      {
        int num10 = num4;
        nullable = epEquipmentDetail.SetupTime;
        int valueOrDefault4 = nullable.GetValueOrDefault();
        num4 = num10 + valueOrDefault4;
        int num11 = num5;
        nullable = epEquipmentDetail.RunTime;
        int valueOrDefault5 = nullable.GetValueOrDefault();
        num5 = num11 + valueOrDefault5;
        int num12 = num6;
        nullable = epEquipmentDetail.SuspendTime;
        int valueOrDefault6 = nullable.GetValueOrDefault();
        num6 = num12 + valueOrDefault6;
      }
    }
    timecard.TimeSetupCalc = new int?(num1);
    timecard.TimeRunCalc = new int?(num2);
    timecard.TimeSuspendCalc = new int?(num3);
    timecard.TimeBillableSetupCalc = new int?(num4);
    timecard.TimeBillableRunCalc = new int?(num5);
    timecard.TimeBillableSuspendCalc = new int?(num6);
    List<EPEquipmentDetail> epEquipmentDetailList = new List<EPEquipmentDetail>();
    foreach (PXResult<EPEquipmentDetail> pxResult in pxSelectBase.Select(new object[1]
    {
      (object) timecard.TimeCardCD
    }))
    {
      EPEquipmentDetail epEquipmentDetail = PXResult<EPEquipmentDetail>.op_Implicit(pxResult);
      epEquipmentDetailList.Add(epEquipmentDetail);
    }
  }

  public static void Release(List<EPEquipmentTimeCard> timeCards)
  {
    EquipmentTimeCardMaint instance = PXGraph.CreateInstance<EquipmentTimeCardMaint>();
    foreach (EPEquipmentTimeCard timeCard in timeCards)
    {
      try
      {
        PXProcessing.SetCurrentItem((object) timeCard);
        ((PXGraph) instance).Clear();
        ((PXSelectBase<EPEquipmentTimeCard>) instance.Document).Current = PXResultset<EPEquipmentTimeCard>.op_Implicit(((PXSelectBase<EPEquipmentTimeCard>) instance.Document).Search<EPEquipmentTimeCard.timeCardCD>((object) timeCard.TimeCardCD, Array.Empty<object>()));
        ((PXAction) instance.release).Press();
        PXProcessing.SetProcessed();
      }
      catch (Exception ex)
      {
        PXProcessing.SetError(ex);
      }
    }
  }
}
