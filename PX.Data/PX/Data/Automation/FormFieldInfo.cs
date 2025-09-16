// Decompiled with JetBrains decompiler
// Type: PX.Data.Automation.FormFieldInfo
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System.Collections.Generic;

#nullable disable
namespace PX.Data.Automation;

public class FormFieldInfo
{
  public bool? IsRequired { get; internal set; }

  public Dictionary<string, string> PossibleComboBoxValues { get; internal set; }

  public List<AssignmentInfo> Assignments { get; internal set; } = new List<AssignmentInfo>();

  public System.Type DacType { get; internal set; }

  public string DacField { get; internal set; }

  public string TextType { get; internal set; }
}
