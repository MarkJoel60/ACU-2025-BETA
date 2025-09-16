// Decompiled with JetBrains decompiler
// Type: PX.Data.ReferentialIntegrity.SelectorToReferenceConverter
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

#nullable disable
namespace PX.Data.ReferentialIntegrity;

internal class SelectorToReferenceConverter
{
  private readonly ILogger _logger;

  public SelectorToReferenceConverter(ILogger logger) => this._logger = logger;

  public Reference CreateReference(PXSelectorAttribute selector, System.Type currentTable)
  {
    SelectorToReferenceConverter.SelectorInfo selector1 = new SelectorToReferenceConverter.SelectorInfo(selector, currentTable);
    return SelectorToReferenceConverter.NeedToGenerateReferenceFromSelector(selector1) ? this.TransformToReference(selector1) : (Reference) null;
  }

  private static bool NeedToGenerateReferenceFromSelector(
    SelectorToReferenceConverter.SelectorInfo selector)
  {
    System.Type onTable = selector.OnTable;
    System.Type firstTable = selector.Select.GetFirstTable();
    if (Reference.GetReferenceKeys(selector.Select, firstTable, onTable).Length == 0 || !onTable.TableSupportsReferences() || !firstTable.TableSupportsReferences())
      return false;
    int num1 = selector.ExcludeFromReferenceGeneratingProcess ? 1 : 0;
    int num2;
    if (selector.OnField != (System.Type) null)
    {
      PropertyInfo property = onTable.GetProperty(selector.OnField.Name, BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Public);
      System.Type type;
      if ((object) property == null)
      {
        type = (System.Type) null;
      }
      else
      {
        IEnumerable<PXDBDefaultAttribute> customAttributes = property.GetCustomAttributes<PXDBDefaultAttribute>();
        type = customAttributes != null ? customAttributes.FirstOrDefault<PXDBDefaultAttribute>()?.OriginSourceType : (System.Type) null;
      }
      System.Type refField = selector.RefField;
      num2 = type == refField ? 1 : 0;
    }
    else
      num2 = 0;
    bool flag = num2 != 0;
    return num1 == 0 && !flag;
  }

  private Reference TransformToReference(SelectorToReferenceConverter.SelectorInfo selector)
  {
    try
    {
      System.Type cacheExtensionType = selector.CacheExtensionType;
      System.Type nestedType = (object) cacheExtensionType != null ? cacheExtensionType.GetNestedType(selector.OnField.Name, BindingFlags.IgnoreCase | BindingFlags.Public, true) : (System.Type) null;
      if ((object) nestedType == null)
        nestedType = selector.OnTable.GetNestedType(selector.OnField.Name, BindingFlags.IgnoreCase | BindingFlags.Public, true);
      if (!(nestedType == (System.Type) null))
        return Reference.FromParentSelect(selector.Select, ReferenceOrigin.SelectorAttribute, ReferenceBehavior.NoAction, selector.OnTable);
      this._logger.Debug<SelectorToReferenceConverter.SelectorInfo, System.Type>("Cannot create a reference for PXSelectorAttribute ({SelectorInfo}, {BqlTable}) because there is no corresponding field in the DAC", selector, selector.OnTable);
      return (Reference) null;
    }
    catch (Exception ex)
    {
      this._logger.Debug<SelectorToReferenceConverter.SelectorInfo, System.Type>(ex, "Cannot create a reference for PXSelectorAttribute ({SelectorInfo}, {BqlTable})", selector, selector.OnTable);
      return (Reference) null;
    }
  }

  private struct SelectorInfo
  {
    private static System.Type GetField(System.Type table, string name)
    {
      return (object) table == null ? (System.Type) null : table.GetNestedType(name, BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Public, true);
    }

    private static BqlCommand CreateSelect(BqlCommand select, System.Type refField, System.Type onField)
    {
      return select.WhereAnd(typeof (Where<,>).MakeGenericType(refField, typeof (Equal<>).MakeGenericType(typeof (Required<>).MakeGenericType(onField))));
    }

    private static bool RequiresSelect(BqlCommand select, System.Type refField, System.Type onField)
    {
      return (object) refField != null && (object) onField != null && !(refField == onField) && !select.GetParameterPairs().Any<KeyValuePair<System.Type, System.Type>>((Func<KeyValuePair<System.Type, System.Type>, bool>) (pair =>
      {
        if (pair.Key == refField && pair.Value == onField)
          return true;
        return pair.Key == onField && pair.Value == refField;
      }));
    }

    public SelectorInfo(PXSelectorAttribute selector, System.Type table)
    {
      this.ExcludeFromReferenceGeneratingProcess = selector.ExcludeFromReferenceGeneratingProcess;
      this.CacheExtensionType = selector.CacheExtensionType;
      this.RefField = selector.Field;
      System.Type field = SelectorToReferenceConverter.SelectorInfo.GetField(table, selector.FieldName);
      if ((object) field == null)
        field = SelectorToReferenceConverter.SelectorInfo.GetField(this.CacheExtensionType, selector.FieldName);
      this.OnField = field;
      this.OnTable = table;
      BqlCommand select = selector.GetSelect();
      this.Select = SelectorToReferenceConverter.SelectorInfo.RequiresSelect(select, this.RefField, this.OnField) ? SelectorToReferenceConverter.SelectorInfo.CreateSelect(select, this.RefField, this.OnField) : select;
    }

    public bool ExcludeFromReferenceGeneratingProcess { get; }

    public BqlCommand Select { get; }

    public System.Type CacheExtensionType { get; }

    public System.Type RefField { get; }

    public System.Type OnField { get; }

    public System.Type OnTable { get; }
  }
}
