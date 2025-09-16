// Decompiled with JetBrains decompiler
// Type: PX.Data.Update.UntypedService.ElementDescriptor
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
public class ElementDescriptor
{
  private string displayNameField;
  private bool isDisabledField;
  private bool isRequiredField;
  private ElementTypes elementTypeField;
  private int lengthLimitField;
  private string inputMaskField;
  private string displayRulesField;
  private string[] allowedValuesField;

  public ElementDescriptor()
  {
    this.isDisabledField = false;
    this.isRequiredField = false;
    this.elementTypeField = ElementTypes.String;
    this.lengthLimitField = 0;
  }

  public string DisplayName
  {
    get => this.displayNameField;
    set => this.displayNameField = value;
  }

  [DefaultValue(false)]
  public bool IsDisabled
  {
    get => this.isDisabledField;
    set => this.isDisabledField = value;
  }

  [DefaultValue(false)]
  public bool IsRequired
  {
    get => this.isRequiredField;
    set => this.isRequiredField = value;
  }

  [DefaultValue(ElementTypes.String)]
  public ElementTypes ElementType
  {
    get => this.elementTypeField;
    set => this.elementTypeField = value;
  }

  [DefaultValue(0)]
  public int LengthLimit
  {
    get => this.lengthLimitField;
    set => this.lengthLimitField = value;
  }

  public string InputMask
  {
    get => this.inputMaskField;
    set => this.inputMaskField = value;
  }

  public string DisplayRules
  {
    get => this.displayRulesField;
    set => this.displayRulesField = value;
  }

  public string[] AllowedValues
  {
    get => this.allowedValuesField;
    set => this.allowedValuesField = value;
  }
}
