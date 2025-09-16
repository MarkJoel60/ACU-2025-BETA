// Decompiled with JetBrains decompiler
// Type: PX.Data.PXDecimalAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.SQLTree;
using System;
using System.Collections.Generic;
using System.Globalization;

#nullable disable
namespace PX.Data;

/// <summary>Indicates a DAC field of <tt>decimal?</tt> type that is not mapped to a database column.</summary>
/// <remarks>The attribute is added to the value declaration of a DAC field. The field is not bound to a table column.</remarks>
/// <example>
///   <code title="" description="" lang="CS">
/// [PXDecimal(0)]
/// [PXUIField(DisplayName = "SignBalance")]
/// public virtual Decimal? SignBalance { get; set; }</code>
/// </example>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Parameter)]
[PXAttributeFamily(typeof (PXFieldState))]
public class PXDecimalAttribute : 
  PXEventSubscriberAttribute,
  IPXFieldUpdatingSubscriber,
  IPXFieldSelectingSubscriber,
  IPXCommandPreparingSubscriber
{
  protected int? _Precision = new int?(2);
  protected Decimal _MinValue = Decimal.MinValue;
  protected Decimal _MaxValue = Decimal.MaxValue;
  protected bool _IsKey;
  protected System.Type _Type;
  protected BqlCommand _Select;

  /// <summary>Gets or sets the value that indicates whether the field is a
  /// key field.</summary>
  public virtual bool IsKey
  {
    get => this._IsKey;
    set => this._IsKey = value;
  }

  /// <summary>Gets or sets the minimum value for the field.</summary>
  public double MinValue
  {
    get => (double) this._MinValue;
    set => this._MinValue = (Decimal) value;
  }

  /// <summary>Gets or sets the maximum value for the field.</summary>
  public double MaxValue
  {
    get => (double) this._MaxValue;
    set => this._MaxValue = (Decimal) value;
  }

  protected internal virtual int? Precision => this._Precision;

  /// <summary>Initializes a new instance with the default precision, which
  /// equals 2.</summary>
  public PXDecimalAttribute()
  {
  }

  /// <summary>Initializes a new instance with the given
  /// precision.</summary>
  public PXDecimalAttribute(int precision) => this._Precision = new int?(precision);

  /// <summary>Initializes a new instance with the precision calculated at
  /// runtime using a BQL query.</summary>
  /// <param name="type">A BQL query based on a class derived from
  /// <tt>IBqlSearch</tt> or <tt>IBqlField</tt>. For example, the parameter
  /// can be set to <tt>typeof(Search&lt;...&gt;)</tt>, or
  /// <tt>typeof(Table1.field)</tt>.</param>
  public PXDecimalAttribute(System.Type type)
  {
    if (type == (System.Type) null)
      throw new PXArgumentException(nameof (type), "The argument cannot be null.");
    if (typeof (IBqlSearch).IsAssignableFrom(type))
    {
      this._Select = BqlCommand.CreateInstance(type);
      this._Type = BqlCommand.GetItemType(((IBqlSearch) this._Select).GetField());
    }
    else
    {
      this._Type = type.IsNested && typeof (IBqlField).IsAssignableFrom(type) ? type.DeclaringType : throw new PXArgumentException(nameof (type), "A foreign key reference cannot be created from the type '{0}'.", new object[1]
      {
        (object) type
      });
      this._Select = BqlCommand.CreateInstance(typeof (Search<>), type);
    }
  }

  /// <summary>Sets the precision in the attribute instance that marks the
  /// field with the specified name in a particular data record.</summary>
  /// <param name="cache">The cache object to search for the attributes of
  /// <tt>PXDecimal</tt> type.</param>
  /// <param name="data">The data record the method is applied to.</param>
  /// <param name="name">The name of the field that is be marked with the
  /// attribute.</param>
  /// <param name="precision">The new precision value.</param>
  public static void SetPrecision(PXCache cache, object data, string name, int? precision)
  {
    if (data == null)
      cache.SetAltered(name, true);
    foreach (PXEventSubscriberAttribute attribute in cache.GetAttributes(data, name))
    {
      if (attribute is PXDecimalAttribute)
        ((PXDecimalAttribute) attribute)._Precision = precision;
    }
  }

  /// <summary>Sets the precision in the attribute instance that marks the
  /// specified field in a particular data record.</summary>
  /// <param name="cache">The cache object to search for the attributes of
  /// <tt>PXDBDecimal</tt> type.</param>
  /// <param name="data">The data record the method is applied to.</param>
  /// <param name="precision">The new precision value.</param>
  public static void SetPrecision<Field>(PXCache cache, object data, int? precision) where Field : IBqlField
  {
    if (data == null)
      cache.SetAltered<Field>(true);
    foreach (PXEventSubscriberAttribute attribute in cache.GetAttributes<Field>(data))
    {
      if (attribute is PXDecimalAttribute)
        ((PXDecimalAttribute) attribute)._Precision = precision;
    }
  }

  /// <summary>Sets the precision in the attribute instance that marks the
  /// field with the specified name in all data records in the cache
  /// object.</summary>
  /// <param name="cache">The cache object to search for the attributes of
  /// <tt>PXDBDecimal</tt> type.</param>
  /// <param name="name">The name of the field that is be marked with the
  /// attribute.</param>
  /// <param name="precision">The new precision value.</param>
  public static void SetPrecision(PXCache cache, string name, int? precision)
  {
    cache.SetAltered(name, true);
    foreach (PXEventSubscriberAttribute attribute in cache.GetAttributes(name))
    {
      if (attribute is PXDecimalAttribute)
        ((PXDecimalAttribute) attribute)._Precision = precision;
    }
  }

  /// <summary>Sets the precision in the attribute instance that marks the
  /// specified field in all data records in the cache object.</summary>
  /// <param name="cache">The cache object to search for the attributes of
  /// <tt>PXDBDecimal</tt> type.</param>
  /// <param name="precision">The new precision value.</param>
  public static void SetPrecision<Field>(PXCache cache, int? precision) where Field : IBqlField
  {
    cache.SetAltered<Field>(true);
    foreach (PXEventSubscriberAttribute attribute in cache.GetAttributes<Field>())
    {
      if (attribute is PXDecimalAttribute)
        ((PXDecimalAttribute) attribute)._Precision = precision;
    }
  }

  /// <exclude />
  public virtual void FieldUpdating(PXCache sender, PXFieldUpdatingEventArgs e)
  {
    if (e.NewValue is string)
    {
      Decimal result;
      e.NewValue = !Decimal.TryParse((string) e.NewValue, NumberStyles.Any, (IFormatProvider) sender.Graph.Culture, out result) ? (object) null : (object) result;
    }
    if (e.NewValue == null)
      return;
    this._ensurePrecision(sender, e.Row);
    if (!this._Precision.HasValue)
      return;
    e.NewValue = (object) System.Math.Round((Decimal) e.NewValue, this._Precision.Value, MidpointRounding.AwayFromZero);
  }

  /// <exclude />
  public virtual void FieldSelecting(PXCache sender, PXFieldSelectingEventArgs e)
  {
    if (this._AttributeLevel != PXAttributeLevel.Item && !e.IsAltered)
      return;
    this._ensurePrecision(sender, e.Row);
    e.ReturnState = (object) PXDecimalState.CreateInstance(e.ReturnState, this._Precision, this._FieldName, new bool?(this._IsKey), new int?(-1), new Decimal?(this._MinValue), new Decimal?(this._MaxValue));
  }

  /// <exclude />
  public virtual void CommandPreparing(PXCache sender, PXCommandPreparingEventArgs e)
  {
    if ((e.Operation & PXDBOperation.Delete) != PXDBOperation.Select)
      return;
    if (e.Value != null)
    {
      e.BqlTable = this._BqlTable;
      if (e.Expr == null)
        e.Expr = (SQLExpression) new Column(this._FieldName, e.BqlTable);
      e.DataValue = e.Value;
      e.DataLength = new int?(16 /*0x10*/);
    }
    e.DataType = PXDbType.Decimal;
  }

  protected virtual void _ensurePrecision(PXCache sender, object row)
  {
    if (!(this._Type != (System.Type) null))
      return;
    PXView view = sender.Graph.TypedViews.GetView(this._Select, true);
    object obj = (object) null;
    try
    {
      List<object> objectList = view.SelectMultiBound(new object[1]
      {
        row
      });
      if (objectList.Count > 0)
        obj = objectList[0];
    }
    catch
    {
    }
    if (obj == null)
      return;
    int? itemPrecision = this.GetItemPrecision(view, obj);
    if (!itemPrecision.HasValue)
      return;
    this._Precision = itemPrecision;
  }

  protected virtual int? GetItemPrecision(PXView view, object item)
  {
    if (item is PXResult)
      item = ((PXResult) item)[0];
    short? nullable = item != null ? (short?) view.Cache.GetValue(item, ((IBqlSearch) this._Select).GetField().Name) : new short?();
    return !nullable.HasValue ? new int?() : new int?((int) nullable.GetValueOrDefault());
  }

  /// <exclude />
  public override void CacheAttached(PXCache sender)
  {
    if (!this.IsKey)
      return;
    sender.Keys.Add(this._FieldName);
  }
}
