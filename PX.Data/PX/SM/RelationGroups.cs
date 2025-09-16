// Decompiled with JetBrains decompiler
// Type: PX.SM.RelationGroups
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.Descriptor;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web.Compilation;

#nullable disable
namespace PX.SM;

public class RelationGroups : PXGraph<RelationGroups>
{
  private const string MaskedSlotKey = "Masked_List_{0}";
  public PXSelectReadonly<MaskedType> MaskedTypes;
  public PXSelect<MaskedEntity> DetailsGroup;
  public PXSelect<RelationHeader> HeaderGroup;
  private bool refreshGroup;
  public PXAction<RelationHeader> save;
  public PXAction<RelationHeader> cancel;
  public PXInsert<RelationHeader> Insert;
  public PXFirst<RelationHeader> First;
  public PXPrevious<RelationHeader> Previous;
  public PXNext<RelationHeader> Next;
  public PXLast<RelationHeader> Last;

  [InjectDependency]
  internal IBAccountRestrictionHelper BAccountRestrictionHelper { get; set; }

  protected IEnumerable maskedtypes() => RelationGroups.GetMaskedTypes();

  internal static IEnumerable GetMaskedTypes()
  {
    List<MaskedType> slot = PXDatabase.GetSlot<List<MaskedType>>($"Masked_List_{CultureInfo.CurrentCulture.Name}");
    if (slot.Count > 0)
      return (IEnumerable) slot;
    foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
    {
      try
      {
        if (PXSubstManager.IsSuitableTypeExportAssembly(assembly))
        {
          System.Type[] typeArray = (System.Type[]) null;
          try
          {
            if (!assembly.IsDynamic)
              typeArray = assembly.GetExportedTypes();
          }
          catch (ReflectionTypeLoadException ex)
          {
            typeArray = ex.Types;
          }
          if (typeArray != null)
          {
            foreach (System.Type c in typeArray)
            {
              PropertyInfo property;
              PXDBGroupMaskAttribute customAttribute1;
              if (c != (System.Type) null && typeof (IBqlTable).IsAssignableFrom(c) && (property = c.GetProperty("GroupMask", typeof (byte[]))) != (PropertyInfo) null && property.DeclaringType == c && ((customAttribute1 = property.GetCustomAttribute<PXDBGroupMaskAttribute>()) == null || !customAttribute1.HideFromEntityTypesList) && c != typeof (RelationGroup) && !c.IsSubclassOf(typeof (RelationGroup)) && !c.IsDefined(typeof (PXProjectionAttribute), true) && PXDatabase.Provider.GetTables().Contains<string>(c.Name))
              {
                MaskedType maskedType = new MaskedType();
                maskedType.EntityTypeName = c.FullName;
                if (c.IsDefined(typeof (PXCacheNameAttribute), true))
                {
                  PXCacheNameAttribute customAttribute2 = (PXCacheNameAttribute) c.GetCustomAttributes(typeof (PXCacheNameAttribute), true)[0];
                  maskedType.Text = customAttribute2.GetName();
                }
                slot.Add(maskedType);
              }
            }
          }
        }
      }
      catch
      {
      }
    }
    slot.Sort((Comparison<MaskedType>) ((m1, m2) => string.Compare(m1.EntityTypeName, m2.EntityTypeName, StringComparison.OrdinalIgnoreCase)));
    return (IEnumerable) slot;
  }

  protected IEnumerable detailsGroup()
  {
    RelationGroups relationGroups = this;
    RelationHeader header = relationGroups.HeaderGroup.Current;
    if (header == null || header.EntityType == (System.Type) null || string.IsNullOrEmpty(header.GroupName))
    {
      relationGroups.DetailsGroup.Cache.Clear();
      relationGroups.DetailsGroup.Cache.ClearQueryCache();
    }
    else
    {
      object newValue = (object) (header.EntityTypeName ?? header.EntityType.FullName);
      try
      {
        relationGroups.HeaderGroup.Cache.RaiseFieldVerifying<RelationHeader.entityTypeName>((object) header, ref newValue);
      }
      catch
      {
        yield break;
      }
      bool meet = false;
      foreach (MaskedEntity maskedEntity in relationGroups.DetailsGroup.Cache.Inserted)
      {
        if (!meet)
        {
          if (maskedEntity.Instance == null || maskedEntity.Instance.GetType() != header.EntityType)
          {
            meet = false;
            relationGroups.DetailsGroup.Cache.Clear();
            relationGroups.DetailsGroup.Cache.ClearQueryCache();
            break;
          }
          meet = true;
        }
        yield return (object) maskedEntity;
      }
      if (!meet)
      {
        PXSelectBase cmd;
        if (header.EntityType == typeof (EMailAccount))
          cmd = (PXSelectBase) Activator.CreateInstance(typeof (PXSelect<,>).MakeGenericType(header.EntityType, typeof (Where<EMailAccount.userID, IsNull>)), (object) relationGroups);
        else
          cmd = (PXSelectBase) Activator.CreateInstance(typeof (PXSelect<>).MakeGenericType(header.EntityType), (object) relationGroups);
        int cnt = 0;
        List<int> fields = new List<int>();
        foreach (PXEventSubscriberAttribute attribute in cmd.Cache.GetAttributes((string) null))
        {
          if (attribute is IPXInterfaceField pxInterfaceField && (pxInterfaceField.Visibility & PXUIVisibility.SelectorVisible) == PXUIVisibility.SelectorVisible)
            fields.Add(attribute.FieldOrdinal);
        }
        foreach (object obj1 in cmd.View.SelectMulti())
        {
          if (relationGroups.CanBeRestricted(header.EntityType, obj1))
          {
            MaskedEntity maskedEntity = new MaskedEntity();
            maskedEntity.ID = new int?(cnt++);
            maskedEntity.Instance = obj1;
            if (fields.Count == 0)
              maskedEntity.Entity = cmd.Cache.ObjectToString(obj1);
            else if (fields.Count == 1)
            {
              maskedEntity.Entity = cmd.Cache.GetValue(obj1, fields[0]).ToString();
            }
            else
            {
              StringBuilder stringBuilder = new StringBuilder();
              object obj2 = cmd.Cache.GetValue(obj1, fields[0]);
              if (obj2 != null)
                stringBuilder = stringBuilder.Append(obj2.ToString());
              for (int index = 1; index < fields.Count; ++index)
              {
                if (index != fields.Count - 1)
                  stringBuilder.Append(", ");
                else
                  stringBuilder.Append(",");
                object obj3 = cmd.Cache.GetValue(obj1, fields[index]);
                if (obj3 != null)
                  stringBuilder.Append(obj3.ToString());
              }
              maskedEntity.Entity = stringBuilder.ToString();
            }
            byte[] numArray = (byte[]) cmd.Cache.GetValue(obj1, "GroupMask");
            if (numArray != null)
            {
              bool flag = true;
              for (int index = 0; index < header.GroupMask.Length; ++index)
              {
                if ((int) header.GroupMask[index] != (int) (byte) ((index < numArray.Length ? (int) numArray[index] : 0) & (int) header.GroupMask[index]))
                {
                  flag = false;
                  break;
                }
              }
              maskedEntity.Selected = !flag ? new bool?(false) : new bool?(true);
            }
            yield return relationGroups.DetailsGroup.Cache.Insert((object) maskedEntity);
          }
        }
        cmd = (PXSelectBase) null;
        fields = (List<int>) null;
      }
      relationGroups.DetailsGroup.Cache.IsDirty = false;
    }
  }

  public RelationGroups()
  {
    this.DetailsGroup.Cache.AllowDelete = false;
    this.DetailsGroup.Cache.AllowInsert = false;
    PXUIFieldAttribute.SetDisplayName<Graph.text>(this.Caches[typeof (Graph)], "Module");
  }

  public override IEnumerable ExecuteSelect(
    string viewName,
    object[] parameters,
    object[] searches,
    string[] sortcolumns,
    bool[] descendings,
    PXFilterRow[] filters,
    ref int startRow,
    int maximumRows,
    ref int totalRows)
  {
    if (this.refreshGroup && viewName == "DetailsGroup")
      startRow = 0;
    return base.ExecuteSelect(viewName, parameters, searches, sortcolumns, descendings, filters, ref startRow, maximumRows, ref totalRows);
  }

  [PXUIField(DisplayName = "Save", MapEnableRights = PXCacheRights.Update, MapViewRights = PXCacheRights.Update)]
  [PXSaveButton]
  protected IEnumerable Save(PXAdapter adapter)
  {
    RelationGroups relationGroups = this;
    if (!adapter.View.Cache.AllowUpdate)
      throw new PXException("The record cannot be saved.");
    RelationHeader header = relationGroups.HeaderGroup.Current;
    if (header != null && header.EntityType != (System.Type) null)
    {
      if (!relationGroups.Views.Caches.Contains(header.EntityType))
        relationGroups.Views.Caches.Add(header.EntityType);
      PXCache cach = relationGroups.Caches[header.EntityType];
    }
    if (!relationGroups.Views.Caches.Contains(typeof (Neighbour)))
      relationGroups.Views.Caches.Add(typeof (Neighbour));
    if (relationGroups.HeaderGroup.Cache.IsDirty || relationGroups.DetailsGroup.Cache.IsDirty)
      relationGroups.populateNeighbours();
    foreach (object obj in adapter.Get())
      yield return obj;
    relationGroups.Persist();
    relationGroups.SelectTimeStamp();
    GroupHelper.Clear();
    relationGroups.HeaderGroup.Cache.SetStatus((object) header, PXEntryStatus.Updated);
    relationGroups.HeaderGroup.Cache.IsDirty = false;
  }

  [PXUIField(DisplayName = "Cancel", MapEnableRights = PXCacheRights.Select)]
  [PXButton(ImageKey = "Cancel", Tooltip = "Refresh data")]
  protected IEnumerable Cancel(PXAdapter adapter)
  {
    RelationGroups graph = this;
    RelationHeader current = graph.HeaderGroup.Current;
    string groupName = (string) null;
    System.Type entityType = (System.Type) null;
    string entityTypeName = (string) null;
    if (current != null)
    {
      groupName = current.GroupName;
      entityType = current.EntityType;
      entityTypeName = current.EntityTypeName;
    }
    graph.DetailsGroup.Cache.Clear();
    graph.DetailsGroup.Cache.ClearQueryCache();
    foreach (RelationHeader relationHeader in new PXCancel<RelationHeader>((PXGraph) graph, nameof (Cancel)).Press(adapter))
    {
      if (relationHeader.GroupName == groupName)
      {
        relationHeader.EntityTypeName = entityTypeName;
        relationHeader.EntityType = entityType;
        if (graph.HeaderGroup.Cache.GetStatus((object) relationHeader) == PXEntryStatus.Notchanged)
        {
          graph.HeaderGroup.Cache.SetStatus((object) relationHeader, PXEntryStatus.Updated);
          graph.HeaderGroup.Cache.IsDirty = false;
        }
      }
      yield return (object) relationHeader;
    }
  }

  protected void RelationHeader_RowInserted(PXCache cache, PXRowInsertedEventArgs e)
  {
    RelationGroup row = (RelationGroup) e.Row;
    byte[] numArray;
    if (GroupHelper.Count == 0)
      numArray = new byte[4]
      {
        (byte) 128 /*0x80*/,
        (byte) 0,
        (byte) 0,
        (byte) 0
      };
    else if (GroupHelper.Count == 0 || GroupHelper.Count % 32 /*0x20*/ != 0)
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
      byte[] coverageMask = neighbour.CoverageMask;
      Array.Resize<byte>(ref coverageMask, numArray.Length);
      neighbour.CoverageMask = coverageMask;
      byte[] inverseMask = neighbour.InverseMask;
      Array.Resize<byte>(ref inverseMask, numArray.Length);
      neighbour.InverseMask = inverseMask;
      byte[] array = neighbour.WinCoverageMask;
      Array.Resize<byte>(ref array, numArray.Length);
      neighbour.WinCoverageMask = array;
      array = neighbour.WinInverseMask;
      Array.Resize<byte>(ref array, numArray.Length);
      neighbour.WinInverseMask = array;
      cach.Update((object) neighbour);
    }
    cach.IsDirty = false;
  }

  protected void RelationHeader_RowUpdated(PXCache cache, PXRowUpdatedEventArgs e)
  {
    RelationHeader row = e.Row as RelationHeader;
    RelationHeader oldRow = e.OldRow as RelationHeader;
    if (row == null || oldRow == null || !(row.Description == oldRow.Description))
      return;
    bool? active1 = row.Active;
    bool? active2 = oldRow.Active;
    if (!(active1.GetValueOrDefault() == active2.GetValueOrDefault() & active1.HasValue == active2.HasValue) || !(row.GroupType == oldRow.GroupType))
      return;
    cache.IsDirty = false;
  }

  protected void RelationHeader_EntityTypeName_FieldVerifying(
    PXCache cache,
    PXFieldVerifyingEventArgs e)
  {
    string newValue = e.NewValue as string;
    RelationHeader row = e.Row as RelationHeader;
    if (!string.IsNullOrEmpty(newValue))
    {
      System.Type type = PXBuildManager.GetType(newValue, false);
      if (type == (System.Type) null || type.GetProperty("GroupMask", typeof (byte[])) == (PropertyInfo) null || type == typeof (RelationGroup) || type == typeof (RelationHeader))
        throw new PXSetPropertyException("An invalid entity type has been specified.");
      if (row != null)
        row.EntityType = type;
    }
    else if (row != null)
      row.EntityType = (System.Type) null;
    this.refreshGroup = true;
  }

  protected void RelationHeader_EntityTypeName_FieldSelecting(
    PXCache sender,
    PXFieldSelectingEventArgs e)
  {
    string returnValue = e.ReturnValue as string;
    if (string.IsNullOrEmpty(returnValue))
      return;
    foreach (MaskedType maskedtype in this.maskedtypes())
    {
      if (maskedtype.EntityTypeName == returnValue)
      {
        e.ReturnValue = (object) maskedtype.Text;
        break;
      }
    }
  }

  protected void RelationHeader_EntityTypeName_FieldUpdating(
    PXCache sender,
    PXFieldUpdatingEventArgs e)
  {
    string newValue = e.NewValue as string;
    if (string.IsNullOrEmpty(newValue))
      return;
    foreach (MaskedType maskedtype in this.maskedtypes())
    {
      if (maskedtype.Text == newValue)
      {
        e.NewValue = (object) maskedtype.EntityTypeName;
        break;
      }
    }
  }

  protected void RelationHeader_SpecificType_FieldSelecting(
    PXCache sender,
    PXFieldSelectingEventArgs e)
  {
    string returnValue = e.ReturnValue as string;
    if (string.IsNullOrEmpty(returnValue))
      return;
    foreach (MaskedType maskedType in RelationGroups.GetMaskedTypes())
    {
      if (maskedType.EntityTypeName == returnValue)
      {
        e.ReturnValue = (object) maskedType.Text;
        break;
      }
    }
  }

  protected void RelationHeader_SpecificType_FieldUpdating(
    PXCache sender,
    PXFieldUpdatingEventArgs e)
  {
    string newValue = e.NewValue as string;
    if (string.IsNullOrEmpty(newValue))
      return;
    foreach (MaskedType maskedType in RelationGroups.GetMaskedTypes())
    {
      if (maskedType.Text == newValue)
      {
        e.NewValue = (object) maskedType.EntityTypeName;
        break;
      }
    }
  }

  protected void MaskedEntity_RowPersisting(PXCache cache, PXRowPersistingEventArgs e)
  {
    RelationHeader current = this.HeaderGroup.Current;
    MaskedEntity row = e.Row as MaskedEntity;
    if (current != null && current.EntityType != (System.Type) null && row != null && row.Instance != null)
    {
      PXCache cach = this.Caches[current.EntityType];
      byte[] array = (byte[]) cach.GetValue(row.Instance, "GroupMask");
      bool flag1 = false;
      if (array != null)
      {
        flag1 = true;
        for (int index = 0; index < current.GroupMask.Length; ++index)
        {
          if ((int) current.GroupMask[index] != (int) (byte) ((index < array.Length ? (int) array[index] : 0) & (int) current.GroupMask[index]))
          {
            flag1 = false;
            break;
          }
        }
      }
      int num1 = flag1 ? 1 : 0;
      bool? selected = row.Selected;
      int num2 = selected.GetValueOrDefault() ? 1 : 0;
      if (!(num1 == num2 & selected.HasValue))
      {
        if (array == null)
        {
          byte[] numArray = (byte[]) current.GroupMask.Clone();
          cach.SetValue(row.Instance, "GroupMask", (object) numArray);
        }
        else
        {
          if (array.Length < current.GroupMask.Length)
          {
            Array.Resize<byte>(ref array, current.GroupMask.Length);
            cach.SetValue(row.Instance, "GroupMask", (object) array);
          }
          for (int index = 0; index < current.GroupMask.Length; ++index)
          {
            selected = row.Selected;
            bool flag2 = true;
            array[index] = !(selected.GetValueOrDefault() == flag2 & selected.HasValue) ? (byte) ((uint) array[index] & (uint) ~current.GroupMask[index]) : (byte) ((uint) array[index] | (uint) current.GroupMask[index]);
          }
        }
        cach.Update(row.Instance);
      }
    }
    e.Cancel = true;
  }

  private void populateNeighbours()
  {
    RelationHeader current = this.HeaderGroup.Current;
    if (current == null)
      return;
    bool flag1 = false;
    foreach (MaskedEntity maskedEntity in this.DetailsGroup.Cache.Inserted)
    {
      bool? selected = maskedEntity.Selected;
      bool flag2 = true;
      if (selected.GetValueOrDefault() == flag2 & selected.HasValue)
      {
        flag1 = true;
        break;
      }
    }
    List<string> collection = new List<string>();
    PXCache cach = this.Caches[typeof (Neighbour)];
    bool flag3 = false;
    foreach (PXResult<Neighbour> pxResult in PXSelectBase<Neighbour, PXSelect<Neighbour>.Config>.Select((PXGraph) this))
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
        cach.Update((object) neighbour);
      }
      bool flag4 = true;
      for (int index = 0; index < current.GroupMask.Length; ++index)
      {
        if ((int) (byte) ((int) current.GroupMask[index] & (index < neighbour.CoverageMask.Length ? (int) neighbour.CoverageMask[index] : 0)) != (int) current.GroupMask[index] && (int) (byte) ((int) current.GroupMask[index] & (index < neighbour.InverseMask.Length ? (int) neighbour.InverseMask[index] : 0)) != (int) current.GroupMask[index] && (int) (byte) ((int) current.GroupMask[index] & (index < neighbour.WinCoverageMask.Length ? (int) neighbour.WinCoverageMask[index] : 0)) != (int) current.GroupMask[index] && (int) (byte) ((int) current.GroupMask[index] & (index < neighbour.WinInverseMask.Length ? (int) neighbour.WinInverseMask[index] : 0)) != (int) current.GroupMask[index])
        {
          flag4 = false;
          break;
        }
      }
      if (current.EntityType != (System.Type) null && neighbour.LeftEntityType == neighbour.RightEntityType && neighbour.LeftEntityType == current.EntityType.FullName)
      {
        flag3 = true;
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
        cach.Update((object) neighbour);
      }
      else if (flag4)
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
        cach.Update((object) neighbour);
        if (neighbour.LeftEntityType == neighbour.RightEntityType)
          collection.Add(neighbour.LeftEntityType);
      }
    }
    if (!flag3 & flag1)
    {
      Neighbour neighbour = new Neighbour();
      neighbour.LeftEntityType = current.EntityType.FullName;
      neighbour.RightEntityType = current.EntityType.FullName;
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
      cach.Insert((object) neighbour);
    }
    if (!(current.EntityType != (System.Type) null))
      return;
    List<string> stringList1 = new List<string>((IEnumerable<string>) collection);
    foreach (PXResult<Neighbour> pxResult in PXSelectBase<Neighbour, PXSelect<Neighbour, Where<Neighbour.leftEntityType, Equal<Required<Neighbour.leftEntityType>>>>.Config>.Select((PXGraph) this, (object) current.EntityType.FullName))
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
            neighbour.CoverageMask[index] &= ~current.GroupMask[index];
            neighbour.InverseMask[index] &= ~current.GroupMask[index];
          }
        }
        cach.Update((object) neighbour);
        collection.Remove(neighbour.RightEntityType);
      }
    }
    if (flag1 && collection.Count > 0)
    {
      foreach (string str in collection)
      {
        Neighbour neighbour = new Neighbour();
        neighbour.LeftEntityType = current.EntityType.FullName;
        neighbour.RightEntityType = str;
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
        cach.Insert((object) neighbour);
      }
    }
    List<string> stringList2 = stringList1;
    foreach (PXResult<Neighbour> pxResult in PXSelectBase<Neighbour, PXSelect<Neighbour, Where<Neighbour.rightEntityType, Equal<Required<Neighbour.rightEntityType>>>>.Config>.Select((PXGraph) this, (object) current.EntityType.FullName))
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
        cach.Update((object) neighbour);
        stringList2.Remove(neighbour.LeftEntityType);
      }
    }
    if (!flag1 || stringList2.Count <= 0)
      return;
    foreach (string str in stringList2)
    {
      Neighbour neighbour = new Neighbour();
      neighbour.LeftEntityType = str;
      neighbour.RightEntityType = current.EntityType.FullName;
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
      cach.Insert((object) neighbour);
    }
  }

  public override void Persist()
  {
    this.BAccountRestrictionHelper.Persist();
    base.Persist();
    GroupHelper.Clear();
  }

  public virtual bool CanBeRestricted(System.Type entityType, object instance) => true;
}
