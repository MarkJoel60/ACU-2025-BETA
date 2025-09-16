// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.ProjectBalanceValidationProcessExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;

#nullable disable
namespace PX.Objects.PM;

public class ProjectBalanceValidationProcessExt : CommitmentTracking<ProjectBalanceValidationProcess>
{
  [PXOverride]
  public virtual void ProcessCommitments(PMProject project)
  {
    foreach (PXResult<PMCommitment> pxResult in ((PXSelectBase<PMCommitment>) this.Base.ExternalCommitments).Select(new object[1]
    {
      (object) project.ContractID
    }))
      this.RollUpCommitmentBalance(PXResult<PMCommitment>.op_Implicit(pxResult), 1);
  }
}
