// Decompiled with JetBrains decompiler
// Type: PX.Objects.CM.CurySymbolAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CS;
using PX.Objects.IN;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.CM;

public class CurySymbolAttribute : PXEventSubscriberAttribute
{
  protected System.Type organizationID;
  protected System.Type branchID;
  protected System.Type curyID;
  protected System.Type curyInfoID;
  protected System.Type siteID;
  protected System.Type bAccountID;
  protected string displayName;
  protected bool useForeignCury;
  protected bool isDependentOnMBC;
  protected string symbol;

  public CurySymbolAttribute(
    System.Type organizationID = null,
    System.Type branchID = null,
    System.Type curyID = null,
    System.Type curyInfoId = null,
    System.Type siteID = null,
    System.Type bAccountID = null,
    string displayName = null,
    bool useForeignCury = true,
    bool isDependentOnMBC = true)
  {
    this.organizationID = organizationID;
    this.branchID = branchID;
    this.curyID = curyID;
    this.curyInfoID = curyInfoId;
    this.siteID = siteID;
    this.bAccountID = bAccountID;
    this.displayName = displayName;
    this.useForeignCury = useForeignCury;
    this.isDependentOnMBC = isDependentOnMBC;
  }

  public virtual void CacheAttached(PXCache sender)
  {
    base.CacheAttached(sender);
    sender.Fields.Insert(((List<string>) sender.Fields).IndexOf(this._FieldName) + 1, this.LabelField);
    PXGraph.FieldSelectingEvents fieldSelecting = sender.Graph.FieldSelecting;
    System.Type itemType = sender.GetItemType();
    string labelField = this.LabelField;
    CurySymbolAttribute curySymbolAttribute = this;
    // ISSUE: virtual method pointer
    PXFieldSelecting pxFieldSelecting = new PXFieldSelecting((object) curySymbolAttribute, __vmethodptr(curySymbolAttribute, labelFieldSelecting));
    fieldSelecting.AddHandler(itemType, labelField, pxFieldSelecting);
  }

  protected virtual string LabelField => this._FieldName + "_Label";

  protected virtual string GetCurrencySymbol(PXCache sender, object row)
  {
    if (this.symbol != null)
      return this.symbol;
    if (this.curyInfoID != (System.Type) null)
    {
      PXCache cach = sender.Graph.Caches[this.curyInfoID.DeclaringType];
      CurrencyInfo currencyInfo = CurrencyInfoAttribute.GetCurrencyInfo(cach, this.curyInfoID, sender == cach ? row : cach.Current);
      return CurrencyCollection.GetCurrency(this.useForeignCury ? currencyInfo?.CuryID : currencyInfo?.BaseCuryID)?.CurySymbol;
    }
    if (this.curyID != (System.Type) null)
    {
      PXCache cach = sender.Graph.Caches[this.curyID.DeclaringType];
      return CurrencyCollection.GetCurrency(cach.GetValue(sender == cach ? row : cach.Current, this.curyID.Name) as string)?.CurySymbol;
    }
    if (this.branchID != (System.Type) null || this.organizationID != (System.Type) null)
    {
      string currencySymbol = (string) null;
      if (this.branchID != (System.Type) null)
      {
        PXCache cach = sender.Graph.Caches[this.branchID.DeclaringType];
        currencySymbol = CurrencyCollection.GetCurrency(PXAccess.GetBranch(cach.GetValue(sender == cach ? row : cach.Current, this.branchID.Name) as int?)?.BaseCuryID)?.CurySymbol;
      }
      if (string.IsNullOrEmpty(currencySymbol) && this.organizationID != (System.Type) null)
      {
        PXCache cach = sender.Graph.Caches[this.organizationID.DeclaringType];
        currencySymbol = CurrencyCollection.GetCurrency(((PXAccess.Organization) PXAccess.GetOrganizationByID(cach.GetValue(sender == cach ? row : cach.Current, this.organizationID.Name) as int?))?.BaseCuryID)?.CurySymbol;
      }
      return currencySymbol;
    }
    if (this.siteID != (System.Type) null)
    {
      PXCache cach = sender.Graph.Caches[this.siteID.DeclaringType];
      int? siteID = cach.GetValue(sender == cach ? row : cach.Current, this.siteID.Name) as int?;
      return CurrencyCollection.GetCurrency(INSite.PK.Find(sender.Graph, siteID)?.BaseCuryID)?.CurySymbol;
    }
    if (this.bAccountID != (System.Type) null)
    {
      PXCache cach = sender.Graph.Caches[this.bAccountID.DeclaringType];
      int? bAccountID = cach.GetValue(sender == cach ? row : cach.Current, this.bAccountID.Name) as int?;
      PX.Objects.CR.BAccount baccount = PX.Objects.CR.BAccount.PK.Find(sender.Graph, bAccountID);
      if (baccount == null)
        return (string) null;
      if (this.useForeignCury)
        return CurrencyCollection.GetCurrency(baccount.CuryID)?.CurySymbol;
      string baseCuryId = baccount.BaseCuryID;
      if (baccount.BaseCuryID == null && baccount.IsBranch.GetValueOrDefault())
        baseCuryId = PXAccess.GetBranchByBAccountID(baccount.BAccountID)?.BaseCuryID;
      return CurrencyCollection.GetCurrency(baseCuryId)?.CurySymbol;
    }
    return CurrencyCollection.GetCurrency(sender.Graph.Accessinfo.BaseCuryID)?.CurySymbol;
  }

  protected virtual void labelFieldSelecting(PXCache sender, PXFieldSelectingEventArgs e)
  {
    if (!(sender.GetStateExt(e.Row, this._FieldName) is PXFieldState stateExt))
      return;
    string currencySymbol = !this.isDependentOnMBC || PXAccess.FeatureInstalled<FeaturesSet.multipleBaseCurrencies>() ? this.GetCurrencySymbol(sender, e.Row) : (string) null;
    string str = !string.IsNullOrEmpty(this.displayName) ? PXMessages.LocalizeNoPrefix(this.displayName) : stateExt.DisplayName;
    e.ReturnValue = (object) str;
    if (currencySymbol != null)
      e.ReturnValue = (object) $"{str} ({currencySymbol})";
    if (this._AttributeLevel != 2 && !e.IsAltered)
      return;
    PXStringState instance = (PXStringState) PXStringState.CreateInstance(e.ReturnState, new int?(100), new bool?(true), this.LabelField, new bool?(false), new int?(), (string) null, (string[]) null, (string[]) null, new bool?(), (string) null, (string[]) null);
    ((PXFieldState) instance).Required = new bool?(false);
    ((PXFieldState) instance).Enabled = false;
    ((PXFieldState) instance).Visible = stateExt.Visible;
    e.ReturnState = (object) instance;
  }

  public void SetSymbol(string symbol) => this.symbol = symbol;
}
