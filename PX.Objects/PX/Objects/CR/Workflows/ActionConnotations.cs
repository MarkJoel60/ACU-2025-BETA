// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.Workflows.ActionConnotations
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data.WorkflowAPI;

#nullable disable
namespace PX.Objects.CR.Workflows;

internal static class ActionConnotations
{
  public static BoundedTo<CRCaseMaint, CRCase>.ActionState.IAllowOptionalConfig WithSuccessConnotation(
    this BoundedTo<CRCaseMaint, CRCase>.ActionState.IAllowOptionalConfig actionConfig,
    bool applyConnotation)
  {
    return !applyConnotation ? actionConfig : actionConfig.WithConnotation((ActionConnotation) 3);
  }

  public static BoundedTo<LeadMaint, CRLead>.ActionState.IAllowOptionalConfig WithSuccessConnotation(
    this BoundedTo<LeadMaint, CRLead>.ActionState.IAllowOptionalConfig actionConfig,
    bool applyConnotation)
  {
    return !applyConnotation ? actionConfig : actionConfig.WithConnotation((ActionConnotation) 3);
  }
}
