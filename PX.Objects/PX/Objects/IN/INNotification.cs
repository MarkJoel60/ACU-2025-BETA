// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.INNotification
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
namespace PX.Objects.IN;

[PXProjection(typeof (Select<NotificationSetup, Where<NotificationSetup.module, Equal<PXModule.@in>>>), Persistent = true)]
[Serializable]
public class INNotification : NotificationSetup
{
  [PXDBString(2, IsFixed = true, IsKey = true)]
  [PXDefault("IN")]
  public override 
  #nullable disable
  string Module
  {
    get => this._Module;
    set => this._Module = value;
  }

  [PXDefault("None")]
  [PXDBString(10, InputMask = ">aaaaaaaaaa")]
  public override string SourceCD
  {
    get => this._SourceCD;
    set => this._SourceCD = value;
  }

  [PXDBString(8, InputMask = "CC.CC.CC.CC")]
  [PXUIField(DisplayName = "Report ID")]
  [PXSelector(typeof (Search<SiteMap.screenID, Where<SiteMap.screenID, Like<PXModule.in_>, And<Where<SiteMap.url, Like<PX.Objects.Common.urlReports>, Or<SiteMap.url, Like<urlReportsInNewUi>>>>>, OrderBy<Asc<SiteMap.screenID>>>), new Type[] {typeof (SiteMap.screenID), typeof (SiteMap.title)}, Headers = new string[] {"Report ID", "Report Name"}, DescriptionField = typeof (SiteMap.title))]
  public override string ReportID
  {
    get => this._ReportID;
    set => this._ReportID = value;
  }

  public new class PK : PrimaryKeyOf<INNotification>.By<INNotification.setupID>
  {
    public static NotificationSetup Find(PXGraph graph, Guid? setupID, PKFindOptions options = 0)
    {
      return (NotificationSetup) PrimaryKeyOf<INNotification>.By<INNotification.setupID>.FindBy(graph, (object) setupID, options);
    }
  }

  public new static class FK
  {
    public class Branch : 
      PrimaryKeyOf<PX.Objects.GL.Branch>.By<PX.Objects.GL.Branch.branchID>.ForeignKeyOf<INNotification>.By<INNotification.nBranchID>
    {
    }

    public class Report : 
      PrimaryKeyOf<SiteMap>.By<SiteMap.screenID>.ForeignKeyOf<INNotification>.By<INNotification.reportID>
    {
    }

    public class DefaultPrinter : 
      PrimaryKeyOf<SMPrinter>.By<SMPrinter.printerID>.ForeignKeyOf<INNotification>.By<INNotification.defaultPrinterID>
    {
    }
  }

  public new abstract class setupID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  INNotification.setupID>
  {
  }

  public new abstract class module : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INNotification.module>
  {
  }

  public new abstract class sourceCD : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INNotification.sourceCD>
  {
  }

  public new abstract class nBranchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INNotification.nBranchID>
  {
  }

  public new abstract class notificationCD : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INNotification.notificationCD>
  {
  }

  public new abstract class reportID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INNotification.reportID>
  {
  }

  public new abstract class defaultPrinterID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    INNotification.defaultPrinterID>
  {
  }

  public abstract class templateID : IBqlField, IBqlOperand
  {
  }

  public new abstract class active : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  INNotification.active>
  {
  }
}
