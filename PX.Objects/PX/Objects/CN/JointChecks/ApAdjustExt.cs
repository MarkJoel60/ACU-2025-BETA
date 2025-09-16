// Decompiled with JetBrains decompiler
// Type: PX.Objects.CN.JointChecks.ApAdjustExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.AP;
using PX.Objects.CM;
using PX.Objects.CS;
using System;

#nullable enable
namespace PX.Objects.CN.JointChecks;

public sealed class ApAdjustExt : PXCacheExtension<
#nullable disable
APAdjust>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.construction>();

  [PXString(30)]
  [PXUIField(DisplayName = "Joint Payee Name")]
  public string JointPayeeExternalName { get; set; }

  [PXCurrency(typeof (APRegister.curyInfoID), typeof (ApAdjustExt.curyJointAmountOwed))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Joint Amount Owed", Enabled = false)]
  public Decimal? CuryJointAmountOwed { get; set; }

  [PXBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public Decimal? JointAmountOwed { get; set; }

  [PXCurrency(typeof (APRegister.curyInfoID), typeof (ApAdjustExt.curyJointBalance))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Joint Balance", Enabled = true)]
  public Decimal? CuryJointBalance { get; set; }

  [PXBaseCury]
  [PXUIField(DisplayName = "Joint Balance", Enabled = true)]
  public Decimal? JointBalance { get; set; }

  /// <summary>
  /// An indicator of whether the <see cref="P:PX.Objects.AP.APAdjust.CuryAdjgAmt" /> value has been calculated previously by using the joint amount calculation method.
  /// Once the value has been set, it should not be recalculated.
  /// </summary>
  [PXBool]
  [PXUnboundDefault(false)]
  public bool? IsAmountPaidCalculated { get; set; }

  public abstract class jointPayeeExternalName : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ApAdjustExt.jointPayeeExternalName>
  {
  }

  public abstract class curyJointAmountOwed : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ApAdjustExt.curyJointAmountOwed>
  {
  }

  public abstract class jointAmountOwed : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ApAdjustExt.jointAmountOwed>
  {
  }

  public abstract class curyJointBalance : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ApAdjustExt.curyJointBalance>
  {
  }

  public abstract class jointBalance : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ApAdjustExt.jointBalance>
  {
  }

  public abstract class isAmountPaidCalculated : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    ApAdjustExt.isAmountPaidCalculated>
  {
  }
}
