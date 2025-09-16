// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.YearSetupGraph`3
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;

#nullable disable
namespace PX.Objects.GL;

public abstract class YearSetupGraph<TGraph, TYearSetup, TPeriodSetup> : 
  YearSetupGraphBase<TGraph, TYearSetup, TPeriodSetup, PXSelect<TPeriodSetup>>
  where TGraph : PXGraph
  where TYearSetup : class, IYearSetup, IBqlTable, new()
  where TPeriodSetup : class, IPeriodSetup, IBqlTable, new()
{
}
