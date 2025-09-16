// Decompiled with JetBrains decompiler
// Type: PX.Data.Update.ExchangeService.FindPeopleResponseMessageType
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
[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
[Serializable]
public class FindPeopleResponseMessageType : ResponseMessageType
{
  private PersonaType[] peopleField;
  private int totalNumberOfPeopleInViewField;
  private bool totalNumberOfPeopleInViewFieldSpecified;
  private int firstMatchingRowIndexField;
  private bool firstMatchingRowIndexFieldSpecified;
  private int firstLoadedRowIndexField;
  private bool firstLoadedRowIndexFieldSpecified;

  /// <remarks />
  [XmlArrayItem("Persona", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
  public PersonaType[] People
  {
    get => this.peopleField;
    set => this.peopleField = value;
  }

  /// <remarks />
  public int TotalNumberOfPeopleInView
  {
    get => this.totalNumberOfPeopleInViewField;
    set => this.totalNumberOfPeopleInViewField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool TotalNumberOfPeopleInViewSpecified
  {
    get => this.totalNumberOfPeopleInViewFieldSpecified;
    set => this.totalNumberOfPeopleInViewFieldSpecified = value;
  }

  /// <remarks />
  public int FirstMatchingRowIndex
  {
    get => this.firstMatchingRowIndexField;
    set => this.firstMatchingRowIndexField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool FirstMatchingRowIndexSpecified
  {
    get => this.firstMatchingRowIndexFieldSpecified;
    set => this.firstMatchingRowIndexFieldSpecified = value;
  }

  /// <remarks />
  public int FirstLoadedRowIndex
  {
    get => this.firstLoadedRowIndexField;
    set => this.firstLoadedRowIndexField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool FirstLoadedRowIndexSpecified
  {
    get => this.firstLoadedRowIndexFieldSpecified;
    set => this.firstLoadedRowIndexFieldSpecified = value;
  }
}
