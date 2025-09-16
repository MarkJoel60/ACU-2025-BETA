// Decompiled with JetBrains decompiler
// Type: PX.Data.Update.ExchangeService.BodyType
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
public class BodyType
{
  private BodyTypeType bodyType1Field;
  private bool isTruncatedField;
  private bool isTruncatedFieldSpecified;
  private string valueField;

  /// <remarks />
  [XmlAttribute("BodyType")]
  public BodyTypeType BodyType1
  {
    get => this.bodyType1Field;
    set => this.bodyType1Field = value;
  }

  /// <remarks />
  [XmlAttribute]
  public bool IsTruncated
  {
    get => this.isTruncatedField;
    set => this.isTruncatedField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool IsTruncatedSpecified
  {
    get => this.isTruncatedFieldSpecified;
    set => this.isTruncatedFieldSpecified = value;
  }

  /// <remarks />
  [XmlText]
  public string Value
  {
    get => this.valueField;
    set => this.valueField = value;
  }
}
