// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.APAdjustPMExtension
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.AP;
using PX.Objects.CS;

#nullable enable
namespace PX.Objects.PM;

/// <summary>
/// An extension that is used to provide data for the table on the Documents to Apply tab of the Checks and Payments (AP302000) form.
/// </summary>
public sealed class APAdjustPMExtension : PXCacheExtension<APAdjust>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.projectAccounting>();

  /// <summary>
  /// A project that is displayed in a line of the table on the Documents to Apply tab of the Checks and Payments (AP302000) form.
  /// </summary>
  [PXInt]
  [PXUIField(DisplayName = "Project", Enabled = false)]
  [PXSelector(typeof (PMProject.contractID), SubstituteKey = typeof (PMProject.contractCD))]
  public int? PaymentProjectID { get; set; }

  public abstract class paymentProjectID : 
    BqlType<IBqlInt, int>.Field<APAdjustPMExtension.paymentProjectID>
  {
  }
}
