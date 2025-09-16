// Decompiled with JetBrains decompiler
// Type: PX.Data.ProjectDefinition.Attributes.PXWorkflowFormBehaviorAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.WorkflowAPI;

#nullable disable
namespace PX.Data.ProjectDefinition.Attributes;

public class PXWorkflowFormBehaviorAttribute : PXEventSubscriberAttribute
{
  public ComboBoxValuesSource ComboBoxValuesSource { get; set; }

  public DefaultValueSource DefaultValueSource { get; set; }

  public System.Type ComboboxAndDefaultSourceField { get; set; }
}
