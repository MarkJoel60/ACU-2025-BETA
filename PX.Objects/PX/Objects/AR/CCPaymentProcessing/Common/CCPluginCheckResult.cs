// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.CCPaymentProcessing.Common.CCPluginCheckResult
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

#nullable disable
namespace PX.Objects.AR.CCPaymentProcessing.Common;

/// <summary>Result of CC plug-in validation</summary>
public enum CCPluginCheckResult
{
  /// <summary>Plug-in type validation passed.</summary>
  Ok,
  /// <summary>Plug-in type is empty.</summary>
  Empty,
  /// <summary>Plug-in type is missing.</summary>
  Missing,
  /// <summary>Plug-in type is unsupported.</summary>
  Unsupported,
  /// <summary>Validation was not performed.</summary>
  NotPerformed,
}
