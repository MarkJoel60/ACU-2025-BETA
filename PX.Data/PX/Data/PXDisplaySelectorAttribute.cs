// Decompiled with JetBrains decompiler
// Type: PX.Data.PXDisplaySelectorAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;

#nullable disable
namespace PX.Data;

/// <exclude />
[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
internal class PXDisplaySelectorAttribute : PXSelectorAttribute
{
  public PXDisplaySelectorAttribute(System.Type type)
    : base(type)
  {
  }

  public PXDisplaySelectorAttribute(System.Type type, params System.Type[] fieldList)
    : base(type, fieldList)
  {
  }

  public override void FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
  {
  }

  public override void ReadDeletedFieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
  {
  }

  public override void SubstituteKeyFieldUpdating(PXCache sender, PXFieldUpdatingEventArgs e)
  {
    if (this.MacroVariablesManager != null)
      e.NewValue = this.MacroVariablesManager.TryResolveExt(e.NewValue, sender, this.FieldName, e.Row);
    if (e.Cancel || e.NewValue == null)
      return;
    object data1 = (object) null;
    PXSelectorAttribute.GlobalDictionary dict = (PXSelectorAttribute.GlobalDictionary) null;
    if (this._CacheGlobal)
    {
      dict = this.GetGlobalCache();
      lock (dict.SyncRoot)
      {
        PXSelectorAttribute.GlobalDictionary.CacheValue cacheValue;
        if (dict.TryGetValue(this.CreateGlobalCacheKey(sender, e.Row, e.NewValue), out cacheValue))
        {
          if (cacheValue.IsDeleted)
          {
            object obj = sender.Graph.TypedViews.GetView(this._NaturalSelect, !this._DirtyRead).Cache.GetValue(cacheValue.Item, this.ForeignField.Name);
            e.NewValue = obj;
            return;
          }
          data1 = cacheValue.Item;
        }
      }
    }
    if (data1 == null)
    {
      PXView select = sender.Graph.TypedViews.GetView(this._NaturalSelect, !this._DirtyRead);
      object[] pars = this.MakeParameters(e.NewValue, true);
      Func<object> func = (Func<object>) (() =>
      {
        PXView view = select;
        object[] currents = new object[1]{ e.Row };
        object[] objArray;
        if (!this._CacheGlobal)
          objArray = pars;
        else
          objArray = new object[1]{ e.NewValue };
        return PXSelectorAttribute.SelectSingleBound(view, currents, objArray);
      });
      PXCache pxCache = sender;
      object row = e.Row;
      string fieldName = this._FieldName;
      PXView view1 = select;
      object[] pars1;
      if (!this._CacheGlobal)
        pars1 = pars;
      else
        pars1 = new object[1]{ e.NewValue };
      object obj1 = pxCache._InvokeSelectorGetter(row, fieldName, view1, pars1, true) ?? func();
      if (obj1 != null)
      {
        object obj2 = select.Cache.GetValue(obj1, this.ForeignField.Name);
        if (select.Cache.Keys.Count <= 1)
          this.cacheOnReadItem(dict, select.Cache, obj1);
        e.NewValue = obj2;
      }
      else
      {
        bool flag = false;
        if (PXDatabase.IsReadDeletedSupported(this._BqlType))
        {
          using (new PXReadDeletedScope())
          {
            object obj3 = func();
            if (obj3 != null)
            {
              object obj4 = select.Cache.GetValue(obj3, this.ForeignField.Name);
              if (select.Cache.Keys.Count <= 1)
                this.cacheOnReadItem(dict, select.Cache, obj3, true);
              e.NewValue = obj4;
              flag = true;
            }
          }
        }
        if (flag)
          return;
        this.throwNoItem(PXSelectorAttribute.hasRestrictedAccess(sender, this._NaturalSelect, e.Row), !sender.Graph.IsContractBasedAPI, e.NewValue);
      }
    }
    else
    {
      PXCache cach = sender.Graph.Caches[this._Type];
      object newValue = e.NewValue;
      e.NewValue = cach.GetValue(data1, this.ForeignField.Name);
      if (e.NewValue != null || cach.GetItemType().IsAssignableFrom(data1.GetType()))
        return;
      PXView view = sender.Graph.TypedViews.GetView(this._NaturalSelect, !this._DirtyRead);
      object obj = sender._InvokeSelectorGetter(e.Row, this._FieldName, view, new object[1]
      {
        newValue
      }, true);
      if (obj == null)
        obj = PXSelectorAttribute.SelectSingleBound(view, new object[1]
        {
          e.Row
        }, newValue);
      object data2 = obj;
      if (data2 == null)
        return;
      e.NewValue = view.Cache.GetValue(data2, this.ForeignField.Name);
    }
  }
}
