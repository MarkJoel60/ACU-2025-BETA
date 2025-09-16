// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.ShiftDifferentialGLTranExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CS;
using PX.Objects.GL;

#nullable enable
namespace PX.Objects.EP;

public sealed class ShiftDifferentialGLTranExt : PXCacheExtension<
#nullable disable
GLTran>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.shiftDifferential>();

  [PXDBInt]
  public int? ShiftID { get; set; }

  public abstract class shiftID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ShiftDifferentialGLTranExt.shiftID>
  {
  }
}
