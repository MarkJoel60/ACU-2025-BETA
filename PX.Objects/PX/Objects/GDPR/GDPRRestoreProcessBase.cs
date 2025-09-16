// Decompiled with JetBrains decompiler
// Type: PX.Objects.GDPR.GDPRRestoreProcessBase
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

#nullable disable
namespace PX.Objects.GDPR;

public class GDPRRestoreProcessBase : GDPRPersonalDataProcessBase
{
  protected virtual void RestoreObfuscatedEntries(
    PXGraph processingGraph,
    Type childTable,
    IEnumerable<PXPersonalDataFieldAttribute> fields,
    IEnumerable<object> childs)
  {
    foreach (object child in childs)
    {
      Guid? nullable = processingGraph.Caches[childTable].GetValue(child, "NoteID") as Guid?;
      List<PXDataFieldParam> second1 = new List<PXDataFieldParam>();
      foreach (PXDataRecord pxDataRecord in PXDatabase.SelectMulti<SMPersonalData>(new PXDataField[4]
      {
        (PXDataField) new PXDataField<SMPersonalData.field>(),
        (PXDataField) new PXDataField<SMPersonalData.value>(),
        (PXDataField) new PXDataFieldValue<SMPersonalData.table>((object) childTable.FullName),
        (PXDataField) new PXDataFieldValue<SMPersonalData.entityID>((object) nullable)
      }))
        second1.Add((PXDataFieldParam) new PXDataFieldAssign(pxDataRecord.GetString(0), (object) pxDataRecord.GetString(1)));
      List<PXDataFieldParam> second2 = new List<PXDataFieldParam>();
      foreach (PXPersonalDataFieldAttribute field in fields)
      {
        IEnumerable<PXDefaultAttribute> attributesOfType = processingGraph.Caches[childTable].GetAttributesOfType<PXDefaultAttribute>((object) null, ((PXEventSubscriberAttribute) field).FieldName);
        PXDefaultAttribute defaultAttribute = attributesOfType != null ? attributesOfType.FirstOrDefault<PXDefaultAttribute>() : (PXDefaultAttribute) null;
        object obj = field.DefaultValue ?? defaultAttribute?.Constant;
        second2.Add((PXDataFieldParam) new PXDataFieldAssign(((PXEventSubscriberAttribute) field).FieldName, obj));
      }
      second1.Add((PXDataFieldParam) new PXDataFieldAssign("PseudonymizationStatus", (object) this.SetPseudonymizationStatus));
      List<PXDataFieldParam> pxDataFieldParamList = new List<PXDataFieldParam>();
      foreach (string key in (IEnumerable<string>) processingGraph.Caches[childTable].Keys)
        pxDataFieldParamList.Add((PXDataFieldParam) new PXDataFieldRestrict(key, processingGraph.Caches[childTable].GetValue(child, key)));
      if (PXDatabase.Update(childTable, EnumerableExtensions.Distinct<PXDataFieldParam, string>(this.InterruptRestorationHandler(processingGraph.Caches[childTable], pxDataFieldParamList).Union<PXDataFieldParam>((IEnumerable<PXDataFieldParam>) second1).Union<PXDataFieldParam>((IEnumerable<PXDataFieldParam>) second2), (Func<PXDataFieldParam, string>) (_ => _.Column.Name.ToLower())).Union<PXDataFieldParam>((IEnumerable<PXDataFieldParam>) pxDataFieldParamList).ToArray<PXDataFieldParam>()))
        PXDatabase.Delete<SMPersonalData>(new PXDataFieldRestrict[2]
        {
          (PXDataFieldRestrict) new PXDataFieldRestrict<SMPersonalData.table>((object) childTable.FullName),
          (PXDataFieldRestrict) new PXDataFieldRestrict<SMPersonalData.entityID>((object) nullable)
        });
    }
  }

  protected virtual List<PXDataFieldParam> InterruptRestorationHandler(
    PXCache cache,
    List<PXDataFieldParam> restricts)
  {
    List<PXDataFieldParam> pxDataFieldParamList1 = new List<PXDataFieldParam>();
    IEnumerable<Type> source = ((IEnumerable<Type>) cache.GetExtensionTypes()).Where<Type>((Func<Type, bool>) (_ => typeof (IPostRestorable).IsAssignableFrom(_)));
    if (source == null || source.Count<Type>() == 0)
      return pxDataFieldParamList1;
    foreach (Type type in source)
    {
      MethodInfo method = type.GetMethod(nameof (InterruptRestorationHandler));
      if (method != (MethodInfo) null)
      {
        List<PXDataFieldParam> pxDataFieldParamList2 = pxDataFieldParamList1;
        object obj;
        if ((object) method == null)
        {
          obj = (object) null;
        }
        else
        {
          // ISSUE: explicit non-virtual call
          obj = __nonvirtual (method.Invoke(Activator.CreateInstance(type), new object[1]
          {
            (object) restricts
          }));
        }
        if (!(obj is List<PXDataFieldParam> collection))
          collection = new List<PXDataFieldParam>();
        pxDataFieldParamList2.AddRange((IEnumerable<PXDataFieldParam>) collection);
      }
    }
    return pxDataFieldParamList1;
  }
}
