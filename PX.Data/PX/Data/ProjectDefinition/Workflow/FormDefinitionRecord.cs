// Decompiled with JetBrains decompiler
// Type: PX.Data.ProjectDefinition.Workflow.FormDefinitionRecord
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data.ProjectDefinition.Workflow;

[PXHidden]
public class FormDefinitionRecord : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXString(IsKey = true)]
  public string Key { get; set; }

  [PXString]
  public string FormName { get; set; }

  [PXString]
  public string Screen { get; set; }

  [PXString]
  public string ActionName { get; set; }

  [PXBool]
  public bool? UseMulti { get; set; }

  [PXBool]
  public bool? GetFromSession { get; set; }

  [PXString]
  public string FormDacName { get; set; }

  [PXBool]
  public bool? Initialized { get; set; }
}
