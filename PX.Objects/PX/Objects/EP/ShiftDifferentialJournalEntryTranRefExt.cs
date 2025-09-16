// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.ShiftDifferentialJournalEntryTranRefExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CS;
using PX.Objects.GL;
using PX.Objects.PM;
using PX.Objects.PM.GraphExtensions;

#nullable disable
namespace PX.Objects.EP;

public class ShiftDifferentialJournalEntryTranRefExt : PXGraphExtension<JournalEntryTranRef>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.shiftDifferential>();

  [PXOverride]
  public virtual void AssignAdditionalFields(
    GLTran glTran,
    PMTran pmTran,
    ShiftDifferentialJournalEntryTranRefExt.AssignAdditionalFieldsDelegate baseMethod)
  {
    baseMethod(glTran, pmTran);
    ShiftDifferentialGLTranExt extension = PXCache<GLTran>.GetExtension<ShiftDifferentialGLTranExt>(glTran);
    PXCache<PMTran>.GetExtension<ShiftDifferentialPMTranExt>(pmTran).ShiftID = extension.ShiftID;
  }

  public delegate void AssignAdditionalFieldsDelegate(GLTran glTran, PMTran pmTran);
}
