// Decompiled with JetBrains decompiler
// Type: PX.Data.WorkflowAPI.ComboBoxItemsModification
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data.WorkflowAPI;

public class ComboBoxItemsModification
{
  public string ID { get; internal set; }

  public string Description { get; internal set; }

  public ComboBoxItemsModificationAction Action { get; internal set; }

  public override string ToString() => $"{this.Action}|{this.ID}|{this.Description}";
}
