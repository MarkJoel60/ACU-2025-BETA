// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.DAC.CRNotification
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CA;
using PX.Objects.CS;
using PX.SM;
using System;

#nullable enable
namespace PX.Objects.CR.DAC;

[PXProjection(typeof (Select<NotificationSetup, Where<NotificationSetup.module, Equal<PXModule.cr>>>), Persistent = true)]
[PXCacheName("CR Notification")]
[Serializable]
public class CRNotification : NotificationSetup
{
  /// <summary>
  /// The module to which the mailing source settings belong.
  /// This field is a part of the compound key of the record.
  /// </summary>
  /// <value>
  /// The value of this field is always equal to <see cref="F:PX.Objects.CA.PXModule.CR" />.
  /// </value>
  [PXDBString(2, IsFixed = true, IsKey = true)]
  [PXDefault("CR")]
  public override 
  #nullable disable
  string Module
  {
    get => this._Module;
    set => this._Module = value;
  }

  /// <summary>
  /// The name of the source object of email notifications.
  /// This field is a part of the compound key of the record.
  /// </summary>
  /// <value>
  /// Defaults to <see cref="F:PX.Objects.CR.DAC.CRNotificationSource.BAccount" />.
  /// </value>
  [PXDefault("BAccount")]
  [PXDBString(10, IsKey = true, InputMask = "")]
  public override string SourceCD
  {
    get => this._SourceCD;
    set => this._SourceCD = value;
  }

  /// <summary>
  /// The unique identifier of the report that should be
  /// attached to the notification email.
  /// </summary>
  [PXDBString(8, InputMask = "CC.CC.CC.CC")]
  [PXUIField(DisplayName = "Report")]
  [PXSelector(typeof (Search<SiteMap.screenID, Where<SiteMap.screenID, Like<PXModule.cr_>, And<SiteMap.url, Like<urlReports>>>, OrderBy<Asc<SiteMap.screenID>>>), new System.Type[] {typeof (SiteMap.screenID), typeof (SiteMap.title)}, Headers = new string[] {"Report ID", "Report Name"}, DescriptionField = typeof (SiteMap.title))]
  public override string ReportID
  {
    get => this._ReportID;
    set => this._ReportID = value;
  }

  public new abstract class setupID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  CRNotification.setupID>
  {
  }

  public new abstract class module : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CRNotification.module>
  {
  }

  public new abstract class sourceCD : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CRNotification.sourceCD>
  {
  }

  public new abstract class notificationCD : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CRNotification.notificationCD>
  {
  }

  public new abstract class reportID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CRNotification.reportID>
  {
  }

  public abstract class templateID : IBqlField, IBqlOperand
  {
  }

  public new abstract class active : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CRNotification.active>
  {
  }
}
