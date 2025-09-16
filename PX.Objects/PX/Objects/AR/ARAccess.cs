// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.ARAccess
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.SM;
using System;
using System.Collections;
using System.ComponentModel;

#nullable disable
namespace PX.Objects.AR;

public class ARAccess : BaseAccess
{
  public PXSelect<PX.Objects.AR.Customer> Customer;
  public PXSetup<PX.Objects.AR.ARSetup> ARSetup;

  [PXDBString(30, IsUnicode = true, IsKey = true, InputMask = "")]
  [PXUIField]
  [PXDimensionSelector("BIZACCT", typeof (Search2<PX.Objects.AR.Customer.acctCD, LeftJoin<PX.Objects.CR.Contact, On<PX.Objects.CR.Contact.bAccountID, Equal<PX.Objects.AR.Customer.bAccountID>, And<PX.Objects.CR.Contact.contactID, Equal<PX.Objects.AR.Customer.defContactID>>>, LeftJoin<PX.Objects.CR.Address, On<PX.Objects.CR.Address.bAccountID, Equal<PX.Objects.AR.Customer.bAccountID>, And<PX.Objects.CR.Address.addressID, Equal<PX.Objects.AR.Customer.defAddressID>>>>>>), typeof (PX.Objects.AR.Customer.acctCD), new System.Type[] {typeof (PX.Objects.AR.Customer.acctCD), typeof (PX.Objects.AR.Customer.acctName), typeof (PX.Objects.AR.Customer.customerClassID), typeof (PX.Objects.AR.Customer.status), typeof (PX.Objects.CR.Contact.phone1), typeof (PX.Objects.CR.Address.city), typeof (PX.Objects.CR.Address.countryID)})]
  protected virtual void Customer_AcctCD_CacheAttached(PXCache sender)
  {
  }

  [PXDBString(128 /*0x80*/, IsKey = true, InputMask = "")]
  [PXDefault]
  [PXUIField]
  [ARAccess.ARRelationGroupCustomerSelector(typeof (RelationGroup.groupName), Filterable = true)]
  protected virtual void RelationGroup_GroupName_CacheAttached(PXCache sender)
  {
  }

  public ARAccess()
  {
    PX.Objects.AR.ARSetup current = ((PXSelectBase<PX.Objects.AR.ARSetup>) this.ARSetup).Current;
    ((PXSelectBase) this.Customer).Cache.AllowDelete = false;
    PXUIFieldAttribute.SetEnabled(((PXSelectBase) this.Customer).Cache, (string) null, false);
    PXUIFieldAttribute.SetEnabled<PX.Objects.AR.Customer.included>(((PXSelectBase) this.Customer).Cache, (object) null);
    PXUIFieldAttribute.SetEnabled<PX.Objects.AR.Customer.acctCD>(((PXSelectBase) this.Customer).Cache, (object) null);
  }

  public virtual bool CanClipboardCopyPaste() => false;

  protected virtual IEnumerable customer()
  {
    ARAccess arAccess1 = this;
    if (((PXSelectBase<RelationGroup>) arAccess1.Group).Current != null && !string.IsNullOrEmpty(((PXSelectBase<RelationGroup>) arAccess1.Group).Current.GroupName))
    {
      bool inserted = ((PXSelectBase) arAccess1.Group).Cache.GetStatus((object) ((PXSelectBase<RelationGroup>) arAccess1.Group).Current) == 2;
      ARAccess arAccess2 = arAccess1;
      object[] objArray = new object[1]
      {
        (object) new byte[0]
      };
      foreach (PXResult<PX.Objects.AR.Customer> pxResult in PXSelectBase<PX.Objects.AR.Customer, PXSelect<PX.Objects.AR.Customer, Where2<Match<Current<RelationGroup.groupName>>, Or<Match<Required<PX.Objects.AR.Customer.groupMask>>>>>.Config>.Select((PXGraph) arAccess2, objArray))
      {
        PX.Objects.AR.Customer item = PXResult<PX.Objects.AR.Customer>.op_Implicit(pxResult);
        if (!inserted)
        {
          ((PXSelectBase<PX.Objects.AR.Customer>) arAccess1.Customer).Current = item;
          yield return (object) item;
        }
        else if (item.GroupMask != null)
        {
          RelationGroup group = ((PXSelectBase<RelationGroup>) arAccess1.Group).Current;
          bool anyGroup = false;
          for (int i = 0; i < item.GroupMask.Length && i < group.GroupMask.Length; ++i)
          {
            if (group.GroupMask[i] != (byte) 0 && ((int) item.GroupMask[i] & (int) group.GroupMask[i]) == (int) group.GroupMask[i])
            {
              ((PXSelectBase<PX.Objects.AR.Customer>) arAccess1.Customer).Current = item;
              yield return (object) item;
            }
            anyGroup |= item.GroupMask[i] > (byte) 0;
          }
          if (!anyGroup)
          {
            ((PXSelectBase<PX.Objects.AR.Customer>) arAccess1.Customer).Current = item;
            yield return (object) item;
          }
          group = (RelationGroup) null;
        }
        item = (PX.Objects.AR.Customer) null;
      }
    }
  }

  protected virtual void RelationGroup_RowInserted(PXCache cache, PXRowInsertedEventArgs e)
  {
    base.RelationGroup_RowInserted(cache, e);
    ((RelationGroup) e.Row).SpecificModule = typeof (accountsReceivableModule).Namespace;
  }

  protected virtual void RelationGroup_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    if (!(e.Row is RelationGroup row))
      return;
    if (string.IsNullOrEmpty(row.GroupName))
    {
      ((PXAction) this.Save).SetEnabled(false);
      ((PXSelectBase) this.Customer).Cache.AllowInsert = false;
    }
    else
    {
      ((PXAction) this.Save).SetEnabled(true);
      ((PXSelectBase) this.Customer).Cache.AllowInsert = true;
    }
  }

  protected virtual void Customer_AcctCD_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void Customer_AcctCD_FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
  {
    if (PXSelectorAttribute.Select<PX.Objects.AR.Customer.acctCD>(sender, e.Row, e.NewValue) == null)
      throw new PXSetPropertyException("'{0}' cannot be found in the system.", new object[1]
      {
        (object) "CustomerID"
      });
  }

  protected virtual void Customer_RowInserted(PXCache sender, PXRowInsertedEventArgs e)
  {
    if (e.Row == null)
      return;
    if (PXSelectorAttribute.Select<PX.Objects.AR.Customer.acctCD>(sender, e.Row) is PX.Objects.AR.Customer customer)
    {
      customer.Included = new bool?(true);
      PXCache<PX.Objects.AR.Customer>.RestoreCopy((PX.Objects.AR.Customer) e.Row, customer);
      sender.SetStatus(e.Row, (PXEntryStatus) 1);
    }
    else
      sender.Delete(e.Row);
  }

  protected virtual void Customer_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    if (!(e.Row is PX.Objects.AR.Customer row) || sender.GetStatus((object) row) != null)
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

  protected virtual void Customer_RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    PX.Objects.AR.Customer row = e.Row as PX.Objects.AR.Customer;
    RelationGroup current = ((PXSelectBase<RelationGroup>) this.Group).Current;
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

  public virtual void Persist()
  {
    this.populateNeighbours<Users>((PXSelectBase<Users>) this.Users);
    this.populateNeighbours<PX.Objects.AR.Customer>((PXSelectBase<PX.Objects.AR.Customer>) this.Customer);
    this.populateNeighbours<Users>((PXSelectBase<Users>) this.Users);
    base.Persist();
    PXSelectorAttribute.ClearGlobalCache<Users>();
    PXSelectorAttribute.ClearGlobalCache<PX.Objects.AR.Customer>();
  }

  public static IEnumerable GroupDelegate(PXGraph graph, bool inclInserted)
  {
    PXResultset<Neighbour> set = PXSelectBase<Neighbour, PXSelectGroupBy<Neighbour, Where<Neighbour.leftEntityType, Equal<customerType>>, Aggregate<GroupBy<Neighbour.coverageMask, GroupBy<Neighbour.inverseMask, GroupBy<Neighbour.winCoverageMask, GroupBy<Neighbour.winInverseMask>>>>>>.Config>.Select(graph, Array.Empty<object>());
    foreach (PXResult<RelationGroup> pxResult in PXSelectBase<RelationGroup, PXSelect<RelationGroup>.Config>.Select(graph, Array.Empty<object>()))
    {
      RelationGroup relationGroup = PXResult<RelationGroup>.op_Implicit(pxResult);
      if (!string.IsNullOrEmpty(relationGroup.GroupName) | inclInserted && (relationGroup.SpecificModule == null || relationGroup.SpecificModule == typeof (PX.Objects.AR.Customer).Namespace) || UserAccess.InNeighbours(set, relationGroup))
        yield return (object) relationGroup;
    }
  }

  protected virtual IEnumerable group() => ARAccess.GroupDelegate((PXGraph) this, true);

  public class ARRelationGroupCustomerSelectorAttribute(System.Type type) : PXCustomSelectorAttribute(type)
  {
    public virtual IEnumerable GetRecords() => ARAccess.GroupDelegate(this._Graph, false);
  }
}
