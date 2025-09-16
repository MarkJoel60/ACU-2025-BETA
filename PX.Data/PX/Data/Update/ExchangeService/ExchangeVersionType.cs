// Decompiled with JetBrains decompiler
// Type: PX.Data.Update.ExchangeService.ExchangeVersionType
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
[GeneratedCode("System.Xml", "4.0.30319.18408")]
[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
[Serializable]
public enum ExchangeVersionType
{
  /// <remarks />
  Exchange2007,
  /// <remarks />
  Exchange2007_SP1,
  /// <remarks />
  Exchange2010,
  /// <remarks />
  Exchange2010_SP1,
  /// <remarks />
  Exchange2010_SP2,
  /// <remarks />
  Exchange2013,
  /// <remarks />
  Exchange2013_SP1,
}
