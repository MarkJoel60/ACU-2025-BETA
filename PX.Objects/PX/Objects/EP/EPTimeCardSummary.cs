// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.EPTimeCardSummary
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CR;
using PX.Objects.IN;
using PX.Objects.PM;
using System;

#nullable enable
namespace PX.Objects.EP;

/// <summary>
/// Aggregates the information on the activities performed by an employee during a week according to the project, labor item, and day. The information will be displayed on the Employee Time Cards (EP406000) form.
/// </summary>
[PXCacheName("Time Card Summary")]
[Serializable]
public class EPTimeCardSummary : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBDefault(typeof (EPTimeCard.timeCardCD))]
  [PXDBString(10, IsKey = true)]
  [PXUIField(Visible = false)]
  [PXParent(typeof (Select<EPTimeCard, Where<EPTimeCard.timeCardCD, Equal<Current<EPTimeCardSummary.timeCardCD>>>>))]
  public virtual 
  #nullable disable
  string TimeCardCD { get; set; }

  [PXDBInt(IsKey = true)]
  [PXLineNbr(typeof (EPTimeCard.summaryLineCntr))]
  [PXUIField(Visible = false)]
  public virtual int? LineNbr { get; set; }

  [PXDBString(15, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC")]
  [PXDefault(typeof (Search<EPSetup.regularHoursType>))]
  [PXRestrictor(typeof (Where<EPEarningType.isActive, Equal<True>>), "The earning type {0} selected on the Time & Expenses Preferences (EP101000) form is inactive. Inactive earning types are not available for data entry in new activities and time entries.", new System.Type[] {typeof (EPEarningType.typeCD)})]
  [PXSelector(typeof (EPEarningType.typeCD))]
  [PXUIField(DisplayName = "Earning Type")]
  public virtual string EarningType { get; set; }

  [PXDBInt]
  public virtual int? JobID { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "Shift Code", FieldClass = "ShiftDifferential")]
  [TimeCardShiftCodeSelector(typeof (EPTimeCardSummary.employeeID), typeof (EPTimeCard.weekEndDate))]
  [EPShiftCodeActiveRestrictor]
  public virtual int? ShiftID { get; set; }

  [PXUIField(DisplayName = "Task ID")]
  [PXDBGuid(false)]
  [CRTaskSelector]
  [PXForeignReference(typeof (Field<EPTimeCardSummary.parentNoteID>.IsRelatedTo<CRActivity.noteID>))]
  [PXRestrictor(typeof (Where<CRActivity.ownerID, Equal<Current<AccessInfo.contactID>>>), null, new System.Type[] {})]
  public virtual Guid? ParentNoteID { get; set; }

  [ProjectDefault("TA", ForceProjectExplicitly = true)]
  [EPTimeCardProject]
  public virtual int? ProjectID { get; set; }

  [PXDefault(typeof (Search<PMTask.taskID, Where<PMTask.projectID, Equal<Current<EPTimeCardSummary.projectID>>, And<PMTask.isDefault, Equal<True>>>>))]
  [EPTimecardProjectTask(typeof (EPTimeCardSummary.projectID), "TA", DisplayName = "Project Task")]
  [PXForeignReference(typeof (CompositeKey<Field<EPTimeCardSummary.projectID>.IsRelatedTo<PMTask.projectID>, Field<EPTimeCardSummary.projectTaskID>.IsRelatedTo<PMTask.taskID>>))]
  public virtual int? ProjectTaskID { get; set; }

  [CostCode(null, typeof (EPTimeCardSummary.projectTaskID), "E")]
  public virtual int? CostCodeID { get; set; }

  [PXDBBool]
  [PXDefault(typeof (Coalesce<Search<PMProject.certifiedJob, Where<PMProject.contractID, Equal<Current<EPTimeCardSummary.projectID>>>>, Search<PMProject.certifiedJob, Where<PMProject.nonProject, Equal<True>>>>))]
  [PXUIField(DisplayName = "Certified Job", FieldClass = "Construction")]
  public virtual bool? CertifiedJob { get; set; }

  [PXForeignReference(typeof (Field<EPTimeCardSummary.unionID>.IsRelatedTo<PMUnion.unionID>))]
  [PMUnion(typeof (EPTimeCardSummary.projectID), typeof (Select<EPEmployee, Where<EPEmployee.bAccountID, Equal<Current<EPTimeCard.employeeID>>>>))]
  public virtual string UnionID { get; set; }

  [PMActiveLaborItem(typeof (EPTimeCardSummary.projectID), typeof (EPTimeCardSummary.earningType), typeof (Select<EPEmployee, Where<EPEmployee.bAccountID, Equal<Current<EPTimeCard.employeeID>>>>))]
  [PXForeignReference(typeof (Field<EPTimeCardSummary.labourItemID>.IsRelatedTo<PX.Objects.IN.InventoryItem.inventoryID>))]
  public virtual int? LabourItemID { get; set; }

  [PXForeignReference(typeof (Field<EPTimeCardSummary.workCodeID>.IsRelatedTo<PMWorkCode.workCodeID>))]
  [PMWorkCode(typeof (EPTimeCardSummary.costCodeID), typeof (EPTimeCardSummary.projectID), typeof (EPTimeCardSummary.projectTaskID), typeof (EPTimeCardSummary.labourItemID), typeof (EPTimeCardSummary.employeeID))]
  public virtual string WorkCodeID { get; set; }

  [PXInt]
  [PXTimeList]
  [PXDependsOnFields(new System.Type[] {typeof (EPTimeCardSummary.mon), typeof (EPTimeCardSummary.tue), typeof (EPTimeCardSummary.wed), typeof (EPTimeCardSummary.thu), typeof (EPTimeCardSummary.fri), typeof (EPTimeCardSummary.sat), typeof (EPTimeCardSummary.sun)})]
  [PXUIField(DisplayName = "Time Spent", Enabled = false)]
  public virtual int? TimeSpent
  {
    get
    {
      int? nullable = this.Mon;
      int valueOrDefault1 = nullable.GetValueOrDefault();
      nullable = this.Tue;
      int valueOrDefault2 = nullable.GetValueOrDefault();
      int num1 = valueOrDefault1 + valueOrDefault2;
      nullable = this.Wed;
      int valueOrDefault3 = nullable.GetValueOrDefault();
      int num2 = num1 + valueOrDefault3;
      nullable = this.Thu;
      int valueOrDefault4 = nullable.GetValueOrDefault();
      int num3 = num2 + valueOrDefault4;
      nullable = this.Fri;
      int valueOrDefault5 = nullable.GetValueOrDefault();
      int num4 = num3 + valueOrDefault5;
      nullable = this.Sat;
      int valueOrDefault6 = nullable.GetValueOrDefault();
      int num5 = num4 + valueOrDefault6;
      nullable = this.Sun;
      int valueOrDefault7 = nullable.GetValueOrDefault();
      return new int?(num5 + valueOrDefault7);
    }
  }

  [PXTimeList]
  [PXDBInt]
  [PXUIField(DisplayName = "Sun")]
  public virtual int? Sun { get; set; }

  [PXTimeList]
  [PXDBInt]
  [PXUIField(DisplayName = "Mon")]
  public virtual int? Mon { get; set; }

  [PXTimeList]
  [PXDBInt]
  [PXUIField(DisplayName = "Tue")]
  public virtual int? Tue { get; set; }

  [PXTimeList]
  [PXDBInt]
  [PXUIField(DisplayName = "Wed")]
  public virtual int? Wed { get; set; }

  [PXTimeList]
  [PXDBInt]
  [PXUIField(DisplayName = "Thu")]
  public virtual int? Thu { get; set; }

  [PXTimeList]
  [PXDBInt]
  [PXUIField(DisplayName = "Fri")]
  public virtual int? Fri { get; set; }

  [PXTimeList]
  [PXDBInt]
  [PXUIField(DisplayName = "Sat")]
  public virtual int? Sat { get; set; }

  [PXDBBool]
  [PXDefault]
  [PXUIField(DisplayName = "Billable")]
  public virtual bool? IsBillable { get; set; }

  [PXDBString(255 /*0xFF*/, IsUnicode = true)]
  [PXUIField(DisplayName = "Description")]
  public virtual string Description { get; set; }

  [PXInt]
  [PXUnboundDefault(typeof (Parent<EPTimeCard.employeeID>))]
  public int? EmployeeID { get; set; }

  [PXNote]
  public virtual Guid? NoteID { get; set; }

  [PXDBTimestamp]
  public virtual byte[] tstamp { get; set; }

  [PXDBCreatedByID]
  public virtual Guid? CreatedByID { get; set; }

  [PXDBCreatedByScreenID]
  public virtual string CreatedByScreenID { get; set; }

  [PXDBCreatedDateTime]
  public virtual DateTime? CreatedDateTime { get; set; }

  [PXDBLastModifiedByID]
  public virtual Guid? LastModifiedByID { get; set; }

  [PXDBLastModifiedByScreenID]
  public virtual string LastModifiedByScreenID { get; set; }

  [PXDBLastModifiedDateTime]
  public virtual DateTime? LastModifiedDateTime { get; set; }

  /// <summary>
  /// Stores Employee's Hourly rate at the time the activity was released to PM
  /// </summary>
  [PXPriceCost]
  [PXUIField(DisplayName = "Cost Rate", Enabled = false)]
  public virtual Decimal? EmployeeRate { get; set; }

  public int? GetTimeTotal(DayOfWeek day)
  {
    switch (day)
    {
      case DayOfWeek.Sunday:
        return this.Sun;
      case DayOfWeek.Monday:
        return this.Mon;
      case DayOfWeek.Tuesday:
        return this.Tue;
      case DayOfWeek.Wednesday:
        return this.Wed;
      case DayOfWeek.Thursday:
        return this.Thu;
      case DayOfWeek.Friday:
        return this.Fri;
      case DayOfWeek.Saturday:
        return this.Sat;
      default:
        return new int?();
    }
  }

  public int? GetTimeTotal()
  {
    int? nullable = this.Mon;
    int valueOrDefault1 = nullable.GetValueOrDefault();
    nullable = this.Tue;
    int valueOrDefault2 = nullable.GetValueOrDefault();
    int num1 = valueOrDefault1 + valueOrDefault2;
    nullable = this.Wed;
    int valueOrDefault3 = nullable.GetValueOrDefault();
    int num2 = num1 + valueOrDefault3;
    nullable = this.Thu;
    int valueOrDefault4 = nullable.GetValueOrDefault();
    int num3 = num2 + valueOrDefault4;
    nullable = this.Fri;
    int valueOrDefault5 = nullable.GetValueOrDefault();
    int num4 = num3 + valueOrDefault5;
    nullable = this.Sat;
    int valueOrDefault6 = nullable.GetValueOrDefault();
    int num5 = num4 + valueOrDefault6;
    nullable = this.Sun;
    int valueOrDefault7 = nullable.GetValueOrDefault();
    return new int?(num5 + valueOrDefault7);
  }

  public void SetDayTime(DayOfWeek day, int minutes)
  {
    switch (day)
    {
      case DayOfWeek.Sunday:
        this.Sun = new int?(minutes);
        break;
      case DayOfWeek.Monday:
        this.Mon = new int?(minutes);
        break;
      case DayOfWeek.Tuesday:
        this.Tue = new int?(minutes);
        break;
      case DayOfWeek.Wednesday:
        this.Wed = new int?(minutes);
        break;
      case DayOfWeek.Thursday:
        this.Thu = new int?(minutes);
        break;
      case DayOfWeek.Friday:
        this.Fri = new int?(minutes);
        break;
      case DayOfWeek.Saturday:
        this.Sat = new int?(minutes);
        break;
    }
  }

  public virtual string ToString()
  {
    return $"{this.EarningType} {this.Mon} {this.Tue} {this.Wed} {this.Thu} {this.Fri} {this.Sat} {this.Sun}";
  }

  /// <summary>Primary Key</summary>
  public class PK : 
    PrimaryKeyOf<EPTimeCardSummary>.By<EPTimeCardSummary.timeCardCD, EPTimeCardSummary.lineNbr>
  {
    public static EPTimeCardSummary Find(
      PXGraph graph,
      string timeCardCD,
      int? lineNbr,
      PKFindOptions options = 0)
    {
      return (EPTimeCardSummary) PrimaryKeyOf<EPTimeCardSummary>.By<EPTimeCardSummary.timeCardCD, EPTimeCardSummary.lineNbr>.FindBy(graph, (object) timeCardCD, (object) lineNbr, options);
    }
  }

  /// <summary>Foreign Keys</summary>
  public static class FK
  {
    /// <summary>Time Card</summary>
    public class Timecard : 
      PrimaryKeyOf<EPTimeCard>.By<EPTimeCard.timeCardCD>.ForeignKeyOf<EPTimeCardSummary>.By<EPTimeCardSummary.timeCardCD>
    {
    }

    /// <summary>Earning Type</summary>
    public class EarningType : 
      PrimaryKeyOf<EPEarningType>.By<EPEarningType.typeCD>.ForeignKeyOf<EPTimeCardSummary>.By<EPTimeCardSummary.earningType>
    {
    }

    /// <summary>Project</summary>
    public class Project : 
      PrimaryKeyOf<PMProject>.By<PMProject.contractID>.ForeignKeyOf<EPTimeCardSummary>.By<EPTimeCardSummary.projectID>
    {
    }

    /// <summary>Project Task</summary>
    public class ProjectTask : 
      PrimaryKeyOf<PMTask>.By<PMTask.projectID, PMTask.taskID>.ForeignKeyOf<EPTimeCardSummary>.By<EPTimeCardSummary.projectID, EPTimeCardSummary.projectTaskID>
    {
    }

    /// <summary>Cost Code</summary>
    public class CostCode : 
      PrimaryKeyOf<PMCostCode>.By<PMCostCode.costCodeID>.ForeignKeyOf<EPTimeCardSummary>.By<EPTimeCardSummary.costCodeID>
    {
    }

    /// <summary>Union</summary>
    public class Union : 
      PrimaryKeyOf<PMUnion>.By<PMUnion.unionID>.ForeignKeyOf<EPTimeCardSummary>.By<EPTimeCardSummary.unionID>
    {
    }

    /// <summary>Work Code</summary>
    public class WorkCode : 
      PrimaryKeyOf<PMWorkCode>.By<PMWorkCode.workCodeID>.ForeignKeyOf<EPTimeCardSummary>.By<EPTimeCardSummary.workCodeID>
    {
    }

    /// <summary>Labor Item</summary>
    public class LaborItem : 
      PrimaryKeyOf<PX.Objects.IN.InventoryItem>.By<PX.Objects.IN.InventoryItem.inventoryID>.ForeignKeyOf<EPTimeCardSummary>.By<EPTimeCardSummary.labourItemID>
    {
    }

    /// <summary>Shift Code</summary>
    public class ShiftCode : 
      PrimaryKeyOf<EPShiftCode>.By<EPShiftCode.shiftID>.ForeignKeyOf<EPTimeCardSummary>.By<EPTimeCardSummary.shiftID>
    {
    }
  }

  public abstract class timeCardCD : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  EPTimeCardSummary.timeCardCD>
  {
  }

  public abstract class lineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPTimeCardSummary.lineNbr>
  {
  }

  public abstract class earningType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    EPTimeCardSummary.earningType>
  {
  }

  public abstract class jobID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPTimeCardSummary.jobID>
  {
  }

  public abstract class shiftID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPTimeCardSummary.shiftID>
  {
  }

  public abstract class parentNoteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  EPTimeCardSummary.parentNoteID>
  {
  }

  public abstract class projectID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPTimeCardSummary.projectID>
  {
  }

  public abstract class projectTaskID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPTimeCardSummary.projectTaskID>
  {
  }

  public abstract class costCodeID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPTimeCardSummary.costCodeID>
  {
  }

  public abstract class certifiedJob : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  EPTimeCardSummary.certifiedJob>
  {
  }

  public abstract class unionID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  EPTimeCardSummary.unionID>
  {
  }

  public abstract class labourItemID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPTimeCardSummary.labourItemID>
  {
  }

  public abstract class workCodeID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  EPTimeCardSummary.workCodeID>
  {
  }

  public abstract class timeSpent : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPTimeCardSummary.timeSpent>
  {
  }

  public abstract class sun : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPTimeCardSummary.sun>
  {
  }

  public abstract class mon : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPTimeCardSummary.mon>
  {
  }

  public abstract class tue : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPTimeCardSummary.tue>
  {
  }

  public abstract class wed : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPTimeCardSummary.wed>
  {
  }

  public abstract class thu : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPTimeCardSummary.thu>
  {
  }

  public abstract class fri : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPTimeCardSummary.fri>
  {
  }

  public abstract class sat : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPTimeCardSummary.sat>
  {
  }

  public abstract class isBillable : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  EPTimeCardSummary.isBillable>
  {
  }

  public abstract class description : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    EPTimeCardSummary.description>
  {
  }

  public abstract class employeeID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPTimeCardSummary.employeeID>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  EPTimeCardSummary.noteID>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  EPTimeCardSummary.Tstamp>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  EPTimeCardSummary.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    EPTimeCardSummary.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    EPTimeCardSummary.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    EPTimeCardSummary.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    EPTimeCardSummary.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    EPTimeCardSummary.lastModifiedDateTime>
  {
  }

  public abstract class employeeRate : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    EPTimeCardSummary.employeeRate>
  {
  }
}
