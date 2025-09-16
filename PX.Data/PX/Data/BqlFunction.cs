// Decompiled with JetBrains decompiler
// Type: PX.Data.BqlFunction
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.BQL;
using PX.Data.SQLTree;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Data;

/// <exclude />
public abstract class BqlFunction
{
  /// <exclude />
  public static bool getValue<Operand>(
    ref IBqlCreator op,
    PXCache cache,
    object item,
    List<object> pars,
    ref bool? result,
    out object value)
    where Operand : IBqlOperand
  {
    return BqlFunction.getValue(typeof (Operand), ref op, cache, item, pars, ref result, out value);
  }

  public static bool getValue(
    System.Type typeOfOperand,
    ref IBqlCreator op,
    PXCache cache,
    object item,
    List<object> pars,
    ref bool? result,
    out object value)
  {
    value = (object) null;
    if (typeof (IBqlField).IsAssignableFrom(typeOfOperand))
    {
      if (!(cache.GetItemType() == BqlCommand.GetItemType(typeOfOperand)) && !BqlCommand.GetItemType(typeOfOperand).IsAssignableFrom(cache.GetItemType()) && !cache.GetItemType().IsAssignableFrom(BqlCommand.GetItemType(typeOfOperand)))
        return false;
      if (item is BqlFormula.ItemContainer itemContainer)
        itemContainer.InvolvedFields.Add(typeOfOperand);
      value = cache.GetValue(BqlFormula.ItemContainer.Unwrap(item), typeOfOperand.Name);
    }
    else
    {
      if (op == null)
        op = op.createOperand(typeOfOperand);
      op.Verify(cache, item, pars, ref result, ref value);
    }
    return true;
  }

  protected bool GetOperandExpression<Operand>(
    ref SQLExpression exp,
    ref IBqlCreator op,
    PXGraph graph,
    BqlCommandInfo info,
    BqlCommand.Selection selection)
  {
    bool operandExpression = true;
    if (info.Fields is BqlCommand.EqualityList fields)
      fields.NonStrict = true;
    if (typeof (IBqlCreator).IsAssignableFrom(typeof (Operand)))
    {
      if (op == null)
        op = op.createOperand<Operand>();
      operandExpression &= op.AppendExpression(ref exp, graph, info, selection);
    }
    else
    {
      if (info.BuildExpression)
        exp = BqlCommand.GetSingleExpression(typeof (Operand), graph, info.Tables, selection, BqlCommand.FieldPlace.Condition);
      info.Fields?.Add(typeof (Operand));
    }
    return operandExpression;
  }

  /// <exclude />
  protected bool tryCreateOperand<Operand>(ref IBqlCreator op)
  {
    if (typeof (IBqlField).IsAssignableFrom(typeof (Operand)))
      return false;
    if (op == null)
      op = op.createOperand<Operand>();
    return true;
  }

  /// <exclude />
  protected TypeCode getTypeCodeForOperand<Operand>(PXGraph graph)
  {
    return this.getTypeCodeForOperand(typeof (Operand), graph);
  }

  /// <exclude />
  protected TypeCode getTypeCodeForOperand(System.Type tOp, PXGraph graph)
  {
    if (BqlType.IsStronglyTyped(tOp))
    {
      System.Type correspondingDotNetType = BqlType.GetCorrespondingDotNetType(tOp);
      if (correspondingDotNetType != (System.Type) null)
        return System.Type.GetTypeCode(correspondingDotNetType);
    }
    System.Type itemType = BqlCommand.GetItemType(tOp);
    System.Type baseType = tOp.BaseType;
    if ((System.Type) null == itemType && baseType != (System.Type) null && baseType.IsGenericType)
    {
      System.Type[] genericArguments = baseType.GetGenericArguments();
      System.Type type = genericArguments.Length != 0 ? genericArguments[0] : (System.Type) null;
      return typeof (IBqlField).IsAssignableFrom(type) ? this.getTypeCodeForOperand(type, graph) : System.Type.GetTypeCode(type);
    }
    return (System.Type) null == itemType || !typeof (IBqlTable).IsAssignableFrom(itemType) ? TypeCode.Empty : System.Type.GetTypeCode(graph.Caches[itemType].GetFieldType(tOp.Name));
  }
}
