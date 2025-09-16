// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.CostCodeAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using CommonServiceLocator;
using PX.Data;
using PX.Objects.CN.ProjectAccounting.AP.CacheExtensions;
using PX.Objects.CS;
using System;
using System.Collections.Generic;
using System.ComponentModel;

#nullable disable
namespace PX.Objects.PM;

[PXDBInt]
[PXInt]
[PXUIField(DisplayName = "Cost Code", FieldClass = "COSTCODE")]
public class CostCodeAttribute : 
  PXEntityAttribute,
  IPXRowPersistingSubscriber,
  IPXFieldVerifyingSubscriber,
  IPXRowSelectedSubscriber
{
  public const string COSTCODE = "COSTCODE";
  protected Type task;

  public bool AllowNullValue { get; set; }

  public bool SkipVerification { get; set; }

  public bool SkipVerificationForDefault { get; set; }

  public bool AllowNullValueIfReleased { get; set; }

  public Type ReleasedField { get; set; }

  /// <summary>
  /// If you want to use new default-handling logic that is based on ProjectTask, InventoryItem, and Vendor.
  /// </summary>
  public bool UseNewDefaulting { get; set; }

  /// <summary>
  /// If you want to erase the cost code for an empty project.
  /// </summary>
  public Type ProjectField { get; set; }

  /// <summary>
  /// If you want to set the default cost code according to the item.
  /// </summary>
  public Type InventoryField { get; set; }

  /// <summary>
  /// If you want to set the default cost code according to the vendor.
  /// </summary>
  public Type VendorField { get; set; }

  public CostCodeAttribute()
    : this((Type) null, (Type) null, (string) null)
  {
  }

  public CostCodeAttribute(Type account, Type task)
    : this(account, task, (string) null)
  {
  }

  public CostCodeAttribute(Type account, Type task, string budgetType)
    : this(account, task, budgetType, (Type) null)
  {
  }

  public CostCodeAttribute(Type account, Type task, string budgetType, Type accountGroup)
    : this(account, task, budgetType, accountGroup, false)
  {
  }

  public CostCodeAttribute(
    Type account,
    Type task,
    string budgetType,
    Type accountGroup,
    bool disableProjectSpecific,
    bool useNewDefaulting = false)
  {
    this.task = task;
    ((PXAggregateAttribute) this)._Attributes.Add((PXEventSubscriberAttribute) new CostCodeDimensionSelectorAttribute(account, task, budgetType, accountGroup, disableProjectSpecific));
    this._SelAttrIndex = ((List<PXEventSubscriberAttribute>) ((PXAggregateAttribute) this)._Attributes).Count - 1;
    this.UseNewDefaulting = useNewDefaulting;
  }

  public static bool UseCostCode() => PXAccess.FeatureInstalled<FeaturesSet.costCodes>();

  public static int GetDefaultCostCode() => CostCodeAttribute.DefaultCostCode.GetValueOrDefault();

  public static int? DefaultCostCode
  {
    get => ServiceLocator.Current.GetInstance<ICostCodeManager>().DefaultCostCodeID;
  }

  public virtual void CacheAttached(PXCache sender)
  {
    ((PXAggregateAttribute) this).CacheAttached(sender);
    if (this.ProjectField != (Type) null)
    {
      PXGraph.FieldUpdatedEvents fieldUpdated = sender.Graph.FieldUpdated;
      Type itemType = sender.GetItemType();
      string name = this.ProjectField.Name;
      CostCodeAttribute costCodeAttribute = this;
      // ISSUE: virtual method pointer
      PXFieldUpdated pxFieldUpdated = new PXFieldUpdated((object) costCodeAttribute, __vmethodptr(costCodeAttribute, OnProjectUpdated));
      fieldUpdated.AddHandler(itemType, name, pxFieldUpdated);
    }
    // ISSUE: method pointer
    sender.Graph.FieldDefaulting.AddHandler(sender.GetItemType(), ((PXEventSubscriberAttribute) this)._FieldName, new PXFieldDefaulting((object) this, __methodptr(FieldDefaulting)));
  }

  protected virtual void OnProjectUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    object row = e.Row;
    if (row == null || !this.IsCostCodeClearingRequired(sender, row))
      return;
    sender.SetValueExt(row, ((PXEventSubscriberAttribute) this)._FieldName, (object) null);
  }

  protected virtual bool IsCostCodeClearingRequired(PXCache sender, object row)
  {
    if (this.ProjectField != (Type) null)
    {
      object projectID = sender.GetValue(row, this.ProjectField.Name);
      if (projectID == null || ProjectDefaultAttribute.IsNonProject((int?) projectID))
        return true;
    }
    return false;
  }

  public void FieldDefaulting(PXCache sender, PXFieldDefaultingEventArgs e)
  {
    if (((CancelEventArgs) e).Cancel)
      return;
    object row = e.Row;
    if (this.UseNewDefaulting && row != null && !sender.Graph.IsCopyPasteContext)
    {
      PXFieldState stateExt = sender.GetStateExt(row, ((PXEventSubscriberAttribute) this)._FieldName) as PXFieldState;
      if (CostCodeAttribute.UseCostCode() && !stateExt.IsReadOnly && !this.IsCostCodeClearingRequired(sender, row))
      {
        int? taskID = this.task?.Name != null ? (int?) sender.GetValue(row, this.task.Name) : new int?();
        int? nullable1;
        if (taskID.HasValue)
        {
          PMTask pmTask = PMTask.PK.Find(sender.Graph, (int?) sender.GetValue(row, this.ProjectField.Name), taskID);
          nullable1 = pmTask.DefaultCostCodeID;
          if (nullable1.HasValue)
          {
            e.NewValue = (object) pmTask.DefaultCostCodeID;
            ((CancelEventArgs) e).Cancel = true;
            return;
          }
        }
        int? nullable2;
        if (this.InventoryField?.Name == null)
        {
          nullable1 = new int?();
          nullable2 = nullable1;
        }
        else
          nullable2 = (int?) sender.GetValue(row, this.InventoryField.Name);
        int? inventoryID = nullable2;
        if (inventoryID.HasValue)
        {
          PX.Objects.IN.InventoryItem inventoryItem = PX.Objects.IN.InventoryItem.PK.Find(sender.Graph, inventoryID);
          nullable1 = inventoryItem.DefaultCostCodeID;
          if (nullable1.HasValue)
          {
            e.NewValue = (object) inventoryItem.DefaultCostCodeID;
            ((CancelEventArgs) e).Cancel = true;
            return;
          }
        }
        int? nullable3;
        if (this.VendorField?.Name == null)
        {
          nullable1 = new int?();
          nullable3 = nullable1;
        }
        else
          nullable3 = (int?) sender.GetValue(row, this.VendorField.Name);
        int? bAccountID = nullable3;
        if (bAccountID.HasValue)
        {
          VendorExt extension = PXCache<PX.Objects.AP.Vendor>.GetExtension<VendorExt>(PX.Objects.AP.Vendor.PK.Find(sender.Graph, bAccountID));
          int? nullable4;
          if (extension == null)
          {
            nullable1 = new int?();
            nullable4 = nullable1;
          }
          else
            nullable4 = extension.VendorDefaultCostCodeId;
          int? nullable5 = nullable4;
          if (nullable5.HasValue)
          {
            e.NewValue = (object) nullable5;
            ((CancelEventArgs) e).Cancel = true;
            return;
          }
        }
      }
    }
    e.NewValue = e.NewValue ?? sender.GetValue(row, ((PXEventSubscriberAttribute) this)._FieldName);
  }

  public void RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    if (this.task == (Type) null || this.AllowNullValue || !CostCodeAttribute.UseCostCode() || !((int?) sender.GetValue(e.Row, this.task.Name)).HasValue || this.AllowNullValueIfReleased && this.ReleasedField != (Type) null && ((bool?) sender.GetValue(e.Row, this.ReleasedField.Name)).GetValueOrDefault() || ((int?) sender.GetValue(e.Row, ((PXEventSubscriberAttribute) this).FieldOrdinal)).HasValue)
      return;
    if (sender.RaiseExceptionHandling(this.FieldName, e.Row, (object) null, (Exception) new PXSetPropertyException("'{0}' cannot be empty.", new object[1]
    {
      (object) $"[{this.FieldName}]"
    })))
      throw new PXRowPersistingException(this.FieldName, (object) null, "'{0}' cannot be empty.", new object[1]
      {
        (object) this.FieldName
      });
  }

  public void FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
  {
    if (!(this.task != (Type) null) || this.SkipVerification || e.NewValue == null || !CostCodeAttribute.UseCostCode())
      return;
    int newValue = (int) e.NewValue;
    if (this.SkipVerificationForDefault && newValue == CostCodeAttribute.GetDefaultCostCode())
      return;
    PMCostCode pmCostCode = PMCostCode.PK.Find(sender.Graph, new int?(newValue));
    int num;
    if (pmCostCode == null)
    {
      num = 0;
    }
    else
    {
      bool? isActive = pmCostCode.IsActive;
      bool flag = false;
      num = isActive.GetValueOrDefault() == flag & isActive.HasValue ? 1 : 0;
    }
    if (num == 0)
      return;
    string codeInactiveWarning = this.GetCostCodeInactiveWarning(new int?(newValue), sender, e.Row);
    sender.RaiseExceptionHandling(this.FieldName, e.Row, e.NewValue, (Exception) new PXSetPropertyException(codeInactiveWarning, (PXErrorLevel) 2));
  }

  protected virtual string GetCostCodeInactiveWarning(int? costCodeID, PXCache cache, object row)
  {
    PMCostCode pmCostCode = PMCostCode.PK.Find(cache.Graph, costCodeID);
    string str = CostCodeSelectorAttribute.FormatValueByMask(cache, row, this.FieldName, pmCostCode?.CostCodeCD);
    return string.Format(PXLocalizer.Localize("The {0} cost code is inactive."), (object) str);
  }

  public void RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    if (this.SkipVerification || e.Row == null || !CostCodeAttribute.UseCostCode())
      return;
    int? costCodeID = (int?) sender.GetValue(e.Row, ((PXEventSubscriberAttribute) this)._FieldName);
    if (!costCodeID.HasValue || this.ReleasedField != (Type) null && ((bool?) sender.GetValue(e.Row, this.ReleasedField.Name)).GetValueOrDefault())
      return;
    if (this.SkipVerificationForDefault)
    {
      int? nullable = costCodeID;
      int defaultCostCode = CostCodeAttribute.GetDefaultCostCode();
      if (nullable.GetValueOrDefault() == defaultCostCode & nullable.HasValue)
        return;
    }
    bool? isActive = PMCostCode.PK.Find(sender.Graph, costCodeID).IsActive;
    bool flag = false;
    if (!(isActive.GetValueOrDefault() == flag & isActive.HasValue))
      return;
    string codeInactiveWarning = this.GetCostCodeInactiveWarning(new int?(costCodeID.Value), sender, e.Row);
    sender.RaiseExceptionHandling(this.FieldName, e.Row, (object) costCodeID.Value, (Exception) new PXSetPropertyException(codeInactiveWarning, (PXErrorLevel) 2));
  }
}
