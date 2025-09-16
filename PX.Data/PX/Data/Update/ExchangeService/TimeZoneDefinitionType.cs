// Decompiled with JetBrains decompiler
// Type: PX.Data.Update.ExchangeService.TimeZoneDefinitionType
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
namespace PX.Data.Update.ExchangeService;

/// <remarks />
[GeneratedCode("System.Xml", "4.0.30319.18408")]
[DebuggerStepThrough]
[DesignerCategory("code")]
[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
[Serializable]
public class TimeZoneDefinitionType
{
  private PeriodType[] periodsField;
  private ArrayOfTransitionsType[] transitionsGroupsField;
  private ArrayOfTransitionsType transitionsField;
  private string idField;
  private string nameField;

  /// <remarks />
  [XmlArrayItem("Period", IsNullable = false)]
  public PeriodType[] Periods
  {
    get => this.periodsField;
    set => this.periodsField = value;
  }

  /// <remarks />
  [XmlArrayItem("TransitionsGroup", IsNullable = false)]
  public ArrayOfTransitionsType[] TransitionsGroups
  {
    get => this.transitionsGroupsField;
    set => this.transitionsGroupsField = value;
  }

  /// <remarks />
  public ArrayOfTransitionsType Transitions
  {
    get => this.transitionsField;
    set => this.transitionsField = value;
  }

  /// <remarks />
  [XmlAttribute]
  public string Id
  {
    get => this.idField;
    set => this.idField = value;
  }

  /// <remarks />
  [XmlAttribute]
  public string Name
  {
    get => this.nameField;
    set => this.nameField = value;
  }
}
