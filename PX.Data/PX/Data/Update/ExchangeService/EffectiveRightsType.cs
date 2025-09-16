// Decompiled with JetBrains decompiler
// Type: PX.Data.Update.ExchangeService.EffectiveRightsType
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
public class EffectiveRightsType
{
  private bool createAssociatedField;
  private bool createContentsField;
  private bool createHierarchyField;
  private bool deleteField;
  private bool modifyField;
  private bool readField;
  private bool viewPrivateItemsField;
  private bool viewPrivateItemsFieldSpecified;

  /// <remarks />
  public bool CreateAssociated
  {
    get => this.createAssociatedField;
    set => this.createAssociatedField = value;
  }

  /// <remarks />
  public bool CreateContents
  {
    get => this.createContentsField;
    set => this.createContentsField = value;
  }

  /// <remarks />
  public bool CreateHierarchy
  {
    get => this.createHierarchyField;
    set => this.createHierarchyField = value;
  }

  /// <remarks />
  public bool Delete
  {
    get => this.deleteField;
    set => this.deleteField = value;
  }

  /// <remarks />
  public bool Modify
  {
    get => this.modifyField;
    set => this.modifyField = value;
  }

  /// <remarks />
  public bool Read
  {
    get => this.readField;
    set => this.readField = value;
  }

  /// <remarks />
  public bool ViewPrivateItems
  {
    get => this.viewPrivateItemsField;
    set => this.viewPrivateItemsField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool ViewPrivateItemsSpecified
  {
    get => this.viewPrivateItemsFieldSpecified;
    set => this.viewPrivateItemsFieldSpecified = value;
  }
}
