// Decompiled with JetBrains decompiler
// Type: PX.Data.Update.ExchangeService.RetentionPolicyTagType
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
public class RetentionPolicyTagType
{
  private string displayNameField;
  private string retentionIdField;
  private int retentionPeriodField;
  private ElcFolderType typeField;
  private RetentionActionType retentionActionField;
  private string descriptionField;
  private bool isVisibleField;
  private bool optedIntoField;
  private bool isArchiveField;

  /// <remarks />
  public string DisplayName
  {
    get => this.displayNameField;
    set => this.displayNameField = value;
  }

  /// <remarks />
  public string RetentionId
  {
    get => this.retentionIdField;
    set => this.retentionIdField = value;
  }

  /// <remarks />
  public int RetentionPeriod
  {
    get => this.retentionPeriodField;
    set => this.retentionPeriodField = value;
  }

  /// <remarks />
  public ElcFolderType Type
  {
    get => this.typeField;
    set => this.typeField = value;
  }

  /// <remarks />
  public RetentionActionType RetentionAction
  {
    get => this.retentionActionField;
    set => this.retentionActionField = value;
  }

  /// <remarks />
  public string Description
  {
    get => this.descriptionField;
    set => this.descriptionField = value;
  }

  /// <remarks />
  public bool IsVisible
  {
    get => this.isVisibleField;
    set => this.isVisibleField = value;
  }

  /// <remarks />
  public bool OptedInto
  {
    get => this.optedIntoField;
    set => this.optedIntoField = value;
  }

  /// <remarks />
  public bool IsArchive
  {
    get => this.isArchiveField;
    set => this.isArchiveField = value;
  }
}
