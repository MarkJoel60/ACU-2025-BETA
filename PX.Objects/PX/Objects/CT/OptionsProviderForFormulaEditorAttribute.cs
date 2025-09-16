// Decompiled with JetBrains decompiler
// Type: PX.Objects.CT.OptionsProviderForFormulaEditorAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.CT;

public class OptionsProviderForFormulaEditorAttribute : PXFormulaEditor.OptionsProviderAttribute
{
  private readonly string[] parameters;
  private readonly Type[] types;

  public OptionsProviderForFormulaEditorAttribute(string[] Parameters, params Type[] Types)
  {
    this.parameters = Parameters;
    this.types = Types;
  }

  public virtual void ChangeOptionsSet(PXGraph graph, ISet<FormulaOption> options)
  {
    string objRoot = PXLocalizer.Localize("Objects");
    string paramsRoot = PXLocalizer.Localize("Parameters");
    EnumerableExtensions.ForEach<string>((IEnumerable<string>) this.parameters, (Action<string>) (p => options.Add(new FormulaOption()
    {
      Category = paramsRoot,
      Value = "@" + p
    })));
    EnumerableExtensions.ForEach<Type>((IEnumerable<Type>) this.types, (Action<Type>) (t => ((List<string>) graph.Caches[t].Fields).ForEach((Action<string>) (f => options.Add(new FormulaOption()
    {
      Category = $"{objRoot}/{t.Name}",
      Value = $"[{t.Name}.{f}]"
    })))));
  }
}
