// Decompiled with JetBrains decompiler
// Type: PX.Data.PXViewExtensions
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.SQLTree;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Data;

internal static class PXViewExtensions
{
  public static IEnumerable<PXDataValue> PrepareParameters(
    this PXView pxView,
    BqlCommand command,
    IList<Tuple<string, string>> paramNames)
  {
    return pxView.PrepareParameters(command, paramNames, (Func<string, string, string, bool>) ((_1, _2, _3) => false));
  }

  public static IEnumerable<PXDataValue> PrepareParameters(
    this PXView pxView,
    BqlCommand command,
    IList<Tuple<string, string>> paramNames,
    Func<string, string, string, bool> shouldUseParamFieldName)
  {
    IBqlParameter[] parameters1 = command.GetParameters();
    IBqlParameter[] parameters2 = command.GetParameters();
    object[] parameters3 = pxView.PrepareParametersInternal((object[]) null, (object[]) null, parameters2);
    PXGraph graph = pxView.Graph;
    PXViewExtensions.CheckParameters(parameters3, parameters1, graph, paramNames, shouldUseParamFieldName);
    List<PXDataValue> pxDataValueList = new List<PXDataValue>();
    if (parameters3 != null)
    {
      System.Type firstTable = command.GetFirstTable();
      PXCache cach1 = graph.Caches[firstTable];
      for (int index = 0; index < parameters1.Length && index < parameters3.Length; ++index)
      {
        PXCommandPreparingEventArgs.FieldDescription description = (PXCommandPreparingEventArgs.FieldDescription) null;
        System.Type restrition = (System.Type) null;
        if (!parameters1[index].IsArgument)
        {
          if (parameters1[index] is FieldNameParam)
          {
            if (parameters3[index] is PXFieldName pxFieldName)
            {
              pxDataValueList.Add((PXDataValue) pxFieldName);
              continue;
            }
            pxDataValueList.Add((PXDataValue) new PXFieldName((string) parameters3[index]));
            continue;
          }
          System.Type referencedType = parameters1[index].GetReferencedType();
          if (parameters1[index] is ListOperand listOperand)
          {
            pxDataValueList.AddRange(((IEnumerable<string>) listOperand.GetParameters(graph)).Select<string, PXDataValue>((Func<string, PXDataValue>) (c => new PXDataValue(PXDbType.NVarChar, new int?(c.Length), (object) c))));
            continue;
          }
          if (!referencedType.IsNested || BqlCommand.GetItemType(referencedType) == firstTable)
          {
            cach1.RaiseCommandPreparing(referencedType.Name, (object) null, parameters3[index], PXDBOperation.ReadOnly, (System.Type) null, out description);
            if (parameters1[index].MaskedType != (System.Type) null)
              restrition = GroupHelper.GetReferencedType(cach1, referencedType.Name);
          }
          else
          {
            PXCache cach2 = graph.Caches[BqlCommand.GetItemType(referencedType)];
            cach2.RaiseCommandPreparing(referencedType.Name, (object) null, parameters3[index], PXDBOperation.ReadOnly, (System.Type) null, out description);
            if (parameters1[index].MaskedType != (System.Type) null)
              restrition = GroupHelper.GetReferencedType(cach2, referencedType.Name);
          }
        }
        if (description == null || description.DataValue == null && !parameters1[index].NullAllowed)
          throw new InvalidOperationException("Cannot prepare parameters");
        if (parameters1[index].MaskedType == (System.Type) null)
          pxDataValueList.Add(new PXDataValue(description.DataType, description.DataLength, description.DataValue));
        else if (parameters1[index].MaskedType == typeof (Array))
          pxDataValueList.Add(new PXDataValue(PXDbType.DirectExpression, description.DataValue));
        else if (graph.Caches[parameters1[index].MaskedType].Fields.Contains("GroupMask"))
        {
          byte[] dataValue = description.DataValue as byte[];
          foreach (GroupHelper.ParamsPair paramsPair in GroupHelper.GetParams(restrition, parameters1[index].MaskedType, dataValue))
          {
            pxDataValueList.Add(new PXDataValue(PXDbType.Int, new int?(4), (object) paramsPair.First));
            pxDataValueList.Add(new PXDataValue(PXDbType.Int, new int?(4), (object) paramsPair.Second));
          }
        }
      }
    }
    return (IEnumerable<PXDataValue>) pxDataValueList;
  }

  private static void CheckParameters(
    object[] parameters,
    IBqlParameter[] cmdpars,
    PXGraph graph,
    IList<Tuple<string, string>> paramNames,
    Func<string, string, string, bool> shouldUseParamFieldName)
  {
    int index1 = 0;
    if (parameters == null && cmdpars.Length != 0)
      throw new InvalidOperationException("Cannot prepare parameters");
    if (parameters == null)
      return;
    if (cmdpars.Length > parameters.Length)
      throw new InvalidOperationException("Cannot prepare parameters");
    for (int index2 = 0; index2 < parameters.Length && index2 < cmdpars.Length; ++index2)
    {
      if (!cmdpars[index2].NullAllowed && parameters[index2] == null)
      {
        if (cmdpars[index2] is ListOperand cmdpar && parameters[index2] == null)
          parameters[index2] = (object) cmdpar.GetParameters(graph);
        if (cmdpars[index2] is FieldNameParam && parameters[index2] == null)
        {
          parameters[index2] = PXViewExtensions.GetParamValueForFieldNameParam(paramNames[index1], graph, shouldUseParamFieldName);
          ++index1;
        }
        if (parameters[index2] == null)
        {
          System.Type referencedType = cmdpars[index2].GetReferencedType();
          if (typeof (IBqlField).IsAssignableFrom(referencedType))
          {
            PXCache cach = graph.Caches[referencedType.DeclaringType];
            PXCommandPreparingEventArgs.FieldDescription description;
            cach.RaiseCommandPreparing(referencedType.Name, (object) null, (object) null, PXDBOperation.Select, (System.Type) null, out description);
            if (description?.Expr == null)
            {
              object instance = cach.CreateInstance();
              object obj = cach.GetValue(instance, referencedType.Name);
              if (obj != null)
                parameters[index2] = obj;
            }
          }
        }
        if (parameters[index2] == null)
          throw new InvalidOperationException("Cannot prepare parameters");
      }
    }
  }

  private static object GetParamValueForFieldNameParam(
    Tuple<string, string> paramName,
    PXGraph graph,
    Func<string, string, string, bool> shouldUseParamFieldName)
  {
    string str1 = paramName.Item1;
    SQLExpression sqlExpression = (SQLExpression) null;
    char[] chArray = new char[1]{ '_' };
    string[] strArray = str1.Split(chArray);
    if (strArray.Length == 2)
    {
      string str2 = strArray[0];
      string key = strArray[1];
      PXCache cach = graph.Caches[key];
      PXCommandPreparingEventArgs.FieldDescription description;
      cach.RaiseCommandPreparing(paramName.Item2, (object) null, (object) null, PXDBOperation.ReadOnly, cach.GetItemType(), out description);
      if (description?.Expr != null)
      {
        sqlExpression = description.Expr;
        sqlExpression.substituteColumnAliases(new Dictionary<string, string>()
        {
          {
            key,
            paramName.Item1
          }
        });
        if (sqlExpression is Column column && shouldUseParamFieldName(paramName.Item2, column.Name, paramName.Item1))
          sqlExpression = (SQLExpression) new Column(paramName.Item2, column.Table().AliasOrName(), column.GetDBType());
      }
    }
    string tableName = paramName.Item1;
    return (object) new PXFieldName($"{tableName}.{paramName.Item2}", sqlExpression ?? (SQLExpression) new Column(paramName.Item2, tableName));
  }
}
