// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.Matrix.GraphExtensions.ExtensionSorting
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using Autofac;
using System;
using System.Collections.Generic;
using System.Web.Compilation;

#nullable disable
namespace PX.Objects.IN.Matrix.GraphExtensions;

public class ExtensionSorting : Module
{
  private static readonly Dictionary<Type, int> _order = new Dictionary<Type, int>()
  {
    {
      typeof (CreateMatrixItemsTabExt),
      1
    },
    {
      typeof (ApplyToMatrixItemsExt),
      2
    }
  };

  protected virtual void Load(ContainerBuilder builder)
  {
    ApplicationStartActivation.RunOnApplicationStart(builder, (Action) (() => PXBuildManager.SortExtensions += (Action<List<Type>>) (list => PXBuildManager.PartialSort(list, ExtensionSorting._order))), (string) null);
  }
}
