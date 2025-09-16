// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.INSiteStatusLookupExt`2
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.Common.Interfaces;
using PX.Objects.Extensions.AddItemLookup;
using System;

#nullable disable
namespace PX.Objects.IN;

public abstract class INSiteStatusLookupExt<TGraph, TItemInfo> : 
  SiteStatusLookupExt<TGraph, INRegister, INTran, TItemInfo, INSiteStatusFilter>
  where TGraph : INRegisterEntryBase
  where TItemInfo : class, IQtySelectable, IPXSelectable, IBqlTable, new()
{
  protected PXSelectBase<INRegister> Document => this.Base.INRegisterDataMember;

  protected PXSelectBase<INTran> Transactions => this.Base.INTranDataMember;

  protected PXSelectBase<INTranSplit> Splits => this.Base.INTranSplitDataMember;

  protected PXSelectBase<INTran> LSSelect => this.Base.LSSelectDataMember;

  protected override INTran CreateNewLine(TItemInfo line)
  {
    INTran inTran = this.InitTran(PXCache<INTran>.CreateCopy(this.Transactions.Insert(new INTran())), line);
    inTran.Qty = new Decimal?(((Decimal?) ((PXCache) GraphHelper.Caches<TItemInfo>((PXGraph) (object) this.Base)).GetValue((object) line, this.QtySelected)).GetValueOrDefault());
    return this.Transactions.Update(inTran);
  }

  protected abstract INTran InitTran(INTran newTran, TItemInfo siteStatus);

  protected abstract bool IsAddItemEnabled(INRegister doc);

  protected virtual void _(Events.RowSelected<INRegister> args)
  {
    if (args.Row == null)
      return;
    bool flag = this.IsAddItemEnabled(args.Row);
    ((PXAction) this.showItems).SetEnabled(flag);
    ((PXAction) this.addSelectedItems).SetEnabled(flag);
  }

  protected virtual void _(Events.RowInserted<INSiteStatusFilter> args)
  {
    if (args.Row == null || this.Document.Current == null)
      return;
    args.Row.SiteID = this.Document.Current.SiteID;
  }
}
