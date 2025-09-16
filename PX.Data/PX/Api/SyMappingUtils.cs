// Decompiled with JetBrains decompiler
// Type: PX.Api.SyMappingUtils
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Async;
using PX.Common;
using PX.Common.Extensions;
using PX.Data;
using PX.SM;
using PX.Translation;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web.Compilation;

#nullable disable
namespace PX.Api;

internal static class SyMappingUtils
{
  private const string VIEW_NUMBER_SEPATATOR = ": ";

  public static void ProcessMapping(
    SYProcess graph,
    SYMappingActive mapping,
    SYImportOperation operation,
    CancellationToken token)
  {
    using (new SyMappingUtils.ProcessMappingScope(graph, (SYMapping) mapping))
    {
      SyProviderInstance.Provider = (object) SyMappingUtils.GetProvider((PXGraph) graph, (SYMapping) mapping, graph.Parameters);
      SYMappingActive updated = (SYMappingActive) PXSelectBase<SYMappingActive, PXSelect<SYMappingActive, Where<SYMapping.mappingID, Equal<Required<SYMapping.mappingID>>>>.Config>.Select((PXGraph) graph, (object) mapping.MappingID);
      SyMappingUtils.PrepareData(graph, mapping, operation, updated, token);
      SyMappingUtils.ImportData(graph, mapping, operation, updated, token);
      SyMappingUtils.RollbackData(graph, operation, updated);
      mapping.Status = updated.Status;
      mapping.DataCntr = updated.DataCntr;
      mapping.NbrRecords = updated.NbrRecords;
      mapping.PreparedOn = updated.PreparedOn;
      mapping.CompletedOn = updated.CompletedOn;
    }
  }

  private static System.DateTime? GetDateFromTimeStamp(string timeStamp)
  {
    System.DateTime result;
    return timeStamp != null && System.DateTime.TryParse(timeStamp, (IFormatProvider) CultureInfo.InvariantCulture, DateTimeStyles.None, out result) ? new System.DateTime?(result) : new System.DateTime?();
  }

  private static void PrepareData(
    SYProcess graph,
    SYMappingActive mapping,
    SYImportOperation operation,
    SYMappingActive updated,
    CancellationToken token)
  {
    if (operation.Operation == "C")
    {
      if (!(mapping.Status != "P") || !(mapping.Status != "F"))
      {
        bool? discardResult = mapping.DiscardResult;
        bool flag = true;
        if (discardResult.GetValueOrDefault() == flag & discardResult.HasValue)
          goto label_4;
      }
      else
        goto label_4;
    }
    if (!(operation.Operation == "P"))
      return;
label_4:
    PXSYTable pxsyTable = SyMappingUtils.RemoveEmptyRowIfSingle(graph.QueryPreparedData((SYMapping) mapping, operation, token));
    updated.Status = "P";
    updated.PreparedOn = new System.DateTime?(PXTimeZoneInfo.Now);
    updated.NbrRecords = new int?(pxsyTable.Count);
    updated.DataCntr = new int?(pxsyTable.Count);
    if (mapping.MappingType == "I")
    {
      updated.ImportTimeStamp = pxsyTable.TimeStamp;
    }
    else
    {
      updated.ExportTimeStamp = SyMappingUtils.GetDateFromTimeStamp(pxsyTable.TimeStamp);
      updated.ExportTimeStampUtc = SyMappingUtils.GetDateFromTimeStamp(pxsyTable.TimeStampUtc);
    }
    updated = graph.Mappings.Update(updated);
    SyMappingUtils.InsertHistoryEntry(graph, (SYMapping) updated, "P", updated.PreparedOn, updated.NbrRecords, pxsyTable.Description);
    using (new PXConnectionScope())
    {
      using (PXTransactionScope transactionScope = new PXTransactionScope())
      {
        SyMappingUtils.DeleteDataEntries(updated.MappingID);
        PXDataFieldAssign[] pxDataFieldAssignArray = new PXDataFieldAssign[13]
        {
          new PXDataFieldAssign(typeof (SYData.mappingID).Name, PXDbType.UniqueIdentifier, new int?(16 /*0x10*/), (object) updated.MappingID),
          new PXDataFieldAssign(typeof (SYData.lineNbr).Name, PXDbType.Int, new int?(8), (object) 0),
          new PXDataFieldAssign(typeof (SYData.isActive).Name, PXDbType.Bit, new int?(1), (object) 1),
          new PXDataFieldAssign(typeof (SYData.isProcessed).Name, PXDbType.Bit, new int?(1), (object) 0),
          new PXDataFieldAssign(typeof (SYData.fieldValues).Name, PXDbType.NVarChar, new int?(0), (object) ""),
          new PXDataFieldAssign(typeof (SYData.createdByID).Name, PXDbType.UniqueIdentifier, new int?(16 /*0x10*/), (object) updated.LastModifiedByID),
          new PXDataFieldAssign(typeof (SYData.createdByScreenID).Name, PXDbType.Char, new int?(8), (object) updated.LastModifiedByScreenID),
          new PXDataFieldAssign(typeof (SYData.createdDateTime).Name, PXDbType.SmallDateTime, new int?(4), (object) updated.LastModifiedDateTime),
          new PXDataFieldAssign(typeof (SYData.lastModifiedByID).Name, PXDbType.UniqueIdentifier, new int?(16 /*0x10*/), (object) updated.LastModifiedByID),
          new PXDataFieldAssign(typeof (SYData.lastModifiedByScreenID).Name, PXDbType.Char, new int?(8), (object) updated.LastModifiedByScreenID),
          new PXDataFieldAssign(typeof (SYData.lastModifiedDateTime).Name, PXDbType.SmallDateTime, new int?(4), (object) updated.LastModifiedDateTime),
          new PXDataFieldAssign(typeof (SYData.keys).Name, PXDbType.NVarChar, new int?(0), (object) null),
          new PXDataFieldAssign(typeof (SYData.noteID).Name, PXDbType.UniqueIdentifier, new int?(16 /*0x10*/), (object) Guid.NewGuid())
        };
        int num = 0;
        foreach (PXSYRow fields in pxsyTable)
        {
          string field1;
          pxDataFieldAssignArray[4].Value = (object) (field1 = SYData.JoinFields((IEnumerable<string>) fields));
          pxDataFieldAssignArray[4].ValueLength = new int?(SYData.GetFieldLength(field1));
          pxDataFieldAssignArray[1].Value = (object) ++num;
          if (fields.Keys != null)
          {
            string field2;
            pxDataFieldAssignArray[11].Value = (object) (field2 = SYData.JoinFields((IEnumerable<string>) fields.Keys.Values));
            pxDataFieldAssignArray[11].ValueLength = new int?(SYData.GetFieldLength(field2));
          }
          pxDataFieldAssignArray[12].Value = (object) Guid.NewGuid();
          PXDatabase.Insert<SYData>(pxDataFieldAssignArray);
        }
        graph.Data.Cache.Clear();
        graph.Data.View.Clear();
        graph.Save.Press();
        transactionScope.Complete();
      }
    }
  }

  private static void ImportData(
    SYProcess graph,
    SYMappingActive mapping,
    SYImportOperation operation,
    SYMappingActive updated,
    CancellationToken token)
  {
    if (!(operation.Operation == "C") && !(operation.Operation == "I"))
      return;
    updated.Status = "F";
    updated = graph.Mappings.Update(updated);
    graph.Save.Press();
    IList<SYProviderField> sources = SyMappingUtils.LoadProviderFields(graph);
    if (mapping.IsExportOnlyMappingFields.GetValueOrDefault())
    {
      IPXSYProviderWithRequiredFields provider = SyProviderInstance.Provider as IPXSYProviderWithRequiredFields;
      HashSet<string> hashSet = SyMappingUtils.LoadScriptCommands((PXGraph) graph, (SYMapping) mapping).Select<SYMappingField, string>((Func<SYMappingField, string>) (c => c.Value)).Concat<string>((IEnumerable<string>) provider?.RequiredFields ?? Enumerable.Empty<string>()).Distinct<string>().ToHashSet<string>();
      foreach (SYProviderField syProviderField in (IEnumerable<SYProviderField>) sources)
      {
        if (!hashSet.Contains(syProviderField.Name))
          syProviderField.IsMapped = new bool?(false);
      }
    }
    PXSYTableEx pxsyTableEx = SyMappingUtils.ReadPreparedData(graph, sources, mapping);
    int num = graph.ImportPreparedData((SYMapping) mapping, operation, (PXSYTable) pxsyTableEx, token);
    bool flag = num == pxsyTableEx.Count;
    if (flag)
    {
      updated.Status = "I";
      updated.CompletedOn = new System.DateTime?(PXTimeZoneInfo.Now);
      updated = graph.Mappings.Update(updated);
    }
    SyMappingUtils.InsertHistoryEntry(graph, (SYMapping) updated, "I", flag ? updated.CompletedOn : new System.DateTime?(PXTimeZoneInfo.Now), new int?(num), (string) null);
    using (new PXConnectionScope())
    {
      using (PXTransactionScope transactionScope = new PXTransactionScope())
      {
        PXDataFieldParam[] pxDataFieldParamArray = new PXDataFieldParam[7]
        {
          (PXDataFieldParam) new PXDataFieldRestrict("mappingID", PXDbType.UniqueIdentifier, new int?(16 /*0x10*/), (object) updated.MappingID),
          (PXDataFieldParam) new PXDataFieldRestrict("lineNbr", PXDbType.Int, new int?(8), (object) 0),
          (PXDataFieldParam) new PXDataFieldAssign("isProcessed", PXDbType.Bit, new int?(1), (object) 1),
          (PXDataFieldParam) new PXDataFieldAssign("errorMessage", PXDbType.NVarChar, new int?(0), (object) null),
          (PXDataFieldParam) new PXDataFieldAssign("fieldErrors", PXDbType.NVarChar, new int?(0), (object) null),
          (PXDataFieldParam) new PXDataFieldAssign("fieldExceptions", PXDbType.NVarChar, new int?(0), (object) null),
          (PXDataFieldParam) new PXDataFieldAssign("keys", PXDbType.NVarChar, new int?(0), (object) null)
        };
        foreach (SYData syData in graph.Data.Cache.Updated)
        {
          pxDataFieldParamArray[1].Value = (object) syData.LineNbr;
          pxDataFieldParamArray[2].Value = (object) syData.IsProcessed;
          pxDataFieldParamArray[3].Value = (object) syData.ErrorMessage;
          pxDataFieldParamArray[3].ValueLength = new int?(SYData.GetFieldLength(syData.ErrorMessage));
          pxDataFieldParamArray[4].Value = (object) syData.FieldErrors;
          pxDataFieldParamArray[4].ValueLength = new int?(SYData.GetFieldLength(syData.FieldErrors));
          pxDataFieldParamArray[5].Value = (object) syData.FieldExceptions;
          pxDataFieldParamArray[5].ValueLength = new int?(SYData.GetFieldLength(syData.FieldExceptions));
          pxDataFieldParamArray[6].Value = (object) syData.Keys;
          pxDataFieldParamArray[6].ValueLength = new int?(SYData.GetFieldLength(syData.Keys));
          PXDatabase.Update<SYData>(pxDataFieldParamArray);
        }
        graph.Data.Cache.Clear();
        graph.Data.View.Clear();
        graph.Save.Press();
        transactionScope.Complete();
      }
    }
    graph.RaiseOnImportCompleted((SYMapping) mapping, pxsyTableEx);
    if (num < pxsyTableEx.Count && !token.IsCancellationRequested)
      throw new PXSetPropertyException("{0} items have not been processed successfully. View name: {1}.", (PXErrorLevel) (num > 0 ? 3 : 4), new object[2]
      {
        (object) (pxsyTableEx.Count - num),
        (object) mapping.ViewName
      });
  }

  private static void RollbackData(
    SYProcess graph,
    SYImportOperation operation,
    SYMappingActive updated)
  {
    if (!(operation.Operation == "R"))
      return;
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    SyMappingUtils.RollbackData(graph, updated, SyMappingUtils.\u003C\u003EO.\u003C0\u003E__RollbackHistory ?? (SyMappingUtils.\u003C\u003EO.\u003C0\u003E__RollbackHistory = new SyMappingUtils.RollbackHistoryHandler(SyMappingUtils.RollbackHistory)));
  }

  internal static void RollbackData(
    SYProcess graph,
    SYMappingActive mapping,
    SyMappingUtils.RollbackHistoryHandler rollbackHistory)
  {
    mapping.Status = "N";
    mapping.DataCntr = new int?(0);
    mapping.NbrRecords = new int?(0);
    mapping.PreparedOn = new System.DateTime?();
    mapping.CompletedOn = new System.DateTime?();
    mapping.ImportTimeStamp = (string) null;
    mapping.ExportTimeStamp = new System.DateTime?();
    mapping.ExportTimeStampUtc = new System.DateTime?();
    System.DateTime? dateLimit = rollbackHistory(graph, (SYMapping) mapping);
    using (new PXConnectionScope())
    {
      using (PXTransactionScope transactionScope = new PXTransactionScope())
      {
        SyMappingUtils.DeleteHistoryEntries(mapping.MappingID, dateLimit, graph);
        SyMappingUtils.DeleteDataEntries(mapping.MappingID);
        graph.SelectTimeStamp();
        mapping = graph.Mappings.Update(mapping);
        graph.Mappings.Cache.PersistUpdated((object) mapping);
        graph.History.Cache.Clear();
        graph.History.View.Clear();
        graph.Data.Cache.Clear();
        graph.Data.View.Clear();
        graph.Actions.PressSave();
        transactionScope.Complete();
      }
    }
  }

  private static PXSYTable RemoveEmptyRowIfSingle(PXSYTable preparedData)
  {
    if (preparedData.Rows.Count != 1 || !((IEnumerable<PXSYItem>) preparedData.Rows[0].Items).All<PXSYItem>((Func<PXSYItem, bool>) (c => c.IsAbsent)))
      return preparedData;
    preparedData.Rows.RemoveAt(0);
    return preparedData;
  }

  internal static PXSYTable QueryProviderForPreparedData(
    SYProcess graph,
    SYMapping mapping,
    SYImportOperation operation)
  {
    IList<SYProviderField> syProviderFieldList = SyMappingUtils.LoadProviderFields(graph);
    List<PXSYFilterRow> filters = new List<PXSYFilterRow>();
    SyMappingUtils.LoadProviderFilters(graph, mapping, syProviderFieldList, filters);
    return SyMappingUtils.QueryProvider(graph, mapping, operation, syProviderFieldList, filters);
  }

  private static void OnRowPersisting(SYProcess graph, int rowIndex, List<SYData> data)
  {
    if (rowIndex >= data.Count || rowIndex < 0)
      return;
    SYData syData = data[rowIndex];
    PXDatabase.Update<SYData>((PXDataFieldParam) new PXDataFieldAssign("IsProcessed", PXDbType.Bit, new int?(1), (object) 1), (PXDataFieldParam) new PXDataFieldRestrict("MappingID", PXDbType.UniqueIdentifier, new int?(16 /*0x10*/), (object) syData.MappingID), (PXDataFieldParam) new PXDataFieldRestrict("LineNbr", PXDbType.Int, new int?(4), (object) syData.LineNbr));
    PXTimeStampScope.PutPersisted(graph.Data.Cache, (object) syData, (object) PXDatabase.SelectTimeStamp());
  }

  internal static SyImportRowResult[] ImportPreparedData(
    PXGraph graph,
    SYMapping mapping,
    PXSYTable preparedData,
    SYImportOperation operation,
    SyImportContext.RowPersistingDelegate onRowPersisting,
    StringComparer fieldNamesComparer)
  {
    return SyMappingUtils.ImportPreparedData(graph, mapping, preparedData, operation, onRowPersisting, fieldNamesComparer, CancellationToken.None);
  }

  internal static SyImportRowResult[] ImportPreparedData(
    PXGraph graph,
    SYMapping mapping,
    PXSYTable preparedData,
    SYImportOperation operation,
    SyImportContext.RowPersistingDelegate onRowPersisting,
    StringComparer fieldNamesComparer,
    CancellationToken token)
  {
    PXFilterRow[] filters = SyMappingUtils.ParseFilters(SyMappingUtils.ReadTargetConditions(graph, mapping));
    SYMappingField[] array = SyMappingUtils.LoadScriptCommands(graph, mapping).ToArray<SYMappingField>();
    bool valueOrDefault1 = operation.BreakOnError.GetValueOrDefault();
    bool valueOrDefault2 = operation.BreakOnTarget.GetValueOrDefault();
    SYValidation validationSettings = operation.GetValidationSettings();
    SyImportContext context = new SyImportContext(mapping, array, preparedData, valueOrDefault1, valueOrDefault2, validationSettings, filters, fieldNamesComparer, operation.BatchSize.GetValueOrDefault())
    {
      RowPersisting = onRowPersisting
    };
    PXParallelProcessingOptions parallelOpt = new PXParallelProcessingOptions()
    {
      IsEnabled = ((int) operation.ProcessInParallel ?? (mapping.ProcessInParallel.GetValueOrDefault() ? 1 : 0)) != 0
    };
    if (!parallelOpt.IsEnabled)
    {
      context.FillImportResultExternalKeys();
      using (new PXScreenIDScope(mapping.ScreenID))
        SyImportProcessor.ImportTable(context, token);
    }
    else
    {
      object dataProvider = SyProviderInstance.Provider;
      PXBatchList.ProcessAll(context.SplitToBatches(parallelOpt).Select<SyImportContext, System.Action<CancellationToken>>((Func<SyImportContext, System.Action<CancellationToken>>) (subContext => (System.Action<CancellationToken>) (batchCancellationToken => CancellationIgnorantExtensions.RunWithCancellationViaThreadAbort((System.Action) (() =>
      {
        SyProviderInstance.Provider = dataProvider;
        subContext.FillImportResultExternalKeys(context);
        using (new PXScreenIDScope(mapping.ScreenID))
          SyImportProcessor.ImportTable(subContext, batchCancellationToken);
        SyMappingUtils.MergeResultToContext(subContext, context);
      }), batchCancellationToken)))), CancellationToken.None);
    }
    SyImportProcessor.FillImportResultKeys(context);
    return context.ImportResult;
  }

  private static void MergeResultToContext(SyImportContext subContext, SyImportContext context)
  {
    if (subContext == context)
      return;
    for (int startRow = subContext.StartRow; startRow <= subContext.EndRow; ++startRow)
      context.ImportResult[startRow] = subContext.ImportResult[startRow];
  }

  public static int ImportPreparedData(
    SYProcess graph,
    SYMapping mapping,
    SYImportOperation operation,
    PXSYTable preparedData,
    StringComparer fieldNamesComparer,
    CancellationToken token)
  {
    PXSYTableEx preparedDataEx = (PXSYTableEx) preparedData;
    SyImportRowResult[] importResults = SyMappingUtils.ImportPreparedData((PXGraph) graph, mapping, preparedData, operation, (SyImportContext.RowPersistingDelegate) (rowIndex => SyMappingUtils.OnRowPersisting(graph, rowIndex, preparedDataEx.Data)), fieldNamesComparer, token);
    return SyMappingUtils.MergePreparedDataWithImportResults(graph, preparedDataEx, importResults, ((int) operation.BreakOnTarget ?? 1) != 0);
  }

  public static int WritePreparedDataToProvider(
    SYProcess graph,
    SYMapping mapping,
    SYImportOperation operation,
    PXSYTable preparedData)
  {
    IPXSYProvider provider = SyMappingUtils.GetProvider((PXGraph) graph, mapping, operation, graph.Parameters);
    List<SyProviderRowResult> results = new List<SyProviderRowResult>();
    SYProviderObject syProviderObject = (SYProviderObject) PXSelectBase<SYProviderObject, PXSelect<SYProviderObject, Where<SYProviderObject.providerID, Equal<Current<SYMapping.providerID>>, And<SYProviderObject.name, Equal<Current<SYMapping.providerObject>>, And<SYProviderObject.isActive, Equal<PX.Data.True>>>>>.Config>.Select((PXGraph) graph);
    if (syProviderObject == null)
      throw new PXException("Cannot find the specified provider object.");
    string objectName = syProviderObject.Command ?? syProviderObject.Name;
    PXSYTable table = preparedData;
    bool? nullable = operation.BreakOnError;
    int num = (int) nullable ?? 1;
    System.Action<SyProviderRowResult> callback = (System.Action<SyProviderRowResult>) (r =>
    {
      if (r.RowIndex != results.Count)
        throw new PXException("Invalid row index");
      if (!r.HasError && !string.IsNullOrEmpty(r.ErrorMessage))
        throw new PXException("HasError conflicts with ErrorMessage.");
      results.Add(r);
    });
    provider.Export(objectName, table, num != 0, callback);
    List<SYData> data = ((PXSYTableEx) preparedData).Data;
    for (int index = 0; index < data.Count && index < results.Count; ++index)
    {
      SYData syData = data[index];
      SyProviderRowResult providerRowResult = results[index];
      syData.ErrorMessage = providerRowResult.ErrorMessage;
      syData.IsProcessed = new bool?(!providerRowResult.HasError);
      nullable = syData.IsProcessed;
      bool flag = true;
      if (nullable.GetValueOrDefault() == flag & nullable.HasValue)
        syData.ExtRefNbr = providerRowResult.ExtRefNbr;
      graph.Data.Update(syData);
    }
    return results.Count;
  }

  internal static PXSYTable ExportPreparedData(
    SYProcess graph,
    SYMapping mapping,
    bool breakOnError)
  {
    return SyMappingUtils.ExportPreparedData(graph, mapping, breakOnError, CancellationToken.None);
  }

  internal static PXSYTable ExportPreparedData(
    SYProcess graph,
    SYMapping mapping,
    bool breakOnError,
    CancellationToken token)
  {
    return SyMappingUtils.ExportPreparedData(graph, mapping, breakOnError, false, token);
  }

  public static PXSYTable ExportPreparedData(
    SYProcess graph,
    SYMapping mapping,
    bool breakOnError,
    bool newApi,
    CancellationToken token)
  {
    IEnumerable<SYMappingCondition> mappingConditions = SyMappingUtils.ReadTargetConditions((PXGraph) graph, mapping);
    SYMappingField[] array1 = SyMappingUtils.LoadScriptCommands((PXGraph) graph, mapping).ToArray<SYMappingField>();
    string[] array2 = SyMappingUtils.LoadProviderFields(graph).Select<SYProviderField, string>((Func<SYProviderField, string>) (f => f.Name)).ToArray<string>();
    string viewName = mapping.ViewName;
    foreach (SYMappingCondition mappingCondition in mappingConditions.Where<SYMappingCondition>((Func<SYMappingCondition, bool>) (c => string.IsNullOrEmpty(c.ObjectName))))
      mappingCondition.ObjectName = viewName;
    SyExportContext context = new SyExportContext(mapping, (IEnumerable<SYMappingField>) array1, array2, SyMappingUtils.ParseFiltersGroupByObjectName(mappingConditions), breakOnError);
    context.NewApi = newApi;
    System.DateTime dtLocal;
    System.DateTime dtUtc;
    PXDatabase.SelectDate(out dtLocal, out dtUtc);
    System.DateTime dateTime = PXTimeZoneInfo.ConvertTimeFromUtc(dtUtc, LocaleInfo.GetTimeZone());
    if (mapping.SyncType != "F")
    {
      System.DateTime? nullable1 = mapping.ExportTimeStamp;
      if (nullable1.HasValue)
      {
        SyExportContext syExportContext1 = context;
        nullable1 = mapping.ExportTimeStamp;
        KeyValuePair<System.DateTime, System.DateTime>? nullable2 = new KeyValuePair<System.DateTime, System.DateTime>?(new KeyValuePair<System.DateTime, System.DateTime>(nullable1.Value, dtLocal));
        syExportContext1.TimeRange = nullable2;
        nullable1 = mapping.ExportTimeStampUtc;
        if (nullable1.HasValue)
        {
          SyExportContext syExportContext2 = context;
          nullable1 = mapping.ExportTimeStampUtc;
          KeyValuePair<System.DateTime, System.DateTime>? nullable3 = new KeyValuePair<System.DateTime, System.DateTime>?(new KeyValuePair<System.DateTime, System.DateTime>(nullable1.Value, dateTime));
          syExportContext2.TimeRangeUtc = nullable3;
        }
        context.NewOnly = mapping.SyncType == "N";
      }
    }
    PXSYTablePr prepareResults = SyImportProcessor.ExportTable(context, token);
    prepareResults.TimeStamp = dtLocal.ToString((IFormatProvider) CultureInfo.InvariantCulture);
    SyImportProcessor.FillPrepareResultKeys(context, prepareResults);
    prepareResults.TimeStampUtc = dateTime.ToString((IFormatProvider) CultureInfo.InvariantCulture);
    return (PXSYTable) prepareResults;
  }

  private static PXFilterRow[] ParseFilters(IEnumerable<SYMappingCondition> targetConditions)
  {
    List<PXFilterRow> pxFilterRowList = new List<PXFilterRow>();
    System.DateTime now = System.DateTime.Now;
    System.DateTime dateTime = PXContext.GetBusinessDate() ?? new System.DateTime(now.Year, now.Month, now.Day);
    foreach (SYMappingCondition targetCondition in targetConditions)
    {
      object obj1 = RelativeDatesManager.IsRelativeDatesString(targetCondition.Value) ? (object) RelativeDatesManager.EvaluateAsString(targetCondition.Value) : (object) targetCondition.Value;
      object obj2 = RelativeDatesManager.IsRelativeDatesString(targetCondition.Value2) ? (object) RelativeDatesManager.EvaluateAsString(targetCondition.Value2) : (object) targetCondition.Value2;
      string fieldName = targetCondition.FieldName;
      int? condition1 = targetCondition.Condition;
      int? nullable1;
      int? nullable2;
      if (!condition1.HasValue)
      {
        nullable1 = new int?();
        nullable2 = nullable1;
      }
      else
        nullable2 = new int?(condition1.GetValueOrDefault() - 1);
      nullable1 = nullable2;
      int condition2 = nullable1.Value;
      object obj3 = obj1;
      object obj4 = obj2;
      PXFilterRow pxFilterRow1 = new PXFilterRow(fieldName, (PXCondition) condition2, obj3, obj4);
      nullable1 = targetCondition.OpenBrackets;
      pxFilterRow1.OpenBrackets = nullable1.Value;
      nullable1 = targetCondition.CloseBrackets;
      pxFilterRow1.CloseBrackets = nullable1.Value;
      nullable1 = targetCondition.Operator;
      int num = 1;
      pxFilterRow1.OrOperator = nullable1.GetValueOrDefault() == num & nullable1.HasValue;
      pxFilterRow1.LocaleOverride = "en-US";
      PXFilterRow pxFilterRow2 = pxFilterRow1;
      pxFilterRowList.Add(pxFilterRow2);
    }
    return pxFilterRowList.ToArray();
  }

  private static Dictionary<string, PXFilterRow[]> ParseFiltersGroupByObjectName(
    IEnumerable<SYMappingCondition> targetConditions)
  {
    return targetConditions.GroupBy<SYMappingCondition, string>((Func<SYMappingCondition, string>) (tc => SyMappingUtils.ParseViewNameFromObjectName(tc.ObjectName))).ToDictionary<IGrouping<string, SYMappingCondition>, string, PXFilterRow[]>((Func<IGrouping<string, SYMappingCondition>, string>) (g => g.Key), (Func<IGrouping<string, SYMappingCondition>, PXFilterRow[]>) (g => SyMappingUtils.ParseFilters((IEnumerable<SYMappingCondition>) g)));
  }

  private static string ParseViewNameFromObjectName(string objectName)
  {
    int startIndex = objectName.IndexOf(": ");
    return startIndex <= 0 ? objectName : objectName.Remove(startIndex);
  }

  private static System.DateTime? RollbackHistory(SYProcess graph, SYMapping mapping)
  {
    System.DateTime? nullable = new System.DateTime?(System.DateTime.MaxValue);
    if (SyMappingUtils.GetLastHistoryRecord(graph, mapping.MappingID, nullable.Value) != null)
    {
      SYHistory lastHistoryRecord1 = SyMappingUtils.GetLastHistoryRecord(graph, mapping.MappingID, nullable.Value, "I");
      if (lastHistoryRecord1 == null)
      {
        nullable = new System.DateTime?();
      }
      else
      {
        nullable = new System.DateTime?(lastHistoryRecord1.StatusDate.Value);
        mapping.Status = lastHistoryRecord1.Status;
        mapping.NbrRecords = lastHistoryRecord1.NbrRecords;
        mapping.ImportTimeStamp = lastHistoryRecord1.ImportTimeStamp;
        mapping.ExportTimeStamp = lastHistoryRecord1.ExportTimeStamp;
        mapping.ExportTimeStampUtc = lastHistoryRecord1.ExportTimeStampUtc;
        mapping.CompletedOn = lastHistoryRecord1.StatusDate;
        SYHistory lastHistoryRecord2 = SyMappingUtils.GetLastHistoryRecord(graph, mapping.MappingID, lastHistoryRecord1.StatusDate.Value, "P");
        if (lastHistoryRecord2 != null)
          mapping.PreparedOn = lastHistoryRecord2.StatusDate;
      }
    }
    return nullable;
  }

  internal static System.DateTime ConvertDateToDatabaseFormat(
    System.DateTime dateTime,
    PXCache fieldCache,
    string fieldName)
  {
    PXDBDateAttribute pxdbDateAttribute = fieldCache.GetAttributesOfType<PXDBDateAttribute>((object) null, fieldName).FirstOrDefault<PXDBDateAttribute>();
    return (pxdbDateAttribute != null ? (pxdbDateAttribute.UseTimeZone ? 1 : 0) : 0) == 0 ? dateTime : PXTimeZoneInfo.ConvertTimeToUtc(dateTime, LocaleInfo.GetTimeZone());
  }

  internal static void DeleteDataEntries(Guid? mappingID)
  {
    using (new PXCommandScope(PXDatabase.Provider.DefaultQueryTimeout * 20))
      PXDatabase.Delete<SYData>(new PXDataFieldRestrict(nameof (mappingID), PXDbType.UniqueIdentifier, new int?(16 /*0x10*/), (object) mappingID));
  }

  internal static void DeleteHistoryEntries(Guid? mappingID, System.DateTime? dateLimit = null, SYProcess graph = null)
  {
    using (new PXCommandScope(PXDatabase.Provider.DefaultQueryTimeout * 20))
    {
      List<PXDataFieldRestrict> dataFieldRestrictList = new List<PXDataFieldRestrict>(2)
      {
        new PXDataFieldRestrict(nameof (mappingID), PXDbType.UniqueIdentifier, new int?(16 /*0x10*/), (object) mappingID)
      };
      if (dateLimit.HasValue)
      {
        dateLimit = new System.DateTime?(SyMappingUtils.ConvertDateToDatabaseFormat(dateLimit.Value, graph?.History.Cache, "statusDate"));
        dataFieldRestrictList.Add(new PXDataFieldRestrict("statusDate", PXDbType.DateTime, new int?(8), (object) dateLimit, PXComp.GT));
      }
      PXDatabase.Delete<SYHistory>(dataFieldRestrictList.ToArray());
    }
  }

  private static void InsertHistoryEntry(
    SYProcess graph,
    SYMapping mapping,
    string status,
    System.DateTime? statusDate,
    int? nbrRecords,
    string description)
  {
    SYHistory syHistory = new SYHistory()
    {
      Status = status,
      StatusDate = statusDate,
      NbrRecords = nbrRecords,
      Description = description,
      ImportTimeStamp = mapping.ImportTimeStamp,
      ExportTimeStamp = mapping.ExportTimeStamp,
      ExportTimeStampUtc = mapping.ExportTimeStampUtc
    };
    graph.History.Insert(syHistory);
  }

  private static int MergePreparedDataWithImportResults(
    SYProcess graph,
    PXSYTableEx preparedDataEx,
    SyImportRowResult[] importResults,
    bool breakOnTarget)
  {
    int num = 0;
    List<SYData> data = preparedDataEx.Data;
    for (int index = 0; index < data.Count && index < importResults.Length; ++index)
    {
      SYData syData = data[index];
      bool? isActive = syData.IsActive;
      bool flag1 = true;
      if (isActive.GetValueOrDefault() == flag1 & isActive.HasValue)
      {
        bool? isProcessed = syData.IsProcessed;
        bool flag2 = false;
        if (isProcessed.GetValueOrDefault() == flag2 & isProcessed.HasValue)
        {
          SyImportRowResult importResult = importResults[index];
          if (importResult.IsPersisted && importResult.Error == null && importResult.PersistingError == null)
          {
            syData.IsProcessed = new bool?(true);
            if (importResult.Keys != null)
              syData.WriteKeys((IEnumerable<string>) importResult.Keys.Values);
            preparedDataEx[index].Keys = importResult.Keys;
            preparedDataEx[index].IsProcessed = true;
          }
          syData.FieldExceptions = importResult.GetFieldExceptions();
          syData.FieldErrors = importResult.GetFieldErrors();
          syData.ErrorMessage = importResult.GetErrorMessage(true, false);
          if (importResult.IsPersisted && importResult.Error == null && importResult.PersistingError == null || !breakOnTarget && importResult.Error is SyImportProcessor.InvalidTargetException)
            ++num;
          graph.Data.Update(syData);
        }
      }
    }
    return num;
  }

  private static IEnumerable<SYMappingCondition> ReadTargetConditions(
    PXGraph graph,
    SYMapping mapping)
  {
    PXGraph graph1 = graph;
    object[] objArray = new object[1]
    {
      (object) mapping.MappingID
    };
    foreach (PXResult<SYMappingCondition> pxResult in PXSelectBase<SYMappingCondition, PXSelect<SYMappingCondition, Where<SYMappingCondition.mappingID, Equal<Required<SYMapping.mappingID>>, And<SYMappingCondition.isActive, Equal<PX.Data.True>>>>.Config>.Select(graph1, objArray))
      yield return (SYMappingCondition) pxResult;
  }

  internal static void RaiseOnImportCompleted(SYProcess graph, SYMapping mapping, PXSYTableEx data)
  {
    if (!(SyMappingUtils.GetProvider((PXGraph) graph, mapping, graph.Parameters) is IPXSYProviderWithCallback provider) || provider.OnImportCompleted == null)
      return;
    provider.OnImportCompleted(data);
  }

  private static PXSYTableEx ReadPreparedData(
    SYProcess graph,
    IList<SYProviderField> sources,
    SYMappingActive mapping)
  {
    SYProviderField[] array = sources.Where<SYProviderField>((Func<SYProviderField, bool>) (c =>
    {
      bool? isMapped = c.IsMapped;
      bool flag = true;
      return isMapped.GetValueOrDefault() == flag & isMapped.HasValue;
    })).ToArray<SYProviderField>();
    string[] source = new string[array.Length];
    for (int index = 0; index < array.Length; ++index)
      source[index] = array[index].Name;
    PXSYTableEx table = new PXSYTableEx(((IEnumerable<string>) source).Distinct<string>());
    PXGraph graph1 = SyImportProcessor.CreateGraph(mapping.GraphName, mapping.ScreenID);
    foreach (PXResult<SYData> pxResult in graph.Data.Select())
    {
      SYData row = (SYData) pxResult;
      bool? nullable = row.IsActive;
      bool flag1 = true;
      if (nullable.GetValueOrDefault() == flag1 & nullable.HasValue)
      {
        nullable = row.IsProcessed;
        bool flag2 = false;
        if (nullable.GetValueOrDefault() == flag2 & nullable.HasValue)
        {
          table.Data.Add(row);
          PXSYRow pxsyRow = new PXSYRow((PXSYTable) table);
          string[] strArray = SYData.SplitFields(row.FieldValues);
          int index1 = 0;
          for (int index2 = 0; index2 < sources.Count && index1 < pxsyRow.Count; ++index2)
          {
            nullable = sources[index2].IsMapped;
            bool flag3 = true;
            if (nullable.GetValueOrDefault() == flag3 & nullable.HasValue && index2 < strArray.Length)
            {
              pxsyRow[index1] = strArray[index2] != "" ? strArray[index2] : (string) null;
              ++index1;
            }
          }
          pxsyRow.Keys = SyMappingUtils.ReadPreparedDataKeys(graph1, mapping.GridViewName, row);
          table.Add(pxsyRow);
        }
      }
    }
    return table;
  }

  public static PXCache GetKeysSourceCache(
    string primaryView,
    string gridView,
    PXGraph graph,
    out bool isGI)
  {
    PXCache keysSourceCache = (PXCache) null;
    isGI = false;
    if (!string.IsNullOrEmpty(primaryView))
    {
      PXCache cache = graph.Views[primaryView].Cache;
      if (cache.Keys.Count == 0 && !string.IsNullOrEmpty(gridView))
      {
        isGI = true;
        keysSourceCache = graph.Views[gridView].Cache;
      }
      else
        keysSourceCache = cache;
    }
    return keysSourceCache;
  }

  internal static Dictionary<string, string> ReadPreparedDataKeys(
    PXGraph sourceGraph,
    string gridView,
    SYData row)
  {
    Dictionary<string, string> dictionary = new Dictionary<string, string>();
    string[] source = row.ReadKeys();
    if (source != null && ((IEnumerable<string>) source).Any<string>())
    {
      PXCache keysSourceCache = SyMappingUtils.GetKeysSourceCache(sourceGraph.PrimaryView, gridView, sourceGraph, out bool _);
      if (keysSourceCache != null)
      {
        string[] array = keysSourceCache.Keys.ToArray();
        for (int index = 0; index < array.Length; ++index)
          dictionary[array[index]] = source[index];
      }
    }
    return dictionary;
  }

  private static PXSYTable QueryProvider(
    SYProcess graph,
    SYMapping mapping,
    SYImportOperation operation,
    IList<SYProviderField> sources,
    List<PXSYFilterRow> filters)
  {
    IPXSYProvider provider = SyMappingUtils.GetProvider((PXGraph) graph, mapping, operation, graph.Parameters);
    if (sources.Count == 0)
      throw new PXException("No source field mapping is specified.");
    SYProviderObject syProviderObject = (SYProviderObject) PXSelectBase<SYProviderObject, PXSelect<SYProviderObject, Where<SYProviderObject.providerID, Equal<Current<SYMapping.providerID>>, And<SYProviderObject.name, Equal<Current<SYMapping.providerObject>>, And<SYProviderObject.isActive, Equal<PX.Data.True>>>>>.Config>.Select((PXGraph) graph);
    if (syProviderObject == null)
      throw new PXException("Cannot find the specified provider object.");
    string[] fieldNames = new string[sources.Count];
    for (int index = 0; index < sources.Count; ++index)
      fieldNames[index] = sources[index].Command ?? sources[index].Name;
    string importTimeStamp = mapping.SyncType != "F" ? mapping.ImportTimeStamp : (string) null;
    return provider.Import(syProviderObject.Command ?? syProviderObject.Name, fieldNames, filters.ToArray(), importTimeStamp, SyMappingUtils.ConvertSyncType(mapping.SyncType));
  }

  internal static IPXSYProvider GetProvider(
    PXGraph graph,
    SYMapping mapping,
    SYImportOperation operation,
    PXSYParameter[] input)
  {
    if (operation != null && operation.SkipHeaders.GetValueOrDefault())
      input = new List<PXSYParameter>((IEnumerable<PXSYParameter>) input)
      {
        new PXSYParameter("SkipHeaders", bool.TrueString)
      }.ToArray();
    return SyMappingUtils.GetProvider(graph, mapping, input);
  }

  internal static IPXSYProvider GetProvider(
    PXGraph graph,
    SYMapping mapping,
    PXSYParameter[] input)
  {
    IPXSYProvider provider = (IPXSYProvider) null;
    SYProvider data = (SYProvider) PXSelectBase<SYProvider, PXSelect<SYProvider, Where<SYProvider.providerID, Equal<Required<SYMapping.providerID>>>>.Config>.Select(graph, (object) mapping.ProviderID);
    if (data != null && !string.IsNullOrEmpty(data.ProviderType))
    {
      System.Type type = PXBuildManager.GetType(data.ProviderType, false);
      if (type != (System.Type) null)
        provider = Activator.CreateInstance(type) as IPXSYProvider;
    }
    if (provider == null)
      throw new PXException("The provider is not found.");
    Dictionary<string, string> dictionary1 = new Dictionary<string, string>();
    if (input != null)
    {
      foreach (PXSYParameter pxsyParameter in input)
      {
        if (!string.IsNullOrEmpty(pxsyParameter.Name))
          dictionary1[pxsyParameter.Name] = pxsyParameter.Value;
      }
    }
    Guid? noteId = data.NoteID;
    if (!noteId.HasValue)
      data.NoteID = PXNoteAttribute.GetNoteIDNow(graph.Caches[typeof (SYProvider)], (object) data);
    Dictionary<string, string> dictionary2 = dictionary1;
    noteId = data.NoteID;
    string str = noteId.ToString();
    dictionary2["ProviderNoteID"] = str;
    List<PXSYParameter> pxsyParameterList = new List<PXSYParameter>();
    foreach (PXResult<SYProviderParameter> pxResult in PXSelectBase<SYProviderParameter, PXSelect<SYProviderParameter, Where<SYProviderParameter.providerID, Equal<Required<SYProvider.providerID>>>>.Config>.Select(graph, (object) data.ProviderID))
    {
      SYProviderParameter providerParameter = (SYProviderParameter) pxResult;
      if (dictionary1.ContainsKey(providerParameter.Name))
      {
        pxsyParameterList.Add(new PXSYParameter(providerParameter.Name, dictionary1[providerParameter.Name]));
        dictionary1.Remove(providerParameter.Name);
      }
      else
      {
        if (providerParameter.Value == "<EmptyFileName>")
        {
          string attachedFileName = SyMappingUtils.GetAttachedFileName(graph, data.NoteID);
          if (!string.IsNullOrEmpty(attachedFileName))
            providerParameter.Value = attachedFileName;
        }
        pxsyParameterList.Add(new PXSYParameter(providerParameter.Name, providerParameter.Value));
      }
    }
    foreach (KeyValuePair<string, string> keyValuePair in dictionary1)
      pxsyParameterList.Add(new PXSYParameter(keyValuePair.Key, keyValuePair.Value));
    provider.SetParameters(pxsyParameterList.ToArray());
    if (provider is IPXSYProviderWithSources providerWithSources)
      providerWithSources.SetSources((IEnumerable<SYProviderField>) SyMappingUtils.LoadProviderFields(graph as SYProcess));
    return provider;
  }

  private static string GetAttachedFileName(PXGraph graph, Guid? noteID)
  {
    return ((UploadFile) PXSelectBase<UploadFile, PXSelectJoin<UploadFile, InnerJoin<NoteDoc, On<UploadFile.fileID, Equal<NoteDoc.fileID>>>, Where<NoteDoc.noteID, Equal<Required<SYProvider.noteID>>>, OrderBy<Desc<UploadFile.createdDateTime>>>.Config>.Select(graph, (object) noteID))?.Name;
  }

  internal static void LoadProviderFilters(
    SYProcess graph,
    SYMapping mapping,
    IList<SYProviderField> fields,
    List<PXSYFilterRow> filters)
  {
    PXResultset<SYImportCondition> pxResultset = PXSelectBase<SYImportCondition, PXSelect<SYImportCondition, Where<SYImportCondition.mappingID, Equal<Required<SYMapping.mappingID>>, And<SYImportCondition.isActive, Equal<PX.Data.True>>>>.Config>.Select((PXGraph) graph, (object) mapping.MappingID);
    filters.AddRange(SyMappingUtils.PrepareProviderFilters(fields, pxResultset.FirstTableItems));
  }

  public static IEnumerable<PXSYFilterRow> PrepareProviderFilters(
    IList<SYProviderField> fields,
    IEnumerable<SYImportCondition> conditions)
  {
    foreach (SYImportCondition condition1 in conditions)
    {
      System.Type type1 = (System.Type) null;
      foreach (SYProviderField field in (IEnumerable<SYProviderField>) fields)
      {
        if (field.Name == condition1.FieldName || field.Command != null && field.Command == condition1.FieldName)
        {
          type1 = System.Type.GetType(field.DataType, false);
          break;
        }
      }
      string str1 = RelativeDatesManager.IsRelativeDatesString(condition1.Value) ? RelativeDatesManager.EvaluateAsString(condition1.Value) : condition1.Value;
      string str2 = RelativeDatesManager.IsRelativeDatesString(condition1.Value2) ? RelativeDatesManager.EvaluateAsString(condition1.Value2) : condition1.Value2;
      string fieldName = condition1.FieldName;
      int? condition2 = condition1.Condition;
      int condition3 = (condition2.HasValue ? new int?(condition2.GetValueOrDefault() - 1) : new int?()).Value;
      string str3 = str1;
      string str4 = str2;
      PXSYFilterRow pxsyFilterRow = new PXSYFilterRow(fieldName, (PXCondition) condition3, (object) str3, (object) str4);
      int? nullable = condition1.OpenBrackets;
      pxsyFilterRow.OpenBrackets = nullable.Value;
      nullable = condition1.CloseBrackets;
      pxsyFilterRow.CloseBrackets = nullable.Value;
      nullable = condition1.Operator;
      int num = 1;
      pxsyFilterRow.OrOperator = nullable.GetValueOrDefault() == num & nullable.HasValue;
      System.Type type2 = type1;
      if ((object) type2 == null)
        type2 = typeof (string);
      pxsyFilterRow.DataFieldType = type2;
      yield return pxsyFilterRow;
    }
  }

  private static IList<SYProviderField> LoadProviderFields(SYProcess graph)
  {
    return (IList<SYProviderField>) PXSelectBase<SYProviderField, PXSelect<SYProviderField, Where<SYProviderField.providerID, Equal<Current<SYMapping.providerID>>, And<SYProviderField.objectName, Equal<Current<SYMapping.providerObject>>, And<SYProviderField.isActive, Equal<PX.Data.True>>>>, OrderBy<Asc<SYProviderField.providerID, Asc<SYProviderField.objectName, Asc<SYProviderField.lineNbr>>>>>.Config>.Select((PXGraph) graph).FirstTableItems.AsEnumerable<SYProviderField>().Select<SYProviderField, SYProviderField>((Func<SYProviderField, SYProviderField>) (field => field.Clone())).ToArray<SYProviderField>();
  }

  private static IEnumerable<SYMappingField> LoadScriptCommands(PXGraph graph, SYMapping mapping)
  {
    PXGraph graph1 = graph;
    object[] objArray = new object[1]
    {
      (object) mapping.MappingID
    };
    foreach (PXResult<SYMappingField> pxResult in PXSelectBase<SYMappingField, PXSelect<SYMappingField, Where<SYMappingField.mappingID, Equal<Required<SYMapping.mappingID>>, And<SYMappingField.isActive, Equal<PX.Data.True>>>, OrderBy<Asc<SYMappingField.orderNumber>>>.Config>.Select(graph1, objArray))
      yield return (SYMappingField) pxResult;
  }

  internal static void ProcessMappingInit(SYProcess graph, SYMapping mapping)
  {
    if (mapping != null && !string.IsNullOrEmpty(mapping.FormatLocale))
    {
      Thread.CurrentThread.CurrentCulture = new CultureInfo(mapping.FormatLocale);
      Thread.CurrentThread.CurrentUICulture = new CultureInfo(mapping.FormatLocale);
      PXContext.SetSlot<PXDictionaryManager>((PXDictionaryManager) null);
      PXLocalizer.Localize("Explicit", typeof (InfoMessages).FullName);
    }
    graph.Clear();
    graph.Mappings.Current = (SYMappingActive) mapping;
  }

  internal static PXSYSyncTypes ConvertSyncType(string syncType)
  {
    switch (syncType)
    {
      case "F":
        return PXSYSyncTypes.Full;
      case "A":
        return PXSYSyncTypes.Incremental_AllRecords;
      case "N":
        return PXSYSyncTypes.Incremental_NewOnly;
      default:
        throw new PXException("Incorrect sync type");
    }
  }

  internal static ExecutionBehavior? ParseExecutionBehavior(SYMappingField field)
  {
    switch (field.ExecuteActionBehavior)
    {
      case "E":
        return new ExecutionBehavior?(ExecutionBehavior.ForEachRecord);
      case "F":
        return new ExecutionBehavior?(ExecutionBehavior.FirstRecordOnly);
      case "L":
        return new ExecutionBehavior?(ExecutionBehavior.LastRecordOnly);
      default:
        return new ExecutionBehavior?();
    }
  }

  public static SyCommand ParseCommand(SYMappingField field)
  {
    SyCommand command = new SyCommand();
    command.View = SyMappingUtils.CleanViewName(field.ObjectName);
    command.ViewAlias = field.ObjectName ?? "";
    command.Field = field.FieldName;
    command.Formula = field.Value;
    bool? nullable = field.NeedCommit;
    command.Commit = nullable.GetValueOrDefault();
    nullable = field.IgnoreError;
    command.IgnoreError = nullable.GetValueOrDefault();
    command.UseCurrent = field.UseCurrent;
    command.ExecutionBehavior = SyMappingUtils.ParseExecutionBehavior(field);
    return command;
  }

  public static string CleanViewName(string view)
  {
    view = view ?? "";
    return !view.Contains(":") ? view : StringExtensions.FirstSegment(view, ':').TrimEnd();
  }

  internal static bool IsLastHistoryRecord(SYProcess graph, Guid? mappingID, System.DateTime dateLimit)
  {
    return !PXSelectBase<SYHistory, PXSelect<SYHistory, Where<SYHistory.mappingID, Equal<Required<SYMapping.mappingID>>, And<SYHistory.statusDate, Greater<Required<SYHistory.statusDate>>>>, OrderBy<Desc<SYHistory.statusDate>>>.Config>.SelectSingleBound((PXGraph) graph, (object[]) null, (object) mappingID, (object) dateLimit).FirstTableItems.Any<SYHistory>();
  }

  internal static SYHistory GetLastHistoryRecord(
    SYProcess graph,
    Guid? mappingID,
    System.DateTime dateLimit)
  {
    return PXSelectBase<SYHistory, PXSelect<SYHistory, Where<SYHistory.mappingID, Equal<Required<SYMapping.mappingID>>, And<SYHistory.statusDate, Less<Required<SYHistory.statusDate>>>>, OrderBy<Desc<SYHistory.statusDate>>>.Config>.SelectSingleBound((PXGraph) graph, (object[]) null, (object) mappingID, (object) dateLimit).FirstTableItems.FirstOrDefault<SYHistory>();
  }

  internal static SYHistory GetLastHistoryRecord(
    SYProcess graph,
    Guid? mappingID,
    System.DateTime dateLimit,
    string status)
  {
    return PXSelectBase<SYHistory, PXSelect<SYHistory, Where<SYHistory.mappingID, Equal<Required<SYMapping.mappingID>>, And<SYHistory.statusDate, Less<Required<SYHistory.statusDate>>, And<SYHistory.status, Equal<Required<SYHistory.status>>>>>, OrderBy<Desc<SYHistory.statusDate>>>.Config>.SelectSingleBound((PXGraph) graph, (object[]) null, (object) mappingID, (object) dateLimit, (object) status).FirstTableItems.FirstOrDefault<SYHistory>();
  }

  internal static SYImportOperation GetPrepareAndProcessOperation(Guid? mappingID)
  {
    return new SYImportOperation()
    {
      MappingID = mappingID,
      Operation = "C",
      BreakOnError = new bool?(true),
      BreakOnTarget = new bool?(true),
      Validate = new bool?(false),
      ValidateAndSave = new bool?(false)
    };
  }

  internal delegate System.DateTime? RollbackHistoryHandler(SYProcess graph, SYMapping mapping);

  internal sealed class ProcessMappingScope : IDisposable
  {
    private CultureInfo prev;
    private CultureInfo prevUI;

    public static string OperationType
    {
      get => PXContext.GetSlot<string>("ProcessMappingScope.OperationType");
      private set => PXContext.SetSlot<string>("ProcessMappingScope.OperationType", value);
    }

    public ProcessMappingScope(SYProcess graph, SYMapping mapping)
    {
      this.prev = Thread.CurrentThread.CurrentCulture;
      this.prevUI = Thread.CurrentThread.CurrentUICulture;
      SyMappingUtils.ProcessMappingScope.OperationType = mapping.MappingType;
      SyMappingUtils.ProcessMappingInit(graph, mapping);
    }

    public void Dispose()
    {
      Thread.CurrentThread.CurrentCulture = this.prev;
      Thread.CurrentThread.CurrentUICulture = this.prevUI;
      PXContext.SetSlot<PXDictionaryManager>((PXDictionaryManager) null);
      PXContext.SetSlot("ProcessMappingScope.OperationType", (object) null);
      PXLocalizer.Localize("Explicit", typeof (InfoMessages).FullName);
    }
  }
}
