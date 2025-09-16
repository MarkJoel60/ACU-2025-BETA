// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.RouteSequenceMaint
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Objects.CR;
using System;
using System.Collections;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.FS;

public class RouteSequenceMaint : PXGraph<RouteSequenceMaint>
{
  public PXFilter<ServiceContractsByRouteFilter> Filter;
  public PXCancel<ServiceContractsByRouteFilter> Cancel;
  public PXSave<ServiceContractsByRouteFilter> Save;
  public PXSelect<BAccount> bAccountRecords;
  [PXHidden]
  public PXSelect<FSServiceContract> ServiceContractsDummy;
  [PXHidden]
  public PXSelect<FSSchedule> SchedulesDummy;
  [PXHidden]
  public PXSelect<PX.Objects.CR.Location> LocationDummy;
  [PXHidden]
  public PXSelect<PX.Objects.CR.Address> AddressDummy;
  public PXSelectOrderBy<FSScheduleRoute, OrderBy<Asc<FSScheduleRoute.sortingIndex, Asc<FSScheduleRoute.scheduleID>>>> ServiceContracts;
  public PXAction<ServiceContractsByRouteFilter> resequence;
  public PXAction<ServiceContractsByRouteFilter> openContract;
  public PXAction<ServiceContractsByRouteFilter> openSchedule;

  [PXDBString(15, IsUnicode = true, IsKey = true, InputMask = ">CCCCCCCCCCCCCCC")]
  [PXUIField]
  [PXSelector(typeof (Search3<FSSchedule.refNbr, OrderBy<Desc<FSSchedule.refNbr>>>))]
  protected virtual void FSSchedule_RefNbr_CacheAttached(PXCache sender)
  {
  }

  [PXDBString(1, IsFixed = true)]
  [ListField_Status_ServiceContract.ListAtrribute]
  [PXUIField]
  protected virtual void FSServiceContract_Status_CacheAttached(PXCache sender)
  {
  }

  [PXDBString(50, IsUnicode = true)]
  [PXUIField(DisplayName = "Address")]
  protected virtual void Address_AddressLine1_CacheAttached(PXCache sender)
  {
  }

  [PXUIField]
  public virtual void OpenContract()
  {
    RouteServiceContractEntry instance = PXGraph.CreateInstance<RouteServiceContractEntry>();
    FSServiceContract fsServiceContract = PXResultset<FSServiceContract>.op_Implicit(PXSelectBase<FSServiceContract, PXSelectJoin<FSServiceContract, InnerJoin<FSSchedule, On<FSSchedule.entityID, Equal<FSServiceContract.serviceContractID>, And<FSSchedule.customerID, Equal<FSServiceContract.customerID>>>>, Where<FSSchedule.scheduleID, Equal<Required<FSSchedule.scheduleID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) ((PXSelectBase<FSScheduleRoute>) this.ServiceContracts).Current.ScheduleID
    }));
    ((PXSelectBase<FSServiceContract>) instance.ServiceContractRecords).Current = fsServiceContract;
    PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, (string) null);
    ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 3;
    throw requiredException;
  }

  [PXUIField]
  public virtual void OpenSchedule()
  {
    RouteServiceContractScheduleEntry instance = PXGraph.CreateInstance<RouteServiceContractScheduleEntry>();
    FSRouteContractSchedule contractSchedule = PXResultset<FSRouteContractSchedule>.op_Implicit(PXSelectBase<FSRouteContractSchedule, PXSelect<FSRouteContractSchedule, Where<FSSchedule.scheduleID, Equal<Required<FSSchedule.scheduleID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) ((PXSelectBase<FSScheduleRoute>) this.ServiceContracts).Current.ScheduleID
    }));
    ((PXSelectBase<FSRouteContractSchedule>) instance.ContractScheduleRecords).Current = contractSchedule;
    PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, (string) null);
    ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 3;
    throw requiredException;
  }

  [PXUIField]
  public virtual void Resequence()
  {
    int num = 10;
    foreach (PXResult<FSScheduleRoute> pxResult in ((PXSelectBase<FSScheduleRoute>) this.ServiceContracts).Select(Array.Empty<object>()))
    {
      PXResult<FSScheduleRoute>.op_Implicit(pxResult).GlobalSequence = num.ToString().PadLeft(5, '0');
      num += 10;
    }
    ((PXAction) this.Save).Press();
    ((PXSelectBase) this.ServiceContracts).View.RequestRefresh();
  }

  public virtual IEnumerable serviceContracts()
  {
    PXResultset<FSScheduleRoute> pxResultset = new PXResultset<FSScheduleRoute>();
    List<object> objectList1 = new List<object>();
    if (!((PXSelectBase<ServiceContractsByRouteFilter>) this.Filter).Current.RouteID.HasValue || ((PXSelectBase<ServiceContractsByRouteFilter>) this.Filter).Current.WeekDay == null)
      return (IEnumerable) pxResultset;
    PXSelectBase<FSScheduleRoute> pxSelectBase = (PXSelectBase<FSScheduleRoute>) new PXSelectJoin<FSScheduleRoute, InnerJoin<FSSchedule, On<FSSchedule.scheduleID, Equal<FSScheduleRoute.scheduleID>>, InnerJoin<FSServiceContract, On<FSServiceContract.serviceContractID, Equal<FSSchedule.entityID>, And<FSSchedule.entityType, Equal<ListField_Schedule_EntityType.Contract>>>, LeftJoin<PX.Objects.CR.Location, On<PX.Objects.CR.Location.bAccountID, Equal<FSServiceContract.customerID>, And<PX.Objects.CR.Location.locationID, Equal<FSServiceContract.customerLocationID>>>, LeftJoin<PX.Objects.CR.Address, On<PX.Objects.CR.Address.bAccountID, Equal<PX.Objects.CR.Location.bAccountID>, And<PX.Objects.CR.Address.addressID, Equal<PX.Objects.CR.Location.defAddressID>>>>>>>>((PXGraph) this);
    if (((PXSelectBase<ServiceContractsByRouteFilter>) this.Filter).Current.ScheduleFlag.GetValueOrDefault())
    {
      pxSelectBase.WhereAnd<Where<FSSchedule.active, Equal<Required<FSSchedule.active>>>>();
      objectList1.Add((object) ((PXSelectBase<ServiceContractsByRouteFilter>) this.Filter).Current.ScheduleFlag);
    }
    if (((PXSelectBase<ServiceContractsByRouteFilter>) this.Filter).Current.ServiceContractFlag.GetValueOrDefault())
    {
      pxSelectBase.WhereAnd<Where<FSServiceContract.status, Equal<Required<FSServiceContract.status>>>>();
      objectList1.Add((object) "A");
    }
    string weekDay = ((PXSelectBase<ServiceContractsByRouteFilter>) this.Filter).Current.WeekDay;
    bool flag;
    if (weekDay != null && weekDay.Length == 2)
    {
      switch (weekDay[1])
      {
        case 'A':
          if (weekDay == "SA")
          {
            pxSelectBase.WhereAnd<Where<FSScheduleRoute.routeIDSaturday, Equal<Required<FSScheduleRoute.routeIDSaturday>>>>();
            flag = true;
            goto label_24;
          }
          break;
        case 'E':
          if (weekDay == "WE")
          {
            pxSelectBase.WhereAnd<Where<FSScheduleRoute.routeIDWednesday, Equal<Required<FSScheduleRoute.routeIDWednesday>>>>();
            flag = true;
            goto label_24;
          }
          break;
        case 'H':
          if (weekDay == "TH")
          {
            pxSelectBase.WhereAnd<Where<FSScheduleRoute.routeIDThursday, Equal<Required<FSScheduleRoute.routeIDThursday>>>>();
            flag = true;
            goto label_24;
          }
          break;
        case 'O':
          if (weekDay == "MO")
          {
            pxSelectBase.WhereAnd<Where<FSScheduleRoute.routeIDMonday, Equal<Required<FSScheduleRoute.routeIDMonday>>>>();
            flag = true;
            goto label_24;
          }
          break;
        case 'R':
          if (weekDay == "FR")
          {
            pxSelectBase.WhereAnd<Where<FSScheduleRoute.routeIDFriday, Equal<Required<FSScheduleRoute.routeIDFriday>>>>();
            flag = true;
            goto label_24;
          }
          break;
        case 'T':
          if (weekDay == "NT")
          {
            pxSelectBase.WhereAnd<Where<FSScheduleRoute.dfltRouteID, Equal<Required<FSScheduleRoute.dfltRouteID>>>>();
            flag = true;
            goto label_24;
          }
          break;
        case 'U':
          switch (weekDay)
          {
            case "SU":
              pxSelectBase.WhereAnd<Where<FSScheduleRoute.routeIDSunday, Equal<Required<FSScheduleRoute.routeIDSunday>>>>();
              flag = true;
              goto label_24;
            case "TU":
              pxSelectBase.WhereAnd<Where<FSScheduleRoute.routeIDTuesday, Equal<Required<FSScheduleRoute.routeIDTuesday>>>>();
              flag = true;
              goto label_24;
          }
          break;
      }
    }
    pxSelectBase.WhereAnd<Where<FSScheduleRoute.dfltRouteID, Equal<Required<FSScheduleRoute.dfltRouteID>>>>();
    flag = true;
label_24:
    if (flag)
      objectList1.Add((object) ((PXSelectBase<ServiceContractsByRouteFilter>) this.Filter).Current.RouteID);
    int startRow = PXView.StartRow;
    int num = 0;
    List<object> objectList2 = ((PXSelectBase) pxSelectBase).View.Select(PXView.Currents, objectList1.ToArray(), PXView.Searches, PXView.SortColumns, PXView.Descendings, PXView.PXFilterRowCollection.op_Implicit(PXView.Filters), ref startRow, PXView.MaximumRows, ref num);
    PXView.StartRow = 0;
    return (IEnumerable) objectList2;
  }

  protected virtual void _(
    PX.Data.Events.RowSelected<ServiceContractsByRouteFilter> e)
  {
    if (e.Row == null)
      return;
    foreach (PXResult<FSScheduleRoute> pxResult in ((PXSelectBase<FSScheduleRoute>) this.ServiceContracts).Select(Array.Empty<object>()))
    {
      FSScheduleRoute fsScheduleRoute = PXResult<FSScheduleRoute>.op_Implicit(pxResult);
      string weekDay = ((PXSelectBase<ServiceContractsByRouteFilter>) this.Filter).Current.WeekDay;
      if (weekDay != null && weekDay.Length == 2)
      {
        switch (weekDay[1])
        {
          case 'A':
            if (weekDay == "SA")
            {
              int? sortingIndex = fsScheduleRoute.SortingIndex;
              int num = int.Parse(fsScheduleRoute.SequenceSaturday);
              if (!(sortingIndex.GetValueOrDefault() == num & sortingIndex.HasValue))
              {
                fsScheduleRoute.SortingIndex = new int?(int.Parse(fsScheduleRoute.SequenceSaturday));
                ((PXSelectBase<FSScheduleRoute>) this.ServiceContracts).Update(fsScheduleRoute);
                continue;
              }
              continue;
            }
            continue;
          case 'E':
            if (weekDay == "WE")
            {
              int? sortingIndex = fsScheduleRoute.SortingIndex;
              int num = int.Parse(fsScheduleRoute.SequenceWednesday);
              if (!(sortingIndex.GetValueOrDefault() == num & sortingIndex.HasValue))
              {
                fsScheduleRoute.SortingIndex = new int?(int.Parse(fsScheduleRoute.SequenceWednesday));
                ((PXSelectBase<FSScheduleRoute>) this.ServiceContracts).Update(fsScheduleRoute);
                continue;
              }
              continue;
            }
            continue;
          case 'H':
            if (weekDay == "TH")
            {
              int? sortingIndex = fsScheduleRoute.SortingIndex;
              int num = int.Parse(fsScheduleRoute.SequenceThursday);
              if (!(sortingIndex.GetValueOrDefault() == num & sortingIndex.HasValue))
              {
                fsScheduleRoute.SortingIndex = new int?(int.Parse(fsScheduleRoute.SequenceThursday));
                ((PXSelectBase<FSScheduleRoute>) this.ServiceContracts).Update(fsScheduleRoute);
                continue;
              }
              continue;
            }
            continue;
          case 'O':
            if (weekDay == "MO")
            {
              int? sortingIndex = fsScheduleRoute.SortingIndex;
              int num = int.Parse(fsScheduleRoute.SequenceMonday);
              if (!(sortingIndex.GetValueOrDefault() == num & sortingIndex.HasValue))
              {
                fsScheduleRoute.SortingIndex = new int?(int.Parse(fsScheduleRoute.SequenceMonday));
                ((PXSelectBase<FSScheduleRoute>) this.ServiceContracts).Update(fsScheduleRoute);
                continue;
              }
              continue;
            }
            continue;
          case 'R':
            if (weekDay == "FR")
            {
              int? sortingIndex = fsScheduleRoute.SortingIndex;
              int num = int.Parse(fsScheduleRoute.SequenceFriday);
              if (!(sortingIndex.GetValueOrDefault() == num & sortingIndex.HasValue))
              {
                fsScheduleRoute.SortingIndex = new int?(int.Parse(fsScheduleRoute.SequenceFriday));
                ((PXSelectBase<FSScheduleRoute>) this.ServiceContracts).Update(fsScheduleRoute);
                continue;
              }
              continue;
            }
            continue;
          case 'T':
            if (weekDay == "NT")
            {
              int? sortingIndex = fsScheduleRoute.SortingIndex;
              int num = int.Parse(fsScheduleRoute.GlobalSequence);
              if (!(sortingIndex.GetValueOrDefault() == num & sortingIndex.HasValue))
              {
                fsScheduleRoute.SortingIndex = new int?(int.Parse(fsScheduleRoute.GlobalSequence));
                ((PXSelectBase<FSScheduleRoute>) this.ServiceContracts).Update(fsScheduleRoute);
                continue;
              }
              continue;
            }
            continue;
          case 'U':
            switch (weekDay)
            {
              case "SU":
                int? sortingIndex1 = fsScheduleRoute.SortingIndex;
                int num1 = int.Parse(fsScheduleRoute.SequenceSunday);
                if (!(sortingIndex1.GetValueOrDefault() == num1 & sortingIndex1.HasValue))
                {
                  fsScheduleRoute.SortingIndex = new int?(int.Parse(fsScheduleRoute.SequenceSunday));
                  ((PXSelectBase<FSScheduleRoute>) this.ServiceContracts).Update(fsScheduleRoute);
                  continue;
                }
                continue;
              case "TU":
                int? sortingIndex2 = fsScheduleRoute.SortingIndex;
                int num2 = int.Parse(fsScheduleRoute.SequenceTuesday);
                if (!(sortingIndex2.GetValueOrDefault() == num2 & sortingIndex2.HasValue))
                {
                  fsScheduleRoute.SortingIndex = new int?(int.Parse(fsScheduleRoute.SequenceTuesday));
                  ((PXSelectBase<FSScheduleRoute>) this.ServiceContracts).Update(fsScheduleRoute);
                  continue;
                }
                continue;
              default:
                continue;
            }
          default:
            continue;
        }
      }
    }
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<FSScheduleRoute, FSScheduleRoute.globalSequence> e)
  {
    if (e.Row == null)
      return;
    FSScheduleRoute row = e.Row;
    row.GlobalSequence = row.GlobalSequence.PadLeft(5, '0');
    ((PXSelectBase) this.ServiceContracts).View.RequestRefresh();
  }

  protected virtual void _(PX.Data.Events.RowSelecting<FSScheduleRoute> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowSelected<FSScheduleRoute> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowInserting<FSScheduleRoute> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowInserted<FSScheduleRoute> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowUpdating<FSScheduleRoute> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowUpdated<FSScheduleRoute> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowDeleting<FSScheduleRoute> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowDeleted<FSScheduleRoute> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowPersisting<FSScheduleRoute> e)
  {
    if (e.Row == null)
      return;
    FSScheduleRoute row = e.Row;
    row.GlobalSequence = row.GlobalSequence.PadLeft(5, '0');
  }

  protected virtual void _(PX.Data.Events.RowPersisted<FSScheduleRoute> e)
  {
  }
}
