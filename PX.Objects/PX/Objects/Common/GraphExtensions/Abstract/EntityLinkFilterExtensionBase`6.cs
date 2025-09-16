// Decompiled with JetBrains decompiler
// Type: PX.Objects.Common.GraphExtensions.Abstract.EntityLinkFilterExtensionBase`6
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace PX.Objects.Common.GraphExtensions.Abstract;

public abstract class EntityLinkFilterExtensionBase<TGraph, TGraphFilter, TGraphFilterEntityID, TEntity, TEntityID, TDataType> : 
  PXGraphExtension<
  #nullable disable
  TGraph>
  where TGraph : PXGraph, PXImportAttribute.IPXPrepareItems, PXImportAttribute.IPXProcess
  where TGraphFilter : class, IBqlTable, new()
  where TGraphFilterEntityID : class, IBqlField
  where TEntity : class, IBqlTable, new()
  where TEntityID : class, IBqlField
{
  private int excelRowNumber = 2;
  private bool importHasError;

  protected abstract string EntityViewName { get; }

  protected abstract void _(Events.CacheAttached<TEntityID> e);

  protected virtual void _(Events.RowInserted<TEntity> e)
  {
    PXCache<TGraphFilter> pxCache = GraphHelper.Caches<TGraphFilter>(((Events.Event<PXRowInsertedEventArgs, Events.RowInserted<TEntity>>) e).Cache.Graph);
    ((PXCache) pxCache).SetValueExt<TGraphFilterEntityID>(((PXCache) pxCache).Current, (object) null);
  }

  protected virtual void _(Events.RowUpdated<TEntity> e)
  {
    PXCache<TGraphFilter> pxCache = GraphHelper.Caches<TGraphFilter>(((Events.Event<PXRowUpdatedEventArgs, Events.RowUpdated<TEntity>>) e).Cache.Graph);
    ((PXCache) pxCache).SetValueExt<TGraphFilterEntityID>(((PXCache) pxCache).Current, (object) null);
  }

  protected virtual void _(
    Events.FieldSelecting<TGraphFilter, TGraphFilterEntityID> e)
  {
    if (!this.GetEntities().Any<TEntity>())
      return;
    ((Events.FieldSelectingBase<Events.FieldSelecting<TGraphFilter, TGraphFilterEntityID>>) e).ReturnValue = (object) PXMessages.LocalizeNoPrefix("<LIST>");
  }

  protected virtual void _(
    Events.FieldUpdated<TGraphFilter, TGraphFilterEntityID> e)
  {
    if (((Events.Event<PXFieldUpdatedEventArgs, Events.FieldUpdated<TGraphFilter, TGraphFilterEntityID>>) e).Cache.GetValue<TGraphFilterEntityID>((object) e.Row) == null)
      return;
    ((PXCache) GraphHelper.Caches<TEntity>((PXGraph) this.Base)).Clear();
  }

  /// Overrides <see cref="M:PX.Data.PXImportAttribute.IPXPrepareItems.PrepareImportRow(System.String,System.Collections.IDictionary,System.Collections.IDictionary)" />
  [PXOverride]
  public bool PrepareImportRow(
    string viewName,
    IDictionary keys,
    IDictionary values,
    Func<string, IDictionary, IDictionary, bool> base_PrepareImportRow)
  {
    bool flag = base_PrepareImportRow(viewName, keys, values);
    try
    {
      if (viewName.Equals(this.EntityViewName, StringComparison.InvariantCultureIgnoreCase))
        this.ImportEntity(values);
    }
    catch (Exception ex)
    {
      PXTrace.WriteError("Row number {0}. Error message \"{1}\"", new object[2]
      {
        (object) this.excelRowNumber,
        (object) ex.Message
      });
      this.importHasError = true;
    }
    finally
    {
      ++this.excelRowNumber;
    }
    return !(viewName == this.EntityViewName) && flag;
  }

  protected virtual void ImportEntity(IDictionary values)
  {
    PXCache pxCache = (PXCache) GraphHelper.Caches<TEntity>((PXGraph) this.Base);
    TEntity instance = (TEntity) pxCache.CreateInstance();
    if (!values.Contains((object) typeof (TEntityID).Name))
      return;
    try
    {
      object obj = values[(object) typeof (TEntityID).Name];
      pxCache.SetValueExt<TEntityID>((object) instance, obj);
      pxCache.Update((object) instance);
    }
    catch (Exception ex)
    {
      PXTrace.WriteError("Row number {0}. Error message \"{1}\"", new object[2]
      {
        (object) this.excelRowNumber,
        (object) ex.Message
      });
    }
  }

  [PXOverride]
  public void ImportDone(
    PXImportAttribute.ImportMode.Value mode,
    Action<PXImportAttribute.ImportMode.Value> base_ImportDone)
  {
    base_ImportDone(mode);
    this.excelRowNumber = 0;
    if (this.importHasError)
    {
      this.importHasError = false;
      throw new Exception("Import has some error. The list of incorrect records is recorded in the Trace.");
    }
  }

  protected virtual IEnumerable<TEntity> GetEntities()
  {
    PXCache<TEntity> entityCache = GraphHelper.Caches<TEntity>((PXGraph) this.Base);
    return (IEnumerable<TEntity>) ((PXCache) entityCache).Cached.Cast<TEntity>().Where<TEntity>((Func<TEntity, bool>) (t => EnumerableExtensions.IsNotIn<PXEntryStatus>(entityCache.GetStatus(t), (PXEntryStatus) 3, (PXEntryStatus) 4))).ToArray<TEntity>();
  }

  protected virtual IEnumerable<TDataType> GetSelectedEntities(TGraphFilter filter)
  {
    PXCache<TEntity> entityCache = GraphHelper.Caches<TEntity>((PXGraph) this.Base);
    TDataType dataType = (TDataType) ((PXCache) GraphHelper.Caches<TGraphFilter>((PXGraph) this.Base)).GetValue<TGraphFilterEntityID>((object) filter);
    if ((object) dataType == null)
      return (IEnumerable<TDataType>) this.GetEntities().Select<TEntity, TDataType>((Func<TEntity, TDataType>) (e => (TDataType) ((PXCache) entityCache).GetValue<TEntityID>((object) e))).ToArray<TDataType>();
    return (IEnumerable<TDataType>) new TDataType[1]
    {
      dataType
    };
  }
}
