// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.TimeCardMaint
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Data.EP;
using PX.Data.Localization;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Data.WorkflowAPI;
using PX.Objects.CR;
using PX.Objects.CT;
using PX.Objects.GL.FinPeriods;
using PX.Objects.GL.FinPeriods.TableDefinition;
using PX.Objects.IN;
using PX.Objects.PM;
using PX.Objects.PR.Standalone;
using PX.TM;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;

#nullable enable
namespace PX.Objects.EP;

[Serializable]
public class TimeCardMaint : PXGraph<
#nullable disable
TimeCardMaint, EPTimeCard>, PXImportAttribute.IPXPrepareItems
{
  [PXViewName("Employee")]
  public PXSetup<EPEmployee>.Where<BqlOperand<
  #nullable enable
  EPEmployee.bAccountID, IBqlInt>.IsEqual<
  #nullable disable
  BqlField<
  #nullable enable
  EPTimeCard.employeeID, IBqlInt>.AsOptional>> Employee;
  [PXHidden]
  public 
  #nullable disable
  PXSetup<EPSetup> EpSetup;
  [PXHidden]
  public PXSetup<PMSetup> PmSetup;
  [PXHidden]
  public PXSelect<EPEarningType> EarningTypes;
  [PXHidden]
  public PXSelect<PX.Objects.CR.BAccount> AccountBase;
  [PXViewName("Document")]
  public PXSelectJoin<EPTimeCard, InnerJoin<EPEmployee, On<EPEmployee.bAccountID, Equal<EPTimeCard.employeeID>>>, Where<EPTimeCard.createdByID, Equal<Current<AccessInfo.userID>>, Or<EPEmployee.defContactID, Equal<Current<AccessInfo.contactID>>, Or<EPEmployee.defContactID, IsSubordinateOfContact<Current<AccessInfo.contactID>>, Or<EPTimeCard.noteID, Approver<Current<AccessInfo.contactID>>, Or<EPTimeCard.employeeID, WingmanUser<Current<AccessInfo.userID>, EPDelegationOf.timeEntries>>>>>>> Document;
  [PXHidden]
  public PXFilter<CRActivityMaint.EPTempData> TempData;
  [PXImport(typeof (EPTimeCard))]
  [PXViewName("Time Card Summary")]
  public PXSelect<TimeCardMaint.EPTimeCardSummaryWithInfo, Where<EPTimeCardSummary.timeCardCD, Equal<Current<EPTimeCard.timeCardCD>>>, PX.Data.OrderBy<Asc<EPTimeCardSummary.lineNbr>>> Summary;
  protected Dictionary<string, TimeCardMaint.SummaryRecord> sumList = new Dictionary<string, TimeCardMaint.SummaryRecord>();
  protected Dictionary<string, TimeCardMaint.EPTimeCardSummaryWithInfo> existingSummaryRowsByKeys = new Dictionary<string, TimeCardMaint.EPTimeCardSummaryWithInfo>();
  protected Dictionary<int, TimeCardMaint.EPTimeCardSummaryWithInfo> existingSummaryRowsByLineNbr = new Dictionary<int, TimeCardMaint.EPTimeCardSummaryWithInfo>();
  [PXViewName("Time Card Detail")]
  public PXSelect<TimeCardMaint.EPTimecardDetail, Where<PMTimeActivity.trackTime, Equal<True>>, PX.Data.OrderBy<Asc<TimeCardMaint.EPTimecardDetail.date>>> Activities;
  [PXHidden]
  public PXSelect<TimeCardMaint.EPTimecardDetail, Where<PMTimeActivity.ownerID, Equal<Current<EPEmployee.defContactID>>, And<TimeCardMaint.EPTimecardDetail.weekID, Equal<Current<EPTimeCard.weekId>>, And<PMTimeActivity.trackTime, Equal<True>, And<PMTimeActivity.approvalStatus, NotEqual<ActivityStatusListAttribute.canceled>, PX.Data.And<Where<TimeCardMaint.EPTimecardDetail.timeCardCD, PX.Data.IsNull, Or<TimeCardMaint.EPTimecardDetail.timeCardCD, Equal<Current<EPTimeCard.timeCardCD>>>>>>>>>, PX.Data.OrderBy<Asc<TimeCardMaint.EPTimecardDetail.date>>> AllActivities;
  [PXHidden]
  public PXSelect<TimeCardMaint.EPTimecardDetail, Where<TimeCardMaint.EPTimecardDetail.timeCardCD, Equal<Current<EPTimeCard.timeCardCD>>>, PX.Data.OrderBy<Asc<TimeCardMaint.EPTimecardDetail.date>>> TimecardActivities;
  [PXImport(typeof (EPTimeCardItem))]
  [PXCopyPasteHiddenFields(new System.Type[] {typeof (EPTimeCardItem.noteID), typeof (Note.noteText)})]
  public PXSelect<EPTimeCardItem, Where<EPTimeCardItem.timeCardCD, Equal<Current<EPTimeCard.timeCardCD>>>, PX.Data.OrderBy<Asc<EPTimeCardItem.lineNbr>>> Items;
  public PXSelectJoin<TimeCardMaint.EPTimecardTask, InnerJoin<CREmployee, On<CREmployee.defContactID, Equal<TimeCardMaint.EPTimecardTask.ownerID>>>, Where<CREmployee.bAccountID, Equal<Current<EPTimeCard.employeeID>>, And<TimeCardMaint.EPTimecardTask.classID, Equal<CRActivityClass.task>>>> Tasks;
  [PXViewName("Approval")]
  public TimecardApprovalAction Approval;
  /// <summary>
  /// When True activities billable field is not updated when created TC correction.
  /// </summary>
  protected bool isCreateCorrectionFlag;
  /// <summary>
  /// When True Summary records are not updated as a result of a detail row update.
  /// </summary>
  protected bool dontSyncSummary;
  /// <summary>
  /// When True detail row is not updated when a summary record is modified.
  /// </summary>
  protected bool dontSyncDetails;
  protected bool skipProjectDefaultingFromEarningType;
  protected bool skipValidation;
  public PXAction<EPTimeCard> viewActivity;
  public PXAction<EPTimeCard> submit;
  public PXAction<EPTimeCard> viewOrigTimecard;
  protected EmployeeCostEngine costEngine;
  public PXAction<EPTimeCard> edit;
  public PXAction<EPTimeCard> release;
  public PXAction<EPTimeCard> correct;
  public PXAction<EPTimeCard> preloadFromTasks;
  public PXAction<EPTimeCard> preloadFromPreviousTimecard;
  public PXAction<EPTimeCard> preloadHolidays;
  public PXAction<EPTimeCard> normalizeTimecard;
  public PXAction<EPTimeCard> View;
  public PXAction<EPTimeCard> viewPMTran;
  public PXAction<EPTimeCard> ViewContract;
  public PXWorkflowEventHandler<EPTimeCard> OnUpdateStatus;

  [PXDefault(typeof (EPTimeCard.createdDateTime), PersistingCheck = PXPersistingCheck.Nothing)]
  [PXMergeAttributes(Method = MergeMethod.Merge)]
  protected virtual void EPApproval_DocDate_CacheAttached(PXCache sender)
  {
  }

  [PXDefault(typeof (EPTimeCard.employeeID), PersistingCheck = PXPersistingCheck.Nothing)]
  [PXMergeAttributes(Method = MergeMethod.Merge)]
  protected virtual void EPApproval_BAccountID_CacheAttached(PXCache sender)
  {
  }

  [PXDefault(typeof (FbqlSelect<SelectFromBase<PX.Objects.CR.Contact, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<EPEmployee>.On<EPEmployee.FK.ContactInfo>>>.Where<BqlOperand<EPEmployee.bAccountID, IBqlInt>.IsEqual<BqlField<EPTimeCard.employeeID, IBqlInt>.FromCurrent>>, PX.Objects.CR.Contact>.SearchFor<PX.Objects.CR.Contact.contactID>), PersistingCheck = PXPersistingCheck.Nothing)]
  [PXMergeAttributes(Method = MergeMethod.Merge)]
  protected virtual void EPApproval_DocumentOwnerID_CacheAttached(PXCache sender)
  {
  }

  public virtual IEnumerable summary()
  {
    EPTimeCard document = this.Document.Current;
    if (PX.Data.PXView.Currents != null)
    {
      foreach (object current in PX.Data.PXView.Currents)
      {
        if (current is EPTimeCard epTimeCard)
          document = epTimeCard;
      }
    }
    if (document == null)
      return (IEnumerable) new List<TimeCardMaint.EPTimeCardSummaryWithInfo>();
    return !document.WeekID.HasValue ? (IEnumerable) new List<TimeCardMaint.EPTimeCardSummaryWithInfo>() : (IEnumerable) this.SelectSummaryRecords(document, true);
  }

  public virtual IList<TimeCardMaint.EPTimeCardSummaryWithInfo> SelectSummaryRecords(
    EPTimeCard document,
    bool autocorrect)
  {
    if (document == null)
      throw new ArgumentNullException(nameof (document));
    this.SelectExistingSummaryRecords(document.TimeCardCD);
    this.ProcessActivities(autocorrect);
    return (IList<TimeCardMaint.EPTimeCardSummaryWithInfo>) this.BuildSummaryList(document, autocorrect);
  }

  private void SelectExistingSummaryRecords(string timecardCD)
  {
    this.existingSummaryRowsByKeys.Clear();
    this.existingSummaryRowsByLineNbr.Clear();
    foreach (PXResult<TimeCardMaint.EPTimeCardSummaryWithInfo> pxResult in new PXSelect<TimeCardMaint.EPTimeCardSummaryWithInfo, Where<EPTimeCardSummary.timeCardCD, Equal<Required<EPTimeCard.timeCardCD>>>, PX.Data.OrderBy<Asc<EPTimeCardSummary.lineNbr>>>((PXGraph) this).Select((object) timecardCD))
    {
      TimeCardMaint.EPTimeCardSummaryWithInfo summary = (TimeCardMaint.EPTimeCardSummaryWithInfo) pxResult;
      string summaryKey = this.GetSummaryKey((EPTimeCardSummary) summary);
      if (!this.existingSummaryRowsByKeys.ContainsKey(summaryKey))
        this.existingSummaryRowsByKeys.Add(summaryKey, summary);
      Dictionary<int, TimeCardMaint.EPTimeCardSummaryWithInfo> summaryRowsByLineNbr1 = this.existingSummaryRowsByLineNbr;
      int? lineNbr = summary.LineNbr;
      int key1 = lineNbr.Value;
      if (!summaryRowsByLineNbr1.ContainsKey(key1))
      {
        Dictionary<int, TimeCardMaint.EPTimeCardSummaryWithInfo> summaryRowsByLineNbr2 = this.existingSummaryRowsByLineNbr;
        lineNbr = summary.LineNbr;
        int key2 = lineNbr.Value;
        TimeCardMaint.EPTimeCardSummaryWithInfo cardSummaryWithInfo = summary;
        summaryRowsByLineNbr2.Add(key2, cardSummaryWithInfo);
      }
    }
  }

  private void ProcessActivities(bool autocorrect)
  {
    this.sumList.Clear();
    foreach (PXResult activity in this.activities())
    {
      TimeCardMaint.EPTimecardDetail epTimecardDetail = (TimeCardMaint.EPTimecardDetail) activity[typeof (TimeCardMaint.EPTimecardDetail)];
      if (!string.IsNullOrEmpty(epTimecardDetail.EarningTypeID))
      {
        string summaryKey = this.GetSummaryKey(epTimecardDetail);
        TimeCardMaint.SummaryRecord summaryRecord1 = (TimeCardMaint.SummaryRecord) null;
        TimeCardMaint.EPTimeCardSummaryWithInfo summary1 = (TimeCardMaint.EPTimeCardSummaryWithInfo) null;
        int? nullable;
        if (this.existingSummaryRowsByKeys.TryGetValue(summaryKey, out summary1))
        {
          TimeCardMaint.EPTimeCardSummaryWithInfo summary2 = (TimeCardMaint.EPTimeCardSummaryWithInfo) null;
          nullable = epTimecardDetail.SummaryLineNbr;
          if (nullable.HasValue)
          {
            Dictionary<int, TimeCardMaint.EPTimeCardSummaryWithInfo> summaryRowsByLineNbr = this.existingSummaryRowsByLineNbr;
            nullable = epTimecardDetail.SummaryLineNbr;
            int key = nullable.Value;
            ref TimeCardMaint.EPTimeCardSummaryWithInfo local = ref summary2;
            if (summaryRowsByLineNbr.TryGetValue(key, out local))
            {
              if (this.GetSummaryKey((EPTimeCardSummary) summary1) == this.GetSummaryKey((EPTimeCardSummary) summary2))
                summary1 = summary2;
              else if (autocorrect)
              {
                this.Activities.Cache.SetValue<PMTimeActivity.summaryLineNbr>((object) epTimecardDetail, (object) null);
                this.Activities.Cache.MarkUpdated((object) epTimecardDetail);
              }
            }
            else if (autocorrect)
            {
              this.Activities.Cache.SetValue<PMTimeActivity.summaryLineNbr>((object) epTimecardDetail, (object) null);
              this.Activities.Cache.MarkUpdated((object) epTimecardDetail);
            }
          }
        }
        if (summary1 != null)
        {
          Dictionary<string, TimeCardMaint.SummaryRecord> sumList1 = this.sumList;
          nullable = summary1.LineNbr;
          string key1 = nullable.ToString();
          if (sumList1.ContainsKey(key1))
          {
            Dictionary<string, TimeCardMaint.SummaryRecord> sumList2 = this.sumList;
            nullable = summary1.LineNbr;
            string key2 = nullable.ToString();
            summaryRecord1 = sumList2[key2];
          }
        }
        else if (this.sumList.ContainsKey(summaryKey))
          summaryRecord1 = this.sumList[summaryKey];
        nullable = epTimecardDetail.SummaryLineNbr;
        if (nullable.HasValue)
        {
          if (summary1 != null)
          {
            if (summaryRecord1 == null)
            {
              summaryRecord1 = new TimeCardMaint.SummaryRecord(summary1);
              Dictionary<string, TimeCardMaint.SummaryRecord> sumList = this.sumList;
              nullable = summaryRecord1.Summary.LineNbr;
              string key = nullable.ToString();
              TimeCardMaint.SummaryRecord summaryRecord2 = summaryRecord1;
              sumList.Add(key, summaryRecord2);
            }
            summaryRecord1.LinkedDetails.Add(epTimecardDetail);
          }
          else
          {
            if (autocorrect)
            {
              this.Activities.Cache.SetValue<PMTimeActivity.summaryLineNbr>((object) epTimecardDetail, (object) null);
              this.Activities.Cache.MarkUpdated((object) epTimecardDetail);
            }
            if (summaryRecord1 == null)
            {
              summaryRecord1 = new TimeCardMaint.SummaryRecord((TimeCardMaint.EPTimeCardSummaryWithInfo) null);
              this.sumList.Add(summaryKey, summaryRecord1);
            }
            summaryRecord1.SummaryKey = summaryKey;
            summaryRecord1.NotLinkedDetails.Add(epTimecardDetail);
          }
        }
        else
        {
          if (summary1 != null)
          {
            if (summaryRecord1 == null)
            {
              summaryRecord1 = new TimeCardMaint.SummaryRecord(summary1);
              Dictionary<string, TimeCardMaint.SummaryRecord> sumList = this.sumList;
              nullable = summaryRecord1.Summary.LineNbr;
              string key = nullable.ToString();
              TimeCardMaint.SummaryRecord summaryRecord3 = summaryRecord1;
              sumList.Add(key, summaryRecord3);
            }
          }
          else
          {
            if (autocorrect)
            {
              this.Activities.Cache.SetValue<PMTimeActivity.summaryLineNbr>((object) epTimecardDetail, (object) null);
              this.Activities.Cache.MarkUpdated((object) epTimecardDetail);
            }
            if (summaryRecord1 == null)
            {
              summaryRecord1 = new TimeCardMaint.SummaryRecord((TimeCardMaint.EPTimeCardSummaryWithInfo) null);
              this.sumList.Add(summaryKey, summaryRecord1);
            }
            summaryRecord1.SummaryKey = summaryKey;
          }
          summaryRecord1.NotLinkedDetails.Add(epTimecardDetail);
        }
      }
    }
  }

  private List<TimeCardMaint.EPTimeCardSummaryWithInfo> BuildSummaryList(
    EPTimeCard document,
    bool autocorrect)
  {
    List<TimeCardMaint.EPTimeCardSummaryWithInfo> cardSummaryWithInfoList = new List<TimeCardMaint.EPTimeCardSummaryWithInfo>();
    EPTimeCard epTimeCard1 = document;
    EPTimeCard epTimeCard2 = document;
    EPTimeCard epTimeCard3 = document;
    EPTimeCard epTimeCard4 = document;
    EPTimeCard epTimeCard5 = document;
    EPTimeCard epTimeCard6 = document;
    EPTimeCard epTimeCard7 = document;
    int? nullable1 = new int?(0);
    int? nullable2 = nullable1;
    epTimeCard7.SatTotal = nullable2;
    int? nullable3;
    int? nullable4 = nullable3 = nullable1;
    epTimeCard6.FriTotal = nullable3;
    int? nullable5;
    int? nullable6 = nullable5 = nullable4;
    epTimeCard5.ThuTotal = nullable5;
    int? nullable7;
    int? nullable8 = nullable7 = nullable6;
    epTimeCard4.WedTotal = nullable7;
    int? nullable9;
    int? nullable10 = nullable9 = nullable8;
    epTimeCard3.TueTotal = nullable9;
    int? nullable11;
    int? nullable12 = nullable11 = nullable10;
    epTimeCard2.MonTotal = nullable11;
    int? nullable13 = nullable12;
    epTimeCard1.SunTotal = nullable13;
    foreach (TimeCardMaint.EPTimeCardSummaryWithInfo cardSummaryWithInfo in this.existingSummaryRowsByLineNbr.Values)
    {
      Dictionary<string, TimeCardMaint.SummaryRecord> sumList1 = this.sumList;
      int? nullable14 = cardSummaryWithInfo.LineNbr;
      string key1 = nullable14.ToString();
      if (sumList1.ContainsKey(key1))
      {
        Dictionary<string, TimeCardMaint.SummaryRecord> sumList2 = this.sumList;
        nullable14 = cardSummaryWithInfo.LineNbr;
        string key2 = nullable14.ToString();
        TimeCardMaint.EPTimeCardSummaryWithInfo summary = sumList2[key2].Summary;
        if (autocorrect)
        {
          bool flag = false;
          Dictionary<string, TimeCardMaint.SummaryRecord> sumList3 = this.sumList;
          nullable14 = cardSummaryWithInfo.LineNbr;
          string key3 = nullable14.ToString();
          EPTimeCardSummary epTimeCardSummary = sumList3[key3].SummariseDetails();
          nullable14 = summary.Mon;
          int valueOrDefault1 = nullable14.GetValueOrDefault();
          nullable14 = epTimeCardSummary.Mon;
          int valueOrDefault2 = nullable14.GetValueOrDefault();
          if (valueOrDefault1 != valueOrDefault2)
          {
            flag = true;
            summary.Mon = epTimeCardSummary.Mon;
          }
          nullable14 = summary.Tue;
          int valueOrDefault3 = nullable14.GetValueOrDefault();
          nullable14 = epTimeCardSummary.Tue;
          int valueOrDefault4 = nullable14.GetValueOrDefault();
          if (valueOrDefault3 != valueOrDefault4)
          {
            flag = true;
            summary.Tue = epTimeCardSummary.Tue;
          }
          nullable14 = summary.Wed;
          int valueOrDefault5 = nullable14.GetValueOrDefault();
          nullable14 = epTimeCardSummary.Wed;
          int valueOrDefault6 = nullable14.GetValueOrDefault();
          if (valueOrDefault5 != valueOrDefault6)
          {
            flag = true;
            summary.Wed = epTimeCardSummary.Wed;
          }
          nullable14 = summary.Thu;
          int valueOrDefault7 = nullable14.GetValueOrDefault();
          nullable14 = epTimeCardSummary.Thu;
          int valueOrDefault8 = nullable14.GetValueOrDefault();
          if (valueOrDefault7 != valueOrDefault8)
          {
            flag = true;
            summary.Thu = epTimeCardSummary.Thu;
          }
          nullable14 = summary.Fri;
          int valueOrDefault9 = nullable14.GetValueOrDefault();
          nullable14 = epTimeCardSummary.Fri;
          int valueOrDefault10 = nullable14.GetValueOrDefault();
          if (valueOrDefault9 != valueOrDefault10)
          {
            flag = true;
            summary.Fri = epTimeCardSummary.Fri;
          }
          nullable14 = summary.Sat;
          int valueOrDefault11 = nullable14.GetValueOrDefault();
          nullable14 = epTimeCardSummary.Sat;
          int valueOrDefault12 = nullable14.GetValueOrDefault();
          if (valueOrDefault11 != valueOrDefault12)
          {
            flag = true;
            summary.Sat = epTimeCardSummary.Sat;
          }
          nullable14 = summary.Sun;
          int valueOrDefault13 = nullable14.GetValueOrDefault();
          nullable14 = epTimeCardSummary.Sun;
          int valueOrDefault14 = nullable14.GetValueOrDefault();
          if (valueOrDefault13 != valueOrDefault14)
          {
            flag = true;
            summary.Sun = epTimeCardSummary.Sun;
          }
          if (flag & autocorrect)
            this.Summary.Cache.MarkUpdated((object) summary);
        }
        this.AddDayTotal(document, summary);
        cardSummaryWithInfoList.Add(summary);
      }
      else
      {
        if (this.Summary.Cache.GetStatus((object) cardSummaryWithInfo) == PXEntryStatus.Notchanged & autocorrect)
        {
          bool flag = false;
          nullable14 = cardSummaryWithInfo.Mon;
          if (nullable14.GetValueOrDefault() != 0)
          {
            cardSummaryWithInfo.Mon = new int?(0);
            flag = true;
          }
          nullable14 = cardSummaryWithInfo.Tue;
          if (nullable14.GetValueOrDefault() != 0)
          {
            cardSummaryWithInfo.Tue = new int?(0);
            flag = true;
          }
          nullable14 = cardSummaryWithInfo.Wed;
          if (nullable14.GetValueOrDefault() != 0)
          {
            cardSummaryWithInfo.Wed = new int?(0);
            flag = true;
          }
          nullable14 = cardSummaryWithInfo.Thu;
          if (nullable14.GetValueOrDefault() != 0)
          {
            cardSummaryWithInfo.Thu = new int?(0);
            flag = true;
          }
          nullable14 = cardSummaryWithInfo.Fri;
          if (nullable14.GetValueOrDefault() != 0)
          {
            cardSummaryWithInfo.Fri = new int?(0);
            flag = true;
          }
          nullable14 = cardSummaryWithInfo.Sat;
          if (nullable14.GetValueOrDefault() != 0)
          {
            cardSummaryWithInfo.Sat = new int?(0);
            flag = true;
          }
          nullable14 = cardSummaryWithInfo.Sun;
          if (nullable14.GetValueOrDefault() != 0)
          {
            cardSummaryWithInfo.Sun = new int?(0);
            flag = true;
          }
          if (flag)
            this.Summary.Cache.MarkUpdated((object) cardSummaryWithInfo);
        }
        this.AddDayTotal(document, cardSummaryWithInfo);
        cardSummaryWithInfoList.Add(cardSummaryWithInfo);
      }
    }
    foreach (TimeCardMaint.SummaryRecord summaryRecord in this.sumList.Values)
    {
      if (summaryRecord.SummaryKey != null & autocorrect)
      {
        List<TimeCardMaint.EPTimecardDetail> notLinkedDetails = summaryRecord.NotLinkedDetails;
        TimeCardMaint.EPTimeCardSummaryWithInfo summary = (TimeCardMaint.EPTimeCardSummaryWithInfo) null;
        foreach (TimeCardMaint.EPTimecardDetail epTimecardDetail in notLinkedDetails)
        {
          if (epTimecardDetail.TimeSpent.GetValueOrDefault() != 0)
          {
            try
            {
              this.skipValidation = true;
              summary = this.AddToSummary(summary, (PMTimeActivity) epTimecardDetail);
            }
            catch (PXFieldProcessingException ex)
            {
              summary = (TimeCardMaint.EPTimeCardSummaryWithInfo) null;
              if (ex.FieldName == "ParentNoteID")
                this.Activities.Cache.RaiseExceptionHandling<TimeCardMaint.EPTimecardDetail.parentTaskNoteID>((object) epTimecardDetail, (object) epTimecardDetail.Summary, (Exception) new PXSetPropertyException("Activity cannot be synced with the Summary records. Task not found, please clear Task field to continue.", PXErrorLevel.RowError));
              else
                this.Activities.Cache.RaiseExceptionHandling<PMTimeActivity.summary>((object) epTimecardDetail, (object) epTimecardDetail.Summary, (Exception) new PXSetPropertyException("Activity cannot be synced with the Summary records.{0}", PXErrorLevel.RowError, new object[1]
                {
                  (object) (Environment.NewLine + ex.MessageNoPrefix)
                }));
            }
            catch (PXSetPropertyException ex)
            {
              summary = (TimeCardMaint.EPTimeCardSummaryWithInfo) null;
              this.Activities.Cache.RaiseExceptionHandling<PMTimeActivity.summary>((object) epTimecardDetail, (object) epTimecardDetail.Summary, (Exception) new PXSetPropertyException("Activity cannot be synced with the Summary records.{0}", PXErrorLevel.RowError, new object[1]
              {
                (object) (Environment.NewLine + ex.MessageNoPrefix)
              }));
            }
            finally
            {
              this.skipValidation = false;
            }
          }
        }
        if (summary != null)
        {
          this.AddDayTotal(document, summary);
          cardSummaryWithInfoList.Add(summary);
        }
      }
    }
    return cardSummaryWithInfoList;
  }

  private void AddDayTotal(EPTimeCard document, TimeCardMaint.EPTimeCardSummaryWithInfo summary)
  {
    EPTimeCard epTimeCard1 = document;
    int? nullable = epTimeCard1.SunTotal;
    int valueOrDefault1 = summary.Sun.GetValueOrDefault();
    epTimeCard1.SunTotal = nullable.HasValue ? new int?(nullable.GetValueOrDefault() + valueOrDefault1) : new int?();
    EPTimeCard epTimeCard2 = document;
    nullable = epTimeCard2.MonTotal;
    int valueOrDefault2 = summary.Mon.GetValueOrDefault();
    epTimeCard2.MonTotal = nullable.HasValue ? new int?(nullable.GetValueOrDefault() + valueOrDefault2) : new int?();
    EPTimeCard epTimeCard3 = document;
    nullable = epTimeCard3.TueTotal;
    int valueOrDefault3 = summary.Tue.GetValueOrDefault();
    epTimeCard3.TueTotal = nullable.HasValue ? new int?(nullable.GetValueOrDefault() + valueOrDefault3) : new int?();
    EPTimeCard epTimeCard4 = document;
    nullable = epTimeCard4.WedTotal;
    int valueOrDefault4 = summary.Wed.GetValueOrDefault();
    epTimeCard4.WedTotal = nullable.HasValue ? new int?(nullable.GetValueOrDefault() + valueOrDefault4) : new int?();
    EPTimeCard epTimeCard5 = document;
    nullable = epTimeCard5.ThuTotal;
    int valueOrDefault5 = summary.Thu.GetValueOrDefault();
    epTimeCard5.ThuTotal = nullable.HasValue ? new int?(nullable.GetValueOrDefault() + valueOrDefault5) : new int?();
    EPTimeCard epTimeCard6 = document;
    nullable = epTimeCard6.FriTotal;
    int valueOrDefault6 = summary.Fri.GetValueOrDefault();
    epTimeCard6.FriTotal = nullable.HasValue ? new int?(nullable.GetValueOrDefault() + valueOrDefault6) : new int?();
    EPTimeCard epTimeCard7 = document;
    nullable = epTimeCard7.SatTotal;
    int valueOrDefault7 = summary.Sat.GetValueOrDefault();
    epTimeCard7.SatTotal = nullable.HasValue ? new int?(nullable.GetValueOrDefault() + valueOrDefault7) : new int?();
  }

  public virtual IEnumerable activities()
  {
    EPTimeCard document = this.Document.Current;
    if (PX.Data.PXView.Currents != null)
    {
      foreach (object current in PX.Data.PXView.Currents)
      {
        if (current is EPTimeCard epTimeCard)
          document = epTimeCard;
      }
    }
    if (document == null)
      return (IEnumerable) new TimeCardMaint.EPTimecardDetail[0];
    if (!document.WeekID.HasValue)
      return (IEnumerable) new TimeCardMaint.EPTimecardDetail[0];
    return !this.CanSelectAllDetailsByTimeCardCD(document) ? (IEnumerable) this.AllActivities.Select() : (IEnumerable) this.TimecardActivities.Select();
  }

  [InjectDependency]
  public IFinPeriodRepository FinPeriodRepository { get; set; }

  public TimeCardMaint()
  {
    if (this.EpSetup.Current?.TimeCardNumberingID == null)
      throw new PXSetupNotEnteredException("The required configuration data is not entered on the {0} form.", typeof (EPSetup), new object[1]
      {
        (object) typeof (EPSetup).Name
      });
    PXUIFieldAttribute.SetEnabled<PMTimeActivity.overtimeSpent>(this.Activities.Cache, (object) null, !this.ShowActivityTime);
    PXDBDateAndTimeAttribute.SetTimeVisible<TimeCardMaint.EPTimecardDetail.date>(this.Activities.Cache, (object) null, this.ShowActivityTime);
    if (!PXAccess.FeatureInstalled<PX.Objects.CS.FeaturesSet.timeReportingModule>())
    {
      this.preloadFromTasks.SetVisible(false);
      PXUIFieldAttribute.SetVisible<EPTimeCardSummary.parentNoteID>(this.Summary.Cache, (object) null, false);
      PXUIFieldAttribute.SetVisible<PMTimeActivity.approvalStatus>(this.Activities.Cache, (object) null, false);
    }
    if (!PXAccess.FeatureInstalled<PX.Objects.CS.FeaturesSet.projectAccounting>())
      PXUIFieldAttribute.SetVisible<PMTimeActivity.approvalStatus>(this.Activities.Cache, (object) null, false);
    if (!PXAccess.FeatureInstalled<PX.Objects.CS.FeaturesSet.contractManagement>())
      PXUIFieldAttribute.SetVisible<TimeCardMaint.ContractEx.contractCD>(this.Caches[typeof (TimeCardMaint.ContractEx)], (object) null, false);
    if (!PXAccess.FeatureInstalled<PX.Objects.CS.FeaturesSet.customerModule>())
      PXUIFieldAttribute.SetVisible<CRCase.caseCD>(this.Caches[typeof (CRCase)], (object) null, false);
    string field = typeof (TimeCardMaint.EPTimecardDetail.date).Name + "_Date";
    this.FieldUpdating.AddHandler(typeof (TimeCardMaint.EPTimecardDetail), field, new PXFieldUpdating(this.EPTimecardDetail_StartDate_Date_FieldUpdating));
    this.FieldUpdated.AddHandler(typeof (TimeCardMaint.EPTimecardDetail), field, new PXFieldUpdated(this.EPTimecardDetail_StartDate_Date_FieldUpdated));
    this.FieldVerifying.AddHandler(typeof (TimeCardMaint.EPTimecardDetail), field, new PXFieldVerifying(this.EPTimecardDetail_StartDate_Date_FieldVerifying));
    this.FieldUpdating.AddHandler(typeof (TimeCardMaint.EPTimecardDetail), typeof (TimeCardMaint.EPTimecardDetail.date).Name + "_Time", new PXFieldUpdating(this.EPTimecardDetail_StartDate_Time_FieldUpdating));
    PXDimensionAttribute.SuppressAutoNumbering<TimeCardMaint.EPTimecardDetail.contractCD>(this.Activities.Cache, true);
  }

  public override bool CanClipboardCopyPaste() => false;

  public void CheckAllowedUser()
  {
    if ((EPEmployee) PXSelectBase<EPEmployee, PXSelect<EPEmployee, Where<EPEmployee.userID, Equal<Current<AccessInfo.userID>>>>.Config>.Select((PXGraph) this) != null || PXGraph.ProxyIsActive)
      return;
    if (this.IsExport || this.IsImport)
      throw new PXException("User must be an Employee to use current screen.");
    Redirector.Redirect(HttpContext.Current, $"~/Frames/Error.aspx?exceptionID={"User must be an Employee to use current screen."}&typeID={"error"}");
  }

  public static IEnumerable QSelect(PXGraph graph, BqlCommand bqlCommand, object[] parameters)
  {
    PX.Data.PXView pxView = new PX.Data.PXView(graph, false, bqlCommand);
    int startRow = PX.Data.PXView.StartRow;
    int num = 0;
    Stopwatch stopwatch = new Stopwatch();
    stopwatch.Start();
    object[] currents = PX.Data.PXView.Currents;
    object[] parameters1 = parameters;
    object[] searches = PX.Data.PXView.Searches;
    string[] sortColumns = PX.Data.PXView.SortColumns;
    bool[] descendings = PX.Data.PXView.Descendings;
    PXFilterRow[] filters = (PXFilterRow[]) PX.Data.PXView.Filters;
    ref int local1 = ref startRow;
    int maximumRows = PX.Data.PXView.MaximumRows;
    ref int local2 = ref num;
    List<object> objectList = pxView.Select(currents, parameters1, searches, sortColumns, descendings, filters, ref local1, maximumRows, ref local2);
    stopwatch.Stop();
    PX.Data.PXView.StartRow = 0;
    return (IEnumerable) objectList;
  }

  public virtual string GetSummaryKey(TimeCardMaint.EPTimecardDetail activity)
  {
    return $"{activity.EarningTypeID.ToUpper().Trim()}.{activity.ProjectID.GetValueOrDefault()}.{activity.ProjectTaskID ?? -1}.{activity.IsBillable.GetValueOrDefault()}.{activity.ParentTaskNoteID ?? Guid.Empty}.{activity.JobID ?? -1}.{activity.ShiftID ?? -1}.{activity.CostCodeID ?? -1}.{activity.UnionID}.{activity.LabourItemID ?? -1}.{activity.WorkCodeID}.{activity.CertifiedJob.GetValueOrDefault()}";
  }

  public virtual string GetSummaryKey(EPTimeCardSummary summary)
  {
    return $"{summary.EarningType.ToUpper().Trim()}.{summary.ProjectID.GetValueOrDefault()}.{summary.ProjectTaskID ?? -1}.{summary.IsBillable.GetValueOrDefault()}.{summary.ParentNoteID ?? Guid.Empty}.{summary.JobID ?? -1}.{summary.ShiftID ?? -1}.{summary.CostCodeID ?? -1}.{summary.UnionID}.{summary.LabourItemID ?? -1}.{summary.WorkCodeID}.{summary.CertifiedJob.GetValueOrDefault()}";
  }

  /// <summary>Gets the source for the generated PMTran.AccountID</summary>
  public string ExpenseAccountSource
  {
    get
    {
      return !string.IsNullOrEmpty(this.PmSetup.Current?.ExpenseAccountSource) ? this.PmSetup.Current.ExpenseAccountSource : "I";
    }
  }

  public string ExpenseSubMask
  {
    get
    {
      return !string.IsNullOrEmpty(this.PmSetup.Current?.ExpenseSubMask) ? this.PmSetup.Current.ExpenseSubMask : (string) null;
    }
  }

  public string ExpenseAccrualAccountSource
  {
    get
    {
      return !string.IsNullOrEmpty(this.PmSetup.Current?.ExpenseAccountSource) ? this.PmSetup.Current.ExpenseAccountSource : "I";
    }
  }

  public string ExpenseAccrualSubMask
  {
    get
    {
      return !string.IsNullOrEmpty(this.PmSetup.Current?.ExpenseAccrualSubMask) ? this.PmSetup.Current.ExpenseAccrualSubMask : (string) null;
    }
  }

  public string ActivityTimeUnit
  {
    get
    {
      return !string.IsNullOrEmpty(this.EpSetup.Current?.ActivityTimeUnit) ? this.EpSetup.Current.ActivityTimeUnit : "MINUTE";
    }
  }

  public string EmployeeRateUnit
  {
    get
    {
      return !string.IsNullOrEmpty(this.EpSetup.Current?.EmployeeRateUnit) ? this.EpSetup.Current.EmployeeRateUnit : "HOUR";
    }
  }

  public bool ShowActivityTime
  {
    get
    {
      EPSetup current = this.EpSetup.Current;
      return current != null && current.RequireTimes.GetValueOrDefault();
    }
  }

  [PXUIField(Visible = false)]
  [PXButton]
  public virtual IEnumerable ViewActivity(PXAdapter adapter)
  {
    if (this.Activities.Current != null)
    {
      this.Save.Press();
      PXRedirectHelper.TryRedirect((PXGraph) this, (object) this.Activities.Current, PXRedirectHelper.WindowMode.NewWindow);
    }
    return adapter.Get();
  }

  [PXUIField(DisplayName = "Submit")]
  [PXButton]
  public virtual void Submit()
  {
    this.Actions.PressSave();
    this.OnSubmitClicked();
  }

  [PXUIField(Visible = false, MapEnableRights = PXCacheRights.Select, MapViewRights = PXCacheRights.Select)]
  [PXLookupButton]
  public virtual IEnumerable ViewOrigTimecard(PXAdapter adapter)
  {
    if (!string.IsNullOrEmpty(this.Document.Current?.OrigTimeCardCD))
    {
      EPTimeCard row = (EPTimeCard) this.Document.Search<EPTimeCard.timeCardCD>((object) this.Document.Current.OrigTimeCardCD);
      if (row != null)
        PXRedirectHelper.TryRedirect((PXGraph) this, (object) row, PXRedirectHelper.WindowMode.Same);
    }
    return adapter.Get();
  }

  public EmployeeCostEngine CostEngine
  {
    get
    {
      if (this.costEngine == null)
        this.costEngine = this.CreateEmployeeCostEngine();
      return this.costEngine;
    }
  }

  public virtual bool RequireApproved() => false;

  public virtual void OnSubmitClicked()
  {
    if (this.Document.Current == null)
      return;
    List<TimeCardMaint.EPTimecardDetail> details = new List<TimeCardMaint.EPTimecardDetail>();
    List<int?> nullableList = new List<int?>();
    HashSet<TimeCardMaint.ActivityValidationError> activityValidationErrorSet = new HashSet<TimeCardMaint.ActivityValidationError>();
    EPEmployee current = this.Employee.Current;
    foreach (PXResult<TimeCardMaint.EPTimecardDetail> activity in this.activities())
    {
      TimeCardMaint.EPTimecardDetail epTimecardDetail = (TimeCardMaint.EPTimecardDetail) activity;
      CRCase refCase = (CRCase) PXSelectorAttribute.Select<TimeCardMaint.EPTimecardDetail.caseCD>(this.Activities.Cache, (object) epTimecardDetail, (object) epTimecardDetail.CaseCD);
      PMProject pr = (PMProject) PXSelectorAttribute.Select<TimeCardMaint.EPTimecardDetail.projectID>(this.Activities.Cache, (object) epTimecardDetail, (object) epTimecardDetail.ProjectID);
      if (pr == null)
      {
        string str = $"'{PXUIFieldAttribute.GetDisplayName<TimeCardMaint.EPTimecardDetail.projectID>(this.Activities.Cache)}' cannot be empty.";
        PXUIFieldAttribute.SetError<TimeCardMaint.EPTimecardDetail.projectID>(this.Activities.Cache, (object) epTimecardDetail, str);
        throw new PXException(str);
      }
      if (string.IsNullOrEmpty(this.Document.Current.OrigTimeCardCD))
        PMActiveLaborItemAttribute.VerifyLaborItem<TimeCardMaint.EPTimecardDetail.labourItemID>(this.Activities.Cache, (object) epTimecardDetail);
      int? laborClass = this.CostEngine.GetLaborClass((PMTimeActivity) epTimecardDetail, current, refCase);
      bool? nullable = epTimecardDetail.Released;
      if (!nullable.GetValueOrDefault())
      {
        if (!laborClass.HasValue)
        {
          nullable = epTimecardDetail.IsOvertimeCalc;
          if (nullable.GetValueOrDefault())
            activityValidationErrorSet.Add(TimeCardMaint.ActivityValidationError.OvertimeLaborClassNotSpecified);
          else
            activityValidationErrorSet.Add(TimeCardMaint.ActivityValidationError.LaborClassNotSpecified);
        }
        List<TimeCardMaint.ActivityValidationError> errors = this.ValidateActivityOnSubmit(epTimecardDetail, pr);
        EnumerableExtensions.AddRange<TimeCardMaint.ActivityValidationError>((ISet<TimeCardMaint.ActivityValidationError>) activityValidationErrorSet, (IEnumerable<TimeCardMaint.ActivityValidationError>) errors);
        this.DisplayErrors(epTimecardDetail, pr, errors);
      }
      nullableList.Add(laborClass);
      details.Add(epTimecardDetail);
    }
    if (activityValidationErrorSet.Count > 0)
    {
      StringBuilder stringBuilder = new StringBuilder();
      if (activityValidationErrorSet.Contains(TimeCardMaint.ActivityValidationError.ActivityIsNotCompleted))
        stringBuilder.AppendLine(PXMessages.LocalizeNoPrefix("The time card includes one or multiple time activities with the Open status. All time activities must be completed before the time card may be submitted for approval."));
      if (activityValidationErrorSet.Contains(TimeCardMaint.ActivityValidationError.ActivityIsNotApproved) && this.RequireApproved())
        stringBuilder.AppendLine(PXMessages.LocalizeNoPrefix("The time card includes one or multiple time activities that require approval by project manager. All time activities must be approved before the time card may be submitted for approval."));
      if (activityValidationErrorSet.Contains(TimeCardMaint.ActivityValidationError.ActivityIsRejected))
        stringBuilder.AppendLine(PXMessages.LocalizeNoPrefix("The time card includes one or multiple time activities that require approval by project manager. All time activities must be approved before the time card may be submitted for approval."));
      if (activityValidationErrorSet.Contains(TimeCardMaint.ActivityValidationError.ProjectIsNotActive) || activityValidationErrorSet.Contains(TimeCardMaint.ActivityValidationError.ProjectIsCompleted) || activityValidationErrorSet.Contains(TimeCardMaint.ActivityValidationError.ProjectTaskIsCancelled) || activityValidationErrorSet.Contains(TimeCardMaint.ActivityValidationError.ProjectTaskIsCompleted) || activityValidationErrorSet.Contains(TimeCardMaint.ActivityValidationError.ProjectTaskIsNotActive))
        stringBuilder.AppendLine(PXMessages.LocalizeNoPrefix("There is one or more open activities referencing Inactive project. Please Activate Project to proceed with approval."));
      if (activityValidationErrorSet.Contains(TimeCardMaint.ActivityValidationError.LaborClassNotSpecified) || activityValidationErrorSet.Contains(TimeCardMaint.ActivityValidationError.OvertimeLaborClassNotSpecified))
        stringBuilder.AppendLine(PXMessages.LocalizeNoPrefix("The time card cannot be submitted. Specify a labor item either in the time card lines or on the Employees (EP203000) form."));
      string message = stringBuilder.ToString();
      if (!string.IsNullOrEmpty(message))
        throw new PXException(message);
    }
    this.RecalculateTotals(this.Document.Current, details);
    string errorMsg;
    if (!this.ValidateTotals(this.Document.Current, out errorMsg) && current.HoursValidation == "V")
      throw new PXException($"{PXMessages.LocalizeNoPrefix("Time Card is not valid. Please correct and try again.")} {errorMsg}");
    for (int index = 0; index < details.Count; ++index)
    {
      bool flag = false;
      if (!details[index].Released.GetValueOrDefault())
      {
        int? labourItemId = details[index].LabourItemID;
        int? nullable = nullableList[index];
        if (!(labourItemId.GetValueOrDefault() == nullable.GetValueOrDefault() & labourItemId.HasValue == nullable.HasValue))
        {
          TimeCardMaint.EPTimeCardSummaryWithInfo summaryRecord = this.GetSummaryRecord(details[index]);
          this.Summary.Cache.SetValue<TimeCardMaint.EPTimeCardSummaryWithInfo.labourItemID>((object) summaryRecord, (object) nullableList[index]);
          this.Summary.Cache.MarkUpdated((object) summaryRecord);
          this.Activities.Cache.SetValue<TimeCardMaint.EPTimecardDetail.labourItemID>((object) details[index], (object) nullableList[index]);
          flag = true;
        }
      }
      if (details[index].TimeCardCD != this.Document.Current.TimeCardCD)
      {
        this.Activities.Cache.SetValue<TimeCardMaint.EPTimecardDetail.timeCardCD>((object) details[index], (object) this.Document.Current.TimeCardCD);
        flag = true;
      }
      if (!details[index].Released.GetValueOrDefault())
      {
        EmployeeCostEngine.LaborCost employeeCost = this.CostEngine.CalculateEmployeeCost(details[index].TimeCardCD, details[index].EarningTypeID, details[index].LabourItemID, details[index].ProjectID, details[index].ProjectTaskID, details[index].CertifiedJob, details[index].UnionID, this.Document.Current.EmployeeID, details[index].Date.Value, details[index].ShiftID);
        if (employeeCost != null)
        {
          Decimal? rate = employeeCost.Rate;
          if (rate.HasValue)
          {
            rate = employeeCost.Rate;
            Decimal? employeeRate = details[index].EmployeeRate;
            if (!(rate.GetValueOrDefault() == employeeRate.GetValueOrDefault() & rate.HasValue == employeeRate.HasValue))
            {
              this.Activities.Cache.SetValue<PMTimeActivity.employeeRate>((object) details[index], (object) employeeCost.Rate);
              flag = true;
            }
          }
        }
      }
      if (flag)
        this.Activities.Cache.MarkUpdated((object) details[index]);
    }
    this.Document.Current.IsHold = new bool?(false);
    this.Document.Current.TimeSpent = this.Document.Current.TimeSpentCalc;
    this.Document.Current.OvertimeSpent = this.Document.Current.OvertimeSpentCalc;
    this.Document.Current.TimeBillable = this.Document.Current.TimeBillableCalc;
    this.Document.Current.OvertimeBillable = this.Document.Current.OvertimeBillableCalc;
    this.Document.Update(this.Document.Current);
    this.Save.Press();
  }

  public virtual List<TimeCardMaint.ActivityValidationError> ValidateActivityOnSubmit(
    TimeCardMaint.EPTimecardDetail act,
    PMProject pr)
  {
    List<TimeCardMaint.ActivityValidationError> activityValidationErrorList = new List<TimeCardMaint.ActivityValidationError>();
    switch (act.ApprovalStatus)
    {
      case "OP":
        activityValidationErrorList.Add(TimeCardMaint.ActivityValidationError.ActivityIsNotCompleted);
        break;
      case "PA":
        activityValidationErrorList.Add(TimeCardMaint.ActivityValidationError.ActivityIsNotApproved);
        break;
      case "RJ":
        activityValidationErrorList.Add(TimeCardMaint.ActivityValidationError.ActivityIsRejected);
        break;
    }
    if (!pr.IsActive.GetValueOrDefault())
      activityValidationErrorList.Add(TimeCardMaint.ActivityValidationError.ProjectIsNotActive);
    if (pr.IsCompleted.GetValueOrDefault())
      activityValidationErrorList.Add(TimeCardMaint.ActivityValidationError.ProjectIsCompleted);
    PMTask pmTask = (PMTask) PXSelectorAttribute.Select<PMTimeActivity.projectTaskID>(this.Activities.Cache, (object) act);
    if (pmTask != null)
    {
      if (pmTask.IsCancelled.GetValueOrDefault())
        activityValidationErrorList.Add(TimeCardMaint.ActivityValidationError.ProjectTaskIsCancelled);
      else if (pmTask.IsCompleted.GetValueOrDefault())
        activityValidationErrorList.Add(TimeCardMaint.ActivityValidationError.ProjectTaskIsCompleted);
      else if (!pmTask.IsActive.GetValueOrDefault())
        activityValidationErrorList.Add(TimeCardMaint.ActivityValidationError.ProjectTaskIsNotActive);
    }
    return activityValidationErrorList;
  }

  public virtual void DisplayErrors(
    TimeCardMaint.EPTimecardDetail act,
    PMProject pr,
    List<TimeCardMaint.ActivityValidationError> errors)
  {
    PMTask pmTask = (PMTask) PXSelectorAttribute.Select<PMTimeActivity.projectTaskID>(this.Activities.Cache, (object) act);
    foreach (int error in errors)
    {
      switch (error)
      {
        case 0:
          this.Activities.Cache.RaiseExceptionHandling<TimeCardMaint.EPTimecardDetail.date>((object) act, (object) null, (Exception) new PXSetPropertyException("The activity is not completed.", PXErrorLevel.RowError));
          continue;
        case 2:
          if (this.RequireApproved())
          {
            this.Activities.Cache.RaiseExceptionHandling<TimeCardMaint.EPTimecardDetail.date>((object) act, (object) null, (Exception) new PXSetPropertyException("The activity is not approved.", PXErrorLevel.RowError));
            continue;
          }
          continue;
        case 3:
          this.Activities.Cache.RaiseExceptionHandling<TimeCardMaint.EPTimecardDetail.projectID>((object) act, (object) pr.ContractCD, (Exception) new PXSetPropertyException("Project is not active. Cannot record cost transaction against inactive project. Project: {0}", PXErrorLevel.Error, new object[1]
          {
            (object) pr.ContractCD
          }));
          continue;
        case 4:
          this.Activities.Cache.RaiseExceptionHandling<TimeCardMaint.EPTimecardDetail.projectID>((object) act, (object) pr.ContractCD, (Exception) new PXSetPropertyException("Project is completed. Cannot record cost transaction against completed project. Project: {0}", PXErrorLevel.Error, new object[1]
          {
            (object) pr.ContractCD
          }));
          continue;
        case 5:
          this.Activities.Cache.RaiseExceptionHandling<TimeCardMaint.EPTimecardDetail.projectTaskID>((object) act, (object) pmTask.TaskCD, (Exception) new PXSetPropertyException("Project Task is cancelled. Cannot record cost transaction against cancelled task. ProjectID: {0} TaskID:{1}", PXErrorLevel.Error, new object[2]
          {
            (object) pr.ContractCD,
            (object) pmTask.TaskCD
          }));
          continue;
        case 6:
          this.Activities.Cache.RaiseExceptionHandling<TimeCardMaint.EPTimecardDetail.projectTaskID>((object) act, (object) pmTask.TaskCD, (Exception) new PXSetPropertyException("Project Task is completed. Cannot record cost transaction against completed task. ProjectID: {0} TaskID:{1}", PXErrorLevel.Error, new object[2]
          {
            (object) pr.ContractCD,
            (object) pmTask.TaskCD
          }));
          continue;
        case 7:
          this.Activities.Cache.RaiseExceptionHandling<TimeCardMaint.EPTimecardDetail.projectTaskID>((object) act, (object) pmTask.TaskCD, (Exception) new PXSetPropertyException("Project Task is not active. Cannot record cost transaction against inactive task. ProjectID: {0} TaskID:{1}", PXErrorLevel.Error, new object[2]
          {
            (object) pr.ContractCD,
            (object) pmTask.TaskCD
          }));
          continue;
        case 8:
          this.Activities.Cache.RaiseExceptionHandling<TimeCardMaint.EPTimecardDetail.labourItemID>((object) act, (object) null, (Exception) new PXSetPropertyException("Overtime Labor Item is not specified for the Employee.", PXErrorLevel.RowError));
          continue;
        case 9:
          this.Activities.Cache.RaiseExceptionHandling<TimeCardMaint.EPTimecardDetail.labourItemID>((object) act, (object) null, (Exception) new PXSetPropertyException("Labor Item is not specified for the Employee.", PXErrorLevel.RowError));
          continue;
        default:
          continue;
      }
    }
  }

  [PXUIField(DisplayName = "Hold")]
  [PXButton]
  public virtual void Edit()
  {
    if (this.Document.Current == null)
      return;
    this.Document.Current.TimeSpent = new int?(0);
    this.Document.Current.OvertimeSpent = new int?(0);
    this.Document.Current.TimeBillable = new int?(0);
    this.Document.Current.OvertimeBillable = new int?(0);
    this.Document.Cache.SetStatus((object) this.Document.Current, PXEntryStatus.Updated);
    foreach (PXResult<TimeCardMaint.EPTimecardDetail> pxResult in this.Activities.Select())
    {
      TimeCardMaint.EPTimecardDetail data = (TimeCardMaint.EPTimecardDetail) pxResult;
      this.Activities.Cache.SetValue<TimeCardMaint.EPTimecardDetail.timeCardCD>((object) data, (object) null);
      this.Activities.Cache.SetStatus((object) data, PXEntryStatus.Updated);
    }
  }

  [PXUIField(DisplayName = "Release")]
  [PXButton]
  public virtual IEnumerable Release(PXAdapter adapter)
  {
    List<EPTimeCard> list = new List<EPTimeCard>();
    foreach (object row in adapter.Get())
    {
      EPTimeCard epTimeCard = PXResult.Unwrap<EPTimeCard>(row);
      if (epTimeCard != null && !epTimeCard.IsReleased.GetValueOrDefault())
        list.Add(epTimeCard);
    }
    if (!list.Any<EPTimeCard>())
      throw new PXException("This document is already released.");
    this.Save.Press();
    PXLongOperation.StartOperation((PXGraph) this, (PXToggleAsyncDelegate) (() =>
    {
      RegisterEntry instance1 = (RegisterEntry) PXGraph.CreateInstance(typeof (RegisterEntry));
      TimeCardMaint instance2 = PXGraph.CreateInstance<TimeCardMaint>();
      instance1.Clear();
      foreach (EPTimeCard timecard in list)
      {
        instance1.Clear();
        PXTimeStampScope.SetRecordComesFirst(typeof (EPTimeCard), true);
        if ((PMSetup) PXSelectBase<PMSetup, PXSelect<PMSetup>.Config>.Select((PXGraph) instance1) == null)
          instance1.Setup.Insert();
        using (PXTransactionScope transactionScope = new PXTransactionScope())
        {
          instance2.Clear();
          instance2.Document.Current = timecard;
          instance2.Document.Cache.SetStatus((object) timecard, PXEntryStatus.Notchanged);
          if (string.IsNullOrEmpty(timecard.OrigTimeCardCD))
            instance2.ProcessRegularTimecard(instance1, timecard);
          else
            instance2.ProcessCorrectingTimecard(instance1, timecard);
          if (EPSetupMaint.GetPostPMTransaction((PXGraph) this, this.EpSetup.Current, timecard.EmployeeID))
            instance1.Save.Press();
          timecard.Status = "R";
          timecard.IsReleased = new bool?(true);
          instance2.Document.Update(timecard);
          instance2.Save.Press();
          transactionScope.Complete();
        }
        if (this.EpSetup.Current.AutomaticReleasePM.GetValueOrDefault())
          RegisterRelease.Release(instance1.Document.Current);
      }
    }));
    return (IEnumerable) list;
  }

  [PXUIField(DisplayName = "Correct")]
  [PXButton]
  public virtual IEnumerable Correct(PXAdapter adapter)
  {
    if (this.Document.Current == null)
      return adapter.Get();
    EPTimeCard lastCorrection = this.GetLastCorrection(this.Document.Current);
    if (!lastCorrection.IsReleased.GetValueOrDefault())
      return (IEnumerable) new EPTimeCard[1]
      {
        lastCorrection
      };
    this.CheckTimeCardUsage(lastCorrection);
    EPTimeCard instance = (EPTimeCard) this.Document.Cache.CreateInstance();
    instance.WeekID = lastCorrection.WeekID;
    instance.OrigTimeCardCD = lastCorrection.TimeCardCD;
    EPTimeCard dst_row1 = this.Document.Insert(instance);
    dst_row1.EmployeeID = lastCorrection.EmployeeID;
    PXNoteAttribute.CopyNoteAndFiles(this.Document.Cache, (object) lastCorrection, this.Document.Cache, (object) dst_row1, new bool?(true), new bool?(true));
    bool flag = false;
    Dictionary<string, TimeCardMaint.TimeCardSummaryCopiedInfo> dictionary = new Dictionary<string, TimeCardMaint.TimeCardSummaryCopiedInfo>();
    PX.Data.PXView view1 = this.Summary.View;
    object[] currents1 = new object[1]
    {
      (object) lastCorrection
    };
    object[] objArray1 = Array.Empty<object>();
    foreach (EPTimeCardSummary epTimeCardSummary in view1.SelectMultiBound(currents1, objArray1))
    {
      string summaryKey = this.GetSummaryKey(epTimeCardSummary);
      if (!dictionary.ContainsKey(summaryKey))
      {
        string note = PXNoteAttribute.GetNote(this.Summary.Cache, (object) epTimeCardSummary);
        TimeCardMaint.TimeCardSummaryCopiedInfo summaryCopiedInfo = new TimeCardMaint.TimeCardSummaryCopiedInfo(epTimeCardSummary.Description, note);
        dictionary.Add(summaryKey, summaryCopiedInfo);
      }
    }
    PX.Data.PXView view2 = this.TimecardActivities.View;
    object[] currents2 = new object[1]
    {
      (object) lastCorrection
    };
    object[] objArray2 = Array.Empty<object>();
    foreach (TimeCardMaint.EPTimecardDetail epTimecardDetail1 in view2.SelectMultiBound(currents2, objArray2))
    {
      TimeCardMaint.EPTimecardDetail copy = PXCache<TimeCardMaint.EPTimecardDetail>.CreateCopy(epTimecardDetail1);
      copy.Released = new bool?(false);
      copy.Billed = new bool?(false);
      TimeCardMaint.EPTimecardDetail epTimecardDetail2 = copy;
      Guid? nullable1 = new Guid?();
      Guid? nullable2 = nullable1;
      epTimecardDetail2.NoteID = nullable2;
      copy.TimeCardCD = (string) null;
      copy.TimeSheetCD = (string) null;
      copy.OrigNoteID = epTimecardDetail1.NoteID;
      copy.Date = epTimecardDetail1.Date;
      copy.Billed = new bool?(false);
      copy.SummaryLineNbr = new int?();
      TimeCardMaint.EPTimecardDetail epTimecardDetail3 = copy;
      nullable1 = new Guid?();
      Guid? nullable3 = nullable1;
      epTimecardDetail3.NoteID = nullable3;
      copy.ContractCD = (string) null;
      this.isCreateCorrectionFlag = true;
      TimeCardMaint.EPTimecardDetail dst_row2;
      try
      {
        dst_row2 = this.Activities.Insert(copy);
      }
      catch (PXSetPropertyException ex)
      {
        flag = true;
        this.Activities.Cache.RaiseExceptionHandling<PMTimeActivity.summary>((object) epTimecardDetail1, (object) epTimecardDetail1.Summary, (Exception) new PXSetPropertyException(ex.MessageNoPrefix, PXErrorLevel.RowError));
        continue;
      }
      dst_row2.TrackTime = epTimecardDetail1.TrackTime;
      dst_row2.ShiftID = epTimecardDetail1.ShiftID;
      dst_row2.ReportedInTimeZoneID = epTimecardDetail1.ReportedInTimeZoneID;
      this.isCreateCorrectionFlag = false;
      dst_row2.ApprovalStatus = "CD";
      TimeCardMaint.EPTimecardDetail epTimecardDetail4 = dst_row2;
      nullable1 = epTimecardDetail1.NoteID;
      Guid? refNoteId = epTimecardDetail1.RefNoteID;
      Guid? nullable4 = (nullable1.HasValue == refNoteId.HasValue ? (nullable1.HasValue ? (nullable1.GetValueOrDefault() == refNoteId.GetValueOrDefault() ? 1 : 0) : 1) : 0) != 0 ? dst_row2.NoteID : epTimecardDetail1.RefNoteID;
      epTimecardDetail4.RefNoteID = nullable4;
      dst_row2.ContractCD = epTimecardDetail1.ContractCD;
      PXNoteAttribute.CopyNoteAndFiles(this.Activities.Cache, (object) epTimecardDetail1, this.Activities.Cache, (object) dst_row2);
      this.Activities.Cache.SetValue<PMTimeActivity.isCorrected>((object) epTimecardDetail1, (object) true);
      this.Activities.Cache.SetStatus((object) epTimecardDetail1, PXEntryStatus.Updated);
    }
    if (flag)
      throw new PXException("Failed to create correction timecard. Please check the errors on the Details.");
    PX.Data.PXView view3 = this.Items.View;
    object[] currents3 = new object[1]
    {
      (object) lastCorrection
    };
    object[] objArray3 = Array.Empty<object>();
    foreach (EPTimeCardItem epTimeCardItem1 in view3.SelectMultiBound(currents3, objArray3))
    {
      EPTimeCardItem epTimeCardItem2 = this.Items.Insert();
      epTimeCardItem2.ProjectID = epTimeCardItem1.ProjectID;
      epTimeCardItem2.TaskID = epTimeCardItem1.TaskID;
      epTimeCardItem2.Description = epTimeCardItem1.Description;
      epTimeCardItem2.InventoryID = epTimeCardItem1.InventoryID;
      epTimeCardItem2.CostCodeID = epTimeCardItem1.CostCodeID;
      epTimeCardItem2.UOM = epTimeCardItem1.UOM;
      epTimeCardItem2.Mon = epTimeCardItem1.Mon;
      epTimeCardItem2.Tue = epTimeCardItem1.Tue;
      epTimeCardItem2.Wed = epTimeCardItem1.Wed;
      epTimeCardItem2.Thu = epTimeCardItem1.Thu;
      epTimeCardItem2.Fri = epTimeCardItem1.Fri;
      epTimeCardItem2.Sat = epTimeCardItem1.Sat;
      epTimeCardItem2.Sun = epTimeCardItem1.Sun;
      epTimeCardItem2.OrigLineNbr = epTimeCardItem1.LineNbr;
    }
    foreach (PXResult<TimeCardMaint.EPTimeCardSummaryWithInfo> pxResult in this.Summary.Select())
    {
      EPTimeCardSummary epTimeCardSummary = (EPTimeCardSummary) (TimeCardMaint.EPTimeCardSummaryWithInfo) pxResult;
      string summaryKey = this.GetSummaryKey(epTimeCardSummary);
      if (dictionary.ContainsKey(summaryKey))
      {
        PXNoteAttribute.SetNote(this.Summary.Cache, (object) epTimeCardSummary, dictionary[summaryKey].Note);
        this.Summary.Cache.SetValue<EPTimeCardSummary.description>((object) epTimeCardSummary, (object) dictionary[summaryKey].Description);
      }
    }
    this.Save.Press();
    return (IEnumerable) new EPTimeCard[1]{ dst_row1 };
  }

  [PXUIField(DisplayName = "Preload from Tasks")]
  [PXButton(Tooltip = "Preload Activities from Tasks")]
  public virtual void PreloadFromTasks()
  {
    if (this.Tasks.AskExt() != WebDialogResult.OK)
      return;
    foreach (TimeCardMaint.EPTimecardTask data in this.Tasks.Cache.Updated)
    {
      if (data.Selected.GetValueOrDefault())
      {
        bool flag = false;
        foreach (PXResult<TimeCardMaint.EPTimeCardSummaryWithInfo> pxResult in this.Summary.Select())
        {
          TimeCardMaint.EPTimeCardSummaryWithInfo cardSummaryWithInfo = (TimeCardMaint.EPTimeCardSummaryWithInfo) pxResult;
          int? nullable1 = cardSummaryWithInfo.ProjectID;
          int? projectId = data.ProjectID;
          if (nullable1.GetValueOrDefault() == projectId.GetValueOrDefault() & nullable1.HasValue == projectId.HasValue)
          {
            int? nullable2 = cardSummaryWithInfo.ProjectTaskID;
            nullable1 = data.ProjectTaskID;
            if (nullable2.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable2.HasValue == nullable1.HasValue)
            {
              nullable1 = cardSummaryWithInfo.JobID;
              nullable2 = data.JobID;
              if (nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue)
              {
                nullable2 = cardSummaryWithInfo.ShiftID;
                nullable1 = data.ShiftID;
                if (nullable2.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable2.HasValue == nullable1.HasValue)
                {
                  nullable1 = cardSummaryWithInfo.CostCodeID;
                  nullable2 = data.CostCodeID;
                  if (nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue)
                  {
                    flag = true;
                    break;
                  }
                }
              }
            }
          }
        }
        if (!flag)
        {
          TimeCardMaint.EPTimeCardSummaryWithInfo instance = (TimeCardMaint.EPTimeCardSummaryWithInfo) this.Summary.Cache.CreateInstance();
          instance.ParentNoteID = data.NoteID;
          instance.ProjectID = data.ProjectID;
          instance.ProjectTaskID = data.ProjectTaskID;
          instance.IsBillable = data.IsBillable;
          instance.Description = data.Subject;
          instance.JobID = data.JobID;
          instance.ShiftID = data.ShiftID;
          instance.CostCodeID = data.CostCodeID;
          PMProject pmProject = (PMProject) PXSelectorAttribute.Select<TimeCardMaint.EPTimecardTask.projectID>(this.Tasks.Cache, (object) data, (object) data.ProjectID);
          if (pmProject != null)
            instance.ProjectManager = pmProject.ApproverID;
          PMTask pmTask = (PMTask) PXSelectorAttribute.Select<TimeCardMaint.EPTimecardTask.projectTaskID>(this.Tasks.Cache, (object) data, (object) data.ProjectTaskID);
          if (pmTask != null)
            instance.TaskApproverID = pmTask.ApproverID;
          this.Summary.Insert(instance);
        }
      }
    }
    if (this.Document.Cache.GetStatus((object) this.Document.Current) == PXEntryStatus.Inserted)
      return;
    this.Save.Press();
  }

  [PXUIField(DisplayName = "Preload from Previous Time Card")]
  [PXButton(Tooltip = "Preload Time from Previous Time Card")]
  public virtual void PreloadFromPreviousTimecard()
  {
    EPTimeCard current = this.Document.Current;
    if ((current != null ? (!current.WeekID.HasValue ? 1 : 0) : 1) != 0 || this.Employee.Current == null)
      return;
    EPTimeCard epTimeCard = (EPTimeCard) PXSelectBase<EPTimeCard, PXSelectReadonly<EPTimeCard, Where<EPTimeCard.employeeID, Equal<Required<EPTimeCard.employeeID>>, And<EPTimeCard.weekId, Less<Required<EPTimeCard.weekId>>>>, PX.Data.OrderBy<Desc<EPTimeCard.weekId>>>.Config>.SelectSingleBound((PXGraph) this, (object[]) null, (object) this.Document.Current.EmployeeID, (object) this.Document.Current.WeekID);
    if (epTimeCard == null)
      epTimeCard = (EPTimeCard) new PXSelect<EPTimeCard, Where<EPTimeCard.employeeID, Equal<Current<EPTimeCard.employeeID>>, And<EPTimeCard.weekId, NotEqual<Required<EPTimeCard.weekId>>>>, PX.Data.OrderBy<Desc<EPTimeCard.timeCardCD>>>((PXGraph) this).SelectWindowed(0, 1, (object) this.Document.Current.WeekID);
    if (epTimeCard == null)
      return;
    foreach (PXResult<TimeCardMaint.EPTimeCardSummaryWithInfo> pxResult1 in new PXSelect<TimeCardMaint.EPTimeCardSummaryWithInfo, Where<EPTimeCardSummary.timeCardCD, Equal<Required<EPTimeCardSummary.timeCardCD>>>>((PXGraph) this).Select((object) epTimeCard.TimeCardCD))
    {
      TimeCardMaint.EPTimeCardSummaryWithInfo cardSummaryWithInfo1 = (TimeCardMaint.EPTimeCardSummaryWithInfo) pxResult1;
      if (cardSummaryWithInfo1.EarningType != this.EpSetup.Current.HolidaysType && cardSummaryWithInfo1.EarningType != this.EpSetup.Current.VacationsType)
      {
        bool flag = false;
        foreach (PXResult<TimeCardMaint.EPTimeCardSummaryWithInfo> pxResult2 in this.Summary.Select())
        {
          TimeCardMaint.EPTimeCardSummaryWithInfo cardSummaryWithInfo2 = (TimeCardMaint.EPTimeCardSummaryWithInfo) pxResult2;
          int? nullable = cardSummaryWithInfo2.ProjectID;
          int? projectId = cardSummaryWithInfo1.ProjectID;
          if (nullable.GetValueOrDefault() == projectId.GetValueOrDefault() & nullable.HasValue == projectId.HasValue)
          {
            int? projectTaskId = cardSummaryWithInfo2.ProjectTaskID;
            nullable = cardSummaryWithInfo1.ProjectTaskID;
            if (projectTaskId.GetValueOrDefault() == nullable.GetValueOrDefault() & projectTaskId.HasValue == nullable.HasValue)
            {
              nullable = cardSummaryWithInfo2.JobID;
              int? jobId = cardSummaryWithInfo1.JobID;
              if (nullable.GetValueOrDefault() == jobId.GetValueOrDefault() & nullable.HasValue == jobId.HasValue)
              {
                int? shiftId = cardSummaryWithInfo2.ShiftID;
                nullable = cardSummaryWithInfo1.ShiftID;
                if (shiftId.GetValueOrDefault() == nullable.GetValueOrDefault() & shiftId.HasValue == nullable.HasValue && cardSummaryWithInfo2.EarningType == cardSummaryWithInfo1.EarningType)
                {
                  nullable = cardSummaryWithInfo2.CostCodeID;
                  int? costCodeId = cardSummaryWithInfo1.CostCodeID;
                  if (nullable.GetValueOrDefault() == costCodeId.GetValueOrDefault() & nullable.HasValue == costCodeId.HasValue && cardSummaryWithInfo2.UnionID == cardSummaryWithInfo1.UnionID)
                  {
                    int? labourItemId = cardSummaryWithInfo2.LabourItemID;
                    nullable = cardSummaryWithInfo1.LabourItemID;
                    if (labourItemId.GetValueOrDefault() == nullable.GetValueOrDefault() & labourItemId.HasValue == nullable.HasValue && cardSummaryWithInfo2.WorkCodeID == cardSummaryWithInfo1.WorkCodeID)
                    {
                      flag = true;
                      break;
                    }
                  }
                }
              }
            }
          }
          PMProject pmProject = PMProject.PK.Find((PXGraph) this, cardSummaryWithInfo1.ProjectID);
          if (pmProject.IsCompleted.GetValueOrDefault() || pmProject.IsCancelled.GetValueOrDefault() || !pmProject.VisibleInTA.GetValueOrDefault())
            flag = true;
          PMTask dirty = PMTask.PK.FindDirty((PXGraph) this, cardSummaryWithInfo1.ProjectID, cardSummaryWithInfo1.ProjectTaskID);
          if (dirty != null && (dirty.IsCompleted.GetValueOrDefault() || dirty.IsCancelled.GetValueOrDefault() || !pmProject.VisibleInTA.GetValueOrDefault()))
            flag = true;
        }
        if (!flag)
        {
          TimeCardMaint.EPTimeCardSummaryWithInfo copy = PXCache<TimeCardMaint.EPTimeCardSummaryWithInfo>.CreateCopy(cardSummaryWithInfo1);
          copy.TimeCardCD = (string) null;
          copy.Description = (string) null;
          copy.Mon = new int?();
          copy.Tue = new int?();
          copy.Wed = new int?();
          copy.Thu = new int?();
          copy.Fri = new int?();
          copy.Sat = new int?();
          copy.Sun = new int?();
          copy.NoteID = new Guid?();
          TimeCardMaint.EPTimeCardSummaryWithInfo cardSummaryWithInfo3 = this.Summary.Insert(copy);
          cardSummaryWithInfo3.ShiftID = cardSummaryWithInfo1.ShiftID;
          this.Summary.Update(cardSummaryWithInfo3);
        }
      }
    }
    if (this.Document.Cache.GetStatus((object) this.Document.Current) == PXEntryStatus.Inserted)
      return;
    try
    {
      this.Save.Press();
    }
    catch (Exception ex)
    {
      PXGraph.ThrowWithoutRollback(ex);
    }
  }

  [PXUIField(DisplayName = "Preload Holidays")]
  [PXButton(Tooltip = "Preload Holidays")]
  public virtual void PreloadHolidays()
  {
    EPTimeCard current = this.Document.Current;
    if ((current != null ? (!current.WeekStartDate.HasValue ? 1 : 0) : 1) != 0 || this.Employee.Current == null)
      return;
    int num1 = this.CostEngine.GetEmployeeRegularWeeklyMinutes(this.Document.Current.EmployeeID, this.Document.Current.WeekStartDate) / 5;
    TimeCardMaint.EPTimeCardSummaryWithInfo data = (TimeCardMaint.EPTimeCardSummaryWithInfo) this.Summary.Cache.CreateInstance();
    data.EarningType = this.EpSetup.Current.HolidaysType;
    data.IsBillable = new bool?(false);
    data.Description = PXMessages.LocalizeNoPrefix("Holiday");
    for (int index = 0; index < 7; ++index)
    {
      System.DateTime date = this.Document.Current.WeekStartDate.Value.AddDays((double) index);
      if (CalendarHelper.IsHoliday((PXGraph) this, this.Employee.Current.CalendarID, date))
      {
        int? fromExceptionOnDay = CalendarHelper.GetMinutesFromExceptionOnDay((PXGraph) this, this.Employee.Current.CalendarID, date);
        data.SetDayTime(date.DayOfWeek, fromExceptionOnDay ?? num1);
      }
    }
    int? timeTotal = data.GetTimeTotal();
    int num2 = 0;
    if (timeTotal.GetValueOrDefault() > num2 & timeTotal.HasValue)
      data = this.Summary.Insert(data);
    this.Summary.Cache.SetDefaultExt<TimeCardMaint.EPTimeCardSummaryWithInfo.projectDescription>((object) data);
    if (this.Document.Cache.GetStatus((object) this.Document.Current) == PXEntryStatus.Inserted)
      return;
    this.Save.Press();
  }

  [PXUIField(DisplayName = "Normalize Time Card")]
  [PXButton(Tooltip = "Normalize Time Card")]
  public virtual void NormalizeTimecard()
  {
    if (this.Document.Current == null || !this.Summary.Cache.AllowInsert)
      return;
    int regularWeeklyMinutes = this.CostEngine.GetEmployeeRegularWeeklyMinutes(this.Document.Current.EmployeeID, this.Document.Current.WeekStartDate);
    int num1 = regularWeeklyMinutes / 5;
    int num2 = 0;
    int num3 = 0;
    int num4 = 0;
    int num5 = 0;
    int num6 = 0;
    int num7 = 0;
    int num8 = 0;
    int? nullable1 = ProjectDefaultAttribute.NonProject();
    string workCodeId = PXAccess.FeatureInstalled<PX.Objects.CS.FeaturesSet.payrollModule>() ? PREmployee.PK.Find((PXGraph) this, this.Document.Current.EmployeeID)?.WorkCodeID : (string) null;
    int num9 = 0;
    int? nullable2 = new int?();
    foreach (PXResult<TimeCardMaint.EPTimeCardSummaryWithInfo> pxResult in this.Summary.Select())
    {
      TimeCardMaint.EPTimeCardSummaryWithInfo cardSummaryWithInfo = (TimeCardMaint.EPTimeCardSummaryWithInfo) pxResult;
      int num10 = num2;
      int? nullable3 = cardSummaryWithInfo.Mon;
      int valueOrDefault1 = nullable3.GetValueOrDefault();
      num2 = num10 + valueOrDefault1;
      int num11 = num3;
      nullable3 = cardSummaryWithInfo.Tue;
      int valueOrDefault2 = nullable3.GetValueOrDefault();
      num3 = num11 + valueOrDefault2;
      int num12 = num4;
      nullable3 = cardSummaryWithInfo.Wed;
      int valueOrDefault3 = nullable3.GetValueOrDefault();
      num4 = num12 + valueOrDefault3;
      int num13 = num5;
      nullable3 = cardSummaryWithInfo.Thu;
      int valueOrDefault4 = nullable3.GetValueOrDefault();
      num5 = num13 + valueOrDefault4;
      int num14 = num6;
      nullable3 = cardSummaryWithInfo.Fri;
      int valueOrDefault5 = nullable3.GetValueOrDefault();
      num6 = num14 + valueOrDefault5;
      int num15 = num7;
      nullable3 = cardSummaryWithInfo.Sat;
      int valueOrDefault6 = nullable3.GetValueOrDefault();
      num7 = num15 + valueOrDefault6;
      int num16 = num8;
      nullable3 = cardSummaryWithInfo.Sun;
      int valueOrDefault7 = nullable3.GetValueOrDefault();
      num8 = num16 + valueOrDefault7;
      if (cardSummaryWithInfo.EarningType.Trim() == this.EpSetup.Current.RegularHoursType.Trim())
      {
        bool? isBillable = cardSummaryWithInfo.IsBillable;
        bool flag = false;
        if (isBillable.GetValueOrDefault() == flag & isBillable.HasValue)
        {
          nullable3 = cardSummaryWithInfo.ProjectID;
          int? nullable4 = nullable1;
          if (nullable3.GetValueOrDefault() == nullable4.GetValueOrDefault() & nullable3.HasValue == nullable4.HasValue)
          {
            ++num9;
            nullable2 = cardSummaryWithInfo.LineNbr;
          }
        }
      }
    }
    int num17 = regularWeeklyMinutes - (num2 + num3 + num4 + num5 + num6 + num7 + num8);
    if (num17 <= 0)
      return;
    if (num9 != 1)
    {
      TimeCardMaint.EPTimeCardSummaryWithInfo cardSummaryWithInfo = new TimeCardMaint.EPTimeCardSummaryWithInfo();
      cardSummaryWithInfo.EarningType = this.EpSetup.Current.RegularHoursType;
      cardSummaryWithInfo.IsBillable = new bool?(false);
      cardSummaryWithInfo.Description = PXMessages.LocalizeNoPrefix("Normalization");
      cardSummaryWithInfo.ProjectID = nullable1;
      nullable2 = new int?();
      try
      {
        this.dontSyncDetails = true;
        this.skipProjectDefaultingFromEarningType = true;
        nullable2 = this.Summary.Insert(cardSummaryWithInfo).LineNbr;
      }
      finally
      {
        this.dontSyncDetails = false;
        this.skipProjectDefaultingFromEarningType = false;
      }
    }
    PXWeekSelector2Attribute.WeekInfo weekInfo = PXWeekSelector2Attribute.GetWeekInfo((PXGraph) this, this.Document.Current.WeekID.Value);
    this.isCreateCorrectionFlag = true;
    int num18 = num1 - num2;
    System.DateTime? nullable5;
    if (num18 > 0)
    {
      TimeCardMaint.EPTimecardDetail instance = (TimeCardMaint.EPTimecardDetail) this.Activities.Cache.CreateInstance();
      TimeCardMaint.EPTimecardDetail epTimecardDetail = instance;
      nullable5 = this.SetEmployeeTime(weekInfo.Mon.Date);
      System.DateTime? nullable6 = new System.DateTime?(nullable5.Value);
      epTimecardDetail.Date = nullable6;
      instance.TimeSpent = new int?(num17 < num18 ? num17 : num18);
      instance.Summary = PXMessages.LocalizeNoPrefix("Normalization");
      instance.ProjectID = nullable1;
      instance.WorkCodeID = workCodeId;
      instance.IsBillable = new bool?(false);
      if (nullable2.HasValue)
        instance.SummaryLineNbr = nullable2;
      this.Activities.Insert(instance);
      num17 -= num18;
    }
    if (num17 >= 0)
    {
      int num19 = num1 - num3;
      if (num19 > 0)
      {
        TimeCardMaint.EPTimecardDetail instance = (TimeCardMaint.EPTimecardDetail) this.Activities.Cache.CreateInstance();
        TimeCardMaint.EPTimecardDetail epTimecardDetail = instance;
        nullable5 = this.SetEmployeeTime(weekInfo.Tue.Date);
        System.DateTime? nullable7 = new System.DateTime?(nullable5.Value);
        epTimecardDetail.Date = nullable7;
        instance.TimeSpent = new int?(num17 < num19 ? num17 : num19);
        instance.Summary = PXMessages.LocalizeNoPrefix("Normalization");
        instance.ProjectID = nullable1;
        instance.WorkCodeID = workCodeId;
        instance.IsBillable = new bool?(false);
        if (nullable2.HasValue)
          instance.SummaryLineNbr = nullable2;
        this.Activities.Insert(instance);
        num17 -= num19;
      }
    }
    if (num17 >= 0)
    {
      int num20 = num1 - num4;
      if (num20 > 0)
      {
        TimeCardMaint.EPTimecardDetail instance = (TimeCardMaint.EPTimecardDetail) this.Activities.Cache.CreateInstance();
        TimeCardMaint.EPTimecardDetail epTimecardDetail = instance;
        nullable5 = this.SetEmployeeTime(weekInfo.Wed.Date);
        System.DateTime? nullable8 = new System.DateTime?(nullable5.Value);
        epTimecardDetail.Date = nullable8;
        instance.TimeSpent = new int?(num17 < num20 ? num17 : num20);
        instance.Summary = PXMessages.LocalizeNoPrefix("Normalization");
        instance.ProjectID = nullable1;
        instance.WorkCodeID = workCodeId;
        instance.IsBillable = new bool?(false);
        if (nullable2.HasValue)
          instance.SummaryLineNbr = nullable2;
        this.Activities.Insert(instance);
        num17 -= num20;
      }
    }
    if (num17 >= 0)
    {
      int num21 = num1 - num5;
      if (num21 > 0)
      {
        TimeCardMaint.EPTimecardDetail instance = (TimeCardMaint.EPTimecardDetail) this.Activities.Cache.CreateInstance();
        TimeCardMaint.EPTimecardDetail epTimecardDetail = instance;
        nullable5 = this.SetEmployeeTime(weekInfo.Thu.Date);
        System.DateTime? nullable9 = new System.DateTime?(nullable5.Value);
        epTimecardDetail.Date = nullable9;
        instance.TimeSpent = new int?(num17 < num21 ? num17 : num21);
        instance.Summary = PXMessages.LocalizeNoPrefix("Normalization");
        instance.ProjectID = nullable1;
        instance.WorkCodeID = workCodeId;
        instance.IsBillable = new bool?(false);
        if (nullable2.HasValue)
          instance.SummaryLineNbr = nullable2;
        this.Activities.Insert(instance);
        num17 -= num21;
      }
    }
    if (num17 >= 0)
    {
      int num22 = num1 - num6;
      if (num22 > 0)
      {
        TimeCardMaint.EPTimecardDetail instance = (TimeCardMaint.EPTimecardDetail) this.Activities.Cache.CreateInstance();
        TimeCardMaint.EPTimecardDetail epTimecardDetail = instance;
        nullable5 = this.SetEmployeeTime(weekInfo.Fri.Date);
        System.DateTime? nullable10 = new System.DateTime?(nullable5.Value);
        epTimecardDetail.Date = nullable10;
        instance.TimeSpent = new int?(num17 < num22 ? num17 : num22);
        instance.Summary = PXMessages.LocalizeNoPrefix("Normalization");
        instance.ProjectID = nullable1;
        instance.WorkCodeID = workCodeId;
        instance.IsBillable = new bool?(false);
        if (nullable2.HasValue)
          instance.SummaryLineNbr = nullable2;
        this.Activities.Insert(instance);
        int num23 = num17 - num22;
      }
    }
    this.isCreateCorrectionFlag = false;
    if (this.Document.Cache.GetStatus((object) this.Document.Current) == PXEntryStatus.Inserted)
      return;
    this.Save.Press();
  }

  [PXUIField(DisplayName = "View")]
  [PXButton]
  public virtual IEnumerable view(PXAdapter adapter)
  {
    TimeCardMaint.EPTimecardDetail current = this.Activities.Current;
    if (current == null)
      return adapter.Get();
    if (this.Document.Cache.GetStatus((object) this.Document.Current) == PXEntryStatus.Inserted)
      throw new PXException("Time Card must be saved before you can view the Activities.");
    List<object> objectList = new List<object>(this.Save.Press(adapter).Cast<object>());
    PXRedirectHelper.TryRedirect((PXGraph) this, (object) current, PXRedirectHelper.WindowMode.NewWindow);
    return (IEnumerable) objectList;
  }

  [PXUIField(DisplayName = "View Transactions", FieldClass = "PROJECT")]
  [PXButton]
  public virtual IEnumerable ViewPMTran(PXAdapter adapter)
  {
    if (this.Document.Current != null)
    {
      RegisterEntry instance = PXGraph.CreateInstance<RegisterEntry>();
      instance.Clear();
      PMRegister pmRegister = (PMRegister) PXSelectBase<PMRegister, PXSelect<PMRegister, Where<PMRegister.origDocType, Equal<PMOrigDocType.timeCard>, And<PMRegister.origNoteID, Equal<Current<EPTimeCard.noteID>>>>>.Config>.SelectSingleBound((PXGraph) this, new object[1]
      {
        (object) this.Document
      });
      if (pmRegister == null)
      {
        int num = (int) adapter.View.Ask("Transaction does not exist", MessageButtons.OK);
      }
      else
      {
        instance.Document.Current = pmRegister;
        throw new PXRedirectRequiredException((PXGraph) instance, "View Transactions");
      }
    }
    return adapter.Get();
  }

  [PXUIField(DisplayName = "View Contract", MapEnableRights = PXCacheRights.Select, MapViewRights = PXCacheRights.Select)]
  [PXButton]
  protected virtual void viewContract()
  {
    PX.Objects.CT.Contract row = (PX.Objects.CT.Contract) PXSelectBase<PX.Objects.CT.Contract, PXViewOf<PX.Objects.CT.Contract>.BasedOn<SelectFromBase<PX.Objects.CT.Contract, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<PX.Objects.CT.Contract.contractCD, IBqlString>.IsEqual<P.AsString>>>.Config>.Select((PXGraph) this, (object) this.Activities.Current?.ContractCD);
    if (row == null || !(row.BaseType == "C"))
      return;
    PXGraph.CreateInstance<ContractMaint>().Contracts.Current = row;
    PXRedirectHelper.TryRedirect((PXGraph) this, (object) row, PXRedirectHelper.WindowMode.NewWindow);
  }

  protected virtual void EPTimecardDetail_EarningTypeID_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    if ((TimeCardMaint.EPTimecardDetail) e.Row != null && e.NewValue == null)
    {
      e.Cancel = true;
      throw new PXSetPropertyException("'{0}' cannot be empty.", new object[1]
      {
        (object) "[earningTypeID]"
      });
    }
  }

  protected virtual void EPTimeCardSummaryWithInfo_EarningType_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    if ((TimeCardMaint.EPTimeCardSummaryWithInfo) e.Row != null && e.NewValue == null)
    {
      e.Cancel = true;
      throw new PXSetPropertyException("'{0}' cannot be empty.", new object[1]
      {
        (object) "[earningType]"
      });
    }
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<EPTimeCard, EPTimeCard.weekId> e)
  {
    EPTimeCard row = e.Row;
    if (row == null)
      return;
    int num = this.GetCurrentDateWeekId().Value;
    e.NewValue = (object) (this.HasDuplicate(row.EmployeeID, new int?(num), row.TimeCardCD) ? this.GetNextWeekID(e.Row.EmployeeID) : new int?(num));
  }

  protected virtual void EPTimeCard_RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    EPTimeCard row = e.Row as EPTimeCard;
    EPTimeCard oldRow = e.OldRow as EPTimeCard;
    if (row == null || oldRow == null)
      return;
    int? nullable = row.EmployeeID;
    int? employeeId = oldRow.EmployeeID;
    bool flag1 = !(nullable.GetValueOrDefault() == employeeId.GetValueOrDefault() & nullable.HasValue == employeeId.HasValue);
    int? weekId = row.WeekID;
    nullable = oldRow.WeekID;
    bool flag2 = !(weekId.GetValueOrDefault() == nullable.GetValueOrDefault() & weekId.HasValue == nullable.HasValue);
    nullable = row.WeekID;
    if (!nullable.HasValue | flag1)
    {
      try
      {
        nullable = this.GetCurrentDateWeekId();
        int num = nullable.Value;
        if (!this.HasDuplicate(row.EmployeeID, new int?(num), row.TimeCardCD))
          sender.SetValueExt<EPTimeCard.weekId>((object) row, (object) num);
        else
          sender.SetValueExt<EPTimeCard.weekId>((object) row, (object) this.GetNextWeekID(row.EmployeeID));
      }
      catch (PXException ex)
      {
        sender.SetValueExt<EPTimeCard.weekId>((object) row, (object) null);
        sender.RaiseExceptionHandling<EPTimeCard.weekId>((object) row, (object) null, (Exception) ex);
      }
    }
    if (!(flag1 | flag2))
      return;
    this.Items.Cache.Clear();
    this.Activities.Cache.Clear();
    this.Summary.Cache.Clear();
    this.Clear(PXClearOption.ClearQueriesOnly);
  }

  protected virtual void EPTimeCard_RowDeleting(PXCache sender, PXRowDeletingEventArgs e)
  {
    if (!(e.Row is EPTimeCard))
      return;
    foreach (PXResult<TimeCardMaint.EPTimeCardSummaryWithInfo> pxResult in this.Summary.Select())
    {
      TimeCardMaint.EPTimeCardSummaryWithInfo cardSummaryWithInfo = (TimeCardMaint.EPTimeCardSummaryWithInfo) pxResult;
      try
      {
        this.dontSyncDetails = true;
        this.Summary.Delete(cardSummaryWithInfo);
      }
      finally
      {
        this.dontSyncDetails = false;
      }
    }
    try
    {
      this.dontSyncSummary = true;
      foreach (PXResult<TimeCardMaint.EPTimecardDetail, CREmployee> pxResult in new PXSelectJoin<TimeCardMaint.EPTimecardDetail, InnerJoin<CREmployee, On<CREmployee.defContactID, Equal<PMTimeActivity.ownerID>>>, Where<CREmployee.bAccountID, Equal<Current<EPTimeCard.employeeID>>, And<TimeCardMaint.EPTimecardDetail.weekID, Equal<Current<EPTimeCard.weekId>>, And<PMTimeActivity.released, NotEqual<True>, And<PMTimeActivity.isCorrected, NotEqual<True>, And2<Where<PMTimeActivity.summaryLineNbr, PX.Data.IsNotNull, Or<PMTimeActivity.origNoteID, PX.Data.IsNotNull>>, PX.Data.And<Where<TimeCardMaint.EPTimecardDetail.timeCardCD, PX.Data.IsNull, Or<TimeCardMaint.EPTimecardDetail.timeCardCD, Equal<Current<EPTimeCard.timeCardCD>>>>>>>>>>>((PXGraph) this).Select())
      {
        TimeCardMaint.EPTimecardDetail epTimecardDetail = (TimeCardMaint.EPTimecardDetail) pxResult;
        if (epTimecardDetail.OrigNoteID.HasValue)
        {
          TimeCardMaint.EPTimecardDetail row = (TimeCardMaint.EPTimecardDetail) PXSelectBase<TimeCardMaint.EPTimecardDetail, PXSelect<TimeCardMaint.EPTimecardDetail>.Config>.Search<TimeCardMaint.EPTimecardDetail.noteID>((PXGraph) this, (object) epTimecardDetail.OrigNoteID);
          if (row != null)
          {
            row.IsCorrected = new bool?(false);
            this.Activities.Cache.MarkUpdated((object) row);
          }
        }
        this.Activities.Delete(epTimecardDetail);
      }
    }
    finally
    {
      this.dontSyncSummary = false;
    }
  }

  protected virtual void EPTimeCard_RowDeleted(PXCache sender, PXRowDeletedEventArgs e)
  {
    EPTimeCard row = (EPTimeCard) e.Row;
    EPTimeCard epTimeCard = (EPTimeCard) PXSelectBase<EPTimeCard, PXSelect<EPTimeCard, Where<EPTimeCard.employeeID, Equal<Current<EPTimeCard.employeeID>>, And<EPTimeCard.weekId, Greater<Current<EPTimeCard.weekId>>, PX.Data.And<Where<EPTimeCard.timeCardCD, Equal<Current<EPTimeCard.origTimeCardCD>>, Or<Current<EPTimeCard.origTimeCardCD>, PX.Data.IsNull>>>>>>.Config>.SelectWindowed((PXGraph) this, 0, 1);
    if (epTimeCard != null && epTimeCard.TimeCardCD != row.OrigTimeCardCD)
      throw new PXException("Since there exists a timecard for the future week you cannot delete this Time Card.");
  }

  protected virtual void _(PX.Data.Events.RowPersisting<EPTimeCard> e)
  {
    if (e.Operation != PXDBOperation.Insert || !string.IsNullOrEmpty(e.Row.OrigTimeCardCD))
      return;
    if (this.HasDuplicate(e.Row.EmployeeID, e.Row.WeekID, e.Row.TimeCardCD))
      throw new PXException("A time card for this week already exists in the system. Most probably, it has just been created by another user.");
    EPSetup topFirst = PXSelectBase<EPSetup, PXViewOf<EPSetup>.BasedOn<SelectFromBase<EPSetup, TypeArrayOf<IFbqlJoin>.Empty>>.ReadOnly.Config>.Select((PXGraph) this).TopFirst;
    bool? customWeek1 = this.EpSetup.Current.CustomWeek;
    bool? customWeek2 = topFirst.CustomWeek;
    if (customWeek1.GetValueOrDefault() == customWeek2.GetValueOrDefault() & customWeek1.HasValue == customWeek2.HasValue)
    {
      int? lastCustomWeekId1 = this.EpSetup.Current.LastCustomWeekID;
      int? lastCustomWeekId2 = topFirst.LastCustomWeekID;
      if (lastCustomWeekId1.GetValueOrDefault() == lastCustomWeekId2.GetValueOrDefault() & lastCustomWeekId1.HasValue == lastCustomWeekId2.HasValue)
        return;
    }
    throw new PXException("This time card is no longer valid because the custom week settings have been changed. Please re-create the time card.");
  }

  protected virtual void EPTimeCard_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    if (!(e.Row is EPTimeCard row))
      return;
    int? nullable1 = row.EmployeeID;
    bool flag1 = !nullable1.HasValue || PXSubordinateSelectorAttribute.IsSubordinated((PXGraph) this, (object) row.EmployeeID);
    if (!flag1)
      flag1 = (EPEmployee) PXSelectBase<EPEmployee, PXSelect<EPEmployee, Where<EPEmployee.bAccountID, WingmanUser<Current<AccessInfo.userID>, EPDelegationOf.timeEntries>>>.Config>.SelectSingleBound((PXGraph) this, (object[]) null) != null;
    if (!flag1)
    {
      PXUIFieldAttribute.SetEnabled(sender, (object) row, false);
      PXUIFieldAttribute.SetEnabled<EPTimeCard.timeCardCD>(sender, (object) row, true);
    }
    PXCache cache = this.Document.Cache;
    bool? nullable2;
    int num1;
    if (flag1)
    {
      nullable2 = row.IsReleased;
      if (!nullable2.GetValueOrDefault())
      {
        num1 = row.Status == "H" ? 1 : 0;
        goto label_10;
      }
    }
    num1 = 0;
label_10:
    cache.AllowDelete = num1 != 0;
    int num2;
    if (flag1)
    {
      nullable2 = row.IsHold;
      if (nullable2.GetValueOrDefault())
      {
        nullable2 = row.IsApproved;
        if (!nullable2.GetValueOrDefault())
        {
          nullable2 = row.IsRejected;
          if (!nullable2.GetValueOrDefault())
          {
            nullable2 = row.IsReleased;
            num2 = !nullable2.GetValueOrDefault() ? 1 : 0;
            goto label_16;
          }
        }
      }
    }
    num2 = 0;
label_16:
    bool flag2 = num2 != 0;
    int num3;
    if (flag2)
    {
      nullable1 = row.WeekID;
      num3 = nullable1.HasValue ? 1 : 0;
    }
    else
      num3 = 0;
    bool isEnabled = num3 != 0;
    this.Activities.Cache.AllowInsert = isEnabled;
    this.Activities.Cache.AllowUpdate = isEnabled;
    this.Activities.Cache.AllowDelete = isEnabled;
    this.Summary.Cache.AllowInsert = isEnabled;
    this.Summary.Cache.AllowUpdate = isEnabled;
    this.Summary.Cache.AllowDelete = isEnabled;
    this.Items.Cache.AllowInsert = isEnabled;
    this.Items.Cache.AllowUpdate = isEnabled;
    this.Items.Cache.AllowDelete = isEnabled;
    this.preloadFromTasks.SetEnabled(isEnabled);
    this.preloadFromPreviousTimecard.SetEnabled(isEnabled);
    this.preloadHolidays.SetEnabled(isEnabled);
    this.normalizeTimecard.SetEnabled(isEnabled);
    PXAction<EPTimeCard> viewPmTran = this.viewPMTran;
    nullable2 = row.IsReleased;
    int num4 = nullable2.GetValueOrDefault() ? 1 : 0;
    viewPmTran.SetEnabled(num4 != 0);
    this.submit.SetEnabled(sender.GetStatus((object) row) != PXEntryStatus.Inserted);
    nullable1 = row.EmployeeID;
    if (nullable1.HasValue)
    {
      if (this.Summary.Select().Count > 0 && this.Document.Cache.GetStatus((object) row) != PXEntryStatus.Inserted)
        PXUIFieldAttribute.SetEnabled<EPTimeCard.employeeID>(sender, (object) row, false);
      else
        PXUIFieldAttribute.SetEnabled<EPTimeCard.employeeID>(sender, (object) row, isEnabled && row.OrigTimeCardCD == null);
    }
    bool flag3 = sender.GetStatus(e.Row) == PXEntryStatus.Inserted;
    PXUIFieldAttribute.SetEnabled<EPTimeCard.weekId>(sender, (object) row, flag2 & flag3);
    this.RecalculateTotals(row);
    this.ValidateTotals(row, out string _);
    nullable1 = row.WeekID;
    if (!nullable1.HasValue)
      return;
    nullable1 = row.WeekID;
    PXWeekSelector2Attribute.WeekInfo weekInfo = PXWeekSelector2Attribute.GetWeekInfo((PXGraph) this, nullable1.Value);
    PXUIFieldAttribute.SetEnabled<EPTimeCardSummary.mon>(this.Summary.Cache, (object) null, weekInfo.Mon.Enabled);
    PXUIFieldAttribute.SetEnabled<EPTimeCardSummary.tue>(this.Summary.Cache, (object) null, weekInfo.Tue.Enabled);
    PXUIFieldAttribute.SetEnabled<EPTimeCardSummary.wed>(this.Summary.Cache, (object) null, weekInfo.Wed.Enabled);
    PXUIFieldAttribute.SetEnabled<EPTimeCardSummary.thu>(this.Summary.Cache, (object) null, weekInfo.Thu.Enabled);
    PXUIFieldAttribute.SetEnabled<EPTimeCardSummary.fri>(this.Summary.Cache, (object) null, weekInfo.Fri.Enabled);
    PXUIFieldAttribute.SetEnabled<EPTimeCardSummary.sat>(this.Summary.Cache, (object) null, weekInfo.Sat.Enabled);
    PXUIFieldAttribute.SetEnabled<EPTimeCardSummary.sun>(this.Summary.Cache, (object) null, weekInfo.Sun.Enabled);
    PXUIFieldAttribute.SetEnabled<EPTimeCardItem.mon>(this.Items.Cache, (object) null, weekInfo.Mon.Enabled);
    PXUIFieldAttribute.SetEnabled<EPTimeCardItem.tue>(this.Items.Cache, (object) null, weekInfo.Tue.Enabled);
    PXUIFieldAttribute.SetEnabled<EPTimeCardItem.wed>(this.Items.Cache, (object) null, weekInfo.Wed.Enabled);
    PXUIFieldAttribute.SetEnabled<EPTimeCardItem.thu>(this.Items.Cache, (object) null, weekInfo.Thu.Enabled);
    PXUIFieldAttribute.SetEnabled<EPTimeCardItem.fri>(this.Items.Cache, (object) null, weekInfo.Fri.Enabled);
    PXUIFieldAttribute.SetEnabled<EPTimeCardItem.sat>(this.Items.Cache, (object) null, weekInfo.Sat.Enabled);
    PXUIFieldAttribute.SetEnabled<EPTimeCardItem.sun>(this.Items.Cache, (object) null, weekInfo.Sun.Enabled);
    TimeCardMaint.SetSummaryColumnsDisplayName(weekInfo, this.Summary.Cache, this.Items.Cache);
  }

  protected virtual void EPTimeCard_EmployeeID_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    if (e.Row is EPTimeCard row && row.WeekID.HasValue && (EPTimecardLite) PXSelectBase<EPTimecardLite, PXSelect<EPTimecardLite, Where<EPTimecardLite.employeeID, Equal<Current<EPTimeCard.employeeID>>, And<EPTimecardLite.weekId, Greater<Current<EPTimeCard.weekId>>>>>.Config>.Select((PXGraph) this) != null)
      throw new PXSetPropertyException("Since there exists a Time Card for the future week you cannot change the Employee in the given week.");
  }

  protected virtual void EPTimeCard_IsApproved_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    if (!(e.Row is EPTimeCard))
      return;
    bool? nullable1 = (bool?) e.NewValue;
    if (!nullable1.GetValueOrDefault())
      return;
    foreach (PXResult activity in this.activities())
    {
      TimeCardMaint.EPTimecardDetail row = (TimeCardMaint.EPTimecardDetail) activity[typeof (TimeCardMaint.EPTimecardDetail)];
      nullable1 = row.Released;
      if (!nullable1.GetValueOrDefault())
      {
        int? nullable2;
        if (row.ApprovalStatus == "PA")
        {
          nullable2 = row.ApproverID;
          if (!nullable2.HasValue)
          {
            object obj = PXFormulaAttribute.Evaluate<PMTimeActivity.approvalStatus>(this.Caches[typeof (PMTimeActivity)], (object) row);
            if (!string.IsNullOrEmpty(obj?.ToString()))
            {
              row.ApprovalStatus = obj.ToString();
              this.Activities.Update(row);
            }
          }
        }
        nullable2 = row.TimeSpent;
        if (nullable2.GetValueOrDefault() == 0)
        {
          nullable2 = row.TimeBillable;
          if (nullable2.GetValueOrDefault() == 0)
            continue;
        }
        if (row.ApprovalStatus != "CD" && row.ApprovalStatus != "AP" && row.ApprovalStatus != "CL")
        {
          e.NewValue = (object) false;
          e.Cancel = true;
          this.Activities.Cache.RaiseExceptionHandling<TimeCardMaint.EPTimecardDetail.timeSpent>((object) row, (object) row.TimeSpent, (Exception) new PXSetPropertyException("The activity is not approved.", PXErrorLevel.RowError));
          throw new PXException("At least one activity of the time card is not approved. The time card can be approved only when all its activities are approved.");
        }
      }
    }
  }

  protected virtual void EPTimeCardSummaryWithInfo_RowSelected(
    PXCache sender,
    PXRowSelectedEventArgs e)
  {
    if (!(e.Row is TimeCardMaint.EPTimeCardSummaryWithInfo row) || this.dontSyncDetails)
      return;
    string summaryKey = this.GetSummaryKey((EPTimeCardSummary) row);
    TimeCardMaint.SummaryRecord.SummaryRecordInfo summaryRecordInfo = new TimeCardMaint.SummaryRecord.SummaryRecordInfo();
    TimeCardMaint.SummaryRecord summaryRecord;
    if (this.sumList.TryGetValue(row.LineNbr.ToString(), out summaryRecord))
      summaryRecordInfo = summaryRecord.GetInfo();
    else if (this.sumList.TryGetValue(summaryKey, out summaryRecord))
      summaryRecordInfo = summaryRecord.GetInfo();
    bool isEnabled = true;
    if (row.ProjectID.HasValue)
      isEnabled = !summaryRecordInfo.HasManualDetails;
    PXUIFieldAttribute.SetEnabled<EPTimeCardSummary.earningType>(sender, (object) row, isEnabled);
    PXUIFieldAttribute.SetEnabled<EPTimeCardSummary.parentNoteID>(sender, (object) row, isEnabled);
    PXUIFieldAttribute.SetEnabled<EPTimeCardSummary.projectID>(sender, (object) row, isEnabled);
    PXUIFieldAttribute.SetEnabled<EPTimeCardSummary.projectTaskID>(sender, (object) row, isEnabled);
    PXUIFieldAttribute.SetEnabled<EPTimeCardSummary.isBillable>(sender, (object) row, isEnabled);
    PXUIFieldAttribute.SetEnabled<EPTimeCardSummary.shiftID>(sender, (object) row, isEnabled);
    PXUIFieldAttribute.SetEnabled<EPTimeCardSummary.labourItemID>(sender, (object) row, isEnabled);
    PXUIFieldAttribute.SetEnabled<EPTimeCardSummary.costCodeID>(sender, (object) row, isEnabled);
    PXUIFieldAttribute.SetEnabled<EPTimeCardSummary.unionID>(sender, (object) row, isEnabled);
    PXUIFieldAttribute.SetEnabled<EPTimeCardSummary.workCodeID>(sender, (object) row, isEnabled);
    PXUIFieldAttribute.SetEnabled<EPTimeCardSummary.certifiedJob>(sender, (object) row, isEnabled);
    row.ApprovalStatus = !summaryRecordInfo.ApprovalRequired ? "NR" : (summaryRecordInfo.HasCompleted || summaryRecordInfo.HasOpen ? "PA" : (!summaryRecordInfo.HasApproved || !summaryRecordInfo.HasRejected ? (!summaryRecordInfo.HasApproved ? (!summaryRecordInfo.HasRejected ? "NR" : "RJ") : "AP") : "PR"));
    if (summaryRecordInfo.Rate.HasValue)
      row.EmployeeRate = summaryRecordInfo.Rate;
    PXUIFieldAttribute.SetWarning<EPTimeCardSummary.employeeRate>(sender, e.Row, (string) null);
    if (!summaryRecordInfo.ContainsMixedRates)
      return;
    PXUIFieldAttribute.SetWarning<EPTimeCardSummary.employeeRate>(sender, e.Row, "The Cost Rate contains mixed rates. You can find the exact rate by date on the Details tab.");
  }

  protected virtual void EPTimeCardSummaryWithInfo_RowInserting(
    PXCache sender,
    PXRowInsertingEventArgs e)
  {
    if (!(e.Row is TimeCardMaint.EPTimeCardSummaryWithInfo row))
      return;
    EPEarningType epEarningType = EPEarningType.PK.Find((PXGraph) this, row.EarningType);
    int? nullable;
    if (epEarningType != null && !this.skipProjectDefaultingFromEarningType)
    {
      if (!this.IsImportFromExcel)
        row.IsBillable = new bool?(epEarningType.isBillable.GetValueOrDefault());
      row.EarningType = epEarningType.TypeCD;
      int? projectId = epEarningType.ProjectID;
      if (projectId.HasValue)
      {
        projectId = row.ProjectID;
        if (projectId.HasValue)
        {
          projectId = row.ProjectID;
          nullable = ProjectDefaultAttribute.NonProject();
          if (!(projectId.GetValueOrDefault() == nullable.GetValueOrDefault() & projectId.HasValue == nullable.HasValue))
            goto label_10;
        }
        if (PXSelectorAttribute.Select(sender, e.Row, sender.GetField(typeof (EPTimeCardSummary.projectID)), (object) epEarningType.ProjectID) != null)
          sender.SetValueExt<TimeCardMaint.EPTimeCardSummaryWithInfo.projectID>((object) row, (object) epEarningType.ProjectID);
      }
label_10:
      nullable = epEarningType.TaskID;
      if (nullable.HasValue)
      {
        nullable = row.ProjectTaskID;
        if (!nullable.HasValue && !ProjectDefaultAttribute.IsNonProject(row.ProjectID))
        {
          PMTask dirty = PMTask.PK.FindDirty((PXGraph) this, epEarningType.ProjectID, epEarningType.TaskID);
          if (dirty != null && dirty.VisibleInTA.GetValueOrDefault())
            sender.SetValueExt<TimeCardMaint.EPTimeCardSummaryWithInfo.projectTaskID>((object) row, (object) epEarningType.TaskID);
        }
      }
    }
    nullable = row.ProjectID;
    if (!nullable.HasValue && e.ExternalCall)
    {
      TimeCardMaint.EPTimeCardSummaryWithInfo cardSummaryWithInfo = (TimeCardMaint.EPTimeCardSummaryWithInfo) PXSelectBase<TimeCardMaint.EPTimeCardSummaryWithInfo, PXSelect<TimeCardMaint.EPTimeCardSummaryWithInfo, Where<EPTimeCardSummary.lineNbr, NotEqual<Current<EPTimeCardSummary.lineNbr>>, And<EPTimeCardSummary.timeCardCD, Equal<Current<EPTimeCardSummary.timeCardCD>>>>, PX.Data.OrderBy<Desc<EPTimeCardSummary.lineNbr>>>.Config>.SelectSingleBound((PXGraph) this, new object[1]
      {
        (object) row
      });
      if (cardSummaryWithInfo != null && !string.Equals(cardSummaryWithInfo.EarningType, row.EarningType, StringComparison.InvariantCultureIgnoreCase))
      {
        nullable = cardSummaryWithInfo.ProjectID;
        if (nullable.HasValue)
        {
          sender.SetValueExt<TimeCardMaint.EPTimeCardSummaryWithInfo.projectID>((object) row, (object) cardSummaryWithInfo.ProjectID);
          sender.SetValueExt<TimeCardMaint.EPTimeCardSummaryWithInfo.projectTaskID>((object) row, (object) cardSummaryWithInfo.ProjectTaskID);
        }
      }
    }
    nullable = row.ProjectID;
    if (nullable.HasValue || epEarningType == null || !(epEarningType.TypeCD == this.EpSetup.Current.HolidaysType))
      return;
    sender.SetValueExt<TimeCardMaint.EPTimeCardSummaryWithInfo.projectID>((object) row, (object) ProjectDefaultAttribute.NonProject());
  }

  protected virtual void EPTimeCardSummaryWithInfo_RowInserted(
    PXCache sender,
    PXRowInsertedEventArgs e)
  {
    try
    {
      if (!(e.Row is TimeCardMaint.EPTimeCardSummaryWithInfo row) || this.dontSyncDetails)
        return;
      this.UpdateAdjustingActivities((EPTimeCardSummary) row);
    }
    finally
    {
    }
  }

  protected virtual void EPTimeCardSummaryWithInfo_RowUpdating(
    PXCache sender,
    PXRowUpdatingEventArgs e)
  {
    TimeCardMaint.EPTimeCardSummaryWithInfo newRow = e.NewRow as TimeCardMaint.EPTimeCardSummaryWithInfo;
    TimeCardMaint.EPTimeCardSummaryWithInfo row = e.Row as TimeCardMaint.EPTimeCardSummaryWithInfo;
    EPEarningType epEarningType = EPEarningType.PK.Find((PXGraph) this, newRow.EarningType);
    if (epEarningType == null)
      return;
    bool? nullable1 = newRow.IsBillable;
    if (!nullable1.HasValue || newRow.EarningType != row.EarningType)
    {
      TimeCardMaint.EPTimeCardSummaryWithInfo cardSummaryWithInfo = newRow;
      nullable1 = epEarningType.isBillable;
      bool? nullable2 = new bool?(nullable1.GetValueOrDefault());
      cardSummaryWithInfo.IsBillable = nullable2;
    }
    int? nullable3 = epEarningType.ProjectID;
    if (nullable3.HasValue)
    {
      nullable3 = newRow.ProjectID;
      if ((!nullable3.HasValue || newRow.EarningType != row.EarningType) && PXSelectorAttribute.Select(sender, e.Row, sender.GetField(typeof (EPTimeCardSummary.projectID)), (object) epEarningType.ProjectID) != null)
      {
        sender.SetValueExt<TimeCardMaint.EPTimeCardSummaryWithInfo.projectID>((object) newRow, (object) epEarningType.ProjectID);
        if (ProjectDefaultAttribute.IsNonProject(newRow.ProjectID))
          sender.SetValueExt<TimeCardMaint.EPTimeCardSummaryWithInfo.projectTaskID>((object) newRow, (object) null);
      }
    }
    nullable3 = epEarningType.TaskID;
    if (!nullable3.HasValue)
      return;
    nullable3 = newRow.ProjectTaskID;
    if (nullable3.HasValue && !(newRow.EarningType != row.EarningType))
      return;
    nullable3 = newRow.ProjectID;
    int? projectId = epEarningType.ProjectID;
    if (!(nullable3.GetValueOrDefault() == projectId.GetValueOrDefault() & nullable3.HasValue == projectId.HasValue))
      return;
    PMTask dirty = PMTask.PK.FindDirty((PXGraph) this, epEarningType.ProjectID, epEarningType.TaskID);
    if (dirty == null)
      return;
    nullable1 = dirty.VisibleInTA;
    if (!nullable1.GetValueOrDefault())
      return;
    sender.SetValueExt<TimeCardMaint.EPTimeCardSummaryWithInfo.projectTaskID>((object) newRow, (object) epEarningType.TaskID);
  }

  protected virtual void EPTimeCardSummaryWithInfo_RowUpdated(
    PXCache sender,
    PXRowUpdatedEventArgs e)
  {
    TimeCardMaint.EPTimeCardSummaryWithInfo row = e.Row as TimeCardMaint.EPTimeCardSummaryWithInfo;
    TimeCardMaint.EPTimeCardSummaryWithInfo oldRow = e.OldRow as TimeCardMaint.EPTimeCardSummaryWithInfo;
    if (row == null || this.dontSyncDetails)
      return;
    int? projectId1 = oldRow.ProjectID;
    int? nullable1 = row.ProjectID;
    int? nullable2;
    if (projectId1.GetValueOrDefault() == nullable1.GetValueOrDefault() & projectId1.HasValue == nullable1.HasValue)
    {
      nullable1 = oldRow.ProjectTaskID;
      nullable2 = row.ProjectTaskID;
      if (nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue)
      {
        bool? isBillable1 = oldRow.IsBillable;
        bool? isBillable2 = row.IsBillable;
        if (isBillable1.GetValueOrDefault() == isBillable2.GetValueOrDefault() & isBillable1.HasValue == isBillable2.HasValue)
        {
          Guid? parentNoteId1 = oldRow.ParentNoteID;
          Guid? parentNoteId2 = row.ParentNoteID;
          if ((parentNoteId1.HasValue == parentNoteId2.HasValue ? (parentNoteId1.HasValue ? (parentNoteId1.GetValueOrDefault() != parentNoteId2.GetValueOrDefault() ? 1 : 0) : 0) : 1) == 0 && !(oldRow.EarningType != row.EarningType))
          {
            nullable2 = oldRow.JobID;
            nullable1 = row.JobID;
            if (nullable2.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable2.HasValue == nullable1.HasValue)
            {
              nullable1 = oldRow.ShiftID;
              nullable2 = row.ShiftID;
              if (nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue)
              {
                nullable2 = oldRow.CostCodeID;
                nullable1 = row.CostCodeID;
                if (nullable2.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable2.HasValue == nullable1.HasValue && !(oldRow.UnionID != row.UnionID))
                {
                  nullable1 = oldRow.LabourItemID;
                  nullable2 = row.LabourItemID;
                  if (nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue && !(oldRow.WorkCodeID != row.WorkCodeID))
                    goto label_19;
                }
              }
            }
          }
        }
      }
    }
    List<TimeCardMaint.EPTimecardDetail> details = this.GetDetails((EPTimeCardSummary) oldRow, this.Document.Current, false);
    try
    {
      this.dontSyncSummary = true;
      foreach (TimeCardMaint.EPTimecardDetail epTimecardDetail in details)
      {
        nullable2 = epTimecardDetail.SummaryLineNbr;
        nullable1 = row.LineNbr;
        if (nullable2.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable2.HasValue == nullable1.HasValue)
          this.Activities.Delete(epTimecardDetail);
      }
    }
    finally
    {
      this.dontSyncSummary = false;
    }
label_19:
    this.UpdateAdjustingActivities((EPTimeCardSummary) row, oldRow.Description == row.Description);
    int? projectId2 = oldRow.ProjectID;
    int? projectId3 = row.ProjectID;
    if (projectId2.GetValueOrDefault() == projectId3.GetValueOrDefault() & projectId2.HasValue == projectId3.HasValue && !(oldRow.EarningType != row.EarningType))
      return;
    sender.SetDefaultExt<TimeCardMaint.EPTimeCardSummaryWithInfo.labourItemID>(e.Row);
  }

  protected virtual void EPTimeCardSummaryWithInfo_RowDeleting(
    PXCache sender,
    PXRowDeletingEventArgs e)
  {
    try
    {
      if (!(e.Row is TimeCardMaint.EPTimeCardSummaryWithInfo row) || this.dontSyncDetails)
        return;
      int? nullable = row.Mon;
      if (!nullable.HasValue)
      {
        nullable = row.Tue;
        if (!nullable.HasValue)
        {
          nullable = row.Wed;
          if (!nullable.HasValue)
          {
            nullable = row.Thu;
            if (!nullable.HasValue)
            {
              nullable = row.Fri;
              if (!nullable.HasValue)
              {
                nullable = row.Sat;
                if (!nullable.HasValue)
                {
                  nullable = row.Sun;
                  if (!nullable.HasValue)
                    return;
                }
              }
            }
          }
        }
      }
      if (this.GetDetails((EPTimeCardSummary) row, this.Document.Current, true).Count > 0 && this.FindDuplicates((EPTimeCardSummary) row).Count == 0)
      {
        e.Cancel = true;
        throw new PXException("The summary record cannot be deleted.");
      }
    }
    finally
    {
    }
  }

  protected virtual void EPTimeCardSummaryWithInfo_RowDeleted(
    PXCache sender,
    PXRowDeletedEventArgs e)
  {
    try
    {
      if (!(e.Row is TimeCardMaint.EPTimeCardSummaryWithInfo row) || this.dontSyncDetails)
        return;
      this.UpdateAdjustingActivities((EPTimeCardSummary) row);
    }
    finally
    {
    }
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<TimeCardMaint.EPTimeCardSummaryWithInfo, EPTimeCardSummary.employeeRate> e)
  {
    if (this.Document.Current == null)
      return;
    System.DateTime? nullable = new System.DateTime?(PXWeekSelector2Attribute.GetWeekStartDate((PXGraph) this, this.Document.Current.WeekID.Value));
    if (!nullable.HasValue)
      return;
    e.NewValue = (object) (Decimal?) this.CostEngine.CalculateEmployeeCost((string) null, e.Row.EarningType, e.Row.LabourItemID, e.Row.ProjectID, e.Row.ProjectTaskID, e.Row.CertifiedJob, e.Row.UnionID, this.Document.Current.EmployeeID, nullable.Value, e.Row.ShiftID)?.Rate;
  }

  protected virtual void EPTimeCardSummaryWithInfo_ProjectID_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    if (!(e.Row is TimeCardMaint.EPTimeCardSummaryWithInfo row))
      return;
    PMTask dirty = PMTask.PK.FindDirty((PXGraph) this, row.ProjectID, row.ProjectTaskID);
    if (dirty != null)
    {
      PMTask pmTask = PMTask.UK.Find((PXGraph) this, row.ProjectID, dirty.TaskCD);
      if (pmTask != null)
        row.ProjectTaskID = pmTask.TaskID;
      else
        row.ProjectTaskID = new int?();
    }
    sender.SetDefaultExt<EPTimeCardSummary.unionID>(e.Row);
    sender.SetDefaultExt<EPTimeCardSummary.certifiedJob>(e.Row);
    sender.SetDefaultExt<EPTimeCardSummary.labourItemID>(e.Row);
    sender.SetDefaultExt<EPTimeCardSummary.employeeRate>(e.Row);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<TimeCardMaint.EPTimeCardSummaryWithInfo, TimeCardMaint.EPTimeCardSummaryWithInfo.projectTaskID> e)
  {
    e.Cache.SetDefaultExt<EPTimeCardSummary.employeeRate>((object) e.Row);
    e.Cache.SetDefaultExt<TimeCardMaint.EPTimeCardSummaryWithInfo.costCodeID>((object) e.Row);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<TimeCardMaint.EPTimeCardSummaryWithInfo, TimeCardMaint.EPTimeCardSummaryWithInfo.costCodeID> e)
  {
    e.Cache.SetDefaultExt<TimeCardMaint.EPTimeCardSummaryWithInfo.workCodeID>((object) e.Row);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<TimeCardMaint.EPTimeCardSummaryWithInfo, EPTimeCardSummary.earningType> e)
  {
    TimeCardMaint.EPTimeCardSummaryWithInfo row = e.Row;
    if (!this.IsImportFromExcel & (!row.ProjectID.HasValue || !row.ProjectTaskID.HasValue))
    {
      e.Cache.SetValue<TimeCardMaint.EPTimeCardSummaryWithInfo.projectTaskID>((object) e.Row, (object) null);
      e.Cache.SetDefaultExt<TimeCardMaint.EPTimeCardSummaryWithInfo.projectID>((object) e.Row);
    }
    e.Cache.SetDefaultExt<TimeCardMaint.EPTimeCardSummaryWithInfo.labourItemID>((object) e.Row);
    e.Cache.SetDefaultExt<EPTimeCardSummary.employeeRate>((object) e.Row);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<TimeCardMaint.EPTimeCardSummaryWithInfo, TimeCardMaint.EPTimeCardSummaryWithInfo.labourItemID> e)
  {
    e.Cache.SetDefaultExt<EPTimeCardSummary.employeeRate>((object) e.Row);
    e.Cache.SetDefaultExt<TimeCardMaint.EPTimeCardSummaryWithInfo.costCodeID>((object) e.Row);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<TimeCardMaint.EPTimeCardSummaryWithInfo, TimeCardMaint.EPTimeCardSummaryWithInfo.certifiedJob> e)
  {
    e.Cache.SetDefaultExt<EPTimeCardSummary.employeeRate>((object) e.Row);
  }

  protected virtual void EPTimeCardSummaryWithInfo_ProjectID_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    TimeCardMaint.EPTimeCardSummaryWithInfo row = (TimeCardMaint.EPTimeCardSummaryWithInfo) e.Row;
    if (row == null || this.skipValidation || e.NewValue == null || !(e.NewValue is int))
      return;
    PMProject pmProject = (PMProject) PXSelectorAttribute.Select<TimeCardMaint.EPTimeCardSummaryWithInfo.projectID>(sender, e.Row, e.NewValue);
    if (pmProject == null)
      return;
    bool? nullable = pmProject.IsCompleted;
    if (nullable.GetValueOrDefault())
      sender.RaiseExceptionHandling<EPTimeCardSummary.projectID>((object) row, e.NewValue, (Exception) new PXSetPropertyException("Project is Completed and cannot be used for data entry.", PXErrorLevel.Error)
      {
        ErrorValue = (object) pmProject.ContractCD
      });
    nullable = pmProject.IsCancelled;
    if (!nullable.GetValueOrDefault())
      return;
    sender.RaiseExceptionHandling<EPTimeCardSummary.projectID>((object) row, e.NewValue, (Exception) new PXSetPropertyException("Project is Canceled and cannot be used for data entry.", PXErrorLevel.Error)
    {
      ErrorValue = (object) pmProject.ContractCD
    });
  }

  protected virtual void EPTimeCardSummaryWithInfo_ProjectTaskID_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    TimeCardMaint.EPTimeCardSummaryWithInfo row = (TimeCardMaint.EPTimeCardSummaryWithInfo) e.Row;
    if (row == null || this.skipValidation || e.NewValue == null || !(e.NewValue is int newValue))
      return;
    PMTask pmTask = PMTask.PK.Find(sender.Graph, row.ProjectID, new int?(newValue));
    if (pmTask == null)
      return;
    bool? nullable = pmTask.IsCompleted;
    if (nullable.GetValueOrDefault())
      sender.RaiseExceptionHandling<EPTimeCardSummary.projectTaskID>((object) row, e.NewValue, (Exception) new PXSetPropertyException("Task is Completed and cannot be used for data entry.", PXErrorLevel.Error)
      {
        ErrorValue = (object) pmTask.TaskCD
      });
    nullable = pmTask.IsCancelled;
    if (!nullable.GetValueOrDefault())
      return;
    sender.RaiseExceptionHandling<EPTimeCardSummary.projectTaskID>((object) row, e.NewValue, (Exception) new PXSetPropertyException("Task is Canceled and cannot be used for data entry.", PXErrorLevel.Error)
    {
      ErrorValue = (object) pmTask.TaskCD
    });
  }

  protected virtual void EPTimecardDetail_RowSelecting(PXCache sender, PXRowSelectingEventArgs e)
  {
    if (!(e.Row is TimeCardMaint.EPTimecardDetail row))
      return;
    using (new PXConnectionScope())
      this.InitEarningType(row);
    this.RecalculateFields(row);
    System.DateTime? date = row.Date;
    if (!date.HasValue)
      return;
    TimeCardMaint.EPTimecardDetail epTimecardDetail = row;
    date = row.Date;
    string str = ((int) date.Value.DayOfWeek).ToString();
    epTimecardDetail.Day = str;
  }

  protected virtual void EPTimecardDetail_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    if (!(e.Row is TimeCardMaint.EPTimecardDetail row1))
      return;
    bool? nullable = row1.Released;
    if (nullable.GetValueOrDefault())
    {
      PXUIFieldAttribute.SetEnabled(sender, e.Row, false);
    }
    else
    {
      PXCache cache1 = sender;
      object row2 = e.Row;
      nullable = row1.IsOvertimeCalc;
      int num1;
      if (!nullable.GetValueOrDefault())
      {
        nullable = row1.IsBillable;
        num1 = nullable.GetValueOrDefault() ? 1 : 0;
      }
      else
        num1 = 0;
      PXUIFieldAttribute.SetEnabled<TimeCardMaint.EPTimecardDetail.billableTimeCalc>(cache1, row2, num1 != 0);
      PXCache cache2 = sender;
      object row3 = e.Row;
      nullable = row1.IsOvertimeCalc;
      int num2;
      if (nullable.GetValueOrDefault())
      {
        nullable = row1.IsBillable;
        num2 = nullable.GetValueOrDefault() ? 1 : 0;
      }
      else
        num2 = 0;
      PXUIFieldAttribute.SetEnabled<TimeCardMaint.EPTimecardDetail.billableOvertimeCalc>(cache2, row3, num2 != 0);
    }
    if (row1.ApprovalStatus == "OP" && row1.ApproverID.HasValue)
      PXUIFieldAttribute.SetWarning<PMTimeActivity.approvalStatus>(sender, (object) row1, "Activity is Open and is not visible to the Approver. Please complete the activity so that it can be approved.");
    this.ValidateProjectAndProjectTask(row1);
  }

  protected virtual void EPTimecardDetail_RowInserting(PXCache sender, PXRowInsertingEventArgs e)
  {
    if (!(e.Row is TimeCardMaint.EPTimecardDetail row) || row.OrigNoteID.HasValue)
      return;
    EPTimeCard current = this.Document.Current;
    if (current != null)
    {
      int? nullable1 = current.WeekID;
      if (nullable1.HasValue)
      {
        nullable1 = current.EmployeeID;
        if (nullable1.HasValue)
        {
          EPEarningType epEarningType = this.InitEarningType(row);
          if (epEarningType != null)
          {
            row.EarningTypeID = epEarningType.TypeCD;
            if (!this.isCreateCorrectionFlag)
              row.IsBillable = new bool?(epEarningType.isBillable.GetValueOrDefault());
          }
          this.RecalculateFields(row);
          row.OwnerID = this.Employee.Current.With<EPEmployee, int?>((Func<EPEmployee, int?>) (_ => _.DefContactID));
          row.Billed = new bool?(false);
          row.TrackTime = new bool?(true);
          if (epEarningType != null)
          {
            nullable1 = epEarningType.ProjectID;
            if (nullable1.HasValue)
            {
              nullable1 = row.ProjectID;
              if (!nullable1.HasValue && PXSelectorAttribute.Select(sender, e.Row, sender.GetField(typeof (TimeCardMaint.EPTimecardDetail.projectID)), (object) epEarningType.ProjectID) != null)
                row.ProjectID = epEarningType.ProjectID;
            }
          }
          bool? nullable2;
          if (epEarningType != null)
          {
            nullable1 = epEarningType.TaskID;
            if (nullable1.HasValue)
            {
              nullable1 = row.ProjectTaskID;
              if (!nullable1.HasValue && !ProjectDefaultAttribute.IsNonProject(row.ProjectID))
              {
                PMTask dirty = PMTask.PK.FindDirty((PXGraph) this, epEarningType.ProjectID, epEarningType.TaskID);
                if (dirty != null)
                {
                  nullable2 = dirty.VisibleInTA;
                  if (nullable2.GetValueOrDefault())
                    row.ProjectTaskID = epEarningType.TaskID;
                }
              }
            }
          }
          nullable2 = row.IsBillable;
          if (nullable2.GetValueOrDefault())
            row.TimeBillable = row.TimeSpent;
          row.ApprovalStatus = "CD";
          System.DateTime? date = row.Date;
          if (!date.HasValue)
            row.Date = CRActivityMaint.GetNextActivityStartDate<TimeCardMaint.EPTimecardDetail>((PXGraph) this, this.Activities.Select(), (PMTimeActivity) row, current.WeekID, current.WeekID, this.TempData.Cache, typeof (CRActivityMaint.EPTempData.lastEnteredDate));
          row.WeekID = current.WeekID;
          TimeCardMaint.EPTimecardDetail epTimecardDetail = row;
          date = row.Date;
          string str = ((int) date.Value.DayOfWeek).ToString();
          epTimecardDetail.Day = str;
          nullable1 = row.ProjectID;
          if (nullable1.HasValue || ProjectAttribute.IsPMVisible("TA"))
            return;
          row.ProjectID = ProjectDefaultAttribute.NonProject();
          return;
        }
      }
    }
    e.Cancel = true;
  }

  protected virtual void EPTimecardDetail_RowInserted(PXCache sender, PXRowInsertedEventArgs e)
  {
    if (!(e.Row is TimeCardMaint.EPTimecardDetail row))
      return;
    EmployeeActivitiesEntry.UpdateReportedInTimeZoneIDIfNeeded(sender, (PMTimeActivity) row, new System.DateTime?(), row.Date);
    if (this.dontSyncSummary)
      return;
    this.AddToSummary(this.GetSummaryRecord(row), (PMTimeActivity) row);
  }

  protected virtual void EPTimecardDetail_RowUpdating(PXCache sender, PXRowUpdatingEventArgs e)
  {
    TimeCardMaint.EPTimecardDetail newRow = e.NewRow as TimeCardMaint.EPTimecardDetail;
    TimeCardMaint.EPTimecardDetail row = e.Row as TimeCardMaint.EPTimecardDetail;
    if (newRow == null)
      return;
    bool? nullable1 = newRow.Billed;
    if (nullable1.GetValueOrDefault())
      return;
    EPTimeCard current = this.Document.Current;
    if (current != null)
    {
      int? nullable2 = current.WeekID;
      if (nullable2.HasValue)
      {
        int? nullable3;
        if (!this.dontSyncSummary)
        {
          EPEarningType epEarningType = this.InitEarningType(newRow);
          newRow.EarningTypeID = epEarningType.TypeCD;
          nullable1 = newRow.IsBillable;
          if (!nullable1.HasValue || newRow.EarningTypeID != row.EarningTypeID)
          {
            TimeCardMaint.EPTimecardDetail epTimecardDetail = newRow;
            nullable1 = epEarningType.isBillable;
            bool? nullable4 = new bool?(nullable1.GetValueOrDefault());
            epTimecardDetail.IsBillable = nullable4;
          }
          nullable2 = epEarningType.ProjectID;
          if (nullable2.HasValue)
          {
            nullable2 = newRow.ProjectID;
            if ((!nullable2.HasValue || newRow.EarningTypeID != row.EarningTypeID) && PXSelectorAttribute.Select(sender, e.Row, sender.GetField(typeof (TimeCardMaint.EPTimecardDetail.projectID)), (object) epEarningType.ProjectID) != null)
            {
              newRow.ProjectID = epEarningType.ProjectID;
              if (ProjectDefaultAttribute.IsNonProject(newRow.ProjectID))
              {
                TimeCardMaint.EPTimecardDetail epTimecardDetail = newRow;
                nullable2 = new int?();
                int? nullable5 = nullable2;
                epTimecardDetail.ProjectTaskID = nullable5;
              }
            }
          }
          nullable2 = epEarningType.TaskID;
          if (nullable2.HasValue)
          {
            nullable2 = newRow.ProjectTaskID;
            if (!nullable2.HasValue || newRow.EarningTypeID != row.EarningTypeID)
            {
              nullable2 = newRow.ProjectID;
              nullable3 = epEarningType.ProjectID;
              if (nullable2.GetValueOrDefault() == nullable3.GetValueOrDefault() & nullable2.HasValue == nullable3.HasValue)
              {
                PMTask dirty = PMTask.PK.FindDirty((PXGraph) this, epEarningType.ProjectID, epEarningType.TaskID);
                if (dirty != null)
                {
                  nullable1 = dirty.VisibleInTA;
                  if (nullable1.GetValueOrDefault())
                    newRow.ProjectTaskID = epEarningType.TaskID;
                }
              }
            }
          }
        }
        nullable3 = newRow.ProjectID;
        if (!nullable3.HasValue)
        {
          nullable3 = newRow.TimeSpent;
          if (nullable3.GetValueOrDefault() != 0)
            sender.RaiseExceptionHandling<TimeCardMaint.EPTimecardDetail.projectID>((object) newRow, (object) null, (Exception) new PXSetPropertyException("'{0}' cannot be empty.", new object[1]
            {
              (object) "[projectID]"
            }));
        }
        System.DateTime? date = newRow.Date;
        if (date.HasValue)
        {
          date = newRow.Date;
          int weekId = PXWeekSelector2Attribute.GetWeekID((PXGraph) this, date.Value);
          nullable3 = current.WeekID;
          int valueOrDefault = nullable3.GetValueOrDefault();
          if (weekId == valueOrDefault & nullable3.HasValue)
            goto label_25;
        }
        TimeCardMaint.EPTimecardDetail epTimecardDetail1 = newRow;
        nullable3 = current.WeekID;
        System.DateTime? nullable6 = new System.DateTime?(PXWeekSelector2Attribute.GetWeekStartDate((PXGraph) this, nullable3.Value));
        epTimecardDetail1.Date = nullable6;
label_25:
        newRow.WeekID = current.WeekID;
        return;
      }
    }
    e.Cancel = true;
  }

  protected virtual void EPTimecardDetail_RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    TimeCardMaint.EPTimecardDetail row = e.Row as TimeCardMaint.EPTimecardDetail;
    TimeCardMaint.EPTimecardDetail oldRow = e.OldRow as TimeCardMaint.EPTimecardDetail;
    if (row == null)
      return;
    EmployeeActivitiesEntry.UpdateReportedInTimeZoneIDIfNeeded(sender, (PMTimeActivity) row, oldRow.Date, row.Date);
    int? nullable1 = row.SummaryLineNbr;
    if (nullable1.HasValue)
    {
      TimeCardMaint.EPTimeCardSummaryWithInfo summaryRecord = this.GetSummaryRecord(row);
      if (summaryRecord != null)
      {
        bool? nullable2 = summaryRecord.IsBillable;
        int num1 = nullable2.GetValueOrDefault() ? 1 : 0;
        nullable2 = row.IsBillable;
        int num2 = nullable2.GetValueOrDefault() ? 1 : 0;
        if (num1 == num2 && !(summaryRecord.EarningType != row.EarningTypeID))
        {
          nullable1 = summaryRecord.ProjectID;
          int? projectId = row.ProjectID;
          if (nullable1.GetValueOrDefault() == projectId.GetValueOrDefault() & nullable1.HasValue == projectId.HasValue)
          {
            int? projectTaskId = summaryRecord.ProjectTaskID;
            nullable1 = row.ProjectTaskID;
            if (projectTaskId.GetValueOrDefault() == nullable1.GetValueOrDefault() & projectTaskId.HasValue == nullable1.HasValue)
            {
              Guid? parentNoteId = summaryRecord.ParentNoteID;
              Guid? parentTaskNoteId = row.ParentTaskNoteID;
              if ((parentNoteId.HasValue == parentTaskNoteId.HasValue ? (parentNoteId.HasValue ? (parentNoteId.GetValueOrDefault() != parentTaskNoteId.GetValueOrDefault() ? 1 : 0) : 0) : 1) == 0)
              {
                nullable1 = summaryRecord.JobID;
                int? jobId = row.JobID;
                if (nullable1.GetValueOrDefault() == jobId.GetValueOrDefault() & nullable1.HasValue == jobId.HasValue)
                {
                  int? shiftId = summaryRecord.ShiftID;
                  nullable1 = row.ShiftID;
                  if (shiftId.GetValueOrDefault() == nullable1.GetValueOrDefault() & shiftId.HasValue == nullable1.HasValue)
                  {
                    nullable1 = summaryRecord.CostCodeID;
                    int? costCodeId = row.CostCodeID;
                    if (nullable1.GetValueOrDefault() == costCodeId.GetValueOrDefault() & nullable1.HasValue == costCodeId.HasValue && !(summaryRecord.UnionID != row.UnionID))
                    {
                      int? labourItemId = summaryRecord.LabourItemID;
                      nullable1 = row.LabourItemID;
                      if (labourItemId.GetValueOrDefault() == nullable1.GetValueOrDefault() & labourItemId.HasValue == nullable1.HasValue && !(summaryRecord.WorkCodeID != row.WorkCodeID))
                      {
                        nullable2 = summaryRecord.CertifiedJob;
                        bool? certifiedJob = row.CertifiedJob;
                        if (nullable2.GetValueOrDefault() == certifiedJob.GetValueOrDefault() & nullable2.HasValue == certifiedJob.HasValue)
                          goto label_14;
                      }
                    }
                  }
                }
              }
            }
          }
        }
        TimeCardMaint.EPTimecardDetail epTimecardDetail = row;
        nullable1 = new int?();
        int? nullable3 = nullable1;
        epTimecardDetail.SummaryLineNbr = nullable3;
      }
    }
label_14:
    if (row.EarningTypeID != oldRow.EarningTypeID)
      this.InitEarningType(row);
    System.DateTime? date1 = row.Date;
    System.DateTime valueOrDefault1 = date1.GetValueOrDefault();
    date1 = oldRow.Date;
    System.DateTime valueOrDefault2 = date1.GetValueOrDefault();
    bool? nullable4;
    if (!(valueOrDefault1 != valueOrDefault2) && !(row.EarningTypeID != oldRow.EarningTypeID))
    {
      nullable1 = row.ProjectID;
      int valueOrDefault3 = nullable1.GetValueOrDefault();
      nullable1 = oldRow.ProjectID;
      int valueOrDefault4 = nullable1.GetValueOrDefault();
      if (valueOrDefault3 == valueOrDefault4)
      {
        nullable1 = row.ProjectTaskID;
        int valueOrDefault5 = nullable1.GetValueOrDefault();
        nullable1 = oldRow.ProjectTaskID;
        int valueOrDefault6 = nullable1.GetValueOrDefault();
        if (valueOrDefault5 == valueOrDefault6)
        {
          nullable1 = row.CostCodeID;
          int? costCodeId = oldRow.CostCodeID;
          if (nullable1.GetValueOrDefault() == costCodeId.GetValueOrDefault() & nullable1.HasValue == costCodeId.HasValue && !(row.UnionID != oldRow.UnionID))
          {
            int? labourItemId = row.LabourItemID;
            nullable1 = oldRow.LabourItemID;
            if (labourItemId.GetValueOrDefault() == nullable1.GetValueOrDefault() & labourItemId.HasValue == nullable1.HasValue)
            {
              nullable4 = row.CertifiedJob;
              int num3 = nullable4.GetValueOrDefault() ? 1 : 0;
              nullable4 = oldRow.CertifiedJob;
              int num4 = nullable4.GetValueOrDefault() ? 1 : 0;
              if (num3 == num4)
              {
                nullable1 = row.ShiftID;
                int valueOrDefault7 = nullable1.GetValueOrDefault();
                nullable1 = oldRow.ShiftID;
                int valueOrDefault8 = nullable1.GetValueOrDefault();
                if (valueOrDefault7 == valueOrDefault8)
                  goto label_24;
              }
            }
          }
        }
      }
    }
    EmployeeCostEngine costEngine = this.CostEngine;
    string earningTypeId = row.EarningTypeID;
    int? labourItemId1 = row.LabourItemID;
    int? projectId1 = row.ProjectID;
    int? projectTaskId1 = row.ProjectTaskID;
    bool? certifiedJob1 = row.CertifiedJob;
    string unionId = row.UnionID;
    int? employeeId = this.Document.Current.EmployeeID;
    date1 = row.Date;
    System.DateTime date2 = date1.Value;
    int? shiftId1 = row.ShiftID;
    EmployeeCostEngine.LaborCost employeeCost = costEngine.CalculateEmployeeCost((string) null, earningTypeId, labourItemId1, projectId1, projectTaskId1, certifiedJob1, unionId, employeeId, date2, shiftId1);
    row.EmployeeRate = (Decimal?) employeeCost?.Rate;
    row.OvertimeMultiplierCalc = (Decimal?) employeeCost?.OvertimeMultiplier;
label_24:
    if (!(row.EarningTypeID != oldRow.EarningTypeID))
    {
      nullable1 = row.TimeSpent;
      int valueOrDefault9 = nullable1.GetValueOrDefault();
      nullable1 = oldRow.TimeSpent;
      int valueOrDefault10 = nullable1.GetValueOrDefault();
      if (valueOrDefault9 == valueOrDefault10)
      {
        nullable4 = row.IsBillable;
        int num5 = nullable4.GetValueOrDefault() ? 1 : 0;
        nullable4 = oldRow.IsBillable;
        int num6 = nullable4.GetValueOrDefault() ? 1 : 0;
        if (num5 == num6)
          goto label_28;
      }
    }
    this.RecalculateFields(row);
label_28:
    nullable1 = row.ProjectTaskID;
    int? projectTaskId2 = oldRow.ProjectTaskID;
    if (!(nullable1.GetValueOrDefault() == projectTaskId2.GetValueOrDefault() & nullable1.HasValue == projectTaskId2.HasValue))
      sender.SetDefaultExt<PMTimeActivity.approverID>((object) row);
    date1 = row.Date;
    System.DateTime valueOrDefault11 = date1.GetValueOrDefault();
    date1 = oldRow.Date;
    System.DateTime valueOrDefault12 = date1.GetValueOrDefault();
    int? nullable5;
    if (!(valueOrDefault11 != valueOrDefault12) && !(row.EarningTypeID != oldRow.EarningTypeID))
    {
      nullable5 = row.ProjectID;
      int valueOrDefault13 = nullable5.GetValueOrDefault();
      nullable5 = oldRow.ProjectID;
      int valueOrDefault14 = nullable5.GetValueOrDefault();
      if (valueOrDefault13 == valueOrDefault14)
      {
        nullable5 = row.ProjectTaskID;
        int valueOrDefault15 = nullable5.GetValueOrDefault();
        nullable5 = oldRow.ProjectTaskID;
        int valueOrDefault16 = nullable5.GetValueOrDefault();
        if (valueOrDefault15 == valueOrDefault16)
        {
          nullable5 = row.TimeSpent;
          int valueOrDefault17 = nullable5.GetValueOrDefault();
          nullable5 = oldRow.TimeSpent;
          int valueOrDefault18 = nullable5.GetValueOrDefault();
          if (valueOrDefault17 == valueOrDefault18)
          {
            nullable4 = row.IsBillable;
            int num7 = nullable4.GetValueOrDefault() ? 1 : 0;
            nullable4 = oldRow.IsBillable;
            int num8 = nullable4.GetValueOrDefault() ? 1 : 0;
            if (num7 == num8)
            {
              nullable5 = row.TimeBillable;
              int valueOrDefault19 = nullable5.GetValueOrDefault();
              nullable5 = oldRow.TimeBillable;
              int valueOrDefault20 = nullable5.GetValueOrDefault();
              if (valueOrDefault19 == valueOrDefault20)
              {
                nullable5 = row.JobID;
                nullable1 = oldRow.JobID;
                if (nullable5.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable5.HasValue == nullable1.HasValue)
                {
                  nullable1 = row.CostCodeID;
                  nullable5 = oldRow.CostCodeID;
                  if (nullable1.GetValueOrDefault() == nullable5.GetValueOrDefault() & nullable1.HasValue == nullable5.HasValue && !(row.UnionID != oldRow.UnionID))
                  {
                    nullable5 = row.LabourItemID;
                    nullable1 = oldRow.LabourItemID;
                    if (nullable5.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable5.HasValue == nullable1.HasValue && !(row.WorkCodeID != oldRow.WorkCodeID))
                    {
                      nullable4 = row.CertifiedJob;
                      int num9 = nullable4.GetValueOrDefault() ? 1 : 0;
                      nullable4 = oldRow.CertifiedJob;
                      int num10 = nullable4.GetValueOrDefault() ? 1 : 0;
                      if (num9 == num10)
                      {
                        nullable1 = row.ShiftID;
                        int valueOrDefault21 = nullable1.GetValueOrDefault();
                        nullable1 = oldRow.ShiftID;
                        int valueOrDefault22 = nullable1.GetValueOrDefault();
                        if (valueOrDefault21 == valueOrDefault22)
                          goto label_43;
                      }
                    }
                  }
                }
              }
            }
          }
        }
      }
    }
    nullable1 = row.ApproverID;
    if (nullable1.HasValue)
      row.ApprovalStatus = "PA";
label_43:
    if (!(row.EarningTypeID != oldRow.EarningTypeID))
    {
      nullable1 = row.ProjectID;
      nullable5 = oldRow.ProjectID;
      if (nullable1.GetValueOrDefault() == nullable5.GetValueOrDefault() & nullable1.HasValue == nullable5.HasValue)
        goto label_46;
    }
    sender.SetDefaultExt<TimeCardMaint.EPTimecardDetail.labourItemID>(e.Row);
label_46:
    if (this.dontSyncSummary)
      return;
    TimeCardMaint.EPTimeCardSummaryWithInfo summaryRecord1 = this.GetSummaryRecord(row);
    this.AddToSummary(summaryRecord1, (PMTimeActivity) row);
    if (summaryRecord1 != null)
    {
      nullable5 = row.SummaryLineNbr;
      if (!nullable5.HasValue)
        row.SummaryLineNbr = summaryRecord1.LineNbr;
    }
    this.SubtractFromSummary(this.GetSummaryRecord(oldRow), (PMTimeActivity) oldRow);
  }

  protected virtual void EPTimecardDetail_RowDeleting(PXCache sender, PXRowDeletingEventArgs e)
  {
    try
    {
      if (!(e.Row is TimeCardMaint.EPTimecardDetail row))
        return;
      if (row.Released.GetValueOrDefault())
        throw new PXException("The time activity cannot be deleted because it has been released.");
      if (this.Document.Current != null && !string.IsNullOrEmpty(this.Document.Current.OrigTimeCardCD) && ((PMTimeActivity) e.Row).OrigNoteID.HasValue && this.Document.Cache.GetStatus((object) this.Document.Current) != PXEntryStatus.Deleted)
        throw new PXException("In the correction Time Card if you want to delete/eliminate previosly released Activity just set the Time to zero.");
      if (row.SummaryLineNbr.HasValue || this.dontSyncSummary)
        return;
      this.SubtractFromSummary(this.GetSummaryRecord(row), (PMTimeActivity) row);
    }
    finally
    {
    }
  }

  protected virtual void EPTimecardDetail_RowDeleted(PXCache sender, PXRowDeletedEventArgs e)
  {
    try
    {
      if (!(e.Row is TimeCardMaint.EPTimecardDetail row) || !row.SummaryLineNbr.HasValue || this.dontSyncSummary)
        return;
      this.SubtractFromSummary(this.GetSummaryRecord(row), (PMTimeActivity) row);
    }
    finally
    {
    }
  }

  protected virtual void EPTimecardDetail_StartDate_FieldUpdating(
    PXCache sender,
    PXFieldUpdatingEventArgs e)
  {
    if (!(e.Row is TimeCardMaint.EPTimecardDetail row))
      return;
    EPTimeCard current = this.Document.Current;
    if (row.Date.HasValue || current == null)
      return;
    int? nullable = current.WeekID;
    if (!nullable.HasValue)
      return;
    nullable = current.EmployeeID;
    if (!nullable.HasValue)
      return;
    System.DateTime? activityStartDate = CRActivityMaint.GetNextActivityStartDate<TimeCardMaint.EPTimecardDetail>((PXGraph) this, this.Activities.Select(), (PMTimeActivity) row, current.WeekID, current.WeekID, this.TempData.Cache, typeof (CRActivityMaint.EPTempData.lastEnteredDate));
    sender.SetValueExt<TimeCardMaint.EPTimecardDetail.date>((object) row, (object) activityStartDate);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<TimeCardMaint.EPTimecardDetail, TimeCardMaint.EPTimecardDetail.date> e)
  {
    TimeCardMaint.EPTimecardDetail row = e.Row;
    if (row == null)
      return;
    if (row.Date.HasValue)
      this.TempData.Cache.SetValue<CRActivityMaint.EPTempData.lastEnteredDate>((object) this.TempData.Current, (object) row.Date);
    EmployeeActivitiesEntry.UpdateReportedInTimeZoneIDIfNeeded(e.Cache, (PMTimeActivity) e.Row, (System.DateTime?) e.OldValue, (System.DateTime?) e.NewValue);
  }

  internal static void SetSummaryColumnsDisplayName(
    PXWeekSelector2Attribute.WeekInfo weekInfo,
    params PXCache[] caches)
  {
    string shortDatePattern = LocalesFormatProvider.GetLocaleFormatInfo()?.ShortDatePattern;
    if (shortDatePattern == null || caches.Length == 0)
      return;
    (DayOfWeek, System.Type, System.DateTime?)[] valueTupleArray = new (DayOfWeek, System.Type, System.DateTime?)[7]
    {
      (DayOfWeek.Monday, typeof (EPTimeCardSummary.mon), weekInfo.Mon.Date),
      (DayOfWeek.Tuesday, typeof (EPTimeCardSummary.tue), weekInfo.Tue.Date),
      (DayOfWeek.Wednesday, typeof (EPTimeCardSummary.wed), weekInfo.Wed.Date),
      (DayOfWeek.Thursday, typeof (EPTimeCardSummary.thu), weekInfo.Thu.Date),
      (DayOfWeek.Friday, typeof (EPTimeCardSummary.fri), weekInfo.Fri.Date),
      (DayOfWeek.Saturday, typeof (EPTimeCardSummary.sat), weekInfo.Sat.Date),
      (DayOfWeek.Sunday, typeof (EPTimeCardSummary.sun), weekInfo.Sun.Date)
    };
    PXCache cach1 = caches[0].Graph.Caches[typeof (EPTimeCardSummary)];
    foreach ((DayOfWeek _, System.Type type, System.DateTime? nullable) in valueTupleArray)
    {
      string displayName1 = PXUIFieldAttribute.GetDisplayName(cach1, type.Name);
      string displayName2 = nullable.HasValue ? $"{displayName1} {nullable.Value.ToString(shortDatePattern, (IFormatProvider) CultureInfo.InvariantCulture)}" : displayName1;
      foreach (PXCache cach2 in caches)
        PXUIFieldAttribute.SetDisplayNameLocalized(cach2, type.Name, displayName2);
    }
  }

  private static string LocalizeDayOfWeek(DayOfWeek dayOfWeek)
  {
    string strMessage;
    switch (dayOfWeek)
    {
      case DayOfWeek.Monday:
        strMessage = "Monday";
        break;
      case DayOfWeek.Tuesday:
        strMessage = "Tuesday";
        break;
      case DayOfWeek.Wednesday:
        strMessage = "Wednesday";
        break;
      case DayOfWeek.Thursday:
        strMessage = "Thursday";
        break;
      case DayOfWeek.Friday:
        strMessage = "Friday";
        break;
      case DayOfWeek.Saturday:
        strMessage = "Saturday";
        break;
      default:
        strMessage = "Sunday";
        break;
    }
    return PXMessages.LocalizeNoPrefix(strMessage);
  }

  protected virtual void EPTimecardDetail_Day_FieldSelecting(
    PXCache sender,
    PXFieldSelectingEventArgs e)
  {
    if (this.Document.Current == null)
      return;
    int? weekId = this.Document.Current.WeekID;
    if (!weekId.HasValue)
      return;
    weekId = this.Document.Current.WeekID;
    PXWeekSelector2Attribute.WeekInfo weekInfo = PXWeekSelector2Attribute.GetWeekInfo((PXGraph) this, weekId.Value);
    List<string> stringList1 = new List<string>();
    List<string> stringList2 = new List<string>();
    if (weekInfo.Mon.Enabled)
    {
      stringList1.Add("1");
      stringList2.Add(TimeCardMaint.LocalizeDayOfWeek(DayOfWeek.Monday));
    }
    if (weekInfo.Tue.Enabled)
    {
      stringList1.Add("2");
      stringList2.Add(TimeCardMaint.LocalizeDayOfWeek(DayOfWeek.Tuesday));
    }
    if (weekInfo.Wed.Enabled)
    {
      stringList1.Add("3");
      stringList2.Add(TimeCardMaint.LocalizeDayOfWeek(DayOfWeek.Wednesday));
    }
    if (weekInfo.Thu.Enabled)
    {
      stringList1.Add("4");
      stringList2.Add(TimeCardMaint.LocalizeDayOfWeek(DayOfWeek.Thursday));
    }
    if (weekInfo.Fri.Enabled)
    {
      stringList1.Add("5");
      stringList2.Add(TimeCardMaint.LocalizeDayOfWeek(DayOfWeek.Friday));
    }
    if (weekInfo.Sat.Enabled)
    {
      stringList1.Add("6");
      stringList2.Add(TimeCardMaint.LocalizeDayOfWeek(DayOfWeek.Saturday));
    }
    if (weekInfo.Sun.Enabled)
    {
      stringList1.Add("0");
      stringList2.Add(TimeCardMaint.LocalizeDayOfWeek(DayOfWeek.Sunday));
    }
    e.ReturnState = (object) PXStringState.CreateInstance(e.ReturnState, new int?(1), new bool?(false), typeof (TimeCardMaint.EPTimecardDetail.day).Name, new bool?(false), new int?(1), (string) null, stringList1.ToArray(), stringList2.ToArray(), new bool?(true), stringList1[0]);
  }

  protected virtual void EPTimecardDetail_Day_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    if (!(e.Row is TimeCardMaint.EPTimecardDetail row) || !e.ExternalCall || this.Document.Current == null)
      return;
    PXWeekSelector2Attribute.WeekInfo weekInfo = PXWeekSelector2Attribute.GetWeekInfo((PXGraph) this, this.Document.Current.WeekID.Value);
    switch ((DayOfWeek) Enum.Parse(typeof (DayOfWeek), row.Day))
    {
      case DayOfWeek.Sunday:
        sender.SetValueExt<TimeCardMaint.EPTimecardDetail.date>((object) row, (object) this.CombineDateAndTime(weekInfo.Sun.Date, row.Date));
        break;
      case DayOfWeek.Monday:
        sender.SetValueExt<TimeCardMaint.EPTimecardDetail.date>((object) row, (object) this.CombineDateAndTime(weekInfo.Mon.Date, row.Date));
        break;
      case DayOfWeek.Tuesday:
        sender.SetValueExt<TimeCardMaint.EPTimecardDetail.date>((object) row, (object) this.CombineDateAndTime(weekInfo.Tue.Date, row.Date));
        break;
      case DayOfWeek.Wednesday:
        sender.SetValueExt<TimeCardMaint.EPTimecardDetail.date>((object) row, (object) this.CombineDateAndTime(weekInfo.Wed.Date, row.Date));
        break;
      case DayOfWeek.Thursday:
        sender.SetValueExt<TimeCardMaint.EPTimecardDetail.date>((object) row, (object) this.CombineDateAndTime(weekInfo.Thu.Date, row.Date));
        break;
      case DayOfWeek.Friday:
        sender.SetValueExt<TimeCardMaint.EPTimecardDetail.date>((object) row, (object) this.CombineDateAndTime(weekInfo.Fri.Date, row.Date));
        break;
      case DayOfWeek.Saturday:
        sender.SetValueExt<TimeCardMaint.EPTimecardDetail.date>((object) row, (object) this.CombineDateAndTime(weekInfo.Sat.Date, row.Date));
        break;
    }
  }

  private System.DateTime CombineDateAndTime(System.DateTime? datePart, System.DateTime? timePart)
  {
    System.DateTime dateTime = datePart.Value;
    int year = dateTime.Year;
    dateTime = datePart.Value;
    int month = dateTime.Month;
    dateTime = datePart.Value;
    int day = dateTime.Day;
    dateTime = timePart.Value;
    int hour = dateTime.Hour;
    dateTime = timePart.Value;
    int minute = dateTime.Minute;
    dateTime = timePart.Value;
    int second = dateTime.Second;
    return new System.DateTime(year, month, day, hour, minute, second);
  }

  protected virtual void EPTimecardDetail_StartDate_Date_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    if (!(e.Row is TimeCardMaint.EPTimecardDetail row))
      return;
    int? weekId = row.WeekID;
    if (!(weekId ?? this.Document.Current.With<EPTimeCard, int?>((Func<EPTimeCard, int?>) (_ => _.WeekID))).HasValue)
      return;
    System.DateTime? nullable = new System.DateTime?();
    System.DateTime result;
    if (e.NewValue is string newValue2 && System.DateTime.TryParse(newValue2, (IFormatProvider) sender.Graph.Culture, DateTimeStyles.None, out result))
      nullable = new System.DateTime?(result);
    else if (e.NewValue is System.DateTime newValue1)
      nullable = new System.DateTime?(newValue1);
    weekId = this.Document.Current.WeekID;
    PXWeekSelector2Attribute.WeekInfo weekInfo = PXWeekSelector2Attribute.GetWeekInfo((PXGraph) this, weekId.Value);
    if (nullable.HasValue && !weekInfo.IsValid(nullable.Value.Date))
      throw new PXSetPropertyException("The selected date does not belong to the week selected in the Summary area.");
  }

  protected virtual void EPTimecardDetail_OwnerID_FieldUpdating(
    PXCache sender,
    PXFieldUpdatingEventArgs e)
  {
    if (!(e.Row is TimeCardMaint.EPTimecardDetail row) || row.OwnerID.HasValue)
      return;
    row.OwnerID = this.Employee.Current.With<EPEmployee, int?>((Func<EPEmployee, int?>) (_ => _.DefContactID));
  }

  protected virtual void EPTimecardDetail_StartDate_Date_FieldUpdating(
    PXCache sender,
    PXFieldUpdatingEventArgs e)
  {
    if (!(e.Row is TimeCardMaint.EPTimecardDetail))
      return;
    System.DateTime? nullable = new System.DateTime?();
    System.DateTime result;
    if (e.NewValue is string newValue2 && System.DateTime.TryParse(newValue2, (IFormatProvider) sender.Graph.Culture, DateTimeStyles.None, out result))
      nullable = new System.DateTime?(result);
    else if (e.NewValue is System.DateTime newValue1)
      nullable = new System.DateTime?(newValue1);
    sender.SetValuePending(e.Row, typeof (TimeCardMaint.EPTimecardDetail.date).Name + "_oldValue", (object) nullable);
  }

  protected virtual void EPTimecardDetail_StartDate_Time_FieldUpdating(
    PXCache sender,
    PXFieldUpdatingEventArgs e)
  {
    if (!(e.Row is TimeCardMaint.EPTimecardDetail))
      return;
    System.DateTime? nullable1 = new System.DateTime?();
    System.DateTime result;
    if (e.NewValue is string newValue2 && System.DateTime.TryParse(newValue2, (IFormatProvider) sender.Graph.Culture, DateTimeStyles.None, out result))
      nullable1 = new System.DateTime?(result);
    else if (e.NewValue is System.DateTime newValue1)
      nullable1 = new System.DateTime?(newValue1);
    object valuePending = sender.GetValuePending(e.Row, typeof (TimeCardMaint.EPTimecardDetail.date).Name + "_oldValue");
    if (valuePending == PXCache.NotSetValue)
      return;
    System.DateTime? nullable2 = (System.DateTime?) valuePending;
    if (!nullable2.HasValue || !nullable1.HasValue)
      return;
    System.DateTime dateTime = nullable2.Value;
    TimeSpan timeOfDay = dateTime.TimeOfDay;
    int totalMinutes1 = (int) timeOfDay.TotalMinutes;
    dateTime = nullable1.Value;
    timeOfDay = dateTime.TimeOfDay;
    int totalMinutes2 = (int) timeOfDay.TotalMinutes;
    if (totalMinutes1 != totalMinutes2)
      return;
    e.Cancel = true;
  }

  protected virtual void EPTimecardDetail_StartDate_Date_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    if (!(e.Row is TimeCardMaint.EPTimecardDetail row))
      return;
    object valuePending = sender.GetValuePending(e.Row, typeof (TimeCardMaint.EPTimecardDetail.date).Name + "_oldValue");
    System.DateTime? nullable1;
    System.DateTime dateTime1;
    if (valuePending != PXCache.NotSetValue)
    {
      System.DateTime? nullable2 = (System.DateTime?) valuePending;
      System.DateTime? date1 = row.Date;
      if (date1.HasValue)
      {
        if (nullable2.HasValue)
        {
          System.DateTime? nullable3 = nullable2;
          nullable1 = date1;
          if ((nullable3.HasValue == nullable1.HasValue ? (nullable3.HasValue ? (nullable3.GetValueOrDefault() != nullable1.GetValueOrDefault() ? 1 : 0) : 0) : 1) != 0)
          {
            dateTime1 = nullable2.Value;
            TimeSpan timeOfDay = dateTime1.TimeOfDay;
            int totalMinutes1 = (int) timeOfDay.TotalMinutes;
            dateTime1 = date1.Value;
            timeOfDay = dateTime1.TimeOfDay;
            int totalMinutes2 = (int) timeOfDay.TotalMinutes;
            if (totalMinutes1 != totalMinutes2)
              goto label_9;
          }
          else
            goto label_9;
        }
        string calendarId = CRActivityMaint.GetCalendarID((PXGraph) this, (PMTimeActivity) row);
        if (!string.IsNullOrEmpty(calendarId))
        {
          string calendarID = calendarId;
          dateTime1 = date1.Value;
          System.DateTime date2 = dateTime1.Date;
          System.DateTime dateTime2;
          ref System.DateTime local1 = ref dateTime2;
          System.DateTime dateTime3;
          ref System.DateTime local2 = ref dateTime3;
          CalendarHelper.CalculateStartEndTime((PXGraph) this, calendarID, date2, out local1, out local2);
          sender.SetValueExt<TimeCardMaint.EPTimecardDetail.date>((object) row, (object) dateTime2);
        }
      }
    }
label_9:
    TimeCardMaint.EPTimecardDetail epTimecardDetail = row;
    nullable1 = row.Date;
    dateTime1 = nullable1.Value;
    string str = ((int) dateTime1.DayOfWeek).ToString();
    epTimecardDetail.Day = str;
    if (row == null)
      return;
    nullable1 = row.Date;
    if (!nullable1.HasValue)
      return;
    this.TempData.Cache.SetValue<CRActivityMaint.EPTempData.lastEnteredDate>((object) this.TempData.Current, (object) row.Date);
  }

  protected virtual void EPTimecardDetail_BillableTimeCalc_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    if (!(e.Row is TimeCardMaint.EPTimecardDetail row))
      return;
    int? newValue = (int?) e.NewValue;
    int? timeSpent = row.TimeSpent;
    int? nullable = newValue;
    if (timeSpent.GetValueOrDefault() < nullable.GetValueOrDefault() & timeSpent.HasValue & nullable.HasValue)
      throw new PXSetPropertyException("Time Billable cannot be greater than Time Spent.");
  }

  protected virtual void EPTimecardDetail_UIStatus_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    e.Cancel = true;
  }

  protected virtual void EPTimecardDetail_BillableTimeCalc_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    if (!(e.Row is TimeCardMaint.EPTimecardDetail row))
      return;
    row.TimeBillable = row.BillableTimeCalc;
  }

  protected virtual void EPTimecardDetail_BillableOvertimeCalc_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    if (!(e.Row is TimeCardMaint.EPTimecardDetail row))
      return;
    int? newValue = (int?) e.NewValue;
    if (!row.IsBillable.GetValueOrDefault())
      return;
    int? timeSpent = row.TimeSpent;
    int? nullable = newValue;
    if (timeSpent.GetValueOrDefault() < nullable.GetValueOrDefault() & timeSpent.HasValue & nullable.HasValue)
      throw new PXSetPropertyException("Overtime Billable cannot be greater than the Overtime Spent.");
  }

  protected virtual void EPTimecardDetail_BillableOvertimeCalc_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    if (!(e.Row is TimeCardMaint.EPTimecardDetail row))
      return;
    row.TimeBillable = row.BillableOvertimeCalc;
  }

  protected virtual void EPTimecardDetail_IsBillable_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    if (!(e.Row is TimeCardMaint.EPTimecardDetail row) || !row.IsBillable.GetValueOrDefault() || row.TimeBillable.GetValueOrDefault() != 0)
      return;
    row.TimeBillable = row.TimeSpent;
  }

  protected virtual void EPTimeCardItem_InventoryID_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    sender.SetDefaultExt<EPTimeCardItem.uOM>(e.Row);
    sender.SetDefaultExt<EPTimeCardItem.costCodeID>(e.Row);
  }

  protected virtual void EPTimeCardItem_TaskID_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    sender.SetDefaultExt<EPTimeCardItem.costCodeID>(e.Row);
  }

  protected virtual void EPApproval_Details_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    if (this.Document.Current == null)
      return;
    e.NewValue = (object) null;
    System.Type[] typeArray = new System.Type[5]
    {
      typeof (EPTimeCard.weekDescription),
      typeof (EPTimeCard.timeSpentCalc),
      typeof (EPTimeCard.overtimeSpentCalc),
      typeof (EPTimeCard.timeBillableCalc),
      typeof (EPTimeCard.overtimeBillableCalc)
    };
    foreach (MemberInfo memberInfo in typeArray)
    {
      if (this.Document.Cache.GetValueExt((object) this.Document.Current, memberInfo.Name) is PXStringState valueExt)
      {
        string str = valueExt.InputMask != null ? Mask.Format(valueExt.InputMask, (string) (PXFieldState) valueExt) : valueExt.Value?.ToString();
        if (!string.IsNullOrEmpty(str))
        {
          PXFieldDefaultingEventArgs defaultingEventArgs = e;
          defaultingEventArgs.NewValue = (object) $"{defaultingEventArgs.NewValue?.ToString()}{(e.NewValue != null ? ", " : string.Empty)}{valueExt.DisplayName}={str.Trim()}";
        }
      }
    }
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<TimeCardMaint.EPTimecardDetail, TimeCardMaint.EPTimecardDetail.overtimeMultiplierCalc> e)
  {
    if (this.Document.Current == null)
      return;
    System.DateTime? date1 = e.Row.Date;
    if (!date1.HasValue)
      return;
    PX.Data.Events.FieldDefaulting<TimeCardMaint.EPTimecardDetail, TimeCardMaint.EPTimecardDetail.overtimeMultiplierCalc> fieldDefaulting = e;
    EmployeeCostEngine costEngine = this.CostEngine;
    string earningTypeId = e.Row.EarningTypeID;
    int? labourItemId = e.Row.LabourItemID;
    int? projectId = e.Row.ProjectID;
    int? projectTaskId = e.Row.ProjectTaskID;
    bool? certifiedJob = e.Row.CertifiedJob;
    string unionId = e.Row.UnionID;
    int? employeeId = this.Document.Current.EmployeeID;
    date1 = e.Row.Date;
    System.DateTime date2 = date1.Value;
    int? shiftId = e.Row.ShiftID;
    // ISSUE: variable of a boxed type
    __Boxed<Decimal?> overtimeMultiplier = (ValueType) (Decimal?) costEngine.CalculateEmployeeCost((string) null, earningTypeId, labourItemId, projectId, projectTaskId, certifiedJob, unionId, employeeId, date2, shiftId)?.OvertimeMultiplier;
    fieldDefaulting.NewValue = (object) overtimeMultiplier;
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<TimeCardMaint.EPTimecardDetail, TimeCardMaint.EPTimecardDetail.projectTaskID> e)
  {
    e.Cache.SetDefaultExt<TimeCardMaint.EPTimecardDetail.costCodeID>((object) e.Row);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<TimeCardMaint.EPTimecardDetail, TimeCardMaint.EPTimecardDetail.labourItemID> e)
  {
    e.Cache.SetDefaultExt<TimeCardMaint.EPTimecardDetail.costCodeID>((object) e.Row);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<TimeCardMaint.EPTimecardDetail, TimeCardMaint.EPTimecardDetail.costCodeID> e)
  {
    e.Cache.SetDefaultExt<TimeCardMaint.EPTimecardDetail.workCodeID>((object) e.Row);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<TimeCardMaint.EPTimecardDetail, TimeCardMaint.EPTimecardDetail.projectID> e)
  {
    e.Cache.SetDefaultExt<TimeCardMaint.EPTimecardDetail.unionID>((object) e.Row);
    e.Cache.SetDefaultExt<PMTimeActivity.certifiedJob>((object) e.Row);
    e.Cache.SetDefaultExt<TimeCardMaint.EPTimecardDetail.labourItemID>((object) e.Row);
    e.Cache.SetDefaultExt<PMTimeActivity.employeeRate>((object) e.Row);
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<EPTimeCard, EPTimeCard.weekId> e)
  {
    EPTimeCard row = e.Row;
    if (row != null && e.NewValue != null && string.IsNullOrEmpty(row.OrigTimeCardCD) && this.HasDuplicate(row.EmployeeID, new int?((int) e.NewValue), row.TimeCardCD))
      throw new PXSetPropertyException((IBqlTable) row, PXMessages.LocalizeFormatNoPrefix("A time card for the {0} employee and {1} week already exists.", (object) row.EmployeeID, (object) (int) e.NewValue));
  }

  public virtual void ProcessRegularTimecard(RegisterEntry releaseGraph, EPTimeCard timecard)
  {
    PXCache cache1 = releaseGraph.Document.Cache;
    PXCache cache2 = releaseGraph.Transactions.Cache;
    PMRegister pmRegister = (PMRegister) cache1.Insert();
    pmRegister.OrigDocType = "TC";
    pmRegister.OrigNoteID = timecard.NoteID;
    releaseGraph.FieldVerifying.AddHandler<PMTran.inventoryID>((PXFieldVerifying) ((sender, e) => e.Cancel = true));
    EPEmployee epEmployee = EPEmployee.PK.Find((PXGraph) releaseGraph, timecard.EmployeeID);
    if (epEmployee != null)
      pmRegister.Description = $"{epEmployee.AcctName} - {timecard.WeekID}";
    List<TimeCardMaint.ReleasedActivity> releasedActivityList = new List<TimeCardMaint.ReleasedActivity>();
    foreach (PXResult<TimeCardMaint.EPTimecardDetail> pxResult in this.Activities.Select())
    {
      TimeCardMaint.EPTimecardDetail epTimecardDetail = (TimeCardMaint.EPTimecardDetail) pxResult;
      if (!epTimecardDetail.Released.GetValueOrDefault())
      {
        PMActiveLaborItemAttribute.VerifyLaborItem<TimeCardMaint.EPTimecardDetail.labourItemID>(this.Activities.Cache, (object) epTimecardDetail);
        bool isBilled = false;
        int? nullable = !(epTimecardDetail.ApprovalStatus == "PA") ? epTimecardDetail.ContractID : throw new PXException("The time card includes one or multiple time activities that require approval by the project manager. The time card can be released after the time activities are approved.");
        if (nullable.HasValue)
        {
          RegisterEntry graph = releaseGraph;
          int? contractId = epTimecardDetail.ContractID;
          TimeCardMaint.EPTimecardDetail timeActivity = epTimecardDetail;
          nullable = epTimecardDetail.TimeBillable;
          int valueOrDefault = nullable.GetValueOrDefault();
          if (EmployeeActivitiesRelease.CreateContractUsage(graph, contractId, (PMTimeActivity) timeActivity, valueOrDefault) != null)
            isBilled = true;
        }
        else if (epTimecardDetail.RefNoteID.HasValue)
        {
          if (PXSelectBase<CRCase, PXSelectJoin<CRCase, InnerJoin<CRActivityLink, On<CRActivityLink.refNoteID, Equal<CRCase.noteID>>>, Where<CRActivityLink.noteID, Equal<Required<TimeCardMaint.EPTimecardDetail.refNoteID>>>>.Config>.Select((PXGraph) releaseGraph, (object) epTimecardDetail.RefNoteID).Count == 1)
          {
            RegisterEntry registerEntry = releaseGraph;
            TimeCardMaint.EPTimecardDetail timeActivity = epTimecardDetail;
            nullable = epTimecardDetail.TimeBillable;
            int valueOrDefault = nullable.GetValueOrDefault();
            if (registerEntry.CreateContractUsage((PMTimeActivity) timeActivity, valueOrDefault) != null)
              isBilled = true;
          }
        }
        System.DateTime reportedDate = this.GetReportedDate((PMTimeActivity) epTimecardDetail);
        bool postPmTransaction = EPSetupMaint.GetPostPMTransaction((PXGraph) this, this.EpSetup.Current, timecard.EmployeeID);
        EmployeeCostEngine.LaborCost employeeCost = this.CostEngine.CalculateEmployeeCost(epTimecardDetail.TimeCardCD, epTimecardDetail.EarningTypeID, epTimecardDetail.LabourItemID, epTimecardDetail.ProjectID, epTimecardDetail.ProjectTaskID, epTimecardDetail.CertifiedJob, epTimecardDetail.UnionID, timecard.EmployeeID, reportedDate, epTimecardDetail.ShiftID);
        if (((employeeCost != null ? 0 : (PXAccess.FeatureInstalled<PX.Objects.CS.FeaturesSet.projectAccounting>() ? 1 : 0)) & (postPmTransaction ? 1 : 0)) != 0 && ProjectDefaultAttribute.IsProject((PXGraph) this, epTimecardDetail.ProjectID))
          throw new PXException("The Employee Labor Cost Rate has not been found.");
        if (employeeCost != null)
          epTimecardDetail.EmployeeRate = employeeCost.Rate;
        if (postPmTransaction)
          releaseGraph.CreateTransaction(new RegisterEntry.CreatePMTran((PMTimeActivity) epTimecardDetail, timecard.EmployeeID, epTimecardDetail.Date.Value, epTimecardDetail.TimeSpent, epTimecardDetail.TimeBillable, (Decimal?) employeeCost?.Rate, (Decimal?) employeeCost?.OvertimeMultiplier, employeeCost?.CuryID, true));
        releasedActivityList.Add(new TimeCardMaint.ReleasedActivity(epTimecardDetail, epTimecardDetail.EmployeeRate.GetValueOrDefault(), isBilled));
      }
    }
    PXWeekSelector2Attribute.WeekInfo weekInfo = PXWeekSelector2Attribute.GetWeekInfo((PXGraph) this, timecard.WeekID.Value);
    PX.Data.PXView view = this.Items.View;
    object[] currents = new object[1]{ (object) timecard };
    object[] objArray = Array.Empty<object>();
    foreach (EPTimeCardItem record in view.SelectMultiBound(currents, objArray))
    {
      if (record.Sun.GetValueOrDefault() != 0M)
        this.CreateItemTransaction(record, timecard.EmployeeID, weekInfo.Sun.Date, record.Sun, cache2);
      if (record.Mon.GetValueOrDefault() != 0M)
        this.CreateItemTransaction(record, timecard.EmployeeID, weekInfo.Mon.Date, record.Mon, cache2);
      if (record.Tue.GetValueOrDefault() != 0M)
        this.CreateItemTransaction(record, timecard.EmployeeID, weekInfo.Tue.Date, record.Tue, cache2);
      if (record.Wed.GetValueOrDefault() != 0M)
        this.CreateItemTransaction(record, timecard.EmployeeID, weekInfo.Wed.Date, record.Wed, cache2);
      if (record.Thu.GetValueOrDefault() != 0M)
        this.CreateItemTransaction(record, timecard.EmployeeID, weekInfo.Thu.Date, record.Thu, cache2);
      if (record.Fri.GetValueOrDefault() != 0M)
        this.CreateItemTransaction(record, timecard.EmployeeID, weekInfo.Fri.Date, record.Fri, cache2);
      if (record.Sat.GetValueOrDefault() != 0M)
        this.CreateItemTransaction(record, timecard.EmployeeID, weekInfo.Sat.Date, record.Sat, cache2);
    }
    foreach (TimeCardMaint.ReleasedActivity releasedActivity in releasedActivityList)
    {
      this.Activities.Cache.SetValueExt<PMTimeActivity.released>((object) releasedActivity.Activity, (object) true);
      this.Activities.Cache.SetValue<PMTimeActivity.employeeRate>((object) releasedActivity.Activity, (object) releasedActivity.Cost);
      if (releasedActivity.IsBilled)
        this.Activities.Cache.SetValue<PMTimeActivity.billed>((object) releasedActivity.Activity, (object) true);
      this.Activities.Cache.SetStatus((object) releasedActivity.Activity, PXEntryStatus.Updated);
    }
  }

  protected System.DateTime GetReportedDate(PMTimeActivity activity)
  {
    System.DateTime reportedDate = activity.Date.Value;
    PXTimeZoneInfo timeZone = LocaleInfo.GetTimeZone();
    if (timeZone != null && !string.IsNullOrEmpty(activity.ReportedInTimeZoneID) && timeZone.Id != activity.ReportedInTimeZoneID)
      reportedDate = PXTimeZoneInfo.ConvertTimeFromUtc(PXTimeZoneInfo.ConvertTimeToUtc(activity.Date.Value, timeZone), PXTimeZoneInfo.FindSystemTimeZoneById(activity.ReportedInTimeZoneID));
    return reportedDate;
  }

  public virtual void ProcessCorrectingTimecard(RegisterEntry releaseGraph, EPTimeCard timecard)
  {
    PXCache cache1 = releaseGraph.Document.Cache;
    PXCache cache2 = releaseGraph.Transactions.Cache;
    PMRegister pmRegister = (PMRegister) cache1.Insert();
    pmRegister.OrigDocType = "TC";
    pmRegister.OrigNoteID = timecard.NoteID;
    releaseGraph.FieldVerifying.AddHandler<PMTran.inventoryID>((PXFieldVerifying) ((sender, e) => e.Cancel = true));
    EPEmployee epEmployee = EPEmployee.PK.Find((PXGraph) releaseGraph, timecard.EmployeeID);
    if (epEmployee != null)
      pmRegister.Description = PXMessages.LocalizeFormatNoPrefixNLA("{0} - {1} correction", (object) epEmployee.AcctName, (object) timecard.WeekID);
    List<TimeCardMaint.ReleasedActivity> releasedActivityList = new List<TimeCardMaint.ReleasedActivity>();
    bool postPmTransaction = EPSetupMaint.GetPostPMTransaction((PXGraph) this, this.EpSetup.Current, timecard.EmployeeID);
    foreach (PXResult<TimeCardMaint.EPTimecardDetailOrig, CREmployee, TimeCardMaint.EPTimecardDetailEx> pxResult in new PXSelectJoin<TimeCardMaint.EPTimecardDetailOrig, InnerJoin<CREmployee, On<CREmployee.defContactID, Equal<PMTimeActivity.ownerID>>, LeftJoin<TimeCardMaint.EPTimecardDetailEx, On<TimeCardMaint.EPTimecardDetailOrig.noteID, Equal<TimeCardMaint.EPTimecardDetailEx.origNoteID>>>>, Where<CREmployee.bAccountID, Equal<Required<EPTimeCard.employeeID>>, And<TimeCardMaint.EPTimecardDetail.weekID, Equal<Required<EPTimeCard.weekId>>, And<PMTimeActivity.timeSheetCD, PX.Data.IsNull, And<TimeCardMaint.EPTimecardDetailEx.noteID, PX.Data.IsNull, And<PMTimeActivity.trackTime, Equal<True>, PX.Data.And<Where<TimeCardMaint.EPTimecardDetail.timeCardCD, PX.Data.IsNull, Or<TimeCardMaint.EPTimecardDetail.timeCardCD, Equal<Required<EPTimeCard.timeCardCD>>>>>>>>>>, PX.Data.OrderBy<Asc<TimeCardMaint.EPTimecardDetail.date>>>((PXGraph) this).Select((object) timecard.EmployeeID, (object) timecard.WeekID, (object) timecard.OrigTimeCardCD))
    {
      TimeCardMaint.EPTimecardDetailOrig timecardDetailOrig = (TimeCardMaint.EPTimecardDetailOrig) pxResult;
      bool isBilled = false;
      EmployeeCostEngine costEngine = this.CostEngine;
      string timeCardCd = timecardDetailOrig.TimeCardCD;
      string earningTypeId = timecardDetailOrig.EarningTypeID;
      int? labourItemId = timecardDetailOrig.LabourItemID;
      int? projectId = timecardDetailOrig.ProjectID;
      int? projectTaskId = timecardDetailOrig.ProjectTaskID;
      bool? certifiedJob = timecardDetailOrig.CertifiedJob;
      string unionId = timecardDetailOrig.UnionID;
      int? employeeId1 = timecard.EmployeeID;
      System.DateTime? date1 = timecardDetailOrig.Date;
      System.DateTime date2 = date1.Value;
      int? shiftId = timecardDetailOrig.ShiftID;
      EmployeeCostEngine.LaborCost employeeCost = costEngine.CalculateEmployeeCost(timeCardCd, earningTypeId, labourItemId, projectId, projectTaskId, certifiedJob, unionId, employeeId1, date2, shiftId);
      if (((employeeCost != null ? 0 : (PXAccess.FeatureInstalled<PX.Objects.CS.FeaturesSet.projectAccounting>() ? 1 : 0)) & (postPmTransaction ? 1 : 0)) != 0 && ProjectDefaultAttribute.IsProject((PXGraph) this, timecardDetailOrig.ProjectID))
        throw new PXException("The Employee Labor Cost Rate has not been found.");
      Decimal? nullable1 = timecardDetailOrig.EmployeeRate;
      Decimal? nullable2 = (Decimal?) (nullable1 ?? employeeCost?.Rate);
      int? nullable3;
      if (postPmTransaction)
      {
        RegisterEntry registerEntry = releaseGraph;
        TimeCardMaint.EPTimecardDetailOrig timeActivity = timecardDetailOrig;
        int? employeeId2 = timecard.EmployeeID;
        date1 = timecardDetailOrig.Date;
        System.DateTime date3 = date1.Value;
        nullable3 = timecardDetailOrig.TimeSpent;
        int? timeSpent = nullable3.HasValue ? new int?(-nullable3.GetValueOrDefault()) : new int?();
        nullable3 = timecardDetailOrig.TimeBillable;
        int? timeBillable = nullable3.HasValue ? new int?(-nullable3.GetValueOrDefault()) : new int?();
        Decimal? cost = nullable2;
        Decimal? overtimeMult;
        if (employeeCost == null)
        {
          nullable1 = new Decimal?();
          overtimeMult = nullable1;
        }
        else
          overtimeMult = employeeCost.OvertimeMultiplier;
        string curyId = employeeCost?.CuryID;
        RegisterEntry.CreatePMTran createPMTran = new RegisterEntry.CreatePMTran((PMTimeActivity) timeActivity, employeeId2, date3, timeSpent, timeBillable, cost, overtimeMult, curyId, true);
        registerEntry.CreateTransaction(createPMTran);
      }
      this.Activities.Cache.SetValueExt<PMTimeActivity.released>((object) timecardDetailOrig, (object) true);
      this.Activities.Cache.SetValue<PMTimeActivity.employeeRate>((object) timecardDetailOrig, (object) PXDBPriceCostAttribute.Round(nullable2.Value));
      this.Activities.Cache.SetStatus((object) timecardDetailOrig, PXEntryStatus.Updated);
      if (timecardDetailOrig.RefNoteID.HasValue)
      {
        CRCase crCase = (CRCase) PXSelectBase<CRCase, PXSelectJoin<CRCase, InnerJoin<CRActivityLink, On<CRActivityLink.refNoteID, Equal<CRCase.noteID>>>, Where<CRActivityLink.noteID, Equal<Required<TimeCardMaint.EPTimecardDetailOrig.refNoteID>>>>.Config>.Select((PXGraph) this, (object) timecardDetailOrig.RefNoteID);
        if (crCase != null)
        {
          nullable3 = crCase.ContractID;
          if (nullable3.HasValue)
          {
            PMTran pmTran = (PMTran) null;
            if (timecardDetailOrig.IsBillable.GetValueOrDefault())
            {
              RegisterEntry registerEntry = releaseGraph;
              TimeCardMaint.EPTimecardDetailOrig timeActivity = timecardDetailOrig;
              nullable3 = timecardDetailOrig.TimeBillable;
              int billableMinutes = -nullable3.GetValueOrDefault();
              pmTran = registerEntry.CreateContractUsage((PMTimeActivity) timeActivity, billableMinutes);
            }
            if (pmTran != null)
              isBilled = true;
          }
        }
      }
      releasedActivityList.Add(new TimeCardMaint.ReleasedActivity((TimeCardMaint.EPTimecardDetail) timecardDetailOrig, nullable2.GetValueOrDefault(), isBilled));
    }
    foreach (PXResult<TimeCardMaint.EPTimecardDetail, CREmployee, TimeCardMaint.EPTimecardDetailOrig> pxResult in new PXSelectJoin<TimeCardMaint.EPTimecardDetail, InnerJoin<CREmployee, On<CREmployee.defContactID, Equal<PMTimeActivity.ownerID>>, LeftJoin<TimeCardMaint.EPTimecardDetailOrig, On<TimeCardMaint.EPTimecardDetailOrig.noteID, Equal<PMTimeActivity.origNoteID>>>>, Where<CREmployee.bAccountID, Equal<Required<EPTimeCard.employeeID>>, And<TimeCardMaint.EPTimecardDetail.weekID, Equal<Required<EPTimeCard.weekId>>, And<PMTimeActivity.timeSheetCD, PX.Data.IsNull, And<PMTimeActivity.trackTime, Equal<True>, PX.Data.And<Where<TimeCardMaint.EPTimecardDetail.timeCardCD, PX.Data.IsNull, Or<TimeCardMaint.EPTimecardDetail.timeCardCD, Equal<Required<EPTimeCard.timeCardCD>>>>>>>>>, PX.Data.OrderBy<Asc<TimeCardMaint.EPTimecardDetail.date>>>((PXGraph) this).Select((object) timecard.EmployeeID, (object) timecard.WeekID, (object) timecard.TimeCardCD))
    {
      TimeCardMaint.EPTimecardDetail epTimecardDetail = (TimeCardMaint.EPTimecardDetail) pxResult;
      if (!epTimecardDetail.Released.GetValueOrDefault())
      {
        bool isBilled = false;
        TimeCardMaint.EPTimecardDetailOrig timecardDetailOrig = (TimeCardMaint.EPTimecardDetailOrig) pxResult;
        this.Activities.Cache.RaiseRowSelected((object) epTimecardDetail);
        Decimal? nullable4 = new Decimal?();
        EmployeeCostEngine.LaborCost employeeCost = this.CostEngine.CalculateEmployeeCost(epTimecardDetail.TimeCardCD, epTimecardDetail.EarningTypeID, epTimecardDetail.LabourItemID, epTimecardDetail.ProjectID, epTimecardDetail.ProjectTaskID, epTimecardDetail.CertifiedJob, epTimecardDetail.UnionID, timecard.EmployeeID, epTimecardDetail.Date.Value, epTimecardDetail.ShiftID);
        if (((employeeCost != null ? 0 : (PXAccess.FeatureInstalled<PX.Objects.CS.FeaturesSet.projectAccounting>() ? 1 : 0)) & (postPmTransaction ? 1 : 0)) != 0 && ProjectDefaultAttribute.IsProject((PXGraph) this, epTimecardDetail.ProjectID))
          throw new PXException("The Employee Labor Cost Rate has not been found.");
        Decimal? nullable5 = (Decimal?) employeeCost?.Rate;
        Decimal? cost1 = (Decimal?) (nullable5 ?? timecardDetailOrig?.EmployeeRate);
        Guid? nullable6 = timecardDetailOrig.NoteID;
        if (nullable6.HasValue)
        {
          nullable5 = timecardDetailOrig.EmployeeRate;
          nullable4 = (Decimal?) (nullable5 ?? this.CostEngine.CalculateEmployeeCost(timecardDetailOrig.TimeCardCD, timecardDetailOrig.EarningTypeID, timecardDetailOrig.LabourItemID, timecardDetailOrig.ProjectID, timecardDetailOrig.ProjectTaskID, timecardDetailOrig.CertifiedJob, timecardDetailOrig.UnionID, timecard.EmployeeID, timecardDetailOrig.Date.Value, timecardDetailOrig.ShiftID)?.Rate);
        }
        int? timeSpent1 = epTimecardDetail.TimeSpent;
        nullable6 = timecardDetailOrig.NoteID;
        int? nullable7;
        if (nullable6.HasValue)
        {
          ref int? local = ref timeSpent1;
          nullable7 = epTimecardDetail.TimeSpent;
          int valueOrDefault1 = nullable7.GetValueOrDefault();
          nullable7 = timecardDetailOrig.TimeSpent;
          int valueOrDefault2 = nullable7.GetValueOrDefault();
          int num = valueOrDefault1 - valueOrDefault2;
          local = new int?(num);
        }
        int? timeBillable1 = epTimecardDetail.TimeBillable;
        nullable6 = timecardDetailOrig.NoteID;
        if (nullable6.HasValue)
        {
          ref int? local = ref timeBillable1;
          nullable7 = epTimecardDetail.TimeBillable;
          int valueOrDefault3 = nullable7.GetValueOrDefault();
          nullable7 = timecardDetailOrig.TimeBillable;
          int valueOrDefault4 = nullable7.GetValueOrDefault();
          int num = valueOrDefault3 - valueOrDefault4;
          local = new int?(num);
        }
        nullable6 = epTimecardDetail.RefNoteID;
        bool? isBillable1;
        if (nullable6.HasValue)
        {
          CRCase crCase = (CRCase) PXSelectBase<CRCase, PXSelectJoin<CRCase, InnerJoin<CRActivityLink, On<CRActivityLink.refNoteID, Equal<CRCase.noteID>>>, Where<CRActivityLink.noteID, Equal<Required<TimeCardMaint.EPTimecardDetail.refNoteID>>>>.Config>.Select((PXGraph) this, (object) epTimecardDetail.RefNoteID);
          if (crCase != null)
          {
            nullable7 = crCase.ContractID;
            if (nullable7.HasValue)
            {
              bool? isBillable2 = epTimecardDetail.IsBillable;
              isBillable1 = timecardDetailOrig.IsBillable;
              PMTran pmTran;
              if (isBillable2.GetValueOrDefault() == isBillable1.GetValueOrDefault() & isBillable2.HasValue == isBillable1.HasValue)
              {
                pmTran = releaseGraph.CreateContractUsage((PMTimeActivity) epTimecardDetail, timeBillable1.GetValueOrDefault());
              }
              else
              {
                isBillable1 = epTimecardDetail.IsBillable;
                pmTran = !isBillable1.GetValueOrDefault() ? releaseGraph.CreateContractUsage((PMTimeActivity) epTimecardDetail, -timeBillable1.GetValueOrDefault()) : releaseGraph.CreateContractUsage((PMTimeActivity) epTimecardDetail, timeBillable1.GetValueOrDefault());
              }
              if (pmTran != null)
                isBilled = false;
            }
          }
        }
        nullable6 = timecardDetailOrig.NoteID;
        if (nullable6.HasValue)
        {
          nullable7 = epTimecardDetail.ProjectID;
          int? nullable8 = timecardDetailOrig.ProjectID;
          if (nullable7.GetValueOrDefault() == nullable8.GetValueOrDefault() & nullable7.HasValue == nullable8.HasValue)
          {
            nullable8 = epTimecardDetail.ProjectTaskID;
            nullable7 = timecardDetailOrig.ProjectTaskID;
            if (nullable8.GetValueOrDefault() == nullable7.GetValueOrDefault() & nullable8.HasValue == nullable7.HasValue)
            {
              nullable7 = epTimecardDetail.CostCodeID;
              nullable8 = timecardDetailOrig.CostCodeID;
              if (nullable7.GetValueOrDefault() == nullable8.GetValueOrDefault() & nullable7.HasValue == nullable8.HasValue && epTimecardDetail.UnionID == timecardDetailOrig.UnionID)
              {
                nullable8 = epTimecardDetail.LabourItemID;
                nullable7 = timecardDetailOrig.LabourItemID;
                if (nullable8.GetValueOrDefault() == nullable7.GetValueOrDefault() & nullable8.HasValue == nullable7.HasValue && epTimecardDetail.WorkCodeID == timecardDetailOrig.WorkCodeID)
                {
                  nullable7 = epTimecardDetail.ShiftID;
                  nullable8 = timecardDetailOrig.ShiftID;
                  if (nullable7.GetValueOrDefault() == nullable8.GetValueOrDefault() & nullable7.HasValue == nullable8.HasValue)
                  {
                    nullable5 = nullable4;
                    Decimal? nullable9 = cost1;
                    if (nullable5.GetValueOrDefault() == nullable9.GetValueOrDefault() & nullable5.HasValue == nullable9.HasValue)
                    {
                      isBillable1 = epTimecardDetail.IsBillable;
                      bool? isBillable3 = timecardDetailOrig.IsBillable;
                      if (isBillable1.GetValueOrDefault() == isBillable3.GetValueOrDefault() & isBillable1.HasValue == isBillable3.HasValue)
                        goto label_50;
                    }
                  }
                }
              }
            }
          }
          if (postPmTransaction)
          {
            RegisterEntry registerEntry = releaseGraph;
            TimeCardMaint.EPTimecardDetailOrig timeActivity = timecardDetailOrig;
            int? employeeId = timecard.EmployeeID;
            System.DateTime date = timecardDetailOrig.Date.Value;
            nullable8 = timecardDetailOrig.TimeSpent;
            int? timeSpent2;
            if (!nullable8.HasValue)
            {
              nullable7 = new int?();
              timeSpent2 = nullable7;
            }
            else
              timeSpent2 = new int?(-nullable8.GetValueOrDefault());
            nullable8 = timecardDetailOrig.TimeBillable;
            int? timeBillable2;
            if (!nullable8.HasValue)
            {
              nullable7 = new int?();
              timeBillable2 = nullable7;
            }
            else
              timeBillable2 = new int?(-nullable8.GetValueOrDefault());
            Decimal? cost2 = nullable4;
            Decimal? overtimeMultiplier = employeeCost?.OvertimeMultiplier;
            string curyId = employeeCost?.CuryID;
            RegisterEntry.CreatePMTran createPMTran = new RegisterEntry.CreatePMTran((PMTimeActivity) timeActivity, employeeId, date, timeSpent2, timeBillable2, cost2, overtimeMultiplier, curyId, true);
            registerEntry.CreateTransaction(createPMTran);
          }
          if (postPmTransaction)
          {
            releaseGraph.CreateTransaction(new RegisterEntry.CreatePMTran((PMTimeActivity) epTimecardDetail, timecard.EmployeeID, epTimecardDetail.Date.Value, epTimecardDetail.TimeSpent, epTimecardDetail.TimeBillable, cost1, (Decimal?) employeeCost?.OvertimeMultiplier, employeeCost?.CuryID, true));
            goto label_62;
          }
          goto label_62;
        }
label_50:
        if (postPmTransaction)
          releaseGraph.CreateTransaction(new RegisterEntry.CreatePMTran((PMTimeActivity) epTimecardDetail, timecard.EmployeeID, epTimecardDetail.Date.Value, timeSpent1, timeBillable1, cost1, (Decimal?) employeeCost?.OvertimeMultiplier, employeeCost?.CuryID, true));
label_62:
        releasedActivityList.Add(new TimeCardMaint.ReleasedActivity(epTimecardDetail, PXDBPriceCostAttribute.Round(cost1.Value), isBilled));
      }
    }
    PXWeekSelector2Attribute.WeekInfo weekInfo = PXWeekSelector2Attribute.GetWeekInfo((PXGraph) this, timecard.WeekID.Value);
    PX.Data.PXView view1 = new PXSelectJoin<TimeCardMaint.EPTimeCardItemOrig, LeftJoin<TimeCardMaint.EPTimeCardItemEx, On<TimeCardMaint.EPTimeCardItemEx.timeCardCD, Equal<Current<EPTimeCard.timeCardCD>>, And<TimeCardMaint.EPTimeCardItemEx.origLineNbr, Equal<TimeCardMaint.EPTimeCardItemOrig.lineNbr>>>>, Where<TimeCardMaint.EPTimeCardItemOrig.timeCardCD, Equal<Current<EPTimeCard.origTimeCardCD>>, And<TimeCardMaint.EPTimeCardItemEx.timeCardCD, PX.Data.IsNull>>>((PXGraph) this).View;
    object[] currents1 = new object[1]{ (object) timecard };
    object[] objArray1 = Array.Empty<object>();
    foreach (PXResult<TimeCardMaint.EPTimeCardItemOrig, TimeCardMaint.EPTimeCardItemEx> pxResult in view1.SelectMultiBound(currents1, objArray1))
    {
      TimeCardMaint.EPTimeCardItemOrig timeCardItemOrig = (TimeCardMaint.EPTimeCardItemOrig) pxResult;
      Decimal? nullable = timeCardItemOrig.Sun;
      if (nullable.GetValueOrDefault() > 0M)
      {
        TimeCardMaint.EPTimeCardItemOrig record = timeCardItemOrig;
        int? employeeId = timecard.EmployeeID;
        System.DateTime? date = weekInfo.Sun.Date;
        nullable = timeCardItemOrig.Sun;
        Decimal? qty = nullable.HasValue ? new Decimal?(-nullable.GetValueOrDefault()) : new Decimal?();
        PXCache tranCache = cache2;
        this.CreateItemTransaction((EPTimeCardItem) record, employeeId, date, qty, tranCache);
      }
      nullable = timeCardItemOrig.Mon;
      if (nullable.GetValueOrDefault() > 0M)
      {
        TimeCardMaint.EPTimeCardItemOrig record = timeCardItemOrig;
        int? employeeId = timecard.EmployeeID;
        System.DateTime? date = weekInfo.Mon.Date;
        nullable = timeCardItemOrig.Mon;
        Decimal? qty = nullable.HasValue ? new Decimal?(-nullable.GetValueOrDefault()) : new Decimal?();
        PXCache tranCache = cache2;
        this.CreateItemTransaction((EPTimeCardItem) record, employeeId, date, qty, tranCache);
      }
      nullable = timeCardItemOrig.Tue;
      if (nullable.GetValueOrDefault() > 0M)
      {
        TimeCardMaint.EPTimeCardItemOrig record = timeCardItemOrig;
        int? employeeId = timecard.EmployeeID;
        System.DateTime? date = weekInfo.Tue.Date;
        nullable = timeCardItemOrig.Tue;
        Decimal? qty = nullable.HasValue ? new Decimal?(-nullable.GetValueOrDefault()) : new Decimal?();
        PXCache tranCache = cache2;
        this.CreateItemTransaction((EPTimeCardItem) record, employeeId, date, qty, tranCache);
      }
      nullable = timeCardItemOrig.Wed;
      if (nullable.GetValueOrDefault() > 0M)
      {
        TimeCardMaint.EPTimeCardItemOrig record = timeCardItemOrig;
        int? employeeId = timecard.EmployeeID;
        System.DateTime? date = weekInfo.Wed.Date;
        nullable = timeCardItemOrig.Wed;
        Decimal? qty = nullable.HasValue ? new Decimal?(-nullable.GetValueOrDefault()) : new Decimal?();
        PXCache tranCache = cache2;
        this.CreateItemTransaction((EPTimeCardItem) record, employeeId, date, qty, tranCache);
      }
      nullable = timeCardItemOrig.Thu;
      if (nullable.GetValueOrDefault() > 0M)
      {
        TimeCardMaint.EPTimeCardItemOrig record = timeCardItemOrig;
        int? employeeId = timecard.EmployeeID;
        System.DateTime? date = weekInfo.Thu.Date;
        nullable = timeCardItemOrig.Thu;
        Decimal? qty = nullable.HasValue ? new Decimal?(-nullable.GetValueOrDefault()) : new Decimal?();
        PXCache tranCache = cache2;
        this.CreateItemTransaction((EPTimeCardItem) record, employeeId, date, qty, tranCache);
      }
      nullable = timeCardItemOrig.Fri;
      if (nullable.GetValueOrDefault() > 0M)
      {
        TimeCardMaint.EPTimeCardItemOrig record = timeCardItemOrig;
        int? employeeId = timecard.EmployeeID;
        System.DateTime? date = weekInfo.Fri.Date;
        nullable = timeCardItemOrig.Fri;
        Decimal? qty = nullable.HasValue ? new Decimal?(-nullable.GetValueOrDefault()) : new Decimal?();
        PXCache tranCache = cache2;
        this.CreateItemTransaction((EPTimeCardItem) record, employeeId, date, qty, tranCache);
      }
      nullable = timeCardItemOrig.Sat;
      if (nullable.GetValueOrDefault() > 0M)
      {
        TimeCardMaint.EPTimeCardItemOrig record = timeCardItemOrig;
        int? employeeId = timecard.EmployeeID;
        System.DateTime? date = weekInfo.Sat.Date;
        nullable = timeCardItemOrig.Sat;
        Decimal? qty = nullable.HasValue ? new Decimal?(-nullable.GetValueOrDefault()) : new Decimal?();
        PXCache tranCache = cache2;
        this.CreateItemTransaction((EPTimeCardItem) record, employeeId, date, qty, tranCache);
      }
    }
    PX.Data.PXView view2 = new PXSelect<EPTimeCardItem, Where<EPTimeCardItem.timeCardCD, Equal<Current<EPTimeCard.timeCardCD>>, And<EPTimeCardItem.origLineNbr, PX.Data.IsNull>>>((PXGraph) this).View;
    object[] currents2 = new object[1]{ (object) timecard };
    object[] objArray2 = Array.Empty<object>();
    foreach (EPTimeCardItem record in view2.SelectMultiBound(currents2, objArray2))
    {
      if (record.Sun.GetValueOrDefault() != 0M)
        this.CreateItemTransaction(record, timecard.EmployeeID, weekInfo.Sun.Date, record.Sun, cache2);
      if (record.Mon.GetValueOrDefault() != 0M)
        this.CreateItemTransaction(record, timecard.EmployeeID, weekInfo.Mon.Date, record.Mon, cache2);
      if (record.Tue.GetValueOrDefault() != 0M)
        this.CreateItemTransaction(record, timecard.EmployeeID, weekInfo.Tue.Date, record.Tue, cache2);
      if (record.Wed.GetValueOrDefault() != 0M)
        this.CreateItemTransaction(record, timecard.EmployeeID, weekInfo.Wed.Date, record.Wed, cache2);
      if (record.Thu.GetValueOrDefault() != 0M)
        this.CreateItemTransaction(record, timecard.EmployeeID, weekInfo.Thu.Date, record.Thu, cache2);
      if (record.Fri.GetValueOrDefault() != 0M)
        this.CreateItemTransaction(record, timecard.EmployeeID, weekInfo.Fri.Date, record.Fri, cache2);
      if (record.Sat.GetValueOrDefault() != 0M)
        this.CreateItemTransaction(record, timecard.EmployeeID, weekInfo.Sat.Date, record.Sat, cache2);
    }
    PX.Data.PXView view3 = new PXSelectJoin<TimeCardMaint.EPTimeCardItemOrig, LeftJoin<TimeCardMaint.EPTimeCardItemEx, On<TimeCardMaint.EPTimeCardItemEx.timeCardCD, Equal<Current<EPTimeCard.timeCardCD>>, And<TimeCardMaint.EPTimeCardItemEx.origLineNbr, Equal<TimeCardMaint.EPTimeCardItemOrig.lineNbr>>>>, Where<TimeCardMaint.EPTimeCardItemOrig.timeCardCD, Equal<Current<EPTimeCard.origTimeCardCD>>, And<TimeCardMaint.EPTimeCardItemEx.timeCardCD, PX.Data.IsNotNull>>>((PXGraph) this).View;
    object[] currents3 = new object[1]{ (object) timecard };
    object[] objArray3 = Array.Empty<object>();
    foreach (PXResult<TimeCardMaint.EPTimeCardItemOrig, TimeCardMaint.EPTimeCardItemEx> pxResult in view3.SelectMultiBound(currents3, objArray3))
    {
      TimeCardMaint.EPTimeCardItemOrig record1 = (TimeCardMaint.EPTimeCardItemOrig) pxResult;
      TimeCardMaint.EPTimeCardItemEx record2 = (TimeCardMaint.EPTimeCardItemEx) pxResult;
      int? projectId1 = record1.ProjectID;
      int? nullable = record2.ProjectID;
      if (projectId1.GetValueOrDefault() == nullable.GetValueOrDefault() & projectId1.HasValue == nullable.HasValue)
      {
        nullable = record1.TaskID;
        int? taskId = record2.TaskID;
        if (nullable.GetValueOrDefault() == taskId.GetValueOrDefault() & nullable.HasValue == taskId.HasValue)
        {
          int? inventoryId = record1.InventoryID;
          nullable = record2.InventoryID;
          if (inventoryId.GetValueOrDefault() == nullable.GetValueOrDefault() & inventoryId.HasValue == nullable.HasValue)
          {
            nullable = record1.CostCodeID;
            int? costCodeId = record2.CostCodeID;
            if (nullable.GetValueOrDefault() == costCodeId.GetValueOrDefault() & nullable.HasValue == costCodeId.HasValue)
            {
              if (record2.Sun.GetValueOrDefault() - record1.Sun.GetValueOrDefault() != 0M)
              {
                this.CreateItemTransaction((EPTimeCardItem) record2, timecard.EmployeeID, weekInfo.Sun.Date, new Decimal?(record2.Sun.GetValueOrDefault() - record1.Sun.GetValueOrDefault()), cache2);
                goto label_115;
              }
              goto label_115;
            }
          }
        }
      }
      if (record1.Sun.GetValueOrDefault() != 0M)
        this.CreateItemTransaction((EPTimeCardItem) record1, timecard.EmployeeID, weekInfo.Sun.Date, new Decimal?(-record1.Sun.GetValueOrDefault()), cache2);
      if (record2.Sun.GetValueOrDefault() != 0M)
        this.CreateItemTransaction((EPTimeCardItem) record2, timecard.EmployeeID, weekInfo.Sun.Date, new Decimal?(record2.Sun.GetValueOrDefault()), cache2);
label_115:
      int? projectId2 = record1.ProjectID;
      nullable = record2.ProjectID;
      if (projectId2.GetValueOrDefault() == nullable.GetValueOrDefault() & projectId2.HasValue == nullable.HasValue)
      {
        nullable = record1.TaskID;
        int? taskId = record2.TaskID;
        if (nullable.GetValueOrDefault() == taskId.GetValueOrDefault() & nullable.HasValue == taskId.HasValue)
        {
          int? inventoryId = record1.InventoryID;
          nullable = record2.InventoryID;
          if (inventoryId.GetValueOrDefault() == nullable.GetValueOrDefault() & inventoryId.HasValue == nullable.HasValue)
          {
            nullable = record1.CostCodeID;
            int? costCodeId = record2.CostCodeID;
            if (nullable.GetValueOrDefault() == costCodeId.GetValueOrDefault() & nullable.HasValue == costCodeId.HasValue)
            {
              if (record2.Mon.GetValueOrDefault() - record1.Mon.GetValueOrDefault() != 0M)
              {
                this.CreateItemTransaction((EPTimeCardItem) record2, timecard.EmployeeID, weekInfo.Mon.Date, new Decimal?(record2.Mon.GetValueOrDefault() - record1.Mon.GetValueOrDefault()), cache2);
                goto label_125;
              }
              goto label_125;
            }
          }
        }
      }
      if (record1.Mon.GetValueOrDefault() != 0M)
        this.CreateItemTransaction((EPTimeCardItem) record1, timecard.EmployeeID, weekInfo.Mon.Date, new Decimal?(-record1.Mon.GetValueOrDefault()), cache2);
      if (record2.Mon.GetValueOrDefault() != 0M)
        this.CreateItemTransaction((EPTimeCardItem) record2, timecard.EmployeeID, weekInfo.Mon.Date, new Decimal?(record2.Mon.GetValueOrDefault()), cache2);
label_125:
      int? projectId3 = record1.ProjectID;
      nullable = record2.ProjectID;
      if (projectId3.GetValueOrDefault() == nullable.GetValueOrDefault() & projectId3.HasValue == nullable.HasValue)
      {
        nullable = record1.TaskID;
        int? taskId = record2.TaskID;
        if (nullable.GetValueOrDefault() == taskId.GetValueOrDefault() & nullable.HasValue == taskId.HasValue)
        {
          int? inventoryId = record1.InventoryID;
          nullable = record2.InventoryID;
          if (inventoryId.GetValueOrDefault() == nullable.GetValueOrDefault() & inventoryId.HasValue == nullable.HasValue)
          {
            nullable = record1.CostCodeID;
            int? costCodeId = record2.CostCodeID;
            if (nullable.GetValueOrDefault() == costCodeId.GetValueOrDefault() & nullable.HasValue == costCodeId.HasValue)
            {
              if (record2.Tue.GetValueOrDefault() - record1.Tue.GetValueOrDefault() != 0M)
              {
                this.CreateItemTransaction((EPTimeCardItem) record2, timecard.EmployeeID, weekInfo.Tue.Date, new Decimal?(record2.Tue.GetValueOrDefault() - record1.Tue.GetValueOrDefault()), cache2);
                goto label_135;
              }
              goto label_135;
            }
          }
        }
      }
      if (record1.Tue.GetValueOrDefault() != 0M)
        this.CreateItemTransaction((EPTimeCardItem) record1, timecard.EmployeeID, weekInfo.Tue.Date, new Decimal?(-record1.Tue.GetValueOrDefault()), cache2);
      if (record2.Tue.GetValueOrDefault() != 0M)
        this.CreateItemTransaction((EPTimeCardItem) record2, timecard.EmployeeID, weekInfo.Tue.Date, new Decimal?(record2.Tue.GetValueOrDefault()), cache2);
label_135:
      int? projectId4 = record1.ProjectID;
      nullable = record2.ProjectID;
      if (projectId4.GetValueOrDefault() == nullable.GetValueOrDefault() & projectId4.HasValue == nullable.HasValue)
      {
        nullable = record1.TaskID;
        int? taskId = record2.TaskID;
        if (nullable.GetValueOrDefault() == taskId.GetValueOrDefault() & nullable.HasValue == taskId.HasValue)
        {
          int? inventoryId = record1.InventoryID;
          nullable = record2.InventoryID;
          if (inventoryId.GetValueOrDefault() == nullable.GetValueOrDefault() & inventoryId.HasValue == nullable.HasValue)
          {
            nullable = record1.CostCodeID;
            int? costCodeId = record2.CostCodeID;
            if (nullable.GetValueOrDefault() == costCodeId.GetValueOrDefault() & nullable.HasValue == costCodeId.HasValue)
            {
              if (record2.Wed.GetValueOrDefault() - record1.Wed.GetValueOrDefault() != 0M)
              {
                this.CreateItemTransaction((EPTimeCardItem) record2, timecard.EmployeeID, weekInfo.Wed.Date, new Decimal?(record2.Wed.GetValueOrDefault() - record1.Wed.GetValueOrDefault()), cache2);
                goto label_145;
              }
              goto label_145;
            }
          }
        }
      }
      if (record1.Wed.GetValueOrDefault() != 0M)
        this.CreateItemTransaction((EPTimeCardItem) record1, timecard.EmployeeID, weekInfo.Wed.Date, new Decimal?(-record1.Wed.GetValueOrDefault()), cache2);
      if (record2.Wed.GetValueOrDefault() != 0M)
        this.CreateItemTransaction((EPTimeCardItem) record2, timecard.EmployeeID, weekInfo.Wed.Date, new Decimal?(record2.Wed.GetValueOrDefault()), cache2);
label_145:
      int? projectId5 = record1.ProjectID;
      nullable = record2.ProjectID;
      if (projectId5.GetValueOrDefault() == nullable.GetValueOrDefault() & projectId5.HasValue == nullable.HasValue)
      {
        nullable = record1.TaskID;
        int? taskId = record2.TaskID;
        if (nullable.GetValueOrDefault() == taskId.GetValueOrDefault() & nullable.HasValue == taskId.HasValue)
        {
          int? inventoryId = record1.InventoryID;
          nullable = record2.InventoryID;
          if (inventoryId.GetValueOrDefault() == nullable.GetValueOrDefault() & inventoryId.HasValue == nullable.HasValue)
          {
            nullable = record1.CostCodeID;
            int? costCodeId = record2.CostCodeID;
            if (nullable.GetValueOrDefault() == costCodeId.GetValueOrDefault() & nullable.HasValue == costCodeId.HasValue)
            {
              if (record2.Thu.GetValueOrDefault() - record1.Thu.GetValueOrDefault() != 0M)
              {
                this.CreateItemTransaction((EPTimeCardItem) record2, timecard.EmployeeID, weekInfo.Thu.Date, new Decimal?(record2.Thu.GetValueOrDefault() - record1.Thu.GetValueOrDefault()), cache2);
                goto label_155;
              }
              goto label_155;
            }
          }
        }
      }
      if (record1.Thu.GetValueOrDefault() != 0M)
        this.CreateItemTransaction((EPTimeCardItem) record1, timecard.EmployeeID, weekInfo.Thu.Date, new Decimal?(-record1.Thu.GetValueOrDefault()), cache2);
      if (record2.Thu.GetValueOrDefault() != 0M)
        this.CreateItemTransaction((EPTimeCardItem) record2, timecard.EmployeeID, weekInfo.Thu.Date, new Decimal?(record2.Thu.GetValueOrDefault()), cache2);
label_155:
      int? projectId6 = record1.ProjectID;
      nullable = record2.ProjectID;
      if (projectId6.GetValueOrDefault() == nullable.GetValueOrDefault() & projectId6.HasValue == nullable.HasValue)
      {
        nullable = record1.TaskID;
        int? taskId = record2.TaskID;
        if (nullable.GetValueOrDefault() == taskId.GetValueOrDefault() & nullable.HasValue == taskId.HasValue)
        {
          int? inventoryId = record1.InventoryID;
          nullable = record2.InventoryID;
          if (inventoryId.GetValueOrDefault() == nullable.GetValueOrDefault() & inventoryId.HasValue == nullable.HasValue)
          {
            nullable = record1.CostCodeID;
            int? costCodeId = record2.CostCodeID;
            if (nullable.GetValueOrDefault() == costCodeId.GetValueOrDefault() & nullable.HasValue == costCodeId.HasValue)
            {
              if (record2.Fri.GetValueOrDefault() - record1.Fri.GetValueOrDefault() != 0M)
              {
                this.CreateItemTransaction((EPTimeCardItem) record2, timecard.EmployeeID, weekInfo.Fri.Date, new Decimal?(record2.Fri.GetValueOrDefault() - record1.Fri.GetValueOrDefault()), cache2);
                goto label_165;
              }
              goto label_165;
            }
          }
        }
      }
      if (record1.Fri.GetValueOrDefault() != 0M)
        this.CreateItemTransaction((EPTimeCardItem) record1, timecard.EmployeeID, weekInfo.Fri.Date, new Decimal?(-record1.Fri.GetValueOrDefault()), cache2);
      if (record2.Fri.GetValueOrDefault() != 0M)
        this.CreateItemTransaction((EPTimeCardItem) record2, timecard.EmployeeID, weekInfo.Fri.Date, new Decimal?(record2.Fri.GetValueOrDefault()), cache2);
label_165:
      int? projectId7 = record1.ProjectID;
      nullable = record2.ProjectID;
      if (projectId7.GetValueOrDefault() == nullable.GetValueOrDefault() & projectId7.HasValue == nullable.HasValue)
      {
        nullable = record1.TaskID;
        int? taskId = record2.TaskID;
        if (nullable.GetValueOrDefault() == taskId.GetValueOrDefault() & nullable.HasValue == taskId.HasValue)
        {
          int? inventoryId = record1.InventoryID;
          nullable = record2.InventoryID;
          if (inventoryId.GetValueOrDefault() == nullable.GetValueOrDefault() & inventoryId.HasValue == nullable.HasValue)
          {
            nullable = record1.CostCodeID;
            int? costCodeId = record2.CostCodeID;
            if (nullable.GetValueOrDefault() == costCodeId.GetValueOrDefault() & nullable.HasValue == costCodeId.HasValue)
            {
              if (record2.Sat.GetValueOrDefault() - record1.Sat.GetValueOrDefault() != 0M)
              {
                this.CreateItemTransaction((EPTimeCardItem) record2, timecard.EmployeeID, weekInfo.Sat.Date, new Decimal?(record2.Sat.GetValueOrDefault() - record1.Sat.GetValueOrDefault()), cache2);
                continue;
              }
              continue;
            }
          }
        }
      }
      if (record1.Sat.GetValueOrDefault() != 0M)
        this.CreateItemTransaction((EPTimeCardItem) record1, timecard.EmployeeID, weekInfo.Sat.Date, new Decimal?(-record1.Sat.GetValueOrDefault()), cache2);
      if (record2.Sat.GetValueOrDefault() != 0M)
        this.CreateItemTransaction((EPTimeCardItem) record2, timecard.EmployeeID, weekInfo.Sat.Date, new Decimal?(record2.Sat.GetValueOrDefault()), cache2);
    }
    foreach (TimeCardMaint.ReleasedActivity releasedActivity in releasedActivityList)
    {
      this.Activities.Cache.SetValueExt<PMTimeActivity.released>((object) releasedActivity.Activity, (object) true);
      this.Activities.Cache.SetValue<PMTimeActivity.employeeRate>((object) releasedActivity.Activity, (object) releasedActivity.Cost);
      if (releasedActivity.IsBilled)
        this.Activities.Cache.SetValue<PMTimeActivity.billed>((object) releasedActivity.Activity, (object) true);
      this.Activities.Cache.SetStatus((object) releasedActivity.Activity, PXEntryStatus.Updated);
    }
  }

  public virtual EPEarningType InitEarningType(TimeCardMaint.EPTimecardDetail row)
  {
    EPEarningType epEarningType = row != null ? EPEarningType.PK.Find((PXGraph) this, row.EarningTypeID) : throw new ArgumentNullException();
    if (epEarningType != null && row.EarningTypeID != null && this.Document.Current != null && this.Document.Current.EmployeeID.HasValue)
    {
      System.DateTime? date1 = row.Date;
      if (date1.HasValue)
      {
        row.IsOvertimeCalc = epEarningType.IsOvertime;
        this.RecalculateFields(row);
        TimeCardMaint.EPTimecardDetail epTimecardDetail = row;
        EmployeeCostEngine costEngine = this.CostEngine;
        string earningTypeId = row.EarningTypeID;
        int? employeeId = this.Document.Current.EmployeeID;
        int? labourItemId = row.LabourItemID;
        date1 = row.Date;
        System.DateTime date2 = date1.Value;
        Decimal? nullable = new Decimal?(costEngine.GetOvertimeMultiplier(earningTypeId, employeeId, labourItemId, date2));
        epTimecardDetail.OvertimeMultiplierCalc = nullable;
      }
    }
    return epEarningType;
  }

  public virtual void RecalculateFields(TimeCardMaint.EPTimecardDetail row)
  {
    if (row == null)
      throw new ArgumentNullException();
    row.BillableTimeCalc = new int?();
    row.BillableOvertimeCalc = new int?();
    row.OvertimeBillable = new int?();
    row.RegularTimeCalc = new int?();
    row.OverTimeCalc = new int?();
    bool? nullable = row.IsOvertimeCalc;
    if (nullable.GetValueOrDefault())
    {
      row.OverTimeCalc = row.TimeSpent;
      row.OvertimeSpent = row.TimeSpent;
      nullable = row.IsBillable;
      if (!nullable.GetValueOrDefault())
        return;
      row.BillableOvertimeCalc = row.TimeBillable;
      row.OvertimeBillable = row.TimeBillable;
    }
    else
    {
      row.RegularTimeCalc = row.TimeSpent;
      nullable = row.IsBillable;
      if (!nullable.GetValueOrDefault())
        return;
      row.BillableTimeCalc = row.TimeBillable;
    }
  }

  public virtual TimeCardMaint.EPTimeCardSummaryWithInfo GetSummaryRecord(
    TimeCardMaint.EPTimecardDetail activity)
  {
    foreach (TimeCardMaint.EPTimeCardSummaryWithInfo selectSummaryRecord in (IEnumerable<TimeCardMaint.EPTimeCardSummaryWithInfo>) this.SelectSummaryRecords(this.Document.Current, false))
    {
      if (this.IsFit((EPTimeCardSummary) selectSummaryRecord, activity))
        return selectSummaryRecord;
    }
    return (TimeCardMaint.EPTimeCardSummaryWithInfo) null;
  }

  public virtual bool IsFit(EPTimeCardSummary summary, TimeCardMaint.EPTimecardDetail activity)
  {
    if (!string.IsNullOrEmpty(activity.TimeCardCD) && activity.TimeCardCD != summary.TimeCardCD)
      return false;
    int? nullable1 = activity.SummaryLineNbr;
    if (nullable1.HasValue)
    {
      nullable1 = summary.LineNbr;
      int? summaryLineNbr = activity.SummaryLineNbr;
      return nullable1.GetValueOrDefault() == summaryLineNbr.GetValueOrDefault() & nullable1.HasValue == summaryLineNbr.HasValue;
    }
    int? nullable2 = activity.ProjectID;
    ref int? local1 = ref nullable2;
    nullable1 = ProjectDefaultAttribute.NonProject();
    int num1 = nullable1 ?? 0;
    int num2 = local1 ?? num1;
    nullable2 = summary.ProjectID;
    ref int? local2 = ref nullable2;
    nullable1 = ProjectDefaultAttribute.NonProject();
    int num3 = nullable1 ?? 0;
    int num4 = local2 ?? num3;
    if (num2 != num4)
      return false;
    nullable2 = activity.ProjectTaskID;
    if (!nullable2.HasValue)
    {
      nullable2 = summary.ProjectTaskID;
      if (!nullable2.HasValue)
        goto label_10;
    }
    nullable2 = activity.ProjectTaskID;
    nullable1 = summary.ProjectTaskID;
    if (!(nullable2.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable2.HasValue == nullable1.HasValue))
      return false;
label_10:
    if (activity.EarningTypeID != summary.EarningType)
      return false;
    bool? nullable3 = activity.IsBillable;
    int num5 = nullable3.GetValueOrDefault() ? 1 : 0;
    nullable3 = summary.IsBillable;
    int num6 = nullable3.GetValueOrDefault() ? 1 : 0;
    if (num5 != num6)
      return false;
    nullable3 = activity.CertifiedJob;
    int num7 = nullable3.GetValueOrDefault() ? 1 : 0;
    nullable3 = summary.CertifiedJob;
    int num8 = nullable3.GetValueOrDefault() ? 1 : 0;
    if (num7 != num8)
      return false;
    Guid? parentTaskNoteId = activity.ParentTaskNoteID;
    if (parentTaskNoteId.HasValue)
    {
      parentTaskNoteId = activity.ParentTaskNoteID;
      Guid? parentNoteId = summary.ParentNoteID;
      if ((parentTaskNoteId.HasValue == parentNoteId.HasValue ? (parentTaskNoteId.HasValue ? (parentTaskNoteId.GetValueOrDefault() != parentNoteId.GetValueOrDefault() ? 1 : 0) : 0) : 1) != 0)
        return false;
    }
    else if (summary.ParentNoteID.HasValue)
      return false;
    nullable1 = activity.JobID;
    if (nullable1.HasValue)
    {
      nullable1 = activity.JobID;
      nullable2 = summary.JobID;
      if (!(nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue))
        return false;
    }
    else
    {
      nullable2 = summary.JobID;
      if (nullable2.HasValue)
        return false;
    }
    nullable2 = activity.ShiftID;
    if (nullable2.HasValue)
    {
      nullable2 = activity.ShiftID;
      nullable1 = summary.ShiftID;
      if (!(nullable2.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable2.HasValue == nullable1.HasValue))
        return false;
    }
    else
    {
      nullable1 = summary.ShiftID;
      if (nullable1.HasValue)
        return false;
    }
    nullable1 = activity.JobID;
    if (nullable1.HasValue)
    {
      nullable1 = activity.JobID;
      nullable2 = summary.JobID;
      if (!(nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue))
        return false;
    }
    else
    {
      nullable2 = summary.JobID;
      if (nullable2.HasValue)
        return false;
    }
    nullable2 = activity.CostCodeID;
    if (nullable2.HasValue)
    {
      nullable2 = activity.CostCodeID;
      nullable1 = summary.CostCodeID;
      if (!(nullable2.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable2.HasValue == nullable1.HasValue))
        return false;
    }
    else
    {
      nullable1 = summary.CostCodeID;
      if (nullable1.HasValue)
        return false;
    }
    if (activity.UnionID != null)
    {
      if (activity.UnionID != summary.UnionID)
        return false;
    }
    else if (summary.UnionID != null)
      return false;
    nullable1 = activity.LabourItemID;
    if (nullable1.HasValue)
    {
      nullable1 = activity.LabourItemID;
      nullable2 = summary.LabourItemID;
      if (!(nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue))
        return false;
    }
    else
    {
      nullable2 = summary.LabourItemID;
      if (nullable2.HasValue)
        return false;
    }
    if (activity.WorkCodeID != null)
    {
      if (activity.WorkCodeID != summary.WorkCodeID)
        return false;
    }
    else if (summary.WorkCodeID != null)
      return false;
    return true;
  }

  public virtual bool IsDuplicate(EPTimeCardSummary summary, EPTimeCardSummary duplicate)
  {
    int? lineNbr1 = summary.LineNbr;
    int? lineNbr2 = duplicate.LineNbr;
    if (lineNbr1.GetValueOrDefault() == lineNbr2.GetValueOrDefault() & lineNbr1.HasValue == lineNbr2.HasValue)
      return false;
    int? projectId1 = duplicate.ProjectID;
    ref int? local1 = ref projectId1;
    int? nullable = ProjectDefaultAttribute.NonProject();
    int num1 = nullable ?? 0;
    int num2 = local1 ?? num1;
    int? projectId2 = summary.ProjectID;
    ref int? local2 = ref projectId2;
    nullable = ProjectDefaultAttribute.NonProject();
    int num3 = nullable ?? 0;
    int num4 = local2 ?? num3;
    if (num2 != num4)
      return false;
    if (duplicate.ProjectTaskID.HasValue || summary.ProjectTaskID.HasValue)
    {
      int? projectTaskId1 = duplicate.ProjectTaskID;
      int? projectTaskId2 = summary.ProjectTaskID;
      if (!(projectTaskId1.GetValueOrDefault() == projectTaskId2.GetValueOrDefault() & projectTaskId1.HasValue == projectTaskId2.HasValue))
        return false;
    }
    if (duplicate.EarningType != summary.EarningType || duplicate.IsBillable.GetValueOrDefault() != summary.IsBillable.GetValueOrDefault() || duplicate.CertifiedJob.GetValueOrDefault() != summary.CertifiedJob.GetValueOrDefault())
      return false;
    if (duplicate.ParentNoteID.HasValue)
    {
      Guid? parentNoteId1 = duplicate.ParentNoteID;
      Guid? parentNoteId2 = summary.ParentNoteID;
      if ((parentNoteId1.HasValue == parentNoteId2.HasValue ? (parentNoteId1.HasValue ? (parentNoteId1.GetValueOrDefault() != parentNoteId2.GetValueOrDefault() ? 1 : 0) : 0) : 1) != 0)
        return false;
    }
    else if (summary.ParentNoteID.HasValue)
      return false;
    if (duplicate.JobID.HasValue)
    {
      int? jobId1 = duplicate.JobID;
      int? jobId2 = summary.JobID;
      if (!(jobId1.GetValueOrDefault() == jobId2.GetValueOrDefault() & jobId1.HasValue == jobId2.HasValue))
        return false;
    }
    else if (summary.JobID.HasValue)
      return false;
    if (duplicate.ShiftID.HasValue)
    {
      int? shiftId1 = duplicate.ShiftID;
      int? shiftId2 = summary.ShiftID;
      if (!(shiftId1.GetValueOrDefault() == shiftId2.GetValueOrDefault() & shiftId1.HasValue == shiftId2.HasValue))
        return false;
    }
    else if (summary.ShiftID.HasValue)
      return false;
    if (duplicate.JobID.HasValue)
    {
      int? jobId3 = duplicate.JobID;
      int? jobId4 = summary.JobID;
      if (!(jobId3.GetValueOrDefault() == jobId4.GetValueOrDefault() & jobId3.HasValue == jobId4.HasValue))
        return false;
    }
    else if (summary.JobID.HasValue)
      return false;
    if (duplicate.CostCodeID.HasValue)
    {
      int? costCodeId1 = duplicate.CostCodeID;
      int? costCodeId2 = summary.CostCodeID;
      if (!(costCodeId1.GetValueOrDefault() == costCodeId2.GetValueOrDefault() & costCodeId1.HasValue == costCodeId2.HasValue))
        return false;
    }
    else if (summary.CostCodeID.HasValue)
      return false;
    if (duplicate.UnionID != null)
    {
      if (duplicate.UnionID != summary.UnionID)
        return false;
    }
    else if (summary.UnionID != null)
      return false;
    if (duplicate.LabourItemID.HasValue)
    {
      int? labourItemId1 = duplicate.LabourItemID;
      int? labourItemId2 = summary.LabourItemID;
      if (!(labourItemId1.GetValueOrDefault() == labourItemId2.GetValueOrDefault() & labourItemId1.HasValue == labourItemId2.HasValue))
        return false;
    }
    else if (summary.LabourItemID.HasValue)
      return false;
    if (duplicate.WorkCodeID != null)
    {
      if (duplicate.WorkCodeID != summary.WorkCodeID)
        return false;
    }
    else if (summary.WorkCodeID != null)
      return false;
    return true;
  }

  public virtual TimeCardMaint.EPTimeCardSummaryWithInfo AddToSummary(
    TimeCardMaint.EPTimeCardSummaryWithInfo summary,
    PMTimeActivity activity)
  {
    return this.AddToSummary(summary, activity, 1);
  }

  public virtual void SubtractFromSummary(
    TimeCardMaint.EPTimeCardSummaryWithInfo summary,
    PMTimeActivity activity)
  {
    summary = this.AddToSummary(summary, activity, -1);
    try
    {
      this.dontSyncDetails = true;
      if (summary == null || summary.TimeSpent.Value != 0)
        return;
      this.Summary.Delete(summary);
    }
    finally
    {
      this.dontSyncDetails = false;
    }
  }

  public virtual TimeCardMaint.EPTimeCardSummaryWithInfo AddToSummary(
    TimeCardMaint.EPTimeCardSummaryWithInfo summary,
    PMTimeActivity activity,
    int mult)
  {
    int? nullable = activity != null ? activity.TimeSpent : throw new ArgumentNullException();
    if (nullable.GetValueOrDefault() == 0)
      return (TimeCardMaint.EPTimeCardSummaryWithInfo) null;
    nullable = activity.ProjectID;
    if (!nullable.HasValue)
      return (TimeCardMaint.EPTimeCardSummaryWithInfo) null;
    nullable = activity.ProjectTaskID;
    if (!nullable.HasValue && !ProjectDefaultAttribute.IsNonProject(activity.ProjectID))
      return (TimeCardMaint.EPTimeCardSummaryWithInfo) null;
    if (summary == null)
    {
      summary = (TimeCardMaint.EPTimeCardSummaryWithInfo) this.Summary.Cache.CreateInstance();
      summary.EarningType = activity.EarningTypeID;
      summary.ParentNoteID = activity.ParentTaskNoteID;
      summary.ProjectID = activity.ProjectID;
      summary.ProjectTaskID = activity.ProjectTaskID;
      summary.IsBillable = activity.IsBillable;
      summary.JobID = activity.JobID;
      summary.ShiftID = activity.ShiftID;
      summary.CostCodeID = activity.CostCodeID;
      summary.UnionID = activity.UnionID;
      summary.LabourItemID = activity.LabourItemID;
      summary.WorkCodeID = activity.WorkCodeID;
      summary.CertifiedJob = activity.CertifiedJob;
      PMProject pmProject = (PMProject) PXSelectorAttribute.Select<TimeCardMaint.EPTimecardDetail.projectID>(this.Activities.Cache, (object) activity, (object) activity.ProjectID);
      if (pmProject != null)
        summary.ProjectManager = pmProject.ApproverID;
      PMTask pmTask1 = (PMTask) PXSelectorAttribute.Select<TimeCardMaint.EPTimecardDetail.projectTaskID>(this.Activities.Cache, (object) activity, (object) activity.ProjectTaskID);
      if (pmTask1 != null)
        summary.TaskApproverID = pmTask1.ApproverID;
      if (activity.ParentTaskNoteID.HasValue)
      {
        PX.Objects.CR.CRActivity crActivity = (PX.Objects.CR.CRActivity) PXSelectBase<PX.Objects.CR.CRActivity, PXSelect<PX.Objects.CR.CRActivity, Where<PX.Objects.CR.CRActivity.noteID, Equal<Required<PMTimeActivity.parentTaskNoteID>>>>.Config>.Select((PXGraph) this, (object) activity.ParentTaskNoteID);
        if (crActivity != null)
          summary.Description = crActivity.Subject;
      }
      else
      {
        PMTask pmTask2 = (PMTask) PXSelectBase<PMTask, PXSelect<PMTask, Where<PMTask.projectID, Equal<Required<PMTask.projectID>>, And<PMTask.noteID, Equal<Required<PMTask.noteID>>>>>.Config>.Select((PXGraph) this, (object) activity.ProjectID, (object) activity.ParentTaskNoteID);
        if (pmTask2 != null)
          summary.Description = pmTask2.Description;
      }
      this.dontSyncDetails = true;
      try
      {
        summary = this.Summary.Insert(summary);
        if (summary == null)
          return (TimeCardMaint.EPTimeCardSummaryWithInfo) null;
        summary.ProjectID = activity.ProjectID;
        summary.ProjectTaskID = activity.ProjectTaskID;
        summary.CostCodeID = activity.CostCodeID;
        summary.UnionID = activity.UnionID;
        summary.LabourItemID = activity.LabourItemID;
        summary.WorkCodeID = activity.WorkCodeID;
        summary.CertifiedJob = activity.CertifiedJob;
        summary.IsBillable = activity.IsBillable;
        summary.ShiftID = activity.ShiftID;
      }
      finally
      {
        this.dontSyncDetails = false;
      }
    }
    this.AddActivityTimeToSummary((EPTimeCardSummary) summary, activity, mult);
    this.Summary.Cache.MarkUpdated((object) summary);
    return summary;
  }

  public virtual void AddActivityTimeToSummary(
    EPTimeCardSummary summary,
    PMTimeActivity activity,
    int mult)
  {
    if (!activity.TimeSpent.HasValue || !activity.Date.HasValue)
      return;
    switch (activity.Date.Value.DayOfWeek)
    {
      case DayOfWeek.Sunday:
        summary.Sun = new int?(summary.Sun.GetValueOrDefault() + mult * activity.TimeSpent.Value);
        break;
      case DayOfWeek.Monday:
        summary.Mon = new int?(summary.Mon.GetValueOrDefault() + mult * activity.TimeSpent.Value);
        break;
      case DayOfWeek.Tuesday:
        summary.Tue = new int?(summary.Tue.GetValueOrDefault() + mult * activity.TimeSpent.Value);
        break;
      case DayOfWeek.Wednesday:
        summary.Wed = new int?(summary.Wed.GetValueOrDefault() + mult * activity.TimeSpent.Value);
        break;
      case DayOfWeek.Thursday:
        summary.Thu = new int?(summary.Thu.GetValueOrDefault() + mult * activity.TimeSpent.Value);
        break;
      case DayOfWeek.Friday:
        summary.Fri = new int?(summary.Fri.GetValueOrDefault() + mult * activity.TimeSpent.Value);
        break;
      case DayOfWeek.Saturday:
        summary.Sat = new int?(summary.Sat.GetValueOrDefault() + mult * activity.TimeSpent.Value);
        break;
    }
  }

  public virtual void UpdateAdjustingActivities(EPTimeCardSummary summary)
  {
    this.UpdateAdjustingActivities(summary, false);
  }

  public virtual void UpdateAdjustingActivities(
    EPTimeCardSummary summary,
    bool skipDescriptionUpdate)
  {
    EPTimeCard doc = summary != null ? (EPTimeCard) PXSelectBase<EPTimeCard, PXSelect<EPTimeCard, Where<EPTimeCard.timeCardCD, Equal<Required<EPTimeCard.timeCardCD>>>>.Config>.Select((PXGraph) this, (object) summary.TimeCardCD) : throw new ArgumentNullException();
    if (doc == null)
      return;
    Dictionary<DayOfWeek, TimeCardMaint.DayActivities> activities = this.GetActivities(summary, doc);
    PXWeekSelector2Attribute.WeekInfo weekInfo = PXWeekSelector2Attribute.GetWeekInfo((PXGraph) this, doc.WeekID.Value);
    if (weekInfo.Mon.Enabled)
      this.UpdateAdjustingActivities(summary, activities, DayOfWeek.Monday, this.SetEmployeeTime(weekInfo.Mon.Date).Value, skipDescriptionUpdate);
    if (weekInfo.Tue.Enabled)
      this.UpdateAdjustingActivities(summary, activities, DayOfWeek.Tuesday, this.SetEmployeeTime(weekInfo.Tue.Date).Value, skipDescriptionUpdate);
    if (weekInfo.Wed.Enabled)
      this.UpdateAdjustingActivities(summary, activities, DayOfWeek.Wednesday, this.SetEmployeeTime(weekInfo.Wed.Date).Value, skipDescriptionUpdate);
    if (weekInfo.Thu.Enabled)
      this.UpdateAdjustingActivities(summary, activities, DayOfWeek.Thursday, this.SetEmployeeTime(weekInfo.Thu.Date).Value, skipDescriptionUpdate);
    if (weekInfo.Fri.Enabled)
      this.UpdateAdjustingActivities(summary, activities, DayOfWeek.Friday, this.SetEmployeeTime(weekInfo.Fri.Date).Value, skipDescriptionUpdate);
    if (weekInfo.Sat.Enabled)
      this.UpdateAdjustingActivities(summary, activities, DayOfWeek.Saturday, this.SetEmployeeTime(weekInfo.Sat.Date).Value, skipDescriptionUpdate);
    if (!weekInfo.Sun.Enabled)
      return;
    this.UpdateAdjustingActivities(summary, activities, DayOfWeek.Sunday, this.SetEmployeeTime(weekInfo.Sun.Date).Value, skipDescriptionUpdate);
  }

  public virtual System.DateTime? SetEmployeeTime(System.DateTime? date)
  {
    PX.Objects.CS.CSCalendar csCalendar = PX.Objects.CS.CSCalendar.PK.Find((PXGraph) this, this.Employee.Current?.CalendarID);
    if (csCalendar != null)
    {
      switch (date.Value.DayOfWeek)
      {
        case DayOfWeek.Sunday:
          return PXDBDateAndTimeAttribute.CombineDateTime(date, csCalendar.SunStartTime);
        case DayOfWeek.Monday:
          return PXDBDateAndTimeAttribute.CombineDateTime(date, csCalendar.MonStartTime);
        case DayOfWeek.Tuesday:
          return PXDBDateAndTimeAttribute.CombineDateTime(date, csCalendar.TueStartTime);
        case DayOfWeek.Wednesday:
          return PXDBDateAndTimeAttribute.CombineDateTime(date, csCalendar.WedStartTime);
        case DayOfWeek.Thursday:
          return PXDBDateAndTimeAttribute.CombineDateTime(date, csCalendar.ThuStartTime);
        case DayOfWeek.Friday:
          return PXDBDateAndTimeAttribute.CombineDateTime(date, csCalendar.FriStartTime);
        case DayOfWeek.Saturday:
          return PXDBDateAndTimeAttribute.CombineDateTime(date, csCalendar.SatStartTime);
      }
    }
    return PXDateAndTimeAttribute.CombineDateTime(date, new System.DateTime?(new System.DateTime(2008, 1, 1, 9, 0, 0)));
  }

  public virtual void UpdateAdjustingActivities(
    EPTimeCardSummary summary,
    Dictionary<DayOfWeek, TimeCardMaint.DayActivities> dict,
    DayOfWeek dayOfWeek,
    System.DateTime startDate,
    bool skipDescriptionUpdate)
  {
    if (summary == null)
      throw new ArgumentNullException(nameof (summary));
    if (dict == null)
      throw new ArgumentNullException(nameof (dict));
    int num1 = 0;
    int? nullable1;
    if (this.Summary.Cache.GetStatus((object) summary) != PXEntryStatus.Deleted && this.Summary.Cache.GetStatus((object) summary) != PXEntryStatus.InsertedDeleted)
    {
      nullable1 = summary.GetTimeTotal(dayOfWeek);
      num1 = nullable1.GetValueOrDefault();
    }
    int num2 = 0;
    if (dict.ContainsKey(dayOfWeek))
    {
      TimeCardMaint.DayActivities dayActivities = dict[dayOfWeek];
      nullable1 = summary.LineNbr;
      int summaryLineNbr = nullable1.Value;
      num2 = dayActivities.GetTotalTime(summaryLineNbr);
    }
    TimeCardMaint.EPTimecardDetail epTimecardDetail1 = (TimeCardMaint.EPTimecardDetail) null;
    if (dict.ContainsKey(dayOfWeek))
    {
      TimeCardMaint.DayActivities dayActivities = dict[dayOfWeek];
      nullable1 = summary.LineNbr;
      int summaryLineNbr = nullable1.Value;
      epTimecardDetail1 = dayActivities.GetAdjustingActivity(summaryLineNbr);
    }
    if (num1 != num2)
    {
      if (epTimecardDetail1 == null && num1 - num2 != 0)
      {
        this.dontSyncSummary = true;
        try
        {
          TimeCardMaint.EPTimecardDetail epTimecardDetail2 = this.Activities.Insert();
          this.Activities.Cache.SetValueExt<TimeCardMaint.EPTimecardDetail.date>((object) epTimecardDetail2, (object) startDate);
          if (!string.IsNullOrEmpty(summary.Description))
            epTimecardDetail2.Summary = summary.Description;
          else
            epTimecardDetail2.Summary = string.Format(PXMessages.LocalizeNoPrefix("Summary {0} Activities"), (object) TimeCardMaint.LocalizeDayOfWeek(dayOfWeek));
          epTimecardDetail2.ParentTaskNoteID = summary.ParentNoteID;
          epTimecardDetail2.ApprovalStatus = "CD";
          epTimecardDetail2.EarningTypeID = summary.EarningType;
          epTimecardDetail2.JobID = summary.JobID;
          epTimecardDetail2.ShiftID = summary.ShiftID;
          epTimecardDetail2.CostCodeID = summary.CostCodeID;
          epTimecardDetail2.UnionID = summary.UnionID;
          epTimecardDetail2.LabourItemID = summary.LabourItemID;
          epTimecardDetail2.WorkCodeID = summary.WorkCodeID;
          epTimecardDetail2.SummaryLineNbr = summary.LineNbr;
          epTimecardDetail2.Day = ((int) epTimecardDetail2.Date.Value.DayOfWeek).ToString();
          epTimecardDetail2.IsBillable = summary.IsBillable;
          epTimecardDetail2.CertifiedJob = summary.CertifiedJob;
          epTimecardDetail2.TimeSpent = new int?(num1 - num2);
          if (epTimecardDetail2.IsBillable.GetValueOrDefault())
            epTimecardDetail2.TimeBillable = epTimecardDetail2.TimeSpent;
          epTimecardDetail2.ProjectID = summary.ProjectID;
          epTimecardDetail2.ProjectTaskID = summary.ProjectTaskID;
          this.Activities.Cache.SetDefaultExt<PMTimeActivity.approverID>((object) epTimecardDetail2);
          this.Activities.Cache.SetDefaultExt<PMTimeActivity.approvalStatus>((object) epTimecardDetail2);
          EmployeeCostEngine.LaborCost employeeCost = this.CostEngine.CalculateEmployeeCost((string) null, epTimecardDetail2.EarningTypeID, epTimecardDetail2.LabourItemID, epTimecardDetail2.ProjectID, epTimecardDetail2.ProjectTaskID, epTimecardDetail2.CertifiedJob, epTimecardDetail2.UnionID, this.Document.Current.EmployeeID, epTimecardDetail2.Date.Value, epTimecardDetail2.ShiftID);
          epTimecardDetail2.EmployeeRate = (Decimal?) employeeCost?.Rate;
          epTimecardDetail2.OvertimeMultiplierCalc = (Decimal?) employeeCost?.OvertimeMultiplier;
          this.InitEarningType(epTimecardDetail2);
          this.RecalculateFields(epTimecardDetail2);
        }
        finally
        {
          this.dontSyncSummary = false;
        }
      }
      else
      {
        if (epTimecardDetail1 != null && num1 == 0)
        {
          nullable1 = epTimecardDetail1.SummaryLineNbr;
          if (nullable1.HasValue)
          {
            this.dontSyncSummary = true;
            try
            {
              this.Activities.Delete(epTimecardDetail1);
              return;
            }
            finally
            {
              this.dontSyncSummary = false;
            }
          }
        }
        if (epTimecardDetail1 == null)
          return;
        TimeCardMaint.EPTimecardDetail epTimecardDetail3 = epTimecardDetail1;
        int? nullable2 = epTimecardDetail1.TimeSpent;
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
        epTimecardDetail3.TimeSpent = nullable3;
        if (!string.IsNullOrEmpty(summary.Description) && (!skipDescriptionUpdate || string.IsNullOrEmpty(epTimecardDetail1.Summary)))
          epTimecardDetail1.Summary = summary.Description;
        epTimecardDetail1.IsBillable = summary.IsBillable;
        if (epTimecardDetail1.IsBillable.GetValueOrDefault())
          epTimecardDetail1.TimeBillable = epTimecardDetail1.TimeSpent;
        epTimecardDetail1.CertifiedJob = summary.CertifiedJob;
        this.RecalculateFields(epTimecardDetail1);
        object obj = PXFormulaAttribute.Evaluate<PMTimeActivity.approvalStatus>(this.Caches[typeof (PMTimeActivity)], (object) epTimecardDetail1);
        if (!string.IsNullOrEmpty(obj?.ToString()))
          epTimecardDetail1.ApprovalStatus = obj.ToString();
        this.Activities.Cache.MarkUpdated((object) epTimecardDetail1);
      }
    }
    else
    {
      if (epTimecardDetail1 == null || string.IsNullOrEmpty(summary.Description) || skipDescriptionUpdate)
        return;
      this.Activities.Cache.SetValue<PMTimeActivity.summary>((object) epTimecardDetail1, (object) summary.Description);
      this.Activities.Cache.MarkUpdated((object) epTimecardDetail1);
    }
  }

  public virtual Dictionary<DayOfWeek, TimeCardMaint.DayActivities> GetActivities(
    EPTimeCardSummary summary,
    EPTimeCard doc)
  {
    if (summary == null)
      throw new ArgumentNullException(nameof (summary));
    if (doc == null)
      throw new ArgumentNullException(nameof (doc));
    Dictionary<DayOfWeek, TimeCardMaint.DayActivities> activities = new Dictionary<DayOfWeek, TimeCardMaint.DayActivities>();
    List<EPTimeCardSummary> duplicates = this.FindDuplicates(summary);
    foreach (TimeCardMaint.EPTimecardDetail detail in this.GetDetails(summary, doc, false))
    {
      bool flag = false;
      foreach (EPTimeCardSummary summary1 in duplicates)
      {
        if (this.IsFit(summary1, detail))
        {
          flag = true;
          break;
        }
      }
      if (!flag)
      {
        System.DateTime dateTime = detail.Date.Value;
        DayOfWeek dayOfWeek1 = dateTime.DayOfWeek;
        Dictionary<DayOfWeek, TimeCardMaint.DayActivities> dictionary1 = activities;
        dateTime = detail.Date.Value;
        int dayOfWeek2 = (int) dateTime.DayOfWeek;
        if (dictionary1.ContainsKey((DayOfWeek) dayOfWeek2))
        {
          Dictionary<DayOfWeek, TimeCardMaint.DayActivities> dictionary2 = activities;
          dateTime = detail.Date.Value;
          int dayOfWeek3 = (int) dateTime.DayOfWeek;
          dictionary2[(DayOfWeek) dayOfWeek3].Activities.Add(detail);
        }
        else
        {
          TimeCardMaint.DayActivities dayActivities1 = new TimeCardMaint.DayActivities();
          dayActivities1.Day = dayOfWeek1;
          dayActivities1.Activities.Add(detail);
          Dictionary<DayOfWeek, TimeCardMaint.DayActivities> dictionary3 = activities;
          dateTime = detail.Date.Value;
          int dayOfWeek4 = (int) dateTime.DayOfWeek;
          TimeCardMaint.DayActivities dayActivities2 = dayActivities1;
          dictionary3.Add((DayOfWeek) dayOfWeek4, dayActivities2);
        }
      }
    }
    return activities;
  }

  public virtual bool AreTheseConnected(
    EPTimeCardSummary summary,
    TimeCardMaint.EPTimecardDetail detail)
  {
    if (detail.SummaryLineNbr.HasValue)
    {
      int? summaryLineNbr = detail.SummaryLineNbr;
      int? lineNbr = summary.LineNbr;
      return summaryLineNbr.GetValueOrDefault() == lineNbr.GetValueOrDefault() & summaryLineNbr.HasValue == lineNbr.HasValue;
    }
    if (summary.EarningType != detail.EarningTypeID)
      return false;
    int? nullable1 = summary.JobID;
    int? nullable2 = detail.JobID;
    if (!(nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue))
      return false;
    nullable2 = summary.ShiftID;
    nullable1 = detail.ShiftID;
    if (!(nullable2.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable2.HasValue == nullable1.HasValue))
      return false;
    nullable1 = summary.ProjectID;
    nullable2 = detail.ProjectID;
    if (!(nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue))
      return false;
    nullable2 = summary.ProjectTaskID;
    nullable1 = detail.ProjectTaskID;
    if (!(nullable2.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable2.HasValue == nullable1.HasValue))
      return false;
    nullable1 = summary.CostCodeID;
    nullable2 = detail.CostCodeID;
    if (!(nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue))
      return false;
    if (!summary.ParentNoteID.HasValue)
      return true;
    Guid? parentNoteId = summary.ParentNoteID;
    Guid? parentTaskNoteId = detail.ParentTaskNoteID;
    if (parentNoteId.HasValue != parentTaskNoteId.HasValue)
      return false;
    return !parentNoteId.HasValue || parentNoteId.GetValueOrDefault() == parentTaskNoteId.GetValueOrDefault();
  }

  public virtual List<EPTimeCardSummary> FindDuplicates(EPTimeCardSummary summary)
  {
    List<EPTimeCardSummary> duplicates = new List<EPTimeCardSummary>();
    foreach (EPTimeCardSummary selectSummaryRecord in (IEnumerable<TimeCardMaint.EPTimeCardSummaryWithInfo>) this.SelectSummaryRecords(this.Document.Current, false))
    {
      if (this.IsDuplicate(summary, selectSummaryRecord))
        duplicates.Add(selectSummaryRecord);
    }
    return duplicates;
  }

  public virtual List<TimeCardMaint.EPTimecardDetail> GetDetails(
    EPTimeCardSummary summary,
    EPTimeCard doc,
    bool onlyManual)
  {
    if (summary == null)
      throw new ArgumentNullException(nameof (summary));
    if (doc == null)
      throw new ArgumentNullException(nameof (doc));
    List<TimeCardMaint.EPTimecardDetail> details = new List<TimeCardMaint.EPTimecardDetail>();
    foreach (PXResult activity1 in this.activities())
    {
      TimeCardMaint.EPTimecardDetail activity2 = (TimeCardMaint.EPTimecardDetail) activity1[typeof (TimeCardMaint.EPTimecardDetail)];
      if (this.IsFit(summary, activity2) && (!onlyManual || !activity2.SummaryLineNbr.HasValue))
        details.Add(activity2);
    }
    return details;
  }

  public virtual void RecalculateTotals(EPTimeCard timecard)
  {
    if (timecard == null)
      throw new ArgumentNullException();
    List<TimeCardMaint.EPTimecardDetail> details = new List<TimeCardMaint.EPTimecardDetail>();
    if (timecard.IsHold.GetValueOrDefault())
    {
      foreach (PXResult activity in this.activities())
      {
        TimeCardMaint.EPTimecardDetail epTimecardDetail = (TimeCardMaint.EPTimecardDetail) activity[typeof (TimeCardMaint.EPTimecardDetail)];
        details.Add(epTimecardDetail);
      }
    }
    this.RecalculateTotals(timecard, details);
  }

  public virtual void RecalculateTotals(
    EPTimeCard timecard,
    List<TimeCardMaint.EPTimecardDetail> details)
  {
    if (timecard == null)
      throw new ArgumentNullException(nameof (timecard));
    if (details == null)
      throw new ArgumentNullException(nameof (details));
    int num1 = timecard.TimeSpent.GetValueOrDefault();
    int? nullable = timecard.OvertimeSpent;
    int num2 = nullable.GetValueOrDefault();
    nullable = timecard.TimeBillable;
    int num3 = nullable.GetValueOrDefault();
    nullable = timecard.OvertimeBillable;
    int num4 = nullable.GetValueOrDefault();
    if (timecard.IsHold.GetValueOrDefault())
    {
      num1 = 0;
      num2 = 0;
      num3 = 0;
      num4 = 0;
      foreach (TimeCardMaint.EPTimecardDetail detail in details)
      {
        int num5 = num1;
        nullable = detail.RegularTimeCalc;
        int valueOrDefault1 = nullable.GetValueOrDefault();
        num1 = num5 + valueOrDefault1;
        int num6 = num3;
        nullable = detail.BillableTimeCalc;
        int valueOrDefault2 = nullable.GetValueOrDefault();
        num3 = num6 + valueOrDefault2;
        int num7 = num2;
        nullable = detail.OverTimeCalc;
        int valueOrDefault3 = nullable.GetValueOrDefault();
        num2 = num7 + valueOrDefault3;
        int num8 = num4;
        nullable = detail.BillableOvertimeCalc;
        int valueOrDefault4 = nullable.GetValueOrDefault();
        num4 = num8 + valueOrDefault4;
      }
    }
    timecard.TimeSpentCalc = new int?(num1);
    timecard.OvertimeSpentCalc = new int?(num2);
    timecard.TotalSpentCalc = new int?(num1 + num2);
    timecard.TimeBillableCalc = new int?(num3);
    timecard.OvertimeBillableCalc = new int?(num4);
    timecard.TotalBillableCalc = new int?(num3 + num4);
  }

  public virtual void ValidateProjectAndProjectTask(TimeCardMaint.EPTimecardDetail timeCardDetail)
  {
    if (timeCardDetail == null || this.Document.Current == null || this.Document.Current.IsReleased.GetValueOrDefault())
      return;
    string error1 = PXUIFieldAttribute.GetError<TimeCardMaint.EPTimecardDetail.projectID>(this.Activities.Cache, (object) timeCardDetail);
    if (!string.IsNullOrEmpty(error1) && error1.Equals(PXLocalizer.Localize("The project is expired.")))
      PXUIFieldAttribute.SetError<TimeCardMaint.EPTimecardDetail.projectID>(this.Activities.Cache, (object) timeCardDetail, (string) null);
    System.DateTime? nullable;
    if (timeCardDetail.ProjectID.HasValue)
    {
      PMProject pmProject = (PMProject) PXSelectorAttribute.Select<TimeCardMaint.EPTimecardDetail.projectID>(this.Activities.Cache, (object) timeCardDetail, (object) timeCardDetail.ProjectID);
      if (pmProject != null && timeCardDetail != null && pmProject.ExpireDate.HasValue && timeCardDetail.Date.HasValue)
      {
        System.DateTime? date = timeCardDetail.Date;
        nullable = pmProject.ExpireDate;
        if ((date.HasValue & nullable.HasValue ? (date.GetValueOrDefault() > nullable.GetValueOrDefault() ? 1 : 0) : 0) != 0)
          this.Activities.Cache.RaiseExceptionHandling<TimeCardMaint.EPTimecardDetail.projectID>((object) timeCardDetail, (object) timeCardDetail.ProjectID, (Exception) new PXSetPropertyException("The project is expired.", PXErrorLevel.Warning));
      }
    }
    string error2 = PXUIFieldAttribute.GetError<TimeCardMaint.EPTimecardDetail.projectTaskID>(this.Activities.Cache, (object) timeCardDetail);
    if (!string.IsNullOrEmpty(error2) && error2.Equals(PXLocalizer.Localize("The project task is expired.")))
      PXUIFieldAttribute.SetError<TimeCardMaint.EPTimecardDetail.projectTaskID>(this.Activities.Cache, (object) timeCardDetail, (string) null);
    if (!timeCardDetail.ProjectTaskID.HasValue)
      return;
    PMTask dirty = PMTask.PK.FindDirty((PXGraph) this, timeCardDetail.ProjectID, timeCardDetail.ProjectTaskID);
    if (dirty == null || timeCardDetail == null)
      return;
    nullable = dirty.EndDate;
    if (!nullable.HasValue)
      return;
    nullable = timeCardDetail.Date;
    if (!nullable.HasValue)
      return;
    nullable = timeCardDetail.Date;
    System.DateTime? endDate = dirty.EndDate;
    if ((nullable.HasValue & endDate.HasValue ? (nullable.GetValueOrDefault() > endDate.GetValueOrDefault() ? 1 : 0) : 0) == 0)
      return;
    this.Activities.Cache.RaiseExceptionHandling<TimeCardMaint.EPTimecardDetail.projectTaskID>((object) timeCardDetail, (object) timeCardDetail.ProjectTaskID, (Exception) new PXSetPropertyException("The project task is expired.", PXErrorLevel.Warning));
  }

  private bool HasDuplicate(int? employeeID, int? weekId, string timeCardCD)
  {
    return (EPTimeCard) PXSelectBase<EPTimeCard, PXSelectReadonly<EPTimeCard, Where<EPTimeCard.employeeID, Equal<Required<EPTimeCard.employeeID>>, And<EPTimeCard.weekId, Equal<Required<EPTimeCard.weekId>>, And<EPTimeCard.timeCardCD, NotEqual<Required<EPTimeCard.timeCardCD>>>>>>.Config>.SelectSingleBound((PXGraph) this, (object[]) null, (object) employeeID, (object) weekId, (object) timeCardCD) != null;
  }

  public virtual bool ValidateTotals(EPTimeCard timecard, out string errorMsg)
  {
    if (timecard == null)
      throw new ArgumentNullException();
    bool flag1 = true;
    errorMsg = (string) null;
    EPEmployee epEmployee = EPEmployee.PK.Find((PXGraph) this, timecard.EmployeeID);
    if (epEmployee == null || epEmployee.HoursValidation == "N")
      return true;
    PXUIFieldAttribute.SetError<EPTimeCard.timeSpentCalc>(this.Document.Cache, (object) timecard, (string) null);
    PXUIFieldAttribute.SetError<EPTimeCard.overtimeSpentCalc>(this.Document.Cache, (object) timecard, (string) null);
    PXUIFieldAttribute.SetError<EPTimeCard.totalSpentCalc>(this.Document.Cache, (object) timecard, (string) null);
    System.DateTime? businessDate = this.Accessinfo.BusinessDate;
    if (!businessDate.HasValue)
    {
      System.DateTime now = System.DateTime.Now;
    }
    else
      businessDate.GetValueOrDefault();
    bool flag2 = true;
    int? nullable1 = timecard.WeekID;
    if (nullable1.HasValue && PXSelectorAttribute.Select<EPTimeCard.weekId>(this.Document.Cache, (object) timecard) is EPWeekRaw epWeekRaw)
    {
      System.DateTime dateTime = epWeekRaw.StartDate.Value;
      flag2 = epWeekRaw.IsFullWeek.Value;
    }
    int regularWeeklyMinutes = this.CostEngine.GetEmployeeRegularWeeklyMinutes(this.Document.Current.EmployeeID, this.Document.Current.WeekStartDate);
    Decimal? nullable2 = new Decimal?((Decimal) regularWeeklyMinutes / 60M);
    if (flag2)
    {
      nullable1 = timecard.TimeSpentCalc;
      int num = regularWeeklyMinutes;
      if (nullable1.GetValueOrDefault() > num & nullable1.HasValue)
      {
        flag1 = false;
        errorMsg = PXMessages.LocalizeFormatNoPrefixNLA("Regular Time for the week cannot exceed {0} hrs.", (object) nullable2);
        this.Document.Cache.RaiseExceptionHandling<EPTimeCard.timeSpentCalc>((object) timecard, (object) timecard.TimeSpentCalc, (Exception) new PXSetPropertyException(errorMsg, PXErrorLevel.Warning));
        goto label_17;
      }
    }
    if (flag2)
    {
      nullable1 = timecard.TimeSpentCalc;
      int num1 = regularWeeklyMinutes;
      if (nullable1.GetValueOrDefault() < num1 & nullable1.HasValue)
      {
        flag1 = false;
        nullable1 = timecard.OvertimeSpentCalc;
        int num2 = 0;
        if (nullable1.GetValueOrDefault() > num2 & nullable1.HasValue)
        {
          errorMsg = PXMessages.LocalizeFormatNoPrefixNLA("Overtime cannot be specified untill all the available regular time is utilised. Regular time for week = {0} hrs", (object) nullable2);
          this.Document.Cache.RaiseExceptionHandling<EPTimeCard.overtimeSpentCalc>((object) timecard, (object) timecard.OvertimeSpentCalc, (Exception) new PXSetPropertyException(errorMsg, PXErrorLevel.Warning));
        }
        else
        {
          errorMsg = PXMessages.LocalizeFormatNoPrefixNLA("The time card must be filled out completely. The norm of regular hours this employee should spend during a week is {0} hours. You can click Normalize Time Card on the table toolbar of the Summary tab to automatically fill up the remaining hours.", (object) nullable2);
          this.Document.Cache.RaiseExceptionHandling<EPTimeCard.totalSpentCalc>((object) timecard, (object) timecard.TotalSpentCalc, (Exception) new PXSetPropertyException(errorMsg, PXErrorLevel.Warning));
        }
      }
    }
label_17:
    return flag1;
  }

  public virtual PMTran CreateItemTransaction(
    EPTimeCardItem record,
    int? employeeID,
    System.DateTime? date,
    Decimal? qty,
    PXCache tranCache)
  {
    bool flag = this.EpSetup.Current != null && EPSetupMaint.GetPostToOffBalance((PXGraph) this, this.EpSetup.Current, employeeID);
    PX.Objects.IN.InventoryItem inventoryItem = PX.Objects.IN.InventoryItem.PK.Find((PXGraph) this, record.InventoryID);
    if (inventoryItem == null)
      throw new PXException("Inventory Item is not entered");
    int? nullable1;
    if (!flag)
    {
      nullable1 = inventoryItem.InvtAcctID;
      if (!nullable1.HasValue)
        throw new PXException("Expense Accrual Account is Required but is not configured for Non-Stock Item '{0}'. Please setup the account and try again.", new object[1]
        {
          (object) inventoryItem.InventoryCD.Trim()
        });
    }
    if (!flag)
    {
      nullable1 = inventoryItem.InvtSubID;
      if (!nullable1.HasValue)
        throw new PXException("Expense Accrual Subaccount is Required but is not configured for Non-Stock Item '{0}'. Please setup the subaccount and try again.", new object[1]
        {
          (object) inventoryItem.InventoryCD.Trim()
        });
    }
    PX.Objects.CT.Contract contract = PX.Objects.CT.Contract.PK.Find((PXGraph) this, record.ProjectID);
    Decimal? nullable2 = new Decimal?(0M);
    int? accountID = inventoryItem.COGSAcctID;
    int? nullable3 = inventoryItem.InvtAcctID;
    int? nullable4 = new int?();
    string str1 = (string) null;
    string str2 = (string) null;
    int? branchID = new int?();
    EPEmployee epEmployee = EPEmployee.PK.Find((PXGraph) this, employeeID);
    if (epEmployee != null)
    {
      PX.Objects.GL.Branch branch = (PX.Objects.GL.Branch) PXSelectBase<PX.Objects.GL.Branch, PXSelect<PX.Objects.GL.Branch, Where<PX.Objects.GL.Branch.bAccountID, Equal<Required<EPEmployee.parentBAccountID>>>>.Config>.Select((PXGraph) this, (object) epEmployee.ParentBAccountID);
      if (branch != null)
        branchID = branch.BranchID;
    }
    if (contract.BaseType == "P")
    {
      nullable2 = inventoryItem.StdCost;
      if (!contract.NonProject.GetValueOrDefault())
      {
        PMTask dirty = PMTask.PK.FindDirty((PXGraph) this, record.ProjectID, record.TaskID);
        if (this.ExpenseAccountSource == "P")
        {
          nullable1 = contract.DefaultExpenseAccountID;
          if (nullable1.HasValue)
          {
            accountID = contract.DefaultExpenseAccountID;
            PX.Objects.GL.Account account = PX.Objects.GL.Account.PK.Find((PXGraph) this, accountID);
            nullable1 = account.AccountGroupID;
            if (!nullable1.HasValue)
              throw new PXException("Project preferences have been configured to get the expense account from project tasks, but the {0} default cost account specified for the {1} task of the {2} project is not mapped to any account group.", new object[2]
              {
                (object) account.AccountCD.Trim(),
                (object) contract.ContractCD.Trim()
              });
            nullable4 = account.AccountGroupID;
          }
          else
            PXTrace.WriteWarning("Project preferences have been configured to get the expense account from projects, but the default cost account is not specified for the {0} project.", (object) contract.ContractCD.Trim());
        }
        else if (this.ExpenseAccountSource == "T")
        {
          nullable1 = dirty.DefaultExpenseAccountID;
          if (nullable1.HasValue)
          {
            accountID = dirty.DefaultExpenseAccountID;
            PX.Objects.GL.Account account = PX.Objects.GL.Account.PK.Find((PXGraph) this, accountID);
            nullable1 = account.AccountGroupID;
            if (!nullable1.HasValue)
              throw new PXException("Project preferences have been configured to get the expense account from project tasks, but the {0} default cost account of the {2} task of the {1} project is not mapped to any account group.", new object[3]
              {
                (object) account.AccountCD.Trim(),
                (object) contract.ContractCD.Trim(),
                (object) dirty.TaskCD.Trim()
              });
            nullable4 = account.AccountGroupID;
          }
          else
            PXTrace.WriteWarning("Project preferences have been configured to get the expense account from project tasks, but the default cost account is not specified for the {0} task of the {1} project.", (object) contract.ContractCD.Trim(), (object) dirty.TaskCD.Trim());
        }
        else if (this.ExpenseAccountSource == "E")
        {
          nullable1 = epEmployee.ExpenseAcctID;
          if (nullable1.HasValue)
          {
            accountID = epEmployee.ExpenseAcctID;
            PX.Objects.GL.Account account = PX.Objects.GL.Account.PK.Find((PXGraph) this, accountID);
            nullable1 = account.AccountGroupID;
            if (!nullable1.HasValue)
              throw new PXException("Project preferences have been configured to get the expense account from resources, but the {0} expense account of the {1} employee is not mapped to any account group.", new object[2]
              {
                (object) account.AccountCD,
                (object) epEmployee.AcctCD.Trim()
              });
            nullable4 = account.AccountGroupID;
          }
          else
            PXTrace.WriteWarning("Project preferences have been configured to get the expense account from employees, but the expense account is not specified for the {0} employee.", (object) epEmployee.AcctCD.Trim());
        }
        else
        {
          PX.Objects.GL.Account account = accountID.HasValue ? PX.Objects.GL.Account.PK.Find((PXGraph) this, accountID) : throw new PXException("Project preferences have been configured to get the expense account from inventory items, but the expense account is not specified for the {0} inventory item.", new object[1]
          {
            (object) inventoryItem.InventoryCD.Trim()
          });
          nullable1 = account.AccountGroupID;
          if (!nullable1.HasValue)
            throw new PXException("Project preferences have been configured to get the expense account from inventory items, but the {0} expense account of the {1} inventory item is not mapped to any account group.", new object[2]
            {
              (object) account.AccountCD.Trim(),
              (object) inventoryItem.InventoryCD.Trim()
            });
          nullable4 = account.AccountGroupID;
        }
        if (!nullable4.HasValue)
        {
          PX.Objects.GL.Account account = PX.Objects.GL.Account.PK.Find((PXGraph) this, accountID);
          nullable1 = account.AccountGroupID;
          if (!nullable1.HasValue)
            throw new PXException("Expense Account '{0}' is not included in any Account Group. Please assign an Account Group given Account and try again.", new object[1]
            {
              (object) account.AccountCD.Trim()
            });
          nullable4 = account.AccountGroupID;
        }
        if (!string.IsNullOrEmpty(this.ExpenseSubMask))
        {
          if (this.ExpenseSubMask.Contains("I"))
          {
            nullable1 = inventoryItem.COGSSubID;
            if (!nullable1.HasValue)
              throw new PXException("Project preferences have been configured to combine the expense subaccount from inventory items, but the expense subaccount is not specified for the {0} inventory item.", new object[1]
              {
                (object) inventoryItem.InventoryCD.Trim()
              });
          }
          if (this.ExpenseSubMask.Contains("P"))
          {
            nullable1 = contract.DefaultExpenseSubID;
            if (!nullable1.HasValue)
              throw new PXException("Project preferences have been configured to combine the expense subaccount from projects, but the expense subaccount is not specified for the {0} project.", new object[1]
              {
                (object) contract.ContractCD.Trim()
              });
          }
          if (this.ExpenseSubMask.Contains("T"))
          {
            nullable1 = dirty.DefaultExpenseSubID;
            if (!nullable1.HasValue)
              throw new PXException("Project preferences have been configured to combine the expense subaccount from project tasks, but the expense subaccount is not specified for the {1} task of the {0} project.", new object[2]
              {
                (object) contract.ContractCD.Trim(),
                (object) dirty.TaskCD.Trim()
              });
          }
          if (this.ExpenseSubMask.Contains("E"))
          {
            nullable1 = epEmployee.ExpenseSubID;
            if (!nullable1.HasValue)
              throw new PXException("Project preferences have been configured to combine the expense subaccount from resources, but the expense subaccount is not specified for the {0} employee.", new object[1]
              {
                (object) epEmployee.AcctCD.Trim()
              });
          }
          str1 = PX.Objects.PM.SubAccountMaskAttribute.MakeSub<PMSetup.expenseSubMask>((PXGraph) this, this.ExpenseSubMask, new object[4]
          {
            (object) inventoryItem.COGSSubID,
            (object) contract.DefaultExpenseSubID,
            (object) dirty.DefaultExpenseSubID,
            (object) epEmployee.ExpenseSubID
          }, new System.Type[4]
          {
            typeof (PX.Objects.IN.InventoryItem.cOGSSubID),
            typeof (PX.Objects.CT.Contract.defaultExpenseSubID),
            typeof (PMTask.defaultExpenseSubID),
            typeof (EPEmployee.expenseSubID)
          });
        }
        if (this.ExpenseAccrualAccountSource == "P")
        {
          nullable1 = contract.DefaultAccrualAccountID;
          if (nullable1.HasValue)
            nullable3 = contract.DefaultAccrualAccountID;
          else
            PXTrace.WriteWarning("Project preferences have been configured to get the expense accrual account from projects, but the default accrual account is not specified for the {0} project.", (object) contract.ContractCD.Trim());
        }
        else if (this.ExpenseAccrualAccountSource == "T")
        {
          nullable1 = dirty.DefaultAccrualAccountID;
          if (nullable1.HasValue)
            nullable3 = dirty.DefaultAccrualAccountID;
          else
            PXTrace.WriteWarning("Project preferences have been configured to get the expense account from project tasks, but the default cost account is not specified for the {0} task of the {1} project.", (object) contract.ContractCD.Trim(), (object) dirty.TaskCD.Trim());
        }
        else
        {
          nullable1 = inventoryItem.InvtAcctID;
          if (nullable1.HasValue && !nullable3.HasValue)
            throw new PXException("Project preferences have been configured to get the expense accrual account from inventory items, but the expense accrual account is not specified for the {0} inventory item.", new object[1]
            {
              (object) inventoryItem.InventoryCD.Trim()
            });
        }
        if (!string.IsNullOrEmpty(this.ExpenseAccrualSubMask))
        {
          if (!flag && this.ExpenseAccrualSubMask.Contains("I"))
          {
            nullable1 = inventoryItem.InvtSubID;
            if (!nullable1.HasValue)
              throw new PXException("Project preferences have been configured to combine the expense accrual subaccount from inventory items, but the expense accrual subaccount is not specified for the {0} inventory item.", new object[1]
              {
                (object) inventoryItem.InventoryCD.Trim()
              });
          }
          if (this.ExpenseAccrualSubMask.Contains("P"))
          {
            nullable1 = contract.DefaultAccrualSubID;
            if (!nullable1.HasValue)
              throw new PXException("Project preferences have been configured to combine the expense accrual subaccount from projects, but the expense accrual subaccount is not specified for the {0} project.", new object[1]
              {
                (object) contract.ContractCD.Trim()
              });
          }
          if (this.ExpenseAccrualSubMask.Contains("T"))
          {
            nullable1 = dirty.DefaultAccrualSubID;
            if (!nullable1.HasValue)
              throw new PXException("Project preferences have been configured to combine the expense accrual subaccount from project tasks, but the expense accrual subaccount is not specified for the {1} task of the {0} project.", new object[2]
              {
                (object) contract.ContractCD.Trim(),
                (object) dirty.TaskCD.Trim()
              });
          }
          if (this.ExpenseAccrualSubMask.Contains("E"))
          {
            nullable1 = epEmployee.ExpenseSubID;
            if (!nullable1.HasValue)
              throw new PXException("Project preferences have been configured to combine the expense subaccount from resources, but the expense subaccount is not specified for the {0} employee.", new object[1]
              {
                (object) epEmployee.AcctCD.Trim()
              });
          }
          nullable1 = inventoryItem.InvtSubID;
          if (nullable1.HasValue)
            str2 = PX.Objects.PM.SubAccountMaskAttribute.MakeSub<PMSetup.expenseAccrualSubMask>((PXGraph) this, this.ExpenseAccrualSubMask, new object[4]
            {
              (object) inventoryItem.InvtSubID,
              (object) contract.DefaultAccrualSubID,
              (object) dirty.DefaultAccrualSubID,
              (object) epEmployee.ExpenseSubID
            }, new System.Type[4]
            {
              typeof (PX.Objects.IN.InventoryItem.invtSubID),
              typeof (PX.Objects.CT.Contract.defaultAccrualSubID),
              typeof (PMTask.defaultAccrualSubID),
              typeof (EPEmployee.expenseSubID)
            });
        }
      }
      else
      {
        PX.Objects.GL.Account account = PX.Objects.GL.Account.PK.Find((PXGraph) this, accountID);
        nullable1 = account.AccountGroupID;
        if (nullable1.HasValue)
          nullable4 = account.AccountGroupID;
      }
    }
    int? nullable5 = inventoryItem.COGSSubID;
    int? nullable6 = inventoryItem.InvtSubID;
    if (flag)
    {
      nullable4 = EPSetupMaint.GetOffBalancePostingAccount((PXGraph) this, this.EpSetup.Current, employeeID);
      accountID = new int?();
      nullable3 = new int?();
      nullable6 = new int?();
      str1 = (string) null;
      nullable5 = new int?();
    }
    PMTran pmTran = (PMTran) tranCache.Insert();
    pmTran.BranchID = branchID;
    pmTran.AccountID = accountID;
    if (string.IsNullOrEmpty(str1))
      pmTran.SubID = nullable5;
    if (string.IsNullOrEmpty(str2))
      pmTran.OffsetSubID = nullable6;
    pmTran.AccountGroupID = nullable4;
    pmTran.ProjectID = record.ProjectID;
    pmTran.TaskID = record.TaskID;
    pmTran.CostCodeID = record.CostCodeID;
    pmTran.InventoryID = record.InventoryID;
    pmTran.ResourceID = employeeID;
    pmTran.Date = date;
    FinPeriod finPeriodByDate = this.FinPeriodRepository.GetFinPeriodByDate(pmTran.Date, PXAccess.GetParentOrganizationID(branchID));
    pmTran.FinPeriodID = finPeriodByDate?.FinPeriodID;
    pmTran.Qty = qty;
    pmTran.UOM = record.UOM;
    pmTran.Billable = new bool?(true);
    pmTran.BillableQty = pmTran.Qty;
    pmTran.TranCuryUnitRate = new Decimal?(PXDBPriceCostAttribute.Round(nullable2.Value));
    pmTran.Amount = new Decimal?();
    pmTran.Description = record.Description;
    pmTran.OffsetAccountID = nullable3;
    pmTran.IsQtyOnly = new bool?(contract.BaseType == "C");
    PMTran itemTransaction = (PMTran) tranCache.Update((object) pmTran);
    if (!string.IsNullOrEmpty(str1))
      tranCache.SetValueExt<PMTran.subID>((object) itemTransaction, (object) str1);
    if (!string.IsNullOrEmpty(str2))
      tranCache.SetValueExt<PMTran.offsetSubID>((object) itemTransaction, (object) str2);
    PXNoteAttribute.CopyNoteAndFiles(this.Items.Cache, (object) record, tranCache, (object) itemTransaction);
    return itemTransaction;
  }

  public virtual EmployeeCostEngine CreateEmployeeCostEngine()
  {
    return new EmployeeCostEngine((PXGraph) this);
  }

  protected PXCache CreateInstanceCache<TNode>(System.Type graphType) where TNode : IBqlTable
  {
    if (graphType != (System.Type) null)
    {
      PXGraph instance = PXGraph.CreateInstance(graphType);
      instance.Clear();
      foreach (System.Type cach1 in instance.Views.Caches)
      {
        PXCache cach2 = instance.Caches[cach1];
        if (typeof (TNode).IsAssignableFrom(cach2.GetItemType()))
          return cach2;
      }
    }
    return (PXCache) null;
  }

  public virtual bool IsFirstTimeCard(int? employeeID)
  {
    if (!employeeID.HasValue)
      return true;
    return PXSelectBase<EPTimecardLite, PXSelectReadonly<EPTimecardLite, Where<EPTimecardLite.employeeID, Equal<Required<EPTimecardLite.employeeID>>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, (object) employeeID).Count == 0;
  }

  public virtual int? GetNextWeekID(int? employeeID)
  {
    if (!this.IsFirstTimeCard(employeeID))
    {
      EPTimecardLite epTimecardLite = (EPTimecardLite) PXSelectBase<EPTimecardLite, PXSelectReadonly<EPTimecardLite, Where<EPTimecardLite.employeeID, Equal<Required<EPTimecardLite.employeeID>>>, PX.Data.OrderBy<Desc<EPTimecardLite.weekId>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, (object) employeeID);
      if (epTimecardLite != null)
      {
        int? weekId = epTimecardLite.WeekID;
        if (weekId.HasValue)
        {
          weekId = epTimecardLite.WeekID;
          return new int?(PXWeekSelector2Attribute.GetNextWeekID((PXGraph) this, weekId.Value));
        }
      }
    }
    return new int?(this.Accessinfo.BusinessDate.With<System.DateTime, int>((Func<System.DateTime, int>) (_ => PXWeekSelector2Attribute.GetWeekID((PXGraph) this, _))));
  }

  private int? GetCurrentDateWeekId()
  {
    return new int?(this.Accessinfo.BusinessDate.With<System.DateTime, int>((Func<System.DateTime, int>) (_ => PXWeekSelector2Attribute.GetWeekID((PXGraph) this, _))));
  }

  public virtual EPTimeCard GetLastCorrection(EPTimeCard source)
  {
    if (source.IsReleased.GetValueOrDefault())
    {
      EPTimeCard source1 = (EPTimeCard) this.Document.Search<EPTimeCard.origTimeCardCD>((object) source.TimeCardCD);
      if (source1 != null)
        return this.GetLastCorrection(source1);
    }
    return source;
  }

  public virtual void CheckTimeCardUsage(EPTimeCard timeCard)
  {
  }

  public bool CanSelectAllDetailsByTimeCardCD(EPTimeCard document)
  {
    return !document.IsHold.GetValueOrDefault() || document.IsApproved.GetValueOrDefault() || document.IsRejected.GetValueOrDefault() || document.IsReleased.GetValueOrDefault();
  }

  [Obsolete("The method is obsolete and will be removed in the later Acumatica versions.")]
  public virtual void ValidateDisplayVSReportedTimeZones(EPTimeCard timeCard)
  {
    PXTimeZoneInfo timeZone = LocaleInfo.GetTimeZone();
    int totalRows = 2;
    PXResultset<TimeCardMaint.EPTimecardDetail> resultSet;
    if (this.CanSelectAllDetailsByTimeCardCD(this.Document.Current))
      resultSet = PXSelectBase<TimeCardMaint.EPTimecardDetail, PXSelectGroupBy<TimeCardMaint.EPTimecardDetail, Where<TimeCardMaint.EPTimecardDetail.timeCardCD, Equal<Required<EPTimeCard.timeCardCD>>, And<PMTimeActivity.reportedInTimeZoneID, PX.Data.IsNotNull, And<PMTimeActivity.reportedInTimeZoneID, NotEqual<Required<PMTimeActivity.reportedInTimeZoneID>>>>>, PX.Data.Aggregate<GroupBy<PMTimeActivity.reportedInTimeZoneID>>, PX.Data.OrderBy<Asc<PMTimeActivity.reportedInTimeZoneID>>>.Config>.SelectWindowed((PXGraph) this, 0, totalRows, (object) timeCard.TimeCardCD, (object) timeZone?.Id);
    else
      resultSet = PXSelectBase<TimeCardMaint.EPTimecardDetail, PXSelectJoinGroupBy<TimeCardMaint.EPTimecardDetail, InnerJoin<CREmployee, On<CREmployee.defContactID, Equal<PMTimeActivity.ownerID>>>, Where<PMTimeActivity.reportedInTimeZoneID, PX.Data.IsNotNull, And<PMTimeActivity.reportedInTimeZoneID, NotEqual<Required<PMTimeActivity.reportedInTimeZoneID>>, And<CREmployee.bAccountID, Equal<Required<EPTimeCard.employeeID>>, And<TimeCardMaint.EPTimecardDetail.weekID, Equal<Required<EPTimeCard.weekId>>, And<PMTimeActivity.trackTime, Equal<True>, And<PMTimeActivity.approvalStatus, NotEqual<ActivityStatusListAttribute.canceled>, PX.Data.And<Where<TimeCardMaint.EPTimecardDetail.timeCardCD, PX.Data.IsNull, Or<TimeCardMaint.EPTimecardDetail.timeCardCD, Equal<Required<EPTimeCard.timeCardCD>>>>>>>>>>>, PX.Data.Aggregate<GroupBy<PMTimeActivity.reportedInTimeZoneID>>, PX.Data.OrderBy<Asc<PMTimeActivity.reportedInTimeZoneID>>>.Config>.SelectWindowed((PXGraph) this, 0, totalRows, (object) timeZone?.Id, (object) timeCard.EmployeeID, (object) timeCard.WeekID, (object) timeCard.TimeCardCD);
    TimeCardMaint.EPTimecardDetail[] array = resultSet.RowCast<TimeCardMaint.EPTimecardDetail>().ToArray<TimeCardMaint.EPTimecardDetail>();
    if (array.Length > 1)
    {
      this.Document.Cache.RaiseExceptionHandling<EPTimeCard.timeCardCD>((object) this.Document.Current, (object) this.Document.Current.TimeCardCD, (Exception) new PXSetPropertyException("Time is displayed in the time zone of the current user ({0}), which differs from at least one of the time zones the time card activities were reported in.", PXErrorLevel.Warning, new object[1]
      {
        (object) timeZone.DisplayName
      }));
    }
    else
    {
      if (array.Length != 1)
        return;
      string displayName = PXTimeZoneInfo.FindSystemTimeZoneById(((IEnumerable<TimeCardMaint.EPTimecardDetail>) array).Single<TimeCardMaint.EPTimecardDetail>().ReportedInTimeZoneID).DisplayName;
      this.Document.Cache.RaiseExceptionHandling<EPTimeCard.timeCardCD>((object) this.Document.Current, (object) this.Document.Current.TimeCardCD, (Exception) new PXSetPropertyException("Time is displayed in the time zone of the current user ({0}), which differs from the time zone the time card activities were reported in ({1}).", PXErrorLevel.Warning, new object[2]
      {
        (object) timeZone.DisplayName,
        (object) displayName
      }));
    }
  }

  public bool PrepareImportRow(string viewName, IDictionary keys, IDictionary values) => true;

  public bool RowImporting(string viewName, object row) => row == null;

  public bool RowImported(string viewName, object row, object oldRow) => oldRow == null;

  public void PrepareItems(string viewName, IEnumerable items)
  {
  }

  public class DayActivities
  {
    public List<TimeCardMaint.EPTimecardDetail> Activities;
    public DayOfWeek Day;

    public DayActivities() => this.Activities = new List<TimeCardMaint.EPTimecardDetail>();

    public int GetTotalTime(int summaryLineNbr)
    {
      int totalTime = 0;
      foreach (TimeCardMaint.EPTimecardDetail activity in this.Activities)
      {
        int? nullable = activity.SummaryLineNbr;
        if (nullable.HasValue)
        {
          nullable = activity.SummaryLineNbr;
          int num = summaryLineNbr;
          if (!(nullable.GetValueOrDefault() == num & nullable.HasValue))
            continue;
        }
        int num1 = totalTime;
        nullable = activity.TimeSpent;
        int valueOrDefault = nullable.GetValueOrDefault();
        totalTime = num1 + valueOrDefault;
      }
      return totalTime;
    }

    public TimeCardMaint.EPTimecardDetail GetAdjustingActivity(int summaryLineNbr)
    {
      foreach (TimeCardMaint.EPTimecardDetail activity in this.Activities)
      {
        int? summaryLineNbr1 = activity.SummaryLineNbr;
        int num = summaryLineNbr;
        if (summaryLineNbr1.GetValueOrDefault() == num & summaryLineNbr1.HasValue && !activity.Released.GetValueOrDefault())
          return activity;
      }
      return this.Activities.Count == 1 && !this.Activities[0].SummaryLineNbr.HasValue && !this.Activities[0].Released.GetValueOrDefault() ? this.Activities[0] : (TimeCardMaint.EPTimecardDetail) null;
    }
  }

  protected class SummaryRecord
  {
    public SummaryRecord(TimeCardMaint.EPTimeCardSummaryWithInfo summary)
    {
      this.Summary = summary;
      this.LinkedDetails = new List<TimeCardMaint.EPTimecardDetail>();
      this.NotLinkedDetails = new List<TimeCardMaint.EPTimecardDetail>();
    }

    public TimeCardMaint.EPTimeCardSummaryWithInfo Summary { get; private set; }

    public string SummaryKey { get; set; }

    public List<TimeCardMaint.EPTimecardDetail> LinkedDetails { get; private set; }

    public List<TimeCardMaint.EPTimecardDetail> NotLinkedDetails { get; private set; }

    public EPTimeCardSummary SummariseDetails()
    {
      EPTimeCardSummary summary = new EPTimeCardSummary();
      if (this.Summary != null)
      {
        summary.TimeCardCD = this.Summary.TimeCardCD;
        summary.LineNbr = this.Summary.LineNbr;
      }
      foreach (TimeCardMaint.EPTimecardDetail linkedDetail in this.LinkedDetails)
        this.AddActivityTimeToSummary(summary, (PMTimeActivity) linkedDetail, 1);
      foreach (TimeCardMaint.EPTimecardDetail notLinkedDetail in this.NotLinkedDetails)
        this.AddActivityTimeToSummary(summary, (PMTimeActivity) notLinkedDetail, 1);
      return summary;
    }

    public TimeCardMaint.SummaryRecord.SummaryRecordInfo GetInfo()
    {
      TimeCardMaint.SummaryRecord.SummaryRecordInfo info = new TimeCardMaint.SummaryRecord.SummaryRecordInfo();
      Decimal? nullable1 = new Decimal?();
      bool flag = false;
      foreach (TimeCardMaint.EPTimecardDetail epTimecardDetail in this.LinkedDetails.Union<TimeCardMaint.EPTimecardDetail>((IEnumerable<TimeCardMaint.EPTimecardDetail>) this.NotLinkedDetails))
      {
        int? nullable2 = epTimecardDetail.SummaryLineNbr;
        if (!nullable2.HasValue)
          info.HasManualDetails = true;
        if (epTimecardDetail.ApprovalStatus == "OP")
          info.HasOpen = true;
        if (epTimecardDetail.ApprovalStatus == "PA")
          info.HasCompleted = true;
        if (epTimecardDetail.ApprovalStatus == "AP")
          info.HasApproved = true;
        if (epTimecardDetail.ApprovalStatus == "RJ")
          info.HasRejected = true;
        nullable2 = epTimecardDetail.ApproverID;
        if (nullable2.HasValue)
          info.ApprovalRequired = true;
        Decimal? nullable3;
        if (!nullable1.HasValue)
        {
          nullable3 = epTimecardDetail.EmployeeRate;
          if (nullable3.HasValue)
          {
            nullable1 = epTimecardDetail.EmployeeRate;
            continue;
          }
        }
        if (nullable1.HasValue)
        {
          nullable3 = epTimecardDetail.EmployeeRate;
          if (nullable3.HasValue)
          {
            nullable3 = nullable1;
            Decimal? employeeRate = epTimecardDetail.EmployeeRate;
            if (!(nullable3.GetValueOrDefault() == employeeRate.GetValueOrDefault() & nullable3.HasValue == employeeRate.HasValue))
              flag = true;
          }
        }
      }
      info.Rate = nullable1;
      info.ContainsMixedRates = flag;
      return info;
    }

    private void AddActivityTimeToSummary(
      EPTimeCardSummary summary,
      PMTimeActivity activity,
      int mult)
    {
      if (!activity.TimeSpent.HasValue)
        return;
      System.DateTime? nullable = activity.ReportedOnDate;
      if (!nullable.HasValue)
        nullable = new System.DateTime?(PXDateAndTimeWithTimeZoneAttribute.GetTimeZoneAdjustedActivityDate(activity.Date.Value, activity.ReportedInTimeZoneID));
      DayOfWeek? dayOfWeek = nullable?.DayOfWeek;
      if (!dayOfWeek.HasValue)
        return;
      switch (dayOfWeek.GetValueOrDefault())
      {
        case DayOfWeek.Sunday:
          summary.Sun = new int?(summary.Sun.GetValueOrDefault() + mult * activity.TimeSpent.Value);
          break;
        case DayOfWeek.Monday:
          summary.Mon = new int?(summary.Mon.GetValueOrDefault() + mult * activity.TimeSpent.Value);
          break;
        case DayOfWeek.Tuesday:
          summary.Tue = new int?(summary.Tue.GetValueOrDefault() + mult * activity.TimeSpent.Value);
          break;
        case DayOfWeek.Wednesday:
          summary.Wed = new int?(summary.Wed.GetValueOrDefault() + mult * activity.TimeSpent.Value);
          break;
        case DayOfWeek.Thursday:
          summary.Thu = new int?(summary.Thu.GetValueOrDefault() + mult * activity.TimeSpent.Value);
          break;
        case DayOfWeek.Friday:
          summary.Fri = new int?(summary.Fri.GetValueOrDefault() + mult * activity.TimeSpent.Value);
          break;
        case DayOfWeek.Saturday:
          summary.Sat = new int?(summary.Sat.GetValueOrDefault() + mult * activity.TimeSpent.Value);
          break;
      }
    }

    public struct SummaryRecordInfo
    {
      public bool HasManualDetails { get; set; }

      public bool HasOpen { get; set; }

      public bool HasApproved { get; set; }

      public bool HasRejected { get; set; }

      public bool HasCompleted { get; set; }

      public bool ApprovalRequired { get; set; }

      public Decimal? Rate { get; set; }

      public bool ContainsMixedRates { get; set; }
    }
  }

  private class ReleasedActivity
  {
    public TimeCardMaint.EPTimecardDetail Activity { get; private set; }

    public Decimal Cost { get; private set; }

    public bool IsBilled { get; private set; }

    public ReleasedActivity(TimeCardMaint.EPTimecardDetail activity, Decimal cost, bool isBilled)
    {
      this.Activity = activity;
      this.Cost = cost;
      this.IsBilled = isBilled;
    }
  }

  /// <summary>Required for correct join</summary>
  [PXHidden]
  [Serializable]
  public class EPTimecardDetailOrig : TimeCardMaint.EPTimecardDetail
  {
    public new abstract class noteID : 
      BqlType<
      #nullable enable
      IBqlGuid, Guid>.Field<
      #nullable disable
      TimeCardMaint.EPTimecardDetailOrig.noteID>
    {
    }

    public new abstract class refNoteID : 
      BqlType<
      #nullable enable
      IBqlGuid, Guid>.Field<
      #nullable disable
      TimeCardMaint.EPTimecardDetailOrig.refNoteID>
    {
    }

    public new abstract class origNoteID : 
      BqlType<
      #nullable enable
      IBqlGuid, Guid>.Field<
      #nullable disable
      TimeCardMaint.EPTimecardDetailOrig.origNoteID>
    {
    }
  }

  /// <summary>Required for correct join</summary>
  [PXHidden]
  [Serializable]
  public class EPTimecardDetailEx : TimeCardMaint.EPTimecardDetail
  {
    public new abstract class noteID : 
      BqlType<
      #nullable enable
      IBqlGuid, Guid>.Field<
      #nullable disable
      TimeCardMaint.EPTimecardDetailEx.noteID>
    {
    }

    public new abstract class origNoteID : 
      BqlType<
      #nullable enable
      IBqlGuid, Guid>.Field<
      #nullable disable
      TimeCardMaint.EPTimecardDetailEx.origNoteID>
    {
    }
  }

  /// <summary>
  /// Specialized version of <see cref="T:PX.Objects.CR.PMTimeActivity" />.<br></br>
  /// Defines work hours for Employee Time Cards.<br></br>
  /// Includes detailed information on the work hours the employee has spent on various activities that may be
  /// associated with particular projects and project tasks.
  /// </summary>
  [PXBreakInheritance]
  [PXProjection(typeof (Select2<PMTimeActivity, LeftJoin<CRActivityLink, On<CRActivityLink.noteID, Equal<PMTimeActivity.refNoteID>>, LeftJoin<CRCase, On<CRCase.noteID, Equal<CRActivityLink.refNoteID>>, LeftJoin<TimeCardMaint.ContractEx, On<TimeCardMaint.ContractEx.contractID, Equal<CRCase.contractID>>, LeftJoin<PMProject, On<PMProject.contractID, Equal<PMTimeActivity.projectID>>>>>>>), new System.Type[] {typeof (PMTimeActivity)})]
  [Serializable]
  public class EPTimecardDetail : PMTimeActivity
  {
    [PXDBSequentialGuid(IsKey = true)]
    public override Guid? NoteID { get; set; }

    /// <summary>
    /// The identifier of the related <see cref="T:PX.Objects.CR.CRActivity" />.
    /// </summary>
    /// <value>
    /// Corresponds to the value of the <see cref="!:CRActivity.NoteID" /> field.
    /// </value>
    [PXSequentialSelfRefNote(SuppressActivitiesCount = true, NoteField = typeof (TimeCardMaint.EPTimecardDetail.noteID), Persistent = true)]
    [PXUIField(Visible = false)]
    [PXParent(typeof (PX.Data.Select<PX.Objects.CR.CRActivity, Where<PX.Objects.CR.CRActivity.noteID, Equal<Current<TimeCardMaint.EPTimecardDetail.refNoteID>>>>), ParentCreate = true)]
    public override Guid? RefNoteID { get; set; }

    /// <summary>
    /// The <see cref="T:PX.Objects.CR.CRActivityClass">task</see> associated with this activity.<br></br>
    /// Only tasks that belong to the selected employee are available for selection.
    /// </summary>
    [PXDBGuid(false)]
    [PXDBDefault(null, PersistingCheck = PXPersistingCheck.Nothing)]
    [CRTaskSelector]
    [PXRestrictor(typeof (Where<PX.Objects.CR.CRActivity.classID, Equal<CRActivityClass.task>, And<PX.Objects.CR.CRActivity.ownerID, Equal<Current<AccessInfo.contactID>>>>), null, new System.Type[] {})]
    [PXUIField(DisplayName = "Task")]
    public override Guid? ParentTaskNoteID { get; set; }

    /// <summary>
    /// The <see cref="T:PX.Objects.EP.EPTimeCard">Time Card</see> associated with this activity.<br></br>
    /// </summary>
    [PXDBString(10)]
    [PXUIField(Visible = false)]
    public override string TimeCardCD { get; set; }

    /// <summary>
    /// The <see cref="T:PX.Objects.PM.PMProject">project</see> on which the employee worked.<br></br>
    /// Available only if the Project Accounting feature is enabled.
    /// </summary>
    [ProjectDefault("TA", ForceProjectExplicitly = true)]
    [EPTimeCardProject]
    public override int? ProjectID { get; set; }

    /// <summary>
    /// The <see cref="T:PX.Objects.PM.PMTask">task</see> on which the employee worked. <br></br>
    /// Canceled project tasks cannot be used in time cards. If you select a project that has the default project task, this task is automatically populated. <br></br>
    /// Only available if the Project Accounting feature is enabled. <br></br>
    /// </summary>
    /// <value>The project task identifier.</value>
    [PXDefault(typeof (Search<PMTask.taskID, Where<PMTask.projectID, Equal<Current<TimeCardMaint.EPTimecardDetail.projectID>>, And<PMTask.isDefault, Equal<True>>>>), PersistingCheck = PXPersistingCheck.Nothing)]
    [ProjectTask(typeof (TimeCardMaint.EPTimecardDetail.projectID), "TA", DisplayName = "Project Task")]
    [PXForeignReference(typeof (CompositeKey<PX.Data.ReferentialIntegrity.Attributes.Field<TimeCardMaint.EPTimecardDetail.projectID>.IsRelatedTo<PMTask.projectID>, PX.Data.ReferentialIntegrity.Attributes.Field<TimeCardMaint.EPTimecardDetail.projectTaskID>.IsRelatedTo<PMTask.taskID>>))]
    public override int? ProjectTaskID { get; set; }

    /// <summary>
    /// The union local associated with the time entry.<br></br>
    /// The system automatically populates the Union Local column in the time card line by using the employee and project settings as follows:<br></br>
    /// <list type="bullet">
    /// 	<item>If the employee has an associated union local and the project does not, the system inserts the union local of the employee.</item>
    /// 	<item>If the employee has an associated union local that is one of the union locals associated with the project, the system inserts the union local of the employee.</item>
    /// 	<item>If the employee has an associated union local that is not one of the union locals associated with the project, the system leaves the column blank.</item>
    /// 	<item>If the employee has no associated union local, the system leaves the column blank.</item>
    /// </list>
    /// You can clear this property if a union local has been selected or select a union local manually, if needed.<br></br>
    /// The property is available only when the Construction feature is enabled.
    /// </summary>
    /// <value>
    /// The identifier of the <see cref="T:PX.Objects.PM.PMUnion">union local</see> associated with the activity.
    /// </value>
    [PMUnion(typeof (TimeCardMaint.EPTimecardDetail.projectID), typeof (PX.Data.Select<EPEmployee, Where<EPEmployee.bAccountID, Equal<Current<EPTimeCard.employeeID>>>>))]
    public override string UnionID { get; set; }

    /// <summary>
    /// Unique identifier of the Week associated with this Time Card.
    /// </summary>
    /// <value>
    /// The value of this field corresponds to the value of the <see cref="P:PX.Objects.EP.EPWeekRaw.Description" /> field.
    /// </value>
    [PXDBInt]
    [PXUIField(DisplayName = "Time Card Week")]
    public override int? WeekID { get; set; }

    /// <summary>
    /// The identifier of the <see cref="T:PX.Objects.IN.InventoryItem">labor item</see> associated with the activity.
    /// </summary>
    /// <value>The labour item identifier.</value>
    [PMActiveLaborItem(typeof (TimeCardMaint.EPTimecardDetail.projectID), typeof (PMTimeActivity.earningTypeID), typeof (PX.Data.Select<EPEmployee, Where<EPEmployee.bAccountID, Equal<Current<EPTimeCard.employeeID>>>>), DescriptionField = typeof (PX.Objects.IN.InventoryItem.descr))]
    [PXForeignReference(typeof (PX.Data.ReferentialIntegrity.Attributes.Field<TimeCardMaint.EPTimecardDetail.labourItemID>.IsRelatedTo<PX.Objects.IN.InventoryItem.inventoryID>))]
    public override int? LabourItemID { get; set; }

    /// <summary>
    /// The identifier of the <see cref="T:PX.Objects.PM.PMCostCode">cost code</see> associated with the activity.
    /// </summary>
    /// <value>
    /// The value of this field corresponds to the value of the <see cref="T:PX.Objects.PM.PMCostCode.costCodeID" /> field.
    /// </value>
    [CostCode(null, typeof (TimeCardMaint.EPTimecardDetail.projectTaskID), "E", ProjectField = typeof (TimeCardMaint.EPTimecardDetail.projectID), InventoryField = typeof (TimeCardMaint.EPTimecardDetail.labourItemID), UseNewDefaulting = true, ReleasedField = typeof (PMTimeActivity.released))]
    public override int? CostCodeID { get; set; }

    /// <summary>
    /// The work class compensation code (WCC Code) associated with the time card line.
    /// </summary>
    /// <value>
    /// The <see cref="P:PX.Objects.CR.PMTimeActivity.WorkCodeID">work code</see> associated with this activity.
    /// </value>
    [PXForeignReference(typeof (PMTimeActivity.FK.WorkCode))]
    [PMWorkCodeInTimeActivity(typeof (TimeCardMaint.EPTimecardDetail.costCodeID), typeof (TimeCardMaint.EPTimecardDetail.projectID), typeof (TimeCardMaint.EPTimecardDetail.projectTaskID), typeof (TimeCardMaint.EPTimecardDetail.labourItemID), typeof (PMTimeActivity.ownerID))]
    public override string WorkCodeID { get; set; }

    /// <summary>The work time reported for the selected date.</summary>
    [PXTimeList]
    [PXDefault(0)]
    [PXDBInt]
    [PXUIField(DisplayName = "Time Spent")]
    public override int? TimeSpent { get; set; }

    /// <summary>Indicates if the work hours are billable.</summary>
    [PXDBBool]
    [PXUIField(DisplayName = "Billable")]
    public override bool? IsBillable { get; set; }

    /// <summary>The billable time for the employee.</summary>
    [PXTimeList]
    [PXDefault(0)]
    [PXDBInt]
    [PXUIEnabled(typeof (TimeCardMaint.EPTimecardDetail.isBillable))]
    [PXUIVerify(typeof (Where<TimeCardMaint.EPTimecardDetail.timeSpent, PX.Data.IsNull, Or<TimeCardMaint.EPTimecardDetail.timeBillable, PX.Data.IsNull, Or<TimeCardMaint.EPTimecardDetail.timeSpent, GreaterEqual<TimeCardMaint.EPTimecardDetail.timeBillable>, Or<TimeCardMaint.EPTimecardDetail.isBillable, Equal<False>>>>>), PXErrorLevel.Error, "Time Billable cannot be greater than Time Spent.", new System.Type[] {})]
    [PXFormula(typeof (Switch<Case<Where<TimeCardMaint.EPTimecardDetail.isBillable, Equal<True>>, TimeCardMaint.EPTimecardDetail.timeSpent, Case<Where<TimeCardMaint.EPTimecardDetail.isBillable, Equal<False>>, PX.Objects.CS.int0>>, TimeCardMaint.EPTimecardDetail.timeBillable>))]
    [PXUIField(DisplayName = "Billable Time", FieldClass = "BILLABLE")]
    public override int? TimeBillable { get; set; }

    /// <summary>
    /// The date of the record. <br></br>
    /// The default date is defined in <see cref="T:PX.Objects.EP.EPEmployeeClass" />.
    /// </summary>
    [PXDBDateAndTime(DisplayNameDate = "Date", DisplayNameTime = "Time", UseTimeZone = true)]
    [PXUIField(DisplayName = "Date")]
    [PXFormula(typeof (IsNull<Current<PX.Objects.CR.CRActivity.startDate>, Current<CRSMEmail.startDate>>))]
    public override System.DateTime? Date { get; set; }

    /// <summary>
    /// The work shift during which the activity was performed.<br></br>
    /// The default shift code is defined in <see cref="T:PX.Objects.EP.EPEmployee" />.<br></br>
    /// This column appears only if the Shift Differential feature is enabled.
    /// </summary>
    [PXDBInt]
    [PXUIField(DisplayName = "Shift Code", FieldClass = "ShiftDifferential")]
    [TimeActivityShiftCodeSelector(typeof (PMTimeActivity.ownerID), typeof (TimeCardMaint.EPTimecardDetail.date))]
    [EPShiftCodeActiveRestrictor]
    public override int? ShiftID { get; set; }

    [PXDBTimestamp(VerifyTimestamp = VerifyTimestampOptions.FromRecord)]
    public override byte[] tstamp { get; set; }

    /// <summary>
    /// The user-friendly unique identifier of the <see cref="T:PX.Objects.CR.CRCase">Case</see> associated with this activity.
    /// </summary>
    [PXDBString(15, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC", BqlField = typeof (CRCase.caseCD))]
    [PXUIField(DisplayName = "Case ID", Visibility = PXUIVisibility.SelectorVisible, Enabled = false)]
    [PXSelector(typeof (Search3<CRCase.caseCD, LeftJoin<PX.Objects.CR.BAccount, On<PX.Objects.CR.BAccount.bAccountID, Equal<CRCase.customerID>>>, PX.Data.OrderBy<Desc<CRCase.caseCD>>>), new System.Type[] {typeof (CRCase.caseCD), typeof (CRCase.subject), typeof (CRCase.status), typeof (CRCase.priority), typeof (CRCase.severity), typeof (CRCase.caseClassID), typeof (PX.Objects.CR.BAccount.acctName)}, Filterable = true)]
    public virtual string CaseCD { get; set; }

    /// <summary>
    /// The user-friendly unique identifier of the <see cref="T:PX.Objects.CT.Contract">Contract</see> associated with this activity.
    /// </summary>
    [PXDimensionSelector("CONTRACT", typeof (Search2<TimeCardMaint.EPTimecardDetail.contractCD, InnerJoin<ContractBillingSchedule, On<PMTimeActivity.contractID, Equal<ContractBillingSchedule.contractID>>, LeftJoin<PX.Objects.AR.Customer, On<PX.Objects.AR.Customer.bAccountID, Equal<TimeCardMaint.ContractEx.customerID>>>>, Where<TimeCardMaint.ContractEx.baseType, Equal<CTPRType.contract>>>), typeof (TimeCardMaint.EPTimecardDetail.contractCD), new System.Type[] {typeof (TimeCardMaint.EPTimecardDetail.contractCD), typeof (TimeCardMaint.ContractEx.customerID), typeof (PX.Objects.AR.Customer.acctName), typeof (PX.Objects.CT.Contract.locationID), typeof (TimeCardMaint.ContractEx.description), typeof (TimeCardMaint.ContractEx.status), typeof (TimeCardMaint.ContractEx.expireDate), typeof (ContractBillingSchedule.lastDate), typeof (ContractBillingSchedule.nextDate)}, DescriptionField = typeof (TimeCardMaint.ContractEx.description), Filterable = true)]
    [PXDBString(IsUnicode = true, InputMask = "", BqlField = typeof (TimeCardMaint.ContractEx.contractCD))]
    [PXUIField(DisplayName = "Contract ID", Visibility = PXUIVisibility.SelectorVisible, Enabled = false)]
    public virtual string ContractCD { get; set; }

    /// <summary>The workgroup to which the employee belongs.</summary>
    [PXDBInt]
    [PXUIField(DisplayName = "Workgroup")]
    [PXWorkgroupSelector]
    [PXParent(typeof (PX.Data.Select<EPTimeActivitiesSummary, Where<EPTimeActivitiesSummary.workgroupID, Equal<Current<TimeCardMaint.EPTimecardDetail.workgroupID>>, And<EPTimeActivitiesSummary.week, Equal<Current<TimeCardMaint.EPTimecardDetail.weekID>>, And<EPTimeActivitiesSummary.contactID, Equal<Current<PMTimeActivity.ownerID>>>>>>), ParentCreate = true, LeaveChildren = true)]
    [PXDefault(typeof (SearchFor<EPEmployee.defaultWorkgroupID>.Where<BqlOperand<EPEmployee.defaultWorkgroupID, IBqlInt>.IsEqual<BqlField<EPEmployee.defaultWorkgroupID, IBqlInt>.FromCurrent>>), PersistingCheck = PXPersistingCheck.Nothing)]
    public override int? WorkgroupID { get; set; }

    /// <summary>
    /// The day of the week for which the activity has been reported.<br></br>
    /// This is a read-only calculated field.<br></br>
    /// Possible values:
    /// Sunday, Monday, Tuesday, Wednesday, Thursday, Friday, Saturday.
    /// </summary>
    [PXString]
    [PXUIField(DisplayName = "Day")]
    public virtual string Day { get; set; }

    /// <summary>
    /// Indicates whether this work activity is associated with an overtime earning type, as defined by the <see cref="P:PX.Objects.CR.PMTimeActivity.EarningTypeID" /> property.<br></br>
    /// This is a read-only calculated field.
    /// </summary>
    [PXBool]
    [PXDefault(false, PersistingCheck = PXPersistingCheck.Nothing)]
    public virtual bool? IsOvertimeCalc { get; set; }

    /// <summary>The overtime multiplier.</summary>
    /// <value>
    /// The value by which the employee cost for this earning type is multiplied when the time activity is released.
    /// </value>
    [PXDecimal(2)]
    [PXUIField(DisplayName = "OT Mult", Enabled = false)]
    public virtual Decimal? OvertimeMultiplierCalc { get; set; }

    /// <summary>The billable time for the employee.</summary>
    [PXTimeList]
    [PXInt]
    [PXUIField(DisplayName = "Billable Time")]
    public virtual int? BillableTimeCalc { get; set; }

    [PXTimeList]
    [PXInt]
    [PXUIField(DisplayName = "Billable OT")]
    public virtual int? BillableOvertimeCalc { get; set; }

    /// <summary>
    /// The regular work time the employee spent on the project and task.
    /// </summary>
    [PXTimeList]
    [PXInt]
    [PXUIField(DisplayName = "RH", Enabled = false)]
    public virtual int? RegularTimeCalc { get; set; }

    /// <summary>
    /// Represents the time spent for overtime items.<br></br>
    /// This is a read-only calculated field.
    /// </summary>
    /// <value>
    /// For overtime items, the value is taken from the <see cref="P:PX.Objects.EP.TimeCardMaint.EPTimecardDetail.TimeSpent" /> property.
    /// </value>
    [PXTimeList]
    [PXInt]
    [PXUIField(DisplayName = "OT", Enabled = false)]
    public virtual int? OverTimeCalc { get; set; }

    public new abstract class noteID : 
      BqlType<
      #nullable enable
      IBqlGuid, Guid>.Field<
      #nullable disable
      TimeCardMaint.EPTimecardDetail.noteID>
    {
    }

    public new abstract class refNoteID : 
      BqlType<
      #nullable enable
      IBqlGuid, Guid>.Field<
      #nullable disable
      TimeCardMaint.EPTimecardDetail.refNoteID>
    {
    }

    public new abstract class parentTaskNoteID : 
      BqlType<
      #nullable enable
      IBqlGuid, Guid>.Field<
      #nullable disable
      TimeCardMaint.EPTimecardDetail.parentTaskNoteID>
    {
    }

    public new abstract class timeCardCD : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      TimeCardMaint.EPTimecardDetail.timeCardCD>
    {
    }

    public new abstract class projectID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      TimeCardMaint.EPTimecardDetail.projectID>
    {
    }

    public new abstract class projectTaskID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      TimeCardMaint.EPTimecardDetail.projectTaskID>
    {
    }

    public new abstract class unionID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      TimeCardMaint.EPTimecardDetail.unionID>
    {
    }

    public new abstract class weekID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      TimeCardMaint.EPTimecardDetail.weekID>
    {
    }

    public new abstract class labourItemID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      TimeCardMaint.EPTimecardDetail.labourItemID>
    {
    }

    public new abstract class costCodeID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      TimeCardMaint.EPTimecardDetail.costCodeID>
    {
    }

    public new abstract class workCodeID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      TimeCardMaint.EPTimecardDetail.workCodeID>
    {
    }

    public new abstract class timeSpent : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      TimeCardMaint.EPTimecardDetail.timeSpent>
    {
    }

    public new abstract class isBillable : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      TimeCardMaint.EPTimecardDetail.isBillable>
    {
    }

    public new abstract class timeBillable : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      TimeCardMaint.EPTimecardDetail.timeBillable>
    {
    }

    public new abstract class date : 
      BqlType<
      #nullable enable
      IBqlDateTime, System.DateTime>.Field<
      #nullable disable
      TimeCardMaint.EPTimecardDetail.date>
    {
    }

    public new abstract class shiftID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      TimeCardMaint.EPTimecardDetail.shiftID>
    {
    }

    public new abstract class Tstamp : 
      BqlType<
      #nullable enable
      IBqlByteArray, byte[]>.Field<
      #nullable disable
      TimeCardMaint.EPTimecardDetail.Tstamp>
    {
    }

    public abstract class caseCD : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      TimeCardMaint.EPTimecardDetail.caseCD>
    {
    }

    public abstract class contractCD : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      TimeCardMaint.EPTimecardDetail.contractCD>
    {
    }

    public new abstract class workgroupID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      TimeCardMaint.EPTimecardDetail.workgroupID>
    {
    }

    public abstract class day : BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    TimeCardMaint.EPTimecardDetail.day>
    {
    }

    public abstract class isOvertimeCalc : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      TimeCardMaint.EPTimecardDetail.isOvertimeCalc>
    {
    }

    public abstract class overtimeMultiplierCalc : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      TimeCardMaint.EPTimecardDetail.overtimeMultiplierCalc>
    {
    }

    public abstract class billableTimeCalc : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      TimeCardMaint.EPTimecardDetail.billableTimeCalc>
    {
    }

    /// <summary>The billable overtime for the employee.</summary>
    public abstract class billableOvertimeCalc : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      TimeCardMaint.EPTimecardDetail.billableOvertimeCalc>
    {
    }

    public abstract class regularTimeCalc : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      TimeCardMaint.EPTimecardDetail.regularTimeCalc>
    {
    }

    public abstract class overTimeCalc : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      TimeCardMaint.EPTimecardDetail.overTimeCalc>
    {
    }
  }

  /// <summary>
  /// Timecard Summary Record projection with additional fields from Project and Project Task.
  /// </summary>
  [PXCacheName("Time Card Summary")]
  [PXBreakInheritance]
  [PXProjection(typeof (Select2<EPTimeCardSummary, InnerJoin<PMProject, On<EPTimeCardSummary.projectID, Equal<PMProject.contractID>>, InnerJoin<PX.Objects.CT.Contract, On<EPTimeCardSummary.projectID, Equal<PX.Objects.CT.Contract.contractID>>, LeftJoin<PMTask, On<EPTimeCardSummary.projectID, Equal<PMTask.projectID>, And<EPTimeCardSummary.projectTaskID, Equal<PMTask.taskID>>>>>>>), new System.Type[] {typeof (EPTimeCardSummary)})]
  [Serializable]
  public class EPTimeCardSummaryWithInfo : EPTimeCardSummary
  {
    /// <summary>Gets sets ProjectID</summary>
    [ProjectDefault("TA", ForceProjectExplicitly = true)]
    [EPTimeCardProject]
    public override int? ProjectID { get; set; }

    /// <summary>Gets sets Project Task</summary>
    [PXDefault(typeof (Search<PMTask.taskID, Where<PMTask.projectID, Equal<Current<TimeCardMaint.EPTimeCardSummaryWithInfo.projectID>>, And<PMTask.isDefault, Equal<True>, And<PMTask.isCancelled, NotEqual<True>>>>>), PersistingCheck = PXPersistingCheck.Nothing)]
    [PXRestrictor(typeof (Where<PMTask.isCancelled, NotEqual<True>>), "Task is Canceled and cannot be used for data entry.", new System.Type[] {})]
    [EPTimecardProjectTask(typeof (TimeCardMaint.EPTimeCardSummaryWithInfo.projectID), "TA", DisplayName = "Project Task")]
    public override int? ProjectTaskID { get; set; }

    /// <summary>Gets sets Certified Job</summary>
    [PXDBBool]
    [PXDefault(typeof (Coalesce<Search<PMProject.certifiedJob, Where<PMProject.contractID, Equal<Current<TimeCardMaint.EPTimeCardSummaryWithInfo.projectID>>>>, Search<PMProject.certifiedJob, Where<PMProject.nonProject, Equal<True>>>>))]
    [PXUIField(DisplayName = "Certified Job", FieldClass = "Construction")]
    public override bool? CertifiedJob { get; set; }

    /// <summary>Gets sets Union</summary>
    [PMUnion(typeof (TimeCardMaint.EPTimeCardSummaryWithInfo.projectID), typeof (PX.Data.Select<EPEmployee, Where<EPEmployee.bAccountID, Equal<Current<EPTimeCard.employeeID>>>>))]
    public override string UnionID { get; set; }

    /// <summary>
    /// The non-stock item of the Labor type associated with the time card line.<br></br>
    /// By default, the system selects the labor item associated with the employee.
    /// </summary>
    [PMActiveLaborItem(typeof (TimeCardMaint.EPTimeCardSummaryWithInfo.projectID), typeof (EPTimeCardSummary.earningType), typeof (PX.Data.Select<EPEmployee, Where<EPEmployee.bAccountID, Equal<Current<EPTimeCard.employeeID>>>>), DescriptionField = typeof (PX.Objects.IN.InventoryItem.descr))]
    [PXForeignReference(typeof (PX.Data.ReferentialIntegrity.Attributes.Field<TimeCardMaint.EPTimeCardSummaryWithInfo.labourItemID>.IsRelatedTo<PX.Objects.IN.InventoryItem.inventoryID>))]
    public override int? LabourItemID { get; set; }

    /// <summary>Gets sets CostCode</summary>
    [CostCode(null, typeof (TimeCardMaint.EPTimeCardSummaryWithInfo.projectTaskID), "E", ProjectField = typeof (TimeCardMaint.EPTimeCardSummaryWithInfo.projectID), InventoryField = typeof (TimeCardMaint.EPTimeCardSummaryWithInfo.labourItemID), UseNewDefaulting = true)]
    public override int? CostCodeID { get; set; }

    /// <summary>Gets sets Work code</summary>
    [PMWorkCode(typeof (TimeCardMaint.EPTimeCardSummaryWithInfo.costCodeID), typeof (TimeCardMaint.EPTimeCardSummaryWithInfo.projectID), typeof (TimeCardMaint.EPTimeCardSummaryWithInfo.projectTaskID), typeof (TimeCardMaint.EPTimeCardSummaryWithInfo.labourItemID), typeof (EPTimeCardSummary.employeeID))]
    public override string WorkCodeID { get; set; }

    /// <summary>Gets sets Project's Description</summary>
    [PXUIField(DisplayName = "Project Description", IsReadOnly = true)]
    [PXDBLocalizableString(BqlField = typeof (PX.Objects.CT.Contract.description), IsProjection = true)]
    [PXDefault(typeof (Search<PMProject.description, Where<PMProject.contractID, Equal<Current<TimeCardMaint.EPTimeCardSummaryWithInfo.projectID>>>>))]
    [PXFormula(typeof (Default<TimeCardMaint.EPTimeCardSummaryWithInfo.projectID>))]
    public virtual string ProjectDescription { get; set; }

    /// <summary>Gets sets Project Manager</summary>
    [PXDBInt(BqlField = typeof (PMProject.approverID))]
    [PXUIField(DisplayName = "Project Manager", Visibility = PXUIVisibility.SelectorVisible)]
    public virtual int? ProjectManager { get; set; }

    /// <summary>Gets sets Project Task's description</summary>
    [PXUIField(DisplayName = "Project Task Description", IsReadOnly = true)]
    [PXDBLocalizableString(BqlField = typeof (PMTask.description), IsProjection = true)]
    [PXDefault(typeof (Search<PMTask.description, Where<PMTask.projectID, Equal<Current<TimeCardMaint.EPTimeCardSummaryWithInfo.projectID>>, And<PMTask.taskID, Equal<Current<TimeCardMaint.EPTimeCardSummaryWithInfo.projectTaskID>>>>>), PersistingCheck = PXPersistingCheck.Nothing)]
    [PXFormula(typeof (Default<TimeCardMaint.EPTimeCardSummaryWithInfo.projectID, TimeCardMaint.EPTimeCardSummaryWithInfo.projectTaskID>))]
    public virtual string ProjectTaskDescription { get; set; }

    [PXNote(BqlField = typeof (EPTimeCardSummary.noteID))]
    public override Guid? NoteID { get; set; }

    /// <summary>Gets sets Task Approver</summary>
    [PXDBInt(BqlField = typeof (PMTask.approverID))]
    [PXEPEmployeeSelector]
    public virtual int? TaskApproverID { get; set; }

    /// <summary>Gets sets Cost Code's description</summary>
    [PXUIField(DisplayName = "Cost Code Description", IsReadOnly = true, FieldClass = "COSTCODE")]
    [PXString]
    [PXFieldDescription]
    [PXUnboundDefault(typeof (Search<PMCostCode.description, Where<PMCostCode.costCodeID, Equal<Current<TimeCardMaint.EPTimeCardSummaryWithInfo.costCodeID>>>>))]
    [PXFormula(typeof (Default<TimeCardMaint.EPTimeCardSummaryWithInfo.costCodeID>))]
    public virtual string CostCodeDescription { get; set; }

    /// <summary>Gets sets Approval Status</summary>
    [PXString(2, IsFixed = true)]
    [ApprovalStatusList]
    [PXUIField(DisplayName = "Approval Status", Enabled = false)]
    public virtual string ApprovalStatus { get; set; }

    /// <summary>Gets sets Approver (Task or Project)</summary>
    [PXInt]
    [PXEPEmployeeSelector]
    [PXFormula(typeof (Switch<Case<Where<Selector<TimeCardMaint.EPTimeCardSummaryWithInfo.projectTaskID, PMTask.approverID>, PX.Data.IsNotNull>, Selector<TimeCardMaint.EPTimeCardSummaryWithInfo.projectTaskID, PMTask.approverID>>, Case<Where<TimeCardMaint.EPTimeCardSummaryWithInfo.projectID, PX.Data.IsNotNull>, Selector<TimeCardMaint.EPTimeCardSummaryWithInfo.projectID, PMProject.approverID>>>))]
    [PXUIField(DisplayName = "Approver", Visibility = PXUIVisibility.SelectorVisible, Enabled = false)]
    public virtual int? ApproverID { get; set; }

    /// <summary>ProjectID field</summary>
    public new abstract class projectID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      TimeCardMaint.EPTimeCardSummaryWithInfo.projectID>
    {
    }

    /// <summary>ProjectTaskID field</summary>
    public new abstract class projectTaskID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      TimeCardMaint.EPTimeCardSummaryWithInfo.projectTaskID>
    {
    }

    /// <summary>CertifiedJob field</summary>
    public new abstract class certifiedJob : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      TimeCardMaint.EPTimeCardSummaryWithInfo.certifiedJob>
    {
    }

    /// <summary>UnionID field</summary>
    public new abstract class unionID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      TimeCardMaint.EPTimeCardSummaryWithInfo.unionID>
    {
    }

    public new abstract class labourItemID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      TimeCardMaint.EPTimeCardSummaryWithInfo.labourItemID>
    {
    }

    /// <summary>CostCodeID field</summary>
    public new abstract class costCodeID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      TimeCardMaint.EPTimeCardSummaryWithInfo.costCodeID>
    {
    }

    /// <summary>WorkCodeID field</summary>
    public new abstract class workCodeID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      TimeCardMaint.EPTimeCardSummaryWithInfo.workCodeID>
    {
    }

    /// <summary>Project's Description field</summary>
    public abstract class projectDescription : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      TimeCardMaint.EPTimeCardSummaryWithInfo.projectDescription>
    {
    }

    /// <summary>Project Manager field</summary>
    public abstract class projectManager : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      TimeCardMaint.EPTimeCardSummaryWithInfo.projectManager>
    {
    }

    /// <summary>Project Task's description field</summary>
    public abstract class projectTaskDescription : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      TimeCardMaint.EPTimeCardSummaryWithInfo.projectTaskDescription>
    {
    }

    public new abstract class noteID : 
      BqlType<
      #nullable enable
      IBqlGuid, Guid>.Field<
      #nullable disable
      TimeCardMaint.EPTimeCardSummaryWithInfo.noteID>
    {
    }

    /// <summary>Task Approver field</summary>
    public abstract class taskApproverID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      TimeCardMaint.EPTimeCardSummaryWithInfo.taskApproverID>
    {
    }

    /// <summary>Cost Code's description field</summary>
    public abstract class costCodeDescription : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      TimeCardMaint.EPTimeCardSummaryWithInfo.costCodeDescription>
    {
    }

    /// <summary>Approval Status field</summary>
    public abstract class approvalStatus : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      TimeCardMaint.EPTimeCardSummaryWithInfo.approvalStatus>
    {
    }

    /// <summary>Approver (Task or Project) field</summary>
    public abstract class approverID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      TimeCardMaint.EPTimeCardSummaryWithInfo.approverID>
    {
    }
  }

  [PXHidden]
  [PXBreakInheritance]
  [Serializable]
  public class EPTimecardTask : CRPMTimeActivity
  {
    [PXDefault]
    [EPDBDateAndTime(typeof (TimeCardMaint.EPTimecardTask.ownerID), WithoutDisplayNames = true, BqlField = typeof (PX.Objects.CR.CRActivity.startDate))]
    [PXUIField(DisplayName = "Date")]
    public override System.DateTime? StartDate { get; set; }

    [PXDBString(10, BqlField = typeof (PMTimeActivity.timeCardCD))]
    [PXUIField(Visible = false)]
    public override string TimeCardCD { get; set; }

    [Project(BqlField = typeof (PMTimeActivity.projectID))]
    public override int? ProjectID { get; set; }

    [ProjectTask(typeof (TimeCardMaint.EPTimecardTask.projectID), "TA", DisplayName = "Project Task", BqlField = typeof (PMTimeActivity.projectTaskID))]
    public override int? ProjectTaskID { get; set; }

    [CostCode(null, typeof (TimeCardMaint.EPTimecardTask.projectTaskID), "E", BqlField = typeof (PMTimeActivity.costCodeID), ReleasedField = typeof (CRPMTimeActivity.released))]
    public override int? CostCodeID { get; set; }

    [PXDBInt(BqlField = typeof (PMTimeActivity.weekID))]
    [PXUIField(DisplayName = "Time Card Week")]
    public override int? WeekID { get; set; }

    [PXTimeList]
    [PXDBInt(BqlField = typeof (PMTimeActivity.timeSpent))]
    [PXUIField(DisplayName = "Time Spent")]
    public override int? TimeSpent { get; set; }

    [PXBool]
    [PXDefault(false, PersistingCheck = PXPersistingCheck.Nothing)]
    public virtual bool? IsOvertimeCalc { get; set; }

    [PXDecimal(1)]
    [PXUIField(DisplayName = "OT Mult")]
    public virtual Decimal? OvertimeMultiplierCalc { get; set; }

    [PXInt]
    [PXUIField(DisplayName = "Billable Time")]
    public virtual int? BillableTimeCalc { get; set; }

    [PXInt]
    [PXUIField(DisplayName = "Billable OT")]
    public virtual int? BillableOvertimeCalc { get; set; }

    [PXInt]
    [PXUIField(DisplayName = "RH", Enabled = false)]
    public virtual int? RegularTimeCalc { get; set; }

    [PXInt]
    [PXUIField(DisplayName = "OT", Enabled = false)]
    public virtual int? OverTimeCalc { get; set; }

    public new abstract class startDate : 
      BqlType<
      #nullable enable
      IBqlDateTime, System.DateTime>.Field<
      #nullable disable
      TimeCardMaint.EPTimecardTask.startDate>
    {
    }

    public new abstract class noteID : 
      BqlType<
      #nullable enable
      IBqlGuid, Guid>.Field<
      #nullable disable
      TimeCardMaint.EPTimecardTask.noteID>
    {
    }

    public new abstract class parentNoteID : 
      BqlType<
      #nullable enable
      IBqlGuid, Guid>.Field<
      #nullable disable
      TimeCardMaint.EPTimecardTask.parentNoteID>
    {
    }

    public new abstract class ownerID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      TimeCardMaint.EPTimecardTask.ownerID>
    {
    }

    public new abstract class classID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      TimeCardMaint.EPTimecardTask.classID>
    {
    }

    public new abstract class type : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      TimeCardMaint.EPTimecardTask.type>
    {
    }

    public new abstract class uistatus : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      TimeCardMaint.EPTimecardTask.uistatus>
    {
    }

    public new abstract class endDate : 
      BqlType<
      #nullable enable
      IBqlDateTime, System.DateTime>.Field<
      #nullable disable
      TimeCardMaint.EPTimecardTask.endDate>
    {
    }

    public new abstract class timeCardCD : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      TimeCardMaint.EPTimecardTask.timeCardCD>
    {
    }

    public new abstract class projectID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      TimeCardMaint.EPTimecardTask.projectID>
    {
    }

    public new abstract class projectTaskID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      TimeCardMaint.EPTimecardTask.projectTaskID>
    {
    }

    public new abstract class costCodeID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      TimeCardMaint.EPTimecardTask.costCodeID>
    {
    }

    public new abstract class weekID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      TimeCardMaint.EPTimecardTask.weekID>
    {
    }

    public new abstract class timeSpent : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      TimeCardMaint.EPTimecardTask.timeSpent>
    {
    }

    public new abstract class origNoteID : 
      BqlType<
      #nullable enable
      IBqlGuid, Guid>.Field<
      #nullable disable
      TimeCardMaint.EPTimecardTask.origNoteID>
    {
    }

    public new abstract class overtimeSpent : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      TimeCardMaint.EPTimecardTask.overtimeSpent>
    {
    }

    public new abstract class summaryLineNbr : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      TimeCardMaint.EPTimecardTask.summaryLineNbr>
    {
    }

    public abstract class isOvertimeCalc : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      TimeCardMaint.EPTimecardTask.isOvertimeCalc>
    {
    }

    public abstract class overtimeMultiplierCalc : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      TimeCardMaint.EPTimecardTask.overtimeMultiplierCalc>
    {
    }

    public abstract class billableTimeCalc : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      TimeCardMaint.EPTimecardTask.billableTimeCalc>
    {
    }

    public abstract class billableOvertimeCalc : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      TimeCardMaint.EPTimecardTask.billableOvertimeCalc>
    {
    }

    public abstract class regularTimeCalc : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      TimeCardMaint.EPTimecardTask.regularTimeCalc>
    {
    }

    public abstract class overTimeCalc : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      TimeCardMaint.EPTimecardTask.overTimeCalc>
    {
    }
  }

  [PXHidden]
  [Serializable]
  public class EPTimeCardItemOrig : EPTimeCardItem
  {
    public new abstract class timeCardCD : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      TimeCardMaint.EPTimeCardItemOrig.timeCardCD>
    {
    }

    public new abstract class lineNbr : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      TimeCardMaint.EPTimeCardItemOrig.lineNbr>
    {
    }

    public new abstract class origLineNbr : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      TimeCardMaint.EPTimeCardItemOrig.origLineNbr>
    {
    }
  }

  [PXHidden]
  [Serializable]
  public class EPTimeCardItemEx : EPTimeCardItem
  {
    public new abstract class timeCardCD : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      TimeCardMaint.EPTimeCardItemEx.timeCardCD>
    {
    }

    public new abstract class lineNbr : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      TimeCardMaint.EPTimeCardItemEx.lineNbr>
    {
    }

    public new abstract class origLineNbr : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      TimeCardMaint.EPTimeCardItemEx.origLineNbr>
    {
    }
  }

  /// <summary>Extends the Contract DAC for use with Time Cards.</summary>
  [Serializable]
  public class ContractEx : PX.Objects.CT.Contract
  {
    /// <summary>
    /// Key field.
    /// User-friendly unique identifier of the Contract.
    /// </summary>
    [PXDimensionSelector("CONTRACT", typeof (Search2<TimeCardMaint.ContractEx.contractCD, InnerJoin<ContractBillingSchedule, On<TimeCardMaint.ContractEx.contractID, Equal<ContractBillingSchedule.contractID>>, LeftJoin<PX.Objects.AR.Customer, On<PX.Objects.AR.Customer.bAccountID, Equal<TimeCardMaint.ContractEx.customerID>>>>, Where<TimeCardMaint.ContractEx.baseType, Equal<CTPRType.contract>>>), typeof (TimeCardMaint.ContractEx.contractCD), new System.Type[] {typeof (TimeCardMaint.ContractEx.contractCD), typeof (TimeCardMaint.ContractEx.customerID), typeof (PX.Objects.AR.Customer.acctName), typeof (PX.Objects.CT.Contract.locationID), typeof (TimeCardMaint.ContractEx.description), typeof (TimeCardMaint.ContractEx.status), typeof (TimeCardMaint.ContractEx.expireDate), typeof (ContractBillingSchedule.lastDate), typeof (ContractBillingSchedule.nextDate)}, DescriptionField = typeof (TimeCardMaint.ContractEx.description), Filterable = true)]
    [PXDBString(IsUnicode = true, IsKey = true, InputMask = "")]
    [PXUIField(DisplayName = "Contract ID", Visibility = PXUIVisibility.SelectorVisible, Enabled = false)]
    public override string ContractCD { get; set; }

    /// <summary>
    /// Database identity.
    /// Unique identifier of the Contract.
    /// </summary>
    public new abstract class contractID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      TimeCardMaint.ContractEx.contractID>
    {
    }

    [Obsolete("This property has been deprecated and will be removed in Acumatica ERP 2019R2.")]
    public new abstract class isTemplate : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      TimeCardMaint.ContractEx.isTemplate>
    {
    }

    /// <summary>The type of the Entity.</summary>
    /// <value>
    /// The field can have one of the values described in <see cref="T:PX.Objects.CT.CTPRType.ListAttribute" />.
    /// </value>
    public new abstract class baseType : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      TimeCardMaint.ContractEx.baseType>
    {
    }

    /// <summary>The description.</summary>
    public new abstract class description : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      TimeCardMaint.ContractEx.description>
    {
    }

    /// <summary>
    /// Identifier of the <see cref="T:PX.Objects.AR.Customer" /> to which the contract belongs.
    /// </summary>
    /// <value>
    /// Corresponds to the <see cref="T:PX.Objects.AR.Customer.PK" /> field.
    /// </value>
    public new abstract class customerID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      TimeCardMaint.ContractEx.customerID>
    {
    }

    /// <summary>The status of the contract.</summary>
    /// <value>
    /// The field can have one of the values described in <see cref="T:PX.Objects.CT.Contract.status.ListAttribute" />.
    /// </value>
    public new abstract class status : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      TimeCardMaint.ContractEx.status>
    {
    }

    /// <summary>Expiration Date.</summary>
    public new abstract class expireDate : 
      BqlType<
      #nullable enable
      IBqlDateTime, System.DateTime>.Field<
      #nullable disable
      TimeCardMaint.ContractEx.expireDate>
    {
    }

    public new abstract class contractCD : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      TimeCardMaint.ContractEx.contractCD>
    {
    }
  }

  [Serializable]
  public enum ActivityValidationError
  {
    ActivityIsNotCompleted,
    ActivityIsRejected,
    ActivityIsNotApproved,
    ProjectIsNotActive,
    ProjectIsCompleted,
    ProjectTaskIsCancelled,
    ProjectTaskIsCompleted,
    ProjectTaskIsNotActive,
    LaborClassNotSpecified,
    OvertimeLaborClassNotSpecified,
  }

  public class TimeCardSummaryCopiedInfo
  {
    public string Description { get; set; }

    public string Note { get; set; }

    public TimeCardSummaryCopiedInfo(string description, string note)
    {
      this.Description = description;
      this.Note = note;
    }
  }
}
