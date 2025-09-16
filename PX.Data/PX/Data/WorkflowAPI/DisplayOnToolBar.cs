// Decompiled with JetBrains decompiler
// Type: PX.Data.WorkflowAPI.DisplayOnToolBar
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data.WorkflowAPI;

/// <summary>Specifies how an action can be displayed on the form toolbar.</summary>
public enum DisplayOnToolBar
{
  /// <summary>Never display an action on the form toolbar.</summary>
  Hidden,
  /// <summary>Display an action on the form toolbar only if the action is enabled.</summary>
  IfEnabled,
  /// <summary>Always display an action on the form toolbar.</summary>
  Always,
}
