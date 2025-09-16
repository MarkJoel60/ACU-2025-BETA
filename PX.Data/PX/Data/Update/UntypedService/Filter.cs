// Decompiled with JetBrains decompiler
// Type: PX.Data.Update.UntypedService.Filter
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
public class Filter
{
  private Field fieldField;
  private FilterCondition conditionField;
  private object valueField;
  private object value2Field;
  private int openBracketsField;
  private int closeBracketsField;
  private FilterOperator operatorField;

  public Field Field
  {
    get => this.fieldField;
    set => this.fieldField = value;
  }

  public FilterCondition Condition
  {
    get => this.conditionField;
    set => this.conditionField = value;
  }

  public object Value
  {
    get => this.valueField;
    set => this.valueField = value;
  }

  public object Value2
  {
    get => this.value2Field;
    set => this.value2Field = value;
  }

  public int OpenBrackets
  {
    get => this.openBracketsField;
    set => this.openBracketsField = value;
  }

  public int CloseBrackets
  {
    get => this.closeBracketsField;
    set => this.closeBracketsField = value;
  }

  public FilterOperator Operator
  {
    get => this.operatorField;
    set => this.operatorField = value;
  }
}
