// Decompiled with JetBrains decompiler
// Type: PX.Data.DeletedRecordsTracking.DeletedRecordsTrackingService
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Common.Context;
using PX.Data.DeletedRecordsTracking.DAC;
using PX.DbServices.Commands;
using PX.DbServices.Commands.Data;
using PX.DbServices.Points;
using PX.DbServices.QueryObjectModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Compilation;

#nullable enable
namespace PX.Data.DeletedRecordsTracking;

internal class DeletedRecordsTrackingService : IDeletedRecordsTrackingService
{
  private const string SlotKey = "DeletedRecordsTracking$Tables";

  private DeletedRecordsTrackingService.ServiceCache GetServiceCache()
  {
    DeletedRecordsTrackingService.ServiceCache slot = PXContext.GetSlot<DeletedRecordsTrackingService.ServiceCache>("DeletedRecordsTracking$Tables");
    if (slot != null)
      return slot;
    return PXContext.SetSlot<DeletedRecordsTrackingService.ServiceCache>("DeletedRecordsTracking$Tables", PXDatabase.Provider.GetSlot<DeletedRecordsTrackingService.ServiceCache>("DeletedRecordsTracking$Tables", new PrefetchDelegate<DeletedRecordsTrackingService.ServiceCache>(this.Initialize), typeof (SMDeletedRecordsTrackingTables), typeof (PXGraph.FeaturesSet)));
  }

  public void ResetTablesSlot()
  {
    PXDatabase.Provider.ResetSlot<HashSet<string>>("DeletedRecordsTracking$Tables");
  }

  private DeletedRecordsTrackingService.ServiceCache Initialize()
  {
    DeletedRecordsTrackingMaint instance = PXGraph.CreateInstance<DeletedRecordsTrackingMaint>();
    DeletedRecordsTrackingService.ServiceCache result = new DeletedRecordsTrackingService.ServiceCache();
    EnumerableExtensions.ForEach<SMDeletedRecordsTrackingTables>((IEnumerable<SMDeletedRecordsTrackingTables>) instance.Tables.Select().Cast<SMDeletedRecordsTrackingTables>(), (System.Action<SMDeletedRecordsTrackingTables>) (x =>
    {
      result.Tables.Add(this.GetTableNameFromDacType(PXBuildManager.GetType(x.TableName, true)));
      result.Dacs.Add(x.TableName);
    }));
    return result;
  }

  public bool ContainsTable(System.Type dac)
  {
    return this.GetServiceCache().Tables.Contains(this.GetTableNameFromDacType(dac));
  }

  public bool ContainsDac(System.Type dac) => this.GetServiceCache().Dacs.Contains(dac.FullName);

  public DeleteTranInfo PrepareToTrack(System.Type dac, PXDataFieldParam[] parameters)
  {
    return new DeleteTranInfo()
    {
      TableName = this.GetTableNameFromDacType(dac),
      DacName = dac.FullName,
      NoteId = (Guid) ((IEnumerable<PXDataFieldParam>) parameters).First<PXDataFieldParam>((System.Func<PXDataFieldParam, bool>) (x => x.Column.Name == "NoteID")).Value,
      CompanyId = SlotStore.Instance.GetSingleCompanyId().GetValueOrDefault()
    };
  }

  public void AddNoteIDValueIfNeed(
    PXCache sender,
    object row,
    System.Type dac,
    List<PXDataFieldRestrict> pars)
  {
    if (!this.ContainsTable(dac) || pars.Any<PXDataFieldRestrict>((System.Func<PXDataFieldRestrict, bool>) (x => x.Column.Name == "NoteID")))
      return;
    object obj = sender.GetValue(row, "NoteID");
    if (obj == null)
      return;
    pars.Add((PXDataFieldRestrict) new PXDummyDataFieldRestrict("NoteID", PXDbType.UniqueIdentifier, new int?(16 /*0x10*/), obj));
  }

  public string GetTableNameFromDacType(System.Type dac) => BqlCommand.GetTableName(dac);

  public void SaveHistory(IEnumerable<DeleteTranInfo> toSave)
  {
    if (!toSave.Any<DeleteTranInfo>())
      return;
    YaqlScalar yaqlScalar = Yaql.utcnow();
    DeleteTranInfo deleteTranInfo1 = toSave.First<DeleteTranInfo>();
    CmdInsert cmdInsert = new CmdInsert(YaqlSchemaTable.op_Implicit("SMDeletedRecordsTrackingHistory"));
    cmdInsert.AddColumnWithValue<int>(YaqlColumn.op_Implicit("CompanyID"), deleteTranInfo1.CompanyId);
    cmdInsert.AddColumnWithValue<string>(YaqlColumn.op_Implicit("TableName"), deleteTranInfo1.TableName);
    cmdInsert.AddColumnWithValue<string>(YaqlColumn.op_Implicit("DacName"), deleteTranInfo1.DacName);
    cmdInsert.AddColumnWithValue<Guid>(YaqlColumn.op_Implicit("RefNoteID"), deleteTranInfo1.NoteId);
    cmdInsert.AddColumnWithValue<YaqlScalar>(YaqlColumn.op_Implicit("DeleteDate"), yaqlScalar);
    foreach (DeleteTranInfo deleteTranInfo2 in toSave.Skip<DeleteTranInfo>(1))
    {
      YaqlScalar[] yaqlScalarArray = cmdInsert.AddOtherRow();
      yaqlScalarArray[0] = Yaql.constant<int>(deleteTranInfo2.CompanyId, SqlDbType.Variant);
      yaqlScalarArray[1] = Yaql.constant<string>(deleteTranInfo2.TableName, SqlDbType.Variant);
      yaqlScalarArray[2] = Yaql.constant<string>(deleteTranInfo2.DacName, SqlDbType.Variant);
      yaqlScalarArray[3] = Yaql.constant<Guid>(deleteTranInfo2.NoteId, SqlDbType.Variant);
      yaqlScalarArray[4] = yaqlScalar;
    }
    PXDatabase.Provider.CreateDbServicesPoint().executeCommands((IEnumerable<CommandBase>) new \u003C\u003Ez__ReadOnlyArray<CommandBase>(new CommandBase[1]
    {
      (CommandBase) cmdInsert
    }), new ExecutionContext((IExecutionObserver) null), false);
  }

  private class ServiceCache
  {
    public HashSet<string> Tables { get; set; } = new HashSet<string>();

    public HashSet<string> Dacs { get; set; } = new HashSet<string>();
  }
}
