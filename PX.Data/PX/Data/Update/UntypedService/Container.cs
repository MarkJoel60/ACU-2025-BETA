// Decompiled with JetBrains decompiler
// Type: PX.Data.Update.UntypedService.Container
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
namespace PX.Data.Update.UntypedService;

[GeneratedCode("System.Xml", "4.0.30319.233")]
[DebuggerStepThrough]
[DesignerCategory("code")]
[XmlType(Namespace = "http://www.acumatica.com/untyped/")]
[Serializable]
public class Container
{
  private Field[] fieldsField;
  private string nameField;
  private Command[] serviceCommandsField;

  public Field[] Fields
  {
    get => this.fieldsField;
    set => this.fieldsField = value;
  }

  public string Name
  {
    get => this.nameField;
    set => this.nameField = value;
  }

  public Command[] ServiceCommands
  {
    get => this.serviceCommandsField;
    set => this.serviceCommandsField = value;
  }
}
