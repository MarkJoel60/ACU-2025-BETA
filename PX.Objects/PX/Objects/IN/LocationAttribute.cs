// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.LocationAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

#nullable enable
namespace PX.Objects.IN;

[PXDBInt]
[PXUIField]
public class LocationAttribute : PXEntityAttribute
{
  public const 
  #nullable disable
  string DimensionName = "INLOCATION";
  protected Type _SiteIDType;
  protected bool _KeepEntry = true;
  protected bool _ResetEntry = true;
  protected Dictionary<int?, int?> _JustPersisted;

  public bool KeepEntry
  {
    get => this._KeepEntry;
    set => this._KeepEntry = value;
  }

  public bool ResetEntry
  {
    get => this._ResetEntry;
    set => this._ResetEntry = value;
  }

  [Obsolete("This item has been deprecated and will be removed in Acumatica ERP 2023 R1.")]
  public LocationAttribute()
  {
    ((PXAggregateAttribute) this)._Attributes.Add((PXEventSubscriberAttribute) new PXDimensionSelectorAttribute("INLOCATION", typeof (Search<INLocation.locationID>), typeof (INLocation.locationCD))
    {
      CacheGlobal = true,
      DescriptionField = typeof (INLocation.descr)
    });
    this._SelAttrIndex = ((List<PXEventSubscriberAttribute>) ((PXAggregateAttribute) this)._Attributes).Count - 1;
  }

  public LocationAttribute(Type SiteIDType)
  {
    this._SiteIDType = SiteIDType ?? throw new PXArgumentException(nameof (SiteIDType), "The argument cannot be null.");
    ((PXAggregateAttribute) this)._Attributes.Add((PXEventSubscriberAttribute) new LocationAttribute.LocationDimensionSelectorAttribute(((IBqlTemplate) BqlTemplate.OfCommand<Search<INLocation.locationID, Where<INLocation.siteID, Equal<Optional<BqlPlaceholder.A>>>>>.Replace<BqlPlaceholder.A>(this._SiteIDType)).ToType(), this.GetSiteIDKeyRelation(SiteIDType)));
    this._SelAttrIndex = ((List<PXEventSubscriberAttribute>) ((PXAggregateAttribute) this)._Attributes).Count - 1;
  }

  protected Type GetSiteIDKeyRelation(Type siteIDField)
  {
    return typeof (Field<>.IsRelatedTo<>).MakeGenericType(siteIDField, typeof (INLocation.siteID));
  }

  public bool IsWarehouseLocationEnabled(PXCache sender)
  {
    if (sender.Graph is IFeatureAccessProvider && ((IFeatureAccessProvider) sender.Graph).IsFeatureInstalled<FeaturesSet.warehouseLocation>())
      return true;
    return !(sender.Graph is IFeatureAccessProvider) && PXAccess.FeatureInstalled<FeaturesSet.warehouseLocation>();
  }

  public virtual void CacheAttached(PXCache sender)
  {
    if (this._SiteIDType != (Type) null && !this.IsWarehouseLocationEnabled(sender) && sender.Graph.GetType() != typeof (PXGraph))
    {
      ((PXDimensionSelectorAttribute) ((PXAggregateAttribute) this)._Attributes[this._SelAttrIndex]).ValidComboRequired = false;
      PXGraph.FieldDefaultingEvents fieldDefaulting = sender.Graph.FieldDefaulting;
      Type itemType1 = sender.GetItemType();
      string fieldName1 = ((PXEventSubscriberAttribute) this)._FieldName;
      LocationAttribute locationAttribute1 = this;
      // ISSUE: virtual method pointer
      PXFieldDefaulting pxFieldDefaulting = new PXFieldDefaulting((object) locationAttribute1, __vmethodptr(locationAttribute1, Feature_FieldDefaulting));
      fieldDefaulting.AddHandler(itemType1, fieldName1, pxFieldDefaulting);
      PXGraph.FieldUpdatingEvents fieldUpdating1 = sender.Graph.FieldUpdating;
      Type itemType2 = sender.GetItemType();
      string fieldName2 = ((PXEventSubscriberAttribute) this)._FieldName;
      LocationAttribute locationAttribute2 = this;
      // ISSUE: virtual method pointer
      PXFieldUpdating pxFieldUpdating1 = new PXFieldUpdating((object) locationAttribute2, __vmethodptr(locationAttribute2, Feature_FieldUpdating));
      fieldUpdating1.AddHandler(itemType2, fieldName2, pxFieldUpdating1);
      PXGraph.FieldUpdatingEvents fieldUpdating2 = sender.Graph.FieldUpdating;
      Type itemType3 = sender.GetItemType();
      string fieldName3 = ((PXEventSubscriberAttribute) this)._FieldName;
      PXDimensionSelectorAttribute attribute = (PXDimensionSelectorAttribute) ((PXAggregateAttribute) this)._Attributes[this._SelAttrIndex];
      // ISSUE: virtual method pointer
      PXFieldUpdating pxFieldUpdating2 = new PXFieldUpdating((object) attribute, __vmethodptr(attribute, FieldUpdating));
      fieldUpdating2.RemoveHandler(itemType3, fieldName3, pxFieldUpdating2);
      if (!PXAccess.FeatureInstalled<FeaturesSet.warehouse>() && sender.GetItemType() == typeof (INSite))
      {
        this._JustPersisted = new Dictionary<int?, int?>();
        PXGraph.RowPersistingEvents rowPersisting = sender.Graph.RowPersisting;
        LocationAttribute locationAttribute3 = this;
        // ISSUE: virtual method pointer
        PXRowPersisting pxRowPersisting = new PXRowPersisting((object) locationAttribute3, __vmethodptr(locationAttribute3, Feature_RowPersisting));
        rowPersisting.AddHandler<INLocation>(pxRowPersisting);
        PXGraph.RowPersistedEvents rowPersisted = sender.Graph.RowPersisted;
        LocationAttribute locationAttribute4 = this;
        // ISSUE: virtual method pointer
        PXRowPersisted pxRowPersisted = new PXRowPersisted((object) locationAttribute4, __vmethodptr(locationAttribute4, Feature_RowPersisted));
        rowPersisted.AddHandler<INLocation>(pxRowPersisted);
      }
      if (!PXAccess.FeatureInstalled<FeaturesSet.warehouse>() && !sender.Graph.Views.Caches.Contains(typeof (INSite)))
        sender.Graph.Views.Caches.Add(typeof (INSite));
      if (!PXAccess.FeatureInstalled<FeaturesSet.warehouse>() && !sender.Graph.Views.Caches.Contains(typeof (INLocation)))
        sender.Graph.Views.Caches.Add(typeof (INLocation));
    }
    ((PXAggregateAttribute) this).CacheAttached(sender);
    if (!(this._SiteIDType != (Type) null))
      return;
    PXGraph.FieldUpdatedEvents fieldUpdated = sender.Graph.FieldUpdated;
    Type itemType4 = sender.GetItemType();
    string name = this._SiteIDType.Name;
    LocationAttribute locationAttribute5 = this;
    // ISSUE: virtual method pointer
    PXFieldUpdated pxFieldUpdated = new PXFieldUpdated((object) locationAttribute5, __vmethodptr(locationAttribute5, SiteID_FieldUpdated));
    fieldUpdated.AddHandler(itemType4, name, pxFieldUpdated);
    if (!this.IsWarehouseLocationEnabled(sender))
      return;
    string lower = ((PXEventSubscriberAttribute) this)._FieldName.ToLower();
    PXGraph.FieldUpdatingEvents fieldUpdating3 = sender.Graph.FieldUpdating;
    Type itemType5 = sender.GetItemType();
    string str1 = lower;
    LocationAttribute locationAttribute6 = this;
    // ISSUE: virtual method pointer
    PXFieldUpdating pxFieldUpdating3 = new PXFieldUpdating((object) locationAttribute6, __vmethodptr(locationAttribute6, FieldUpdating));
    fieldUpdating3.AddHandler(itemType5, str1, pxFieldUpdating3);
    PXGraph.FieldUpdatingEvents fieldUpdating4 = sender.Graph.FieldUpdating;
    Type itemType6 = sender.GetItemType();
    string str2 = lower;
    PXDimensionSelectorAttribute attribute1 = (PXDimensionSelectorAttribute) ((PXAggregateAttribute) this)._Attributes[this._SelAttrIndex];
    // ISSUE: virtual method pointer
    PXFieldUpdating pxFieldUpdating4 = new PXFieldUpdating((object) attribute1, __vmethodptr(attribute1, FieldUpdating));
    fieldUpdating4.RemoveHandler(itemType6, str2, pxFieldUpdating4);
    PXGraph.FieldSelectingEvents fieldSelecting1 = sender.Graph.FieldSelecting;
    Type itemType7 = sender.GetItemType();
    string str3 = lower;
    LocationAttribute locationAttribute7 = this;
    // ISSUE: virtual method pointer
    PXFieldSelecting pxFieldSelecting1 = new PXFieldSelecting((object) locationAttribute7, __vmethodptr(locationAttribute7, FieldSelecting));
    fieldSelecting1.AddHandler(itemType7, str3, pxFieldSelecting1);
    PXGraph.FieldSelectingEvents fieldSelecting2 = sender.Graph.FieldSelecting;
    Type itemType8 = sender.GetItemType();
    string str4 = lower;
    PXDimensionSelectorAttribute attribute2 = (PXDimensionSelectorAttribute) ((PXAggregateAttribute) this)._Attributes[this._SelAttrIndex];
    // ISSUE: virtual method pointer
    PXFieldSelecting pxFieldSelecting2 = new PXFieldSelecting((object) attribute2, __vmethodptr(attribute2, FieldSelecting));
    fieldSelecting2.RemoveHandler(itemType8, str4, pxFieldSelecting2);
    PXDimensionSelectorAttribute.SetValidCombo(sender, lower, false);
  }

  public virtual void Feature_RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    int? nullable1 = (int?) sender.GetValue(e.Row, this._SiteIDType.Name);
    if ((e.Operation & 3) != 2)
      return;
    int? nullable2 = nullable1;
    int num = 0;
    if (!(nullable2.GetValueOrDefault() < num & nullable2.HasValue))
      return;
    INSite inSite = ((IEnumerable<INSite>) sender.Graph.Caches[typeof (INSite)].Inserted).First<INSite>();
    sender.SetValue(e.Row, this._SiteIDType.Name, (object) inSite.SiteID);
    this._JustPersisted.Add(inSite.SiteID, nullable1);
  }

  public virtual void Feature_RowPersisted(PXCache sender, PXRowPersistedEventArgs e)
  {
    int? key = (int?) sender.GetValue(e.Row, this._SiteIDType.Name);
    int? nullable;
    if ((e.Operation & 3) != 2 || e.TranStatus != 2 || !this._JustPersisted.TryGetValue(key, out nullable))
      return;
    sender.SetValue(e.Row, this._SiteIDType.Name, (object) nullable);
  }

  public virtual void Feature_FieldUpdating(PXCache sender, PXFieldUpdatingEventArgs e)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    LocationAttribute.\u003C\u003Ec__DisplayClass19_0 cDisplayClass190 = new LocationAttribute.\u003C\u003Ec__DisplayClass19_0();
    if (e.NewValue == null || ((CancelEventArgs) e).Cancel)
      return;
    // ISSUE: reference to a compiler-generated field
    cDisplayClass190.siteval = (int?) sender.GetValue(e.Row, this._SiteIDType.Name);
    PXCache cach = sender.Graph.Caches[typeof (INSite)];
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    if ((cDisplayClass190._current = INSite.PK.Find(sender.Graph, cDisplayClass190.siteval)) == null)
    {
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated method
      cDisplayClass190._current = (cach == sender ? e.Row : (!cDisplayClass190.siteval.HasValue ? (object) null : (object) ((IEnumerable<INSite>) cach.Inserted).FirstOrDefault<INSite>(new Func<INSite, bool>(cDisplayClass190.\u003CFeature_FieldUpdating\u003Eb__0)))) as INSite;
    }
    PXDimensionSelectorAttribute attribute = (PXDimensionSelectorAttribute) ((PXAggregateAttribute) this)._Attributes[this._SelAttrIndex];
    // ISSUE: virtual method pointer
    PXFieldUpdating pxFieldUpdating = new PXFieldUpdating((object) attribute, __vmethodptr(attribute, FieldUpdating));
    // ISSUE: method pointer
    PXFieldDefaulting pxFieldDefaulting = new PXFieldDefaulting((object) cDisplayClass190, __methodptr(\u003CFeature_FieldUpdating\u003Eb__1));
    GraphHelper.Caches<INLocation>(sender.Graph);
    sender.Graph.FieldDefaulting.AddHandler<INLocation.siteID>(pxFieldDefaulting);
    try
    {
      pxFieldUpdating.Invoke(sender, e);
    }
    finally
    {
      sender.Graph.FieldDefaulting.RemoveHandler<INLocation.siteID>(pxFieldDefaulting);
    }
  }

  public virtual void Feature_FieldDefaulting(PXCache sender, PXFieldDefaultingEventArgs e)
  {
    if (((CancelEventArgs) e).Cancel)
      return;
    int? key = (int?) sender.GetValue(e.Row, this._SiteIDType.Name);
    object obj = (object) null;
    if (key.HasValue)
    {
      if (!this.Definitions.DefaultLocations.TryGetValue(key, out obj))
      {
        try
        {
          obj = (object) "MAIN";
          sender.RaiseFieldUpdating(((PXEventSubscriberAttribute) this)._FieldName, e.Row, ref obj);
        }
        catch (InvalidOperationException ex)
        {
        }
      }
    }
    e.NewValue = obj;
    ((CancelEventArgs) e).Cancel = true;
  }

  public virtual void FieldSelecting(PXCache sender, PXFieldSelectingEventArgs e)
  {
    if (((CancelEventArgs) e).Cancel)
      return;
    PXDimensionSelectorAttribute attribute = (PXDimensionSelectorAttribute) ((PXAggregateAttribute) this)._Attributes[this._SelAttrIndex];
    attribute.DirtyRead = true;
    attribute.FieldSelecting(sender, e, attribute.SuppressViewCreation, true);
    if (e.ReturnState is PXSegmentedState returnState)
      returnState.ValidCombos = true;
    int? nullable = (int?) sender.GetValue(e.Row, ((PXEventSubscriberAttribute) this)._FieldOrdinal);
    int num = 0;
    if (!(nullable.GetValueOrDefault() < num & nullable.HasValue))
      return;
    int? siteID = (int?) sender.GetValue(e.Row, this._SiteIDType.Name);
    if (!((string) sender.Graph.Caches[typeof (INSite)].GetValue<INSite.locationValid>((object) INSite.PK.Find(sender.Graph, siteID)) == "W"))
      return;
    sender.RaiseExceptionHandling(((PXEventSubscriberAttribute) this)._FieldName, e.Row, (object) null, (Exception) new PXSetPropertyException("'{0}' cannot be found in the system.", (PXErrorLevel) 2, new object[1]
    {
      (object) "Location"
    }));
  }

  public virtual void FieldUpdating(PXCache sender, PXFieldUpdatingEventArgs e)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    LocationAttribute.\u003C\u003Ec__DisplayClass22_0 cDisplayClass220 = new LocationAttribute.\u003C\u003Ec__DisplayClass22_0();
    if (e.NewValue == null || ((CancelEventArgs) e).Cancel)
      return;
    int? siteID = (int?) sender.GetValue(e.Row, this._SiteIDType.Name);
    PXCache cach = sender.Graph.Caches[typeof (INSite)];
    // ISSUE: reference to a compiler-generated field
    cDisplayClass220._current = INSite.PK.Find(sender.Graph, siteID);
    // ISSUE: reference to a compiler-generated field
    if (cDisplayClass220._current == null)
    {
      // ISSUE: reference to a compiler-generated field
      cDisplayClass220._current = cach.Current as INSite;
    }
    PXDimensionSelectorAttribute attribute = (PXDimensionSelectorAttribute) ((PXAggregateAttribute) this)._Attributes[this._SelAttrIndex];
    // ISSUE: virtual method pointer
    PXFieldUpdating pxFieldUpdating = new PXFieldUpdating((object) attribute, __vmethodptr(attribute, FieldUpdating));
    // ISSUE: method pointer
    PXFieldDefaulting pxFieldDefaulting = new PXFieldDefaulting((object) cDisplayClass220, __methodptr(\u003CFieldUpdating\u003Eb__0));
    // ISSUE: method pointer
    PXRowInserting pxRowInserting = new PXRowInserting((object) cDisplayClass220, __methodptr(\u003CFieldUpdating\u003Eb__1));
    sender.Graph.RowInserting.AddHandler<INLocation>(pxRowInserting);
    sender.Graph.FieldDefaulting.AddHandler<INLocation.siteID>(pxFieldDefaulting);
    try
    {
      pxFieldUpdating.Invoke(sender, e);
    }
    catch (PXSetPropertyException ex)
    {
      object newValue = e.NewValue;
      ex.ErrorValue = newValue;
      throw;
    }
    finally
    {
      sender.Graph.RowInserting.RemoveHandler<INLocation>(pxRowInserting);
      sender.Graph.FieldDefaulting.RemoveHandler<INLocation.siteID>(pxFieldDefaulting);
    }
  }

  public virtual void SiteID_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    if (this._KeepEntry)
    {
      object valueExt = sender.GetValueExt(e.Row, ((PXEventSubscriberAttribute) this)._FieldName);
      sender.SetValue(e.Row, ((PXEventSubscriberAttribute) this)._FieldOrdinal, (object) null);
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      // ISSUE: method pointer
      PXRowInserting pxRowInserting = LocationAttribute.\u003C\u003Ec.\u003C\u003E9__23_0 ?? (LocationAttribute.\u003C\u003Ec.\u003C\u003E9__23_0 = new PXRowInserting((object) LocationAttribute.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003CSiteID_FieldUpdated\u003Eb__23_0)));
      sender.Graph.RowInserting.AddHandler<INLocation>(pxRowInserting);
      try
      {
        object obj = valueExt is PXFieldState ? ((PXFieldState) valueExt).Value : valueExt;
        sender.SetValueExt(e.Row, ((PXEventSubscriberAttribute) this)._FieldName, obj);
      }
      catch (PXException ex)
      {
      }
      finally
      {
        sender.Graph.RowInserting.RemoveHandler<INLocation>(pxRowInserting);
      }
    }
    else
    {
      if (!this._ResetEntry)
        return;
      sender.SetValueExt(e.Row, ((PXEventSubscriberAttribute) this)._FieldName, (object) null);
    }
  }

  protected virtual LocationAttribute.Definition Definitions
  {
    get
    {
      LocationAttribute.Definition definitions = PXContext.GetSlot<LocationAttribute.Definition>();
      if (definitions == null)
        definitions = PXContext.SetSlot<LocationAttribute.Definition>(PXDatabase.GetSlot<LocationAttribute.Definition>("INLocation.Definition", new Type[1]
        {
          typeof (INLocation)
        }));
      return definitions;
    }
  }

  public class dimensionName : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  LocationAttribute.dimensionName>
  {
    public dimensionName()
      : base("INLOCATION")
    {
    }
  }

  protected class Definition : IPrefetchable, IPXCompanyDependent
  {
    public Dictionary<int?, object> DefaultLocations;

    public void Prefetch()
    {
      this.DefaultLocations = new Dictionary<int?, object>();
      foreach (PXDataRecord pxDataRecord in PXDatabase.SelectMulti<INLocation>(new PXDataField[4]
      {
        (PXDataField) new PXDataField<INLocation.siteID>(),
        (PXDataField) new PXDataField<INLocation.locationID>(),
        (PXDataField) new PXDataFieldOrder<INLocation.siteID>(),
        (PXDataField) new PXDataFieldOrder<INLocation.locationID>()
      }))
      {
        int? int32 = pxDataRecord.GetInt32(0);
        if (!this.DefaultLocations.ContainsKey(int32))
          this.DefaultLocations.Add(int32, (object) pxDataRecord.GetInt32(1));
      }
    }
  }

  public class LocationSelectorAttribute : PXSelectorAttribute.WithCachingByCompositeKeyAttribute
  {
    public LocationSelectorAttribute(Type search, Type additionalKeysRelation)
      : base(search, additionalKeysRelation)
    {
      ((PXSelectorAttribute) this).SubstituteKey = typeof (INLocation.locationCD);
    }

    public LocationSelectorAttribute(
      Type search,
      Type additionalKeysRelation,
      Type lookupJoin,
      Type[] fieldList)
      : base(search, additionalKeysRelation, lookupJoin, fieldList)
    {
      ((PXSelectorAttribute) this).SubstituteKey = typeof (INLocation.locationCD);
    }

    protected virtual void OnItemCached(
      PXCache foreignCache,
      object foreignItem,
      bool isItemDeleted)
    {
      ((PXSelectorAttribute) this).OnItemCached(foreignCache, foreignItem, isItemDeleted);
      if (isItemDeleted)
        return;
      PrimaryKeyOf<INLocation>.By<INLocation.locationID>.Dirty.PutToGlobalCache(foreignCache.Graph, (INLocation.locationID) foreignItem);
    }
  }

  public class LocationDimensionSelectorAttribute : 
    PXDimensionSelector.WithCachingByCompositeKeyAttribute
  {
    public LocationDimensionSelectorAttribute(Type search, Type additionalKeysRelation)
      : base("INLOCATION")
    {
      this.Initialize(new LocationAttribute.LocationSelectorAttribute(search, additionalKeysRelation));
    }

    public LocationDimensionSelectorAttribute(
      Type search,
      Type additionalKeysRelation,
      Type lookupJoin,
      Type[] fieldList)
      : base("INLOCATION")
    {
      this.Initialize(new LocationAttribute.LocationSelectorAttribute(search, additionalKeysRelation, lookupJoin, fieldList));
      ((PXDimensionSelectorAttribute) this).DirtyRead = true;
    }

    private void Initialize(
      LocationAttribute.LocationSelectorAttribute locationSelectorAttribute)
    {
      ((PXDimensionSelectorAttribute) this).RegisterSelector((PXSelectorAttribute) locationSelectorAttribute);
      ((PXDimensionSelectorAttribute) this).ValidComboRequired = true;
      this.OnlyKeyConditions = true;
      ((PXDimensionSelectorAttribute) this).DescriptionField = typeof (INLocation.descr);
    }
  }
}
