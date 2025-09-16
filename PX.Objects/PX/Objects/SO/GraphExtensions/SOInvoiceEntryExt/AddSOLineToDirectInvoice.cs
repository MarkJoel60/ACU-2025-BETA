// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.GraphExtensions.SOInvoiceEntryExt.AddSOLineToDirectInvoice
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.AR;
using PX.Objects.CS;
using PX.Objects.SO.DAC.Projections;
using System;
using System.Collections;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.SO.GraphExtensions.SOInvoiceEntryExt;

public class AddSOLineToDirectInvoice : PXGraphExtension<SOInvoiceEntry>
{
  [PXCopyPasteHiddenView]
  public PXSelect<SOLineForDirectInvoice, Where<SOLineForDirectInvoice.customerID, Equal<Current<PX.Objects.AR.ARInvoice.customerID>>>> soLineList;
  public PXAction<PX.Objects.AR.ARInvoice> selectSOLine;
  public PXAction<PX.Objects.AR.ARInvoice> addSOLine;

  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.advancedSOInvoices>();

  [PXUIField]
  [PXLookupButton]
  public virtual IEnumerable SelectSOLine(PXAdapter adapter)
  {
    if (((PXSelectBase) this.Base.Document).Cache.AllowInsert && ((PXSelectBase<SOLineForDirectInvoice>) this.soLineList).AskExt() == 1)
      this.AddSOLine(adapter);
    return adapter.Get();
  }

  [PXUIField]
  [PXLookupButton]
  public virtual IEnumerable AddSOLine(PXAdapter adapter)
  {
    HashSet<(string, string)> valueTupleSet = new HashSet<(string, string)>();
    foreach (SOLineForDirectInvoice forDirectInvoice in ((PXSelectBase) this.soLineList).Cache.Updated)
    {
      if (forDirectInvoice.Selected.GetValueOrDefault())
      {
        ARTran instance = (ARTran) ((PXSelectBase) this.Base.Transactions).Cache.CreateInstance();
        instance.SOOrderType = forDirectInvoice.OrderType;
        instance.SOOrderNbr = forDirectInvoice.OrderNbr;
        instance.SOOrderLineNbr = forDirectInvoice.LineNbr;
        this.Base.InsertInvoiceDirectLine(instance);
        forDirectInvoice.Selected = new bool?(false);
        valueTupleSet.Add((instance.SOOrderType, instance.SOOrderNbr));
      }
    }
    SOOrderType orderType = new SOOrderType();
    foreach ((string, string) valueTuple in valueTupleSet)
    {
      if (orderType.OrderType != valueTuple.Item1)
        orderType = SOOrderType.PK.Find((PXGraph) this.Base, valueTuple.Item1);
      if (orderType != null)
      {
        bool? nullable = orderType.CopyHeaderNotesToInvoice;
        if (!nullable.GetValueOrDefault())
        {
          nullable = orderType.CopyHeaderFilesToInvoice;
          if (!nullable.GetValueOrDefault())
            continue;
        }
        PX.Objects.SO.SOOrder srcOrder = PX.Objects.SO.SOOrder.PK.Find((PXGraph) this.Base, valueTuple.Item1, valueTuple.Item2);
        if (orderType != null && orderType.OrderType != null && srcOrder != null && ((PXSelectBase<PX.Objects.AR.ARInvoice>) this.Base.Document).Current != null)
          this.Base.CopyOrderHeaderNoteAndFiles(srcOrder, ((PXSelectBase<PX.Objects.AR.ARInvoice>) this.Base.Document).Current, orderType);
      }
    }
    return adapter.Get();
  }

  protected virtual void _(PX.Data.Events.RowSelected<PX.Objects.AR.ARInvoice> e)
  {
    PXAction<PX.Objects.AR.ARInvoice> selectSoLine = this.selectSOLine;
    int num;
    if (((PXSelectBase) this.Base.Transactions).AllowInsert)
    {
      PX.Objects.AR.ARInvoice row = e.Row;
      num = row != null ? (row.CustomerID.HasValue ? 1 : 0) : 0;
    }
    else
      num = 0;
    ((PXAction) selectSoLine).SetEnabled(num != 0);
  }

  /// Overrides <see cref="M:PX.Objects.SO.SOInvoiceEntry.Persist" />
  [PXOverride]
  public virtual void Persist(Action baseMethod)
  {
    baseMethod();
    ((PXSelectBase) this.soLineList).Cache.Clear();
    ((PXSelectBase) this.soLineList).Cache.ClearQueryCache();
  }
}
