// Decompiled with JetBrains decompiler
// Type: PX.Data.Descriptor.KeyValue.UdfMaint
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.CS;
using PX.Data.DependencyInjection;
using PX.Data.Descriptor.KeyValue.DAC;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web.Compilation;

#nullable disable
namespace PX.Data.Descriptor.KeyValue;

public class UdfMaint : CSAttributeMaint2, IGraphWithInitialization
{
  public PXSelect<CSAddedAttribute> AddedUDFs;
  public PXSelect<CSScreenAttributeProperties> ScreenAttributeProperties;
  public PXSelect<CSAttribute, Where<CSAttribute.attributeID, Equal<Required<CSAddedAttribute.attributeID>>>> Attribute;
  public PXSelect<CSAttributeDetail, Where<CSAttributeDetail.attributeID, Equal<Required<CSAttributeDetail.attributeID>>>> Values;
  public PXSelect<CSScreenAttribute, Where<CSScreenAttribute.screenID, Equal<Current<AttribParams.screenID>>, And<CSScreenAttribute.attributeID, Equal<Current<CSAddedAttribute.attributeID>>>>> CurrentScreenAttributes;
  public PXSelect<CSScreenAttribute, Where<CSScreenAttribute.screenID, Equal<Current<AttribParams.screenID>>, And<CSScreenAttribute.attributeID, Equal<Required<CSAddedAttribute.attributeID>>>>> ScreenAttributes;
  public PXSelect<CSScreenAttribute, Where<CSScreenAttribute.screenID, Equal<Current<AttribParams.screenID>>>> AllScreenAttributes;
  public PXSelect<CSScreenAttribute, Where<CSScreenAttribute.screenID, Equal<Required<CSScreenAttribute.screenID>>, And<CSScreenAttribute.attributeID, Equal<Required<CSScreenAttribute.attributeID>>, And<CSScreenAttribute.typeValue, Equal<Current<CSScreenAttributeProperties.typeValue>>>>>> UdfTypedAttribute;
  public PXSelect<CSScreenAttribute, Where<CSScreenAttribute.screenID, Equal<Required<CSScreenAttribute.screenID>>, And<CSScreenAttribute.attributeID, Equal<Required<CSScreenAttribute.attributeID>>, And<CSScreenAttribute.typeValue, Equal<StringEmpty>>>>> UdfAttributeWithoutType;
  public PXAction<AttribParams> Apply;
  public PXAction<AttribParams> CancelUdfModification;
  private string _udfTypeField;
  private List<string> _typeValues;
  private PXCache _primaryCache;

  [InjectDependency]
  private IPXPageIndexingService PageIndexingService { get; set; }

  public void Initialize()
  {
    string screenId = this.ScreenSettings.Current.ScreenID;
    if (string.IsNullOrEmpty(screenId))
      return;
    PXSiteMapNode mapNodeByScreenId = PXSiteMap.Provider.FindSiteMapNodeByScreenID(screenId);
    this._udfTypeField = this.PageIndexingService.GetUDFTypeField(mapNodeByScreenId.GraphType);
    if (string.IsNullOrEmpty(this._udfTypeField))
    {
      PXUIFieldAttribute.SetVisible<CSScreenAttributeProperties.typeValue>(this.ScreenAttributeProperties.Cache, (object) null, false);
      this.InsertAddedAttributes();
    }
    else
    {
      PXGraph instance = PXGraph.CreateInstance(PXBuildManager.GetType(mapNodeByScreenId.GraphType, true));
      string primaryView = this.PageIndexingService.GetPrimaryView(mapNodeByScreenId.GraphType);
      PXView pxView = new PXView((PXGraph) this, true, BqlCommand.CreateInstance(typeof (PX.Data.Select<>), instance.GetItemType(primaryView)), (Delegate) new PXSelectDelegate(Enumerable.Empty<object>));
      this._primaryCache = pxView.Cache;
      this.Views.Add(primaryView, pxView);
      PXFieldState typeValueFieldState = this._primaryCache.GetStateExt((object) null, this._udfTypeField) as PXFieldState;
      List<string> stringList;
      if (!(typeValueFieldState is PXStringState pxStringState))
      {
        stringList = (List<string>) null;
      }
      else
      {
        string[] allowedValues = pxStringState.AllowedValues;
        stringList = allowedValues != null ? ((IEnumerable<string>) allowedValues).ToList<string>() : (List<string>) null;
      }
      this._typeValues = stringList;
      if (this._typeValues == null)
      {
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        IBqlTable[] array1 = PXSelectorAttribute.SelectAll(this._primaryCache, this._udfTypeField, (object) null).Select<object, IBqlTable>(UdfMaint.\u003C\u003EO.\u003C0\u003E__UnwrapMain ?? (UdfMaint.\u003C\u003EO.\u003C0\u003E__UnwrapMain = new Func<object, IBqlTable>(PXResult.UnwrapMain))).ToArray<IBqlTable>();
        PXView udfView = new PXView((PXGraph) this, true, BqlCommand.CreateInstance(typeof (PX.Data.Select<>), array1[0].GetType()), (Delegate) new PXSelectDelegate(Enumerable.Empty<object>));
        string[] array2 = ((IEnumerable<IBqlTable>) array1).Select<IBqlTable, string>((Func<IBqlTable, string>) (row => udfView.Cache.GetValue((object) row, typeValueFieldState.ValueField)?.ToString())).ToArray<string>();
        this._typeValues = ((IEnumerable<string>) array2).ToList<string>();
        PXStringListAttribute.SetList<CSScreenAttributeProperties.typeValue>(this.ScreenAttributeProperties.Cache, (object) null, array2, array2);
      }
      else
        PXStringListAttribute.SetList<CSScreenAttributeProperties.typeValue>(this.ScreenAttributeProperties.Cache, (object) null, pxStringState.AllowedValues, pxStringState.AllowedLabels);
      this.InsertAddedAttributes(typeValueFieldState);
      PXUIFieldAttribute.SetDisplayName<CSScreenAttributeProperties.typeValue>(this.ScreenAttributeProperties.Cache, typeValueFieldState.DisplayName);
    }
  }

  private void InsertAddedAttributes(PXFieldState state = null)
  {
    if (this.AddedUDFs.Cache.IsDirty || this.ScreenAttributeProperties.Cache.IsDirty || this.AddedUDFs.Select().Any<PXResult<CSAddedAttribute>>())
      return;
    foreach (CSAttribute csAttribute in this.Attributes.Select<CSAttribute>())
    {
      CSAddedAttribute csAddedAttribute = new CSAddedAttribute()
      {
        AttributeID = csAttribute.AttributeID,
        Description = csAttribute.Description
      };
      this.AddedUDFs.Current = csAddedAttribute;
      this.AddedUDFs.Insert(csAddedAttribute);
    }
    if (state == null)
      return;
    PXUIFieldAttribute.SetDisplayName<CSScreenAttributeProperties.typeValue>(this.ScreenAttributeProperties.Cache, state.DisplayName);
  }

  public IEnumerable addedUDFs() => this.AddedUDFs.Cache.Inserted;

  public void _(
    Events.FieldUpdated<CSAddedAttribute.attributeID> e)
  {
    CSAddedAttribute row = (CSAddedAttribute) e.Row;
    string oldValue = (string) e.OldValue;
    string attributeId = row.AttributeID;
    CSAttribute csAttribute = this.Attribute.SelectSingle((object) attributeId);
    e.Cache.SetValueExt<CSAddedAttribute.description>((object) row, (object) csAttribute.Description);
    if (!string.IsNullOrEmpty(oldValue))
    {
      PXSelect<CSScreenAttribute, Where<CSScreenAttribute.screenID, Equal<Current<AttribParams.screenID>>, And<CSScreenAttribute.attributeID, Equal<Required<CSAddedAttribute.attributeID>>>>> screenAttributes = this.ScreenAttributes;
      object[] objArray = new object[1]{ (object) oldValue };
      foreach (CSScreenAttribute csScreenAttribute in screenAttributes.Select<CSScreenAttribute>(objArray))
        this.ScreenAttributes.Delete(csScreenAttribute);
    }
    CSScreenAttribute[] source = this.AllScreenAttributes.Select<CSScreenAttribute>();
    short num = ((IEnumerable<CSScreenAttribute>) source).Any<CSScreenAttribute>() ? (short) ((int) ((IEnumerable<CSScreenAttribute>) source).Max<CSScreenAttribute, short>((Func<CSScreenAttribute, short>) (attr => attr.Row.GetValueOrDefault())) + 1) : (short) 1;
    this.ScreenAttributes.Insert(new CSScreenAttribute()
    {
      ScreenID = this.ScreenSettings.Current.ScreenID,
      AttributeID = attributeId,
      Row = new short?(num),
      Column = new short?((short) 1)
    });
    this.AddedUDFs.View.RequestRefresh();
  }

  public void _(Events.RowDeleted<CSAddedAttribute> e)
  {
    CSAddedAttribute row = e.Row;
    if (this.Attribute.SelectSingle((object) row.AttributeID) == null)
      return;
    PXSelect<CSScreenAttribute, Where<CSScreenAttribute.screenID, Equal<Current<AttribParams.screenID>>, And<CSScreenAttribute.attributeID, Equal<Required<CSAddedAttribute.attributeID>>>>> screenAttributes = this.ScreenAttributes;
    object[] objArray = new object[1]
    {
      (object) row.AttributeID
    };
    foreach (CSScreenAttribute csScreenAttribute in screenAttributes.Select<CSScreenAttribute>(objArray))
      this.ScreenAttributes.Delete(csScreenAttribute);
  }

  public void _(
    Events.FieldSelecting<CSScreenAttributeProperties.defaultValue> e)
  {
    CSScreenAttributeProperties row = (CSScreenAttributeProperties) e.Row;
    if (this.AddedUDFs.Current == null || row == null)
    {
      e.Cancel = true;
    }
    else
    {
      CSAttribute attribute = this.Attribute.SelectSingle((object) row.AttributeID);
      CSAttributeDetail[] values = this.Values.Select<CSAttributeDetail>((object) row.AttributeID);
      PXFieldState state = KeyValueHelper.GetState(attribute, row, values);
      if (state == null)
        return;
      state.Value = e.ReturnValue;
      state.Name = "defaultValue";
      state.DisplayName = "Default Value";
      e.ReturnState = (object) state;
      e.Cancel = true;
    }
  }

  public IEnumerable screenAttributeProperties()
  {
    string attributeID = this.AddedUDFs.Current?.AttributeID;
    CSScreenAttributeProperties[] array = this.ScreenAttributeProperties.Cache.Inserted.Cast<CSScreenAttributeProperties>().Where<CSScreenAttributeProperties>((Func<CSScreenAttributeProperties, bool>) (attr => attr.AttributeID.Equals(attributeID, StringComparison.OrdinalIgnoreCase))).ToArray<CSScreenAttributeProperties>();
    if (((IEnumerable<CSScreenAttributeProperties>) array).Any<CSScreenAttributeProperties>())
    {
      CSScreenAttributeProperties[] attributePropertiesArray = array;
      for (int index = 0; index < attributePropertiesArray.Length; ++index)
        yield return (object) attributePropertiesArray[index];
      attributePropertiesArray = (CSScreenAttributeProperties[]) null;
    }
    else if (this.AddedUDFs.Select().Any<PXResult<CSAddedAttribute>>())
    {
      CSScreenAttribute[] screenAttributes = this.CurrentScreenAttributes.Select<CSScreenAttribute>();
      CSScreenAttribute defaultScreenAttribute = ((IEnumerable<CSScreenAttribute>) screenAttributes).FirstOrDefault<CSScreenAttribute>((Func<CSScreenAttribute, bool>) (attr => string.IsNullOrEmpty(attr.TypeValue)));
      if (defaultScreenAttribute != null)
      {
        if (this._typeValues == null)
        {
          CSScreenAttributeProperties attributeProperties = new CSScreenAttributeProperties()
          {
            AttributeID = defaultScreenAttribute.AttributeID,
            TypeValue = string.Empty,
            Hidden = defaultScreenAttribute.Hidden,
            Required = defaultScreenAttribute.Required,
            DefaultValue = defaultScreenAttribute.DefaultValue
          };
          this.ScreenAttributeProperties.Insert(attributeProperties);
          yield return (object) attributeProperties;
        }
        else
        {
          foreach (string typeValue1 in this._typeValues)
          {
            string typeValue = typeValue1;
            CSScreenAttribute csScreenAttribute = ((IEnumerable<CSScreenAttribute>) screenAttributes).FirstOrDefault<CSScreenAttribute>((Func<CSScreenAttribute, bool>) (attr => typeValue.Equals(attr.TypeValue, StringComparison.OrdinalIgnoreCase)));
            CSScreenAttributeProperties attributeProperties1;
            if (csScreenAttribute != null)
            {
              attributeProperties1 = new CSScreenAttributeProperties()
              {
                AttributeID = csScreenAttribute.AttributeID,
                TypeValue = csScreenAttribute.TypeValue,
                Hidden = csScreenAttribute.Hidden,
                Required = csScreenAttribute.Required,
                DefaultValue = csScreenAttribute.DefaultValue
              };
            }
            else
            {
              attributeProperties1 = new CSScreenAttributeProperties();
              attributeProperties1.AttributeID = defaultScreenAttribute.AttributeID;
              attributeProperties1.TypeValue = typeValue;
              attributeProperties1.Hidden = defaultScreenAttribute.Hidden;
              attributeProperties1.Required = defaultScreenAttribute.Required;
              attributeProperties1.DefaultValue = defaultScreenAttribute.DefaultValue;
            }
            CSScreenAttributeProperties attributeProperties2 = attributeProperties1;
            this.ScreenAttributeProperties.Insert(attributeProperties2);
            yield return (object) attributeProperties2;
          }
        }
      }
    }
  }

  public void _(
    Events.FieldUpdating<CSScreenAttributeProperties.required> e)
  {
    bool? newValue = (bool?) e.NewValue;
    bool? hidden = ((CSScreenAttributeProperties) e.Row).Hidden;
    bool? nullable1 = newValue;
    bool flag1 = true;
    if (!(nullable1.GetValueOrDefault() == flag1 & nullable1.HasValue))
      return;
    bool? nullable2 = hidden;
    bool flag2 = true;
    if (!(nullable2.GetValueOrDefault() == flag2 & nullable2.HasValue))
      return;
    e.Cancel = true;
    e.NewValue = (object) false;
  }

  public void _(
    Events.FieldUpdating<CSScreenAttributeProperties.hidden> e)
  {
    bool? newValue = (bool?) e.NewValue;
    bool? required = ((CSScreenAttributeProperties) e.Row).Required;
    bool flag1 = true;
    if (!(required.GetValueOrDefault() == flag1 & required.HasValue))
      return;
    bool? nullable = newValue;
    bool flag2 = true;
    if (!(nullable.GetValueOrDefault() == flag2 & nullable.HasValue))
      return;
    e.Cache.SetValueExt<CSScreenAttributeProperties.required>(e.Row, (object) false);
  }

  public void _(Events.RowUpdated<CSScreenAttributeProperties> e)
  {
    CSScreenAttributeProperties row = e.Row;
    CSScreenAttributeProperties oldRow = e.OldRow;
    CSScreenAttribute[] source = this.ScreenAttributes.Select<CSScreenAttribute>((object) row.AttributeID);
    CSScreenAttribute row1 = ((IEnumerable<CSScreenAttribute>) source).FirstOrDefault<CSScreenAttribute>((Func<CSScreenAttribute, bool>) (attr => row.TypeValue.Equals(attr.TypeValue, StringComparison.OrdinalIgnoreCase)));
    bool flag = false;
    if (row1 == null)
    {
      flag = true;
      CSScreenAttribute csScreenAttribute = ((IEnumerable<CSScreenAttribute>) source).First<CSScreenAttribute>((Func<CSScreenAttribute, bool>) (attr => string.IsNullOrEmpty(attr.TypeValue)));
      row1 = new CSScreenAttribute()
      {
        AttributeID = csScreenAttribute.AttributeID,
        TypeValue = row.TypeValue,
        ScreenID = csScreenAttribute.ScreenID,
        Column = csScreenAttribute.Column,
        Row = csScreenAttribute.Row
      };
    }
    row1.Required = row.Required;
    row1.Hidden = row.Hidden;
    row1.DefaultValue = row.DefaultValue;
    if (flag)
      this.ScreenAttributes.Insert(row1);
    else
      this.ScreenAttributes.Update(row1);
    if (string.Equals(Str.NullIfWhitespace(oldRow.DefaultValue), Str.NullIfWhitespace(row.DefaultValue), StringComparison.CurrentCulture))
      return;
    this.UpdateUdfsForOtherScreens(row1);
  }

  private void UpdateUdfsForOtherScreens(CSScreenAttribute row)
  {
    foreach (string str in this.GetScreensWithTheSameCacheType(row.ScreenID))
    {
      CSScreenAttribute csScreenAttribute1 = this.UdfTypedAttribute.SelectSingle((object) str, (object) row.AttributeID);
      if (csScreenAttribute1 != null)
      {
        csScreenAttribute1.DefaultValue = row.DefaultValue;
        this.UdfTypedAttribute.Update(csScreenAttribute1);
      }
      else
      {
        CSScreenAttribute csScreenAttribute2 = this.UdfAttributeWithoutType.SelectSingle((object) str, (object) row.AttributeID);
        csScreenAttribute2.TypeValue = row.TypeValue;
        csScreenAttribute2.DefaultValue = row.DefaultValue;
        this.UdfTypedAttribute.Insert(csScreenAttribute2);
      }
    }
  }

  private List<string> GetScreensWithTheSameCacheType(string screenID)
  {
    System.Type cacheType = GraphHelper.GetPrimaryCache(PXSiteMap.Provider.FindSiteMapNodeByScreenID(screenID).GraphType)?.CacheType;
    if (cacheType == (System.Type) null)
      return new List<string>();
    List<string> theSameCacheType = KeyValueHelper.Def?.GetScreensWithAttributesForTable(cacheType) ?? new List<string>();
    theSameCacheType.Remove(screenID);
    return theSameCacheType;
  }

  [PXButton]
  [PXUIField(DisplayName = "Apply")]
  public IEnumerable apply(PXAdapter adapter)
  {
    HashSet<string> hashSet = this.Caches["CSScreenAttribute"].Deleted.Cast<CSScreenAttribute>().Select<CSScreenAttribute, string>((Func<CSScreenAttribute, string>) (a => a.AttributeID)).ToHashSet<string>();
    this.Caches["CSAddedAttribute"].Clear();
    this.Caches["CSScreenAttributeProperties"].Clear();
    this.Persist();
    if (hashSet.Any<string>())
      this.DeleteAttributeValues((IEnumerable<string>) hashSet);
    return adapter.Get();
  }

  private void DeleteAttributeValues(IEnumerable<string> attributeIDs)
  {
    using (PXTransactionScope transactionScope = new PXTransactionScope())
    {
      foreach (string attributeId in attributeIDs)
        this.DeleteUdfValues("Attribute" + attributeId);
      transactionScope.Complete();
    }
  }

  [PXButton]
  [PXUIField(DisplayName = "Cancel")]
  public IEnumerable cancelUdfModification(PXAdapter adapter)
  {
    this.Caches["CSAddedAttribute"].Clear();
    this.Caches["CSScreenAttributeProperties"].Clear();
    this.Caches["CSScreenAttribute"].Clear();
    return adapter.Get();
  }

  protected override string GetNameOfTableContainingUDF()
  {
    return this.GetNameOfTableContainingUDF(this._primaryCache);
  }

  public override int ExecuteUpdate(
    string viewName,
    IDictionary keys,
    IDictionary values,
    params object[] parameters)
  {
    switch (viewName)
    {
      case "AddedUDFs":
        this.UpdateAttribute(keys[(object) "AttributeID"] as string, values[(object) "AttributeID"] as string, values[(object) "Description"] as string);
        return 1;
      case "ScreenAttributeProperties":
        this.UpdateProperties(values);
        return 1;
      default:
        return base.ExecuteUpdate(viewName, keys, values, parameters);
    }
  }

  public void UpdateAttribute(string attributeID, string newAttributeID, string description)
  {
    PXCache cache = this.AddedUDFs.Cache;
    CSAddedAttribute current = this.AddedUDFs.Current;
    if (string.IsNullOrEmpty(attributeID))
      this.AddedUDFs.Delete(this.AddedUDFs.Current);
    else if (!string.Equals(attributeID, newAttributeID, StringComparison.OrdinalIgnoreCase))
      cache.SetValueExt<CSAddedAttribute.attributeID>((object) current, (object) newAttributeID);
    else if (string.IsNullOrEmpty(description))
    {
      if (((IEnumerable<CSAddedAttribute>) this.AddedUDFs.Select<CSAddedAttribute>()).Any<CSAddedAttribute>((Func<CSAddedAttribute, bool>) (attribute => string.Equals(attributeID, attribute.AttributeID, StringComparison.OrdinalIgnoreCase))))
        throw new ArgumentException(PXLocalizer.LocalizeFormat("The {0} attribute is already in use.", (object) attributeID));
      this.AddedUDFs.Insert(new CSAddedAttribute()
      {
        AttributeID = attributeID
      });
    }
    else
      cache.SetValueExt<CSAddedAttribute.attributeID>((object) current, (object) attributeID);
  }

  public void UpdateProperties(IDictionary values)
  {
    PXCache cache = this.ScreenAttributeProperties.Cache;
    IEnumerable<CSScreenAttributeProperties> source = cache.Inserted.Cast<CSScreenAttributeProperties>();
    string typeValue = values[(object) "TypeValue"] as string;
    string attributeID = values[(object) "AttributeID"] as string;
    Func<CSScreenAttributeProperties, bool> predicate = (Func<CSScreenAttributeProperties, bool>) (p => p.TypeValue.Equals(typeValue, StringComparison.OrdinalIgnoreCase) && p.AttributeID.Equals(attributeID, StringComparison.OrdinalIgnoreCase));
    CSScreenAttributeProperties attributeProperties = source.First<CSScreenAttributeProperties>(predicate);
    object copy = cache.CreateCopy((object) attributeProperties);
    if (values[(object) "Hidden"] is bool flag1)
      cache.SetValueExt<CSScreenAttributeProperties.hidden>((object) attributeProperties, (object) flag1);
    if (values[(object) "Required"] is bool flag2)
      cache.SetValueExt<CSScreenAttributeProperties.required>((object) attributeProperties, (object) flag2);
    object obj = values[(object) "DefaultValue"];
    bool flag3;
    switch (obj)
    {
      case string _:
      case null:
        flag3 = true;
        break;
      default:
        flag3 = false;
        break;
    }
    if (flag3)
      cache.SetValueExt<CSScreenAttributeProperties.defaultValue>((object) attributeProperties, obj);
    cache.RaiseRowUpdated((object) attributeProperties, copy);
    this.ScreenAttributeProperties.View.RequestRefresh();
  }
}
