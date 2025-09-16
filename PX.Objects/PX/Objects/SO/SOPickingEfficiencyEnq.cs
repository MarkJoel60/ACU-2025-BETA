// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.SOPickingEfficiencyEnq
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CS;
using PX.Objects.IN;
using PX.SM;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace PX.Objects.SO;

/// <exclude />
public class SOPickingEfficiencyEnq : PXGraph<
#nullable disable
SOPickingEfficiencyEnq>
{
  public PXSetupOptional<SOPickPackShipSetup, Where<BqlOperand<
  #nullable enable
  SOPickPackShipSetup.branchID, IBqlInt>.IsEqual<
  #nullable disable
  BqlField<
  #nullable enable
  AccessInfo.branchID, IBqlInt>.FromCurrent>>> PPSSetup;
  public 
  #nullable disable
  PXFilter<EfficiencyFilter> Filter;
  public FbqlSelect<SelectFromBase<PickingEfficiency, TypeArrayOf<IFbqlJoin>.Empty>.Order<By<BqlField<
  #nullable enable
  PickingEfficiency.endDate, IBqlDateTime>.Desc>>, 
  #nullable disable
  PickingEfficiency>.View Efficiency;
  public PXAction<EfficiencyFilter> viewDocument;

  public IEnumerable efficiency()
  {
    EfficiencyFilter current = ((PXSelectBase<EfficiencyFilter>) this.Filter).Current;
    SOPickPackShipSetup ppsSetup = ((PXSelectBase<SOPickPackShipSetup>) this.PPSSetup).Current;
    Func<Guid?, SOPickPackShipUserSetup> func1 = Func.Memorize<Guid?, SOPickPackShipUserSetup>((Func<Guid?, SOPickPackShipUserSetup>) (userID => SOPickPackShipUserSetup.PK.Find((PXGraph) this, userID) ?? new SOPickPackShipUserSetup().ApplyValuesFrom(ppsSetup)));
    Func<string, PX.Objects.CS.CSBox> GetBox = Func.Memorize<string, PX.Objects.CS.CSBox>((Func<string, PX.Objects.CS.CSBox>) (boxID => PX.Objects.CS.CSBox.PK.Find((PXGraph) this, boxID)));
    Func<int?, (PX.Objects.IN.InventoryItem, INLotSerClass)> func2 = Func.Memorize<int?, (PX.Objects.IN.InventoryItem, INLotSerClass)>((Func<int?, (PX.Objects.IN.InventoryItem, INLotSerClass)>) (inventoryID =>
    {
      PX.Objects.IN.InventoryItem inventoryItem = PX.Objects.IN.InventoryItem.PK.Find((PXGraph) this, inventoryID);
      INLotSerClass inLotSerClass1 = INLotSerClass.PK.Find((PXGraph) this, inventoryItem.LotSerClassID);
      if (inLotSerClass1 == null)
        inLotSerClass1 = new INLotSerClass()
        {
          LotSerTrack = "N",
          LotSerAssign = "R",
          LotSerTrackExpiration = new bool?(false),
          AutoNextNbr = new bool?(true)
        };
      INLotSerClass inLotSerClass2 = inLotSerClass1;
      return (inventoryItem, inLotSerClass2);
    }));
    PXSelectBase<SOShipmentProcessedByUser> pxSelectBase = (PXSelectBase<SOShipmentProcessedByUser>) new FbqlSelect<SelectFromBase<SOShipmentProcessedByUser, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<Users>.On<SOShipmentProcessedByUser.FK.User>>, FbqlJoins.Left<SOShipment>.On<SOShipmentProcessedByUser.FK.Shipment>>, FbqlJoins.Left<SOPickingWorksheet>.On<SOShipmentProcessedByUser.FK.Worksheet>>, FbqlJoins.Left<PX.Objects.IN.INSite>.On<KeysRelation<Field<SOShipment.siteID>.IsRelatedTo<PX.Objects.IN.INSite.siteID>.AsSimpleKey.WithTablesOf<PX.Objects.IN.INSite, SOShipment>, PX.Objects.IN.INSite, SOShipment>.Or<SOPickingWorksheet.FK.Site>>>>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<SOShipmentProcessedByUser.overallEndDateTime, IsNotNull>>>, And<MatchUserFor<PX.Objects.IN.INSite>>>>.And<BqlOperand<SOShipmentProcessedByUser.overallStartDateTime, IBqlDateTime>.IsGreaterEqual<BqlField<EfficiencyFilter.startDate, IBqlDateTime>.FromCurrent>>>.Order<By<BqlField<SOShipmentProcessedByUser.jobType, IBqlString>.Asc, BqlField<SOShipment.siteID, IBqlInt>.Asc, BqlField<SOShipmentProcessedByUser.shipmentNbr, IBqlString>.Asc, BqlField<SOShipmentProcessedByUser.worksheetNbr, IBqlString>.Asc, BqlField<SOShipmentProcessedByUser.pickerNbr, IBqlInt>.Asc, BqlField<Users.username, IBqlString>.Asc>>, SOShipmentProcessedByUser>.View((PXGraph) this);
    if (current.EndDate.HasValue)
      pxSelectBase.WhereAnd<Where<BqlOperand<SOShipmentProcessedByUser.overallStartDateTime, IBqlDateTime>.IsLessEqual<BqlField<EfficiencyFilter.endDate, IBqlDateTime>.FromCurrent>>>();
    if (current.UserID.HasValue)
      pxSelectBase.WhereAnd<Where<BqlOperand<SOShipmentProcessedByUser.userID, IBqlGuid>.IsEqual<BqlField<EfficiencyFilter.userID, IBqlGuid>.FromCurrent>>>();
    if (current.DocType == "SHPT" && current.ShipmentNbr != null)
      pxSelectBase.WhereAnd<Where<BqlOperand<SOShipmentProcessedByUser.shipmentNbr, IBqlString>.IsEqual<BqlField<EfficiencyFilter.shipmentNbr, IBqlString>.FromCurrent>>>();
    if (current.DocType == "PLST" && current.WorksheetNbr != null)
      pxSelectBase.WhereAnd<Where<BqlOperand<SOShipmentProcessedByUser.worksheetNbr, IBqlString>.IsEqual<BqlField<EfficiencyFilter.worksheetNbr, IBqlString>.FromCurrent>>>();
    if (current.SiteID.HasValue)
      pxSelectBase.WhereAnd<Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<SOShipment.siteID, Equal<BqlField<EfficiencyFilter.siteID, IBqlInt>.FromCurrent>>>>>.Or<BqlOperand<SOPickingWorksheet.siteID, IBqlInt>.IsEqual<BqlField<EfficiencyFilter.siteID, IBqlInt>.FromCurrent>>>>();
    if (!PXAccess.FeatureInstalled<FeaturesSet.wMSAdvancedPicking>())
      pxSelectBase.WhereAnd<Where<BqlOperand<SOShipmentProcessedByUser.worksheetNbr, IBqlString>.IsNull>>();
    List<PickingEfficiency> pickingEfficiencyList = new List<PickingEfficiency>();
    PickingEfficiency pickingEfficiency1 = (PickingEfficiency) null;
    HashSet<int?> nullableSet1 = new HashSet<int?>();
    HashSet<int?> nullableSet2 = new HashSet<int?>();
    foreach (PXResult<SOShipmentProcessedByUser, Users, SOShipment, SOPickingWorksheet, PX.Objects.IN.INSite> pxResult in pxSelectBase.Select(Array.Empty<object>()))
    {
      SOShipmentProcessedByUser shipmentProcessedByUser1;
      Users users;
      SOShipment soShipment1;
      SOPickingWorksheet pickingWorksheet1;
      PX.Objects.IN.INSite inSite;
      pxResult.Deconstruct(ref shipmentProcessedByUser1, ref users, ref soShipment1, ref pickingWorksheet1, ref inSite);
      SOShipmentProcessedByUser shipmentProcessedByUser2 = shipmentProcessedByUser1;
      SOShipment soShipment2 = soShipment1;
      SOPickingWorksheet pickingWorksheet2 = pickingWorksheet1;
      bool flag1 = false;
      int? nullable1;
      int? nullable2;
      int? nullable3;
      if (pickingEfficiency1 != null && !(pickingEfficiency1.JobType != shipmentProcessedByUser2.JobType))
      {
        nullable1 = pickingEfficiency1.SiteID;
        nullable2 = (int?) soShipment2?.SiteID;
        nullable3 = (int?) (nullable2 ?? pickingWorksheet2?.SiteID);
        if (nullable1.GetValueOrDefault() == nullable3.GetValueOrDefault() & nullable1.HasValue == nullable3.HasValue)
        {
          if (current.ExpandByUser.GetValueOrDefault())
          {
            Guid? userId1 = pickingEfficiency1.UserID;
            Guid? userId2 = shipmentProcessedByUser2.UserID;
            if ((userId1.HasValue == userId2.HasValue ? (userId1.HasValue ? (userId1.GetValueOrDefault() != userId2.GetValueOrDefault() ? 1 : 0) : 0) : 1) != 0)
              goto label_19;
          }
          if ((!current.ExpandByShipment.GetValueOrDefault() || !(pickingEfficiency1.PickListNbr != shipmentProcessedByUser2.PickListNbr)) && (!current.ExpandByDay.GetValueOrDefault() || !(pickingEfficiency1.StartDate.Value.Date != shipmentProcessedByUser2.StartDateTime.Value.Date)))
            goto label_28;
        }
      }
label_19:
      PickingEfficiency pickingEfficiency2 = new PickingEfficiency();
      pickingEfficiency2.JobType = shipmentProcessedByUser2.JobType;
      pickingEfficiency2.DocType = shipmentProcessedByUser2.DocType;
      pickingEfficiency2.ShipmentNbr = shipmentProcessedByUser2.ShipmentNbr;
      pickingEfficiency2.WorksheetNbr = shipmentProcessedByUser2.WorksheetNbr;
      pickingEfficiency2.PickerNbr = shipmentProcessedByUser2.PickerNbr;
      pickingEfficiency2.PickListNbr = shipmentProcessedByUser2.PickListNbr;
      pickingEfficiency2.UserID = shipmentProcessedByUser2.UserID;
      int? nullable4;
      if (soShipment2 == null)
      {
        nullable1 = new int?();
        nullable4 = nullable1;
      }
      else
        nullable4 = soShipment2.SiteID;
      nullable3 = nullable4;
      int? nullable5;
      if (!nullable3.HasValue)
      {
        if (pickingWorksheet2 == null)
        {
          nullable1 = new int?();
          nullable5 = nullable1;
        }
        else
          nullable5 = pickingWorksheet2.SiteID;
      }
      else
        nullable5 = nullable3;
      pickingEfficiency2.SiteID = nullable5;
      pickingEfficiency2.OverallStartDate = new DateTime?(shipmentProcessedByUser2.OverallStartDateTime.Value);
      DateTime? nullable6 = shipmentProcessedByUser2.StartDateTime;
      pickingEfficiency2.StartDate = new DateTime?(nullable6.Value);
      nullable6 = shipmentProcessedByUser2.EndDateTime;
      pickingEfficiency2.EndDate = new DateTime?(nullable6.Value);
      nullable6 = shipmentProcessedByUser2.OverallEndDateTime;
      pickingEfficiency2.OverallEndDate = new DateTime?(nullable6.Value);
      pickingEfficiency2.NumberOfShipments = new int?(0);
      pickingEfficiency2.NumberOfLines = new int?(0);
      pickingEfficiency2.NumberOfInventories = new int?(0);
      pickingEfficiency2.NumberOfPackages = new int?(0);
      pickingEfficiency2.TotalSeconds = new Decimal?(0M);
      pickingEfficiency2.EffectiveSeconds = new Decimal?(0M);
      pickingEfficiency2.TotalQty = new Decimal?(0M);
      pickingEfficiency2.NumberOfUsefulOperations = new int?(0);
      pickingEfficiency2.NumberOfScans = new int?(0);
      pickingEfficiency2.NumberOfFailedScans = new int?(0);
      pickingEfficiency1 = pickingEfficiency2;
      nullableSet1.Clear();
      nullableSet2.Clear();
      flag1 = true;
label_28:
      PickingEfficiency pickingEfficiency3 = pickingEfficiency1;
      Decimal? nullable7 = pickingEfficiency3.EffectiveSeconds;
      nullable6 = shipmentProcessedByUser2.EndDateTime;
      DateTime? startDateTime = shipmentProcessedByUser2.StartDateTime;
      TimeSpan? nullable8 = nullable6.HasValue & startDateTime.HasValue ? new TimeSpan?(nullable6.GetValueOrDefault() - startDateTime.GetValueOrDefault()) : new TimeSpan?();
      TimeSpan timeSpan = nullable8.Value;
      Decimal totalSeconds = (Decimal) timeSpan.TotalSeconds;
      pickingEfficiency3.EffectiveSeconds = nullable7.HasValue ? new Decimal?(nullable7.GetValueOrDefault() + totalSeconds) : new Decimal?();
      PickingEfficiency pickingEfficiency4 = pickingEfficiency1;
      nullable6 = pickingEfficiency1.StartDate;
      DateTime first1 = nullable6.Value;
      nullable6 = shipmentProcessedByUser2.StartDateTime;
      DateTime second1 = nullable6.Value;
      DateTime? nullable9 = new DateTime?(Tools.Min<DateTime>(first1, second1));
      pickingEfficiency4.StartDate = nullable9;
      PickingEfficiency pickingEfficiency5 = pickingEfficiency1;
      nullable6 = pickingEfficiency1.EndDate;
      DateTime first2 = nullable6.Value;
      nullable6 = shipmentProcessedByUser2.EndDateTime;
      DateTime second2 = nullable6.Value;
      DateTime? nullable10 = new DateTime?(Tools.Max<DateTime>(first2, second2));
      pickingEfficiency5.EndDate = nullable10;
      PickingEfficiency pickingEfficiency6 = pickingEfficiency1;
      nullable6 = pickingEfficiency1.OverallStartDate;
      DateTime first3 = nullable6.Value;
      nullable6 = shipmentProcessedByUser2.OverallStartDateTime;
      DateTime second3 = nullable6.Value;
      DateTime? nullable11 = new DateTime?(Tools.Min<DateTime>(first3, second3));
      pickingEfficiency6.OverallStartDate = nullable11;
      PickingEfficiency pickingEfficiency7 = pickingEfficiency1;
      nullable6 = pickingEfficiency1.OverallEndDate;
      DateTime first4 = nullable6.Value;
      nullable6 = shipmentProcessedByUser2.OverallEndDateTime;
      DateTime second4 = nullable6.Value;
      DateTime? nullable12 = new DateTime?(Tools.Max<DateTime>(first4, second4));
      pickingEfficiency7.OverallEndDate = nullable12;
      PickingEfficiency pickingEfficiency8 = pickingEfficiency1;
      nullable6 = pickingEfficiency1.OverallEndDate;
      DateTime? overallStartDate = pickingEfficiency1.OverallStartDate;
      TimeSpan? nullable13;
      if (!(nullable6.HasValue & overallStartDate.HasValue))
      {
        nullable8 = new TimeSpan?();
        nullable13 = nullable8;
      }
      else
        nullable13 = new TimeSpan?(nullable6.GetValueOrDefault() - overallStartDate.GetValueOrDefault());
      nullable8 = nullable13;
      timeSpan = nullable8.Value;
      Decimal? nullable14 = new Decimal?((Decimal) timeSpan.TotalSeconds);
      pickingEfficiency8.TotalSeconds = nullable14;
      if (flag1 || pickingEfficiency1.PickListNbr != shipmentProcessedByUser2.PickListNbr)
      {
        pickingEfficiency1.DocType = shipmentProcessedByUser2.DocType;
        pickingEfficiency1.PickListNbr = shipmentProcessedByUser2.PickListNbr;
        pickingEfficiency1.ShipmentNbr = shipmentProcessedByUser2.ShipmentNbr;
        pickingEfficiency1.WorksheetNbr = shipmentProcessedByUser2.WorksheetNbr;
        pickingEfficiency1.PickerNbr = shipmentProcessedByUser2.PickerNbr;
        SOPackageDetailEx[] source1;
        if (!(shipmentProcessedByUser2.DocType == "SHPT"))
          source1 = Array.Empty<SOPackageDetailEx>();
        else
          source1 = GraphHelper.RowCast<SOPackageDetailEx>((IEnumerable) PXSelectBase<SOPackageDetailEx, PXViewOf<SOPackageDetailEx>.BasedOn<SelectFromBase<SOPackageDetailEx, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<SOPackageDetailEx.shipmentNbr, IBqlString>.IsEqual<P.AsString>>>.Config>.Select((PXGraph) this, new object[1]
          {
            (object) shipmentProcessedByUser2.ShipmentNbr
          })).ToArray<SOPackageDetailEx>();
        int length = source1.Length;
        int num1 = ((IEnumerable<SOPackageDetailEx>) source1).Count<SOPackageDetailEx>((Func<SOPackageDetailEx, bool>) (p => GetBox(p.BoxID).AllowOverrideDimension.GetValueOrDefault()));
        (int?, int?, int?, string, Decimal?)[] array;
        if (!(shipmentProcessedByUser2.DocType == "SHPT"))
          array = GraphHelper.RowCast<SOPickerListEntry>((IEnumerable) PXSelectBase<SOPickerListEntry, PXViewOf<SOPickerListEntry>.BasedOn<SelectFromBase<SOPickerListEntry, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<SOPickerListEntry.worksheetNbr, Equal<P.AsString>>>>>.And<BqlOperand<SOPickerListEntry.pickerNbr, IBqlInt>.IsEqual<P.AsInt>>>>.Config>.Select((PXGraph) this, new object[2]
          {
            (object) shipmentProcessedByUser2.WorksheetNbr,
            (object) shipmentProcessedByUser2.PickerNbr
          })).Select<SOPickerListEntry, (int?, int?, int?, string, Decimal?)>((Func<SOPickerListEntry, (int?, int?, int?, string, Decimal?)>) (entry => (entry.EntryNbr, entry.LocationID, entry.InventoryID, entry.LotSerialNbr, entry.BaseQty))).ToArray<(int?, int?, int?, string, Decimal?)>();
        else
          array = GraphHelper.RowCast<SOShipLineSplit>((IEnumerable) PXSelectBase<SOShipLineSplit, PXViewOf<SOShipLineSplit>.BasedOn<SelectFromBase<SOShipLineSplit, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<SOShipLineSplit.shipmentNbr, IBqlString>.IsEqual<P.AsString>>>.Config>.Select((PXGraph) this, new object[1]
          {
            (object) shipmentProcessedByUser2.ShipmentNbr
          })).Select<SOShipLineSplit, (int?, int?, int?, string, Decimal?)>((Func<SOShipLineSplit, (int?, int?, int?, string, Decimal?)>) (split => (split.LineNbr, split.LocationID, split.InventoryID, split.LotSerialNbr, split.BaseQty))).ToArray<(int?, int?, int?, string, Decimal?)>();
        (int?, int?, int?, string, Decimal?)[] source2 = array;
        Dictionary<int?, (Decimal?, int)> dictionary = ((IEnumerable<(int?, int?, int?, string, Decimal?)>) source2).Where<(int?, int?, int?, string, Decimal?)>((Func<(int?, int?, int?, string, Decimal?), bool>) (s => s.InventoryID.HasValue)).GroupBy<(int?, int?, int?, string, Decimal?), int?>((Func<(int?, int?, int?, string, Decimal?), int?>) (s => s.InventoryID)).ToDictionary<IGrouping<int?, (int?, int?, int?, string, Decimal?)>, int?, (Decimal?, int)>((Func<IGrouping<int?, (int?, int?, int?, string, Decimal?)>, int?>) (g => g.Key), (Func<IGrouping<int?, (int?, int?, int?, string, Decimal?)>, (Decimal?, int)>) (g => (g.Sum<(int?, int?, int?, string, Decimal?)>((Func<(int?, int?, int?, string, Decimal?), Decimal?>) (s => s.BaseQty)), EnumerableExtensions.Distinct<(int?, int?, int?, string, Decimal?), string>(g.Where<(int?, int?, int?, string, Decimal?)>((Func<(int?, int?, int?, string, Decimal?), bool>) (s => !string.IsNullOrWhiteSpace(s.LotSerialNbr))), (Func<(int?, int?, int?, string, Decimal?), string>) (s => s.LotSerialNbr)).Count<(int?, int?, int?, string, Decimal?)>())));
        EnumerableExtensions.AddRange<int?>((ISet<int?>) nullableSet1, (IEnumerable<int?>) dictionary.Keys);
        pickingEfficiency1.NumberOfInventories = new int?(nullableSet1.Count);
        HashSet<int?> hashSet = ((IEnumerable<(int?, int?, int?, string, Decimal?)>) source2).Where<(int?, int?, int?, string, Decimal?)>((Func<(int?, int?, int?, string, Decimal?), bool>) (s => s.LocationID.HasValue)).Select<(int?, int?, int?, string, Decimal?), int?>((Func<(int?, int?, int?, string, Decimal?), int?>) (s => s.LocationID)).ToHashSet<int?>();
        EnumerableExtensions.AddRange<int?>((ISet<int?>) nullableSet2, (IEnumerable<int?>) hashSet);
        pickingEfficiency1.NumberOfLocations = new int?(nullableSet2.Count);
        PickingEfficiency pickingEfficiency9 = pickingEfficiency1;
        nullable3 = pickingEfficiency9.NumberOfShipments;
        nullable1 = nullable3;
        int? nullable15;
        if (!nullable1.HasValue)
        {
          nullable2 = new int?();
          nullable15 = nullable2;
        }
        else
          nullable15 = new int?(nullable1.GetValueOrDefault() + 1);
        pickingEfficiency9.NumberOfShipments = nullable15;
        PickingEfficiency pickingEfficiency10 = pickingEfficiency1;
        nullable3 = pickingEfficiency10.NumberOfLines;
        int num2 = EnumerableExtensions.Distinct<(int?, int?, int?, string, Decimal?), int?>((IEnumerable<(int?, int?, int?, string, Decimal?)>) source2, (Func<(int?, int?, int?, string, Decimal?), int?>) (s => s.LineNbr)).Count<(int?, int?, int?, string, Decimal?)>();
        int? nullable16;
        if (!nullable3.HasValue)
        {
          nullable1 = new int?();
          nullable16 = nullable1;
        }
        else
          nullable16 = new int?(nullable3.GetValueOrDefault() + num2);
        pickingEfficiency10.NumberOfLines = nullable16;
        PickingEfficiency pickingEfficiency11 = pickingEfficiency1;
        nullable3 = pickingEfficiency11.NumberOfPackages;
        int num3 = length;
        int? nullable17;
        if (!nullable3.HasValue)
        {
          nullable1 = new int?();
          nullable17 = nullable1;
        }
        else
          nullable17 = new int?(nullable3.GetValueOrDefault() + num3);
        pickingEfficiency11.NumberOfPackages = nullable17;
        PickingEfficiency pickingEfficiency12 = pickingEfficiency1;
        nullable3 = pickingEfficiency12.NumberOfScans;
        nullable1 = shipmentProcessedByUser2.NumberOfScans;
        int? nullable18;
        if (!(nullable3.HasValue & nullable1.HasValue))
        {
          nullable2 = new int?();
          nullable18 = nullable2;
        }
        else
          nullable18 = new int?(nullable3.GetValueOrDefault() + nullable1.GetValueOrDefault());
        pickingEfficiency12.NumberOfScans = nullable18;
        PickingEfficiency pickingEfficiency13 = pickingEfficiency1;
        nullable1 = pickingEfficiency13.NumberOfFailedScans;
        nullable3 = shipmentProcessedByUser2.NumberOfFailedScans;
        int? nullable19;
        if (!(nullable1.HasValue & nullable3.HasValue))
        {
          nullable2 = new int?();
          nullable19 = nullable2;
        }
        else
          nullable19 = new int?(nullable1.GetValueOrDefault() + nullable3.GetValueOrDefault());
        pickingEfficiency13.NumberOfFailedScans = nullable19;
        PickingEfficiency pickingEfficiency14 = pickingEfficiency1;
        nullable7 = pickingEfficiency14.TotalQty;
        Decimal num4 = ((IEnumerable<(int?, int?, int?, string, Decimal?)>) source2).Sum<(int?, int?, int?, string, Decimal?)>((Func<(int?, int?, int?, string, Decimal?), Decimal>) (line => line.BaseQty.GetValueOrDefault()));
        pickingEfficiency14.TotalQty = nullable7.HasValue ? new Decimal?(nullable7.GetValueOrDefault() + num4) : new Decimal?();
        SOPickPackShipUserSetup packShipUserSetup = func1(pickingEfficiency1.UserID);
        bool? nullable20 = ppsSetup.UseCartsForPick;
        int num5 = !nullable20.GetValueOrDefault() ? 0 : (EnumerableExtensions.IsIn<string>(pickingEfficiency1.JobType, "PICK", "PPCK") ? 1 : 0);
        bool flag2 = EnumerableExtensions.IsIn<string>(pickingEfficiency1.JobType, "PACK", "PPCK");
        nullable20 = packShipUserSetup.DefaultLocationFromShipment;
        bool flag3 = false;
        bool flag4 = nullable20.GetValueOrDefault() == flag3 & nullable20.HasValue && EnumerableExtensions.IsIn<string>(pickingEfficiency1.JobType, "PICK", "PPCK");
        bool flag5 = EnumerableExtensions.IsIn<string>(pickingEfficiency1.JobType, "PICK", "PPCK");
        bool flag6 = pickingEfficiency1.JobType == "PACK";
        int num6 = 0;
        int num7 = 1;
        int num8 = 1;
        int num9 = (uint) num5 > 0U ? 1 : 0;
        int num10 = num6 + (num7 + num9 + num8);
        if (flag2)
        {
          int num11 = 1;
          int num12 = 1;
          nullable20 = ppsSetup.ConfirmEachPackageWeight;
          int num13 = nullable20.GetValueOrDefault() ? 1 : 0;
          nullable20 = ppsSetup.ConfirmEachPackageDimensions;
          int num14 = nullable20.GetValueOrDefault() ? 1 : 0;
          num10 += length * (num11 + num12 + num13) + num1 * num14;
        }
        if (flag4)
        {
          nullable20 = ppsSetup.RequestLocationForEachItem;
          bool flag7 = false;
          if (nullable20.GetValueOrDefault() == flag7 & nullable20.HasValue)
          {
            int num15 = 1;
            num10 += hashSet.Count * num15;
          }
        }
        foreach (KeyValuePair<int?, (Decimal?, int)> keyValuePair in dictionary)
        {
          INLotSerClass inLotSerClass = func2(keyValuePair.Key).Item2;
          int num16;
          if (inLotSerClass.LotSerTrack != "N")
          {
            if (flag5)
            {
              nullable20 = packShipUserSetup.DefaultLotSerialFromShipment;
              bool flag8 = false;
              if (nullable20.GetValueOrDefault() == flag8 & nullable20.HasValue || inLotSerClass.LotSerAssign == "U")
              {
                num16 = 1;
                goto label_68;
              }
            }
            if (flag6)
            {
              nullable20 = packShipUserSetup.DefaultLotSerialFromShipment;
              bool flag9 = false;
              num16 = nullable20.GetValueOrDefault() == flag9 & nullable20.HasValue ? 1 : 0;
            }
            else
              num16 = 0;
          }
          else
            num16 = 0;
label_68:
          bool flag10 = num16 != 0;
          int val1 = 1;
          int val2 = flag10 ? keyValuePair.Value.Item2 : 0;
          int num17 = flag10 & flag5 ? keyValuePair.Value.Item2 : 0;
          int num18;
          if (flag4)
          {
            nullable20 = ppsSetup.RequestLocationForEachItem;
            if (nullable20.GetValueOrDefault())
            {
              num18 = Math.Max(val1, val2);
              goto label_72;
            }
          }
          num18 = 0;
label_72:
          int num19 = num18;
          int num20 = inLotSerClass.LotSerTrack == "S" ? 0 : (flag10 ? keyValuePair.Value.Item2 : 1);
          nullable20 = ppsSetup.ExplicitLineConfirmation;
          int num21 = nullable20.GetValueOrDefault() ? Math.Max(val1, val2) : 0;
          num10 += num19 + val1 + val2 + num17 + num20 + num21;
        }
        PickingEfficiency pickingEfficiency15 = pickingEfficiency1;
        int? usefulOperations = pickingEfficiency15.NumberOfUsefulOperations;
        int num22 = num10;
        pickingEfficiency15.NumberOfUsefulOperations = usefulOperations.HasValue ? new int?(usefulOperations.GetValueOrDefault() + num22) : new int?();
      }
      if (flag1)
        pickingEfficiencyList.Add(pickingEfficiency1);
    }
    return (IEnumerable) pickingEfficiencyList;
  }

  [PXButton]
  [PXUIField(Visible = false)]
  protected IEnumerable ViewDocument(PXAdapter adapter)
  {
    if (((PXSelectBase<PickingEfficiency>) this.Efficiency).Current != null)
    {
      if (((PXSelectBase<PickingEfficiency>) this.Efficiency).Current.DocType == "SHPT")
      {
        SOShipmentEntry instance = PXGraph.CreateInstance<SOShipmentEntry>();
        ((PXSelectBase<SOShipment>) instance.Document).Current = PXResultset<SOShipment>.op_Implicit(((PXSelectBase<SOShipment>) instance.Document).Search<SOShipment.shipmentNbr>((object) ((PXSelectBase<PickingEfficiency>) this.Efficiency).Current.ShipmentNbr, Array.Empty<object>()));
        PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, string.Empty);
        ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 3;
        throw requiredException;
      }
      if (((PXSelectBase<PickingEfficiency>) this.Efficiency).Current.DocType == "PLST")
      {
        SOPickingWorksheetReview instance = PXGraph.CreateInstance<SOPickingWorksheetReview>();
        ((PXSelectBase<SOPickingWorksheet>) instance.worksheet).Current = PXResultset<SOPickingWorksheet>.op_Implicit(((PXSelectBase<SOPickingWorksheet>) instance.worksheet).Search<SOPickingWorksheet.worksheetNbr>((object) ((PXSelectBase<PickingEfficiency>) this.Efficiency).Current.WorksheetNbr, Array.Empty<object>()));
        PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, string.Empty);
        ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 3;
        throw requiredException;
      }
    }
    return adapter.Get();
  }

  protected virtual void _(PX.Data.Events.RowSelected<EfficiencyFilter> e)
  {
    if (e.Row == null)
      return;
    PXCacheEx.Adjust<PXUIFieldAttribute>(((PXSelectBase) this.Efficiency).Cache, (object) null).For<PickingEfficiency.day>((Action<PXUIFieldAttribute>) (a => a.Visible = e.Row.ExpandByDay.GetValueOrDefault())).For<PickingEfficiency.startDate>((Action<PXUIFieldAttribute>) (a =>
    {
      PXUIFieldAttribute pxuiFieldAttribute = a;
      bool? expandByDay = e.Row.ExpandByDay;
      bool flag = false;
      int num = expandByDay.GetValueOrDefault() == flag & expandByDay.HasValue ? 1 : 0;
      pxuiFieldAttribute.Visible = num != 0;
    })).For<PickingEfficiency.endDate>((Action<PXUIFieldAttribute>) (a =>
    {
      PXUIFieldAttribute pxuiFieldAttribute = a;
      bool? expandByDay = e.Row.ExpandByDay;
      bool flag = false;
      int num = expandByDay.GetValueOrDefault() == flag & expandByDay.HasValue ? 1 : 0;
      pxuiFieldAttribute.Visible = num != 0;
    })).For<PickingEfficiency.userID>((Action<PXUIFieldAttribute>) (a => a.Visible = e.Row.ExpandByUser.GetValueOrDefault())).For<PickingEfficiency.numberOfShipments>((Action<PXUIFieldAttribute>) (a =>
    {
      PXUIFieldAttribute pxuiFieldAttribute = a;
      bool? expandByShipment = e.Row.ExpandByShipment;
      bool flag = false;
      int num = expandByShipment.GetValueOrDefault() == flag & expandByShipment.HasValue ? 1 : 0;
      pxuiFieldAttribute.Visible = num != 0;
    })).For<PickingEfficiency.pickListNbr>((Action<PXUIFieldAttribute>) (a => a.Visible = e.Row.ExpandByShipment.GetValueOrDefault())).For<PickingEfficiency.siteID>((Action<PXUIFieldAttribute>) (a => a.Visible = !e.Row.SiteID.HasValue));
  }
}
