// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.ClockInClockOut.EPClockInTimerMaint
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web.Compilation;

#nullable enable
namespace PX.Objects.EP.ClockInClockOut;

[PXInternalUseOnly]
public class EPClockInTimerMaint : PXGraph<
#nullable disable
EPClockInTimerMaint>
{
  [PXViewName("Available Clock In Timers")]
  [PXReadOnlyView]
  public PXSelect<EPClockInTimerMaint.EPClockInTimerDataCurrent> AvailableTimers;
  [PXViewName("Active Clock In Timers")]
  public PXSelectJoin<EPClockInTimerData, InnerJoin<PX.Objects.EP.EPEmployee, On<PX.Objects.EP.EPEmployee.userID, Equal<Current<AccessInfo.userID>>>>, Where<EPClockInTimerData.employeeID, Equal<PX.Objects.EP.EPEmployee.bAccountID>>, OrderBy<Desc<EPClockInTimerData.startDate>>> ActiveTimers;
  [PXHidden]
  public PXSelect<PX.Objects.EP.EPEmployee, Where<PX.Objects.EP.EPEmployee.userID, Equal<Current<AccessInfo.userID>>>> CurrentEmployee;
  [PXHidden]
  public PXSelectJoin<EPClockInTimerData, InnerJoin<PX.Objects.EP.EPEmployee, On<PX.Objects.EP.EPEmployee.userID, Equal<Current<AccessInfo.userID>>, And<EPEmployeeClockIn.activeClockInTimerID, Equal<EPClockInTimerData.timerDataID>>>>, Where<EPClockInTimerData.employeeID, Equal<PX.Objects.EP.EPEmployee.bAccountID>>> CurrentTimer;
  [PXHidden]
  public PXSelect<EPTimeLogType> TimeLogTypes;
  [PXHidden]
  public PXSelect<EPTimeLog> TimeLogs;
  public PXAction<EPClockInTimerMaint.EPClockInTimerDataCurrent> clockIn;
  public PXAction<EPClockInTimerData> start;
  public PXAction<EPClockInTimerData> stop;
  public PXAction<EPClockInTimerData> pause;
  public PXAction<EPClockInTimerData> open;
  public PXAction<EPClockInTimerData> initCurrentDocument;

  public virtual IEnumerable availableTimers() => this.AvailableTimers.Cache.Cached;

  private System.DateTime businessDateTimeNowUTC => System.DateTime.UtcNow;

  [PXUIField(DisplayName = "Clock In")]
  [PXButton]
  public virtual IEnumerable ClockIn(PXAdapter adapter)
  {
    EPClockInTimerData current = (EPClockInTimerData) this.AvailableTimers.Current;
    PX.Objects.EP.EPEmployee epEmployee = this.CurrentEmployee.Current ?? (PX.Objects.EP.EPEmployee) this.CurrentEmployee.Select();
    EPEmployeeClockIn extension = epEmployee != null ? epEmployee.GetExtension<EPEmployeeClockIn>() : (EPEmployeeClockIn) null;
    if (current != null && extension != null)
    {
      this.StopInProgressTimers();
      current.StartDate = new System.DateTime?(this.businessDateTimeNowUTC);
      EPClockInTimerData clockInTimerData = this.ActiveTimers.Cache.Insert((object) current) as EPClockInTimerData;
      this.ClearCurrentTimer();
      this.Actions.PressSave();
      if (clockInTimerData != null)
      {
        extension.ActiveClockInTimerID = clockInTimerData.TimerDataID;
        this.CurrentEmployee.Update(epEmployee);
        this.Actions.PressSave();
      }
    }
    this.ClearCurrentTimer();
    this.AvailableTimers.View.RequestRefresh();
    this.ActiveTimers.View.RequestRefresh();
    return adapter.Get();
  }

  [PXUIField(DisplayName = "Start")]
  [PXButton]
  public virtual IEnumerable Start(PXAdapter adapter)
  {
    EPClockInTimerData current = this.ActiveTimers.Current;
    PX.Objects.EP.EPEmployee epEmployee = this.CurrentEmployee.Current ?? (PX.Objects.EP.EPEmployee) this.CurrentEmployee.Select();
    EPEmployeeClockIn extension = epEmployee != null ? epEmployee.GetExtension<EPEmployeeClockIn>() : (EPEmployeeClockIn) null;
    if (current != null && extension != null)
    {
      this.StopInProgressTimers();
      current.StartDate = new System.DateTime?(this.businessDateTimeNowUTC);
      if (this.ActiveTimers.Cache.Update((object) current) is EPClockInTimerData clockInTimerData)
      {
        extension.ActiveClockInTimerID = clockInTimerData.TimerDataID;
        this.CurrentEmployee.Update(epEmployee);
        this.Actions.PressSave();
      }
    }
    this.AvailableTimers.View.RequestRefresh();
    this.ActiveTimers.View.RequestRefresh();
    return adapter.Get();
  }

  [PXUIField(DisplayName = "Stop")]
  [PXButton]
  public virtual IEnumerable Stop(PXAdapter adapter)
  {
    EPClockInTimerData current = this.ActiveTimers.Current;
    PX.Objects.EP.EPEmployee epEmployee = this.CurrentEmployee.Current ?? (PX.Objects.EP.EPEmployee) this.CurrentEmployee.Select();
    EPEmployeeClockIn extension = epEmployee != null ? epEmployee.GetExtension<EPEmployeeClockIn>() : (EPEmployeeClockIn) null;
    if (current != null && extension != null)
    {
      this.ActiveTimers.Cache.Delete((object) current);
      int? activeClockInTimerId = extension.ActiveClockInTimerID;
      int? nullable1 = current.TimerDataID;
      if (activeClockInTimerId.GetValueOrDefault() == nullable1.GetValueOrDefault() & activeClockInTimerId.HasValue == nullable1.HasValue)
      {
        EPEmployeeClockIn epEmployeeClockIn = extension;
        nullable1 = new int?();
        int? nullable2 = nullable1;
        epEmployeeClockIn.ActiveClockInTimerID = nullable2;
        this.CurrentEmployee.Update(epEmployee);
      }
      this.InsertTimeLog(current);
      this.Actions.PressSave();
    }
    this.AvailableTimers.View.RequestRefresh();
    this.ActiveTimers.View.RequestRefresh();
    return adapter.Get();
  }

  [PXUIField(DisplayName = "Pause")]
  [PXButton]
  public virtual IEnumerable Pause(PXAdapter adapter)
  {
    EPClockInTimerData current = this.ActiveTimers.Current;
    PX.Objects.EP.EPEmployee epEmployee = this.CurrentEmployee.Current ?? (PX.Objects.EP.EPEmployee) this.CurrentEmployee.Select();
    EPEmployeeClockIn extension = epEmployee != null ? epEmployee.GetExtension<EPEmployeeClockIn>() : (EPEmployeeClockIn) null;
    if (current != null && extension != null)
    {
      int num = this.InsertTimeLog(current);
      current.StartDate = new System.DateTime?(this.businessDateTimeNowUTC);
      current.TimeSpent = new int?(current.TimeSpent.GetValueOrDefault() + num);
      if (this.ActiveTimers.Cache.Update((object) current) is EPClockInTimerData clockInTimerData)
      {
        int? timerDataId = clockInTimerData.TimerDataID;
        int? activeClockInTimerId = extension.ActiveClockInTimerID;
        if (timerDataId.GetValueOrDefault() == activeClockInTimerId.GetValueOrDefault() & timerDataId.HasValue == activeClockInTimerId.HasValue)
        {
          extension.ActiveClockInTimerID = new int?();
          this.CurrentEmployee.Update(epEmployee);
          this.Actions.PressSave();
        }
      }
    }
    this.AvailableTimers.View.RequestRefresh();
    this.ActiveTimers.View.RequestRefresh();
    return adapter.Get();
  }

  [PXUIField]
  [PXButton(DisplayOnMainToolbar = false)]
  public virtual void Open()
  {
    EPClockInTimerData current = this.ActiveTimers.Current;
    if (current == null)
      return;
    new EntityHelper((PXGraph) this).NavigateToRow(current.RelatedEntityType, current.RelatedEntityID, PXRedirectHelper.WindowMode.NewWindow);
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable InitCurrentDocument(PXAdapter adapter)
  {
    this.ClearCurrentTimer();
    if (adapter.CommandArguments == null || !adapter.CommandArguments.StartsWith("ScreenId") || !adapter.CommandArguments.Contains<char>('&'))
      return adapter.Get();
    PXGraph graph = (PXGraph) null;
    IClockInClockOut clockInClockOut = (IClockInClockOut) null;
    string commandArguments = adapter.CommandArguments;
    string[] strArray1;
    if (commandArguments == null)
      strArray1 = (string[]) null;
    else
      strArray1 = commandArguments.TrimEnd().Split('&');
    string[] strArray2 = strArray1;
    string screenID = strArray2[0].Split('=')[1];
    if (strArray2 != null && screenID != null)
    {
      PXSiteMapNode mapNodeByScreenId = PXSiteMap.Provider.FindSiteMapNodeByScreenID(screenID);
      if (mapNodeByScreenId != null)
      {
        System.Type type = PXBuildManager.GetType(mapNodeByScreenId.GraphType, false);
        if (type != (System.Type) null)
        {
          graph = PXGraph.CreateInstance(type);
          if (graph != null)
            clockInClockOut = graph.FindImplementation<IClockInClockOut>();
        }
      }
    }
    if (clockInClockOut != null)
    {
      Dictionary<string, string> dictionary = new Dictionary<string, string>();
      foreach (string str in strArray2)
      {
        char[] chArray = new char[1]{ '=' };
        string[] strArray3 = str.Split(chArray);
        dictionary.Add(strArray3[0], strArray3[1].Trim().TrimEnd('+'));
      }
      if (dictionary.Count<KeyValuePair<string, string>>() > 0)
      {
        Guid? noteID = clockInClockOut.SelectEntityID(dictionary);
        if (noteID.HasValue)
        {
          if ((EPClockInTimerMaint.EPClockInTimerDataCurrent) PXSelectBase<EPClockInTimerMaint.EPClockInTimerDataCurrent, PXSelect<EPClockInTimerMaint.EPClockInTimerDataCurrent, Where<EPClockInTimerMaint.EPClockInTimerDataCurrent.relatedEntityID, Equal<Required<EPClockInTimerMaint.EPClockInTimerDataCurrent.relatedEntityID>>>>.Config>.SelectSingleBound((PXGraph) this, (object[]) null, (object) noteID) == null)
          {
            TimeLogData timeLogData = clockInClockOut.GetTimeLogData(noteID);
            EPClockInTimerMaint.EPClockInTimerDataCurrent timerDataCurrent = new EPClockInTimerMaint.EPClockInTimerDataCurrent();
            timerDataCurrent.EmployeeID = this.CurrentEmployee.SelectSingle((object[]) null).BAccountID;
            timerDataCurrent.RelatedEntityID = noteID;
            timerDataCurrent.RelatedEntityType = graph.GetPrimaryCache().GetItemType().FullName;
            timerDataCurrent.DocumentNbr = timeLogData.DocumentNbr;
            timerDataCurrent.Summary = timeLogData.Summary;
            this.AvailableTimers.Insert(timerDataCurrent);
          }
          else
            this.ClearCurrentTimer();
        }
      }
    }
    return adapter.Get();
  }

  private void ClearCurrentTimer()
  {
    this.AvailableTimers.Cache.Clear();
    this.AvailableTimers.Cache.ClearQueryCache();
  }

  private void StopInProgressTimers()
  {
    EPClockInTimerData clockInTimerData = this.CurrentTimer.SelectSingle();
    if (clockInTimerData == null || !clockInTimerData.StartDate.HasValue)
      return;
    int totalSeconds = (int) (this.businessDateTimeNowUTC - clockInTimerData.StartDate.Value).TotalSeconds;
    clockInTimerData.TimeSpent = new int?(clockInTimerData.TimeSpent.GetValueOrDefault() + totalSeconds);
    this.ActiveTimers.Update(clockInTimerData);
  }

  private int InsertTimeLog(EPClockInTimerData timerData)
  {
    System.DateTime dateTime = timerData.StartDate.Value;
    System.DateTime utcNow = System.DateTime.UtcNow;
    TimeSpan timeSpan = utcNow - dateTime;
    int totalSeconds = (int) timeSpan.TotalSeconds;
    timeSpan = utcNow - dateTime;
    int num = (int) System.Math.Ceiling(timeSpan.TotalMinutes);
    this.TimeLogs.Insert(new EPTimeLog()
    {
      EmployeeID = timerData.EmployeeID,
      RelatedEntityType = timerData.RelatedEntityType,
      RelatedEntityID = timerData.RelatedEntityID,
      TimeLogTypeID = timerData.TimeLogTypeID,
      DocumentNbr = timerData.DocumentNbr,
      Summary = timerData.Summary,
      ReportedInTimeZoneID = LocaleInfo.GetTimeZone()?.Id,
      StartDate = new System.DateTime?(dateTime),
      EndDate = new System.DateTime?(utcNow),
      TimeSpent = new int?(num)
    });
    return totalSeconds;
  }

  [PXHidden]
  [PXBreakInheritance]
  public class EPClockInTimerDataCurrent : EPClockInTimerData
  {
    public new abstract class timerDataID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      EPClockInTimerMaint.EPClockInTimerDataCurrent.timerDataID>
    {
    }

    public new abstract class employeeID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      EPClockInTimerMaint.EPClockInTimerDataCurrent.employeeID>
    {
    }

    public new abstract class timeLogTypeID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      EPClockInTimerMaint.EPClockInTimerDataCurrent.timeLogTypeID>
    {
    }

    public new abstract class summary : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      EPClockInTimerMaint.EPClockInTimerDataCurrent.summary>
    {
    }

    public new abstract class documentNbr : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      EPClockInTimerMaint.EPClockInTimerDataCurrent.documentNbr>
    {
    }

    public new abstract class relatedEntityType : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      EPClockInTimerMaint.EPClockInTimerDataCurrent.relatedEntityType>
    {
    }

    public new abstract class relatedEntityID : 
      BqlType<
      #nullable enable
      IBqlGuid, Guid>.Field<
      #nullable disable
      EPClockInTimerMaint.EPClockInTimerDataCurrent.relatedEntityID>
    {
    }

    public new abstract class startDate : 
      BqlType<
      #nullable enable
      IBqlDateTime, System.DateTime>.Field<
      #nullable disable
      EPClockInTimerMaint.EPClockInTimerDataCurrent.startDate>
    {
    }

    public new abstract class timeSpent : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      EPClockInTimerMaint.EPClockInTimerDataCurrent.timeSpent>
    {
    }
  }
}
