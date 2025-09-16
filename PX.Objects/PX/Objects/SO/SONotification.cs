// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.SONotification
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
namespace PX.Objects.SO;

[PXProjection(typeof (Select<NotificationSetup, Where<NotificationSetup.module, Equal<PXModule.so>>>), Persistent = true)]
[Serializable]
public class SONotification : NotificationSetup
{
  [PXDBString(2, IsFixed = true, IsKey = true)]
  [PXDefault("SO")]
  public override 
  #nullable disable
  string Module
  {
    get => this._Module;
    set => this._Module = value;
  }

  [PXDefault("Customer")]
  [PXDBString(10, InputMask = ">aaaaaaaaaa")]
  public override string SourceCD
  {
    get => this._SourceCD;
    set => this._SourceCD = value;
  }

  [PXDBString(8, InputMask = "CC.CC.CC.CC")]
  [PXUIField(DisplayName = "Report ID")]
  [PXSelector(typeof (Search<SiteMap.screenID, Where<SiteMap.screenID, Like<PXModule.so_>, And<Where<SiteMap.url, Like<PX.Objects.Common.urlReports>, Or<SiteMap.url, Like<urlReportsInNewUi>>>>>, OrderBy<Asc<SiteMap.screenID>>>), new Type[] {typeof (SiteMap.screenID), typeof (SiteMap.title)}, Headers = new string[] {"Report ID", "Report Name"}, DescriptionField = typeof (SiteMap.title))]
  public override string ReportID
  {
    get => this._ReportID;
    set => this._ReportID = value;
  }

  [PXDBString(15, IsUnicode = true)]
  [PXUIField(DisplayName = "Ship Via")]
  [PXSelector(typeof (Search<PX.Objects.CS.Carrier.carrierID>), DescriptionField = typeof (PX.Objects.CS.Carrier.description), CacheGlobal = true)]
  public override string ShipVia { get; set; }

  public new class PK : PrimaryKeyOf<SONotification>.By<SONotification.setupID>
  {
    public static SONotification Find(PXGraph graph, Guid? setupID, PKFindOptions options = 0)
    {
      return (SONotification) PrimaryKeyOf<SONotification>.By<SONotification.setupID>.FindBy(graph, (object) setupID, options);
    }
  }

  public new static class FK
  {
    public class Branch : 
      PrimaryKeyOf<PX.Objects.GL.Branch>.By<PX.Objects.GL.Branch.branchID>.ForeignKeyOf<SONotification>.By<SONotification.nBranchID>
    {
    }

    public class Report : 
      PrimaryKeyOf<SiteMap>.By<SiteMap.nodeID>.ForeignKeyOf<SONotification>.By<SONotification.reportID>
    {
    }

    public class DefaultPrinter : 
      PrimaryKeyOf<SMPrinter>.By<SMPrinter.printerID>.ForeignKeyOf<SONotification>.By<SONotification.defaultPrinterID>
    {
    }

    public class Carrier : 
      PrimaryKeyOf<PX.Objects.CS.Carrier>.By<PX.Objects.CS.Carrier.carrierID>.ForeignKeyOf<SONotification>.By<SONotification.shipVia>
    {
    }
  }

  public new abstract class setupID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  SONotification.setupID>
  {
  }

  public new abstract class module : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SONotification.module>
  {
  }

  public new abstract class sourceCD : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SONotification.sourceCD>
  {
  }

  public new abstract class nBranchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SONotification.nBranchID>
  {
  }

  public new abstract class notificationCD : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SONotification.notificationCD>
  {
  }

  public new abstract class reportID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SONotification.reportID>
  {
  }

  public new abstract class defaultPrinterID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    SONotification.defaultPrinterID>
  {
  }

  public abstract class templateID : IBqlField, IBqlOperand
  {
  }

  public new abstract class active : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SONotification.active>
  {
  }

  public new abstract class shipVia : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SONotification.shipVia>
  {
  }
}
