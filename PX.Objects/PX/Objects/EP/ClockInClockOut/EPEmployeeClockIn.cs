// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.ClockInClockOut.EPEmployeeClockIn
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.Objects.EP.ClockInClockOut;

/// <summary>
/// Extends <see cref="T:PX.Objects.EP.EPEmployee" /> to add the clock-in and clock-out functionality.
/// </summary>
[PXInternalUseOnly]
public sealed class EPEmployeeClockIn : PXCacheExtension<
#nullable disable
PX.Objects.EP.EPEmployee>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<PX.Objects.CS.FeaturesSet.clockInClockOut>();

  /// <summary>
  /// The unique identifier of the current timer associated with this employee.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the timer identifier specified in <see cref="T:PX.Objects.EP.ClockInClockOut.EPClockInTimerData.timerDataID" />.
  /// </value>
  [PXDBInt]
  public int? ActiveClockInTimerID { get; set; }

  public abstract class activeClockInTimerID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    EPEmployeeClockIn.activeClockInTimerID>
  {
  }
}
