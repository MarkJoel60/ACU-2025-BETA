// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.RelatedItems.GraphExtensions.PossibleRelatedItemsProcessExt`1
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.ML.CrossSales.DAC;
using PX.Objects.Common.Exceptions;
using PX.Objects.Common.Extensions;
using PX.Objects.CR;
using PX.Objects.EP;
using PX.Objects.IN.RelatedItems.DAC;
using PX.SM;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web.Configuration;

#nullable enable
namespace PX.Objects.IN.RelatedItems.GraphExtensions;

public abstract class PossibleRelatedItemsProcessExt<TGraph> : PXGraphExtension<
#nullable disable
TGraph> where TGraph : PXGraph
{
  [PXCopyPasteHiddenFields(new System.Type[] {})]
  public PXViewOf<MLPossibleRelatedInventory>.BasedOn<SelectFromBase<MLPossibleRelatedInventory, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  MLPossibleRelatedInventory.status, 
  #nullable disable
  Equal<MLPossibleRelatedInventory.status.@new>>>>>.And<BqlOperand<
  #nullable enable
  MLPossibleRelatedInventory.processingID, IBqlInt>.IsEqual<
  #nullable disable
  P.AsInt>>>>.ReadOnly PossibleRelatedItems;
  [PXCopyPasteHiddenFields(new System.Type[] {})]
  public FbqlSelect<SelectFromBase<INRelatedInventory, TypeArrayOf<IFbqlJoin>.Empty>, INRelatedInventory>.View Suggestions;
  public PXAction<PX.Objects.IN.InventoryItem> uploadPossibleRelatedItems;

  public virtual void Initialize()
  {
    ((PXGraphExtension) this).Initialize();
    if (!this.ShowUploadPossibleRelatedItemsButton())
      return;
    ((PXAction) this.uploadPossibleRelatedItems).SetVisible(true);
  }

  private bool ShowUploadPossibleRelatedItemsButton()
  {
    return WebConfigurationManager.AppSettings[nameof (ShowUploadPossibleRelatedItemsButton)] == "1";
  }

  [PXButton]
  [PXUIField]
  protected virtual IEnumerable UploadPossibleRelatedItems(PXAdapter adapter)
  {
    int result;
    if (int.TryParse(adapter.CommandArguments, out result))
    {
      PossibleRelatedItemsProcessExt<TGraph>.Start((PXGraph) this.Base, result);
    }
    else
    {
      if (!this.ShowUploadPossibleRelatedItemsButton())
        throw new ArgumentException();
      PossibleRelatedItemsProcessExt<TGraph>.Start((PXGraph) this.Base, 1);
    }
    return adapter.Get();
  }

  public static void Start(PXGraph graph, int uploadID)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: method pointer
    PXLongOperation.StartOperation(graph, new PXToggleAsyncDelegate((object) new PossibleRelatedItemsProcessExt<TGraph>.\u003C\u003Ec__DisplayClass6_0()
    {
      uploadID = uploadID
    }, __methodptr(\u003CStart\u003Eb__0)));
  }

  protected virtual void Start(int uploadID)
  {
    MLCrossSalesProcessing crossSalesProcessing = MLCrossSalesProcessing.PK.Find((PXGraph) this.Base, new int?(uploadID));
    if (crossSalesProcessing == null)
      throw new RowNotFoundException((PXCache) GraphHelper.Caches<MLCrossSalesProcessing>((PXGraph) this.Base), new object[1]
      {
        (object) uploadID
      });
    this.DeleteProcessed(crossSalesProcessing, uploadID);
    this.ProcessPossibleRelatedItems(crossSalesProcessing, uploadID);
    this.TruncateSuggestions(crossSalesProcessing, uploadID);
    this.SendNotification(crossSalesProcessing);
  }

  protected virtual void DeleteProcessed(MLCrossSalesProcessing crossSellSetup, int uploadID)
  {
    if (PXResultset<INSetup>.op_Implicit(((PXSelectBase<INSetup>) new PXSetup<INSetup>((PXGraph) this.Base)).Select(Array.Empty<object>())).LeaveOldCrossSellSuggestion.GetValueOrDefault())
      return;
    PXDatabase.Delete<MLPossibleRelatedInventory>(new PXDataFieldRestrict[2]
    {
      (PXDataFieldRestrict) new PXDataFieldRestrict<MLPossibleRelatedInventory.status>((object) "P"),
      (PXDataFieldRestrict) new PXDataFieldRestrict<MLPossibleRelatedInventory.processingID>((PXDbType) 8, new int?(4), (object) uploadID, (PXComp) 1)
    });
  }

  protected virtual void ProcessPossibleRelatedItems(
    MLCrossSalesProcessing crossSellSetup,
    int uploadID)
  {
    PXResultset<MLPossibleRelatedInventory> pxResultset;
    do
    {
      pxResultset = ((PXSelectBase<MLPossibleRelatedInventory>) this.PossibleRelatedItems).SelectWindowed(0, 1000, new object[1]
      {
        (object) uploadID
      });
      foreach (PXResult<MLPossibleRelatedInventory> pxResult in pxResultset)
      {
        MLPossibleRelatedInventory possibleRelatedItem = PXResult<MLPossibleRelatedInventory>.op_Implicit(pxResult);
        this.ProcessPossibleRelatedItem(crossSellSetup, possibleRelatedItem);
      }
      this.Base.Actions.PressSave();
    }
    while (pxResultset.Count >= 1000);
  }

  protected virtual void ProcessPossibleRelatedItem(
    MLCrossSalesProcessing crossSellSetup,
    MLPossibleRelatedInventory possibleRelatedItem)
  {
    MLPossibleRelatedInventory copy = PXCache<MLPossibleRelatedInventory>.CreateCopy(possibleRelatedItem);
    PX.Objects.IN.InventoryItem inventoryItem1 = PX.Objects.IN.InventoryItem.PK.Find((PXGraph) this.Base, new int?(possibleRelatedItem.InventoryID));
    if (inventoryItem1 == null)
      throw new RowNotFoundException((PXCache) GraphHelper.Caches<PX.Objects.IN.InventoryItem>((PXGraph) this.Base), new object[1]
      {
        (object) possibleRelatedItem.InventoryID
      });
    PX.Objects.IN.InventoryItem inventoryItem2 = PX.Objects.IN.InventoryItem.PK.Find((PXGraph) this.Base, new int?(possibleRelatedItem.RelatedInventoryID));
    if (inventoryItem2 == null)
      throw new RowNotFoundException((PXCache) GraphHelper.Caches<PX.Objects.IN.InventoryItem>((PXGraph) this.Base), new object[1]
      {
        (object) possibleRelatedItem.RelatedInventoryID
      });
    bool? isTemplate = inventoryItem1.IsTemplate;
    int? inventoryId;
    int[] numArray1;
    if (!isTemplate.GetValueOrDefault())
    {
      inventoryId = inventoryItem1.InventoryID;
      numArray1 = inventoryId.Value.SingleToArray<int>();
    }
    else
      numArray1 = this.GetItemIDsByTemplate(inventoryItem1.InventoryID);
    isTemplate = inventoryItem2.IsTemplate;
    int[] numArray2;
    if (!isTemplate.GetValueOrDefault())
    {
      inventoryId = inventoryItem2.InventoryID;
      numArray2 = inventoryId.Value.SingleToArray<int>();
    }
    else
      numArray2 = this.GetItemIDsByTemplate(inventoryItem2.InventoryID);
    int[] numArray3 = numArray2;
    foreach (int num1 in numArray1)
    {
      foreach (int num2 in numArray3)
      {
        copy.InventoryID = num1;
        copy.RelatedInventoryID = num2;
        if (!this.ValidateItem(crossSellSetup, copy))
          this.DeleteRelatedInventory(copy);
        else
          this.UpdateRelatedInventory(crossSellSetup, copy);
      }
    }
    this.MarkPossibleRelatedItemProcessed(possibleRelatedItem);
  }

  protected virtual int[] GetItemIDsByTemplate(int? templateID)
  {
    return GraphHelper.RowCast<PX.Objects.IN.InventoryItem>((IEnumerable) PXSelectBase<PX.Objects.IN.InventoryItem, PXViewOf<PX.Objects.IN.InventoryItem>.BasedOn<SelectFromBase<PX.Objects.IN.InventoryItem, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<PX.Objects.IN.InventoryItem.templateItemID, IBqlInt>.IsEqual<P.AsInt>>>.Config>.Select((PXGraph) this.Base, new object[1]
    {
      (object) templateID
    })).Select<PX.Objects.IN.InventoryItem, int>((Func<PX.Objects.IN.InventoryItem, int>) (r => r.InventoryID.Value)).ToArray<int>();
  }

  protected virtual bool ValidateItem(
    MLCrossSalesProcessing crossSellSetup,
    MLPossibleRelatedInventory possibleRelatedItem)
  {
    Decimal score = possibleRelatedItem.Score;
    Decimal? minRelevanceScore = crossSellSetup.MinRelevanceScore;
    Decimal valueOrDefault = minRelevanceScore.GetValueOrDefault();
    if (score < valueOrDefault & minRelevanceScore.HasValue)
      return false;
    PX.Objects.IN.InventoryItem inventoryItem1 = PX.Objects.IN.InventoryItem.PK.Find((PXGraph) this.Base, new int?(possibleRelatedItem.InventoryID));
    if (inventoryItem1 == null)
      throw new RowNotFoundException((PXCache) GraphHelper.Caches<PX.Objects.IN.InventoryItem>((PXGraph) this.Base), new object[1]
      {
        (object) possibleRelatedItem.InventoryID
      });
    PX.Objects.IN.InventoryItem inventoryItem2 = PX.Objects.IN.InventoryItem.PK.Find((PXGraph) this.Base, new int?(possibleRelatedItem.RelatedInventoryID));
    if (inventoryItem2 == null)
      throw new RowNotFoundException((PXCache) GraphHelper.Caches<PX.Objects.IN.InventoryItem>((PXGraph) this.Base), new object[1]
      {
        (object) possibleRelatedItem.RelatedInventoryID
      });
    string itemStatus1 = inventoryItem1.ItemStatus;
    if ((itemStatus1 != null ? (EnumerableExtensions.IsIn<string>(itemStatus1, "IN", "DE") ? 1 : 0) : 0) == 0)
    {
      string itemStatus2 = inventoryItem2.ItemStatus;
      if ((itemStatus2 != null ? (EnumerableExtensions.IsIn<string>(itemStatus2, "IN", "DE") ? 1 : 0) : 0) == 0)
      {
        if (inventoryItem1?.PriceOfSuggestedItems == "L")
        {
          Decimal? basePrice1 = inventoryItem1.BasePrice;
          Decimal? basePrice2 = (Decimal?) inventoryItem2?.BasePrice;
          if (basePrice1.GetValueOrDefault() < basePrice2.GetValueOrDefault() & basePrice1.HasValue & basePrice2.HasValue)
            return false;
        }
        INRelatedInventoryUserFeedback inventoryUserFeedback = PXResultset<INRelatedInventoryUserFeedback>.op_Implicit(PXSelectBase<INRelatedInventoryUserFeedback, PXViewOf<INRelatedInventoryUserFeedback>.BasedOn<SelectFromBase<INRelatedInventoryUserFeedback, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INRelatedInventoryUserFeedback.inventoryID, Equal<P.AsInt>>>>, And<BqlOperand<INRelatedInventoryUserFeedback.relatedInventoryID, IBqlInt>.IsEqual<P.AsInt>>>>.And<BqlOperand<INRelatedInventoryUserFeedback.isCrossSell, IBqlBool>.IsEqual<False>>>>.Config>.Select((PXGraph) this.Base, new object[2]
        {
          (object) inventoryItem1.InventoryID,
          (object) inventoryItem2.InventoryID
        }));
        if (inventoryUserFeedback == null)
          return true;
        bool? isCrossSell = inventoryUserFeedback.IsCrossSell;
        bool flag = false;
        return !(isCrossSell.GetValueOrDefault() == flag & isCrossSell.HasValue);
      }
    }
    return false;
  }

  protected virtual void DeleteRelatedInventory(MLPossibleRelatedInventory possibleRelatedItem)
  {
    foreach (INRelatedInventory relatedInventory in GraphHelper.RowCast<INRelatedInventory>((IEnumerable) PXSelectBase<INRelatedInventory, PXViewOf<INRelatedInventory>.BasedOn<SelectFromBase<INRelatedInventory, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INRelatedInventory.inventoryID, Equal<BqlField<MLPossibleRelatedInventory.inventoryID, IBqlInt>.FromCurrent>>>>, And<BqlOperand<INRelatedInventory.relatedInventoryID, IBqlInt>.IsEqual<BqlField<MLPossibleRelatedInventory.relatedInventoryID, IBqlInt>.FromCurrent>>>, And<BqlOperand<INRelatedInventory.relation, IBqlString>.IsEqual<InventoryRelation.crossSell>>>, And<BqlOperand<INRelatedInventory.createdByPossibleRelatedItem, IBqlBool>.IsEqual<True>>>>.And<BqlOperand<INRelatedInventory.acceptedMLSuggestion, IBqlBool>.IsEqual<False>>>>.Config>.SelectMultiBound((PXGraph) this.Base, new object[1]
    {
      (object) possibleRelatedItem
    }, Array.Empty<object>())))
      ((PXSelectBase<INRelatedInventory>) this.Suggestions).Delete(relatedInventory);
  }

  protected virtual void UpdateRelatedInventory(
    MLCrossSalesProcessing crossSellSetup,
    MLPossibleRelatedInventory possibleRelatedItem)
  {
    INRelatedInventory[] array = GraphHelper.RowCast<INRelatedInventory>((IEnumerable) PXSelectBase<INRelatedInventory, PXViewOf<INRelatedInventory>.BasedOn<SelectFromBase<INRelatedInventory, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INRelatedInventory.inventoryID, Equal<BqlField<MLPossibleRelatedInventory.inventoryID, IBqlInt>.FromCurrent>>>>>.And<BqlOperand<INRelatedInventory.relatedInventoryID, IBqlInt>.IsEqual<BqlField<MLPossibleRelatedInventory.relatedInventoryID, IBqlInt>.FromCurrent>>>>.Config>.SelectMultiBound((PXGraph) this.Base, new object[1]
    {
      (object) possibleRelatedItem
    }, Array.Empty<object>())).ToArray<INRelatedInventory>();
    if (!((IEnumerable<INRelatedInventory>) array).Any<INRelatedInventory>())
    {
      ((PXSelectBase<INRelatedInventory>) this.Suggestions).Insert(new INRelatedInventory()
      {
        InventoryID = new int?(possibleRelatedItem.InventoryID),
        RelatedInventoryID = new int?(possibleRelatedItem.RelatedInventoryID),
        Relation = "CSELL",
        MLScore = new Decimal?(possibleRelatedItem.Score),
        Qty = new Decimal?(1M),
        CreatedByPossibleRelatedItem = new bool?(true),
        IsActive = new bool?(crossSellSetup.AddRelationsAsActive.GetValueOrDefault()),
        Rank = new int?(this.CalculateRank(new Decimal?(possibleRelatedItem.Score)))
      });
    }
    else
    {
      foreach (INRelatedInventory relatedInventory in array)
      {
        bool? nullable = relatedInventory.CreatedByPossibleRelatedItem;
        if (nullable.GetValueOrDefault())
        {
          nullable = relatedInventory.AcceptedMLSuggestion;
          if (!nullable.GetValueOrDefault())
          {
            int rank1 = this.CalculateRank(relatedInventory.MLScore);
            int? rank2 = relatedInventory.Rank;
            int valueOrDefault = rank2.GetValueOrDefault();
            if (rank1 == valueOrDefault & rank2.HasValue)
              relatedInventory.Rank = new int?(this.CalculateRank(new Decimal?(possibleRelatedItem.Score)));
            relatedInventory.MLScore = new Decimal?(possibleRelatedItem.Score);
            ((PXSelectBase<INRelatedInventory>) this.Suggestions).Update(relatedInventory);
          }
        }
      }
    }
  }

  private int CalculateRank(Decimal? mlScore)
  {
    int rank = 1000;
    Decimal? nullable1 = mlScore;
    Decimal num1 = (Decimal) 1;
    if (nullable1.GetValueOrDefault() == num1 & nullable1.HasValue)
      return 1;
    if (!mlScore.HasValue)
      return rank;
    Decimal num2 = (Decimal) rank;
    Decimal? nullable2 = mlScore;
    Decimal num3 = (Decimal) rank;
    Decimal? nullable3 = nullable2.HasValue ? new Decimal?(nullable2.GetValueOrDefault() * num3) : new Decimal?();
    Decimal? nullable4;
    if (!nullable3.HasValue)
    {
      nullable2 = new Decimal?();
      nullable4 = nullable2;
    }
    else
      nullable4 = new Decimal?(num2 - nullable3.GetValueOrDefault());
    nullable2 = nullable4;
    return (int) nullable2.Value;
  }

  protected virtual void MarkPossibleRelatedItemProcessed(
    MLPossibleRelatedInventory possibleRelatedInventory)
  {
    possibleRelatedInventory.Status = "P";
    ((PXSelectBase<MLPossibleRelatedInventory>) this.PossibleRelatedItems).Update(possibleRelatedInventory);
  }

  protected virtual void TruncateSuggestions(MLCrossSalesProcessing crossSellSetup, int uploadID)
  {
    PXResultset<INRelatedInventory> pxResultset = PXSelectBase<INRelatedInventory, PXViewOf<INRelatedInventory>.BasedOn<SelectFromBase<INRelatedInventory, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INRelatedInventory.relation, Equal<InventoryRelation.crossSell>>>>, And<BqlOperand<INRelatedInventory.createdByPossibleRelatedItem, IBqlBool>.IsEqual<True>>>>.And<BqlOperand<INRelatedInventory.acceptedMLSuggestion, IBqlBool>.IsEqual<False>>>.Order<By<BqlField<INRelatedInventory.inventoryID, IBqlInt>.Asc, BqlField<INRelatedInventory.mLScore, IBqlDecimal>.Desc, BqlField<INRelatedInventory.relatedInventoryID, IBqlInt>.Asc>>>.Config>.Select((PXGraph) this.Base, Array.Empty<object>());
    int? nullable1 = new int?();
    int? nullable2 = new int?();
    foreach (PXResult<INRelatedInventory> pxResult in pxResultset)
    {
      INRelatedInventory relatedInventory = PXResult<INRelatedInventory>.op_Implicit(pxResult);
      if (this.GetPossibleRelatedInventory(uploadID, relatedInventory.InventoryID, relatedInventory.RelatedInventoryID) == null)
      {
        ((PXSelectBase<INRelatedInventory>) this.Suggestions).Delete(relatedInventory);
      }
      else
      {
        int? nullable3 = relatedInventory.InventoryID;
        int? nullable4 = nullable1;
        if (!(nullable3.GetValueOrDefault() == nullable4.GetValueOrDefault() & nullable3.HasValue == nullable4.HasValue))
        {
          nullable1 = relatedInventory.InventoryID;
          Decimal? mlScore = relatedInventory.MLScore;
          nullable2 = new int?(1);
        }
        else
        {
          nullable4 = nullable2;
          nullable3 = crossSellSetup.MaxNumberOfSuggestions;
          if (nullable4.GetValueOrDefault() < nullable3.GetValueOrDefault() & nullable4.HasValue & nullable3.HasValue)
          {
            nullable3 = nullable2;
            int? nullable5;
            if (!nullable3.HasValue)
            {
              nullable4 = new int?();
              nullable5 = nullable4;
            }
            else
              nullable5 = new int?(nullable3.GetValueOrDefault() + 1);
            nullable2 = nullable5;
          }
          else
          {
            int? numberOfSuggestions = crossSellSetup.MaxNumberOfSuggestions;
            nullable3 = new int?();
            int? nullable6 = nullable3;
            int? nullable7 = new int?(0);
            if (EnumerableExtensions.IsNotIn<int?>(numberOfSuggestions, nullable6, nullable7))
              ((PXSelectBase<INRelatedInventory>) this.Suggestions).Delete(relatedInventory);
          }
        }
      }
    }
    this.Base.Actions.PressSave();
  }

  private MLPossibleRelatedInventory GetPossibleRelatedInventory(
    int uploadID,
    int? inventoryID,
    int? relatedInventoryID)
  {
    FbqlSelect<SelectFromBase<MLPossibleRelatedInventory, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<MLPossibleRelatedInventory.processingID, Equal<P.AsInt>>>>, And<BqlOperand<MLPossibleRelatedInventory.inventoryID, IBqlInt>.IsEqual<P.AsInt>>>>.And<BqlOperand<MLPossibleRelatedInventory.relatedInventoryID, IBqlInt>.IsEqual<P.AsInt>>>, MLPossibleRelatedInventory>.View view = new FbqlSelect<SelectFromBase<MLPossibleRelatedInventory, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<MLPossibleRelatedInventory.processingID, Equal<P.AsInt>>>>, And<BqlOperand<MLPossibleRelatedInventory.inventoryID, IBqlInt>.IsEqual<P.AsInt>>>>.And<BqlOperand<MLPossibleRelatedInventory.relatedInventoryID, IBqlInt>.IsEqual<P.AsInt>>>, MLPossibleRelatedInventory>.View((PXGraph) this.Base);
    MLPossibleRelatedInventory relatedInventory1 = PXResultset<MLPossibleRelatedInventory>.op_Implicit(((PXSelectBase<MLPossibleRelatedInventory>) view).Select(new object[3]
    {
      (object) uploadID,
      (object) inventoryID,
      (object) relatedInventoryID
    }));
    if (relatedInventory1 != null)
      return relatedInventory1;
    PX.Objects.IN.InventoryItem inventoryItem1 = PX.Objects.IN.InventoryItem.PK.Find((PXGraph) this.Base, inventoryID);
    if (inventoryItem1 == null)
      throw new RowNotFoundException((PXCache) GraphHelper.Caches<PX.Objects.IN.InventoryItem>((PXGraph) this.Base), new object[1]
      {
        (object) inventoryID
      });
    if (inventoryItem1.TemplateItemID.HasValue)
    {
      MLPossibleRelatedInventory relatedInventory2 = PXResultset<MLPossibleRelatedInventory>.op_Implicit(((PXSelectBase<MLPossibleRelatedInventory>) view).Select(new object[3]
      {
        (object) uploadID,
        (object) inventoryItem1.TemplateItemID,
        (object) relatedInventoryID
      }));
      if (relatedInventory2 != null)
        return relatedInventory2;
    }
    PX.Objects.IN.InventoryItem inventoryItem2 = PX.Objects.IN.InventoryItem.PK.Find((PXGraph) this.Base, relatedInventoryID);
    if (inventoryItem2 == null)
      throw new RowNotFoundException((PXCache) GraphHelper.Caches<PX.Objects.IN.InventoryItem>((PXGraph) this.Base), new object[1]
      {
        (object) relatedInventoryID
      });
    if (inventoryItem2.TemplateItemID.HasValue)
    {
      MLPossibleRelatedInventory relatedInventory3 = PXResultset<MLPossibleRelatedInventory>.op_Implicit(((PXSelectBase<MLPossibleRelatedInventory>) view).Select(new object[3]
      {
        (object) uploadID,
        (object) inventoryID,
        (object) inventoryItem2.TemplateItemID
      }));
      if (relatedInventory3 != null)
        return relatedInventory3;
    }
    if (inventoryItem1.TemplateItemID.HasValue && inventoryItem2.TemplateItemID.HasValue)
    {
      MLPossibleRelatedInventory relatedInventory4 = PXResultset<MLPossibleRelatedInventory>.op_Implicit(((PXSelectBase<MLPossibleRelatedInventory>) view).Select(new object[3]
      {
        (object) uploadID,
        (object) inventoryItem1.TemplateItemID,
        (object) inventoryItem2.TemplateItemID
      }));
      if (relatedInventory4 != null)
        return relatedInventory4;
    }
    return (MLPossibleRelatedInventory) null;
  }

  protected virtual void SendNotification(MLCrossSalesProcessing crossSalesProcessing)
  {
    MLCrossSalesSetup mLCrossSalesSetup = PXResultset<MLCrossSalesSetup>.op_Implicit(((PXSelectBase<MLCrossSalesSetup>) new PXSetup<MLCrossSalesSetup>((PXGraph) this.Base)).Select(Array.Empty<object>()));
    if (!mLCrossSalesSetup.GeneratedSuggestionNotification.HasValue || this.CreateEmailNotification(crossSalesProcessing, mLCrossSalesSetup).Send().Any<CRSMEmail>())
      return;
    PXTrace.WriteError("Failed to send the email.");
  }

  protected virtual NotificationGenerator CreateEmailNotification(
    MLCrossSalesProcessing row,
    MLCrossSalesSetup mLCrossSalesSetup)
  {
    Notification notification = Notification.PK.Find((PXGraph) this.Base, mLCrossSalesSetup.GeneratedSuggestionNotification, (PKFindOptions) 0);
    int? nullable = notification != null ? notification.NFrom : throw new RowNotFoundException((PXCache) GraphHelper.Caches<Notification>((PXGraph) this.Base), new object[1]
    {
      (object) mLCrossSalesSetup.GeneratedSuggestionNotification
    });
    if (!nullable.HasValue)
      throw new PXException("The From box is empty in the {0} email template on the Email Templates (SM204003) form.", new object[1]
      {
        (object) notification.Name
      });
    if (notification.NTo == null)
      throw new PXException("The To box is empty in the {0} email template on the Email Templates (SM204003) form.", new object[1]
      {
        (object) notification.Name
      });
    MLCrossSalesSetup copy = (MLCrossSalesSetup) ((PXCache) GraphHelper.Caches<MLCrossSalesSetup>((PXGraph) this.Base)).CreateCopy((object) mLCrossSalesSetup);
    // ISSUE: variable of a boxed type
    __Boxed<TGraph> graph = (object) this.Base;
    MLCrossSalesSetup row1 = copy;
    nullable = notification.NotificationID;
    int notificationId = nullable.Value;
    return (NotificationGenerator) TemplateNotificationGenerator.Create((PXGraph) graph, (object) row1, notificationId);
  }
}
