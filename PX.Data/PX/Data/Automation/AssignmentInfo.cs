// Decompiled with JetBrains decompiler
// Type: PX.Data.Automation.AssignmentInfo
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data.Automation;

public class AssignmentInfo
{
  public string FieldName { get; internal set; }

  public bool InTransition { get; internal set; }

  public string Expression { get; internal set; }
}
