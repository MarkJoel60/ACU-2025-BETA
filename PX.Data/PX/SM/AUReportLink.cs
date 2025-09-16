// Decompiled with JetBrains decompiler
// Type: PX.SM.AUReportLink
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

[PXCacheName("Report Link")]
public class AUReportLink : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected int? _ScheduleID;
  protected short? _RowNbr;

  [PXDBGuid(false, IsKey = true)]
  public virtual Guid? TemplateID { get; set; }

  [PXDBInt(IsKey = true)]
  [PXDefault]
  public virtual int? ScheduleID
  {
    get => this._ScheduleID;
    set => this._ScheduleID = value;
  }

  [PXDBShort(IsKey = true)]
  [PXDefault]
  public virtual short? RowNbr
  {
    get => this._RowNbr;
    set => this._RowNbr = value;
  }

  public class PK : 
    PrimaryKeyOf<
    #nullable disable
    AUReportLink>.By<AUReportLink.templateID, AUReportLink.scheduleID, AUReportLink.rowNbr>
  {
    public static AUReportLink Find(
      PXGraph graph,
      Guid? templateID,
      int? scheduleID,
      short? rowNbr,
      PKFindOptions options = PKFindOptions.None)
    {
      return PrimaryKeyOf<AUReportLink>.By<AUReportLink.templateID, AUReportLink.scheduleID, AUReportLink.rowNbr>.FindBy(graph, (object) templateID, (object) scheduleID, (object) rowNbr, options);
    }
  }

  public static class FK
  {
    public class Schedule : 
      PrimaryKeyOf<AUSchedule>.By<AUSchedule.scheduleID>.ForeignKeyOf<AUReportLink>.By<AUReportLink.scheduleID>
    {
    }
  }

  public abstract class templateID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  AUReportLink.templateID>
  {
  }

  public abstract class scheduleID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  AUReportLink.scheduleID>
  {
  }

  public abstract class rowNbr : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  AUReportLink.rowNbr>
  {
  }
}
