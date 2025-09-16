// Decompiled with JetBrains decompiler
// Type: PX.Data.PXDBDecimalAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Data.Maintenance.GI;
using PX.Data.Reports;
using PX.DbServices.Model.Entities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;

#nullable disable
namespace PX.Data;

/// <summary>Maps a DAC field of <tt>decimal?</tt> type to the database
/// column of <tt>decimal</tt> type.</summary>
/// <remarks>
/// The attribute is added to the value declaration of a DAC field. The field becomes bound to the database column with the same name. A minimum value, maximum
/// value, and precision can be specified. The precision can be calculated at runtime using BQL. The default precision is 2.
/// </remarks>
/// <example>
/// <code title="Example" lang="CS">
/// // Declaration of a DAC field with a specific precision is shown below.
/// [PXDBDecimal(6, MinValue = 0, MaxValue = 100)]
/// public virtual decimal? Price { get; set; }</code>
/// <code title="Example1" lang="CS">
/// // Declaration of a DAC field with a precision calculated at runtime.
/// // The BQL query in this example will search for the Currency data record that
/// // satisfies the specified Where condition. The field precision will be set to
/// // the DecimalPlaces value from this data record.
/// [PXDBDecimal(typeof(
///     Search&lt;Currency.decimalPlaces,
///         Where&lt;Currency.curyID, Equal&lt;Current&lt;POCreateFilter.vendorID&gt;&gt;&gt;&gt;
/// ))]
/// public virtual decimal? OrderTotal { get; set; }</code>
/// </example>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Parameter)]
public class PXDBDecimalAttribute : 
  PXDBFieldAttribute,
  IPXRowSelectingSubscriber,
  IPXCommandPreparingSubscriber,
  IPXFieldUpdatingSubscriber,
  IPXFieldSelectingSubscriber,
  IPXRowPersistingSubscriber,
  IPXFieldVerifyingSubscriber
{
  protected int? _Precision = new int?(2);
  protected Decimal _MinValue = Decimal.MinValue;
  protected Decimal _MaxValue = Decimal.MaxValue;
  protected System.Type _Type;
  protected BqlCommand _Select;
  protected System.Type _SignFormula;
  /// <exclude />
  public const string SignSuffix = "Signed";
  protected bool _IsInitialized;
  private bool _signFieldActive;
  private PXDBDecimalAttribute.PXSignAttribute SignAttribute;

  /// <exclude />
  public PXDBDecimalAttribute.DBDecimalProperties DBProperties { get; private set; }

  /// <summary>Gets or sets the minimum value for the field.</summary>
  public double MinValue
  {
    get => (double) this._MinValue;
    set
    {
      this._MinValue = value <= -7.9228162514264338E+28 ? Decimal.MinValue : (value >= 7.9228162514264338E+28 ? Decimal.MaxValue : (Decimal) value);
    }
  }

  /// <summary>Gets or sets the minimum value for the field.</summary>
  public double MaxValue
  {
    get => (double) this._MaxValue;
    set
    {
      this._MaxValue = value <= -7.9228162514264338E+28 ? Decimal.MinValue : (value >= 7.9228162514264338E+28 ? Decimal.MaxValue : (Decimal) value);
    }
  }

  protected internal virtual int? Precision => this._Precision;

  /// <exclude />
  public System.Type SignFormula
  {
    get => this._SignFormula;
    set
    {
      if (!typeof (IBqlCreator).IsAssignableFrom(value))
        throw new PXException("Cannot create {0}. The sign formula should be IBqlCreator.", new object[1]
        {
          (object) this.GetSignedFieldName()
        });
      if (this._IsInitialized)
        throw new PXNotSupportedException("Cannot create {0}. The sign formula should be set before CacheAttached.", new object[1]
        {
          (object) this.GetSignedFieldName()
        });
      this._SignFormula = value;
    }
  }

  /// <summary>Initializes a new instance with the default precision, which
  /// equals 2.</summary>
  /// <example>
  /// <code>
  /// [PXDBDecimal(MaxValue = 100, MinValue = 0)]
  /// [PXDefault(TypeCode.Decimal, "50.0")]
  /// [PXUIField(DisplayName = "Group/Document Discount Limit (%)")]
  /// public virtual Decimal? DiscountLimit { get; set; }
  /// </code>
  /// </example>
  public PXDBDecimalAttribute()
  {
    this.DBProperties = new PXDBDecimalAttribute.DBDecimalProperties();
  }

  /// <summary>Initializes a new instance with the given
  /// precision.</summary>
  /// <param name="precision">The precision value.</param>
  /// <example>
  ///   <code title="" description="" lang="CS">
  /// [PXDBDecimal(4)]
  /// [PXDefault(TypeCode.Decimal, "0.0")]
  /// public virtual Decimal? TaxTotal { get; set; }</code>
  /// </example>
  public PXDBDecimalAttribute(int precision)
    : this()
  {
    this._Precision = new int?(precision);
  }

  /// <summary>Initializes a new instance with the precision calculated at runtime with a BQL query.</summary>
  /// <param name="type">A BQL query based on a class derived from
  /// <tt>IBqlSearch</tt> or <tt>IBqlField</tt>. For example, the parameter
  /// can be set to <tt>typeof(Search&lt;...&gt;)</tt>, or
  /// <tt>typeof(Table1.field)</tt>.</param>
  /// <example>
  /// The code below shows declaration of a DAC field with a precision calculated at runtime. The BQL query in this example will search for the <tt>Currency</tt>
  /// data record that satisfies the specified <tt>Where</tt> condition. The field precision will be set to the <tt>DecimalPlaces</tt> value from this data record.
  /// <code title="" description="" lang="CS">
  /// [PXDBDecimal(typeof(
  ///     Search&lt;Currency.decimalPlaces,
  ///         Where&lt;Currency.curyID, Equal&lt;Current&lt;POCreateFilter.vendorID&gt;&gt;&gt;&gt;
  /// ))]
  /// public virtual decimal? OrderTotal { get; set; }</code></example>
  public PXDBDecimalAttribute(System.Type type)
    : this()
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
      this._Type = type.IsNested && typeof (IBqlField).IsAssignableFrom(type) ? BqlCommand.GetItemType(type) : throw new PXArgumentException(nameof (type), "A foreign key reference cannot be created from the type '{0}'.", new object[1]
      {
        (object) type
      });
      this._Select = BqlCommand.CreateInstance(typeof (Search<>), type);
    }
  }

  /// <summary>Sets the precision in the attribute instance that marks the
  /// field with the specified name in a particular data record.</summary>
  /// <param name="cache">The cache object to search for the attributes of
  /// <tt>PXDBDecimal</tt> type.</param>
  /// <param name="data">The data record the method is applied to.</param>
  /// <param name="name">The name of the field that is be marked with the
  /// attribute.</param>
  /// <param name="precision">The new precision value.</param>
  /// <example>
  /// The code below shows the <tt>RowSelected</tt> event handler (used to
  /// configure the UI at run time), in which you set the precision for the
  /// <tt>Qty</tt> field in the provided data record.
  /// <code>
  /// protected virtual void LotSerOptions_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  /// {
  ///     LotSerOptions opt = (LotSerOptions)e.Row;
  ///     ...
  ///     PXDBDecimalAttribute.SetPrecision(sender, opt, "Qty", (opt.IsSerial == true ? 0 : INSetupDecPl.Qty));
  ///     ...
  /// }
  /// </code>
  /// </example>
  public static void SetPrecision(PXCache cache, object data, string name, int? precision)
  {
    if (data == null)
      cache.SetAltered(name, true);
    foreach (PXEventSubscriberAttribute attribute in cache.GetAttributes(data, name))
    {
      if (attribute is PXDBDecimalAttribute)
        ((PXDBDecimalAttribute) attribute)._Precision = precision;
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
      if (attribute is PXDBDecimalAttribute)
        ((PXDBDecimalAttribute) attribute)._Precision = precision;
    }
  }

  protected string Check(object value)
  {
    if (value is Decimal num)
    {
      Decimal num1 = this.Normalize(num);
      if (!this.DBProperties.IsSet)
        this.DBProperties.Fill(this._BqlTable, this._DatabaseFieldName);
      if (this.DBProperties.IsSet)
      {
        Decimal num2 = System.Math.Abs(num1);
        Decimal? maxValue = this.DBProperties.MaxValue;
        Decimal valueOrDefault = maxValue.GetValueOrDefault();
        if (num2 >= valueOrDefault & maxValue.HasValue)
          return PXMessages.LocalizeFormat("The number in {0} does not fit the SQL decimal data type.", (object) this._FieldName);
      }
    }
    return (string) null;
  }

  protected Decimal Normalize(Decimal value) => Decimal.Round(value, 28);

  /// <exclude />
  public virtual void RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    string message = this.Check(sender.GetValue(e.Row, this._FieldOrdinal));
    if (message != null && sender.RaiseExceptionHandling(this._FieldName, e.Row, (object) null, (Exception) new PXSetPropertyKeepPreviousException(message)))
      throw new PXRowPersistingException(this._FieldName, (object) null, message);
  }

  /// <exclude />
  protected override void PrepareCommandImpl(string dbFieldName, PXCommandPreparingEventArgs e)
  {
    base.PrepareCommandImpl(dbFieldName, e);
    e.DataType = PXDbType.Decimal;
    e.DataLength = new int?(16 /*0x10*/);
  }

  /// <exclude />
  public override void RowSelecting(PXCache sender, PXRowSelectingEventArgs e)
  {
    if (e.Row != null)
      sender.SetValue(e.Row, this._FieldOrdinal, (object) e.Record.GetDecimal(e.Position));
    ++e.Position;
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

  public virtual void FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
  {
    if (!(e.NewValue is Decimal newValue))
      return;
    if (newValue < this._MinValue)
      throw new PXSetPropertyException(e.Row as IBqlTable, "The value must be greater than or equal to {0}.", new object[1]
      {
        (object) this._MinValue
      });
    string message = !(newValue > this._MaxValue) ? this.Check((object) newValue) : throw new PXSetPropertyException(e.Row as IBqlTable, "The value must be less than or equal to {0}.", new object[1]
    {
      (object) this._MaxValue
    });
    if (message != null)
      throw new PXSetPropertyException(e.Row as IBqlTable, message);
  }

  /// <summary>Retrieves the precision value if it is set by a BQL query specified in the constructor, and sets its to all attribute instances in the cache object.</summary>
  /// <param name="cache">The cache object to search for the attributes of
  /// <tt>PXDBDecimal</tt> type.</param>
  public static void EnsurePrecision(PXCache cache)
  {
    foreach (PXEventSubscriberAttribute subscriberAttribute in cache.GetAttributesReadonly((string) null))
    {
      if (subscriberAttribute is PXDBDecimalAttribute decimalAttribute && decimalAttribute.AttributeLevel == PXAttributeLevel.Cache)
      {
        int? nullable = decimalAttribute._Precision;
        try
        {
          decimalAttribute._Precision = new int?();
          decimalAttribute._ensurePrecision(cache, (object) null);
          if (decimalAttribute._Precision.HasValue)
          {
            nullable = new int?(decimalAttribute._Precision.Value);
            cache.SetAltered(decimalAttribute._FieldName, true);
            decimalAttribute._Type = (System.Type) null;
          }
        }
        catch (InvalidOperationException ex)
        {
        }
        finally
        {
          decimalAttribute._Precision = nullable;
        }
      }
    }
  }

  /// <exclude />
  public override void CacheAttached(PXCache sender)
  {
    base.CacheAttached(sender);
    this._IsInitialized = true;
    this.AddVirtualFieldWithSign(sender);
  }

  protected override void ActivateDynamicFields()
  {
    base.ActivateDynamicFields();
    this._signFieldActive = true;
  }

  private bool IsSignFieldActive(PXCache cache)
  {
    return this._signFieldActive || PXDBDecimalAttribute.PXSignAttribute.ShouldBeActiveForCache(cache);
  }

  protected string GetSignedFieldName() => this._FieldName + "_Signed";

  protected virtual void AddVirtualFieldWithSign(PXCache cache)
  {
    if (this.SignFormula == (System.Type) null)
      return;
    string fieldName = this.GetSignedFieldName();
    if (cache.Fields.Contains(fieldName))
      return;
    cache.Fields.Add(fieldName);
    int slotIndex = cache.SetupSlot<Decimal?>((Func<Decimal?>) (() => new Decimal?()), (Func<Decimal?, Decimal?, Decimal?>) ((item, copy) => copy), (Func<Decimal?, Decimal?>) (item => item));
    PXDBDecimalAttribute.PXSignAttribute pxSignAttribute = new PXDBDecimalAttribute.PXSignAttribute();
    pxSignAttribute.SlotIndex = slotIndex;
    pxSignAttribute.BqlTable = this.BqlTable;
    pxSignAttribute.FieldName = fieldName;
    this.SignAttribute = pxSignAttribute;
    cache.RowSelectingWhileReading += (PXRowSelecting) ((sender, args) =>
    {
      if (!this.IsSignFieldActive(sender))
        return;
      this.SignAttribute.RowSelecting(sender, args);
    });
    PXDBCalcedAttribute pxdbCalcedAttribute = new PXDBCalcedAttribute(this.SignFormula, typeof (Decimal));
    pxdbCalcedAttribute.BqlTable = this.BqlTable;
    pxdbCalcedAttribute.FieldName = fieldName;
    PXDBCalcedAttribute attrDBCalced = pxdbCalcedAttribute;
    PXDBDecimalAttribute decimalAttribute = new PXDBDecimalAttribute();
    decimalAttribute.BqlTable = this.BqlTable;
    decimalAttribute.FieldName = fieldName;
    PXDBDecimalAttribute attrDBDecimal = decimalAttribute;
    cache.FieldSelectingEvents.Add(fieldName, (PXFieldSelecting) ((sender, e) =>
    {
      PXFieldState instance = PXDecimalState.CreateInstance((object) sender.GetSlot<Decimal?>(e.Row, slotIndex), this.Precision, fieldName, new bool?(this._IsKey), new int?(-1), new Decimal?(this._MinValue), new Decimal?(this._MaxValue));
      e.ReturnValue = (object) instance;
    }));
    cache.CommandPreparingEvents.Add(fieldName, (PXCommandPreparing) ((sender, e) =>
    {
      if ((e.Operation & PXDBOperation.Delete) == PXDBOperation.Select && this.IsSignFieldActive(sender))
      {
        attrDBDecimal.CommandPreparing(sender, e);
        attrDBCalced.CommandPreparing(sender, e);
        e.Expr?.SetAlias(fieldName);
      }
      else
      {
        Decimal? slot = sender.GetSlot<Decimal?>(e.Row, slotIndex);
        if (!slot.HasValue)
          return;
        e.Value = (object) slot;
      }
    }));
  }

  /// <exclude />
  public override void GetSubscriber<ISubscriber>(List<ISubscriber> subscribers)
  {
    base.GetSubscriber<ISubscriber>(subscribers);
    if (this.SignAttribute == null || !(typeof (ISubscriber) == typeof (IPXRowSelectingSubscriber)))
      return;
    subscribers.Add(this.SignAttribute as ISubscriber);
  }

  protected virtual int? GetItemPrecision(PXView view, object item)
  {
    if (item is PXResult)
      item = ((PXResult) item)[0];
    short? nullable = item != null ? (short?) view.Cache.GetValue(item, ((IBqlSearch) this._Select).GetField().Name) : new short?();
    return !nullable.HasValue ? new int?() : new int?((int) nullable.GetValueOrDefault());
  }

  /// <exclude />
  public class DBDecimalProperties
  {
    public int? _scale;
    public int? _precision;
    public Decimal? _maxValue;
    private ReaderWriterLock _sync = new ReaderWriterLock();

    public int? Scale
    {
      get
      {
        PXReaderWriterScope readerWriterScope;
        ((PXReaderWriterScope) ref readerWriterScope).\u002Ector(this._sync);
        try
        {
          ((PXReaderWriterScope) ref readerWriterScope).AcquireReaderLock();
          return this._scale;
        }
        finally
        {
          readerWriterScope.Dispose();
        }
      }
    }

    public int? Precision
    {
      get
      {
        PXReaderWriterScope readerWriterScope;
        ((PXReaderWriterScope) ref readerWriterScope).\u002Ector(this._sync);
        try
        {
          ((PXReaderWriterScope) ref readerWriterScope).AcquireReaderLock();
          return this._precision;
        }
        finally
        {
          readerWriterScope.Dispose();
        }
      }
    }

    public Decimal? MaxValue
    {
      get
      {
        PXReaderWriterScope readerWriterScope;
        ((PXReaderWriterScope) ref readerWriterScope).\u002Ector(this._sync);
        try
        {
          ((PXReaderWriterScope) ref readerWriterScope).AcquireReaderLock();
          return this._maxValue;
        }
        finally
        {
          readerWriterScope.Dispose();
        }
      }
    }

    public void Fill(System.Type table, string field)
    {
      PXReaderWriterScope readerWriterScope;
      // ISSUE: explicit constructor call
      ((PXReaderWriterScope) ref readerWriterScope).\u002Ector(this._sync);
      try
      {
        ((PXReaderWriterScope) ref readerWriterScope).AcquireReaderLock();
        if (this._scale.HasValue && this._precision.HasValue && this._maxValue.HasValue)
          return;
        ((PXReaderWriterScope) ref readerWriterScope).UpgradeToWriterLock();
        if (this._scale.HasValue && this._precision.HasValue && this._maxValue.HasValue)
          return;
        try
        {
          TableHeader tableStructure = PXDatabase.Provider.GetTableStructure(table.Name);
          if (tableStructure == null)
            return;
          TableColumn tableColumn = tableStructure.Columns.FirstOrDefault<TableColumn>((Func<TableColumn, bool>) (c => string.Equals(((TableEntityBase) c).Name, field, StringComparison.OrdinalIgnoreCase)));
          if (tableColumn != null)
          {
            this._scale = tableColumn.Scale;
            this._precision = new int?(tableColumn.Precision);
            int precision1 = tableColumn.Precision;
            int? nullable1 = tableColumn.Scale;
            int? nullable2 = nullable1.HasValue ? new int?(precision1 - nullable1.GetValueOrDefault()) : new int?();
            int num = 28;
            if (nullable2.GetValueOrDefault() <= num & nullable2.HasValue)
            {
              int precision2 = tableColumn.Precision;
              int? scale = tableColumn.Scale;
              int? nullable3;
              if (!scale.HasValue)
              {
                nullable1 = new int?();
                nullable3 = nullable1;
              }
              else
                nullable3 = new int?(precision2 - scale.GetValueOrDefault());
              nullable1 = nullable3;
              this._maxValue = new Decimal?((Decimal) System.Math.Pow(10.0, (double) nullable1.Value));
            }
            else
              this._maxValue = new Decimal?(Decimal.MaxValue);
          }
          else
          {
            this._scale = new int?(29);
            this._precision = new int?(28);
            this._maxValue = new Decimal?(Decimal.MaxValue);
          }
        }
        catch
        {
        }
      }
      finally
      {
        readerWriterScope.Dispose();
      }
    }

    public bool IsSet
    {
      get
      {
        PXReaderWriterScope readerWriterScope;
        ((PXReaderWriterScope) ref readerWriterScope).\u002Ector(this._sync);
        try
        {
          ((PXReaderWriterScope) ref readerWriterScope).AcquireReaderLock();
          return this._scale.HasValue && this._precision.HasValue && this._maxValue.HasValue;
        }
        finally
        {
          readerWriterScope.Dispose();
        }
      }
    }
  }

  private class PXSignAttribute : PXEventSubscriberAttribute, IPXRowSelectingSubscriber
  {
    public int SlotIndex;

    public void RowSelecting(PXCache sender, PXRowSelectingEventArgs e)
    {
      if (e.Row == null || e.Record == null)
      {
        ++e.Position;
      }
      else
      {
        Decimal? slot = e.Record.GetDecimal(e.Position);
        sender.SetSlot<Decimal?>(e.Row, this.SlotIndex, slot, true);
        ++e.Position;
      }
    }

    public static bool ShouldBeActiveForCache(PXCache cache)
    {
      System.Type type = cache.Graph.GetType();
      return type == typeof (PXGraph) || type == typeof (ReportMaint) || typeof (GenericInquiryDesigner).IsAssignableFrom(type);
    }
  }
}
