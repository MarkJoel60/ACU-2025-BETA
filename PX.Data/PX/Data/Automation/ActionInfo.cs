// Decompiled with JetBrains decompiler
// Type: PX.Data.Automation.ActionInfo
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System.Collections.Generic;

#nullable disable
namespace PX.Data.Automation;

public class ActionInfo
{
  public string ActionName { get; internal set; }

  public string FormName { get; internal set; }

  public string FormCaption { get; internal set; }

  public Dictionary<string, FormFieldInfo> Fields { get; } = new Dictionary<string, FormFieldInfo>();
}
