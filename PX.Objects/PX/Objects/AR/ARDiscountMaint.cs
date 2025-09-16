// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.ARDiscountMaint
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.AP;
using PX.Objects.Common.Discount;
using PX.Objects.CS;
using PX.Objects.SO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

#nullable disable
namespace PX.Objects.AR;

public class ARDiscountMaint : PXGraph<SOSetupMaint>
{
  public PXSelect<ARDiscount> Document;
  public PXSavePerRow<ARDiscount> Save;
  public PXCancel<ARDiscount> Cancel;
  public PXAction<ARDiscount> viewARDiscountSequence;

  public ARDiscountMaint()
  {
    foreach (Type discountChildType in this.DiscountChildTypes())
    {
      PXCache cach = ((PXGraph) this).Caches[discountChildType];
    }
    PXParentAttribute.SetLeaveChildren<DiscountSequenceDetail.discountSequenceID>((PXCache) GraphHelper.Caches<DiscountSequenceDetail>((PXGraph) this), (object) null, false);
    PXParentAttribute.SetLeaveChildren<DiscountSequence.discountID>((PXCache) GraphHelper.Caches<DiscountSequence>((PXGraph) this), (object) null, true);
  }

  protected virtual IEnumerable<Type> DiscountChildTypes()
  {
    yield return typeof (DiscountSequence);
    yield return typeof (DiscountSite);
    yield return typeof (DiscountItem);
    yield return typeof (DiscountBranch);
    yield return typeof (DiscountCustomer);
    yield return typeof (DiscountCustomerPriceClass);
    yield return typeof (DiscountInventoryPriceClass);
    yield return typeof (DiscountSequenceDetail);
  }

  [PXUIField]
  [PXLookupButton]
  public virtual IEnumerable ViewARDiscountSequence(PXAdapter adapter)
  {
    if (((PXSelectBase<ARDiscount>) this.Document).Current != null && ((PXSelectBase<ARDiscount>) this.Document).Current.DiscountID != null)
    {
      ARDiscountSequenceMaint instance = PXGraph.CreateInstance<ARDiscountSequenceMaint>();
      ((PXSelectBase<DiscountSequence>) instance.Sequence).Current = new DiscountSequence();
      ((PXSelectBase<DiscountSequence>) instance.Sequence).Current.DiscountID = ((PXSelectBase<ARDiscount>) this.Document).Current.DiscountID;
      throw new PXRedirectRequiredException((PXGraph) instance, "View Discount Sequence");
    }
    return adapter.Get();
  }

  [PXRemoveBaseAttribute(typeof (PXStringListAttribute))]
  protected virtual void _(PX.Data.Events.CacheAttached<ARDiscount.applicableTo> e)
  {
  }

  protected virtual void ARDiscount_ApplicableTo_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    if (!(e.Row is ARDiscount row))
      return;
    if (PXResultset<DiscountSequence>.op_Implicit(PXSelectBase<DiscountSequence, PXSelect<DiscountSequence, Where<DiscountSequence.discountID, Equal<Required<DiscountSequence.discountID>>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, new object[1]
    {
      (object) row.DiscountID
    })) != null)
    {
      ((CancelEventArgs) e).Cancel = true;
      throw new PXSetPropertyException("Cannot change Applicable To for the Discount if there already exist one or more Discount Sequences associated with the given Discount");
    }
  }

  protected virtual void ARDiscount_RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    if (!(e.Row is ARDiscount row) || e.Operation != 2 && e.Operation != 1)
      return;
    if (PXSelectBase<APDiscount, PXSelectReadonly<APDiscount, Where<APDiscount.discountID, Equal<Required<APDiscount.discountID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) row.DiscountID
    }).Count != 0)
      sender.RaiseExceptionHandling<ARDiscount.discountID>((object) row, (object) row.DiscountID, (Exception) new PXSetPropertyException("The discount code already exists in AP. Specify another discount code.", (PXErrorLevel) 4));
    bool? nullable;
    if (row.Type == "L" || row.Type == "D")
    {
      nullable = row.SkipDocumentDiscounts;
      if (nullable.GetValueOrDefault())
        row.SkipDocumentDiscounts = new bool?(false);
    }
    if (!(row.Type == "G") && !(row.Type == "D"))
      return;
    nullable = row.ExcludeFromDiscountableAmt;
    if (!nullable.GetValueOrDefault())
      return;
    row.ExcludeFromDiscountableAmt = new bool?(false);
  }

  protected virtual void ARDiscount_RowPersisted(PXCache sender, PXRowPersistedEventArgs e)
  {
    if ((e.Operation & 3) == 3 && e.TranStatus == null)
      this.RemoveChildReferences((ARDiscount) e.Row);
    DiscountEngine.GetDiscountTypes(true);
  }

  protected virtual void RemoveChildReferences(ARDiscount discount)
  {
    clearChildCaches();
    try
    {
      PXParentAttribute.SetLeaveChildren<DiscountSequence.discountID>((PXCache) GraphHelper.Caches<DiscountSequence>((PXGraph) this), (object) null, false);
      ((PXSelectBase) this.Document).Cache.RaiseRowDeleted((object) discount);
      foreach (Type discountChildType in this.DiscountChildTypes())
        ((PXGraph) this).Persist(discountChildType, (PXDBOperation) 3);
      clearChildCaches();
    }
    finally
    {
      PXParentAttribute.SetLeaveChildren<DiscountSequence.discountID>((PXCache) GraphHelper.Caches<DiscountSequence>((PXGraph) this), (object) null, true);
    }

    void clearChildCaches()
    {
      foreach (Type discountChildType in this.DiscountChildTypes())
        ((PXGraph) this).Caches[discountChildType].Clear();
    }
  }

  protected virtual void ARDiscount_Type_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    if (!(e.Row is ARDiscount row))
      return;
    if (PXResultset<DiscountSequence>.op_Implicit(PXSelectBase<DiscountSequence, PXSelect<DiscountSequence, Where<DiscountSequence.discountID, Equal<Required<DiscountSequence.discountID>>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, new object[1]
    {
      (object) row.DiscountID
    })) != null)
    {
      row.Type = (string) e.OldValue;
      sender.RaiseExceptionHandling<ARDiscount.type>((object) row, e.OldValue, (Exception) new PXSetPropertyException("Discount Type can not be changed if Discount Code has Discount Sequence"));
    }
    else
    {
      if (!(row.Type == "D") || ARDiscountMaint.GetAllowedDiscountTargetsForDocumentDiscountType().Item1.Contains(row.ApplicableTo))
        return;
      sender.SetValueExt<ARDiscount.applicableTo>((object) row, (object) "CU");
    }
  }

  protected virtual void ARDiscount_RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    if (!(e.Row is ARDiscount row) || !(row.Type != "L"))
      return;
    sender.SetValueExt<ARDiscount.isAppliedToDR>((object) row, (object) false);
  }

  protected virtual void ARDiscount_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    if (!(e.Row is ARDiscount row))
      return;
    PXUIFieldAttribute.SetEnabled<ARDiscount.excludeFromDiscountableAmt>(sender, (object) row, row.Type == "L");
    PXUIFieldAttribute.SetEnabled<ARDiscount.skipDocumentDiscounts>(sender, (object) row, row.Type == "G");
    PXUIFieldAttribute.SetEnabled<ARDiscount.isAppliedToDR>(sender, (object) row, row.Type == "L");
    PXStringListAttribute.SetList<ARDiscount.type>(sender, (object) null, new Tuple<string, string>[3]
    {
      new Tuple<string, string>("L", "Line"),
      new Tuple<string, string>("G", "Group"),
      new Tuple<string, string>("D", "Document")
    });
  }

  private static Tuple<List<string>, List<string>> GetAllowedDiscountTargetsForDocumentDiscountType()
  {
    List<string> stringList1 = new List<string>();
    List<string> stringList2 = new List<string>();
    stringList1.Add("CU");
    stringList2.Add("Customer");
    if (PXAccess.FeatureInstalled<FeaturesSet.branch>() || PXAccess.FeatureInstalled<FeaturesSet.multiCompany>())
    {
      stringList1.Add("CB");
      stringList2.Add("Customer and Branch");
    }
    stringList1.Add("CE");
    stringList2.Add("Customer Price Class");
    if (PXAccess.FeatureInstalled<FeaturesSet.branch>() || PXAccess.FeatureInstalled<FeaturesSet.multiCompany>())
    {
      stringList1.Add("PB");
      stringList2.Add("Customer Price Class and Branch");
    }
    stringList1.Add("UN");
    stringList2.Add("Unconditional");
    return Tuple.Create<List<string>, List<string>>(stringList1, stringList2);
  }

  protected virtual void ARDiscount_ApplicableTo_FieldSelecting(
    PXCache sender,
    PXFieldSelectingEventArgs e)
  {
    ARDiscount row = e.Row as ARDiscount;
    List<string> stringList1 = new List<string>();
    List<string> stringList2 = new List<string>();
    if (row != null && row.Type == "D")
    {
      Tuple<List<string>, List<string>> documentDiscountType = ARDiscountMaint.GetAllowedDiscountTargetsForDocumentDiscountType();
      List<string> stringList3 = documentDiscountType.Item1;
      List<string> stringList4 = documentDiscountType.Item2;
      e.ReturnState = (object) PXStringState.CreateInstance(e.ReturnValue, new int?(1), new bool?(false), "ApplicableTo", new bool?(false), new int?(1), (string) null, stringList3.ToArray(), ((IEnumerable<string>) stringList4.ToArray()).Select<string, string>(new Func<string, string>(PXMessages.LocalizeNoPrefix)).ToArray<string>(), new bool?(true), "CU", (string[]) null);
    }
    else
    {
      stringList1.AddRange((IEnumerable<string>) new string[8]
      {
        "CU",
        "IN",
        "IE",
        "CI",
        "CP",
        "CE",
        "PI",
        "PP"
      });
      stringList2.AddRange((IEnumerable<string>) new string[8]
      {
        "Customer",
        "Item",
        "Item Price Class",
        "Customer and Item",
        "Customer and Item Price Class",
        "Customer Price Class",
        "Customer Price Class and Item",
        "Customer Price Class and Item Price Class"
      });
      if (PXAccess.FeatureInstalled<FeaturesSet.warehouse>())
      {
        stringList1.AddRange((IEnumerable<string>) new string[5]
        {
          "WH",
          "WI",
          "WC",
          "WP",
          "WE"
        });
        stringList2.AddRange((IEnumerable<string>) new string[5]
        {
          "Warehouse",
          "Warehouse and Item",
          "Warehouse and Customer",
          "Warehouse and Item Price Class",
          "Warehouse and Customer Price Class"
        });
      }
      if (PXAccess.FeatureInstalled<FeaturesSet.branch>() || PXAccess.FeatureInstalled<FeaturesSet.multiCompany>())
      {
        stringList1.Add("BR");
        stringList2.Add("Branch");
      }
      stringList1.Add("UN");
      stringList2.Add("Unconditional");
      e.ReturnState = (object) PXStringState.CreateInstance(e.ReturnValue, new int?(1), new bool?(false), "ApplicableTo", new bool?(false), new int?(1), (string) null, stringList1.ToArray(), ((IEnumerable<string>) stringList2.ToArray()).Select<string, string>((Func<string, string>) (l => PXMessages.LocalizeNoPrefix(l))).ToArray<string>(), new bool?(true), "IN", (string[]) null);
    }
  }
}
