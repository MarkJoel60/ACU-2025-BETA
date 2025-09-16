// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.INPIController
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL.Fluent;
using PX.Objects.IN.PhysicalInventory;
using System;
using System.Linq;

#nullable disable
namespace PX.Objects.IN;

public class INPIController : PXGraph<INPIController>
{
  public PXSave<INPIHeader> Save;
  public PXCancel<INPIHeader> Cancel;
  public FbqlSelect<SelectFromBase<INPIHeader, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<INSite>.On<INPIHeader.FK.Site>>>.Where<MatchUserFor<INSite>>, INPIHeader>.View PIHeader;
  public FbqlSelect<SelectFromBase<INPIDetailUpdate, TypeArrayOf<IFbqlJoin>.Empty>, INPIDetailUpdate>.View PIDetailUpdate;
  public FbqlSelect<SelectFromBase<INPIStatusItem, TypeArrayOf<IFbqlJoin>.Empty>, INPIStatusItem>.View PIStatusItem;
  public FbqlSelect<SelectFromBase<INPIStatusLoc, TypeArrayOf<IFbqlJoin>.Empty>, INPIStatusLoc>.View PIStatusLoc;

  [PXMergeAttributes]
  [PXRemoveBaseAttribute(typeof (PXSelectorAttribute))]
  protected virtual void _(Events.CacheAttached<INPIStatusItem.pIID> e)
  {
  }

  [PXMergeAttributes]
  [PXRemoveBaseAttribute(typeof (PXSelectorAttribute))]
  protected virtual void _(Events.CacheAttached<INPIStatusLoc.pIID> e)
  {
  }

  public virtual void ReopenPI(string piId)
  {
    INPIHeader inpiHeader = ((PXSelectBase<INPIHeader>) this.PIHeader).Current = PXResultset<INPIHeader>.op_Implicit(((PXSelectBase<INPIHeader>) this.PIHeader).Search<INPIHeader.pIID>((object) piId, Array.Empty<object>()));
    if (inpiHeader?.Status != "R")
      return;
    inpiHeader.Status = "E";
    inpiHeader.PIAdjRefNbr = (string) null;
    ((PXSelectBase<INPIHeader>) this.PIHeader).Update(inpiHeader);
    ((PXAction) this.Save).Press();
  }

  public virtual INPIDetailUpdate AccumulateFinalCost(string piId, int piLineNbr, Decimal costAmt)
  {
    INPIDetailUpdate inpiDetailUpdate1 = ((PXSelectBase<INPIDetailUpdate>) this.PIDetailUpdate).Insert(new INPIDetailUpdate()
    {
      PIID = piId,
      LineNbr = new int?(piLineNbr)
    });
    INPIDetailUpdate inpiDetailUpdate2 = inpiDetailUpdate1;
    Decimal? finalExtVarCost = inpiDetailUpdate2.FinalExtVarCost;
    Decimal num = costAmt;
    inpiDetailUpdate2.FinalExtVarCost = finalExtVarCost.HasValue ? new Decimal?(finalExtVarCost.GetValueOrDefault() + num) : new Decimal?();
    return ((PXSelectBase<INPIDetailUpdate>) this.PIDetailUpdate).Update(inpiDetailUpdate1);
  }

  public virtual void ReleasePI(string piId)
  {
    INPIHeader inpiHeader = ((PXSelectBase<INPIHeader>) this.PIHeader).Current = PXResultset<INPIHeader>.op_Implicit(((PXSelectBase<INPIHeader>) this.PIHeader).Search<INPIHeader.pIID>((object) piId, Array.Empty<object>()));
    this.CreatePILocksManager().UnlockInventory();
    inpiHeader.TotalVarCost = new Decimal?(GraphHelper.RowCast<INPIDetailUpdate>(((PXSelectBase) this.PIDetailUpdate).Cache.Inserted).Sum<INPIDetailUpdate>((Func<INPIDetailUpdate, Decimal?>) (d => d.FinalExtVarCost)).GetValueOrDefault());
    inpiHeader.Status = "C";
    ((PXSelectBase<INPIHeader>) this.PIHeader).Update(inpiHeader);
    ((PXAction) this.Save).Press();
  }

  protected virtual PILocksManager CreatePILocksManager()
  {
    INPIHeader current = ((PXSelectBase<INPIHeader>) this.PIHeader).Current;
    return new PILocksManager((PXGraph) this, (PXSelectBase<INPIStatusItem>) this.PIStatusItem, (PXSelectBase<INPIStatusLoc>) this.PIStatusLoc, current.SiteID.Value, current.PIID);
  }
}
