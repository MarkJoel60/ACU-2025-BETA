// Decompiled with JetBrains decompiler
// Type: PX.SM.NotificationReportParameter
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using System;

#nullable enable
namespace PX.SM;

[PXCacheName("Notification Report Parameter")]
public class NotificationReportParameter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBGuid(false, IsKey = true)]
  [PXUIField]
  [PXDefault(typeof (NotificationReport.reportID))]
  [PXParent(typeof (Select<NotificationReport, Where<NotificationReport.reportID, Equal<Current<NotificationReportParameter.reportID>>>>))]
  public virtual Guid? ReportID { get; set; }

  [PXDefault]
  [PXDBString(64 /*0x40*/, InputMask = "", IsUnicode = true, IsKey = true)]
  [PXUIField(DisplayName = "Parameter Name")]
  [PrimaryViewFieldsList(typeof (NotificationReport.screenID), KeysOnly = true, ShowDisplayNameAsLabel = true, CopyPasteOptimization = false)]
  public virtual 
  #nullable disable
  string Name { get; set; }

  [PXUIField(DisplayName = "Parameter Value")]
  [PrimaryViewValueList(256 /*0x0100*/, typeof (NotificationReport.screenID), typeof (NotificationReportParameter.name))]
  public virtual string Value { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "From Schema")]
  public bool? FromSchema { get; set; }

  [PXBool]
  [PXUIField(DisplayName = "Overridden")]
  public bool? IsOverride { get; set; }

  [PXBool]
  public virtual bool? FromDB { get; set; }

  [PXString]
  public virtual string ScreenID { get; set; }

  public class PK : 
    PrimaryKeyOf<NotificationReportParameter>.By<NotificationReportParameter.reportID, NotificationReportParameter.name>
  {
    public static NotificationReportParameter Find(
      PXGraph graph,
      Guid? reportID,
      string name,
      PKFindOptions options = PKFindOptions.None)
    {
      return PrimaryKeyOf<NotificationReportParameter>.By<NotificationReportParameter.reportID, NotificationReportParameter.name>.FindBy(graph, (object) reportID, (object) name, options);
    }
  }

  public static class FK
  {
    public class NotificationReport : 
      PrimaryKeyOf<NotificationReport>.By<NotificationReport.reportID>.ForeignKeyOf<NotificationReportParameter>.By<NotificationReportParameter.reportID>
    {
    }
  }

  /// <exclude />
  public abstract class reportID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    NotificationReportParameter.reportID>
  {
  }

  /// <exclude />
  public abstract class name : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  NotificationReportParameter.name>
  {
  }

  /// <exclude />
  public abstract class value : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  NotificationReportParameter.value>
  {
  }

  public abstract class fromSchema : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    NotificationReportParameter.fromSchema>
  {
  }

  public abstract class isOverride : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    NotificationReportParameter.isOverride>
  {
  }

  public abstract class fromDB : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  NotificationReportParameter.fromDB>
  {
  }

  public abstract class screenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    NotificationReportParameter.screenID>
  {
  }
}
