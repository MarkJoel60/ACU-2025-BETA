// Decompiled with JetBrains decompiler
// Type: PX.Data.BqlFormula
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.SQLTree;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Data;

/// <exclude />
public abstract class BqlFormula
{
  private Dictionary<System.Type, IBqlCreator> _operands;

  protected object Calculate<Operand>(PXCache cache, object item) where Operand : IBqlOperand
  {
    bool? result = new bool?();
    this._operands = this._operands ?? new Dictionary<System.Type, IBqlCreator>();
    Dictionary<System.Type, IBqlCreator> operands = this._operands;
    IBqlCreator op = (IBqlCreator) null;
    bool flag = typeof (IBqlField).IsAssignableFrom(typeof (Operand));
    lock (((ICollection) operands).SyncRoot)
    {
      if (!flag)
      {
        if (!operands.TryGetValue(typeof (Operand), out op))
          operands[typeof (Operand)] = op = op.createOperand<Operand>();
      }
    }
    object obj;
    return !BqlFunction.getValue<Operand>(ref op, cache, item, flag ? (List<object>) null : BqlFormula.EvaluateParameters(cache, item, op), ref result, out obj) ? (object) null : obj;
  }

  private static List<object> EvaluateParameters(PXCache cache, object item, IBqlCreator formula)
  {
    List<IBqlParameter> bqlParameterList = new List<IBqlParameter>();
    List<object> parameters = new List<object>();
    SQLExpression sqlExpression = SQLExpression.None();
    IBqlCreator bqlCreator = formula;
    ref SQLExpression local = ref sqlExpression;
    BqlCommandInfo info = new BqlCommandInfo(false);
    info.Parameters = bqlParameterList;
    info.BuildExpression = false;
    BqlCommand.Selection selection = new BqlCommand.Selection();
    bqlCreator.AppendExpression(ref local, (PXGraph) null, info, selection);
    foreach (IBqlParameter bqlParameter in bqlParameterList)
    {
      object newValue = (object) null;
      if (bqlParameter.HasDefault)
      {
        System.Type referencedType = bqlParameter.GetReferencedType();
        if (referencedType.IsNested)
        {
          System.Type itemType = BqlCommand.GetItemType(referencedType);
          PXCache cach = cache.Graph.Caches[itemType];
          bool flag = false;
          if (item != null && (item.GetType() == itemType || item.GetType().IsSubclassOf(itemType)))
          {
            newValue = cache.GetValue(item, referencedType.Name);
            flag = true;
          }
          if (!flag && cach.Current != null)
            newValue = cach.GetValue(cach.Current, referencedType.Name);
          if (newValue == null && bqlParameter.TryDefault && cach.RaiseFieldDefaulting(referencedType.Name, (object) null, out newValue))
            cach.RaiseFieldUpdating(referencedType.Name, (object) null, ref newValue);
        }
      }
      parameters.Add(newValue);
    }
    return parameters;
  }

  public static void Verify(
    PXCache cache,
    object item,
    IBqlCreator formula,
    ref bool? result,
    ref object value)
  {
    object obj = BqlFormula.ItemContainer.Unwrap(item);
    formula.Verify(cache, item is BqlFormula.ItemContainer ? item : cache.CreateCopy(obj), BqlFormula.EvaluateParameters(cache, obj, formula), ref result, ref value);
  }

  public static bool IsContextualFormula(PXCache cache, System.Type Condition)
  {
    return ((IEnumerable<System.Type>) BqlCommand.Decompose(Condition)).Any<System.Type>((Func<System.Type, bool>) (t => typeof (IBqlField).IsAssignableFrom(t) && BqlCommand.GetItemType(t).IsAssignableFrom(cache.GetItemType())));
  }

  protected bool GetOperandExpression<TOperand>(
    ref SQLExpression exp,
    ref IBqlCreator operand,
    PXGraph graph,
    BqlCommandInfo info,
    BqlCommand.Selection selection)
  {
    bool operandExpression = true;
    if (typeof (IBqlField).IsAssignableFrom(typeof (TOperand)))
    {
      info.Fields?.Add(typeof (TOperand));
    }
    else
    {
      if (operand == null)
        operand = operand.createOperand<TOperand>();
      operandExpression &= operand.AppendExpression(ref exp, graph, info, selection);
    }
    return operandExpression;
  }

  public class ItemContainer
  {
    private readonly HashSet<System.Type> _involvedFields = new HashSet<System.Type>();

    public object Item { get; }

    public ISet<System.Type> InvolvedFields => (ISet<System.Type>) this._involvedFields;

    public bool IsExternalCall { get; }

    public System.Type DependentField { get; }

    public object PendingValue { get; }

    public ItemContainer(object item)
    {
      this.Item = item;
      this.IsExternalCall = true;
      this.DependentField = (System.Type) null;
    }

    public ItemContainer(
      object item,
      System.Type dependentField,
      bool isExternalCall,
      object pendingValue = null)
      : this(item)
    {
      this.IsExternalCall = isExternalCall;
      this.DependentField = dependentField;
      this.PendingValue = pendingValue;
    }

    public static object Unwrap(object src)
    {
      return !(src is BqlFormula.ItemContainer itemContainer) ? src : itemContainer.Item;
    }
  }
}
