// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.ClockInClockOut.EPSetupClockIn
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
/// Extends <see cref="T:PX.Objects.EP.EPSetup" /> to add the clock-in and clock-out functionality.
/// </summary>
[PXInternalUseOnly]
public sealed class EPSetupClockIn : PXCacheExtension<
#nullable disable
EPSetup>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<PX.Objects.CS.FeaturesSet.clockInClockOut>();

  /// <summary>
  /// The default time log type for the clock-in and clock-out functionality.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the time log type in <see cref="T:PX.Objects.EP.ClockInClockOut.EPTimeLogType.timeLogTypeID" />.
  /// </value>
  [PXDBString(15, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC")]
  [PXUIField(DisplayName = "Default Time Log Type")]
  [PXSelector(typeof (EPTimeLogType.timeLogTypeID), DescriptionField = typeof (EPTimeLogType.description))]
  public string DefTimeLogTypeID { get; set; }

  public abstract class defTimeLogTypeID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    EPSetupClockIn.defTimeLogTypeID>
  {
  }
}
