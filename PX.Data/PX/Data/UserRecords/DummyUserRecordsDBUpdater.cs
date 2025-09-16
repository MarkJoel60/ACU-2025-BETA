// Decompiled with JetBrains decompiler
// Type: PX.Data.UserRecords.DummyUserRecordsDBUpdater
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System.Collections.Generic;

#nullable disable
namespace PX.Data.UserRecords;

/// <summary>
/// A dummy for user records database updater class. Used in unit tests as a substitute for a real user records updater <see cref="T:PX.Data.UserRecords.UserRecordsDBUpdater" />.
/// </summary>
internal class DummyUserRecordsDBUpdater : IUserRecordsDBUpdater
{
  public void UpdateUserRecords(
    IReadOnlyCollection<ModifiedDacEntryForUserRecordsUpdate> modifiedDacEntries)
  {
  }
}
