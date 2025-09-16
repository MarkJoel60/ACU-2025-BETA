// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.APAccessDetail
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.SM;
using System.Collections;

#nullable disable
namespace PX.Objects.AP;

public class APAccessDetail : UserAccess
{
  public PXSelect<PX.Objects.AP.Vendor> Vendor;
  public PXSave<PX.Objects.AP.Vendor> Save;
  public PXCancel<PX.Objects.AP.Vendor> Cancel;
  public PXFirst<PX.Objects.AP.Vendor> First;
  public PXPrevious<PX.Objects.AP.Vendor> Prev;
  public PXNext<PX.Objects.AP.Vendor> Next;
  public PXLast<PX.Objects.AP.Vendor> Last;
  public PXSetup<PX.Objects.AP.APSetup> APSetup;

  [PXDBString(30, IsUnicode = true, IsKey = true, InputMask = "")]
  [PXUIField(DisplayName = "Vendor ID", Visibility = PXUIVisibility.SelectorVisible)]
  [PXDimensionSelector("VENDOR", typeof (Search2<PX.Objects.AP.Vendor.acctCD, LeftJoin<PX.Objects.CR.Contact, On<PX.Objects.CR.Contact.bAccountID, Equal<PX.Objects.AP.Vendor.bAccountID>, And<PX.Objects.CR.Contact.contactID, Equal<PX.Objects.AP.Vendor.defContactID>>>, LeftJoin<PX.Objects.CR.Address, On<PX.Objects.CR.Address.bAccountID, Equal<PX.Objects.AP.Vendor.bAccountID>, And<PX.Objects.CR.Address.addressID, Equal<PX.Objects.AP.Vendor.defAddressID>>>>>>), typeof (PX.Objects.AP.Vendor.acctCD), new System.Type[] {typeof (PX.Objects.AP.Vendor.acctCD), typeof (PX.Objects.AP.Vendor.acctName), typeof (PX.Objects.AP.Vendor.vendorClassID), typeof (PX.Objects.AP.Vendor.vStatus), typeof (PX.Objects.CR.Contact.phone1), typeof (PX.Objects.CR.Address.city), typeof (PX.Objects.CR.Address.countryID)})]
  protected virtual void Vendor_AcctCD_CacheAttached(PXCache sender)
  {
  }

  public APAccessDetail()
  {
    PX.Objects.AP.APSetup current = this.APSetup.Current;
    this.Vendor.Cache.AllowDelete = false;
    this.Vendor.Cache.AllowInsert = false;
    PXUIFieldAttribute.SetEnabled(this.Vendor.Cache, (string) null, false);
    PXUIFieldAttribute.SetEnabled<PX.Objects.AP.Vendor.acctCD>(this.Vendor.Cache, (object) null);
    this.Views.Caches.Remove(this.Groups.GetItemType());
    this.Views.Caches.Add(this.Groups.GetItemType());
  }

  protected override IEnumerable groups()
  {
    APAccessDetail graph = this;
    foreach (PXResult<RelationGroup> pxResult in PXSelectBase<RelationGroup, PXSelect<RelationGroup>.Config>.Select((PXGraph) graph))
    {
      RelationGroup group = (RelationGroup) pxResult;
      if (group.SpecificModule == null || group.SpecificModule == typeof (PX.Objects.AP.Vendor).Namespace || UserAccess.IsIncluded(graph.getMask(), group))
      {
        graph.Groups.Current = group;
        yield return (object) group;
      }
    }
  }

  protected override byte[] getMask()
  {
    byte[] mask = (byte[]) null;
    if (this.User.Current != null)
      mask = this.User.Current.GroupMask;
    else if (this.Vendor.Current != null)
      mask = this.Vendor.Current.GroupMask;
    return mask;
  }

  public override void Persist()
  {
    if (this.User.Current != null)
    {
      UserAccess.PopulateNeighbours<Users>((PXSelectBase<Users>) this.User, (PXSelectBase<RelationGroup>) this.Groups);
      PXSelectorAttribute.ClearGlobalCache<Users>();
    }
    else
    {
      if (this.Vendor.Current == null)
        return;
      UserAccess.PopulateNeighbours<PX.Objects.AP.Vendor>((PXSelectBase<PX.Objects.AP.Vendor>) this.Vendor, (PXSelectBase<RelationGroup>) this.Groups);
      PXSelectorAttribute.ClearGlobalCache<PX.Objects.AP.Vendor>();
    }
    base.Persist();
  }
}
