// Decompiled with JetBrains decompiler
// Type: PX.Objects.Common.GraphExtensions.CurySettingsExtension`3
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CM;
using PX.Objects.Common.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.Common.GraphExtensions;

public class CurySettingsExtension<TGraph, TDAC, TCuryDAC> : PXGraphExtension<TGraph>
  where TGraph : PXGraph
  where TDAC : class, IBqlTable, new()
  where TCuryDAC : class, IBqlTable, new()
{
  protected PXView curySettings;
  protected string curyID = "CuryID";
  private object updatedRecord;

  protected virtual string ViewName => "CurySettings_" + typeof (TDAC).Name;

  public virtual void Initialize()
  {
    ((PXGraphExtension) this).Initialize();
    this.curySettings = new PXView((PXGraph) this.Base, false, BqlCommand.CreateInstance(this.ComposeCommand().ToArray()));
    this.Base.Views.Add(this.ViewName, this.curySettings);
    this.Base.Views.Caches.Add(typeof (TCuryDAC));
    // ISSUE: method pointer
    this.Base.FieldDefaulting.AddHandler(typeof (TCuryDAC), this.curyID, new PXFieldDefaulting((object) this, __methodptr(\u003CInitialize\u003Eb__5_0)));
  }

  protected virtual void _(Events.RowUpdated<TCuryDAC> e)
  {
    PXCache pxCache = (PXCache) GraphHelper.Caches<TDAC>((PXGraph) this.Base);
    TDAC dac = PXParentAttribute.SelectParent<TDAC>(((Events.Event<PXRowUpdatedEventArgs, Events.RowUpdated<TCuryDAC>>) e).Cache, (object) e.Row);
    if (this.updatedRecord == (object) dac || !this.IsTenantBaseCurrency((object) e.Row) || !this.GetCurySettingsFields().Any<string>((Func<string, bool>) (field => !object.Equals(((Events.Event<PXRowUpdatedEventArgs, Events.RowUpdated<TCuryDAC>>) e).Cache.GetValue((object) e.Row, field), ((Events.Event<PXRowUpdatedEventArgs, Events.RowUpdated<TCuryDAC>>) e).Cache.GetValue((object) e.OldRow, field)))))
      return;
    TDAC copy = PXCache<TDAC>.CreateCopy(dac);
    foreach (string curySettingsField in this.GetCurySettingsFields())
      pxCache.SetValue((object) copy, curySettingsField, ((Events.Event<PXRowUpdatedEventArgs, Events.RowUpdated<TCuryDAC>>) e).Cache.GetValue((object) e.Row, curySettingsField));
    pxCache.Update((object) copy);
  }

  protected virtual void _(Events.RowInserted<TCuryDAC> e)
  {
    PXCache pxCache = (PXCache) GraphHelper.Caches<TDAC>((PXGraph) this.Base);
    TDAC dac = PXParentAttribute.SelectParent<TDAC>(((Events.Event<PXRowInsertedEventArgs, Events.RowInserted<TCuryDAC>>) e).Cache, (object) e.Row);
    if (this.updatedRecord == (object) dac || !this.IsTenantBaseCurrency((object) e.Row))
      return;
    TDAC copy = PXCache<TDAC>.CreateCopy(dac);
    foreach (string curySettingsField in this.GetCurySettingsFields())
      pxCache.SetValue((object) copy, curySettingsField, ((Events.Event<PXRowInsertedEventArgs, Events.RowInserted<TCuryDAC>>) e).Cache.GetValue((object) e.Row, curySettingsField));
    pxCache.Update((object) copy);
  }

  protected virtual void _(Events.RowUpdated<TDAC> e)
  {
    PXCache pxCache = (PXCache) GraphHelper.Caches<TCuryDAC>((PXGraph) this.Base);
    if (!this.GetCurySettingsFields().Any<string>((Func<string, bool>) (field => !object.Equals(((Events.Event<PXRowUpdatedEventArgs, Events.RowUpdated<TDAC>>) e).Cache.GetValue((object) e.Row, field), ((Events.Event<PXRowUpdatedEventArgs, Events.RowUpdated<TDAC>>) e).Cache.GetValue((object) e.OldRow, field)))))
      return;
    this.updatedRecord = (object) e.Row;
    try
    {
      object obj = this.curySettings.SelectSingleBound(new object[1]
      {
        this.updatedRecord
      }, new object[1]{ (object) this.GetBaseCuryID() }) ?? pxCache.Insert();
      object copy = pxCache.CreateCopy(obj);
      foreach (string curySettingsField in this.GetCurySettingsFields())
        pxCache.SetValue(copy, curySettingsField, ((Events.Event<PXRowUpdatedEventArgs, Events.RowUpdated<TDAC>>) e).Cache.GetValue((object) e.Row, curySettingsField));
      pxCache.Update(copy);
    }
    finally
    {
      this.updatedRecord = (object) null;
    }
  }

  protected virtual void _(Events.RowInserted<TDAC> e)
  {
    this.updatedRecord = (object) e.Row;
    try
    {
      if (!e.Row.AreAllKeysFilled<TDAC>(((Events.Event<PXRowInsertedEventArgs, Events.RowInserted<TDAC>>) e).Cache))
        return;
      if (this.curySettings.SelectSingleBound(new object[1]
      {
        (object) e.Row
      }, new object[1]{ (object) this.GetBaseCuryID() }) != null)
        return;
      PXCache pxCache = (PXCache) GraphHelper.Caches<TCuryDAC>((PXGraph) this.Base);
      object instance = pxCache.CreateInstance();
      foreach (string curySettingsField in this.GetCurySettingsFields())
        pxCache.SetValue(instance, curySettingsField, ((Events.Event<PXRowInsertedEventArgs, Events.RowInserted<TDAC>>) e).Cache.GetValue((object) e.Row, curySettingsField));
      ((PXCache) GraphHelper.Caches<TCuryDAC>((PXGraph) this.Base)).Insert(instance);
    }
    finally
    {
      this.updatedRecord = (object) null;
    }
  }

  protected virtual List<Type> ComposeCommand()
  {
    PXCache pxCache1 = (PXCache) GraphHelper.Caches<TDAC>((PXGraph) this.Base);
    PXCache pxCache2 = (PXCache) GraphHelper.Caches<TCuryDAC>((PXGraph) this.Base);
    List<Type> typeList = new List<Type>(15)
    {
      typeof (Select<,>),
      typeof (TCuryDAC)
    };
    for (int index = 0; index < pxCache2.Keys.Count; ++index)
    {
      string key = pxCache2.Keys[index];
      if (typeList.Count == 2)
        typeList.Add(typeof (Where<,,>));
      else if (index < pxCache2.Keys.Count - 2)
        typeList.Add(typeof (And<,,>));
      else
        typeList.Add(typeof (And<,>));
      typeList.Add(pxCache2.GetBqlField(key));
      typeList.Add(typeof (Equal<>));
      typeList.Add(key == this.curyID ? typeof (Optional<>) : typeof (Current<>));
      typeList.Add(key == this.curyID ? typeof (AccessInfo.baseCuryID) : pxCache1.GetBqlField(key));
    }
    return typeList;
  }

  protected virtual IEnumerable<string> GetCurySettingsFields()
  {
    string[] excludedFields = new string[1]
    {
      "dfltPutawayLocationID"
    };
    PXCache cache = this.Base.Caches[typeof (TCuryDAC)];
    return ((IEnumerable<string>) cache.Fields).Where<string>((Func<string, bool>) (f => !cache.Keys.Contains(f) && !GraphHelper.IsAuditFieldName(f) && !((IEnumerable<string>) excludedFields).Any<string>((Func<string, bool>) (ef => string.Equals(ef, f, StringComparison.OrdinalIgnoreCase)))));
  }

  protected bool IsTenantBaseCurrency(object row)
  {
    return string.Compare(((PXCache) GraphHelper.Caches<TCuryDAC>((PXGraph) this.Base)).GetValue(row, this.curyID) as string, this.GetBaseCuryID(), StringComparison.InvariantCulture) == 0;
  }

  protected virtual string GetBaseCuryID()
  {
    string baseCuryId = this.Base.Accessinfo.BaseCuryID;
    if (baseCuryId != null)
      return baseCuryId;
    return CurrencyCollection.GetBaseCurrency()?.CuryID;
  }
}
