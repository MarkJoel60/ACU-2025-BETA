// Decompiled with JetBrains decompiler
// Type: PX.Data.Update.ExchangeService.ItemResponseShapeType
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
public class ItemResponseShapeType
{
  private DefaultShapeNamesType baseShapeField;
  private bool includeMimeContentField;
  private bool includeMimeContentFieldSpecified;
  private BodyTypeResponseType bodyTypeField;
  private bool bodyTypeFieldSpecified;
  private BodyTypeResponseType uniqueBodyTypeField;
  private bool uniqueBodyTypeFieldSpecified;
  private BodyTypeResponseType normalizedBodyTypeField;
  private bool normalizedBodyTypeFieldSpecified;
  private bool filterHtmlContentField;
  private bool filterHtmlContentFieldSpecified;
  private bool convertHtmlCodePageToUTF8Field;
  private bool convertHtmlCodePageToUTF8FieldSpecified;
  private string inlineImageUrlTemplateField;
  private bool blockExternalImagesField;
  private bool blockExternalImagesFieldSpecified;
  private bool addBlankTargetToLinksField;
  private bool addBlankTargetToLinksFieldSpecified;
  private int maximumBodySizeField;
  private bool maximumBodySizeFieldSpecified;
  private BasePathToElementType[] additionalPropertiesField;

  /// <remarks />
  public DefaultShapeNamesType BaseShape
  {
    get => this.baseShapeField;
    set => this.baseShapeField = value;
  }

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
  public BodyTypeResponseType UniqueBodyType
  {
    get => this.uniqueBodyTypeField;
    set => this.uniqueBodyTypeField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool UniqueBodyTypeSpecified
  {
    get => this.uniqueBodyTypeFieldSpecified;
    set => this.uniqueBodyTypeFieldSpecified = value;
  }

  /// <remarks />
  public BodyTypeResponseType NormalizedBodyType
  {
    get => this.normalizedBodyTypeField;
    set => this.normalizedBodyTypeField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool NormalizedBodyTypeSpecified
  {
    get => this.normalizedBodyTypeFieldSpecified;
    set => this.normalizedBodyTypeFieldSpecified = value;
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
  public bool ConvertHtmlCodePageToUTF8
  {
    get => this.convertHtmlCodePageToUTF8Field;
    set => this.convertHtmlCodePageToUTF8Field = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool ConvertHtmlCodePageToUTF8Specified
  {
    get => this.convertHtmlCodePageToUTF8FieldSpecified;
    set => this.convertHtmlCodePageToUTF8FieldSpecified = value;
  }

  /// <remarks />
  public string InlineImageUrlTemplate
  {
    get => this.inlineImageUrlTemplateField;
    set => this.inlineImageUrlTemplateField = value;
  }

  /// <remarks />
  public bool BlockExternalImages
  {
    get => this.blockExternalImagesField;
    set => this.blockExternalImagesField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool BlockExternalImagesSpecified
  {
    get => this.blockExternalImagesFieldSpecified;
    set => this.blockExternalImagesFieldSpecified = value;
  }

  /// <remarks />
  public bool AddBlankTargetToLinks
  {
    get => this.addBlankTargetToLinksField;
    set => this.addBlankTargetToLinksField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool AddBlankTargetToLinksSpecified
  {
    get => this.addBlankTargetToLinksFieldSpecified;
    set => this.addBlankTargetToLinksFieldSpecified = value;
  }

  /// <remarks />
  public int MaximumBodySize
  {
    get => this.maximumBodySizeField;
    set => this.maximumBodySizeField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool MaximumBodySizeSpecified
  {
    get => this.maximumBodySizeFieldSpecified;
    set => this.maximumBodySizeFieldSpecified = value;
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
