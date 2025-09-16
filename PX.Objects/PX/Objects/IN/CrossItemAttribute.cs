// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.CrossItemAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Api;
using PX.Common;
using PX.Data;
using PX.Objects.AR;
using PX.Objects.CS;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;

#nullable disable
namespace PX.Objects.IN;

[PXDBInt]
[PXUIField]
public class CrossItemAttribute : InventoryIncludingTemplatesAttribute, IPXFieldVerifyingSubscriber
{
  protected INPrimaryAlternateType? _PrimaryAltType;
  protected string _AlternateID = "AlternateID";
  protected string _SubItemID = "SubItemID";
  protected string _UOM = "UOM";
  private static readonly ReadOnlyDictionary<INPrimaryAlternateType?, Type> AltTypeToDefaultBAccountFieldMap = new ReadOnlyDictionary<INPrimaryAlternateType?, Type>((IDictionary<INPrimaryAlternateType?, Type>) new Dictionary<INPrimaryAlternateType?, Type>()
  {
    [new INPrimaryAlternateType?(INPrimaryAlternateType.CPN)] = typeof (Customer.bAccountID),
    [new INPrimaryAlternateType?(INPrimaryAlternateType.VPN)] = typeof (PX.Objects.AP.Vendor.bAccountID)
  });
  private Type _bAccountField;
  private Type[] _inventoryRestrictingConditions;
  protected int _templateItemsRestrictorIndex = -1;

  public Type BAccountField
  {
    get
    {
      Type bAccountField = this._bAccountField;
      return (object) bAccountField != null ? bAccountField : EnumerableEx.GetOrDefault<INPrimaryAlternateType?, Type>((IDictionary<INPrimaryAlternateType?, Type>) CrossItemAttribute.AltTypeToDefaultBAccountFieldMap, this._PrimaryAltType, (Type) null);
    }
    set => this._bAccountField = value;
  }

  public string[] AlternateTypePriority { get; set; } = new string[5]
  {
    "0CPN",
    "0VPN",
    "BAR",
    "GIN",
    "GLBL"
  };

  public bool EnableAlternateSubstitution { get; set; } = true;

  protected Type[] InventoryRestrictingConditions
  {
    get
    {
      if (this._inventoryRestrictingConditions == null)
        this._inventoryRestrictingConditions = ((PXAggregateAttribute) this).GetAttributes().OfType<PXRestrictorAttribute>().Select<PXRestrictorAttribute, Type>((Func<PXRestrictorAttribute, Type>) (r => r.RestrictingCondition)).Where<Type>((Func<Type, bool>) (r => ((IEnumerable<Type>) BqlCommand.Decompose(r)).All<Type>((Func<Type, bool>) (c => !typeof (IBqlField).IsAssignableFrom(c) || EnumerableExtensions.IsIn<Type>(c.DeclaringType, typeof (InventoryItem), typeof (FeaturesSet)))))).ToArray<Type>();
      return this._inventoryRestrictingConditions;
    }
  }

  public bool WarningOnNonUniqueSubstitution { get; set; }

  public virtual bool AllowTemplateItems
  {
    get => this._templateItemsRestrictorIndex < 0;
    set
    {
      if (value && this._templateItemsRestrictorIndex >= 0)
      {
        ((PXAggregateAttribute) this)._Attributes.RemoveAt(this._templateItemsRestrictorIndex);
        this._templateItemsRestrictorIndex = -1;
      }
      else
      {
        if (value || this._templateItemsRestrictorIndex >= 0)
          return;
        ((PXAggregateAttribute) this)._Attributes.Add((PXEventSubscriberAttribute) new PXRestrictorAttribute(typeof (Where<InventoryItem.isTemplate, Equal<False>>), "The inventory item is a template item.", Array.Empty<Type>())
        {
          ShowWarning = true
        });
        this._templateItemsRestrictorIndex = ((List<PXEventSubscriberAttribute>) ((PXAggregateAttribute) this)._Attributes).Count - 1;
      }
    }
  }

  public CrossItemAttribute()
    : base(typeof (Search<InventoryItem.inventoryID, Where2<Match<Current<AccessInfo.userName>>, And<Where<InventoryItem.stkItem, Equal<True>, Or<InventoryItem.kitItem, Equal<True>>>>>>), typeof (InventoryItem.inventoryCD), typeof (InventoryItem.descr))
  {
    CrossItemAttribute.ReplaceSelectorMessages(this.SelectorAttribute);
    this.AllowTemplateItems = false;
  }

  public CrossItemAttribute(
    Type SearchType,
    Type SubstituteKey,
    Type DescriptionField,
    INPrimaryAlternateType PrimaryAltType)
    : base(SearchType, SubstituteKey, DescriptionField)
  {
    this._PrimaryAltType = new INPrimaryAlternateType?(PrimaryAltType);
    CrossItemAttribute.ReplaceSelectorMessages(this.SelectorAttribute);
    this.AllowTemplateItems = false;
  }

  public CrossItemAttribute(INPrimaryAlternateType PrimaryAltType)
    : this()
  {
    this._PrimaryAltType = new INPrimaryAlternateType?(PrimaryAltType);
  }

  public virtual void CacheAttached(PXCache sender)
  {
    ((PXAggregateAttribute) this).CacheAttached(sender);
    PXGraph.FieldUpdatingEvents fieldUpdating1 = sender.Graph.FieldUpdating;
    Type itemType1 = sender.GetItemType();
    string fieldName1 = ((PXEventSubscriberAttribute) this)._FieldName;
    PXDimensionSelectorAttribute selectorAttribute = this.SelectorAttribute;
    // ISSUE: virtual method pointer
    PXFieldUpdating pxFieldUpdating1 = new PXFieldUpdating((object) selectorAttribute, __vmethodptr(selectorAttribute, FieldUpdating));
    fieldUpdating1.RemoveHandler(itemType1, fieldName1, pxFieldUpdating1);
    PXGraph.FieldUpdatingEvents fieldUpdating2 = sender.Graph.FieldUpdating;
    Type itemType2 = sender.GetItemType();
    string fieldName2 = ((PXEventSubscriberAttribute) this)._FieldName;
    CrossItemAttribute crossItemAttribute = this;
    // ISSUE: virtual method pointer
    PXFieldUpdating pxFieldUpdating2 = new PXFieldUpdating((object) crossItemAttribute, __vmethodptr(crossItemAttribute, FieldUpdating));
    fieldUpdating2.AddHandler(itemType2, fieldName2, pxFieldUpdating2);
  }

  public virtual void GetSubscriber<ISubscriber>(List<ISubscriber> subscribers) where ISubscriber : class
  {
    base.GetSubscriber<ISubscriber>(subscribers);
    if (!(typeof (ISubscriber) == typeof (IPXFieldVerifyingSubscriber)))
      return;
    subscribers.Remove(((PXAggregateAttribute) this)._Attributes[this._SelAttrIndex] as ISubscriber);
  }

  public static void ReplaceSelectorMessages(PXDimensionSelectorAttribute selector)
  {
    selector.CustomMessageElementDoesntExist = "The specified inventory ID or alternate ID cannot be found in the system.";
    selector.CustomMessageElementDoesntExistOrNoRights = "The specified inventory ID or alternate ID cannot be found in the system. Please verify whether you have proper access rights to this object.";
    selector.CustomMessageValueDoesntExist = "The specified inventory ID or alternate ID \"{1}\" cannot be found in the system.";
    selector.CustomMessageValueDoesntExistOrNoRights = "The specified inventory ID or alternate ID \"{1}\" cannot be found in the system. Please verify whether you have proper access rights to this object.";
  }

  public virtual void FieldUpdating(PXCache sender, PXFieldUpdatingEventArgs e)
  {
    try
    {
      this.SelectorAttribute.FieldUpdating(sender, e);
      return;
    }
    catch (PXSetPropertyException ex)
    {
    }
    CrossItemAttribute.FoundInventory alternate = this.FindAlternate(sender, e.NewValue as string);
    e.NewValue = (alternate != null ? (object) alternate.InventoryCD : (object) null) ?? e.NewValue;
    this.SelectorAttribute.FieldUpdating(sender, e);
    this.SetValuesPending(sender, e.Row, alternate);
    this.RaiseWarningIfReferenceIsNotUnique(sender, e.Row, alternate);
  }

  public virtual void FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs a)
  {
    try
    {
      this.SelectorAttribute.FieldVerifying(sender, a);
      return;
    }
    catch (PXSetPropertyException ex)
    {
      if (a.Row == null)
        throw;
    }
    PXFieldUpdatingEventArgs updatingEventArgs = new PXFieldUpdatingEventArgs(a.Row, sender.GetValuePending(a.Row, ((PXEventSubscriberAttribute) this)._FieldName));
    CrossItemAttribute.FoundInventory alternate = this.FindAlternate(sender, updatingEventArgs.NewValue as string);
    updatingEventArgs.NewValue = (alternate != null ? (object) alternate.InventoryCD : (object) null) ?? updatingEventArgs.NewValue;
    this.SelectorAttribute.FieldUpdating(sender, updatingEventArgs);
    a.NewValue = updatingEventArgs.NewValue;
    this.SelectorAttribute.FieldVerifying(sender, a);
    this.SetValuesPending(sender, updatingEventArgs.Row, alternate);
    this.RaiseWarningIfReferenceIsNotUnique(sender, a.Row, alternate);
  }

  private void RaiseWarningIfReferenceIsNotUnique(
    PXCache cache,
    object row,
    CrossItemAttribute.FoundInventory foundAlternate)
  {
    if (!this.WarningOnNonUniqueSubstitution || foundAlternate == null || foundAlternate.UniqueReference)
      return;
    cache.RaiseExceptionHandling(((PXEventSubscriberAttribute) this)._FieldName, row, (object) foundAlternate.AlternateID, (Exception) new PXSetPropertyException("The specified alternate ID is assigned to multiple inventory items. Please make sure that the correct inventory ID has been specified in the row.", (PXErrorLevel) 2));
  }

  private void SetValuesPending(
    PXCache cache,
    object row,
    CrossItemAttribute.FoundInventory foundInventory)
  {
    if (foundInventory == null)
      return;
    if (foundInventory.AlternateID != null && cache.Fields.Contains(this._AlternateID))
      cache.SetValuePending(row, this._AlternateID, (object) foundInventory.AlternateID);
    if (foundInventory.SubItemCD != null && cache.Fields.Contains(this._SubItemID))
      cache.SetValuePending(row, this._SubItemID, (object) foundInventory.SubItemCD);
    if (foundInventory.UOM == null || !cache.Fields.Contains(this._UOM))
      return;
    cache.SetValuePending(row, this._UOM, (object) foundInventory.UOM);
  }

  protected CrossItemAttribute.FoundInventory FindAlternate(PXCache sender, string alternateID)
  {
    return GenericCall.Of<CrossItemAttribute.FoundInventory>((Expression<Func<CrossItemAttribute.FoundInventory>>) (() => this.findAlternate<IBqlField>(sender, alternateID))).ButWith(this.BAccountField, Array.Empty<Type>());
  }

  protected virtual CrossItemAttribute.FoundInventory findAlternate<BAccountID>(
    PXCache sender,
    string alternateID)
    where BAccountID : IBqlField
  {
    if (!this.EnableAlternateSubstitution)
      return CrossItemAttribute.FoundInventory.NotFound(alternateID);
    PXResult<INItemXRef> pxResult = (PXResult<INItemXRef>) null;
    bool flag = false;
    if (!string.IsNullOrEmpty(alternateID))
    {
      PXSelectBase<INItemXRef> pxSelectBase = (PXSelectBase<INItemXRef>) new PXSelectJoin<INItemXRef, InnerJoin<InventoryItem, On<INItemXRef.FK.InventoryItem>, LeftJoin<INSubItem, On<INItemXRef.FK.SubItem>>>, Where2<Match<Current<AccessInfo.userName>>, And<INItemXRef.alternateID, Equal<Required<INItemXRef.alternateID>>>>>(sender.Graph);
      foreach (Type restrictingCondition in this.InventoryRestrictingConditions)
        pxSelectBase.WhereAnd(restrictingCondition);
      INPrimaryAlternateType? primaryAltType = this._PrimaryAltType;
      if (primaryAltType.HasValue)
      {
        switch (primaryAltType.GetValueOrDefault())
        {
          case INPrimaryAlternateType.VPN:
            pxSelectBase.WhereAnd<Where<INItemXRef.alternateType, Equal<INAlternateType.vPN>, And<INItemXRef.bAccountID, Equal<Current<BAccountID>>, Or<INItemXRef.alternateType, NotEqual<INAlternateType.cPN>, And<INItemXRef.alternateType, NotEqual<INAlternateType.vPN>>>>>>();
            goto label_11;
          case INPrimaryAlternateType.CPN:
            pxSelectBase.WhereAnd<Where<INItemXRef.alternateType, Equal<INAlternateType.cPN>, And<INItemXRef.bAccountID, Equal<Current<BAccountID>>, Or<INItemXRef.alternateType, NotEqual<INAlternateType.cPN>, And<INItemXRef.alternateType, NotEqual<INAlternateType.vPN>>>>>>();
            goto label_11;
        }
      }
      pxSelectBase.WhereAnd<Where<INItemXRef.alternateType, NotEqual<INAlternateType.cPN>, And<INItemXRef.alternateType, NotEqual<INAlternateType.vPN>>>>();
label_11:
      PXResult<INItemXRef>[] array = EnumerableExtensions.OrderBy<PXResult<INItemXRef>, string>((IEnumerable<PXResult<INItemXRef>>) pxSelectBase.Select(new object[1]
      {
        (object) alternateID
      }), (Func<PXResult<INItemXRef>, string>) (r => PXResult<INItemXRef>.op_Implicit(r).AlternateType), ((IEnumerable<string>) this.AlternateTypePriority).ToArray<string>()).ToArray<PXResult<INItemXRef>>();
      pxResult = ((IEnumerable<PXResult<INItemXRef>>) array).FirstOrDefault<PXResult<INItemXRef>>();
      flag = array.Length > 1;
    }
    if (pxResult == null)
      return CrossItemAttribute.FoundInventory.NotFound(alternateID);
    InventoryItem inventoryItem = ((PXResult) pxResult).GetItem<InventoryItem>();
    INItemXRef inItemXref = ((PXResult) pxResult).GetItem<INItemXRef>();
    string inventoryCd = inventoryItem.InventoryCD;
    string uom = inItemXref.UOM;
    if (this._PrimaryAltType.HasValue)
    {
      string str = INAlternateType.ConvertFromPrimary(this._PrimaryAltType.Value);
      if (!string.IsNullOrEmpty(str) && inItemXref.AlternateType == str)
        alternateID = inItemXref.AlternateID;
      else if (str == "0CPN" || str == "0VPN")
      {
        if (((PXSelectBase<INItemXRef>) new PXSelect<INItemXRef, Where<INItemXRef.inventoryID, Equal<Required<INItemXRef.inventoryID>>, And<INItemXRef.subItemID, Equal<Required<INItemXRef.subItemID>>, And<INItemXRef.alternateType, Equal<Required<INItemXRef.alternateType>>, And<INItemXRef.uOM, Equal<Required<INItemXRef.uOM>>, And<INItemXRef.bAccountID, Equal<Current<BAccountID>>>>>>>>(sender.Graph)).SelectSingle(new object[4]
        {
          (object) inItemXref.InventoryID,
          (object) inItemXref.SubItemID,
          (object) str,
          (object) inItemXref.UOM
        }) == null)
          alternateID = inItemXref.AlternateID;
      }
    }
    else
      alternateID = inItemXref.AlternateID;
    string subItemCD = (string) null;
    if (alternateID != null && string.Equals(inventoryCd.Trim(), alternateID.Trim()))
    {
      alternateID = (string) null;
      uom = (string) null;
    }
    else if (inventoryItem.StkItem.GetValueOrDefault())
      subItemCD = ((PXResult) pxResult).GetItem<INSubItem>().SubItemCD;
    return CrossItemAttribute.FoundInventory.Found(alternateID, inventoryCd, subItemCD, uom, !flag);
  }

  public static void SetEnableAlternateSubstitution<TField>(
    PXCache cache,
    object row,
    bool enableAlternateSubstitution)
    where TField : IBqlField
  {
    foreach (CrossItemAttribute crossItemAttribute in cache.GetAttributes<TField>(row).OfType<CrossItemAttribute>())
      crossItemAttribute.EnableAlternateSubstitution = enableAlternateSubstitution;
  }

  public static void SetEnableAlternateSubstitution(
    PXCache cache,
    object row,
    Type field,
    bool enableAlternateSubstitution)
  {
    foreach (CrossItemAttribute crossItemAttribute in cache.GetAttributes(row, field.Name).OfType<CrossItemAttribute>())
      crossItemAttribute.EnableAlternateSubstitution = enableAlternateSubstitution;
  }

  protected class FoundInventory : Tuple<string, string, string, string, bool>
  {
    public static CrossItemAttribute.FoundInventory Found(
      string alternateID,
      string inventoryCD,
      string subItemCD,
      string uom,
      bool uniqueReference)
    {
      return new CrossItemAttribute.FoundInventory(alternateID, inventoryCD, subItemCD, uom, uniqueReference);
    }

    public static CrossItemAttribute.FoundInventory NotFound(string alternateID)
    {
      return new CrossItemAttribute.FoundInventory(alternateID, (string) null, (string) null, (string) null, false);
    }

    private FoundInventory(
      string alternateID,
      string inventoryCD,
      string subItemCD,
      string uom,
      bool uniqueReference)
      : base(alternateID, inventoryCD, subItemCD, uom, uniqueReference)
    {
    }

    public string AlternateID => this.Item1;

    public string InventoryCD => this.Item2;

    public string SubItemCD => this.Item3;

    public string UOM => this.Item4;

    public bool UniqueReference => this.Item5;

    public bool IsFound => !Str.IsNullOrEmpty(this.InventoryCD);
  }

  [PXLocalizable]
  public class CrossItemMessages
  {
    public const string ElementDoesntExist = "The specified inventory ID or alternate ID cannot be found in the system.";
    public const string ValueDoesntExist = "The specified inventory ID or alternate ID \"{1}\" cannot be found in the system.";
    public const string ElementDoesntExistOrNoRights = "The specified inventory ID or alternate ID cannot be found in the system. Please verify whether you have proper access rights to this object.";
    public const string ValueDoesntExistOrNoRights = "The specified inventory ID or alternate ID \"{1}\" cannot be found in the system. Please verify whether you have proper access rights to this object.";
    public const string ManyItemsForCurrentAlternateID = "The specified alternate ID is assigned to multiple inventory items. Please make sure that the correct inventory ID has been specified in the row.";
  }
}
