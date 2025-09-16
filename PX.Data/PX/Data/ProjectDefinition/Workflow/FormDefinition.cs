// Decompiled with JetBrains decompiler
// Type: PX.Data.ProjectDefinition.Workflow.FormDefinition
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.SM;

#nullable disable
namespace PX.Data.ProjectDefinition.Workflow;

[PXInternalUseOnly]
public class FormDefinition
{
  public AUWorkflowForm Form;
  public AUWorkflowFormField[] Fields;

  public string Key { get; set; }

  public string FormName { get; set; }

  public string Screen { get; set; }

  public string DacType { get; set; }
}
