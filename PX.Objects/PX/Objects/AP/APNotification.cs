// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.APNotification
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
using System;

#nullable enable
namespace PX.Objects.AP;

[PXProjection(typeof (Select<NotificationSetup, Where<NotificationSetup.module, Equal<PXModule.ap>>>), Persistent = true)]
[PXCacheName("AP Notification")]
[Serializable]
public class APNotification : NotificationSetup
{
  [PXDBString(2, IsFixed = true, IsKey = true)]
  [PXDefault("AP")]
  public override 
  #nullable disable
  string Module
  {
    get => this._Module;
    set => this._Module = value;
  }

  [PXDefault("Vendor")]
  [PXDBString(10, IsKey = true, InputMask = ">aaaaaaaaaa")]
  public override string SourceCD
  {
    get => this._SourceCD;
    set => this._SourceCD = value;
  }

  [PXDBString(8, InputMask = "CC.CC.CC.CC")]
  [PXUIField(DisplayName = "Report")]
  [PXSelector(typeof (Search<PX.SM.SiteMap.screenID, Where<PX.SM.SiteMap.screenID, Like<PXModule.ap_>, PX.Data.And<Where<PX.SM.SiteMap.url, Like<PX.Objects.Common.urlReports>, Or<PX.SM.SiteMap.url, Like<urlReportsInNewUi>>>>>, OrderBy<Asc<PX.SM.SiteMap.screenID>>>), new System.Type[] {typeof (PX.SM.SiteMap.screenID), typeof (PX.SM.SiteMap.title)}, Headers = new string[] {"Report ID", "Report Name"}, DescriptionField = typeof (PX.SM.SiteMap.title))]
  public override string ReportID
  {
    get => this._ReportID;
    set => this._ReportID = value;
  }

  public new class PK : PrimaryKeyOf<APNotification>.By<APNotification.setupID>
  {
    public static APNotification Find(PXGraph graph, Guid? setupID, PKFindOptions options = PKFindOptions.None)
    {
      return PrimaryKeyOf<APNotification>.By<APNotification.setupID>.FindBy(graph, (object) setupID, options);
    }
  }

  public new abstract class setupID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  APNotification.setupID>
  {
  }

  public new abstract class module : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APNotification.module>
  {
  }

  public new abstract class sourceCD : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APNotification.sourceCD>
  {
  }

  public new abstract class nBranchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  APNotification.nBranchID>
  {
  }

  public new abstract class notificationCD : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    APNotification.notificationCD>
  {
  }

  public new abstract class reportID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APNotification.reportID>
  {
  }

  public new abstract class defaultPrinterID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    APNotification.defaultPrinterID>
  {
  }

  public abstract class templateID : IBqlField, IBqlOperand
  {
  }

  public new abstract class active : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  APNotification.active>
  {
  }
}
