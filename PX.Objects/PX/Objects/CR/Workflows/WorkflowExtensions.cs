// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.Workflows.WorkflowExtensions
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.WorkflowAPI;

#nullable disable
namespace PX.Objects.CR.Workflows;

internal static class WorkflowExtensions
{
  public static BoundedTo<TGraph, TPrimary>.FieldState.IAllowOptionalConfig IsDisabled<TGraph, TPrimary>(
    this BoundedTo<TGraph, TPrimary>.FieldState.IAllowOptionalConfig config,
    bool disabled)
    where TGraph : PXGraph
    where TPrimary : class, IBqlTable, new()
  {
    return !disabled ? config : ((BoundedTo<TGraph, TPrimary>.FieldState.INeedAnyConfigField) config).IsDisabled();
  }

  public static BoundedTo<TGraph, TPrimary>.ActionState.IAllowOptionalConfig IsDuplicatedInToolbar<TGraph, TPrimary>(
    this BoundedTo<TGraph, TPrimary>.ActionState.IAllowOptionalConfig config,
    bool duplicated)
    where TGraph : PXGraph
    where TPrimary : class, IBqlTable, new()
  {
    return !duplicated ? config : config.IsDuplicatedInToolbar();
  }
}
