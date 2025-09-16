// Decompiled with JetBrains decompiler
// Type: PX.Api.SyFormulaProcessorTest
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Api;

public static class SyFormulaProcessorTest
{
  public static void Test()
  {
    SyProviderInstance.Provider = (object) 3;
    SyFormulaProcessor formulaProcessor = new SyFormulaProcessor();
    SyFormulaFinalDelegate getter = (SyFormulaFinalDelegate) (names => (object) names.Length);
    string[] strArray = new string[5]
    {
      "=1+Provider.GetHashCode()",
      "=[External]",
      "=[view.field]",
      "=1 + [ex]*[v.n]",
      "=IIf(1=2,[ex], [v.n])"
    };
    foreach (string formula in strArray)
      formulaProcessor.Evaluate(formula, getter);
  }
}
