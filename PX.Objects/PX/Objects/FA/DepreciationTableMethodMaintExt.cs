// Decompiled with JetBrains decompiler
// Type: PX.Objects.FA.DepreciationTableMethodMaintExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;

#nullable disable
namespace PX.Objects.FA;

public class DepreciationTableMethodMaintExt : 
  DepreciationMethodCancelExt<DepreciationTableMethodMaint, FADepreciationMethod, Where<FADepreciationMethod.methodCD, Equal<Current<FADepreciationMethod.methodCD>>, And<FADepreciationMethod.isTableMethod, Equal<False>>>>
{
  public override string Message
  {
    get => "The formula-based depreciation method with the same ID exists in the system.";
  }
}
