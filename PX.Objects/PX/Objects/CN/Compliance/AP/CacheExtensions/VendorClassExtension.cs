// Decompiled with JetBrains decompiler
// Type: PX.Objects.CN.Compliance.AP.CacheExtensions.VendorClassExtension
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Objects.AP;
using PX.Objects.CN.Compliance.CL.DAC;
using PX.Objects.CS;

#nullable enable
namespace PX.Objects.CN.Compliance.AP.CacheExtensions;

public sealed class VendorClassExtension : PXCacheExtension<
#nullable disable
VendorClass>
{
  [PXDBBool]
  [PXDefault(typeof (BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Current<LienWaiverSetup.shouldGenerateConditional>, Equal<True>>>>>.Or<BqlOperand<Current<LienWaiverSetup.shouldGenerateUnconditional>, IBqlBool>.IsEqual<True>>))]
  [PXUIField(DisplayName = "Generate Lien Waivers Based on Project Settings")]
  [PXUIVisible(typeof (Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Current<LienWaiverSetup.shouldGenerateConditional>, Equal<True>>>>>.Or<BqlOperand<Current<LienWaiverSetup.shouldGenerateUnconditional>, IBqlBool>.IsEqual<True>>>))]
  public bool? ShouldGenerateLienWaivers { get; set; }

  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.construction>();

  public abstract class shouldGenerateLienWaivers : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    VendorClassExtension.shouldGenerateLienWaivers>
  {
  }
}
