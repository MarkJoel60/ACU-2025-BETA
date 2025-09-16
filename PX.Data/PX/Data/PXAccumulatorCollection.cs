// Decompiled with JetBrains decompiler
// Type: PX.Data.PXAccumulatorCollection
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Data;

/// <summary>Represents a collection of settings for individual fields
/// processed by the <see cref="T:PX.Data.PXAccumulatorAttribute">PXAccumulator</see>
/// attribute.</summary>
/// <remarks>The type is used by the <tt>PXAccumulator</tt> attribute in the <see cref="M:PX.Data.PXAccumulatorAttribute.PrepareInsert(PX.Data.PXCache,System.Object,PX.Data.PXAccumulatorCollection)"> PrepareInsert</see>
/// method. You can use the columns parameters to set updating policies and restrictions.</remarks>
public sealed class PXAccumulatorCollection : Dictionary<string, PXAccumulatorItem>
{
  private readonly PXCache _cache;
  private readonly object _row;

  public List<KeyValuePair<string, PXAccumulatorRestriction[]>> Exceptions { get; private set; }

  /// <summary>Gets or sets the value that indicates whether the attribute
  /// is allowed only to insert new data records in the database table and
  /// is not allowed to update them.</summary>
  public bool InsertOnly { get; set; }

  /// <summary>Gets or sets the value that indicates whether the attribute
  /// is allowed only to update existing data records in the database table
  /// and is not allowed to insert new.</summary>
  public bool UpdateOnly { get; set; }

  public PXAccumulatorCollection(PXCache cache, object row)
    : base((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase)
  {
    this._cache = cache;
    this._row = row;
  }

  /// <summary>Adds a node for the specified field into the collection. The
  /// field is specified through the type parameter.</summary>
  /// <param name="bqlField">The BQL type of the field.</param>
  public void Add<Field>() where Field : IBqlField => this.Add(typeof (Field).Name);

  /// <summary>Adds a node for the specified field into the
  /// collection.</summary>
  /// <param name="bqlField">The BQL type of the field.</param>
  public void Add(System.Type bqlField) => this.Add(bqlField.Name);

  /// <summary>Adds a node for the specified field into the
  /// collection.</summary>
  /// <param name="bqlField">The BQL type of the field.</param>
  public void Add(string field) => this.Add(field, new PXAccumulatorItem(field));

  /// <summary>Remove the setting for the specified field from the
  /// collection. The field is specified through the type
  /// parameter.</summary>
  public void Remove<Field>() where Field : IBqlField => this.Remove(typeof (Field).Name);

  /// <summary>Remove the setting for the specified field from the
  /// collection.</summary>
  /// <param name="bqlField">The BQL type of the field.</param>
  public void Remove(System.Type bqlField) => this.Remove(bqlField.Name);

  /// <summary>Remove the setting for the specified field from the
  /// collection.</summary>
  /// <param name="field">The name of the field.</param>
  public void Remove(string field) => base.Remove(field);

  /// <summary>Configures update of the specified field as addition of the
  /// new value to the value kept in the database. The field is specified
  /// through the type parameter.</summary>
  /// <remarks>The value for update will be taken from the specified field of the processed row.</remarks>
  public void Update<Field>() where Field : IBqlField
  {
    this.Update(typeof (Field).Name, this._cache.GetValue<Field>(this._row), PXDataFieldAssign.AssignBehavior.Summarize);
  }

  /// <summary>The field is specified through the type parameter.</summary>
  /// <param name="behavior">The <see cref="T:PX.Data.PXDataFieldAssign.AssignBehavior" />
  /// value that specifies how the new value of the field is combined with
  /// the database value.</param>
  /// <remarks>The value for update will be taken from the specified field of the processed row.</remarks>
  public void Update<Field>(PXDataFieldAssign.AssignBehavior behavior) where Field : IBqlField
  {
    this.Update(typeof (Field).Name, this._cache.GetValue<Field>(this._row), behavior);
  }

  /// <summary>Configures update of the specified field as addition of the
  /// new value to the value kept in the database. The field is specified
  /// through the type parameter.</summary>
  /// <param name="value">The new value of the field.</param>
  public void Update<Field>(object value) where Field : IBqlField
  {
    this.Update(typeof (Field).Name, value, PXDataFieldAssign.AssignBehavior.Summarize);
  }

  /// <summary>The field is specified through the type parameter.</summary>
  /// <param name="value">The new value of the field.</param>
  /// <param name="behavior">The <see cref="T:PX.Data.PXDataFieldAssign.AssignBehavior" />
  /// value that specifies how the new value of the field is combined with
  /// the database value.</param>
  public void Update<Field>(object value, PXDataFieldAssign.AssignBehavior behavior) where Field : IBqlField
  {
    this.Update(typeof (Field).Name, value, behavior);
  }

  /// <summary>Configures update of the specified field as addition of the
  /// new value to the value kept in the database.</summary>
  /// <param name="bqlField">The BQL type of the field.</param>
  /// <remarks>The value for update will be taken from the specified field of the processed row.</remarks>
  public void Update(System.Type bqlField)
  {
    this.Update(bqlField.Name, this._cache.GetValue(this._row, bqlField.Name), PXDataFieldAssign.AssignBehavior.Summarize);
  }

  /// <param name="bqlField">The BQL type of the field.</param>
  /// <param name="behavior">The <see cref="T:PX.Data.PXDataFieldAssign.AssignBehavior" />
  /// value that specifies how the new value of the field is combined with
  /// the database value.</param>
  /// <remarks>The value for update will be taken from the specified field of the processed row.</remarks>
  public void Update(System.Type bqlField, PXDataFieldAssign.AssignBehavior behavior)
  {
    this.Update(bqlField.Name, this._cache.GetValue(this._row, bqlField.Name), behavior);
  }

  /// <summary>Configures update of the specified field as addition of the
  /// new value to the value kept in the database.</summary>
  /// <param name="bqlField">The BQL type of the field.</param>
  /// <param name="value">The new value of the field.</param>
  public void Update(System.Type bqlField, object value)
  {
    this.Update(bqlField.Name, value, PXDataFieldAssign.AssignBehavior.Summarize);
  }

  /// <param name="bqlField">The BQL type of the field.</param>
  /// <param name="value">The new value of the field.</param>
  /// <param name="behavior">The <see cref="T:PX.Data.PXDataFieldAssign.AssignBehavior" />
  /// value that specifies how the new value of the field is combined with
  /// the database value.</param>
  public void Update(System.Type bqlField, object value, PXDataFieldAssign.AssignBehavior behavior)
  {
    this.Update(bqlField.Name, value, behavior);
  }

  /// <summary>Configures update of the specified field as addition of the
  /// new value to the value kept in the database.</summary>
  /// <param name="field">The name of the field.</param>
  /// <remarks>The value for update will be taken from the specified field of the processed row.</remarks>
  public void Update(string field)
  {
    this.Update(field, this._cache.GetValue(this._row, field), PXDataFieldAssign.AssignBehavior.Summarize);
  }

  /// <param name="field">The name of the field.</param>
  /// <param name="behavior">The <see cref="T:PX.Data.PXDataFieldAssign.AssignBehavior" />
  /// value that specifies how the new value of the field is combined with
  /// the database value.</param>
  /// <remarks>The value for update will be taken from the specified field of the processed row.</remarks>
  public void Update(string field, PXDataFieldAssign.AssignBehavior behavior)
  {
    this.Update(field, this._cache.GetValue(this._row, field), behavior);
  }

  /// <summary>Configures update of the specified field as addition of the
  /// new value to the value kept in the database.</summary>
  /// <param name="field">The name of the field.</param>
  /// <param name="value">The new value of the field.</param>
  public void Update(string field, object value)
  {
    this.Update(field, value, PXDataFieldAssign.AssignBehavior.Summarize);
  }

  /// <param name="field">The name of the field.</param>
  /// <param name="value">The new value of the field.</param>
  /// <param name="behavior">The <see cref="T:PX.Data.PXDataFieldAssign.AssignBehavior" />
  /// value that specifies how the new value of the field is combined with
  /// the database value.</param>
  public void Update(string field, object value, PXDataFieldAssign.AssignBehavior behavior)
  {
    this[field].UpdateCurrent(value, behavior);
  }

  /// <param name="value">The new value of the field.</param>
  public void UpdateFuture<Field>(object value) where Field : IBqlField
  {
    this.UpdateFuture(typeof (Field).Name, value, PXDataFieldAssign.AssignBehavior.Summarize);
  }

  /// <param name="value">The new value of the field.</param>
  /// <param name="behavior">The <see cref="T:PX.Data.PXDataFieldAssign.AssignBehavior" />
  /// value that specifies how the new value of the field is combined with
  /// the database value.</param>
  public void UpdateFuture<Field>(object value, PXDataFieldAssign.AssignBehavior behavior) where Field : IBqlField
  {
    this.UpdateFuture(typeof (Field).Name, value, behavior);
  }

  /// <param name="bqlField">The BQL type of the field.</param>
  /// <param name="value">The new value of the field.</param>
  public void UpdateFuture(System.Type bqlField, object value)
  {
    this.UpdateFuture(bqlField.Name, value, PXDataFieldAssign.AssignBehavior.Summarize);
  }

  /// <param name="bqlField">The BQL type of the field.</param>
  /// <param name="value">The new value of the field.</param>
  /// <param name="behavior">The <see cref="T:PX.Data.PXDataFieldAssign.AssignBehavior" />
  /// value that specifies how the new value of the field is combined with
  /// the database value.</param>
  public void UpdateFuture(System.Type bqlField, object value, PXDataFieldAssign.AssignBehavior behavior)
  {
    this.UpdateFuture(bqlField.Name, value, behavior);
  }

  /// <param name="field">The name of the field.</param>
  /// <param name="value">The new value of the field.</param>
  public void UpdateFuture(string field, object value)
  {
    this.UpdateFuture(field, value, PXDataFieldAssign.AssignBehavior.Summarize);
  }

  /// <param name="field">The name of the field.</param>
  /// <param name="value">The new value of the field.</param>
  /// <param name="behavior">The <see cref="T:PX.Data.PXDataFieldAssign.AssignBehavior" />
  /// value that specifies how the new value of the field is combined with
  /// the database value.</param>
  public void UpdateFuture(string field, object value, PXDataFieldAssign.AssignBehavior behavior)
  {
    this[field].UpdateFuture(value, behavior);
  }

  /// <summary>Adds a value restriction for the field.</summary>
  /// <remarks>If the restriction is violated, the <tt>PXLockViolationException</tt> exception is thrown.
  /// You must handle the exception in the overridden <tt>PXAccumulatorAttribute.PersistInserted()</tt> method.</remarks>
  /// <typeparam name="Field">The field for which the restriction is added.</typeparam>
  /// <param name="comparison">A <tt>PXComp</tt> constant that indicates the restriction condition.</param>
  /// <param name="value">The value to be compared to.</param>
  public void Restrict<Field>(PXComp comparison, object value) where Field : IBqlField
  {
    this.Restrict(typeof (Field).Name, comparison, value);
  }

  /// <summary>Adds a value restriction for the field.</summary>
  /// <remarks>If the restriction is violated, the <tt>PXLockViolationException</tt> exception is thrown.
  /// You must handle the exception in the overridden <tt>PXAccumulatorAttribute.PersistInserted()</tt> method.</remarks>
  /// <param name="bqlField">The field for which the restriction is added.</param>
  /// <param name="comparison">A <tt>PXComp</tt> constant that indicates the restriction condition.</param>
  /// <param name="value">The value to be compared to.</param>
  public void Restrict(System.Type bqlField, PXComp comparison, object value)
  {
    this.Restrict(bqlField.Name, comparison, value);
  }

  /// <summary>Adds a value restriction for the field.</summary>
  /// <remarks>If the restriction is violated, the <tt>PXLockViolationException</tt> exception is thrown.
  /// You must handle the exception in the overridden <tt>PXAccumulatorAttribute.PersistInserted()</tt> method.</remarks>
  /// <param name="field">The field for which the restriction is added.</param>
  /// <param name="comparison">A <tt>PXComp</tt> constant that indicates the restriction condition.</param>
  /// <param name="value">The value to be compared to.</param>
  public void Restrict(string field, PXComp comparison, object value)
  {
    this[field].RestrictCurrent(comparison, value);
  }

  /// <summary>
  /// Sets the minimum for the values of the specified field: Only those records are
  /// affected by the <tt>PXAccumulatorAttribute.PersistInserted()</tt> method whose
  /// field value is equal to or greater than this minimum.
  /// The <tt>RestrictPast</tt> generic method is used when single-record mode is off.
  /// </summary>
  /// <typeparam name="Field">The field for which the minimum value is added.</typeparam>
  /// <param name="comparison">A <tt>PXComp</tt> constant that indicates the restriction condition.</param>
  /// <param name="value">The value to be compared to.</param>
  /// <example>This code prescribes the PXAccumulatorAttribute.PersistInserted method
  /// to affect records whose EPHistory.FinPeriodID field value is related to the same year
  /// as the record being inserted (financial periods from 01 to 99).
  /// <code title="Example" desciption="" lang="CS">
  /// protected override bool PrepareInsert(PXCache sender, object row, PXAccumulatorCollection columns)
  /// {
  ///     if (!base.PrepareInsert(sender, row, columns))
  ///     {
  ///         return false;
  ///     }
  ///     EPHistory hist = (EPHistory)row;
  ///     columns.RestrictPast&lt;EPHistory.finPeriodID&gt;(PXComp.GE, hist.FinPeriodID.Substring(0, 4) + "01");
  ///     columns.RestrictFuture&lt;EPHistory.finPeriodID&gt;(PXComp.LE, hist.FinPeriodID.Substring(0, 4) + "99");
  /// 
  ///     return true;
  /// }
  /// </code>
  /// </example>
  /// <seealso cref="M:PX.Data.PXAccumulatorCollection.RestrictFuture``1(PX.Data.PXComp,System.Object)" />
  public void RestrictPast<Field>(PXComp comparison, object value) where Field : IBqlField
  {
    this.RestrictPast(typeof (Field).Name, comparison, value);
  }

  /// <summary>
  /// Sets the minimum for the values of the specified field: Only those records are
  /// affected by the <tt>PXAccumulatorAttribute.PersistInserted()</tt> method whose
  /// field value is equal to or greater than this minimum.
  /// The <tt>RestrictPast</tt> method is used when single-record mode is off.
  /// </summary>
  /// <param name="bqlField">The field for which the minimum value is added.</param>
  /// <param name="comparison">A <tt>PXComp</tt> constant that indicates the restriction condition.</param>
  /// <param name="value">The value to be compared to.</param>
  /// <see cref="M:PX.Data.PXAccumulatorCollection.RestrictPast``1(PX.Data.PXComp,System.Object)" />
  public void RestrictPast(System.Type bqlField, PXComp comparison, object value)
  {
    this.RestrictPast(bqlField.Name, comparison, value);
  }

  /// <summary>
  /// Sets the minimum for the values of the specified field: Only those records are
  /// affected by the <tt>PXAccumulatorAttribute.PersistInserted()</tt> method whose
  /// field value is equal to or greater than this minimum.
  /// The <tt>RestrictPast</tt> method is used when single-record mode is off.
  /// </summary>
  /// <param name="field">The field for which the minimum value is added.</param>
  /// <param name="comparison">A <tt>PXComp</tt> constant that indicates the restriction condition.</param>
  /// <param name="value">The value to be compared to.</param>
  /// <see cref="M:PX.Data.PXAccumulatorCollection.RestrictPast``1(PX.Data.PXComp,System.Object)" />
  public void RestrictPast(string field, PXComp comparison, object value)
  {
    this[field].RestrictPast(comparison, value);
  }

  /// <summary>
  /// Sets the maximum for the values of the specified field: Only those records are
  /// affected by the <tt>PXAccumulatorAttribute.PersistInserted()</tt> method whose
  /// field value is equal to or less than this maximum.
  /// The <tt>RestrictFuture</tt> generic method is used when single-record mode is off.
  /// </summary>
  /// <example>This code prescribes the PXAccumulatorAttribute.PersistInserted method
  /// to affect records whose EPHistory.FinPeriodID field value is related to the same year
  /// as the record being inserted (financial periods from 01 to 99).
  /// <code title="Example" desciption="" lang="CS">
  /// protected override bool PrepareInsert(PXCache sender, object row, PXAccumulatorCollection columns)
  /// {
  ///     if (!base.PrepareInsert(sender, row, columns))
  ///     {
  ///         return false;
  ///     }
  ///     EPHistory hist = (EPHistory)row;
  ///     columns.RestrictPast&lt;EPHistory.finPeriodID&gt;(PXComp.GE, hist.FinPeriodID.Substring(0, 4) + "01");
  ///     columns.RestrictFuture&lt;EPHistory.finPeriodID&gt;(PXComp.LE, hist.FinPeriodID.Substring(0, 4) + "99");
  /// 
  ///     return true;
  /// }
  /// </code>
  /// </example>
  /// <typeparam name="Field">The field for which the maximum value is added.</typeparam>
  /// <param name="comparison">A <tt>PXComp</tt> constant that indicates the restriction condition.</param>
  /// <param name="value">The value to be compared to.</param>
  /// <seealso cref="M:PX.Data.PXAccumulatorCollection.RestrictPast``1(PX.Data.PXComp,System.Object)" />
  public void RestrictFuture<Field>(PXComp comparison, object value) where Field : IBqlField
  {
    this.RestrictFuture(typeof (Field).Name, comparison, value);
  }

  /// <summary>
  /// Sets the maximum for the values of the specified field: Only those records are
  /// affected by the <tt>PXAccumulatorAttribute.PersistInserted()</tt> method whose
  /// field value is equal to or less than this maximum.
  /// The <tt>RestrictFuture</tt> method is used when single-record mode is off.
  /// </summary>
  /// <param name="bqlField">The field for which the maximum value is added.</param>
  /// <param name="comparison">A <tt>PXComp</tt> constant that indicates the restriction condition.</param>
  /// <param name="value">The value to be compared to.</param>
  /// <see cref="M:PX.Data.PXAccumulatorCollection.RestrictFuture``1(PX.Data.PXComp,System.Object)" />
  public void RestrictFuture(System.Type bqlField, PXComp comparison, object value)
  {
    this.RestrictFuture(bqlField.Name, comparison, value);
  }

  /// <summary>
  /// Sets the maximum for the values of the specified field: Only those records are
  /// affected by the <tt>PXAccumulatorAttribute.PersistInserted()</tt> method whose
  /// field value is equal to or less than this maximum.
  /// The <tt>RestrictFuture</tt> method is used when single-record mode is off.
  /// </summary>
  /// <param name="field">The field for which the maximum value is added.</param>
  /// <param name="comparison">A <tt>PXComp</tt> constant that indicates the restriction condition.</param>
  /// <param name="value">The value to be compared to.</param>
  /// <see cref="M:PX.Data.PXAccumulatorCollection.RestrictFuture``1(PX.Data.PXComp,System.Object)" />
  public void RestrictFuture(string field, PXComp comparison, object value)
  {
    this[field].RestrictFuture(comparison, value);
  }

  /// <summary>The target field and the source fields are specified through
  /// the type parameters.</summary>
  public void InitializeFrom<Field, Source>()
    where Field : IBqlField
    where Source : IBqlField
  {
    this.InitializeFrom(typeof (Field).Name, typeof (Source).Name);
  }

  /// <summary>The field is specified through the type parameter.</summary>
  /// <param name="bqlField">The BQL type of the field.</param>
  public void InitializeFrom<Field>(System.Type source) where Field : IBqlField
  {
    this.InitializeFrom(typeof (Field).Name, source.Name);
  }

  /// <param name="bqlField">The BQL type of the field.</param>
  /// <param name="source"></param>
  public void InitializeFrom(System.Type bqlField, System.Type source)
  {
    this.InitializeFrom(bqlField.Name, source.Name);
  }

  /// <param name="field">The name of the field.</param>
  /// <param name="source"></param>
  public void InitializeFrom(string field, string source) => this[field].InitializeFrom(source);

  /// <summary>The field is specified through the type parameter.</summary>
  /// <param name="value">The new value.</param>
  public void InitializeWith<Field>(object value) where Field : IBqlField
  {
    this.InitializeWith(typeof (Field).Name, value);
  }

  /// <param name="bqlField">The BQL type of the field.</param>
  /// <param name="value">The new value.</param>
  public void InitializeWith(System.Type bqlField, object value)
  {
    this.InitializeWith(bqlField.Name, value);
  }

  /// <param name="field">The name of the field.</param>
  /// <param name="value">The new value.</param>
  public void InitializeWith(string field, object value) => this[field].InitializeWith(value);

  /// <param name="ascending">The value indicating whether data records are
  /// sorted in the ascending order.</param>
  public void OrderBy<Field>(bool ascending) where Field : IBqlField
  {
    this.OrderBy(typeof (Field).Name, ascending);
  }

  /// <param name="bqlField">The BQL type of the field.</param>
  /// <param name="ascending">The value indicating whether data records are
  /// sorted in the ascending order.</param>
  public void OrderBy(System.Type bqlField, bool ascending)
  {
    this.OrderBy(bqlField.Name, ascending);
  }

  /// <param name="field">The name of the field.</param>
  /// <param name="ascending">The value indicating whether data records are
  /// sorted in the ascending order.</param>
  public void OrderBy(string field, bool ascending) => this[field].OrderPast(ascending);

  /// <param name="message"></param>
  /// <param name="params exceptionexception"></param>
  public void AppendException(string message, params PXAccumulatorRestriction[] exception)
  {
    if (exception.Length == 0)
      return;
    if (this.Exceptions == null)
      this.Exceptions = new List<KeyValuePair<string, PXAccumulatorRestriction[]>>();
    this.Exceptions.Add(new KeyValuePair<string, PXAccumulatorRestriction[]>(message, exception));
  }
}
