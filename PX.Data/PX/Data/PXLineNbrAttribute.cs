// Decompiled with JetBrains decompiler
// Type: PX.Data.PXLineNbrAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Data.BQL.Validations;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Data;

/// <summary>Automatically generates unique line numbers that identify for
/// child data records in the parent-child relationship. This attribute
/// does not work without the <see cref="T:PX.Data.PXParentAttribute">PXParent</see>
/// attribute.</summary>
/// <remarks>
/// <para>The attribute should be placed on the child DAC field that
/// stores the line number. The line number is a two-byte integer
/// incremented by the <tt>IncrementStep</tt> property value, which equals
/// 1 by default. The line number uniquely identifies a data record among
/// the child data records related to a given parent data record. The
/// attribute calculates each next value by incrementing the current
/// number of the child data records.</para>
/// <para>The child DAC field to store the line number typically has the
/// <tt>short?</tt> data type. It also should be a key field. You indicate
/// that the field is a key field by setting the <tt>IsKey</tt> property
/// of the data type attribute to <tt>true</tt>.</para>
/// <para>As a parameter, you should pass either the parent DAC field that
/// stores the number of related child data records or the parent DAC
/// itself. In the latter case, the attribute will determine the number of
/// related child data records by itself. If the parent DAC field is
/// specified, the attribute automatically updates its value.</para>
/// </remarks>
/// <example><para>The attribute below takes the number of related child data records from the provided parent field. The PXParent attribute must be added to some other field of this DAC.</para>
/// <code title="Example" lang="CS">
/// [PXDBShort(IsKey = true)]
/// [PXLineNbr(typeof(ARRegister.lineCntr))]
/// public virtual short? LineNbr { get; set; }</code>
/// <para>In the following example, the attribute calculates the number of related child data records by itself.</para>
/// <code title="Example2" description="" groupname="Example" lang="CS">
/// [PXDBShort(IsKey = true)]
/// [PXLineNbr(typeof(Vendor))]
/// [PXParent(typeof(
///     Select&lt;Vendor,
///         Where&lt;Vendor.bAccountID, Equal&lt;Current&lt;TaxReportLine.vendorID&gt;&gt;&gt;&gt;))]
/// public virtual short? LineNbr { get; set; }</code>
/// </example>
public class PXLineNbrAttribute : 
  PXEventSubscriberAttribute,
  IPXFieldDefaultingSubscriber,
  IPXRowDeletedSubscriber,
  IPXRowInsertedSubscriber,
  IPXRowPersistedSubscriber
{
  private System.Type _dataType;
  internal readonly System.Type _sourceType;
  internal readonly string _sourceField;
  private readonly bool _enabled;
  protected PXLineNbrAttribute.LastDefault _LastDefaultValue;

  /// <summary>
  /// Initializes a new instance of the attribute. As a parameter you can provide
  /// the parent data record field that stores the number of child data records or
  /// the parent DAC if there is no such field. In the latter case the attribute
  /// will calculate the number of child data records automatically.
  /// </summary>
  /// <param name="sourceType">The parent data record field that stores the number
  /// of children or the parent DAC.</param>
  /// <param name="Enabled">Allows to make field editable.</param>
  /// <example>
  /// In the following example, the attribute calculates the number of related child
  /// data records by itself.
  /// <code>
  /// [PXDBShort(IsKey = true)]
  /// [PXLineNbr(typeof(Vendor))]
  /// [PXParent(typeof(
  ///     Select&lt;Vendor,
  ///         Where&lt;Vendor.bAccountID, Equal&lt;Current&lt;TaxReportLine.vendorID&gt;&gt;&gt;&gt;))]
  /// public virtual short? LineNbr { get; set; }
  /// </code>
  /// </example>
  public PXLineNbrAttribute(System.Type sourceType)
    : this(sourceType, false)
  {
  }

  /// <summary>
  /// Initializes a new instance of the attribute. As a parameter you can provide
  /// the parent data record field that stores the number of child data records or
  /// the parent DAC if there is no such field. In the latter case the attribute
  /// will calculate the number of child data records automatically.
  /// </summary>
  /// <param name="sourceType">The parent data record field that stores the number
  /// of children or the parent DAC.</param>
  /// <param name="Enabled">Allows to make field editable.</param>
  /// <example>
  /// In the following example, the attribute calculates the number of related child
  /// data records by itself.
  /// <code>
  /// [PXDBShort(IsKey = true)]
  /// [PXLineNbr(typeof(Vendor))]
  /// [PXParent(typeof(
  ///     Select&lt;Vendor,
  ///         Where&lt;Vendor.bAccountID, Equal&lt;Current&lt;TaxReportLine.vendorID&gt;&gt;&gt;&gt;))]
  /// public virtual short? LineNbr { get; set; }
  /// </code>
  /// </example>
  public PXLineNbrAttribute(System.Type sourceType, bool Enabled)
  {
    this._enabled = Enabled;
    if (typeof (IBqlField).IsAssignableFrom(sourceType) && sourceType.IsNested)
    {
      this._sourceType = BqlCommand.GetItemType(sourceType);
      this._sourceField = sourceType.Name;
    }
    else
      this._sourceType = typeof (IBqlTable).IsAssignableFrom(sourceType) ? sourceType : throw new PXArgumentException("type", "A foreign key reference cannot be created from the type '{0}'.", new object[1]
      {
        (object) sourceType
      });
  }

  /// <summary>Gets or sets the number by which the line number is
  /// incremented or decremented. By default, the property equals
  /// 1.</summary>
  public short IncrementStep { get; set; } = 1;

  /// <summary>Indicates whether the source field would be decremented
  /// on the row deleting or not. By default, the property equals true.</summary>
  public bool DecrementOnDelete { get; set; } = true;

  /// <summary>
  /// Indicates whether the line number gaps produced by the delete operation should be reused
  /// </summary>
  public bool ReuseGaps { get; set; }

  /// <summary>
  /// Gets or sets a value indicating that line counter calculation will be
  /// validated when records are saved to the database.
  /// </summary>
  public virtual bool ValidateLineCounterCalculation { get; set; }

  /// <summary>
  /// Gets or sets a value indicating that line counter calculation should be
  /// validated for all child records, even if they are not changed.
  /// </summary>
  public virtual bool UseStrongLineCounterCalculation { get; set; }

  /// <exclude />
  public virtual void LineNbr_FieldSelecting(PXCache sender, PXFieldSelectingEventArgs e)
  {
    if (this._AttributeLevel != PXAttributeLevel.Item && !e.IsAltered)
      return;
    e.ReturnState = (object) PXFieldState.CreateInstance(e.ReturnValue, (System.Type) null, fieldName: this._FieldName, enabled: new bool?(this._enabled));
  }

  /// <exclude />
  public virtual void RowInserted(PXCache sender, PXRowInsertedEventArgs e)
  {
    if (this._sourceField == null)
      return;
    PXCache cach = sender.Graph.Caches[this._sourceType];
    if (cach.Current == null)
      return;
    object obj1 = cach.GetValue(cach.Current, this._sourceField);
    object obj2 = sender.GetValue(e.Row, this._FieldOrdinal);
    if (obj2 == null || ((IComparable) obj1).CompareTo(obj2) >= 0)
      return;
    this._LastDefaultValue.Clear();
    cach.SetValue(cach.Current, this._sourceField, obj2);
    cach.MarkUpdated(cach.Current);
  }

  /// <exclude />
  public virtual void RowDeleted(PXCache sender, PXRowDeletedEventArgs e)
  {
    if (this._sourceField != null && this.DecrementOnDelete)
    {
      PXCache cach = sender.Graph.Caches[this._sourceType];
      if (cach.Current != null)
      {
        object obj1 = cach.GetValue(cach.Current, this._sourceField);
        object obj2 = sender.GetValue(e.Row, this._FieldOrdinal);
        if (obj2 != null && ((IComparable) obj1).CompareTo(obj2) == 0)
        {
          this._LastDefaultValue.Clear();
          cach.SetValue(cach.Current, this._sourceField, this.Decrement(obj1, (short) 1));
          cach.MarkUpdated(cach.Current);
        }
      }
    }
    if (!this.ReuseGaps || e.Row == null)
      return;
    this._LastDefaultValue.StoreGap(sender.GetValue(e.Row, this._FieldOrdinal));
  }

  /// <exclude />
  public virtual void RowPersisted(PXCache sender, PXRowPersistedEventArgs e)
  {
    if (e.TranStatus != PXTranStatus.Completed)
      return;
    this.ClearLastDefaultValue();
  }

  /// <exclude />
  public virtual void ClearLastDefaultValue() => this._LastDefaultValue.Clear();

  /// <exclude />
  public override void CacheAttached(PXCache sender)
  {
    this._dataType = ((PXFieldState) sender.GetStateExt((object) null, this._FieldName)).DataType;
    this._LastDefaultValue = new PXLineNbrAttribute.LastDefault(this);
    sender.Graph.FieldSelecting.AddHandler(sender.GetItemType(), this._FieldName, new PXFieldSelecting(this.LineNbr_FieldSelecting));
    sender.Graph.OnClear += (PXGraphClearDelegate) ((_param1, _param2) => this.ClearLastDefaultValue());
    sender.Graph.OnReuseInitialize += new System.Action(this.ClearLastDefaultValue);
    if (!this.ValidateLineCounterCalculation)
      return;
    this.InitValidation(sender);
  }

  /// <exclude />
  public object NewLineNbr(PXCache sender, object Current)
  {
    object obj1 = this.DefaultValue;
    if (Current != null)
    {
      if (this.ReuseGaps)
      {
        this._LastDefaultValue.RestoreGaps(sender, Current);
        object gap = this._LastDefaultValue.GetGap();
        if (gap != null)
          return gap;
      }
      if (!string.IsNullOrEmpty(this._sourceField))
      {
        PXCache cach = sender.Graph.Caches[this._sourceType];
        obj1 = this.Increment(cach.GetValue(Current, this._sourceField), this.IncrementStep);
        cach.SetValue(Current, this._sourceField, obj1);
        cach.MarkUpdated(Current);
      }
      else
      {
        foreach (object selectChild in PXParentAttribute.SelectChildren(sender, Current, this._sourceType))
        {
          PXCache pxCache = sender;
          if (selectChild.GetType() != sender.GetItemType())
            pxCache = sender.Graph.Caches[selectChild.GetType()];
          object obj2 = pxCache.GetValue(selectChild, this._FieldOrdinal);
          if (((IComparable) obj2).CompareTo(obj1) > 0)
            obj1 = obj2;
        }
        obj1 = this.Increment(obj1, this.IncrementStep);
      }
    }
    return obj1;
  }

  /// <summary>Returns the next line number for the provided parent data
  /// record. The returned value should be used as the child identifier
  /// stored in the specified field.</summary>
  /// <param name="cache">The cache object to search for the</param>
  /// <param name="Current">The parent data record for which the next child
  /// identifier (line number) is returned.</param>
  /// <returns>The line number as an object. Cast to <tt>short?</tt>.</returns>
  public static object NewLineNbr<TField>(PXCache cache, object Current) where TField : class, IBqlField
  {
    return cache.GetAttributes<TField>().OfType<PXLineNbrAttribute>().FirstOrDefault<PXLineNbrAttribute>()?.NewLineNbr(cache, Current);
  }

  /// <exclude />
  public virtual void FieldDefaulting(PXCache sender, PXFieldDefaultingEventArgs e)
  {
    if (this.ReuseGaps)
      this._LastDefaultValue.RestoreGaps(sender);
    if ((int) this._LastDefaultValue > 0)
    {
      if (e.Row != null)
      {
        foreach (object row in this._LastDefaultValue.Rows)
        {
          if (sender.Locate(row) != null)
          {
            this._LastDefaultValue.Rows.Clear();
            object gap = this.ReuseGaps ? this._LastDefaultValue.GetGap() : (object) null;
            if (gap != null)
            {
              this._LastDefaultValue.Value = gap;
              break;
            }
            ++this._LastDefaultValue;
            break;
          }
        }
        this._LastDefaultValue.Rows.Add(e.Row);
      }
      e.NewValue = this._LastDefaultValue.Value;
    }
    else
    {
      object gap = this.ReuseGaps ? this._LastDefaultValue.GetGap() : (object) null;
      if (gap != null)
        this._LastDefaultValue.Value = this.Decrement(gap, (short) 1);
      else if (this._sourceField != null)
      {
        PXCache cach = sender.Graph.Caches[this._sourceType];
        if (cach.Current != null)
        {
          object obj = cach.GetValue(cach.Current, this._sourceField);
          if (obj != null)
            this._LastDefaultValue.Value = obj;
        }
      }
      else
      {
        bool flag = false;
        foreach (object data in sender.Inserted)
        {
          IComparable comparable = sender.GetValue(data, this._FieldOrdinal) as IComparable;
          if (comparable.CompareTo(this._LastDefaultValue.Value) > 0)
          {
            this._LastDefaultValue.Value = (object) comparable;
            flag = true;
          }
        }
        foreach (object data in sender.Updated)
        {
          IComparable comparable = sender.GetValue(data, this._FieldOrdinal) as IComparable;
          if (comparable.CompareTo(this._LastDefaultValue.Value) > 0)
            this._LastDefaultValue.Value = (object) comparable;
        }
        if (!flag)
        {
          foreach (object selectSibling in PXParentAttribute.SelectSiblings(sender, e.Row, this._sourceType))
          {
            PXCache pxCache = sender;
            if (selectSibling.GetType() != sender.GetItemType())
              pxCache = sender.Graph.Caches[selectSibling.GetType()];
            object obj = pxCache.GetValue(selectSibling, this._FieldOrdinal);
            if (((IComparable) obj).CompareTo(this._LastDefaultValue.Value) > 0)
              this._LastDefaultValue.Value = obj;
          }
        }
      }
      ++this._LastDefaultValue;
      e.NewValue = this._LastDefaultValue.Value;
      if (e.Row == null)
        return;
      this._LastDefaultValue.Rows.Add(e.Row);
    }
  }

  public object DefaultValue
  {
    get
    {
      if (this._dataType == typeof (int))
        return (object) 0;
      if (this._dataType == typeof (short))
        return (object) (short) 0;
      return this._dataType == typeof (ushort) ? (object) (ushort) 0 : (object) 0L;
    }
  }

  protected object Increment(object value, short step)
  {
    if (this._dataType == typeof (int))
      return (object) ((int) value + (int) step);
    if (this._dataType == typeof (short))
      return (object) (short) ((int) (short) value + (int) step);
    return this._dataType == typeof (ushort) ? (object) (ushort) ((uint) (ushort) value + (uint) step) : (object) ((long) value + (long) step);
  }

  protected object Decrement(object value, short step)
  {
    if (this._dataType == typeof (int))
      return (object) ((int) value - (int) step);
    if (this._dataType == typeof (short))
      return (object) (short) ((int) (short) value - (int) step);
    return this._dataType == typeof (ushort) ? (object) (ushort) ((uint) (ushort) value - (uint) step) : (object) ((long) value - (long) step);
  }

  protected void InitValidation(PXCache sender)
  {
    LineCounterValidation.CreateValidation(sender.Graph.Caches[this._sourceType].GetFieldType(this._sourceField), this.UseStrongLineCounterCalculation)?.Initialize(sender, this._sourceType, this._FieldName, this._sourceField);
  }

  /// <exclude />
  protected class LastDefault
  {
    protected object _Value;
    public List<object> Rows = new List<object>();
    private readonly SortedSet<object> Gaps = new SortedSet<object>();
    private readonly PXLineNbrAttribute _owner;
    private bool _gapsRestored;

    public LastDefault(PXLineNbrAttribute owner)
    {
      this._owner = owner;
      this.Initialize();
    }

    public void Clear()
    {
      this.Rows.Clear();
      this.Initialize();
    }

    public void Initialize() => this._Value = this._owner.DefaultValue;

    public void StoreGap(object value)
    {
      if (value == null)
        return;
      this.Gaps.Add(value);
    }

    public object GetGap()
    {
      if (!this.Gaps.Any<object>())
        return (object) null;
      object min = this.Gaps.Min;
      this.Gaps.Remove(min);
      return min;
    }

    public void RestoreGaps(PXCache cache, object source = null)
    {
      if (this._gapsRestored)
        return;
      PXCache sourceCache = cache.Graph.Caches[this._owner._sourceType];
      source = source ?? sourceCache.Current;
      if (source != null)
      {
        object[] array = EnumerableExtensions.WhereNotNull<object>(((IEnumerable<object>) PXParentAttribute.SelectChildren(cache, source, this._owner._sourceType)).Select<object, object>((Func<object, object>) (t => cache.GetValue(t, this._owner._FieldOrdinal))).Union<object>((IEnumerable<object>) this._owner._sourceField.With<string, object[]>((Func<string, object[]>) (fn => sourceCache.GetValue(source, fn).With<object, object[]>((Func<object, object[]>) (v => new object[1]
        {
          v
        })))) ?? Enumerable.Empty<object>())).OrderBy<object, object>((Func<object, object>) (t => t)).ToArray<object>();
        object obj1 = this._owner.Increment(this._owner.DefaultValue, (short) 1);
        foreach (object obj2 in array)
        {
          for (; ((IComparable) obj2).CompareTo(obj1) > 0; obj1 = this._owner.Increment(obj1, this._owner.IncrementStep))
            this.StoreGap(obj1);
          obj1 = this._owner.Increment(obj1, this._owner.IncrementStep);
        }
      }
      this._gapsRestored = true;
    }

    public object Value
    {
      get => this._Value;
      set => this._Value = value;
    }

    public static PXLineNbrAttribute.LastDefault operator ++(PXLineNbrAttribute.LastDefault value)
    {
      value._Value = value._owner.Increment(value._Value, value._owner.IncrementStep);
      return value;
    }

    public static PXLineNbrAttribute.LastDefault operator --(PXLineNbrAttribute.LastDefault value)
    {
      value._Value = value._owner.Decrement(value._Value, value._owner.IncrementStep);
      return value;
    }

    public static explicit operator int(PXLineNbrAttribute.LastDefault value)
    {
      return (int) Convert.ChangeType(value.Value, typeof (int));
    }
  }
}
