// Decompiled with JetBrains decompiler
// Type: PX.SM.AUReportProcess
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using CommonServiceLocator;
using PX.Api;
using PX.Common;
using PX.Common.Mail;
using PX.Data;
using PX.Data.BQL;
using PX.Data.EP;
using PX.Data.Wiki.Parser;
using PX.Metadata;
using PX.Reports;
using PX.Reports.Controls;
using PX.Reports.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net.Mail;
using System.Web.Compilation;
using System.Web.UI;

#nullable enable
namespace PX.SM;

public class AUReportProcess : PXGraph<
#nullable disable
AUReportProcess>
{
  public PXFilter<AUReportProcess.Parameters> Filter;
  public PXCancel<AUReportProcess.Parameters> Cancel;
  public PXSave<AUReportProcess.Parameters> Save;
  public PXFilteredProcessing<AUReportProcess.ReportSettings, AUReportProcess.Parameters, Where2<Where<AUReportProcess.ReportSettings.screenID, Equal<Current<AUReportProcess.Parameters.reportID>>, Or<Current<AUReportProcess.Parameters.reportID>, PX.Data.IsNull>>, PX.Data.And<Where<AUReportProcess.ReportSettings.username, Equal<Current<AUReportProcess.Parameters.username>>, Or<Current<AUReportProcess.Parameters.username>, PX.Data.IsNull>>>>> Templates;

  public static KeyValuePair<int, string>[] GetSchedules()
  {
    AUReportProcess.Definition slot = PXDatabase.GetSlot<AUReportProcess.Definition>("SM205060", typeof (AUSchedule));
    return slot != null ? slot.Schedules : new KeyValuePair<int, string>[0];
  }

  public static void AppendTemplate(
    int scheduleID,
    Guid settingsID,
    bool mergeReports = false,
    int? mergingOrder = null)
  {
    ScreenUtils.ScreenInfo.Get("SM205060");
    AUScheduleMaint instance = PXGraph.CreateInstance<AUScheduleMaint>();
    AUReportProcess.Definition slot = PXDatabase.GetSlot<AUReportProcess.Definition>("SM205060", typeof (AUSchedule));
    if (slot != null && slot.Schedules.Length == 1 && slot.Schedules[0].Key == -1 && scheduleID == -1)
    {
      AUReportProcess.ReportSettings reportSettings = (AUReportProcess.ReportSettings) PXSelectBase<AUReportProcess.ReportSettings, PXSelect<AUReportProcess.ReportSettings, Where<AUReportProcess.ReportSettings.settingsID, Equal<Required<AUReportProcess.ReportSettings.settingsID>>>>.Config>.SelectSingleBound((PXGraph) instance, (object[]) null, (object) settingsID);
      reportSettings.MergeReports = new bool?(mergeReports);
      reportSettings.MergingOrder = mergingOrder;
      instance.Caches[typeof (AUReportProcess.ReportSettings)].Update((object) reportSettings);
      instance.Schedule.Insert(new AUSchedule()
      {
        ScreenID = "SM205060",
        ViewName = "Filter",
        GraphName = typeof (AUReportProcess).FullName
      }).ActionName = "ProcessAll";
      instance.Schedule.Cache.IsDirty = false;
      instance.Filters.Insert(new AUScheduleFilter()
      {
        FieldName = "ScreenID",
        Condition = new int?(12),
        Operator = new int?(1)
      });
      AUReportProcess.AppendToFilters(instance, settingsID);
      instance.Filters.Cache.IsDirty = false;
      throw new PXRedirectRequiredException((PXGraph) instance, "Schedule");
    }
    instance.Views.Caches.Add(typeof (AUReportLink));
    foreach (PXResult<AUReportLink> pxResult1 in PXSelectBase<AUReportLink, PXSelect<AUReportLink, Where<AUReportLink.templateID, Equal<Required<AUReportLink.templateID>>>, OrderBy<Asc<AUReportLink.scheduleID>>>.Config>.Select((PXGraph) instance, (object) settingsID))
    {
      AUReportLink auReportLink = (AUReportLink) pxResult1;
      int? nullable1;
      if (instance.Schedule.Current != null)
      {
        int? scheduleId = instance.Schedule.Current.ScheduleID;
        nullable1 = auReportLink.ScheduleID;
        if (scheduleId.GetValueOrDefault() == nullable1.GetValueOrDefault() & scheduleId.HasValue == nullable1.HasValue)
          goto label_9;
      }
      if (instance.IsDirty)
      {
        instance.Save.Press();
        instance.Clear();
      }
      instance.ScheduleCurrentScreen.Current.ScreenID = "SM205060";
      instance.Schedule.Current = (AUSchedule) instance.Schedule.Search<AUSchedule.scheduleID>((object) auReportLink.ScheduleID, (object) "SM205060");
label_9:
      if (instance.Schedule.Current != null)
      {
        foreach (PXResult<AUScheduleFilter> pxResult2 in instance.Filters.Select())
        {
          AUScheduleFilter auScheduleFilter = (AUScheduleFilter) pxResult2;
          short? rowNbr = auScheduleFilter.RowNbr;
          nullable1 = rowNbr.HasValue ? new int?((int) rowNbr.GetValueOrDefault()) : new int?();
          rowNbr = auReportLink.RowNbr;
          int? nullable2 = rowNbr.HasValue ? new int?((int) rowNbr.GetValueOrDefault()) : new int?();
          if (nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue)
          {
            instance.Filters.Delete(auScheduleFilter);
            break;
          }
        }
        instance.Caches[typeof (AUReportLink)].Delete((object) auReportLink);
      }
    }
    if (instance.IsDirty)
    {
      instance.Save.Press();
      instance.Clear();
    }
    if (scheduleID == -1)
      return;
    instance.ScheduleCurrentScreen.Current.ScreenID = "SM205060";
    instance.Schedule.Current = (AUSchedule) instance.Schedule.Search<AUSchedule.scheduleID>((object) scheduleID, (object) "SM205060");
    if (instance.Schedule.Current == null)
      return;
    AUScheduleFilter auScheduleFilter1 = (AUScheduleFilter) null;
    foreach (PXResult<AUScheduleFilter> pxResult in instance.Filters.Select())
      auScheduleFilter1 = (AUScheduleFilter) pxResult;
    if (auScheduleFilter1 != null)
    {
      auScheduleFilter1.Operator = new int?(1);
      instance.Filters.Update(auScheduleFilter1);
    }
    AUReportProcess.AppendToFilters(instance, settingsID);
    instance.Save.Press();
  }

  protected static void AppendToFilters(AUScheduleMaint graph, Guid settingsID)
  {
    AUScheduleFilter auScheduleFilter1 = new AUScheduleFilter();
    AUReportProcess.ReportSettings reportSettings = (AUReportProcess.ReportSettings) PXSelectBase<AUReportProcess.ReportSettings, PXSelect<AUReportProcess.ReportSettings, Where<AUReportProcess.ReportSettings.settingsID, Equal<Required<AUReportProcess.ReportSettings.settingsID>>>>.Config>.SelectSingleBound((PXGraph) graph, (object[]) null, (object) settingsID);
    auScheduleFilter1.FieldName = "SettingsID";
    auScheduleFilter1.Value = reportSettings.ScreenID;
    AUScheduleFilter auScheduleFilter2 = graph.Filters.Insert(auScheduleFilter1);
    if (auScheduleFilter2 != null)
      graph.Caches[typeof (AUReportLink)].Insert((object) new AUReportLink()
      {
        TemplateID = new Guid?(settingsID),
        ScheduleID = graph.Schedule.Current.ScheduleID,
        RowNbr = auScheduleFilter2.RowNbr
      });
    AUScheduleFilter auScheduleFilter3 = graph.Filters.Insert(new AUScheduleFilter()
    {
      FieldName = "Username",
      Value = reportSettings.Username
    });
    if (auScheduleFilter3 != null)
      graph.Caches[typeof (AUReportLink)].Insert((object) new AUReportLink()
      {
        TemplateID = new Guid?(settingsID),
        ScheduleID = graph.Schedule.Current.ScheduleID,
        RowNbr = auScheduleFilter3.RowNbr
      });
    AUScheduleFilter auScheduleFilter4 = graph.Filters.Insert(new AUScheduleFilter()
    {
      FieldName = "Name",
      Value = reportSettings.Name
    });
    if (auScheduleFilter4 == null)
      return;
    graph.Caches[typeof (AUReportLink)].Insert((object) new AUReportLink()
    {
      TemplateID = new Guid?(settingsID),
      ScheduleID = graph.Schedule.Current.ScheduleID,
      RowNbr = auScheduleFilter4.RowNbr
    });
  }

  [InjectDependency]
  protected IReportLoaderService ReportLoader { get; private set; }

  [InjectDependency]
  protected IReportDataBinder ReportDataBinder { get; private set; }

  [InjectDependency]
  protected IReportRenderer ReportRenderer { get; private set; }

  [InjectDependency]
  protected SettingsProvider SettingsProvider { get; private set; }

  public AUReportProcess()
  {
    this.Templates.SetProcessDelegate((PXProcessingBase<AUReportProcess.ReportSettings>.ProcessListDelegate) (items => PXGraph.CreateInstance<AUReportProcess>().Send(items)));
    PXUIFieldAttribute.SetEnabled<AUReportProcess.ReportSettings.mergeReports>(this.Caches[typeof (AUReportProcess.ReportSettings)], (object) null, true);
    PXUIFieldAttribute.SetEnabled<AUReportProcess.ReportSettings.mergingOrder>(this.Caches[typeof (AUReportProcess.ReportSettings)], (object) null, true);
  }

  protected virtual void Parameters_My_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    bool? my = this.Filter.Current.My;
    bool flag = true;
    if (my.GetValueOrDefault() == flag & my.HasValue)
      this.Filter.Current.Username = this.Accessinfo.UserName;
    else
      this.Filter.Current.Username = (string) null;
  }

  protected virtual void ReportSettings_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    if (e.Row == null)
      return;
    PXCache cache = sender;
    object row = e.Row;
    bool? mergeReports = ((AUReportProcess.ReportSettings) e.Row).MergeReports;
    bool flag = true;
    int num = mergeReports.GetValueOrDefault() == flag & mergeReports.HasValue ? 1 : 0;
    PXUIFieldAttribute.SetEnabled<AUReportProcess.ReportSettings.mergingOrder>(cache, row, num != 0);
  }

  protected virtual void Parameters_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    if (e.Row == null)
      return;
    PXCache cache = sender;
    object row = e.Row;
    bool? my = this.Filter.Current.My;
    bool flag = true;
    int num = !(my.GetValueOrDefault() == flag & my.HasValue) ? 1 : 0;
    PXUIFieldAttribute.SetEnabled<AUReportProcess.Parameters.username>(cache, row, num != 0);
  }

  /// <summary>render report or aRmReport</summary>
  /// <param name="item"></param>
  /// <returns></returns>
  protected Report RenderUniversalReport(AUReportProcess.ReportSettings item)
  {
    return this.ReportLoader.RenderUniversalReport(this.ReportLoader.LoadReport(item.ScreenID, (IPXResultset) null), this.SettingsProvider.Read(item.SettingsID));
  }

  [Obsolete("This method will be removed in Acumatica 2021R2. Please use a corresponding extension method for IReportLoaderService instead")]
  public static Report RenderUniversalReport(Report report, PXReportSettings settings)
  {
    return ServiceLocator.Current.GetInstance<IReportLoaderService>().RenderUniversalReport(report, settings);
  }

  [Obsolete("This method will be removed in Acumatica 2021R2. Please use a corresponding extension method for IReportLoaderService instead")]
  internal static Report RenderARMReport(
    Report report,
    PXReportSettings settings,
    IDictionary<string, string> reportParams = null)
  {
    return ServiceLocator.Current.GetInstance<IReportLoaderService>().RenderARMReport(report, settings, reportParams);
  }

  [Obsolete("This method will be removed in Acumatica 2021R2. Please use a corresponding extension method for IReportRenderer instead")]
  public static StreamManager RenderReport(
    ReportNode reportNode,
    string format,
    Hashtable deviceInfo = null,
    bool embedStreams = false)
  {
    return ServiceLocator.Current.GetInstance<IReportRenderer>().RenderReport(reportNode, format, deviceInfo, embedStreams);
  }

  protected void SendToAll(ReportNode reportNode, List<PXReportSettings> recipients)
  {
    using (MailSender sender = MailAccountManager.Sender)
    {
      if (sender == null)
        throw new PXException("The email account is empty. Please define the Default Email Account in Email Preferences.");
      foreach (PXReportSettings recipient in recipients)
      {
        List<Attachment> attachmentList = new List<Attachment>();
        string format = recipient.Mail.Format == null || recipient.Mail.Format.Trim().Length == 0 ? "PDF" : recipient.Mail.Format;
        StreamManager streamManager = this.ReportRenderer.RenderReport(reportNode, format);
        string name1 = streamManager.MainStream.Name;
        foreach (ReportStream stream in streamManager.Streams)
        {
          string name2 = stream.Name;
          string[] strArray = name2.Split('\\');
          Guid guid;
          if (strArray.Length == 0 || !GUID.TryParse(strArray[strArray.Length - 1].ToString(), ref guid))
            guid = Guid.NewGuid();
          string name3 = name2;
          if (!Path.HasExtension(name2))
          {
            string extension = MimeTypes.GetExtension(stream.MimeType);
            if (extension != null)
              name3 += extension;
          }
          attachmentList.Add(new Attachment((Stream) stream, name3, stream.MimeType));
        }
        MailSender.MailMessageT message = new MailSender.MailMessageT(MailAccountManager.GetDefaultEmailAccount().With<EMailAccount, string>((Func<EMailAccount, string>) (_ => _.Address)), (string) null, new MailSender.MessageAddressee(recipient.Mail.EMail, (string) null, recipient.Mail.Cc, recipient.Mail.Bcc), new MailSender.MessageContent(recipient.Mail.Subject, false, ""));
        sender.Send(message, attachmentList.ToArray());
      }
    }
  }

  protected void Send(List<AUReportProcess.ReportSettings> list)
  {
    try
    {
      BuildManager.CreateInstanceFromVirtualPath(PXUrl.ToAbsoluteUrl("~/frames/reportlauncher.aspx"), typeof (Page));
    }
    catch
    {
    }
    list.Sort((Comparison<AUReportProcess.ReportSettings>) ((x, y) => (x.MergingOrder ?? -1).CompareTo((object) y.MergingOrder)));
    int index1;
    for (index1 = 0; index1 < list.Count && !list[index1].MergeReports.Value; ++index1)
      this.SendToAll(this.ReportDataBinder.ProcessReportDataBinding(this.RenderUniversalReport(list[index1])), new List<PXReportSettings>(1)
      {
        this.SettingsProvider.Read(list[index1].SettingsID)
      });
    if (index1 == list.Count)
      return;
    List<PXReportSettings> recipients = new List<PXReportSettings>()
    {
      this.SettingsProvider.Read(list[index1].SettingsID)
    };
    List<AUReportProcess.ReportSettings> reportSettingsList = list;
    int index2 = index1;
    int index3 = index2 + 1;
    Report report1 = this.RenderUniversalReport(reportSettingsList[index2]);
    for (; index3 < list.Count; ++index3)
    {
      PXReportSettings recipient = this.SettingsProvider.Read(list[index3].SettingsID);
      if (!recipients.Exists((Predicate<PXReportSettings>) (x => x.Mail.EMail == recipient.Mail.EMail)))
        recipients.Add(recipient);
      Report report2 = this.RenderUniversalReport(list[index3]);
      report1.SiblingReports.Add(report2);
    }
    this.SendToAll(this.ReportDataBinder.ProcessReportDataBinding(report1), recipients);
  }

  [Serializable]
  public class Parameters : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    protected bool? _My;
    protected string _ReportID;
    protected string _Username;

    [PXDBBool]
    [PXDefault(false)]
    [PXUIField(DisplayName = "Me")]
    public virtual bool? My
    {
      get => this._My;
      set => this._My = value;
    }

    [PXDBString(8, IsFixed = true, InputMask = "CC.CC.CC.CC")]
    [PXUIField(DisplayName = "Report ID")]
    public virtual string ReportID
    {
      get => this._ReportID;
      set => this._ReportID = value;
    }

    [PXDBString(256 /*0x0100*/, IsUnicode = true, InputMask = "")]
    [PXUIField(DisplayName = "User")]
    [PXSelector(typeof (Users.username), new System.Type[] {typeof (Users.username), typeof (Users.displayName), typeof (Users.fullName), typeof (Users.phone), typeof (Users.comment), typeof (Users.isApproved)}, DescriptionField = typeof (Users.displayName))]
    public virtual string Username
    {
      get => this._Username;
      set => this._Username = value;
    }

    public abstract class my : BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    AUReportProcess.Parameters.my>
    {
    }

    public abstract class reportID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      AUReportProcess.Parameters.reportID>
    {
    }

    public abstract class username : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      AUReportProcess.Parameters.username>
    {
    }
  }

  [Serializable]
  public class ReportSettings : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    protected bool? _Selected;
    protected string _ScreenID;
    protected string _Username;
    protected string _Name;
    protected bool? _IsDefault;
    protected bool? _IsShared;
    protected bool? _MergeReports;
    protected int? _MergingOrder;

    [PXUnboundDefault(false, PersistingCheck = PXPersistingCheck.Nothing)]
    [PXBool]
    [PXUIField(DisplayName = "Selected")]
    public virtual bool? Selected
    {
      get => this._Selected;
      set => this._Selected = value;
    }

    [PXDBGuid(false, IsKey = true)]
    public virtual Guid? SettingsID { get; set; }

    [PXDBString(8, InputMask = "CC.CC.CC.CC")]
    [PXUIField(DisplayName = "Report ID")]
    [PXSelector(typeof (SiteMap.screenID), DescriptionField = typeof (SiteMap.title))]
    public virtual string ScreenID
    {
      get => this._ScreenID;
      set => this._ScreenID = value;
    }

    [PXDBString(256 /*0x0100*/, IsUnicode = true, InputMask = "")]
    [PXDefault]
    [PXUIField(DisplayName = "User")]
    [PXSelector(typeof (Users.username), DescriptionField = typeof (Users.displayName))]
    public virtual string Username
    {
      get => this._Username;
      set => this._Username = value;
    }

    [PXDBString(50, InputMask = "")]
    [PXUIField(DisplayName = "Template")]
    public string Name
    {
      get => this._Name;
      set => this._Name = value;
    }

    [PXDefault(false)]
    [PXDBBool]
    [PXUIField(DisplayName = "Default")]
    public virtual bool? IsDefault
    {
      get => this._IsDefault;
      set => this._IsDefault = value;
    }

    [PXDefault(false)]
    [PXDBBool]
    [PXUIField(DisplayName = "Shared")]
    public virtual bool? IsShared
    {
      get => this._IsShared;
      set => this._IsShared = value;
    }

    [PXDefault(false)]
    [PXUIField(DisplayName = "Merge Reports")]
    [PXDBBool]
    public bool? MergeReports
    {
      get => this._MergeReports;
      set => this._MergeReports = value;
    }

    [PXUIField(DisplayName = "Merging Order", Enabled = true)]
    [PXDBInt]
    public int? MergingOrder
    {
      get => this._MergingOrder;
      set => this._MergingOrder = value;
    }

    public abstract class selected : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      AUReportProcess.ReportSettings.selected>
    {
    }

    public abstract class settingsID : 
      BqlType<
      #nullable enable
      IBqlGuid, Guid>.Field<
      #nullable disable
      AUReportProcess.ReportSettings.settingsID>
    {
    }

    public abstract class screenID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      AUReportProcess.ReportSettings.screenID>
    {
    }

    public abstract class username : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      AUReportProcess.ReportSettings.username>
    {
    }

    public abstract class name : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      AUReportProcess.ReportSettings.name>
    {
    }

    public abstract class isDefault : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      AUReportProcess.ReportSettings.isDefault>
    {
    }

    public abstract class isShared : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      AUReportProcess.ReportSettings.isShared>
    {
    }

    public abstract class mergeReports : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      AUReportProcess.ReportSettings.mergeReports>
    {
    }

    public abstract class mergingOrder : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      AUReportProcess.ReportSettings.mergingOrder>
    {
    }
  }

  protected sealed class Definition : IPrefetchable, IPXCompanyDependent
  {
    private List<KeyValuePair<int, string>> ret;

    public KeyValuePair<int, string>[] Schedules => this.ret.ToArray();

    public void Prefetch()
    {
      this.ret = new List<KeyValuePair<int, string>>();
      foreach (PXResult<AUSchedule> pxResult in PXSelectBase<AUSchedule, PXSelect<AUSchedule, Where<AUSchedule.screenID, Equal<Required<AUSchedule.screenID>>, And<AUSchedule.isActive, Equal<PX.Data.True>>>>.Config>.Select((PXGraph) new AUReportProcess(), (object) "SM205060"))
      {
        AUSchedule auSchedule = (AUSchedule) pxResult;
        this.ret.Add(new KeyValuePair<int, string>(auSchedule.ScheduleID.Value, auSchedule.Description));
      }
      this.ret.Add(new KeyValuePair<int, string>(-1, this.ret.Count > 0 ? PXLocalizer.Localize("<Remove from the schedule.>") : PXLocalizer.Localize("<Set up a schedule first.>")));
    }
  }
}
