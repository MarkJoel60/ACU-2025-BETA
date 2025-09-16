// Decompiled with JetBrains decompiler
// Type: PX.Data.PXCustomSelectorAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;

#nullable disable
namespace PX.Data;

/// <summary>The base class for custom selector attributes. To create a custom selector attribute, you derive a class from this class and implement the
/// <tt>GetRecords()</tt> method, as shown in the example below.</summary>
/// <remarks>You can also override the <see cref="M:PX.Data.PXCustomSelectorAttribute.CacheAttached(PX.Data.PXCache)">CacheAttached</see> method to add initialization logic and override
/// the <see cref="M:PX.Data.PXCustomSelectorAttribute.FieldVerifying(PX.Data.PXCache,PX.Data.PXFieldVerifyingEventArgs)">FieldVerifying</see> method to redefine the verification logic for the field value.</remarks>
/// <example>
///   <code title="" description="" lang="CS">
/// public class MyCustomSelector : PXCustomSelectorAttribute
/// {
///     public MyCustomSelector(Type type)
///         : base(type) { }
/// 
///     public virtual IEnumerable GetRecords()
///     {
///         ...
///     }
/// }</code>
/// </example>
public class PXCustomSelectorAttribute : PXSelectorAttribute
{
  private readonly long createDate;
  protected PXGraph _Graph;
  private bool _raiseFieldSelecting = true;
  private static readonly System.Type[] SelectDelegateMap = new System.Type[12]
  {
    typeof (PXSelectDelegate),
    typeof (PXSelectDelegate<>),
    typeof (PXSelectDelegate<,>),
    typeof (PXSelectDelegate<,,>),
    typeof (PXSelectDelegate<,,,>),
    typeof (PXSelectDelegate<,,,,>),
    typeof (PXSelectDelegate<,,,,,>),
    typeof (PXSelectDelegate<,,,,,,>),
    typeof (PXSelectDelegate<,,,,,,,>),
    typeof (PXSelectDelegate<,,,,,,,,>),
    typeof (PXSelectDelegate<,,,,,,,,,>),
    typeof (PXSelectDelegate<,,,,,,,,,,>)
  };
  private static readonly Dictionary<string, PXCustomSelectorAttribute.CreateViewDelegate> createView = new Dictionary<string, PXCustomSelectorAttribute.CreateViewDelegate>();
  private static readonly object _vlock = new object();
  protected bool writeLog;

  /// <summary>Initializes a new instance with the specified BQL query for selecting the data records to show to the user.</summary>
  /// <param name="type">A BQL query that defines the data set that is shown to the user along with the key field that is used as a value. Set to a field (type part of a DAC field) to
  /// select all data records from the referenced table. Set to a BQL command of <tt>Search</tt> type to specify a complex select statement.</param>
  public PXCustomSelectorAttribute(System.Type type)
    : base(type)
  {
    this.createDate = System.DateTime.Now.Ticks;
  }

  /// <summary>Initializes a new instance that will use the specified BQL query to retrieve the data records to select from and display the provided set of columns.</summary>
  /// <param name="type">A BQL query that defines the data set that is shown to the user along with the key field that is used as a value. Set to a field (type part of a DAC field) to
  /// select all data records from the referenced table. Set to a BQL command of <tt>Search</tt> type to specify a complex select statement.</param>
  /// <param name="fieldList">Fields to display in the control.</param>
  public PXCustomSelectorAttribute(System.Type type, params System.Type[] fieldList)
    : base(type, fieldList)
  {
    this.createDate = System.DateTime.Now.Ticks;
  }

  protected override PXView GetView(PXCache cache, BqlCommand select, bool isReadOnly)
  {
    return (PXView) new PXCustomSelectorAttribute.FilteredView(cache.Graph.Views[this._ViewName], base.GetView(cache, select, isReadOnly), this);
  }

  protected override PXView GetUnconditionalView(PXCache cache)
  {
    return this.GetView(cache, this._UnconditionalSelect, !this.DirtyRead);
  }

  public override bool ExcludeFromReferenceGeneratingProcess { get; set; } = true;

  /// <summary>
  /// The handler of the <tt>FieldVerifying</tt> event.
  /// </summary>
  /// <param name="sender">The cache object that has raised the event.</param>
  /// <param name="e">The event arguments.</param>
  public override void FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
  {
    if (this.ValidateValue && e.NewValue != null && (sender.Keys.Count == 0 || this._FieldName != sender.Keys[sender.Keys.Count - 1]))
    {
      List<object> objectList = sender.Graph.Views[this._ViewName].SelectMultiBound(new object[1]
      {
        e.Row
      });
      PXCache cach = sender.Graph.Caches[BqlCommand.GetItemType(this.ForeignField)];
      foreach (object row in objectList)
      {
        object data = (object) PXResult.UnwrapMain(row);
        object objA = cach.GetValue(data, this.ForeignField.Name);
        if (object.Equals(objA, e.NewValue))
          return;
        if (objA is Array && e.NewValue is Array && ((Array) objA).Length == ((Array) e.NewValue).Length)
        {
          bool flag = true;
          int index = 0;
          while (index < ((Array) objA).Length && (flag = flag && object.Equals(((Array) objA).GetValue(index), ((Array) e.NewValue).GetValue(index))))
            ++index;
          if (flag)
            return;
        }
      }
      throw new PXSetPropertyException("'{0}' cannot be found in the system.", new object[1]
      {
        (object) $"[{this._FieldName}]"
      });
    }
  }

  private string GetHash() => this.GetType().FullName + this.createDate.ToString();

  private static PXCustomSelectorAttribute.CreateViewDelegate CreateDelegate(
    PXCustomSelectorAttribute attr)
  {
    DynamicMethod dynamicMethod;
    if (!PXGraph.IsRestricted)
      dynamicMethod = new DynamicMethod("InitView", typeof (PXView), new System.Type[4]
      {
        typeof (PXCustomSelectorAttribute),
        typeof (PXGraph),
        typeof (bool),
        typeof (BqlCommand)
      }, typeof (PXCustomSelectorAttribute), true);
    else
      dynamicMethod = new DynamicMethod("InitView", typeof (PXView), new System.Type[4]
      {
        typeof (PXCustomSelectorAttribute),
        typeof (PXGraph),
        typeof (bool),
        typeof (BqlCommand)
      }, true);
    MethodInfo method = attr.GetType().GetMethod("GetRecords", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
    if (method == (MethodInfo) null)
      return (PXCustomSelectorAttribute.CreateViewDelegate) null;
    System.Type type = (System.Type) null;
    ParameterInfo[] parameters = method.GetParameters();
    if (typeof (IEnumerable).IsAssignableFrom(method.ReturnType))
    {
      if (parameters.Length <= PXCustomSelectorAttribute.SelectDelegateMap.Length)
      {
        type = PXCustomSelectorAttribute.SelectDelegateMap[parameters.Length];
        if (parameters.Length != 0)
          type = type.MakeGenericType(((IEnumerable<ParameterInfo>) parameters).Select<ParameterInfo, System.Type>((Func<ParameterInfo, System.Type>) (p => p.ParameterType)).ToArray<System.Type>());
      }
    }
    else if (parameters.Length == 0)
      type = typeof (PXPrepareDelegate);
    if (type == (System.Type) null)
      return (PXCustomSelectorAttribute.CreateViewDelegate) null;
    ILGenerator ilGenerator = dynamicMethod.GetILGenerator();
    LocalBuilder localBuilder = ilGenerator.DeclareLocal(typeof (PXView));
    ilGenerator.Emit(OpCodes.Nop);
    ilGenerator.Emit(OpCodes.Ldarg_1);
    ilGenerator.Emit(OpCodes.Ldarg_2);
    ilGenerator.Emit(OpCodes.Ldarg_3);
    ilGenerator.Emit(OpCodes.Ldarg_0);
    ilGenerator.Emit(OpCodes.Castclass, attr.GetType());
    ilGenerator.Emit(OpCodes.Ldftn, method);
    ilGenerator.Emit(OpCodes.Newobj, type.GetConstructor(new System.Type[2]
    {
      typeof (object),
      typeof (IntPtr)
    }));
    ilGenerator.Emit(OpCodes.Newobj, typeof (PXView).GetConstructor(new System.Type[4]
    {
      typeof (PXGraph),
      typeof (bool),
      typeof (BqlCommand),
      typeof (Delegate)
    }));
    ilGenerator.Emit(OpCodes.Stloc, localBuilder.LocalIndex);
    ilGenerator.Emit(OpCodes.Ldloc, localBuilder.LocalIndex);
    ilGenerator.Emit(OpCodes.Ret);
    return (PXCustomSelectorAttribute.CreateViewDelegate) dynamicMethod.CreateDelegate(typeof (PXCustomSelectorAttribute.CreateViewDelegate));
  }

  /// <summary>
  /// The method executed when the attribute is copied to the cache level.
  /// </summary>
  /// <param name="sender">The cache object that has raised the event.</param>
  public override void CacheAttached(PXCache sender)
  {
    this._CacheType = sender.GetItemType();
    this._Graph = sender.Graph;
    this._BypassFieldVerifying = new PXEventSubscriberAttribute.ObjectRef<bool>();
    string hash = this.GetHash();
    lock (PXCustomSelectorAttribute._vlock)
    {
      if (!PXCustomSelectorAttribute.createView.ContainsKey(hash))
        PXCustomSelectorAttribute.createView.Add(hash, PXCustomSelectorAttribute.CreateDelegate(this));
    }
    this.populateFields(sender, true);
    this._ViewName = $"_{sender.GetItemType().Name}{this._FieldName}{this._ViewName}";
    PXView pxView;
    if (!sender.Graph.Views.TryGetValue(this._ViewName, out pxView) || pxView.BqlTarget != this.GetType())
    {
      lock (PXCustomSelectorAttribute._vlock)
        pxView = PXCustomSelectorAttribute.createView[hash](this, sender.Graph, !this._DirtyRead, this._Select);
      sender.Graph.Views[this._ViewName] = pxView;
      if (this._Filterable)
        this.CreateFilter(sender.Graph);
    }
    if (!this.IsSelfReferencing)
      this.EmitColumnForDescriptionField(sender);
    if (!(this._SubstituteKey != (System.Type) null))
      return;
    string lower = this._FieldName.ToLower();
    sender.FieldSelectingEvents[lower] += new PXFieldSelecting(((PXSelectorAttribute) this).SubstituteKeyFieldSelecting);
    sender.FieldUpdatingEvents[lower] += new PXFieldUpdating(((PXSelectorAttribute) this).SubstituteKeyFieldUpdating);
    if (string.Compare(this._SubstituteKey.Name, this._FieldName, StringComparison.OrdinalIgnoreCase) == 0)
      return;
    sender.CommandPreparingEvents[lower] += new PXCommandPreparing(((PXSelectorAttribute) this).SubstituteKeyCommandPreparing);
  }

  /// <exclude />
  protected sealed class FilteredView : PXView
  {
    private PXView _OuterView;
    private PXView _TemplateView;
    private PXCustomSelectorAttribute _attribute;

    public FilteredView(PXView outerView, PXView templateView)
      : base(templateView.Graph, templateView.IsReadOnly, templateView.BqlSelect)
    {
      this._OuterView = outerView;
      this._TemplateView = templateView;
    }

    public FilteredView(PXView outerView, PXView templateView, PXCustomSelectorAttribute attribute)
      : base(templateView.Graph, templateView.IsReadOnly, templateView.BqlSelect)
    {
      this._OuterView = outerView;
      this._TemplateView = templateView;
      this._attribute = attribute;
    }

    public override List<object> Select(
      object[] currents,
      object[] parameters,
      object[] searches,
      string[] sortcolumns,
      bool[] descendings,
      PXFilterRow[] filters,
      ref int startRow,
      int maximumRows,
      ref int totalRows)
    {
      if (parameters != null && parameters.Length != 0)
      {
        string[] parameterNames = this._TemplateView.GetParameterNames();
        int num;
        if (parameterNames.Length != 0 && !string.IsNullOrEmpty(parameterNames[parameterNames.Length - 1]) && (num = parameterNames[parameterNames.Length - 1].LastIndexOf('.')) != -1 && num + 1 < parameterNames[parameterNames.Length - 1].Length)
        {
          string str = parameterNames[parameterNames.Length - 1].Substring(num + 1);
          object returnValue = parameters[parameters.Length - 1];
          bool flag = true;
          if (this._attribute != null)
            flag = this._attribute._raiseFieldSelecting;
          try
          {
            if (flag)
            {
              if (this._attribute != null)
                this._attribute._raiseFieldSelecting = false;
              this.Cache.RaiseFieldSelecting(str, currents == null || currents.Length == 0 ? (object) null : currents[0], ref returnValue, false);
              returnValue = PXFieldState.UnwrapValue(returnValue);
            }
          }
          catch
          {
          }
          finally
          {
            if (this._attribute != null & flag)
              this._attribute._raiseFieldSelecting = true;
          }
          if (returnValue == null)
            returnValue = parameters[parameters.Length - 1];
          PXFilterRow pxFilterRow = new PXFilterRow(str, PXCondition.EQ, returnValue);
          if (filters == null || filters.Length == 0)
          {
            filters = new PXFilterRow[1]{ pxFilterRow };
          }
          else
          {
            filters = ((IEnumerable<PXFilterRow>) EnumerableExtensions.Append<PXFilterRow>(filters, pxFilterRow)).ToArray<PXFilterRow>();
            if (filters.Length > 2)
            {
              ++filters[0].OpenBrackets;
              ++filters[filters.Length - 2].CloseBrackets;
            }
            filters[filters.Length - 2].OrOperator = false;
          }
          Array.Resize<object>(ref parameters, parameters.Length - 1);
        }
      }
      return this._OuterView.Select(currents, parameters, searches, sortcolumns, descendings, filters, ref startRow, maximumRows, ref totalRows);
    }
  }

  /// <exclude />
  private delegate PXView CreateViewDelegate(
    PXCustomSelectorAttribute attr,
    PXGraph graph,
    bool IsReadOnly,
    BqlCommand select);
}
