// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.EquipmentTimeCardMaint
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.WorkflowAPI;
using PX.Objects.CA;
using PX.Objects.IN;
using PX.Objects.PM;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;

#nullable enable
namespace PX.Objects.EP;

[Serializable]
public class EquipmentTimeCardMaint : PXGraph<
#nullable disable
EquipmentTimeCardMaint, EPEquipmentTimeCard>
{
  [PXHidden]
  public PXSetup<EPSetup> EpSetup;
  [PXViewName("Document")]
  public PXSelect<EPEquipmentTimeCard> Document;
  public PXSelect<EPEquipmentTimeCard, Where<EPEquipmentTimeCard.timeCardCD, Equal<Current<EPEquipmentTimeCard.timeCardCD>>>> DocumentProperties;
  [PXViewName("Equipment Time Card Summary")]
  public PXSelect<EPEquipmentSummary, Where<EPEquipmentSummary.timeCardCD, Equal<Current<EPEquipmentTimeCard.timeCardCD>>>, OrderBy<Asc<EPEquipmentSummary.lineNbr>>> Summary;
  [PXViewName("Equipment Time Card Detail")]
  public PXSelect<EPEquipmentDetail, Where<EPEquipmentDetail.timeCardCD, Equal<Current<EPEquipmentTimeCard.timeCardCD>>>, OrderBy<Asc<EPEquipmentDetail.lineNbr>>> Details;
  public PXSetup<EPSetup> Setup;
  [PXViewName("Approval")]
  public EPApprovalAutomation<EPEquipmentTimeCard, EPEquipmentTimeCard.isApproved, EPEquipmentTimeCard.isRejected, EPEquipmentTimeCard.isHold, EPSetupEquipmentTimeCardApproval> Approval;
  public PXAction<EPEquipmentTimeCard> submit;
  public PXAction<EPEquipmentTimeCard> edit;
  public PXAction<EPEquipmentTimeCard> release;
  public PXAction<EPEquipmentTimeCard> correct;
  public PXAction<EPEquipmentTimeCard> preloadFromPreviousTimecard;
  public PXAction<EPEquipmentTimeCard> viewOrigTimecard;
  public PXWorkflowEventHandler<EPEquipmentTimeCard> OnUpdateStatus;
  /// <summary>
  /// When True detail row is not updated when a summary record is modified.
  /// </summary>
  protected bool dontSyncDetails;
  /// <summary>
  /// When True Summary records are not updated as a result of a detail row update.
  /// </summary>
  protected bool dontSyncSummary;

  public EquipmentTimeCardMaint()
  {
    if (((PXSelectBase<EPSetup>) this.EpSetup).Current.EquipmentTimeCardNumberingID == null)
      throw new PXSetupNotEnteredException("The required configuration data is not entered on the {0} form.", typeof (EPSetup), new object[1]
      {
        (object) typeof (EPSetup).Name
      });
  }

  public virtual bool CanClipboardCopyPaste() => false;

  /// <summary>Gets the source for the generated PMTran.AccountID</summary>
  public string ExpenseAccountSource
  {
    get
    {
      string expenseAccountSource = "I";
      PMSetup pmSetup = PXResultset<PMSetup>.op_Implicit(PXSelectBase<PMSetup, PXSelect<PMSetup>.Config>.Select((PXGraph) this, Array.Empty<object>()));
      if (pmSetup != null && !string.IsNullOrEmpty(pmSetup.ExpenseAccountSource))
        expenseAccountSource = pmSetup.ExpenseAccountSource;
      return expenseAccountSource;
    }
  }

  public string ExpenseSubMask
  {
    get
    {
      string expenseSubMask = (string) null;
      PMSetup pmSetup = PXResultset<PMSetup>.op_Implicit(PXSelectBase<PMSetup, PXSelect<PMSetup>.Config>.Select((PXGraph) this, Array.Empty<object>()));
      if (pmSetup != null && !string.IsNullOrEmpty(pmSetup.ExpenseSubMask))
        expenseSubMask = pmSetup.ExpenseSubMask;
      return expenseSubMask;
    }
  }

  public string ExpenseAccrualAccountSource
  {
    get
    {
      string accrualAccountSource = "I";
      PMSetup pmSetup = PXResultset<PMSetup>.op_Implicit(PXSelectBase<PMSetup, PXSelect<PMSetup>.Config>.Select((PXGraph) this, Array.Empty<object>()));
      if (pmSetup != null && !string.IsNullOrEmpty(pmSetup.ExpenseAccountSource))
        accrualAccountSource = pmSetup.ExpenseAccrualAccountSource;
      return accrualAccountSource;
    }
  }

  public string ExpenseAccrualSubMask
  {
    get
    {
      string expenseAccrualSubMask = (string) null;
      PMSetup pmSetup = PXResultset<PMSetup>.op_Implicit(PXSelectBase<PMSetup, PXSelect<PMSetup>.Config>.Select((PXGraph) this, Array.Empty<object>()));
      if (pmSetup != null && !string.IsNullOrEmpty(pmSetup.ExpenseAccrualSubMask))
        expenseAccrualSubMask = pmSetup.ExpenseAccrualSubMask;
      return expenseAccrualSubMask;
    }
  }

  public string ActivityTimeUnit
  {
    get
    {
      string activityTimeUnit = "MINUTE";
      EPSetup epSetup = PXResultset<EPSetup>.op_Implicit(PXSelectBase<EPSetup, PXSelect<EPSetup>.Config>.Select((PXGraph) this, Array.Empty<object>()));
      if (epSetup != null && !string.IsNullOrEmpty(epSetup.ActivityTimeUnit))
        activityTimeUnit = epSetup.ActivityTimeUnit;
      return activityTimeUnit;
    }
  }

  public virtual IEnumerable summary()
  {
    EquipmentTimeCardMaint equipmentTimeCardMaint = this;
    EPEquipmentTimeCard timecard = ((PXSelectBase<EPEquipmentTimeCard>) equipmentTimeCardMaint.Document).Current;
    timecard.SunTotal = timecard.MonTotal = timecard.TueTotal = timecard.WedTotal = timecard.ThuTotal = timecard.FriTotal = timecard.SatTotal = new int?(0);
    foreach (PXResult<EPEquipmentSummary> pxResult in PXSelectBase<EPEquipmentSummary, PXSelect<EPEquipmentSummary, Where<EPEquipmentSummary.timeCardCD, Equal<Current<EPEquipmentTimeCard.timeCardCD>>>, OrderBy<Asc<EPEquipmentSummary.lineNbr>>>.Config>.Select((PXGraph) equipmentTimeCardMaint, Array.Empty<object>()))
    {
      EPEquipmentSummary equipmentSummary = PXResult<EPEquipmentSummary>.op_Implicit(pxResult);
      EPEquipmentTimeCard equipmentTimeCard1 = timecard;
      int? nullable1 = equipmentTimeCard1.SunTotal;
      int? nullable2 = equipmentSummary.Sun;
      int valueOrDefault1 = nullable2.GetValueOrDefault();
      int? nullable3;
      if (!nullable1.HasValue)
      {
        nullable2 = new int?();
        nullable3 = nullable2;
      }
      else
        nullable3 = new int?(nullable1.GetValueOrDefault() + valueOrDefault1);
      equipmentTimeCard1.SunTotal = nullable3;
      EPEquipmentTimeCard equipmentTimeCard2 = timecard;
      nullable1 = equipmentTimeCard2.MonTotal;
      nullable2 = equipmentSummary.Mon;
      int valueOrDefault2 = nullable2.GetValueOrDefault();
      int? nullable4;
      if (!nullable1.HasValue)
      {
        nullable2 = new int?();
        nullable4 = nullable2;
      }
      else
        nullable4 = new int?(nullable1.GetValueOrDefault() + valueOrDefault2);
      equipmentTimeCard2.MonTotal = nullable4;
      EPEquipmentTimeCard equipmentTimeCard3 = timecard;
      nullable1 = equipmentTimeCard3.TueTotal;
      nullable2 = equipmentSummary.Tue;
      int valueOrDefault3 = nullable2.GetValueOrDefault();
      int? nullable5;
      if (!nullable1.HasValue)
      {
        nullable2 = new int?();
        nullable5 = nullable2;
      }
      else
        nullable5 = new int?(nullable1.GetValueOrDefault() + valueOrDefault3);
      equipmentTimeCard3.TueTotal = nullable5;
      EPEquipmentTimeCard equipmentTimeCard4 = timecard;
      nullable1 = equipmentTimeCard4.WedTotal;
      nullable2 = equipmentSummary.Wed;
      int valueOrDefault4 = nullable2.GetValueOrDefault();
      int? nullable6;
      if (!nullable1.HasValue)
      {
        nullable2 = new int?();
        nullable6 = nullable2;
      }
      else
        nullable6 = new int?(nullable1.GetValueOrDefault() + valueOrDefault4);
      equipmentTimeCard4.WedTotal = nullable6;
      EPEquipmentTimeCard equipmentTimeCard5 = timecard;
      nullable1 = equipmentTimeCard5.ThuTotal;
      nullable2 = equipmentSummary.Thu;
      int valueOrDefault5 = nullable2.GetValueOrDefault();
      int? nullable7;
      if (!nullable1.HasValue)
      {
        nullable2 = new int?();
        nullable7 = nullable2;
      }
      else
        nullable7 = new int?(nullable1.GetValueOrDefault() + valueOrDefault5);
      equipmentTimeCard5.ThuTotal = nullable7;
      EPEquipmentTimeCard equipmentTimeCard6 = timecard;
      nullable1 = equipmentTimeCard6.FriTotal;
      nullable2 = equipmentSummary.Fri;
      int valueOrDefault6 = nullable2.GetValueOrDefault();
      int? nullable8;
      if (!nullable1.HasValue)
      {
        nullable2 = new int?();
        nullable8 = nullable2;
      }
      else
        nullable8 = new int?(nullable1.GetValueOrDefault() + valueOrDefault6);
      equipmentTimeCard6.FriTotal = nullable8;
      EPEquipmentTimeCard equipmentTimeCard7 = timecard;
      nullable1 = equipmentTimeCard7.SatTotal;
      nullable2 = equipmentSummary.Sat;
      int valueOrDefault7 = nullable2.GetValueOrDefault();
      int? nullable9;
      if (!nullable1.HasValue)
      {
        nullable2 = new int?();
        nullable9 = nullable2;
      }
      else
        nullable9 = new int?(nullable1.GetValueOrDefault() + valueOrDefault7);
      equipmentTimeCard7.SatTotal = nullable9;
      yield return (object) equipmentSummary;
    }
  }

  [PXButton(CommitChanges = true)]
  [PXUIField(DisplayName = "Submit")]
  protected virtual IEnumerable Submit(PXAdapter adapter) => adapter.Get();

  [PXButton(CommitChanges = true)]
  [PXUIField(DisplayName = "Hold")]
  protected virtual IEnumerable Edit(PXAdapter adapter) => adapter.Get();

  [PXUIField(DisplayName = "Release")]
  [PXButton]
  protected virtual IEnumerable Release(PXAdapter adapter)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    EquipmentTimeCardMaint.\u003C\u003Ec__DisplayClass25_0 cDisplayClass250 = new EquipmentTimeCardMaint.\u003C\u003Ec__DisplayClass25_0();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass250.\u003C\u003E4__this = this;
    // ISSUE: reference to a compiler-generated field
    cDisplayClass250.list = adapter.Get().Cast<EPEquipmentTimeCard>().Where<EPEquipmentTimeCard>((Func<EPEquipmentTimeCard, bool>) (item => !item.IsReleased.GetValueOrDefault())).ToList<EPEquipmentTimeCard>();
    // ISSUE: reference to a compiler-generated field
    if (!cDisplayClass250.list.Any<EPEquipmentTimeCard>())
      throw new PXException("This document is already released.");
    ((PXAction) this.Save).Press();
    // ISSUE: method pointer
    PXLongOperation.StartOperation((PXGraph) this, new PXToggleAsyncDelegate((object) cDisplayClass250, __methodptr(\u003CRelease\u003Eb__1)));
    // ISSUE: reference to a compiler-generated field
    return (IEnumerable) cDisplayClass250.list;
  }

  [PXUIField(DisplayName = "Correct")]
  [PXButton]
  protected virtual IEnumerable Correct(PXAdapter adapter)
  {
    if (((PXSelectBase<EPEquipmentTimeCard>) this.Document).Current == null)
      return adapter.Get();
    EPEquipmentTimeCard current = ((PXSelectBase<EPEquipmentTimeCard>) this.Document).Current;
    EPEquipmentTimeCard equipmentTimeCard = (EPEquipmentTimeCard) ((PXSelectBase) this.Document).Cache.Insert();
    equipmentTimeCard.EquipmentID = current.EquipmentID;
    equipmentTimeCard.WeekID = current.WeekID;
    equipmentTimeCard.OrigTimeCardCD = current.TimeCardCD;
    PXView view = ((PXSelectBase) this.Details).View;
    object[] objArray1 = new object[1]{ (object) current };
    object[] objArray2 = Array.Empty<object>();
    foreach (EPEquipmentDetail epEquipmentDetail in view.SelectMultiBound(objArray1, objArray2))
    {
      EPEquipmentDetail copy = PXCache<EPEquipmentDetail>.CreateCopy(epEquipmentDetail);
      copy.TimeCardCD = (string) null;
      copy.LineNbr = new int?();
      copy.OrigLineNbr = epEquipmentDetail.LineNbr;
      copy.NoteID = new Guid?();
      ((PXSelectBase<EPEquipmentDetail>) this.Details).Insert(copy);
    }
    return (IEnumerable) new EPEquipmentTimeCard[1]
    {
      equipmentTimeCard
    };
  }

  [PXUIField(DisplayName = "Preload from Previous Time Card")]
  [PXButton]
  protected virtual void PreloadFromPreviousTimecard()
  {
    if (((PXSelectBase<EPEquipmentTimeCard>) this.Document).Current == null)
      return;
    EPEquipmentTimeCard current = ((PXSelectBase<EPEquipmentTimeCard>) this.Document).Current;
    if ((current != null ? (!current.WeekID.HasValue ? 1 : 0) : 1) != 0)
      return;
    EPEquipmentTimeCard equipmentTimeCard = PXResultset<EPEquipmentTimeCard>.op_Implicit(PXSelectBase<EPEquipmentTimeCard, PXSelectReadonly<EPEquipmentTimeCard, Where<EPEquipmentTimeCard.equipmentID, Equal<Required<EPEquipmentTimeCard.equipmentID>>, And<EPEquipmentTimeCard.weekId, Less<Required<EPEquipmentTimeCard.weekId>>>>, OrderBy<Desc<EPEquipmentTimeCard.weekId>>>.Config>.SelectSingleBound((PXGraph) this, (object[]) null, new object[2]
    {
      (object) ((PXSelectBase<EPEquipmentTimeCard>) this.Document).Current.EquipmentID,
      (object) ((PXSelectBase<EPEquipmentTimeCard>) this.Document).Current.WeekID
    }));
    if (equipmentTimeCard == null)
      return;
    foreach (PXResult<EPEquipmentSummary> pxResult in ((PXSelectBase<EPEquipmentSummary>) new PXSelect<EPEquipmentSummary, Where<EPEquipmentSummary.timeCardCD, Equal<Required<EPEquipmentSummary.timeCardCD>>>>((PXGraph) this)).Select(new object[1]
    {
      (object) equipmentTimeCard.TimeCardCD
    }))
    {
      EPEquipmentSummary copy = PXCache<EPEquipmentSummary>.CreateCopy(PXResult<EPEquipmentSummary>.op_Implicit(pxResult));
      copy.TimeCardCD = (string) null;
      copy.LineNbr = new int?();
      copy.NoteID = new Guid?();
      ((PXSelectBase<EPEquipmentSummary>) this.Summary).Insert(copy);
    }
  }

  [PXUIField]
  [PXLookupButton]
  public virtual IEnumerable ViewOrigTimecard(PXAdapter adapter)
  {
    if (!string.IsNullOrEmpty(((PXSelectBase<EPEquipmentTimeCard>) this.Document).Current?.OrigTimeCardCD))
    {
      EPEquipmentTimeCard equipmentTimeCard = PXResultset<EPEquipmentTimeCard>.op_Implicit(((PXSelectBase<EPEquipmentTimeCard>) this.Document).Search<EPEquipmentTimeCard.timeCardCD>((object) ((PXSelectBase<EPEquipmentTimeCard>) this.Document).Current.OrigTimeCardCD, Array.Empty<object>()));
      if (equipmentTimeCard != null)
        PXRedirectHelper.TryRedirect((PXGraph) this, (object) equipmentTimeCard, (PXRedirectHelper.WindowMode) 0);
    }
    return adapter.Get();
  }

  [PXDefault(typeof (EPEquipmentTimeCard.createdDateTime))]
  [PXMergeAttributes]
  protected virtual void EPApproval_DocDate_CacheAttached(PXCache sender)
  {
  }

  [PXDefault(typeof (Search<EPEquipment.equipmentCD, Where<EPEquipment.equipmentID, Equal<Current<EPEquipmentTimeCard.equipmentID>>, And<EPEquipment.isActive, Equal<True>>>>))]
  [PXMergeAttributes]
  protected virtual void EPApproval_Descr_CacheAttached(PXCache sender)
  {
  }

  [PXDefault(typeof (Search<EPEmployee.bAccountID, Where<EPEmployee.userID, Equal<Current<EPEquipmentTimeCard.createdByID>>>>))]
  [PXMergeAttributes]
  protected virtual void EPApproval_BAccountID_CacheAttached(PXCache sender)
  {
  }

  protected virtual void EPEquipmentTimeCard_RowInserted(PXCache sender, PXRowInsertedEventArgs e)
  {
  }

  protected virtual void EPEquipmentTimeCard_RowInserting(PXCache sender, PXRowInsertingEventArgs e)
  {
    if (!(e.Row is EPEquipmentTimeCard row))
      return;
    int? nullable = row.WeekID;
    if (nullable.HasValue)
      return;
    try
    {
      nullable = this.GetCurrentDateWeekId();
      int num = nullable.Value;
      row.WeekID = this.HasDuplicate(row.EquipmentID, new int?(num), row.TimeCardCD) ? this.GetNextWeekID(row.EquipmentID) : new int?(num);
    }
    catch (PXException ex)
    {
      row.WeekID = new int?();
      sender.RaiseExceptionHandling<EPEquipmentTimeCard.weekId>((object) row, (object) null, (Exception) ex);
    }
  }

  protected virtual void EPEquipmentTimeCard_RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    EPEquipmentTimeCard row = e.Row as EPEquipmentTimeCard;
    EPEquipmentTimeCard oldRow = e.OldRow as EPEquipmentTimeCard;
    if (row == null || oldRow == null)
      return;
    int? nullable = row.WeekID;
    if (nullable.HasValue)
    {
      nullable = row.EquipmentID;
      int? equipmentId = oldRow.EquipmentID;
      if (nullable.GetValueOrDefault() == equipmentId.GetValueOrDefault() & nullable.HasValue == equipmentId.HasValue)
        return;
    }
    try
    {
      int num = this.GetCurrentDateWeekId().Value;
      row.WeekID = this.HasDuplicate(row.EquipmentID, new int?(num), row.TimeCardCD) ? this.GetNextWeekID(row.EquipmentID) : new int?(num);
    }
    catch (PXException ex)
    {
      row.WeekID = new int?();
      sender.RaiseExceptionHandling<EPEquipmentTimeCard.weekId>((object) row, (object) null, (Exception) ex);
    }
  }

  protected virtual void EPEquipmentTimeCard_RowDeleted(PXCache sender, PXRowDeletedEventArgs e)
  {
    if (PXResultset<EPEquipmentTimeCard>.op_Implicit(PXSelectBase<EPEquipmentTimeCard, PXSelect<EPEquipmentTimeCard, Where<EPEquipmentTimeCard.equipmentID, Equal<Current<EPEquipmentTimeCard.equipmentID>>, And<EPEquipmentTimeCard.weekId, Greater<Current<EPEquipmentTimeCard.weekId>>>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, Array.Empty<object>())) != null)
      throw new PXException("Since there exists a timecard for the future week you cannot delete this Time Card.");
    if (!(e.Row is EPEquipmentTimeCard row) || string.IsNullOrEmpty(row.OrigTimeCardCD))
      return;
    foreach (PXResult<EPEquipmentDetail> pxResult in ((PXSelectBase<EPEquipmentDetail>) new PXSelect<EPEquipmentDetail, Where<EPEquipmentDetail.timeCardCD, Equal<Current<EPEquipmentTimeCard.timeCardCD>>>>((PXGraph) this)).Select(Array.Empty<object>()))
      ((PXSelectBase<EPEquipmentDetail>) this.Details).Delete(PXResult<EPEquipmentDetail>.op_Implicit(pxResult));
  }

  protected virtual void EPEquipmentTimeCard_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    if (!(e.Row is EPEquipmentTimeCard row))
      return;
    bool? nullable1 = row.IsReleased;
    ((PXSelectBase) this.Document).Cache.AllowDelete = !nullable1.GetValueOrDefault();
    nullable1 = row.IsHold;
    int? nullable2;
    int num;
    if (nullable1.GetValueOrDefault())
    {
      nullable1 = row.IsApproved;
      if (!nullable1.GetValueOrDefault())
      {
        nullable1 = row.IsRejected;
        if (!nullable1.GetValueOrDefault())
        {
          nullable1 = row.IsReleased;
          if (!nullable1.GetValueOrDefault())
          {
            nullable2 = row.EquipmentID;
            num = nullable2.HasValue ? 1 : 0;
            goto label_7;
          }
        }
      }
    }
    num = 0;
label_7:
    bool flag1 = num != 0;
    ((PXSelectBase) this.Details).Cache.AllowInsert = flag1;
    ((PXSelectBase) this.Details).Cache.AllowUpdate = flag1;
    ((PXSelectBase) this.Details).Cache.AllowDelete = flag1;
    ((PXSelectBase) this.Summary).Cache.AllowInsert = flag1;
    ((PXSelectBase) this.Summary).Cache.AllowUpdate = flag1;
    ((PXSelectBase) this.Summary).Cache.AllowDelete = flag1;
    ((PXAction) this.preloadFromPreviousTimecard).SetEnabled(flag1);
    nullable2 = row.EquipmentID;
    if (nullable2.HasValue)
    {
      if (((PXSelectBase<EPEquipmentSummary>) this.Summary).Select(Array.Empty<object>()).Count > 0)
        PXUIFieldAttribute.SetEnabled<EPEquipmentTimeCard.equipmentID>(sender, (object) row, false);
      else
        PXUIFieldAttribute.SetEnabled<EPEquipmentTimeCard.equipmentID>(sender, (object) row, flag1);
    }
    bool flag2 = sender.GetStatus(e.Row) == 2;
    PXUIFieldAttribute.SetEnabled<EPEquipmentTimeCard.weekId>(sender, (object) row, flag1 & flag2);
    nullable2 = row.WeekID;
    if (nullable2.HasValue)
    {
      nullable2 = row.WeekID;
      TimeCardMaint.SetSummaryColumnsDisplayName(PXWeekSelector2Attribute.GetWeekInfo((PXGraph) this, nullable2.Value), ((PXSelectBase) this.Summary).Cache);
    }
    this.RecalculateTotals(row);
  }

  protected virtual void EPEquipmentTimeCard_EquipmentID_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    if (e.Row is EPEquipmentTimeCard && PXResultset<EPEquipmentTimeCard>.op_Implicit(PXSelectBase<EPEquipmentTimeCard, PXSelect<EPEquipmentTimeCard, Where<EPEquipmentTimeCard.equipmentID, Equal<Current<EPEquipmentTimeCard.equipmentID>>, And<EPEquipmentTimeCard.weekId, Greater<Current<EPEquipmentTimeCard.weekId>>>>>.Config>.Select((PXGraph) this, Array.Empty<object>())) != null)
      throw new PXSetPropertyException("Since there exists a Time Card for the future week you cannot change the Equipment in the given week.");
  }

  protected virtual void EPEquipmentSummary_RowInserted(PXCache sender, PXRowInsertedEventArgs e)
  {
    try
    {
      if (!(e.Row is EPEquipmentSummary row) || this.dontSyncDetails)
        return;
      this.UpdateAdjustingDetails(row);
    }
    finally
    {
    }
  }

  protected virtual void EPEquipmentSummary_RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    try
    {
      EPEquipmentSummary row = e.Row as EPEquipmentSummary;
      EPEquipmentSummary oldRow = e.OldRow as EPEquipmentSummary;
      if (row == null || this.dontSyncDetails)
        return;
      if (row.RateType != oldRow.RateType)
        this.ResetRateType(row, oldRow.RateType);
      this.UpdateAdjustingDetails(row);
    }
    finally
    {
    }
  }

  protected virtual void EPEquipmentSummary_RowDeleted(PXCache sender, PXRowDeletedEventArgs e)
  {
    try
    {
      if (!(e.Row is EPEquipmentSummary row) || this.dontSyncDetails)
        return;
      this.UpdateAdjustingDetails(row);
    }
    finally
    {
    }
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<EPEquipmentDetail, EPEquipmentDetail.date> e)
  {
    if (((PXSelectBase<EPEquipmentTimeCard>) this.Document).Current == null)
      return;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<EPEquipmentDetail, EPEquipmentDetail.date>, EPEquipmentDetail, object>) e).NewValue = (object) ((PXSelectBase<EPEquipmentTimeCard>) this.Document).Current.WeekStartDate;
  }

  protected virtual void EPEquipmentDetail_Date_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    if (!(e.Row is EPEquipmentDetail))
      return;
    DateTime? nullable = new DateTime?();
    DateTime result;
    if (e.NewValue is string && DateTime.TryParse((string) e.NewValue, (IFormatProvider) sender.Graph.Culture, DateTimeStyles.None, out result))
      nullable = new DateTime?(result);
    if (e.NewValue is DateTime)
      nullable = new DateTime?((DateTime) e.NewValue);
    PXWeekSelector2Attribute.WeekInfo weekInfo = PXWeekSelector2Attribute.GetWeekInfo((PXGraph) this, ((PXSelectBase<EPEquipmentTimeCard>) this.Document).Current.WeekID.Value);
    if (nullable.HasValue && !weekInfo.IsValid(nullable.Value.Date))
      throw new PXSetPropertyException("The selected date does not belong to the week selected in the Summary area.");
  }

  protected virtual void EPEquipmentDetail_RowInserted(PXCache sender, PXRowInsertedEventArgs e)
  {
    try
    {
      if (!(e.Row is EPEquipmentDetail row) || this.dontSyncSummary)
        return;
      this.AddToSummary(this.GetSummaryRecords(row), row);
    }
    finally
    {
    }
  }

  protected virtual void EPEquipmentDetail_RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    try
    {
      EPEquipmentDetail row = e.Row as EPEquipmentDetail;
      EPEquipmentDetail oldRow = e.OldRow as EPEquipmentDetail;
      if (row == null || this.dontSyncSummary)
        return;
      this.SubtractFromSummary(this.GetSummaryRecords(oldRow), oldRow);
      this.AddToSummary(this.GetSummaryRecords(row), row);
    }
    finally
    {
    }
  }

  protected virtual void EPEquipmentDetail_RowDeleting(PXCache sender, PXRowDeletingEventArgs e)
  {
    try
    {
      if (e.Row is EPEquipmentDetail && ((PXSelectBase<EPEquipmentTimeCard>) this.Document).Current != null && !string.IsNullOrEmpty(((PXSelectBase<EPEquipmentTimeCard>) this.Document).Current.OrigTimeCardCD) && ((EPEquipmentDetail) e.Row).OrigLineNbr.HasValue && ((PXSelectBase) this.Document).Cache.GetStatus((object) ((PXSelectBase<EPEquipmentTimeCard>) this.Document).Current) != 3)
        throw new PXException("In the correction Time Card if you want to delete/eliminate previosly released time record just set the Time to zero.");
    }
    finally
    {
    }
  }

  protected virtual void EPEquipmentDetail_RowDeleted(PXCache sender, PXRowDeletedEventArgs e)
  {
    try
    {
      if (!(e.Row is EPEquipmentDetail row) || this.dontSyncSummary)
        return;
      this.SubtractFromSummary(this.GetSummaryRecords(row), row);
    }
    finally
    {
    }
  }

  protected virtual void EPApproval_Details_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    if (((PXSelectBase<EPEquipmentTimeCard>) this.Document).Current == null)
      return;
    e.NewValue = (object) null;
    Type[] typeArray = new Type[3]
    {
      typeof (EPEquipmentTimeCard.weekDescription),
      typeof (EPEquipmentTimeCard.timeTotalCalc),
      typeof (EPEquipmentTimeCard.timeBillableTotalCalc)
    };
    foreach (MemberInfo memberInfo in typeArray)
    {
      if (((PXSelectBase) this.Document).Cache.GetValueExt((object) ((PXSelectBase<EPEquipmentTimeCard>) this.Document).Current, memberInfo.Name) is PXStringState valueExt)
      {
        string str = valueExt.InputMask != null ? Mask.Format(valueExt.InputMask, PXFieldState.op_Implicit((PXFieldState) valueExt)) : (((PXFieldState) valueExt).Value != null ? ((PXFieldState) valueExt).Value.ToString() : (string) null);
        if (!string.IsNullOrEmpty(str))
        {
          PXFieldDefaultingEventArgs defaultingEventArgs = e;
          defaultingEventArgs.NewValue = (object) $"{defaultingEventArgs.NewValue?.ToString()}{(e.NewValue != null ? ", " : string.Empty)}{((PXFieldState) valueExt).DisplayName}={str.Trim()}";
        }
      }
    }
  }

  protected virtual void _(PX.Data.Events.RowPersisting<EPEquipmentTimeCard> e)
  {
    EPEquipmentTimeCard row = e.Row;
    if (row != null && e.Operation == 2 && string.IsNullOrEmpty(e.Row.OrigTimeCardCD) && this.HasDuplicate(row.EquipmentID, row.WeekID, row.TimeCardCD))
      throw new PXException(PXMessages.LocalizeFormatNoPrefix("An equipment time card for the {0} equipment and {1} week already exists.", new object[2]
      {
        (object) row.EquipmentID,
        (object) row.WeekID
      }));
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<EPEquipmentTimeCard, EPEquipmentTimeCard.weekId> e)
  {
    EPEquipmentTimeCard row = e.Row;
    if (row != null && ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<EPEquipmentTimeCard, EPEquipmentTimeCard.weekId>, EPEquipmentTimeCard, object>) e).NewValue != null && string.IsNullOrEmpty(row.OrigTimeCardCD) && this.HasDuplicate(row.EquipmentID, new int?((int) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<EPEquipmentTimeCard, EPEquipmentTimeCard.weekId>, EPEquipmentTimeCard, object>) e).NewValue), row.TimeCardCD))
      throw new PXSetPropertyException((IBqlTable) row, PXMessages.LocalizeFormatNoPrefix("An equipment time card for the {0} equipment and {1} week already exists.", new object[2]
      {
        (object) row.EquipmentID,
        (object) (int) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<EPEquipmentTimeCard, EPEquipmentTimeCard.weekId>, EPEquipmentTimeCard, object>) e).NewValue
      }));
  }

  protected virtual void ProcessRegularTimecard(
    RegisterEntry releaseGraph,
    EPEquipmentTimeCard timecard)
  {
    PMRegister pmRegister = (PMRegister) ((PXSelectBase) releaseGraph.Document).Cache.Insert();
    pmRegister.OrigDocType = "ET";
    pmRegister.OrigNoteID = timecard.NoteID;
    EPEquipment epEquipment = PXResultset<EPEquipment>.op_Implicit(PXSelectBase<EPEquipment, PXSelect<EPEquipment, Where<EPEquipment.equipmentID, Equal<Required<EPEquipment.equipmentID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) timecard.EquipmentID
    }));
    foreach (PXResult<EPEquipmentDetail> pxResult in ((PXSelectBase<EPEquipmentDetail>) this.Details).Select(Array.Empty<object>()))
    {
      EPEquipmentDetail row = PXResult<EPEquipmentDetail>.op_Implicit(pxResult);
      int? nullable = row.SetupTime;
      if (nullable.GetValueOrDefault() != 0)
      {
        nullable = epEquipment.SetupRateItemID;
        if (!nullable.HasValue)
          throw new PXException("The Setup Rate Class is not defined for the given Equipment. Please set the Setup Rate Class on the Equipment screen and try agaain.");
      }
      nullable = row.RunTime;
      if (nullable.GetValueOrDefault() != 0)
      {
        nullable = epEquipment.RunRateItemID;
        if (!nullable.HasValue)
          throw new PXException("The Run-Rate Item is not specified for the {0} equipment on the Equipment (EP208000) form.", new object[1]
          {
            (object) epEquipment.EquipmentID
          });
      }
      nullable = row.SuspendTime;
      if (nullable.GetValueOrDefault() != 0)
      {
        nullable = epEquipment.SuspendRateItemID;
        if (!nullable.HasValue)
          throw new PXException("The Suspend Rate Item is not defined for the given Equipment. Please set the Suspend Rate Class on the Equipment screen and try agaain.");
      }
      this.InsertPMTran(releaseGraph, row, epEquipment.SetupRateItemID, row.SetupTime, this.GetSetupCost(epEquipment.EquipmentID, row.ProjectID), epEquipment.EquipmentID);
      this.InsertPMTran(releaseGraph, row, epEquipment.RunRateItemID, row.RunTime, this.GetRunCost(epEquipment.EquipmentID, row.ProjectID), epEquipment.EquipmentID);
      this.InsertPMTran(releaseGraph, row, epEquipment.SuspendRateItemID, row.SuspendTime, this.GetSuspendCost(epEquipment.EquipmentID, row.ProjectID), epEquipment.EquipmentID);
    }
  }

  protected virtual void ProcessCorrectingTimecard(
    RegisterEntry releaseGraph,
    EPEquipmentTimeCard timecard)
  {
    EPEquipment epEquipment = PXResultset<EPEquipment>.op_Implicit(PXSelectBase<EPEquipment, PXSelect<EPEquipment, Where<EPEquipment.equipmentID, Equal<Required<EPEquipment.equipmentID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) timecard.EquipmentID
    }));
    if (epEquipment == null)
      throw new PXException("Equipment was not selected.");
    PXCache cache1 = ((PXSelectBase) releaseGraph.Document).Cache;
    PXCache cache2 = ((PXSelectBase) releaseGraph.Transactions).Cache;
    PMRegister pmRegister = (PMRegister) cache1.Insert();
    pmRegister.OrigDocType = "ET";
    pmRegister.OrigNoteID = timecard.NoteID;
    pmRegister.Description = PXMessages.LocalizeFormatNoPrefixNLA("{0} - {1} correction", new object[2]
    {
      (object) epEquipment.EquipmentCD,
      (object) timecard.WeekID
    });
    PXView view1 = ((PXSelectBase) new PXSelectJoin<EquipmentTimeCardMaint.EPEquipmentDetailOrig, LeftJoin<EquipmentTimeCardMaint.EPEquipmentDetailEx, On<EquipmentTimeCardMaint.EPEquipmentDetailEx.timeCardCD, Equal<Current<EPEquipmentTimeCard.timeCardCD>>, And<EquipmentTimeCardMaint.EPEquipmentDetailEx.origLineNbr, Equal<EquipmentTimeCardMaint.EPEquipmentDetailOrig.lineNbr>>>>, Where<EquipmentTimeCardMaint.EPEquipmentDetailOrig.timeCardCD, Equal<Current<EPEquipmentTimeCard.origTimeCardCD>>, And<EquipmentTimeCardMaint.EPEquipmentDetailEx.timeCardCD, IsNull>>>((PXGraph) this)).View;
    object[] objArray1 = new object[1]{ (object) timecard };
    object[] objArray2 = Array.Empty<object>();
    foreach (PXResult<EquipmentTimeCardMaint.EPEquipmentDetailOrig, EquipmentTimeCardMaint.EPEquipmentDetailEx> pxResult in view1.SelectMultiBound(objArray1, objArray2))
    {
      EquipmentTimeCardMaint.EPEquipmentDetailOrig equipmentDetailOrig = PXResult<EquipmentTimeCardMaint.EPEquipmentDetailOrig, EquipmentTimeCardMaint.EPEquipmentDetailEx>.op_Implicit(pxResult);
      int? nullable = equipmentDetailOrig.SetupTime;
      if (nullable.GetValueOrDefault() > 0)
      {
        nullable = epEquipment.SetupRateItemID;
        if (!nullable.HasValue)
          throw new PXException("The Setup Rate Class is not defined for the given Equipment. Please set the Setup Rate Class on the Equipment screen and try agaain.");
        RegisterEntry pmGraph = releaseGraph;
        EquipmentTimeCardMaint.EPEquipmentDetailOrig row = equipmentDetailOrig;
        int? setupRateItemId = epEquipment.SetupRateItemID;
        nullable = equipmentDetailOrig.SetupTime;
        int? minutes = nullable.HasValue ? new int?(-nullable.GetValueOrDefault()) : new int?();
        Decimal? setupCost = this.GetSetupCost(epEquipment.EquipmentID, equipmentDetailOrig.ProjectID);
        int? equipmentId = epEquipment.EquipmentID;
        this.InsertPMTran(pmGraph, (EPEquipmentDetail) row, setupRateItemId, minutes, setupCost, equipmentId);
      }
      nullable = equipmentDetailOrig.RunTime;
      if (nullable.GetValueOrDefault() > 0)
      {
        nullable = epEquipment.RunRateItemID;
        if (!nullable.HasValue)
          throw new PXException("The Run-Rate Item is not specified for the {0} equipment on the Equipment (EP208000) form.", new object[1]
          {
            (object) epEquipment.EquipmentID
          });
        RegisterEntry pmGraph = releaseGraph;
        EquipmentTimeCardMaint.EPEquipmentDetailOrig row = equipmentDetailOrig;
        int? runRateItemId = epEquipment.RunRateItemID;
        nullable = equipmentDetailOrig.RunTime;
        int? minutes = nullable.HasValue ? new int?(-nullable.GetValueOrDefault()) : new int?();
        Decimal? runCost = this.GetRunCost(epEquipment.EquipmentID, equipmentDetailOrig.ProjectID);
        int? equipmentId = epEquipment.EquipmentID;
        this.InsertPMTran(pmGraph, (EPEquipmentDetail) row, runRateItemId, minutes, runCost, equipmentId);
      }
      nullable = equipmentDetailOrig.SuspendTime;
      if (nullable.GetValueOrDefault() > 0)
      {
        nullable = epEquipment.SuspendRateItemID;
        if (!nullable.HasValue)
          throw new PXException("The Suspend Rate Item is not defined for the given Equipment. Please set the Suspend Rate Class on the Equipment screen and try agaain.");
        RegisterEntry pmGraph = releaseGraph;
        EquipmentTimeCardMaint.EPEquipmentDetailOrig row = equipmentDetailOrig;
        int? suspendRateItemId = epEquipment.SuspendRateItemID;
        nullable = equipmentDetailOrig.SuspendTime;
        int? minutes = nullable.HasValue ? new int?(-nullable.GetValueOrDefault()) : new int?();
        Decimal? suspendCost = this.GetSuspendCost(epEquipment.EquipmentID, equipmentDetailOrig.ProjectID);
        int? equipmentId = epEquipment.EquipmentID;
        this.InsertPMTran(pmGraph, (EPEquipmentDetail) row, suspendRateItemId, minutes, suspendCost, equipmentId);
      }
    }
    PXView view2 = ((PXSelectBase) new PXSelect<EPEquipmentDetail, Where<EPEquipmentDetail.timeCardCD, Equal<Current<EPEquipmentTimeCard.timeCardCD>>, And<EPEquipmentDetail.origLineNbr, IsNull>>>((PXGraph) this)).View;
    object[] objArray3 = new object[1]{ (object) timecard };
    object[] objArray4 = Array.Empty<object>();
    foreach (EPEquipmentDetail row in view2.SelectMultiBound(objArray3, objArray4))
    {
      int? nullable = row.SetupTime;
      if (nullable.GetValueOrDefault() > 0)
      {
        nullable = epEquipment.SetupRateItemID;
        if (!nullable.HasValue)
          throw new PXException("The Setup Rate Class is not defined for the given Equipment. Please set the Setup Rate Class on the Equipment screen and try agaain.");
        this.InsertPMTran(releaseGraph, row, epEquipment.SetupRateItemID, row.SetupTime, this.GetSetupCost(epEquipment.EquipmentID, row.ProjectID), epEquipment.EquipmentID);
      }
      nullable = row.RunTime;
      if (nullable.GetValueOrDefault() > 0)
      {
        nullable = epEquipment.RunRateItemID;
        if (!nullable.HasValue)
          throw new PXException("The Run-Rate Item is not specified for the {0} equipment on the Equipment (EP208000) form.", new object[1]
          {
            (object) epEquipment.EquipmentID
          });
        this.InsertPMTran(releaseGraph, row, epEquipment.RunRateItemID, row.RunTime, this.GetRunCost(epEquipment.EquipmentID, row.ProjectID), epEquipment.EquipmentID);
      }
      nullable = row.SuspendTime;
      if (nullable.GetValueOrDefault() > 0)
      {
        nullable = epEquipment.SuspendRateItemID;
        if (!nullable.HasValue)
          throw new PXException("The Suspend Rate Item is not defined for the given Equipment. Please set the Suspend Rate Class on the Equipment screen and try agaain.");
        this.InsertPMTran(releaseGraph, row, epEquipment.SuspendRateItemID, row.SuspendTime, this.GetSuspendCost(epEquipment.EquipmentID, row.ProjectID), epEquipment.EquipmentID);
      }
    }
    foreach (PXResult<EquipmentTimeCardMaint.EPEquipmentDetailOrig, EquipmentTimeCardMaint.EPEquipmentDetailEx> pxResult in ((PXSelectBase) new PXSelectJoin<EquipmentTimeCardMaint.EPEquipmentDetailOrig, LeftJoin<EquipmentTimeCardMaint.EPEquipmentDetailEx, On<EquipmentTimeCardMaint.EPEquipmentDetailEx.timeCardCD, Equal<Current<EPEquipmentTimeCard.timeCardCD>>, And<EquipmentTimeCardMaint.EPEquipmentDetailEx.origLineNbr, Equal<EquipmentTimeCardMaint.EPEquipmentDetailOrig.lineNbr>>>>, Where<EquipmentTimeCardMaint.EPEquipmentDetailOrig.timeCardCD, Equal<Current<EPEquipmentTimeCard.origTimeCardCD>>, And<EquipmentTimeCardMaint.EPEquipmentDetailEx.timeCardCD, IsNotNull>>>((PXGraph) this)).View.SelectMultiBound(new object[1]
    {
      (object) timecard
    }, Array.Empty<object>()))
    {
      EquipmentTimeCardMaint.EPEquipmentDetailOrig equipmentDetailOrig = PXResult<EquipmentTimeCardMaint.EPEquipmentDetailOrig, EquipmentTimeCardMaint.EPEquipmentDetailEx>.op_Implicit(pxResult);
      EquipmentTimeCardMaint.EPEquipmentDetailEx equipmentDetailEx = PXResult<EquipmentTimeCardMaint.EPEquipmentDetailOrig, EquipmentTimeCardMaint.EPEquipmentDetailEx>.op_Implicit(pxResult);
      int? nullable1 = equipmentDetailEx.SetupTime;
      int valueOrDefault1 = nullable1.GetValueOrDefault();
      nullable1 = equipmentDetailOrig.SetupTime;
      int valueOrDefault2 = nullable1.GetValueOrDefault();
      int? nullable2;
      if (valueOrDefault1 - valueOrDefault2 != 0)
      {
        nullable1 = epEquipment.SetupRateItemID;
        nullable1 = nullable1.HasValue ? equipmentDetailEx.CostCodeID : throw new PXException("The Setup Rate Class is not defined for the given Equipment. Please set the Setup Rate Class on the Equipment screen and try agaain.");
        nullable2 = equipmentDetailOrig.CostCodeID;
        if (nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue)
        {
          RegisterEntry pmGraph = releaseGraph;
          EquipmentTimeCardMaint.EPEquipmentDetailEx row = equipmentDetailEx;
          int? setupRateItemId = epEquipment.SetupRateItemID;
          nullable2 = equipmentDetailEx.SetupTime;
          int valueOrDefault3 = nullable2.GetValueOrDefault();
          nullable2 = equipmentDetailOrig.SetupTime;
          int valueOrDefault4 = nullable2.GetValueOrDefault();
          int? minutes = new int?(valueOrDefault3 - valueOrDefault4);
          Decimal? setupCost = this.GetSetupCost(epEquipment.EquipmentID, equipmentDetailEx.ProjectID);
          int? equipmentId = epEquipment.EquipmentID;
          this.InsertPMTran(pmGraph, (EPEquipmentDetail) row, setupRateItemId, minutes, setupCost, equipmentId);
        }
        else
        {
          RegisterEntry pmGraph1 = releaseGraph;
          EquipmentTimeCardMaint.EPEquipmentDetailOrig row1 = equipmentDetailOrig;
          int? setupRateItemId1 = epEquipment.SetupRateItemID;
          nullable2 = equipmentDetailOrig.SetupTime;
          int? minutes1 = new int?(-nullable2.GetValueOrDefault());
          Decimal? setupCost1 = this.GetSetupCost(epEquipment.EquipmentID, equipmentDetailEx.ProjectID);
          int? equipmentId1 = epEquipment.EquipmentID;
          this.InsertPMTran(pmGraph1, (EPEquipmentDetail) row1, setupRateItemId1, minutes1, setupCost1, equipmentId1);
          RegisterEntry pmGraph2 = releaseGraph;
          EquipmentTimeCardMaint.EPEquipmentDetailEx row2 = equipmentDetailEx;
          int? setupRateItemId2 = epEquipment.SetupRateItemID;
          nullable2 = equipmentDetailEx.SetupTime;
          int? minutes2 = new int?(nullable2.GetValueOrDefault());
          Decimal? setupCost2 = this.GetSetupCost(epEquipment.EquipmentID, equipmentDetailEx.ProjectID);
          int? equipmentId2 = epEquipment.EquipmentID;
          this.InsertPMTran(pmGraph2, (EPEquipmentDetail) row2, setupRateItemId2, minutes2, setupCost2, equipmentId2);
        }
      }
      nullable2 = equipmentDetailEx.RunTime;
      int valueOrDefault5 = nullable2.GetValueOrDefault();
      nullable2 = equipmentDetailOrig.RunTime;
      int valueOrDefault6 = nullable2.GetValueOrDefault();
      if (valueOrDefault5 - valueOrDefault6 != 0)
      {
        nullable2 = epEquipment.RunRateItemID;
        nullable2 = nullable2.HasValue ? equipmentDetailEx.CostCodeID : throw new PXException("The Run-Rate Item is not specified for the {0} equipment on the Equipment (EP208000) form.", new object[1]
        {
          (object) epEquipment.EquipmentID
        });
        nullable1 = equipmentDetailOrig.CostCodeID;
        if (nullable2.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable2.HasValue == nullable1.HasValue)
        {
          RegisterEntry pmGraph = releaseGraph;
          EquipmentTimeCardMaint.EPEquipmentDetailEx row = equipmentDetailEx;
          int? runRateItemId = epEquipment.RunRateItemID;
          nullable1 = equipmentDetailEx.RunTime;
          int valueOrDefault7 = nullable1.GetValueOrDefault();
          nullable1 = equipmentDetailOrig.RunTime;
          int valueOrDefault8 = nullable1.GetValueOrDefault();
          int? minutes = new int?(valueOrDefault7 - valueOrDefault8);
          Decimal? runCost = this.GetRunCost(epEquipment.EquipmentID, equipmentDetailEx.ProjectID);
          int? equipmentId = epEquipment.EquipmentID;
          this.InsertPMTran(pmGraph, (EPEquipmentDetail) row, runRateItemId, minutes, runCost, equipmentId);
        }
        else
        {
          RegisterEntry pmGraph3 = releaseGraph;
          EquipmentTimeCardMaint.EPEquipmentDetailOrig row3 = equipmentDetailOrig;
          int? runRateItemId1 = epEquipment.RunRateItemID;
          nullable1 = equipmentDetailOrig.RunTime;
          int? minutes3 = new int?(-nullable1.GetValueOrDefault());
          Decimal? runCost1 = this.GetRunCost(epEquipment.EquipmentID, equipmentDetailEx.ProjectID);
          int? equipmentId3 = epEquipment.EquipmentID;
          this.InsertPMTran(pmGraph3, (EPEquipmentDetail) row3, runRateItemId1, minutes3, runCost1, equipmentId3);
          RegisterEntry pmGraph4 = releaseGraph;
          EquipmentTimeCardMaint.EPEquipmentDetailEx row4 = equipmentDetailEx;
          int? runRateItemId2 = epEquipment.RunRateItemID;
          nullable1 = equipmentDetailEx.RunTime;
          int? minutes4 = new int?(nullable1.GetValueOrDefault());
          Decimal? runCost2 = this.GetRunCost(epEquipment.EquipmentID, equipmentDetailEx.ProjectID);
          int? equipmentId4 = epEquipment.EquipmentID;
          this.InsertPMTran(pmGraph4, (EPEquipmentDetail) row4, runRateItemId2, minutes4, runCost2, equipmentId4);
        }
      }
      nullable1 = equipmentDetailEx.SuspendTime;
      int valueOrDefault9 = nullable1.GetValueOrDefault();
      nullable1 = equipmentDetailOrig.SuspendTime;
      int valueOrDefault10 = nullable1.GetValueOrDefault();
      if (valueOrDefault9 - valueOrDefault10 != 0)
      {
        nullable1 = epEquipment.SuspendRateItemID;
        nullable1 = nullable1.HasValue ? equipmentDetailEx.CostCodeID : throw new PXException("The Suspend Rate Item is not defined for the given Equipment. Please set the Suspend Rate Class on the Equipment screen and try agaain.");
        nullable2 = equipmentDetailOrig.CostCodeID;
        if (nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue)
        {
          RegisterEntry pmGraph = releaseGraph;
          EquipmentTimeCardMaint.EPEquipmentDetailEx row = equipmentDetailEx;
          int? suspendRateItemId = epEquipment.SuspendRateItemID;
          nullable2 = equipmentDetailEx.SuspendTime;
          int valueOrDefault11 = nullable2.GetValueOrDefault();
          nullable2 = equipmentDetailOrig.SuspendTime;
          int valueOrDefault12 = nullable2.GetValueOrDefault();
          int? minutes = new int?(valueOrDefault11 - valueOrDefault12);
          Decimal? suspendCost = this.GetSuspendCost(epEquipment.EquipmentID, equipmentDetailEx.ProjectID);
          int? equipmentId = epEquipment.EquipmentID;
          this.InsertPMTran(pmGraph, (EPEquipmentDetail) row, suspendRateItemId, minutes, suspendCost, equipmentId);
        }
        else
        {
          RegisterEntry pmGraph5 = releaseGraph;
          EquipmentTimeCardMaint.EPEquipmentDetailOrig row5 = equipmentDetailOrig;
          int? suspendRateItemId1 = epEquipment.SuspendRateItemID;
          nullable2 = equipmentDetailOrig.SuspendTime;
          int? minutes5 = new int?(-nullable2.GetValueOrDefault());
          Decimal? suspendCost1 = this.GetSuspendCost(epEquipment.EquipmentID, equipmentDetailEx.ProjectID);
          int? equipmentId5 = epEquipment.EquipmentID;
          this.InsertPMTran(pmGraph5, (EPEquipmentDetail) row5, suspendRateItemId1, minutes5, suspendCost1, equipmentId5);
          RegisterEntry pmGraph6 = releaseGraph;
          EquipmentTimeCardMaint.EPEquipmentDetailEx row6 = equipmentDetailEx;
          int? suspendRateItemId2 = epEquipment.SuspendRateItemID;
          nullable2 = equipmentDetailEx.SuspendTime;
          int? minutes6 = new int?(nullable2.GetValueOrDefault());
          Decimal? suspendCost2 = this.GetSuspendCost(epEquipment.EquipmentID, equipmentDetailEx.ProjectID);
          int? equipmentId6 = epEquipment.EquipmentID;
          this.InsertPMTran(pmGraph6, (EPEquipmentDetail) row6, suspendRateItemId2, minutes6, suspendCost2, equipmentId6);
        }
      }
    }
  }

  protected virtual SortedList<string, EPEquipmentSummary> GetSummaryRecords(
    EPEquipmentDetail detail)
  {
    if (detail == null)
      throw new ArgumentNullException();
    SortedList<string, EPEquipmentSummary> summaryRecords = new SortedList<string, EPEquipmentSummary>();
    int? nullable = detail.SetupTime;
    if (nullable.GetValueOrDefault() != 0)
    {
      EPEquipmentSummary equipmentSummary = PXResultset<EPEquipmentSummary>.op_Implicit(PXSelectBase<EPEquipmentSummary, PXSelect<EPEquipmentSummary, Where<EPEquipmentSummary.timeCardCD, Equal<Current<EPEquipmentTimeCard.timeCardCD>>, And<EPEquipmentSummary.lineNbr, Equal<Required<EPEquipmentSummary.lineNbr>>>>>.Config>.Select((PXGraph) this, new object[1]
      {
        (object) detail.SetupSummaryLineNbr
      }));
      if (equipmentSummary != null)
        summaryRecords.Add("ST", equipmentSummary);
    }
    nullable = detail.RunTime;
    if (nullable.GetValueOrDefault() != 0)
    {
      EPEquipmentSummary equipmentSummary = PXResultset<EPEquipmentSummary>.op_Implicit(PXSelectBase<EPEquipmentSummary, PXSelect<EPEquipmentSummary, Where<EPEquipmentSummary.timeCardCD, Equal<Current<EPEquipmentTimeCard.timeCardCD>>, And<EPEquipmentSummary.lineNbr, Equal<Required<EPEquipmentSummary.lineNbr>>>>>.Config>.Select((PXGraph) this, new object[1]
      {
        (object) detail.RunSummaryLineNbr
      }));
      if (equipmentSummary != null)
        summaryRecords.Add("RU", equipmentSummary);
    }
    nullable = detail.SuspendTime;
    if (nullable.GetValueOrDefault() != 0)
    {
      EPEquipmentSummary equipmentSummary = PXResultset<EPEquipmentSummary>.op_Implicit(PXSelectBase<EPEquipmentSummary, PXSelect<EPEquipmentSummary, Where<EPEquipmentSummary.timeCardCD, Equal<Current<EPEquipmentTimeCard.timeCardCD>>, And<EPEquipmentSummary.lineNbr, Equal<Required<EPEquipmentSummary.lineNbr>>>>>.Config>.Select((PXGraph) this, new object[1]
      {
        (object) detail.SuspendSummaryLineNbr
      }));
      if (equipmentSummary != null)
        summaryRecords.Add("SD", equipmentSummary);
    }
    return summaryRecords;
  }

  protected virtual List<EPEquipmentSummary> AddToSummary(
    SortedList<string, EPEquipmentSummary> list,
    EPEquipmentDetail detail)
  {
    return this.AddToSummary(list, detail, 1);
  }

  protected virtual void SubtractFromSummary(
    SortedList<string, EPEquipmentSummary> list,
    EPEquipmentDetail detail)
  {
    List<EPEquipmentSummary> summary = this.AddToSummary(list, detail, -1);
    try
    {
      this.dontSyncDetails = true;
      foreach (EPEquipmentSummary equipmentSummary in summary)
      {
        if (equipmentSummary.TimeSpent.Value == 0)
          ((PXSelectBase<EPEquipmentSummary>) this.Summary).Delete(equipmentSummary);
      }
    }
    finally
    {
      this.dontSyncDetails = false;
    }
  }

  protected virtual List<EPEquipmentSummary> AddToSummary(
    SortedList<string, EPEquipmentSummary> list,
    EPEquipmentDetail detail,
    int mult)
  {
    int? nullable1 = detail != null ? detail.SetupTime : throw new ArgumentNullException();
    if (nullable1.GetValueOrDefault() == 0)
    {
      nullable1 = detail.SuspendTime;
      if (nullable1.GetValueOrDefault() == 0)
      {
        nullable1 = detail.RunTime;
        if (nullable1.GetValueOrDefault() == 0)
          return new List<EPEquipmentSummary>();
      }
    }
    nullable1 = detail.SetupTime;
    if (nullable1.GetValueOrDefault() != 0 && !list.ContainsKey("ST"))
    {
      EPEquipmentSummary instance = (EPEquipmentSummary) ((PXSelectBase) this.Summary).Cache.CreateInstance();
      instance.RateType = "ST";
      instance.ProjectID = detail.ProjectID;
      instance.ProjectTaskID = detail.ProjectTaskID;
      instance.CostCodeID = detail.CostCodeID;
      instance.IsBillable = detail.IsBillable;
      this.dontSyncDetails = true;
      try
      {
        EPEquipmentSummary equipmentSummary = ((PXSelectBase<EPEquipmentSummary>) this.Summary).Insert(instance);
        detail.SetupSummaryLineNbr = equipmentSummary.LineNbr;
        list.Add("ST", equipmentSummary);
      }
      finally
      {
        this.dontSyncDetails = false;
      }
    }
    nullable1 = detail.RunTime;
    if (nullable1.GetValueOrDefault() != 0 && !list.ContainsKey("RU"))
    {
      EPEquipmentSummary instance = (EPEquipmentSummary) ((PXSelectBase) this.Summary).Cache.CreateInstance();
      instance.RateType = "RU";
      instance.ProjectID = detail.ProjectID;
      instance.ProjectTaskID = detail.ProjectTaskID;
      instance.CostCodeID = detail.CostCodeID;
      instance.IsBillable = detail.IsBillable;
      this.dontSyncDetails = true;
      try
      {
        EPEquipmentSummary equipmentSummary = ((PXSelectBase<EPEquipmentSummary>) this.Summary).Insert(instance);
        detail.RunSummaryLineNbr = equipmentSummary.LineNbr;
        list.Add("RU", equipmentSummary);
      }
      finally
      {
        this.dontSyncDetails = false;
      }
    }
    nullable1 = detail.SuspendTime;
    if (nullable1.GetValueOrDefault() != 0 && !list.ContainsKey("SD"))
    {
      EPEquipmentSummary instance = (EPEquipmentSummary) ((PXSelectBase) this.Summary).Cache.CreateInstance();
      instance.RateType = "SD";
      instance.ProjectID = detail.ProjectID;
      instance.ProjectTaskID = detail.ProjectTaskID;
      instance.CostCodeID = detail.CostCodeID;
      instance.IsBillable = detail.IsBillable;
      this.dontSyncDetails = true;
      try
      {
        EPEquipmentSummary equipmentSummary = ((PXSelectBase<EPEquipmentSummary>) this.Summary).Insert(instance);
        detail.SuspendSummaryLineNbr = equipmentSummary.LineNbr;
        list.Add("SD", equipmentSummary);
      }
      finally
      {
        this.dontSyncDetails = false;
      }
    }
    List<EPEquipmentSummary> summary = new List<EPEquipmentSummary>();
    foreach (EPEquipmentSummary equipmentSummary1 in (IEnumerable<EPEquipmentSummary>) list.Values)
    {
      int valueOrDefault;
      switch (equipmentSummary1.RateType)
      {
        case "ST":
          nullable1 = detail.SetupTime;
          valueOrDefault = nullable1.GetValueOrDefault();
          break;
        case "SD":
          nullable1 = detail.SuspendTime;
          valueOrDefault = nullable1.GetValueOrDefault();
          break;
        default:
          nullable1 = detail.RunTime;
          valueOrDefault = nullable1.GetValueOrDefault();
          break;
      }
      if (valueOrDefault != 0)
      {
        switch (detail.Date.Value.DayOfWeek)
        {
          case DayOfWeek.Sunday:
            EPEquipmentSummary equipmentSummary2 = equipmentSummary1;
            nullable1 = equipmentSummary1.Sun;
            int? nullable2 = new int?(nullable1.GetValueOrDefault() + mult * valueOrDefault);
            equipmentSummary2.Sun = nullable2;
            break;
          case DayOfWeek.Monday:
            EPEquipmentSummary equipmentSummary3 = equipmentSummary1;
            nullable1 = equipmentSummary1.Mon;
            int? nullable3 = new int?(nullable1.GetValueOrDefault() + mult * valueOrDefault);
            equipmentSummary3.Mon = nullable3;
            break;
          case DayOfWeek.Tuesday:
            EPEquipmentSummary equipmentSummary4 = equipmentSummary1;
            nullable1 = equipmentSummary1.Tue;
            int? nullable4 = new int?(nullable1.GetValueOrDefault() + mult * valueOrDefault);
            equipmentSummary4.Tue = nullable4;
            break;
          case DayOfWeek.Wednesday:
            EPEquipmentSummary equipmentSummary5 = equipmentSummary1;
            nullable1 = equipmentSummary1.Wed;
            int? nullable5 = new int?(nullable1.GetValueOrDefault() + mult * valueOrDefault);
            equipmentSummary5.Wed = nullable5;
            break;
          case DayOfWeek.Thursday:
            EPEquipmentSummary equipmentSummary6 = equipmentSummary1;
            nullable1 = equipmentSummary1.Thu;
            int? nullable6 = new int?(nullable1.GetValueOrDefault() + mult * valueOrDefault);
            equipmentSummary6.Thu = nullable6;
            break;
          case DayOfWeek.Friday:
            EPEquipmentSummary equipmentSummary7 = equipmentSummary1;
            nullable1 = equipmentSummary1.Fri;
            int? nullable7 = new int?(nullable1.GetValueOrDefault() + mult * valueOrDefault);
            equipmentSummary7.Fri = nullable7;
            break;
          case DayOfWeek.Saturday:
            EPEquipmentSummary equipmentSummary8 = equipmentSummary1;
            nullable1 = equipmentSummary1.Sat;
            int? nullable8 = new int?(nullable1.GetValueOrDefault() + mult * valueOrDefault);
            equipmentSummary8.Sat = nullable8;
            break;
        }
      }
      try
      {
        this.dontSyncDetails = true;
        summary.Add(((PXSelectBase<EPEquipmentSummary>) this.Summary).Update(equipmentSummary1));
      }
      finally
      {
        this.dontSyncDetails = false;
      }
    }
    return summary;
  }

  protected virtual void UpdateAdjustingDetails(EPEquipmentSummary summary)
  {
    EPEquipmentTimeCard doc = summary != null ? PXResultset<EPEquipmentTimeCard>.op_Implicit(PXSelectBase<EPEquipmentTimeCard, PXSelect<EPEquipmentTimeCard, Where<EPEquipmentTimeCard.timeCardCD, Equal<Required<EPEquipmentTimeCard.timeCardCD>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) summary.TimeCardCD
    })) : throw new ArgumentNullException();
    if (doc == null)
      return;
    Dictionary<DayOfWeek, EquipmentTimeCardMaint.DayDetails> detailRecords = this.GetDetailRecords(summary, doc);
    PXWeekSelector2Attribute.WeekInfo weekInfo = PXWeekSelector2Attribute.GetWeekInfo((PXGraph) this, doc.WeekID.Value);
    DateTime? date;
    if (weekInfo.Mon.Enabled)
    {
      EPEquipmentSummary summary1 = summary;
      Dictionary<DayOfWeek, EquipmentTimeCardMaint.DayDetails> dict = detailRecords;
      date = weekInfo.Mon.Date;
      DateTime startDate = date.Value;
      this.UpdateAdjustingDetails(summary1, dict, DayOfWeek.Monday, startDate);
    }
    if (weekInfo.Tue.Enabled)
    {
      EPEquipmentSummary summary2 = summary;
      Dictionary<DayOfWeek, EquipmentTimeCardMaint.DayDetails> dict = detailRecords;
      date = weekInfo.Tue.Date;
      DateTime startDate = date.Value;
      this.UpdateAdjustingDetails(summary2, dict, DayOfWeek.Tuesday, startDate);
    }
    if (weekInfo.Wed.Enabled)
    {
      EPEquipmentSummary summary3 = summary;
      Dictionary<DayOfWeek, EquipmentTimeCardMaint.DayDetails> dict = detailRecords;
      date = weekInfo.Wed.Date;
      DateTime startDate = date.Value;
      this.UpdateAdjustingDetails(summary3, dict, DayOfWeek.Wednesday, startDate);
    }
    if (weekInfo.Thu.Enabled)
    {
      EPEquipmentSummary summary4 = summary;
      Dictionary<DayOfWeek, EquipmentTimeCardMaint.DayDetails> dict = detailRecords;
      date = weekInfo.Thu.Date;
      DateTime startDate = date.Value;
      this.UpdateAdjustingDetails(summary4, dict, DayOfWeek.Thursday, startDate);
    }
    if (weekInfo.Fri.Enabled)
    {
      EPEquipmentSummary summary5 = summary;
      Dictionary<DayOfWeek, EquipmentTimeCardMaint.DayDetails> dict = detailRecords;
      date = weekInfo.Fri.Date;
      DateTime startDate = date.Value;
      this.UpdateAdjustingDetails(summary5, dict, DayOfWeek.Friday, startDate);
    }
    if (weekInfo.Sat.Enabled)
    {
      EPEquipmentSummary summary6 = summary;
      Dictionary<DayOfWeek, EquipmentTimeCardMaint.DayDetails> dict = detailRecords;
      date = weekInfo.Sat.Date;
      DateTime startDate = date.Value;
      this.UpdateAdjustingDetails(summary6, dict, DayOfWeek.Saturday, startDate);
    }
    if (!weekInfo.Sun.Enabled)
      return;
    EPEquipmentSummary summary7 = summary;
    Dictionary<DayOfWeek, EquipmentTimeCardMaint.DayDetails> dict1 = detailRecords;
    date = weekInfo.Sun.Date;
    DateTime startDate1 = date.Value;
    this.UpdateAdjustingDetails(summary7, dict1, DayOfWeek.Sunday, startDate1);
  }

  protected virtual void UpdateAdjustingDetails(
    EPEquipmentSummary summary,
    Dictionary<DayOfWeek, EquipmentTimeCardMaint.DayDetails> dict,
    DayOfWeek dayOfWeek,
    DateTime startDate)
  {
    if (summary == null)
      throw new ArgumentNullException(nameof (summary));
    if (dict == null)
      throw new ArgumentNullException(nameof (dict));
    int num1 = 0;
    int? nullable1;
    if (((PXSelectBase) this.Summary).Cache.GetStatus((object) summary) != 3 && ((PXSelectBase) this.Summary).Cache.GetStatus((object) summary) != 4)
    {
      nullable1 = summary.GetTimeTotal(dayOfWeek);
      num1 = nullable1.GetValueOrDefault();
    }
    int num2 = 0;
    if (dict.ContainsKey(dayOfWeek))
      num2 = dict[dayOfWeek].GetTotalTime();
    EPEquipmentDetail epEquipmentDetail1 = (EPEquipmentDetail) null;
    if (dict.ContainsKey(dayOfWeek))
      epEquipmentDetail1 = dict[dayOfWeek].GetAdjustingActivity();
    if (num1 != num2)
    {
      if (epEquipmentDetail1 == null && num1 - num2 != 0)
      {
        EPEquipmentDetail instance = (EPEquipmentDetail) ((PXSelectBase) this.Details).Cache.CreateInstance();
        instance.Date = new DateTime?(startDate);
        instance.IsBillable = summary.IsBillable;
        if (!string.IsNullOrEmpty(summary.Description))
          instance.Description = summary.Description;
        else
          instance.Description = PXMessages.LocalizeFormatNoPrefixNLA("Summary {0} Record", new object[1]
          {
            (object) DateTimeFormatInfo.CurrentInfo?.GetDayName(dayOfWeek)
          });
        switch (summary.RateType)
        {
          case "ST":
            instance.SetupTime = new int?(num1 - num2);
            instance.SetupSummaryLineNbr = summary.LineNbr;
            break;
          case "SD":
            instance.SuspendTime = new int?(num1 - num2);
            instance.SuspendSummaryLineNbr = summary.LineNbr;
            break;
          default:
            instance.RunTime = new int?(num1 - num2);
            instance.RunSummaryLineNbr = summary.LineNbr;
            break;
        }
        instance.ProjectID = summary.ProjectID;
        instance.ProjectTaskID = summary.ProjectTaskID;
        instance.CostCodeID = summary.CostCodeID;
        this.dontSyncSummary = true;
        try
        {
          ((PXSelectBase<EPEquipmentDetail>) this.Details).Insert(instance);
        }
        finally
        {
          this.dontSyncSummary = false;
        }
      }
      else
      {
        if (epEquipmentDetail1 != null && num1 == 0)
        {
          nullable1 = epEquipmentDetail1.OrigLineNbr;
          if (!nullable1.HasValue)
          {
            this.dontSyncSummary = true;
            try
            {
              ((PXSelectBase<EPEquipmentDetail>) this.Details).Delete(epEquipmentDetail1);
              return;
            }
            finally
            {
              this.dontSyncSummary = false;
            }
          }
        }
        if (epEquipmentDetail1 == null)
          return;
        switch (summary.RateType)
        {
          case "ST":
            EPEquipmentDetail epEquipmentDetail2 = epEquipmentDetail1;
            int? nullable2 = epEquipmentDetail1.SetupTime;
            int num3 = num1;
            nullable1 = nullable2.HasValue ? new int?(nullable2.GetValueOrDefault() + num3) : new int?();
            int num4 = num2;
            int? nullable3;
            if (!nullable1.HasValue)
            {
              nullable2 = new int?();
              nullable3 = nullable2;
            }
            else
              nullable3 = new int?(nullable1.GetValueOrDefault() - num4);
            epEquipmentDetail2.SetupTime = nullable3;
            break;
          case "SD":
            EPEquipmentDetail epEquipmentDetail3 = epEquipmentDetail1;
            int? nullable4 = epEquipmentDetail1.SuspendTime;
            int num5 = num1;
            nullable1 = nullable4.HasValue ? new int?(nullable4.GetValueOrDefault() + num5) : new int?();
            int num6 = num2;
            int? nullable5;
            if (!nullable1.HasValue)
            {
              nullable4 = new int?();
              nullable5 = nullable4;
            }
            else
              nullable5 = new int?(nullable1.GetValueOrDefault() - num6);
            epEquipmentDetail3.SuspendTime = nullable5;
            break;
          default:
            EPEquipmentDetail epEquipmentDetail4 = epEquipmentDetail1;
            int? nullable6 = epEquipmentDetail1.RunTime;
            int num7 = num1;
            nullable1 = nullable6.HasValue ? new int?(nullable6.GetValueOrDefault() + num7) : new int?();
            int num8 = num2;
            int? nullable7;
            if (!nullable1.HasValue)
            {
              nullable6 = new int?();
              nullable7 = nullable6;
            }
            else
              nullable7 = new int?(nullable1.GetValueOrDefault() - num8);
            epEquipmentDetail4.RunTime = nullable7;
            break;
        }
        if (!string.IsNullOrEmpty(summary.Description))
          epEquipmentDetail1.Description = summary.Description;
        epEquipmentDetail1.IsBillable = summary.IsBillable;
        epEquipmentDetail1.ProjectID = summary.ProjectID;
        epEquipmentDetail1.ProjectTaskID = summary.ProjectTaskID;
        this.dontSyncSummary = true;
        try
        {
          ((PXSelectBase<EPEquipmentDetail>) this.Details).Update(epEquipmentDetail1);
        }
        finally
        {
          this.dontSyncSummary = false;
        }
      }
    }
    else
    {
      if (epEquipmentDetail1 == null)
        return;
      if (!string.IsNullOrEmpty(summary.Description))
        epEquipmentDetail1.Description = summary.Description;
      epEquipmentDetail1.IsBillable = summary.IsBillable;
      epEquipmentDetail1.ProjectID = summary.ProjectID;
      epEquipmentDetail1.ProjectTaskID = summary.ProjectTaskID;
      epEquipmentDetail1.CostCodeID = summary.CostCodeID;
      this.dontSyncSummary = true;
      try
      {
        ((PXSelectBase<EPEquipmentDetail>) this.Details).Update(epEquipmentDetail1);
      }
      finally
      {
        this.dontSyncSummary = false;
      }
    }
  }

  protected virtual Dictionary<DayOfWeek, EquipmentTimeCardMaint.DayDetails> GetDetailRecords(
    EPEquipmentSummary summary,
    EPEquipmentTimeCard doc)
  {
    if (summary == null)
      throw new ArgumentNullException(nameof (summary));
    if (doc == null)
      throw new ArgumentNullException(nameof (doc));
    Dictionary<DayOfWeek, EquipmentTimeCardMaint.DayDetails> detailRecords = new Dictionary<DayOfWeek, EquipmentTimeCardMaint.DayDetails>();
    foreach (EPEquipmentDetail detail in this.GetDetails(summary, doc))
    {
      DateTime? date = detail.Date;
      DateTime dateTime = date.Value;
      DayOfWeek dayOfWeek1 = dateTime.DayOfWeek;
      Dictionary<DayOfWeek, EquipmentTimeCardMaint.DayDetails> dictionary1 = detailRecords;
      date = detail.Date;
      dateTime = date.Value;
      int dayOfWeek2 = (int) dateTime.DayOfWeek;
      if (dictionary1.ContainsKey((DayOfWeek) dayOfWeek2))
      {
        Dictionary<DayOfWeek, EquipmentTimeCardMaint.DayDetails> dictionary2 = detailRecords;
        date = detail.Date;
        dateTime = date.Value;
        int dayOfWeek3 = (int) dateTime.DayOfWeek;
        dictionary2[(DayOfWeek) dayOfWeek3].Details.Add(detail);
      }
      else
      {
        EquipmentTimeCardMaint.DayDetails dayDetails1 = new EquipmentTimeCardMaint.DayDetails(summary.RateType);
        dayDetails1.Day = dayOfWeek1;
        dayDetails1.Details.Add(detail);
        Dictionary<DayOfWeek, EquipmentTimeCardMaint.DayDetails> dictionary3 = detailRecords;
        date = detail.Date;
        dateTime = date.Value;
        int dayOfWeek4 = (int) dateTime.DayOfWeek;
        EquipmentTimeCardMaint.DayDetails dayDetails2 = dayDetails1;
        dictionary3.Add((DayOfWeek) dayOfWeek4, dayDetails2);
      }
    }
    return detailRecords;
  }

  protected virtual List<EPEquipmentDetail> GetDetails(
    EPEquipmentSummary summary,
    EPEquipmentTimeCard doc)
  {
    if (summary == null)
      throw new ArgumentNullException(nameof (summary));
    if (doc == null)
      throw new ArgumentNullException(nameof (doc));
    List<object> objectList = ((PXSelectBase) new PXSelect<EPEquipmentDetail, Where<EPEquipmentDetail.timeCardCD, Equal<Current<EPEquipmentSummary.timeCardCD>>, And<Where<EPEquipmentDetail.setupSummarylineNbr, Equal<Current<EPEquipmentSummary.lineNbr>>, Or<EPEquipmentDetail.runSummarylineNbr, Equal<Current<EPEquipmentSummary.lineNbr>>, Or<EPEquipmentDetail.suspendSummarylineNbr, Equal<Current<EPEquipmentSummary.lineNbr>>>>>>>>((PXGraph) this)).View.SelectMultiBound(new object[2]
    {
      (object) summary,
      (object) doc
    }, Array.Empty<object>());
    List<EPEquipmentDetail> details = new List<EPEquipmentDetail>(objectList.Count);
    foreach (EPEquipmentDetail epEquipmentDetail in objectList)
      details.Add(epEquipmentDetail);
    return details;
  }

  protected virtual void ResetRateType(EPEquipmentSummary summary, string oldRateType)
  {
    EPEquipmentTimeCard doc = PXResultset<EPEquipmentTimeCard>.op_Implicit(PXSelectBase<EPEquipmentTimeCard, PXSelect<EPEquipmentTimeCard, Where<EPEquipmentTimeCard.timeCardCD, Equal<Required<EPEquipmentTimeCard.timeCardCD>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) summary.TimeCardCD
    }));
    if (doc == null)
      return;
    foreach (EPEquipmentDetail detail in this.GetDetails(summary, doc))
    {
      int num = 0;
      switch (oldRateType)
      {
        case "RU":
          int? nullable1 = detail.RunTime;
          num = nullable1.GetValueOrDefault();
          EPEquipmentDetail epEquipmentDetail1 = detail;
          nullable1 = new int?();
          int? nullable2 = nullable1;
          epEquipmentDetail1.RunTime = nullable2;
          detail.RunSummaryLineNbr = new int?();
          break;
        case "ST":
          int? nullable3 = detail.SetupTime;
          num = nullable3.GetValueOrDefault();
          EPEquipmentDetail epEquipmentDetail2 = detail;
          nullable3 = new int?();
          int? nullable4 = nullable3;
          epEquipmentDetail2.SetupTime = nullable4;
          detail.SetupSummaryLineNbr = new int?();
          break;
        case "SD":
          int? nullable5 = detail.SuspendTime;
          num = nullable5.GetValueOrDefault();
          EPEquipmentDetail epEquipmentDetail3 = detail;
          nullable5 = new int?();
          int? nullable6 = nullable5;
          epEquipmentDetail3.SuspendTime = nullable6;
          detail.SuspendSummaryLineNbr = new int?();
          break;
      }
      if (summary.RateType == "RU")
      {
        if (!detail.RunSummaryLineNbr.HasValue)
        {
          detail.RunTime = new int?(num);
          detail.RunSummaryLineNbr = summary.LineNbr;
        }
      }
      else if (summary.RateType == "ST")
      {
        int? nullable7 = detail.SetupSummaryLineNbr;
        if (!nullable7.HasValue)
        {
          EPEquipmentDetail epEquipmentDetail4 = detail;
          nullable7 = detail.SetupTime;
          int? nullable8 = new int?(nullable7.GetValueOrDefault() + num);
          epEquipmentDetail4.SetupTime = nullable8;
          detail.SetupSummaryLineNbr = summary.LineNbr;
        }
      }
      else if (summary.RateType == "SD")
      {
        int? nullable9 = detail.SuspendSummaryLineNbr;
        if (!nullable9.HasValue)
        {
          EPEquipmentDetail epEquipmentDetail5 = detail;
          nullable9 = detail.SuspendTime;
          int? nullable10 = new int?(nullable9.GetValueOrDefault() + num);
          epEquipmentDetail5.SuspendTime = nullable10;
          detail.SuspendSummaryLineNbr = summary.LineNbr;
        }
      }
      this.dontSyncSummary = true;
      try
      {
        ((PXSelectBase<EPEquipmentDetail>) this.Details).Update(detail);
      }
      finally
      {
        this.dontSyncSummary = false;
      }
    }
  }

  protected virtual void RecalculateTotals(EPEquipmentTimeCard timecard)
  {
    if (timecard == null)
      throw new ArgumentNullException();
    List<EPEquipmentDetail> details = new List<EPEquipmentDetail>();
    foreach (PXResult<EPEquipmentDetail> pxResult in ((PXSelectBase<EPEquipmentDetail>) this.Details).Select(Array.Empty<object>()))
    {
      EPEquipmentDetail epEquipmentDetail = PXResult<EPEquipmentDetail>.op_Implicit(pxResult);
      details.Add(epEquipmentDetail);
    }
    this.RecalculateTotals(timecard, details);
  }

  protected virtual void RecalculateTotals(
    EPEquipmentTimeCard timecard,
    List<EPEquipmentDetail> details)
  {
    if (timecard == null)
      throw new ArgumentNullException(nameof (timecard));
    if (details == null)
      throw new ArgumentNullException(nameof (details));
    int num1 = 0;
    int num2 = 0;
    int num3 = 0;
    int num4 = 0;
    int num5 = 0;
    int num6 = 0;
    foreach (EPEquipmentDetail detail in details)
    {
      int num7 = num1;
      int? nullable = detail.SetupTime;
      int valueOrDefault1 = nullable.GetValueOrDefault();
      num1 = num7 + valueOrDefault1;
      int num8 = num2;
      nullable = detail.RunTime;
      int valueOrDefault2 = nullable.GetValueOrDefault();
      num2 = num8 + valueOrDefault2;
      int num9 = num3;
      nullable = detail.SuspendTime;
      int valueOrDefault3 = nullable.GetValueOrDefault();
      num3 = num9 + valueOrDefault3;
      if (detail.IsBillable.GetValueOrDefault())
      {
        int num10 = num4;
        nullable = detail.SetupTime;
        int valueOrDefault4 = nullable.GetValueOrDefault();
        num4 = num10 + valueOrDefault4;
        int num11 = num5;
        nullable = detail.RunTime;
        int valueOrDefault5 = nullable.GetValueOrDefault();
        num5 = num11 + valueOrDefault5;
        int num12 = num6;
        nullable = detail.SuspendTime;
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
  }

  protected virtual bool IsFirstTimeCard(int? equipmentID)
  {
    if (!equipmentID.HasValue)
      return true;
    return PXSelectBase<EPEquipmentTimeCard, PXSelectReadonly<EPEquipmentTimeCard, Where<EPEquipmentTimeCard.equipmentID, Equal<Required<EPEquipmentTimeCard.equipmentID>>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, new object[1]
    {
      (object) equipmentID
    }).Count == 0;
  }

  protected virtual int? GetNextWeekID(int? equipmentID)
  {
    if (!this.IsFirstTimeCard(equipmentID))
    {
      EPEquipmentTimeCard equipmentTimeCard = PXResultset<EPEquipmentTimeCard>.op_Implicit(PXSelectBase<EPEquipmentTimeCard, PXSelectReadonly<EPEquipmentTimeCard, Where<EPEquipmentTimeCard.equipmentID, Equal<Required<EPEquipmentTimeCard.equipmentID>>>, OrderBy<Desc<EPEquipmentTimeCard.weekId>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, new object[1]
      {
        (object) equipmentID
      }));
      if (equipmentTimeCard != null)
      {
        int? weekId = equipmentTimeCard.WeekID;
        if (weekId.HasValue)
        {
          weekId = equipmentTimeCard.WeekID;
          return new int?(PXWeekSelector2Attribute.GetNextWeekID((PXGraph) this, weekId.Value));
        }
      }
    }
    return new int?(((PXGraph) this).Accessinfo.BusinessDate.With<DateTime, int>((Func<DateTime, int>) (_ => PXWeekSelector2Attribute.GetWeekID((PXGraph) this, _))));
  }

  private int? GetCurrentDateWeekId()
  {
    return new int?(((PXGraph) this).Accessinfo.BusinessDate.With<DateTime, int>((Func<DateTime, int>) (_ => PXWeekSelector2Attribute.GetWeekID((PXGraph) this, _))));
  }

  protected virtual void InsertPMTran(
    RegisterEntry pmGraph,
    EPEquipmentDetail row,
    int? inventoryID,
    int? minutes,
    Decimal? rate,
    int? equipmentID)
  {
    EPSetup epSetup = PXResultset<EPSetup>.op_Implicit(PXSelectBase<EPSetup, PXSelect<EPSetup>.Config>.Select((PXGraph) this, Array.Empty<object>()));
    bool flag = epSetup?.PostingOption == "O";
    PX.Objects.IN.InventoryItem inventoryItem = PXResultset<PX.Objects.IN.InventoryItem>.op_Implicit(PXSelectBase<PX.Objects.IN.InventoryItem, PXSelect<PX.Objects.IN.InventoryItem, Where<PX.Objects.IN.InventoryItem.inventoryID, Equal<Required<PX.Objects.IN.InventoryItem.inventoryID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) inventoryID
    }));
    if (inventoryItem == null || !minutes.HasValue)
      return;
    if (!flag && !inventoryItem.InvtAcctID.HasValue)
      throw new PXException("Expense Accrual Account is Required but is not configured for Non-Stock Item '{0}'. Please setup the account and try again.", new object[1]
      {
        (object) inventoryItem.InventoryCD.Trim()
      });
    if (!flag && !inventoryItem.InvtSubID.HasValue)
      throw new PXException("Expense Accrual Subaccount is Required but is not configured for Non-Stock Item '{0}'. Please setup the subaccount and try again.", new object[1]
      {
        (object) inventoryItem.InventoryCD.Trim()
      });
    PMProject pmProject = PMProject.PK.Find((PXGraph) this, row.ProjectID);
    Decimal num = INUnitAttribute.ConvertGlobalUnits((PXGraph) this, this.ActivityTimeUnit, inventoryItem.BaseUnit, (Decimal) minutes.GetValueOrDefault(), INPrecision.QUANTITY);
    int? nullable1 = inventoryItem.COGSAcctID;
    int? nullable2 = inventoryItem.InvtAcctID;
    int? nullable3 = new int?();
    string str1 = (string) null;
    string str2 = (string) null;
    if (!pmProject.NonProject.GetValueOrDefault())
    {
      PMTask dirty = PMTask.PK.FindDirty((PXGraph) this, row.ProjectID, row.ProjectTaskID);
      EPEquipment epEquipment = PXResultset<EPEquipment>.op_Implicit(PXSelectBase<EPEquipment, PXSelect<EPEquipment, Where<EPEquipment.equipmentID, Equal<Required<EPEquipment.equipmentID>>>>.Config>.Select((PXGraph) this, new object[1]
      {
        (object) equipmentID
      }));
      int? nullable4;
      if (this.ExpenseAccountSource == "P")
      {
        if (pmProject.DefaultExpenseAccountID.HasValue)
        {
          nullable1 = pmProject.DefaultExpenseAccountID;
          PX.Objects.GL.Account account = PXResultset<PX.Objects.GL.Account>.op_Implicit(PXSelectBase<PX.Objects.GL.Account, PXSelect<PX.Objects.GL.Account, Where<PX.Objects.GL.Account.accountID, Equal<Required<PX.Objects.GL.Account.accountID>>>>.Config>.Select((PXGraph) this, new object[1]
          {
            (object) nullable1
          }));
          nullable4 = account.AccountGroupID;
          if (!nullable4.HasValue)
            throw new PXException("Project preferences have been configured to get the expense account from project tasks, but the {0} default cost account specified for the {1} task of the {2} project is not mapped to any account group.", new object[2]
            {
              (object) account.AccountCD.Trim(),
              (object) pmProject.ContractCD.Trim()
            });
          nullable3 = account.AccountGroupID;
        }
        else
          PXTrace.WriteWarning("Project preferences have been configured to get the expense account from projects, but the default cost account is not specified for the {0} project.", new object[1]
          {
            (object) pmProject.ContractCD.Trim()
          });
      }
      else if (this.ExpenseAccountSource == "T")
      {
        if (dirty.DefaultExpenseAccountID.HasValue)
        {
          nullable1 = dirty.DefaultExpenseAccountID;
          PX.Objects.GL.Account account = PXResultset<PX.Objects.GL.Account>.op_Implicit(PXSelectBase<PX.Objects.GL.Account, PXSelect<PX.Objects.GL.Account, Where<PX.Objects.GL.Account.accountID, Equal<Required<PX.Objects.GL.Account.accountID>>>>.Config>.Select((PXGraph) this, new object[1]
          {
            (object) nullable1
          }));
          nullable4 = account.AccountGroupID;
          if (!nullable4.HasValue)
            throw new PXException("Project preferences have been configured to get the expense account from project tasks, but the {0} default cost account of the {2} task of the {1} project is not mapped to any account group.", new object[3]
            {
              (object) account.AccountCD.Trim(),
              (object) pmProject.ContractCD.Trim(),
              (object) dirty.TaskCD.Trim()
            });
          nullable3 = account.AccountGroupID;
        }
        else
          PXTrace.WriteWarning("Project preferences have been configured to get the expense account from project tasks, but the default cost account is not specified for the {0} task of the {1} project.", new object[2]
          {
            (object) pmProject.ContractCD.Trim(),
            (object) dirty.TaskCD.Trim()
          });
      }
      else if (this.ExpenseAccountSource == "E")
      {
        if (epEquipment.DefaultAccountID.HasValue)
        {
          nullable1 = epEquipment.DefaultAccountID;
          PX.Objects.GL.Account account = PXResultset<PX.Objects.GL.Account>.op_Implicit(PXSelectBase<PX.Objects.GL.Account, PXSelect<PX.Objects.GL.Account, Where<PX.Objects.GL.Account.accountID, Equal<Required<PX.Objects.GL.Account.accountID>>>>.Config>.Select((PXGraph) this, new object[1]
          {
            (object) nullable1
          }));
          nullable4 = account.AccountGroupID;
          if (!nullable4.HasValue)
            throw new PXException("Project preferences have been configured to get the expense account from resources, but the {0} expense account for the {1} equipment is not mapped to any account group.", new object[2]
            {
              (object) account.AccountCD.Trim(),
              (object) epEquipment.EquipmentCD.Trim()
            });
          nullable3 = account.AccountGroupID;
        }
        else
          PXTrace.WriteWarning("Project preferences have been configured to get the expense account from equipment, but the default account is not specified for the {0} equipment.", new object[1]
          {
            (object) epEquipment.EquipmentCD.Trim()
          });
      }
      else
      {
        PX.Objects.GL.Account account = nullable1.HasValue ? PXResultset<PX.Objects.GL.Account>.op_Implicit(PXSelectBase<PX.Objects.GL.Account, PXSelect<PX.Objects.GL.Account, Where<PX.Objects.GL.Account.accountID, Equal<Required<PX.Objects.GL.Account.accountID>>>>.Config>.Select((PXGraph) this, new object[1]
        {
          (object) nullable1
        })) : throw new PXException("Project preferences have been configured to get the expense account from inventory items, but the expense account is not specified for the {0} inventory item.", new object[1]
        {
          (object) inventoryItem.InventoryCD.Trim()
        });
        nullable4 = account.AccountGroupID;
        if (!nullable4.HasValue)
          throw new PXException("Project preferences have been configured to get the expense account from inventory items, but the {0} expense account of the {1} inventory item is not mapped to any account group.", new object[2]
          {
            (object) account.AccountCD.Trim(),
            (object) inventoryItem.InventoryCD.Trim()
          });
        nullable3 = account.AccountGroupID;
      }
      if (!nullable3.HasValue)
      {
        PX.Objects.GL.Account account = PXResultset<PX.Objects.GL.Account>.op_Implicit(PXSelectBase<PX.Objects.GL.Account, PXSelect<PX.Objects.GL.Account, Where<PX.Objects.GL.Account.accountID, Equal<Required<PX.Objects.GL.Account.accountID>>>>.Config>.Select((PXGraph) this, new object[1]
        {
          (object) nullable1
        }));
        nullable4 = account.AccountGroupID;
        if (!nullable4.HasValue)
          throw new PXException("Expense Account '{0}' is not included in any Account Group. Please assign an Account Group given Account and try again.", new object[1]
          {
            (object) account.AccountCD.Trim()
          });
        nullable3 = account.AccountGroupID;
      }
      if (!string.IsNullOrEmpty(this.ExpenseSubMask))
      {
        if (this.ExpenseSubMask.Contains("I"))
        {
          nullable4 = inventoryItem.COGSSubID;
          if (!nullable4.HasValue)
            throw new PXException("Project preferences have been configured to combine the expense subaccount from inventory items, but the expense subaccount is not specified for the {0} inventory item.", new object[1]
            {
              (object) inventoryItem.InventoryCD.Trim()
            });
        }
        if (this.ExpenseSubMask.Contains("P"))
        {
          nullable4 = pmProject.DefaultExpenseSubID;
          if (!nullable4.HasValue)
            throw new PXException("Project preferences have been configured to combine the expense subaccount from projects, but the expense subaccount is not specified for the {0} project.", new object[1]
            {
              (object) pmProject.ContractCD.Trim()
            });
        }
        if (this.ExpenseSubMask.Contains("T"))
        {
          nullable4 = dirty.DefaultExpenseSubID;
          if (!nullable4.HasValue)
            throw new PXException("Project preferences have been configured to combine the expense subaccount from project tasks, but the expense subaccount is not specified for the {1} task of the {0} project.", new object[2]
            {
              (object) pmProject.ContractCD.Trim(),
              (object) dirty.TaskCD.Trim()
            });
        }
        if (this.ExpenseSubMask.Contains("E"))
        {
          nullable4 = epEquipment.DefaultSubID;
          if (!nullable4.HasValue)
            throw new PXException("Project preferences have been configured to combine the expense subaccount from resources, but the default subaccount is not specified for the {0} equipment.", new object[1]
            {
              (object) epEquipment.EquipmentCD.Trim()
            });
        }
        str1 = PX.Objects.PM.SubAccountMaskAttribute.MakeSub<PMSetup.expenseSubMask>((PXGraph) this, this.ExpenseSubMask, new object[4]
        {
          (object) inventoryItem.COGSSubID,
          (object) pmProject.DefaultExpenseSubID,
          (object) dirty.DefaultExpenseSubID,
          (object) epEquipment.DefaultSubID
        }, new Type[4]
        {
          typeof (PX.Objects.IN.InventoryItem.cOGSSubID),
          typeof (PMProject.defaultExpenseSubID),
          typeof (PMTask.defaultExpenseSubID),
          typeof (EPEquipment.defaultSubID)
        });
      }
      int? accrualAccountId;
      if (this.ExpenseAccrualAccountSource == "P")
      {
        nullable4 = pmProject.DefaultAccrualAccountID;
        if (nullable4.HasValue)
          accrualAccountId = pmProject.DefaultAccrualAccountID;
        else
          PXTrace.WriteWarning("Project preferences have been configured to get the expense accrual account from projects, but the default accrual account is not specified for the {0} project.", new object[1]
          {
            (object) pmProject.ContractCD.Trim()
          });
      }
      else if (this.ExpenseAccrualAccountSource == "T")
      {
        nullable4 = dirty.DefaultAccrualAccountID;
        if (nullable4.HasValue)
          accrualAccountId = dirty.DefaultAccrualAccountID;
        else
          PXTrace.WriteWarning("Project preferences have been configured to get the expense account from project tasks, but the default cost account is not specified for the {0} task of the {1} project.", new object[2]
          {
            (object) pmProject.ContractCD.Trim(),
            (object) dirty.TaskCD.Trim()
          });
      }
      else
      {
        nullable4 = inventoryItem.InvtAcctID;
        if (nullable4.HasValue && !nullable2.HasValue)
          throw new PXException("Project preferences have been configured to get the expense accrual account from inventory items, but the expense accrual account is not specified for the {0} inventory item.", new object[1]
          {
            (object) inventoryItem.InventoryCD.Trim()
          });
      }
      if (!string.IsNullOrEmpty(this.ExpenseAccrualSubMask))
      {
        if (!flag && this.ExpenseAccrualSubMask.Contains("I"))
        {
          nullable4 = inventoryItem.InvtSubID;
          if (!nullable4.HasValue)
            throw new PXException("Project preferences have been configured to combine the expense accrual subaccount from inventory items, but the expense accrual subaccount is not specified for the {0} inventory item.", new object[1]
            {
              (object) inventoryItem.InventoryCD.Trim()
            });
        }
        if (this.ExpenseAccrualSubMask.Contains("P"))
        {
          nullable4 = pmProject.DefaultAccrualSubID;
          if (!nullable4.HasValue)
            throw new PXException("Project preferences have been configured to combine the expense accrual subaccount from projects, but the expense accrual subaccount is not specified for the {0} project.", new object[1]
            {
              (object) pmProject.ContractCD.Trim()
            });
        }
        if (this.ExpenseAccrualSubMask.Contains("T"))
        {
          nullable4 = dirty.DefaultAccrualSubID;
          if (!nullable4.HasValue)
            throw new PXException("Project preferences have been configured to combine the expense accrual subaccount from project tasks, but the expense accrual subaccount is not specified for the {1} task of the {0} project.", new object[2]
            {
              (object) pmProject.ContractCD.Trim(),
              (object) dirty.TaskCD.Trim()
            });
        }
        if (this.ExpenseAccrualSubMask.Contains("E"))
        {
          nullable4 = epEquipment.DefaultSubID;
          if (!nullable4.HasValue)
            throw new PXException("Project preferences have been configured to combine the expense subaccount from resources, but the default subaccount is not specified for the {0} equipment.", new object[1]
            {
              (object) epEquipment.EquipmentCD.Trim()
            });
        }
        nullable4 = inventoryItem.InvtSubID;
        if (nullable4.HasValue)
          str2 = PX.Objects.PM.SubAccountMaskAttribute.MakeSub<PMSetup.expenseAccrualSubMask>((PXGraph) this, this.ExpenseAccrualSubMask, new object[4]
          {
            (object) inventoryItem.InvtSubID,
            (object) pmProject.DefaultAccrualSubID,
            (object) dirty.DefaultAccrualSubID,
            (object) epEquipment.DefaultSubID
          }, new Type[4]
          {
            typeof (PX.Objects.IN.InventoryItem.invtSubID),
            typeof (PMProject.defaultAccrualSubID),
            typeof (PMTask.defaultAccrualSubID),
            typeof (EPEquipment.defaultSubID)
          });
      }
    }
    else
    {
      PX.Objects.GL.Account account = PXResultset<PX.Objects.GL.Account>.op_Implicit(PXSelectBase<PX.Objects.GL.Account, PXSelect<PX.Objects.GL.Account, Where<PX.Objects.GL.Account.accountID, Equal<Required<PX.Objects.GL.Account.accountID>>>>.Config>.Select((PXGraph) this, new object[1]
      {
        (object) nullable1
      }));
      if (!account.AccountGroupID.HasValue)
        throw new PXException("Project preferences have been configured to get the expense account from inventory items, but the {0} expense account of the {1} inventory item is not mapped to any account group.", new object[2]
        {
          (object) account.AccountCD.Trim(),
          (object) inventoryItem.InventoryCD.Trim()
        });
      nullable3 = account.AccountGroupID;
    }
    int? cogsSubId = inventoryItem.COGSSubID;
    int? nullable5 = inventoryItem.InvtSubID;
    if (flag)
    {
      nullable3 = epSetup.OffBalanceAccountGroupID;
      nullable1 = new int?();
      nullable2 = new int?();
      nullable5 = new int?();
      str1 = (string) null;
    }
    PMTran pmTran = (PMTran) ((PXSelectBase) pmGraph.Transactions).Cache.Insert();
    pmTran.AccountID = nullable1;
    if (string.IsNullOrEmpty(str1))
      pmTran.SubID = inventoryItem.COGSSubID;
    if (string.IsNullOrEmpty(str2))
      pmTran.OffsetSubID = nullable5;
    pmTran.AccountGroupID = nullable3;
    pmTran.ProjectID = row.ProjectID;
    pmTran.TaskID = row.ProjectTaskID;
    pmTran.CostCodeID = row.CostCodeID;
    pmTran.InventoryID = inventoryID;
    pmTran.Description = row.Description;
    pmTran.Qty = new Decimal?(num);
    pmTran.Billable = row.IsBillable;
    pmTran.BillableQty = new Decimal?(num);
    pmTran.UOM = inventoryItem.BaseUnit;
    pmTran.TranCuryUnitRate = rate;
    pmTran.OffsetAccountID = inventoryItem.InvtAcctID;
    pmTran.Date = row.Date;
    pmTran.StartDate = row.Date;
    pmTran.EndDate = row.Date;
    ((PXSelectBase<PMTran>) pmGraph.Transactions).Update(pmTran);
    if (!string.IsNullOrEmpty(str1))
      ((PXSelectBase<PMTran>) pmGraph.Transactions).SetValueExt<PMTran.subID>(pmTran, (object) str1);
    if (!string.IsNullOrEmpty(str2))
      ((PXSelectBase<PMTran>) pmGraph.Transactions).SetValueExt<PMTran.offsetSubID>(pmTran, (object) str2);
    PXNoteAttribute.CopyNoteAndFiles(((PXSelectBase) this.Details).Cache, (object) row, ((PXSelectBase) pmGraph.Transactions).Cache, (object) pmTran, ((PXSelectBase<EPSetup>) this.Setup).Current.GetCopyNoteSettings<PXModule.pm>());
  }

  protected virtual Decimal? GetRunCost(int? equipmentID, int? projectID)
  {
    EPEquipmentRate epEquipmentRate = PXResultset<EPEquipmentRate>.op_Implicit(PXSelectBase<EPEquipmentRate, PXSelect<EPEquipmentRate, Where<EPEquipmentRate.equipmentID, Equal<Required<EPEquipmentRate.equipmentID>>, And<EPEquipmentRate.projectID, Equal<Required<EPEquipmentRate.projectID>>>>>.Config>.Select((PXGraph) this, new object[2]
    {
      (object) equipmentID,
      (object) projectID
    }));
    if (epEquipmentRate != null)
      return epEquipmentRate.RunRate;
    EPEquipment epEquipment = PXResultset<EPEquipment>.op_Implicit(PXSelectBase<EPEquipment, PXSelect<EPEquipment, Where<EPEquipment.equipmentID, Equal<Required<EPEquipment.equipmentID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) equipmentID
    }));
    if (epEquipment != null)
      return epEquipment.RunRate;
    PXTrace.WriteWarning("Failed to determine Run Cost for Equipment. Equipment with the given id was not found in the system. EquipmentID={0}", new object[1]
    {
      (object) equipmentID
    });
    return new Decimal?();
  }

  protected virtual Decimal? GetSetupCost(int? equipmentID, int? projectID)
  {
    EPEquipmentRate epEquipmentRate = PXResultset<EPEquipmentRate>.op_Implicit(PXSelectBase<EPEquipmentRate, PXSelect<EPEquipmentRate, Where<EPEquipmentRate.equipmentID, Equal<Required<EPEquipmentRate.equipmentID>>, And<EPEquipmentRate.projectID, Equal<Required<EPEquipmentRate.projectID>>>>>.Config>.Select((PXGraph) this, new object[2]
    {
      (object) equipmentID,
      (object) projectID
    }));
    if (epEquipmentRate != null)
      return epEquipmentRate.SetupRate;
    EPEquipment epEquipment = PXResultset<EPEquipment>.op_Implicit(PXSelectBase<EPEquipment, PXSelect<EPEquipment, Where<EPEquipment.equipmentID, Equal<Required<EPEquipment.equipmentID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) equipmentID
    }));
    if (epEquipment != null)
      return epEquipment.SetupRate;
    PXTrace.WriteWarning("Failed to determine Setup Cost for Equipment. Equipment with the given id was not found in the system. EquipmentID={0}", new object[1]
    {
      (object) equipmentID
    });
    return new Decimal?();
  }

  protected virtual Decimal? GetSuspendCost(int? equipmentID, int? projectID)
  {
    EPEquipmentRate epEquipmentRate = PXResultset<EPEquipmentRate>.op_Implicit(PXSelectBase<EPEquipmentRate, PXSelect<EPEquipmentRate, Where<EPEquipmentRate.equipmentID, Equal<Required<EPEquipmentRate.equipmentID>>, And<EPEquipmentRate.projectID, Equal<Required<EPEquipmentRate.projectID>>>>>.Config>.Select((PXGraph) this, new object[2]
    {
      (object) equipmentID,
      (object) projectID
    }));
    if (epEquipmentRate != null)
      return epEquipmentRate.SuspendRate;
    EPEquipment epEquipment = PXResultset<EPEquipment>.op_Implicit(PXSelectBase<EPEquipment, PXSelect<EPEquipment, Where<EPEquipment.equipmentID, Equal<Required<EPEquipment.equipmentID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) equipmentID
    }));
    if (epEquipment != null)
      return epEquipment.SuspendRate;
    PXTrace.WriteWarning("Failed to determine Suspend Cost for Equipment. Equipment with the given id was not found in the system. EquipmentID={0}", new object[1]
    {
      (object) equipmentID
    });
    return new Decimal?();
  }

  private bool HasDuplicate(int? equipmentID, int? weekId, string timeCardCD)
  {
    return PXResultset<EPEquipmentTimeCard>.op_Implicit(PXSelectBase<EPEquipmentTimeCard, PXSelectReadonly<EPEquipmentTimeCard, Where<EPEquipmentTimeCard.equipmentID, Equal<Required<EPEquipmentTimeCard.equipmentID>>, And<EPEquipmentTimeCard.weekId, Equal<Required<EPEquipmentTimeCard.weekId>>, And<EPEquipmentTimeCard.timeCardCD, NotEqual<Required<EPEquipmentTimeCard.timeCardCD>>>>>>.Config>.SelectSingleBound((PXGraph) this, (object[]) null, new object[3]
    {
      (object) equipmentID,
      (object) weekId,
      (object) timeCardCD
    })) != null;
  }

  public class DayDetails
  {
    private string rateType;
    public List<EPEquipmentDetail> Details;
    public DayOfWeek Day;

    public DayDetails(string rateType)
    {
      this.rateType = rateType;
      this.Details = new List<EPEquipmentDetail>();
    }

    public int GetTotalTime()
    {
      int totalTime = 0;
      foreach (EPEquipmentDetail detail in this.Details)
      {
        switch (this.rateType)
        {
          case "ST":
            totalTime += detail.SetupTime.GetValueOrDefault();
            continue;
          case "SD":
            totalTime += detail.SuspendTime.GetValueOrDefault();
            continue;
          default:
            totalTime += detail.RunTime.GetValueOrDefault();
            continue;
        }
      }
      return totalTime;
    }

    public EPEquipmentDetail GetAdjustingActivity()
    {
      return this.Details.Count > 0 ? this.Details[this.Details.Count - 1] : (EPEquipmentDetail) null;
    }
  }

  [PXHidden]
  public class EPEquipmentDetailOrig : EPEquipmentDetail
  {
    public new abstract class timeCardCD : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      EquipmentTimeCardMaint.EPEquipmentDetailOrig.timeCardCD>
    {
    }

    public new abstract class lineNbr : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      EquipmentTimeCardMaint.EPEquipmentDetailOrig.lineNbr>
    {
    }

    public new abstract class origLineNbr : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      EquipmentTimeCardMaint.EPEquipmentDetailOrig.origLineNbr>
    {
    }
  }

  [PXHidden]
  public class EPEquipmentDetailEx : EPEquipmentDetail
  {
    public new abstract class timeCardCD : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      EquipmentTimeCardMaint.EPEquipmentDetailEx.timeCardCD>
    {
    }

    public new abstract class lineNbr : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      EquipmentTimeCardMaint.EPEquipmentDetailEx.lineNbr>
    {
    }

    public new abstract class origLineNbr : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      EquipmentTimeCardMaint.EPEquipmentDetailEx.origLineNbr>
    {
    }
  }
}
