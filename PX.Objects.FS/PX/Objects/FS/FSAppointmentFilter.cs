// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.FSAppointmentFilter
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;
using PX.SM;
using System;

#nullable enable
namespace PX.Objects.FS;

[PXHidden]
[Serializable]
public class FSAppointmentFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected DateTime? _StartDate;
  protected DateTime? _StartDateWithTime;
  protected DateTime? _EndDateWithTime;

  [PXString(2, IsFixed = true)]
  [PXUIField(DisplayName = "Type")]
  [PXUnboundDefault("AP")]
  [ListField_ROType.ListAtrribute]
  public virtual 
  #nullable disable
  string Type { get; set; }

  [PXBool]
  [PXUIField(DisplayName = "Consider Skills")]
  [PXUIVisible(typeof (Where<FSAppointmentFilter.type, Equal<ListField_ROType.UnassignedApp>>))]
  public virtual bool? ConsiderSkills { get; set; }

  [PXInt]
  [PXDefault(typeof (AccessInfo.branchID))]
  [PXUIField(DisplayName = "Branch")]
  [PXSelector(typeof (Search<PX.Objects.GL.Branch.branchID>), SubstituteKey = typeof (PX.Objects.GL.Branch.branchCD), DescriptionField = typeof (PX.Objects.GL.Branch.acctName))]
  public virtual int? BranchID { get; set; }

  [PXInt]
  [PXDefault(typeof (Search<FSxUserPreferences.dfltBranchLocationID, Where<UserPreferences.userID, Equal<CurrentValue<AccessInfo.userID>>, And<UserPreferences.defBranchID, Equal<Current<FSAppointmentFilter.branchID>>>>>))]
  [PXUIField(DisplayName = "Branch Location")]
  [PXSelector(typeof (Search<FSBranchLocation.branchLocationID, Where<FSBranchLocation.branchID, Equal<Current<FSAppointmentFilter.branchID>>>>), SubstituteKey = typeof (FSBranchLocation.branchLocationCD), DescriptionField = typeof (FSBranchLocation.descr))]
  [PXFormula(typeof (Default<FSAppointmentFilter.branchID>))]
  public virtual int? BranchLocationID { get; set; }

  [PXDBDate(UseTimeZone = true)]
  [PXDefault(typeof (AccessInfo.businessDate))]
  [PXUIField(DisplayName = "Date")]
  public virtual DateTime? StartDate
  {
    get => this._StartDate;
    set
    {
      this._StartDate = value;
      if (!this._StartDate.HasValue)
      {
        this._StartDateWithTime = new DateTime?();
        this._EndDateWithTime = new DateTime?();
      }
      else
      {
        this._StartDateWithTime = new DateTime?(new DateTime(this._StartDate.Value.Year, this._StartDate.Value.Month, this._StartDate.Value.Day, 0, 0, 0));
        DateTime dateTime = this._StartDate.Value;
        int year = dateTime.Year;
        dateTime = this._StartDate.Value;
        int month = dateTime.Month;
        int day = this._StartDate.Value.Day;
        this._EndDateWithTime = new DateTime?(new DateTime(year, month, day, 23, 59, 59));
      }
    }
  }

  [PXDBDateAndTime(UseTimeZone = true)]
  public virtual DateTime? StartDateWithTime => this._StartDateWithTime;

  [PXDBDateAndTime(UseTimeZone = true)]
  public virtual DateTime? EndDateWithTime => this._EndDateWithTime;

  public abstract class type : ListField_ROType
  {
  }

  public abstract class considerSkills : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSAppointmentFilter.considerSkills>
  {
  }

  public abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSAppointmentFilter.branchID>
  {
  }

  public abstract class branchLocationID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSAppointmentFilter.branchLocationID>
  {
  }

  public abstract class startDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FSAppointmentFilter.startDate>
  {
  }

  public abstract class startDateWithTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FSAppointmentFilter.startDateWithTime>
  {
  }

  public abstract class endDateWithTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FSAppointmentFilter.endDateWithTime>
  {
  }
}
