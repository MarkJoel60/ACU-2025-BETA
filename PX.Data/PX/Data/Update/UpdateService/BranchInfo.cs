// Decompiled with JetBrains decompiler
// Type: PX.Data.Update.UpdateService.BranchInfo
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

#nullable disable
namespace PX.Data.Update.UpdateService;

/// <remarks />
[GeneratedCode("System.Xml", "2.0.50727.5420")]
[DebuggerStepThrough]
[DesignerCategory("code")]
[XmlType(Namespace = "http://acumatica.com/")]
[Serializable]
public class BranchInfo
{
  private string nameField;
  private string descriptionField;

  /// <remarks />
  public string Name
  {
    get => this.nameField;
    set => this.nameField = value;
  }

  /// <remarks />
  public string Description
  {
    get => this.descriptionField;
    set => this.descriptionField = value;
  }
}
