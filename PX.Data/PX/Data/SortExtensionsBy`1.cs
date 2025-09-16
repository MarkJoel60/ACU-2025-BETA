// Decompiled with JetBrains decompiler
// Type: PX.Data.SortExtensionsBy`1
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using Autofac;
using PX.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Compilation;

#nullable disable
namespace PX.Data;

/// <summary>
/// A declarative rule, used for <see cref="T:PX.Data.PXGraphExtension" /> sorting.
/// </summary>
/// <typeparam name="TExtensionOrder"><see cref="T:PX.Data.ExtensionOrderFor`1" /></typeparam>
public class SortExtensionsBy<TExtensionOrder> : Module where TExtensionOrder : ITypeArrayOf<PXGraphExtension>, TypeArray.IsNotEmpty
{
  private static readonly Dictionary<System.Type, int> Order = SortExtensionsBy<TExtensionOrder>.GetOrders(TypeArrayOf<PXGraphExtension>.CheckAndExtract(typeof (TExtensionOrder), (string) null));

  protected virtual void Load(ContainerBuilder builder)
  {
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    ApplicationStartActivation.RunOnApplicationStart(builder, (System.Action) (() => PXBuildManager.SortExtensions += SortExtensionsBy<TExtensionOrder>.\u003C\u003EO.\u003C0\u003E__SortExtensions ?? (SortExtensionsBy<TExtensionOrder>.\u003C\u003EO.\u003C0\u003E__SortExtensions = new System.Action<List<System.Type>>(SortExtensionsBy<TExtensionOrder>.SortExtensions))), (string) null);
  }

  private static Dictionary<System.Type, int> GetOrders(System.Type[] orderArray)
  {
    return ((IEnumerable<System.Type>) orderArray).ToDictionary<System.Type, System.Type, int>((Func<System.Type, System.Type>) (_ => _), (Func<System.Type, int>) (_ => Array.IndexOf<System.Type>(orderArray, _)));
  }

  private static void SortExtensions(List<System.Type> list)
  {
    PXBuildManager.PartialSort(list, SortExtensionsBy<TExtensionOrder>.Order);
  }
}
