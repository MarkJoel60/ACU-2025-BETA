// Decompiled with JetBrains decompiler
// Type: PX.Data.PXTranStatus
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data;

/// <summary>Describes the current status of a transaction
/// scope.</summary>
public enum PXTranStatus
{
  /// <summary>The status of the transaction is unknown, because some
  /// participants still have to be polled.</summary>
  Open,
  /// <summary>The changes associated with the transaction scope have been
  /// successfully committed to the database.</summary>
  Completed,
  /// <summary>The changes within the transaction scope have been dropped
  /// because of an error.</summary>
  Aborted,
}
