// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.RoutePendingInq
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.FS;

public class RoutePendingInq : PXGraph<
#nullable disable
RoutePendingInq>
{
  public PXFilter<RoutePendingInq.RouteWrkSheetFilter> Filter;
  [PXFilterable(new Type[] {})]
  public PXSelectJoin<FSRouteDocument, InnerJoin<FSRoute, On<FSRouteDocument.routeID, Equal<FSRoute.routeID>>>, Where2<Where<CurrentValue<RoutePendingInq.RouteWrkSheetFilter.date>, IsNull, Or<FSRouteDocument.date, Equal<CurrentValue<RoutePendingInq.RouteWrkSheetFilter.date>>>>, And<Where<FSRouteDocument.status, Equal<ListField_Status_Route.Completed>, Or<FSRouteDocument.status, Equal<ListField_Status_Route.Closed>>>>>, OrderBy<Asc<FSRouteDocument.refNbr>>> Routes;
  public PXAction<RoutePendingInq.RouteWrkSheetFilter> openRouteClosing;

  public RoutePendingInq() => ((PXSelectBase) this.Routes).AllowUpdate = false;

  [PXUIField]
  [PXButton]
  public virtual void OpenRouteClosing()
  {
    if (((PXSelectBase<FSRouteDocument>) this.Routes).Current != null)
    {
      RouteClosingMaint instance = PXGraph.CreateInstance<RouteClosingMaint>();
      ((PXSelectBase<FSRouteDocument>) instance.RouteRecords).Current = PXResultset<FSRouteDocument>.op_Implicit(((PXSelectBase<FSRouteDocument>) instance.RouteRecords).Search<FSRouteDocument.refNbr>((object) ((PXSelectBase<FSRouteDocument>) this.Routes).Current.RefNbr, Array.Empty<object>()));
      PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, (string) null);
      ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 3;
      throw requiredException;
    }
  }

  [Serializable]
  public class RouteWrkSheetFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    [PXDBDate]
    [PXUIField(DisplayName = "Date")]
    [PXDefault]
    public virtual DateTime? Date { get; set; }

    public abstract class date : 
      BqlType<
      #nullable enable
      IBqlDateTime, DateTime>.Field<
      #nullable disable
      RoutePendingInq.RouteWrkSheetFilter.date>
    {
    }
  }
}
