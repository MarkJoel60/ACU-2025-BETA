// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.GraphExtensions.APInvoiceSmartPanel.ExtensionSorting
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using Autofac;
using PX.Objects.PO.GraphExtensions.APInvoiceEntryExt;
using System;
using System.Collections.Generic;
using System.Web.Compilation;

#nullable disable
namespace PX.Objects.PO.GraphExtensions.APInvoiceSmartPanel;

public class ExtensionSorting : Module
{
  private static readonly Dictionary<Type, int> _order = new Dictionary<Type, int>()
  {
    {
      typeof (Prepayments),
      0
    },
    {
      typeof (LinkLineExtension),
      1
    },
    {
      typeof (AddPOOrderExtension),
      2
    },
    {
      typeof (AddPOOrderLineExtension),
      3
    },
    {
      typeof (AddPOReceiptExtension),
      4
    },
    {
      typeof (AddPOReceiptLineExtension),
      5
    },
    {
      typeof (AddLandedCostExtension),
      6
    }
  };

  protected virtual void Load(ContainerBuilder builder)
  {
    ApplicationStartActivation.RunOnApplicationStart(builder, (Action) (() => PXBuildManager.SortExtensions += (Action<List<Type>>) (list => PXBuildManager.PartialSort(list, ExtensionSorting._order))), (string) null);
  }
}
