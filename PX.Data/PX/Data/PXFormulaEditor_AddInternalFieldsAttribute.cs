// Decompiled with JetBrains decompiler
// Type: PX.Data.PXFormulaEditor_AddInternalFieldsAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System.Collections.Generic;

#nullable disable
namespace PX.Data;

internal class PXFormulaEditor_AddInternalFieldsAttribute : PXFormulaEditor.OptionsProviderAttribute
{
  public override void ChangeOptionsSet(PXGraph graph, ISet<FormulaOption> options)
  {
    foreach (string internalField in ((IFormulaEditorInternalFields) graph).GetInternalFields())
      options.Add(new FormulaOption()
      {
        Category = "Fields/Internal",
        Value = internalField
      });
  }
}
