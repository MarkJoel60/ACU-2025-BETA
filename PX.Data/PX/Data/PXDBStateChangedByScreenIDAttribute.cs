// Decompiled with JetBrains decompiler
// Type: PX.Data.PXDBStateChangedByScreenIDAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Data.Automation;

#nullable disable
namespace PX.Data;

/// <summary>Maps a DAC field to the database column and automatically
/// sets the field value to the string ID of the application screen on
/// which the data record performed last workflow transition.</summary>
/// <remarks>The attribute is added to the value declaration of a DAC field.
/// The field data type should be <tt>string</tt>.</remarks>
/// <example>
/// <code>
/// [PXDBStateChangedByScreenID()]
/// public virtual string StateChangedByScreenID { get; set; }
/// </code>
/// </example>
public class PXDBStateChangedByScreenIDAttribute : 
  PXDBBaseScreenIDAttribute,
  IPXRowInsertingSubscriber,
  IPXRowPersistingSubscriber
{
  private string _workflowProperty;

  [InjectDependencyOnTypeLevel]
  protected IWorkflowService WorkflowService { get; set; }

  [InjectDependencyOnTypeLevel]
  protected IScreenToGraphWorkflowMappingService ScreenToGraphWorkflowMappingService { get; set; }

  protected void WorkflowFieldUpdated(PXCache cache, PXFieldUpdatedEventArgs e)
  {
    object objB = cache.GetValue(e.Row, this._workflowProperty);
    if (object.Equals(e.OldValue, objB))
      return;
    cache.SetValue(e.Row, this._FieldOrdinal, (object) PXContext.GetScreenID()?.Replace(".", ""));
  }

  public override void CacheAttached(PXCache cache)
  {
    base.CacheAttached(cache);
    string workflowScreenId = this.GetWorkflowScreenID(cache);
    if (workflowScreenId == null)
      return;
    this._workflowProperty = this.WorkflowService.GetWorkflowStatePropertyName(workflowScreenId);
    if (this._workflowProperty == null)
      return;
    cache.FieldUpdatedEvents[this._workflowProperty] += new PXFieldUpdated(this.WorkflowFieldUpdated);
  }

  void IPXRowInsertingSubscriber.RowInserting(PXCache cache, PXRowInsertingEventArgs e)
  {
    cache.SetValue(e.Row, this._FieldOrdinal, (object) PXContext.GetScreenID()?.Replace(".", ""));
  }

  protected string GetWorkflowScreenID(PXCache sender)
  {
    return this.ScreenToGraphWorkflowMappingService.GetScreenIDFromGraphType(sender.Graph?.GetType());
  }

  void IPXRowPersistingSubscriber.RowPersisting(PXCache cache, PXRowPersistingEventArgs e)
  {
    bool flag = false;
    if ((e.Operation & PXDBOperation.Insert) != PXDBOperation.Select)
      flag = true;
    else if ((e.Operation & PXDBOperation.Update) != PXDBOperation.Select && this._workflowProperty != null && !object.Equals(cache.GetValueOriginal(e.Row, this._workflowProperty), cache.GetValue(e.Row, this._workflowProperty)))
      flag = true;
    if (!flag)
      return;
    cache.SetValue(e.Row, this._FieldOrdinal, (object) this.GetScreenID(cache));
  }
}
