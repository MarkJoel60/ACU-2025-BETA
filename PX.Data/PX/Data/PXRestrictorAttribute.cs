// Decompiled with JetBrains decompiler
// Type: PX.Data.PXRestrictorAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Data.Maintenance.GI;
using PX.Data.ReferentialIntegrity.Attributes;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

#nullable disable
namespace PX.Data;

/// <summary>Adds a restriction to a BQL command that selects data for a
/// lookup control and displays the error message when the value entered
/// does not fit the restriction.</summary>
/// <remarks>
/// <para>The attribute is used on DAC fields represented by lookup
/// controls in the user interface. For example, such fields can have the
/// <see cref="T:PX.Data.PXSelectorAttribute">PXSelector</see> attribute attached
/// to them. The attribute adds the <tt>Where&lt;&gt;</tt> clause to the
/// BQL command that selects data for the control. As a result, the
/// control lists the data records that satisfy the BQL command and the
/// new restriction. If the user enters a value that is not in the list,
/// the error message configured by the attribute is displayed.</para>
/// <para>A typical example of attribute's usage is specifying condition
/// that checks whether a referenced data record is active. This condition
/// could be specified in the <tt>PXSelector</tt> attribute. But in this
/// case, if an active data record once selected through the lookup
/// control becomes inactive, saving the data record that includes this
/// lookup field will result in an error. Adding the condition through
/// <tt>PXRestrictor</tt> attribute prevents this error. The lookup field
/// can still hold a reference to the inactive data record. However, the
/// new value can be selected only among the active data records.</para>
/// </remarks>
/// <example>
/// The code below shows the use of the attribute on a lookup field.
/// Notice that the message includes <i>{0}</i> that is replaced
/// with the value of the <tt>TaxCategoryID</tt> field when the error message
/// is displayed.
/// <code>
/// [PXDBString(10, IsUnicode = true)]
/// [PXUIField(DisplayName = "Tax Category")]
/// [PXSelector(
///     typeof(TaxCategory.taxCategoryID),
///     DescriptionField = typeof(TaxCategory.descr))]
/// [PXRestrictor(
///     typeof(Where&lt;TaxCategory.active, Equal&lt;True&gt;&gt;),
///     "Tax Category '{0}' is inactive",
///     typeof(TaxCategory.taxCategoryID))]
/// public virtual string TaxCategoryID { get; set; }</code>
/// </example>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Parameter, AllowMultiple = true)]
public class PXRestrictorAttribute : PXEventSubscriberAttribute, IPXFieldVerifyingSubscriber
{
  protected const string PlaceholderSign = "[";
  protected const string ScreenNamePlaceholder = "[ScreenName]";
  protected const string ScreenIDPlaceholder = "[ScreenID]";
  protected string _Message;
  protected System.Type[] _MsgParams;
  protected System.Type _Where;
  protected BqlCommand _OriginalCmd;
  protected BqlCommand _AlteredCmd;
  protected List<System.Type> _AlteredParams;
  protected System.Type _ValueField;
  protected System.Type _Type;
  protected bool _CacheGlobal;
  protected int _ParsCount;
  protected bool _DirtyRead;
  protected System.Type _SubstituteKey;
  protected bool _ShowWarning;
  protected int _RestrictLevel;
  protected int _ReplaceLevel = -1;
  private int? _TablesCount;
  protected Dictionary<System.Type, Tuple<BqlCommand, BqlCommand, List<System.Type>>> _altered = new Dictionary<System.Type, Tuple<BqlCommand, BqlCommand, List<System.Type>>>();
  private static ConcurrentDictionary<Tuple<System.Type, System.Type, System.Type>, Func<BqlCommand>> dict = new ConcurrentDictionary<Tuple<System.Type, System.Type, System.Type>, Func<BqlCommand>>();

  public IReadOnlyCollection<IFieldsRelation> AdditionalKeysRelationsArray { get; private set; }

  /// <summary>Gets or sets the value indicating whether the current
  /// <tt>PXRestrictor</tt> attribute should override the inherited
  /// <tt>PXRestrictor</tt> attributes.</summary>
  public bool ReplaceInherited
  {
    get => this._ReplaceLevel >= 0;
    set
    {
      if (!value)
        return;
      this._ReplaceLevel = 0;
      this._RestrictLevel = 1;
    }
  }

  public bool CacheGlobal
  {
    get => this._CacheGlobal;
    set => this._CacheGlobal = value;
  }

  public string Message
  {
    get => this._Message;
    set => this._Message = value;
  }

  /// <summary>Gets or sets the value indicating whether
  /// warning message will be displayed on a field if the
  /// referenced record does not satisfy <tt>PXRestrictor</tt>
  /// attribute condition.</summary>
  public bool ShowWarning
  {
    get => this._ShowWarning;
    set => this._ShowWarning = value;
  }

  /// <summary>Gets or sets the value indicating whether
  /// FieldVerifying event handler will be executed.</summary>
  public bool SuppressVerify { get; set; }

  public System.Type RestrictingCondition => this._Where;

  /// <summary>Initializes a new instance of the attribute.</summary>
  /// <param name="where">The <tt>Where&lt;&gt;</tt> BQL clause used as the
  /// additional restriction for a BQL command.</param>
  /// <param name="message">The error message that is displayed when a value
  /// violating the restriction is entered. The error message can reference the
  /// fields specified in the third parameter, as <i>{0}пїЅ{N}</i>. The attribute
  /// will take the values of these fields from the data record whose identifier
  /// was entered as the value of the current field.</param>
  /// <param name="pars">The types of fields that are referenced by the error message.</param>
  public PXRestrictorAttribute(System.Type where, string message, params System.Type[] pars)
  {
    this.SuppressVerify = false;
    if (where == (System.Type) null)
      throw new PXArgumentException(nameof (where), "The argument cannot be null.");
    this._Where = typeof (IBqlWhere).IsAssignableFrom(where) ? where : throw new PXArgumentException(nameof (where), "An invalid argument has been specified.");
    this._Message = message;
    this._MsgParams = !((IEnumerable<System.Type>) pars).Any<System.Type>((Func<System.Type, bool>) (par => !typeof (IBqlField).IsAssignableFrom(par))) ? pars : throw new PXArgumentException("params", "An invalid argument has been specified.");
  }

  /// <exclude />
  public override void CacheAttached(PXCache sender)
  {
    List<PXEventSubscriberAttribute> attributesReadonly = sender.GetAttributesReadonly(this._FieldName);
    if (attributesReadonly.OfType<PXRestrictorAttribute>().Any<PXRestrictorAttribute>((Func<PXRestrictorAttribute, bool>) (r => r._ReplaceLevel >= this._RestrictLevel)))
      return;
    using (IEnumerator<PXSelectorAttribute> enumerator = attributesReadonly.OfType<PXSelectorAttribute>().GetEnumerator())
    {
      if (enumerator.MoveNext())
      {
        PXSelectorAttribute current = enumerator.Current;
        this._OriginalCmd = current.PrimarySelect;
        this._AlteredCmd = this.WhereAnd(sender, current, this._Where);
        this._ValueField = current.ValueField;
        this._ParsCount = current.ParsCount;
        this._DirtyRead = current.DirtyRead;
        this._Type = current.Type;
        this.AdditionalKeysRelationsArray = (IReadOnlyCollection<IFieldsRelation>) ((current is PXSelectorAttribute.WithCachingByCompositeKeyAttribute compositeKeyAttribute ? (object) compositeKeyAttribute.AdditionalKeysRelationsArray : (object) null) ?? (object) Array<IFieldsRelation>.Empty);
        if (sender.Graph.Prototype.Memoize<int>(new Func<int>(this.GetTablesCount), (object) sender.GetItemType(), (object) this.FieldName) < 2)
          this._CacheGlobal = current.CacheGlobal;
        this._SubstituteKey = current.SubstituteKey;
        this.AlterCommand(sender);
      }
    }
    if (this._ShowWarning && EnumerableExtensions.IsIn<System.Type>(sender.Graph.GetType(), typeof (PXGraph), typeof (PXGenericInqGrph), typeof (GenericInquiryDesigner)).Implies(!sender.Graph.UnattendedMode))
      sender.FieldSelectingEvents[this._FieldName.ToLower()] += new PXFieldSelecting(this.FieldSelecting);
    if (this._Message == null || !this._Message.Contains("["))
      return;
    string screenId = PXContext.GetScreenID();
    PXSiteMapNode mapNodeByScreenId;
    if (string.IsNullOrEmpty(screenId) || (mapNodeByScreenId = PXSiteMap.Provider.FindSiteMapNodeByScreenID(screenId)) == null || string.IsNullOrEmpty(mapNodeByScreenId.Title))
      return;
    this._Message = this._Message.Replace("[ScreenName]", mapNodeByScreenId.Title).Replace("[ScreenID]", screenId);
  }

  private int GetTablesCount()
  {
    int length;
    if (WebConfig.EnablePageOpenOptimizations)
    {
      if (!this._TablesCount.HasValue)
        this._TablesCount = new int?(this._OriginalCmd.GetTables().Length);
      length = this._TablesCount.Value;
    }
    else
      length = this._OriginalCmd.GetTables().Length;
    return length;
  }

  /// <exclude />
  protected virtual BqlCommand WhereAnd(PXCache sender, PXSelectorAttribute selattr, System.Type Where)
  {
    selattr.WhereAnd(sender, Where);
    if (WebConfig.EnablePageOpenOptimizations)
    {
      Func<BqlCommand> func = (Func<BqlCommand>) null;
      Tuple<System.Type, System.Type, System.Type> key = Tuple.Create<System.Type, System.Type, System.Type>(this._OriginalCmd.GetType(), Where, selattr.ValueField);
      if (PXRestrictorAttribute.dict.TryGetValue(key, out func))
        return func();
      BqlCommand bqlCommand = this._OriginalCmd.WhereNew(BqlCommand.Compose(typeof (Where<,>), selattr.ValueField, typeof (Equal<>), typeof (Required<>), selattr.ValueField)).WhereAnd(Where);
      func = ((Expression<Func<BqlCommand>>) (() => Expression.New(bqlCommand.GetType()))).Compile();
      PXRestrictorAttribute.dict.TryAdd(key, func);
      return bqlCommand;
    }
    return this._OriginalCmd.WhereNew(BqlCommand.Compose(typeof (Where<,>), selattr.ValueField, typeof (Equal<>), typeof (Required<>), selattr.ValueField)).WhereAnd(Where);
  }

  protected virtual void AlterCommand(PXCache sender)
  {
    if (this._AlteredCmd == null)
      return;
    System.Type key = typeof (PXGraph);
    if (sender.IsGraphSpecificField(this._FieldName))
      key = sender.Graph.GetType();
    lock (((ICollection) this._altered).SyncRoot)
    {
      Tuple<BqlCommand, BqlCommand, List<System.Type>> tuple;
      if (this._altered.TryGetValue(key, out tuple))
      {
        if (this._AlteredCmd.GetSelectType() == tuple.Item1.GetSelectType())
        {
          this._AlteredCmd = tuple.Item2;
          this._AlteredParams = tuple.Item3;
          return;
        }
      }
    }
    PXView view = sender.Graph.TypedViews.GetView(this._OriginalCmd, !this._DirtyRead);
    System.Type[] tables = this._AlteredCmd.GetTables();
    System.Type[] source = BqlCommand.Decompose(this._AlteredCmd.GetSelectType());
    IBqlParameter[] parameters = this._AlteredCmd.GetParameters();
    int length = 0;
    int num1 = 0;
    if (parameters != null)
    {
      foreach (IBqlParameter bqlParameter in parameters)
      {
        if (bqlParameter.IsVisible)
          ++length;
        if (bqlParameter.IsVisible && !bqlParameter.HasDefault && bqlParameter.GetReferencedType() == this._ValueField)
          num1 = length - 1;
      }
    }
    int num2 = 0;
    this._AlteredParams = new List<System.Type>((IEnumerable<System.Type>) new System.Type[length])
    {
      [num1] = this._ValueField
    };
    for (int count = 0; count < source.Length; ++count)
    {
      if (source[count] == typeof (Aggregate<>))
      {
        System.Type[] second;
        if (((IEnumerable<System.Type>) source).Skip<System.Type>(count).Contains<System.Type>(typeof (OrderBy<>)))
          second = new System.Type[2]
          {
            typeof (BqlNone),
            typeof (BqlNone)
          };
        else
          second = new System.Type[1]{ typeof (BqlNone) };
        source = ((IEnumerable<System.Type>) source).Take<System.Type>(count).Concat<System.Type>((IEnumerable<System.Type>) second).ToArray<System.Type>();
        break;
      }
      if (typeof (IBqlParameter).IsAssignableFrom(source[count]) && EnumerableExtensions.IsIn<System.Type>(source[count].GetGenericTypeDefinition(), typeof (Optional<>), typeof (Optional2<>), typeof (Required<>)))
        ++num2;
      if (typeof (IBqlField).IsAssignableFrom(source[count]) && !typeof (IBqlParameter).IsAssignableFrom(source[count - 1]))
      {
        System.Type itemType = BqlCommand.GetItemType(source[count]);
        bool readonlyCacheCreation = sender.Graph._ReadonlyCacheCreation;
        try
        {
          sender.Graph._ReadonlyCacheCreation = true;
          if (!(view.CacheGetItemType() == itemType))
          {
            if (!itemType.IsAssignableFrom(view.CacheGetItemType()))
            {
              if (Array.IndexOf<System.Type>(tables, itemType) > -1)
              {
                this._AlteredParams.Insert(num2++, source[count]);
                source[count] = typeof (Required<>).MakeGenericType(source[count]);
              }
            }
          }
        }
        finally
        {
          sender.Graph._ReadonlyCacheCreation = readonlyCacheCreation;
        }
      }
    }
    BqlCommand alteredCmd = this._AlteredCmd;
    this._AlteredCmd = BqlCommand.CreateInstance(BqlCommand.Compose(source));
    lock (((ICollection) this._altered).SyncRoot)
      this._altered[key] = new Tuple<BqlCommand, BqlCommand, List<System.Type>>(alteredCmd, this._AlteredCmd, this._AlteredParams);
  }

  /// <exclude />
  public static object GetItem(PXCache cache, PXRestrictorAttribute attr, object data, object key)
  {
    object row = (object) null;
    PXSelectorAttribute.GlobalDictionary globalDictionary = (PXSelectorAttribute.GlobalDictionary) null;
    byte keysCount = (byte) (1 + attr.AdditionalKeysRelationsArray.Count);
    if (attr._CacheGlobal && key != null)
    {
      globalDictionary = PXSelectorAttribute.GlobalDictionary.GetOrCreate(attr._Type, cache.Graph.Caches[attr._Type].BqlTable, keysCount);
      lock (globalDictionary.SyncRoot)
      {
        object key1 = keysCount == (byte) 1 ? PXSelectorAttribute.GlobalDictionary.NormalizeKeyFieldValue(key) : (object) PXSelectorAttribute.WithCachingByCompositeKeyAttribute.CreateCompositeKey(cache, data, key, (IEnumerable<IFieldsRelation>) attr.AdditionalKeysRelationsArray, false);
        PXSelectorAttribute.GlobalDictionary.CacheValue cacheValue;
        if (globalDictionary.TryGetValue(key1, out cacheValue))
        {
          if (!cacheValue.IsDeleted)
          {
            if (!(cacheValue.Item is IDictionary))
              row = cacheValue.Item;
          }
        }
      }
    }
    if (row == null && (key == null || key.GetType() == cache.GetFieldType(attr._FieldName)))
    {
      PXView view = cache.Graph.TypedViews.GetView(attr._OriginalCmd, !attr._DirtyRead);
      object[] pars = new object[attr._ParsCount + 1];
      pars[pars.Length - 1] = key;
      row = cache._InvokeSelectorGetter(data, attr.FieldName, view, pars, false);
      object foreignRow;
      if (row != null)
      {
        foreignRow = (object) PXResult.UnwrapMain(row);
      }
      else
      {
        List<object> objectList = view.SelectMultiBound(new object[1]
        {
          data
        }, pars);
        if (objectList.Count == 0)
          return (object) null;
        foreignRow = (object) PXResult.UnwrapMain(row = objectList[0]);
      }
      if (attr._CacheGlobal && key != null && view.Cache.GetStatus(foreignRow) == PXEntryStatus.Notchanged && !PXDatabase.ReadDeleted && view.Cache.Keys.Count <= (int) keysCount)
        PXSelectorAttribute.CheckIntegrityAndPutGlobal(globalDictionary, view.Cache, ((IBqlSearch) attr._OriginalCmd).GetField().Name, foreignRow, PXSelectorAttribute.GlobalDictionary.NormalizeKeyFieldValue(key), false);
    }
    return row;
  }

  /// <exclude />
  public virtual void FieldSelecting(PXCache sender, PXFieldSelectingEventArgs e)
  {
    object newValue = sender.GetValue(e.Row, this._FieldOrdinal);
    PXFieldVerifyingEventArgs e1 = new PXFieldVerifyingEventArgs(e.Row, newValue, e.ExternalCall);
    string messageNoPrefix = this.TryVerify(sender, e1, false)?.MessageNoPrefix;
    if (this._AttributeLevel != PXAttributeLevel.Item && !e.IsAltered)
      return;
    e.ReturnState = (object) PXFieldState.CreateInstance(e.ReturnState, (System.Type) null, fieldName: this._FieldName, error: messageNoPrefix, errorLevel: messageNoPrefix != null ? PXErrorLevel.Warning : PXErrorLevel.Undefined);
  }

  /// <exclude />
  public virtual void FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
  {
    if (this.SuppressVerify)
      return;
    this.Verify(sender, e, true);
  }

  /// <exclude />
  public virtual void Verify(
    PXCache sender,
    PXFieldVerifyingEventArgs e,
    bool IsErrorValueRequired)
  {
    PXException pxException = this.TryVerify(sender, e, IsErrorValueRequired);
    if (pxException != null)
      throw pxException;
  }

  protected virtual PXException TryVerify(
    PXCache sender,
    PXFieldVerifyingEventArgs e,
    bool IsErrorValueRequired)
  {
    if (this._AlteredCmd != null && this._AlteredParams != null && this._OriginalCmd != null)
    {
      if (this._Message != null)
      {
        object row;
        object itemres;
        try
        {
          itemres = row = PXRestrictorAttribute.GetItem(sender, this, e.Row, e.NewValue);
        }
        catch (PXSetPropertyException ex)
        {
          return (PXException) ex;
        }
        if (row != null)
        {
          object[] parameters = new object[this._AlteredParams.Count];
          for (int index = 0; index < this._AlteredParams.Count; ++index)
          {
            if (this._AlteredParams[index] != (System.Type) null)
            {
              System.Type itemType = BqlCommand.GetItemType(this._AlteredParams[index]);
              object data = (object) PXResult.Unwrap(row, itemType);
              if (data != null)
                parameters[index] = sender.Graph.Caches[itemType].GetValue(data, this._AlteredParams[index].Name);
            }
          }
          PXView view = sender.Graph.TypedViews.GetView(this._AlteredCmd, !this._DirtyRead);
          object[] objArray = view.PrepareParameters(new object[1]
          {
            e.Row
          }, parameters);
          object obj = (object) PXResult.UnwrapMain(row);
          if (!this._AlteredCmd.Meet(view.Cache, obj, objArray))
          {
            if (this._SubstituteKey != (System.Type) null & IsErrorValueRequired)
            {
              object newValue = e.NewValue;
              sender.RaiseFieldSelecting(this._FieldName, e.Row, ref newValue, false);
              PXFieldState pxFieldState = newValue as PXFieldState;
              e.NewValue = pxFieldState != null ? pxFieldState.Value : newValue;
            }
            return (PXException) new PXSetPropertyException(this._Message, this.GetMessageParameters(sender, itemres, e.Row));
          }
        }
        return (PXException) null;
      }
    }
    return (PXException) null;
  }

  public virtual object[] GetMessageParameters(PXCache sender, object itemres, object row)
  {
    List<object> objectList = new List<object>();
    foreach (System.Type msgParam in this._MsgParams)
    {
      System.Type itemType = BqlCommand.GetItemType(msgParam);
      PXCache cach = sender.Graph.Caches[itemType];
      object stateExt = cach.GetStateExt((object) PXResult.Unwrap(itemres, itemType) ?? cach.Current, msgParam.Name);
      objectList.Add(stateExt);
    }
    return objectList.ToArray();
  }
}
