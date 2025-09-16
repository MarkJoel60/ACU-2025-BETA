// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.ProjectCDRestrictorAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.PM;

/// <summary>
/// Attribute that checks ContractCD for equality to the non-project code
/// </summary>
public class ProjectCDRestrictorAttribute : PXEventSubscriberAttribute, IPXFieldVerifyingSubscriber
{
  public static void Verify(PXCache sender, PXFieldVerifyingEventArgs e)
  {
    if (sender.GetStatus(e.Row) == null && sender.Graph.IsImport || string.IsNullOrWhiteSpace(e.NewValue is string newValue ? newValue.Trim() : (string) null))
      return;
    PMProject pmProject = PXResultset<PMProject>.op_Implicit(PXSelectBase<PMProject, PXSelect<PMProject, Where<PMProject.contractID, Equal<Required<PMProject.contractID>>>>.Config>.SelectSingleBound(sender.Graph, (object[]) null, new object[1]
    {
      (object) ProjectDefaultAttribute.NonProject()
    }));
    if (newValue.Trim().Equals(pmProject?.ContractCD.Trim(), StringComparison.OrdinalIgnoreCase))
      throw new PXSetPropertyException("The {0} identifier is specified as the non-project code on the Projects Preferences (PM101000) form. Specify another identifier.".ToString(), new object[1]
      {
        (object) newValue.Trim()
      })
      {
        ErrorValue = (object) newValue.Trim()
      };
  }

  public virtual void FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
  {
    ProjectCDRestrictorAttribute.Verify(sender, e);
  }
}
