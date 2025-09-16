// Decompiled with JetBrains decompiler
// Type: PX.Data.PXFormulaAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Data.BQL.AggregateCalculators;
using PX.Data.SQLTree;
using PX.SM;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

#nullable disable
namespace PX.Data;

/// <summary>Calculates a field from other fields of the same data record
/// and sets an aggregation formula to calculate a parent data record
/// field from child data record fields.</summary>
/// <remarks>
///   <para>The attribute assigns the computed value to the field the
/// attribute is attached to. The value is also used for aggregated
/// calculation of the parent data record field (if the aggregate
/// expression has been specified in the attribute parameter).</para>
///   <para>The <see cref="T:PX.Data.PXParentAttribute">PXParent</see> attribute must be added to some field of
/// the child DAC.</para>
///   <para>The attribute can be used on both bound and unbound DAC fields.
/// For unbound fields, the attribute dynamically adds the RowSelecting
/// event handler which tries to calculate the field value when the data
/// record is retrieved from the database. For bound fields, the attribute
/// doesn't calculate the field value when the data record is retrieved
/// from the database. Also, if the <see cref="P:PX.Data.PXFormulaAttribute.Persistent" /> property is set
/// to <see langword="true" />, the attribute recalculates the field value on
/// the RowPersisted event (for bound and unbound fields).</para>
/// </remarks>
/// <example>
///   <code title="Example">//The attribute below sums two fields and assigns it the field the attribute is attached to.
/// [PXFormula(typeof(
///     SOOrder.curyPremiumFreightAmt.Add&lt;SOOrder.curyFreightAmt&gt;))]
/// public virtual Decimal? CuryFreightTot { get; set; }</code>
///   <code title="Example2">//This example shows a more complex calculation.
/// [PXFormula(typeof(
///     SOOrder.curyOrderTotal.
///         When&lt;SOOrder.releasedCntr.Add&lt;SOOrder.billedCntr&gt;.
///             IsEqual&lt;short0&gt;&gt;.
///     Else&lt;SOOrder.curyUnbilledOrderTotal.Add&lt;SOOrder.curyFreightTot&gt;&gt;
/// ))]
/// public decimal? CuryDocBal { get; set; }</code>
///   <code title="Example3">//The attribute below multiplies the TranQty and UnitPrice fields
/// //and assigns the result to the ExtPrice field. The attribute also calculates
/// //the sum of the computed ExtPrice values over all child DocTransaction data records
/// //and assigns the result to the parent's TotalAmt field.
/// [PXUIField(DisplayName = "Line Total", Enabled = false)]
/// [PXFormula(
///     typeof(DocTransaction.tranQty.Multiply&lt;DocTransaction.unitPrice&gt;),
///     typeof(SumCalc&lt;Document.totalAmt&gt;))]
/// public virtual decimal? ExtPrice { get; set; }</code>
///   <code title="Example4">//The attribute below does not provide a formula for calculating the TranQty property.
/// //The value inputted by a user is assigned to the field.
/// //The attribute only sets the formula to calculate the TotalQty field
/// //in the parent data record as the sum of TranQty values over all related child data records.
/// [PXFormula(null, typeof(SumCalc&lt;Document.totalQty&gt;))]
/// public virtual decimal? TranQty { get; set; }</code>
/// </example>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Parameter, AllowMultiple = true)]
public class PXFormulaAttribute : 
  PXEventSubscriberAttribute,
  IPXRowUpdatedSubscriber,
  IPXRowInsertedSubscriber,
  IPXRowDeletedSubscriber,
  IPXDependsOnFields
{
  protected IBqlCreator _Formula;
  protected System.Type _ParentFieldType;
  protected object _Aggregate;
  protected HashSet<System.Type> _Dependencies;
  protected bool _Persistent;
  protected string _FieldClass;
  protected PXEventSubscriberAttribute.ObjectRef<bool> _recursion;
  protected bool _CancelDefaulting;

  /// <summary>Get the name of the field the attribute is attached
  /// to.</summary>
  public virtual string FormulaFieldName => this._FieldName;

  /// <summary>Gets or sets the BQL query that is used to calculate the
  /// value of the field the attribute is attached to. The value should
  /// derive from <tt>Constant&lt;&gt;</tt>, <tt>IBqlField</tt>, or
  /// <tt>IBqlCreator</tt>.</summary>
  public virtual System.Type Formula
  {
    get => this._Formula.GetType();
    set => this._Formula = PXFormulaAttribute.InitFormula(value);
  }

  /// <summary>Gets or sets the parent data record field the aggregation
  /// result is assigned to. The value should derive from
  /// <tt>IBqlField</tt>.</summary>
  public virtual System.Type ParentField
  {
    get => this._ParentFieldType;
    set => this._ParentFieldType = value;
  }

  /// <summary>Gets or sets the BQL query that represents the aggregation
  /// formula used to calculate the parent data record field from the child
  /// data records fields. The value should derive from
  /// <tt>IBqlAggregateCalculator</tt>.</summary>
  public virtual System.Type Aggregate
  {
    get => this._Aggregate?.GetType();
    set => this.InitAggregate(value);
  }

  /// <summary>Gets or sets a value indicating whether the formula should
  /// update <see cref="P:PX.Data.PXFormulaAttribute.ParentField">field</see> of the parent record during
  /// Insert, Update, Delete operations.</summary>
  public virtual bool EnableAggregation { get; set; } = true;

  /// <summary>Gets or sets the value that indicates whether the attribute
  /// recalculates the formula for the child field after a saving of changes
  /// to the database. You may need recalculation if the fields the formula
  /// depends on are updated on the <tt>RowPersisting</tt> event. By
  /// default, the property equals <see langword="false" />.</summary>
  public virtual bool Persistent
  {
    get => this._Persistent;
    set => this._Persistent = value;
  }

  public virtual string FieldClass
  {
    get => this._FieldClass;
    set => this._FieldClass = value;
  }

  protected bool IsRestricted
  {
    get
    {
      string[] fieldClassRestricted = PXAccess.Provider.FieldClassRestricted;
      return fieldClassRestricted != null && fieldClassRestricted.Length != 0 && !string.IsNullOrEmpty(this._FieldClass) && Array.IndexOf<string>(fieldClassRestricted, this._FieldClass) >= 0;
    }
  }

  /// <summary>
  /// Gets or sets a value indicating whether the formula should
  /// not recalculate during defaulting, insertion, dependent
  /// field updating etc.
  /// </summary>
  public bool CancelCalculation { get; set; }

  /// <summary>
  /// Gets or sets a value indicating whether the formula should
  /// not raise RowUpdated event on updating parent record.
  /// </summary>
  public bool CancelParentUpdate { get; set; }

  /// <summary>
  /// Gets or sets a value indicating that aggregate field will be
  /// recalculated from scratch on every change of child records.
  /// </summary>
  public virtual bool ForceAggregateRecalculation { get; set; }

  /// <summary>
  /// Gets or sets a value indicating that aggregate calculation  will be
  /// validated when records are saved to the database.
  /// </summary>
  public virtual bool ValidateAggregateCalculation { get; set; }

  /// <summary>
  /// Gets or sets a value indicating that zero updates of parent records will be skipped.
  /// </summary>
  public virtual bool SkipZeroUpdates { get; set; } = true;

  /// <summary>
  /// Gets or sets a value indicating that SetValueExt call (and following it
  /// FieldVerifying and FieldUpdated events) will be performed even if the
  /// newly calculated formula value equals to the previous one.
  /// </summary>
  public virtual bool KeepIdleSelfUpdates { get; set; }

  /// <exclude />
  public virtual ISet<System.Type> GetDependencies(PXCache cache)
  {
    if (this._Dependencies == null)
    {
      this._Dependencies = new HashSet<System.Type>();
      if (this._Formula != null)
      {
        List<System.Type> typeList = new List<System.Type>();
        SQLExpression sqlExpression = SQLExpression.None();
        IBqlCreator formula = this._Formula;
        ref SQLExpression local = ref sqlExpression;
        PXGraph graph = cache.Graph;
        BqlCommandInfo info = new BqlCommandInfo(false);
        info.Fields = typeList;
        info.BuildExpression = false;
        BqlCommand.Selection selection = new BqlCommand.Selection();
        formula.AppendExpression(ref local, graph, info, selection);
        EnumerableExtensions.AddRange<System.Type>((ISet<System.Type>) this._Dependencies, (IEnumerable<System.Type>) typeList);
      }
    }
    return (ISet<System.Type>) this._Dependencies;
  }

  /// <summary>Initializes a new instance that calculates the value of the
  /// field the attribute is atached to, by the provided formula.</summary>
  /// <param name="formulaType">The formula to calculate the field value
  /// from other fields of the same data record. The formula can be an
  /// expression built from BQL functions such as <tt>Add</tt>,
  /// <tt>Sub</tt>, <tt>Mult</tt>, <tt>Div</tt>, <tt>Switch</tt> and <a href="BQL_Functions_For_Formulas.html">other
  /// functions</a>.</param>
  public PXFormulaAttribute(System.Type formulaType)
  {
    this._Formula = PXFormulaAttribute.InitFormula(formulaType);
  }

  /// <summary>Initializes a new instance that calculates the value of the
  /// field the attribute is atached to and sets an aggregate function to
  /// calculate the value of a field in the parent data record. The
  /// aggregation function is applied to the values calculated by the first
  /// parameter for all child data records.</summary>
  /// <param name="formulaType">The formula to calculate the field value
  /// from other fields of the same data record. The formula can be an
  /// expression built from BQL functions such as <tt>Add</tt>,
  /// <tt>Sub</tt>, <tt>Mult</tt>, <tt>Div</tt>, <tt>Switch</tt> and <a href="BQL_Functions_For_Formulas.html">other functions</a>. If
  /// <tt>null</tt>, the aggregation function takes into account the field
  /// value inputted by the user.</param>
  /// <param name="aggregateType">The aggregation formula to calculate the
  /// parent data record field from the child data records fields. Use an <a href="BQL_Formulas.html">aggregation function</a> such as
  /// <tt>SumCalc</tt>, <tt>CountCalc</tt>, <tt>MinCalc</tt>, and
  /// <tt>MaxCalc</tt>.</param>
  /// <example>
  /// <para>The attribute below multiplies the <tt>TranQty</tt> and
  /// <tt>UnitPrice</tt> fields and assigns the result to the
  /// <tt>ExtPrice</tt> field. The attribute also calculates the sum of the computed
  /// <tt>ExtPrice</tt> values over all child <tt>DocTransaction</tt> data
  /// records and assigns the result to the parent's <tt>TotalAmt</tt> field.
  /// A common practice is to disable the input control for a calculated field. In the example
  /// below, the control is disabled using the <see cref="T:PX.Data.PXUIFieldAttribute">PXUIField</see>
  /// attribute.</para>
  /// <para>In the second example, the attribute does not provide a formula for calculating
  /// the <tt>TranQty</tt> property. The value inputted by a user is assigned to the field.
  /// The attribute only sets the formula to calculate the <tt>TotalQty</tt> field in the
  /// parent data record as the sum of <tt>TranQty</tt> values over all related child data
  /// records.</para>
  /// <code>
  /// [PXUIField(DisplayName = "Line Total", Enabled = false)]
  /// [PXFormula(
  ///     typeof(Mult&lt;DocTransaction.tranQty, DocTransaction.unitPrice&gt;),
  ///     typeof(SumCalc&lt;Document.totalAmt&gt;))]
  /// public virtual decimal? ExtPrice { get; set; }
  /// </code>
  /// <code>
  /// [PXFormula(null, typeof(SumCalc&lt;Document.totalQty&gt;))]
  /// public virtual decimal? TranQty { get; set; }
  /// </code>
  /// </example>
  public PXFormulaAttribute(System.Type formulaType, System.Type aggregateType)
  {
    this._Formula = PXFormulaAttribute.InitFormula(formulaType);
    this.InitAggregate(aggregateType);
  }

  /// <exclude />
  public static IBqlCreator InitFormula(System.Type formulaType)
  {
    if (!(formulaType != (System.Type) null))
      return (IBqlCreator) null;
    System.Type type = formulaType;
    if (typeof (IBqlField).IsAssignableFrom(formulaType))
      type = typeof (Row<>).MakeGenericType(formulaType);
    else if (typeof (IConstant).IsAssignableFrom(formulaType))
      type = typeof (Const<>).MakeGenericType(formulaType);
    else if (typeof (IBqlWhere).IsAssignableFrom(formulaType))
      type = BqlCommand.MakeGenericType(typeof (Switch<,>), typeof (Case<,>), formulaType, typeof (True), typeof (False));
    else if (typeof (IBqlUnary).IsAssignableFrom(formulaType))
      type = BqlCommand.MakeGenericType(typeof (Switch<,>), typeof (Case<,>), typeof (Where<>), formulaType, typeof (True), typeof (False));
    else if (!typeof (IBqlCreator).IsAssignableFrom(formulaType))
      throw new PXArgumentException(nameof (formulaType), "The formula '{0}' cannot be created.", new object[1]
      {
        (object) formulaType.Name
      });
    return (IBqlCreator) Activator.CreateInstance(type);
  }

  protected virtual void InitAggregate(System.Type aggregateType)
  {
    if (aggregateType != (System.Type) null)
    {
      this._ParentFieldType = aggregateType.GetGenericArguments()[0];
      if (!typeof (IBqlField).IsAssignableFrom(this._ParentFieldType))
        throw new PXArgumentException("_ParentFieldType", "The parent field '{0}' cannot be obtained.", new object[1]
        {
          (object) this._ParentFieldType.Name
        });
      this._Aggregate = typeof (IBqlAggregateCalculator).IsAssignableFrom(aggregateType) ? (object) (IBqlAggregateCalculator) Activator.CreateInstance(aggregateType) : throw new PXArgumentException("_Aggregate", "The aggregate type {0} cannot be found.", new object[1]
      {
        (object) aggregateType.Name
      });
    }
    else
    {
      this._ParentFieldType = (System.Type) null;
      this._Aggregate = (object) null;
    }
  }

  /// <summary>Sets the new aggregation formula in the attribute instances
  /// that mark the specified field, for all data records in the cache
  /// object.</summary>
  /// <param name="sender">The cache object to search for the attributes of
  /// <tt>PXFormula</tt> type.</param>
  /// <param name="aggregateType">The new aggregation formula that will be
  /// used to calculate the parent data record field from the child data
  /// records fields.</param>
  public static void SetAggregate<Field>(PXCache sender, System.Type aggregateType, System.Type parentFieldType = null) where Field : IBqlField
  {
    foreach (PXFormulaAttribute formulaAttribute in sender.GetAttributes<Field>().OfType<PXFormulaAttribute>().Where<PXFormulaAttribute>((Func<PXFormulaAttribute, bool>) (attr => parentFieldType == (System.Type) null || parentFieldType == attr.ParentField)))
      formulaAttribute.Aggregate = aggregateType;
  }

  /// <summary>Sets the flag that determines if the formula aggregation
  /// should be performed during Insert, Update, Delete operation.</summary>
  /// <param name="sender">The cache object to search for the attributes of
  /// <tt>PXFormula</tt> type.</param>
  /// <param name="enableFlag">The flag value</param>
  /// 
  ///             cache object.
  ///             <param name="parentFieldType">The parent field for which the property value
  /// is to be set.</param>
  public static void SetAggregation<Field>(
    PXCache sender,
    bool EnableAggregationFlag,
    System.Type parentFieldType = null)
    where Field : IBqlField
  {
    foreach (PXFormulaAttribute formulaAttribute in sender.GetAttributes<Field>().OfType<PXFormulaAttribute>().Where<PXFormulaAttribute>((Func<PXFormulaAttribute, bool>) (attr => parentFieldType == (System.Type) null || parentFieldType == attr.ParentField)))
      formulaAttribute.EnableAggregation = EnableAggregationFlag;
  }

  /// <summary>Calculates the fields of the parent data record using the
  /// aggregation formula from the attribute instance that marks the
  /// specified field. The calculation is applied to the child data records
  /// merged with the modifications kept in the session.</summary>
  /// <param name="sender">The cache object to search for the attributes of
  /// <tt>PXFormula</tt> type.</param>
  /// <param name="parent">The parent data record.</param>
  public static void CalcAggregate<Field>(PXCache sender, object parent) where Field : IBqlField
  {
    PXFormulaAttribute.CalcAggregate<Field>(sender, parent, false);
  }

  public static void CalcAggregate<Field>(PXCache sender, object parent, bool IsReadOnly) where Field : IBqlField
  {
    List<PXFormulaAttribute> list = sender.GetAttributesReadonly<Field>().OfType<PXFormulaAttribute>().Where<PXFormulaAttribute>((Func<PXFormulaAttribute, bool>) (attribute => attribute._Aggregate != null)).ToList<PXFormulaAttribute>();
    System.Type ParentType = parent.GetType();
    using (IEnumerator<PXView> enumerator = sender.GetAttributesReadonly((string) null).OfType<PXParentAttribute>().Where<PXParentAttribute>((Func<PXParentAttribute, bool>) (parentAttribute => parentAttribute.ParentType == ParentType || ParentType.IsSubclassOf(parentAttribute.ParentType))).Select<PXParentAttribute, PXView>((Func<PXParentAttribute, PXView>) (parentAttribute => parentAttribute.GetChildrenSelect(sender, IsReadOnly))).GetEnumerator())
    {
      if (!enumerator.MoveNext())
        return;
      List<object> objectList = enumerator.Current.SelectMultiBound(new object[1]
      {
        parent
      });
      foreach (object obj in objectList)
        sender.RaiseRowSelected(obj);
      foreach (PXFormulaAttribute formulaAttribute in list)
        formulaAttribute.CalcAggregate(sender, objectList.ToArray(), parent);
    }
  }

  public static void CalcAggregate<Field>(
    PXCache sender,
    object row,
    object oldrow,
    System.Type parentFieldType)
    where Field : IBqlField
  {
    foreach (PXFormulaAttribute formulaAttribute in sender.GetAttributes<Field>().OfType<PXFormulaAttribute>().Where<PXFormulaAttribute>((Func<PXFormulaAttribute, bool>) (attr => parentFieldType == (System.Type) null || parentFieldType == attr.ParentField)))
    {
      bool enableAggregation = formulaAttribute.EnableAggregation;
      try
      {
        formulaAttribute.EnableAggregation = true;
        formulaAttribute.UpdateParent(sender, row, oldrow);
      }
      finally
      {
        formulaAttribute.EnableAggregation = enableAggregation;
      }
    }
  }

  public static object Evaluate<Field>(PXCache sender, object row) where Field : IBqlField
  {
    return PXFormulaAttribute.Evaluate(sender, typeof (Field).Name, row);
  }

  public static object Evaluate(PXCache sender, string field, object row)
  {
    PXFormulaAttribute formulaAttribute = sender.GetAttributesReadonly(field).OfType<PXFormulaAttribute>().FirstOrDefault<PXFormulaAttribute>();
    if (formulaAttribute == null)
      return (object) null;
    using (new PXConnectionScope())
    {
      PXFieldDefaultingEventArgs e = new PXFieldDefaultingEventArgs(row);
      formulaAttribute.FormulaDefaulting(sender, e);
      return e.NewValue;
    }
  }

  protected object EnsureParent(PXCache cache, object Row)
  {
    return this.EnsureParent(cache, Row, (object) null);
  }

  protected virtual object EnsureParent(PXCache cache, object Row, object NewRow)
  {
    if (this._ParentFieldType == (System.Type) null)
      return (object) null;
    System.Type ParentType = typeof (IBqlField).IsAssignableFrom(this._ParentFieldType) ? BqlCommand.GetItemType(this._ParentFieldType) : throw new PXArgumentException("_ParentFieldType", "Invalid field: '{0}'", new object[1]
    {
      (object) this._ParentFieldType.Name
    });
    return PXParentAttribute.SelectParent(cache, Row, NewRow, ParentType) ?? PXParentAttribute.CreateParent(cache, Row, ParentType);
  }

  protected virtual void EnsureChildren(PXCache cache, object Row)
  {
    if (this._ParentFieldType == (System.Type) null || !(this._Aggregate is ICountCalc))
      return;
    System.Type type = typeof (IBqlField).IsAssignableFrom(this._ParentFieldType) ? BqlCommand.GetItemType(this._ParentFieldType) : throw new PXArgumentException("_ParentFieldType", "Invalid field: '{0}'", new object[1]
    {
      (object) this._ParentFieldType.Name
    });
    PXCache cach = cache.Graph.Caches[type];
    if (!PXParentAttribute.GetParentCreate(cache, Row, type))
      return;
    object data = PXParentAttribute.SelectParent(cache, Row, type);
    if (data == null)
      return;
    object obj = cach.GetValue(data, this._ParentFieldType.Name);
    if ((!(obj is int num1) || num1 > 0) && (!(obj is short num2) || num2 > (short) 0))
      return;
    cach.Delete(data);
  }

  /// <exclude />
  public virtual void RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    using (new PXPerformanceInfoTimerScope((Func<PXPerformanceInfo, Stopwatch>) (info => info.TmFormulaCalculations)))
    {
      this.UpdateParent(sender, e.Row, e.OldRow, new Func<PXCache, object, object>(this.EnsureParent));
      this.EnsureChildren(sender, e.OldRow);
    }
  }

  /// <exclude />
  public virtual void RowInserted(PXCache sender, PXRowInsertedEventArgs e)
  {
    using (new PXPerformanceInfoTimerScope((Func<PXPerformanceInfo, Stopwatch>) (info => info.TmFormulaCalculations)))
    {
      bool? result = new bool?();
      object newValue = (object) null;
      if (this._Formula != null && !this._CancelDefaulting && !this.CancelCalculation && sender.GetValue(e.Row, this._FieldOrdinal) == null)
      {
        BqlFormula.Verify(sender, e.Row, this._Formula, ref result, ref newValue);
        if (newValue != PXCache.NotSetValue)
        {
          sender.RaiseFieldUpdating(this._FieldName, e.Row, ref newValue);
          sender.SetValue(e.Row, this._FieldOrdinal, newValue);
        }
      }
      if (this._Formula == null && !this._CancelDefaulting && !this.CancelCalculation && sender.GetValue(e.Row, this._FieldOrdinal) == null)
      {
        using (IEnumerator<PXFormulaAttribute> enumerator = sender.GetAttributesReadonly(this._FieldName).OfType<PXFormulaAttribute>().Where<PXFormulaAttribute>((Func<PXFormulaAttribute, bool>) (attribute => attribute._Formula != null && attribute.FormulaFieldName != null)).GetEnumerator())
        {
          if (enumerator.MoveNext())
          {
            PXFormulaAttribute current = enumerator.Current;
            BqlFormula.Verify(sender, e.Row, current._Formula, ref result, ref newValue);
            sender.RaiseFieldUpdating(this._FieldName, e.Row, ref newValue);
            sender.SetValue(e.Row, this._FieldOrdinal, newValue);
          }
        }
      }
      object parent = this.EnsureParent(sender, e.Row, e.PendingRow);
      this.UpdateParent(sender, e.Row, (object) null, (Func<PXCache, object, object>) ((cache, row) => parent));
    }
  }

  /// <exclude />
  public virtual void RowDeleted(PXCache sender, PXRowDeletedEventArgs e)
  {
    using (new PXPerformanceInfoTimerScope((Func<PXPerformanceInfo, Stopwatch>) (info => info.TmFormulaCalculations)))
    {
      this.UpdateParent(sender, (object) null, e.Row);
      this.EnsureChildren(sender, e.Row);
    }
  }

  /// <exclude />
  public virtual void FormulaDefaulting(PXCache sender, PXFieldDefaultingEventArgs e)
  {
    if (this.CancelCalculation)
      return;
    bool? result = new bool?();
    object newValue = (object) null;
    if (e.Row != null)
      this._CancelDefaulting = e.Cancel;
    if (e.Row == null || e.Cancel)
      return;
    this._recursion.Value = !this._recursion.Value ? true : throw new PXException($"There is a circular reference in the calculated field {this._BqlTable.FullName}.{this._FieldName} (formula: {this._Formula.GetType().Name})");
    try
    {
      BqlFormula.Verify(sender, e.Row, this._Formula, ref result, ref newValue);
    }
    finally
    {
      this._recursion.Value = false;
    }
    if (newValue == null || newValue == PXCache.NotSetValue)
    {
      bool? nullable = result;
      bool flag = true;
      if (!(nullable.GetValueOrDefault() == flag & nullable.HasValue))
        return;
    }
    sender.RaiseFieldUpdating(this._FieldName, e.Row, ref newValue);
    e.NewValue = newValue;
    e.Cancel = true;
  }

  protected virtual void dependentFieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e,
    System.Type dependentField)
  {
    if (this.CancelCalculation)
      return;
    if (this._Formula is IBqlTrigger formula)
    {
      formula.Verify(sender, this._FieldName, e.Row);
    }
    else
    {
      bool flag = false;
      bool? result = new bool?();
      object objB = (object) null;
      BqlFormula.ItemContainer itemContainer = new BqlFormula.ItemContainer(sender.CreateCopy(e.Row), dependentField, e.ExternalCall, sender.GetValuePending(e.Row, this._FieldName));
      try
      {
        BqlFormula.Verify(sender, (object) itemContainer, this._Formula, ref result, ref objB);
      }
      catch (PXSetPropertyException ex)
      {
        sender.SetValue(e.Row, dependentField.Name, e.OldValue);
        sender.RaiseExceptionHandling(this._FieldName, e.Row, sender.GetValue(e.Row, this._FieldName), (Exception) ex);
        flag = true;
      }
      object objA = sender.GetValue(e.Row, this._FieldName);
      if (flag || objB == PXCache.NotSetValue || !itemContainer.InvolvedFields.Contains(dependentField) || !this.KeepIdleSelfUpdates && object.Equals(objA, objB))
        return;
      sender.SetValueExt(e.Row, this._FieldName, (object) new PXCache.ExternalCallMarker(objB)
      {
        IsInternalCall = true
      });
    }
  }

  protected virtual bool IsZeroValue(object val)
  {
    return this._Aggregate is IBqlZeroValueChecker aggregate && aggregate.IsZeroValue(val);
  }

  protected virtual object CalcAggregate(PXCache cache, object row, object oldrow, int digit)
  {
    return ((IBqlAggregateCalculator) this._Aggregate).Calculate(cache, row, oldrow, this._FieldOrdinal, -1);
  }

  protected virtual object CalcAggregate(PXCache cache, object row, object[] records, int digit)
  {
    return ((IBqlAggregateCalculator) this._Aggregate).Calculate(cache, row, this._FieldOrdinal, records, -1);
  }

  protected virtual void UpdateParent(
    PXCache sender,
    object row,
    object oldrow,
    Func<PXCache, object, object> ensureNewParentFunc = null)
  {
    if (this._ParentFieldType == (System.Type) null || this._Aggregate == null || !this.EnableAggregation)
      return;
    System.Type type = typeof (IBqlField).IsAssignableFrom(this._ParentFieldType) ? BqlCommand.GetItemType(this._ParentFieldType) : throw new PXArgumentException("_ParentFieldType", "Invalid field: '{0}'", new object[1]
    {
      (object) this._ParentFieldType.Name
    });
    bool attributeFound;
    bool useCurrent;
    bool flag = PXParentAttribute.IsParentReferenceTheSame(sender, row, oldrow, type, out attributeFound, out useCurrent);
    Lazy<object> lazy = Lazy.By<object>((Func<object>) (() => this.CalcAggregate(sender, row, oldrow, -1)));
    if (!attributeFound || this.SkipZeroUpdates && (flag || oldrow == null) && this.IsZeroValue(lazy.Value))
      return;
    object obj1 = (object) null;
    if (ensureNewParentFunc != null)
      obj1 = ensureNewParentFunc(sender, row);
    else if (row != null)
      obj1 = PXParentAttribute.SelectParent(sender, row, type);
    object obj2 = (object) null;
    if (oldrow != null && !flag | useCurrent)
    {
      obj2 = PXParentAttribute.SelectParent(sender, oldrow, type);
      if (obj2 != obj1 && obj1 != null)
      {
        this.UpdateParent(sender, (object) null, oldrow);
        this.UpdateParent(sender, row, (object) null);
        return;
      }
    }
    if (obj1 == null)
      row = (object) null;
    object row1 = row ?? oldrow;
    object obj3 = obj1 ?? obj2;
    if (obj3 == null)
      return;
    PXCache cach = sender.Graph.Caches[type];
    bool curyViewState = sender.Graph.Accessinfo.CuryViewState;
    object stateExt;
    try
    {
      sender.Graph.Accessinfo.CuryViewState = false;
      stateExt = cach.GetStateExt(obj3, this._ParentFieldType.Name);
    }
    finally
    {
      sender.Graph.Accessinfo.CuryViewState = curyViewState;
    }
    int digit = -1;
    TypeCode tc = TypeCode.Empty;
    if (stateExt is PXFieldState)
    {
      tc = System.Type.GetTypeCode(((PXFieldState) stateExt).DataType);
      digit = ((PXFieldState) stateExt).Precision;
      stateExt = ((PXFieldState) stateExt).Value;
    }
    else if (stateExt != null)
      tc = System.Type.GetTypeCode(stateExt.GetType());
    if (tc == TypeCode.String)
    {
      stateExt = cach.GetValue(obj3, this._ParentFieldType.Name);
      if (stateExt != null)
        tc = System.Type.GetTypeCode(stateExt.GetType());
      if (tc == TypeCode.String)
        return;
    }
    object val = (object) null;
    if (stateExt != null && !this.ForceAggregateRecalculation)
      val = this.CalcAggregate(sender, row, oldrow, digit);
    if (val == null)
    {
      object[] records = PXParentAttribute.SelectSiblings(sender, row1, type);
      val = this.CalcAggregate(sender, row, records, -1);
    }
    else
    {
      switch (tc)
      {
        case TypeCode.Int16:
          val = (object) ((int) Convert.ToInt16(stateExt) + (int) Convert.ToInt16(val));
          break;
        case TypeCode.Int32:
          val = (object) (Convert.ToInt32(stateExt) + Convert.ToInt32(val));
          break;
        case TypeCode.Double:
          val = (object) (Convert.ToDouble(stateExt) + Convert.ToDouble(val));
          break;
        case TypeCode.Decimal:
          val = (object) (Convert.ToDecimal(stateExt) + Convert.ToDecimal(val));
          break;
        case TypeCode.DateTime:
          val = (object) (Convert.ToDateTime(stateExt) + new TimeSpan(0, 0, Convert.ToInt32(val), 0));
          break;
      }
    }
    object newValue = PXFormulaAttribute.ConvertValue(tc, val);
    object copy = cach.CreateCopy(obj3);
    if (!object.Equals(obj3, cach.Current))
    {
      cach.RaiseFieldUpdating(this._ParentFieldType.Name, copy, ref newValue);
      cach.SetValue(copy, this._ParentFieldType.Name, newValue);
      cach.Update(copy);
    }
    else
    {
      PXUIFieldAttribute.SetError(cach, obj3, this._ParentFieldType.Name, (string) null);
      cach.SetValueExt(obj3, this._ParentFieldType.Name, newValue);
      cach.MarkUpdated(obj3, true);
      if (this.CancelParentUpdate)
        return;
      cach.RaiseRowUpdated(obj3, copy);
    }
  }

  /// <exclude />
  public void CalcAggregate(PXCache sender, object[] records, object parent)
  {
    System.Type itemType = BqlCommand.GetItemType(this._ParentFieldType);
    PXCache cach = sender.Graph.Caches[itemType];
    PXFieldState stateExt = (PXFieldState) cach.GetStateExt(itemType.IsAssignableFrom(parent.GetType()) ? parent : (object) null, this._ParentFieldType.Name);
    object val = this.CalcAggregate(sender, (object) null, records, -1);
    TypeCode typeCode = System.Type.GetTypeCode(stateExt.DataType);
    cach.SetValue(parent, this._ParentFieldType.Name, PXFormulaAttribute.ConvertValue(typeCode, val));
    AggregateValidation.IgnoreDeltas(sender.Graph, this._ParentFieldType, parent);
  }

  private static object ConvertValue(TypeCode tc, object val)
  {
    switch (tc)
    {
      case TypeCode.Int16:
        return (object) Convert.ToInt16(val);
      case TypeCode.Int32:
        return (object) Convert.ToInt32(val);
      case TypeCode.Double:
        return (object) Convert.ToDouble(val);
      case TypeCode.Decimal:
        return (object) Convert.ToDecimal(val);
      case TypeCode.String:
        return (object) Convert.ToString(val);
      default:
        return val;
    }
  }

  protected virtual void InitDependencies(PXCache sender)
  {
    if (this._Formula == null)
      return;
    List<System.Type> collection = new List<System.Type>();
    bool readonlyCacheCreation = sender.Graph._ReadonlyCacheCreation;
    try
    {
      sender.Graph._ReadonlyCacheCreation = true;
      SQLExpression sqlExpression = SQLExpression.None();
      IBqlCreator formula = this._Formula;
      ref SQLExpression local = ref sqlExpression;
      PXGraph graph = sender.Graph;
      BqlCommandInfo info = new BqlCommandInfo(false);
      info.Fields = collection;
      info.BuildExpression = false;
      BqlCommand.Selection selection = new BqlCommand.Selection();
      formula.AppendExpression(ref local, graph, info, selection);
    }
    finally
    {
      sender.Graph._ReadonlyCacheCreation = readonlyCacheCreation;
    }
    this._Dependencies = new HashSet<System.Type>((IEnumerable<System.Type>) collection);
    foreach (System.Type dependency in this._Dependencies)
    {
      if (dependency.IsNested && (BqlCommand.GetItemType(dependency) == sender.GetItemType() || sender.GetItemType().IsSubclassOf(BqlCommand.GetItemType(dependency))) && !dependency.Name.Equals(this._FieldName, StringComparison.OrdinalIgnoreCase))
      {
        System.Type dependentFld = dependency;
        sender.FieldUpdatedEvents[dependency.Name.ToLower()] += (PXFieldUpdated) ((cache, e) => this.dependentFieldUpdated(cache, e, dependentFld));
      }
    }
    if (this._Formula is IBqlTrigger)
      return;
    sender.FieldDefaultingEvents[this._FieldName.ToLower()] += new PXFieldDefaulting(this.FormulaDefaulting);
  }

  /// <exclude />
  public override void CacheAttached(PXCache sender)
  {
    base.CacheAttached(sender);
    this._recursion = new PXEventSubscriberAttribute.ObjectRef<bool>();
    this.InitDependencies(sender);
    if (this._Formula is ISwitch formula)
      formula.OuterField = sender.GetBqlField(this._FieldName);
    if (this._Formula != null && !(this is PXUnboundFormulaAttribute))
    {
      if (!(this._Formula is IBqlTrigger) && !this.IsFieldBound(sender, this._FieldName))
      {
        if (!sender.Keys.Contains(this._FieldName))
          sender.RowSelecting += (PXRowSelecting) ((cache, e) => formulaRowSelecting(cache, e, false));
        else
          sender.RowSelectingWhileReading += (PXRowSelecting) ((cache, e) => formulaRowSelecting(cache, e, true));
      }
      if (this._Persistent)
        sender.Graph.RowPersisted.AddHandler(sender.GetItemType(), new PXRowPersisted(this.Graph_RowPersisted));
    }
    if (this.IsRestricted)
      this.Aggregate = (System.Type) null;
    if (!this.ValidateAggregateCalculation)
      return;
    this.InitValidations(sender);

    void formulaRowSelecting(PXCache cache, PXRowSelectingEventArgs e, bool openConnection)
    {
      if (this._recursion.Value && e.IsReadOnly || e.Record != null && !e.Record.IsFieldRestricted(cache, this._FieldName))
        return;
      using (openConnection ? new PXConnectionScope() : (PXConnectionScope) null)
        this.SetFormulaValue(cache, e.Row);
    }
  }

  protected virtual void InitValidations(PXCache sender)
  {
    if (!(this._ParentFieldType != (System.Type) null) || !(this._Aggregate is IBqlAggregateValidation aggregate) || !(sender.Graph.GetType() != typeof (PXGraph)) || sender.Graph.FindImplementation<ISuppressAggregateValidation>() != null || !this.IsFieldBound(sender.Graph.Caches[BqlCommand.GetItemType(this._ParentFieldType)], this._ParentFieldType.Name))
      return;
    aggregate.CreateValidation(sender, this.FormulaFieldName, this._ParentFieldType, this._Formula);
  }

  protected void SetFormulaValue(PXCache sender, object row)
  {
    PXFieldDefaultingEventArgs e = new PXFieldDefaultingEventArgs(row);
    this.FormulaDefaulting(sender, e);
    sender.SetValue(row, this._FieldName, e.NewValue);
  }

  /// <exclude />
  public virtual void Graph_RowPersisted(PXCache sender, PXRowPersistedEventArgs e)
  {
    if (e.TranStatus != PXTranStatus.Completed || (e.Operation & PXDBOperation.Delete) != PXDBOperation.Insert && (e.Operation & PXDBOperation.Delete) != PXDBOperation.Update)
      return;
    this.SetFormulaValue(sender, e.Row);
  }

  private bool IsFieldBound(PXCache cache, string fieldName)
  {
    return cache.GetAttributesReadonly(fieldName).With<List<PXEventSubscriberAttribute>, bool>((Func<List<PXEventSubscriberAttribute>, bool>) (atts => atts.Any<PXEventSubscriberAttribute>((Func<PXEventSubscriberAttribute, bool>) (a => a is PXDBFieldAttribute || a is PXDBCalcedAttribute)) && !atts.Any<PXEventSubscriberAttribute>((Func<PXEventSubscriberAttribute, bool>) (a => a is PXEntityAttribute pxEntityAttribute && !pxEntityAttribute.IsDBField))));
  }
}
