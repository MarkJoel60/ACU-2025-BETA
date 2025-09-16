// Decompiled with JetBrains decompiler
// Type: PX.Data.Update.ExchangeService.MailboxCultureType
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Web.Services.Protocols;
using System.Xml;
using System.Xml.Serialization;

#nullable disable
namespace PX.Data.Update.ExchangeService;

/// <remarks />
[GeneratedCode("System.Xml", "4.0.30319.18408")]
[DebuggerStepThrough]
[DesignerCategory("code")]
[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
[XmlRoot("MailboxCulture", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
[Serializable]
public class MailboxCultureType : SoapHeader
{
  private XmlAttribute[] anyAttrField;
  private string valueField;

  /// <remarks />
  [XmlAnyAttribute]
  public XmlAttribute[] AnyAttr
  {
    get => this.anyAttrField;
    set => this.anyAttrField = value;
  }

  /// <remarks />
  [XmlText(DataType = "language")]
  public string Value
  {
    get => this.valueField;
    set => this.valueField = value;
  }
}
