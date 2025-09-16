// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.APDiscountMaint
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.AR;
using PX.Objects.Common.Discount;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.AP;

public class APDiscountMaint : PXGraph<APDiscountMaint>
{
  public PXSave<Vendor> Save;
  public PXCancel<Vendor> Cancel;
  public PXFirst<Vendor> First;
  public PXPrevious<Vendor> Previous;
  public PXNext<Vendor> Next;
  public PXLast<Vendor> Last;
  public PXSelect<Vendor> Filter;
  public PXSelect<Vendor, Where<Vendor.bAccountID, Equal<Current<Vendor.bAccountID>>>> CurrentVendor;
  public PXSelect<APDiscount, Where<APDiscount.bAccountID, Equal<Current<Vendor.bAccountID>>>> CurrentDiscounts;
  public PXSelect<VendorDiscountSequence> DiscountSequences;
  public PXAction<Vendor> viewAPDiscountSequence;

  public APDiscountMaint()
  {
    this.Filter.Cache.AllowInsert = false;
    this.Filter.Cache.AllowDelete = false;
    foreach (System.Type discountChildType in this.DiscountChildTypes())
      this.Views.Caches.Add(discountChildType);
    PXParentAttribute.SetLeaveChildren<DiscountSequenceDetail.discountSequenceID>((PXCache) this.Caches<DiscountSequenceDetail>(), (object) null, false);
  }

  protected virtual IEnumerable<System.Type> DiscountChildTypes()
  {
    yield return typeof (DiscountSequence);
    yield return typeof (APDiscountVendor);
    yield return typeof (APDiscountLocation);
    yield return typeof (DiscountInventoryPriceClass);
    yield return typeof (DiscountItem);
    yield return typeof (DiscountSequenceDetail);
  }

  [PXUIField(DisplayName = "View Discount Sequence", MapEnableRights = PXCacheRights.Select, MapViewRights = PXCacheRights.Select, Visible = false)]
  [PXLookupButton]
  public virtual IEnumerable ViewAPDiscountSequence(PXAdapter adapter)
  {
    if (this.Filter.Current != null && this.CurrentDiscounts.Current != null && this.Filter.Current.BAccountID.HasValue && this.CurrentDiscounts.Current.DiscountID != null)
    {
      APDiscountSequenceMaint instance = PXGraph.CreateInstance<APDiscountSequenceMaint>();
      instance.Sequence.Current = new VendorDiscountSequence();
      instance.Sequence.Current.VendorID = this.Filter.Current.BAccountID;
      instance.Sequence.Current.DiscountID = this.CurrentDiscounts.Current.DiscountID;
      throw new PXRedirectRequiredException((PXGraph) instance, "View Discount Sequence");
    }
    return adapter.Get();
  }

  [PXRemoveBaseAttribute(typeof (PXStringListAttribute))]
  protected virtual void _(PX.Data.Events.CacheAttached<APDiscount.applicableTo> e)
  {
  }

  protected virtual void Vendor_RowSelecting(PXCache sender, PXRowSelectingEventArgs e)
  {
    if (e.Row == null)
      return;
    if (((Vendor) e.Row).LineDiscountTarget == null)
      this.Filter.Cache.SetDefaultExt<Vendor.lineDiscountTarget>(e.Row);
    if (((Vendor) e.Row).IgnoreConfiguredDiscounts.HasValue)
      return;
    this.Filter.Cache.SetDefaultExt<Vendor.ignoreConfiguredDiscounts>(e.Row);
  }

  protected virtual void APDiscount_ApplicableTo_FieldSelecting(
    PXCache sender,
    PXFieldSelectingEventArgs e)
  {
    if (e.Row is APDiscount row && row.Type == "D")
    {
      string[] allowedValues = new string[1]{ "VE" };
      string[] array = ((IEnumerable<string>) new string[1]
      {
        "Unconditional"
      }).Select<string, string>(new Func<string, string>(PXMessages.LocalizeNoPrefix)).ToArray<string>();
      e.ReturnState = (object) PXStringState.CreateInstance(e.ReturnValue, new int?(1), new bool?(false), "ApplicableTo", new bool?(false), new int?(1), (string) null, allowedValues, array, new bool?(true), "UN");
    }
    else if (!PXAccess.FeatureInstalled<PX.Objects.CS.FeaturesSet.accountLocations>())
    {
      string[] allowedValues = new string[3]
      {
        "VI",
        "VP",
        "VE"
      };
      string[] array = ((IEnumerable<string>) new string[3]
      {
        "Item",
        "Item Price Class",
        "Unconditional"
      }).Select<string, string>(new Func<string, string>(PXMessages.LocalizeNoPrefix)).ToArray<string>();
      e.ReturnState = (object) PXStringState.CreateInstance(e.ReturnValue, new int?(1), new bool?(false), "ApplicableTo", new bool?(false), new int?(1), (string) null, allowedValues, array, new bool?(true), "IN");
    }
    else
    {
      string[] allowedValues = new string[5]
      {
        "VI",
        "VP",
        "VL",
        "LI",
        "VE"
      };
      string[] array = ((IEnumerable<string>) new string[5]
      {
        "Item",
        "Item Price Class",
        "Location",
        "Item and Location",
        "Unconditional"
      }).Select<string, string>(new Func<string, string>(PXMessages.LocalizeNoPrefix)).ToArray<string>();
      e.ReturnState = (object) PXStringState.CreateInstance(e.ReturnValue, new int?(1), new bool?(false), "ApplicableTo", new bool?(false), new int?(1), (string) null, allowedValues, array, new bool?(true), "IN");
    }
  }

  protected virtual void APDiscount_RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    if (!(e.Row is APDiscount row))
      return;
    if (e.Operation == PXDBOperation.Insert || e.Operation == PXDBOperation.Update)
    {
      if (PXSelectBase<ARDiscount, PXSelectReadonly<ARDiscount, Where<ARDiscount.discountID, Equal<Required<ARDiscount.discountID>>>>.Config>.Select((PXGraph) this, (object) row.DiscountID).Count != 0)
        sender.RaiseExceptionHandling<APDiscount.discountID>((object) row, (object) row.DiscountID, (Exception) new PXSetPropertyException("The discount code already exists in AR. Specify another discount code.", PXErrorLevel.Error));
      bool? nullable;
      if (row.Type == "L" || row.Type == "D")
      {
        nullable = row.SkipDocumentDiscounts;
        if (nullable.GetValueOrDefault())
          row.SkipDocumentDiscounts = new bool?(false);
      }
      if (row.Type == "G" || row.Type == "D")
      {
        nullable = row.ExcludeFromDiscountableAmt;
        if (nullable.GetValueOrDefault())
          row.ExcludeFromDiscountableAmt = new bool?(false);
      }
    }
    if (e.Operation != PXDBOperation.Insert)
      return;
    if (PXSelectBase<APDiscount, PXSelectReadonly<APDiscount, Where<APDiscount.discountID, Equal<Required<APDiscount.discountID>>>>.Config>.Select((PXGraph) this, (object) row.DiscountID).Count == 0)
      return;
    sender.RaiseExceptionHandling<APDiscount.discountID>((object) row, (object) row.DiscountID, (Exception) new PXSetPropertyException("Discount Code already exists.", PXErrorLevel.Error));
  }

  protected virtual void APDiscount_RowPersisted(PXCache sender, PXRowPersistedEventArgs e)
  {
    DiscountEngine.GetDiscountTypes(true);
  }

  protected virtual void APDiscount_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    if (!(e.Row is APDiscount row))
      return;
    PXUIFieldAttribute.SetEnabled<APDiscount.excludeFromDiscountableAmt>(sender, (object) row, row.Type == "L");
    PXUIFieldAttribute.SetEnabled<APDiscount.skipDocumentDiscounts>(sender, (object) row, row.Type == "G");
    PXStringListAttribute.SetList<APDiscount.type>(sender, (object) null, new Tuple<string, string>("L", "Line"), new Tuple<string, string>("G", "Group"), new Tuple<string, string>("D", "Document"));
  }

  protected virtual void APDiscount_Type_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    if (!(e.Row is APDiscount row))
      return;
    if ((VendorDiscountSequence) PXSelectBase<VendorDiscountSequence, PXSelect<VendorDiscountSequence, Where<VendorDiscountSequence.discountID, Equal<Required<VendorDiscountSequence.discountID>>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, (object) row.DiscountID) != null)
    {
      row.Type = (string) e.OldValue;
      sender.RaiseExceptionHandling<APDiscount.type>((object) row, e.OldValue, (Exception) new PXSetPropertyException("Discount Type can not be changed if Discount Code has Discount Sequence"));
    }
    else
    {
      if (!(row.Type == "D") || !(row.ApplicableTo != "VE"))
        return;
      sender.SetValueExt<APDiscount.applicableTo>(e.Row, (object) "VE");
    }
  }
}
