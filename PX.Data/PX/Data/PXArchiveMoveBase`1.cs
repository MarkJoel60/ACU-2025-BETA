// Decompiled with JetBrains decompiler
// Type: PX.Data.PXArchiveMoveBase`1
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Data.Archiving;
using PX.Data.SQLTree;
using PX.Security.Authorization;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Data;

/// <exclude />
public abstract class PXArchiveMoveBase<TNode> : PXAction<TNode> where TNode : class, IBqlTable, new()
{
  [InjectDependency]
  private IAUArchivingRuleEngine ArchivingRuleEngine { get; set; }

  [InjectDependency]
  private ISystemRolesProvider RolesProvider { get; set; }

  protected abstract bool MoveToArchive { get; }

  /// <inheritdoc />
  protected PXArchiveMoveBase(PXGraph graph)
    : base(graph)
  {
  }

  /// <inheritdoc />
  public PXArchiveMoveBase(PXGraph graph, string name)
    : base(graph, name)
  {
  }

  /// <inheritdoc />
  public PXArchiveMoveBase(PXGraph graph, Delegate handler)
    : base(graph, handler)
  {
  }

  public override object GetState(object row)
  {
    object state = base.GetState(row);
    if (!(state is PXButtonState pxButtonState))
      return state;
    if (!PXContext.PXIdentity.User.IsInRole(this.RolesProvider.GetArchivistRole()) && !this.RolesProvider.GetAdministratorRoles().Any<string>(new Func<string, bool>(PXContext.PXIdentity.User.IsInRole)))
    {
      pxButtonState.Enabled = false;
      pxButtonState.Visible = false;
    }
    if (string.IsNullOrEmpty(pxButtonState.ConfirmationMessage))
      return state;
    if (typeof (TNode).IsDefined(typeof (PXCacheNameAttribute), true))
    {
      PXCacheNameAttribute customAttribute = (PXCacheNameAttribute) typeof (TNode).GetCustomAttributes(typeof (PXCacheNameAttribute), true)[0];
      pxButtonState.ConfirmationMessage = string.Format(pxButtonState.ConfirmationMessage, (object) customAttribute.GetName());
      return state;
    }
    pxButtonState.ConfirmationMessage = string.Format(pxButtonState.ConfirmationMessage, (object) typeof (TNode).Name);
    return state;
  }

  protected override IEnumerable Handler(PXAdapter adapter)
  {
    try
    {
      using (PXTransactionScope transactionScope = new PXTransactionScope())
      {
        IBqlTable current = (IBqlTable) adapter.View.Cache.Current;
        foreach (KeyValuePair<System.Type, Func<IEnumerable<IBqlTable>>> recordsExtractor in (IEnumerable<KeyValuePair<System.Type, Func<IEnumerable<IBqlTable>>>>) this.ArchivingRuleEngine.GetRecordsExtractors(this.Graph, current, this.MoveToArchive))
        {
          foreach (IBqlTable row in recordsExtractor.Value())
            this.MoveEntity(recordsExtractor.Key, (object) row);
        }
        this.MoveEntity(adapter.View.Cache.BqlTable, (object) current);
        transactionScope.Complete(this._Graph);
      }
      using (new PXReadThroughArchivedScope())
        return PXCancel<TNode>.PerformCancel(adapter);
    }
    catch
    {
      this._Graph.Clear();
      throw;
    }
  }

  private void MoveEntity(System.Type table, object row)
  {
    List<PXDataFieldRestrict> entityKeys = this.GetEntityKeys(table, row);
    if (this.MoveToArchive)
      this._Graph.ProviderArchive(table, entityKeys.ToArray());
    else
      this._Graph.ProviderExtract(table, entityKeys.ToArray());
  }

  private List<PXDataFieldRestrict> GetEntityKeys(System.Type table, object row)
  {
    List<PXDataFieldRestrict> entityKeys = new List<PXDataFieldRestrict>();
    PXCache cach = this.Graph.Caches[table];
    foreach (string key in (IEnumerable<string>) cach.Keys)
    {
      object obj = cach.GetValue(row, key);
      PXCommandPreparingEventArgs.FieldDescription description;
      cach.RaiseCommandPreparing(key, row, obj, PXDBOperation.Delete, cach.BqlTable, out description);
      if (description?.Expr == null)
        cach.RaiseCommandPreparing(key, row, obj, PXDBOperation.Select, cach.BqlTable, out description);
      if (description?.Expr != null)
        entityKeys.Add(new PXDataFieldRestrict((Column) description.Expr, description.DataType, description.DataLength, description.DataValue));
    }
    return entityKeys;
  }
}
