// Decompiled with JetBrains decompiler
// Type: PX.SM.BaseAccess
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.Descriptor;
using PX.Data.SQLTree;
using System;
using System.Collections;
using System.Collections.Generic;

#nullable disable
namespace PX.SM;

[PXHidden]
public class BaseAccess : PXGraph<BaseAccess>
{
  public PXSave<RelationGroup> Save;
  public PXCancel<RelationGroup> Cancel;
  public PXInsert<RelationGroup> Insert;
  public PXFirst<RelationGroup> First;
  public PXPrevious<RelationGroup> Prev;
  public PXNext<RelationGroup> Next;
  public PXLast<RelationGroup> Last;
  public PXSelect<RelationGroup> Group;
  public PXSelectOrderBy<PX.SM.Users, OrderBy<Desc<PX.SM.Users.included, Asc<PX.SM.Users.username>>>> Users;
  public PXSelect<Neighbour> NeighbourAll;
  public PXSelect<Neighbour, Where<Neighbour.leftEntityType, Equal<Required<Neighbour.leftEntityType>>>> NeighbourLeft;
  public PXSelect<Neighbour, Where<Neighbour.rightEntityType, Equal<Required<Neighbour.rightEntityType>>>> NeighbourRight;

  [InjectDependency]
  internal IBAccountRestrictionHelper BAccountRestrictionHelper { get; set; }

  public BaseAccess()
  {
    this.Save.SetEnabled(false);
    this.Group.Cache.AllowDelete = false;
    this.Users.Cache.AllowDelete = false;
    this.Users.Cache.AllowInsert = false;
    PXUIFieldAttribute.SetEnabled(this.Users.Cache, (string) null, false);
    PXUIFieldAttribute.SetEnabled<PX.SM.Users.included>(this.Users.Cache, (object) null);
  }

  protected IEnumerable users()
  {
    BaseAccess graph = this;
    if (graph.Group.Current != null && !string.IsNullOrEmpty(graph.Group.Current.GroupName))
    {
      foreach (PXResult<PX.SM.Users> pxResult in PXSelectBase<PX.SM.Users, PXSelect<PX.SM.Users>.Config>.Select((PXGraph) graph))
      {
        PX.SM.Users users = (PX.SM.Users) pxResult;
        graph.Users.Current = users;
        yield return (object) users;
      }
    }
  }

  protected virtual void RelationGroup_RowInserted(PXCache cache, PXRowInsertedEventArgs e)
  {
    if (!(e.Row is RelationGroup row) || string.IsNullOrEmpty(row.GroupName))
      return;
    byte[] numArray;
    if (GroupHelper.Count == 0)
      numArray = new byte[4]
      {
        (byte) 128 /*0x80*/,
        (byte) 0,
        (byte) 0,
        (byte) 0
      };
    else if (GroupHelper.Count % 32 /*0x20*/ != 0)
    {
      numArray = new byte[(GroupHelper.Count + 31 /*0x1F*/) / 32 /*0x20*/ * 4];
      numArray[GroupHelper.Count / 8] = (byte) (128 /*0x80*/ >> GroupHelper.Count % 8);
    }
    else
    {
      numArray = new byte[(GroupHelper.Count + 31 /*0x1F*/) / 32 /*0x20*/ * 4 + 4];
      numArray[numArray.Length - 4] = (byte) 128 /*0x80*/;
    }
    row.GroupMask = numArray;
    if (GroupHelper.Count >= numArray.Length * 8)
      return;
    PXCache cach = this.Caches[typeof (Neighbour)];
    foreach (PXResult<Neighbour> pxResult in PXSelectBase<Neighbour, PXSelect<Neighbour>.Config>.Select((PXGraph) this))
    {
      Neighbour neighbour = (Neighbour) pxResult;
      byte[] array = neighbour.CoverageMask;
      Array.Resize<byte>(ref array, numArray.Length);
      neighbour.CoverageMask = array;
      array = neighbour.InverseMask;
      Array.Resize<byte>(ref array, numArray.Length);
      neighbour.InverseMask = array;
      array = neighbour.WinCoverageMask;
      Array.Resize<byte>(ref array, numArray.Length);
      neighbour.WinCoverageMask = array;
      array = neighbour.WinInverseMask;
      Array.Resize<byte>(ref array, numArray.Length);
      neighbour.WinInverseMask = array;
      cach.Update((object) neighbour);
    }
    cach.IsDirty = false;
  }

  protected void Users_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    PX.SM.Users row = e.Row as PX.SM.Users;
    RelationGroup current = this.Group.Current;
    if (row == null || current == null || row.GroupMask == null || current.GroupMask == null || sender.GetStatus((object) row) != PXEntryStatus.Notchanged)
      return;
    bool flag = false;
    for (int index = 0; index < row.GroupMask.Length && index < current.GroupMask.Length; ++index)
    {
      if (current.GroupMask[index] != (byte) 0 && ((int) row.GroupMask[index] & (int) current.GroupMask[index]) == (int) current.GroupMask[index])
        flag = true;
    }
    row.Included = new bool?(flag);
  }

  protected void Users_RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    PX.SM.Users row = e.Row as PX.SM.Users;
    RelationGroup current = this.Group.Current;
    if (row == null || row.GroupMask == null || current == null || current.GroupMask == null)
      return;
    if (row.GroupMask.Length < current.GroupMask.Length)
    {
      byte[] groupMask = row.GroupMask;
      Array.Resize<byte>(ref groupMask, current.GroupMask.Length);
      row.GroupMask = groupMask;
    }
    for (int index = 0; index < current.GroupMask.Length; ++index)
    {
      if (current.GroupMask[index] != (byte) 0)
      {
        bool? included = row.Included;
        bool flag = true;
        row.GroupMask[index] = !(included.GetValueOrDefault() == flag & included.HasValue) ? (byte) ((uint) row.GroupMask[index] & (uint) ~current.GroupMask[index]) : (byte) ((uint) row.GroupMask[index] | (uint) current.GroupMask[index]);
      }
    }
  }

  protected virtual void Users_Password_CommandPreparing(
    PXCache sender,
    PXCommandPreparingEventArgs e)
  {
    if ((e.Operation & PXDBOperation.Delete) != PXDBOperation.Insert || PXTransactionScope.GetSharedInsert() <= 0)
      return;
    e.DataType = PXDbType.NVarChar;
    e.DataLength = new int?(512 /*0x0200*/);
    e.DataValue = (object) ((PX.SM.Users) e.Row).Password;
    e.BqlTable = sender.BqlTable;
    PXCommandPreparingEventArgs preparingEventArgs = e;
    System.Type dac = e.Table;
    if ((object) dac == null)
      dac = e.BqlTable;
    Column column = new Column("Password", (Table) new SimpleTable(dac), e.DataType);
    preparingEventArgs.Expr = (SQLExpression) column;
  }

  public override void Persist()
  {
    this.BAccountRestrictionHelper.Persist();
    base.Persist();
    GroupHelper.Clear();
  }

  protected void populateNeighbours<Table>(PXSelectBase<Table> select) where Table : class, IBqlTable, IIncludable, new()
  {
    RelationGroup current = this.Group.Current;
    if (current == null)
      return;
    bool flag1 = false;
    bool? nullable = current.Active;
    bool flag2 = true;
    if (nullable.GetValueOrDefault() == flag2 & nullable.HasValue)
    {
      foreach (PXResult<Table> pxResult in select.Select())
      {
        Table able = (Table) pxResult;
        if (select.Cache.GetStatus((object) able) == PXEntryStatus.Notchanged)
          select.Current = able;
        nullable = able.Included;
        bool flag3 = true;
        if (nullable.GetValueOrDefault() == flag3 & nullable.HasValue)
        {
          flag1 = true;
          break;
        }
      }
    }
    List<string> collection = new List<string>();
    bool flag4 = false;
    foreach (PXResult<Neighbour> pxResult in this.NeighbourAll.Select())
    {
      Neighbour neighbour = (Neighbour) pxResult;
      if (neighbour.CoverageMask.Length < current.GroupMask.Length || neighbour.InverseMask.Length < current.GroupMask.Length || neighbour.WinCoverageMask.Length < current.GroupMask.Length || neighbour.WinInverseMask.Length < current.GroupMask.Length)
      {
        byte[] array = neighbour.CoverageMask;
        Array.Resize<byte>(ref array, current.GroupMask.Length);
        neighbour.CoverageMask = array;
        array = neighbour.InverseMask;
        Array.Resize<byte>(ref array, current.GroupMask.Length);
        neighbour.InverseMask = array;
        array = neighbour.WinCoverageMask;
        Array.Resize<byte>(ref array, current.GroupMask.Length);
        neighbour.WinCoverageMask = array;
        array = neighbour.WinInverseMask;
        Array.Resize<byte>(ref array, current.GroupMask.Length);
        neighbour.WinInverseMask = array;
        this.NeighbourAll.Update(neighbour);
      }
      bool flag5 = true;
      for (int index = 0; index < current.GroupMask.Length; ++index)
      {
        if ((int) (byte) ((int) current.GroupMask[index] & (index < neighbour.CoverageMask.Length ? (int) neighbour.CoverageMask[index] : 0)) != (int) current.GroupMask[index] && (int) (byte) ((int) current.GroupMask[index] & (index < neighbour.InverseMask.Length ? (int) neighbour.InverseMask[index] : 0)) != (int) current.GroupMask[index] && (int) (byte) ((int) current.GroupMask[index] & (index < neighbour.WinCoverageMask.Length ? (int) neighbour.WinCoverageMask[index] : 0)) != (int) current.GroupMask[index] && (int) (byte) ((int) current.GroupMask[index] & (index < neighbour.WinInverseMask.Length ? (int) neighbour.WinInverseMask[index] : 0)) != (int) current.GroupMask[index])
        {
          flag5 = false;
          break;
        }
      }
      if (neighbour.LeftEntityType == neighbour.RightEntityType && neighbour.LeftEntityType == select.Cache.BqlTable.FullName)
      {
        flag4 = true;
        for (int index = 0; index < current.GroupMask.Length; ++index)
        {
          if (current.GroupType == "IE")
          {
            if (flag1)
              neighbour.CoverageMask[index] |= current.GroupMask[index];
            else
              neighbour.CoverageMask[index] &= ~current.GroupMask[index];
            neighbour.InverseMask[index] &= ~current.GroupMask[index];
            neighbour.WinCoverageMask[index] &= ~current.GroupMask[index];
            neighbour.WinInverseMask[index] &= ~current.GroupMask[index];
          }
          else if (current.GroupType == "EE")
          {
            if (flag1)
              neighbour.InverseMask[index] |= current.GroupMask[index];
            else
              neighbour.InverseMask[index] &= ~current.GroupMask[index];
            neighbour.CoverageMask[index] &= ~current.GroupMask[index];
            neighbour.WinCoverageMask[index] &= ~current.GroupMask[index];
            neighbour.WinInverseMask[index] &= ~current.GroupMask[index];
          }
          else if (current.GroupType == "IO")
          {
            if (flag1)
              neighbour.WinCoverageMask[index] |= current.GroupMask[index];
            else
              neighbour.WinCoverageMask[index] &= ~current.GroupMask[index];
            neighbour.WinInverseMask[index] &= ~current.GroupMask[index];
            neighbour.CoverageMask[index] &= ~current.GroupMask[index];
            neighbour.InverseMask[index] &= ~current.GroupMask[index];
          }
          else if (current.GroupType == "EO")
          {
            if (flag1)
              neighbour.WinInverseMask[index] |= current.GroupMask[index];
            else
              neighbour.WinInverseMask[index] &= ~current.GroupMask[index];
            neighbour.WinCoverageMask[index] &= ~current.GroupMask[index];
            neighbour.CoverageMask[index] &= ~current.GroupMask[index];
            neighbour.InverseMask[index] &= ~current.GroupMask[index];
          }
        }
        this.NeighbourAll.Update(neighbour);
      }
      else if (flag5)
      {
        for (int index = 0; index < current.GroupMask.Length; ++index)
        {
          if (current.GroupType == "IE")
          {
            neighbour.CoverageMask[index] |= current.GroupMask[index];
            neighbour.InverseMask[index] &= ~current.GroupMask[index];
            neighbour.WinCoverageMask[index] &= ~current.GroupMask[index];
            neighbour.WinInverseMask[index] &= ~current.GroupMask[index];
          }
          else if (current.GroupType == "EE")
          {
            neighbour.InverseMask[index] |= current.GroupMask[index];
            neighbour.CoverageMask[index] &= ~current.GroupMask[index];
            neighbour.WinCoverageMask[index] &= ~current.GroupMask[index];
            neighbour.WinInverseMask[index] &= ~current.GroupMask[index];
          }
          else if (current.GroupType == "IO")
          {
            neighbour.WinCoverageMask[index] |= current.GroupMask[index];
            neighbour.WinInverseMask[index] &= ~current.GroupMask[index];
            neighbour.CoverageMask[index] &= ~current.GroupMask[index];
            neighbour.InverseMask[index] &= ~current.GroupMask[index];
          }
          else if (current.GroupType == "EO")
          {
            neighbour.WinInverseMask[index] |= current.GroupMask[index];
            neighbour.WinCoverageMask[index] &= ~current.GroupMask[index];
            neighbour.CoverageMask[index] &= ~current.GroupMask[index];
            neighbour.InverseMask[index] &= ~current.GroupMask[index];
          }
        }
        this.NeighbourAll.Update(neighbour);
        if (neighbour.LeftEntityType == neighbour.RightEntityType)
          collection.Add(neighbour.LeftEntityType);
      }
    }
    if (!flag4 & flag1)
    {
      Neighbour neighbour = new Neighbour();
      neighbour.RightEntityType = neighbour.LeftEntityType = select.Cache.BqlTable.FullName;
      if (current.GroupType == "IE")
      {
        neighbour.CoverageMask = (byte[]) current.GroupMask.Clone();
        neighbour.InverseMask = new byte[current.GroupMask.Length];
        neighbour.WinCoverageMask = new byte[current.GroupMask.Length];
        neighbour.WinInverseMask = new byte[current.GroupMask.Length];
      }
      else if (current.GroupType == "EE")
      {
        neighbour.InverseMask = (byte[]) current.GroupMask.Clone();
        neighbour.CoverageMask = new byte[current.GroupMask.Length];
        neighbour.WinCoverageMask = new byte[current.GroupMask.Length];
        neighbour.WinInverseMask = new byte[current.GroupMask.Length];
      }
      else if (current.GroupType == "IO")
      {
        neighbour.WinCoverageMask = (byte[]) current.GroupMask.Clone();
        neighbour.WinInverseMask = new byte[current.GroupMask.Length];
        neighbour.CoverageMask = new byte[current.GroupMask.Length];
        neighbour.InverseMask = new byte[current.GroupMask.Length];
      }
      else if (current.GroupType == "EO")
      {
        neighbour.WinInverseMask = (byte[]) current.GroupMask.Clone();
        neighbour.WinCoverageMask = new byte[current.GroupMask.Length];
        neighbour.CoverageMask = new byte[current.GroupMask.Length];
        neighbour.InverseMask = new byte[current.GroupMask.Length];
      }
      this.NeighbourAll.Insert(neighbour);
    }
    List<string> stringList1 = new List<string>((IEnumerable<string>) collection);
    foreach (PXResult<Neighbour> pxResult in this.NeighbourLeft.Select((object) select.Cache.BqlTable.FullName))
    {
      Neighbour neighbour = (Neighbour) pxResult;
      if (collection.Contains(neighbour.RightEntityType))
      {
        for (int index = 0; index < current.GroupMask.Length; ++index)
        {
          if (current.GroupType == "IE")
          {
            if (flag1)
              neighbour.CoverageMask[index] |= current.GroupMask[index];
            else
              neighbour.CoverageMask[index] &= ~current.GroupMask[index];
            neighbour.InverseMask[index] &= ~current.GroupMask[index];
            neighbour.WinCoverageMask[index] &= ~current.GroupMask[index];
            neighbour.WinInverseMask[index] &= ~current.GroupMask[index];
          }
          else if (current.GroupType == "EE")
          {
            if (flag1)
              neighbour.InverseMask[index] |= current.GroupMask[index];
            else
              neighbour.InverseMask[index] &= ~current.GroupMask[index];
            neighbour.CoverageMask[index] &= ~current.GroupMask[index];
            neighbour.WinCoverageMask[index] &= ~current.GroupMask[index];
            neighbour.WinInverseMask[index] &= ~current.GroupMask[index];
          }
          else if (current.GroupType == "IO")
          {
            if (flag1)
              neighbour.WinCoverageMask[index] |= current.GroupMask[index];
            else
              neighbour.WinCoverageMask[index] &= ~current.GroupMask[index];
            neighbour.WinInverseMask[index] &= ~current.GroupMask[index];
            neighbour.CoverageMask[index] &= ~current.GroupMask[index];
            neighbour.InverseMask[index] &= ~current.GroupMask[index];
          }
          else if (current.GroupType == "EO")
          {
            if (flag1)
              neighbour.WinInverseMask[index] |= current.GroupMask[index];
            else
              neighbour.WinInverseMask[index] &= ~current.GroupMask[index];
            neighbour.WinCoverageMask[index] &= ~current.GroupMask[index];
            neighbour.InverseMask[index] &= ~current.GroupMask[index];
            neighbour.CoverageMask[index] &= ~current.GroupMask[index];
          }
        }
        this.NeighbourAll.Update(neighbour);
        collection.Remove(neighbour.RightEntityType);
      }
    }
    if (flag1 && collection.Count > 0)
    {
      foreach (string str in collection)
      {
        Neighbour neighbour = new Neighbour();
        neighbour.RightEntityType = neighbour.LeftEntityType = select.Cache.BqlTable.FullName;
        if (current.GroupType == "IE")
        {
          neighbour.CoverageMask = (byte[]) current.GroupMask.Clone();
          neighbour.InverseMask = new byte[current.GroupMask.Length];
          neighbour.WinCoverageMask = new byte[current.GroupMask.Length];
          neighbour.WinInverseMask = new byte[current.GroupMask.Length];
        }
        else if (current.GroupType == "EE")
        {
          neighbour.InverseMask = (byte[]) current.GroupMask.Clone();
          neighbour.CoverageMask = new byte[current.GroupMask.Length];
          neighbour.WinCoverageMask = new byte[current.GroupMask.Length];
          neighbour.WinInverseMask = new byte[current.GroupMask.Length];
        }
        else if (current.GroupType == "IO")
        {
          neighbour.WinCoverageMask = (byte[]) current.GroupMask.Clone();
          neighbour.WinInverseMask = new byte[current.GroupMask.Length];
          neighbour.CoverageMask = new byte[current.GroupMask.Length];
          neighbour.InverseMask = new byte[current.GroupMask.Length];
        }
        else if (current.GroupType == "EO")
        {
          neighbour.WinInverseMask = (byte[]) current.GroupMask.Clone();
          neighbour.WinCoverageMask = new byte[current.GroupMask.Length];
          neighbour.CoverageMask = new byte[current.GroupMask.Length];
          neighbour.InverseMask = new byte[current.GroupMask.Length];
        }
        this.NeighbourAll.Insert(neighbour);
      }
    }
    List<string> stringList2 = stringList1;
    foreach (PXResult<Neighbour> pxResult in this.NeighbourRight.Select((object) select.Cache.BqlTable.FullName))
    {
      Neighbour neighbour = (Neighbour) pxResult;
      if (stringList2.Contains(neighbour.LeftEntityType))
      {
        for (int index = 0; index < current.GroupMask.Length; ++index)
        {
          if (current.GroupType == "IE")
          {
            if (flag1)
              neighbour.CoverageMask[index] |= current.GroupMask[index];
            else
              neighbour.CoverageMask[index] &= ~current.GroupMask[index];
            neighbour.InverseMask[index] &= ~current.GroupMask[index];
            neighbour.WinCoverageMask[index] &= ~current.GroupMask[index];
            neighbour.WinInverseMask[index] &= ~current.GroupMask[index];
          }
          else if (current.GroupType == "EE")
          {
            if (flag1)
              neighbour.InverseMask[index] |= current.GroupMask[index];
            else
              neighbour.InverseMask[index] &= ~current.GroupMask[index];
            neighbour.CoverageMask[index] &= ~current.GroupMask[index];
            neighbour.WinCoverageMask[index] &= ~current.GroupMask[index];
            neighbour.WinInverseMask[index] &= ~current.GroupMask[index];
          }
          else if (current.GroupType == "IO")
          {
            if (flag1)
              neighbour.WinCoverageMask[index] |= current.GroupMask[index];
            else
              neighbour.WinCoverageMask[index] &= ~current.GroupMask[index];
            neighbour.WinInverseMask[index] &= ~current.GroupMask[index];
            neighbour.CoverageMask[index] &= ~current.GroupMask[index];
            neighbour.InverseMask[index] &= ~current.GroupMask[index];
          }
          else if (current.GroupType == "EO")
          {
            if (flag1)
              neighbour.WinInverseMask[index] |= current.GroupMask[index];
            else
              neighbour.WinInverseMask[index] &= ~current.GroupMask[index];
            neighbour.WinCoverageMask[index] &= ~current.GroupMask[index];
            neighbour.CoverageMask[index] &= ~current.GroupMask[index];
            neighbour.InverseMask[index] &= ~current.GroupMask[index];
          }
        }
        this.NeighbourAll.Update(neighbour);
        stringList2.Remove(neighbour.LeftEntityType);
      }
    }
    if (!flag1 || stringList2.Count <= 0)
      return;
    foreach (string str in stringList2)
    {
      Neighbour neighbour = new Neighbour();
      neighbour.LeftEntityType = str;
      neighbour.RightEntityType = select.Cache.BqlTable.FullName;
      if (current.GroupType == "IE")
      {
        neighbour.CoverageMask = (byte[]) current.GroupMask.Clone();
        neighbour.InverseMask = new byte[current.GroupMask.Length];
        neighbour.WinCoverageMask = new byte[current.GroupMask.Length];
        neighbour.WinInverseMask = new byte[current.GroupMask.Length];
      }
      else if (current.GroupType == "EE")
      {
        neighbour.InverseMask = (byte[]) current.GroupMask.Clone();
        neighbour.CoverageMask = new byte[current.GroupMask.Length];
        neighbour.WinCoverageMask = new byte[current.GroupMask.Length];
        neighbour.WinInverseMask = new byte[current.GroupMask.Length];
      }
      else if (current.GroupType == "IO")
      {
        neighbour.WinCoverageMask = (byte[]) current.GroupMask.Clone();
        neighbour.WinInverseMask = new byte[current.GroupMask.Length];
        neighbour.CoverageMask = new byte[current.GroupMask.Length];
        neighbour.InverseMask = new byte[current.GroupMask.Length];
      }
      else if (current.GroupType == "EO")
      {
        neighbour.WinInverseMask = (byte[]) current.GroupMask.Clone();
        neighbour.WinCoverageMask = new byte[current.GroupMask.Length];
        neighbour.CoverageMask = new byte[current.GroupMask.Length];
        neighbour.InverseMask = new byte[current.GroupMask.Length];
      }
      this.NeighbourAll.Insert(neighbour);
    }
  }
}
