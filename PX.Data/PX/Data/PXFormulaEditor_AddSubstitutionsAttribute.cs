// Decompiled with JetBrains decompiler
// Type: PX.Data.PXFormulaEditor_AddSubstitutionsAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Api;
using PX.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

#nullable disable
namespace PX.Data;

[PXInternalUseOnly]
public class PXFormulaEditor_AddSubstitutionsAttribute : PXFormulaEditor.OptionsProviderAttribute
{
  public override void ChangeOptionsSet(PXGraph graph, ISet<FormulaOption> options)
  {
    foreach (string substitutionKey in PXFormulaEditor_AddSubstitutionsAttribute.GetSubstitutionKeys(graph))
      options.Add(new FormulaOption()
      {
        Category = "Substitution Lists",
        Value = substitutionKey
      });
  }

  public static IEnumerable<string> GetSubstitutionKeys(PXGraph graph)
  {
    return (IEnumerable<string>) PXSelectBase<SYSubstitution, PXSelect<SYSubstitution>.Config>.Select(graph).Select<PXResult<SYSubstitution>, string>((Expression<Func<PXResult<SYSubstitution>, string>>) (substitution => $"'{((SYSubstitution) substitution).SubstitutionID}'"));
  }
}
