// Decompiled with JetBrains decompiler
// Type: PX.Data.PXDimensionSelectorAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

#nullable disable
namespace PX.Data;

/// <summary>Sets up the lookup control for a DAC field that holds a
/// segmented key value or references a data record identified by a
/// segmented key. The attribute combines the <tt>PXDimension</tt> and
/// <tt>PXSelector</tt> attributes.</summary>
/// <example><para>The attribute below sets up the control for input of the BIZACCT segmented key's values. Since the AcctCD field itself is specified as the substitute key it will keep the segmented key value.</para>
/// <code title="Example" lang="CS">
/// [PXDimensionSelector(
///     "BIZACCT",
///     typeof(BAccount.acctCD),   // BQL query for lookup
///     typeof(BAccount.acctCD))]  // Substitute key
/// public virtual string AcctCD { get; set; }</code>
/// <code title="Example2" description="In the following example the RunRateItemID field references the data records from" groupname="Example" lang="CS">
/// [PXDimensionSelector(
///     InventoryAttribute.DimensionName,
///     typeof(
///         Search&lt;InventoryItem.inventoryID,
///             Where&lt;InventoryItem.itemType, Equal&lt;INItemTypes.nonStockItem&gt;,
///                 And&lt;Match&lt;Current&lt;AccessInfo.userName&gt;&gt;&gt;&gt;&gt;),
///     typeof(InventoryItem.inventoryCD),
///     DescriptionField = typeof(InventoryItem.descr))]
/// public virtual int? RunRateItemID { get; set; }</code>
/// </example>
[PXAttributeFamily(typeof (PXSelectorAttribute))]
public class PXDimensionSelectorAttribute : 
  PXAggregateAttribute,
  IPXFieldVerifyingSubscriber,
  IPXRowPersistingSubscriber,
  IPXRowPersistedSubscriber,
  IPXDependsOnFields
{
  protected object _KeyToAbort;
  protected Dictionary<object, object> _Persisted;
  protected Dictionary<object, object> _SubstitutePersisted;
  protected System.Type _keyType;
  protected object _JustPersistedKey;
  protected bool _SuppressViewCreation;
  protected System.Type _SubstituteKey;
  protected bool _DirtyRead;
  private int _SelectorAttributeId = -1;
  private int _DimensionAttributeId = -1;

  protected PXSelectorAttribute SelectorAttribute
  {
    get => (PXSelectorAttribute) this._Attributes[this._SelectorAttributeId];
  }

  protected PXDimensionAttribute DimensionAttribute
  {
    get => (PXDimensionAttribute) this._Attributes[this._DimensionAttributeId];
  }

  public bool SuppressViewCreation => this._SuppressViewCreation;

  /// <exclude />
  public virtual void SetSegmentDelegate(Delegate handler)
  {
    this.DimensionAttribute.SetSegmentDelegate(handler);
  }

  /// <exclude />
  public IEnumerable SegmentSelect([PXShort] short? segment, [PXString] string value)
  {
    return this.DimensionAttribute.SegmentSelect(segment, value);
  }

  /// <summary>Gets or sets the value that indicates whether only the values
  /// from the combobox are allowed in segments.</summary>
  public virtual bool ValidComboRequired
  {
    get => this.DimensionAttribute.ValidComboRequired;
    set => this.DimensionAttribute.ValidComboRequired = value;
  }

  /// <summary>Gets or sets the field from the referenced table that
  /// contains the description.</summary>
  public virtual System.Type DescriptionField
  {
    get => this.SelectorAttribute.DescriptionField;
    set => this.SelectorAttribute.DescriptionField = value;
  }

  /// <summary>
  /// The name that is displayed in the user interface for the <see cref="P:PX.Data.PXDimensionSelectorAttribute.DescriptionField" /> field.
  /// </summary>
  public virtual string DescriptionDisplayName
  {
    get => this.SelectorAttribute.DescriptionDisplayName;
    set => this.SelectorAttribute.DescriptionDisplayName = value;
  }

  /// <summary>
  /// Gets or sets the type that is used as a key for saved filters.
  /// </summary>
  public virtual System.Type FilterEntity
  {
    get => this.SelectorAttribute.FilterEntity;
    set => this.SelectorAttribute.FilterEntity = value;
  }

  /// <summary>Gets or sets the value that indicates whether the attribute
  /// should cache the data records retrieved from the database to show in
  /// the lookup control. By default, the attribute does not cache the data
  /// records.</summary>
  public virtual bool CacheGlobal
  {
    get => this.SelectorAttribute.CacheGlobal;
    set => this.SelectorAttribute.CacheGlobal = value;
  }

  /// <summary>Gets or sets the value that indicates whether the filters
  /// defined by the user should be stored in the database.</summary>
  public virtual bool Filterable
  {
    get => this.SelectorAttribute.Filterable;
    set => this.SelectorAttribute.Filterable = value;
  }

  /// <summary>Gets or sets a value that indicates whether the attribute
  /// should take into account the unsaved modifications when displaying
  /// data records in control. If <tt>false</tt>, the data records are taken
  /// from the database and not merged with the cache object. If
  /// <tt>true</tt>, the data records are merged with the modification
  /// stored in the cache object.</summary>
  public virtual bool DirtyRead
  {
    get => this.SelectorAttribute.DirtyRead;
    set => this.SelectorAttribute.DirtyRead = this._DirtyRead = value;
  }

  /// <summary>Gets or sets the list of labels for column headers that are
  /// displayed in the lookup control. By default, the attribute uses
  /// display names of the fields.</summary>
  public virtual string[] Headers
  {
    get => this.SelectorAttribute.Headers;
    set => this.SelectorAttribute.Headers = value;
  }

  /// <summary>
  /// Gets or sets the value that determines the value displayed by
  /// the selector control in the UI and some aspects of
  /// attribute's behavior. You can assign a combination of
  /// <see cref="T:PX.Data.PXSelectorMode">PXSelectorMode</see> values joined
  /// by bitwise or ("|").
  /// </summary>
  public virtual PXSelectorMode SelectorMode
  {
    get => this.SelectorAttribute.SelectorMode;
    set => this.SelectorAttribute.SelectorMode = value;
  }

  /// <summary>Allows to control validation process.</summary>
  public virtual bool ValidateValue
  {
    get => this.SelectorAttribute.ValidateValue;
    set => this.SelectorAttribute.ValidateValue = value;
  }

  /// <summary>Gets the field that identifies a referenced data record
  /// (surrogate key) and is assigned to the field annotated with the
  /// <tt>PXSelector</tt> attribute. Typically, it is the first parameter of
  /// the BQL query passed to the attribute constructor.</summary>
  public virtual System.Type Field => this.SelectorAttribute.Field;

  /// <summary>
  /// Returns Bql command used for selection of referenced records.
  /// </summary>
  public BqlCommand GetSelect() => this.SelectorAttribute.GetSelect();

  public virtual string CustomMessageElementDoesntExist
  {
    get => this.SelectorAttribute.CustomMessageElementDoesntExist;
    set => this.SelectorAttribute.CustomMessageElementDoesntExist = value;
  }

  public virtual string CustomMessageValueDoesntExist
  {
    get => this.SelectorAttribute.CustomMessageValueDoesntExist;
    set => this.SelectorAttribute.CustomMessageValueDoesntExist = value;
  }

  public virtual string CustomMessageElementDoesntExistOrNoRights
  {
    get => this.SelectorAttribute.CustomMessageElementDoesntExistOrNoRights;
    set => this.SelectorAttribute.CustomMessageElementDoesntExistOrNoRights = value;
  }

  public virtual string CustomMessageValueDoesntExistOrNoRights
  {
    get => this.SelectorAttribute.CustomMessageValueDoesntExistOrNoRights;
    set => this.SelectorAttribute.CustomMessageValueDoesntExistOrNoRights = value;
  }

  public virtual bool SupportNewValues { get; set; }

  protected PXDimensionSelectorAttribute(string dimension)
  {
    this._Attributes.Add((PXEventSubscriberAttribute) new PXDimensionAttribute(dimension));
    this._DimensionAttributeId = this._Attributes.Count - 1;
  }

  /// <summary>Initializes a new instance to reference the data records that
  /// are identified by the specified segmented key. Uses the provided BQL
  /// query to retrieve the data records.</summary>
  /// <param name="dimension">The string identifier of the segmented
  /// key.</param>
  /// <param name="type">A BQL query that defines the data set that is shown
  /// to the user along with the key field that is used as a value. Set to a
  /// field (type part of a DAC field) to select all data records from the
  /// referenced table. Set to a BQL command of <tt>Search</tt> type to
  /// specify a complex select statement.</param>
  public PXDimensionSelectorAttribute(string dimension, System.Type type)
    : this(dimension)
  {
    this.RegisterSelector(new PXSelectorAttribute(type));
  }

  /// <summary>Initializes a new instance to reference the data records that
  /// are identified by the specified segmented key. Uses the provided BQL
  /// query to retrieve the data records and substitutes the field value
  /// (surrogate key) with the provided field (natural key).</summary>
  /// <param name="dimension">The string identifier of the segmented
  /// key.</param>
  /// <param name="type">A BQL query that defines the data set that is shown
  /// to the user along with the key field that is used as a value. Set to a
  /// field (type part of a DAC field) to select all data records from the
  /// referenced table. Set to a BQL command of <tt>Search</tt> type to
  /// specify a complex select statement.</param>
  /// <param name="substituteKey">The field to sustitute the surrogate
  /// field's value in the user interface.</param>
  /// <example>
  /// <code>
  /// [PXDimensionSelector(
  ///     InventoryAttribute.DimensionName,
  ///     typeof(
  ///         Search&lt;InventoryItem.inventoryID,
  ///             Where&lt;InventoryItem.itemType, Equal&lt;INItemTypes.nonStockItem&gt;,
  ///                 And&lt;Match&lt;Current&lt;AccessInfo.userName&gt;&gt;&gt;&gt;&gt;),
  ///     typeof(InventoryItem.inventoryCD),
  ///     DescriptionField = typeof(InventoryItem.descr))]
  /// public virtual int? RunRateItemID { get; set; }
  /// </code>
  /// </example>
  public PXDimensionSelectorAttribute(string dimension, System.Type type, System.Type substituteKey)
    : this(dimension)
  {
    this.RegisterSelector(new PXSelectorAttribute(type)
    {
      SubstituteKey = substituteKey
    });
  }

  /// <summary>Initializes a new instance to reference the data records that
  /// are identified by the specified segmented key. Uses the provided BQL
  /// query to retrieve the data records and substitutes the field value
  /// (surrogate key) with the provided field (natural key).</summary>
  /// <param name="dimension">The string identifier of the segmented
  /// key.</param>
  /// <param name="type">A BQL query that defines the data set that is shown
  /// to the user along with the key field that is used as a value. Set to a
  /// field (type part of a DAC field) to select all data records from the
  /// referenced table. Set to a BQL command of <tt>Search</tt> type to
  /// specify a complex select statement.</param>
  /// <param name="substituteKey">The field to sustitute the surrogate
  /// field's value in the user interface.</param>
  /// <param name="fieldList">Fields to display in the control.</param>
  public PXDimensionSelectorAttribute(
    string dimension,
    System.Type type,
    System.Type substituteKey,
    params System.Type[] fieldList)
    : this(dimension)
  {
    this.RegisterSelector(new PXSelectorAttribute(type, fieldList)
    {
      SubstituteKey = substituteKey
    });
  }

  public PXDimensionSelectorAttribute(
    string dimension,
    System.Type search,
    System.Type lookupJoin,
    System.Type substituteKey,
    bool cacheGlobal,
    params System.Type[] fieldList)
    : this(dimension)
  {
    this.RegisterSelector(new PXSelectorAttribute(search, lookupJoin, cacheGlobal, fieldList)
    {
      SubstituteKey = substituteKey
    });
  }

  protected void RegisterSelector(PXSelectorAttribute selector)
  {
    this._SubstituteKey = selector.SubstituteKey;
    this._Attributes.Add((PXEventSubscriberAttribute) selector);
    this._SelectorAttributeId = this._Attributes.Count - 1;
  }

  /// <exclude />
  public virtual void FieldUpdating(PXCache sender, PXFieldUpdatingEventArgs e)
  {
    if (this.SelectorAttribute._BypassFieldVerifying.Value)
      return;
    new PXFieldUpdating(this.DimensionAttribute.FieldUpdating)(sender, e);
    e.Cancel = false;
    if (this._SubstituteKey == (System.Type) null || this._BqlTable.IsAssignableFrom(BqlCommand.GetItemType(this._SubstituteKey)) && string.Compare(this._SubstituteKey.Name, this._FieldName, StringComparison.OrdinalIgnoreCase) == 0)
    {
      e.Cancel = true;
    }
    else
    {
      try
      {
        new PXFieldUpdating(this.SelectorAttribute.SubstituteKeyFieldUpdating)(sender, e);
      }
      catch (Exception ex)
      {
        switch (ex)
        {
          case PXForeignRecordDeletedException _:
          case PXSetPropertyException _:
            if (this.DimensionAttribute.ValidComboRequired)
              throw;
            System.Type field = this.SelectorAttribute.Field;
            PXCache cach = sender.Graph.Caches[BqlCommand.GetItemType(field)];
            Dictionary<string, object> dictionary = new Dictionary<string, object>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase)
            {
              [this._SubstituteKey.Name] = e.NewValue,
              [field.Name] = (object) null
            };
            bool isDirty = cach.IsDirty;
            bool insertRights = cach.InsertRights;
            bool allowInsert = cach.AllowInsert;
            cach.InsertRights = true;
            cach.AllowInsert = true;
            try
            {
              if (ex is PXForeignRecordDeletedException && cach.Locate((IDictionary) dictionary) > 0)
                cach.Remove(cach.Current);
              if (cach.Insert((IDictionary) dictionary) > 0)
              {
                cach.IsDirty = isDirty;
                if (dictionary[this._SubstituteKey.Name] is PXFieldState pxFieldState1 && !string.IsNullOrEmpty(pxFieldState1.Error))
                {
                  object objA = (object) (dictionary[field.Name] as PXFieldState) ?? dictionary[field.Name];
                  foreach (object data in cach.Inserted)
                  {
                    if (object.Equals(objA, cach.GetValue(data, field.Name)))
                    {
                      cach.Delete(data);
                      break;
                    }
                  }
                  throw new PXSetPropertyException(pxFieldState1.Error);
                }
                if (dictionary[field.Name] is PXFieldState pxFieldState2)
                {
                  e.NewValue = pxFieldState2.Value;
                  break;
                }
                if (dictionary[field.Name] != null)
                {
                  e.NewValue = dictionary[field.Name];
                  break;
                }
                throw;
              }
              throw;
            }
            catch
            {
              cach.IsDirty = isDirty;
              cach.AllowInsert = allowInsert;
              cach.InsertRights = insertRights;
              throw;
            }
          default:
            throw;
        }
      }
    }
  }

  /// <exclude />
  public virtual void FieldSelecting(PXCache sender, PXFieldSelectingEventArgs e)
  {
    this.FieldSelecting(sender, e, this._SuppressViewCreation, this.ValidComboRequired);
  }

  public virtual void FieldSelecting(
    PXCache sender,
    PXFieldSelectingEventArgs e,
    bool bypassViewCreation,
    bool validComboRequired)
  {
    if (!e.IsAltered)
      e.IsAltered = sender.HasAttributes(e.Row);
    if (this._SubstituteKey != (System.Type) null && (!this._BqlTable.IsAssignableFrom(BqlCommand.GetItemType(this._SubstituteKey)) || string.Compare(this._SubstituteKey.Name, this._FieldName, StringComparison.OrdinalIgnoreCase) != 0))
      new PXFieldSelecting(this.SelectorAttribute.SubstituteKeyFieldSelecting)(sender, e);
    PXFieldSelecting pxFieldSelecting1 = new PXFieldSelecting(this.DimensionAttribute.FieldSelecting);
    if (e.ReturnState is PXFieldState)
      ((PXFieldState) e.ReturnState).ViewName = (string) null;
    pxFieldSelecting1(sender, e);
    PXFieldSelecting pxFieldSelecting2 = new PXFieldSelecting(this.SelectorAttribute.FieldSelecting);
    PXView pxView1 = (PXView) null;
    if (validComboRequired)
    {
      pxFieldSelecting2(sender, e);
      if (!(this.SelectorAttribute.DescriptionField == (System.Type) null) || !(e.ReturnState is PXFieldState))
        return;
      ((PXFieldState) e.ReturnState).DescriptionName = (string) null;
    }
    else
    {
      if (e.Row != null && !sender.HasAttributes(e.Row) && (!e.IsAltered || sender.SecuredFields.Contains(this._FieldName.ToLower())))
        return;
      PXFieldSelectingEventArgs args = new PXFieldSelectingEventArgs((object) null, (object) null, true, true);
      pxFieldSelecting2(sender, args);
      PXSegmentedState returnState1 = (PXSegmentedState) e.ReturnState;
      PXFieldState returnState2 = (PXFieldState) args.ReturnState;
      returnState1.ViewName = returnState2.ViewName;
      returnState1.FieldList = returnState2.FieldList;
      returnState1.HeaderList = returnState2.HeaderList;
      returnState1.ValueField = returnState2.ValueField;
      returnState1.DescriptionName = returnState2.DescriptionName;
      PXView view = sender.Graph.Views[returnState1.ViewName];
      BqlCommand select = (BqlCommand) null;
      foreach (IBqlParameter parameter in view.BqlSelect.GetParameters())
      {
        if (parameter.MaskedType != (System.Type) null)
        {
          if (select == null)
          {
            pxView1 = sender.Graph.Views[returnState1.SegmentViewName];
            select = pxView1.BqlSelect;
          }
          if (parameter.NullAllowed)
            select = select.WhereAnd(BqlCommand.Compose(typeof (Where<,,>), parameter.HasDefault ? (parameter.IsVisible ? typeof (Optional<>) : typeof (Current<>)) : typeof (Required<>), parameter.GetReferencedType(), typeof (IsNull), typeof (Or<>), typeof (PX.Data.Match<>), parameter.HasDefault ? (parameter.IsVisible ? typeof (Optional<>) : typeof (Current<>)) : typeof (Required<>), parameter.GetReferencedType()));
          else
            select = select.WhereAnd(BqlCommand.Compose(typeof (Where<>), typeof (PX.Data.Match<>), parameter.HasDefault ? (parameter.IsVisible ? typeof (Optional<>) : typeof (Current<>)) : typeof (Required<>), parameter.GetReferencedType()));
        }
      }
      if (select == null)
        return;
      if (!bypassViewCreation)
      {
        string key = $"__{sender.GetItemType().Name}{this._FieldName}{returnState1.SegmentViewName}";
        sender.Graph.Views[key] = (PXView) new PXDimensionSelectorAttribute.segmentView(sender.Graph, true, select, this.DimensionAttribute);
        PXView pxView2;
        if (sender.Graph.Views.TryGetValue(returnState1.ViewName + "$FilterHeader", out pxView2))
        {
          sender.Graph.Views[key + "$FilterHeader"] = pxView2;
          if (sender.Graph.Views.TryGetValue(returnState1.ViewName + "$FilterRow", out pxView2))
            sender.Graph.Views[key + "$FilterRow"] = pxView2;
        }
        ((PXSegmentedState) e.ReturnState).SegmentViewName = key;
      }
      else
      {
        List<GroupHelper.ParamsPair[]> paramsPairArrayList = (List<GroupHelper.ParamsPair[]>) null;
        IBqlParameter[] parameters = select.GetParameters();
        System.Type table = select.GetTables()[0];
        for (int index1 = 0; index1 < parameters.Length; ++index1)
        {
          if (parameters[index1].MaskedType != (System.Type) null)
          {
            object newValue = (object) null;
            System.Type referencedType = parameters[index1].GetReferencedType();
            if (parameters[index1].HasDefault && referencedType.IsNested)
            {
              System.Type itemType = BqlCommand.GetItemType(referencedType);
              PXCache cache = itemType == table ? pxView1.Cache : sender.Graph.Caches[itemType];
              object data = e.Row == null || !(e.Row.GetType() == itemType) && !e.Row.GetType().IsSubclassOf(itemType) ? cache.Current : e.Row;
              if (data != null)
                newValue = cache.GetValue(data, referencedType.Name);
              if (newValue == null && parameters[index1].TryDefault && cache.RaiseFieldDefaulting(referencedType.Name, (object) null, out newValue))
                cache.RaiseFieldUpdating(referencedType.Name, (object) null, ref newValue);
              newValue = GroupHelper.GetReferencedValue(cache, data, referencedType.Name, newValue, false);
            }
            if (newValue == null)
            {
              newValue = (object) new byte[(GroupHelper.Count + 7) / 8];
              for (int index2 = 0; index2 < ((byte[]) newValue).Length; ++index2)
                ((byte[]) newValue)[index2] = byte.MaxValue;
            }
            if (paramsPairArrayList == null)
              paramsPairArrayList = new List<GroupHelper.ParamsPair[]>();
            System.Type type = !referencedType.IsNested || BqlCommand.GetItemType(referencedType) == table ? GroupHelper.GetReferencedType(pxView1.Cache, referencedType.Name) : GroupHelper.GetReferencedType(sender.Graph.Caches[BqlCommand.GetItemType(referencedType)], referencedType.Name);
            paramsPairArrayList.Add(GroupHelper.GetParams(type, GroupHelper.FindRestricted(type, "SegmentValue"), newValue as byte[]));
          }
        }
        if (paramsPairArrayList == null)
          return;
        this.DimensionAttribute.Restrictions = paramsPairArrayList.ToArray();
      }
    }
  }

  /// <exclude />
  public virtual void FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
  {
    List<IPXFieldVerifyingSubscriber> subscribers = new List<IPXFieldVerifyingSubscriber>();
    this.DimensionAttribute.GetSubscriber<IPXFieldVerifyingSubscriber>(subscribers);
    if (subscribers.Count > 0)
    {
      PXFieldSelectingEventArgs e1 = new PXFieldSelectingEventArgs(e.Row, e.NewValue, !this.ValidComboRequired, true);
      this.FieldSelecting(sender, e1, true, this.ValidComboRequired);
      try
      {
        bool flag1 = object.Equals(e1.ReturnValue, e.NewValue);
        PXFieldVerifyingEventArgs e2 = new PXFieldVerifyingEventArgs(e.Row, e1.ReturnValue, e.ExternalCall);
        for (int index = 0; index < subscribers.Count; ++index)
          subscribers[index].FieldVerifying(sender, e2);
        if (flag1)
          e.NewValue = e2.NewValue;
        PXSetPropertyException propertyException = (PXSetPropertyException) null;
        object obj1 = (object) null;
        try
        {
          this.SelectorAttribute.FieldVerifying(sender, e);
        }
        catch (PXSetPropertyException ex)
        {
          object obj2 = (object) null;
          bool flag2 = false;
          if (this._SubstitutePersisted != null && e.NewValue != null)
          {
            flag2 = this._SubstitutePersisted.TryGetValue(e.NewValue, out obj2);
            int result;
            if (!flag2 && this._keyType == typeof (int) && int.TryParse(e.NewValue as string, NumberStyles.Integer, (IFormatProvider) CultureInfo.InvariantCulture, out result))
            {
              e.NewValue = (object) result;
              flag2 = this._SubstitutePersisted.TryGetValue(e.NewValue, out obj2);
            }
          }
          if (flag2)
          {
            propertyException = ex;
            obj1 = e.NewValue;
            e.NewValue = obj2;
          }
          else
            throw;
        }
        if (propertyException == null)
          return;
        try
        {
          this.SelectorAttribute.FieldVerifying(sender, e);
        }
        catch
        {
          throw propertyException;
        }
        finally
        {
          e.NewValue = obj1;
        }
      }
      catch (PXSetPropertyException ex)
      {
        object returnValue = e1.ReturnValue;
        ex.ErrorValue = returnValue;
        throw ex;
      }
    }
    else
      this.SelectorAttribute.FieldVerifying(sender, e);
  }

  /// <exclude />
  public virtual void RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    if (this._SubstituteKey == (System.Type) null || this._BqlTable.IsAssignableFrom(BqlCommand.GetItemType(this._SubstituteKey)) && string.Compare(this._SubstituteKey.Name, this._FieldName, StringComparison.OrdinalIgnoreCase) == 0)
      return;
    object obj1 = sender.GetValue(e.Row, this._FieldOrdinal);
    if (obj1 == null || Convert.ToInt32(obj1) >= 0)
      return;
    System.Type field = this.SelectorAttribute.Field;
    PXCache cach = sender.Graph.Caches[BqlCommand.GetItemType(field)];
    this._KeyToAbort = obj1;
    object objB;
    if (this._Persisted.TryGetValue(obj1, out objB))
    {
      sender.SetValue(e.Row, this._FieldOrdinal, objB);
    }
    else
    {
      bool flag = false;
      foreach (object obj2 in cach.Inserted)
      {
        if (object.Equals(obj1, cach.GetValue(obj2, field.Name)))
        {
          try
          {
            cach.PersistInserted(obj2);
          }
          catch
          {
          }
          objB = cach.GetValue(obj2, field.Name);
          if (objB != null)
          {
            if (Convert.ToInt32(objB) > 0)
            {
              sender.SetValue(e.Row, this._FieldOrdinal, objB);
              if (!object.Equals(this._KeyToAbort, objB))
                this._Persisted[this._KeyToAbort] = objB;
              flag = true;
              break;
            }
            break;
          }
          break;
        }
      }
      if (flag)
        return;
      if (this._SubstitutePersisted.TryGetValue(obj1, out objB))
        sender.SetValue(e.Row, this._FieldOrdinal, objB);
      else
        sender.SetValue(e.Row, this._FieldOrdinal, (object) null);
    }
  }

  /// <exclude />
  public virtual void RowPersisted(PXCache sender, PXRowPersistedEventArgs e)
  {
    if (this._KeyToAbort == null || e.TranStatus == PXTranStatus.Open)
      return;
    if (e.TranStatus == PXTranStatus.Aborted)
    {
      object objA;
      if (this._Persisted.TryGetValue(this._KeyToAbort, out objA))
      {
        System.Type field = this.SelectorAttribute.Field;
        PXCache cach = sender.Graph.Caches[BqlCommand.GetItemType(field)];
        foreach (object data in cach.Inserted)
        {
          if (object.Equals(objA, cach.GetValue(data, field.Name)))
          {
            try
            {
              cach.RaiseRowPersisted(data, PXDBOperation.Insert, PXTranStatus.Aborted, (Exception) null);
              break;
            }
            catch
            {
              break;
            }
          }
        }
      }
      sender.SetValue(e.Row, this._FieldOrdinal, this._KeyToAbort);
    }
    this._Persisted.Remove(this._KeyToAbort);
    this._KeyToAbort = (object) null;
  }

  /// <exclude />
  public virtual void SubstituteRowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    if ((e.Operation & PXDBOperation.Delete) != PXDBOperation.Insert)
      return;
    System.Type field = this.SelectorAttribute.Field;
    this._JustPersistedKey = sender.GetValue(e.Row, field.Name);
  }

  /// <exclude />
  public virtual void SubstituteRowPersisted(PXCache sender, PXRowPersistedEventArgs e)
  {
    if ((e.Operation & PXDBOperation.Delete) != PXDBOperation.Insert || e.TranStatus != PXTranStatus.Open || this._JustPersistedKey == null)
      return;
    System.Type field = this.SelectorAttribute.Field;
    object obj = sender.GetValue(e.Row, field.Name);
    this._SubstitutePersisted[this._JustPersistedKey] = obj;
    if (!(this._keyType == (System.Type) null))
      return;
    this._keyType = obj.GetType();
  }

  public ISet<System.Type> GetDependencies(PXCache cache)
  {
    return this.SelectorAttribute.GetDependencies(cache);
  }

  protected void resetValidCombos(PXCache sender, bool oldRequired, bool newRequired)
  {
    if (this._SubstituteKey == (System.Type) null)
      return;
    if (oldRequired && !newRequired)
    {
      System.Type itemType;
      sender.Graph.RowPersisting.AddHandler(itemType = BqlCommand.GetItemType(this._SubstituteKey), new PXRowPersisting(this.SubstituteRowPersisting));
      sender.Graph.RowPersisted.AddHandler(itemType, new PXRowPersisted(this.SubstituteRowPersisted));
      if (sender.Graph.Views.Caches.Contains(itemType) || sender.Graph.Views.RestorableCaches.Contains(itemType))
        return;
      sender.Graph.Views.RestorableCaches.Add(itemType);
    }
    else
    {
      if (!(!oldRequired & newRequired))
        return;
      sender.Graph.RowPersisting.RemoveHandler(BqlCommand.GetItemType(this._SubstituteKey), new PXRowPersisting(this.SubstituteRowPersisting));
      sender.Graph.RowPersisted.RemoveHandler(BqlCommand.GetItemType(this._SubstituteKey), new PXRowPersisted(this.SubstituteRowPersisted));
    }
  }

  /// <exclude />
  public override void CacheAttached(PXCache sender)
  {
    this.DimensionAttribute.ParentSelect = this.SelectorAttribute.OriginalSelect.GetType();
    PXDimensionAttribute dimensionAttribute = this.DimensionAttribute;
    System.Type type = this._SubstituteKey;
    if ((object) type == null)
      type = this.SelectorAttribute.Field;
    dimensionAttribute.ParentValueField = type;
    base.CacheAttached(sender);
    string lower = this._FieldName.ToLower();
    sender.FieldUpdatingEvents[lower] -= new PXFieldUpdating(this.SelectorAttribute.SubstituteKeyFieldUpdating);
    sender.FieldUpdatingEvents[lower] += new PXFieldUpdating(this.FieldUpdating);
    sender.FieldSelectingEvents[lower] -= new PXFieldSelecting(this.SelectorAttribute.SubstituteKeyFieldSelecting);
    sender.FieldSelectingEvents[lower] += new PXFieldSelecting(this.FieldSelecting);
    if (!this._DirtyRead)
      this.SelectorAttribute.DirtyRead = !this.DimensionAttribute.ValidComboRequired;
    if (this.SelectorAttribute.DirtyRead || this.SupportNewValues)
      this.resetValidCombos(sender, true, false);
    this._Persisted = new Dictionary<object, object>();
    this._SubstitutePersisted = new Dictionary<object, object>();
  }

  /// <exclude />
  public override void GetSubscriber<ISubscriber>(List<ISubscriber> subscribers)
  {
    base.GetSubscriber<ISubscriber>(subscribers);
    if (typeof (ISubscriber) == typeof (IPXFieldVerifyingSubscriber))
    {
      if (this._SubstituteKey != (System.Type) null)
      {
        subscribers.Remove(this.DimensionAttribute as ISubscriber);
        subscribers.Remove(this.SelectorAttribute as ISubscriber);
      }
      else
        subscribers.Remove(this as ISubscriber);
    }
    else if (typeof (ISubscriber) == typeof (IPXFieldSelectingSubscriber))
    {
      subscribers.Remove(this.DimensionAttribute as ISubscriber);
      subscribers.Remove(this.SelectorAttribute as ISubscriber);
    }
    else if (typeof (ISubscriber) == typeof (IPXFieldUpdatingSubscriber))
    {
      subscribers.Remove(this.DimensionAttribute as ISubscriber);
    }
    else
    {
      if (!(this._SubstituteKey == (System.Type) null) && string.Compare(this._SubstituteKey.Name, this._FieldName, StringComparison.OrdinalIgnoreCase) == 0)
        return;
      if (typeof (ISubscriber) == typeof (IPXFieldDefaultingSubscriber))
        subscribers.Remove(this.DimensionAttribute as ISubscriber);
      else if (typeof (ISubscriber) == typeof (IPXRowPersistingSubscriber))
      {
        subscribers.Remove(this.DimensionAttribute as ISubscriber);
      }
      else
      {
        if (!(typeof (ISubscriber) == typeof (IPXRowPersistedSubscriber)))
          return;
        subscribers.Remove(this.DimensionAttribute as ISubscriber);
      }
    }
  }

  public static void SetValidCombo<Field>(PXCache cache, bool isRequired) where Field : IBqlField
  {
    cache.SetAltered<Field>(true);
    foreach (PXDimensionSelectorAttribute selectorAttribute in cache.GetAttributes<Field>().OfType<PXDimensionSelectorAttribute>())
    {
      bool validComboRequired = selectorAttribute.DimensionAttribute.ValidComboRequired;
      if (validComboRequired != isRequired)
      {
        selectorAttribute.resetValidCombos(cache, validComboRequired, isRequired);
        selectorAttribute.DimensionAttribute.ValidComboRequired = isRequired;
        selectorAttribute.SelectorAttribute.DirtyRead = !isRequired;
      }
    }
  }

  public static void SetValidCombo(PXCache cache, string name, bool isRequired)
  {
    cache.SetAltered(name, true);
    foreach (PXDimensionSelectorAttribute selectorAttribute in cache.GetAttributes(name).OfType<PXDimensionSelectorAttribute>())
    {
      bool validComboRequired = selectorAttribute.DimensionAttribute.ValidComboRequired;
      if (validComboRequired != isRequired)
      {
        selectorAttribute.resetValidCombos(cache, validComboRequired, isRequired);
        selectorAttribute.DimensionAttribute.ValidComboRequired = isRequired;
        selectorAttribute.SelectorAttribute.DirtyRead = !isRequired;
      }
    }
  }

  public static void SetSuppressViewCreation(PXCache cache)
  {
    foreach (PXDimensionSelectorAttribute selectorAttribute in cache.GetAttributes((string) null).OfType<PXDimensionSelectorAttribute>())
      selectorAttribute._SuppressViewCreation = true;
  }

  protected sealed class segmentView : PXView
  {
    private PXDimensionAttribute _Attribute;

    public segmentView(
      PXGraph graph,
      bool isReadonly,
      BqlCommand select,
      PXDimensionAttribute attribute)
      : base(graph, isReadonly, select, attribute.GetSegmentDelegate())
    {
      this._Attribute = attribute;
    }

    protected override List<object> InvokeDelegate(object[] parameters)
    {
      List<GroupHelper.ParamsPair[]> paramsPairArrayList = (List<GroupHelper.ParamsPair[]>) null;
      IBqlParameter[] parameters1 = this.BqlSelect.GetParameters();
      for (int index1 = 0; index1 < parameters1.Length; ++index1)
      {
        if (parameters1[index1].MaskedType != (System.Type) null && !parameters1[index1].IsArgument)
        {
          if (parameters[index1] == null)
          {
            if (!parameters1[index1].NullAllowed)
              return new List<object>();
            parameters[index1] = (object) new byte[(GroupHelper.Count + 7) / 8];
            for (int index2 = 0; index2 < ((byte[]) parameters[index1]).Length; ++index2)
              ((byte[]) parameters[index1])[index2] = byte.MaxValue;
          }
          if (paramsPairArrayList == null)
            paramsPairArrayList = new List<GroupHelper.ParamsPair[]>();
          System.Type referencedType = parameters1[index1].GetReferencedType();
          System.Type type = !referencedType.IsNested || BqlCommand.GetItemType(referencedType) == this._CacheType ? GroupHelper.GetReferencedType(this.Cache, referencedType.Name) : GroupHelper.GetReferencedType(this._Graph.Caches[BqlCommand.GetItemType(referencedType)], referencedType.Name);
          paramsPairArrayList.Add(GroupHelper.GetParams(type, GroupHelper.FindRestricted(type, "SegmentValue"), parameters[index1] as byte[]));
        }
      }
      if (paramsPairArrayList != null)
        this._Attribute.Restrictions = paramsPairArrayList.ToArray();
      return base.InvokeDelegate(parameters);
    }
  }
}
