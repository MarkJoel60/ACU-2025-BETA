// Decompiled with JetBrains decompiler
// Type: PX.Data.Update.UntypedService.Command
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

[XmlInclude(typeof (Parameter))]
[XmlInclude(typeof (NewRow))]
[XmlInclude(typeof (RowNumber))]
[XmlInclude(typeof (DeleteRow))]
[XmlInclude(typeof (Answer))]
[XmlInclude(typeof (Field))]
[XmlInclude(typeof (PX.Data.Update.UntypedService.Value))]
[XmlInclude(typeof (Attachment))]
[XmlInclude(typeof (Action))]
[XmlInclude(typeof (Key))]
[XmlInclude(typeof (EveryValue))]
[GeneratedCode("System.Xml", "4.0.30319.233")]
[DebuggerStepThrough]
[DesignerCategory("code")]
[XmlType(Namespace = "http://www.acumatica.com/untyped/")]
[Serializable]
public class Command
{
  private string fieldNameField;
  private string objectNameField;
  private string valueField;
  private bool commitField;
  private bool ignoreErrorField;
  private Command linkedCommandField;
  private string nameField;
  private ElementDescriptor descriptorField;

  public Command()
  {
    this.commitField = false;
    this.ignoreErrorField = false;
  }

  public string FieldName
  {
    get => this.fieldNameField;
    set => this.fieldNameField = value;
  }

  public string ObjectName
  {
    get => this.objectNameField;
    set => this.objectNameField = value;
  }

  public string Value
  {
    get => this.valueField;
    set => this.valueField = value;
  }

  [DefaultValue(false)]
  public bool Commit
  {
    get => this.commitField;
    set => this.commitField = value;
  }

  [DefaultValue(false)]
  public bool IgnoreError
  {
    get => this.ignoreErrorField;
    set => this.ignoreErrorField = value;
  }

  public Command LinkedCommand
  {
    get => this.linkedCommandField;
    set => this.linkedCommandField = value;
  }

  public string Name
  {
    get => this.nameField;
    set => this.nameField = value;
  }

  public ElementDescriptor Descriptor
  {
    get => this.descriptorField;
    set => this.descriptorField = value;
  }
}
