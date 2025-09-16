// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.CompleteRouteProcess
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.FS;

public class CompleteRouteProcess : PXGraph<
#nullable disable
CompleteRouteProcess>
{
  [PXHidden]
  public PXSetup<FSSetup> SetupRecord;
  [PXHidden]
  public PXFilter<CompleteRouteProcess.RouteFilter> Filter;
  public PXCancel<CompleteRouteProcess.RouteFilter> Cancel;
  [PXFilterable(new Type[] {})]
  public PXFilteredProcessingJoin<FSRouteDocument, CompleteRouteProcess.RouteFilter, InnerJoin<FSRoute, On<FSRoute.routeID, Equal<FSRouteDocument.routeID>>>, Where2<Where<CurrentValue<CompleteRouteProcess.RouteFilter.date>, IsNull, Or<FSRouteDocument.date, Equal<CurrentValue<CompleteRouteProcess.RouteFilter.date>>>>, And<Where2<Where<FSRouteDocument.status, Equal<ListField_Status_Route.Open>, Or<FSRouteDocument.status, Equal<ListField_Status_Route.InProcess>>>, Or<Where<FSRouteDocument.status, Equal<ListField_Status_Route.Completed>, And<CurrentValue<CompleteRouteProcess.RouteFilter.showCompletedRoutes>, Equal<True>>>>>>>, OrderBy<Asc<FSRouteDocument.refNbr>>> RouteDocs;

  public CompleteRouteProcess()
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: method pointer
    ((PXProcessingBase<FSRouteDocument>) this.RouteDocs).SetProcessDelegate<CompleteRouteProcess>(new PXProcessingBase<FSRouteDocument>.ProcessItemDelegate<CompleteRouteProcess>((object) new CompleteRouteProcess.\u003C\u003Ec__DisplayClass0_0()
    {
      graphRouteDocumentMaint = PXGraph.CreateInstance<RouteDocumentMaint>()
    }, __methodptr(\u003C\u002Ector\u003Eb__0)));
  }

  /// <summary>Try to complete a set of routes.</summary>
  /// <param name="graphRouteDocumentMaint"> Route Document graph.</param>
  /// <param name="fsRouteDocumentRow">FSRouteDocument row to be processed.</param>
  public virtual void CompleteRoute(
    RouteDocumentMaint graphRouteDocumentMaint,
    FSRouteDocument fsRouteDocumentRow)
  {
    if (!(fsRouteDocumentRow.Status != "C"))
      return;
    ((PXSelectBase<FSRouteDocument>) graphRouteDocumentMaint.RouteRecords).Current = PXResultset<FSRouteDocument>.op_Implicit(((PXSelectBase<FSRouteDocument>) graphRouteDocumentMaint.RouteRecords).Search<FSRouteDocument.refNbr>((object) fsRouteDocumentRow.RefNbr, Array.Empty<object>()));
    GraphHelper.PressButton((PXAction) graphRouteDocumentMaint.completeRoute);
  }

  protected virtual void _(Events.RowSelected<FSRouteDocument> e)
  {
    if (e.Row == null)
      return;
    FSRouteDocument row = e.Row;
    if (!(row.Status == "C"))
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
    [PXUIField(DisplayName = "Show Completed Routes")]
    public virtual bool? ShowCompletedRoutes { get; set; }

    public abstract class date : 
      BqlType<
      #nullable enable
      IBqlDateTime, DateTime>.Field<
      #nullable disable
      CompleteRouteProcess.RouteFilter.date>
    {
    }

    public abstract class showCompletedRoutes : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      CompleteRouteProcess.RouteFilter.showCompletedRoutes>
    {
    }
  }
}
