// Decompiled with JetBrains decompiler
// Type: PX.Data.PXAccumulatorAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Data.SQLTree;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Data;

/// <summary>Updates values of a data record in the database according to
/// specified policies. You can derive a custom attribute from this
/// attribute and override the <tt>PrepareInsert()</tt> method to set
/// other assignment behavior for target values (such as taking the
/// maximum instead of summarizing).</summary>
/// <remarks>
///   <para>You can use the attribute on its own or derive a custom attribute. Both a successor of <tt>PXAccumulator</tt> and the <tt>PXAccumulator</tt> attribute
/// itself should be placed on the definition of a DAC.</para>
///   <para>To define custom policy for fields of the specified DAC, you should derive a custom class from this attribute and override the <tt>PrepareInsert()</tt>
/// method. The method is called within the <tt>PersistInserted()</tt> method of the <tt>PXAccumulator</tt>. You can override the <tt>PersistInserted()</tt> method
/// as well.</para>With default settings, the attribute doesn't work with tables that contain an identity column. To use the attribute on these tables, you should set
/// to true the <tt>UpdateOnly</tt> property of the <tt>columns</tt> parameter in the <tt>PrepareInsert()</tt> method.
/// <para>The logic of the <tt>PXAccumulator</tt> attribute works on saving of the inserted data records to the database. This process is implemented in the
/// PersistInserted() method of the cache. This methods detects the <tt>PXAccumulator</tt>-derived attribute and calls the <tt>PersistInserted()</tt> method
/// defined in this attribute.</para><para>When you update a data record using the attribute, you typically initialize a new instance of the DAC, set the key fields to the key values of the data
/// record you need to update, and insert it into the cache. When a user saves changes on the webpage, or you save changes from code, your custom attribute
/// processes these inserted data records in its own way, updating database records instead of inserting new records and applying the policies you specify.</para><para>By deriving from this attribute, you can implement an attribute that will prevent certain fields from further updates once they are initialized with
/// values.</para></remarks>
/// <example><para>The code below shows how the attribute can be used directly. When a data record is saved, value of every field from the first array will be added to the previously saved value of the corresponding field from the second array. That is, FinYtdBalance values will be accumulated in the FinBegBalance value, TranYtdBalance values in the TranBegBalance value, and so on.</para>
///   <code title="Example" lang="CS">
/// [PXAccumulator(
///     new Type[] {
///         typeof(CuryAPHistory.finYtdBalance),
///         typeof(CuryAPHistory.tranYtdBalance),
///         typeof(CuryAPHistory.curyFinYtdBalance),
///         typeof(CuryAPHistory.curyTranYtdBalance)
///     },
///     new Type[] {
///         typeof(CuryAPHistory.finBegBalance),
///         typeof(CuryAPHistory.tranBegBalance),
///         typeof(CuryAPHistory.curyFinBegBalance),
///         typeof(CuryAPHistory.curyTranBegBalance)
///     }
/// )]
/// public partial class CuryAPHist : CuryAPHistory
/// { ... }</code>
///   <code title="Example2" description="In the next xample, the class derived from PXAccumulatorAttribute overrides the PrepareInsert() method and specifies the assignment behavior for several fields." lang="CS">
/// public class SupplierDataAccumulatorAttribute : PXAccumulatorAttribute
/// {
///     public SupplierDataAccumulatorAttribute()
///     {
///         base._SingleRecord = true;
///     }
/// 
///     protected override bool PrepareInsert(PXCache sender, object row,
///                                           PXAccumulatorCollection columns)
///     {
///         if (!base.PrepareInsert(sender, row, columns))
///             return false;
/// 
///         SupplierData bal = (SupplierData)row;
///         columns.Update&lt;SupplierData.supplierPrice&gt;(
///             bal.SupplierPrice, PXDataFieldAssign.AssignBehavior.Initialize);
///         columns.Update&lt;SupplierData.supplierUnit&gt;(
///             bal.SupplierUnit, PXDataFieldAssign.AssignBehavior.Initialize);
///         columns.Update&lt;SupplierData.conversionFactor&gt;(
///             bal.ConversionFactor, PXDataFieldAssign.AssignBehavior.Initialize);
///         columns.Update&lt;SupplierData.lastSupplierPrice&gt;(
///             bal.LastSupplierPrice, PXDataFieldAssign.AssignBehavior.Replace);
///         columns.Update&lt;SupplierData.lastPurchaseDate&gt;(
///             bal.LastPurchaseDate, PXDataFieldAssign.AssignBehavior.Replace);
/// 
///         return true;
///     }
/// }
/// ...
/// // Applying the custom attribute to a DAC
/// [System.SerializableAttribute()]
/// [SupplierDataAccumulator]
/// public class SupplierData : PXBqlTable, PX.Data.IBqlTable
/// { ... }</code>
/// </example>
[AttributeUsage(AttributeTargets.Class)]
public class PXAccumulatorAttribute : PXDBInterceptorAttribute
{
  /// <exclude />
  protected BqlCommand rowselection;
  /// <exclude />
  protected System.Type[] _Source;
  /// <exclude />
  protected System.Type[] _Destination;
  /// <exclude />
  protected bool _SingleRecord;
  /// <exclude />
  protected System.Type _BqlTable;

  /// <exclude />
  public override bool CacheSelected => false;

  /// <summary>The value that indicates (if set to <tt>true</tt>) that the attribute always updates only a single data record.</summary>
  public virtual bool SingleRecord
  {
    get => this._SingleRecord;
    set => this._SingleRecord = value;
  }

  /// <summary>The DAC the cache is associated with.</summary>
  public virtual System.Type BqlTable
  {
    get => this._BqlTable;
    set => this._BqlTable = value;
  }

  /// <summary>Empty default constructor.</summary>
  public PXAccumulatorAttribute()
  {
  }

  /// <summary>The constructor that initializes an instance of the attribute with the source fields and destination fields.</summary>
  /// <param name="source">Fields whose values are summarized in the
  /// corresponding destination fields.</param>
  /// <param name="destination">Fields that store sums of source fields from
  /// the data records inserted into the database previously to the current
  /// data record.</param>
  /// <remarks>For example, a source field may be the transaction amount and
  /// the destination field the current balance.</remarks>
  public PXAccumulatorAttribute(System.Type[] source, System.Type[] destination)
  {
    this._Source = source;
    this._Destination = destination;
  }

  /// <exclude />
  protected PXAccumulatorAttribute(
    params PXAccumulatorAttribute.RunningTotalRule[] pairs)
  {
    this._Source = new System.Type[pairs.Length];
    this._Destination = new System.Type[pairs.Length];
    for (int index = 0; index < pairs.Length; ++index)
    {
      this._Source[index] = pairs[index].Source;
      this._Destination[index] = pairs[index].Destination;
    }
  }

  /// <exclude />
  public override void CacheAttached(PXCache sender)
  {
    System.Type[] typeArray;
    if (sender.BqlKeys.Count == 0)
      typeArray = new System.Type[2]
      {
        typeof (Select<>),
        sender.GetItemType()
      };
    else if (sender.BqlKeys.Count == 1)
    {
      typeArray = new System.Type[7]
      {
        typeof (Select<,>),
        sender.GetItemType(),
        typeof (Where<,>),
        sender.BqlKeys[0],
        typeof (Equal<>),
        typeof (Required<>),
        sender.BqlKeys[0]
      };
    }
    else
    {
      typeArray = new System.Type[7 + (sender.BqlKeys.Count - 1) * 5];
      typeArray[0] = typeof (Select<,>);
      typeArray[1] = sender.GetItemType();
      typeArray[2] = typeof (Where<,,>);
      for (int index = 0; index < sender.BqlKeys.Count; ++index)
      {
        typeArray[3 + 5 * index] = sender.BqlKeys[index];
        typeArray[3 + 5 * index + 1] = typeof (Equal<>);
        typeArray[3 + 5 * index + 2] = typeof (Required<>);
        typeArray[3 + 5 * index + 3] = sender.BqlKeys[index];
        if (index < sender.BqlKeys.Count - 2)
          typeArray[3 + 5 * index + 4] = typeof (And<,,>);
        else if (index < sender.BqlKeys.Count - 1)
          typeArray[3 + 5 * index + 4] = typeof (And<,>);
      }
    }
    this.rowselection = BqlCommand.CreateInstance(typeArray);
    sender._PreventDeadlock = true;
  }

  /// <exclude />
  public override BqlCommand GetRowCommand() => this.rowselection;

  /// <exclude />
  public override BqlCommand GetTableCommand() => (BqlCommand) null;

  /// <summary>
  /// The method to override in a successor of the <tt>PXAccumulator</tt>
  /// attribute and set policies for fields.
  /// </summary>
  /// <param name="sender">The cache object into which the data record is inserted.</param>
  /// <param name="row">The data record to insert into the cache.</param>
  /// <param name="columns">The object representing columns.</param>
  /// <remarks>
  ///   <para>The method is invoked by the <tt>PersistInserted(...)</tt> method
  /// of the <tt>PXAccumulator</tt> attribute.</para>
  ///   <para>Typically, when you override this method, you call the base version
  /// of the method and set the policies for fields by calling the
  /// <tt>Update&lt;&gt;()</tt> method of the columns parameter.</para>
  /// </remarks>
  protected virtual bool PrepareInsert(PXCache sender, object row, PXAccumulatorCollection columns)
  {
    Func<string, PXEventSubscriberAttribute[]> func = Func.Memorize<string, PXEventSubscriberAttribute[]>((Func<string, PXEventSubscriberAttribute[]>) (fieldName => sender.GetAttributesReadonly(row, fieldName).ToArray<PXEventSubscriberAttribute>()));
    foreach (string field in (List<string>) sender.Fields)
    {
      columns.Add(field);
      object obj = sender.GetValue(row, field);
      if (obj != null)
      {
        int num1 = sender.Keys.IndexOf(field);
        if (num1 == sender.Keys.Count - 1)
        {
          columns.Restrict(field, PXComp.EQ, obj);
          if (!this._SingleRecord)
          {
            columns.RestrictPast(field, PXComp.LT, obj);
            columns.RestrictFuture(field, PXComp.GT, obj);
            columns.OrderBy(field, false);
          }
          else
            columns.RestrictPast(field, PXComp.EQ, obj);
          columns.InitializeWith(field, obj);
        }
        else if (num1 != -1)
        {
          columns.Restrict(field, PXComp.EQ, obj);
          columns.RestrictPast(field, PXComp.EQ, obj);
          if (!this._SingleRecord)
            columns.RestrictFuture(field, PXComp.EQ, obj);
          columns.InitializeWith(field, obj);
        }
        else
        {
          switch (obj)
          {
            case Decimal num2:
              columns.InitializeWith(field, obj);
              if (num2 != 0M)
              {
                columns.Update(field, obj, PXDataFieldAssign.AssignBehavior.Summarize);
                continue;
              }
              continue;
            case double num3:
              columns.InitializeWith(field, obj);
              if (num3 != 0.0)
              {
                columns.Update(field, obj, PXDataFieldAssign.AssignBehavior.Summarize);
                continue;
              }
              continue;
            case System.DateTime _ when ((IEnumerable<PXEventSubscriberAttribute>) func(field)).Any<PXEventSubscriberAttribute>((Func<PXEventSubscriberAttribute, bool>) (a => a is PXDBLastModifiedDateTimeAttribute || a is PXDBLastChangeDateTimeAttribute)):
            case string _ when ((IEnumerable<PXEventSubscriberAttribute>) func(field)).Any<PXEventSubscriberAttribute>((Func<PXEventSubscriberAttribute, bool>) (a => a is PXDBLastModifiedByScreenIDAttribute)):
            case Guid _ when ((IEnumerable<PXEventSubscriberAttribute>) func(field)).Any<PXEventSubscriberAttribute>((Func<PXEventSubscriberAttribute, bool>) (a => a is PXDBLastModifiedByIDAttribute)):
              columns.InitializeWith(field, obj);
              columns.Update(field, obj, PXDataFieldAssign.AssignBehavior.Replace);
              continue;
            default:
              columns.InitializeWith(field, obj);
              continue;
          }
        }
      }
    }
    if (!this._SingleRecord && this._Source != null && this._Destination != null)
    {
      for (int index = 0; index < this._Source.Length && index < this._Destination.Length; ++index)
      {
        columns.InitializeFrom(this._Destination[index], this._Source[index]);
        columns.UpdateFuture(this._Destination[index], sender.GetValue(row, this._Source[index].Name));
      }
    }
    return true;
  }

  /// <summary>
  /// The method that will be executed by the cache instead of the cache's
  /// <see cref="M:PX.Data.PXCache`1.PersistInserted(System.Object,System.Boolean)">PersistInserted()</see> method.
  /// If the attribute is attached to the cache, the cache will discover
  /// that a successor of the <tt>PXInterceptor</tt> attribute is attached,
  /// invoke the attribute's method from the standard method, and quit the
  /// standard method.
  /// </summary>
  /// <remarks>
  /// If you only need to set insertion policies for some DAC field, you should override only the
  /// <tt>PrepareInsert()</tt> method. Overriding the <tt>PersistInserted()</tt>
  /// method is needed to tweak the persist operation, such as to catch and
  /// process errors.
  /// </remarks>
  /// <param name="sender">The cache object into which the data record is inserted.</param>
  /// <param name="row">The inserted data record to be saved to the database.</param>
  public override bool PersistInserted(PXCache sender, object row)
  {
    PXAccumulatorCollection columns = new PXAccumulatorCollection(sender, row);
    if (!this.PrepareInsert(sender, row, columns))
      return false;
    List<PXAccumulatorItem> pxAccumulatorItemList = new List<PXAccumulatorItem>();
    int num1 = 0;
    int index1 = 0;
    foreach (PXAccumulatorItem pxAccumulatorItem in columns.Values)
    {
      if (pxAccumulatorItem.OrderBy.HasValue || pxAccumulatorItem.CurrentComparison.Length != 0 || pxAccumulatorItem.PastComparison.Length != 0 || pxAccumulatorItem.FutureComparison.Length != 0)
      {
        pxAccumulatorItemList.Add(pxAccumulatorItem);
      }
      else
      {
        KeyValuePair<string, object>? initializer = pxAccumulatorItem.Initializer;
        if (initializer.HasValue)
        {
          initializer = pxAccumulatorItem.Initializer;
          if (!string.IsNullOrEmpty(initializer.Value.Key))
          {
            pxAccumulatorItemList.Insert(index1, pxAccumulatorItem);
            ++num1;
            continue;
          }
        }
        pxAccumulatorItemList.Insert(0, pxAccumulatorItem);
        ++num1;
        ++index1;
      }
    }
    List<PXDataFieldAssign> pxDataFieldAssignList = new List<PXDataFieldAssign>();
    List<PXDataField> pxDataFieldList1 = new List<PXDataField>();
    List<PXDataFieldParam> first1 = new List<PXDataFieldParam>();
    List<PXDataFieldParam> first2 = new List<PXDataFieldParam>();
    List<PXDataField> collection1 = (List<PXDataField>) null;
    List<PXDataFieldParam> collection2 = (List<PXDataFieldParam>) null;
    List<KeyValuePair<int, KeyValuePair<int, int>>> keyValuePairList = (List<KeyValuePair<int, KeyValuePair<int, int>>>) null;
    bool flag1 = false;
    try
    {
      int index2 = -1;
      for (int index3 = pxAccumulatorItemList.Count - 1; index3 >= 0; --index3)
      {
        PXAccumulatorItem pxAccumulatorItem = pxAccumulatorItemList[index3];
        KeyValuePair<string, object>? initializer;
        KeyValuePair<string, object> keyValuePair1;
        if (index3 >= num1)
        {
          if (!columns.UpdateOnly)
          {
            PXCommandPreparingEventArgs.FieldDescription fieldDescription = (PXCommandPreparingEventArgs.FieldDescription) null;
            PXCache pxCache = sender;
            string field = pxAccumulatorItem.Field;
            object row1 = row;
            initializer = pxAccumulatorItem.Initializer;
            object obj;
            if (!initializer.HasValue)
            {
              obj = (object) null;
            }
            else
            {
              initializer = pxAccumulatorItem.Initializer;
              keyValuePair1 = initializer.Value;
              obj = keyValuePair1.Value;
            }
            ref PXCommandPreparingEventArgs.FieldDescription local = ref fieldDescription;
            pxCache.RaiseCommandPreparing(field, row1, obj, PXDBOperation.Insert, (System.Type) null, out local);
            if (fieldDescription?.Expr != null && !fieldDescription.IsExcludedFromUpdate)
              pxDataFieldAssignList.Add(new PXDataFieldAssign((fieldDescription.Expr as Column).Name, fieldDescription.DataType, fieldDescription.DataLength, fieldDescription.DataValue));
            bool flag2 = false;
            foreach (KeyValuePair<PXComp, object> keyValuePair2 in pxAccumulatorItem.PastComparison)
            {
              PXCommandPreparingEventArgs.FieldDescription description = (PXCommandPreparingEventArgs.FieldDescription) null;
              sender.RaiseCommandPreparing(pxAccumulatorItem.Field, row, keyValuePair2.Value, PXDBOperation.Select, (System.Type) null, out description);
              if (description != null && description.Expr != null)
              {
                string name = (description.Expr as Column).Name;
                pxDataFieldList1.Add((PXDataField) new PXDataFieldValue(name, description.DataType, description.DataLength, description.DataValue, keyValuePair2.Key));
                if (pxAccumulatorItem.OrderBy.HasValue && !flag2)
                {
                  pxDataFieldList1.Add((PXDataField) new PXDataFieldOrder(name, !pxAccumulatorItem.OrderBy.Value));
                  flag2 = true;
                }
              }
            }
          }
          foreach (KeyValuePair<PXComp, object> keyValuePair3 in pxAccumulatorItem.CurrentComparison)
          {
            PXCommandPreparingEventArgs.FieldDescription description = (PXCommandPreparingEventArgs.FieldDescription) null;
            sender.RaiseCommandPreparing(pxAccumulatorItem.Field, row, keyValuePair3.Value, PXDBOperation.Update, (System.Type) null, out description);
            if (description?.Expr != null && !description.IsExcludedFromUpdate)
              first1.Add((PXDataFieldParam) new PXDataFieldRestrict((description.Expr as Column).Name, description.DataType, description.DataLength, description.DataValue, keyValuePair3.Key));
          }
          foreach (KeyValuePair<PXComp, object> keyValuePair4 in pxAccumulatorItem.FutureComparison)
          {
            PXCommandPreparingEventArgs.FieldDescription description = (PXCommandPreparingEventArgs.FieldDescription) null;
            sender.RaiseCommandPreparing(pxAccumulatorItem.Field, row, keyValuePair4.Value, PXDBOperation.Update, (System.Type) null, out description);
            if (description?.Expr != null && !description.IsExcludedFromUpdate)
              first2.Add((PXDataFieldParam) new PXDataFieldRestrict((description.Expr as Column).Name, description.DataType, description.DataLength, description.DataValue, keyValuePair4.Key));
          }
        }
        else if (!columns.UpdateOnly)
        {
          initializer = pxAccumulatorItem.Initializer;
          if (initializer.HasValue)
          {
            if (index2 == -1)
              index2 = pxDataFieldAssignList.Count;
            PXCommandPreparingEventArgs.FieldDescription description = (PXCommandPreparingEventArgs.FieldDescription) null;
            PXCache pxCache1 = sender;
            string field = pxAccumulatorItem.Field;
            object row2 = row;
            initializer = pxAccumulatorItem.Initializer;
            keyValuePair1 = initializer.Value;
            object obj1 = keyValuePair1.Value;
            ref PXCommandPreparingEventArgs.FieldDescription local1 = ref description;
            pxCache1.RaiseCommandPreparing(field, row2, obj1, PXDBOperation.Insert, (System.Type) null, out local1);
            if (description?.Expr != null && !description.IsExcludedFromUpdate && this.tableMeet(description, sender.BqlTable, sender.Graph.SqlDialect) && !description.IsRestriction)
            {
              pxDataFieldAssignList.Insert(index2, new PXDataFieldAssign((description.Expr as Column).Name, description.DataType, description.DataLength, description.DataValue));
              if (index3 >= index1)
              {
                description = (PXCommandPreparingEventArgs.FieldDescription) null;
                PXCache pxCache2 = sender;
                initializer = pxAccumulatorItem.Initializer;
                keyValuePair1 = initializer.Value;
                string key = keyValuePair1.Key;
                object row3 = row;
                initializer = pxAccumulatorItem.Initializer;
                keyValuePair1 = initializer.Value;
                object obj2 = keyValuePair1.Value;
                ref PXCommandPreparingEventArgs.FieldDescription local2 = ref description;
                pxCache2.RaiseCommandPreparing(key, row3, obj2, PXDBOperation.Insert, (System.Type) null, out local2);
                if (description?.Expr != null && !description.IsExcludedFromUpdate)
                {
                  pxDataFieldList1.Insert(0, new PXDataField(description.Expr));
                  if (pxAccumulatorItem.CurrentUpdate != null || pxAccumulatorItem.CurrentUpdateBehavior == PXDataFieldAssign.AssignBehavior.Nullout)
                    pxDataFieldAssignList[index2].Behavior = pxAccumulatorItem.CurrentUpdateBehavior;
                }
              }
            }
          }
        }
        if (!columns.InsertOnly && (pxAccumulatorItem.CurrentUpdate != null || pxAccumulatorItem.CurrentUpdateBehavior == PXDataFieldAssign.AssignBehavior.Nullout))
        {
          PXCommandPreparingEventArgs.FieldDescription description = (PXCommandPreparingEventArgs.FieldDescription) null;
          sender.RaiseCommandPreparing(pxAccumulatorItem.Field, row, pxAccumulatorItem.CurrentUpdate, PXDBOperation.Update, (System.Type) null, out description);
          if (description?.Expr != null && !description.IsExcludedFromUpdate && !description.IsRestriction)
            first1.Add((PXDataFieldParam) new PXDataFieldAssign((description.Expr as Column).Name, description.DataType, description.DataLength, description.DataValue)
            {
              Behavior = pxAccumulatorItem.CurrentUpdateBehavior
            });
        }
        if (pxAccumulatorItem.FutureUpdate != null)
        {
          PXCommandPreparingEventArgs.FieldDescription description = (PXCommandPreparingEventArgs.FieldDescription) null;
          sender.RaiseCommandPreparing(pxAccumulatorItem.Field, row, pxAccumulatorItem.FutureUpdate, PXDBOperation.Update, (System.Type) null, out description);
          if (description?.Expr != null && !description.IsExcludedFromUpdate && !description.IsRestriction)
          {
            first2.Add((PXDataFieldParam) new PXDataFieldAssign((description.Expr as Column).Name, description.DataType, description.DataLength, description.DataValue)
            {
              Behavior = pxAccumulatorItem.FutureUpdateBehavior
            });
            flag1 = true;
          }
        }
      }
      if (columns.Exceptions != null)
      {
        if (columns.Exceptions.Count > 0)
        {
          collection2 = new List<PXDataFieldParam>();
          collection1 = new List<PXDataField>();
          keyValuePairList = new List<KeyValuePair<int, KeyValuePair<int, int>>>();
          int key = 0;
          for (int index4 = 0; index4 < columns.Exceptions.Count; ++index4)
          {
            int num2 = key;
            for (int index5 = 0; index5 < columns.Exceptions[index4].Value.Length; ++index5)
            {
              PXCommandPreparingEventArgs.FieldDescription description = (PXCommandPreparingEventArgs.FieldDescription) null;
              sender.RaiseCommandPreparing(columns.Exceptions[index4].Value[index5].FieldName, row, columns.Exceptions[index4].Value[index5].Value, PXDBOperation.Update, (System.Type) null, out description);
              if (description?.Expr != null && !description.IsExcludedFromUpdate)
              {
                Column expr = (Column) description.Expr;
                PXDataFieldRestrict dataFieldRestrict = new PXDataFieldRestrict(expr.Name, description.DataType, description.DataLength, description.DataValue, columns.Exceptions[index4].Value[index5].Comp);
                if (index5 == 0)
                  ++dataFieldRestrict.OpenBrackets;
                else
                  dataFieldRestrict.OrOperator = true;
                if (index5 == columns.Exceptions[index4].Value.Length - 1)
                  ++dataFieldRestrict.CloseBrackets;
                dataFieldRestrict.CheckResultOnly = true;
                collection2.Add((PXDataFieldParam) dataFieldRestrict);
                collection1.Add((PXDataField) new PXDataFieldValue(expr.Name, dataFieldRestrict.ValueType, dataFieldRestrict.ValueLength, dataFieldRestrict.Value, dataFieldRestrict.Comp)
                {
                  OpenBrackets = dataFieldRestrict.OpenBrackets,
                  CloseBrackets = dataFieldRestrict.CloseBrackets,
                  OrOperator = dataFieldRestrict.OrOperator,
                  CheckResultOnly = true
                });
                ++num2;
              }
            }
            if (key != num2)
              keyValuePairList.Add(new KeyValuePair<int, KeyValuePair<int, int>>(index4, new KeyValuePair<int, int>(key, num2 - 1)));
            key = num2;
          }
          if (collection2.Count == 0)
            collection2 = (List<PXDataFieldParam>) null;
        }
      }
    }
    catch (PXCommandPreparingException ex)
    {
      if (!sender.RaiseExceptionHandling(ex.Name, row, ex.Value, (Exception) ex))
        return false;
      throw;
    }
    PXDBOperation operation = PXDBOperation.Insert;
    try
    {
      int count1 = pxDataFieldList1.Count;
      int count2 = first1.Count;
      if (collection2 != null)
        pxDataFieldList1.AddRange((IEnumerable<PXDataField>) collection1);
      if (count1 != 0)
      {
        PXGraph graph = sender.Graph;
        System.Type bqlTable = this._BqlTable;
        if ((object) bqlTable == null)
          bqlTable = sender.BqlTable;
        PXDataFieldAssign[] array1 = pxDataFieldAssignList.ToArray();
        PXDataField[] array2 = pxDataFieldList1.ToArray();
        if (graph.ProviderEnsure(bqlTable, array1, array2))
          goto label_121;
      }
      if (first1.Count > 0)
      {
        operation = PXDBOperation.Update;
        if (collection2 != null)
          first1.AddRange((IEnumerable<PXDataFieldParam>) collection2);
        PXGraph graph1 = sender.Graph;
        System.Type bqlTable1 = this._BqlTable;
        if ((object) bqlTable1 == null)
          bqlTable1 = sender.BqlTable;
        PXDataFieldParam[] array3 = first1.Concat<PXDataFieldParam>((IEnumerable<PXDataFieldParam>) new PXDataFieldRestrict[1]
        {
          PXSelectOriginalsRestrict.SelectAllOriginalValues
        }).ToArray<PXDataFieldParam>();
        if (!graph1.ProviderUpdate(bqlTable1, array3))
        {
          if (collection2 != null)
          {
            for (int index6 = 1; index6 <= keyValuePairList.Count; ++index6)
            {
              for (int index7 = 0; (double) index7 < System.Math.Pow(2.0, (double) keyValuePairList.Count) && index7 < 2147483646; ++index7)
              {
                int num3 = index7;
                int num4 = 0;
                for (int index8 = 0; index8 < keyValuePairList.Count; ++index8)
                {
                  if ((num3 & 1) == 0)
                    ++num4;
                  num3 >>= 1;
                }
                if (num4 == index6)
                {
                  pxDataFieldList1 = pxDataFieldList1.GetRange(0, count1);
                  first1 = first1.GetRange(0, count2);
                  int index9 = -1;
                  int num5 = index7;
                  KeyValuePair<int, KeyValuePair<int, int>> keyValuePair5;
                  for (int index10 = 0; index10 < keyValuePairList.Count; ++index10)
                  {
                    if ((num5 & 1) == 1)
                    {
                      List<PXDataField> pxDataFieldList2 = pxDataFieldList1;
                      List<PXDataField> pxDataFieldList3 = collection1;
                      keyValuePair5 = keyValuePairList[index10];
                      KeyValuePair<int, int> keyValuePair6 = keyValuePair5.Value;
                      int key1 = keyValuePair6.Key;
                      keyValuePair5 = keyValuePairList[index10];
                      keyValuePair6 = keyValuePair5.Value;
                      int num6 = keyValuePair6.Value;
                      keyValuePair5 = keyValuePairList[index10];
                      keyValuePair6 = keyValuePair5.Value;
                      int key2 = keyValuePair6.Key;
                      int count3 = num6 - key2 + 1;
                      List<PXDataField> range1 = pxDataFieldList3.GetRange(key1, count3);
                      pxDataFieldList2.AddRange((IEnumerable<PXDataField>) range1);
                      List<PXDataFieldParam> pxDataFieldParamList1 = first1;
                      List<PXDataFieldParam> pxDataFieldParamList2 = collection2;
                      keyValuePair5 = keyValuePairList[index10];
                      keyValuePair6 = keyValuePair5.Value;
                      int key3 = keyValuePair6.Key;
                      keyValuePair5 = keyValuePairList[index10];
                      keyValuePair6 = keyValuePair5.Value;
                      int num7 = keyValuePair6.Value;
                      keyValuePair5 = keyValuePairList[index10];
                      keyValuePair6 = keyValuePair5.Value;
                      int key4 = keyValuePair6.Key;
                      int count4 = num7 - key4 + 1;
                      List<PXDataFieldParam> range2 = pxDataFieldParamList2.GetRange(key3, count4);
                      pxDataFieldParamList1.AddRange((IEnumerable<PXDataFieldParam>) range2);
                    }
                    else if (index9 == -1)
                      index9 = index10;
                    num5 >>= 1;
                  }
                  if (count1 != 0)
                  {
                    PXGraph graph2 = sender.Graph;
                    System.Type bqlTable2 = this._BqlTable;
                    if ((object) bqlTable2 == null)
                      bqlTable2 = sender.BqlTable;
                    PXDataFieldAssign[] array4 = pxDataFieldAssignList.ToArray();
                    PXDataField[] array5 = pxDataFieldList1.ToArray();
                    if (graph2.ProviderEnsure(bqlTable2, array4, array5))
                      goto label_113;
                  }
                  if (first1.Count > 0)
                  {
                    PXGraph graph3 = sender.Graph;
                    System.Type bqlTable3 = this._BqlTable;
                    if ((object) bqlTable3 == null)
                      bqlTable3 = sender.BqlTable;
                    PXDataFieldParam[] array6 = first1.Concat<PXDataFieldParam>((IEnumerable<PXDataFieldParam>) new PXDataFieldRestrict[1]
                    {
                      PXSelectOriginalsRestrict.SelectAllOriginalValues
                    }).ToArray<PXDataFieldParam>();
                    if (!graph3.ProviderUpdate(bqlTable3, array6))
                      continue;
                  }
                  else
                    continue;
label_113:
                  List<KeyValuePair<string, PXAccumulatorRestriction[]>> exceptions = columns.Exceptions;
                  keyValuePair5 = keyValuePairList[index9];
                  int key5 = keyValuePair5.Key;
                  string key6 = exceptions[key5].Key;
                  object[] keys = PXDBInterceptorAttribute.getKeys(sender, row);
                  keyValuePair5 = keyValuePairList[index9];
                  int key7 = keyValuePair5.Key;
                  throw new PXRestrictionViolationException(key6, keys, key7);
                }
              }
            }
          }
          System.Type bqlTable4 = this._BqlTable;
          if ((object) bqlTable4 == null)
            bqlTable4 = sender.BqlTable;
          throw PXDBInterceptorAttribute.GetLockViolationException(bqlTable4, first1.ToArray(), PXDBOperation.Update);
        }
      }
label_121:
      sender.RaiseRowPersisted(row, operation, PXTranStatus.Open, (Exception) null);
      if (flag1)
      {
        PXGraph graph = sender.Graph;
        System.Type bqlTable = this._BqlTable;
        if ((object) bqlTable == null)
          bqlTable = sender.BqlTable;
        PXDataFieldParam[] array = first2.Concat<PXDataFieldParam>((IEnumerable<PXDataFieldParam>) new PXDataFieldRestrict[1]
        {
          PXSelectOriginalsRestrict.SelectAllOriginalValues
        }).ToArray<PXDataFieldParam>();
        graph.ProviderUpdate(bqlTable, array);
      }
    }
    catch (PXDatabaseException ex)
    {
      ex.Keys = PXDBInterceptorAttribute.getKeys(sender, row);
      if (ex.ErrorCode == PXDbExceptions.Timeout)
        ex.Retry = true;
      throw;
    }
    return true;
  }

  /// <exclude />
  public override bool PersistUpdated(PXCache sender, object row)
  {
    List<PXDataFieldParam> first = new List<PXDataFieldParam>();
    try
    {
      foreach (string field in (List<string>) sender.Fields)
      {
        PXCommandPreparingEventArgs.FieldDescription description = (PXCommandPreparingEventArgs.FieldDescription) null;
        sender.RaiseCommandPreparing(field, row, sender.GetValue(row, field), PXDBOperation.Update, (System.Type) null, out description);
        if (description?.Expr != null && !description.IsExcludedFromUpdate)
        {
          if (description.IsRestriction)
            first.Add((PXDataFieldParam) new PXDataFieldRestrict((Column) description.Expr, description.DataType, description.DataLength, description.DataValue));
          else
            first.Add((PXDataFieldParam) new PXDataFieldAssign((Column) description.Expr, description.DataType, description.DataLength, description.DataValue));
        }
      }
    }
    catch (PXCommandPreparingException ex)
    {
      if (!sender.RaiseExceptionHandling(ex.Name, row, ex.Value, (Exception) ex))
        return false;
      throw;
    }
    try
    {
      if (!sender.Graph.ProviderUpdate(sender.BqlTable, first.Concat<PXDataFieldParam>((IEnumerable<PXDataFieldParam>) new PXDataFieldRestrict[1]
      {
        PXSelectOriginalsRestrict.SelectAllOriginalValues
      }).ToArray<PXDataFieldParam>()))
        throw PXDBInterceptorAttribute.GetLockViolationException(sender.BqlTable, first.ToArray(), PXDBOperation.Update);
    }
    catch (PXDatabaseException ex)
    {
      object[] keys = PXDBInterceptorAttribute.getKeys(sender, row);
      ex.Keys = keys;
      throw;
    }
    return true;
  }

  /// <exclude />
  public override bool PersistDeleted(PXCache sender, object row)
  {
    List<PXDataFieldRestrict> first = new List<PXDataFieldRestrict>();
    try
    {
      foreach (string field in (List<string>) sender.Fields)
      {
        PXCommandPreparingEventArgs.FieldDescription description = (PXCommandPreparingEventArgs.FieldDescription) null;
        sender.RaiseCommandPreparing(field, row, sender.GetValue(row, field), PXDBOperation.Delete, (System.Type) null, out description);
        if (description != null && description.Expr != null && description.IsRestriction)
          first.Add(new PXDataFieldRestrict((Column) description.Expr, description.DataType, description.DataLength, description.DataValue));
      }
    }
    catch (PXCommandPreparingException ex)
    {
      if (!sender.RaiseExceptionHandling(ex.Name, row, ex.Value, (Exception) ex))
        return false;
      throw;
    }
    try
    {
      if (!sender.Graph.ProviderDelete(sender.BqlTable, first.Concat<PXDataFieldRestrict>((IEnumerable<PXDataFieldRestrict>) new PXDataFieldRestrict[1]
      {
        PXSelectOriginalsRestrict.SelectAllOriginalValues
      }).ToArray<PXDataFieldRestrict>()))
        throw PXDBInterceptorAttribute.GetLockViolationException(sender.BqlTable, (PXDataFieldParam[]) first.ToArray(), PXDBOperation.Delete);
      sender.RaiseRowPersisted(row, PXDBOperation.Delete, PXTranStatus.Open, (Exception) null);
    }
    catch (PXDatabaseException ex)
    {
      object[] keys = PXDBInterceptorAttribute.getKeys(sender, row);
      ex.Keys = keys;
      throw;
    }
    return true;
  }

  /// <exclude />
  public override object Insert(PXCache sender, object row)
  {
    object obj = sender.Locate(row);
    if (obj == null)
      return base.Insert(sender, row);
    if (sender.GetStatus(obj) == PXEntryStatus.Inserted)
    {
      sender.Current = obj;
      return obj;
    }
    sender.Remove(obj);
    return base.Insert(sender, row);
  }

  [PXInternalUseOnly]
  public override PersistOrder PersistOrder { get; set; } = PersistOrder.AtTheEndOfTransaction;

  /// <summary>
  /// Begins a definition of a running total fields association by specifying of a destination field.
  /// </summary>
  /// <typeparam name="TDestinationField">The field that will store a running total value.</typeparam>
  protected static PXAccumulatorAttribute.RunningTotalPairer Run<TDestinationField>() where TDestinationField : IBqlField
  {
    return new PXAccumulatorAttribute.RunningTotalPairer(typeof (TDestinationField));
  }

  /// <exclude />
  protected readonly struct RunningTotalPairer
  {
    private readonly System.Type _destination;

    internal RunningTotalPairer(System.Type destination) => this._destination = destination;

    /// <summary>
    /// Pairs the specified destination field with a source of its running total value.
    /// </summary>
    /// <typeparam name="TSourceField">The field that will be used as a source of a running total value.</typeparam>
    public PXAccumulatorAttribute.RunningTotalRule WithValueOf<TSourceField>() where TSourceField : IBqlField
    {
      return new PXAccumulatorAttribute.RunningTotalRule(this._destination, typeof (TSourceField));
    }

    /// <summary>
    /// Pairs the specified destination field with its own value.
    /// </summary>
    public PXAccumulatorAttribute.RunningTotalRule WithOwnValue()
    {
      return new PXAccumulatorAttribute.RunningTotalRule(this._destination, this._destination);
    }
  }

  /// <summary>
  /// Represents a rule of running total accumulation. Should be created via <see cref="M:PX.Data.PXAccumulatorAttribute.Run``1" /> call.
  /// </summary>
  protected readonly struct RunningTotalRule
  {
    internal System.Type Destination { get; }

    internal System.Type Source { get; }

    internal RunningTotalRule(System.Type destination, System.Type source)
    {
      System.Type type1 = destination;
      System.Type type2 = source;
      this.Destination = type1;
      this.Source = type2;
    }
  }
}
