// Decompiled with JetBrains decompiler
// Type: PX.Data.Maintenance.KeyValueExtHelper
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.SQLTree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

#nullable disable
namespace PX.Data.Maintenance;

public class KeyValueExtHelper
{
  public static void CopyKeyValueExtensions<T>(
    Func<string, string> getSourceField,
    Func<TypeCode, Attribute, string, string, string> getValueField,
    bool defaultChanged)
    where T : Attribute
  {
    System.Type type1 = typeof (IBqlTable);
    List<System.Type> typeList = new List<System.Type>();
    foreach (Assembly a in new List<Assembly>((IEnumerable<Assembly>) AppDomain.CurrentDomain.GetAssemblies()))
    {
      if (PXSubstManager.IsSuitableTypeExportAssembly(a, false))
      {
        try
        {
          System.Type[] typeArray = (System.Type[]) null;
          try
          {
            if (!a.IsDynamic)
              typeArray = a.GetExportedTypes();
          }
          catch (ReflectionTypeLoadException ex)
          {
            typeArray = ex.Types;
          }
          if (typeArray != null)
          {
            foreach (System.Type c in typeArray)
            {
              if (!(c == (System.Type) null) && c.IsClass && !c.IsAbstract && !c.IsNotPublic && type1.IsAssignableFrom(c) && !typeList.Contains(c))
                typeList.Add(c);
            }
          }
        }
        catch
        {
        }
      }
    }
    HashSet<System.Type> typeSet = new HashSet<System.Type>();
    HashSet<PropertyInfo> propertyInfoSet = new HashSet<PropertyInfo>();
    ISqlDialect sqlDialect = PXDatabase.Provider.SqlDialect;
    PXDatabaseProviderBase provider = PXDatabase.Provider as PXDatabaseProviderBase;
    foreach (System.Type type2 in typeList)
    {
      if (!typeSet.Contains(type2) && PXDatabase.Provider.SchemaCache.GetTableHeader(type2.Name) != null)
      {
        List<System.Type> extensions = PXCache._GetExtensions(type2, false, out Dictionary<string, string> _);
        foreach (PXCache.DACFieldDescriptor property in PXCache._GetProperties(type2, extensions, out Dictionary<string, List<PropertyInfo>> _, out int _))
        {
          PropertyInfo prop = property.Property;
          if (!(prop == (PropertyInfo) null))
          {
            T[] customAttributesEx = (T[]) prop.GetCustomAttributesEx(typeof (T), false);
            if (customAttributesEx.Length != 0 && (prop.DeclaringType == type2 || extensions != null && extensions.Any<System.Type>((Func<System.Type, bool>) (_ => _ == prop.DeclaringType))) && !propertyInfoSet.Contains(prop))
            {
              typeSet.Add(type2);
              propertyInfoSet.Add(prop);
              string name1 = type2.Name;
              string name2 = prop.Name;
              string str1 = getSourceField(prop.Name);
              string str2 = getValueField(System.Type.GetTypeCode(prop.PropertyType), (Attribute) customAttributesEx[0], name1, name2);
              if (!string.IsNullOrEmpty(str1) && !string.IsNullOrEmpty(str2))
              {
                string kvExtTableName = sqlDialect.GetKvExtTableName(type2.Name);
                SQLExpression r = SQLExpression.None();
                OrderSegment segment = (OrderSegment) null;
                if (provider != null)
                {
                  companySetting settings;
                  int companyId = provider.getCompanyID(name1, out settings);
                  int[] selectables;
                  if (settings != null && settings.Flag != companySetting.companyFlag.Separate && provider.tryGetSelectableCompanies(companyId, out selectables))
                  {
                    SQLExpression sqlExpression = SQLExpression.None();
                    foreach (int v in selectables)
                      sqlExpression = sqlExpression.Seq((SQLExpression) new SQLConst((object) v));
                    SQLExpression exp = sqlExpression.Seq((SQLExpression) new SQLConst((object) companyId));
                    Column column = new Column("CompanyID", kvExtTableName);
                    r = column.In(exp);
                    segment = new OrderSegment(column.Duplicate(), false);
                  }
                  else
                    r = provider.GetRestrictionExpression(kvExtTableName, kvExtTableName, true);
                }
                Query q = new Query().Field((SQLExpression) new Column(str2, kvExtTableName)).From((Table) new SimpleTable(kvExtTableName)).Where(SQLExpressionExt.EQ(new Column("RecordID", kvExtTableName), (SQLExpression) new Column("NoteID", type2.Name)).And(SQLExpressionExt.EQ(new Column("FieldName", kvExtTableName), (SQLExpression) new SQLConst((object) str1))).And(r)).Limit(1);
                if (segment != null)
                  q.AddOrderSegment(segment);
                Dictionary<Guid, string> dictionary = new Dictionary<Guid, string>();
                foreach (PXDataRecord pxDataRecord in PXDatabase.SelectMulti(type2, new PXDataField("NoteID"), new PXDataField((SQLExpression) new SubQuery(q))))
                {
                  string str3 = pxDataRecord.GetString(1);
                  if (!string.IsNullOrEmpty(str3))
                  {
                    Guid key = pxDataRecord.GetGuid(0).Value;
                    if (!dictionary.ContainsKey(key))
                      dictionary[key] = str3;
                  }
                }
                if (defaultChanged)
                {
                  foreach (KeyValuePair<Guid, string> keyValuePair in dictionary)
                    PXDatabase.Update(type2, (PXDataFieldParam) new PXDataFieldAssign(name2, PXDbType.NVarChar, (object) keyValuePair.Value), (PXDataFieldParam) new PXDataFieldRestrict("NoteID", PXDbType.UniqueIdentifier, new int?(16 /*0x10*/), (object) keyValuePair.Key));
                }
                foreach (PXDataRecord pxDataRecord in PXDatabase.SelectMulti(type2, new PXDataField("NoteID"), new PXDataField(name2)))
                {
                  Guid? guid = pxDataRecord.GetGuid(0);
                  string str4 = pxDataRecord.GetString(1);
                  if (guid.HasValue && !dictionary.ContainsKey(guid.Value))
                  {
                    if (!string.IsNullOrEmpty(str4))
                    {
                      try
                      {
                        PXDataFieldAssign pxDataFieldAssign1 = new PXDataFieldAssign("RecordID", PXDbType.UniqueIdentifier, new int?(16 /*0x10*/), (object) guid.Value);
                        pxDataFieldAssign1.Storage = StorageBehavior.KeyValueKey;
                        PXDataFieldAssign pxDataFieldAssign2 = new PXDataFieldAssign(str1, PXDbType.NVarChar, (object) str4);
                        pxDataFieldAssign2.Storage = string.Equals(str2, "ValueString", StringComparison.OrdinalIgnoreCase) ? StorageBehavior.KeyValueString : StorageBehavior.KeyValueText;
                        PXDatabase.Provider.Insert(type2, pxDataFieldAssign1, pxDataFieldAssign2);
                      }
                      catch (PXLockViolationException ex)
                      {
                      }
                    }
                  }
                }
              }
            }
          }
        }
      }
    }
  }
}
