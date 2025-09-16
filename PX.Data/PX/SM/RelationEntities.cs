// Decompiled with JetBrains decompiler
// Type: PX.SM.RelationEntities
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.Descriptor;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web.Compilation;

#nullable disable
namespace PX.SM;

public class RelationEntities : PXGraph<RelationEntities>
{
  public PXSelectReadonly<FilterEntity> Instances;
  private const string MaskedSlotKey = "Masked_List_{0}";
  public PXSelect<RelationDetail> DetailsEntity;
  public PXSelect<FilterEntity> HeaderEntity;
  public PXAction<FilterEntity> Save;
  public PXAction<FilterEntity> Cancel;
  public PXAction<FilterEntity> First;
  public PXAction<FilterEntity> Previous;
  public PXAction<FilterEntity> Next;
  public PXAction<FilterEntity> Last;

  [InjectDependency]
  internal IBAccountRestrictionHelper BAccountRestrictionHelper { get; set; }

  protected IEnumerable instances()
  {
    return (IEnumerable) this.HeaderEntity.Cache.Inserted.Cast<FilterEntity>().Where<FilterEntity>((Func<FilterEntity, bool>) (f => f != null && string.Equals(f.EntityTypeName, this.HeaderEntity.Current.EntityTypeName, StringComparison.Ordinal)));
  }

  protected IEnumerable detailsEntity()
  {
    RelationEntities graph = this;
    FilterEntity current = graph.HeaderEntity.Current;
    if (current != null && current.EntityType != (System.Type) null)
    {
      byte[] mask = graph.Caches[current.EntityType].GetValue(current.Instance, "GroupMask") as byte[];
      foreach (PXResult<RelationDetail> pxResult in PXSelectBase<RelationDetail, PXSelect<RelationDetail>.Config>.Select((PXGraph) graph))
      {
        RelationDetail relationDetail = (RelationDetail) pxResult;
        if (!relationDetail.Selected.HasValue)
        {
          relationDetail.Selected = new bool?(true);
          for (int index = 0; index < relationDetail.GroupMask.Length; ++index)
          {
            if (((int) relationDetail.GroupMask[index] & (mask == null || index >= mask.Length ? 0 : (int) mask[index])) != (int) relationDetail.GroupMask[index])
            {
              relationDetail.Selected = new bool?(false);
              break;
            }
          }
        }
        yield return graph.DetailsEntity.Cache.Update((object) relationDetail);
      }
      graph.DetailsEntity.Cache.IsDirty = false;
      mask = (byte[]) null;
    }
  }

  public RelationEntities()
  {
    this.DetailsEntity.Cache.AllowDelete = false;
    this.DetailsEntity.Cache.AllowInsert = false;
    PXUIFieldAttribute.SetDisplayName<Graph.text>(this.Caches[typeof (Graph)], "Module");
  }

  protected IEnumerable headerEntity() => this.HeaderEntity.Cache.Inserted;

  [PXUIField(DisplayName = "Save", MapEnableRights = PXCacheRights.Update, MapViewRights = PXCacheRights.Update)]
  [PXSaveButton(ImageKey = "Save", Tooltip = "Save current record")]
  protected IEnumerable save(PXAdapter adapter)
  {
    RelationEntities graph = this;
    FilterEntity current1 = graph.HeaderEntity.Current;
    if (current1 != null && current1.EntityType != (System.Type) null && current1.Instance != null)
    {
      int length = 0;
      foreach (PXResult<RelationDetail> pxResult in PXSelectBase<RelationDetail, PXSelect<RelationDetail>.Config>.Select((PXGraph) graph))
      {
        RelationDetail relationDetail = (RelationDetail) pxResult;
        if (relationDetail.GroupMask.Length > length)
          length = relationDetail.GroupMask.Length;
      }
      byte[] numArray = new byte[length];
      using (IEnumerator<PXResult<RelationDetail>> enumerator = PXSelectBase<RelationDetail, PXSelect<RelationDetail>.Config>.Select((PXGraph) graph).GetEnumerator())
      {
label_15:
        while (enumerator.MoveNext())
        {
          RelationDetail current2 = (RelationDetail) enumerator.Current;
          bool? selected = current2.Selected;
          bool flag = true;
          if (selected.GetValueOrDefault() == flag & selected.HasValue)
          {
            int index = 0;
            while (true)
            {
              if (index < current2.GroupMask.Length && index < numArray.Length)
              {
                numArray[index] |= current2.GroupMask[index];
                ++index;
              }
              else
                goto label_15;
            }
          }
        }
      }
      PXCache cach = graph.Caches[current1.EntityType];
      cach.SetValue(current1.Instance, "GroupMask", (object) numArray);
      cach.Update(current1.Instance);
      if (!graph.Views.Caches.Contains(current1.EntityType))
        graph.Views.Caches.Add(current1.EntityType);
      if (!graph.Views.Caches.Contains(typeof (Neighbour)))
        graph.Views.Caches.Add(typeof (Neighbour));
      graph.populateNeighbours(length);
    }
    graph.DetailsEntity.Cache.Clear();
    graph.DetailsEntity.Cache.ClearQueryCache();
    yield return (object) current1;
    graph.Persist();
    graph.SelectTimeStamp();
  }

  [PXUIField(DisplayName = "Cancel", MapEnableRights = PXCacheRights.Select)]
  [PXCancelButton(ImageKey = "Cancel", Tooltip = "Refresh data")]
  protected IEnumerable cancel(PXAdapter adapter)
  {
    this.HeaderEntity.Cache.Clear();
    this.HeaderEntity.Cache.ClearQueryCache();
    this.DetailsEntity.Cache.Clear();
    this.DetailsEntity.Cache.ClearQueryCache();
    FilterEntity filterEntity1 = new FilterEntity()
    {
      EntityTypeName = (string) adapter.Searches[0]
    };
    this.FillEntityType(filterEntity1, filterEntity1.EntityTypeName);
    this.PopulateFilterEntities(filterEntity1);
    bool found = false;
    foreach (FilterEntity filterEntity2 in adapter.Get())
    {
      found = true;
      yield return (object) filterEntity2;
    }
    if (!found && adapter.Searches != null && adapter.Searches.Length == 2)
    {
      adapter.Searches[1] = (object) null;
      foreach (FilterEntity filterEntity3 in adapter.Get())
        yield return (object) filterEntity3;
    }
  }

  [PXUIField(DisplayName = "First", MapEnableRights = PXCacheRights.Select)]
  [PXFirstButton(ImageKey = "PageFirst", Tooltip = "First record", ConfirmationMessage = "Any unsaved changes will be discarded.")]
  protected IEnumerable first(PXAdapter adapter)
  {
    this.DetailsEntity.Cache.Clear();
    this.DetailsEntity.Cache.ClearQueryCache();
    FilterEntity filterEntity1 = (FilterEntity) null;
    foreach (FilterEntity filterEntity2 in this.HeaderEntity.Cache.Inserted)
    {
      if (filterEntity1 == null || string.Compare(filterEntity1.Entity, filterEntity2.Entity) > 0)
        filterEntity1 = filterEntity2;
    }
    if (filterEntity1 != null)
    {
      this.HeaderEntity.Current = filterEntity1;
      yield return (object) filterEntity1;
    }
  }

  [PXUIField(DisplayName = "Prev", MapEnableRights = PXCacheRights.Select)]
  [PXPreviousButton(ImageKey = "PagePrev", Tooltip = "Previous record", ConfirmationMessage = "Any unsaved changes will be discarded.")]
  protected IEnumerable previous(PXAdapter adapter)
  {
    this.DetailsEntity.Cache.Clear();
    this.DetailsEntity.Cache.ClearQueryCache();
    FilterEntity filterEntity1 = (FilterEntity) null;
    FilterEntity filterEntity2 = (FilterEntity) null;
    FilterEntity current = this.HeaderEntity.Current;
    foreach (FilterEntity filterEntity3 in this.HeaderEntity.Cache.Inserted)
    {
      if (filterEntity1 == null || string.Compare(filterEntity1.Entity, filterEntity3.Entity) < 0)
        filterEntity1 = filterEntity3;
      if (current != null && string.Compare(filterEntity3.Entity, current.Entity) < 0 && (filterEntity2 == null || string.Compare(filterEntity2.Entity, filterEntity3.Entity) < 0))
        filterEntity2 = filterEntity3;
    }
    if (filterEntity2 != null)
    {
      this.HeaderEntity.Current = filterEntity2;
      yield return (object) filterEntity2;
    }
    else if (filterEntity1 != null)
    {
      this.HeaderEntity.Current = filterEntity1;
      yield return (object) filterEntity1;
    }
  }

  [PXUIField(DisplayName = "Next", MapEnableRights = PXCacheRights.Select)]
  [PXNextButton(ImageKey = "PageNext", Tooltip = "Previous record", ConfirmationMessage = "Any unsaved changes will be discarded.")]
  protected IEnumerable next(PXAdapter adapter)
  {
    this.DetailsEntity.Cache.Clear();
    this.DetailsEntity.Cache.ClearQueryCache();
    FilterEntity filterEntity1 = (FilterEntity) null;
    FilterEntity filterEntity2 = (FilterEntity) null;
    FilterEntity current = this.HeaderEntity.Current;
    foreach (FilterEntity filterEntity3 in this.HeaderEntity.Cache.Inserted)
    {
      if (filterEntity1 == null || string.Compare(filterEntity1.Entity, filterEntity3.Entity) > 0)
        filterEntity1 = filterEntity3;
      if (current != null && string.Compare(filterEntity3.Entity, current.Entity) > 0 && (filterEntity2 == null || string.Compare(filterEntity2.Entity, filterEntity3.Entity) > 0))
        filterEntity2 = filterEntity3;
    }
    if (filterEntity2 != null)
    {
      this.HeaderEntity.Current = filterEntity2;
      yield return (object) filterEntity2;
    }
    else if (filterEntity1 != null)
    {
      this.HeaderEntity.Current = filterEntity1;
      yield return (object) filterEntity1;
    }
  }

  [PXUIField(DisplayName = "Last", MapEnableRights = PXCacheRights.Select)]
  [PXLastButton(ImageKey = "PageLast", Tooltip = "Last record", ConfirmationMessage = "Any unsaved changes will be discarded.")]
  protected IEnumerable last(PXAdapter adapter)
  {
    this.DetailsEntity.Cache.Clear();
    this.DetailsEntity.Cache.ClearQueryCache();
    FilterEntity filterEntity1 = (FilterEntity) null;
    foreach (FilterEntity filterEntity2 in this.HeaderEntity.Cache.Inserted)
    {
      if (filterEntity1 == null || string.Compare(filterEntity1.Entity, filterEntity2.Entity) < 0)
        filterEntity1 = filterEntity2;
    }
    if (filterEntity1 != null)
    {
      this.HeaderEntity.Current = filterEntity1;
      yield return (object) filterEntity1;
    }
  }

  private void FillEntityType(FilterEntity entity, string entityTypeName)
  {
    if (entity == null || string.IsNullOrEmpty(entityTypeName))
      return;
    System.Type type = PXBuildManager.GetType(entityTypeName, false);
    entity.EntityType = !(type == (System.Type) null) && !(type.GetProperty("GroupMask", typeof (byte[])) == (PropertyInfo) null) && !(type == typeof (RelationGroup)) && !type.IsSubclassOf(typeof (RelationGroup)) ? type : throw new PXSetPropertyException("An invalid entity type has been specified.");
  }

  private void PopulateFilterEntities(FilterEntity header)
  {
    if (header == null || !(header.EntityType != (System.Type) null))
      return;
    PXSelectBase instance;
    if (header.EntityType == typeof (EMailAccount))
      instance = (PXSelectBase) Activator.CreateInstance(typeof (PXSelect<,>).MakeGenericType(header.EntityType, typeof (Where<EMailAccount.userID, IsNull, And<Match<Current<AccessInfo.userName>>>>)), (object) this);
    else
      instance = (PXSelectBase) Activator.CreateInstance(typeof (PXSelect<>).MakeGenericType(header.EntityType), (object) this);
    int num = 0;
    List<int> intList = new List<int>();
    foreach (PXEventSubscriberAttribute attribute in instance.Cache.GetAttributes((string) null))
    {
      if (attribute is IPXInterfaceField pxInterfaceField && (pxInterfaceField.Visibility & PXUIVisibility.SelectorVisible) == PXUIVisibility.SelectorVisible)
        intList.Add(attribute.FieldOrdinal);
    }
    foreach (object data in instance.View.SelectMulti())
    {
      FilterEntity filterEntity = new FilterEntity();
      filterEntity.ID = new int?(num++);
      filterEntity.EntityTypeName = header.EntityTypeName;
      filterEntity.EntityType = header.EntityType;
      filterEntity.Instance = data;
      if (intList.Count == 0)
        filterEntity.Entity = instance.Cache.ObjectToString(data);
      else if (intList.Count == 1)
      {
        filterEntity.Entity = instance.Cache.GetValue(data, intList[0]).ToString();
      }
      else
      {
        filterEntity.Entity = EntityHelper.GetEntityDescription((PXGraph) this, data);
        if (string.IsNullOrEmpty(filterEntity.Entity))
        {
          StringBuilder stringBuilder = new StringBuilder();
          object obj1 = instance.Cache.GetValue(data, intList[0]);
          if (obj1 != null)
            stringBuilder = stringBuilder.Append(obj1.ToString());
          for (int index = 1; index < intList.Count; ++index)
          {
            if (index != intList.Count - 1)
              stringBuilder.Append(", ");
            else
              stringBuilder.Append(",");
            object obj2 = instance.Cache.GetValue(data, intList[index]);
            if (obj2 != null)
              stringBuilder.Append(obj2.ToString());
          }
          filterEntity.Entity = stringBuilder.ToString().Trim();
        }
      }
      this.HeaderEntity.Cache.Insert((object) filterEntity);
      if (num == 1)
      {
        header.Entity = filterEntity.Entity;
        header = filterEntity;
      }
    }
    this.HeaderEntity.Cache.IsDirty = false;
  }

  protected void FilterEntity_RowPersisting(PXCache cache, PXRowPersistingEventArgs e)
  {
    e.Cancel = true;
  }

  private void populateNeighbours(int length)
  {
    byte[] numArray1 = new byte[length];
    System.Type key1 = (System.Type) null;
    PXCache pxCache = (PXCache) null;
    foreach (FilterEntity filterEntity in this.HeaderEntity.Cache.Inserted)
    {
      if (key1 == (System.Type) null && filterEntity.EntityType != (System.Type) null)
      {
        key1 = filterEntity.EntityType;
        pxCache = this.Caches[key1];
      }
      if (pxCache != null && filterEntity.Instance != null)
      {
        byte[] numArray2 = pxCache.GetValue(filterEntity.Instance, "GroupMask") as byte[];
        for (int index = 0; index < numArray1.Length; ++index)
          numArray1[index] |= numArray2 == null || index >= numArray2.Length ? (byte) 0 : numArray2[index];
      }
    }
    if (!(key1 != (System.Type) null))
      return;
    bool flag1 = false;
    foreach (byte num in numArray1)
    {
      if (num != (byte) 0)
      {
        flag1 = true;
        break;
      }
    }
    byte[] numArray3 = new byte[numArray1.Length];
    byte[] numArray4 = new byte[numArray1.Length];
    byte[] numArray5 = new byte[numArray1.Length];
    byte[] numArray6 = new byte[numArray1.Length];
    foreach (PXResult<RelationGroup> pxResult in PXSelectBase<RelationGroup, PXSelectReadonly<RelationGroup>.Config>.Select((PXGraph) this))
    {
      RelationGroup relationGroup = (RelationGroup) pxResult;
      for (int index = 0; index < relationGroup.GroupMask.Length; ++index)
      {
        if (relationGroup.GroupType == "EE")
          numArray4[index] |= (byte) ((uint) numArray1[index] & (uint) relationGroup.GroupMask[index]);
        else if (relationGroup.GroupType == "IE")
          numArray3[index] |= (byte) ((uint) numArray1[index] & (uint) relationGroup.GroupMask[index]);
        else if (relationGroup.GroupType == "EO")
          numArray6[index] |= (byte) ((uint) numArray1[index] & (uint) relationGroup.GroupMask[index]);
        else if (relationGroup.GroupType == "IO")
          numArray5[index] |= (byte) ((uint) numArray1[index] & (uint) relationGroup.GroupMask[index]);
      }
    }
    Dictionary<string, byte[][]> dictionary1 = new Dictionary<string, byte[][]>();
    PXCache cach = this.Caches[typeof (Neighbour)];
    bool flag2 = false;
    foreach (PXResult<Neighbour> pxResult in PXSelectBase<Neighbour, PXSelect<Neighbour>.Config>.Select((PXGraph) this))
    {
      Neighbour neighbour = (Neighbour) pxResult;
      if (neighbour.LeftEntityType == neighbour.RightEntityType)
      {
        if (neighbour.LeftEntityType == key1.FullName)
        {
          flag2 = true;
          neighbour.CoverageMask = numArray3;
          neighbour.InverseMask = numArray4;
          neighbour.WinCoverageMask = numArray5;
          neighbour.WinInverseMask = numArray6;
          cach.Update((object) neighbour);
        }
        else
        {
          byte[] numArray7 = (byte[]) numArray3.Clone();
          byte[] numArray8 = (byte[]) numArray4.Clone();
          byte[] numArray9 = (byte[]) numArray5.Clone();
          byte[] numArray10 = (byte[]) numArray6.Clone();
          for (int index = 0; index < numArray7.Length; ++index)
          {
            numArray7[index] &= index < neighbour.CoverageMask.Length ? neighbour.CoverageMask[index] : (byte) 0;
            numArray8[index] &= index < neighbour.InverseMask.Length ? neighbour.InverseMask[index] : (byte) 0;
          }
          for (int index = 0; index < numArray9.Length; ++index)
          {
            numArray9[index] &= index < neighbour.WinCoverageMask.Length ? neighbour.WinCoverageMask[index] : (byte) 0;
            numArray10[index] &= index < neighbour.WinInverseMask.Length ? neighbour.WinInverseMask[index] : (byte) 0;
          }
          dictionary1[neighbour.LeftEntityType] = new byte[4][]
          {
            numArray7,
            numArray8,
            numArray9,
            numArray10
          };
        }
      }
    }
    if (!flag2 & flag1)
      cach.Insert((object) new Neighbour()
      {
        LeftEntityType = key1.FullName,
        RightEntityType = key1.FullName,
        CoverageMask = numArray3,
        InverseMask = numArray4,
        WinCoverageMask = numArray5,
        WinInverseMask = numArray6
      });
    Dictionary<string, byte[][]> dictionary2 = new Dictionary<string, byte[][]>((IDictionary<string, byte[][]>) dictionary1);
    foreach (PXResult<Neighbour> pxResult in PXSelectBase<Neighbour, PXSelect<Neighbour, Where<Neighbour.leftEntityType, Equal<Required<Neighbour.leftEntityType>>>>.Config>.Select((PXGraph) this, (object) key1.FullName))
    {
      Neighbour neighbour = (Neighbour) pxResult;
      if (dictionary1.ContainsKey(neighbour.RightEntityType))
      {
        neighbour.CoverageMask = dictionary1[neighbour.RightEntityType][0];
        neighbour.InverseMask = dictionary1[neighbour.RightEntityType][1];
        neighbour.WinCoverageMask = dictionary1[neighbour.RightEntityType][2];
        neighbour.WinInverseMask = dictionary1[neighbour.RightEntityType][3];
        cach.Update((object) neighbour);
        dictionary1.Remove(neighbour.RightEntityType);
      }
    }
    if (flag1 && dictionary1.Count > 0)
    {
      foreach (string key2 in dictionary1.Keys)
        cach.Insert((object) new Neighbour()
        {
          LeftEntityType = key1.FullName,
          RightEntityType = key2,
          CoverageMask = dictionary1[key2][0],
          InverseMask = dictionary1[key2][1],
          WinCoverageMask = dictionary1[key2][2],
          WinInverseMask = dictionary1[key2][3]
        });
    }
    Dictionary<string, byte[][]> dictionary3 = dictionary2;
    foreach (PXResult<Neighbour> pxResult in PXSelectBase<Neighbour, PXSelect<Neighbour, Where<Neighbour.rightEntityType, Equal<Required<Neighbour.rightEntityType>>>>.Config>.Select((PXGraph) this, (object) key1.FullName))
    {
      Neighbour neighbour = (Neighbour) pxResult;
      if (dictionary3.ContainsKey(neighbour.LeftEntityType))
      {
        neighbour.CoverageMask = dictionary3[neighbour.LeftEntityType][0];
        neighbour.InverseMask = dictionary3[neighbour.LeftEntityType][1];
        neighbour.WinCoverageMask = dictionary3[neighbour.LeftEntityType][2];
        neighbour.WinInverseMask = dictionary3[neighbour.LeftEntityType][3];
        cach.Update((object) neighbour);
        dictionary3.Remove(neighbour.LeftEntityType);
      }
    }
    if (!flag1 || dictionary3.Count <= 0)
      return;
    foreach (string key3 in dictionary3.Keys)
      cach.Insert((object) new Neighbour()
      {
        LeftEntityType = key3,
        RightEntityType = key1.FullName,
        CoverageMask = dictionary3[key3][0],
        InverseMask = dictionary3[key3][1],
        WinCoverageMask = dictionary3[key3][2],
        WinInverseMask = dictionary3[key3][3]
      });
  }

  public override void Persist()
  {
    this.BAccountRestrictionHelper.Persist();
    base.Persist();
    GroupHelper.Clear();
  }
}
