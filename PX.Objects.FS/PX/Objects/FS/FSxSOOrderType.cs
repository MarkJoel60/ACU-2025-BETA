// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.FSxSOOrderType
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CS;
using PX.Objects.SO;

#nullable enable
namespace PX.Objects.FS;

public sealed class FSxSOOrderType : PXCacheExtension<
#nullable disable
PX.Objects.SO.SOOrderType>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.serviceManagementModule>();

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Enable Field Services Integration")]
  [PXFormula(typeof (Switch<Case<Where<PX.Objects.SO.SOOrderType.behavior, Equal<SOBehavior.bL>>, False>, IsNull<Current2<FSxSOOrderType.enableFSIntegration>, False>>))]
  public bool? EnableFSIntegration { get; set; }

  public abstract class enableFSIntegration : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSxSOOrderType.enableFSIntegration>
  {
  }
}
