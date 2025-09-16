// Decompiled with JetBrains decompiler
// Type: PX.Data.Update.ExchangeService.RightsManagementLicenseDataType
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
public class RightsManagementLicenseDataType
{
  private int rightsManagedMessageDecryptionStatusField;
  private bool rightsManagedMessageDecryptionStatusFieldSpecified;
  private string rmsTemplateIdField;
  private string templateNameField;
  private string templateDescriptionField;
  private bool editAllowedField;
  private bool editAllowedFieldSpecified;
  private bool replyAllowedField;
  private bool replyAllowedFieldSpecified;
  private bool replyAllAllowedField;
  private bool replyAllAllowedFieldSpecified;
  private bool forwardAllowedField;
  private bool forwardAllowedFieldSpecified;
  private bool modifyRecipientsAllowedField;
  private bool modifyRecipientsAllowedFieldSpecified;
  private bool extractAllowedField;
  private bool extractAllowedFieldSpecified;
  private bool printAllowedField;
  private bool printAllowedFieldSpecified;
  private bool exportAllowedField;
  private bool exportAllowedFieldSpecified;
  private bool programmaticAccessAllowedField;
  private bool programmaticAccessAllowedFieldSpecified;
  private bool isOwnerField;
  private bool isOwnerFieldSpecified;
  private string contentOwnerField;
  private string contentExpiryDateField;

  /// <remarks />
  public int RightsManagedMessageDecryptionStatus
  {
    get => this.rightsManagedMessageDecryptionStatusField;
    set => this.rightsManagedMessageDecryptionStatusField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool RightsManagedMessageDecryptionStatusSpecified
  {
    get => this.rightsManagedMessageDecryptionStatusFieldSpecified;
    set => this.rightsManagedMessageDecryptionStatusFieldSpecified = value;
  }

  /// <remarks />
  public string RmsTemplateId
  {
    get => this.rmsTemplateIdField;
    set => this.rmsTemplateIdField = value;
  }

  /// <remarks />
  public string TemplateName
  {
    get => this.templateNameField;
    set => this.templateNameField = value;
  }

  /// <remarks />
  public string TemplateDescription
  {
    get => this.templateDescriptionField;
    set => this.templateDescriptionField = value;
  }

  /// <remarks />
  public bool EditAllowed
  {
    get => this.editAllowedField;
    set => this.editAllowedField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool EditAllowedSpecified
  {
    get => this.editAllowedFieldSpecified;
    set => this.editAllowedFieldSpecified = value;
  }

  /// <remarks />
  public bool ReplyAllowed
  {
    get => this.replyAllowedField;
    set => this.replyAllowedField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool ReplyAllowedSpecified
  {
    get => this.replyAllowedFieldSpecified;
    set => this.replyAllowedFieldSpecified = value;
  }

  /// <remarks />
  public bool ReplyAllAllowed
  {
    get => this.replyAllAllowedField;
    set => this.replyAllAllowedField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool ReplyAllAllowedSpecified
  {
    get => this.replyAllAllowedFieldSpecified;
    set => this.replyAllAllowedFieldSpecified = value;
  }

  /// <remarks />
  public bool ForwardAllowed
  {
    get => this.forwardAllowedField;
    set => this.forwardAllowedField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool ForwardAllowedSpecified
  {
    get => this.forwardAllowedFieldSpecified;
    set => this.forwardAllowedFieldSpecified = value;
  }

  /// <remarks />
  public bool ModifyRecipientsAllowed
  {
    get => this.modifyRecipientsAllowedField;
    set => this.modifyRecipientsAllowedField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool ModifyRecipientsAllowedSpecified
  {
    get => this.modifyRecipientsAllowedFieldSpecified;
    set => this.modifyRecipientsAllowedFieldSpecified = value;
  }

  /// <remarks />
  public bool ExtractAllowed
  {
    get => this.extractAllowedField;
    set => this.extractAllowedField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool ExtractAllowedSpecified
  {
    get => this.extractAllowedFieldSpecified;
    set => this.extractAllowedFieldSpecified = value;
  }

  /// <remarks />
  public bool PrintAllowed
  {
    get => this.printAllowedField;
    set => this.printAllowedField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool PrintAllowedSpecified
  {
    get => this.printAllowedFieldSpecified;
    set => this.printAllowedFieldSpecified = value;
  }

  /// <remarks />
  public bool ExportAllowed
  {
    get => this.exportAllowedField;
    set => this.exportAllowedField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool ExportAllowedSpecified
  {
    get => this.exportAllowedFieldSpecified;
    set => this.exportAllowedFieldSpecified = value;
  }

  /// <remarks />
  public bool ProgrammaticAccessAllowed
  {
    get => this.programmaticAccessAllowedField;
    set => this.programmaticAccessAllowedField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool ProgrammaticAccessAllowedSpecified
  {
    get => this.programmaticAccessAllowedFieldSpecified;
    set => this.programmaticAccessAllowedFieldSpecified = value;
  }

  /// <remarks />
  public bool IsOwner
  {
    get => this.isOwnerField;
    set => this.isOwnerField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool IsOwnerSpecified
  {
    get => this.isOwnerFieldSpecified;
    set => this.isOwnerFieldSpecified = value;
  }

  /// <remarks />
  public string ContentOwner
  {
    get => this.contentOwnerField;
    set => this.contentOwnerField = value;
  }

  /// <remarks />
  public string ContentExpiryDate
  {
    get => this.contentExpiryDateField;
    set => this.contentExpiryDateField = value;
  }
}
