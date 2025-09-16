// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.TimeSlotMaint
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Objects.FS.Scheduler;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.FS;

public class TimeSlotMaint : PXGraph<TimeSlotMaint, FSTimeSlot>
{
  public PXSelect<FSTimeSlot> TimeSlotRecords;

  /// <summary>Converts a FSTimeSlot to a generic class Slot.</summary>
  public virtual Slot ConvertFSTimeSlotToSlot(FSTimeSlot fsTimeSlotRow)
  {
    return new Slot()
    {
      SlotType = fsTimeSlotRow.ScheduleType,
      DateTimeBegin = fsTimeSlotRow.TimeStart.Value,
      DateTimeEnd = fsTimeSlotRow.TimeEnd.Value
    };
  }

  /// <summary>
  /// Converts a generic Slot in a FSTimeSlot based in [fsTimeSlotRow] and [slotLevel].
  /// </summary>
  public virtual FSTimeSlot ConvertSlotToFSTimeSlot(
    Slot slot,
    FSTimeSlot fsTimeSlotRow,
    int slotLevel)
  {
    FSTimeSlot fsTimeSlot = new FSTimeSlot();
    TimeSpan timeSpan = slot.DateTimeEnd - slot.DateTimeBegin;
    fsTimeSlot.BranchID = fsTimeSlotRow.BranchID;
    fsTimeSlot.BranchLocationID = fsTimeSlotRow.BranchLocationID;
    fsTimeSlot.EmployeeID = fsTimeSlotRow.EmployeeID;
    fsTimeSlot.TimeStart = new DateTime?(slot.DateTimeBegin);
    fsTimeSlot.TimeEnd = new DateTime?(slot.DateTimeEnd);
    fsTimeSlot.ScheduleType = slot.SlotType;
    fsTimeSlot.TimeDiff = new Decimal?((Decimal) timeSpan.TotalMinutes);
    fsTimeSlot.RecordCount = new int?(1);
    fsTimeSlot.SlotLevel = new int?(slotLevel);
    return fsTimeSlot;
  }

  /// <summary>
  /// Compress a List of Slots in unique slots without overlapping. (The [slotList] must be ordered by TimeBegin and the Slots in [slotList] must be part of same type of Availability).
  /// </summary>
  public virtual List<Slot> CompressListOfSlots(List<Slot> slotList, string availability)
  {
    if (slotList.Count == 0)
      return slotList;
    List<Slot> slotList1 = new List<Slot>();
    DateTime? nullable1 = new DateTime?();
    DateTime? nullable2 = new DateTime?();
    foreach (Slot slot in slotList)
    {
      if (!nullable1.HasValue && !nullable2.HasValue)
      {
        nullable1 = new DateTime?(slot.DateTimeBegin);
        nullable2 = new DateTime?(slot.DateTimeEnd);
      }
      else
      {
        DateTime dateTimeBegin = slot.DateTimeBegin;
        DateTime? nullable3 = nullable2;
        if ((nullable3.HasValue ? (dateTimeBegin > nullable3.GetValueOrDefault() ? 1 : 0) : 0) != 0)
        {
          slotList1.Add(new Slot()
          {
            DateTimeBegin = nullable1.Value,
            DateTimeEnd = nullable2.Value,
            SlotType = availability
          });
          nullable1 = new DateTime?(slot.DateTimeBegin);
          nullable2 = new DateTime?(slot.DateTimeEnd);
        }
        else
        {
          DateTime dateTimeEnd = slot.DateTimeEnd;
          DateTime? nullable4 = nullable2;
          if ((nullable4.HasValue ? (dateTimeEnd > nullable4.GetValueOrDefault() ? 1 : 0) : 0) != 0)
            nullable2 = new DateTime?(slot.DateTimeEnd);
        }
      }
    }
    slotList1.Add(new Slot()
    {
      DateTimeBegin = nullable1.Value,
      DateTimeEnd = nullable2.Value,
      SlotType = availability
    });
    return slotList1;
  }

  /// <summary>
  /// Compress the Availability and Unavailability lists and then proceed to intersect them. This function returns the intersection and the Unavailability records compressed as a List.
  /// </summary>
  public virtual List<Slot> CompressAndIntersectSlots(List<Slot> slotList)
  {
    List<Slot> list1 = slotList.Select<Slot, Slot>((Func<Slot, Slot>) (x => x)).Where<Slot>((Func<Slot, bool>) (y => y.SlotType == "U")).OrderBy<Slot, DateTime>((Func<Slot, DateTime>) (z => z.DateTimeBegin)).ToList<Slot>();
    List<Slot> list2 = slotList.Select<Slot, Slot>((Func<Slot, Slot>) (x => x)).Where<Slot>((Func<Slot, bool>) (y => y.SlotType == "A")).OrderBy<Slot, DateTime>((Func<Slot, DateTime>) (z => z.DateTimeBegin)).ToList<Slot>();
    List<Slot> slotList1 = new List<Slot>();
    List<Slot> slotList2 = new List<Slot>();
    List<Slot> first = new List<Slot>();
    bool flag = false;
    List<Slot> second = this.CompressListOfSlots(list1, "U");
    foreach (Slot compressListOfSlot in this.CompressListOfSlots(list2, "A"))
    {
      DateTime dateTime1 = compressListOfSlot.DateTimeBegin;
      DateTime dateTime2 = compressListOfSlot.DateTimeEnd;
      foreach (Slot slot in second)
      {
        int num = (int) this.SlotIsContainedInSlot(new DateTime?(dateTime1), new DateTime?(dateTime2), new DateTime?(slot.DateTimeBegin), new DateTime?(slot.DateTimeEnd));
        if (num == 2)
        {
          if (dateTime1 != slot.DateTimeBegin)
            first.Add(new Slot()
            {
              DateTimeBegin = dateTime1,
              DateTimeEnd = slot.DateTimeBegin,
              SlotType = "A"
            });
          dateTime1 = slot.DateTimeEnd;
        }
        if (num == 3 && slot.DateTimeBegin < dateTime1)
          dateTime1 = slot.DateTimeEnd;
        if (num == 3 && slot.DateTimeBegin < dateTime2 && dateTime1 < slot.DateTimeBegin)
          dateTime2 = slot.DateTimeBegin;
        if (num == 4)
        {
          flag = true;
          break;
        }
      }
      if (!flag && dateTime1 != dateTime2)
        first.Add(new Slot()
        {
          DateTimeBegin = dateTime1,
          DateTimeEnd = dateTime2,
          SlotType = "A"
        });
      flag = false;
    }
    return first.Concat<Slot>((IEnumerable<Slot>) second).ToList<Slot>().OrderBy<Slot, DateTime>((Func<Slot, DateTime>) (x => x.DateTimeBegin)).ToList<Slot>();
  }

  /// <summary>
  /// Creates the compressed Time Slots that applies for the [fsTimeSlotRow].
  /// </summary>
  public virtual void CreateAndCompressedTimeSlots(
    FSTimeSlot fsTimeSlotRow,
    TimeSlotMaint timeSlotMaintGraph)
  {
    List<object> objectList = new List<object>();
    List<FSTimeSlot> fsTimeSlotList = new List<FSTimeSlot>();
    List<Slot> slotList1 = new List<Slot>();
    PXResultset<FSBranchLocation> pxResultset = PXSelectBase<FSBranchLocation, PXSelect<FSBranchLocation, Where<FSBranchLocation.branchID, Equal<Required<FSBranchLocation.branchID>>>, OrderBy<Asc<FSBranchLocation.branchLocationID>>>.Config>.Select((PXGraph) timeSlotMaintGraph, new object[1]
    {
      (object) fsTimeSlotRow.BranchID
    });
    DateHandler dateHandler = new DateHandler(SharedFunctions.TryParseHandlingDateTime(((PXSelectBase) timeSlotMaintGraph.TimeSlotRecords).Cache, (object) fsTimeSlotRow.TimeStart));
    PXSelectBase<FSTimeSlot> pxSelectBase = (PXSelectBase<FSTimeSlot>) new PXSelect<FSTimeSlot, Where<FSTimeSlot.branchID, Equal<Required<FSTimeSlot.branchID>>, And<FSTimeSlot.employeeID, Equal<Required<FSTimeSlot.employeeID>>, And<FSTimeSlot.slotLevel, Equal<Required<FSTimeSlot.slotLevel>>, And<FSTimeSlot.timeStart, GreaterEqual<Required<FSTimeSlot.timeStart>>, And<FSTimeSlot.timeStart, LessEqual<Required<FSTimeSlot.timeStart>>>>>>>>((PXGraph) timeSlotMaintGraph);
    objectList.Add((object) fsTimeSlotRow.BranchID);
    objectList.Add((object) fsTimeSlotRow.EmployeeID);
    objectList.Add((object) 0);
    objectList.Add((object) dateHandler.StartOfDay());
    objectList.Add((object) dateHandler.EndOfDay());
    if (fsTimeSlotRow.BranchLocationID.HasValue)
    {
      pxSelectBase.WhereAnd<Where<FSTimeSlot.branchLocationID, Equal<Required<FSTimeSlot.branchLocationID>>>>();
      objectList.Add((object) fsTimeSlotRow.BranchLocationID);
    }
    foreach (PXResult<FSTimeSlot> pxResult in pxSelectBase.Select(objectList.ToArray()))
    {
      FSTimeSlot fsTimeSlot = PXResult<FSTimeSlot>.op_Implicit(pxResult);
      fsTimeSlotList.Add(fsTimeSlot);
    }
    List<\u003C\u003Ef__AnonymousType17<int?, List<FSTimeSlot>>> list1 = fsTimeSlotList.GroupBy<FSTimeSlot, int?>((Func<FSTimeSlot, int?>) (u => u.BranchLocationID.HasValue ? u.BranchLocationID : new int?(-1))).Select(group => new
    {
      BranchLocationID = group.Key,
      FSTimeSlots = group.ToList<FSTimeSlot>()
    }).OrderBy(group => group.BranchLocationID).ToList();
    List<Slot> slotList2 = new List<Slot>();
    foreach (PXResult<FSBranchLocation> pxResult in pxResultset)
    {
      FSBranchLocation fsBranchLocationRow = PXResult<FSBranchLocation>.op_Implicit(pxResult);
      List<\u003C\u003Ef__AnonymousType18<int?, List<FSTimeSlot>>> list2 = list1.Select(bl => new
      {
        BranchLocationID = bl.BranchLocationID,
        FSTimeSlot = bl.FSTimeSlots.ToList<FSTimeSlot>()
      }).Where(S =>
      {
        int? branchLocationId1 = S.BranchLocationID;
        int? branchLocationId2 = fsBranchLocationRow.BranchLocationID;
        return branchLocationId1.GetValueOrDefault() == branchLocationId2.GetValueOrDefault() & branchLocationId1.HasValue == branchLocationId2.HasValue;
      }).ToList();
      fsTimeSlotList.Clear();
      slotList1.Clear();
      if (list2.Count > 0)
        fsTimeSlotList = list2[0].FSTimeSlot;
      if (list1.Count > 0 && list1[0].BranchLocationID.GetValueOrDefault() == -1)
        fsTimeSlotList = fsTimeSlotList.Concat<FSTimeSlot>((IEnumerable<FSTimeSlot>) list1[0].FSTimeSlots).ToList<FSTimeSlot>();
      foreach (FSTimeSlot fsTimeSlotRow1 in fsTimeSlotList)
        slotList1.Add(this.ConvertFSTimeSlotToSlot(fsTimeSlotRow1));
      if (slotList1.Count > 0)
      {
        fsTimeSlotRow.BranchLocationID = fsBranchLocationRow.BranchLocationID;
        this.CreateCompressedSlots(this.CompressAndIntersectSlots(slotList1).ToList<Slot>(), fsTimeSlotRow);
      }
    }
  }

  /// <summary>
  /// Create FSTimeSlot records based in the List [compressedSlots] and the FSTimeSLot record [fsTimeSlotRow].
  /// </summary>
  public virtual void CreateCompressedSlots(List<Slot> compressedSlots, FSTimeSlot fsTimeSlotRow)
  {
    TimeSlotMaint instance = PXGraph.CreateInstance<TimeSlotMaint>();
    foreach (Slot compressedSlot in compressedSlots)
      ((PXSelectBase<FSTimeSlot>) instance.TimeSlotRecords).Insert(this.ConvertSlotToFSTimeSlot(compressedSlot, fsTimeSlotRow, 1));
    ((PXAction) instance.Save).Press();
  }

  /// <summary>
  /// Delete the TimeSlots based in [slotLevel] that applies for the [fsTimeSlotRow].
  /// </summary>
  public virtual void DeleteTimeSlotsByLevel(FSTimeSlot fsTimeSlotRow, int slotLevel)
  {
    using (PXTransactionScope transactionScope = new PXTransactionScope())
    {
      List<object> objectList = new List<object>();
      TimeSlotMaint instance = PXGraph.CreateInstance<TimeSlotMaint>();
      DateHandler dateHandler = new DateHandler(Convert.ToDateTime((object) fsTimeSlotRow.TimeStart));
      PXSelectBase<FSTimeSlot> pxSelectBase = (PXSelectBase<FSTimeSlot>) new PXSelect<FSTimeSlot, Where<FSTimeSlot.branchID, Equal<Required<FSTimeSlot.branchID>>, And<FSTimeSlot.employeeID, Equal<Required<FSTimeSlot.employeeID>>, And<FSTimeSlot.slotLevel, Equal<Required<FSTimeSlot.slotLevel>>, And<FSTimeSlot.timeStart, GreaterEqual<Required<FSTimeSlot.timeStart>>, And<FSTimeSlot.timeStart, LessEqual<Required<FSTimeSlot.timeStart>>>>>>>>((PXGraph) instance);
      objectList.Add((object) fsTimeSlotRow.BranchID);
      objectList.Add((object) fsTimeSlotRow.EmployeeID);
      objectList.Add((object) slotLevel);
      objectList.Add((object) dateHandler.StartOfDay());
      objectList.Add((object) dateHandler.EndOfDay());
      if (fsTimeSlotRow.BranchLocationID.HasValue)
      {
        pxSelectBase.WhereAnd<Where<FSTimeSlot.branchLocationID, Equal<Required<FSTimeSlot.branchLocationID>>>>();
        objectList.Add((object) fsTimeSlotRow.BranchLocationID);
      }
      foreach (PXResult<FSTimeSlot> pxResult in pxSelectBase.Select(objectList.ToArray()))
      {
        FSTimeSlot fsTimeSlot = PXResult<FSTimeSlot>.op_Implicit(pxResult);
        ((PXSelectBase<FSTimeSlot>) instance.TimeSlotRecords).Delete(fsTimeSlot);
      }
      ((PXAction) instance.Save).Press();
      transactionScope.Complete();
    }
  }

  public virtual AppointmentEntry.SlotIsContained SlotIsContainedInSlot(
    DateTime? slotBegin,
    DateTime? slotEnd,
    DateTime? beginTime,
    DateTime? endTime)
  {
    return AppointmentEntry.SlotIsContainedInSlotInt(slotBegin, slotEnd, slotEnd, beginTime);
  }

  protected virtual void _(Events.RowSelecting<FSTimeSlot> e)
  {
  }

  protected virtual void _(Events.RowSelected<FSTimeSlot> e)
  {
  }

  protected virtual void _(Events.RowInserting<FSTimeSlot> e)
  {
  }

  protected virtual void _(Events.RowInserted<FSTimeSlot> e)
  {
  }

  protected virtual void _(Events.RowUpdating<FSTimeSlot> e)
  {
  }

  protected virtual void _(Events.RowUpdated<FSTimeSlot> e)
  {
  }

  protected virtual void _(Events.RowDeleting<FSTimeSlot> e)
  {
  }

  protected virtual void _(Events.RowDeleted<FSTimeSlot> e)
  {
  }

  protected virtual void _(Events.RowPersisting<FSTimeSlot> e)
  {
  }

  protected virtual void _(Events.RowPersisted<FSTimeSlot> e)
  {
    if (e.Row == null)
      return;
    FSTimeSlot row = e.Row;
    int? slotLevel = row.SlotLevel;
    int num = 0;
    if (!(slotLevel.GetValueOrDefault() == num & slotLevel.HasValue) || e.TranStatus != 1)
      return;
    this.DeleteTimeSlotsByLevel(row, 1);
    this.CreateAndCompressedTimeSlots(row, this);
  }
}
