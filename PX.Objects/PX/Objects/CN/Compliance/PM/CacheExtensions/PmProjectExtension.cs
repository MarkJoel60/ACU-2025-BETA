// Decompiled with JetBrains decompiler
// Type: PX.Objects.CN.Compliance.PM.CacheExtensions.PmProjectExtension
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CN.Compliance.CL.DAC;
using PX.Objects.CN.Compliance.CL.Descriptor.Attributes.LienWaiver;
using PX.Objects.CS;
using PX.Objects.PM;

#nullable enable
namespace PX.Objects.CN.Compliance.PM.CacheExtensions;

public sealed class PmProjectExtension : PXCacheExtension<
#nullable disable
PMProject>
{
  [PXDBString]
  [PXDefault(typeof (LienWaiverSetup.throughDateSourceConditional))]
  [PXUIEnabled(typeof (Where<BqlOperand<Current<LienWaiverSetup.shouldGenerateConditional>, IBqlBool>.IsEqual<True>>))]
  [LienWaiverThroughDateSource.List]
  [PXUIField(DisplayName = "Through Date")]
  public string ThroughDateSourceConditional { get; set; }

  [PXDBString]
  [PXDefault(typeof (LienWaiverSetup.throughDateSourceUnconditional))]
  [PXUIEnabled(typeof (Where<BqlOperand<Current<LienWaiverSetup.shouldGenerateUnconditional>, IBqlBool>.IsEqual<True>>))]
  [LienWaiverThroughDateSource.List]
  [PXUIField(DisplayName = "Through Date")]
  public string ThroughDateSourceUnconditional { get; set; }

  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.construction>();

  public abstract class throughDateSourceConditional : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PmProjectExtension.throughDateSourceConditional>
  {
  }

  public abstract class throughDateSourceUnconditional : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PmProjectExtension.throughDateSourceUnconditional>
  {
  }
}
