// Decompiled with JetBrains decompiler
// Type: PX.Data.Update.ExchangeService.GroupMailboxConfigurationActionType
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

#nullable disable
namespace PX.Data.Update.ExchangeService;

/// <remarks />
[Flags]
[GeneratedCode("System.Xml", "4.0.30319.18408")]
[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
[Serializable]
public enum GroupMailboxConfigurationActionType
{
  /// <remarks />
  SetRegionalSettings = 1,
  /// <remarks />
  CreateDefaultFolders = 2,
  /// <remarks />
  SetInitialFolderPermissions = 4,
  /// <remarks />
  SetAllFolderPermissions = 8,
  /// <remarks />
  ConfigureCalendar = 16, // 0x00000010
  /// <remarks />
  SendWelcomeMessage = 32, // 0x00000020
}
