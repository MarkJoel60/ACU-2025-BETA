// Decompiled with JetBrains decompiler
// Type: PX.Objects.CS.DaylightShift
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.CS;

[PXCacheName("Daylight Shift")]
[Serializable]
public class DaylightShift : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  private DateTime? _toDate;

  [PXDBInt(IsKey = true)]
  [PXUIField(DisplayName = "Year")]
  public virtual int? Year { get; set; }

  [PXDBString(9, IsKey = true)]
  [PXUIField(Visible = false)]
  public virtual 
  #nullable disable
  string TimeZone { get; set; }

  [PXString(IsUnicode = true)]
  [PXUIField(DisplayName = "Time Zone", Enabled = false)]
  public virtual string TimeZoneDescription { get; set; }

  [PXDBBool]
  [PXUIField(DisplayName = "Custom")]
  public virtual bool? IsActive { get; set; }

  [PXDBDateAndTime(UseTimeZone = false, PreserveTime = true)]
  [PXUIField(DisplayName = "From")]
  public virtual DateTime? FromDate { get; set; }

  [PXDBDateAndTime(UseTimeZone = false, PreserveTime = true)]
  [PXUIField(DisplayName = "To")]
  public virtual DateTime? ToDate
  {
    get => this._toDate;
    set => this._toDate = value;
  }

  [PXDBInt(MinValue = -360, MaxValue = 360)]
  [PXUIField(DisplayName = "Shift (minutes)")]
  public virtual int? Shift { get; set; }

  [PXDouble]
  [PXUIField(Visible = false)]
  public virtual double? OriginalShift { get; set; }

  public abstract class year : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  DaylightShift.year>
  {
  }

  public abstract class timeZone : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  DaylightShift.timeZone>
  {
  }

  public abstract class timeZoneDescription : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    DaylightShift.timeZoneDescription>
  {
  }

  public abstract class isActive : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  DaylightShift.isActive>
  {
  }

  public abstract class fromDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  DaylightShift.fromDate>
  {
  }

  public abstract class toDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  DaylightShift.toDate>
  {
  }

  public abstract class shift : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  DaylightShift.shift>
  {
  }

  public abstract class originalShift : 
    BqlType<
    #nullable enable
    IBqlDouble, double>.Field<
    #nullable disable
    DaylightShift.originalShift>
  {
  }
}
