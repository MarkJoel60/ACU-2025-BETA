// Decompiled with JetBrains decompiler
// Type: PX.Data.PXDefaultAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;

#nullable disable
namespace PX.Data;

/// <summary>Sets the default value for a DAC field.</summary>
/// <remarks>
///   <para>The <tt>PXDefault</tt> attribute provides the default value for a DAC field. The default value is assigned to the field when the cache raises the
/// <tt>FiedlDefaulting</tt> event. This happens when the a new row is inserted in code or through the user interface.</para>
///   <para>A value specified as default can be a constant or the result of a BQL query. If you provide a BQL query, the attribute will execute it on the
/// <tt>FieldDefaulting</tt> event. You can specify both, in which case the attribute first executes the BQL query and uses the constant if the BQL query returns
/// an empty set. If you provide a DAC field as the BQL query, the attribute takes the value of this field from the cache object's <tt>Current</tt> property. The
/// attribute uses the cache object of the DAC type in which the field is defined.</para>
///   <para>The <tt>PXDefault</tt> attribute also checks that the field value is not <tt>null</tt> before saving to the database. You can adjust this behavior using the
/// <tt>PersistingCheck</tt> property. Its value indicates whether the attribute should check that the value is not <tt>null</tt>, check that the value is not
/// <tt>null</tt> or a blank string, or not check.</para>
///   <para>The attribute can redirect the error that happened on the field to another field if you set the <tt>MapErrorTo</tt> property.</para>
///   <para>You can use the static methods to change the attribute properties for a particular data record in the cache or for all data record in the cache.</para>
/// </remarks>
/// <example>
/// <code title="" lang="CS">
/// //The attribute below sets a constant as the default value.
/// [PXDefault(false)]
/// public virtural bool? IsActive { get; set; }</code>
/// <code title="Example" lang="CS">
/// //The attribute below provides a string constant that is converted to the default value of the specific type.
/// [PXDefault(TypeCode.Decimal, "0.0")]
/// public virtual Decimal? AdjDiscAmt { get; set; }</code>
/// <code title="Example2" description="" lang="CS">
/// //The attribute below will take the default value from the ARPayment cache object
/// //and won't check the field value on saving of the changes to the database.
/// [PXDefault(typeof(ARPayment.adjDate),
///            PersistingCheck = PXPersistingCheck.Nothing)]
/// public virtual DateTime? TillDate { get; set; }</code>
/// <code title="Example3" lang="CS">
/// //The attribute below only prevents the field from being null and does not set a default value.
/// [PXDefault]
/// public virtual string BAccountAcctCD { get; set; }</code>
/// <code title="Example4" lang="CS">
/// //The attribute below will execute the Search BQL query and take the CAEntryType.ReferenceID field value from the result.
/// [PXDefault(typeof(
///     Search&lt;CAEntryType.referenceID,
///         Where&lt;CAEntryType.entryTypeId,
///             Equal&lt;Current&lt;AddTrxFilter.entryTypeID&gt;&gt;&gt;&gt;))]
/// public virtual int? ReferenceID { get; set; }</code>
/// <code title="Example5" lang="CS">
/// //The attribute below will execute the Select BQL query and take the VendorClass.AllowOverrideRate field value
/// //from the result or will use false as the default value if the BQL query returns an empty set.
/// [PXDefault(
///     false,
///     typeof(
///         Select&lt;VendorClass,
///             Where&lt;VendorClass.vendorClassID,
///                   Equal&lt;Current&lt;Vendor.vendorClassID&gt;&gt;&gt;&gt;),
///     SourceField = typeof(VendorClass.allowOverrideRate))]
/// public virtual Boolean? AllowOverrideRate { get; set; }</code>
/// <code title="Example6" lang="CS">
/// //The following example illustrates setting of a new default value to a field at run time.
/// // The view declaration in a graph
/// public PXSelect&lt;ARAdjust&gt; Adjustments;
/// ...
/// // The code executed in some graph method
/// PXDefaultAttribute.SetDefault&lt;ARAdjust.adjdDocType&gt;(Adjustments.Cache, "CRM");</code>
/// <code title="Example7" lang="CS">
/// //The following code shows how to change the way the attribute checks the field value
/// //on saving of the changes to the database.
/// protected virtual void ARPayment_RowSelected(PXCache cache, PXRowSelectedEventArgs e)
/// {
///     ARPayment doc = e.Row as ARPayment;
///     ...
///     PXDefaultAttribute.SetPersistingCheck&lt;ARPayment.depositAfter&gt;(
///         cache, doc,
///         isPayment &amp;&amp; (doc.DepositAsBatch == true)?
///             PXPersistingCheck.NullOrBlank : PXPersistingCheck.Nothing);
///     ...
/// }</code></example>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Parameter)]
[PXAttributeFamily(typeof (PXDefaultAttribute))]
public class PXDefaultAttribute : 
  PXEventSubscriberAttribute,
  IPXFieldDefaultingSubscriber,
  IPXRowPersistingSubscriber,
  IPXFieldSelectingSubscriber
{
  protected bool _CacheGlobal;
  protected System.Type _ReferencedField;
  protected BqlCommand _NeutralSelect;
  protected object _Constant;
  protected System.Type _SourceType;
  protected string _SourceField;
  protected BqlCommand _Select;
  protected IBqlCreator _Formula;
  protected PXPersistingCheck _PersistingCheck;
  protected System.Type _MapErrorTo;
  protected bool _SearchOnDefault = true;

  public virtual System.Type Formula
  {
    get => this._Formula?.GetType();
    set
    {
      if (typeof (IBqlWhere).IsAssignableFrom(value))
        value = BqlCommand.MakeGenericType(typeof (Switch<,>), typeof (Case<,>), value, typeof (True), typeof (False));
      this._Formula = PXFormulaAttribute.InitFormula(value);
    }
  }

  /// <summary>Gets or sets the value that indicates whether the BQL query
  /// specified calculate the default value is executed or ignored. By
  /// default, is <see langword="true" /> (the BQL query is executed).</summary>
  public virtual bool SearchOnDefault
  {
    get => this._SearchOnDefault;
    set => this._SearchOnDefault = value;
  }

  /// <summary>Gets or sets the <see cref="T:PX.Data.PXPersistingCheck">PXPersistingCheck</see> value
  /// that defines how to check the field value for null before saving a
  /// data record to the database. If a value doesn't pass a check, the
  /// attribute will throw the <tt>PXRowPersistingException</tt> exception.
  /// As a result, the save action will fail and the user will get an error
  /// message.</summary>
  /// <remarks>By default, the property equals <tt>PXPersistingCheck.Null</tt>, which disallows null values. Note that for fields that are displayed in the user interface,
  /// this setting also disallows blank values (containing only whitespce characters).</remarks>
  public virtual PXPersistingCheck PersistingCheck
  {
    get => this._PersistingCheck;
    set => this._PersistingCheck = value;
  }

  /// <summary>Gets or sets the value that redirects the error from the
  /// field the attribute is placed on (source field) to another field. If
  /// an error happens on the source field, the error message will be
  /// displayed over the input control of the other field. The property can
  /// be set to a type derived from <tt>IBqlField</tt>. The BQL query is set
  /// in a constructor.</summary>
  public virtual System.Type MapErrorTo
  {
    get => this._MapErrorTo;
    set => this._MapErrorTo = value;
  }

  /// <summary>Gets or sets a constant value that will be used as the
  /// default value.</summary>
  public virtual object Constant
  {
    get => this._Constant;
    set => this._Constant = value;
  }

  /// <summary>Gets or sets the field whose value will be taken from the BQL query result and used as the default value. (The BQL query is set in a constructor.)</summary>
  /// <value>The property can be set to a type derived from <tt>IBqlField</tt>.</value>
  /// <example>
  ///   <code title="Example" description="" lang="CS">
  /// [PXDefault(
  ///     typeof(
  ///         Select&lt;VendorClass,
  ///             Where&lt;VendorClass.vendorClassID,
  ///                   Equal&lt;Current&lt;Vendor.vendorClassID&gt;&gt;&gt;&gt;),
  ///     SourceField = typeof(VendorClass.allowOverrideRate))]
  /// public virtual Boolean? AllowOverrideRate { get; set; }</code>
  /// </example>
  public virtual System.Type SourceField
  {
    get => (System.Type) null;
    set
    {
      if (!(value != (System.Type) null) || !typeof (IBqlField).IsAssignableFrom(value) || !value.IsNested)
        return;
      this._SourceType = BqlCommand.GetItemType(value);
      this._SourceField = value.Name;
    }
  }

  public virtual bool CacheGlobal
  {
    get => this._CacheGlobal;
    set => this._CacheGlobal = value;
  }

  public bool CanDefault
  {
    get => this._Constant != null || this._SourceType != (System.Type) null || this._Select != null;
  }

  /// <summary>Initializes a new instance that calculates the default value
  /// using the provided BQL query.</summary>
  /// <param name="sourceType">The BQL query that is used to calculate the
  /// default value. Accepts the types derived from: <tt>IBqlSearch</tt>,
  /// <tt>IBqlSelect</tt>, <tt>IBqlField</tt>, <tt>IBqlTable</tt>.</param>
  /// <example>
  /// The attribute below will take the default value from the <tt>ARPayment</tt>
  /// cache object and won't check the field value on saving of the changes to
  /// the database. In the second example, the attribute executes the <tt>Search</tt>
  /// BQL query and takes the <tt>CAEntryType.ReferenceID</tt> value from the result.
  /// <code>
  /// [PXDefault(typeof(ARPayment.adjDate),
  ///            PersistingCheck = PXPersistingCheck.Nothing)]
  /// public virtual DateTime? TillDate { get; set; }
  /// </code>
  /// <code>
  /// [PXDefault(typeof(
  ///     Search&lt;CAEntryType.referenceID,
  ///         Where&lt;CAEntryType.entryTypeId,
  ///             Equal&lt;Current&lt;AddTrxFilter.entryTypeID&gt;&gt;&gt;&gt;))]
  /// public virtual int? ReferenceID { get; set; }
  /// </code>
  /// </example>
  public PXDefaultAttribute(System.Type sourceType)
  {
    if (sourceType == (System.Type) null)
      throw new PXArgumentException("type", "The argument cannot be null.");
    if (typeof (IBqlSearch).IsAssignableFrom(sourceType))
    {
      this._Select = BqlCommand.CreateInstance(sourceType);
      this._SourceType = BqlCommand.GetItemType(((IBqlSearch) this._Select).GetField());
      this._SourceField = ((IBqlSearch) this._Select).GetField().Name;
      System.Type[] referencedFields = this._Select.GetReferencedFields(false);
      if (referencedFields.Length != 1 || !(this._Select.GetSelectType().GetGenericTypeDefinition() == typeof (PX.Data.Select<,>)))
        return;
      this._ReferencedField = referencedFields[0];
      this._NeutralSelect = BqlCommand.CreateInstance(typeof (Search<,>), this._ReferencedField, typeof (Where<,>), this._ReferencedField, typeof (Equal<>), typeof (Required<>), this._ReferencedField);
    }
    else if (typeof (IBqlSelect).IsAssignableFrom(sourceType))
    {
      this._Select = BqlCommand.CreateInstance(sourceType);
      this._SourceType = this._Select.GetTables()[0];
      System.Type[] referencedFields = this._Select.GetReferencedFields(false);
      if (referencedFields.Length != 1 || !(this._Select.GetSelectType().GetGenericTypeDefinition() == typeof (PX.Data.Select<,>)))
        return;
      this._ReferencedField = referencedFields[0];
      this._NeutralSelect = BqlCommand.CreateInstance(typeof (Search<,>), this._ReferencedField, typeof (Where<,>), this._ReferencedField, typeof (Equal<>), typeof (Required<>), this._ReferencedField);
    }
    else if (sourceType.IsNested && typeof (IBqlField).IsAssignableFrom(sourceType))
    {
      this._SourceType = BqlCommand.GetItemType(sourceType);
      this._SourceField = sourceType.Name;
    }
    else if (typeof (IBqlTable).IsAssignableFrom(sourceType))
      this._SourceType = sourceType;
    else if (typeof (IConstant).IsAssignableFrom(sourceType))
      this._Constant = ((IConstant) Activator.CreateInstance(sourceType)).Value;
    else
      this.Formula = typeof (IBqlCreator).IsAssignableFrom(sourceType) ? sourceType : throw new PXArgumentException("type", "A foreign key reference cannot be created from the type '{0}'.", new object[1]
      {
        (object) sourceType
      });
  }

  /// <summary>Initializes a new instance that defines the default value as
  /// a constant value.</summary>
  /// <param name="constant">Constant value that is used as the default
  /// value.</param>
  public PXDefaultAttribute(object constant) => this._Constant = constant;

  /// <summary>Initializes a new instance that calculates the default value
  /// using the provided BQL query and uses the constant value if the BQL
  /// query returns nothing. If the BQL query is of <tt>Select</tt> type,
  /// you should also explicitly set the <tt>SourceField</tt> property. If
  /// the BQL query is a DAC field, the attribute will take the value from
  /// the <tt>Current</tt> property of the cache object corresponding to the
  /// DAC.</summary>
  /// <param name="constant">Constant value that is used as the default
  /// value.</param>
  /// <param name="sourceType">The BQL query that is used to calculate the
  /// default value. Accepts the types derived from: <tt>IBqlSearch</tt>,
  /// <tt>IBqlSelect</tt>, <tt>IBqlField</tt>, <tt>IBqlTable</tt>.</param>
  /// <example>
  /// The attribute below will execute the <tt>Select</tt> BQL query and take the
  /// <tt>VendorClass.AllowOverrideRate</tt> field value from the result or will use
  /// <see langword="false" /> as the default value if the BQL query returns an empty set.
  /// <code>
  /// [PXDefault(
  ///     false,
  ///     typeof(
  ///         Select&lt;VendorClass,
  ///             Where&lt;VendorClass.vendorClassID,
  ///                   Equal&lt;Current&lt;Vendor.vendorClassID&gt;&gt;&gt;&gt;),
  ///     SourceField = typeof(VendorClass.allowOverrideRate))]
  /// public virtual Boolean? AllowOverrideRate { get; set; }
  /// </code>
  /// </example>
  public PXDefaultAttribute(object constant, System.Type sourceType)
    : this(sourceType)
  {
    this._Constant = constant;
  }

  /// <summary>Initializes a new instance that does not provide the default
  /// value, but checks whether the field value is not null before saving to
  /// the database.</summary>
  /// <example>
  /// The attribute below only prevents the field from being null and does
  /// not set a default value.
  /// <code>
  /// [PXDefault]
  /// public virtual string BAccountAcctCD { get; set; }
  /// </code>
  /// </example>
  public PXDefaultAttribute()
  {
  }

  /// <summary>Converts the provided string to a specific type and
  /// Initializes a new instance that uses the conversion result as the
  /// default value.</summary>
  /// <param name="converter">The type code that specifies the type to
  /// covert the string to.</param>
  /// <param name="constant">The string representation of the constant used
  /// as the default value.</param>
  /// <example>
  /// The attribute below provides a string constants that is converted to
  /// the default value of the specific type.
  /// <code>
  /// [PXDefault(TypeCode.Decimal, "0.0")]
  /// public virtual Decimal? AdjDiscAmt { get; set; }
  /// </code>
  /// </example>
  public PXDefaultAttribute(TypeCode converter, string constant)
  {
    switch (converter)
    {
      case TypeCode.Boolean:
        this._Constant = (object) bool.Parse(constant);
        break;
      case TypeCode.Char:
        this._Constant = (object) char.Parse(constant);
        break;
      case TypeCode.SByte:
        this._Constant = (object) sbyte.Parse(constant, NumberStyles.Any, (IFormatProvider) CultureInfo.InvariantCulture);
        break;
      case TypeCode.Byte:
        this._Constant = (object) byte.Parse(constant, NumberStyles.Any, (IFormatProvider) CultureInfo.InvariantCulture);
        break;
      case TypeCode.Int16:
        this._Constant = (object) short.Parse(constant, NumberStyles.Any, (IFormatProvider) CultureInfo.InvariantCulture);
        break;
      case TypeCode.UInt16:
        this._Constant = (object) ushort.Parse(constant, NumberStyles.Any, (IFormatProvider) CultureInfo.InvariantCulture);
        break;
      case TypeCode.Int32:
        this._Constant = (object) int.Parse(constant, NumberStyles.Any, (IFormatProvider) CultureInfo.InvariantCulture);
        break;
      case TypeCode.UInt32:
        this._Constant = (object) uint.Parse(constant, NumberStyles.Any, (IFormatProvider) CultureInfo.InvariantCulture);
        break;
      case TypeCode.Int64:
        this._Constant = (object) long.Parse(constant, NumberStyles.Any, (IFormatProvider) CultureInfo.InvariantCulture);
        break;
      case TypeCode.UInt64:
        this._Constant = (object) ulong.Parse(constant, NumberStyles.Any, (IFormatProvider) CultureInfo.InvariantCulture);
        break;
      case TypeCode.Single:
        this._Constant = (object) float.Parse(constant, NumberStyles.Any, (IFormatProvider) CultureInfo.InvariantCulture);
        break;
      case TypeCode.Double:
        this._Constant = (object) double.Parse(constant, NumberStyles.Any, (IFormatProvider) CultureInfo.InvariantCulture);
        break;
      case TypeCode.Decimal:
        this._Constant = (object) Decimal.Parse(constant, NumberStyles.Any, (IFormatProvider) CultureInfo.InvariantCulture);
        break;
      case TypeCode.DateTime:
        this._Constant = (object) System.DateTime.Parse(constant, (IFormatProvider) CultureInfo.InvariantCulture, DateTimeStyles.None);
        break;
      case TypeCode.String:
        this._Constant = (object) constant;
        break;
    }
  }

  /// <summary>Initializes a new instance that determines the default value
  /// using either the provided BQL query or the constant if the BQL query
  /// returns nothing.</summary>
  /// <param name="converter">The type code that specifies the type to
  /// convert the string constant to.</param>
  /// <param name="constant">The string representation of the constant used
  /// as the default value if the BQL query returns nothing.</param>
  /// <param name="sourceType">The BQL command that is used to calculate the
  /// default value. Accepts the types derived from: <tt>IBqlSearch</tt>,
  /// <tt>IBqlSelect</tt>, <tt>IBqlField</tt>, <tt>IBqlTable</tt>.</param>
  public PXDefaultAttribute(TypeCode converter, string constant, System.Type sourceType)
    : this(sourceType)
  {
    switch (converter)
    {
      case TypeCode.Boolean:
        this._Constant = (object) bool.Parse(constant);
        break;
      case TypeCode.Char:
        this._Constant = (object) char.Parse(constant);
        break;
      case TypeCode.SByte:
        this._Constant = (object) sbyte.Parse(constant, NumberStyles.Any, (IFormatProvider) CultureInfo.InvariantCulture);
        break;
      case TypeCode.Byte:
        this._Constant = (object) byte.Parse(constant, NumberStyles.Any, (IFormatProvider) CultureInfo.InvariantCulture);
        break;
      case TypeCode.Int16:
        this._Constant = (object) short.Parse(constant, NumberStyles.Any, (IFormatProvider) CultureInfo.InvariantCulture);
        break;
      case TypeCode.UInt16:
        this._Constant = (object) ushort.Parse(constant, NumberStyles.Any, (IFormatProvider) CultureInfo.InvariantCulture);
        break;
      case TypeCode.Int32:
        this._Constant = (object) int.Parse(constant, NumberStyles.Any, (IFormatProvider) CultureInfo.InvariantCulture);
        break;
      case TypeCode.UInt32:
        this._Constant = (object) uint.Parse(constant, NumberStyles.Any, (IFormatProvider) CultureInfo.InvariantCulture);
        break;
      case TypeCode.Int64:
        this._Constant = (object) long.Parse(constant, NumberStyles.Any, (IFormatProvider) CultureInfo.InvariantCulture);
        break;
      case TypeCode.UInt64:
        this._Constant = (object) ulong.Parse(constant, NumberStyles.Any, (IFormatProvider) CultureInfo.InvariantCulture);
        break;
      case TypeCode.Single:
        this._Constant = (object) float.Parse(constant, NumberStyles.Any, (IFormatProvider) CultureInfo.InvariantCulture);
        break;
      case TypeCode.Double:
        this._Constant = (object) double.Parse(constant, NumberStyles.Any, (IFormatProvider) CultureInfo.InvariantCulture);
        break;
      case TypeCode.Decimal:
        this._Constant = (object) Decimal.Parse(constant, NumberStyles.Any, (IFormatProvider) CultureInfo.InvariantCulture);
        break;
      case TypeCode.DateTime:
        this._Constant = (object) System.DateTime.Parse(constant, (IFormatProvider) CultureInfo.InvariantCulture, DateTimeStyles.None);
        break;
      case TypeCode.String:
        this._Constant = (object) constant;
        break;
    }
  }

  /// <summary>Sets the new default value of the field with the specified
  /// name for a particular data record.</summary>
  /// <param name="cache">The cache object to search for the attributes of
  /// <tt>PXDefault</tt> type.</param>
  /// <param name="data">The data record the method is applied to. If
  /// <tt>null</tt>, the method is applied to all data records in the cache
  /// object.</param>
  /// <param name="field">The name of the field to set the default value
  /// to.</param>
  /// <param name="def">The new default value.</param>
  public static void SetDefault(PXCache cache, object data, string field, object def)
  {
    if (data == null)
      cache.SetAltered(field, true);
    foreach (PXEventSubscriberAttribute attribute in cache.GetAttributes(data, field))
    {
      if (attribute is PXDefaultAttribute)
        ((PXDefaultAttribute) attribute)._Constant = def;
    }
  }

  /// <summary>Sets the new default value of the field with the specified
  /// name for all data records in the cache.</summary>
  /// <param name="cache">The cache object to search for the attributes of
  /// <tt>PXDefault</tt> type.</param>
  /// <param name="field">The name of the field to set the default value
  /// to.</param>
  /// <param name="def">The new default value.</param>
  public static void SetDefault(PXCache cache, string field, object def)
  {
    cache.SetAltered(field, true);
    foreach (PXEventSubscriberAttribute attribute in cache.GetAttributes(field))
    {
      if (attribute is PXDefaultAttribute)
        ((PXDefaultAttribute) attribute)._Constant = def;
    }
  }

  /// <summary>Sets the new default value of the specified field for a
  /// particular data record.</summary>
  /// <param name="cache">The cache object to search for the attributes of
  /// <tt>PXDefault</tt> type.</param>
  /// <param name="data">The data record the method is applied to. If
  /// <tt>null</tt>, the method is applied to all data records kept in the
  /// cache object.</param>
  /// <param name="def">The new default value.</param>
  public static void SetDefault<Field>(PXCache cache, object data, object def) where Field : IBqlField
  {
    if (data == null)
      cache.SetAltered<Field>(true);
    foreach (PXEventSubscriberAttribute attribute in cache.GetAttributes<Field>(data))
    {
      if (attribute is PXDefaultAttribute)
        ((PXDefaultAttribute) attribute)._Constant = def;
    }
  }

  /// <summary>Sets the <tt>PersistingCheck</tt> property for the specifed
  /// field in a particular data record.</summary>
  /// <typeparam name="Field">The field whose attribute is affected.</typeparam>
  /// <param name="cache">The cache object to search for the attributes of
  /// <tt>PXDefault</tt> type.</param>
  /// <param name="data">The data record the method is applied to. If
  /// <tt>null</tt>, the method is applied to all data records kept in the
  /// cache object.</param>
  /// <param name="check">The value that is set to the property.</param>
  /// <example>
  /// The code below changes the way the attribute checks the field value
  /// on saving of the changes to the database.
  /// <code>
  /// protected virtual void ARPayment_RowSelected(PXCache cache, PXRowSelectedEventArgs e)
  /// {
  ///     ARPayment doc = e.Row as ARPayment;
  ///     ...
  ///     PXDefaultAttribute.SetPersistingCheck&lt;ARPayment.depositAfter&gt;(
  ///         cache, doc,
  ///         isPayment &amp;&amp; (doc.DepositAsBatch == true)?
  ///             PXPersistingCheck.NullOrBlank : PXPersistingCheck.Nothing);
  ///     ...
  /// }
  /// </code>
  /// </example>
  public static void SetPersistingCheck<Field>(PXCache cache, object data, PXPersistingCheck check) where Field : IBqlField
  {
    if (data == null)
      cache.SetAltered<Field>(true);
    foreach (PXEventSubscriberAttribute attribute in cache.GetAttributes<Field>(data))
    {
      if (attribute is PXDefaultAttribute)
        ((PXDefaultAttribute) attribute)._PersistingCheck = check;
    }
  }

  /// <summary>Sets the <tt>PersistingCheck</tt> property for the field with
  /// the specified name in a particular data record.</summary>
  /// <param name="cache">The cache object to search for the attributes of
  /// <tt>PXDefault</tt> type.</param>
  /// <param name="field">The field name.</param>
  /// <param name="data">The data record the method is applied to. If
  /// <tt>null</tt>, the method is applied to all data records kept in the
  /// cache object.</param>
  /// <param name="check">The value that is set to the property.</param>
  public static void SetPersistingCheck(
    PXCache cache,
    string field,
    object data,
    PXPersistingCheck check)
  {
    if (data == null)
      cache.SetAltered(field, true);
    foreach (PXEventSubscriberAttribute attribute in cache.GetAttributes(data, field))
    {
      if (attribute is PXDefaultAttribute)
        ((PXDefaultAttribute) attribute)._PersistingCheck = check;
    }
  }

  /// <summary>Sets the new default value of the specified field for all
  /// data records in the cache.</summary>
  /// <param name="cache">The cache object to search for the attributes of
  /// <tt>PXDefault</tt> type.</param>
  /// <param name="def">The new default value.</param>
  /// <example>
  /// The code below sets a new default value to a field at run time.
  /// <code>
  /// // The view declaration in a graph
  /// public PXSelect&lt;ARAdjust&gt; Adjustments;
  /// ...
  /// // The code executed in some graph method
  /// PXDefaultAttribute.SetDefault&lt;ARAdjust.adjdDocType&gt;(Adjustments.Cache, "CRM");
  /// </code>
  /// </example>
  public static void SetDefault<Field>(PXCache cache, object def) where Field : IBqlField
  {
    cache.SetAltered<Field>(true);
    foreach (PXEventSubscriberAttribute attribute in cache.GetAttributes<Field>())
    {
      if (attribute is PXDefaultAttribute)
        ((PXDefaultAttribute) attribute)._Constant = def;
    }
  }

  /// <exclude />
  public static object Select(PXCache cache, PXDefaultAttribute attr, object data)
  {
    Tuple<object, PXCache> tuple = PXDefaultAttribute.SelectRow(cache, attr, data);
    return tuple?.Item2.GetValue(tuple.Item1, attr._SourceField ?? attr._FieldName);
  }

  protected static Tuple<object, PXCache> SelectRow(
    PXCache cache,
    PXDefaultAttribute attr,
    object data)
  {
    object foreignRow = (object) null;
    object key;
    if (attr._CacheGlobal && attr._ReferencedField != (System.Type) null && (key = cache.GetValue(data, attr._ReferencedField.Name)) != null)
    {
      object obj = PXSelectorAttribute.GlobalDictionary.NormalizeKeyFieldValue(key);
      System.Type itemType = BqlCommand.GetItemType(attr._ReferencedField);
      PXSelectorAttribute.GlobalDictionary globalDictionary = PXSelectorAttribute.GlobalDictionary.GetOrCreate(itemType, cache.Graph.Caches[itemType].BqlTable, (byte) 1);
      lock (globalDictionary.SyncRoot)
      {
        PXSelectorAttribute.GlobalDictionary.CacheValue cacheValue;
        if (globalDictionary.TryGetValue(obj, out cacheValue))
        {
          if (!cacheValue.IsDeleted)
          {
            if (!(cacheValue.Item is IDictionary))
              foreignRow = cacheValue.Item;
          }
        }
      }
      PXView view = cache.Graph.TypedViews.GetView(attr._NeutralSelect, false);
      if (foreignRow == null)
      {
        List<object> objectList = view.SelectMulti(key);
        if (objectList.Count == 0)
          return (Tuple<object, PXCache>) null;
        foreignRow = !(objectList[0] is PXResult) ? objectList[0] : ((PXResult) objectList[0])[0];
        if (view.Cache.GetStatus(foreignRow) == PXEntryStatus.Notchanged && !PXDatabase.ReadDeleted && view.Cache.Keys.Count <= 1)
          PXSelectorAttribute.CheckIntegrityAndPutGlobal(globalDictionary, view.Cache, attr._ReferencedField.Name, foreignRow, obj, false);
      }
      return new Tuple<object, PXCache>(foreignRow, view.Cache);
    }
    object obj1 = cache.Graph.TypedViews.GetView(attr._Select, false).SelectSingleBound(new object[1]
    {
      data
    });
    if (obj1 == null)
      return (Tuple<object, PXCache>) null;
    if (obj1 is PXResult)
      obj1 = ((PXResult) obj1)[attr._SourceType];
    return new Tuple<object, PXCache>(obj1, cache.Graph.Caches[attr._SourceType]);
  }

  public static object Select(
    PXGraph graph,
    BqlCommand Select,
    System.Type sourceType,
    string sourceField,
    object row)
  {
    object data = graph.TypedViews.GetView(Select, false).SelectSingleBound(new object[1]
    {
      row
    });
    switch (data)
    {
      case null:
        return (object) null;
      case PXResult _:
        data = ((PXResult) data)[sourceType];
        break;
    }
    return graph.Caches[sourceType].GetValue(data, sourceField);
  }

  /// <summary>Provides the default value</summary>
  /// <param name="sender">Cache</param>
  /// <param name="e">Event arguments to set the NewValue</param>
  /// <exclude />
  public virtual void FieldDefaulting(PXCache sender, PXFieldDefaultingEventArgs e)
  {
    if (e.NewValue != null)
      return;
    if (this._Select == null || this._SearchOnDefault || e.Row != null)
    {
      if (this._Select != null)
      {
        List<BqlCommand> cmds = new List<BqlCommand>();
        if (this._Select is IBqlCoalesce)
        {
          ((IBqlCoalesce) this._Select).GetCommands(cmds);
          foreach (BqlCommand Select in cmds)
          {
            System.Type itemType = BqlCommand.GetItemType(((IBqlSearch) Select).GetField());
            string name = ((IBqlSearch) Select).GetField().Name;
            e.NewValue = PXDefaultAttribute.Select(sender.Graph, Select, itemType, name, e.Row);
            if (e.NewValue != null)
            {
              e.Cancel = true;
              return;
            }
          }
        }
        else
        {
          e.NewValue = PXDefaultAttribute.Select(sender, this, e.Row);
          if (e.NewValue != null)
          {
            e.Cancel = true;
            return;
          }
        }
      }
      else if (this._SourceType != (System.Type) null)
      {
        PXCache cach = sender.Graph.Caches[this._SourceType];
        if (cach.Current != null)
        {
          e.NewValue = cach.GetValue(cach.Current, this._SourceField == null ? this._FieldName : this._SourceField);
          if (e.NewValue != null)
          {
            e.Cancel = true;
            return;
          }
        }
      }
      else if (this._Formula != null)
      {
        bool? result = new bool?();
        object obj = (object) null;
        BqlFormula.Verify(sender, e.Row, this._Formula, ref result, ref obj);
        e.NewValue = obj;
        if (e.NewValue != null)
        {
          e.Cancel = true;
          return;
        }
      }
    }
    if (this._Constant == null)
      return;
    e.NewValue = this._Constant;
  }

  /// <summary>
  /// Check if the value was set before saving the record to the database
  /// </summary>
  /// <param name="sender">Cache</param>
  /// <param name="e">Event arguments to retrive the value from the Row</param>
  /// <exclude />
  public virtual void RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    object obj;
    if (this.PersistingCheck == PXPersistingCheck.Nothing || (e.Operation & PXDBOperation.Delete) != PXDBOperation.Insert && (e.Operation & PXDBOperation.Delete) != PXDBOperation.Update || (obj = sender.GetValue(e.Row, this._FieldOrdinal)) != null && (this.PersistingCheck != PXPersistingCheck.NullOrBlank || !(obj is string) || !(((string) obj).Trim() == string.Empty)))
      return;
    if (this._MapErrorTo == (System.Type) null)
    {
      if (sender.RaiseExceptionHandling(this._FieldName, e.Row, (object) null, (Exception) new PXSetPropertyKeepPreviousException(PXMessages.LocalizeFormat("'{0}' cannot be empty.", (object) $"[{this._FieldName}]"))))
        throw new PXRowPersistingException(this._FieldName, (object) null, "'{0}' cannot be empty.", new object[1]
        {
          (object) this._FieldName
        });
    }
    else
    {
      string name = this._MapErrorTo.Name;
      string str = char.ToUpper(name[0]).ToString() + name.Substring(1);
      object valueExt = sender.GetValueExt(e.Row, str);
      if (valueExt is PXFieldState)
        valueExt = ((PXFieldState) valueExt).Value;
      if (sender.RaiseExceptionHandling(str, e.Row, valueExt, (Exception) new PXSetPropertyKeepPreviousException(PXMessages.LocalizeFormat("An incorrect value in '{0}' resulted in the empty '{1}' field.", (object) str, (object) $"[{this._FieldName}]"))))
        throw new PXRowPersistingException(this._FieldName, (object) null, "'{0}' cannot be empty.", new object[1]
        {
          (object) this._FieldName
        });
    }
  }

  /// <summary>
  /// Provides the default value as a part of the field state
  /// </summary>
  /// <param name="sender">Cache</param>
  /// <param name="e">Event arguments to set the ReturnState</param>
  /// <exclude />
  public virtual void FieldSelecting(PXCache sender, PXFieldSelectingEventArgs e)
  {
    if (this._AttributeLevel != PXAttributeLevel.Item && !e.IsAltered)
      return;
    e.ReturnState = (object) PXFieldState.CreateInstance(e.ReturnState, (System.Type) null, nullable: new bool?(this._Constant == null), required: this._PersistingCheck == PXPersistingCheck.Nothing ? new int?() : new int?(1), defaultValue: this._Constant, fieldName: this._FieldName);
  }
}
