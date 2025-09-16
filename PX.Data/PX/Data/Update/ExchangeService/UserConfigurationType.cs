// Decompiled with JetBrains decompiler
// Type: PX.Data.Update.ExchangeService.UserConfigurationType
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
public class UserConfigurationType
{
  private UserConfigurationNameType userConfigurationNameField;
  private ItemIdType itemIdField;
  private UserConfigurationDictionaryEntryType[] dictionaryField;
  private byte[] xmlDataField;
  private byte[] binaryDataField;

  /// <remarks />
  public UserConfigurationNameType UserConfigurationName
  {
    get => this.userConfigurationNameField;
    set => this.userConfigurationNameField = value;
  }

  /// <remarks />
  public ItemIdType ItemId
  {
    get => this.itemIdField;
    set => this.itemIdField = value;
  }

  /// <remarks />
  [XmlArrayItem("DictionaryEntry", IsNullable = false)]
  public UserConfigurationDictionaryEntryType[] Dictionary
  {
    get => this.dictionaryField;
    set => this.dictionaryField = value;
  }

  /// <remarks />
  [XmlElement(DataType = "base64Binary")]
  public byte[] XmlData
  {
    get => this.xmlDataField;
    set => this.xmlDataField = value;
  }

  /// <remarks />
  [XmlElement(DataType = "base64Binary")]
  public byte[] BinaryData
  {
    get => this.binaryDataField;
    set => this.binaryDataField = value;
  }
}
