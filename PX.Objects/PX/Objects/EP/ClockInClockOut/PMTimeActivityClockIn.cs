// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.ClockInClockOut.PMTimeActivityClockIn
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Objects.CR;

#nullable enable
namespace PX.Objects.EP.ClockInClockOut;

/// <summary>
/// Extends <see cref="T:PX.Objects.CR.PMTimeActivity" /> to add the clock-in and clock-out functionality.
/// </summary>
[PXInternalUseOnly]
public sealed class PMTimeActivityClockIn : PXCacheExtension<
#nullable disable
PMTimeActivity>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<PX.Objects.CS.FeaturesSet.clockInClockOut>();

  /// <summary>The time log associated with this time activity.</summary>
  /// <value>
  /// The value of this field corresponds to the time log in <see cref="T:PX.Objects.EP.ClockInClockOut.EPTimeLog.timeLogID" />.
  /// </value>
  [PXDBInt]
  public int? TimeLogID { get; set; }

  /// <summary>
  /// Read-only field that is equal to <c>true</c> in case the record
  /// was created based on <see cref="T:PX.Objects.EP.ClockInClockOut.EPTimeLog">entity</see>.
  /// </summary>
  [PXBool]
  [PXUIField(DisplayName = "Time Log", Enabled = false, Visible = false)]
  public bool? IsCreatedFromTimeLog
  {
    [PXDependsOnFields(new System.Type[] {typeof (PMTimeActivityClockIn.timeLogID)})] get
    {
      return new bool?(this.TimeLogID.HasValue);
    }
    set
    {
    }
  }

  public abstract class timeLogID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMTimeActivityClockIn.timeLogID>
  {
  }

  public abstract class isCreatedFromTimeLog : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    PMTimeActivityClockIn.isCreatedFromTimeLog>
  {
  }
}
