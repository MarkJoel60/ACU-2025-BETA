// Decompiled with JetBrains decompiler
// Type: PX.Data.Update.ExchangeService.PathToExtendedFieldType
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
public class PathToExtendedFieldType : BasePathToElementType
{
  private DistinguishedPropertySetType distinguishedPropertySetIdField;
  private bool distinguishedPropertySetIdFieldSpecified;
  private string propertySetIdField;
  private string propertyTagField;
  private string propertyNameField;
  private int propertyIdField;
  private bool propertyIdFieldSpecified;
  private MapiPropertyTypeType propertyTypeField;

  /// <remarks />
  [XmlAttribute]
  public DistinguishedPropertySetType DistinguishedPropertySetId
  {
    get => this.distinguishedPropertySetIdField;
    set => this.distinguishedPropertySetIdField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool DistinguishedPropertySetIdSpecified
  {
    get => this.distinguishedPropertySetIdFieldSpecified;
    set => this.distinguishedPropertySetIdFieldSpecified = value;
  }

  /// <remarks />
  [XmlAttribute]
  public string PropertySetId
  {
    get => this.propertySetIdField;
    set => this.propertySetIdField = value;
  }

  /// <remarks />
  [XmlAttribute]
  public string PropertyTag
  {
    get => this.propertyTagField;
    set => this.propertyTagField = value;
  }

  /// <remarks />
  [XmlAttribute]
  public string PropertyName
  {
    get => this.propertyNameField;
    set => this.propertyNameField = value;
  }

  /// <remarks />
  [XmlAttribute]
  public int PropertyId
  {
    get => this.propertyIdField;
    set => this.propertyIdField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool PropertyIdSpecified
  {
    get => this.propertyIdFieldSpecified;
    set => this.propertyIdFieldSpecified = value;
  }

  /// <remarks />
  [XmlAttribute]
  public MapiPropertyTypeType PropertyType
  {
    get => this.propertyTypeField;
    set => this.propertyTypeField = value;
  }
}
