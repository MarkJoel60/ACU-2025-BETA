// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.Extensions.SideBySideComparison.Link.LinkEntitiesExt_EventBased`5
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CR.Wizard;
using System;

#nullable disable
namespace PX.Objects.CR.Extensions.SideBySideComparison.Link;

/// <summary>
/// The event-based version of <see cref="T:PX.Objects.CR.Extensions.SideBySideComparison.Link.LinkEntitiesExt`3" />, which is an extension
/// that provides ability to link two sets of entities after performing comparision of their fields
/// and selecting values from left or right entity sets.
/// </summary>
/// <remarks>
/// Triggers opening of smart panel for linking of entities when <see cref="!:TUpdatingField" /> is updated in <see cref="!:TUpdatingEntity" />
/// and additional conditions are met.
/// </remarks>
/// <typeparam name="TGraph">The entry <see cref="T:PX.Data.PXGraph" /> type.</typeparam>
/// <typeparam name="TMain">The primary DAC (a <see cref="T:PX.Data.IBqlTable" /> type) of the <typeparamref name="TGraph">graph</typeparamref>.</typeparam>
/// <typeparam name="TFilter">The type of <see cref="T:PX.Objects.CR.Extensions.SideBySideComparison.Link.LinkFilter" /> that is used by the current extension.</typeparam>
public abstract class LinkEntitiesExt_EventBased<TGraph, TMain, TFilter, TUpdatingEntity, TUpdatingField> : 
  LinkEntitiesExt<TGraph, TMain, TFilter>
  where TGraph : PXGraph, new()
  where TMain : class, IBqlTable, new()
  where TFilter : LinkFilter, new()
  where TUpdatingEntity : class, IBqlTable, INotable, new()
  where TUpdatingField : class, IBqlField
{
  public virtual PXCache UpdatingEntityCache => this.Base.Caches[typeof (TUpdatingEntity)];

  public virtual TUpdatingEntity UpdatingEntityCurrent
  {
    get => this.UpdatingEntityCache.Current as TUpdatingEntity;
  }

  public virtual string FieldName => this.UpdatingEntityCache.GetField(typeof (TUpdatingField));

  public override void UpdateMainAfterProcess()
  {
    this.UpdatingEntityCache.SetValue<TUpdatingField>((object) this.UpdatingEntityCurrent, this.UpdatingEntityCache.ValueFromString(this.FieldName, ((PXSelectBase<TFilter>) this.Filter).Current.LinkedEntityID));
    this.UpdatingEntityCache.Update((object) this.UpdatingEntityCurrent);
  }

  protected virtual bool ShouldProcess(PXCache cache, TUpdatingEntity row, TUpdatingEntity oldRow)
  {
    bool flag = this.Base.IsImport || this.Base.IsMobile || this.Base.IsContractBasedAPI;
    if (flag && this.ValueChanged(cache, row, oldRow))
      return true;
    if (flag)
      return false;
    return this.SelectEntityForLink?.View?.Answer.GetValueOrDefault() != null || ((PXSelectBase) this.Filter).View.Answer != null || this.ValueChanged(cache, row, oldRow);
  }

  protected virtual bool ValueChanged(PXCache cache, TUpdatingEntity row, TUpdatingEntity oldRow)
  {
    object obj = cache.GetValue<TUpdatingField>((object) row);
    return obj != null && !obj.Equals(cache.GetValue<TUpdatingField>((object) oldRow));
  }

  protected virtual void _(Events.RowUpdated<TUpdatingEntity> e, PXRowUpdated del)
  {
    PreventRecursionCall.Execute((Action) (() =>
    {
      del?.Invoke(((Events.Event<PXRowUpdatedEventArgs, Events.RowUpdated<TUpdatingEntity>>) e).Cache, ((Events.Event<PXRowUpdatedEventArgs, Events.RowUpdated<TUpdatingEntity>>) e).Args);
      if ((object) e.Row == null || !this.ShouldProcess(((Events.Event<PXRowUpdatedEventArgs, Events.RowUpdated<TUpdatingEntity>>) e).Cache, e.Row, e.OldRow) || this.Base.UnattendedMode || this.Base.IsCopyPasteContext)
        return;
      if (((PXSelectBase) this.Filter).View.Answer == 3)
      {
        ((Events.Event<PXRowUpdatedEventArgs, Events.RowUpdated<TUpdatingEntity>>) e).Cache.SetValue<TUpdatingField>((object) e.Row, ((Events.Event<PXRowUpdatedEventArgs, Events.RowUpdated<TUpdatingEntity>>) e).Cache.GetValue<TUpdatingField>((object) e.OldRow));
        throw new CRWizardAbortException();
      }
      try
      {
        this.LinkAsk(((Events.Event<PXRowUpdatedEventArgs, Events.RowUpdated<TUpdatingEntity>>) e).Cache.GetValue<TUpdatingField>((object) e.Row));
      }
      catch (CRWizardBackException ex)
      {
      }
    }), nameof (_), "C:\\build\\code_repo\\WebSites\\Pure\\PX.Objects\\CR\\GraphExtensions\\SideBySideComparison\\Link\\LinkEntitiesExt_EventBased.cs", 69);
  }
}
