// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.ARNotification
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CA;
using PX.Objects.Common;
using PX.Objects.CS;
using PX.SM;
using System;

#nullable enable
namespace PX.Objects.AR;

[PXProjection(typeof (Select<NotificationSetup, Where<NotificationSetup.module, Equal<PXModule.ar>>>), Persistent = true)]
[PXCacheName("AR Notification")]
[Serializable]
public class ARNotification : NotificationSetup
{
  /// <summary>
  /// The module to which the mailing source settings belong.
  /// This field is a part of the compound key of the record.
  /// </summary>
  /// <value>
  /// The value of this field is always equal to <see cref="F:PX.Objects.CA.PXModule.AR" />.
  /// </value>
  [PXDBString(2, IsFixed = true, IsKey = true)]
  [PXDefault("AR")]
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
  /// Defaults to <see cref="F:PX.Objects.AR.ARNotificationSource.Customer" />.
  /// </value>
  [PXDefault("Customer")]
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
  [PXSelector(typeof (Search<SiteMap.screenID, Where<SiteMap.screenID, Like<PXModule.ar_>, And<Where<SiteMap.url, Like<PX.Objects.Common.urlReports>, Or<SiteMap.url, Like<urlReportsInNewUi>>>>>, OrderBy<Asc<SiteMap.screenID>>>), new Type[] {typeof (SiteMap.screenID), typeof (SiteMap.title)}, Headers = new string[] {"Report ID", "Report Name"}, DescriptionField = typeof (SiteMap.title))]
  public override string ReportID
  {
    get => this._ReportID;
    set => this._ReportID = value;
  }

  public new class PK : PrimaryKeyOf<ARNotification>.By<ARNotification.setupID>
  {
    public static ARNotification Find(PXGraph graph, Guid? setupID, PKFindOptions options = 0)
    {
      return (ARNotification) PrimaryKeyOf<ARNotification>.By<ARNotification.setupID>.FindBy(graph, (object) setupID, options);
    }
  }

  public new abstract class setupID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  ARNotification.setupID>
  {
  }

  public new abstract class module : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARNotification.module>
  {
  }

  public new abstract class sourceCD : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARNotification.sourceCD>
  {
  }

  public new abstract class nBranchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARNotification.nBranchID>
  {
  }

  public new abstract class notificationCD : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARNotification.notificationCD>
  {
  }

  public new abstract class reportID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARNotification.reportID>
  {
  }

  public new abstract class defaultPrinterID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    ARNotification.defaultPrinterID>
  {
  }

  public abstract class templateID : IBqlField, IBqlOperand
  {
  }

  public new abstract class active : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARNotification.active>
  {
  }
}
