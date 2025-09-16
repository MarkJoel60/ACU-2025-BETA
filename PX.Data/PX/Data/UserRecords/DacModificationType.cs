// Decompiled with JetBrains decompiler
// Type: PX.Data.UserRecords.DacModificationType
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data.UserRecords;

/// <summary>
/// Values that represent the possible types of DAC modification. This type determines the type of synchronization operation performed on the user records
/// corresponding to the modified DAC when DB transaction commpletes.
/// </summary>
internal enum DacModificationType : byte
{
  /// <summary>
  /// The DAC was deleted therefore all user records corresponding to it must be also removed from DB.
  /// </summary>
  Delete,
  /// <summary>
  /// The DAC was updated therefore the cached content of all user records corresponding to it must be updated.
  /// </summary>
  Update,
}
