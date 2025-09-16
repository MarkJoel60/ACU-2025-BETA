// Decompiled with JetBrains decompiler
// Type: PX.Data.Update.ExchangeService.SyncFolderHierarchyResponseMessageType
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
public class SyncFolderHierarchyResponseMessageType : ResponseMessageType
{
  private string syncStateField;
  private bool includesLastFolderInRangeField;
  private bool includesLastFolderInRangeFieldSpecified;
  private SyncFolderHierarchyChangesType changesField;

  /// <remarks />
  public string SyncState
  {
    get => this.syncStateField;
    set => this.syncStateField = value;
  }

  /// <remarks />
  public bool IncludesLastFolderInRange
  {
    get => this.includesLastFolderInRangeField;
    set => this.includesLastFolderInRangeField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool IncludesLastFolderInRangeSpecified
  {
    get => this.includesLastFolderInRangeFieldSpecified;
    set => this.includesLastFolderInRangeFieldSpecified = value;
  }

  /// <remarks />
  public SyncFolderHierarchyChangesType Changes
  {
    get => this.changesField;
    set => this.changesField = value;
  }
}
