// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.ShiftDifferentialPMTranExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.CS;
using PX.Objects.PM;

#nullable enable
namespace PX.Objects.EP;

public sealed class ShiftDifferentialPMTranExt : PXCacheExtension<
#nullable disable
PMTran>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.shiftDifferential>();

  [PXDBInt]
  [PXUIField(DisplayName = "Shift Code")]
  [PXSelector(typeof (SearchFor<EPShiftCode.shiftID>.Where<BqlOperand<EPShiftCode.isManufacturingShift, IBqlBool>.IsEqual<False>>), SubstituteKey = typeof (EPShiftCode.shiftCD), DescriptionField = typeof (EPShiftCode.description))]
  public int? ShiftID { get; set; }

  public abstract class shiftID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ShiftDifferentialPMTranExt.shiftID>
  {
  }
}
