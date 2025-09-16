// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.EPTimecardLite
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.Objects.EP;

/// <summary>
/// Timecard (Lite version)
/// This is a projection DAC
/// </summary>
[PXHidden]
[PXProjection(typeof (Select<EPTimeCard>), Persistent = false)]
public class EPTimecardLite : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  /// <summary>Gets sets Timecard Number</summary>
  [PXDBString(10, IsUnicode = true, IsKey = true, InputMask = ">CCCCCCCCCC", BqlField = typeof (EPTimeCard.timeCardCD))]
  public virtual 
  #nullable disable
  string TimeCardCD { get; set; }

  /// <summary>Gets sets Employee</summary>
  [PXDBInt(BqlField = typeof (EPTimeCard.employeeID))]
  public virtual int? EmployeeID { get; set; }

  /// <summary>Gets sets Week Number</summary>
  [PXDBInt(BqlField = typeof (EPTimeCard.weekId))]
  public virtual int? WeekID { get; set; }

  /// <summary>timeCardCD Bql field</summary>
  public abstract class timeCardCD : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  EPTimecardLite.timeCardCD>
  {
  }

  /// <summary>employeeID Bql field</summary>
  public abstract class employeeID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPTimecardLite.employeeID>
  {
  }

  /// <summary>weekId Bql field</summary>
  public abstract class weekId : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPTimecardLite.weekId>
  {
  }
}
