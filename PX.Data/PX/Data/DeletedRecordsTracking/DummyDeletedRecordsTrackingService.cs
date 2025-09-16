// Decompiled with JetBrains decompiler
// Type: PX.Data.DeletedRecordsTracking.DummyDeletedRecordsTrackingService
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Collections.Generic;

#nullable enable
namespace PX.Data.DeletedRecordsTracking;

/// <exclude />
internal class DummyDeletedRecordsTrackingService : IDeletedRecordsTrackingService
{
  public void AddNoteIDValueIfNeed(
    PXCache sender,
    object row,
    System.Type dac,
    List<PXDataFieldRestrict> pars)
  {
  }

  public bool ContainsDac(System.Type dac) => false;

  public bool ContainsTable(System.Type dac) => false;

  public string GetTableNameFromDacType(System.Type dac) => throw new NotImplementedException();

  public DeleteTranInfo PrepareToTrack(System.Type dac, PXDataFieldParam[] parameters)
  {
    throw new NotImplementedException();
  }

  public void ResetTablesSlot()
  {
  }

  public void SaveHistory(IEnumerable<DeleteTranInfo> toSave)
  {
  }
}
