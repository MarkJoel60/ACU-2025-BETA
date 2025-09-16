// Decompiled with JetBrains decompiler
// Type: PX.Api.PXFormulaEditor_AddMultiselectSubstituteAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using System.Collections.Generic;

#nullable disable
namespace PX.Api;

internal class PXFormulaEditor_AddMultiselectSubstituteAttribute : 
  PXFormulaEditor.OptionsProviderAttribute
{
  public override void ChangeOptionsSet(PXGraph graph, ISet<FormulaOption> options)
  {
    options.Add(new FormulaOption()
    {
      Category = "Functions/Other",
      Value = "MultiselectSubstituteAll( sourceField, substitutionList, externalDelimiter )"
    });
    options.Add(new FormulaOption()
    {
      Category = "Functions/Other",
      Value = "MultiselectSubstituteListed( sourceField, substitutionList, externalDelimiter )"
    });
  }
}
