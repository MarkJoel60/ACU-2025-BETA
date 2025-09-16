// Decompiled with JetBrains decompiler
// Type: PX.SM.NotificationReport
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using System;

#nullable enable
namespace PX.SM;

[PXCacheName("Notification Report")]
public class NotificationReport : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBGuid(false, IsKey = true)]
  [PXDefault]
  public virtual Guid? ReportID { get; set; }

  [PXDBInt]
  [PXDefault(typeof (Notification.notificationID))]
  [PXParent(typeof (Select<Notification, Where<Notification.notificationID, Equal<Current<NotificationReport.notificationID>>>>))]
  [PXDBChildIdentity(typeof (Notification.notificationID))]
  [PXUIField(DisplayName = "Notification ID")]
  public virtual int? NotificationID { get; set; }

  [PXDefault]
  [PXDBString(8, IsFixed = true, InputMask = "CC.CC.CC.CC")]
  [PXUIField(DisplayName = "Report ID", Visibility = PXUIVisibility.SelectorVisible)]
  [PXSiteMapNodeSelector]
  [PXForeignReference(typeof (NotificationReport.FK.SiteMap))]
  [PXForeignReference(typeof (NotificationReport.FK.PortalMap))]
  public virtual 
  #nullable disable
  string ScreenID { get; set; }

  [PXString(255 /*0xFF*/, IsUnicode = true)]
  [PXUIField(DisplayName = "Title")]
  [PXDependsOnFields(new System.Type[] {typeof (NotificationReport.screenID)})]
  public virtual string Title
  {
    get
    {
      return this.ScreenID != null ? PXSiteMap.Provider.FindSiteMapNodeByScreenIDUnsecure(this.ScreenID).With<PXSiteMapNode, string>((Func<PXSiteMapNode, string>) (_ => _.Title)) : (string) null;
    }
  }

  [PXDBByte]
  [PXIntList(new int[] {2, 1, 0}, new string[] {"Excel", "HTML", "PDF"})]
  [PXUIField(DisplayName = "Report Format")]
  [PXDefault(0)]
  public byte? Format { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Embedded")]
  public bool? Embedded { get; set; }

  [PXDBCreatedDateTime]
  [PXUIField(DisplayName = "Creation Date", Enabled = false)]
  public System.DateTime? CreatedDateTime { get; set; }

  [PXDBByte]
  [PXIntList(new int[] {0, 1}, new string[] {"Parameters Only", "Specified Table Data and Parameters"})]
  [PXDefault(0)]
  public byte? DataToPass { get; set; }

  [PXBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Use Event as Data Source", Visible = false)]
  public bool? PassData
  {
    get
    {
      byte? dataToPass = this.DataToPass;
      int? nullable = dataToPass.HasValue ? new int?((int) dataToPass.GetValueOrDefault()) : new int?();
      int num = 1;
      return new bool?(nullable.GetValueOrDefault() == num & nullable.HasValue);
    }
    set
    {
      bool? nullable = value;
      bool flag = true;
      this.DataToPass = new byte?((byte) (nullable.GetValueOrDefault() == flag & nullable.HasValue));
    }
  }

  [PXDBString(256 /*0x0100*/)]
  [PXStringList]
  [PXUIField(DisplayName = "Source Table", Visible = false)]
  public string TableToPass { get; set; }

  [PXDBGuid(false)]
  [PXSelector(typeof (Search<AUReportProcess.ReportSettings.settingsID, Where<AUReportProcess.ReportSettings.screenID, Equal<Current<NotificationReport.screenID>>, And<AUReportProcess.ReportSettings.isShared, Equal<PX.Data.True>>>>), new System.Type[] {typeof (AUReportProcess.ReportSettings.settingsID), typeof (AUReportProcess.ReportSettings.name), typeof (AUReportProcess.ReportSettings.username)}, SubstituteKey = typeof (AUReportProcess.ReportSettings.name))]
  [PXUIField(DisplayName = "Report Template")]
  public virtual Guid? ReportTemplateID { get; set; }

  public class PK : PrimaryKeyOf<NotificationReport>.By<NotificationReport.reportID>
  {
    public static NotificationReport Find(PXGraph graph, Guid? reportID, PKFindOptions options = PKFindOptions.None)
    {
      return PrimaryKeyOf<NotificationReport>.By<NotificationReport.reportID>.FindBy(graph, (object) reportID, options);
    }
  }

  public static class FK
  {
    public class Notification : 
      PrimaryKeyOf<Notification>.By<Notification.notificationID>.ForeignKeyOf<NotificationReport>.By<NotificationReport.notificationID>
    {
    }

    public class SiteMap : 
      PrimaryKeyOf<SiteMap>.By<SiteMap.screenID>.ForeignKeyOf<NotificationReport>.By<NotificationReport.screenID>
    {
    }

    public class PortalMap : 
      PrimaryKeyOf<PortalMap>.By<PortalMap.screenID>.ForeignKeyOf<NotificationReport>.By<NotificationReport.screenID>
    {
    }
  }

  /// <exclude />
  public abstract class reportID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  NotificationReport.reportID>
  {
  }

  /// <exclude />
  public abstract class notificationID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    NotificationReport.notificationID>
  {
  }

  /// <exclude />
  public abstract class screenID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  NotificationReport.screenID>
  {
  }

  /// <exclude />
  public abstract class title : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  NotificationReport.title>
  {
  }

  public abstract class format : BqlType<
  #nullable enable
  IBqlByte, byte>.Field<
  #nullable disable
  NotificationReport.format>
  {
  }

  public abstract class embedded : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  NotificationReport.embedded>
  {
  }

  /// <exclude />
  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    NotificationReport.createdDateTime>
  {
  }

  public abstract class dataToPass : BqlType<
  #nullable enable
  IBqlByte, byte>.Field<
  #nullable disable
  NotificationReport.dataToPass>
  {
  }

  public abstract class passData : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  NotificationReport.passData>
  {
  }

  public abstract class tableToPass : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    NotificationReport.tableToPass>
  {
  }

  public abstract class reportTemplateID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    NotificationReport.reportTemplateID>
  {
  }
}
