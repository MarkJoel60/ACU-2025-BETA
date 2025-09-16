// Decompiled with JetBrains decompiler
// Type: PX.Data.PXFieldNamesListAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Data;

/// <summary>Implements PXStringList attribute with fixed set of fields from any view of the specified graph.</summary>
[PXInternalUseOnly]
public class PXFieldNamesListAttribute : PXStringListAttribute, IPXRowSelectedSubscriber
{
  private readonly PXGraph _Graph;
  private readonly IGrouping<System.Type, System.Type>[] _FieldGroups;
  internal const char tableAndFieldSeparator = '.';

  /// <param name="graphType">Graph that contains caches with specified fields.</param>
  /// <param name="fieldTypes">Fields those names will form the list of allowed values.</param>
  public PXFieldNamesListAttribute(System.Type graphType, params System.Type[] fieldTypes)
  {
    this.IsLocalizable = false;
    this._Graph = PXGraph.CreateInstance(graphType);
    this._FieldGroups = ((IEnumerable<System.Type>) fieldTypes).GroupBy<System.Type, System.Type>((Func<System.Type, System.Type>) (field => field.DeclaringType)).ToArray<IGrouping<System.Type, System.Type>>();
  }

  public void RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    List<string> stringList1 = new List<string>();
    List<string> stringList2 = new List<string>();
    using (new PXLocaleScope(PXLocalesProvider.GetCurrentLocale()))
    {
      foreach (IGrouping<System.Type, System.Type> fieldGroup in this._FieldGroups)
      {
        PXCache cach = this._Graph.Caches[fieldGroup.Key];
        if (cach != null)
        {
          object instance = cach.CreateInstance();
          foreach (System.Type type in (IEnumerable<System.Type>) fieldGroup)
          {
            string displayName = cach.GetAttributesOfType<PXUIFieldAttribute>(instance, type.Name).FirstOrDefault<PXUIFieldAttribute>()?.DisplayName;
            if (displayName != null)
            {
              stringList1.Add(PXFieldNamesListAttribute.MergeNames(fieldGroup.Key.Name, type.Name));
              stringList2.Add(PXLocalizer.Localize(displayName));
            }
          }
        }
      }
    }
    this._AllowedValues = stringList1.ToArray();
    this._AllowedLabels = stringList2.ToArray();
  }

  public static string MergeNames(string tableName, string fieldName) => $"{tableName}.{fieldName}";

  public static bool SplitNames(string fullName, out string tableName, out string fieldName)
  {
    if (fullName != null)
    {
      string[] strArray = fullName.Split('.');
      if (strArray.Length == 2)
      {
        tableName = strArray[0];
        fieldName = strArray[1];
        return true;
      }
    }
    tableName = (string) null;
    fieldName = (string) null;
    return false;
  }
}
