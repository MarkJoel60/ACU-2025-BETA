// Decompiled with JetBrains decompiler
// Type: PX.Data.Update.ExchangeService.TargetFolderIdType
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
[XmlInclude(typeof (UserConfigurationNameType))]
[GeneratedCode("System.Xml", "4.0.30319.18408")]
[DebuggerStepThrough]
[DesignerCategory("code")]
[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
[Serializable]
public class TargetFolderIdType
{
  private BaseFolderIdType itemField;

  /// <remarks />
  [XmlElement("AddressListId", typeof (AddressListIdType))]
  [XmlElement("DistinguishedFolderId", typeof (DistinguishedFolderIdType))]
  [XmlElement("FolderId", typeof (FolderIdType))]
  public BaseFolderIdType Item
  {
    get => this.itemField;
    set => this.itemField = value;
  }
}
