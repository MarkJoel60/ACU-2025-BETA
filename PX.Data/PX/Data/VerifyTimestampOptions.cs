// Decompiled with JetBrains decompiler
// Type: PX.Data.VerifyTimestampOptions
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data;

/// <summary>
/// Possible options of verifying timestamps for <see cref="T:PX.Data.PXDBTimestampAttribute" />.
/// </summary>
/// <remarks>
/// In cases where you have DACs that are expected to be used in concurrent operations of multiple users and processes,
/// we recommend that you use the <see cref="F:PX.Data.VerifyTimestampOptions.BothFromGraphAndRecord" /> option.
/// This allows for a safe update in the database without any data inconsistency issues, when updating a single document from multiple graphs.
/// </remarks>
public enum VerifyTimestampOptions
{
  /// <summary>
  /// The default approach: the graph timestamp is verified.
  /// </summary>
  /// <remarks>
  /// This option verifies the graph timestamp when updating a record. This is the default behavior of the <see cref="!:VerifyTimestamp" /> property.
  /// </remarks>
  FromGraph,
  /// <summary>The record timestamp is verified.</summary>
  /// <remarks>
  /// This option verifies the record timestamp when updating a record.
  /// </remarks>
  FromRecord,
  /// <summary>
  /// The combination of <see cref="F:PX.Data.VerifyTimestampOptions.FromGraph" /> and <see cref="F:PX.Data.VerifyTimestampOptions.FromRecord" />.
  /// </summary>
  /// <remarks>
  /// This option verifies both the graph and record timestamps when updating a record.
  ///  However, the graph timestamp is only verified if this graph defines logic related to the user interface.
  /// On the other hand, if the graph is created for processing, then the system will only verify the record timestamp.
  /// </remarks>
  BothFromGraphAndRecord,
}
