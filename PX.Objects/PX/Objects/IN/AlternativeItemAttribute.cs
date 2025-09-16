// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.AlternativeItemAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Objects.AR;
using PX.Objects.CS;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

#nullable disable
namespace PX.Objects.IN;

[PXUIField(DisplayName = "Alternate ID")]
[PXDBString(50, IsUnicode = true, InputMask = "")]
public class AlternativeItemAttribute : 
  PXAggregateAttribute,
  IPXRowUpdatingSubscriber,
  IPXRowDeletingSubscriber,
  IPXRowInsertingSubscriber
{
  protected INPrimaryAlternateType? _PrimaryAltType;
  protected Type _InventoryID;
  protected Type _SubItemID;
  protected Type _UOM;
  protected Type _BAccountID;
  protected Type _AlternateIDChangeAction;
  protected AlternateIDOnChangeAction? _OnChangeAction;
  protected bool _KeepSinglePrimaryAltID = true;
  private PXView _xRefView;

  public AlternativeItemAttribute(
    INPrimaryAlternateType PrimaryAltType,
    Type InventoryID,
    Type SubItemID,
    Type uom)
    : this(PrimaryAltType, (Type) null, InventoryID, SubItemID, uom)
  {
  }

  public AlternativeItemAttribute(
    INPrimaryAlternateType PrimaryAltType,
    Type BAccountID,
    Type InventoryID,
    Type SubItemID,
    Type uom)
  {
    this._PrimaryAltType = new INPrimaryAlternateType?(PrimaryAltType);
    this._BAccountID = BAccountID;
    this._InventoryID = InventoryID;
    this._SubItemID = SubItemID;
    this._UOM = uom;
    // ISSUE: method reference
    Type type1 = GenericCall.Of<Type>(Expression.Lambda<Func<Type>>((Expression) Expression.Call((Expression) null, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (AlternativeItemAttribute.BuildDefaultRuleType)), Array.Empty<Expression>()))).ButWith(InventoryID, new Type[3]
    {
      SubItemID,
      uom,
      AlternativeItemAttribute.CreateWhereAltType(new INPrimaryAlternateType?(PrimaryAltType), BAccountID)
    });
    Type type2;
    if (!(this._BAccountID != (Type) null))
      type2 = BqlCommand.Compose(new Type[4]
      {
        typeof (Default<,,>),
        InventoryID,
        SubItemID,
        uom
      });
    else
      type2 = BqlCommand.Compose(new Type[5]
      {
        typeof (Default<,,,>),
        InventoryID,
        SubItemID,
        uom,
        BAccountID
      });
    Type type3 = type2;
    this._Attributes.Add((PXEventSubscriberAttribute) new PXDefaultAttribute(type1)
    {
      PersistingCheck = (PXPersistingCheck) 2
    });
    this._Attributes.Add((PXEventSubscriberAttribute) new PXFormulaAttribute(type3));
  }

  private static Type BuildDefaultRuleType<TInventoryID, TSubItemID, TUOM, TWhereAltType>()
    where TInventoryID : IBqlField
    where TSubItemID : IBqlField
    where TUOM : IBqlField
    where TWhereAltType : IBqlWhere, new()
  {
    return typeof (Coalesce<Search2<INItemXRef.alternateID, LeftJoin<INSetup, On<True, Equal<True>>>, Where<INItemXRef.inventoryID, Equal<Current<TInventoryID>>, And<INItemXRef.subItemID, Equal<Current<TSubItemID>>, And2<Where<INItemXRef.uOM, Equal<Current2<TUOM>>, Or<INItemXRef.uOM, IsNull>>, And<TWhereAltType>>>>, OrderBy<Asc<Switch<Case<Where<INItemXRef.alternateType, Equal<INAlternateType.global>>, int1, Case<Where<INItemXRef.alternateType, Equal<INAlternateType.barcode>>, int2>>, int0>, Desc<INItemXRef.uOM>>>>, Search2<INItemXRef.alternateID, InnerJoin<InventoryItem, On<INItemXRef.FK.InventoryItem>, LeftJoin<INSetup, On<True, Equal<True>>>>, Where<INItemXRef.inventoryID, Equal<Current<TInventoryID>>, And<INItemXRef.subItemID, Equal<InventoryItem.defaultSubItemID>, And2<Where<INItemXRef.uOM, Equal<Current2<TUOM>>, Or<INItemXRef.uOM, IsNull>>, And<TWhereAltType>>>>, OrderBy<Asc<Switch<Case<Where<INItemXRef.alternateType, Equal<INAlternateType.global>>, int1, Case<Where<INItemXRef.alternateType, Equal<INAlternateType.barcode>>, int2>>, int0>, Desc<INItemXRef.uOM>>>>>);
  }

  private static Type CreateWhereAltType(INPrimaryAlternateType? primaryAltType, Type bAccountID)
  {
    Type whereAltType;
    if (primaryAltType.HasValue)
    {
      switch (primaryAltType.GetValueOrDefault())
      {
        case INPrimaryAlternateType.VPN:
          // ISSUE: method reference
          GenericCall.OfFunction<Type> ofFunction1 = GenericCall.Of<Type>(Expression.Lambda<Func<Type>>((Expression) Expression.Call((Expression) null, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (AlternativeItemAttribute.CreateWhereForNonGlobalAltType)), Array.Empty<Expression>())));
          Type type1 = typeof (INAlternateType.vPN);
          Type[] typeArray1 = new Type[1];
          Type type2 = bAccountID;
          if ((object) type2 == null)
            type2 = typeof (PX.Objects.AP.Vendor.bAccountID);
          typeArray1[0] = type2;
          whereAltType = ofFunction1.ButWith(type1, typeArray1);
          goto label_9;
        case INPrimaryAlternateType.CPN:
          // ISSUE: method reference
          GenericCall.OfFunction<Type> ofFunction2 = GenericCall.Of<Type>(Expression.Lambda<Func<Type>>((Expression) Expression.Call((Expression) null, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (AlternativeItemAttribute.CreateWhereForNonGlobalAltType)), Array.Empty<Expression>())));
          Type type3 = typeof (INAlternateType.cPN);
          Type[] typeArray2 = new Type[1];
          Type type4 = bAccountID;
          if ((object) type4 == null)
            type4 = typeof (Customer.bAccountID);
          typeArray2[0] = type4;
          whereAltType = ofFunction2.ButWith(type3, typeArray2);
          goto label_9;
      }
    }
    whereAltType = typeof (Where<INItemXRef.alternateType, Equal<INAlternateType.global>>);
label_9:
    return whereAltType;
  }

  private static Type CreateWhereForNonGlobalAltType<TAltType, TBAccountField>()
    where TAltType : IBqlOperand
    where TBAccountField : IBqlField
  {
    return typeof (Where<INItemXRef.alternateType, Equal<TAltType>, And<INItemXRef.bAccountID, Equal<Current<TBAccountField>>, Or<INItemXRef.alternateType, Equal<INAlternateType.global>, Or<INItemXRef.alternateType, Equal<INAlternateType.barcode>, And<INSetup.showBarcodesInOrderLines, Equal<True>>>>>>);
  }

  public virtual void CacheAttached(PXCache sender)
  {
    base.CacheAttached(sender);
    if (!((Dictionary<string, PXView>) sender.Graph.Views).TryGetValue($"_{typeof (INItemXRef)?.ToString()}_", out this._xRefView))
    {
      this._xRefView = new PXView(sender.Graph, false, (BqlCommand) new Select<INItemXRef>());
      sender.Graph.Views.Add($"_{typeof (INItemXRef)?.ToString()}_", this._xRefView);
    }
    if (sender.Graph.Views.Caches.Contains(typeof (INItemXRef)))
      return;
    sender.Graph.Views.Caches.Add(typeof (INItemXRef));
  }

  public virtual void RowInserting(PXCache sender, PXRowInsertingEventArgs e)
  {
    this.UpdateAltNumber(sender, this.GetBAccountID(sender, e.Row), this.GetInventoryID(sender, e.Row), this.GetSubItemID(sender, e.Row), this.GetUOM(sender, e.Row), (string) null, this.GetAlternateID(sender, e.Row));
  }

  public virtual void RowUpdating(PXCache sender, PXRowUpdatingEventArgs e)
  {
    bool flag1 = this.IsChanged(sender, this._InventoryID, e.Row, e.NewRow);
    bool flag2 = this.IsChanged(sender, this._SubItemID, e.Row, e.NewRow);
    bool flag3 = this.IsChanged(sender, this._BAccountID, e.Row, e.NewRow);
    bool flag4 = this.IsChanged(sender, this._UOM, e.Row, e.NewRow);
    bool flag5 = this.IsChanged(sender, ((PXEventSubscriberAttribute) this)._FieldName, e.Row, e.NewRow);
    if (!(flag1 | flag2 | flag3 | flag4 | flag5))
      return;
    this.DeleteUnsavedNumber(sender, this.GetBAccountID(sender, e.Row), this.GetInventoryID(sender, e.Row), this.GetSubItemID(sender, e.Row), this.GetUOM(sender, e.Row), this.GetAlternateID(sender, e.Row));
    if (!(!(flag1 | flag2 | flag3 | flag4) & flag5))
      return;
    this.UpdateAltNumber(sender, this.GetBAccountID(sender, e.NewRow), this.GetInventoryID(sender, e.NewRow), this.GetSubItemID(sender, e.NewRow), this.GetUOM(sender, e.NewRow), this.GetAlternateID(sender, e.Row), this.GetAlternateID(sender, e.NewRow));
  }

  public virtual void RowDeleting(PXCache sender, PXRowDeletingEventArgs e)
  {
    this.DeleteUnsavedNumber(sender, this.GetBAccountID(sender, e.Row), this.GetInventoryID(sender, e.Row), this.GetSubItemID(sender, e.Row), this.GetUOM(sender, e.Row), this.GetAlternateID(sender, e.Row));
  }

  private void DeleteUnsavedNumber(
    PXCache sender,
    int? bAccountID,
    int? inventoryId,
    int? subItem,
    string uom,
    string altId)
  {
    if (!inventoryId.HasValue || !subItem.HasValue || altId == null)
      return;
    PXCache cach = sender.Graph.Caches[typeof (INItemXRef)];
    foreach (INItemXRef inItemXref in cach.Inserted)
    {
      int? nullable1 = inItemXref.BAccountID;
      int? nullable2 = bAccountID;
      if (nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue && inItemXref.AlternateID == altId)
      {
        nullable2 = inItemXref.InventoryID;
        nullable1 = inventoryId;
        if (nullable2.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable2.HasValue == nullable1.HasValue)
        {
          nullable1 = inItemXref.SubItemID;
          nullable2 = subItem;
          if (nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue && inItemXref.UOM == uom)
            cach.Delete((object) inItemXref);
        }
      }
    }
  }

  private void UpdateAltNumber(
    PXCache cache,
    int? bAccountID,
    int? inventoryId,
    int? subItemId,
    string uom,
    string oldAltID,
    string newAltID)
  {
    if (!inventoryId.HasValue || !subItemId.HasValue || newAltID == null || !this._PrimaryAltType.HasValue || string.IsNullOrWhiteSpace(newAltID) || cache.Graph.IsImport || cache.Graph.IsCopyPasteContext)
      return;
    AlternateIDOnChangeAction onChangeAction = this.GetOnChangeAction(cache.Graph);
    if (onChangeAction == AlternateIDOnChangeAction.StoreLocally || cache.Graph.IsCopyPasteContext)
      return;
    PXSelect<INItemXRef, Where<INItemXRef.alternateID, Equal<Required<INItemXRef.alternateID>>, And<INItemXRef.inventoryID, Equal<Required<INItemXRef.inventoryID>>, And<INItemXRef.subItemID, Equal<Required<INItemXRef.subItemID>>, And<Where<INItemXRef.uOM, Equal<Required<INItemXRef.uOM>>, Or<INItemXRef.uOM, IsNull>>>>>>, OrderBy<Asc<INItemXRef.alternateType, Desc<INItemXRef.alternateID>>>> cmd1 = new PXSelect<INItemXRef, Where<INItemXRef.alternateID, Equal<Required<INItemXRef.alternateID>>, And<INItemXRef.inventoryID, Equal<Required<INItemXRef.inventoryID>>, And<INItemXRef.subItemID, Equal<Required<INItemXRef.subItemID>>, And<Where<INItemXRef.uOM, Equal<Required<INItemXRef.uOM>>, Or<INItemXRef.uOM, IsNull>>>>>>, OrderBy<Asc<INItemXRef.alternateType, Desc<INItemXRef.alternateID>>>>(cache.Graph);
    AlternativeItemAttribute.AddAlternativeTypeWhere((PXSelectBase<INItemXRef>) cmd1, this._PrimaryAltType, false);
    if (PXResultset<INItemXRef>.op_Implicit(((PXSelectBase<INItemXRef>) cmd1).Select(new object[5]
    {
      (object) newAltID,
      (object) inventoryId,
      (object) subItemId,
      (object) uom,
      (object) bAccountID
    })) != null)
      return;
    PXSelect<INItemXRef, Where<INItemXRef.alternateID, Equal<Required<INItemXRef.alternateID>>, And<Where<INItemXRef.inventoryID, NotEqual<Required<INItemXRef.inventoryID>>, Or<INItemXRef.subItemID, NotEqual<Required<INItemXRef.subItemID>>, Or<INItemXRef.uOM, NotEqual<Required<INItemXRef.uOM>>>>>>>> cmd2 = new PXSelect<INItemXRef, Where<INItemXRef.alternateID, Equal<Required<INItemXRef.alternateID>>, And<Where<INItemXRef.inventoryID, NotEqual<Required<INItemXRef.inventoryID>>, Or<INItemXRef.subItemID, NotEqual<Required<INItemXRef.subItemID>>, Or<INItemXRef.uOM, NotEqual<Required<INItemXRef.uOM>>>>>>>>(cache.Graph);
    AlternativeItemAttribute.AddAlternativeTypeWhere((PXSelectBase<INItemXRef>) cmd2, this._PrimaryAltType, false);
    INItemXRef inItemXref1 = GraphHelper.RowCast<INItemXRef>((IEnumerable) ((PXSelectBase<INItemXRef>) cmd2).Select(new object[5]
    {
      (object) newAltID,
      (object) inventoryId,
      (object) subItemId,
      (object) uom,
      (object) bAccountID
    })).FirstOrDefault<INItemXRef>();
    if (inItemXref1 != null)
    {
      int? nullable1 = inItemXref1.InventoryID;
      int? nullable2 = inventoryId;
      if (nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue)
      {
        nullable2 = inItemXref1.SubItemID;
        nullable1 = subItemId;
        if (nullable2.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable2.HasValue == nullable1.HasValue)
          goto label_10;
      }
      throw new AlternatieIDNotUniqueException(newAltID);
    }
label_10:
    if (inItemXref1 != null && inItemXref1.UOM != uom)
    {
      InventoryItem inventoryItem = InventoryItem.PK.Find(cache.Graph, inItemXref1.InventoryID);
      throw new AlternatieIDNotUniqueException(inItemXref1.AlternateID, inventoryItem.InventoryCD, inItemXref1.UOM);
    }
    INItemXRef inItemXref2 = (INItemXRef) null;
    if (onChangeAction == AlternateIDOnChangeAction.UpdateOriginal || onChangeAction == AlternateIDOnChangeAction.AskUser)
    {
      PXSelect<INItemXRef, Where<INItemXRef.alternateID, Equal<Required<INItemXRef.alternateID>>, And<INItemXRef.inventoryID, Equal<Required<INItemXRef.inventoryID>>, And<INItemXRef.subItemID, Equal<Required<INItemXRef.subItemID>>, And<Where<INItemXRef.uOM, Equal<Required<INItemXRef.uOM>>, Or<INItemXRef.uOM, IsNull>>>>>>, OrderBy<Asc<INItemXRef.alternateType, Desc<INItemXRef.alternateID, Desc<INItemXRef.uOM>>>>> cmd3 = new PXSelect<INItemXRef, Where<INItemXRef.alternateID, Equal<Required<INItemXRef.alternateID>>, And<INItemXRef.inventoryID, Equal<Required<INItemXRef.inventoryID>>, And<INItemXRef.subItemID, Equal<Required<INItemXRef.subItemID>>, And<Where<INItemXRef.uOM, Equal<Required<INItemXRef.uOM>>, Or<INItemXRef.uOM, IsNull>>>>>>, OrderBy<Asc<INItemXRef.alternateType, Desc<INItemXRef.alternateID, Desc<INItemXRef.uOM>>>>>(cache.Graph);
      AlternativeItemAttribute.AddAlternativeTypeWhere((PXSelectBase<INItemXRef>) cmd3, this._PrimaryAltType, true);
      inItemXref2 = PXResultset<INItemXRef>.op_Implicit(((PXSelectBase<INItemXRef>) cmd3).Select(new object[5]
      {
        (object) oldAltID,
        (object) inventoryId,
        (object) subItemId,
        (object) uom,
        (object) bAccountID
      }));
      if (inItemXref2 != null)
      {
        if (!string.IsNullOrEmpty(inItemXref2.AlternateID) && onChangeAction == AlternateIDOnChangeAction.AskUser && !this.UserWantsToUpdateXRef())
          return;
        this._xRefView.Cache.Delete((object) inItemXref2);
      }
      else if (this._KeepSinglePrimaryAltID)
      {
        PXSelect<INItemXRef, Where<INItemXRef.alternateID, NotEqual<Required<INItemXRef.alternateID>>, And<INItemXRef.alternateID, NotEqual<Empty>, And<INItemXRef.alternateID, IsNotNull, And<INItemXRef.inventoryID, Equal<Required<INItemXRef.inventoryID>>, And<INItemXRef.subItemID, Equal<Required<INItemXRef.subItemID>>, And<Where<INItemXRef.uOM, Equal<Required<INItemXRef.uOM>>, Or<INItemXRef.uOM, IsNull>>>>>>>>> cmd4 = new PXSelect<INItemXRef, Where<INItemXRef.alternateID, NotEqual<Required<INItemXRef.alternateID>>, And<INItemXRef.alternateID, NotEqual<Empty>, And<INItemXRef.alternateID, IsNotNull, And<INItemXRef.inventoryID, Equal<Required<INItemXRef.inventoryID>>, And<INItemXRef.subItemID, Equal<Required<INItemXRef.subItemID>>, And<Where<INItemXRef.uOM, Equal<Required<INItemXRef.uOM>>, Or<INItemXRef.uOM, IsNull>>>>>>>>>(cache.Graph);
        AlternativeItemAttribute.AddAlternativeTypeWhere((PXSelectBase<INItemXRef>) cmd4, this._PrimaryAltType, true);
        if (((IQueryable<PXResult<INItemXRef>>) ((PXSelectBase<INItemXRef>) cmd4).Select(new object[5]
        {
          (object) newAltID,
          (object) inventoryId,
          (object) subItemId,
          (object) uom,
          (object) bAccountID
        })).Any<PXResult<INItemXRef>>())
          return;
      }
    }
    int num = inItemXref2 == null ? 1 : 0;
    INItemXRef inItemXref3 = num != 0 ? new INItemXRef() : (INItemXRef) this._xRefView.Cache.CreateCopy((object) inItemXref2);
    inItemXref3.InventoryID = inventoryId;
    inItemXref3.SubItemID = subItemId;
    inItemXref3.BAccountID = bAccountID;
    inItemXref3.AlternateID = newAltID;
    if (num != 0 || inItemXref3.UOM != null)
      inItemXref3.UOM = uom;
    inItemXref3.AlternateType = INAlternateType.ConvertFromPrimary(this._PrimaryAltType.Value);
    this._xRefView.Cache.Update((object) inItemXref3);
    this._xRefView.Answer = (WebDialogResult) 0;
  }

  private bool UserWantsToUpdateXRef()
  {
    return this._xRefView.Ask("Substitute previous cross references information?", (MessageButtons) 4, false) == 6;
  }

  public static void AddAlternativeTypeWhere(
    PXSelectBase<INItemXRef> cmd,
    INPrimaryAlternateType? primaryAlternateType,
    bool typeExclusive)
  {
    if (primaryAlternateType.HasValue)
    {
      switch (primaryAlternateType.GetValueOrDefault())
      {
        case INPrimaryAlternateType.VPN:
          if (typeExclusive)
          {
            cmd.WhereAnd<Where<INItemXRef.alternateType, Equal<INAlternateType.vPN>, And<INItemXRef.bAccountID, Equal<Required<INItemXRef.bAccountID>>>>>();
            return;
          }
          cmd.WhereAnd<Where<INItemXRef.alternateType, Equal<INAlternateType.vPN>, And<INItemXRef.bAccountID, Equal<Required<INItemXRef.bAccountID>>, Or<INItemXRef.alternateType, NotEqual<INAlternateType.cPN>, And<INItemXRef.alternateType, NotEqual<INAlternateType.vPN>>>>>>();
          return;
        case INPrimaryAlternateType.CPN:
          if (typeExclusive)
          {
            cmd.WhereAnd<Where<INItemXRef.alternateType, Equal<INAlternateType.cPN>, And<INItemXRef.bAccountID, Equal<Required<INItemXRef.bAccountID>>>>>();
            return;
          }
          cmd.WhereAnd<Where<INItemXRef.alternateType, Equal<INAlternateType.cPN>, And<INItemXRef.bAccountID, Equal<Required<INItemXRef.bAccountID>>, Or<INItemXRef.alternateType, NotEqual<INAlternateType.cPN>, And<INItemXRef.alternateType, NotEqual<INAlternateType.vPN>>>>>>();
          return;
      }
    }
    cmd.WhereAnd<Where<INItemXRef.alternateType, NotEqual<INAlternateType.cPN>, And<INItemXRef.alternateType, NotEqual<INAlternateType.vPN>>>>();
  }

  private string GetAlternateID(PXCache sender, object row)
  {
    return (string) sender.GetValue(row, ((PXEventSubscriberAttribute) this)._FieldName);
  }

  private int? GetSubItemID(PXCache cache, object row)
  {
    return this.GetCurrentValue<int?>(cache, this._SubItemID, row);
  }

  private int? GetInventoryID(PXCache cache, object row)
  {
    return this.GetCurrentValue<int?>(cache, this._InventoryID, row);
  }

  private string GetUOM(PXCache cache, object row)
  {
    return this.GetCurrentValue<string>(cache, this._UOM, row);
  }

  private int? GetBAccountID(PXCache cache, object row)
  {
    if (!(this._BAccountID == (Type) null))
      return this.GetCurrentValue<int?>(cache, this._BAccountID, row);
    INPrimaryAlternateType? primaryAltType = this._PrimaryAltType;
    INPrimaryAlternateType primaryAlternateType = INPrimaryAlternateType.VPN;
    return !(primaryAltType.GetValueOrDefault() == primaryAlternateType & primaryAltType.HasValue) ? this.GetCurrentValue<int?>(cache, typeof (Customer.bAccountID)) : this.GetCurrentValue<int?>(cache, typeof (PX.Objects.AP.Vendor.bAccountID));
  }

  private TOut GetCurrentValue<TOut>(PXCache cache, Type field, object row)
  {
    return (TOut) cache.Graph.Caches[BqlCommand.GetItemType(field)].GetValue(row, field.Name);
  }

  private TOut GetCurrentValue<TOut>(PXCache cache, Type field)
  {
    PXCache cach = cache.Graph.Caches[BqlCommand.GetItemType(field)];
    return (TOut) cach.GetValue(cach.Current, field.Name);
  }

  private bool IsChanged(PXCache cache, Type fieldSource, object row, object newrow)
  {
    return fieldSource != (Type) null && BqlCommand.GetItemType(fieldSource).IsAssignableFrom(cache.GetItemType()) && this.IsChanged(cache, fieldSource.Name, row, newrow);
  }

  private bool IsChanged(PXCache cache, string fieldName, object row, object newrow)
  {
    return !object.Equals(cache.GetValue(newrow, fieldName), cache.GetValue(row, fieldName));
  }

  private AlternateIDOnChangeAction GetOnChangeAction(PXGraph caller)
  {
    if (!this._OnChangeAction.HasValue)
      this._OnChangeAction = new AlternateIDOnChangeAction?(AlternateIDOnChangeAction.AskUser);
    return this._OnChangeAction.Value;
  }
}
