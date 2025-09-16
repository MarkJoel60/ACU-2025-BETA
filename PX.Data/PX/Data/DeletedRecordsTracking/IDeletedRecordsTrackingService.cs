// Decompiled with JetBrains decompiler
// Type: PX.Data.DeletedRecordsTracking.IDeletedRecordsTrackingService
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System.Collections.Generic;

#nullable enable
namespace PX.Data.DeletedRecordsTracking;

internal interface IDeletedRecordsTrackingService
{
  bool ContainsTable(System.Type dac);

  bool ContainsDac(System.Type dac);

  void SaveHistory(IEnumerable<DeleteTranInfo> toSave);

  DeleteTranInfo PrepareToTrack(System.Type dac, PXDataFieldParam[] parameters);

  void AddNoteIDValueIfNeed(PXCache sender, object row, System.Type dac, List<PXDataFieldRestrict> pars);

  void ResetTablesSlot();

  string GetTableNameFromDacType(System.Type dac);
}
