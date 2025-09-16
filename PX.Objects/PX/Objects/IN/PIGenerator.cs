// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.PIGenerator
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.Maintenance;
using PX.Objects.CS;
using PX.Objects.IN.PhysicalInventory;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.IN;

public class PIGenerator : PXGraph<PIGenerator>
{
  public PXCancel<PIGeneratorSettings> Cancel;
  public PXFilter<PIGeneratorSettings> GeneratorSettings;
  public PXFilter<PIGeneratorSettings> CurrentGeneratorSettings;
  public PXSelectOrderBy<PIPreliminaryResult, OrderBy<Asc<PIPreliminaryResult.lineNbr>>> PreliminaryResultRecs;
  [PXReadOnlyView]
  public PXSelect<INLocation> LocationsToLock;
  [PXVirtualDAC]
  [PXNotCleanable]
  [PXReadOnlyView]
  public PXSelect<ExcludedLocation> ExcludedLocations;
  [PXReadOnlyView]
  public PXSelect<InventoryItem> InventoryItemsToLock;
  [PXVirtualDAC]
  [PXNotCleanable]
  [PXReadOnlyView]
  public PXSelect<ExcludedInventoryItem> ExcludedInventoryItems;
  public PXSelect<INPIHeader> piheader;
  public PXSelect<INPIDetail, Where<INPIDetail.pIID, Equal<Current<INPIHeader.pIID>>>> pidetail;
  public PXSelect<INPIStatusItem> inpistatusitem;
  public PXSelect<INPIStatusLoc> inpistatusloc;
  public PXSetup<INSetup> insetup;
  public PXAction<PIGeneratorSettings> GeneratePI;

  [PXMergeAttributes]
  [PXRemoveBaseAttribute(typeof (PXSelectorAttribute))]
  protected virtual void INPIStatusItem_PIID_CacheAttached(PXCache sender)
  {
  }

  [PXMergeAttributes]
  [PXRemoveBaseAttribute(typeof (PXSelectorAttribute))]
  protected virtual void INPIStatusLoc_PIID_CacheAttached(PXCache sender)
  {
  }

  [PXUIField]
  [PXProcessButton]
  public virtual IEnumerable generatePI(PXAdapter adapter)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: method pointer
    PXLongOperation.StartOperation(((PXGraph) this).UID, new PXToggleAsyncDelegate((object) new PIGenerator.\u003C\u003Ec__DisplayClass16_0()
    {
      currentSettings = ((PXSelectBase<PIGeneratorSettings>) this.GeneratorSettings).Current,
      excludedLocations = GraphHelper.RowCast<ExcludedLocation>((IEnumerable) ((PXSelectBase<ExcludedLocation>) this.ExcludedLocations).Select(Array.Empty<object>())).ToList<ExcludedLocation>(),
      excludedInventories = GraphHelper.RowCast<ExcludedInventoryItem>((IEnumerable) ((PXSelectBase<ExcludedInventoryItem>) this.ExcludedInventoryItems).Select(Array.Empty<object>())).ToList<ExcludedInventoryItem>()
    }, __methodptr(\u003CgeneratePI\u003Eb__0)));
    return adapter.Get();
  }

  public PIGenerator()
  {
    ((PXSelectBase) this.PreliminaryResultRecs).Cache.AllowInsert = false;
    ((PXSelectBase) this.PreliminaryResultRecs).Cache.AllowDelete = false;
    ((PXSelectBase) this.PreliminaryResultRecs).Cache.AllowUpdate = false;
    ((PXSelectBase) this.LocationsToLock).AllowInsert = false;
    ((PXSelectBase) this.LocationsToLock).AllowUpdate = false;
    ((PXSelectBase) this.LocationsToLock).AllowDelete = false;
    ((PXSelectBase) this.InventoryItemsToLock).AllowInsert = false;
    ((PXSelectBase) this.InventoryItemsToLock).AllowUpdate = false;
    ((PXSelectBase) this.InventoryItemsToLock).AllowDelete = false;
  }

  public virtual bool IsDirty => false;

  public virtual List<PIPreliminaryResult> CalcPIRows(bool updateDB)
  {
    return this.CalcPIRows(updateDB, true);
  }

  public virtual List<PIPreliminaryResult> CalcPIRows(bool updateDB, bool saveEmpty)
  {
    INSetup current1 = ((PXSelectBase<INSetup>) this.insetup).Current;
    PIGeneratorSettings current2 = ((PXSelectBase<PIGeneratorSettings>) this.GeneratorSettings).Current;
    if (current2 == null || current2.PIClassID == null || (current2 != null ? (!current2.SiteID.HasValue ? 1 : 0) : 1) != 0)
      return new List<PIPreliminaryResult>();
    if (updateDB)
    {
      ((PXSelectBase) this.piheader).Cache.Clear();
      ((PXSelectBase) this.pidetail).Cache.Clear();
      ((PXSelectBase) this.inpistatusitem).Cache.Clear();
      ((PXSelectBase) this.inpistatusloc).Cache.Clear();
    }
    List<INLocation> list = GraphHelper.RowCast<INLocation>((IEnumerable) this.GetPiTypeLocations()).ToList<INLocation>();
    HashSet<int> hashSet = GraphHelper.RowCast<ExcludedInventoryItem>((IEnumerable) ((PXSelectBase<ExcludedInventoryItem>) this.ExcludedInventoryItems).Select(Array.Empty<object>())).Where<ExcludedInventoryItem>((Func<ExcludedInventoryItem, bool>) (i => i.InventoryID.HasValue)).Select<ExcludedInventoryItem, int>((Func<ExcludedInventoryItem, int>) (i => i.InventoryID.Value)).ToHashSet<int>();
    HashSet<int> excludedLocationIds = GraphHelper.RowCast<ExcludedLocation>((IEnumerable) ((PXSelectBase<ExcludedLocation>) this.ExcludedLocations).Select(Array.Empty<object>())).Where<ExcludedLocation>((Func<ExcludedLocation, bool>) (i => i.LocationID.HasValue)).Select<ExcludedLocation, int>((Func<ExcludedLocation, int>) (i => i.LocationID.Value)).ToHashSet<int>();
    List<PIItemLocationInfo> intermediateResult = this.PrepareIntermediateResult(list, hashSet, excludedLocationIds);
    int num1 = 1;
    if (intermediateResult.Count == 0)
    {
      if (!(current2.Method != "F"))
      {
        short? blankLines = current2.BlankLines;
        int? nullable = blankLines.HasValue ? new int?((int) blankLines.GetValueOrDefault()) : new int?();
        int num2 = 0;
        if (!(nullable.GetValueOrDefault() == num2 & nullable.HasValue) && (list.Count <= 0 || !list.All<INLocation>((Func<INLocation, bool>) (l => excludedLocationIds.Contains(l.LocationID.Value)))))
          goto label_8;
      }
      return new List<PIPreliminaryResult>();
    }
label_8:
    if (updateDB)
    {
      INPIHeader inpiHeader = new INPIHeader();
      PX.Objects.CS.ReasonCode reasonCode = PX.Objects.CS.ReasonCode.PK.Find((PXGraph) this, current1.PIReasonCode);
      inpiHeader.PIClassID = current2.PIClassID;
      inpiHeader.Descr = current2.Descr;
      inpiHeader.SiteID = current2.SiteID;
      inpiHeader.Status = "N";
      if (reasonCode != null)
      {
        inpiHeader.PIAdjAcctID = reasonCode.AccountID;
        inpiHeader.PIAdjSubID = reasonCode.SubID;
      }
      inpiHeader.TagNumbered = current1.PIUseTags;
      inpiHeader.TotalNbrOfTags = new int?(0);
      inpiHeader.LineCntr = new int?(0);
      inpiHeader.TotalVarQty = new Decimal?(0M);
      inpiHeader.TotalVarCost = new Decimal?(0M);
      ((PXSelectBase) this.piheader).Cache.Insert((object) inpiHeader);
    }
    List<PIPreliminaryResult> preliminaryResultList = new List<PIPreliminaryResult>();
    foreach (PIItemLocationInfo itemLocationInfo in intermediateResult)
    {
      InventoryItem inventoryItem = itemLocationInfo.QueryResult.GetItem<InventoryItem>();
      PIPreliminaryResult preliminaryResult1 = new PIPreliminaryResult();
      preliminaryResult1.InventoryID = new int?(itemLocationInfo.InventoryID);
      preliminaryResult1.SubItemID = itemLocationInfo.SubItemID;
      preliminaryResult1.SiteID = current2.SiteID;
      preliminaryResult1.LocationID = new int?(itemLocationInfo.LocationID);
      preliminaryResult1.Descr = inventoryItem.Descr;
      preliminaryResult1.BaseUnit = inventoryItem.BaseUnit;
      preliminaryResult1.ItemClassID = inventoryItem.ItemClassID;
      INLotSerialStatus status1 = itemLocationInfo.QueryResult.GetItem<INLotSerialStatus>();
      if (status1 != null && status1.LotSerialNbr != null)
      {
        preliminaryResult1.LotSerialNbr = status1.LotSerialNbr;
        preliminaryResult1.ExpireDate = status1.ExpireDate;
        preliminaryResult1.BookQty = new Decimal?(status1.GetBookQty());
      }
      else
      {
        preliminaryResult1.LotSerialNbr = (string) null;
        preliminaryResult1.ExpireDate = new DateTime?();
        preliminaryResult1.BookQty = new Decimal?(0M);
        INLocationStatus status2 = itemLocationInfo.QueryResult.GetItem<INLocationStatus>();
        if (status2 != null)
          preliminaryResult1.BookQty = new Decimal?(status2.GetBookQty());
      }
      preliminaryResult1.LineNbr = new int?(num1++);
      bool? nullable1 = current1.PIUseTags;
      int? nullable2;
      int? nullable3;
      if (nullable1.GetValueOrDefault())
      {
        PIPreliminaryResult preliminaryResult2 = preliminaryResult1;
        nullable2 = current1.PILastTagNumber;
        int valueOrDefault = nullable2.GetValueOrDefault();
        nullable3 = preliminaryResult1.LineNbr;
        int? nullable4;
        if (!nullable3.HasValue)
        {
          nullable2 = new int?();
          nullable4 = nullable2;
        }
        else
          nullable4 = new int?(valueOrDefault + nullable3.GetValueOrDefault());
        preliminaryResult2.TagNumber = nullable4;
      }
      nullable1 = current2.ByFrequency;
      if (!nullable1.GetValueOrDefault() || (!(current2.Method == "Y") || this.IsReadyToBeCounted<INPICycle>(itemLocationInfo.QueryResult)) && (!(current2.Method == "A") || this.IsReadyToBeCounted<INABCCode>(itemLocationInfo.QueryResult)) && (!(current2.Method == "M") || this.IsReadyToBeCounted<INMovementClass>(itemLocationInfo.QueryResult)))
      {
        preliminaryResultList.Add(preliminaryResult1);
        if (updateDB)
        {
          INPIHeader current3 = ((PXSelectBase<INPIHeader>) this.piheader).Current;
          INPIDetail inpiDetail = new INPIDetail();
          inpiDetail.BookQty = preliminaryResult1.BookQty;
          nullable1 = current1.PIUseTags;
          if (nullable1.GetValueOrDefault())
          {
            INPIHeader inpiHeader = current3;
            nullable3 = inpiHeader.TotalNbrOfTags;
            nullable2 = nullable3;
            inpiHeader.TotalNbrOfTags = nullable2.HasValue ? new int?(nullable2.GetValueOrDefault() + 1) : new int?();
          }
          inpiDetail.LineNbr = preliminaryResult1.LineNbr;
          inpiDetail.TagNumber = preliminaryResult1.TagNumber;
          inpiDetail.InventoryID = preliminaryResult1.InventoryID;
          inpiDetail.LocationID = preliminaryResult1.LocationID;
          inpiDetail.LotSerialNbr = preliminaryResult1.LotSerialNbr;
          inpiDetail.ExpireDate = preliminaryResult1.ExpireDate;
          inpiDetail.Status = "N";
          inpiDetail.SubItemID = preliminaryResult1.SubItemID;
          inpiDetail.LineType = "N";
          ((PXSelectBase<INPIHeader>) this.piheader).Update(current3);
          ((PXSelectBase<INPIDetail>) this.pidetail).Insert(inpiDetail);
        }
      }
    }
    int num3 = 0;
    bool? piUseTags;
    int? nullable5;
    while (true)
    {
      int num4 = num3;
      short? blankLines = current2.BlankLines;
      int? nullable6;
      int? nullable7;
      if (!blankLines.HasValue)
      {
        nullable6 = new int?();
        nullable7 = nullable6;
      }
      else
        nullable7 = new int?((int) blankLines.GetValueOrDefault());
      nullable5 = nullable7;
      int valueOrDefault1 = nullable5.GetValueOrDefault();
      if (num4 < valueOrDefault1 & nullable5.HasValue)
      {
        PIPreliminaryResult preliminaryResult3 = new PIPreliminaryResult();
        preliminaryResult3.LineNbr = new int?(num1++);
        preliminaryResult3.BookQty = new Decimal?(0M);
        preliminaryResult3.SiteID = current2.SiteID;
        piUseTags = current1.PIUseTags;
        if (piUseTags.GetValueOrDefault())
        {
          PIPreliminaryResult preliminaryResult4 = preliminaryResult3;
          nullable6 = current1.PILastTagNumber;
          int valueOrDefault2 = nullable6.GetValueOrDefault();
          nullable5 = preliminaryResult3.LineNbr;
          int? nullable8;
          if (!nullable5.HasValue)
          {
            nullable6 = new int?();
            nullable8 = nullable6;
          }
          else
            nullable8 = new int?(valueOrDefault2 + nullable5.GetValueOrDefault());
          preliminaryResult4.TagNumber = nullable8;
        }
        preliminaryResultList.Add(preliminaryResult3);
        if (updateDB)
        {
          INPIHeader current4 = ((PXSelectBase<INPIHeader>) this.piheader).Current;
          INPIDetail inpiDetail = new INPIDetail();
          inpiDetail.BookQty = preliminaryResult3.BookQty;
          INPIHeader inpiHeader1 = current4;
          nullable5 = inpiHeader1.LineCntr;
          nullable6 = nullable5;
          inpiHeader1.LineCntr = nullable6.HasValue ? new int?(nullable6.GetValueOrDefault() + 1) : new int?();
          piUseTags = current1.PIUseTags;
          if (piUseTags.GetValueOrDefault())
          {
            INPIHeader inpiHeader2 = current4;
            nullable5 = inpiHeader2.TotalNbrOfTags;
            nullable6 = nullable5;
            inpiHeader2.TotalNbrOfTags = nullable6.HasValue ? new int?(nullable6.GetValueOrDefault() + 1) : new int?();
          }
          inpiDetail.LineNbr = preliminaryResult3.LineNbr;
          inpiDetail.TagNumber = preliminaryResult3.TagNumber;
          inpiDetail.InventoryID = preliminaryResult3.InventoryID;
          inpiDetail.LocationID = preliminaryResult3.LocationID;
          inpiDetail.LotSerialNbr = preliminaryResult3.LotSerialNbr;
          inpiDetail.ExpireDate = preliminaryResult3.ExpireDate;
          inpiDetail.Status = "N";
          inpiDetail.SubItemID = preliminaryResult3.SubItemID;
          inpiDetail.LineType = "B";
          ((PXSelectBase) this.piheader).Cache.Update((object) current4);
          ((PXSelectBase<INPIDetail>) this.pidetail).Insert(inpiDetail);
        }
        ++num3;
      }
      else
        break;
    }
    if (!updateDB)
      return preliminaryResultList;
    piUseTags = current1.PIUseTags;
    if (piUseTags.GetValueOrDefault())
    {
      INSetup inSetup = current1;
      nullable5 = current1.PILastTagNumber;
      int? nullable9 = new int?(nullable5.GetValueOrDefault() + num1 - 1);
      inSetup.PILastTagNumber = nullable9;
      ((PXSelectBase) this.insetup).Cache.Update((object) current1);
    }
    bool fullItemsLock = current2.Method == "F";
    HashSet<int> inventoryItemIds = fullItemsLock ? hashSet : this.GetInventoryItemsToLock(intermediateResult, hashSet).Select<InventoryItem, int>((Func<InventoryItem, int>) (i => i.InventoryID.Value)).ToHashSet<int>();
    bool fullLocationsLock = list.Count == 0;
    HashSet<int> locationIds = fullLocationsLock ? excludedLocationIds : GraphHelper.RowCast<INLocation>((IEnumerable) ((PXSelectBase<INLocation>) this.LocationsToLock).Select(Array.Empty<object>())).Select<INLocation, int>((Func<INLocation, int>) (l => l.LocationID.Value)).ToHashSet<int>();
    PIGeneratorSettings current5 = ((PXSelectBase<PIGeneratorSettings>) this.GeneratorSettings).Current;
    int? siteID;
    if (current5 == null)
    {
      nullable5 = new int?();
      siteID = nullable5;
    }
    else
      siteID = current5.SiteID;
    INSite inSite = INSite.PK.Find((PXGraph) this, siteID);
    this.CreatePILocksManager(((PXSelectBase<INPIHeader>) this.piheader).Current.PIID).Lock(fullItemsLock, (ICollection<int>) inventoryItemIds, fullLocationsLock, (ICollection<int>) locationIds, inSite.SiteCD.Trim());
    if (saveEmpty || preliminaryResultList.Count > 0)
      ((PXGraph) this).Actions.PressSave();
    return preliminaryResultList;
  }

  protected virtual List<PIItemLocationInfo> PrepareIntermediateResult(
    List<INLocation> piTypeLocations,
    HashSet<int> excludedInventoryIds,
    HashSet<int> excludedLocationIds)
  {
    PIGeneratorSettings current = ((PXSelectBase<PIGeneratorSettings>) this.GeneratorSettings).Current;
    if (current == null || current.PIClassID == null || (current != null ? (!current.SiteID.HasValue ? 1 : 0) : 1) != 0)
      return new List<PIItemLocationInfo>();
    BqlCommand bqlCommand = BqlCommand.CreateInstance(new Type[1]
    {
      typeof (Select2<INLocationStatus, InnerJoin<InventoryItem, On2<INLocationStatus.FK.InventoryItem, And<InventoryItem.stkItem, Equal<True>, And<InventoryItem.itemStatus, NotEqual<InventoryItemStatus.inactive>, And<InventoryItem.itemStatus, NotEqual<InventoryItemStatus.markedForDeletion>, And<Where<Match<InventoryItem, Current<AccessInfo.userName>>>>>>>>, LeftJoin<INLotSerClass, On<InventoryItem.FK.LotSerialClass>, InnerJoin<INSubItem, On<INLocationStatus.FK.SubItem>, InnerJoin<INLocation, On<INLocationStatus.FK.Location>, InnerJoin<INItemSiteSettings, On<INItemSiteSettings.inventoryID, Equal<INLocationStatus.inventoryID>, And<INItemSiteSettings.siteID, Equal<INLocationStatus.siteID>>>>>>>>, Where<InventoryItem.stkItem, Equal<boolTrue>, And<INLocationStatus.siteID, Equal<Current<PIGeneratorSettings.siteID>>, And<Where<INLotSerialStatus.inventoryID, IsNotNull, And<INLotSerialStatus.qtyActual, NotEqual<decimal0>, Or<INLotSerialStatus.inventoryID, IsNull, And<INLocationStatus.qtyActual, NotEqual<decimal0>>>>>>>>>)
    });
    if (piTypeLocations.Count > 0)
      bqlCommand = this.UpdateCommandByLocationList<INLocationStatus.locationID>(bqlCommand);
    BqlCommandWithParameters commandWithParameters1 = this.UpdateCommandByGenerationMethod<INLocationStatus.siteID, INLocationStatus.inventoryID, INLocationStatus.subItemID, INLocationStatus.locationID>(new BqlCommandWithParameters(bqlCommand));
    commandWithParameters1.Command = this.JoinLotSerial(commandWithParameters1.Command);
    List<PIItemLocationInfo> source = new List<PIItemLocationInfo>();
    INPIClass piClass = (INPIClass) PXSelectorAttribute.Select<PIGeneratorSettings.pIClassID>(((PXSelectBase) this.GeneratorSettings).Cache, (object) current);
    IItemLocationComparer locationComparer = this.CreateItemLocationComparer(piClass);
    PXView view1 = new PXView((PXGraph) this, true, commandWithParameters1.Command);
    source.AddRange(this.SelectWithinFieldScope(view1, commandWithParameters1.GetParameters(), locationComparer.GetSortColumns()).Where<PIItemLocationInfo>((Func<PIItemLocationInfo, bool>) (il => !excludedInventoryIds.Contains(il.InventoryID) && !excludedLocationIds.Contains(il.LocationID))));
    if (piClass.IncludeZeroItems.GetValueOrDefault() && current.Method != "F" && (current.Method != "I" || current.SelectedMethod != "N"))
    {
      BqlCommand instance = BqlCommand.CreateInstance(new Type[1]
      {
        typeof (Select5<INItemSiteHistDay, InnerJoin<InventoryItem, On<InventoryItem.inventoryID, Equal<INItemSiteHistDay.inventoryID>, And<InventoryItem.itemStatus, NotEqual<InventoryItemStatus.inactive>, And<InventoryItem.itemStatus, NotEqual<InventoryItemStatus.markedForDeletion>, And<Where<Match<InventoryItem, Current<AccessInfo.userName>>>>>>>, InnerJoin<INSubItem, On<INSubItem.subItemID, Equal<INItemSiteHistDay.subItemID>>, InnerJoin<INLocation, On<INLocation.locationID, Equal<INItemSiteHistDay.locationID>>, InnerJoin<INLotSerClass, On<INLotSerClass.lotSerClassID, Equal<InventoryItem.lotSerClassID>, And<INLotSerClass.lotSerTrack, Equal<INLotSerTrack.notNumbered>>>>>>>, Where<INItemSiteHistDay.sDate, GreaterEqual<Required<INItemSiteHistDay.sDate>>, And<INItemSiteHistDay.siteID, Equal<Current<PIGeneratorSettings.siteID>>, And<Where<INItemSiteHistDay.qtyDebit, NotEqual<Zero>, Or<INItemSiteHistDay.qtyCredit, NotEqual<Zero>>>>>>, Aggregate<GroupBy<INItemSiteHistDay.inventoryID, GroupBy<INItemSiteHistDay.subItemID, GroupBy<INItemSiteHistDay.locationID>>>>>)
      });
      bool flag = current.Method == "I" && current.SelectedMethod == "I" && current.MaxLastCountDate.HasValue && current.MaxLastCountDate.Value < ((PXGraph) this).Accessinfo.BusinessDate.Value.AddYears(-1);
      BqlCommandWithParameters cmd = new BqlCommandWithParameters(instance);
      cmd.WhereParameters.Add((object) (flag ? current.MaxLastCountDate : new DateTime?(((PXGraph) this).Accessinfo.BusinessDate.Value.AddYears(-1))));
      if (piTypeLocations.Count > 0)
        cmd.Command = this.UpdateCommandByLocationList<INItemSiteHistDay.locationID>(cmd.Command);
      BqlCommandWithParameters commandWithParameters2 = this.UpdateCommandByGenerationMethod<INItemSiteHistDay.siteID, INItemSiteHistDay.inventoryID, INItemSiteHistDay.subItemID, INItemSiteHistDay.locationID>(cmd);
      HashSet<PIItemLocationInfo> existingItems = source.ToHashSet<PIItemLocationInfo>();
      PXView view2 = new PXView((PXGraph) this, true, commandWithParameters2.Command);
      source.AddRange(this.SelectWithinFieldScope(view2, commandWithParameters2.GetParameters(), (string[]) null).Where<PIItemLocationInfo>((Func<PIItemLocationInfo, bool>) (il => !existingItems.Contains(il) && !excludedInventoryIds.Contains(il.InventoryID) && !excludedLocationIds.Contains(il.LocationID))));
    }
    if (current.Method == "I")
    {
      switch (current.SelectedMethod)
      {
        case "R":
          short valueOrDefault = current.RandomItemsLimit.GetValueOrDefault();
          if (valueOrDefault != (short) 0)
          {
            List<int> list = source.Select<PIItemLocationInfo, int>((Func<PIItemLocationInfo, int>) (il => il.InventoryID)).Distinct<int>().ToList<int>();
            if (list.Count > (int) valueOrDefault)
            {
              list.Sort();
              this.SetRandomSeedIfZero();
              Random random = new Random(current.RandomSeed);
              HashSet<int> randomInventoryIDs = new HashSet<int>();
              for (int index1 = 0; index1 < (int) valueOrDefault; ++index1)
              {
                int index2 = random.Next(0, list.Count);
                randomInventoryIDs.Add(list[index2]);
                list.RemoveAt(index2);
              }
              source = source.Where<PIItemLocationInfo>((Func<PIItemLocationInfo, bool>) (it => randomInventoryIDs.Contains(it.InventoryID))).ToList<PIItemLocationInfo>();
              break;
            }
            break;
          }
          break;
        case "N":
          List<PIItemLocationInfo> itemLocationInfoList = new List<PIItemLocationInfo>();
          HashSet<(int?, int?)> valueTupleSet = new HashSet<(int?, int?)>();
          foreach (PIItemLocationInfo itemLocationInfo in source)
          {
            if (valueTupleSet.Add((new int?(itemLocationInfo.InventoryID), itemLocationInfo.SubItemID)))
            {
              itemLocationInfoList.Add(itemLocationInfo);
              commandWithParameters1.Command = commandWithParameters1.Command.WhereNew<Where<InventoryItem.stkItem, Equal<boolTrue>, And<INLocationStatus.siteID, Equal<Current<PIGeneratorSettings.siteID>>, And<INLocationStatus.inventoryID, Equal<Required<INLocationStatus.inventoryID>>, And<INLocationStatus.subItemID, Equal<Required<INLocationStatus.subItemID>>, And<INLocationStatus.locationID, NotEqual<Required<INLocationStatus.locationID>>>>>>>>();
              commandWithParameters1.WhereParameters = new List<object>()
              {
                (object) itemLocationInfo.InventoryID,
                (object) itemLocationInfo.SubItemID,
                (object) itemLocationInfo.LocationID
              };
              PXView view3 = new PXView((PXGraph) this, true, commandWithParameters1.Command);
              itemLocationInfoList.AddRange(this.SelectWithinFieldScope(view3, commandWithParameters1.GetParameters(), locationComparer.GetSortColumns()).Where<PIItemLocationInfo>((Func<PIItemLocationInfo, bool>) (il => !excludedInventoryIds.Contains(il.InventoryID) && !excludedLocationIds.Contains(il.LocationID))));
            }
          }
          source = itemLocationInfoList;
          break;
      }
    }
    source.Sort((IComparer<PIItemLocationInfo>) locationComparer);
    return source;
  }

  protected virtual IItemLocationComparer CreateItemLocationComparer(INPIClass piClass)
  {
    return (IItemLocationComparer) new PIItemLocationComparer(piClass);
  }

  protected virtual PILocksManager CreatePILocksManager(string piId)
  {
    return new PILocksManager((PXGraph) this, (PXSelectBase<INPIStatusItem>) this.inpistatusitem, (PXSelectBase<INPIStatusLoc>) this.inpistatusloc, ((PXSelectBase<PIGeneratorSettings>) this.GeneratorSettings).Current.SiteID.Value, piId);
  }

  protected virtual IEnumerable<PIItemLocationInfo> SelectWithinFieldScope(
    PXView view,
    object[] parameters,
    string[] sortColumns)
  {
    int num1 = 0;
    int num2 = 0;
    using (new PXFieldScope(view, new Type[20]
    {
      typeof (InventoryItem.inventoryID),
      typeof (InventoryItem.inventoryCD),
      typeof (InventoryItem.descr),
      typeof (InventoryItem.stkItem),
      typeof (InventoryItem.baseUnit),
      typeof (InventoryItem.itemClassID),
      typeof (INSubItem.subItemID),
      typeof (INSubItem.subItemCD),
      typeof (INLocation.locationID),
      typeof (INLocation.locationCD),
      typeof (INLocationStatus.qtyOnHand),
      typeof (INLocationStatus.qtyActual),
      typeof (INLotSerialStatus.lotSerialNbr),
      typeof (INLotSerialStatus.expireDate),
      typeof (INLotSerialStatus.qtyOnHand),
      typeof (INLotSerialStatus.qtyActual),
      typeof (INPICycle.countsPerYear),
      typeof (INABCCode.countsPerYear),
      typeof (INMovementClass.countsPerYear),
      typeof (LastPICountDate.lastCountDate)
    }))
      return view.Select((object[]) null, parameters, (object[]) null, sortColumns, (bool[]) null, (PXFilterRow[]) null, ref num1, -1, ref num2).Select<object, PIItemLocationInfo>((Func<object, PIItemLocationInfo>) (result => PIItemLocationInfo.Create((PXResult) result)));
  }

  protected virtual ICollection<InventoryItem> GetInventoryItemsToLock(
    List<PIItemLocationInfo> intermediateResult,
    HashSet<int> excludedInventoryIds)
  {
    PIGeneratorSettings current = ((PXSelectBase<PIGeneratorSettings>) this.GeneratorSettings).Current;
    return this.GetDistinctItems(intermediateResult.Select<PIItemLocationInfo, InventoryItem>((Func<PIItemLocationInfo, InventoryItem>) (il => il.QueryResult.GetItem<InventoryItem>())).Union<InventoryItem>((IEnumerable<InventoryItem>) this.GetAdditionalInventoryItemsToLock(current)), excludedInventoryIds);
  }

  protected ICollection<InventoryItem> GetDistinctItems(
    IEnumerable<InventoryItem> inventoryItems,
    HashSet<int> excludedInventoryIds)
  {
    Dictionary<int, InventoryItem> dictionary1 = new Dictionary<int, InventoryItem>();
    foreach (InventoryItem inventoryItem1 in inventoryItems)
    {
      HashSet<int> intSet = excludedInventoryIds;
      int? inventoryId = inventoryItem1.InventoryID;
      int num = inventoryId.Value;
      if (!intSet.Contains(num))
      {
        Dictionary<int, InventoryItem> dictionary2 = dictionary1;
        inventoryId = inventoryItem1.InventoryID;
        int key1 = inventoryId.Value;
        if (!dictionary2.ContainsKey(key1))
        {
          Dictionary<int, InventoryItem> dictionary3 = dictionary1;
          inventoryId = inventoryItem1.InventoryID;
          int key2 = inventoryId.Value;
          InventoryItem inventoryItem2 = inventoryItem1;
          dictionary3.Add(key2, inventoryItem2);
        }
      }
    }
    return (ICollection<InventoryItem>) dictionary1.Values;
  }

  protected virtual List<InventoryItem> GetAdditionalInventoryItemsToLock(PIGeneratorSettings gs)
  {
    if (gs.Method != "F" && (gs.Method != "I" || gs.SelectedMethod != "N" && gs.SelectedMethod != "R" && gs.SelectedMethod != "I"))
    {
      bool? byFrequency = gs.ByFrequency;
      bool flag = false;
      if (byFrequency.GetValueOrDefault() == flag & byFrequency.HasValue || gs.Method != "A" && gs.Method != "M" && gs.Method != "Y")
      {
        BqlCommandWithParameters commandWithParameters = this.UpdateCommandByGenerationMethod<Current<PIGeneratorSettings.siteID>, InventoryItem.inventoryID, BqlPlaceholder.A, BqlPlaceholder.B>(new BqlCommandWithParameters(BqlCommand.CreateInstance(new Type[1]
        {
          typeof (Select4<InventoryItem, Where<InventoryItem.stkItem, Equal<True>, And<InventoryItem.isTemplate, Equal<False>, And<InventoryItem.itemStatus, NotEqual<InventoryItemStatus.inactive>, And<InventoryItem.itemStatus, NotEqual<InventoryItemStatus.markedForDeletion>, And<Where<Match<InventoryItem, Current<AccessInfo.userName>>>>>>>>, Aggregate<GroupBy<InventoryItem.inventoryID, Count>>>)
        })));
        int num1 = 0;
        int num2 = 0;
        PXView pxView = new PXView((PXGraph) this, true, commandWithParameters.Command);
        using (new PXFieldScope(pxView, new Type[3]
        {
          typeof (InventoryItem.inventoryID),
          typeof (InventoryItem.stkItem),
          typeof (InventoryItem.inventoryCD)
        }))
          return GraphHelper.RowCast<InventoryItem>((IEnumerable) pxView.Select((object[]) null, commandWithParameters.GetParameters(), (object[]) null, (string[]) null, (bool[]) null, (PXFilterRow[]) null, ref num1, -1, ref num2)).ToList<InventoryItem>();
      }
    }
    return new List<InventoryItem>();
  }

  private BqlCommand UpdateCommandByLocationList<FieldLocationID>(BqlCommand cmd) where FieldLocationID : IBqlField
  {
    return BqlCommand.AppendJoin<InnerJoin<INPIClassLocation, On<INPIClassLocation.pIClassID, Equal<Current<PIGeneratorSettings.pIClassID>>, And<INPIClassLocation.locationID, Equal<FieldLocationID>>>>>(cmd);
  }

  protected virtual BqlCommandWithParameters UpdateCommandByGenerationMethod<TSiteIdField, TInventoryIdField, TSubItemIdField, TLocationIdField>(
    BqlCommandWithParameters cmd)
    where TSiteIdField : IBqlOperand
    where TInventoryIdField : IBqlField
    where TSubItemIdField : IBqlField
    where TLocationIdField : IBqlField
  {
    PIGeneratorSettings current = ((PXSelectBase<PIGeneratorSettings>) this.GeneratorSettings).Current;
    switch (current.Method)
    {
      case "I":
        if (current.SelectedMethod == "I")
        {
          cmd.Command = BqlCommand.AppendJoin<LeftJoin<LastPICountDate, On<LastPICountDate.siteID, Equal<TSiteIdField>, And<LastPICountDate.inventoryID, Equal<TInventoryIdField>, And<LastPICountDate.subItemID, Equal<TSubItemIdField>, And<LastPICountDate.locationID, Equal<TLocationIdField>>>>>>>(cmd.Command);
          cmd.Command = cmd.Command.WhereAnd<Where<LastPICountDate.lastCountDate, IsNull, Or<LastPICountDate.lastCountDate, LessEqual<Current<PIGeneratorSettings.maxLastCountDate>>>>>();
        }
        if (current.SelectedMethod == "N")
        {
          cmd.Command = this.JoinLotSerial(cmd.Command);
          cmd.Command = cmd.Command.WhereNew<Where<InventoryItem.stkItem, Equal<boolTrue>, And<INLocationStatus.siteID, Equal<Current<PIGeneratorSettings.siteID>>, And<Where<INLotSerialStatus.inventoryID, IsNotNull, And<INLotSerialStatus.qtyActual, Less<decimal0>, Or<INLotSerialStatus.inventoryID, IsNull, And<INLocationStatus.qtyActual, Less<decimal0>>>>>>>>>();
        }
        if (current.SelectedMethod == "L")
        {
          cmd.Command = BqlCommand.AppendJoin<InnerJoin<INPIClassItem, On<INPIClassItem.pIClassID, Equal<Current<PIGeneratorSettings.pIClassID>>, And<INPIClassItem.inventoryID, Equal<InventoryItem.inventoryID>>>>>(cmd.Command);
          break;
        }
        break;
      case "C":
        string[] array1 = GraphHelper.RowCast<INItemClass>((IEnumerable) PXSelectBase<INItemClass, PXSelectReadonly2<INItemClass, InnerJoin<INPIClassItemClass, On<INPIClassItemClass.FK.ItemClass>>, Where<INPIClassItemClass.pIClassID, Equal<Current<PIGeneratorSettings.pIClassID>>>>.Config>.Select((PXGraph) this, Array.Empty<object>())).Select<INItemClass, string>((Func<INItemClass, string>) (ic => DimensionTree<INItemClass.dimension>.MakeWildcard(ic.ItemClassCD.TrimEnd()))).ToArray<string>();
        if (((IEnumerable<string>) array1).Any<string>())
        {
          Type[] array2 = BqlCommand.Decompose(typeof (InnerJoin<INItemClass, On2<InventoryItem.FK.ItemClass, And<INItemClass.itemClassID, In2<Search<INItemClass.itemClassID, BqlNone>>>>>));
          array2[array2.LastIndex<Type>()] = this.WhereFieldIsLikeOneOfParameters<INItemClass.itemClassCD>(array1.Length);
          cmd.Command = BqlCommand.AppendJoin(cmd.Command, BqlCommand.Compose(array2));
          cmd.JoinParameters.AddRange((IEnumerable<object>) array1);
          break;
        }
        break;
      case "A":
        cmd.Command = this.JoinItemSiteSettings<TSiteIdField, TInventoryIdField>(cmd.Command);
        bool? byFrequency1 = current.ByFrequency;
        cmd.Command = !byFrequency1.GetValueOrDefault() ? cmd.Command.WhereAnd<Where<INItemSiteSettings.aBCCodeID, Equal<Current<PIGeneratorSettings.aBCCodeID>>>>() : BqlCommand.AppendJoin<InnerJoin<INABCCode, On<INABCCode.aBCCodeID, Equal<INItemSiteSettings.aBCCodeID>, And<INABCCode.countsPerYear, IsNotNull, And<INABCCode.countsPerYear, NotEqual<short0>>>>, LeftJoin<LastPICountDate, On<LastPICountDate.siteID, Equal<TSiteIdField>, And<LastPICountDate.inventoryID, Equal<TInventoryIdField>, And<LastPICountDate.subItemID, Equal<TSubItemIdField>, And<LastPICountDate.locationID, Equal<TLocationIdField>>>>>>>>(cmd.Command);
        break;
      case "M":
        cmd.Command = this.JoinItemSiteSettings<TSiteIdField, TInventoryIdField>(cmd.Command);
        bool? byFrequency2 = current.ByFrequency;
        cmd.Command = !byFrequency2.GetValueOrDefault() ? cmd.Command.WhereAnd<Where<INItemSiteSettings.movementClassID, Equal<Current<PIGeneratorSettings.movementClassID>>>>() : BqlCommand.AppendJoin<InnerJoin<INMovementClass, On<INMovementClass.movementClassID, Equal<INItemSiteSettings.movementClassID>, And<INMovementClass.countsPerYear, IsNotNull, And<INMovementClass.countsPerYear, NotEqual<short0>>>>, LeftJoin<LastPICountDate, On<LastPICountDate.siteID, Equal<TSiteIdField>, And<LastPICountDate.inventoryID, Equal<TInventoryIdField>, And<LastPICountDate.subItemID, Equal<TSubItemIdField>, And<LastPICountDate.locationID, Equal<TLocationIdField>>>>>>>>(cmd.Command);
        break;
      case "Y":
        bool? byFrequency3 = current.ByFrequency;
        cmd.Command = !byFrequency3.GetValueOrDefault() ? cmd.Command.WhereAnd<Where<InventoryItem.cycleID, Equal<Current<PIGeneratorSettings.cycleID>>>>() : BqlCommand.AppendJoin<InnerJoin<INPICycle, On<INPICycle.cycleID, Equal<InventoryItem.cycleID>, And<INPICycle.countsPerYear, IsNotNull, And<INPICycle.countsPerYear, NotEqual<short0>>>>, LeftJoin<LastPICountDate, On<LastPICountDate.siteID, Equal<TSiteIdField>, And<LastPICountDate.inventoryID, Equal<TInventoryIdField>, And<LastPICountDate.subItemID, Equal<TSubItemIdField>, And<LastPICountDate.locationID, Equal<TLocationIdField>>>>>>>>(cmd.Command);
        break;
    }
    return cmd;
  }

  private Type WhereFieldIsLikeOneOfParameters<TField>(int parametersCount) where TField : IBqlField
  {
    if (parametersCount < 1)
      throw new IndexOutOfRangeException();
    Type type1 = typeof (Like<Required<TField>>);
    if (parametersCount == 1)
      return typeof (Where<,>).MakeGenericType(typeof (TField), type1);
    Type type2 = typeof (Or<,>).MakeGenericType(typeof (TField), type1);
    for (int index = 0; index < parametersCount - 2; ++index)
      type2 = typeof (Or<,,>).MakeGenericType(typeof (TField), type1, type2);
    return typeof (Where<,,>).MakeGenericType(typeof (TField), type1, type2);
  }

  private BqlCommand JoinItemSiteSettings<TSiteIdField, TInventoryIdField>(BqlCommand cmd)
    where TSiteIdField : IBqlOperand
    where TInventoryIdField : IBqlField
  {
    return ((IEnumerable<Type>) cmd.GetTables()).Contains<Type>(typeof (INItemSiteSettings)) ? cmd : BqlCommand.AppendJoin<InnerJoin<INItemSiteSettings, On<INItemSiteSettings.inventoryID, Equal<TInventoryIdField>, And<INItemSiteSettings.siteID, Equal<TSiteIdField>>>>>(cmd);
  }

  private BqlCommand JoinLotSerial(BqlCommand cmd)
  {
    if (!((IEnumerable<Type>) cmd.GetTables()).Contains<Type>(typeof (INLotSerClass)) && ((IEnumerable<Type>) cmd.GetTables()).Contains<Type>(typeof (InventoryItem)))
      cmd = BqlCommand.AppendJoin<LeftJoin<INLotSerClass, On<InventoryItem.FK.LotSerialClass>>>(cmd);
    if (!((IEnumerable<Type>) cmd.GetTables()).Contains<Type>(typeof (INLotSerialStatus)))
      cmd = BqlCommand.AppendJoin<LeftJoin<INLotSerialStatus, On<INLotSerialStatus.inventoryID, Equal<INLocationStatus.inventoryID>, And<INLotSerClass.lotSerAssign, Equal<INLotSerAssign.whenReceived>, And<INLotSerialStatus.subItemID, Equal<INLocationStatus.subItemID>, And<INLotSerialStatus.siteID, Equal<INLocationStatus.siteID>, And<INLotSerialStatus.locationID, Equal<INLocationStatus.locationID>, And<INLotSerClass.lotSerTrack, NotEqual<INLotSerTrack.notNumbered>>>>>>>>>(cmd);
    return cmd;
  }

  private bool IsReadyToBeCounted<Cycle>(PXResult it) where Cycle : class, IBqlTable, new()
  {
    Cycle cycle = PXResult.Unwrap<Cycle>((object) it);
    LastPICountDate lastPiCountDate = PXResult.Unwrap<LastPICountDate>((object) it);
    if ((object) cycle == null || lastPiCountDate == null)
      return false;
    DateTime? nullable1 = lastPiCountDate.LastCountDate;
    DateTime dateTime = nullable1 ?? DateTime.MinValue;
    nullable1 = ((PXGraph) this).Accessinfo.BusinessDate;
    DateTime valueOrDefault = nullable1.GetValueOrDefault();
    short? nullable2 = (short?) ((PXGraph) this).Caches[typeof (Cycle)].GetValue((object) cycle, typeof (INMovementClass.countsPerYear).Name);
    if (nullable2.HasValue)
    {
      short? nullable3 = nullable2;
      int? nullable4 = nullable3.HasValue ? new int?((int) nullable3.GetValueOrDefault()) : new int?();
      int num1 = 0;
      if (!(nullable4.GetValueOrDefault() == num1 & nullable4.HasValue))
      {
        if (valueOrDefault.Year > dateTime.Year)
          return true;
        int num2 = (DateTime.IsLeapYear(valueOrDefault.Year) ? 366 : 365) / (int) nullable2.Value;
        return (valueOrDefault - dateTime).TotalDays > (double) num2;
      }
    }
    return false;
  }

  protected virtual IEnumerable preliminaryResultRecs() => (IEnumerable) this.CalcPIRows(false);

  protected virtual IEnumerable inventoryItemsToLock()
  {
    PIGeneratorSettings current = ((PXSelectBase<PIGeneratorSettings>) this.GeneratorSettings).Current;
    if (current == null || current.PIClassID == null)
      return (IEnumerable) new PXResultset<InventoryItem>();
    if (current.Method == "F")
      return (IEnumerable) new PXResultset<InventoryItem>();
    List<INLocation> list = GraphHelper.RowCast<INLocation>((IEnumerable) this.GetPiTypeLocations()).ToList<INLocation>();
    HashSet<int> hashSet1 = GraphHelper.RowCast<ExcludedInventoryItem>((IEnumerable) ((PXSelectBase<ExcludedInventoryItem>) this.ExcludedInventoryItems).Select(Array.Empty<object>())).Where<ExcludedInventoryItem>((Func<ExcludedInventoryItem, bool>) (i => i.InventoryID.HasValue)).Select<ExcludedInventoryItem, int>((Func<ExcludedInventoryItem, int>) (i => i.InventoryID.Value)).ToHashSet<int>();
    HashSet<int> hashSet2 = GraphHelper.RowCast<ExcludedLocation>((IEnumerable) ((PXSelectBase<ExcludedLocation>) this.ExcludedLocations).Select(Array.Empty<object>())).Where<ExcludedLocation>((Func<ExcludedLocation, bool>) (i => i.LocationID.HasValue)).Select<ExcludedLocation, int>((Func<ExcludedLocation, int>) (i => i.LocationID.Value)).ToHashSet<int>();
    return (IEnumerable) this.GetInventoryItemsToLock(this.PrepareIntermediateResult(list, hashSet1, hashSet2), hashSet1);
  }

  protected virtual IEnumerable excludedInventoryItems()
  {
    return ((PXSelectBase) this.ExcludedInventoryItems).Cache.Updated;
  }

  protected virtual IEnumerable locationsToLock()
  {
    if (((PXSelectBase<PIGeneratorSettings>) this.GeneratorSettings).Current?.PIClassID == null)
      return (IEnumerable) new PXResultset<INLocation>();
    PXResultset<INLocation> piTypeLocations = this.GetPiTypeLocations();
    if (piTypeLocations.Count == 0)
      return (IEnumerable) new PXResultset<INLocation>();
    HashSet<int?> hashSet = GraphHelper.RowCast<ExcludedLocation>((IEnumerable) ((PXSelectBase<ExcludedLocation>) this.ExcludedLocations).Select(Array.Empty<object>())).Select<ExcludedLocation, int?>((Func<ExcludedLocation, int?>) (l => l.LocationID)).ToHashSet<int?>();
    PXResultset<INLocation> pxResultset = new PXResultset<INLocation>();
    foreach (PXResult<INLocation> pxResult in piTypeLocations)
    {
      INLocation inLocation = PXResult<INLocation>.op_Implicit(pxResult);
      if (!hashSet.Contains(inLocation.LocationID))
        pxResultset.Add(pxResult);
    }
    return (IEnumerable) pxResultset;
  }

  protected virtual IEnumerable excludedLocations()
  {
    return ((PXSelectBase) this.ExcludedLocations).Cache.Updated;
  }

  private PXResultset<INLocation> GetPiTypeLocations()
  {
    PXSelectReadonly2<INLocation, InnerJoin<INPIClassLocation, On<INLocation.locationID, Equal<INPIClassLocation.locationID>>>, Where<INPIClassLocation.pIClassID, Equal<Current<PIGeneratorSettings.pIClassID>>>> pxSelectReadonly2 = new PXSelectReadonly2<INLocation, InnerJoin<INPIClassLocation, On<INLocation.locationID, Equal<INPIClassLocation.locationID>>>, Where<INPIClassLocation.pIClassID, Equal<Current<PIGeneratorSettings.pIClassID>>>>((PXGraph) this);
    using (new PXFieldScope(((PXSelectBase) pxSelectReadonly2).View, new Type[3]
    {
      typeof (INLocation.locationID),
      typeof (INLocation.locationCD),
      typeof (INLocation.descr)
    }))
      return ((PXSelectBase<INLocation>) pxSelectReadonly2).Select(Array.Empty<object>());
  }

  private void SetRandomSeedIfZero()
  {
    PIGeneratorSettings current = ((PXSelectBase<PIGeneratorSettings>) this.GeneratorSettings).Current;
    if (current == null || current.RandomSeed != 0)
      return;
    Random random = new Random();
    current.RandomSeed = random.Next();
  }

  protected virtual void ExcludedInventoryItem_RowUpdating(PXCache sender, PXRowUpdatingEventArgs e)
  {
    ExcludedInventoryItem newRow = (ExcludedInventoryItem) e.NewRow;
    object field = PXSelectorAttribute.GetField(sender, (object) newRow, "InventoryID", (object) newRow.InventoryID, "descr");
    newRow.Descr = field as string;
  }

  protected virtual void ExcludedLocation_RowUpdating(PXCache sender, PXRowUpdatingEventArgs e)
  {
    ExcludedLocation newRow = (ExcludedLocation) e.NewRow;
    object field = PXSelectorAttribute.GetField(sender, (object) newRow, "LocationID", (object) newRow.LocationID, "descr");
    newRow.Descr = field as string;
  }

  protected virtual void PIGeneratorSettings_ByFrequency_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    PIGeneratorSettings row = (PIGeneratorSettings) e.Row;
    if (row == null || !row.ByFrequency.GetValueOrDefault())
      return;
    row.ABCCodeID = (string) null;
    row.CycleID = (string) null;
    row.MovementClassID = (string) null;
  }

  protected virtual void PIGeneratorSettings_PIClassID_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    PIGeneratorSettings row = (PIGeneratorSettings) e.Row;
    if (row == null)
      return;
    INPIClass inpiClass = (INPIClass) PXSelectorAttribute.Select<PIGeneratorSettings.pIClassID>(sender, (object) row);
    if (inpiClass == null)
      return;
    PXCache cach = ((PXGraph) this).Caches[typeof (INPIClass)];
    foreach (string field in (List<string>) sender.Fields)
    {
      bool flag = field.Equals(typeof (PIGeneratorSettings.pIClassID).Name, StringComparison.InvariantCultureIgnoreCase);
      if (!(!cach.Fields.Contains(field) | flag))
      {
        object valueExt = cach.GetValueExt((object) inpiClass, field);
        object obj = valueExt is PXFieldState ? ((PXFieldState) valueExt).Value : valueExt;
        if (obj != null)
          sender.SetValuePending((object) row, field, obj);
      }
    }
  }

  protected virtual void PIGeneratorSettings_RowInserted(PXCache sender, PXRowInsertedEventArgs e)
  {
    if (((PIGeneratorSettings) e.Row).PIClassID != null)
      return;
    this.ClearExcludesCache<ExcludedInventoryItem>(((PXSelectBase) this.ExcludedInventoryItems).Cache);
    this.ClearExcludesCache<ExcludedLocation>(((PXSelectBase) this.ExcludedLocations).Cache);
  }

  protected virtual void PIGeneratorSettings_RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    PIGeneratorSettings row = (PIGeneratorSettings) e.Row;
    PIGeneratorSettings oldRow = (PIGeneratorSettings) e.OldRow;
    if (row.PIClassID != oldRow.PIClassID)
    {
      this.ClearExcludesCache<ExcludedInventoryItem>(((PXSelectBase) this.ExcludedInventoryItems).Cache);
      this.ClearExcludesCache<ExcludedLocation>(((PXSelectBase) this.ExcludedLocations).Cache);
    }
    else
    {
      int? siteId1 = row.SiteID;
      int? siteId2 = oldRow.SiteID;
      if (siteId1.GetValueOrDefault() == siteId2.GetValueOrDefault() & siteId1.HasValue == siteId2.HasValue)
        return;
      this.ClearExcludesCache<ExcludedLocation>(((PXSelectBase) this.ExcludedLocations).Cache);
    }
  }

  protected virtual void ClearExcludesCache<T>(PXCache excludesCache) where T : class, IBqlTable, new()
  {
    foreach (T obj in excludesCache.Cached.Cast<T>().ToList<T>())
      excludesCache.Remove((object) obj);
  }

  protected virtual void PIGeneratorSettings_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    if (e.Row == null)
      return;
    PIGeneratorSettings row = (PIGeneratorSettings) e.Row;
    PXAction<PIGeneratorSettings> generatePi = this.GeneratePI;
    int? nullable1;
    int num1;
    if (row.Method != null)
    {
      nullable1 = row.SiteID;
      num1 = nullable1.HasValue ? 1 : 0;
    }
    else
      num1 = 0;
    ((PXAction) generatePi).SetEnabled(num1 != 0);
    INPIClass inpiClass1 = (INPIClass) PXSelectorAttribute.Select<PIGeneratorSettings.pIClassID>(sender, (object) row);
    if (inpiClass1 != null)
    {
      PXCache cach = ((PXGraph) this).Caches[typeof (INPIClass)];
      foreach (string field in (List<string>) sender.Fields)
      {
        if (string.Compare(field, typeof (PIGeneratorSettings.pIClassID).Name, true) != 0 && string.Compare(field, typeof (PIGeneratorSettings.descr).Name, true) != 0 && cach.Fields.Contains(field))
        {
          object objA = cach.GetValue((object) inpiClass1, field);
          INPIClass inpiClass2 = new INPIClass();
          object objB;
          cach.RaiseFieldDefaulting(field, (object) inpiClass2, ref objB);
          PXUIFieldAttribute.SetEnabled(sender, field, objA == null || object.Equals(objA, objB));
        }
      }
      PXUIFieldAttribute.SetWarning<PIGeneratorSettings.pIClassID>(sender, (object) row, inpiClass1.UnlockSiteOnCountingFinish.GetValueOrDefault() ? "The Unfreeze Stock When Counting Is Finished check box is selected on the Physical Inventory Types (IN208900) form. This may cause discrepancy in cost or quantity of stock items and inability to release PI adjustments." : (string) null);
    }
    PXUIFieldAttribute.SetVisible<PIGeneratorSettings.selectedMethod>(sender, (object) row, row.Method == "I");
    PXUIFieldAttribute.SetVisible<PIGeneratorSettings.randomItemsLimit>(sender, (object) row, row.Method == "I" && row.SelectedMethod == "R");
    PXUIFieldAttribute.SetVisible<PIGeneratorSettings.maxLastCountDate>(sender, (object) row, row.Method == "I" && row.SelectedMethod == "I");
    PXUIFieldAttribute.SetVisible<PIGeneratorSettings.aBCCodeID>(sender, (object) row, row.Method == "A");
    PXUIFieldAttribute.SetVisible<PIGeneratorSettings.movementClassID>(sender, (object) row, row.Method == "M");
    PXUIFieldAttribute.SetVisible<PIGeneratorSettings.cycleID>(sender, (object) row, row.Method == "Y");
    PXUIFieldAttribute.SetVisible<PIGeneratorSettings.byFrequency>(sender, (object) row, row.Method == "A" || row.Method == "M" || row.Method == "Y");
    PXUIFieldAttribute.SetEnabled<PIGeneratorSettings.byFrequency>(sender, (object) row, inpiClass1 != null && inpiClass1.CycleID == null && inpiClass1.MovementClassID == null && inpiClass1.ABCCodeID == null);
    bool? nullable2;
    if (inpiClass1 != null && inpiClass1.CycleID == null && inpiClass1.MovementClassID == null && inpiClass1.ABCCodeID == null)
    {
      PXCache pxCache1 = sender;
      PIGeneratorSettings generatorSettings1 = row;
      nullable2 = row.ByFrequency;
      bool flag1 = false;
      int num2 = nullable2.GetValueOrDefault() == flag1 & nullable2.HasValue ? 1 : 0;
      PXUIFieldAttribute.SetEnabled<PIGeneratorSettings.aBCCodeID>(pxCache1, (object) generatorSettings1, num2 != 0);
      PXCache pxCache2 = sender;
      PIGeneratorSettings generatorSettings2 = row;
      nullable2 = row.ByFrequency;
      bool flag2 = false;
      int num3 = nullable2.GetValueOrDefault() == flag2 & nullable2.HasValue ? 1 : 0;
      PXUIFieldAttribute.SetEnabled<PIGeneratorSettings.movementClassID>(pxCache2, (object) generatorSettings2, num3 != 0);
      PXCache pxCache3 = sender;
      PIGeneratorSettings generatorSettings3 = row;
      nullable2 = row.ByFrequency;
      bool flag3 = false;
      int num4 = nullable2.GetValueOrDefault() == flag3 & nullable2.HasValue ? 1 : 0;
      PXUIFieldAttribute.SetEnabled<PIGeneratorSettings.cycleID>(pxCache3, (object) generatorSettings3, num4 != 0);
    }
    PXUIFieldAttribute.SetEnabled<PIGeneratorSettings.method>(sender, (object) row, false);
    PXUIFieldAttribute.SetEnabled<PIGeneratorSettings.selectedMethod>(sender, (object) row, false);
    PXCache pxCache = sender;
    PIGeneratorSettings generatorSettings4 = row;
    short? lastCountPeriod;
    int num5;
    if (row.LastCountPeriod.HasValue)
    {
      lastCountPeriod = row.LastCountPeriod;
      nullable1 = lastCountPeriod.HasValue ? new int?((int) lastCountPeriod.GetValueOrDefault()) : new int?();
      int num6 = 0;
      num5 = nullable1.GetValueOrDefault() == num6 & nullable1.HasValue ? 1 : 0;
    }
    else
      num5 = 1;
    PXUIFieldAttribute.SetEnabled<PIGeneratorSettings.maxLastCountDate>(pxCache, (object) generatorSettings4, num5 != 0);
    lastCountPeriod = row.LastCountPeriod;
    if (lastCountPeriod.HasValue)
    {
      lastCountPeriod = row.LastCountPeriod;
      nullable1 = lastCountPeriod.HasValue ? new int?((int) lastCountPeriod.GetValueOrDefault()) : new int?();
      int num7 = 0;
      if (!(nullable1.GetValueOrDefault() == num7 & nullable1.HasValue))
      {
        PIGeneratorSettings generatorSettings5 = row;
        DateTime dateTime = ((PXGraph) this).Accessinfo.BusinessDate.Value;
        ref DateTime local = ref dateTime;
        lastCountPeriod = row.LastCountPeriod;
        double num8 = (double) -lastCountPeriod.GetValueOrDefault();
        DateTime? nullable3 = new DateTime?(local.AddDays(num8));
        generatorSettings5.MaxLastCountDate = nullable3;
      }
    }
    PXSelect<ExcludedLocation> excludedLocations1 = this.ExcludedLocations;
    int num9;
    if (inpiClass1 != null)
    {
      nullable1 = row.SiteID;
      num9 = nullable1.HasValue ? 1 : 0;
    }
    else
      num9 = 0;
    ((PXSelectBase) excludedLocations1).AllowInsert = num9 != 0;
    PXSelect<ExcludedLocation> excludedLocations2 = this.ExcludedLocations;
    int num10;
    if (inpiClass1 != null)
    {
      nullable1 = row.SiteID;
      num10 = nullable1.HasValue ? 1 : 0;
    }
    else
      num10 = 0;
    ((PXSelectBase) excludedLocations2).AllowDelete = num10 != 0;
    PXSelect<ExcludedLocation> excludedLocations3 = this.ExcludedLocations;
    int num11;
    if (inpiClass1 != null)
    {
      nullable1 = row.SiteID;
      num11 = nullable1.HasValue ? 1 : 0;
    }
    else
      num11 = 0;
    ((PXSelectBase) excludedLocations3).AllowUpdate = num11 != 0;
    ((PXSelectBase) this.ExcludedInventoryItems).AllowInsert = inpiClass1 != null;
    ((PXSelectBase) this.ExcludedInventoryItems).AllowDelete = inpiClass1 != null;
    ((PXSelectBase) this.ExcludedInventoryItems).AllowUpdate = inpiClass1 != null;
    PXCache cache = ((PXSelectBase) this.PreliminaryResultRecs).Cache;
    nullable2 = ((PXSelectBase<INSetup>) this.insetup).Current.PIUseTags;
    int num12 = nullable2.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetVisible<PIPreliminaryResult.tagNumber>(cache, (object) null, num12 != 0);
  }

  protected virtual void INSetup_RowPersisting(PXCache cache, PXRowPersistingEventArgs e)
  {
    if ((INSetup) e.Row == null || (e.Operation & 3) == 3)
      return;
    PXDefaultAttribute.SetPersistingCheck<INSetup.iNTransitAcctID>(cache, e.Row, PXAccess.FeatureInstalled<FeaturesSet.warehouse>() ? (PXPersistingCheck) 0 : (PXPersistingCheck) 2);
    PXDefaultAttribute.SetPersistingCheck<INSetup.iNTransitSubID>(cache, e.Row, PXAccess.FeatureInstalled<FeaturesSet.warehouse>() ? (PXPersistingCheck) 0 : (PXPersistingCheck) 2);
  }
}
