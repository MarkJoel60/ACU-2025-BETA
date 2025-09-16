// Decompiled with JetBrains decompiler
// Type: PX.Api.SYImportProcess
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.Process.Automation;
using System;
using System.Threading;

#nullable disable
namespace PX.Api;

[PXDisableWorkflow]
public class SYImportProcess : SYProcess
{
  protected virtual bool HideSave => true;

  protected virtual bool RestrictPreparedDataUpdate => true;

  public SYImportProcess()
  {
    this.Mappings = (PXFilteredProcessing<SYMappingActive, SYImportOperation>) new PXFilteredProcessing<SYMappingActive, SYImportOperation, Where2<Where<Current<SYImportOperation.operation>, Equal<ProcessingOperation.prepareAndProcess>, Or<Current<SYImportOperation.operation>, Equal<ProcessingOperation.prepare>, And<SYMapping.status, NotEqual<MappingStatus.partiallyProcessed>, Or<Current<SYImportOperation.operation>, Equal<ProcessingOperation.process>, And<SYMapping.status, NotEqual<MappingStatus.created>, And<SYMapping.status, NotEqual<MappingStatus.processed>, Or<Current<SYImportOperation.operation>, Equal<ProcessingOperation.rollback>, And<SYMapping.status, NotEqual<MappingStatus.created>>>>>>>>>, And<SYMappingActive.mappingType, Equal<SYMapping.mappingType.typeImport>, And<SYMapping.isActive, Equal<True>, And<SYMappingActive.providerType, NotEqual<BPEventProviderType>>>>>>((PXGraph) this, (Delegate) (() => this.mappings()));
    this.Views["Mappings"] = this.Mappings.View;
    SYImportOperation current = this.Operation.Current;
    PXCache cache1 = this.Operation.Cache;
    bool? nullable;
    int num1;
    if (current == null)
    {
      num1 = 0;
    }
    else
    {
      nullable = current.Validate;
      bool flag = true;
      num1 = nullable.GetValueOrDefault() == flag & nullable.HasValue ? 1 : 0;
    }
    PXUIFieldAttribute.SetEnabled<SYImportOperation.validateAndSave>(cache1, (object) null, num1 != 0);
    PXCache cache2 = this.Operation.Cache;
    int num2;
    if (current == null)
    {
      num2 = 1;
    }
    else
    {
      nullable = current.ProcessInParallel;
      bool flag = true;
      num2 = !(nullable.GetValueOrDefault() == flag & nullable.HasValue) ? 1 : 0;
    }
    PXUIFieldAttribute.SetEnabled<SYImportOperation.breakOnError>(cache2, (object) null, num2 != 0);
    PXCache cache3 = this.Operation.Cache;
    int num3;
    if (current == null)
    {
      num3 = 1;
    }
    else
    {
      nullable = current.ProcessInParallel;
      bool flag = true;
      num3 = !(nullable.GetValueOrDefault() == flag & nullable.HasValue) ? 1 : 0;
    }
    PXUIFieldAttribute.SetEnabled<SYImportOperation.breakOnTarget>(cache3, (object) null, num3 != 0);
    this.Mappings.TrySetRefIdGetter((Func<PXCache, object, Guid?>) ((c, d) => ((SYMapping) d).MappingID), "MappingID");
  }

  internal override PXSYTable QueryPreparedData(
    SYMapping mapping,
    SYImportOperation operation,
    CancellationToken token)
  {
    return SyMappingUtils.QueryProviderForPreparedData((SYProcess) this, mapping, operation);
  }

  internal override int ImportPreparedData(
    SYMapping mapping,
    SYImportOperation operation,
    PXSYTable preparedData,
    CancellationToken token)
  {
    return SyMappingUtils.ImportPreparedData((SYProcess) this, mapping, operation, preparedData, SyProviderInstance.Provider is IPXSYProviderWithFieldNamesComparer provider ? provider.FieldNamesComparer : (StringComparer) null, token);
  }

  internal override void RaiseOnImportCompleted(SYMapping mapping, PXSYTableEx data)
  {
    SyMappingUtils.RaiseOnImportCompleted((SYProcess) this, mapping, data);
  }

  protected override void SYImportOperation_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    PXFilteredProcessing<SYMappingActive, SYImportOperation> mappings = this.Mappings;
    this.SYImportOperationRowSelectedHandler<SYMappingActive>(sender, mappings, e);
    string str1;
    if (e.Row is SYImportOperation row)
    {
      bool? breakOnError = row.BreakOnError;
      bool flag = true;
      if (breakOnError.GetValueOrDefault() == flag & breakOnError.HasValue)
      {
        str1 = PXMessages.LocalizeNoPrefix("This option is ignored for import scenarios with the Parallel Processing option enabled");
        goto label_4;
      }
    }
    str1 = "";
label_4:
    string error1 = str1;
    string str2;
    if (row != null)
    {
      bool? breakOnTarget = row.BreakOnTarget;
      bool flag = true;
      if (breakOnTarget.GetValueOrDefault() == flag & breakOnTarget.HasValue)
      {
        str2 = PXMessages.LocalizeNoPrefix("This option is ignored for import scenarios with the Parallel Processing option enabled");
        goto label_8;
      }
    }
    str2 = "";
label_8:
    string error2 = str2;
    PXUIFieldAttribute.SetWarning<SYImportOperation.breakOnError>(sender, (object) row, error1);
    PXUIFieldAttribute.SetWarning<SYImportOperation.breakOnTarget>(sender, (object) row, error2);
  }

  internal void SYImportOperationRowSelectedHandler<T>(
    PXCache sender,
    PXFilteredProcessing<T, SYImportOperation> pxFilteredProcessing,
    PXRowSelectedEventArgs e)
    where T : SYMappingActive, new()
  {
    SYImportOperation operation = this.Operation.Current;
    operation.ProcessInParallel = new bool?();
    pxFilteredProcessing.SetProcessDelegate<SYImportProcess>((Action<SYImportProcess, T, CancellationToken>) ((graph, mapping, cancellationToken) => SYImportProcess.ProcessMappingInt(graph, (SYMappingActive) mapping, operation, cancellationToken)));
    if (this.HideSave)
      this.Save.SetVisible(false);
    if (this.RestrictPreparedDataUpdate)
      this.PreparedData.AllowUpdate = false;
    PXCache cache1 = sender;
    SYImportOperation data = operation;
    bool? validate = operation.Validate;
    bool flag1 = true;
    int num1 = validate.GetValueOrDefault() == flag1 & validate.HasValue ? 1 : 0;
    PXUIFieldAttribute.SetEnabled<SYImportOperation.validateAndSave>(cache1, (object) data, num1 != 0);
    PXCache cache2 = sender;
    SYImportOperation syImportOperation1 = operation;
    int num2;
    if (syImportOperation1 == null)
    {
      num2 = 1;
    }
    else
    {
      bool? processInParallel = syImportOperation1.ProcessInParallel;
      bool flag2 = true;
      num2 = !(processInParallel.GetValueOrDefault() == flag2 & processInParallel.HasValue) ? 1 : 0;
    }
    PXUIFieldAttribute.SetEnabled<SYImportOperation.breakOnError>(cache2, (object) null, num2 != 0);
    PXCache cache3 = sender;
    SYImportOperation syImportOperation2 = operation;
    int num3;
    if (syImportOperation2 == null)
    {
      num3 = 1;
    }
    else
    {
      bool? processInParallel = syImportOperation2.ProcessInParallel;
      bool flag3 = true;
      num3 = !(processInParallel.GetValueOrDefault() == flag3 & processInParallel.HasValue) ? 1 : 0;
    }
    PXUIFieldAttribute.SetEnabled<SYImportOperation.breakOnTarget>(cache3, (object) null, num3 != 0);
  }

  private static void ProcessMappingInt(
    SYImportProcess graph,
    SYMappingActive mapping,
    SYImportOperation operation,
    CancellationToken token)
  {
    SYImportOperation operation1 = operation.Clone();
    bool? processInParallel = mapping.ProcessInParallel;
    bool flag = true;
    if (processInParallel.GetValueOrDefault() == flag & processInParallel.HasValue)
    {
      operation1.ProcessInParallel = mapping.ProcessInParallel;
      operation1.BreakOnError = new bool?(false);
      operation1.BreakOnTarget = new bool?(false);
    }
    SYProcess.ProcessMapping((SYProcess) graph, mapping, operation1, token);
  }

  protected void _(
    Events.FieldUpdated<SYImportOperation.processInParallel> e)
  {
    if (!(e.Row is SYImportOperation row))
      return;
    bool valueOrDefault = row.ProcessInParallel.GetValueOrDefault();
    if (valueOrDefault)
    {
      e.Cache.SetValue<SYImportOperation.breakOnError>((object) row, (object) false);
      e.Cache.SetValue<SYImportOperation.breakOnTarget>((object) row, (object) false);
    }
    PXUIFieldAttribute.SetEnabled<SYImportOperation.breakOnError>(e.Cache, e.Row, !valueOrDefault);
    PXUIFieldAttribute.SetEnabled<SYImportOperation.breakOnTarget>(e.Cache, e.Row, !valueOrDefault);
  }

  protected void SYImportOperation_Validate_FieldUpdating(PXCache cache, PXFieldUpdatingEventArgs e)
  {
    if (e.NewValue == null || Convert.ToBoolean(e.NewValue))
      return;
    ((SYImportOperation) e.Row).ValidateAndSave = new bool?(false);
  }

  protected void SYImportOperation_Validate_FieldUpdated(PXCache cache, PXFieldUpdatedEventArgs e)
  {
    PXCache cache1 = cache;
    object row = e.Row;
    bool? validate = ((SYImportOperation) e.Row).Validate;
    bool flag = true;
    int num = validate.GetValueOrDefault() == flag & validate.HasValue ? 1 : 0;
    PXUIFieldAttribute.SetEnabled<SYImportOperation.validateAndSave>(cache1, row, num != 0);
  }

  public static void RunScenario(string scenario, params PXSYParameter[] parameters)
  {
    SYImportProcess.RunScenario(scenario, CancellationToken.None, parameters);
  }

  public static void RunScenario(
    string scenario,
    CancellationToken token,
    params PXSYParameter[] parameters)
  {
    SYImportProcess instance = PXGraph.CreateInstance<SYImportProcess>();
    instance.Parameters = parameters;
    SYMappingActive mapping = (SYMappingActive) PXSelectBase<SYMappingActive, PXSelect<SYMappingActive, Where<SYMappingActive.mappingType, Equal<SYMapping.mappingType.typeImport>, And<SYMappingActive.name, Equal<Required<SYMappingActive.name>>>>>.Config>.Select((PXGraph) instance, (object) scenario);
    SYImportOperation operation = mapping != null ? new SYImportOperation()
    {
      MappingID = mapping.MappingID,
      Operation = "C",
      BreakOnError = new bool?(true),
      BreakOnTarget = new bool?(true),
      Validate = new bool?(false),
      ValidateAndSave = new bool?(false)
    } : throw new PXArgumentException(nameof (scenario), "An invalid argument has been specified.");
    SyMappingUtils.ProcessMapping((SYProcess) instance, mapping, operation, token);
  }
}
