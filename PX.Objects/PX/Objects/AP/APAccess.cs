// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.APAccess
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.SM;
using System;
using System.Collections;

#nullable disable
namespace PX.Objects.AP;

public class APAccess : BaseAccess
{
  public PXSelect<PX.Objects.AP.Vendor> Vendor;
  public PXSetup<PX.Objects.AP.APSetup> APSetup;

  [PXDBString(30, IsUnicode = true, IsKey = true, InputMask = "")]
  [PXUIField(DisplayName = "Vendor ID", Visibility = PXUIVisibility.SelectorVisible)]
  [PXDimensionSelector("VENDOR", typeof (Search2<PX.Objects.AP.Vendor.acctCD, LeftJoin<PX.Objects.CR.Contact, On<PX.Objects.CR.Contact.bAccountID, Equal<PX.Objects.AP.Vendor.bAccountID>, And<PX.Objects.CR.Contact.contactID, Equal<PX.Objects.AP.Vendor.defContactID>>>, LeftJoin<PX.Objects.CR.Address, On<PX.Objects.CR.Address.bAccountID, Equal<PX.Objects.AP.Vendor.bAccountID>, And<PX.Objects.CR.Address.addressID, Equal<PX.Objects.AP.Vendor.defAddressID>>>>>>), typeof (PX.Objects.AP.Vendor.acctCD), new System.Type[] {typeof (PX.Objects.AP.Vendor.acctCD), typeof (PX.Objects.AP.Vendor.acctName), typeof (PX.Objects.AP.Vendor.vendorClassID), typeof (PX.Objects.AP.Vendor.vStatus), typeof (PX.Objects.CR.Contact.phone1), typeof (PX.Objects.CR.Address.city), typeof (PX.Objects.CR.Address.countryID)})]
  protected virtual void Vendor_AcctCD_CacheAttached(PXCache sender)
  {
  }

  [PXDBString(128 /*0x80*/, IsKey = true, InputMask = "")]
  [PXDefault]
  [PXUIField(DisplayName = "Group Name", Visibility = PXUIVisibility.SelectorVisible)]
  [APAccess.APRelationGroupVendorSelector(typeof (RelationGroup.groupName), Filterable = true)]
  protected virtual void RelationGroup_GroupName_CacheAttached(PXCache sender)
  {
  }

  public APAccess()
  {
    PX.Objects.AP.APSetup current = this.APSetup.Current;
    this.Vendor.Cache.AllowDelete = false;
    PXUIFieldAttribute.SetEnabled(this.Vendor.Cache, (string) null, false);
    PXUIFieldAttribute.SetEnabled<PX.Objects.AP.Vendor.included>(this.Vendor.Cache, (object) null);
    PXUIFieldAttribute.SetEnabled<PX.Objects.AP.Vendor.acctCD>(this.Vendor.Cache, (object) null);
    PXUIFieldAttribute.SetVisible<PX.SM.Users.username>(this.Caches[typeof (PX.SM.Users)], (object) null);
  }

  protected virtual IEnumerable vendor()
  {
    APAccess apAccess = this;
    if (apAccess.Group.Current != null && !string.IsNullOrEmpty(apAccess.Group.Current.GroupName))
    {
      bool inserted = apAccess.Group.Cache.GetStatus((object) apAccess.Group.Current) == PXEntryStatus.Inserted;
      APAccess graph = apAccess;
      object[] objArray = new object[1]
      {
        (object) new byte[0]
      };
      foreach (PXResult<PX.Objects.AP.Vendor> pxResult in PXSelectBase<PX.Objects.AP.Vendor, PXSelect<PX.Objects.AP.Vendor, Where2<Match<Current<RelationGroup.groupName>>, Or<Match<Required<PX.Objects.AP.Vendor.groupMask>>>>>.Config>.Select((PXGraph) graph, objArray))
      {
        PX.Objects.AP.Vendor item = (PX.Objects.AP.Vendor) pxResult;
        if (!inserted)
        {
          apAccess.Vendor.Current = item;
          yield return (object) item;
        }
        else if (item.GroupMask != null)
        {
          RelationGroup group = apAccess.Group.Current;
          bool anyGroup = false;
          for (int i = 0; i < item.GroupMask.Length && i < group.GroupMask.Length; ++i)
          {
            if (group.GroupMask[i] != (byte) 0 && ((int) item.GroupMask[i] & (int) group.GroupMask[i]) == (int) group.GroupMask[i])
            {
              apAccess.Vendor.Current = item;
              yield return (object) item;
            }
            anyGroup |= item.GroupMask[i] > (byte) 0;
          }
          if (!anyGroup)
          {
            apAccess.Vendor.Current = item;
            yield return (object) item;
          }
          group = (RelationGroup) null;
        }
        item = (PX.Objects.AP.Vendor) null;
      }
    }
  }

  protected override void RelationGroup_RowInserted(PXCache cache, PXRowInsertedEventArgs e)
  {
    base.RelationGroup_RowInserted(cache, e);
    ((RelationGroup) e.Row).SpecificModule = typeof (accountsPayableModule).Namespace;
  }

  protected virtual void RelationGroup_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    if (!(e.Row is RelationGroup row))
      return;
    if (string.IsNullOrEmpty(row.GroupName))
    {
      this.Save.SetEnabled(false);
      this.Vendor.Cache.AllowInsert = false;
    }
    else
    {
      this.Save.SetEnabled(true);
      this.Vendor.Cache.AllowInsert = true;
    }
  }

  protected virtual void Vendor_AcctCD_FieldDefaulting(PXCache sender, PXFieldDefaultingEventArgs e)
  {
    e.Cancel = true;
  }

  protected virtual void Vendor_AcctCD_FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
  {
    if (PXSelectorAttribute.Select<PX.Objects.AP.Vendor.acctCD>(sender, e.Row, e.NewValue) == null)
      throw new PXSetPropertyException("'{0}' cannot be found in the system.", new object[1]
      {
        (object) "VendorID"
      });
  }

  protected virtual void Vendor_RowInserted(PXCache sender, PXRowInsertedEventArgs e)
  {
    if (e.Row == null)
      return;
    if (PXSelectorAttribute.Select<PX.Objects.AP.Vendor.acctCD>(sender, e.Row) is PX.Objects.AP.Vendor copy)
    {
      copy.Included = new bool?(true);
      PXCache<PX.Objects.AP.Vendor>.RestoreCopy((PX.Objects.AP.Vendor) e.Row, copy);
      sender.SetStatus(e.Row, PXEntryStatus.Updated);
    }
    else
      sender.Delete(e.Row);
  }

  protected virtual void Vendor_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    if (!(e.Row is PX.Objects.AP.Vendor row) || sender.GetStatus((object) row) != PXEntryStatus.Notchanged)
      return;
    if (row.GroupMask != null)
    {
      foreach (byte num in row.GroupMask)
      {
        if (num != (byte) 0)
        {
          row.Included = new bool?(true);
          break;
        }
      }
    }
    else
      row.Included = new bool?(true);
  }

  protected virtual void Vendor_RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    PX.Objects.AP.Vendor row = e.Row as PX.Objects.AP.Vendor;
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
        row.GroupMask[index] = !included.GetValueOrDefault() ? (byte) ((uint) row.GroupMask[index] & (uint) ~current.GroupMask[index]) : (byte) ((uint) row.GroupMask[index] | (uint) current.GroupMask[index]);
      }
    }
  }

  public override void Persist()
  {
    this.populateNeighbours<PX.SM.Users>((PXSelectBase<PX.SM.Users>) this.Users);
    this.populateNeighbours<PX.Objects.AP.Vendor>((PXSelectBase<PX.Objects.AP.Vendor>) this.Vendor);
    this.populateNeighbours<PX.SM.Users>((PXSelectBase<PX.SM.Users>) this.Users);
    base.Persist();
    PXSelectorAttribute.ClearGlobalCache<PX.SM.Users>();
    PXSelectorAttribute.ClearGlobalCache<PX.Objects.AP.Vendor>();
  }

  public static IEnumerable GroupDelegate(PXGraph graph, bool inclInserted)
  {
    PXResultset<Neighbour> set = PXSelectBase<Neighbour, PXSelectGroupBy<Neighbour, Where<Neighbour.leftEntityType, Equal<vendorType>>, Aggregate<GroupBy<Neighbour.coverageMask, GroupBy<Neighbour.inverseMask, GroupBy<Neighbour.winCoverageMask, GroupBy<Neighbour.winInverseMask>>>>>>.Config>.Select(graph);
    foreach (PXResult<RelationGroup> pxResult in PXSelectBase<RelationGroup, PXSelect<RelationGroup>.Config>.Select(graph))
    {
      RelationGroup group = (RelationGroup) pxResult;
      if (!string.IsNullOrEmpty(group.GroupName) | inclInserted && (group.SpecificModule == null || group.SpecificModule == typeof (PX.Objects.AP.Vendor).Namespace) || UserAccess.InNeighbours(set, group))
        yield return (object) group;
    }
  }

  protected virtual IEnumerable group() => APAccess.GroupDelegate((PXGraph) this, true);

  public class APRelationGroupVendorSelectorAttribute(System.Type type) : PXCustomSelectorAttribute(type)
  {
    public virtual IEnumerable GetRecords() => APAccess.GroupDelegate(this._Graph, false);
  }
}
