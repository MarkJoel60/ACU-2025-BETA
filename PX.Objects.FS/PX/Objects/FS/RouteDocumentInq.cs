// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.RouteDocumentInq
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.SM;
using System;
using System.Collections;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.FS;

public class RouteDocumentInq : PXGraph<RouteDocumentInq>
{
  [PXHidden]
  public PXSetup<FSRouteSetup> RouteSetupRecord;
  [PXHidden]
  public PXSetup<FSSetup> SetupRecord;
  public PXFilter<RouteDocumentFilter> Filter;
  public PXCancel<RouteDocumentFilter> Cancel;
  [PXFilterable(new Type[] {})]
  [PXViewDetailsButton(typeof (RouteDocumentFilter))]
  public PXSelectJoin<FSRouteDocument, InnerJoin<FSRoute, On<FSRoute.routeID, Equal<FSRouteDocument.routeID>>>> RouteDocuments;
  public PXAction<RouteDocumentFilter> openRouteDocument;

  [PXUIField]
  public virtual void OpenRouteDocument()
  {
    if (((PXSelectBase<FSRouteDocument>) this.RouteDocuments).Current.Status == "Z")
    {
      RouteClosingMaint instance = PXGraph.CreateInstance<RouteClosingMaint>();
      ((PXSelectBase<FSRouteDocument>) instance.RouteRecords).Current = ((PXSelectBase<FSRouteDocument>) this.RouteDocuments).Current;
      PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, (string) null);
      ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 3;
      throw requiredException;
    }
    RouteDocumentMaint instance1 = PXGraph.CreateInstance<RouteDocumentMaint>();
    ((PXSelectBase<FSRouteDocument>) instance1.RouteRecords).Current = ((PXSelectBase<FSRouteDocument>) this.RouteDocuments).Current;
    PXRedirectRequiredException requiredException1 = new PXRedirectRequiredException((PXGraph) instance1, (string) null);
    ((PXBaseRedirectException) requiredException1).Mode = (PXBaseRedirectException.WindowMode) 3;
    throw requiredException1;
  }

  public virtual Type SetWhereStatus(
    bool? statusOpen,
    bool? statusInProcess,
    bool? statusCanceled,
    bool? statusCompleted,
    bool? statusClosed,
    PXSelectBase<FSRouteDocument> commandFilter)
  {
    bool? nullable;
    if (statusOpen.GetValueOrDefault())
    {
      nullable = statusInProcess;
      bool flag1 = false;
      if (nullable.GetValueOrDefault() == flag1 & nullable.HasValue)
      {
        nullable = statusCanceled;
        bool flag2 = false;
        if (nullable.GetValueOrDefault() == flag2 & nullable.HasValue)
        {
          nullable = statusCompleted;
          bool flag3 = false;
          if (nullable.GetValueOrDefault() == flag3 & nullable.HasValue)
          {
            nullable = statusClosed;
            bool flag4 = false;
            if (nullable.GetValueOrDefault() == flag4 & nullable.HasValue)
              commandFilter.WhereAnd<Where<FSRouteDocument.status, Equal<ListField_Status_Route.Open>>>();
          }
        }
      }
    }
    nullable = statusOpen;
    bool flag5 = false;
    if (nullable.GetValueOrDefault() == flag5 & nullable.HasValue && statusInProcess.GetValueOrDefault())
    {
      nullable = statusCanceled;
      bool flag6 = false;
      if (nullable.GetValueOrDefault() == flag6 & nullable.HasValue)
      {
        nullable = statusCompleted;
        bool flag7 = false;
        if (nullable.GetValueOrDefault() == flag7 & nullable.HasValue)
        {
          nullable = statusClosed;
          bool flag8 = false;
          if (nullable.GetValueOrDefault() == flag8 & nullable.HasValue)
            commandFilter.WhereAnd<Where<FSRouteDocument.status, Equal<ListField_Status_Route.InProcess>>>();
        }
      }
    }
    nullable = statusOpen;
    bool flag9 = false;
    if (nullable.GetValueOrDefault() == flag9 & nullable.HasValue)
    {
      nullable = statusInProcess;
      bool flag10 = false;
      if (nullable.GetValueOrDefault() == flag10 & nullable.HasValue && statusCanceled.GetValueOrDefault())
      {
        nullable = statusCompleted;
        bool flag11 = false;
        if (nullable.GetValueOrDefault() == flag11 & nullable.HasValue)
        {
          nullable = statusClosed;
          bool flag12 = false;
          if (nullable.GetValueOrDefault() == flag12 & nullable.HasValue)
            commandFilter.WhereAnd<Where<FSRouteDocument.status, Equal<ListField_Status_Route.Canceled>>>();
        }
      }
    }
    nullable = statusOpen;
    bool flag13 = false;
    if (nullable.GetValueOrDefault() == flag13 & nullable.HasValue)
    {
      nullable = statusInProcess;
      bool flag14 = false;
      if (nullable.GetValueOrDefault() == flag14 & nullable.HasValue)
      {
        nullable = statusCanceled;
        bool flag15 = false;
        if (nullable.GetValueOrDefault() == flag15 & nullable.HasValue && statusCompleted.GetValueOrDefault())
        {
          nullable = statusClosed;
          bool flag16 = false;
          if (nullable.GetValueOrDefault() == flag16 & nullable.HasValue)
            commandFilter.WhereAnd<Where<FSRouteDocument.status, Equal<ListField_Status_Route.Completed>>>();
        }
      }
    }
    nullable = statusOpen;
    bool flag17 = false;
    if (nullable.GetValueOrDefault() == flag17 & nullable.HasValue)
    {
      nullable = statusInProcess;
      bool flag18 = false;
      if (nullable.GetValueOrDefault() == flag18 & nullable.HasValue)
      {
        nullable = statusCanceled;
        bool flag19 = false;
        if (nullable.GetValueOrDefault() == flag19 & nullable.HasValue)
        {
          nullable = statusCompleted;
          bool flag20 = false;
          if (nullable.GetValueOrDefault() == flag20 & nullable.HasValue && statusClosed.GetValueOrDefault())
            commandFilter.WhereAnd<Where<FSRouteDocument.status, Equal<ListField_Status_Route.Closed>>>();
        }
      }
    }
    if (statusOpen.GetValueOrDefault() && statusInProcess.GetValueOrDefault())
    {
      nullable = statusCanceled;
      bool flag21 = false;
      if (nullable.GetValueOrDefault() == flag21 & nullable.HasValue)
      {
        nullable = statusCompleted;
        bool flag22 = false;
        if (nullable.GetValueOrDefault() == flag22 & nullable.HasValue)
        {
          nullable = statusClosed;
          bool flag23 = false;
          if (nullable.GetValueOrDefault() == flag23 & nullable.HasValue)
            commandFilter.WhereAnd<Where<FSRouteDocument.status, Equal<ListField_Status_Route.Open>, Or<FSRouteDocument.status, Equal<ListField_Status_Route.InProcess>>>>();
        }
      }
    }
    if (statusOpen.GetValueOrDefault())
    {
      nullable = statusInProcess;
      bool flag24 = false;
      if (nullable.GetValueOrDefault() == flag24 & nullable.HasValue && statusCanceled.GetValueOrDefault())
      {
        nullable = statusCompleted;
        bool flag25 = false;
        if (nullable.GetValueOrDefault() == flag25 & nullable.HasValue)
        {
          nullable = statusClosed;
          bool flag26 = false;
          if (nullable.GetValueOrDefault() == flag26 & nullable.HasValue)
            commandFilter.WhereAnd<Where<FSRouteDocument.status, Equal<ListField_Status_Route.Open>, Or<FSRouteDocument.status, Equal<ListField_Status_Route.Canceled>>>>();
        }
      }
    }
    if (statusOpen.GetValueOrDefault())
    {
      nullable = statusInProcess;
      bool flag27 = false;
      if (nullable.GetValueOrDefault() == flag27 & nullable.HasValue)
      {
        nullable = statusCanceled;
        bool flag28 = false;
        if (nullable.GetValueOrDefault() == flag28 & nullable.HasValue && statusCompleted.GetValueOrDefault())
        {
          nullable = statusClosed;
          bool flag29 = false;
          if (nullable.GetValueOrDefault() == flag29 & nullable.HasValue)
            commandFilter.WhereAnd<Where<FSRouteDocument.status, Equal<ListField_Status_Route.Open>, Or<FSRouteDocument.status, Equal<ListField_Status_Route.Completed>>>>();
        }
      }
    }
    if (statusOpen.GetValueOrDefault())
    {
      nullable = statusInProcess;
      bool flag30 = false;
      if (nullable.GetValueOrDefault() == flag30 & nullable.HasValue)
      {
        nullable = statusCanceled;
        bool flag31 = false;
        if (nullable.GetValueOrDefault() == flag31 & nullable.HasValue)
        {
          nullable = statusCompleted;
          bool flag32 = false;
          if (nullable.GetValueOrDefault() == flag32 & nullable.HasValue && statusClosed.GetValueOrDefault())
            commandFilter.WhereAnd<Where<FSRouteDocument.status, Equal<ListField_Status_Route.Open>, Or<FSRouteDocument.status, Equal<ListField_Status_Route.Closed>>>>();
        }
      }
    }
    nullable = statusOpen;
    bool flag33 = false;
    if (nullable.GetValueOrDefault() == flag33 & nullable.HasValue && statusInProcess.GetValueOrDefault() && statusCanceled.GetValueOrDefault())
    {
      nullable = statusCompleted;
      bool flag34 = false;
      if (nullable.GetValueOrDefault() == flag34 & nullable.HasValue)
      {
        nullable = statusClosed;
        bool flag35 = false;
        if (nullable.GetValueOrDefault() == flag35 & nullable.HasValue)
          commandFilter.WhereAnd<Where<FSRouteDocument.status, Equal<ListField_Status_Route.InProcess>, Or<FSRouteDocument.status, Equal<ListField_Status_Route.Canceled>>>>();
      }
    }
    nullable = statusOpen;
    bool flag36 = false;
    if (nullable.GetValueOrDefault() == flag36 & nullable.HasValue && statusInProcess.GetValueOrDefault())
    {
      nullable = statusCanceled;
      bool flag37 = false;
      if (nullable.GetValueOrDefault() == flag37 & nullable.HasValue && statusCompleted.GetValueOrDefault())
      {
        nullable = statusClosed;
        bool flag38 = false;
        if (nullable.GetValueOrDefault() == flag38 & nullable.HasValue)
          commandFilter.WhereAnd<Where<FSRouteDocument.status, Equal<ListField_Status_Route.InProcess>, Or<FSRouteDocument.status, Equal<ListField_Status_Route.Completed>>>>();
      }
    }
    nullable = statusOpen;
    bool flag39 = false;
    if (nullable.GetValueOrDefault() == flag39 & nullable.HasValue && statusInProcess.GetValueOrDefault())
    {
      nullable = statusCanceled;
      bool flag40 = false;
      if (nullable.GetValueOrDefault() == flag40 & nullable.HasValue)
      {
        nullable = statusCompleted;
        bool flag41 = false;
        if (nullable.GetValueOrDefault() == flag41 & nullable.HasValue && statusClosed.GetValueOrDefault())
          commandFilter.WhereAnd<Where<FSRouteDocument.status, Equal<ListField_Status_Route.InProcess>, Or<FSRouteDocument.status, Equal<ListField_Status_Route.Closed>>>>();
      }
    }
    nullable = statusOpen;
    bool flag42 = false;
    if (nullable.GetValueOrDefault() == flag42 & nullable.HasValue)
    {
      nullable = statusInProcess;
      bool flag43 = false;
      if (nullable.GetValueOrDefault() == flag43 & nullable.HasValue && statusCanceled.GetValueOrDefault() && statusCompleted.GetValueOrDefault())
      {
        nullable = statusClosed;
        bool flag44 = false;
        if (nullable.GetValueOrDefault() == flag44 & nullable.HasValue)
          commandFilter.WhereAnd<Where<FSRouteDocument.status, Equal<ListField_Status_Route.Canceled>, Or<FSRouteDocument.status, Equal<ListField_Status_Route.Completed>>>>();
      }
    }
    nullable = statusOpen;
    bool flag45 = false;
    if (nullable.GetValueOrDefault() == flag45 & nullable.HasValue)
    {
      nullable = statusInProcess;
      bool flag46 = false;
      if (nullable.GetValueOrDefault() == flag46 & nullable.HasValue && statusCanceled.GetValueOrDefault())
      {
        nullable = statusCompleted;
        bool flag47 = false;
        if (nullable.GetValueOrDefault() == flag47 & nullable.HasValue && statusClosed.GetValueOrDefault())
          commandFilter.WhereAnd<Where<FSRouteDocument.status, Equal<ListField_Status_Route.Canceled>, Or<FSRouteDocument.status, Equal<ListField_Status_Route.Closed>>>>();
      }
    }
    nullable = statusOpen;
    bool flag48 = false;
    if (nullable.GetValueOrDefault() == flag48 & nullable.HasValue)
    {
      nullable = statusInProcess;
      bool flag49 = false;
      if (nullable.GetValueOrDefault() == flag49 & nullable.HasValue)
      {
        nullable = statusCanceled;
        bool flag50 = false;
        if (nullable.GetValueOrDefault() == flag50 & nullable.HasValue && statusCompleted.GetValueOrDefault() && statusClosed.GetValueOrDefault())
          commandFilter.WhereAnd<Where<FSRouteDocument.status, Equal<ListField_Status_Route.Completed>, Or<FSRouteDocument.status, Equal<ListField_Status_Route.Closed>>>>();
      }
    }
    if (statusOpen.GetValueOrDefault() && statusInProcess.GetValueOrDefault() && statusCanceled.GetValueOrDefault())
    {
      nullable = statusCompleted;
      bool flag51 = false;
      if (nullable.GetValueOrDefault() == flag51 & nullable.HasValue)
      {
        nullable = statusClosed;
        bool flag52 = false;
        if (nullable.GetValueOrDefault() == flag52 & nullable.HasValue)
          commandFilter.WhereAnd<Where<FSRouteDocument.status, Equal<ListField_Status_Route.Open>, Or<FSRouteDocument.status, Equal<ListField_Status_Route.InProcess>, Or<FSRouteDocument.status, Equal<ListField_Status_Route.Canceled>>>>>();
      }
    }
    if (statusOpen.GetValueOrDefault() && statusInProcess.GetValueOrDefault())
    {
      nullable = statusCanceled;
      bool flag53 = false;
      if (nullable.GetValueOrDefault() == flag53 & nullable.HasValue && statusCompleted.GetValueOrDefault())
      {
        nullable = statusClosed;
        bool flag54 = false;
        if (nullable.GetValueOrDefault() == flag54 & nullable.HasValue)
          commandFilter.WhereAnd<Where<FSRouteDocument.status, Equal<ListField_Status_Route.Open>, Or<FSRouteDocument.status, Equal<ListField_Status_Route.InProcess>, Or<FSRouteDocument.status, Equal<ListField_Status_Route.Completed>>>>>();
      }
    }
    if (statusOpen.GetValueOrDefault() && statusInProcess.GetValueOrDefault())
    {
      nullable = statusCanceled;
      bool flag55 = false;
      if (nullable.GetValueOrDefault() == flag55 & nullable.HasValue)
      {
        nullable = statusCompleted;
        bool flag56 = false;
        if (nullable.GetValueOrDefault() == flag56 & nullable.HasValue && statusClosed.GetValueOrDefault())
          commandFilter.WhereAnd<Where<FSRouteDocument.status, Equal<ListField_Status_Route.Open>, Or<FSRouteDocument.status, Equal<ListField_Status_Route.InProcess>, Or<FSRouteDocument.status, Equal<ListField_Status_Route.Closed>>>>>();
      }
    }
    if (statusOpen.GetValueOrDefault())
    {
      nullable = statusInProcess;
      bool flag57 = false;
      if (nullable.GetValueOrDefault() == flag57 & nullable.HasValue && statusCanceled.GetValueOrDefault() && statusCompleted.GetValueOrDefault())
      {
        nullable = statusClosed;
        bool flag58 = false;
        if (nullable.GetValueOrDefault() == flag58 & nullable.HasValue)
          commandFilter.WhereAnd<Where<FSRouteDocument.status, Equal<ListField_Status_Route.Open>, Or<FSRouteDocument.status, Equal<ListField_Status_Route.Canceled>, Or<FSRouteDocument.status, Equal<ListField_Status_Route.Completed>>>>>();
      }
    }
    if (statusOpen.GetValueOrDefault())
    {
      nullable = statusInProcess;
      bool flag59 = false;
      if (nullable.GetValueOrDefault() == flag59 & nullable.HasValue && statusCanceled.GetValueOrDefault())
      {
        nullable = statusCompleted;
        bool flag60 = false;
        if (nullable.GetValueOrDefault() == flag60 & nullable.HasValue && statusClosed.GetValueOrDefault())
          commandFilter.WhereAnd<Where<FSRouteDocument.status, Equal<ListField_Status_Route.Open>, Or<FSRouteDocument.status, Equal<ListField_Status_Route.Canceled>, Or<FSRouteDocument.status, Equal<ListField_Status_Route.Closed>>>>>();
      }
    }
    if (statusOpen.GetValueOrDefault())
    {
      nullable = statusInProcess;
      bool flag61 = false;
      if (nullable.GetValueOrDefault() == flag61 & nullable.HasValue)
      {
        nullable = statusCanceled;
        bool flag62 = false;
        if (nullable.GetValueOrDefault() == flag62 & nullable.HasValue && statusCompleted.GetValueOrDefault() && statusClosed.GetValueOrDefault())
          commandFilter.WhereAnd<Where<FSRouteDocument.status, Equal<ListField_Status_Route.Open>, Or<FSRouteDocument.status, Equal<ListField_Status_Route.Completed>, Or<FSRouteDocument.status, Equal<ListField_Status_Route.Closed>>>>>();
      }
    }
    nullable = statusOpen;
    bool flag63 = false;
    if (nullable.GetValueOrDefault() == flag63 & nullable.HasValue && statusInProcess.GetValueOrDefault() && statusCanceled.GetValueOrDefault() && statusCompleted.GetValueOrDefault())
    {
      nullable = statusClosed;
      bool flag64 = false;
      if (nullable.GetValueOrDefault() == flag64 & nullable.HasValue)
        commandFilter.WhereAnd<Where<FSRouteDocument.status, Equal<ListField_Status_Route.InProcess>, Or<FSRouteDocument.status, Equal<ListField_Status_Route.Canceled>, Or<FSRouteDocument.status, Equal<ListField_Status_Route.Completed>>>>>();
    }
    nullable = statusOpen;
    bool flag65 = false;
    if (nullable.GetValueOrDefault() == flag65 & nullable.HasValue && statusInProcess.GetValueOrDefault() && statusCanceled.GetValueOrDefault())
    {
      nullable = statusCompleted;
      bool flag66 = false;
      if (nullable.GetValueOrDefault() == flag66 & nullable.HasValue && statusClosed.GetValueOrDefault())
        commandFilter.WhereAnd<Where<FSRouteDocument.status, Equal<ListField_Status_Route.InProcess>, Or<FSRouteDocument.status, Equal<ListField_Status_Route.Canceled>, Or<FSRouteDocument.status, Equal<ListField_Status_Route.Closed>>>>>();
    }
    nullable = statusOpen;
    bool flag67 = false;
    if (nullable.GetValueOrDefault() == flag67 & nullable.HasValue && statusInProcess.GetValueOrDefault())
    {
      nullable = statusCanceled;
      bool flag68 = false;
      if (nullable.GetValueOrDefault() == flag68 & nullable.HasValue && statusCompleted.GetValueOrDefault() && statusClosed.GetValueOrDefault())
        commandFilter.WhereAnd<Where<FSRouteDocument.status, Equal<ListField_Status_Route.InProcess>, Or<FSRouteDocument.status, Equal<ListField_Status_Route.Completed>, Or<FSRouteDocument.status, Equal<ListField_Status_Route.Closed>>>>>();
    }
    nullable = statusOpen;
    bool flag69 = false;
    if (nullable.GetValueOrDefault() == flag69 & nullable.HasValue)
    {
      nullable = statusInProcess;
      bool flag70 = false;
      if (nullable.GetValueOrDefault() == flag70 & nullable.HasValue && statusCanceled.GetValueOrDefault() && statusCompleted.GetValueOrDefault() && statusClosed.GetValueOrDefault())
        commandFilter.WhereAnd<Where<FSRouteDocument.status, Equal<ListField_Status_Route.Canceled>, Or<FSRouteDocument.status, Equal<ListField_Status_Route.Completed>, Or<FSRouteDocument.status, Equal<ListField_Status_Route.Closed>>>>>();
    }
    if (statusOpen.GetValueOrDefault() && statusInProcess.GetValueOrDefault() && statusCanceled.GetValueOrDefault() && statusCompleted.GetValueOrDefault())
    {
      nullable = statusClosed;
      bool flag71 = false;
      if (nullable.GetValueOrDefault() == flag71 & nullable.HasValue)
        commandFilter.WhereAnd<Where<FSRouteDocument.status, Equal<ListField_Status_Route.Open>, Or<FSRouteDocument.status, Equal<ListField_Status_Route.InProcess>, Or<FSRouteDocument.status, Equal<ListField_Status_Route.Canceled>, Or<FSRouteDocument.status, Equal<ListField_Status_Route.Completed>>>>>>();
    }
    if (statusOpen.GetValueOrDefault())
    {
      nullable = statusInProcess;
      bool flag72 = false;
      if (nullable.GetValueOrDefault() == flag72 & nullable.HasValue && statusCanceled.GetValueOrDefault() && statusCompleted.GetValueOrDefault() && statusClosed.GetValueOrDefault())
        commandFilter.WhereAnd<Where<FSRouteDocument.status, Equal<ListField_Status_Route.Open>, Or<FSRouteDocument.status, Equal<ListField_Status_Route.Canceled>, Or<FSRouteDocument.status, Equal<ListField_Status_Route.Completed>, Or<FSRouteDocument.status, Equal<ListField_Status_Route.Closed>>>>>>();
    }
    if (statusOpen.GetValueOrDefault() && statusInProcess.GetValueOrDefault())
    {
      nullable = statusCanceled;
      bool flag73 = false;
      if (nullable.GetValueOrDefault() == flag73 & nullable.HasValue && statusCompleted.GetValueOrDefault() && statusClosed.GetValueOrDefault())
        commandFilter.WhereAnd<Where<FSRouteDocument.status, Equal<ListField_Status_Route.Open>, Or<FSRouteDocument.status, Equal<ListField_Status_Route.InProcess>, Or<FSRouteDocument.status, Equal<ListField_Status_Route.Completed>, Or<FSRouteDocument.status, Equal<ListField_Status_Route.Closed>>>>>>();
    }
    if (statusOpen.GetValueOrDefault() && statusInProcess.GetValueOrDefault() && statusCanceled.GetValueOrDefault())
    {
      nullable = statusCompleted;
      bool flag74 = false;
      if (nullable.GetValueOrDefault() == flag74 & nullable.HasValue && statusClosed.GetValueOrDefault())
        commandFilter.WhereAnd<Where<FSRouteDocument.status, Equal<ListField_Status_Route.Open>, Or<FSRouteDocument.status, Equal<ListField_Status_Route.InProcess>, Or<FSRouteDocument.status, Equal<ListField_Status_Route.Canceled>, Or<FSRouteDocument.status, Equal<ListField_Status_Route.Closed>>>>>>();
    }
    nullable = statusOpen;
    bool flag75 = false;
    if (nullable.GetValueOrDefault() == flag75 & nullable.HasValue && statusInProcess.GetValueOrDefault() && statusCanceled.GetValueOrDefault() && statusCompleted.GetValueOrDefault() && statusClosed.GetValueOrDefault())
      commandFilter.WhereAnd<Where<FSRouteDocument.status, Equal<ListField_Status_Route.InProcess>, Or<FSRouteDocument.status, Equal<ListField_Status_Route.Canceled>, Or<FSRouteDocument.status, Equal<ListField_Status_Route.Completed>, Or<FSRouteDocument.status, Equal<ListField_Status_Route.Closed>>>>>>();
    if (statusOpen.GetValueOrDefault() && statusInProcess.GetValueOrDefault() && statusCanceled.GetValueOrDefault() && statusCompleted.GetValueOrDefault() && statusClosed.GetValueOrDefault())
      commandFilter.WhereAnd<Where<FSRouteDocument.status, Equal<ListField_Status_Route.Open>, Or<FSRouteDocument.status, Equal<ListField_Status_Route.InProcess>, Or<FSRouteDocument.status, Equal<ListField_Status_Route.Canceled>, Or<FSRouteDocument.status, Equal<ListField_Status_Route.Completed>, Or<FSRouteDocument.status, Equal<ListField_Status_Route.Closed>>>>>>>();
    return (Type) null;
  }

  public virtual IEnumerable routeDocuments()
  {
    PXSelectJoin<FSRouteDocument, InnerJoin<FSRoute, On<FSRoute.routeID, Equal<FSRouteDocument.routeID>>>> commandFilter = new PXSelectJoin<FSRouteDocument, InnerJoin<FSRoute, On<FSRoute.routeID, Equal<FSRouteDocument.routeID>>>>((PXGraph) this);
    RouteDocumentFilter current = ((PXSelectBase<RouteDocumentFilter>) this.Filter).Current;
    if (current.FromDate.HasValue)
      ((PXSelectBase<FSRouteDocument>) commandFilter).WhereAnd<Where<FSRouteDocument.date, GreaterEqual<CurrentValue<RouteDocumentFilter.fromDate>>>>();
    if (current.ToDate.HasValue)
      ((PXSelectBase<FSRouteDocument>) commandFilter).WhereAnd<Where<FSRouteDocument.date, LessEqual<CurrentValue<RouteDocumentFilter.toDate>>>>();
    if (current.RouteID.HasValue)
      ((PXSelectBase<FSRouteDocument>) commandFilter).WhereAnd<Where<FSRouteDocument.routeID, Equal<CurrentValue<RouteDocumentFilter.routeID>>>>();
    bool? nullable = current.StatusOpen;
    if (!nullable.GetValueOrDefault())
    {
      nullable = current.StatusInProcess;
      if (!nullable.GetValueOrDefault())
      {
        nullable = current.StatusCanceled;
        if (!nullable.GetValueOrDefault())
        {
          nullable = current.StatusCompleted;
          if (!nullable.GetValueOrDefault())
          {
            nullable = current.StatusClosed;
            if (!nullable.GetValueOrDefault())
              goto label_12;
          }
        }
      }
    }
    this.SetWhereStatus(current.StatusOpen, current.StatusInProcess, current.StatusCanceled, current.StatusCompleted, current.StatusClosed, (PXSelectBase<FSRouteDocument>) commandFilter);
label_12:
    int startRow = PXView.StartRow;
    int num = 0;
    List<object> objectList = ((PXSelectBase) commandFilter).View.Select(PXView.Currents, PXView.Parameters, PXView.Searches, PXView.SortColumns, PXView.Descendings, PXView.PXFilterRowCollection.op_Implicit(PXView.Filters), ref startRow, PXView.MaximumRows, ref num);
    PXView.StartRow = 0;
    return (IEnumerable) objectList;
  }
}
