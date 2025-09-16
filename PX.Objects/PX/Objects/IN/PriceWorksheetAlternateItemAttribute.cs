// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.PriceWorksheetAlternateItemAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Objects.AP;
using PX.Objects.AR;
using PX.Objects.CS;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

#nullable disable
namespace PX.Objects.IN;

[PXDBString(50, IsUnicode = true, InputMask = "")]
[PXDefault]
[PXUIField]
public class PriceWorksheetAlternateItemAttribute : PXAggregateAttribute
{
  public string[] AlternateTypePriority { get; set; } = new string[5]
  {
    "0CPN",
    "0VPN",
    "BAR",
    "GIN",
    "GLBL"
  };

  public virtual void CacheAttached(PXCache sender)
  {
    base.CacheAttached(sender);
    PXGraph.FieldVerifyingEvents fieldVerifying = sender.Graph.FieldVerifying;
    Type type = typeof (INItemXRef);
    PriceWorksheetAlternateItemAttribute alternateItemAttribute1 = this;
    // ISSUE: virtual method pointer
    PXFieldVerifying pxFieldVerifying = new PXFieldVerifying((object) alternateItemAttribute1, __vmethodptr(alternateItemAttribute1, INItemXRef_BAccountID_FieldVerifying));
    fieldVerifying.AddHandler(type, "BAccountID", pxFieldVerifying);
    PXGraph.FieldUpdatedEvents fieldUpdated1 = sender.Graph.FieldUpdated;
    Type itemType1 = sender.GetItemType();
    PriceWorksheetAlternateItemAttribute alternateItemAttribute2 = this;
    // ISSUE: virtual method pointer
    PXFieldUpdated pxFieldUpdated1 = new PXFieldUpdated((object) alternateItemAttribute2, __vmethodptr(alternateItemAttribute2, PriceWorksheetDetail_InventoryID_FieldUpdated));
    fieldUpdated1.AddHandler(itemType1, "InventoryID", pxFieldUpdated1);
    PXGraph.FieldUpdatedEvents fieldUpdated2 = sender.Graph.FieldUpdated;
    Type itemType2 = sender.GetItemType();
    PriceWorksheetAlternateItemAttribute alternateItemAttribute3 = this;
    // ISSUE: virtual method pointer
    PXFieldUpdated pxFieldUpdated2 = new PXFieldUpdated((object) alternateItemAttribute3, __vmethodptr(alternateItemAttribute3, PriceWorksheetDetail_AlternateID_FieldUpdated));
    fieldUpdated2.AddHandler(itemType2, "AlternateID", pxFieldUpdated2);
    PXGraph.RowSelectedEvents rowSelected = sender.Graph.RowSelected;
    Type itemType3 = sender.GetItemType();
    PriceWorksheetAlternateItemAttribute alternateItemAttribute4 = this;
    // ISSUE: virtual method pointer
    PXRowSelected pxRowSelected = new PXRowSelected((object) alternateItemAttribute4, __vmethodptr(alternateItemAttribute4, PriceWorksheetDetail_RowSelected));
    rowSelected.AddHandler(itemType3, pxRowSelected);
  }

  protected virtual void INItemXRef_BAccountID_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    INItemXRef row = (INItemXRef) e.Row;
    if (!(row.AlternateType != "0VPN") || !(row.AlternateType != "0CPN"))
      return;
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void PriceWorksheetDetail_InventoryID_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    PriceWorksheetAlternateItemAttribute.PriceWrapper priceWrapper = new PriceWorksheetAlternateItemAttribute.PriceWrapper(e.Row);
    PriceWorksheetAlternateItemAttribute.PresetUOMWithSpecialUnits(sender, priceWrapper);
    PriceWorksheetAlternateItemAttribute.UpdateUOMFromCrossReference(sender, priceWrapper, false);
  }

  protected virtual void PriceWorksheetDetail_AlternateID_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    PriceWorksheetAlternateItemAttribute.UpdateUOMFromCrossReference(sender, new PriceWorksheetAlternateItemAttribute.PriceWrapper(e.Row), false);
  }

  protected virtual void PriceWorksheetDetail_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    if (e.Row == null)
      return;
    PriceWorksheetAlternateItemAttribute.UpdateUOMFromCrossReference(sender, new PriceWorksheetAlternateItemAttribute.PriceWrapper(e.Row), true);
  }

  private static void PresetUOMWithSpecialUnits(
    PXCache sender,
    PriceWorksheetAlternateItemAttribute.PriceWrapper priceWrapper)
  {
    InventoryItem inventoryItem = InventoryItem.PK.Find(sender.Graph, priceWrapper.InventoryID);
    INPrimaryAlternateType? alternateType = priceWrapper.AlternateType;
    INPrimaryAlternateType primaryAlternateType = INPrimaryAlternateType.VPN;
    if (alternateType.GetValueOrDefault() == primaryAlternateType & alternateType.HasValue)
    {
      if (inventoryItem == null || inventoryItem.PurchaseUnit == null)
        return;
      sender.SetValueExt(priceWrapper.PriceWorksheetDetail, priceWrapper.UOMField.Name, (object) inventoryItem.PurchaseUnit);
    }
    else
    {
      if (inventoryItem == null || inventoryItem.SalesUnit == null)
        return;
      sender.SetValueExt(priceWrapper.PriceWorksheetDetail, priceWrapper.UOMField.Name, (object) inventoryItem.SalesUnit);
    }
  }

  private static void UpdateUOMFromCrossReference(
    PXCache cache,
    PriceWorksheetAlternateItemAttribute.PriceWrapper priceWorksheetDetail,
    bool warningSettingOnly)
  {
    INPrimaryAlternateType? alternateType1 = priceWorksheetDetail.AlternateType;
    INPrimaryAlternateType primaryAlternateType1 = INPrimaryAlternateType.VPN;
    bool? usingAlternateId;
    int num1;
    if (!(alternateType1.GetValueOrDefault() == primaryAlternateType1 & alternateType1.HasValue) && PXAccess.FeatureInstalled<FeaturesSet.distributionModule>())
    {
      usingAlternateId = ((PXSelectBase<ARSetup>) new PXSetupOptional<ARSetup>(cache.Graph)).Current.LoadSalesPricesUsingAlternateID;
      num1 = usingAlternateId.GetValueOrDefault() ? 1 : 0;
    }
    else
      num1 = 0;
    bool flag = num1 != 0;
    INPrimaryAlternateType? alternateType2 = priceWorksheetDetail.AlternateType;
    INPrimaryAlternateType primaryAlternateType2 = INPrimaryAlternateType.VPN;
    int num2;
    if (alternateType2.GetValueOrDefault() == primaryAlternateType2 & alternateType2.HasValue && PXAccess.FeatureInstalled<FeaturesSet.distributionModule>())
    {
      usingAlternateId = ((PXSelectBase<APSetup>) new PXSetupOptional<APSetup>(cache.Graph)).Current.LoadVendorsPricesUsingAlternateID;
      num2 = usingAlternateId.GetValueOrDefault() ? 1 : 0;
    }
    else
      num2 = 0;
    if (num2 == 0 && !flag)
      return;
    PriceWorksheetAlternateItemAttribute.ClearWarning(cache, priceWorksheetDetail);
    PriceWorksheetAlternateItemAttribute.RestrictInventoryByAlternateID(cache, priceWorksheetDetail, false);
    if (Str.IsNullOrEmpty(priceWorksheetDetail.AlternateID))
      return;
    INItemXRef[] source = PriceWorksheetAlternateItemAttribute.SelectXRefs(cache, priceWorksheetDetail);
    if (!priceWorksheetDetail.InventoryID.HasValue)
    {
      if (source.Length == 0)
        PriceWorksheetAlternateItemAttribute.SetWarning(cache, priceWorksheetDetail, "The specified alternate ID cannot be found in the system.");
      else if (source.Length == 1)
      {
        if (warningSettingOnly)
          return;
        INItemXRef inItemXref = ((IEnumerable<INItemXRef>) source).Single<INItemXRef>();
        cache.SetValueExt(priceWorksheetDetail.PriceWorksheetDetail, priceWorksheetDetail.InventoryIDField.Name, (object) inItemXref.InventoryID);
        if (inItemXref.UOM == null)
          return;
        cache.SetValueExt(priceWorksheetDetail.PriceWorksheetDetail, priceWorksheetDetail.UOMField.Name, (object) inItemXref.UOM);
        PriceWorksheetAlternateItemAttribute.RestrictInventoryByAlternateID(cache, priceWorksheetDetail, true);
      }
      else
      {
        PriceWorksheetAlternateItemAttribute.SetWarning(cache, priceWorksheetDetail, "The specified alternate ID is assigned to multiple inventory items. Please select the appropriate inventory ID in the row.");
        PriceWorksheetAlternateItemAttribute.RestrictInventoryByAlternateID(cache, priceWorksheetDetail, true);
      }
    }
    else
    {
      INItemXRef inItemXref = ((IEnumerable<INItemXRef>) source).FirstOrDefault<INItemXRef>();
      if (inItemXref == null)
      {
        if (PXAccess.FeatureInstalled<FeaturesSet.crossReferenceUniqueness>() && PriceWorksheetAlternateItemAttribute.ExistsGlobalXRefWithSameAltID(cache, priceWorksheetDetail))
          PriceWorksheetAlternateItemAttribute.SetWarning(cache, priceWorksheetDetail, "The specified alternate ID is already defined for another inventory item and thus cannot be assigned to the inventory ID selected in this row.");
        else
          PriceWorksheetAlternateItemAttribute.SetWarning(cache, priceWorksheetDetail, "The specified alternate ID has not been defined for the selected inventory item on the Cross-Reference tab of the Stock Items (IN202500) or Non-Stock Items (IN202000) form. Upon release of the worksheet, the system assigns this alternate ID to the inventory item.");
      }
      else
      {
        if (warningSettingOnly || inItemXref.UOM == null)
          return;
        cache.SetValueExt(priceWorksheetDetail.PriceWorksheetDetail, priceWorksheetDetail.UOMField.Name, (object) inItemXref.UOM);
        PriceWorksheetAlternateItemAttribute.RestrictInventoryByAlternateID(cache, priceWorksheetDetail, true);
      }
    }
  }

  private static bool ExistsGlobalXRefWithSameAltID(
    PXCache cache,
    PriceWorksheetAlternateItemAttribute.PriceWrapper priceWorksheetDetail)
  {
    return GraphHelper.RowCast<INItemXRef>((IEnumerable) ((PXSelectBase<INItemXRef>) new PXSelectReadonly<INItemXRef, Where<INItemXRef.alternateID, Equal<Required<INItemXRef.alternateID>>, And<INItemXRef.alternateType, Equal<INAlternateType.global>>>>(cache.Graph)).Select(new object[1]
    {
      (object) priceWorksheetDetail.AlternateID
    })).Any<INItemXRef>((Func<INItemXRef, bool>) (xr =>
    {
      int? inventoryId1 = xr.InventoryID;
      int? inventoryId2 = priceWorksheetDetail.InventoryID;
      if (!(inventoryId1.GetValueOrDefault() == inventoryId2.GetValueOrDefault() & inventoryId1.HasValue == inventoryId2.HasValue))
        return true;
      int? subItemId1 = xr.SubItemID;
      int? subItemId2 = priceWorksheetDetail.SubItemID;
      return !(subItemId1.GetValueOrDefault() == subItemId2.GetValueOrDefault() & subItemId1.HasValue == subItemId2.HasValue);
    }));
  }

  private static INItemXRef[] SelectXRefs(
    PXCache cache,
    PriceWorksheetAlternateItemAttribute.PriceWrapper priceWorksheetDetail)
  {
    PXSelectBase<INItemXRef> cmd = (PXSelectBase<INItemXRef>) new PXSelectReadonly<INItemXRef, Where<INItemXRef.alternateID, Equal<Required<INItemXRef.alternateID>>>, OrderBy<Asc<INItemXRef.alternateType, Desc<INItemXRef.alternateID>>>>(cache.Graph);
    List<object> objectList = new List<object>()
    {
      (object) priceWorksheetDetail.AlternateID
    };
    AlternativeItemAttribute.AddAlternativeTypeWhere(cmd, priceWorksheetDetail.AlternateType, false);
    if (priceWorksheetDetail.AlternateType.HasValue)
      objectList.Add((object) priceWorksheetDetail.BAccountID);
    int? nullable = priceWorksheetDetail.InventoryID;
    if (nullable.HasValue)
    {
      cmd.WhereAnd<Where<INItemXRef.inventoryID, Equal<Required<INItemXRef.inventoryID>>>>();
      objectList.Add((object) priceWorksheetDetail.InventoryID);
    }
    nullable = priceWorksheetDetail.SubItemID;
    if (nullable.HasValue)
    {
      cmd.WhereAnd<Where<INItemXRef.subItemID, Equal<Required<INItemXRef.subItemID>>>>();
      objectList.Add((object) priceWorksheetDetail.SubItemID);
    }
    INItemXRef[] array = GraphHelper.RowCast<INItemXRef>((IEnumerable) cmd.Select(objectList.ToArray())).ToArray<INItemXRef>();
    string[] alternateTypePriority = cache.GetAttributesOfType<PriceWorksheetAlternateItemAttribute>(priceWorksheetDetail.PriceWorksheetDetail, priceWorksheetDetail.AlternateIDField.Name).FirstOrDefault<PriceWorksheetAlternateItemAttribute>()?.AlternateTypePriority;
    return ((IEnumerable<INItemXRef>) EnumerableExtensions.OrderBy<IGrouping<string, INItemXRef>, string>(((IEnumerable<INItemXRef>) array).GroupBy<INItemXRef, string>((Func<INItemXRef, string>) (x => x.AlternateType)), (Func<IGrouping<string, INItemXRef>, string>) (gx => gx.Key), alternateTypePriority).FirstOrDefault<IGrouping<string, INItemXRef>>((Func<IGrouping<string, INItemXRef>, bool>) (gx => gx.Any<INItemXRef>())) ?? Enumerable.Empty<INItemXRef>()).ToArray<INItemXRef>();
  }

  private static void SetWarning(
    PXCache cache,
    PriceWorksheetAlternateItemAttribute.PriceWrapper priceWorksheetDetail,
    string message)
  {
    cache.RaiseExceptionHandling(priceWorksheetDetail.AlternateIDField.Name, priceWorksheetDetail.PriceWorksheetDetail, (object) priceWorksheetDetail.AlternateID, Str.IsNullOrEmpty(message) ? (Exception) null : (Exception) new PXSetPropertyException(message, (PXErrorLevel) 2));
  }

  private static void ClearWarning(
    PXCache cache,
    PriceWorksheetAlternateItemAttribute.PriceWrapper priceWorksheetDetail)
  {
    PriceWorksheetAlternateItemAttribute.SetWarning(cache, priceWorksheetDetail, (string) null);
  }

  private static void RestrictInventoryByAlternateID(
    PXCache cache,
    PriceWorksheetAlternateItemAttribute.PriceWrapper priceWorksheetDetail,
    bool enable)
  {
    cache.SetValue(priceWorksheetDetail.PriceWorksheetDetail, priceWorksheetDetail.RestrictInventoryByAlternateIDField.Name, (object) enable);
  }

  public static bool XRefsExists(PXCache cache, object priceWorksheetDetail)
  {
    return ((IEnumerable<INItemXRef>) PriceWorksheetAlternateItemAttribute.SelectXRefs(cache, new PriceWorksheetAlternateItemAttribute.PriceWrapper(priceWorksheetDetail))).Any<INItemXRef>();
  }

  public class PriceWrapper
  {
    public PriceWrapper(object priceWorksheetDetail)
    {
      switch (priceWorksheetDetail)
      {
        case null:
          throw new ArgumentNullException(nameof (priceWorksheetDetail));
        case ARPriceWorksheetDetail priceWorksheetDetail1:
          this.PriceWorksheetDetail = (object) priceWorksheetDetail1;
          this.AlternateID = priceWorksheetDetail1.AlternateID;
          this.BAccountID = priceWorksheetDetail1.CustomerID;
          this.InventoryID = priceWorksheetDetail1.InventoryID;
          this.SubItemID = priceWorksheetDetail1.SubItemID;
          this.AlternateType = priceWorksheetDetail1.PriceType == "C" ? INPrimaryAlternateType.CPN.AsNullable<INPrimaryAlternateType>() : new INPrimaryAlternateType?();
          this.UOMField = typeof (ARPriceWorksheetDetail.uOM);
          this.InventoryIDField = typeof (ARPriceWorksheetDetail.inventoryID);
          this.AlternateIDField = typeof (ARPriceWorksheetDetail.alternateID);
          this.RestrictInventoryByAlternateIDField = typeof (ARPriceWorksheetDetail.restrictInventoryByAlternateID);
          break;
        case APPriceWorksheetDetail priceWorksheetDetail2:
          this.PriceWorksheetDetail = (object) priceWorksheetDetail2;
          this.AlternateID = priceWorksheetDetail2.AlternateID;
          this.BAccountID = priceWorksheetDetail2.VendorID;
          this.InventoryID = priceWorksheetDetail2.InventoryID;
          this.SubItemID = priceWorksheetDetail2.SubItemID;
          this.AlternateType = new INPrimaryAlternateType?(INPrimaryAlternateType.VPN);
          this.UOMField = typeof (APPriceWorksheetDetail.uOM);
          this.InventoryIDField = typeof (APPriceWorksheetDetail.inventoryID);
          this.AlternateIDField = typeof (APPriceWorksheetDetail.alternateID);
          this.RestrictInventoryByAlternateIDField = typeof (APPriceWorksheetDetail.restrictInventoryByAlternateID);
          break;
        default:
          throw new PXArgumentException("Attribute supports only {0} and {1} entities", new object[2]
          {
            (object) typeof (ARPriceWorksheetDetail),
            (object) typeof (APPriceWorksheetDetail)
          });
      }
    }

    public object PriceWorksheetDetail { get; }

    public string AlternateID { get; }

    public int? BAccountID { get; }

    public int? InventoryID { get; }

    public int? SubItemID { get; }

    public INPrimaryAlternateType? AlternateType { get; }

    public Type UOMField { get; }

    public Type InventoryIDField { get; }

    public Type AlternateIDField { get; }

    public Type RestrictInventoryByAlternateIDField { get; }
  }
}
