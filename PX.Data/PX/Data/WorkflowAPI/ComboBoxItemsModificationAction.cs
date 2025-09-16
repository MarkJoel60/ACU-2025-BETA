// Decompiled with JetBrains decompiler
// Type: PX.Data.WorkflowAPI.ComboBoxItemsModificationAction
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data.WorkflowAPI;

/// <summary>Possible modifications of combo box items.</summary>
public enum ComboBoxItemsModificationAction
{
  /// <summary>Adds a new combo box value or updates the display name of an existing combo box value.</summary>
  Set,
  /// <summary>Completely removes a value from the list of available combo box values.</summary>
  Remove,
  SetFromUI,
}
