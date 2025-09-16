// Decompiled with JetBrains decompiler
// Type: PX.SM.UserAccess
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Data;
using PX.Data.Descriptor;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.SM;

public class UserAccess : PXGraph<UserAccess>
{
  public PXSelect<Users> User;
  public PXSelect<RelationGroup> Groups;
  public PXSave<Users> Save;
  public PXCancel<Users> Cancel;
  public PXFirst<Users> First;
  public PXPrevious<Users> Prev;
  public PXNext<Users> Next;
  public PXLast<Users> Last;

  [InjectDependency]
  internal IBAccountRestrictionHelper BAccountRestrictionHelper { get; set; }

  public UserAccess()
  {
    this.Groups.Cache.AllowDelete = false;
    this.Groups.Cache.AllowInsert = false;
    PXUIFieldAttribute.SetEnabled(this.Groups.Cache, (string) null, false);
    PXUIFieldAttribute.SetEnabled<RelationGroup.included>(this.Groups.Cache, (object) null);
    this.User.Cache.AllowDelete = false;
    this.User.Cache.AllowInsert = false;
    PXUIFieldAttribute.SetEnabled(this.User.Cache, (string) null, false);
    PXUIFieldAttribute.SetEnabled<Users.username>(this.User.Cache, (object) null);
    PXUIFieldAttribute.SetVisible<Users.username>(this.User.Cache, (object) null);
  }

  protected virtual IEnumerable groups()
  {
    UserAccess graph = this;
    foreach (PXResult<RelationGroup> pxResult in PXSelectBase<RelationGroup, PXSelect<RelationGroup>.Config>.Select((PXGraph) graph))
    {
      RelationGroup relationGroup = (RelationGroup) pxResult;
      graph.Groups.Current = relationGroup;
      yield return (object) relationGroup;
    }
  }

  protected virtual byte[] getMask()
  {
    byte[] mask = (byte[]) null;
    if (this.User.Current != null)
      mask = this.User.Current.GroupMask;
    return mask;
  }

  protected void RelationGroup_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    byte[] mask = this.getMask();
    if (!(e.Row is RelationGroup row) || this.Groups.Cache.GetStatus((object) row) != PXEntryStatus.Notchanged)
      return;
    row.Included = new bool?(UserAccess.IsIncluded(mask, row));
  }

  public static bool IsIncluded(byte[] mask, RelationGroup group)
  {
    if (mask != null && group != null && group.GroupMask != null)
    {
      for (int index = 0; index < mask.Length && index < group.GroupMask.Length; ++index)
      {
        if (group.GroupMask[index] != (byte) 0 && ((int) group.GroupMask[index] & (int) mask[index]) == (int) group.GroupMask[index])
          return true;
      }
    }
    return false;
  }

  public static bool InNeighbours(PXResultset<Neighbour> set, RelationGroup group)
  {
    foreach (PXResult<Neighbour> pxResult in set)
    {
      Neighbour neighbour = (Neighbour) pxResult;
      if (UserAccess.IsIncluded(neighbour.CoverageMask, group) || UserAccess.IsIncluded(neighbour.WinCoverageMask, group) || UserAccess.IsIncluded(neighbour.InverseMask, group) || UserAccess.IsIncluded(neighbour.WinInverseMask, group))
        return true;
    }
    return false;
  }

  public override void Persist()
  {
    if (this.User.Current != null)
    {
      UserAccess.PopulateNeighbours<Users>((PXSelectBase<Users>) this.User, (PXSelectBase<RelationGroup>) this.Groups);
      PXSelectorAttribute.ClearGlobalCache<Users>();
    }
    int num = this.IsDirty ? 1 : 0;
    this.BAccountRestrictionHelper.Persist();
    base.Persist();
    this.Groups.Cache.Clear();
    this.Groups.View.Clear();
    GroupHelper.Clear();
  }

  protected void RelationGroup_RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    e.Cancel = true;
  }

  protected void Users_RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    if ((e.Operation & PXDBOperation.Delete) == PXDBOperation.Update)
      return;
    e.Cancel = true;
  }

  protected void Users_RowUpdating(PXCache sender, PXRowUpdatingEventArgs e)
  {
    Users newRow = e.NewRow as Users;
    Users row = e.Row as Users;
    if (!(newRow.Username != row.Username))
      return;
    sender.RaiseExceptionHandling<Users.username>((object) newRow, (object) newRow.Username, (Exception) new PXSetPropertyException("Changes to the user name are prohibited in the system.", PXErrorLevel.Error));
    e.Cancel = true;
  }

  private static string GetCachedCollectionSlotKey(PXCache cache)
  {
    return $"{cache.Graph.GetType().FullName}_{cache.GetItemType().FullName}";
  }

  private static PXCollection<Table> GetCachedCollection<Table>(PXSelectBase<Table> select) where Table : class, IBqlTable, IRestricted, new()
  {
    string collectionSlotKey = UserAccess.GetCachedCollectionSlotKey(select.Cache);
    PXCollection<Table> slot = PXContext.GetSlot<PXCollection<Table>>(collectionSlotKey);
    if (slot != null)
      return slot;
    Table current = select.Current;
    PXCollection<Table> cachedCollection = new PXCollection<Table>(select.Cache);
    foreach (PXResult<Table> pxResult in select.Select())
    {
      Table able = (Table) pxResult;
      cachedCollection.Add(able);
    }
    select.Current = current;
    PXContext.SetSlot<PXCollection<Table>>(collectionSlotKey, cachedCollection);
    return cachedCollection;
  }

  private static void UpdateCachedCollection<Table>(PXCache cache, Table row) where Table : class, IRestricted
  {
    PXCollection<Table> slot = PXContext.GetSlot<PXCollection<Table>>(UserAccess.GetCachedCollectionSlotKey(cache));
    if (slot == null)
      return;
    Table able = slot.Locate(row);
    if ((object) able == null)
      return;
    able.GroupMask = row.GroupMask;
  }

  public static void PopulateNeighbours<Table>(
    PXSelectBase<Table> select,
    PXSelectBase<RelationGroup> groups,
    params System.Type[] forced)
    where Table : class, IBqlTable, IRestricted, new()
  {
    Table current = select.Current;
    PXCache cache = select.Cache;
    byte[][] array = UserAccess.GetCachedCollection<Table>(select).Where<Table>((Func<Table, bool>) (item => !cache.ObjectsEqual((object) item, (object) current))).Select<Table, byte[]>((Func<Table, byte[]>) (item => item.GroupMask)).ToArray<byte[]>();
    UserAccess.PopulateNeighbours<Table>(cache, select.Current, array, groups, forced);
    UserAccess.UpdateCachedCollection<Table>(cache, current);
  }

  public static void PopulateNeighbours<Table>(
    PXCache sender,
    Table item,
    PXDataFieldValue[] rest,
    PXSelectBase<RelationGroup> groups,
    params System.Type[] forced)
    where Table : class, IBqlTable, IRestricted, new()
  {
    List<byte[]> numArrayList = new List<byte[]>();
    PXDataField[] destinationArray = new PXDataField[rest.Length + 2];
    destinationArray[0] = new PXDataField("GroupMask");
    destinationArray[1] = (PXDataField) new PXDataFieldValue("GroupMask", PXDbType.VarBinary, new int?(0), (object) new byte[0], PXComp.NE);
    Array.Copy((Array) rest, 0, (Array) destinationArray, 2, rest.Length);
    foreach (PXDataRecord pxDataRecord in PXDatabase.SelectMulti<Table>(destinationArray))
      numArrayList.Add(pxDataRecord.GetBytes(0));
    UserAccess.PopulateNeighbours<Table>(sender, item, numArrayList.ToArray(), groups, forced);
  }

  public static void PopulateNeighbours<Table>(
    PXCache sender,
    Table item,
    byte[][] rest,
    PXSelectBase<RelationGroup> groups,
    params System.Type[] forced)
    where Table : class, IBqlTable, IRestricted, new()
  {
    if (!groups.Cache.Graph.Views.Caches.Contains(typeof (Neighbour)))
      groups.Cache.Graph.Views.Caches.Add(typeof (Neighbour));
    int length = 0;
    foreach (PXResult<RelationGroup> pxResult in groups.Select())
    {
      RelationGroup relationGroup = (RelationGroup) pxResult;
      if (relationGroup.GroupMask.Length > length)
        length = relationGroup.GroupMask.Length;
    }
    byte[] numArray1 = new byte[length];
    using (IEnumerator<PXResult<RelationGroup>> enumerator = groups.Select().GetEnumerator())
    {
label_18:
      while (enumerator.MoveNext())
      {
        RelationGroup current = (RelationGroup) enumerator.Current;
        if (groups.Cache.GetStatus((object) current) == PXEntryStatus.Notchanged)
          groups.Current = current;
        bool? included = current.Included;
        bool flag = true;
        if (included.GetValueOrDefault() == flag & included.HasValue)
        {
          int index = 0;
          while (true)
          {
            if (index < current.GroupMask.Length && index < numArray1.Length)
            {
              numArray1[index] |= current.GroupMask[index];
              ++index;
            }
            else
              goto label_18;
          }
        }
      }
    }
    item.GroupMask = numArray1;
    sender.Update((object) item);
    byte[] numArray2 = (byte[]) numArray1.Clone();
    foreach (byte[] numArray3 in rest)
    {
      for (int index = 0; index < numArray2.Length; ++index)
        numArray2[index] |= numArray3 == null || index >= numArray3.Length ? (byte) 0 : numArray3[index];
    }
    bool flag1 = false;
    foreach (byte num in numArray2)
    {
      if (num != (byte) 0)
      {
        flag1 = true;
        break;
      }
    }
    byte[] numArray4 = new byte[numArray2.Length];
    byte[] numArray5 = new byte[numArray2.Length];
    byte[][] numArray6 = new byte[forced.Length][];
    byte[][] numArray7 = new byte[forced.Length][];
    byte[] numArray8 = new byte[numArray2.Length];
    byte[] numArray9 = new byte[numArray2.Length];
    byte[][] numArray10 = new byte[forced.Length][];
    byte[][] numArray11 = new byte[forced.Length][];
    for (int index = 0; index < forced.Length; ++index)
    {
      numArray6[index] = new byte[numArray2.Length];
      numArray7[index] = new byte[numArray2.Length];
      numArray10[index] = new byte[numArray2.Length];
      numArray11[index] = new byte[numArray2.Length];
    }
    foreach (PXResult<RelationGroup> pxResult in groups.Select())
    {
      RelationGroup relationGroup = (RelationGroup) pxResult;
      bool? active = relationGroup.Active;
      bool flag2 = true;
      if (active.GetValueOrDefault() == flag2 & active.HasValue)
      {
        for (int index = 0; index < relationGroup.GroupMask.Length; ++index)
        {
          if (relationGroup.GroupType == "EE")
            numArray5[index] |= (byte) ((uint) numArray2[index] & (uint) relationGroup.GroupMask[index]);
          else if (relationGroup.GroupType == "IE")
            numArray4[index] |= (byte) ((uint) numArray2[index] & (uint) relationGroup.GroupMask[index]);
          else if (relationGroup.GroupType == "EO")
            numArray9[index] |= (byte) ((uint) numArray2[index] & (uint) relationGroup.GroupMask[index]);
          else if (relationGroup.GroupType == "IO")
            numArray8[index] |= (byte) ((uint) numArray2[index] & (uint) relationGroup.GroupMask[index]);
        }
        for (int index1 = 0; index1 < forced.Length; ++index1)
        {
          if (relationGroup.SpecificModule == typeof (Table).Namespace && relationGroup.SpecificType == forced[index1].FullName)
          {
            for (int index2 = 0; index2 < relationGroup.GroupMask.Length; ++index2)
            {
              if (relationGroup.GroupType == "EE")
                numArray7[index1][index2] |= (byte) ((uint) numArray2[index2] & (uint) relationGroup.GroupMask[index2]);
              else if (relationGroup.GroupType == "IE")
                numArray6[index1][index2] |= (byte) ((uint) numArray2[index2] & (uint) relationGroup.GroupMask[index2]);
              else if (relationGroup.GroupType == "EO")
                numArray11[index1][index2] |= (byte) ((uint) numArray2[index2] & (uint) relationGroup.GroupMask[index2]);
              else if (relationGroup.GroupType == "IO")
                numArray10[index1][index2] |= (byte) ((uint) numArray2[index2] & (uint) relationGroup.GroupMask[index2]);
            }
          }
        }
      }
    }
    Dictionary<string, byte[][]> dictionary1 = new Dictionary<string, byte[][]>();
    Dictionary<string, byte[][]>[] dictionaryArray = new Dictionary<string, byte[][]>[forced.Length];
    for (int index = 0; index < forced.Length; ++index)
      dictionaryArray[index] = new Dictionary<string, byte[][]>();
    bool flag3 = false;
    bool[] flagArray = new bool[forced.Length];
    PXSelect<Neighbour> pxSelect = new PXSelect<Neighbour>(groups.Cache.Graph);
    foreach (PXResult<Neighbour> pxResult in pxSelect.Select())
    {
      Neighbour neighbour = (Neighbour) pxResult;
      if (neighbour.LeftEntityType == neighbour.RightEntityType)
      {
        bool flag4 = false;
        if (neighbour.LeftEntityType == sender.BqlTable.FullName)
        {
          flag3 = true;
          neighbour.CoverageMask = numArray4;
          neighbour.InverseMask = numArray5;
          neighbour.WinCoverageMask = numArray8;
          neighbour.WinInverseMask = numArray9;
          pxSelect.Update(neighbour);
          for (int index = 0; index < forced.Length; ++index)
            dictionaryArray[index][neighbour.LeftEntityType] = new byte[4][]
            {
              (byte[]) numArray6[index].Clone(),
              (byte[]) numArray7[index].Clone(),
              (byte[]) numArray10[index].Clone(),
              (byte[]) numArray11[index].Clone()
            };
          flag4 = true;
        }
        else
        {
          for (int index = 0; index < forced.Length; ++index)
          {
            if (neighbour.LeftEntityType == forced[index].FullName)
            {
              flagArray[index] = true;
              neighbour.CoverageMask = numArray6[index];
              neighbour.InverseMask = numArray7[index];
              neighbour.WinCoverageMask = numArray10[index];
              neighbour.WinInverseMask = numArray11[index];
              pxSelect.Update(neighbour);
              dictionary1[neighbour.LeftEntityType] = new byte[4][]
              {
                (byte[]) numArray6[index].Clone(),
                (byte[]) numArray7[index].Clone(),
                (byte[]) numArray10[index].Clone(),
                (byte[]) numArray11[index].Clone()
              };
              flag4 = true;
              break;
            }
          }
        }
        if (!flag4)
        {
          byte[] numArray12 = (byte[]) numArray4.Clone();
          byte[] numArray13 = (byte[]) numArray5.Clone();
          byte[] numArray14 = (byte[]) numArray8.Clone();
          byte[] numArray15 = (byte[]) numArray9.Clone();
          for (int index = 0; index < numArray12.Length; ++index)
          {
            numArray12[index] &= index < neighbour.CoverageMask.Length ? neighbour.CoverageMask[index] : (byte) 0;
            numArray13[index] &= index < neighbour.InverseMask.Length ? neighbour.InverseMask[index] : (byte) 0;
          }
          for (int index = 0; index < numArray14.Length; ++index)
          {
            numArray14[index] &= index < neighbour.WinCoverageMask.Length ? neighbour.WinCoverageMask[index] : (byte) 0;
            numArray15[index] &= index < neighbour.WinInverseMask.Length ? neighbour.WinInverseMask[index] : (byte) 0;
          }
          dictionary1[neighbour.LeftEntityType] = new byte[4][]
          {
            numArray12,
            numArray13,
            numArray14,
            numArray15
          };
          for (int index3 = 0; index3 < forced.Length; ++index3)
          {
            byte[] numArray16 = (byte[]) numArray6[index3].Clone();
            byte[] numArray17 = (byte[]) numArray7[index3].Clone();
            byte[] numArray18 = (byte[]) numArray10[index3].Clone();
            byte[] numArray19 = (byte[]) numArray11[index3].Clone();
            for (int index4 = 0; index4 < numArray16.Length; ++index4)
            {
              numArray16[index4] &= index4 < neighbour.CoverageMask.Length ? neighbour.CoverageMask[index4] : (byte) 0;
              numArray17[index4] &= index4 < neighbour.InverseMask.Length ? neighbour.InverseMask[index4] : (byte) 0;
            }
            for (int index5 = 0; index5 < numArray18.Length; ++index5)
            {
              numArray18[index5] &= index5 < neighbour.WinCoverageMask.Length ? neighbour.WinCoverageMask[index5] : (byte) 0;
              numArray19[index5] &= index5 < neighbour.WinInverseMask.Length ? neighbour.WinInverseMask[index5] : (byte) 0;
            }
            dictionaryArray[index3][neighbour.LeftEntityType] = new byte[4][]
            {
              numArray16,
              numArray17,
              numArray18,
              numArray19
            };
          }
        }
      }
    }
    if (flag1)
    {
      if (!flag3)
        pxSelect.Insert(new Neighbour()
        {
          LeftEntityType = sender.BqlTable.FullName,
          RightEntityType = sender.BqlTable.FullName,
          CoverageMask = numArray4,
          InverseMask = numArray5,
          WinCoverageMask = numArray8,
          WinInverseMask = numArray9
        });
      for (int index = 0; index < forced.Length; ++index)
      {
        if (!flagArray[index])
          pxSelect.Insert(new Neighbour()
          {
            LeftEntityType = forced[index].FullName,
            RightEntityType = forced[index].FullName,
            CoverageMask = numArray6[index],
            InverseMask = numArray7[index],
            WinCoverageMask = numArray10[index],
            WinInverseMask = numArray11[index]
          });
      }
    }
    Dictionary<string, byte[][]> dictionary2 = new Dictionary<string, byte[][]>((IDictionary<string, byte[][]>) dictionary1);
    foreach (PXResult<Neighbour> pxResult in PXSelectBase<Neighbour, PXSelect<Neighbour, Where<Neighbour.leftEntityType, Equal<Required<Neighbour.leftEntityType>>>>.Config>.Select(groups.Cache.Graph, (object) sender.BqlTable.FullName))
    {
      Neighbour neighbour = (Neighbour) pxResult;
      if (dictionary1.ContainsKey(neighbour.RightEntityType))
      {
        neighbour.CoverageMask = dictionary1[neighbour.RightEntityType][0];
        neighbour.InverseMask = dictionary1[neighbour.RightEntityType][1];
        neighbour.WinCoverageMask = dictionary1[neighbour.RightEntityType][2];
        neighbour.WinInverseMask = dictionary1[neighbour.RightEntityType][3];
        pxSelect.Update(neighbour);
        dictionary1.Remove(neighbour.RightEntityType);
      }
    }
    if (flag1 && dictionary1.Count > 0)
    {
      foreach (string key in dictionary1.Keys)
        pxSelect.Insert(new Neighbour()
        {
          LeftEntityType = sender.BqlTable.FullName,
          RightEntityType = key,
          CoverageMask = dictionary1[key][1],
          InverseMask = dictionary1[key][1],
          WinCoverageMask = dictionary1[key][2],
          WinInverseMask = dictionary1[key][3]
        });
    }
    Dictionary<string, byte[][]> dictionary3 = new Dictionary<string, byte[][]>((IDictionary<string, byte[][]>) dictionary2);
    foreach (PXResult<Neighbour> pxResult in PXSelectBase<Neighbour, PXSelect<Neighbour, Where<Neighbour.rightEntityType, Equal<Required<Neighbour.rightEntityType>>>>.Config>.Select(groups.Cache.Graph, (object) sender.BqlTable.FullName))
    {
      Neighbour neighbour = (Neighbour) pxResult;
      if (dictionary3.ContainsKey(neighbour.LeftEntityType))
      {
        neighbour.CoverageMask = dictionary3[neighbour.LeftEntityType][0];
        neighbour.InverseMask = dictionary3[neighbour.LeftEntityType][1];
        neighbour.WinCoverageMask = dictionary3[neighbour.LeftEntityType][2];
        neighbour.WinInverseMask = dictionary3[neighbour.LeftEntityType][3];
        pxSelect.Update(neighbour);
        dictionary3.Remove(neighbour.LeftEntityType);
      }
    }
    if (flag1 && dictionary3.Count > 0)
    {
      foreach (string key in dictionary3.Keys)
        pxSelect.Insert(new Neighbour()
        {
          LeftEntityType = key,
          RightEntityType = sender.BqlTable.FullName,
          CoverageMask = dictionary3[key][0],
          InverseMask = dictionary3[key][1],
          WinCoverageMask = dictionary3[key][2],
          WinInverseMask = dictionary3[key][3]
        });
    }
    for (int index = 0; index < forced.Length; ++index)
    {
      Dictionary<string, byte[][]> dictionary4 = new Dictionary<string, byte[][]>((IDictionary<string, byte[][]>) dictionaryArray[index]);
      Dictionary<string, byte[][]> dictionary5 = dictionaryArray[index];
      foreach (PXResult<Neighbour> pxResult in PXSelectBase<Neighbour, PXSelect<Neighbour, Where<Neighbour.leftEntityType, Equal<Required<Neighbour.leftEntityType>>>>.Config>.Select(groups.Cache.Graph, (object) forced[index].FullName))
      {
        Neighbour neighbour = (Neighbour) pxResult;
        if (dictionary5.ContainsKey(neighbour.RightEntityType))
        {
          neighbour.CoverageMask = dictionary5[neighbour.RightEntityType][0];
          neighbour.InverseMask = dictionary5[neighbour.RightEntityType][1];
          neighbour.WinCoverageMask = dictionary5[neighbour.RightEntityType][2];
          neighbour.WinInverseMask = dictionary5[neighbour.RightEntityType][3];
          pxSelect.Update(neighbour);
          dictionary5.Remove(neighbour.RightEntityType);
        }
      }
      if (flag1 && dictionary5.Count > 0)
      {
        foreach (string key in dictionary5.Keys)
          pxSelect.Insert(new Neighbour()
          {
            LeftEntityType = forced[index].FullName,
            RightEntityType = key,
            CoverageMask = dictionary5[key][0],
            InverseMask = dictionary5[key][1],
            WinCoverageMask = dictionary5[key][2],
            WinInverseMask = dictionary5[key][3]
          });
      }
      Dictionary<string, byte[][]> dictionary6 = new Dictionary<string, byte[][]>((IDictionary<string, byte[][]>) dictionary4);
      foreach (PXResult<Neighbour> pxResult in PXSelectBase<Neighbour, PXSelect<Neighbour, Where<Neighbour.rightEntityType, Equal<Required<Neighbour.rightEntityType>>>>.Config>.Select(groups.Cache.Graph, (object) forced[index].FullName))
      {
        Neighbour neighbour = (Neighbour) pxResult;
        if (dictionary6.ContainsKey(neighbour.LeftEntityType))
        {
          neighbour.CoverageMask = dictionary6[neighbour.LeftEntityType][0];
          neighbour.InverseMask = dictionary6[neighbour.LeftEntityType][1];
          neighbour.WinCoverageMask = dictionary6[neighbour.LeftEntityType][2];
          neighbour.WinInverseMask = dictionary6[neighbour.LeftEntityType][3];
          pxSelect.Update(neighbour);
          dictionary6.Remove(neighbour.LeftEntityType);
        }
      }
      if (flag1 && dictionary6.Count > 0)
      {
        foreach (string key in dictionary6.Keys)
          pxSelect.Insert(new Neighbour()
          {
            LeftEntityType = key,
            RightEntityType = forced[index].FullName,
            CoverageMask = dictionary6[key][0],
            InverseMask = dictionary6[key][1],
            WinCoverageMask = dictionary6[key][2],
            WinInverseMask = dictionary6[key][3]
          });
      }
    }
  }
}
