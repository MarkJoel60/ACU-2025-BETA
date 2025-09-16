// Decompiled with JetBrains decompiler
// Type: PX.Objects.CN.Compliance.CL.DAC.ComplianceNotification
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CA;
using PX.Objects.Common;
using PX.Objects.CS;
using PX.SM;
using System;

#nullable disable
namespace PX.Objects.CN.Compliance.CL.DAC;

[PXProjection(typeof (Select<NotificationSetup, Where<NotificationSetup.module, Equal<PXModule.cl>>>), Persistent = true)]
[PXCacheName("Compliance Notification")]
public class ComplianceNotification : NotificationSetup
{
  [PXDBString(2, IsFixed = true, IsKey = true)]
  [PXDefault("CL")]
  public override string Module { get; set; }

  [PXDefault("Vendor")]
  [PXDBString(10, IsKey = true, InputMask = "")]
  public override string SourceCD { get; set; }

  [PXDBString(8, InputMask = "CC.CC.CC.CC")]
  [PXUIField(DisplayName = "Report")]
  [PXSelector(typeof (Search<SiteMap.screenID, Where<SiteMap.screenID, Like<PXModule.cl_>, And<Where<SiteMap.url, Like<PX.Objects.Common.urlReports>, Or<SiteMap.url, Like<urlReportsInNewUi>>>>>, OrderBy<Asc<SiteMap.screenID>>>), new Type[] {typeof (SiteMap.screenID), typeof (SiteMap.title)}, Headers = new string[] {"Report ID", "Report Name"}, DescriptionField = typeof (SiteMap.title))]
  public override string ReportID { get; set; }
}
