// Decompiled with JetBrains decompiler
// Type: PX.Data.UserRecords.IUserRecordsDBUpdater
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System.Collections.Generic;

#nullable disable
namespace PX.Data.UserRecords;

/// <summary>
/// An interface for the user records database updater. Updates a batch of user records in the database when <see cref="T:PX.Data.PXTransactionScope" /> completes.
/// This functionality works together with <see cref="T:PX.Data.PXSearchableAttribute" /> functionality. Inside the attribute on DAC's row persisted event we add an entry to
/// a special list inside the root transaction scope. This entry <see cref="T:PX.Data.UserRecords.ModifiedDacEntryForUserRecordsUpdate" /> contains information required to update user records at the end of transaction,
/// similar to a mechanism Audit uses. The reason for a special treatment is a high number of DB deadlocks caused by the concurrent access to the VisitedRecord DB table
/// when we process each record separately inside <see cref="T:PX.Data.PXSearchableAttribute" />.
/// </summary>
internal interface IUserRecordsDBUpdater
{
  void UpdateUserRecords(
    IReadOnlyCollection<ModifiedDacEntryForUserRecordsUpdate> modifiedDacEntries);
}
