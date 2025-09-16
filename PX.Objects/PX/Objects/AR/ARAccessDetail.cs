// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.ARAccessDetail
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.SM;
using System;
using System.Collections;

#nullable disable
namespace PX.Objects.AR;

public class ARAccessDetail : UserAccess
{
  public PXSelect<PX.Objects.AR.Customer> Customer;
  public PXSave<PX.Objects.AR.Customer> Save;
  public PXCancel<PX.Objects.AR.Customer> Cancel;
  public PXFirst<PX.Objects.AR.Customer> First;
  public PXPrevious<PX.Objects.AR.Customer> Prev;
  public PXNext<PX.Objects.AR.Customer> Next;
  public PXLast<PX.Objects.AR.Customer> Last;
  public PXSetup<PX.Objects.AR.ARSetup> ARSetup;

  [PXDBString(30, IsUnicode = true, IsKey = true, InputMask = "")]
  [PXUIField]
  [PXDimensionSelector("BIZACCT", typeof (Search2<PX.Objects.AR.Customer.acctCD, LeftJoin<PX.Objects.CR.Contact, On<PX.Objects.CR.Contact.bAccountID, Equal<PX.Objects.AR.Customer.bAccountID>, And<PX.Objects.CR.Contact.contactID, Equal<PX.Objects.AR.Customer.defContactID>>>, LeftJoin<PX.Objects.CR.Address, On<PX.Objects.CR.Address.bAccountID, Equal<PX.Objects.AR.Customer.bAccountID>, And<PX.Objects.CR.Address.addressID, Equal<PX.Objects.AR.Customer.defAddressID>>>>>>), typeof (PX.Objects.AR.Customer.acctCD), new System.Type[] {typeof (PX.Objects.AR.Customer.acctCD), typeof (PX.Objects.AR.Customer.acctName), typeof (PX.Objects.AR.Customer.customerClassID), typeof (PX.Objects.AR.Customer.status), typeof (PX.Objects.CR.Contact.phone1), typeof (PX.Objects.CR.Address.city), typeof (PX.Objects.CR.Address.countryID)})]
  protected virtual void Customer_AcctCD_CacheAttached(PXCache sender)
  {
  }

  public ARAccessDetail()
  {
    PX.Objects.AR.ARSetup current = ((PXSelectBase<PX.Objects.AR.ARSetup>) this.ARSetup).Current;
    ((PXSelectBase) this.Customer).Cache.AllowDelete = false;
    ((PXSelectBase) this.Customer).Cache.AllowInsert = false;
    PXUIFieldAttribute.SetEnabled(((PXSelectBase) this.Customer).Cache, (string) null, false);
    PXUIFieldAttribute.SetEnabled<PX.Objects.AR.Customer.acctCD>(((PXSelectBase) this.Customer).Cache, (object) null);
    ((PXGraph) this).Views.Caches.Remove(((PXSelectBase<RelationGroup>) this.Groups).GetItemType());
    ((PXGraph) this).Views.Caches.Add(((PXSelectBase<RelationGroup>) this.Groups).GetItemType());
  }

  protected virtual IEnumerable groups()
  {
    ARAccessDetail arAccessDetail = this;
    foreach (PXResult<RelationGroup> pxResult in PXSelectBase<RelationGroup, PXSelect<RelationGroup>.Config>.Select((PXGraph) arAccessDetail, Array.Empty<object>()))
    {
      RelationGroup relationGroup = PXResult<RelationGroup>.op_Implicit(pxResult);
      if (relationGroup.SpecificModule == null || relationGroup.SpecificModule == typeof (PX.Objects.AR.Customer).Namespace || UserAccess.IsIncluded(((UserAccess) arAccessDetail).getMask(), relationGroup))
      {
        ((PXSelectBase<RelationGroup>) arAccessDetail.Groups).Current = relationGroup;
        yield return (object) relationGroup;
      }
    }
  }

  protected virtual byte[] getMask()
  {
    byte[] mask = (byte[]) null;
    if (((PXSelectBase<Users>) this.User).Current != null)
      mask = ((PXSelectBase<Users>) this.User).Current.GroupMask;
    else if (((PXSelectBase<PX.Objects.AR.Customer>) this.Customer).Current != null)
      mask = ((PXSelectBase<PX.Objects.AR.Customer>) this.Customer).Current.GroupMask;
    return mask;
  }

  public virtual void Persist()
  {
    if (((PXSelectBase<Users>) this.User).Current != null)
    {
      UserAccess.PopulateNeighbours<Users>((PXSelectBase<Users>) this.User, (PXSelectBase<RelationGroup>) this.Groups, Array.Empty<System.Type>());
      PXSelectorAttribute.ClearGlobalCache<Users>();
    }
    else
    {
      if (((PXSelectBase<PX.Objects.AR.Customer>) this.Customer).Current == null)
        return;
      UserAccess.PopulateNeighbours<PX.Objects.AR.Customer>((PXSelectBase<PX.Objects.AR.Customer>) this.Customer, (PXSelectBase<RelationGroup>) this.Groups, Array.Empty<System.Type>());
      PXSelectorAttribute.ClearGlobalCache<PX.Objects.AR.Customer>();
    }
    base.Persist();
  }
}
