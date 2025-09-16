// Decompiled with JetBrains decompiler
// Type: PX.Data.Update.UpdateService.BuildInfo
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
public class BuildInfo
{
  private string nameField;
  private string qualityField;
  private bool restrictedField;
  private System.DateTime? dateField;
  private string descriptionField;
  private string notesField;

  /// <remarks />
  public string Name
  {
    get => this.nameField;
    set => this.nameField = value;
  }

  /// <remarks />
  public string Quality
  {
    get => this.qualityField;
    set => this.qualityField = value;
  }

  /// <remarks />
  public bool Restricted
  {
    get => this.restrictedField;
    set => this.restrictedField = value;
  }

  /// <remarks />
  [XmlElement(IsNullable = true)]
  public System.DateTime? Date
  {
    get => this.dateField;
    set => this.dateField = value;
  }

  /// <remarks />
  public string Description
  {
    get => this.descriptionField;
    set => this.descriptionField = value;
  }

  /// <remarks />
  public string Notes
  {
    get => this.notesField;
    set => this.notesField = value;
  }
}
