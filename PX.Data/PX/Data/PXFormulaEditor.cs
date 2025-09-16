// Decompiled with JetBrains decompiler
// Type: PX.Data.PXFormulaEditor
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System.Collections.Generic;

#nullable disable
namespace PX.Data;

/// <summary>
/// The class that provides attributes to configure the formula editor control.
/// </summary>
public static class PXFormulaEditor
{
  /// <summary>
  /// Adds the <bold>Functions</bold> node and the default set of functions to the formula editor dialog box.
  /// </summary>
  public class AddFunctionsAttribute : PXFormulaEditor.OptionsProviderAttribute
  {
    public override void ChangeOptionsSet(PXGraph graph, ISet<FormulaOption> options)
    {
      foreach (FormulaOption formulaOption in DefaultFormulaOptions.Functions.GetAll())
        options.Add(formulaOption);
    }
  }

  /// <summary>
  /// Adds the <bold>Operators</bold> node and the default set of operators to the formula editor dialog box.
  /// </summary>
  public class AddOperatorsAttribute : PXFormulaEditor.OptionsProviderAttribute
  {
    public override void ChangeOptionsSet(PXGraph graph, ISet<FormulaOption> options)
    {
      foreach (FormulaOption formulaOption in DefaultFormulaOptions.Operators.GetAll())
        options.Add(formulaOption);
    }
  }

  /// <summary>
  /// Adds the <bold>Styles</bold> node and the default set of styles to the formula editor dialog box.
  /// </summary>
  public class AddStylesAttribute : PXFormulaEditor.OptionsProviderAttribute
  {
    public override void ChangeOptionsSet(PXGraph graph, ISet<FormulaOption> options)
    {
      foreach (FormulaOption formulaOption in DefaultFormulaOptions.Styles.GetAll())
        options.Add(formulaOption);
    }
  }

  /// <summary>
  /// The class that provides attributes to configure options in the formula editor dialog box.
  /// </summary>
  public abstract class OptionsProviderAttribute : PXEventSubscriberAttribute
  {
    /// <summary>
    /// Changes the set of options for the formula editor control.
    /// </summary>
    /// <param name="graph">The graph where the formula editor control is changed.</param>
    /// <param name="options">The set of options in the formula editor dialog box.</param>
    public abstract void ChangeOptionsSet(PXGraph graph, ISet<FormulaOption> options);
  }
}
