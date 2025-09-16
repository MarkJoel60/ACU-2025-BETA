// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.PMProjectEntryTaskValidator
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CS;
using System;

#nullable disable
namespace PX.Objects.PM;

public class PMProjectEntryTaskValidator : PMTaskValidator<ProjectEntry>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.costCodes>();

  protected virtual void _(Events.FieldVerifying<PMTask, PMTask.status> e)
  {
    if (e.Row == null || !((string) ((Events.FieldVerifyingBase<Events.FieldVerifying<PMTask, PMTask.status>, PMTask, object>) e).NewValue == "A"))
      return;
    this.VerifyCostCodeActive(e.Row);
  }

  public override Exception GetCostCodeValidationException(PMTask task)
  {
    return (Exception) new PXSetPropertyException("The {0} project task cannot be activated because it is used with the inactive cost code.", new object[1]
    {
      (object) task.TaskCD
    });
  }
}
