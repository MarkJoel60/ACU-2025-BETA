// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.CloseRouteProcess
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.FS;

public class CloseRouteProcess : PXGraph<
#nullable disable
CloseRouteProcess>
{
  [PXHidden]
  public PXSetup<FSSetup> SetupRecord;
  [PXHidden]
  public PXFilter<CloseRouteProcess.RouteFilter> Filter;
  public PXCancel<CloseRouteProcess.RouteFilter> Cancel;
  [PXFilterable(new Type[] {})]
  public PXFilteredProcessingJoin<FSRouteDocument, CloseRouteProcess.RouteFilter, InnerJoin<FSRoute, On<FSRoute.routeID, Equal<FSRouteDocument.routeID>>>, Where2<Where<CurrentValue<CloseRouteProcess.RouteFilter.date>, IsNull, Or<FSRouteDocument.date, Equal<CurrentValue<CloseRouteProcess.RouteFilter.date>>>>, And<Where2<Where<FSRouteDocument.status, Equal<ListField_Status_Route.Completed>>, Or<Where<FSRouteDocument.status, Equal<ListField_Status_Route.Closed>, And<CurrentValue<CloseRouteProcess.RouteFilter.showClosedRoutes>, Equal<True>>>>>>>, OrderBy<Asc<FSRouteDocument.refNbr>>> RouteDocs;
  public PXAction<CloseRouteProcess.RouteFilter> openRoute;

  public CloseRouteProcess()
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: method pointer
    ((PXProcessingBase<FSRouteDocument>) this.RouteDocs).SetProcessDelegate<CloseRouteProcess>(new PXProcessingBase<FSRouteDocument>.ProcessItemDelegate<CloseRouteProcess>((object) new CloseRouteProcess.\u003C\u003Ec__DisplayClass0_0()
    {
      graphRouteDocumentMaint = PXGraph.CreateInstance<RouteDocumentMaint>(),
      graphRouteClosingMaint = PXGraph.CreateInstance<RouteClosingMaint>()
    }, __methodptr(\u003C\u002Ector\u003Eb__0)));
  }

  [PXUIField]
  public virtual void OpenRoute()
  {
    if (((PXSelectBase<FSRouteDocument>) this.RouteDocs).Current.Status == "C" || ((PXSelectBase<FSRouteDocument>) this.RouteDocs).Current.Status == "Z")
    {
      RouteClosingMaint instance = PXGraph.CreateInstance<RouteClosingMaint>();
      ((PXSelectBase<FSRouteDocument>) instance.RouteDocumentSelected).Current = ((PXSelectBase<FSRouteDocument>) this.RouteDocs).Current;
      PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, (string) null);
      ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 3;
      throw requiredException;
    }
  }

  /// <summary>Try to close a set of routes.</summary>
  /// <param name="graphRouteDocumentMaint"> Route Document graph.</param>
  /// <param name="graphRouteClosingMaint"> Route Closing Document graph.</param>
  /// <param name="fsRouteDocumentRow">FSRouteDocument row to be processed.</param>
  public virtual void CloseRoute(
    RouteDocumentMaint graphRouteDocumentMaint,
    RouteClosingMaint graphRouteClosingMaint,
    FSRouteDocument fsRouteDocumentRow)
  {
    if (!(fsRouteDocumentRow.Status != "Z"))
      return;
    ((PXSelectBase<FSRouteDocument>) graphRouteDocumentMaint.RouteRecords).Current = PXResultset<FSRouteDocument>.op_Implicit(((PXSelectBase<FSRouteDocument>) graphRouteDocumentMaint.RouteRecords).Search<FSRouteDocument.refNbr>((object) fsRouteDocumentRow.RefNbr, Array.Empty<object>()));
    ((PXSelectBase<FSRouteDocument>) graphRouteClosingMaint.RouteDocumentSelected).Current = ((PXSelectBase<FSRouteDocument>) graphRouteDocumentMaint.RouteRecords).Current;
    graphRouteClosingMaint.AutomaticallyCloseRoute = true;
    GraphHelper.PressButton((PXAction) graphRouteClosingMaint.closeRoute);
    fsRouteDocumentRow.Status = ((PXSelectBase<FSRouteDocument>) graphRouteClosingMaint.RouteDocumentSelected).Current.Status;
  }

  protected virtual void _(Events.RowSelected<FSRouteDocument> e)
  {
    if (e.Row == null)
      return;
    FSRouteDocument row = e.Row;
    if (!(row.Status == "Z"))
      return;
    PXUIFieldAttribute.SetEnabled<FSRouteDocument.selected>(((Events.Event<PXRowSelectedEventArgs, Events.RowSelected<FSRouteDocument>>) e).Cache, (object) row, false);
  }

  [Serializable]
  public class RouteFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    [PXDBDate]
    [PXUIField(DisplayName = "Date")]
    public virtual DateTime? Date { get; set; }

    [PXBool]
    [PXDefault(false)]
    [PXUIField(DisplayName = "Show Closed Routes")]
    public virtual bool? ShowClosedRoutes { get; set; }

    public abstract class date : 
      BqlType<
      #nullable enable
      IBqlDateTime, DateTime>.Field<
      #nullable disable
      CloseRouteProcess.RouteFilter.date>
    {
    }

    public abstract class showClosedRoutes : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      CloseRouteProcess.RouteFilter.showClosedRoutes>
    {
    }
  }
}
