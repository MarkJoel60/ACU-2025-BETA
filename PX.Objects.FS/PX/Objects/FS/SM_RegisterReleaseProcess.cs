// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.SM_RegisterReleaseProcess
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Objects.CS;
using PX.Objects.PM;

#nullable disable
namespace PX.Objects.FS;

public class SM_RegisterReleaseProcess : PXGraphExtension<RegisterReleaseProcess>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.serviceManagementModule>();

  [PXOverride]
  public virtual PMRegister OnBeforeRelease(
    PMRegister doc,
    SM_RegisterReleaseProcess.OnBeforeReleaseDelegate del)
  {
    this.ValidatePostBatchStatus((PXDBOperation) 1, "PM", doc.Module, doc.RefNbr);
    return del != null ? del(doc) : doc;
  }

  public virtual void ValidatePostBatchStatus(
    PXDBOperation dbOperation,
    string postTo,
    string createdDocType,
    string createdRefNbr)
  {
    DocGenerationHelper.ValidatePostBatchStatus<PMRegister>((PXGraph) this.Base, dbOperation, postTo, createdDocType, createdRefNbr);
  }

  public delegate PMRegister OnBeforeReleaseDelegate(PMRegister doc);
}
