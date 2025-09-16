// Decompiled with JetBrains decompiler
// Type: PX.Data.EditPreventor`1
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

#nullable disable
namespace PX.Data;

public abstract class EditPreventor<TFields> where TFields : ITypeArrayOf<IBqlField>, TypeArray.IsNotEmpty
{
  public abstract class On<TGraph> : PXGraphExtension<TGraph>, ISuppressiblePreventer, IEditPreventer
    where TGraph : PXGraph
  {
    private readonly Dictionary<System.Type, Stack<RuleWeakeningScope>> _scopes = new Dictionary<System.Type, Stack<RuleWeakeningScope>>();

    protected On()
    {
      foreach (System.Type key in TypeArray.CheckAndExtract<IBqlField>(typeof (TFields), (string) null))
      {
        Stack<RuleWeakeningScope> ruleWeakeningScopeStack = new Stack<RuleWeakeningScope>();
        ruleWeakeningScopeStack.Push(RuleWeakeningScope.Dummy);
        this._scopes.Add(key, ruleWeakeningScopeStack);
      }
    }

    public override void Initialize()
    {
      base.Initialize();
      foreach (System.Type field1 in this.Fields)
      {
        System.Type field = field1;
        this.Base.FieldVerifying.AddHandler(BqlCommand.GetItemType(field), field.Name, (PXFieldVerifying) ((c, e) => this.Handler(c, e, field)));
      }
    }

    private void Handler(PXCache cache, PXFieldVerifyingEventArgs e, System.Type field)
    {
      if (e.Cancel || this.IgnoreSameNewValue && object.Equals(e.NewValue, cache.GetValue(e.Row, field.Name)) || this.AllowEditInsertedRecords && (cache.GetStatus(e.Row) == PXEntryStatus.Inserted || cache.Locate(e.Row) == null))
        return;
      string preventingReason = this.GetEditPreventingReason(new GetEditPreventingReasonArgs(cache, field, e.Row, e.NewValue));
      if (string.IsNullOrEmpty(preventingReason))
        return;
      this.SetError(cache, e, field, preventingReason);
    }

    protected virtual void SetError(
      PXCache cache,
      PXFieldVerifyingEventArgs e,
      System.Type field,
      string editPreventingReason)
    {
      PXSetPropertyException error = new PXSetPropertyException(editPreventingReason, this.ErrorLevel);
      if (this.GetRuleWeakenLevelFor(field) != RuleWeakenLevel.SuppressError)
        throw error;
      this._scopes[field].Peek().RegisterSuppressedError(field, error);
      e.NewValue = cache.GetValue(e.Row, field.Name);
      e.Cancel = true;
    }

    public virtual PXErrorLevel ErrorLevel => PXErrorLevel.Warning;

    public IEnumerable<System.Type> Fields => (IEnumerable<System.Type>) this._scopes.Keys;

    public string GetEditPreventingReason(GetEditPreventingReasonArgs arg)
    {
      this.OnPreventEdit(arg);
      if (!arg.Cancel)
        arg.Cancel = this.GetRuleWeakenLevelFor(arg.Field) == RuleWeakenLevel.AllowEdit;
      return arg.Cancel ? (string) null : this.GetEditPreventingReasonImpl(arg);
    }

    protected virtual void OnPreventEdit(GetEditPreventingReasonArgs args)
    {
    }

    private RuleWeakenLevel GetRuleWeakenLevelFor(System.Type field)
    {
      return this._scopes[field].Peek().GetRuleWeakenLevelForField(field);
    }

    protected abstract string GetEditPreventingReasonImpl(GetEditPreventingReasonArgs arg);

    public virtual bool AllowEditInsertedRecords => true;

    public virtual bool IgnoreSameNewValue => true;

    public void PushWeakeningScope(System.Type field, RuleWeakeningScope scope)
    {
      this._scopes[field].Push(scope);
    }

    public void PopWeakeningScope(System.Type field) => this._scopes[field].Pop();

    public abstract class IfExists<TSelect> : EditPreventor<TFields>.On<TGraph> where TSelect : BqlCommand, new()
    {
      protected virtual bool PreventOnRowPersisting { get; }

      protected virtual bool PreventOnRowDeleting { get; }

      public override void Initialize()
      {
        base.Initialize();
        if (this.PreventOnRowPersisting)
          this.Base.RowPersisting.AddHandler(BqlCommand.GetItemType(this.Fields.First<System.Type>()), (PXRowPersisting) ((c, e) => this.RowEventHandler(c, (CancelEventArgs) e, e.Row, this.Fields)));
        if (!this.PreventOnRowDeleting)
          return;
        this.Base.RowDeleting.AddHandler(BqlCommand.GetItemType(this.Fields.First<System.Type>()), (PXRowDeleting) ((c, e) => this.RowEventHandler(c, (CancelEventArgs) e, e.Row, this.Fields, true)));
      }

      protected virtual void SetRowError(
        PXCache cache,
        object row,
        System.Type field,
        bool isRowDeletion,
        string editPreventingReason)
      {
        PXSetPropertyException error = new PXSetPropertyException(editPreventingReason, PXErrorLevel.RowError);
        if (this.GetRuleWeakenLevelFor(field) == RuleWeakenLevel.SuppressError)
        {
          this._scopes[field].Peek().RegisterSuppressedError(field, error);
        }
        else
        {
          if (isRowDeletion)
            throw new PXException(editPreventingReason);
          if (cache.RaiseExceptionHandling(field.Name, row, (object) null, (Exception) error))
            throw new PXRowPersistingException(field.Name, (object) null, editPreventingReason);
        }
      }

      private void RowEventHandler(
        PXCache cache,
        CancelEventArgs e,
        object row,
        IEnumerable<System.Type> fields,
        bool isRowDeletingEvent = false)
      {
        if (e.Cancel)
          return;
        int status = (int) cache.GetStatus(row);
        bool flag = EnumerableExtensions.IsIn<PXEntryStatus>((PXEntryStatus) status, PXEntryStatus.Deleted, PXEntryStatus.InsertedDeleted) | isRowDeletingEvent;
        if (EnumerableExtensions.IsIn<PXEntryStatus>((PXEntryStatus) status, PXEntryStatus.Inserted, PXEntryStatus.InsertedDeleted) || cache.Locate(row) == null || flag && !this.PreventOnRowDeleting)
          return;
        object original = cache.GetOriginal(row);
        foreach (System.Type field in fields)
        {
          object obj = original != null ? cache.GetValue(row, field.Name) : (object) null;
          if (flag || !object.Equals(obj, cache.GetValue(original, field.Name)))
          {
            this.OnPreventEdit(new GetEditPreventingReasonArgs(cache, field, original, obj, flag));
            if (!e.Cancel)
            {
              string preventingReasonImpl = this.GetEditPreventingReasonImpl(new GetEditPreventingReasonArgs(cache, field, original, obj, flag));
              if (!string.IsNullOrEmpty(preventingReasonImpl))
              {
                this.SetRowError(cache, row, field, flag, preventingReasonImpl);
                break;
              }
            }
          }
        }
      }

      protected virtual string CreateEditPreventingReason(
        GetEditPreventingReasonArgs arg,
        object firstPreventingEntity,
        string fieldName,
        string currentTableName,
        string foreignTableName)
      {
        return !arg.IsRowDeleting ? PXMessages.LocalizeFormat("The {0} value cannot be adjusted because the current {1} is referenced in the {2} table.", (object) fieldName, (object) currentTableName.ToLower(), (object) foreignTableName) : PXMessages.LocalizeFormat("The {0} record cannot be deleted because it is referenced in the {1} table.", (object) currentTableName.ToLower(), (object) foreignTableName);
      }

      protected sealed override string GetEditPreventingReasonImpl(GetEditPreventingReasonArgs arg)
      {
        BqlCommand select = (BqlCommand) new TSelect();
        object firstPreventingEntity = new PXView(arg.Graph, true, select).SelectSingleBound(new object[1]
        {
          arg.Row
        });
        return firstPreventingEntity == null ? (string) null : this.CreateEditPreventingReason(arg, firstPreventingEntity, arg.IsRowDeleting ? (string) null : PXUIFieldAttribute.GetDisplayName(arg.Cache, arg.Field.Name), EntityHelper.GetFriendlyEntityName(arg.Cache.GetItemType()), EntityHelper.GetFriendlyEntityName(select.GetFirstTable()));
      }
    }
  }
}
