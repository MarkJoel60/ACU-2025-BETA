// Decompiled with JetBrains decompiler
// Type: PX.Data.ICancelEventArgs
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data;

/// <summary>
/// <inheritdoc cref="T:System.ComponentModel.CancelEventArgs" />
/// </summary>
public interface ICancelEventArgs
{
  /// <summary>
  /// <inheritdoc cref="P:System.ComponentModel.CancelEventArgs.Cancel" />
  /// </summary>
  bool Cancel { get; set; }
}
