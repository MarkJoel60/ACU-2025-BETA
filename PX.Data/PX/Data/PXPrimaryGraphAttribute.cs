// Decompiled with JetBrains decompiler
// Type: PX.Data.PXPrimaryGraphAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Api.Soap.Screen;
using PX.SM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

#nullable disable
namespace PX.Data;

/// <summary>Sets the primary graph for a DAC. The primary graph
/// determines the default page where a user is redirected for editing a
/// data record.</summary>
/// <remarks>
/// The attribute can be placed on the following declarations:
/// 	<list type="bullet">
/// 		<item>
/// 			<description>On a DAC—To specify the
/// primary graph for this DAC.</description>
/// 		</item>
/// 		<item>
/// 			<description>On a DAC extension—To add or override the
/// specification of the primary graph for the DAC.</description>
/// 		</item>
/// 		<item>
/// 			<description>On a graph—To
/// indicate that it is the primary graph for the specified
/// DACs. This declaration has a higher priority than that on a DAC
/// or a DAC extension. Do not specify the same DAC for different graphs
/// because the behaviour will be unpredictable.</description>
/// 		</item>
/// 		<item>
/// 			<description>On a graph extension—To add or override the default navigation
/// declared on the graph corresponding to the graph extension.</description>
/// 		</item>
/// 	</list>
/// <para>A declaration of this attribute on a higher level extension overrides the
/// declarations of this attribute on DACs (graphs) or their extensions of lower level.</para>
/// <para>For a DAC or a DAC extension, you can specify several graphs and corresponding
/// conditions. In this case, the first graph for which the corresponding condition
/// holds true at run time is considered the primary graph. A condition is
/// a BQL query based on either the <tt>Where</tt> class or the
/// <tt>Select</tt> class.</para>
/// </remarks>
/// <example><para>In the example below, the SalesPersonMaint graph is set as the primary graph for the DAC SalesPerson.</para>
/// <code title="Example" lang="CS">
/// [PXPrimaryGraph(typeof(SalesPersonMaint))]
/// public partial class SalesPerson : PXBqlTable, PX.Data.IBqlTable
/// {
///     ...
/// }</code>
/// <code title="Example2" description="In the example below, the attribute specifies the graph that is used as the primary graph for a DAC if the condition holds true for the data in the cache." groupname="Example" lang="CS">
/// [PXPrimaryGraph(
///     new Type[] { typeof(ShipTermsMaint)},
///     new Type[] { typeof(Select&lt;ShipTerms,
///         Where&lt;ShipTerms.shipTermsID, Equal&lt;Current&lt;ShipTerms.shipTermsID&gt;&gt;&gt;&gt;)
///     })]
/// public partial class ShipTerms : PXBqlTable, PX.Data.IBqlTable
/// {
///     ...
/// }</code>
/// <code title="Example3" description="In the example below, the attribute specifies the graph that is used as the primary graph for a DAC if the Select statement retrieves a non-empty data set." groupname="Example2" lang="CS">
/// [PXPrimaryGraph(
///     new Type[] { typeof(CountryMaint)},
///     new Type[] { typeof(Select&lt;State,
///         Where&lt;State.countryID, Equal&lt;Current&lt;State.countryID&gt;&gt;,
///             And&lt;State.stateID, Equal&lt;Current&lt;State.stateID&gt;&gt;&gt;&gt;&gt;)
///     })]
/// public partial class State : PXBqlTable, PX.Data.IBqlTable
/// {
///     ...
/// }</code>
/// <code title="Example4" description="In the example below, the attribute specifies two graphs and the corresponding Select statements. The first graph for which the Select statement returns a non-empty data set is used as the primary graph for the DAC." groupname="Example3" lang="CS">
/// [PXPrimaryGraph(
///     new Type[] {
///         typeof(APQuickCheckEntry),
///         typeof(APPaymentEntry)
///     },
///     new Type[] {
///         typeof(Select&lt;APQuickCheck,
///             Where&lt;APQuickCheck.docType, Equal&lt;Current&lt;APPayment.docType&gt;&gt;,
///                 And&lt;APQuickCheck.refNbr, Equal&lt;Current&lt;APPayment.refNbr&gt;&gt;&gt;&gt;&gt;),
///         typeof(Select&lt;APPayment,
///             Where&lt;APPayment.docType, Equal&lt;Current&lt;APPayment.docType&gt;&gt;,
///                 And&lt;APPayment.refNbr, Equal&lt;Current&lt;APPayment.refNbr&gt;&gt;&gt;&gt;&gt;)
///     })]
/// public partial class APPayment : APRegister, IInvoice
/// {
///     ...
/// }</code>
/// <code title="Example5" description="In the example below, the attribute defines or redefines the primary graph for the DAC that corresponds to the DAC extension." groupname="Example4" lang="CS">
/// [PXPrimaryGraph(typeof(AnotherGraph))]
/// public class MyDacExtension : PXCacheExtension&lt;MyDac&gt;
/// {
///     ...
/// }</code>
/// <code title="Example6" description="In the example below, the attribute of the graph specifies the DAC for which the graph is used as the primary graph when the Select statement returns a non-empty data set." groupname="Example5" lang="CS">
/// [PXPrimaryGraph(
///     new Type[] {
///         typeof(INUnit)
///     },
///     new Type[] {
///         typeof(Select&lt;UnitOfMeasure,
///             Where&lt;UnitOfMeasure.unit, Equal&lt;Current&lt;INUnit.fromUnit&gt;&gt;&gt;&gt;)
///     })]
/// public class UnitOfMeasureMaint : PXGraph&lt;UnitOfMeasureMaint, UnitOfMeasure&gt;
/// {
///     ...
/// }</code>
/// <code title="Example7" description="In the example below, the attribute defines the graph that corresponds to the graph extension as the primary graph for the DAC. This definition has the highest priority over other definitions of the primary graph." groupname="Example6" lang="CS">
/// [PXPrimaryGraph(typeof(MyDac))]
/// public class MyGraphExtension : PXGraphExtension&lt;MyGraph&gt;
/// {
///     ...
/// }</code>
/// </example>
public class PXPrimaryGraphAttribute : PXPrimaryGraphBaseAttribute
{
  private static Dictionary<System.Type, List<Attribute>> _pgAtts;
  private static readonly object _syncObj = new object();
  protected static Dictionary<System.Type, List<KeyValuePair<System.Type, PXPrimaryGraphAttribute>>> _graphDefined;
  protected bool _UseParent = true;
  protected System.Type _TheOnlyGraph;
  protected System.Type _TheOnlyDac;
  protected System.Type[] _GraphTypes;
  protected System.Type[] _Conditions;
  protected System.Type[] _DacTypes;
  protected System.Type _Filter;
  protected System.Type[] _Filters;

  protected internal static Dictionary<System.Type, List<KeyValuePair<System.Type, PXPrimaryGraphAttribute>>> GraphDefined
  {
    get
    {
      if (PXPrimaryGraphAttribute._graphDefined != null)
        return PXPrimaryGraphAttribute._graphDefined;
      lock (PXPrimaryGraphAttribute._syncObj)
      {
        if (PXPrimaryGraphAttribute._graphDefined != null)
          return PXPrimaryGraphAttribute._graphDefined;
        PXPrimaryGraphAttribute._graphDefined = new Dictionary<System.Type, List<KeyValuePair<System.Type, PXPrimaryGraphAttribute>>>();
        foreach (Graph graph in ServiceManager.AllGraphsNotCustomized)
        {
          System.Type graphTypeByFullName = ServiceManager.GetGraphTypeByFullName(graph.GraphName);
          if (!(graphTypeByFullName == (System.Type) null))
          {
            foreach (System.Type type in PXGraph._GetExtensions(graphTypeByFullName).AsEnumerable<System.Type>().Reverse<System.Type>())
              PXPrimaryGraphAttribute.AppendGraphsAttributes(type, graphTypeByFullName);
            PXPrimaryGraphAttribute.AppendGraphsAttributes(graphTypeByFullName, (System.Type) null);
          }
        }
      }
      return PXPrimaryGraphAttribute._graphDefined;
    }
  }

  /// <exclude />
  public virtual System.Type Filter
  {
    get => this._Filter;
    set => this._Filter = value;
  }

  /// <exclude />
  public virtual System.Type[] Filters
  {
    get => this._Filters;
    set => this._Filters = value;
  }

  /// <exclude />
  public override bool UseParent
  {
    get => this._UseParent;
    set => this._UseParent = value;
  }

  /// <summary>For a DAC, the attribute specifies a graph that will be used
  /// to edit DAC's records. For a graph, the attribute specifies a DAC whose records
  /// will be edited by the graph.</summary>
  /// <param name="type">The business logic controller (graph) or the DAC.
  /// The graph should derive from <tt>PXGraph</tt>. The DAC should
  /// implement <tt>IBqlTable</tt>.</param>
  public PXPrimaryGraphAttribute(System.Type type)
  {
    if (type.IsSubclassOf(typeof (PXGraph)))
      this._TheOnlyGraph = type;
    if (!(type.GetInterface(typeof (IBqlTable).FullName) != (System.Type) null))
      return;
    this._TheOnlyDac = type;
  }

  /// <summary>For a DAC, the attribute specifies graphs and corresponding conditions;
  /// the first graph whose condition is met will be used to edit DAC's records.
  /// For a graph, the attribute specifies DACs and corresponding conditions;
  /// records of the first DAC whose condition is met will be edited by the graph.</summary>
  /// <param name="types">The array of business logic controllers (graphs) or
  /// DACs. A graph should derive from <tt>PXGraph</tt>. A DAC should
  /// implement <tt>IBqlTable</tt>.</param>
  /// <param name="conditions">The array of conditions that correspond to
  /// the graphs or DACs specified in the first parameter. Specify BQL
  /// queries, either <tt>Where</tt> expressions or <tt>Select</tt>
  /// commands.</param>
  /// <example>
  /// In the example below, the attribute specifies the graph that is used as
  /// the primary graph for a DAC if the condition holds true for the data in
  /// the cache.
  /// <code>
  /// [PXPrimaryGraph(
  ///     new Type[] { typeof(ShipTermsMaint)},
  ///     new Type[] { typeof(Select&lt;ShipTerms,
  ///         Where&lt;ShipTerms.shipTermsID, Equal&lt;Current&lt;ShipTerms.shipTermsID&gt;&gt;&gt;&gt;)
  ///     })]
  /// public partial class ShipTerms : PXBqlTable, PX.Data.IBqlTable
  /// {
  ///     ...
  /// }
  /// </code>
  /// </example>
  public PXPrimaryGraphAttribute(System.Type[] types, System.Type[] conditions)
  {
    List<System.Type> typeList1 = new List<System.Type>();
    List<System.Type> typeList2 = new List<System.Type>();
    foreach (System.Type type in types)
    {
      if (type.IsSubclassOf(typeof (PXGraph)))
        typeList2.Add(type);
      if (type.GetInterface(typeof (IBqlTable).FullName) != (System.Type) null)
        typeList1.Add(type);
    }
    if (typeList2.Count >= typeList1.Count)
      this._GraphTypes = typeList2.ToArray();
    else
      this._DacTypes = typeList1.ToArray();
    this._Conditions = conditions;
  }

  internal IEnumerable<System.Type> GetConditions()
  {
    return this._Conditions == null ? Enumerable.Empty<System.Type>() : (IEnumerable<System.Type>) this._Conditions;
  }

  /// <exclude />
  public override System.Type GetGraphType(
    PXCache cache,
    ref object row,
    bool checkRights,
    System.Type preferedType)
  {
    if (this._TheOnlyGraph != (System.Type) null)
      return this._TheOnlyGraph;
    if (this._GraphTypes == null || this._GraphTypes.Length == 0 || this._Conditions == null)
      return (System.Type) null;
    for (int index = 0; index < this._GraphTypes.Length && index < this._Conditions.Length; ++index)
    {
      if (typeof (PXGraph).IsAssignableFrom(this._GraphTypes[index]) && (!checkRights || PXAccess.VerifyRights(this._GraphTypes[index])))
      {
        if (typeof (IBqlWhere).IsAssignableFrom(this._Conditions[index]))
        {
          IBqlWhere instance = (IBqlWhere) Activator.CreateInstance(this._Conditions[index]);
          bool? nullable1 = new bool?();
          object obj1 = (object) null;
          PXCache cache1 = cache;
          object obj2 = row;
          List<object> pars = new List<object>();
          ref bool? local1 = ref nullable1;
          ref object local2 = ref obj1;
          ((IBqlUnary) instance).Verify(cache1, obj2, pars, ref local1, ref local2);
          bool? nullable2 = nullable1;
          bool flag = true;
          if (nullable2.GetValueOrDefault() == flag & nullable2.HasValue && (preferedType == (System.Type) null || preferedType.IsAssignableFrom(this._GraphTypes[index])))
          {
            if (this._Filters != null && index < this._Filters.Length)
              this._Filter = this.Filters[index];
            return this._GraphTypes[index];
          }
        }
        else if (typeof (BqlCommand).IsAssignableFrom(this._Conditions[index]))
        {
          BqlCommand instance = (BqlCommand) Activator.CreateInstance(this._Conditions[index]);
          object obj = new PXView(cache.Graph, false, instance).SelectSingleBound(new object[1]
          {
            row
          });
          if (obj != null && (preferedType == (System.Type) null || preferedType.IsAssignableFrom(this._GraphTypes[index])))
          {
            row = obj;
            if (row is PXResult)
              row = ((PXResult) row)[0];
            if (this._Filters != null && index < this._Filters.Length)
              this._Filter = this.Filters[index];
            return this._GraphTypes[index];
          }
        }
      }
    }
    return row == null && this._GraphTypes != null && this._GraphTypes.Length != 0 ? this._GraphTypes[this._GraphTypes.Length - 1] : (System.Type) null;
  }

  /// <exclude />
  public virtual System.Type ValidateGraphType(
    PXCache cache,
    System.Type graphType,
    System.Type dacType,
    ref object row,
    bool checkRights)
  {
    if (this._TheOnlyDac != (System.Type) null)
      return this._TheOnlyDac;
    if (this._DacTypes == null || this._DacTypes.Length == 0 || this._Conditions == null)
      return (System.Type) null;
    for (int index = 0; index < this._DacTypes.Length && index < this._Conditions.Length; ++index)
    {
      if (dacType.IsAssignableFrom(this._DacTypes[index]) && typeof (IBqlTable).IsAssignableFrom(this._DacTypes[index]) && (!checkRights || PXAccess.VerifyRights(graphType)))
      {
        if (typeof (IBqlWhere).IsAssignableFrom(this._Conditions[index]))
        {
          IBqlWhere instance = (IBqlWhere) Activator.CreateInstance(this._Conditions[index]);
          bool? nullable1 = new bool?();
          object obj1 = (object) null;
          PXCache cache1 = cache;
          object obj2 = row;
          List<object> pars = new List<object>();
          ref bool? local1 = ref nullable1;
          ref object local2 = ref obj1;
          ((IBqlUnary) instance).Verify(cache1, obj2, pars, ref local1, ref local2);
          bool? nullable2 = nullable1;
          bool flag = true;
          if (nullable2.GetValueOrDefault() == flag & nullable2.HasValue)
          {
            if (this._Filters != null && index < this._Filters.Length)
              this._Filter = this.Filters[index];
            return graphType;
          }
        }
        else if (typeof (BqlCommand).IsAssignableFrom(this._Conditions[index]))
        {
          BqlCommand instance = (BqlCommand) Activator.CreateInstance(this._Conditions[index]);
          object obj = new PXView(cache.Graph, false, instance).SelectSingleBound(new object[1]
          {
            row
          });
          if (obj != null)
          {
            row = obj;
            if (row is PXResult)
              row = ((PXResult) row)[0];
            if (this._Filters != null && index < this._Filters.Length)
              this._Filter = this.Filters[index];
            return graphType;
          }
        }
      }
    }
    return (System.Type) null;
  }

  /// <exclude />
  public override IEnumerable<System.Type> GetAllGraphTypes()
  {
    return this._TheOnlyGraph != (System.Type) null ? (IEnumerable<System.Type>) new System.Type[1]
    {
      this._TheOnlyGraph
    } : (this._GraphTypes == null || this._GraphTypes.Length == 0 || this._Conditions == null ? Enumerable.Empty<System.Type>() : (IEnumerable<System.Type>) this._GraphTypes);
  }

  /// <exclude />
  public static PXPrimaryGraphBaseAttribute FindPrimaryGraph(PXCache cache, out System.Type graphType)
  {
    object row = (object) null;
    System.Type declaredType = (System.Type) null;
    PXCache declaredCache = (PXCache) null;
    return PXPrimaryGraphAttribute.FindPrimaryGraph(cache, ref row, out graphType, out declaredType, out declaredCache);
  }

  /// <exclude />
  public static PXPrimaryGraphBaseAttribute FindPrimaryGraph(
    PXCache cache,
    ref object row,
    out System.Type graphType)
  {
    return PXPrimaryGraphAttribute.FindPrimaryGraph(cache, true, ref row, out graphType);
  }

  /// <exclude />
  public static PXPrimaryGraphBaseAttribute FindPrimaryGraph(
    PXCache cache,
    bool checkRights,
    ref object row,
    out System.Type graphType)
  {
    System.Type declaredType = (System.Type) null;
    PXCache declaredCache = (PXCache) null;
    return PXPrimaryGraphAttribute.FindPrimaryGraph(cache, checkRights, ref row, out graphType, out declaredType, out declaredCache);
  }

  /// <exclude />
  public static PXPrimaryGraphBaseAttribute FindPrimaryGraph(
    PXCache cache,
    ref object row,
    out System.Type graphType,
    out System.Type declaredType,
    out PXCache declaredCache)
  {
    return PXPrimaryGraphAttribute.FindPrimaryGraph(cache, true, ref row, out graphType, out declaredType, out declaredCache);
  }

  /// <exclude />
  public static PXPrimaryGraphBaseAttribute FindPrimaryGraph(
    PXCache cache,
    System.Type preferedType,
    ref object row,
    out System.Type graphType,
    out System.Type declaredType,
    out PXCache declaredCache)
  {
    return PXPrimaryGraphAttribute.FindPrimaryGraph(cache, preferedType, true, ref row, out graphType, out declaredType, out declaredCache);
  }

  /// <exclude />
  public static PXPrimaryGraphBaseAttribute FindPrimaryGraph(
    PXCache cache,
    bool checkRights,
    ref object row,
    out System.Type graphType,
    out System.Type declaredType,
    out PXCache declaredCache)
  {
    return PXPrimaryGraphAttribute.FindPrimaryGraph(cache, (System.Type) null, checkRights, ref row, out graphType, out declaredType, out declaredCache);
  }

  /// <exclude />
  public static PXPrimaryGraphBaseAttribute FindPrimaryGraph(
    PXCache cache,
    System.Type preferedType,
    bool checkRights,
    ref object row,
    out System.Type graphType,
    out System.Type declaredType,
    out PXCache declaredCache)
  {
    PXPrimaryGraphAttribute.PrimaryAttributeInfo primaryAttributeInfo = PXPrimaryGraphAttribute.FindPrimaryGraphs(cache, preferedType, checkRights, row).FirstOrDefault<PXPrimaryGraphAttribute.PrimaryAttributeInfo>();
    if (primaryAttributeInfo != null)
    {
      row = primaryAttributeInfo.Row;
      graphType = primaryAttributeInfo.GraphType;
      declaredType = primaryAttributeInfo.DeclaredType;
      declaredCache = primaryAttributeInfo.DeclaredCache;
    }
    else
    {
      graphType = (System.Type) null;
      declaredType = cache.GetItemType();
      declaredCache = cache;
    }
    return primaryAttributeInfo?.Attribute;
  }

  private static IEnumerable<PXPrimaryGraphAttribute.PrimaryAttributeInfo> FindPrimaryGraphs(
    PXCache cache,
    System.Type preferedType,
    bool checkRights,
    object row)
  {
    System.Type origType = (System.Type) null;
    System.Type graphType = (System.Type) null;
    PXCache declaredCache = cache;
    System.Type declaredType = declaredCache.GetItemType();
    while (declaredCache != null && declaredType != (System.Type) null && declaredType != origType)
    {
      origType = declaredType;
      for (; declaredType != (System.Type) null; declaredType = declaredType.BaseType)
      {
        if (PXPrimaryGraphAttribute.GraphDefined.ContainsKey(declaredType))
        {
          foreach (KeyValuePair<System.Type, PXPrimaryGraphAttribute> keyValuePair in PXPrimaryGraphAttribute.GraphDefined[declaredType])
          {
            if (keyValuePair.Value.ValidateGraphType(cache, keyValuePair.Key, declaredType, ref row, checkRights) != (System.Type) null)
            {
              System.Type key = keyValuePair.Key;
              yield return new PXPrimaryGraphAttribute.PrimaryAttributeInfo((PXPrimaryGraphBaseAttribute) keyValuePair.Value, row, key, declaredType, declaredCache);
            }
          }
        }
      }
      declaredType = origType;
      PXPrimaryGraphBaseAttribute attribute1 = (PXPrimaryGraphBaseAttribute) null;
      foreach (System.Type declaredType1 in ((IEnumerable<System.Type>) cache.GetExtensionTypes()).Reverse<System.Type>())
      {
        foreach (PXPrimaryGraphAttribute.PrimaryAttributeInfo info in PXPrimaryGraphAttribute.GetPrimaryAttributesInfo(declaredType1, true))
        {
          if (PXPrimaryGraphAttribute.Unwrap(info, declaredCache, checkRights, preferedType, ref row, out graphType, out declaredType, out attribute1))
            yield return new PXPrimaryGraphAttribute.PrimaryAttributeInfo(attribute1, row, graphType, declaredType, declaredCache);
        }
      }
      declaredType = origType;
      bool UseParent = true;
      foreach (PXPrimaryGraphAttribute.PrimaryAttributeInfo info in PXPrimaryGraphAttribute.GetPrimaryAttributesInfo(declaredType, true))
      {
        UseParent = UseParent && info.Attribute.UseParent;
        if (PXPrimaryGraphAttribute.Unwrap(info, declaredCache, checkRights, preferedType, ref row, out graphType, out declaredType, out attribute1))
          yield return new PXPrimaryGraphAttribute.PrimaryAttributeInfo(attribute1, row, graphType, declaredType, declaredCache);
      }
      bool flag1 = false;
      bool flag2 = false;
      foreach (PXEventSubscriberAttribute attribute2 in declaredCache.GetAttributes(row, (string) null))
      {
        if (attribute2 is PXParentAttribute & UseParent)
        {
          flag1 = true;
          if (row == null)
          {
            declaredType = PXParentAttribute.GetParentType(declaredCache);
            break;
          }
          row = ((PXParentAttribute) attribute2).GetParentSelect(declaredCache).SelectSingleBound(new object[1]
          {
            row
          });
          if (row == null)
          {
            flag2 = true;
          }
          else
          {
            declaredType = row.GetType();
            declaredCache = cache.Graph.Caches[declaredType];
            break;
          }
        }
        else if (flag2)
        {
          declaredType = (System.Type) null;
          declaredCache = (PXCache) null;
        }
      }
      if (!flag1)
        break;
    }
  }

  private static IEnumerable<Attribute> GetAssemblyAttribute(System.Type type)
  {
    lock (PXPrimaryGraphAttribute._syncObj)
    {
      if (PXPrimaryGraphAttribute._pgAtts == null)
      {
        PXPrimaryGraphAttribute._pgAtts = new Dictionary<System.Type, List<Attribute>>();
        foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
        {
          if (PXSubstManager.IsSuitableTypeExportAssembly(assembly, true))
          {
            try
            {
              foreach (PXDACDescriptionAttribute customAttribute in assembly.GetCustomAttributes(typeof (PXDACDescriptionAttribute), true))
              {
                List<Attribute> attributeList;
                if (!PXPrimaryGraphAttribute._pgAtts.TryGetValue(customAttribute.Target, out attributeList))
                {
                  attributeList = new List<Attribute>();
                  PXPrimaryGraphAttribute._pgAtts[customAttribute.Target] = attributeList;
                }
                attributeList.Add(customAttribute.Attribute);
              }
            }
            catch
            {
            }
          }
        }
      }
      List<Attribute> attributeList1;
      return PXPrimaryGraphAttribute._pgAtts.TryGetValue(type, out attributeList1) ? (IEnumerable<Attribute>) attributeList1 : (IEnumerable<Attribute>) new Attribute[0];
    }
  }

  internal static IEnumerable<PXPrimaryGraphBaseAttribute> GetPrimaryAttributes(
    System.Type declaredType,
    bool searchBase = true)
  {
    return PXPrimaryGraphAttribute.GetPrimaryAttributesInfo(declaredType, searchBase).Select<PXPrimaryGraphAttribute.PrimaryAttributeInfo, PXPrimaryGraphBaseAttribute>((Func<PXPrimaryGraphAttribute.PrimaryAttributeInfo, PXPrimaryGraphBaseAttribute>) (attrInfo => attrInfo.Attribute));
  }

  private static IEnumerable<PXPrimaryGraphAttribute.PrimaryAttributeInfo> GetPrimaryAttributesInfo(
    System.Type declaredType,
    bool searchBase)
  {
    if (declaredType.IsDefined(typeof (PXPrimaryGraphBaseAttribute), true))
    {
      for (; declaredType != (System.Type) null; declaredType = searchBase ? declaredType.BaseType : (System.Type) null)
      {
        bool flag = false;
        if (typeof (IBqlTable).IsAssignableFrom(declaredType))
          flag = true;
        else if (declaredType.IsSubclassOf(typeof (PXGraph)))
          flag = true;
        else if (declaredType.IsSubclassOf(typeof (PXCacheExtension)))
          flag = true;
        else if (declaredType.IsSubclassOf(typeof (PXGraphExtension)))
          flag = true;
        if (flag)
        {
          List<Attribute> attributeList = new List<Attribute>();
          attributeList.AddRange(PXPrimaryGraphAttribute.GetAssemblyAttribute(declaredType));
          attributeList.AddRange(declaredType.GetCustomAttributes(false).Cast<Attribute>());
          foreach (Attribute attribute in attributeList)
          {
            if (attribute is PXPrimaryGraphBaseAttribute attr)
              yield return new PXPrimaryGraphAttribute.PrimaryAttributeInfo(declaredType, attr);
          }
        }
      }
    }
  }

  /// <exclude />
  public static IEnumerable<PXPrimaryGraphBaseAttribute> GetAttributes(PXCache cache)
  {
    return PXPrimaryGraphAttribute.FindPrimaryGraphs(cache, (System.Type) null, false, (object) null).Select<PXPrimaryGraphAttribute.PrimaryAttributeInfo, PXPrimaryGraphBaseAttribute>((Func<PXPrimaryGraphAttribute.PrimaryAttributeInfo, PXPrimaryGraphBaseAttribute>) (info => info.Attribute));
  }

  private static void AppendGraphsAttributes(System.Type type, System.Type primaryType)
  {
    using (IEnumerator<PXPrimaryGraphAttribute.PrimaryAttributeInfo> enumerator = PXPrimaryGraphAttribute.GetPrimaryAttributesInfo(type, false).GetEnumerator())
    {
      while (enumerator.MoveNext() && enumerator.Current.Attribute is PXPrimaryGraphAttribute attribute && (!(attribute._TheOnlyDac == (System.Type) null) || attribute._DacTypes != null && attribute._DacTypes.Length != 0))
      {
        System.Type[] typeArray;
        if (!(attribute._TheOnlyDac != (System.Type) null))
          typeArray = attribute._DacTypes;
        else
          typeArray = new System.Type[1]{ attribute._TheOnlyDac };
        foreach (System.Type key1 in (IEnumerable<System.Type>) typeArray)
        {
          List<KeyValuePair<System.Type, PXPrimaryGraphAttribute>> source;
          if (!PXPrimaryGraphAttribute._graphDefined.ContainsKey(key1))
          {
            Dictionary<System.Type, List<KeyValuePair<System.Type, PXPrimaryGraphAttribute>>> graphDefined = PXPrimaryGraphAttribute._graphDefined;
            System.Type key2 = key1;
            List<KeyValuePair<System.Type, PXPrimaryGraphAttribute>> keyValuePairList1;
            List<KeyValuePair<System.Type, PXPrimaryGraphAttribute>> keyValuePairList2 = keyValuePairList1 = new List<KeyValuePair<System.Type, PXPrimaryGraphAttribute>>();
            List<KeyValuePair<System.Type, PXPrimaryGraphAttribute>> keyValuePairList3 = keyValuePairList1;
            graphDefined[key2] = keyValuePairList1;
            source = keyValuePairList3;
          }
          else
            source = PXPrimaryGraphAttribute._graphDefined[key1];
          if (!source.Any<KeyValuePair<System.Type, PXPrimaryGraphAttribute>>((Func<KeyValuePair<System.Type, PXPrimaryGraphAttribute>, bool>) (p => p.Key == primaryType)))
          {
            List<KeyValuePair<System.Type, PXPrimaryGraphAttribute>> keyValuePairList = source;
            System.Type key3 = primaryType;
            if ((object) key3 == null)
              key3 = type;
            KeyValuePair<System.Type, PXPrimaryGraphAttribute> keyValuePair = new KeyValuePair<System.Type, PXPrimaryGraphAttribute>(key3, attribute);
            keyValuePairList.Add(keyValuePair);
          }
        }
      }
    }
  }

  private static bool Unwrap(
    PXPrimaryGraphAttribute.PrimaryAttributeInfo info,
    PXCache cache,
    bool checkRights,
    System.Type preferedType,
    ref object row,
    out System.Type graphType,
    out System.Type declaredType,
    out PXPrimaryGraphBaseAttribute attribute)
  {
    attribute = info.Attribute;
    declaredType = info.DeclaredType;
    if (preferedType != (System.Type) null)
    {
      object row1 = row;
      graphType = attribute.GetGraphType(cache, ref row1, checkRights, preferedType);
      if (graphType != (System.Type) null)
      {
        row = row1;
        return true;
      }
    }
    graphType = attribute.GetGraphType(cache, ref row, checkRights, (System.Type) null);
    return graphType != (System.Type) null;
  }

  /// <exclude />
  private class PrimaryAttributeInfo
  {
    public PXPrimaryGraphBaseAttribute Attribute { get; private set; }

    public object Row { get; private set; }

    public System.Type GraphType { get; private set; }

    public System.Type DeclaredType { get; private set; }

    public PXCache DeclaredCache { get; private set; }

    public PrimaryAttributeInfo(System.Type declaredType, PXPrimaryGraphBaseAttribute attr)
    {
      this.DeclaredType = declaredType;
      this.Attribute = attr;
    }

    public PrimaryAttributeInfo(
      PXPrimaryGraphBaseAttribute attr,
      object row,
      System.Type graphType,
      System.Type declaredType,
      PXCache declaredCache)
    {
      this.Attribute = attr;
      this.Row = row;
      this.GraphType = graphType;
      this.DeclaredType = declaredType;
      this.DeclaredCache = declaredCache;
    }
  }
}
