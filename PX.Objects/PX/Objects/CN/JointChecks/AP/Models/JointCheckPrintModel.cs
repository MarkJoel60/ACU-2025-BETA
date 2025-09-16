// Decompiled with JetBrains decompiler
// Type: PX.Objects.CN.JointChecks.AP.Models.JointCheckPrintModel
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using System.Collections.Generic;

#nullable disable
namespace PX.Objects.CN.JointChecks.AP.Models;

public class JointCheckPrintModel
{
  private JointCheckPrintModel()
  {
  }

  public List<int?> InternalJointPayeeIds { get; set; }

  public List<string> ExternalJointPayeeNames { get; set; }

  public string JointPayeeNames { get; set; }

  public bool IsMultilinePrintMode { get; set; }

  public static JointCheckPrintModel Create(bool isMultiline)
  {
    return new JointCheckPrintModel()
    {
      InternalJointPayeeIds = new List<int?>(),
      ExternalJointPayeeNames = new List<string>(),
      JointPayeeNames = string.Empty,
      IsMultilinePrintMode = isMultiline
    };
  }
}
