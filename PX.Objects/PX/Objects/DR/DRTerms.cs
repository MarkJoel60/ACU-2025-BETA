// Decompiled with JetBrains decompiler
// Type: PX.Objects.DR.DRTerms
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.IN;
using System;
using System.Collections;
using System.Linq;

#nullable disable
namespace PX.Objects.DR;

public class DRTerms
{
  public const string Day = "D";
  public const string Week = "W";
  public const string Month = "M";
  public const string Year = "Y";

  public static DRTerms.Term GetDefaultItemTerms(PXGraph graph, InventoryItem item)
  {
    if (item == null || item.DeferredCode == null)
      return (DRTerms.Term) null;
    DRDeferredCode drDeferredCode1 = (DRDeferredCode) PXSelectorAttribute.Select<InventoryItem.deferredCode>(graph.Caches[typeof (InventoryItem)], (object) item);
    if (drDeferredCode1 == null)
      return (DRTerms.Term) null;
    bool? deliverableArrangement = drDeferredCode1.MultiDeliverableArrangement;
    bool flag = false;
    if (deliverableArrangement.GetValueOrDefault() == flag & deliverableArrangement.HasValue && DeferredMethodType.RequiresTerms(drDeferredCode1.Method))
      return new DRTerms.Term(item.DefaultTerm, item.DefaultTermUOM);
    if (drDeferredCode1.MultiDeliverableArrangement.GetValueOrDefault())
    {
      PXResultset<INComponent> pxResultset = PXSelectBase<INComponent, PXSelect<INComponent, Where<INComponent.inventoryID, Equal<Required<INComponent.inventoryID>>>>.Config>.Select(graph, new object[1]
      {
        (object) item.InventoryID
      });
      pxResultset.Sort(new Comparison<PXResult<INComponent>>(DRTerms.SortDecendingByTermUOM));
      foreach (PXResult<INComponent> pxResult in pxResultset)
      {
        INComponent inComponent = PXResult<INComponent>.op_Implicit(pxResult);
        DRDeferredCode drDeferredCode2 = (DRDeferredCode) PXSelectorAttribute.Select<INComponent.deferredCode>(graph.Caches[typeof (INComponent)], (object) inComponent);
        if (drDeferredCode2 != null && DeferredMethodType.RequiresTerms(drDeferredCode2.Method))
          return new DRTerms.Term(inComponent.DefaultTerm, inComponent.DefaultTermUOM);
      }
    }
    return (DRTerms.Term) null;
  }

  private static int SortOrder(string termUOM)
  {
    switch (termUOM)
    {
      case "D":
        return 0;
      case "W":
        return 1;
      case "M":
        return 2;
      case "Y":
        return 3;
      default:
        return 0;
    }
  }

  private static int SortDecendingByTermUOM(PXResult<INComponent> a, PXResult<INComponent> b)
  {
    INComponent inComponent1 = PXResult<INComponent>.op_Implicit(a);
    INComponent inComponent2 = PXResult<INComponent>.op_Implicit(b);
    int num = ((IComparable) DRTerms.SortOrder(inComponent2.DefaultTermUOM)).CompareTo((object) DRTerms.SortOrder(inComponent1.DefaultTermUOM));
    return num != 0 ? num : ((IComparable) inComponent2.DefaultTerm).CompareTo((object) inComponent1.DefaultTerm);
  }

  public class UOMListAttribute : PXStringListAttribute
  {
    public UOMListAttribute()
      : base(new string[4]{ "D", "W", "M", "Y" }, new string[4]
      {
        "day(s)",
        "week(s)",
        "month(s)",
        "year(s)"
      })
    {
    }
  }

  public class Term(Decimal? term, string uom) : Tuple<Decimal?, string>(term, uom)
  {
    public Decimal? Length => this.Item1;

    public string UOM => this.Item2;

    public DateTime? Delay(DateTime? origin)
    {
      if (!origin.HasValue)
        return new DateTime?();
      DateTime dateTime1 = origin.Value;
      int months = (int) this.Length.Value;
      if (months == 0)
        return new DateTime?(dateTime1);
      DateTime dateTime2;
      switch (this.UOM)
      {
        case "D":
          dateTime2 = dateTime1.AddDays((double) months);
          break;
        case "W":
          dateTime2 = dateTime1.AddDays((double) (7 * months));
          break;
        case "M":
          dateTime2 = dateTime1.AddMonths(months);
          break;
        case "Y":
          dateTime2 = dateTime1.AddYears(months);
          break;
        default:
          return new DateTime?();
      }
      return new DateTime?(dateTime2.AddDays(-1.0));
    }
  }

  /// <summary>
  /// The attribute sets and updates the underlying field that specifies
  /// whether the deferral terms are required for the current item.
  /// If <see cref="P:PX.Objects.DR.DRTerms.DatesAttribute.VerifyDatesPresent" /> property is set, the attribute verifies
  /// that the Term Start Date and Term End Date are present. If they are present,
  /// ensures that they are consistent with each other.
  /// Subscribes to the Field Updated event of Inventory ID, Start Date, and Deferral Code
  /// to set default dates and update the underlying field.
  /// </summary>
  public class DatesAttribute : 
    PXEventSubscriberAttribute,
    IPXRowSelectedSubscriber,
    IPXRowSelectingSubscriber,
    IPXRowPersistingSubscriber
  {
    private Type _StartDateField;
    private Type _EndDateField;
    private Type _InventoryField;
    private Type _DeferralCodeField;
    private Type _Hold;

    public bool VerifyDatesPresent { get; set; }

    public DatesAttribute(
      Type startDateField,
      Type endDateField,
      Type inventoryField,
      Type deferralCodeField,
      Type hold = null)
    {
      this._StartDateField = startDateField;
      this._EndDateField = endDateField;
      this._InventoryField = inventoryField;
      this._DeferralCodeField = deferralCodeField;
      this._Hold = hold;
      this.VerifyDatesPresent = true;
    }

    public DatesAttribute(Type startDateField, Type endDateField, Type inventoryField)
      : this(startDateField, endDateField, inventoryField, (Type) null)
    {
    }

    public DatesAttribute(
      Type startDateField,
      Type endDateField,
      Type inventoryField,
      Type deferralCodeField)
      : this(startDateField, endDateField, inventoryField, deferralCodeField, (Type) null)
    {
    }

    public virtual void CacheAttached(PXCache sender)
    {
      base.CacheAttached(sender);
      PXGraph.FieldUpdatedEvents fieldUpdated1 = sender.Graph.FieldUpdated;
      Type bqlTable1 = this._BqlTable;
      string name1 = this._InventoryField.Name;
      DRTerms.DatesAttribute datesAttribute1 = this;
      // ISSUE: virtual method pointer
      PXFieldUpdated pxFieldUpdated1 = new PXFieldUpdated((object) datesAttribute1, __vmethodptr(datesAttribute1, InventoryOrCodeUpdated));
      fieldUpdated1.AddHandler(bqlTable1, name1, pxFieldUpdated1);
      PXGraph.FieldUpdatedEvents fieldUpdated2 = sender.Graph.FieldUpdated;
      Type bqlTable2 = this._BqlTable;
      string name2 = this._StartDateField.Name;
      DRTerms.DatesAttribute datesAttribute2 = this;
      // ISSUE: virtual method pointer
      PXFieldUpdated pxFieldUpdated2 = new PXFieldUpdated((object) datesAttribute2, __vmethodptr(datesAttribute2, StartDateUpdated));
      fieldUpdated2.AddHandler(bqlTable2, name2, pxFieldUpdated2);
      if (!(this._DeferralCodeField != (Type) null))
        return;
      PXGraph.FieldUpdatedEvents fieldUpdated3 = sender.Graph.FieldUpdated;
      Type bqlTable3 = this._BqlTable;
      string name3 = this._DeferralCodeField.Name;
      DRTerms.DatesAttribute datesAttribute3 = this;
      // ISSUE: virtual method pointer
      PXFieldUpdated pxFieldUpdated3 = new PXFieldUpdated((object) datesAttribute3, __vmethodptr(datesAttribute3, InventoryOrCodeUpdated));
      fieldUpdated3.AddHandler(bqlTable3, name3, pxFieldUpdated3);
    }

    protected virtual void InventoryOrCodeUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
    {
      if (e.Row == null)
        return;
      this.UpdateRequiresTerms(sender, e.Row);
      InventoryItem inventoryItem = (InventoryItem) PXSelectorAttribute.Select(sender, e.Row, sender.GetField(this._InventoryField));
      DRTerms.Term defaultItemTerms = DRTerms.GetDefaultItemTerms(sender.Graph, inventoryItem);
      this.SetDefaultDates(sender, e.Row, defaultItemTerms);
    }

    protected virtual void StartDateUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
    {
      if (e.Row == null)
        return;
      DateTime? origin = (DateTime?) sender.GetValue(e.Row, this._StartDateField.Name);
      if (!origin.HasValue)
        return;
      InventoryItem inventoryItem = (InventoryItem) PXSelectorAttribute.Select(sender, e.Row, sender.GetField(this._InventoryField));
      DateTime? nullable = (DateTime?) DRTerms.GetDefaultItemTerms(sender.Graph, inventoryItem)?.Delay(origin);
      sender.SetValueExt(e.Row, this._EndDateField.Name, (object) nullable);
    }

    public void RowSelecting(PXCache sender, PXRowSelectingEventArgs e)
    {
      if (e.Row == null)
        return;
      bool flag = false;
      int? nullable = (int?) sender.GetValue(e.Row, this._InventoryField.Name);
      using (new PXConnectionScope())
      {
        if (this._DeferralCodeField == (Type) null)
        {
          InventoryItem inventoryItem = (InventoryItem) PXSelectorAttribute.Select(sender, e.Row, sender.GetField(this._InventoryField));
          flag = DRTerms.GetDefaultItemTerms(sender.Graph, inventoryItem) != null;
        }
        else
        {
          string code = (string) sender.GetValue(e.Row, this._DeferralCodeField.Name);
          flag = this.RequireTerms(sender, e.Row, code);
        }
      }
      sender.SetValue(e.Row, this._FieldOrdinal, (object) flag);
    }

    protected virtual void UpdateRequiresTerms(PXCache cache, object row)
    {
      int? nullable = (int?) cache.GetValue(row, this._InventoryField.Name);
      bool flag;
      if (this._DeferralCodeField == (Type) null)
      {
        InventoryItem inventoryItem = (InventoryItem) PXSelectorAttribute.Select(cache, row, cache.GetField(this._InventoryField));
        flag = DRTerms.GetDefaultItemTerms(cache.Graph, inventoryItem) != null;
      }
      else
      {
        string code = (string) cache.GetValue(row, this._DeferralCodeField.Name);
        flag = this.RequireTerms(cache, row, code);
      }
      cache.SetValue(row, this._FieldOrdinal, (object) flag);
    }

    protected virtual bool RequireTerms(PXCache sender, object row, string code)
    {
      if (code == null)
        return false;
      DRDeferredCode drDeferredCode = PXResultset<DRDeferredCode>.op_Implicit(PXSelectBase<DRDeferredCode, PXSelect<DRDeferredCode, Where<DRDeferredCode.deferredCodeID, Equal<Required<DRDeferredCode.deferredCodeID>>>>.Config>.Select(sender.Graph, new object[1]
      {
        (object) code
      }));
      if (drDeferredCode == null)
        return false;
      bool? deliverableArrangement = drDeferredCode.MultiDeliverableArrangement;
      bool flag = false;
      if (deliverableArrangement.GetValueOrDefault() == flag & deliverableArrangement.HasValue)
        return DeferredMethodType.RequiresTerms(drDeferredCode.Method);
      InventoryItem inventoryItem = (InventoryItem) PXSelectorAttribute.Select(sender, row, sender.GetField(this._InventoryField));
      return DRTerms.GetDefaultItemTerms(sender.Graph, inventoryItem) != null;
    }

    protected virtual void SetDefaultDates(PXCache cache, object row, DRTerms.Term term)
    {
      if (term != null)
      {
        cache.SetDefaultExt(row, this._StartDateField.Name, (object) null);
        DateTime? origin = (DateTime?) cache.GetValue(row, this._StartDateField.Name);
        DateTime? nullable = origin.HasValue ? term.Delay(origin) : new DateTime?();
        cache.SetValue(row, this._EndDateField.Name, (object) nullable);
      }
      else
      {
        cache.SetValue(row, this._StartDateField.Name, (object) null);
        cache.SetValue(row, this._EndDateField.Name, (object) null);
      }
    }

    public void RowSelected(PXCache sender, PXRowSelectedEventArgs e)
    {
      if (e.Row == null)
        return;
      bool valueOrDefault = ((bool?) sender.GetValue(e.Row, this._FieldOrdinal)).GetValueOrDefault();
      PXUIFieldAttribute.SetEnabled(sender, e.Row, this._StartDateField.Name, valueOrDefault);
      PXUIFieldAttribute.SetEnabled(sender, e.Row, this._EndDateField.Name, valueOrDefault);
      if (!(this._Hold != (Type) null))
        return;
      Type itemType = BqlCommand.GetItemType(this._Hold);
      bool? nullable1 = (bool?) sender.GetValue(e.Row, this._FieldOrdinal);
      DateTime? nullable2 = (DateTime?) sender.GetValue(e.Row, this._StartDateField.Name);
      DateTime? nullable3 = (DateTime?) sender.GetValue(e.Row, this._EndDateField.Name);
      object obj = PXParentAttribute.SelectParent(sender, e.Row, itemType);
      if (obj != null)
      {
        int num = (bool) sender.Graph.Caches[itemType].GetValue(obj, this._Hold.Name) ? 0 : (nullable1.GetValueOrDefault() ? 1 : 0);
        bool flag1 = num != 0 && !nullable2.HasValue;
        bool flag2 = num != 0 && !nullable3.HasValue;
        sender.RaiseExceptionHandling(this._StartDateField.Name, e.Row, (object) nullable2, flag1 ? (Exception) new PXSetPropertyException("Term Start Date should be specified to generate deferral transactions.", (PXErrorLevel) 2) : (Exception) null);
        sender.RaiseExceptionHandling(this._EndDateField.Name, e.Row, (object) nullable3, flag2 ? (Exception) new PXSetPropertyException("Term End Date should be specified to generate deferral transactions.", (PXErrorLevel) 2) : (Exception) null);
      }
      if (nullable1.GetValueOrDefault() && nullable2.HasValue && nullable3.HasValue)
      {
        DateTime? nullable4 = nullable3;
        DateTime? nullable5 = nullable2;
        if ((nullable4.HasValue & nullable5.HasValue ? (nullable4.GetValueOrDefault() < nullable5.GetValueOrDefault() ? 1 : 0) : 0) != 0)
        {
          if (sender.RaiseExceptionHandling(this._EndDateField.Name, e.Row, (object) nullable3, (Exception) new PXSetPropertyException("Term End Date ({0:d}) cannot be earlier than Term Start Date ({1:d}).", new object[2]
          {
            (object) nullable3,
            (object) nullable2
          })))
            throw new PXRowPersistingException(this._EndDateField.Name, (object) nullable3, "Term End Date ({0:d}) cannot be earlier than Term Start Date ({1:d}).", new object[2]
            {
              (object) nullable3,
              (object) nullable2
            });
        }
      }
      if (!nullable2.HasValue && nullable3.HasValue && sender.RaiseExceptionHandling(this._StartDateField.Name, e.Row, (object) null, (Exception) new PXSetPropertyException("Term Start Date should be specified explicitly when Term End Date is present.")))
        throw new PXRowPersistingException(this._StartDateField.Name, (object) null, "Term Start Date should be specified explicitly when Term End Date is present.");
    }

    public void RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
    {
      bool? nullable1 = (bool?) sender.GetValue(e.Row, this._FieldOrdinal);
      DateTime? nullable2 = (DateTime?) sender.GetValue(e.Row, this._StartDateField.Name);
      DateTime? nullable3 = (DateTime?) sender.GetValue(e.Row, this._EndDateField.Name);
      if (nullable1.GetValueOrDefault() && nullable2.HasValue && nullable3.HasValue)
      {
        DateTime? nullable4 = nullable3;
        DateTime? nullable5 = nullable2;
        if ((nullable4.HasValue & nullable5.HasValue ? (nullable4.GetValueOrDefault() < nullable5.GetValueOrDefault() ? 1 : 0) : 0) != 0)
        {
          if (sender.RaiseExceptionHandling(this._EndDateField.Name, e.Row, (object) nullable3, (Exception) new PXSetPropertyException("Term End Date ({0:d}) cannot be earlier than Term Start Date ({1:d}).", new object[2]
          {
            (object) nullable3,
            (object) nullable2
          })))
            throw new PXRowPersistingException(this._EndDateField.Name, (object) nullable3, "Term End Date ({0:d}) cannot be earlier than Term Start Date ({1:d}).", new object[2]
            {
              (object) nullable3,
              (object) nullable2
            });
        }
      }
      if (!nullable2.HasValue && nullable3.HasValue && sender.RaiseExceptionHandling(this._StartDateField.Name, e.Row, (object) null, (Exception) new PXSetPropertyException("Term Start Date should be specified explicitly when Term End Date is present.")))
        throw new PXRowPersistingException(this._StartDateField.Name, (object) null, "Term Start Date should be specified explicitly when Term End Date is present.");
      PXPersistingCheck pxPersistingCheck = !((bool?) sender.GetValue(e.Row, this._FieldOrdinal)).GetValueOrDefault() || !this.VerifyDatesPresent ? (PXPersistingCheck) 2 : (PXPersistingCheck) 1;
      PXDefaultAttribute.SetPersistingCheck(sender, this._StartDateField.Name, e.Row, pxPersistingCheck);
      PXDefaultAttribute.SetPersistingCheck(sender, this._EndDateField.Name, e.Row, pxPersistingCheck);
    }
  }

  public class VerifyResidualAttribute : 
    PXEventSubscriberAttribute,
    IPXRowSelectedSubscriber,
    IPXRowSelectingSubscriber
  {
    private Type _InventoryField;
    private Type _DeferralCodeField;
    private Type _drUnitPriceField;
    private Type _amountField;

    public VerifyResidualAttribute(
      Type inventoryField,
      Type deferralCodeField,
      Type drUnitPriceField,
      Type amountField)
    {
      this._InventoryField = inventoryField;
      this._DeferralCodeField = deferralCodeField;
      this._drUnitPriceField = drUnitPriceField;
      this._amountField = amountField;
    }

    public virtual void CacheAttached(PXCache sender)
    {
      base.CacheAttached(sender);
      PXGraph.FieldUpdatedEvents fieldUpdated1 = sender.Graph.FieldUpdated;
      Type bqlTable1 = this._BqlTable;
      string name1 = this._InventoryField.Name;
      DRTerms.VerifyResidualAttribute residualAttribute1 = this;
      // ISSUE: virtual method pointer
      PXFieldUpdated pxFieldUpdated1 = new PXFieldUpdated((object) residualAttribute1, __vmethodptr(residualAttribute1, InventoryOrCodeUpdated));
      fieldUpdated1.AddHandler(bqlTable1, name1, pxFieldUpdated1);
      if (!(this._DeferralCodeField != (Type) null))
        return;
      PXGraph.FieldUpdatedEvents fieldUpdated2 = sender.Graph.FieldUpdated;
      Type bqlTable2 = this._BqlTable;
      string name2 = this._DeferralCodeField.Name;
      DRTerms.VerifyResidualAttribute residualAttribute2 = this;
      // ISSUE: virtual method pointer
      PXFieldUpdated pxFieldUpdated2 = new PXFieldUpdated((object) residualAttribute2, __vmethodptr(residualAttribute2, InventoryOrCodeUpdated));
      fieldUpdated2.AddHandler(bqlTable2, name2, pxFieldUpdated2);
    }

    protected virtual void InventoryOrCodeUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
    {
      if (e.Row == null)
        return;
      this.UpdateHasResidual(sender, e.Row);
    }

    public void RowSelecting(PXCache sender, PXRowSelectingEventArgs e)
    {
      if (e.Row == null)
        return;
      using (new PXConnectionScope())
        this.UpdateHasResidual(sender, e.Row);
    }

    protected virtual void UpdateHasResidual(PXCache cache, object row)
    {
      bool flag = (!(this._DeferralCodeField != (Type) null) || cache.GetValue(row, this._DeferralCodeField.Name) != null) && this.HasResidual(cache, row);
      cache.SetValue(row, this._FieldOrdinal, (object) flag);
    }

    public void RowSelected(PXCache sender, PXRowSelectedEventArgs e)
    {
      if (e.Row == null)
        return;
      int num = ((bool?) sender.GetValue(e.Row, this._FieldOrdinal)).GetValueOrDefault() ? 1 : 0;
      Decimal? nullable1 = (Decimal?) sender.GetValue(e.Row, this._drUnitPriceField.Name);
      Decimal? nullable2 = (Decimal?) sender.GetValue(e.Row, this._amountField.Name);
      if (num != 0 && (nullable1 ?? 0.0M) == 0.0M && (nullable2 ?? 0.0M) != 0.0M)
      {
        sender.RaiseExceptionHandling(this._InventoryField.Name, e.Row, sender.GetValue(e.Row, this._InventoryField.Name), (Exception) new PXSetPropertyException("There is no price defined for the item. Residual component won't be used when generating a deferral schedule - amount will be allocated between Fixed Amount and Percentage components only.", (PXErrorLevel) 2));
      }
      else
      {
        if (!(PXUIFieldAttribute.GetError(sender, e.Row, this._InventoryField.Name) == PXMessages.LocalizeNoPrefix("There is no price defined for the item. Residual component won't be used when generating a deferral schedule - amount will be allocated between Fixed Amount and Percentage components only.")))
          return;
        sender.RaiseExceptionHandling(this._InventoryField.Name, e.Row, sender.GetValue(e.Row, this._InventoryField.Name), (Exception) null);
      }
    }

    private bool HasResidual(PXCache sender, object row)
    {
      InventoryItem inventoryItem = (InventoryItem) PXSelectorAttribute.Select(sender, row, sender.GetField(this._InventoryField));
      if (inventoryItem == null || !inventoryItem.IsSplitted.GetValueOrDefault())
        return false;
      return GraphHelper.RowCast<INComponent>((IEnumerable) PXSelectBase<INComponent, PXSelect<INComponent, Where<INComponent.inventoryID, Equal<Required<INComponent.inventoryID>>>>.Config>.Select(sender.Graph, new object[1]
      {
        (object) inventoryItem.InventoryID
      })).Any<INComponent>((Func<INComponent, bool>) (c => c.AmtOption == "R"));
    }
  }
}
