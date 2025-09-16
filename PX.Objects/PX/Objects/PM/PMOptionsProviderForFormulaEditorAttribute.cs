// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.PMOptionsProviderForFormulaEditorAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.EP;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.PM;

public sealed class PMOptionsProviderForFormulaEditorAttribute : 
  PXFormulaEditor.OptionsProviderAttribute
{
  private static readonly string[] DefaultParameters = new string[2]
  {
    "Rate",
    "Price"
  };
  private static readonly Type[] DefaultTypes = new Type[10]
  {
    typeof (PMAccountGroup),
    typeof (PMBudget),
    typeof (PMProject),
    typeof (PMTask),
    typeof (PMTran),
    typeof (PX.Objects.AR.Customer),
    typeof (EPEmployee),
    typeof (PX.Objects.IN.InventoryItem),
    typeof (PX.Objects.AP.Vendor),
    typeof (AccessInfo)
  };
  private readonly string[] _parameters;
  private readonly Type[] _types;

  public PMOptionsProviderForFormulaEditorAttribute()
  {
    this._parameters = PMOptionsProviderForFormulaEditorAttribute.DefaultParameters;
    this._types = PMOptionsProviderForFormulaEditorAttribute.DefaultTypes;
  }

  public PMOptionsProviderForFormulaEditorAttribute(string[] parameters, params Type[] types)
  {
    this._parameters = parameters ?? Array.Empty<string>();
    this._types = types ?? Array.Empty<Type>();
  }

  public virtual void ChangeOptionsSet(PXGraph graph, ISet<FormulaOption> options)
  {
    if (graph == null || options == null)
      return;
    string str1 = PXLocalizer.Localize("Objects");
    string str2 = PXLocalizer.Localize("Parameters");
    HashSet<string> stringSet = new HashSet<string>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
    foreach (string str3 in ((IEnumerable<string>) this._parameters).Where<string>((Func<string, bool>) (s => !string.IsNullOrWhiteSpace(s))))
    {
      string str4 = "@" + str3;
      if (stringSet.Add(str4))
        options.Add(new FormulaOption()
        {
          Category = str2,
          Value = str4
        });
    }
    foreach (Type type in ((IEnumerable<Type>) this._types).Where<Type>((Func<Type, bool>) (t => t != (Type) null)))
    {
      try
      {
        GraphHelper.EnsureCachePersistence(graph, type);
        PXCache cach = graph.Caches[type];
        if (cach != null)
        {
          string str5 = $"{str1}/{type.Name}";
          foreach (string field in (List<string>) cach.Fields)
          {
            string str6 = $"[{type.Name}.{field}]";
            if (stringSet.Add(str6))
              options.Add(new FormulaOption()
              {
                Category = str5,
                Value = str6
              });
          }
        }
      }
      catch (Exception ex)
      {
        PXTrace.WriteError(ex.ToString());
      }
    }
  }
}
