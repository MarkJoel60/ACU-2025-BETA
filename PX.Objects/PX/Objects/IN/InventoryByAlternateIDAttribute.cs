// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.InventoryByAlternateIDAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.IN;

[PXDBInt]
[PXUIField(DisplayName = "Inventory ID")]
public class InventoryByAlternateIDAttribute : PXDimensionSelectorAttribute
{
  public InventoryByAlternateIDAttribute(
    Type bAccount,
    Type alternateID,
    Type alternateType,
    Type restrictInventoryByAlternateID)
    : base("INVENTORY")
  {
    InventoryByAlternateIDAttribute.CustomSelectorAttribute selectorAttribute = new InventoryByAlternateIDAttribute.CustomSelectorAttribute(bAccount, alternateID, alternateType, restrictInventoryByAlternateID);
    this.RegisterSelector((PXSelectorAttribute) selectorAttribute);
    this.DescriptionField = ((PXSelectorAttribute) selectorAttribute).DescriptionField;
  }

  public class CustomSelectorAttribute : PXCustomSelectorAttribute
  {
    protected Type BAccountField { get; }

    protected Type AlternateIDField { get; }

    protected Type AlternateTypeField { get; }

    protected Type RestrictByAlternateIDField { get; }

    public CustomSelectorAttribute(
      Type bAccount,
      Type alternateID,
      Type alternateType,
      Type restrictInventoryByAlternateID)
      : this()
    {
      this.BAccountField = bAccount;
      this.AlternateIDField = alternateID;
      this.AlternateTypeField = alternateType;
      this.RestrictByAlternateIDField = restrictInventoryByAlternateID;
    }

    protected CustomSelectorAttribute()
      : base(typeof (SearchFor<InventoryItem.inventoryID>.In<SelectFrom<InventoryItem, TypeArrayOf<IFbqlJoin>.Empty>.LeftJoin<INItemXRef>.On<INItemXRef.FK.InventoryItem>>), new Type[10]
      {
        typeof (InventoryItem.inventoryCD),
        typeof (InventoryItem.descr),
        typeof (InventoryItem.itemClassID),
        typeof (InventoryItem.itemStatus),
        typeof (InventoryItem.itemType),
        typeof (InventoryItem.baseUnit),
        typeof (InventoryItem.salesUnit),
        typeof (InventoryItem.purchaseUnit),
        typeof (InventoryItem.basePrice),
        typeof (INItemXRef.uOM)
      })
    {
      ((PXSelectorAttribute) this).ValidateValue = false;
      ((PXSelectorAttribute) this).SuppressUnconditionalSelect = true;
      ((PXSelectorAttribute) this).SubstituteKey = typeof (InventoryItem.inventoryCD);
      ((PXSelectorAttribute) this).DescriptionField = typeof (InventoryItem.descr);
    }

    public virtual void CacheAttached(PXCache sender)
    {
      PXUIFieldAttribute.SetDisplayName<INItemXRef.uOM>((PXCache) GraphHelper.Caches<INItemXRef>(sender.Graph), "Alt. ID Unit");
      base.CacheAttached(sender);
    }

    protected virtual BqlCommand BuildQuery(PXCache cache, object row, out object[] parameters)
    {
      BqlCommand bqlCommand;
      if (((bool?) cache.GetValue(row, this.RestrictByAlternateIDField.Name)).GetValueOrDefault())
      {
        bqlCommand = (BqlCommand) new SelectFromBase<InventoryItem, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<INItemXRef>.On<INItemXRef.FK.InventoryItem>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INItemXRef.alternateID, Equal<P.AsString>>>>>.And<Brackets<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INItemXRef.alternateType, Equal<P.AsString>>>>, And<BqlOperand<INItemXRef.bAccountID, IBqlInt>.IsEqual<P.AsInt>>>>.Or<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INItemXRef.alternateType, NotEqual<INAlternateType.cPN>>>>>.And<BqlOperand<INItemXRef.alternateType, IBqlString>.IsNotEqual<INAlternateType.vPN>>>>>>.AggregateTo<GroupBy<InventoryItem.inventoryID>, Max<INItemXRef.uOM>>();
        parameters = new object[3]
        {
          cache.GetValue(row, this.AlternateIDField.Name),
          cache.GetValue(row, this.AlternateTypeField.Name),
          cache.GetValue(row, this.BAccountField.Name)
        };
      }
      else
      {
        bqlCommand = (BqlCommand) new SelectFrom<InventoryItem>();
        parameters = Array<object>.Empty;
      }
      return bqlCommand.WhereAnd<Where<BqlChainableConditionLite<Match<Current<AccessInfo.userName>>>.And<BqlOperand<InventoryItem.itemStatus, IBqlString>.IsNotIn<InventoryItemStatus.unknown, InventoryItemStatus.inactive, InventoryItemStatus.markedForDeletion>>>>();
    }

    protected virtual IEnumerable GetRecords()
    {
      PXCache cach = this._Graph.Caches[((PXEventSubscriberAttribute) this).BqlTable];
      object current = cach.Current;
      if (current == null)
        return (IEnumerable) Array<InventoryItem>.Empty;
      if (PXView.Filters.Length == 1)
      {
        PXFilterRow pxFilterRow = ((IEnumerable) PXView.Filters).OfType<PXFilterRow>().FirstOrDefault<PXFilterRow>((Func<PXFilterRow, bool>) (x => x.DataField.Equals("inventoryID", StringComparison.InvariantCultureIgnoreCase) && EnumerableExtensions.IsIn<PXCondition>(x.Condition, (PXCondition) 11, (PXCondition) 0)));
        if (pxFilterRow != null)
        {
          if (pxFilterRow.Value == null)
            return (IEnumerable) Array<InventoryItem>.Empty;
          InventoryItem inventoryItem = InventoryItem.PK.Find(this._Graph, new int?(Convert.ToInt32(pxFilterRow.Value)));
          if (inventoryItem != null)
            return (IEnumerable) new InventoryItem[1]
            {
              inventoryItem
            };
        }
      }
      object[] parameters;
      BqlCommand bqlCommand = this.BuildQuery(cach, current, out parameters);
      PXView view = cach.Graph.TypedViews.GetView(bqlCommand, true);
      int startRow = PXView.StartRow;
      int num = 0;
      object[] currents = PXView.Currents;
      object[] objArray = parameters;
      object[] searches = PXView.Searches;
      string[] sortColumns = PXView.SortColumns;
      bool[] descendings = PXView.Descendings;
      PXFilterRow[] pxFilterRowArray = PXView.PXFilterRowCollection.op_Implicit(PXView.Filters);
      ref int local1 = ref startRow;
      int maximumRows = PXView.MaximumRows;
      ref int local2 = ref num;
      List<object> records = view.Select(currents, objArray, searches, sortColumns, descendings, pxFilterRowArray, ref local1, maximumRows, ref local2);
      PXView.StartRow = 0;
      return (IEnumerable) records;
    }

    public virtual void SubstituteKeyFieldUpdating(PXCache sender, PXFieldUpdatingEventArgs e)
    {
      if (e.NewValue == null)
        return;
      InventoryItem inventoryItem;
      if (!(e.NewValue is int))
        inventoryItem = PXResultset<InventoryItem>.op_Implicit(PXSelectBase<InventoryItem, PXViewOf<InventoryItem>.BasedOn<SelectFromBase<InventoryItem, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<InventoryItem.inventoryCD, IBqlString>.IsEqual<P.AsString>>>.ReadOnly.Config>.SelectWindowed(sender.Graph, 0, 1, new object[1]
        {
          e.NewValue
        }));
      else
        inventoryItem = InventoryItem.PK.Find(sender.Graph, (int?) e.NewValue);
      e.NewValue = (object) (inventoryItem ?? throw new PXSetPropertyException(PXMessages.LocalizeFormat("{0} '{1}' cannot be found in the system.", new object[2]
      {
        (object) ((PXEventSubscriberAttribute) this)._FieldName,
        e.NewValue
      }))).InventoryID;
    }

    public virtual void SubstituteKeyFieldSelecting(PXCache sender, PXFieldSelectingEventArgs e)
    {
      object returnValue = e.ReturnValue;
      e.ReturnValue = (object) null;
      ((PXSelectorAttribute) this).FieldSelecting(sender, e);
      InventoryItem inventoryItem = InventoryItem.PK.Find(sender.Graph, (int?) returnValue);
      if (inventoryItem != null)
      {
        e.ReturnValue = (object) inventoryItem.InventoryCD;
      }
      else
      {
        if (e.Row == null)
          return;
        e.ReturnValue = (object) null;
      }
    }
  }
}
