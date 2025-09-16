// Decompiled with JetBrains decompiler
// Type: PX.Data.Update.ExchangeService.AddNewTelUriContactToGroupType
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
public class AddNewTelUriContactToGroupType : BaseRequestType
{
  private string telUriAddressField;
  private string imContactSipUriAddressField;
  private string imTelephoneNumberField;
  private ItemIdType groupIdField;

  /// <remarks />
  public string TelUriAddress
  {
    get => this.telUriAddressField;
    set => this.telUriAddressField = value;
  }

  /// <remarks />
  public string ImContactSipUriAddress
  {
    get => this.imContactSipUriAddressField;
    set => this.imContactSipUriAddressField = value;
  }

  /// <remarks />
  public string ImTelephoneNumber
  {
    get => this.imTelephoneNumberField;
    set => this.imTelephoneNumberField = value;
  }

  /// <remarks />
  public ItemIdType GroupId
  {
    get => this.groupIdField;
    set => this.groupIdField = value;
  }
}
