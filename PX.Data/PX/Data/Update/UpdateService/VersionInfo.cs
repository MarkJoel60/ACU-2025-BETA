// Decompiled with JetBrains decompiler
// Type: PX.Data.Update.UpdateService.VersionInfo
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
public class VersionInfo
{
  private ApplicationType typeField;
  private int majorField;
  private int minorField;
  private int buildField;

  /// <remarks />
  public ApplicationType Type
  {
    get => this.typeField;
    set => this.typeField = value;
  }

  /// <remarks />
  public int Major
  {
    get => this.majorField;
    set => this.majorField = value;
  }

  /// <remarks />
  public int Minor
  {
    get => this.minorField;
    set => this.minorField = value;
  }

  /// <remarks />
  public int Build
  {
    get => this.buildField;
    set => this.buildField = value;
  }
}
