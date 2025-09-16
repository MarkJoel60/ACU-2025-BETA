// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.EPSummaryFilter
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.PM;
using System;

#nullable enable
namespace PX.Objects.EP;

[PXHidden]
[Serializable]
public class EPSummaryFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected int? _ApproverID;

  [PXDBInt]
  [PXSubordinateSelector]
  [PXDefault(typeof (Search<EPEmployee.bAccountID, Where<EPEmployee.userID, Equal<Current<AccessInfo.userID>>>>))]
  [PXUIField]
  public virtual int? ApproverID { get; set; }

  [PXDBInt]
  [PXSubordinateSelector]
  [PXUIField(DisplayName = "Employee")]
  public virtual int? EmployeeID { set; get; }

  [PXDBInt]
  [PXWeekSelector2(DescriptionField = typeof (EPWeekRaw.shortDescription))]
  [PXUIField]
  public virtual int? FromWeek { set; get; }

  [PXDBInt]
  [PXWeekSelector2(DescriptionField = typeof (EPWeekRaw.shortDescription))]
  [PXUIField]
  public virtual int? TillWeek { set; get; }

  [PXRestrictor(typeof (Where<PMProject.isActive, Equal<True>>), "The {0} project or contract is inactive.", new Type[] {typeof (PMProject.contractCD)})]
  [ProjectBase(DisplayName = "Project")]
  public virtual int? ProjectID { set; get; }

  [ProjectTask(typeof (EPSummaryFilter.projectID))]
  public virtual int? ProjectTaskID { set; get; }

  [PXInt]
  [PXTimeList]
  [PXUIField]
  public virtual int? RegularTime { set; get; }

  [PXInt]
  [PXTimeList]
  [PXUIField]
  public virtual int? RegularOvertime { set; get; }

  [PXInt]
  [PXTimeList]
  [PXUIField]
  public virtual int? RegularTotal { set; get; }

  public abstract class approverID : BqlType<IBqlInt, int>.Field<
  #nullable disable
  EPSummaryFilter.approverID>
  {
  }

  public abstract class employeeID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPSummaryFilter.employeeID>
  {
  }

  public abstract class fromWeek : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPSummaryFilter.fromWeek>
  {
  }

  public abstract class tillWeek : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPSummaryFilter.tillWeek>
  {
  }

  public abstract class projectID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPSummaryFilter.projectID>
  {
  }

  public abstract class projectTaskID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPSummaryFilter.projectTaskID>
  {
  }

  public abstract class regularTime : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPSummaryFilter.regularTime>
  {
  }

  public abstract class regularOvertime : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    EPSummaryFilter.regularOvertime>
  {
  }

  public abstract class regularTotal : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPSummaryFilter.regularTotal>
  {
  }
}
