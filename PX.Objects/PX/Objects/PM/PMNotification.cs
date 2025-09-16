// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.PMNotification
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CA;
using PX.Objects.Common;
using PX.Objects.CS;
using PX.SM;
using System;
using System.Diagnostics.CodeAnalysis;

#nullable enable
namespace PX.Objects.PM;

[PXHidden]
[PXProjection(typeof (Select<NotificationSetup, Where<NotificationSetup.module, Equal<PXModule.pm>>>), Persistent = true)]
[ExcludeFromCodeCoverage]
[Serializable]
public class PMNotification : NotificationSetup
{
  [PXDBString(2, IsFixed = true, IsKey = true)]
  [PXDefault("PM")]
  public override 
  #nullable disable
  string Module
  {
    get => this._Module;
    set => this._Module = value;
  }

  [PXDefault("Project")]
  [PXDBString(10, InputMask = ">aaaaaaaaaa")]
  public override string SourceCD
  {
    get => this._SourceCD;
    set => this._SourceCD = value;
  }

  [PXDBString(8, InputMask = "CC.CC.CC.CC")]
  [PXUIField(DisplayName = "Report ID")]
  [PXSelector(typeof (Search<SiteMap.screenID, Where<SiteMap.screenID, Like<PXModule.pm_>, And<Where<SiteMap.url, Like<PX.Objects.Common.urlReports>, Or<SiteMap.url, Like<urlReportsInNewUi>>>>>, OrderBy<Asc<SiteMap.screenID>>>), new Type[] {typeof (SiteMap.screenID), typeof (SiteMap.title)}, Headers = new string[] {"Report ID", "Report Name"}, DescriptionField = typeof (SiteMap.title))]
  public override string ReportID
  {
    get => this._ReportID;
    set => this._ReportID = value;
  }

  public new abstract class setupID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  PMNotification.setupID>
  {
  }

  public new abstract class module : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMNotification.module>
  {
  }

  public new abstract class sourceCD : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMNotification.sourceCD>
  {
  }

  public new abstract class notificationCD : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMNotification.notificationCD>
  {
  }

  public new abstract class reportID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMNotification.reportID>
  {
  }

  public abstract class templateID : IBqlField, IBqlOperand
  {
  }

  public new abstract class active : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  PMNotification.active>
  {
  }
}
