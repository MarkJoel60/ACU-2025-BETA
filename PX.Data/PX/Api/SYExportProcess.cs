// Decompiled with JetBrains decompiler
// Type: PX.Api.SYExportProcess
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using System;
using System.Threading;

#nullable disable
namespace PX.Api;

public class SYExportProcess : SYProcess
{
  public const string ExportRefreshingValues = "ExportRefreshingValues";

  protected virtual bool HideSave => true;

  protected virtual bool RestrictPreparedDataUpdate => true;

  public SYExportProcess()
  {
    this.Mappings = (PXFilteredProcessing<SYMappingActive, SYImportOperation>) new PXFilteredProcessing<SYMappingActive, SYImportOperation, Where2<Where<Current<SYImportOperation.operation>, Equal<ProcessingOperation.prepareAndProcess>, Or<Current<SYImportOperation.operation>, Equal<ProcessingOperation.prepare>, And<SYMapping.status, NotEqual<MappingStatus.partiallyProcessed>, Or<Current<SYImportOperation.operation>, Equal<ProcessingOperation.process>, And<SYMapping.status, NotEqual<MappingStatus.created>, And<SYMapping.status, NotEqual<MappingStatus.processed>, Or<Current<SYImportOperation.operation>, Equal<ProcessingOperation.rollback>, And<SYMapping.status, NotEqual<MappingStatus.created>>>>>>>>>, And<SYMappingActive.mappingType, Equal<SYMapping.mappingType.typeExport>, And<SYMapping.isActive, Equal<True>>>>>((PXGraph) this, (Delegate) (() => this.mappings()));
    this.Views["Mappings"] = this.Mappings.View;
    this.Mappings.TrySetRefIdGetter((Func<PXCache, object, Guid?>) ((c, d) => ((SYMapping) d).MappingID), "MappingID");
    PXStringListAttribute.SetList<SYImportOperation.operation>(this.Caches[typeof (SYImportOperation)], (object) null, ProcessingOperation.Values, ProcessingOperation.LabelsExport);
  }

  protected override void SYImportOperation_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    SYImportOperation operation = this.Operation.Current;
    this.Mappings.SetProcessDelegate<SYExportProcess>((Action<SYExportProcess, SYMappingActive, CancellationToken>) ((graph, mapping, cancellationToken) => SYProcess.ProcessMapping((SYProcess) graph, mapping, operation, cancellationToken)));
    if (this.HideSave)
      this.Save.SetVisible(false);
    if (!this.RestrictPreparedDataUpdate)
      return;
    this.PreparedData.AllowUpdate = false;
  }

  internal override PXSYTable QueryPreparedData(
    SYMapping mapping,
    SYImportOperation operation,
    CancellationToken token)
  {
    return SyMappingUtils.ExportPreparedData((SYProcess) this, mapping, ((int) operation.BreakOnError ?? 1) != 0, token);
  }

  internal override int ImportPreparedData(
    SYMapping mapping,
    SYImportOperation operation,
    PXSYTable preparedData,
    CancellationToken token)
  {
    return SyMappingUtils.WritePreparedDataToProvider((SYProcess) this, mapping, operation, preparedData);
  }

  public static void RunScenario(string scenario, params PXSYParameter[] parameters)
  {
    SYExportProcess.RunScenario(scenario, CancellationToken.None, parameters);
  }

  public static void RunScenario(
    string scenario,
    CancellationToken token,
    params PXSYParameter[] parameters)
  {
    SYExportProcess.RunScenario(scenario, SYMapping.RepeatingOption.Primary, true, token, parameters);
  }

  public static void RunScenario(
    string scenario,
    SYMapping.RepeatingOption repeatingData,
    bool breakOnError,
    params PXSYParameter[] parameters)
  {
    SYExportProcess.RunScenario(scenario, repeatingData, breakOnError, CancellationToken.None, parameters);
  }

  public static void RunScenario(
    string scenario,
    SYMapping.RepeatingOption repeatingData,
    bool breakOnError,
    CancellationToken token,
    params PXSYParameter[] parameters)
  {
    SYExportProcess.RunScenario(scenario, repeatingData, breakOnError, false, token, parameters);
  }

  public static void RunScenario(
    string scenario,
    SYMapping.RepeatingOption repeatingData,
    bool breakOnError,
    bool refreshPrepare,
    params PXSYParameter[] parameters)
  {
    SYExportProcess.RunScenario(scenario, repeatingData, breakOnError, refreshPrepare, CancellationToken.None, parameters);
  }

  public static void RunScenario(
    string scenario,
    SYMapping.RepeatingOption repeatingData,
    bool breakOnError,
    bool refreshPrepare,
    CancellationToken token,
    params PXSYParameter[] parameters)
  {
    SYExportProcess instance = PXGraph.CreateInstance<SYExportProcess>();
    instance.Parameters = parameters;
    SYMappingActive mapping = (SYMappingActive) PXSelectBase<SYMappingActive, PXSelect<SYMappingActive, Where<SYMappingActive.mappingType, Equal<SYMapping.mappingType.typeExport>, And<SYMappingActive.name, Equal<Required<SYMappingActive.name>>>>>.Config>.Select((PXGraph) instance, (object) scenario);
    if (mapping == null)
      throw new PXArgumentException(nameof (scenario), "An invalid argument has been specified.");
    if (refreshPrepare)
      mapping.Status = "N";
    SYImportOperation operation = new SYImportOperation()
    {
      MappingID = mapping.MappingID,
      Operation = "C",
      BreakOnError = new bool?(breakOnError),
      BreakOnTarget = new bool?(true),
      Validate = new bool?(false),
      ValidateAndSave = new bool?(false)
    };
    mapping.RepeatingData = new byte?((byte) repeatingData);
    SyMappingUtils.ProcessMapping((SYProcess) instance, mapping, operation, token);
  }
}
