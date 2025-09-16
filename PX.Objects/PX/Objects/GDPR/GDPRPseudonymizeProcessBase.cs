// Decompiled with JetBrains decompiler
// Type: PX.Objects.GDPR.GDPRPseudonymizeProcessBase
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.Process;
using PX.Data.UserRecords.RecentlyVisitedRecords;
using PX.DbServices.Model.Entities;
using PX.SM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

#nullable disable
namespace PX.Objects.GDPR;

public class GDPRPseudonymizeProcessBase : GDPRPersonalDataProcessBase
{
  protected virtual void PseudonymizeChilds(
    PXGraph processingGraph,
    Type childTable,
    IEnumerable<PXPersonalDataFieldAttribute> fields,
    IEnumerable<object> childs)
  {
    TableHeader tableStructure = PXDatabase.GetTableStructure(childTable.Name);
    foreach (object child in childs)
    {
      List<PXDataFieldParam> second = new List<PXDataFieldParam>();
      foreach (PXPersonalDataFieldAttribute field in fields)
      {
        object obfuscateValue = this.GetObfuscateValue(childTable, field);
        TableColumn columnByName = tableStructure.getColumnByName(((PXEventSubscriberAttribute) field).FieldName);
        second.Add((PXDataFieldParam) new PXDataFieldAssign(((PXEventSubscriberAttribute) field).FieldName, PXDbTypeConverter.SqlDbTypeToPXDbType(columnByName.Type), obfuscateValue));
      }
      second.Add((PXDataFieldParam) new PXDataFieldAssign("PseudonymizationStatus", (object) this.SetPseudonymizationStatus));
      List<PXDataFieldParam> pxDataFieldParamList = new List<PXDataFieldParam>();
      foreach (string key in (IEnumerable<string>) processingGraph.Caches[childTable].Keys)
        pxDataFieldParamList.Add((PXDataFieldParam) new PXDataFieldRestrict(key, processingGraph.Caches[childTable].GetValue(child, key)));
      PXDatabase.Update(childTable, EnumerableExtensions.Distinct<PXDataFieldParam, string>(this.InterruptPseudonimyzationHandler(processingGraph.Caches[childTable], pxDataFieldParamList).Union<PXDataFieldParam>((IEnumerable<PXDataFieldParam>) second), (Func<PXDataFieldParam, string>) (_ => _.Column.Name.ToLower())).Union<PXDataFieldParam>((IEnumerable<PXDataFieldParam>) pxDataFieldParamList).ToArray<PXDataFieldParam>());
      this.DeleteSearchIndex(processingGraph.Caches[childTable].GetValue(child, "NoteID") as Guid?);
    }
  }

  protected virtual List<PXDataFieldParam> InterruptPseudonimyzationHandler(
    PXCache cache,
    List<PXDataFieldParam> restricts)
  {
    List<PXDataFieldParam> pxDataFieldParamList1 = new List<PXDataFieldParam>();
    IEnumerable<Type> source = ((IEnumerable<Type>) cache.GetExtensionTypes()).Where<Type>((Func<Type, bool>) (_ => typeof (IPostPseudonymizable).IsAssignableFrom(_)));
    if (source == null || source.Count<Type>() == 0)
      return pxDataFieldParamList1;
    foreach (Type type in source)
    {
      MethodInfo method = type.GetMethod(nameof (InterruptPseudonimyzationHandler));
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

  protected virtual void WipeAudit(
    PXGraph processingGraph,
    Type childTable,
    IEnumerable<PXPersonalDataFieldAttribute> fields,
    IEnumerable<object> childs)
  {
    foreach (object child in childs)
    {
      string str1 = (string) null;
      foreach (string key in (IEnumerable<string>) processingGraph.Caches[childTable].Keys)
        str1 = str1 + processingGraph.Caches[childTable].GetValue(child, key).ToString() + PXAuditHelper.SEPARATOR.ToString();
      string str2 = str1.TrimEnd(PXAuditHelper.SEPARATOR);
      List<Tuple<long, long>> tupleList1 = new List<Tuple<long, long>>();
      foreach (PXDataRecord pxDataRecord in PXDatabase.SelectMulti<AuditHistory>(new PXDataField[4]
      {
        (PXDataField) new PXDataField<AuditHistory.batchID>(),
        (PXDataField) new PXDataField<AuditHistory.changeID>(),
        (PXDataField) new PXDataField<AuditHistory.modifiedFields>(),
        (PXDataField) new PXDataFieldValue<AuditHistory.combinedKey>((object) str2)
      }).Where<PXDataRecord>((Func<PXDataRecord, bool>) (_ => fields.Any<PXPersonalDataFieldAttribute>((Func<PXPersonalDataFieldAttribute, bool>) (field => _.GetString(2).Contains(((PXEventSubscriberAttribute) field).FieldName))))))
      {
        List<Tuple<long, long>> tupleList2 = tupleList1;
        long? int64 = pxDataRecord.GetInt64(0);
        long num1 = int64.Value;
        int64 = pxDataRecord.GetInt64(1);
        long num2 = int64.Value;
        Tuple<long, long> tuple = new Tuple<long, long>(num1, num2);
        tupleList2.Add(tuple);
      }
      foreach (Tuple<long, long> tuple in tupleList1)
        PXDatabase.Delete<AuditHistory>(new PXDataFieldRestrict[2]
        {
          (PXDataFieldRestrict) new PXDataFieldRestrict<AuditHistory.batchID>((object) tuple.Item1),
          (PXDataFieldRestrict) new PXDataFieldRestrict<AuditHistory.changeID>((object) tuple.Item2)
        });
      PXDatabase.Delete<VisitedRecord>(new PXDataFieldRestrict[1]
      {
        (PXDataFieldRestrict) new PXDataFieldRestrict<VisitedRecord.refNoteID>((object) (processingGraph.Caches[childTable].GetValue(child, "NoteID") as Guid?))
      });
    }
  }

  protected virtual object GetObfuscateValue(Type table, PXPersonalDataFieldAttribute field)
  {
    TableColumn columnByName = PXDatabase.GetTableStructure(table.Name).getColumnByName(((PXEventSubscriberAttribute) field).FieldName);
    if (!(columnByName.SystemType == typeof (string)))
      return columnByName.GetDefaultValue();
    string str = field.IsKey ? Guid.NewGuid().ToString() : "*****";
    return (object) str.Substring(0, Math.Min(columnByName.Size, str.Length));
  }

  protected virtual bool DeleteSearchIndex(Guid? topParentNoteID)
  {
    return PXDatabase.Delete<SearchIndex>(new PXDataFieldRestrict[1]
    {
      (PXDataFieldRestrict) new PXDataFieldRestrict<SearchIndex.noteID>((object) topParentNoteID)
    });
  }
}
