// Decompiled with JetBrains decompiler
// Type: PX.Data.PXUpdate
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Data;

internal static class PXUpdate
{
  internal static PXDataValue[] prepareParameters(
    PXGraph graph,
    IBqlUpdate command,
    params object[] parameters)
  {
    List<PXDataValue> pxDataValueList = new List<PXDataValue>();
    IBqlParameter[] parameters1 = command.GetParameters();
    int index1 = 0;
    for (int index2 = 0; index2 < parameters1.Length; ++index2)
    {
      object newValue = (object) null;
      if (parameters1[index2].IsVisible && parameters != null && index1 < parameters.Length)
      {
        newValue = parameters[index1];
        ++index1;
      }
      if (newValue == null)
      {
        if (parameters1[index2].HasDefault)
        {
          System.Type referencedType = parameters1[index2].GetReferencedType();
          if (referencedType.IsNested)
          {
            System.Type itemType = BqlCommand.GetItemType(referencedType);
            PXCache cach = graph.Caches[itemType];
            if (newValue == null && cach.Current != null)
              newValue = cach.GetValue(cach.Current, referencedType.Name);
            if (newValue == null && parameters1[index2].TryDefault && cach.RaiseFieldDefaulting(referencedType.Name, (object) null, out newValue))
              cach.RaiseFieldUpdating(referencedType.Name, (object) null, ref newValue);
            if (parameters1[index2].MaskedType != (System.Type) null && !parameters1[index2].IsArgument)
            {
              object current = cach.Current;
              newValue = GroupHelper.GetReferencedValue(cach, current, referencedType.Name, newValue, false);
            }
          }
        }
      }
      else if (parameters1[index2].HasDefault)
      {
        System.Type referencedType = parameters1[index2].GetReferencedType();
        if (referencedType.IsNested)
        {
          System.Type itemType = BqlCommand.GetItemType(referencedType);
          PXCache cach = graph.Caches[itemType];
          object current = cach.Current;
          cach.RaiseFieldUpdating(referencedType.Name, current, ref newValue);
          if (parameters1[index2].MaskedType != (System.Type) null && !parameters1[index2].IsArgument)
            newValue = GroupHelper.GetReferencedValue(cach, current, referencedType.Name, newValue, false);
        }
      }
      PXCommandPreparingEventArgs.FieldDescription description = (PXCommandPreparingEventArgs.FieldDescription) null;
      System.Type restrition = (System.Type) null;
      if (!parameters1[index2].IsArgument)
      {
        System.Type referencedType = parameters1[index2].GetReferencedType();
        if (referencedType.IsNested)
        {
          PXCache cach = graph.Caches[BqlCommand.GetItemType(referencedType)];
          cach.RaiseCommandPreparing(referencedType.Name, (object) null, parameters[index2], PXDBOperation.Select, (System.Type) null, out description);
          if (parameters1[index2].MaskedType != (System.Type) null)
            restrition = GroupHelper.GetReferencedType(cach, referencedType.Name);
        }
      }
      if (parameters1[index2].MaskedType == (System.Type) null)
        pxDataValueList.Add(new PXDataValue(description.DataType, description.DataLength, description.DataValue));
      else if (parameters1[index2].MaskedType == typeof (Array))
        pxDataValueList.Add(new PXDataValue(PXDbType.DirectExpression, description.DataValue));
      else if (graph.Caches[parameters1[index2].MaskedType].Fields.Contains("GroupMask"))
      {
        byte[] dataValue = description.DataValue as byte[];
        foreach (GroupHelper.ParamsPair paramsPair in GroupHelper.GetParams(restrition, parameters1[index2].MaskedType, dataValue))
        {
          pxDataValueList.Add(new PXDataValue(PXDbType.Int, new int?(4), (object) paramsPair.First));
          pxDataValueList.Add(new PXDataValue(PXDbType.Int, new int?(4), (object) paramsPair.Second));
        }
      }
    }
    return pxDataValueList.ToArray();
  }
}
