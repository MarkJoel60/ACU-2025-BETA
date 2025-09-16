// Decompiled with JetBrains decompiler
// Type: PX.Data.PXDBStateChangedByIDAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.Automation;
using PX.Data.BQL;
using PX.SM;
using System;

#nullable enable
namespace PX.Data;

/// <summary>Maps a DAC field to the database column and automatically
/// sets the field value to the ID of the user who made the last workflow transition
/// for the data record.</summary>
/// <remarks>The attribute is added to the value declaration of a DAC field.
/// The field data type should be <tt>Guid?</tt>.</remarks>
/// <example>
/// <code>
/// [PXDBStateChangedByID()]
/// [PXUIField(DisplayName = "State Changed By")]
/// public virtual Guid? StateChangedByID { get; set; }
/// </code>
/// </example>
[Serializable]
public class PXDBStateChangedByIDAttribute : 
  PXDBBaseIDAttribute,
  IPXRowInsertingSubscriber,
  IPXRowPersistingSubscriber
{
  private 
  #nullable disable
  string _workflowProperty;

  /// <summary>Initializes a new instance with default parameters.</summary>
  public PXDBStateChangedByIDAttribute()
    : base(typeof (PXDBStateChangedByIDAttribute.StateChanger.pKID), typeof (PXDBStateChangedByIDAttribute.StateChanger.username), typeof (PXDBStateChangedByIDAttribute.StateChanger.displayName), typeof (PXDBStateChangedByIDAttribute.StateChanger.pKID), typeof (PXDBStateChangedByIDAttribute.StateChanger.username))
  {
    this.AddUIFieldAttributeIfNeeded("Workflow State Changed By");
  }

  [InjectDependencyOnTypeLevel]
  protected IWorkflowService WorkflowService { get; set; }

  [InjectDependencyOnTypeLevel]
  protected IScreenToGraphWorkflowMappingService ScreenToGraphWorkflowMappingService { get; set; }

  protected void WorkflowFieldUpdated(PXCache cache, PXFieldUpdatedEventArgs e)
  {
    object objB = cache.GetValue(e.Row, this._workflowProperty);
    if (object.Equals(e.OldValue, objB))
      return;
    cache.SetValue(e.Row, this._FieldOrdinal, (object) this.GetUserID(cache));
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

  protected string GetWorkflowScreenID(PXCache sender)
  {
    return this.ScreenToGraphWorkflowMappingService.GetScreenIDFromGraphType(sender.Graph?.GetType());
  }

  void IPXRowInsertingSubscriber.RowInserting(PXCache cache, PXRowInsertingEventArgs e)
  {
    cache.SetValue(e.Row, this._FieldOrdinal, (object) this.GetUserID(cache));
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
    cache.SetValue(e.Row, this._FieldOrdinal, (object) this.GetUserID(cache));
  }

  /// <summary>Is used internally to represent the user who changed workflow state for the data record.</summary>
  [PXBreakInheritance]
  [Serializable]
  public sealed class StateChanger : Users
  {
    [PXDBString(256 /*0x0100*/, IsKey = true, IsUnicode = true, InputMask = "")]
    [PXUIField(DisplayName = "Workflow State Changed By", Enabled = false, Visibility = PXUIVisibility.SelectorVisible)]
    public override string Username { get; set; }

    [PXString]
    [PXUIField(DisplayName = "Workflow State Changed By", Enabled = false, Visibility = PXUIVisibility.SelectorVisible)]
    [PXFormula(typeof (IsNull<IsNull<SmartJoin<Space, Users.firstName, Users.lastName>, PXDBStateChangedByIDAttribute.StateChanger.username>, Empty>))]
    public override string DisplayName { get; set; }

    /// <exclude />
    public new abstract class pKID : 
      BqlType<
      #nullable enable
      IBqlGuid, Guid>.Field<
      #nullable disable
      PXDBStateChangedByIDAttribute.StateChanger.pKID>
    {
    }

    /// <exclude />
    public new abstract class username : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      PXDBStateChangedByIDAttribute.StateChanger.username>
    {
    }

    /// <exclude />
    public new abstract class displayName : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      PXDBStateChangedByIDAttribute.StateChanger.displayName>
    {
    }
  }
}
