// Decompiled with JetBrains decompiler
// Type: PX.Data.PXParentAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Data.BQL;
using PX.Data.BQL.AggregateCalculators;
using PX.Data.ReferentialIntegrity;
using PX.Data.ReferentialIntegrity.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Data;

/// <summary>Creates a reference to the parent record, establishing a parent-child relationship between two tables.</summary>
/// <remarks>
/// 	<para>You can place the attribute on any field of the child DAC. The
/// primary goal of the attribute is to perform cascade deletion of the
/// child data records once a parent data record is deleted.</para>
/// 	<para>The parent data record is defined by a BQL query of
/// <tt>Select&lt;&gt;</tt> type. Typically, the query includes a
/// <tt>Where</tt> clause that adds conditions for the parent's key fields
/// to equal child's key fields. In this case, the values of child data
/// record key fields are specified using the <tt>Current</tt> parameter.
/// The business logic controller that provides the interface for working
/// with these parent and child data records should define a view
/// selecting parent data records and a view selecting child data records.
/// These views will againg be connected using the <tt>Current</tt>
/// parameter.</para>
/// 	<para>You can use the static methods to retrieve a particular parent
/// data record or child data records, or get and set some attribute
/// parameters.</para>
/// 	<para>Once the <tt>PXParent</tt> attribute is added to some DAC field,
/// you can use the <see cref="T:PX.Data.PXFormulaAttribute">PXFormula</see>
/// attribute to define set calculations for parent data record fields
/// from child data record fields.</para>
/// </remarks>
/// <example>
/// <code title="Example" lang="CS">
/// // The attribute below specifies a query for selecting the parent Document data record
/// // for a given child DocTransaction data record.
/// [PXParent(typeof(
///     Select&lt;Document,
///         Where&lt;Document.docNbr, Equal&lt;Current&lt;DocTransaction.docNbr&gt;&gt;,
///             And&lt;Document.docType, Equal&lt;Current&lt;DocTransaction.docType&gt;&gt;&gt;&gt;&gt;))]
/// public virtual string DocNbr { get; set; }</code>
/// 	<code title="Example2" groupname="Example" lang="CS">
/// [PXParent(typeof(
///     Select&lt;ARTran,
///         Where&lt;ARTran.tranType, Equal&lt;Current&lt;ARFinChargeTran.tranType&gt;&gt;,
///             And&lt;ARTran.refNbr, Equal&lt;Current&lt;ARFinChargeTran.refNbr&gt;&gt;,
///             And&lt;ARTran.lineNbr, Equal&lt;Current&lt;ARFinChargeTran.lineNbr&gt;&gt;&gt;&gt;&gt;&gt;))]
/// public virtual short? LineNbr { get; set; }</code>
/// 	<code title="Example3" groupname="Example2" lang="CS">
/// // The following code obtains the parent data record at run time.
/// CR.Location child = (CR.Location)e.Row;
/// BAccount parent =
///     (BAccount)PXParentAttribute.SelectParent(sender, child, typeof(BAccount));</code>
/// 	<code title="Example4" groupname="Example3" lang="CS">
/// // The following example sets the parent data record at run time.
/// // Views definitions in a graph
/// public PXSelect&lt;INRegister&gt; inregister;
/// public PXSelect&lt;INTran&gt; intranselect;
/// ...
/// // Code executed in some graph method
/// INTran tran = (INTran)res;
/// PXParentAttribute.SetParent(
///     intranselect.Cache, tran, typeof(INRegister), inregister.Current);</code>
/// </example>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Property, AllowMultiple = true)]
public class PXParentAttribute : 
  PXForeignReferenceAttribute,
  IPXRowInsertedSubscriber,
  IPXRowPersistedSubscriber
{
  protected System.Type _ChildType;
  protected System.Type _ParentType;
  private BqlCommand _selectParentWithoutAdditionalCondition;
  protected BqlCommand _SelectParent;
  protected BqlCommand _SelectChildren;
  protected bool _UseCurrent;
  protected bool _LeaveChildren;
  protected bool _ParentCreate;
  protected PXParentAttribute.ParentCollection _Currents;
  protected PXParentAttribute.ParentCollection _Pendings;
  protected static Dictionary<System.Type, BqlCommand> _selects = new Dictionary<System.Type, BqlCommand>();
  protected static object _slock = new object();

  /// <summary>Gets or sets the value that permits or forbids creation of
  /// the parent through the <see cref="M:PX.Data.PXParentAttribute.CreateParent(PX.Data.PXCache,System.Object,System.Type)">CreateParent(PXCache, object,
  /// Type)</see> method. In particular, the <tt>PXFormula</tt> attribute
  /// tries to create a parent data record if it doesn't exist, by invoking
  /// this method. By default, the property equals <see langword="false" />.</summary>
  public virtual bool ParentCreate
  {
    get => this._ParentCreate;
    set => this._ParentCreate = value;
  }

  /// <summary>Gets or sets the value that indicates whether the child data
  /// records are left or deleted on parent data record deletion. By
  /// default, the property equals <see langword="false" />, which means that child
  /// data records are deleted.</summary>
  public virtual bool LeaveChildren
  {
    get => this._LeaveChildren;
    set => this._LeaveChildren = value;
  }

  /// <summary>Gets the DAC type of the parent data record. The type is
  /// determined in the constructor as the first table referenced in the
  /// <tt>Select</tt> query.</summary>
  public virtual System.Type ParentType => this._ParentType;

  private protected override bool ForceNoAction => this.LeaveChildren;

  /// <summary>Returns the value of the <tt>ParentCreate</tt> property from
  /// the attribute instance that references the provided parent type or a
  /// type derived from it.</summary>
  /// <param name="cache">The cache object to search for the attributes of
  /// <tt>PXParent</tt> type.</param>
  /// <param name="ParentType">The DAC type of the parent data
  /// record.</param>
  public static bool GetParentCreate(PXCache cache, System.Type ParentType)
  {
    return PXParentAttribute.GetParentCreate(cache, (object) null, ParentType);
  }

  protected internal static bool GetParentCreate(PXCache cache, object row, System.Type ParentType)
  {
    List<PXParentAttribute> pxParentAttributeList = new List<PXParentAttribute>();
    foreach (PXParentAttribute pxParentAttribute in cache.GetAttributesReadonly(row, (string) null).OfType<PXParentAttribute>())
    {
      if (pxParentAttribute._ParentType == ParentType)
      {
        pxParentAttributeList.Insert(0, pxParentAttribute);
        break;
      }
      if (ParentType.IsSubclassOf(pxParentAttribute._ParentType))
        pxParentAttributeList.Add(pxParentAttribute);
    }
    return pxParentAttributeList.Count > 0 && pxParentAttributeList[0]._ParentCreate;
  }

  protected internal static void AlterParentCreate(PXCache cache1, object row)
  {
    foreach (string name in Func.Memorize<PXCache, IEnumerable<string>>((Func<PXCache, IEnumerable<string>>) (cache2 => cache2.GetAttributesReadonly((string) null).OfType<PXParentAttribute>().Where<PXParentAttribute>((Func<PXParentAttribute, bool>) (_ => _._ParentCreate)).Select<PXParentAttribute, string>((Func<PXParentAttribute, string>) (_ => _._FieldName))))(cache1))
    {
      foreach (PXParentAttribute pxParentAttribute in cache1.GetAttributes(row, name).OfType<PXParentAttribute>())
        pxParentAttribute._ParentCreate = false;
    }
  }

  /// <summary>Creates the parent for the provided child data record for the
  /// attribute instance that references the provided parent type or a type
  /// derived from it. Does nothing if <tt>ParentCreate</tt> equals
  /// <see langword="false" /> in this attribute instance. If the parent is created,
  /// it is inserted into the cache object.</summary>
  /// <param name="cache">The cache object to search for the attributes of
  /// <tt>PXParent</tt> type.</param>
  /// <param name="row">The child data record for which the parent is
  /// created.</param>
  /// <param name="ParentType">The DAC type of the parent data
  /// record.</param>
  public static object CreateParent(PXCache cache, object row, System.Type ParentType)
  {
    object parentInstance = PXParentAttribute.CreateParentInstance(cache, row, ParentType, false);
    return parentInstance != null ? cache.Graph.Caches[ParentType].Insert(parentInstance) : (object) null;
  }

  /// <summary>Locates the parent for the provided child data record for the
  /// attribute instance that references the provided parent type or a type
  /// derived from it.</summary>
  /// <param name="cache">The cache object to search for the attributes of
  /// <tt>PXParent</tt> type.</param>
  /// <param name="row">The child data record for which the parent is
  /// created.</param>
  /// <param name="ParentType">The DAC type of the parent data
  /// record.</param>
  public static object LocateParent(PXCache cache, object row, System.Type ParentType)
  {
    object parentInstance = PXParentAttribute.CreateParentInstance(cache, row, ParentType, true);
    return parentInstance != null ? cache.Graph.Caches[ParentType].Locate(parentInstance) : (object) null;
  }

  internal static object CreateParentInstance(
    PXCache cache,
    object row,
    System.Type ParentType,
    bool forceCreate)
  {
    List<PXEventSubscriberAttribute> subscriberAttributeList = new List<PXEventSubscriberAttribute>();
    foreach (PXParentAttribute pxParentAttribute in cache.GetAttributesReadonly((string) null).OfType<PXParentAttribute>())
    {
      if (pxParentAttribute._ParentType == ParentType)
      {
        subscriberAttributeList.Insert(0, (PXEventSubscriberAttribute) pxParentAttribute);
        break;
      }
      if (ParentType.IsSubclassOf(pxParentAttribute._ParentType))
        subscriberAttributeList.Add((PXEventSubscriberAttribute) pxParentAttribute);
    }
    return subscriberAttributeList.Count > 0 && ((PXParentAttribute) subscriberAttributeList[0])._ParentCreate | forceCreate ? PXParentAttribute.CreateParentInstance(cache, row, (PXParentAttribute) subscriberAttributeList[0]) : (object) null;
  }

  private static object CreateParentInstance(PXCache cache, object row, PXParentAttribute attr)
  {
    PXView parentSelect = attr.GetParentSelect(cache);
    BqlCommand bqlSelect = parentSelect.BqlSelect;
    PXCache cache1 = parentSelect.Cache;
    object instance = cache1.CreateInstance();
    foreach (KeyValuePair<System.Type, System.Type> parameterPair in bqlSelect.GetParameterPairs())
    {
      object obj;
      if ((obj = cache.GetValue(row, parameterPair.Value.Name)) == null)
        return (object) null;
      cache1.SetValue(instance, parameterPair.Key.Name, obj);
    }
    foreach (string key in (IEnumerable<string>) cache1.Keys)
    {
      if (cache1.GetValue(instance, key) == null)
      {
        object newValue;
        if (cache1.RaiseFieldDefaulting(key, instance, out newValue))
          cache1.RaiseFieldUpdating(key, instance, ref newValue);
        cache1.SetValue(instance, key, newValue);
      }
    }
    object[] objArray = parentSelect.PrepareParameters(new object[1]
    {
      row
    }, new object[0]);
    return bqlSelect.Meet(cache1, instance, objArray) ? instance : (object) null;
  }

  /// <exclude />
  internal static object FindParent(PXCache cache, object row, System.Type ParentType)
  {
    List<PXEventSubscriberAttribute> subscriberAttributeList = new List<PXEventSubscriberAttribute>();
    foreach (PXParentAttribute pxParentAttribute in cache.GetAttributesReadonly((string) null).OfType<PXParentAttribute>())
    {
      if (pxParentAttribute._ParentType == ParentType)
      {
        subscriberAttributeList.Insert(0, (PXEventSubscriberAttribute) pxParentAttribute);
        break;
      }
      if (ParentType.IsSubclassOf(pxParentAttribute._ParentType))
        subscriberAttributeList.Add((PXEventSubscriberAttribute) pxParentAttribute);
    }
    return subscriberAttributeList.Count > 0 ? PXParentAttribute.FindParent(cache, row, ParentType, (PXParentAttribute) subscriberAttributeList[0]) : (object) null;
  }

  /// <exclude />
  private static object FindParent(
    PXCache cache,
    object row,
    System.Type ParentType,
    PXParentAttribute attr)
  {
    PXView parentSelect = attr.GetParentSelect(cache);
    PXCache cache1 = parentSelect.Cache;
    BqlCommand bqlSelect = parentSelect.BqlSelect;
    object[] objArray = parentSelect.PrepareParameters(new object[1]
    {
      row
    }, new object[0]);
    foreach (object parent in cache1.Cached)
    {
      if (bqlSelect.Meet(cache1, parent, objArray))
        return parent;
    }
    return (object) null;
  }

  /// <summary>Enables or disables cascade deletion of child data records
  /// for the attribute instance in a paricular data record.</summary>
  /// <param name="cache">The cache object to search for the attributes of
  /// <tt>PXParent</tt> type.</param>
  /// <param name="data">The data record the method is applied to. If
  /// <tt>null</tt>, the method is applied to all data records in the cache
  /// object.</param>
  /// <param name="isLeaveChildren">The new value for the
  /// <tt>LeaveChildren</tt> property. If <see langword="true" />, enables cascade
  /// deletion. Otherwise, disables it.</param>
  public static void SetLeaveChildren<Field>(PXCache cache, object data, bool isLeaveChildren) where Field : IBqlField
  {
    if (data == null)
      cache.SetAltered<Field>(true);
    foreach (PXEventSubscriberAttribute attribute in cache.GetAttributes<Field>(data))
    {
      if (attribute is PXParentAttribute)
      {
        ((PXParentAttribute) attribute).LeaveChildren = isLeaveChildren;
        if (!isLeaveChildren)
          cache.Graph.RowDeleted.AddHandler(((PXParentAttribute) attribute)._ParentType, new PXRowDeleted(((PXParentAttribute) attribute).RowDeleted));
        else
          cache.Graph.RowDeleted.RemoveHandler(((PXParentAttribute) attribute)._ParentType, new PXRowDeleted(((PXParentAttribute) attribute).RowDeleted));
      }
    }
  }

  /// <summary>Enables or disables cascade deletion of child data records
  /// for the attribute instance.</summary>
  /// <param name="cache">The cache object to search for the attributes of
  /// <tt>PXParent</tt> type.</param>
  /// <param name="isLeaveChildren">The new value for the
  /// <tt>LeaveChildren</tt> property. If <see langword="true" />, enables cascade
  /// deletion. Otherwise, disables it.</param>
  /// <param name="parentType">The parent type to search for the attribues.</param>
  public static void SetLeaveChildren(PXCache cache, bool isLeaveChildren, System.Type parentType)
  {
    foreach (PXParentAttribute pxParentAttribute in cache.GetAttributes((string) null).OfType<PXParentAttribute>().Where<PXParentAttribute>((Func<PXParentAttribute, bool>) (a => a.ParentType == parentType)))
    {
      cache.SetAltered(pxParentAttribute.FieldName, true);
      pxParentAttribute.LeaveChildren = isLeaveChildren;
      if (!isLeaveChildren)
        cache.Graph.RowDeleted.AddHandler(pxParentAttribute._ParentType, new PXRowDeleted(pxParentAttribute.RowDeleted));
      else
        cache.Graph.RowDeleted.RemoveHandler(pxParentAttribute._ParentType, new PXRowDeleted(pxParentAttribute.RowDeleted));
    }
  }

  /// <exclude />
  public virtual PXView GetParentSelect(PXCache sender)
  {
    return sender.Graph.TypedViews.GetView(this._SelectParent, false);
  }

  /// <exclude />
  public virtual PXView GetChildrenSelect(PXCache sender) => this.GetChildrenSelect(sender, false);

  /// <exclude />
  public virtual PXView GetChildrenSelect(PXCache sender, bool isReadOnly)
  {
    if (this._SelectChildren == null)
      this._initialize(sender);
    return sender.Graph.TypedViews.GetView(this._SelectChildren, isReadOnly);
  }

  /// <summary>Gets or sets the value that indicates at run time whether to
  /// take the parent data record from the <tt>Current</tt> property or
  /// retrieve it from the database. In both cases the attribute uses the
  /// view corresponding to the <tt>Select</tt> query provided in the
  /// constructor.</summary>
  public virtual bool UseCurrent
  {
    get => this._UseCurrent;
    set => this._UseCurrent = value;
  }

  public PXParentAttribute(System.Type fieldsRelation, System.Type additionalCondition)
    : base(fieldsRelation, ReferenceOrigin.ParentAttribute, ReferenceBehavior.Cascade, true)
  {
    System.Type parentSelect = PXParentAttribute.ToParentSelect(fieldsRelation);
    if (!typeof (IBqlSelect).IsAssignableFrom(parentSelect) || parentSelect.GetGenericArguments().Length == 0)
      throw new PXArgumentException("selectParent", "PXParentAttribute accepts BQL selections only.");
    if (additionalCondition == (System.Type) null)
      throw new PXArgumentException(nameof (additionalCondition), "The argument cannot be null.");
    if (!typeof (IBqlWhere).IsAssignableFrom(additionalCondition) || additionalCondition.GetGenericArguments().Length == 0)
      throw new PXArgumentException(nameof (additionalCondition), "The assigned type must implement the {0} interface.", new object[1]
      {
        (object) "IBqlWhere"
      });
    this._selectParentWithoutAdditionalCondition = BqlCommand.CreateInstance(parentSelect);
    this._SelectParent = this._selectParentWithoutAdditionalCondition.WhereAnd(additionalCondition);
    this._ParentType = this._SelectParent.GetTables()[0];
  }

  /// <summary>
  /// Initializes a new instance that defines the parent data record using the
  /// provided BQL query. To provide parameters to the BQL query, use <tt>Current</tt>
  /// to pass the values from the child data record that is <tt>Current</tt> for the
  /// cache object.
  /// </summary>
  /// <param name="selectParent">The BQL query that selects the parent record. Should be
  /// based on a class derived from <tt>IBqlSelect</tt>, such as <tt>Select&lt;&gt;</tt>.</param>
  /// <example>
  /// <code>
  /// [PXParent(typeof(
  ///     Select&lt;ARTran,
  ///         Where&lt;ARTran.tranType, Equal&lt;Current&lt;ARFinChargeTran.tranType&gt;&gt;,
  ///             And&lt;ARTran.refNbr, Equal&lt;Current&lt;ARFinChargeTran.refNbr&gt;&gt;,
  ///             And&lt;ARTran.lineNbr, Equal&lt;Current&lt;ARFinChargeTran.lineNbr&gt;&gt;&gt;&gt;&gt;&gt;))]
  /// public virtual short? LineNbr { get; set; }
  /// </code>
  /// </example>
  public PXParentAttribute(System.Type selectParent)
    : base(selectParent, ReferenceOrigin.ParentAttribute, ReferenceBehavior.Cascade, true)
  {
    selectParent = !(selectParent == (System.Type) null) ? PXParentAttribute.ToParentSelect(selectParent) : throw new PXArgumentException(nameof (selectParent), "The argument cannot be null.");
    if (!typeof (IBqlSelect).IsAssignableFrom(selectParent) || selectParent.GetGenericArguments().Length == 0)
      throw new PXArgumentException(nameof (selectParent), "PXParentAttribute accepts BQL selections only.");
    this._SelectParent = this._selectParentWithoutAdditionalCondition = BqlCommand.CreateInstance(selectParent);
    this._ParentType = this._SelectParent.GetTables()[0];
  }

  /// <example>
  /// <code title="Invalid example">
  /// [PXParent(typeof(Select&lt;ParentDAC, Where&lt;DAC.field, Equal&lt;DAC.field&gt;&gt;&gt;))]
  /// </code>
  /// <code title="Valid example">
  /// [PXParent(typeof(Select&lt;ParentDAC, Where&lt;DAC.field, Equal&lt;Current&lt;DAC.field&gt;&gt;&gt;&gt;))]
  /// </code>
  /// </example>
  private void CheckThatConditionsAreParametrized()
  {
    if (!this._SelectParent.GetFieldPairs().SequenceEqual<KeyValuePair<System.Type, System.Type>>((IEnumerable<KeyValuePair<System.Type, System.Type>>) this._SelectParent.GetParameterPairs()))
      throw new PXException("The condition inside a BQL select for PXParentAttribute declared on {0} must be parametrized.", new object[1]
      {
        (object) $"{this._BqlTable.FullName}.{this._FieldName}"
      });
  }

  /// <example>
  /// <code>
  /// class BaseDAC : PXBqlTable, IBqlTable { public abstract class field : IBqlField }
  /// class DAC : BaseDAC { }
  /// ...
  /// [PXParent(typeof(Select&lt;ParentDAC, Where&lt;ParentDAC.field, Equal&lt;Current&lt;DAC.field&gt;&gt;&gt;&gt;)]
  /// // DAC.field must be re-declared in the "DAC" class
  /// </code>
  /// </example>
  private void CheckThatThereAreNoBqlFieldsFromTheBaseType()
  {
    System.Type currentTable = this.BqlTable;
    System.Type[] array = this._SelectParent.GetParameterPairs().Select<KeyValuePair<System.Type, System.Type>, System.Type>((Func<KeyValuePair<System.Type, System.Type>, System.Type>) (kv => kv.Value.DeclaringType)).Select<System.Type, System.Type>((Func<System.Type, System.Type>) (t =>
    {
      System.Type extendedType = PXExtensionManager.GetExtendedType(t);
      return (object) extendedType != null ? extendedType : t;
    })).Distinct<System.Type>().Where<System.Type>((Func<System.Type, bool>) (t => t != currentTable && t.IsAssignableFrom(currentTable))).ToArray<System.Type>();
    if (((IEnumerable<System.Type>) array).Any<System.Type>())
      throw new PXException("A BQL select for PXParentAttribute declared on {0} references BQL fields that are declared in the base type instead of the current one. They must be redeclared as 'new' to infer the table alias correctly. The BQL fields in question: {1}.", new object[2]
      {
        (object) $"{this._BqlTable.FullName}.{this._FieldName}",
        (object) string.Join(", ", ((IEnumerable<System.Type>) array).Select<System.Type, string>((Func<System.Type, string>) (t => $"{t.DeclaringType.FullName}.{t.Name}")))
      });
  }

  /// <example>
  /// <code>
  /// class A : PXBqlTable, IBqlTable
  /// {
  ///     [PXParent(typeof(Select&lt;B, Where&lt;A.field, Equal&lt;Current&lt;C.field&gt;&gt;&gt;&gt;))]
  ///     // C is neither the child nor the parent table
  /// }
  /// </code>
  /// </example>
  private void CheckThatThereAreNoUnrelatedTablesInTheParentSelect()
  {
    System.Type currentTable = this.BqlTable;
    System.Type[] array = this._selectParentWithoutAdditionalCondition.GetParameterPairs().Select<KeyValuePair<System.Type, System.Type>, System.Type>((Func<KeyValuePair<System.Type, System.Type>, System.Type>) (kv => kv.Value.DeclaringType)).Select<System.Type, System.Type>((Func<System.Type, System.Type>) (t =>
    {
      System.Type extendedType = PXExtensionManager.GetExtendedType(t);
      return (object) extendedType != null ? extendedType : t;
    })).Distinct<System.Type>().Where<System.Type>((Func<System.Type, bool>) (t => !this._ChildType.IsAssignableFrom(t) && !this._ParentType.IsAssignableFrom(t) && !currentTable.IsAssignableFrom(t))).ToArray<System.Type>();
    if (((IEnumerable<System.Type>) array).Any<System.Type>())
      throw new PXException("A BQL select for PXParentAttribute declared on {0} references DACs that are neither child nor parent. Such scenario is only allowed in the 'additionalCondition' constructor argument in exceptional cases. The DACs in question: {1}.", new object[2]
      {
        (object) $"{this._BqlTable.FullName}.{this._FieldName}",
        (object) string.Join(", ", ((IEnumerable<System.Type>) array).Select<System.Type, string>((Func<System.Type, string>) (t => t.FullName)))
      });
  }

  private static System.Type ToParentSelect(System.Type selectOrFieldsRelation)
  {
    if (typeof (IBqlSelect).IsAssignableFrom(selectOrFieldsRelation))
      return selectOrFieldsRelation;
    IFieldsRelation[] source = TypeArrayOf<IFieldsRelation>.IsTypeArrayOrElement(selectOrFieldsRelation) ? TypeArrayOf<IFieldsRelation>.CheckAndExtractInstances(TypeArrayOf<IFieldsRelation>.EmptyOrSingleOrSelf(selectOrFieldsRelation), (string) null) : throw new PXArgumentException("selectParent");
    System.Type[] array1 = ((IEnumerable<IFieldsRelation>) source).Select<IFieldsRelation, System.Type>((Func<IFieldsRelation, System.Type>) (r => r.FieldOfChildTable)).ToArray<System.Type>();
    System.Type[] array2 = ((IEnumerable<IFieldsRelation>) source).Select<IFieldsRelation, System.Type>((Func<IFieldsRelation, System.Type>) (r => r.FieldOfParentTable)).ToArray<System.Type>();
    return new Reference(new TableWithKeys(((IEnumerable<System.Type>) array2).First<System.Type>().DeclaringType, (IEnumerable<System.Type>) array2), new TableWithKeys(((IEnumerable<System.Type>) array1).First<System.Type>().DeclaringType, (IEnumerable<System.Type>) array1), ReferenceOrigin.ParentAttribute, ReferenceBehavior.Cascade, typeof (Current<>)).ParentSelect;
  }

  /// <exclude />
  public virtual void RowInserted(PXCache sender, PXRowInsertedEventArgs e)
  {
    if (e.PendingRow == null)
      return;
    object obj;
    if (this._Pendings.TryGetValue(e.Row, false, out obj) || this._Pendings.TryGetValue(e.PendingRow, false, out obj))
      this._Currents[e.Row] = obj;
    this._Pendings.Remove(e.Row);
  }

  /// <exclude />
  public virtual void SelfRowDeleted(PXCache sender, PXRowDeletedEventArgs e)
  {
    if (!this._Currents.ContainsKey(e.Row))
      return;
    this._Currents.Remove(e.Row);
  }

  /// <exclude />
  public virtual void RowDeleted(PXCache sender, PXRowDeletedEventArgs e)
  {
    if (this._SelectChildren == null)
      this._initialize(sender);
    Lazy<PXCache> lazy = new Lazy<PXCache>((Func<PXCache>) (() => sender.Graph.Caches[this._ChildType]));
    PXView view = sender.Graph.TypedViews.GetView(this._SelectChildren, false);
    object[] currents = new object[1]{ e.Row };
    object[] objArray = Array.Empty<object>();
    foreach (object obj in view.SelectMultiBound(currents, objArray))
    {
      PXParentAttribute.AlterParentCreate(lazy.Value, obj);
      AggregateValidation.IgnoreDeltas(lazy.Value, obj);
      lazy.Value.Delete(obj);
    }
    sender.Graph.TypedViews.GetView(this._SelectChildren, false).Clear();
  }

  /// <exclude />
  public virtual void RowPersisted(PXCache sender, PXRowPersistedEventArgs e)
  {
    if (e.TranStatus != PXTranStatus.Completed)
      return;
    this._Pendings.Clear();
    this._Currents.Clear();
  }

  /// <summary>Returns the child data records that have the same parent as
  /// the provided child data record. Returns an array of zero length if
  /// fails to retrieve the parent. Uses the first attribute instance found
  /// in the cache object.</summary>
  /// <param name="cache">The cache object to search for the attributes of
  /// <tt>PXParent</tt> type.</param>
  /// <param name="row">The child data record for which the data records
  /// having the same parent are retrieved.</param>
  public static object[] SelectSiblings(PXCache cache, object row)
  {
    List<PXEventSubscriberAttribute> subscriberAttributeList = new List<PXEventSubscriberAttribute>();
    foreach (PXEventSubscriberAttribute subscriberAttribute in cache.GetAttributesReadonly((string) null))
    {
      if (subscriberAttribute is PXParentAttribute)
        subscriberAttributeList.Add(subscriberAttribute);
    }
    if (subscriberAttributeList.Count > 1)
      throw new PXException("Multiple PXParentAttribute attributes are found for '{0}'.", new object[1]
      {
        (object) cache.GetItemType().ToString()
      });
    if (subscriberAttributeList.Count > 0)
    {
      PXParentAttribute attr = (PXParentAttribute) subscriberAttributeList[0];
      object obj = PXParentAttribute.SelectParent(cache, row, (object) null, (System.Type) null, attr);
      PXView parentSelect;
      if (obj != null || (parentSelect = attr.GetParentSelect(cache)).Cache.Current != null && parentSelect.Cache.GetStatus(parentSelect.Cache.Current) != PXEntryStatus.Deleted && parentSelect.Cache.GetStatus(parentSelect.Cache.Current) != PXEntryStatus.InsertedDeleted)
        return attr.GetChildrenSelect(cache).SelectMultiBound(new object[1]
        {
          obj
        }).ToArray();
    }
    return new object[0];
  }

  /// <summary>Returns the child data records that have the same parent as
  /// the provided child data record. Returns an array of zero length if
  /// fails to retrieve the parent. Uses the first attribute instance that
  /// references the provided parent type or a type derived from
  /// it.</summary>
  /// <param name="cache">The cache object to search for the attributes of
  /// <tt>PXParent</tt> type.</param>
  /// <param name="row">The child data record for which the data records
  /// having the same parent are retrieved.</param>
  /// <param name="ParentType">The DAC type of the parent data
  /// record.</param>
  public static object[] SelectSiblings(PXCache cache, object row, System.Type ParentType)
  {
    List<PXEventSubscriberAttribute> subscriberAttributeList = new List<PXEventSubscriberAttribute>();
    foreach (PXEventSubscriberAttribute subscriberAttribute in cache.GetAttributesReadonly((string) null))
    {
      if (subscriberAttribute is PXParentAttribute && subscriberAttribute is PXParentAttribute)
      {
        if (((PXParentAttribute) subscriberAttribute)._ParentType == ParentType)
          subscriberAttributeList.Insert(0, subscriberAttribute);
        else if (ParentType.IsSubclassOf(((PXParentAttribute) subscriberAttribute)._ParentType))
          subscriberAttributeList.Add(subscriberAttribute);
      }
    }
    if (subscriberAttributeList.Count > 0)
    {
      PXParentAttribute attr = (PXParentAttribute) subscriberAttributeList[0];
      object obj = PXParentAttribute.SelectParent(cache, row, (object) null, ParentType, attr);
      PXView parentSelect;
      if (obj != null || (parentSelect = attr.GetParentSelect(cache)).Cache.Current != null && parentSelect.Cache.GetStatus(parentSelect.Cache.Current) != PXEntryStatus.Deleted && parentSelect.Cache.GetStatus(parentSelect.Cache.Current) != PXEntryStatus.InsertedDeleted)
        return attr.GetChildrenSelect(cache).SelectMultiBound(new object[1]
        {
          obj
        }).ToArray();
    }
    return new object[0];
  }

  /// <summary>Returns child data records of the specified parent record.</summary>
  /// <param name="ParentType">The DAC that is used to access the parent record.</param>
  /// <param name="cache">The cache object.</param>
  /// <param name="parent">A parent data record.</param>
  /// <example><para>In the example below you iterate through child data records whose parent type is SOLine. Then you validate required conditions and call the SetValueExt method to set the value for the SOLine.shipComplete field." groupname="Example</para>
  /// 	<code title="Example2" lang="CS">
  /// foreach (SOLineSplit split in PXParentAttribute.SelectChildren(splits.Cache, e.Row, typeof(SOLine)))
  /// {
  ///     if (split.ShipmentNbr != null || split.ShippedQty &gt; 0m)
  ///         sender.SetValueExt&lt;SOLine.shipComplete&gt;(e.Row, SOShipComplete.BackOrderAllowed);
  /// }</code>
  /// </example>
  public static object[] SelectChildren(PXCache cache, object parent, System.Type ParentType)
  {
    List<PXEventSubscriberAttribute> subscriberAttributeList = new List<PXEventSubscriberAttribute>();
    foreach (PXEventSubscriberAttribute subscriberAttribute in cache.GetAttributesReadonly((string) null))
    {
      if (subscriberAttribute is PXParentAttribute)
      {
        if (((PXParentAttribute) subscriberAttribute)._ParentType == ParentType)
          subscriberAttributeList.Insert(0, subscriberAttribute);
        else if (ParentType.IsSubclassOf(((PXParentAttribute) subscriberAttribute)._ParentType))
          subscriberAttributeList.Add(subscriberAttribute);
      }
    }
    if (subscriberAttributeList.Count <= 0)
      return new object[0];
    return ((PXParentAttribute) subscriberAttributeList[0]).GetChildrenSelect(cache).SelectMultiBound(new object[1]
    {
      parent
    }).ToArray();
  }

  /// <summary>Returns the parent type of the first attribute instance found
  /// in the cache object.</summary>
  /// <param name="cache">The cache object to search for the attributes of
  /// <tt>PXParent</tt> type.</param>
  public static System.Type GetParentType(PXCache cache)
  {
    foreach (PXEventSubscriberAttribute subscriberAttribute in cache.GetAttributesReadonly((string) null))
    {
      if (subscriberAttribute is PXParentAttribute)
        return ((PXParentAttribute) subscriberAttribute)._ParentType;
    }
    return (System.Type) null;
  }

  /// <summary>Sets the provided data record of parent type as the parent of
  /// the child data record.</summary>
  /// <param name="cache">The cache object to search for the attributes of
  /// <tt>PXParent</tt> type.</param>
  /// <param name="row">The child data record for which the parent data
  /// record is set. Must not be <tt>null</tt>.</param>
  /// <param name="ParentType">The DAC type of the parent data
  /// record.</param>
  /// <param name="parent">The new parent data record.</param>
  /// <example>
  /// The code below sets the parent data record at run time.
  /// <code>
  /// // Views definitions in a graph
  /// public PXSelect&lt;INRegister&gt; inregister;
  /// public PXSelect&lt;INTran&gt; intranselect;
  /// ...
  /// // Code executed in some graph method
  /// INTran tran = (INTran)res;
  /// PXParentAttribute.SetParent(
  ///     intranselect.Cache, tran, typeof(INRegister), inregister.Current);
  /// </code>
  /// </example>
  public static void SetParent(PXCache cache, object row, System.Type ParentType, object parent)
  {
    if (row == null)
      throw new PXArgumentException();
    List<PXEventSubscriberAttribute> subscriberAttributeList = new List<PXEventSubscriberAttribute>();
    foreach (PXEventSubscriberAttribute subscriberAttribute in cache.GetAttributesReadonly((string) null))
    {
      if (subscriberAttribute is PXParentAttribute)
      {
        if (((PXParentAttribute) subscriberAttribute)._ParentType == ParentType)
          subscriberAttributeList.Insert(0, subscriberAttribute);
        else if (ParentType.IsSubclassOf(((PXParentAttribute) subscriberAttribute)._ParentType))
          subscriberAttributeList.Add(subscriberAttribute);
      }
    }
    if (subscriberAttributeList.Count <= 0)
      return;
    PXParentAttribute pxParentAttribute = (PXParentAttribute) subscriberAttributeList[0];
    if (parent == null)
    {
      if (!pxParentAttribute._Currents.ContainsKey(row))
        return;
      pxParentAttribute._Currents.Remove(row);
    }
    else
    {
      object key;
      if ((key = cache.Locate(row)) == null || cache.GetStatus(row) == PXEntryStatus.InsertedDeleted || cache.GetStatus(row) == PXEntryStatus.Deleted)
      {
        pxParentAttribute._Pendings[row] = parent;
        if (!pxParentAttribute._Currents.ContainsKey(row))
          return;
        pxParentAttribute._Currents.Remove(row);
      }
      else
        pxParentAttribute._Currents[key] = parent;
    }
  }

  /// <summary>Makes the parent of the provided data record be the parent of
  /// the other provided data record. Uses the first attribute instance that
  /// references the provided parent type or a type derived from
  /// it.</summary>
  /// <param name="cache">The cache object to search for the attributes of
  /// <tt>PXParent</tt> type.</param>
  /// <param name="item">The child data record whose parent data record is
  /// made the parent of another data record.</param>
  /// <param name="copy">The data record that becomes the child of the
  /// provided data record's parent.</param>
  /// <param name="ParentType">The DAC type of the parent data
  /// record.</param>
  [Obsolete]
  public static void CopyParent(PXCache cache, object item, object copy, System.Type ParentType)
  {
    if (item == null)
      throw new PXArgumentException();
    List<PXEventSubscriberAttribute> subscriberAttributeList = new List<PXEventSubscriberAttribute>();
    foreach (PXEventSubscriberAttribute subscriberAttribute in cache.GetAttributesReadonly((string) null))
    {
      if (subscriberAttribute is PXParentAttribute)
      {
        if (((PXParentAttribute) subscriberAttribute)._ParentType == ParentType)
          subscriberAttributeList.Insert(0, subscriberAttribute);
        else if (ParentType.IsSubclassOf(((PXParentAttribute) subscriberAttribute)._ParentType))
          subscriberAttributeList.Add(subscriberAttribute);
      }
    }
    if (subscriberAttributeList.Count <= 0)
      return;
    PXParentAttribute pxParentAttribute = (PXParentAttribute) subscriberAttributeList[0];
    object obj;
    if (!pxParentAttribute._Currents.TryGetValue(item, out obj))
      return;
    pxParentAttribute._Currents[copy] = obj;
  }

  public static bool IsParent(PXCache cache, object row, System.Type ParentType)
  {
    List<PXEventSubscriberAttribute> subscriberAttributeList = new List<PXEventSubscriberAttribute>();
    foreach (PXEventSubscriberAttribute subscriberAttribute in cache.GetAttributesReadonly((string) null))
    {
      if (subscriberAttribute is PXParentAttribute)
      {
        if (((PXParentAttribute) subscriberAttribute)._ParentType == ParentType)
          subscriberAttributeList.Insert(0, subscriberAttribute);
        else if (ParentType.IsSubclassOf(((PXParentAttribute) subscriberAttribute)._ParentType))
          subscriberAttributeList.Add(subscriberAttribute);
      }
    }
    if (subscriberAttributeList.Count == 0)
      return false;
    PXView parentSelect = ((PXParentAttribute) subscriberAttributeList[0]).GetParentSelect(cache);
    if (!(ParentType == (System.Type) null) && !(parentSelect.CacheGetItemType() == ParentType) && !ParentType.IsAssignableFrom(parentSelect.CacheGetItemType()))
      return false;
    return parentSelect.BqlSelect.Meet(parentSelect.Cache, parentSelect.Cache.Current, parentSelect.PrepareParameters(new object[1]
    {
      row
    }, new object[0]));
  }

  /// <summary>Returns the parent data record of the provided child data
  /// record. Uses the first attribute instance found in the cache
  /// object.</summary>
  /// <param name="cache">The cache object to search for the attributes of
  /// <tt>PXParent</tt> type.</param>
  /// <param name="row">The child data record whose parent data record is
  /// retireved.</param>
  [Obsolete("Use SelectParent methods which specify the type of the parent DAC as safer.")]
  public static object SelectParent(PXCache cache, object row)
  {
    List<PXEventSubscriberAttribute> subscriberAttributeList = new List<PXEventSubscriberAttribute>();
    foreach (PXEventSubscriberAttribute subscriberAttribute in cache.GetAttributesReadonly((string) null))
    {
      if (subscriberAttribute is PXParentAttribute)
        subscriberAttributeList.Add(subscriberAttribute);
    }
    if (subscriberAttributeList.Count > 1)
      throw new PXException("Multiple PXParentAttribute attributes are found for '{0}'.", new object[1]
      {
        (object) cache.GetItemType().ToString()
      });
    return subscriberAttributeList.Count > 0 ? PXParentAttribute.SelectParent(cache, row, (object) null, (System.Type) null, (PXParentAttribute) subscriberAttributeList[0]) : (object) null;
  }

  /// <summary>Returns the parent data record of the provided child data
  /// record. Uses the first attribute instance that references the provided
  /// parent type or a type derived from it.</summary>
  /// <param name="cache">The cache object to search for the attributes of
  /// <tt>PXParent</tt> type.</param>
  /// <param name="row">The child data record whose parent data record is
  /// retireved.</param>
  /// <param name="ParentType">The DAC type of the parent data
  /// record.</param>
  /// <example>
  /// The code below obtains the parent data record at run time.
  /// <code>
  /// CR.Location child = (CR.Location)e.Row;
  /// BAccount parent =
  ///     (BAccount)PXParentAttribute.SelectParent(sender, child, typeof(BAccount));
  /// </code>
  /// </example>
  public static object SelectParent(PXCache cache, object row, System.Type ParentType)
  {
    return PXParentAttribute.SelectParent(cache, row, (object) null, ParentType);
  }

  internal static object SelectParent(PXCache cache, object row, object newRow, System.Type ParentType)
  {
    List<PXEventSubscriberAttribute> subscriberAttributeList = new List<PXEventSubscriberAttribute>();
    foreach (PXEventSubscriberAttribute subscriberAttribute in cache.GetAttributesReadonly((string) null))
    {
      if (subscriberAttribute is PXParentAttribute)
      {
        if (((PXParentAttribute) subscriberAttribute)._ParentType == ParentType)
          subscriberAttributeList.Insert(0, subscriberAttribute);
        else if (ParentType.IsSubclassOf(((PXParentAttribute) subscriberAttribute)._ParentType))
          subscriberAttributeList.Add(subscriberAttribute);
      }
    }
    return subscriberAttributeList.Count > 0 ? PXParentAttribute.SelectParent(cache, row, newRow, ParentType, (PXParentAttribute) subscriberAttributeList[0]) : (object) null;
  }

  private static object SelectParent(
    PXCache cache,
    object row,
    object newRow,
    System.Type ParentType,
    PXParentAttribute attr)
  {
    PXView parentSelect = attr.GetParentSelect(cache);
    if (!(ParentType == (System.Type) null) && !(parentSelect.CacheGetItemType() == ParentType) && !ParentType.IsAssignableFrom(parentSelect.CacheGetItemType()))
      return (object) null;
    if (ParentType == (System.Type) null)
      ParentType = parentSelect.CacheGetItemType();
    if (!attr._UseCurrent)
    {
      if (ParentType.IsAssignableFrom(cache.Graph.PrimaryItemType))
      {
        if (parentSelect.BqlSelect.Meet(parentSelect.Cache, parentSelect.Cache.Current, parentSelect.PrepareParameters(new object[1]
        {
          row
        }, new object[0])))
          goto label_7;
      }
      if (row != null)
      {
        object obj;
        if (attr._Currents.TryGetValue(row, out obj))
          return parentSelect.Cache.Locate(obj) ?? obj;
        if (newRow != null && attr._Pendings.TryGetValue(newRow, out obj))
        {
          attr._Currents[row] = obj;
          attr._Pendings.Remove(newRow);
        }
        if (obj != null)
          return parentSelect.Cache.Locate(obj) ?? obj;
      }
      return PXSelectorAttribute.SelectSingleBound(parentSelect, new object[1]
      {
        row
      });
    }
label_7:
    object current = parentSelect.Cache.Current;
    return parentSelect.Cache.GetStatus(current) == PXEntryStatus.Deleted || parentSelect.Cache.GetStatus(current) == PXEntryStatus.InsertedDeleted ? (object) null : current;
  }

  /// <inheritdoc cref="M:PX.Data.PXParentAttribute.SelectParent(PX.Data.PXCache,System.Object,System.Type)" />
  public static TParentTable SelectParent<TParentTable>(PXCache childCache, object child) where TParentTable : IBqlTable
  {
    return (TParentTable) PXParentAttribute.SelectParent(childCache, child, typeof (TParentTable));
  }

  /// <inheritdoc cref="M:PX.Data.PXParentAttribute.SetParent(PX.Data.PXCache,System.Object,System.Type,System.Object)" />
  public static void SetParent<TParentTable>(PXCache childCache, object child, TParentTable parent) where TParentTable : class, IBqlTable
  {
    PXParentAttribute.SetParent(childCache, child, typeof (TParentTable), (object) parent);
  }

  /// <inheritdoc cref="M:PX.Data.PXParentAttribute.SelectChildren(PX.Data.PXCache,System.Object,System.Type)" />
  public static TChildTable[] SelectChildren<TChildTable, TParentTable>(
    PXCache childCache,
    TParentTable parent)
    where TChildTable : class, IBqlTable
    where TParentTable : class, IBqlTable
  {
    return Enumerable.ToArray<TChildTable>(PXParentAttribute.SelectChildren(childCache, (object) parent, typeof (TParentTable)).Cast<TChildTable>());
  }

  /// <inheritdoc cref="M:PX.Data.PXParentAttribute.SelectSiblings(PX.Data.PXCache,System.Object,System.Type)" />
  public static TChildTable[] SelectSiblings<TChildTable, TParentTable>(
    PXCache childCache,
    TChildTable child)
    where TChildTable : class, IBqlTable
    where TParentTable : class, IBqlTable
  {
    return Enumerable.ToArray<TChildTable>(PXParentAttribute.SelectSiblings(childCache, (object) child, typeof (TParentTable)).Cast<TChildTable>());
  }

  /// <inheritdoc cref="M:PX.Data.PXParentAttribute.SelectSiblings(PX.Data.PXCache,System.Object)" />
  public static TChildTable[] SelectSiblings<TChildTable>(PXCache childCache, TChildTable child) where TChildTable : class, IBqlTable
  {
    return Enumerable.ToArray<TChildTable>(PXParentAttribute.SelectSiblings<TChildTable>(childCache, child).Cast<TChildTable>());
  }

  protected void _initialize(PXCache sender)
  {
    BqlCommand instance;
    lock (PXParentAttribute._slock)
    {
      if (!PXParentAttribute._selects.TryGetValue(this._SelectParent.GetType(), out instance))
      {
        instance = BqlCommand.CreateInstance(PXParentAttribute.Inverse(this._SelectParent.GetType(), this._ParentType, this._BqlTable));
        PXParentAttribute._selects.Add(this._SelectParent.GetType(), instance);
      }
    }
    this._SelectChildren = instance;
  }

  /// <exclude />
  public static System.Type Inverse(System.Type parentSelectCommand, System.Type parentTable, System.Type childTable)
  {
    return PXParentAttribute._inverse(parentSelectCommand, parentTable, ref childTable);
  }

  protected static System.Type _inverse(System.Type command, System.Type parent, ref System.Type child)
  {
    command = BqlCommandDecorator.Unwrap(command);
    if (!command.IsGenericType)
    {
      if (command == parent)
        return child;
      if (!typeof (IBqlField).IsAssignableFrom(command) || !command.IsNested || !(BqlCommand.GetItemType(command) == parent))
        return command;
      return BqlCommand.Compose(typeof (Current<>), command);
    }
    System.Type[] genericArguments = command.GetGenericArguments();
    if ((command.GetGenericTypeDefinition() == typeof (Current<>) || command.GetGenericTypeDefinition() == typeof (Current2<>)) && typeof (IBqlField).IsAssignableFrom(genericArguments[0]) && genericArguments[0].IsNested && child.IsAssignableFrom(BqlCommand.GetItemType(genericArguments[0])))
    {
      child = BqlCommand.GetItemType(genericArguments[0]);
      return genericArguments[0];
    }
    bool flag = false;
    for (int index = genericArguments.Length - 1; index >= 0; --index)
    {
      System.Type type = PXParentAttribute._inverse(genericArguments[index], parent, ref child);
      if (type != genericArguments[index])
      {
        genericArguments[index] = type;
        flag = true;
      }
    }
    if (!flag)
      return command;
    System.Type[] typeArray = new System.Type[genericArguments.Length + 1];
    typeArray[0] = command.GetGenericTypeDefinition();
    for (int index = 1; index < typeArray.Length; ++index)
      typeArray[index] = genericArguments[index - 1];
    return BqlCommand.Compose(typeArray);
  }

  /// <exclude />
  public override void CacheAttached(PXCache sender)
  {
    this._ChildType = sender.GetItemType();
    this.CheckThatConditionsAreParametrized();
    this.CheckThatThereAreNoBqlFieldsFromTheBaseType();
    this.CheckThatThereAreNoUnrelatedTablesInTheParentSelect();
    sender.Graph.RowDeleted.AddHandler(this._ChildType, new PXRowDeleted(this.SelfRowDeleted));
    if (!this._LeaveChildren)
      sender.Graph.RowDeleted.AddHandler(this._ParentType, new PXRowDeleted(this.RowDeleted));
    this._Currents = (PXParentAttribute.ParentCollection) Activator.CreateInstance(typeof (PXParentAttribute.CurrentCollection<>).MakeGenericType(this._ChildType), (object) sender, (object) this);
    this._Pendings = (PXParentAttribute.ParentCollection) Activator.CreateInstance(typeof (PXParentAttribute.PendingCollection<>).MakeGenericType(this._ChildType), (object) sender, (object) this);
  }

  protected bool IsParentReferenceTheSame(PXCache sender, object row, object oldrow)
  {
    foreach (IBqlParameter parameter in this.GetParentSelect(sender).BqlSelect.GetParameters())
    {
      System.Type referencedType = parameter.GetReferencedType();
      if (!object.Equals(sender.GetValue(row, referencedType.Name), sender.GetValue(oldrow, referencedType.Name)))
        return false;
    }
    return true;
  }

  public static bool IsParentReferenceTheSame(
    PXCache sender,
    object row,
    object oldrow,
    System.Type parentType,
    out bool attributeFound)
  {
    return PXParentAttribute.IsParentReferenceTheSame(sender, row, oldrow, parentType, out attributeFound, out bool _);
  }

  public static bool IsParentReferenceTheSame(
    PXCache sender,
    object row,
    object oldrow,
    System.Type parentType,
    out bool attributeFound,
    out bool useCurrent)
  {
    List<PXParentAttribute> pxParentAttributeList = new List<PXParentAttribute>();
    foreach (PXParentAttribute pxParentAttribute in sender.GetAttributesReadonly((string) null).OfType<PXParentAttribute>())
    {
      if (pxParentAttribute._ParentType == parentType)
        pxParentAttributeList.Insert(0, pxParentAttribute);
      else if (parentType.IsSubclassOf(pxParentAttribute._ParentType))
        pxParentAttributeList.Add(pxParentAttribute);
    }
    attributeFound = pxParentAttributeList.Count > 0;
    useCurrent = attributeFound && pxParentAttributeList[0].UseCurrent;
    return !attributeFound || pxParentAttributeList[0].IsParentReferenceTheSame(sender, row, oldrow);
  }

  /// <exclude />
  public abstract class ParentCollection
  {
    public abstract bool TryGetValue(object key, bool remove, out object value);

    public abstract void Clear();

    public abstract bool ContainsKey(object key);

    public abstract bool Remove(object key);

    public abstract object this[object key] { get; set; }

    public bool TryGetValue(object key, out object value) => this.TryGetValue(key, true, out value);
  }

  /// <exclude />
  public class PendingCollection<TNode> : 
    PXParentAttribute.ParentCollection,
    IEqualityComparer<TNode>
    where TNode : class, IBqlTable
  {
    protected PXCache _cache;
    protected PXParentAttribute _owner;
    protected Dictionary<TNode, KeyValuePair<TNode, object>> _dict;

    public PendingCollection(PXCache cache, PXParentAttribute owner)
    {
      this._cache = cache;
      this._owner = owner;
      this._dict = new Dictionary<TNode, KeyValuePair<TNode, object>>((IEqualityComparer<TNode>) this);
    }

    public override bool TryGetValue(object key, bool remove, out object value)
    {
      KeyValuePair<TNode, object> keyValuePair;
      if (this._dict.TryGetValue((TNode) key, out keyValuePair))
      {
        if (key == (object) keyValuePair.Key && this._owner.GetParentSelect(this._cache).Cache.GetStatus(keyValuePair.Value) != PXEntryStatus.Deleted && this._owner.GetParentSelect(this._cache).Cache.GetStatus(keyValuePair.Value) != PXEntryStatus.InsertedDeleted)
        {
          value = keyValuePair.Value;
          return true;
        }
        if (remove)
          this._dict.Remove(keyValuePair.Key);
      }
      value = (object) null;
      return false;
    }

    public override object this[object key]
    {
      get => this._dict[(TNode) key].Value;
      set => this._dict[(TNode) key] = new KeyValuePair<TNode, object>((TNode) key, value);
    }

    public override void Clear() => this._dict.Clear();

    public bool Equals(TNode a, TNode b) => this._cache.ObjectsEqual((object) a, (object) b);

    public int GetHashCode(TNode a) => this._cache.GetObjectHashCode((object) a);

    public override bool ContainsKey(object key) => this._dict.ContainsKey((TNode) key);

    public override bool Remove(object key) => this._dict.Remove((TNode) key);
  }

  /// <exclude />
  public class CurrentCollection<TNode>(PXCache cache, PXParentAttribute owner) : 
    PXParentAttribute.PendingCollection<TNode>(cache, owner)
    where TNode : class, IBqlTable
  {
    public override bool TryGetValue(object key, bool remove, out object value)
    {
      KeyValuePair<TNode, object> keyValuePair;
      if (this._dict.TryGetValue((TNode) key, out keyValuePair))
      {
        if ((key == (object) keyValuePair.Key || this._cache.Locate((object) keyValuePair.Key) == (object) keyValuePair.Key) && this._owner.GetParentSelect(this._cache).Cache.GetStatus(keyValuePair.Value) != PXEntryStatus.Deleted && this._owner.GetParentSelect(this._cache).Cache.GetStatus(keyValuePair.Value) != PXEntryStatus.InsertedDeleted)
        {
          value = keyValuePair.Value;
          return true;
        }
        if (remove)
          this._dict.Remove(keyValuePair.Key);
      }
      value = (object) null;
      return false;
    }
  }
}
