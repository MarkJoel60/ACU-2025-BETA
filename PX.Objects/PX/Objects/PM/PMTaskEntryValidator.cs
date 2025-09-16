// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.PMTaskEntryValidator
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CS;
using System;
using System.Collections;

#nullable disable
namespace PX.Objects.PM;

public class PMTaskEntryValidator : PMTaskValidator<ProjectTaskEntry>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.costCodes>();

  [PXOverride]
  public virtual IEnumerable Activate(PXAdapter adapter, Func<PXAdapter, IEnumerable> baseMethod)
  {
    PMTask current = ((PXSelectBase<PMTask>) this.Base.Task).Current;
    if (current != null)
      this.VerifyCostCodeActive(current);
    return baseMethod(adapter);
  }

  public override Exception GetCostCodeValidationException(PMTask task)
  {
    PMProject pmProject = PXResultset<PMProject>.op_Implicit(((PXSelectBase<PMProject>) this.Base.Project).Select(Array.Empty<object>()));
    return (Exception) new PXException("The {0} project task of the {1} project cannot be activated because it is used with the inactive cost code.", new object[2]
    {
      (object) task.TaskCD,
      (object) (pmProject?.ContractCD ?? string.Empty)
    });
  }
}
