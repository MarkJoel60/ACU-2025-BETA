// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.PMAllocatorEmulator
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

#nullable disable
namespace PX.Objects.PM;

public class PMAllocatorEmulator : PMAllocator
{
  /// <summary>
  /// Gets or sets Source Transactions for the allocation. If null Transactions are selected from the database.
  /// </summary>
  public List<PMTran> SourceTransactions { get; set; }

  public override PXResultset<PMTran> GetTranFromDatabase(int? projectID, int groupID)
  {
    PXResultset<PMTran> tranFromDatabase = new PXResultset<PMTran>();
    foreach (PMTran sourceTransaction in this.SourceTransactions)
    {
      int? projectId = sourceTransaction.ProjectID;
      int? nullable = projectID;
      if (projectId.GetValueOrDefault() == nullable.GetValueOrDefault() & projectId.HasValue == nullable.HasValue)
      {
        nullable = sourceTransaction.AccountGroupID;
        int num = groupID;
        if (nullable.GetValueOrDefault() == num & nullable.HasValue)
          tranFromDatabase.Add(new PXResult<PMTran>(sourceTransaction));
      }
    }
    return tranFromDatabase;
  }

  [ExcludeFromCodeCoverage]
  public virtual void Persist()
  {
  }

  [ExcludeFromCodeCoverage]
  public virtual int Persist(Type cacheType, PXDBOperation operation) => 1;
}
