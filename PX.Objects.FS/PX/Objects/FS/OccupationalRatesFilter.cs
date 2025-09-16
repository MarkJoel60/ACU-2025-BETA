// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.OccupationalRatesFilter
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.FS;

[Serializable]
public class OccupationalRatesFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXString(1, IsFixed = true)]
  [PXDefault("W")]
  [PXUIField(DisplayName = "Period")]
  [ListField_Period_Appointment.ListAtrribute]
  public virtual 
  #nullable disable
  string PeriodType { get; set; }

  [PXDateAndTime(UseTimeZone = true)]
  [PXUIField(DisplayName = "Date in Range")]
  public virtual DateTime? DateInRange { get; set; }

  [PXDateAndTime(UseTimeZone = true)]
  [PXUIField(DisplayName = "Begin Date", Enabled = false, Visible = true)]
  public virtual DateTime? DateBegin { get; set; }

  [PXDateAndTime(UseTimeZone = true)]
  [PXUIField(DisplayName = "End Date", Enabled = false, Visible = true)]
  public virtual DateTime? DateEnd { get; set; }

  public abstract class periodType : ListField_Period_Appointment
  {
  }

  public abstract class dateInRange : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    OccupationalRatesFilter.dateInRange>
  {
  }

  public abstract class dateBegin : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    OccupationalRatesFilter.dateBegin>
  {
  }

  public abstract class dateEnd : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    OccupationalRatesFilter.dateEnd>
  {
  }
}
