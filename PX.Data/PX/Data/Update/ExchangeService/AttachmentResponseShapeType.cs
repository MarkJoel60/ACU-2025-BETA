// Decompiled with JetBrains decompiler
// Type: PX.Data.Update.ExchangeService.AttachmentResponseShapeType
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
public class AttachmentResponseShapeType
{
  private bool includeMimeContentField;
  private bool includeMimeContentFieldSpecified;
  private BodyTypeResponseType bodyTypeField;
  private bool bodyTypeFieldSpecified;
  private bool filterHtmlContentField;
  private bool filterHtmlContentFieldSpecified;
  private BasePathToElementType[] additionalPropertiesField;

  /// <remarks />
  public bool IncludeMimeContent
  {
    get => this.includeMimeContentField;
    set => this.includeMimeContentField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool IncludeMimeContentSpecified
  {
    get => this.includeMimeContentFieldSpecified;
    set => this.includeMimeContentFieldSpecified = value;
  }

  /// <remarks />
  public BodyTypeResponseType BodyType
  {
    get => this.bodyTypeField;
    set => this.bodyTypeField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool BodyTypeSpecified
  {
    get => this.bodyTypeFieldSpecified;
    set => this.bodyTypeFieldSpecified = value;
  }

  /// <remarks />
  public bool FilterHtmlContent
  {
    get => this.filterHtmlContentField;
    set => this.filterHtmlContentField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool FilterHtmlContentSpecified
  {
    get => this.filterHtmlContentFieldSpecified;
    set => this.filterHtmlContentFieldSpecified = value;
  }

  /// <remarks />
  [XmlArrayItem("ExtendedFieldURI", typeof (PathToExtendedFieldType), IsNullable = false)]
  [XmlArrayItem("FieldURI", typeof (PathToUnindexedFieldType), IsNullable = false)]
  [XmlArrayItem("IndexedFieldURI", typeof (PathToIndexedFieldType), IsNullable = false)]
  [XmlArrayItem("Path", IsNullable = false)]
  public BasePathToElementType[] AdditionalProperties
  {
    get => this.additionalPropertiesField;
    set => this.additionalPropertiesField = value;
  }
}
