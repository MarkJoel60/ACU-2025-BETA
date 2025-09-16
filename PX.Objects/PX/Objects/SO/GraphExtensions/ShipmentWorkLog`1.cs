// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.GraphExtensions.ShipmentWorkLog`1
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using System;

#nullable disable
namespace PX.Objects.SO.GraphExtensions;

public class ShipmentWorkLog<TGraph> : PXGraphExtension<TGraph> where TGraph : PXGraph
{
  public FbqlSelect<SelectFromBase<SOShipmentProcessedByUser, TypeArrayOf<IFbqlJoin>.Empty>, SOShipmentProcessedByUser>.View ShipmentWorkLogView;

  public virtual SOShipmentProcessedByUser EnsureFor(
    string shipmentNbr,
    Guid? userID,
    [SOShipmentProcessedByUser.jobType.List] string jobType)
  {
    DateTime touchDateTime = this.GetServerTime();
    SOShipmentProcessedByUser shipmentProcessedByUser = this.Ensure(PXResultset<SOShipmentProcessedByUser>.op_Implicit(PXSelectBase<SOShipmentProcessedByUser, PXViewOf<SOShipmentProcessedByUser>.BasedOn<SelectFromBase<SOShipmentProcessedByUser, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<SOShipmentProcessedByUser.jobType, Equal<P.AsString.Fixed.ASCII>>>>, And<BqlOperand<SOShipmentProcessedByUser.shipmentNbr, IBqlString>.IsEqual<P.AsString>>>, And<BqlOperand<SOShipmentProcessedByUser.userID, IBqlGuid>.IsEqual<P.AsGuid>>>>.And<BqlOperand<SOShipmentProcessedByUser.endDateTime, IBqlDateTime>.IsNull>>>.Config>.Select((PXGraph) this.Base, new object[3]
    {
      (object) jobType,
      (object) shipmentNbr,
      (object) userID
    })), touchDateTime, new Func<DateTime?, SOShipmentProcessedByUser>(NewLink));
    foreach (PXResult<SOShipmentProcessedByUser> pxResult in PXSelectBase<SOShipmentProcessedByUser, PXViewOf<SOShipmentProcessedByUser>.BasedOn<SelectFromBase<SOShipmentProcessedByUser, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<SOShipmentProcessedByUser.shipmentNbr, NotEqual<P.AsString>>>>, And<BqlOperand<SOShipmentProcessedByUser.userID, IBqlGuid>.IsEqual<P.AsGuid>>>>.And<BqlOperand<SOShipmentProcessedByUser.endDateTime, IBqlDateTime>.IsNull>>>.Config>.Select((PXGraph) this.Base, new object[2]
    {
      (object) shipmentNbr,
      (object) userID
    }))
      this.Suspend(PXResult<SOShipmentProcessedByUser>.op_Implicit(pxResult), touchDateTime);
    return shipmentProcessedByUser;

    SOShipmentProcessedByUser NewLink(DateTime? groupStartDateTime)
    {
      return new SOShipmentProcessedByUser()
      {
        JobType = jobType,
        DocType = "SHPT",
        ShipmentNbr = shipmentNbr,
        UserID = userID,
        OverallStartDateTime = new DateTime?(groupStartDateTime ?? touchDateTime),
        StartDateTime = new DateTime?(touchDateTime),
        LastModifiedDateTime = new DateTime?(touchDateTime)
      };
    }
  }

  public virtual SOShipmentProcessedByUser EnsureFor(
    string worksheetNbr,
    int pickerNbr,
    Guid? userID,
    [SOShipmentProcessedByUser.jobType.List] string jobType)
  {
    DateTime touchDateTime = this.GetServerTime();
    SOShipmentProcessedByUser shipmentProcessedByUser = this.Ensure(PXResultset<SOShipmentProcessedByUser>.op_Implicit(PXSelectBase<SOShipmentProcessedByUser, PXViewOf<SOShipmentProcessedByUser>.BasedOn<SelectFromBase<SOShipmentProcessedByUser, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<SOShipmentProcessedByUser.jobType, Equal<P.AsString.Fixed.ASCII>>>>, And<BqlOperand<SOShipmentProcessedByUser.worksheetNbr, IBqlString>.IsEqual<P.AsString>>>, And<BqlOperand<SOShipmentProcessedByUser.pickerNbr, IBqlInt>.IsEqual<P.AsInt>>>, And<BqlOperand<SOShipmentProcessedByUser.userID, IBqlGuid>.IsEqual<P.AsGuid>>>>.And<BqlOperand<SOShipmentProcessedByUser.endDateTime, IBqlDateTime>.IsNull>>>.Config>.Select((PXGraph) this.Base, new object[4]
    {
      (object) jobType,
      (object) worksheetNbr,
      (object) pickerNbr,
      (object) userID
    })), touchDateTime, new Func<DateTime?, SOShipmentProcessedByUser>(NewLink));
    foreach (PXResult<SOShipmentProcessedByUser> pxResult in PXSelectBase<SOShipmentProcessedByUser, PXViewOf<SOShipmentProcessedByUser>.BasedOn<SelectFromBase<SOShipmentProcessedByUser, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<SOShipmentProcessedByUser.worksheetNbr, NotEqual<P.AsString>>>>, And<BqlOperand<SOShipmentProcessedByUser.pickerNbr, IBqlInt>.IsEqual<P.AsInt>>>, And<BqlOperand<SOShipmentProcessedByUser.userID, IBqlGuid>.IsEqual<P.AsGuid>>>>.And<BqlOperand<SOShipmentProcessedByUser.endDateTime, IBqlDateTime>.IsNull>>>.Config>.Select((PXGraph) this.Base, new object[3]
    {
      (object) worksheetNbr,
      (object) pickerNbr,
      (object) userID
    }))
      this.Suspend(PXResult<SOShipmentProcessedByUser>.op_Implicit(pxResult), touchDateTime);
    return shipmentProcessedByUser;

    SOShipmentProcessedByUser NewLink(DateTime? groupStartDateTime)
    {
      return new SOShipmentProcessedByUser()
      {
        JobType = jobType,
        DocType = "PLST",
        WorksheetNbr = worksheetNbr,
        PickerNbr = new int?(pickerNbr),
        UserID = userID,
        OverallStartDateTime = new DateTime?(groupStartDateTime ?? touchDateTime),
        StartDateTime = new DateTime?(touchDateTime),
        LastModifiedDateTime = new DateTime?(touchDateTime)
      };
    }
  }

  private SOShipmentProcessedByUser Ensure(
    SOShipmentProcessedByUser link,
    DateTime touchDateTime,
    Func<DateTime?, SOShipmentProcessedByUser> linkFactory)
  {
    TimeSpan pickingTimeOut = this.GetPickingTimeOut();
    if (link == null)
      return ((PXSelectBase<SOShipmentProcessedByUser>) this.ShipmentWorkLogView).Insert(linkFactory(new DateTime?(touchDateTime)));
    DateTime dateTime = link.LastModifiedDateTime.Value;
    if (dateTime.Add(pickingTimeOut) > touchDateTime)
    {
      SOShipmentProcessedByUser shipmentProcessedByUser = link;
      dateTime = link.LastModifiedDateTime.Value;
      DateTime? nullable = new DateTime?(dateTime.Add(pickingTimeOut));
      shipmentProcessedByUser.LastModifiedDateTime = nullable;
      return ((PXSelectBase<SOShipmentProcessedByUser>) this.ShipmentWorkLogView).Update(link);
    }
    SOShipmentProcessedByUser shipmentProcessedByUser1 = link;
    dateTime = link.LastModifiedDateTime.Value;
    DateTime? nullable1 = new DateTime?(dateTime.Add(pickingTimeOut));
    shipmentProcessedByUser1.EndDateTime = nullable1;
    ((PXSelectBase<SOShipmentProcessedByUser>) this.ShipmentWorkLogView).Update(link);
    return ((PXSelectBase<SOShipmentProcessedByUser>) this.ShipmentWorkLogView).Insert(linkFactory(link.OverallStartDateTime));
  }

  public virtual bool SuspendFor(string shipmentNbr, Guid? userID, [SOShipmentProcessedByUser.jobType.List] string jobType)
  {
    DateTime serverTime = this.GetServerTime();
    SOShipmentProcessedByUser link = PXResultset<SOShipmentProcessedByUser>.op_Implicit(PXSelectBase<SOShipmentProcessedByUser, PXViewOf<SOShipmentProcessedByUser>.BasedOn<SelectFromBase<SOShipmentProcessedByUser, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<SOShipmentProcessedByUser.jobType, Equal<P.AsString.Fixed.ASCII>>>>, And<BqlOperand<SOShipmentProcessedByUser.shipmentNbr, IBqlString>.IsEqual<P.AsString>>>, And<BqlOperand<SOShipmentProcessedByUser.userID, IBqlGuid>.IsEqual<P.AsGuid>>>>.And<BqlOperand<SOShipmentProcessedByUser.endDateTime, IBqlDateTime>.IsNull>>>.Config>.Select((PXGraph) this.Base, new object[3]
    {
      (object) jobType,
      (object) shipmentNbr,
      (object) userID
    }));
    if (link == null)
      return false;
    this.Suspend(link, serverTime);
    return true;
  }

  public virtual bool SuspendFor(string worksheetNbr, int pickerNbr, Guid? userID, [SOShipmentProcessedByUser.jobType.List] string jobType)
  {
    DateTime serverTime = this.GetServerTime();
    SOShipmentProcessedByUser link = PXResultset<SOShipmentProcessedByUser>.op_Implicit(PXSelectBase<SOShipmentProcessedByUser, PXViewOf<SOShipmentProcessedByUser>.BasedOn<SelectFromBase<SOShipmentProcessedByUser, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<SOShipmentProcessedByUser.jobType, Equal<P.AsString.Fixed.ASCII>>>>, And<BqlOperand<SOShipmentProcessedByUser.worksheetNbr, IBqlString>.IsEqual<P.AsString>>>, And<BqlOperand<SOShipmentProcessedByUser.pickerNbr, IBqlInt>.IsEqual<P.AsInt>>>, And<BqlOperand<SOShipmentProcessedByUser.userID, IBqlGuid>.IsEqual<P.AsGuid>>>>.And<BqlOperand<SOShipmentProcessedByUser.endDateTime, IBqlDateTime>.IsNull>>>.Config>.Select((PXGraph) this.Base, new object[4]
    {
      (object) jobType,
      (object) worksheetNbr,
      (object) pickerNbr,
      (object) userID
    }));
    if (link == null)
      return false;
    this.Suspend(link, serverTime);
    return true;
  }

  private void Suspend(SOShipmentProcessedByUser link, DateTime suspendDateTime)
  {
    int? numberOfScans = link.NumberOfScans;
    int num = 1;
    if (numberOfScans.GetValueOrDefault() <= num & numberOfScans.HasValue)
    {
      ((PXSelectBase<SOShipmentProcessedByUser>) this.ShipmentWorkLogView).Delete(link);
    }
    else
    {
      TimeSpan pickingTimeOut = this.GetPickingTimeOut();
      link.EndDateTime = new DateTime?(Tools.Min<DateTime>(suspendDateTime, link.LastModifiedDateTime.Value.Add(pickingTimeOut)));
      ((PXSelectBase<SOShipmentProcessedByUser>) this.ShipmentWorkLogView).Update(link);
    }
  }

  public virtual void CloseFor(string shipmentNbr)
  {
    DateTime serverTime = this.GetServerTime();
    foreach (PXResult<SOShipmentProcessedByUser> pxResult in PXSelectBase<SOShipmentProcessedByUser, PXViewOf<SOShipmentProcessedByUser>.BasedOn<SelectFromBase<SOShipmentProcessedByUser, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<SOShipmentProcessedByUser.shipmentNbr, IBqlString>.IsEqual<P.AsString>>>.Config>.Select((PXGraph) this.Base, new object[1]
    {
      (object) shipmentNbr
    }))
      this.Close(PXResult<SOShipmentProcessedByUser>.op_Implicit(pxResult), serverTime);
  }

  public virtual void CloseFor(string worksheetNbr, int pickerNbr)
  {
    DateTime serverTime = this.GetServerTime();
    foreach (PXResult<SOShipmentProcessedByUser> pxResult in PXSelectBase<SOShipmentProcessedByUser, PXViewOf<SOShipmentProcessedByUser>.BasedOn<SelectFromBase<SOShipmentProcessedByUser, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<SOShipmentProcessedByUser.worksheetNbr, Equal<P.AsString>>>>>.And<BqlOperand<SOShipmentProcessedByUser.pickerNbr, IBqlInt>.IsEqual<P.AsInt>>>>.Config>.Select((PXGraph) this.Base, new object[2]
    {
      (object) worksheetNbr,
      (object) pickerNbr
    }))
      this.Close(PXResult<SOShipmentProcessedByUser>.op_Implicit(pxResult), serverTime);
  }

  private void Close(SOShipmentProcessedByUser link, DateTime closeDateTime)
  {
    int? numberOfScans = link.NumberOfScans;
    int num = 1;
    if (numberOfScans.GetValueOrDefault() <= num & numberOfScans.HasValue)
    {
      ((PXSelectBase<SOShipmentProcessedByUser>) this.ShipmentWorkLogView).Delete(link);
    }
    else
    {
      TimeSpan pickingTimeOut = this.GetPickingTimeOut();
      link.Confirmed = new bool?(true);
      DateTime dateTime = Tools.Min<DateTime>(closeDateTime, link.LastModifiedDateTime.Value.Add(pickingTimeOut));
      if (!link.EndDateTime.HasValue)
        link.EndDateTime = new DateTime?(dateTime);
      link.OverallEndDateTime = new DateTime?(dateTime);
      ((PXSelectBase<SOShipmentProcessedByUser>) this.ShipmentWorkLogView).Update(link);
    }
  }

  public void LogScanFor(string shipmentNbr, Guid? userID, [SOShipmentProcessedByUser.jobType.List] string jobType, bool isError)
  {
    this.LogScan(this.EnsureFor(shipmentNbr, userID, jobType), isError);
  }

  public void LogScanFor(
    string worksheetNbr,
    int pickerNbr,
    Guid userID,
    [SOShipmentProcessedByUser.jobType.List] string jobType,
    bool isError)
  {
    this.LogScan(this.EnsureFor(worksheetNbr, pickerNbr, new Guid?(userID), jobType), isError);
  }

  protected void LogScan(SOShipmentProcessedByUser link, bool isError)
  {
    SOShipmentProcessedByUser shipmentProcessedByUser1 = link;
    int? nullable = shipmentProcessedByUser1.NumberOfScans;
    shipmentProcessedByUser1.NumberOfScans = nullable.HasValue ? new int?(nullable.GetValueOrDefault() + 1) : new int?();
    if (isError)
    {
      SOShipmentProcessedByUser shipmentProcessedByUser2 = link;
      nullable = shipmentProcessedByUser2.NumberOfFailedScans;
      shipmentProcessedByUser2.NumberOfFailedScans = nullable.HasValue ? new int?(nullable.GetValueOrDefault() + 1) : new int?();
    }
    ((PXSelectBase<SOShipmentProcessedByUser>) this.ShipmentWorkLogView).Update(link);
  }

  public virtual void PersistWorkLog()
  {
    using (PXTransactionScope transactionScope = new PXTransactionScope())
    {
      ((PXSelectBase) this.ShipmentWorkLogView).Cache.Persist((PXDBOperation) 2);
      ((PXSelectBase) this.ShipmentWorkLogView).Cache.Persist((PXDBOperation) 1);
      ((PXSelectBase) this.ShipmentWorkLogView).Cache.Persist((PXDBOperation) 3);
      transactionScope.Complete((PXGraph) this.Base);
    }
    ((PXSelectBase) this.ShipmentWorkLogView).Cache.Persisted(false);
  }

  protected virtual TimeSpan GetPickingTimeOut() => TimeSpan.FromMinutes(10.0);

  protected virtual DateTime GetServerTime()
  {
    DateTime dateTime1;
    DateTime dateTime2;
    PXDatabase.SelectDate(ref dateTime1, ref dateTime2);
    return PXTimeZoneInfo.ConvertTimeFromUtc(dateTime2, LocaleInfo.GetTimeZone());
  }
}
