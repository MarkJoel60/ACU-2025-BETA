// Decompiled with JetBrains decompiler
// Type: PX.Data.Automation.TransitionInfo
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;

#nullable disable
namespace PX.Data.Automation;

public class TransitionInfo : ActionInfo
{
  public string Name { get; internal set; }

  public string FromState { get; internal set; }

  public string TargetState { get; internal set; }

  public Guid? Condition { get; internal set; }
}
