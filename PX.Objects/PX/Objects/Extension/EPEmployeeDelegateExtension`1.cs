// Decompiled with JetBrains decompiler
// Type: PX.Objects.Extension.EPEmployeeDelegateExtension`1
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.CS;
using PX.Objects.EP;
using System;
using System.Collections.Generic;

#nullable enable
namespace PX.Objects.Extension;

/// <exclude />
public abstract class EPEmployeeDelegateExtension<TGraph> : PXGraphExtension<
#nullable disable
TGraph> where TGraph : PXGraph
{
  public FbqlSelect<SelectFromBase<EPWingman, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<
  #nullable enable
  EPWingman.employeeID, IBqlInt>.IsEqual<
  #nullable disable
  BqlField<
  #nullable enable
  EPEmployee.bAccountID, IBqlInt>.FromCurrent>>.Order<
  #nullable disable
  By<BqlField<
  #nullable enable
  EPWingman.startsOn, IBqlDateTime>.Desc>>, 
  #nullable disable
  EPWingman>.View Delegates;
  [PXHidden]
  public FbqlSelect<SelectFromBase<EPWingman, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  EPWingman.delegationOf, 
  #nullable disable
  Equal<EPDelegationOf.approvals>>>>>.And<BqlOperand<
  #nullable enable
  EPWingman.employeeID, IBqlInt>.IsEqual<
  #nullable disable
  P.AsInt>>>, EPWingman>.View DelegatesForApprovals;

  protected static bool IsExtensionActive()
  {
    return PXAccess.FeatureInstalled<FeaturesSet.expenseManagement>() || PXAccess.FeatureInstalled<FeaturesSet.financialAdvanced>() || PXAccess.FeatureInstalled<FeaturesSet.timeReportingModule>() || PXAccess.FeatureInstalled<FeaturesSet.approvalWorkflow>();
  }

  public virtual void Initialize()
  {
    ((PXGraphExtension) this).Initialize();
    List<string> stringList1 = new List<string>();
    List<string> stringList2 = new List<string>();
    if (PXAccess.FeatureInstalled<FeaturesSet.approvalWorkflow>())
    {
      stringList1.Add("A");
      stringList2.Add("Approvals");
    }
    if (PXAccess.FeatureInstalled<FeaturesSet.expenseManagement>())
    {
      stringList1.Add("E");
      stringList2.Add("Expense Receipts and Claims");
    }
    if (PXAccess.FeatureInstalled<FeaturesSet.financialAdvanced>() || PXAccess.FeatureInstalled<FeaturesSet.timeReportingModule>())
    {
      stringList1.Add("T");
      stringList2.Add("Time Activities and Employee Time Cards");
    }
    PXStringListAttribute.SetList<EPWingman.delegationOf>(((PXSelectBase) this.Delegates).Cache, (object) null, stringList1.ToArray(), stringList2.ToArray());
  }

  protected virtual void _(
    Events.FieldUpdated<EPWingman, EPWingman.delegationOf> e)
  {
    object newValue = e.NewValue;
    if ((newValue != null ? (EnumerableExtensions.IsIn<object>(newValue, (object) "E", (object) "T") ? 1 : 0) : 0) == 0)
      return;
    e.Row.StartsOn = new DateTime?();
    e.Row.ExpiresOn = new DateTime?();
  }

  protected virtual void _(Events.RowPersisting<EPWingman> e)
  {
    if (e.Row == null)
      return;
    this.VerifyApprovalDelegateRow(((Events.Event<PXRowPersistingEventArgs, Events.RowPersisting<EPWingman>>) e).Cache, e.Row);
  }

  protected virtual void _(
    Events.FieldVerifying<EPWingman, EPWingman.startsOn> e)
  {
    if (e.Row == null)
      return;
    e.Row.StartsOn = ((Events.FieldVerifyingBase<Events.FieldVerifying<EPWingman, EPWingman.startsOn>, EPWingman, object>) e).NewValue != null ? new DateTime?((DateTime) ((Events.FieldVerifyingBase<Events.FieldVerifying<EPWingman, EPWingman.startsOn>, EPWingman, object>) e).NewValue) : throw new PXSetPropertyException("'{0}' cannot be empty.");
    this.VerifyStartsOnDateInThePast(((Events.Event<PXFieldVerifyingEventArgs, Events.FieldVerifying<EPWingman, EPWingman.startsOn>>) e).Cache, e.Row);
  }

  protected virtual void _(
    Events.FieldDefaulting<EPWingman, EPWingman.startsOn> e)
  {
    if (e.Row == null || !"A".Equals(e.Row.DelegationOf) || ((Events.FieldDefaultingBase<Events.FieldDefaulting<EPWingman, EPWingman.startsOn>, EPWingman, object>) e).NewValue != null)
      return;
    ((Events.FieldDefaultingBase<Events.FieldDefaulting<EPWingman, EPWingman.startsOn>, EPWingman, object>) e).NewValue = (object) this.GetLastAllowedStartOnDate(e.Row);
    ((Events.FieldDefaultingBase<Events.FieldDefaulting<EPWingman, EPWingman.startsOn>>) e).Cancel = true;
  }

  protected virtual void VerifyApprovalDelegateRow(PXCache cache, EPWingman row)
  {
    if (row.DelegationOf != "A")
      return;
    this.VerifyStartsOnDateInThePast(cache, row);
    if (row.StartsOn.HasValue && row.ExpiresOn.HasValue)
    {
      DateTime? startsOn = row.StartsOn;
      DateTime? expiresOn = row.ExpiresOn;
      if ((startsOn.HasValue & expiresOn.HasValue ? (startsOn.GetValueOrDefault() > expiresOn.GetValueOrDefault() ? 1 : 0) : 0) != 0)
        cache.RaiseExceptionHandling<EPWingman.expiresOn>((object) row, (object) row.ExpiresOn, (Exception) new PXSetPropertyException("The Expires On date cannot be before the Starts On date."));
    }
    DateTime dateTime1 = row.StartsOn.Value;
    DateTime? nullable = row.ExpiresOn;
    DateTime maxValue1;
    if (!nullable.HasValue)
    {
      maxValue1 = DateTime.MaxValue;
    }
    else
    {
      nullable = row.ExpiresOn;
      maxValue1 = nullable.Value;
    }
    DateTime dateTime2 = maxValue1;
    foreach (PXResult<EPWingman> pxResult in ((PXSelectBase<EPWingman>) this.DelegatesForApprovals).Select(new object[1]
    {
      (object) row.EmployeeID
    }))
    {
      EPWingman epWingman = PXResult<EPWingman>.op_Implicit(pxResult);
      int? recordId1 = epWingman.RecordID;
      int? recordId2 = row.RecordID;
      if (!(recordId1.GetValueOrDefault() == recordId2.GetValueOrDefault() & recordId1.HasValue == recordId2.HasValue))
      {
        nullable = epWingman.StartsOn;
        DateTime dateTime3 = nullable.Value;
        nullable = epWingman.ExpiresOn;
        DateTime maxValue2;
        if (!nullable.HasValue)
        {
          maxValue2 = DateTime.MaxValue;
        }
        else
        {
          nullable = epWingman.ExpiresOn;
          maxValue2 = nullable.Value;
        }
        DateTime dateTime4 = maxValue2;
        if (dateTime3 <= dateTime2 && dateTime2 <= dateTime4 || dateTime1 <= dateTime3 && dateTime4 <= dateTime2 || dateTime3 <= dateTime1 && dateTime1 <= dateTime4)
        {
          nullable = row.ExpiresOn;
          if (nullable.HasValue && (dateTime3 <= dateTime2 && dateTime2 <= dateTime4 || dateTime1 <= dateTime3 && dateTime4 <= dateTime2))
            cache.RaiseExceptionHandling<EPWingman.expiresOn>((object) row, (object) row.ExpiresOn, (Exception) new PXSetPropertyException("The date is within the period specified for one of the existing delegations. The delegation periods cannot intersect."));
          if (dateTime3 <= dateTime1 && dateTime1 <= dateTime4 || dateTime1 <= dateTime3 && dateTime4 <= dateTime2)
            cache.RaiseExceptionHandling<EPWingman.startsOn>((object) row, (object) row.StartsOn, (Exception) new PXSetPropertyException("The date is within the period specified for one of the existing delegations. The delegation periods cannot intersect."));
        }
      }
    }
  }

  protected virtual void VerifyStartsOnDateInThePast(PXCache cache, EPWingman row)
  {
    DateTime? startsOn = row.StartsOn;
    DateTime dateTime1 = PXTimeZoneInfo.Now;
    dateTime1 = dateTime1.Date;
    DateTime dateTime2 = dateTime1.AddMilliseconds(-1.0);
    if ((startsOn.HasValue ? (startsOn.GetValueOrDefault() < dateTime2 ? 1 : 0) : 0) == 0)
      return;
    cache.RaiseExceptionHandling<EPWingman.startsOn>((object) row, (object) row.StartsOn, (Exception) new PXSetPropertyException("The Starts On date cannot be in the past."));
  }

  protected virtual DateTime? GetLastAllowedStartOnDate(EPWingman row, DateTime? startsDate = null)
  {
    DateTime now = PXTimeZoneInfo.Now;
    foreach (PXResult<EPWingman> pxResult in ((PXSelectBase<EPWingman>) this.DelegatesForApprovals).Select(new object[1]
    {
      (object) row.EmployeeID
    }))
    {
      EPWingman epWingman = PXResult<EPWingman>.op_Implicit(pxResult);
      DateTime? nullable = epWingman.ExpiresOn ?? epWingman.StartsOn;
      if (now < nullable.Value)
        now = nullable.Value;
    }
    return new DateTime?(now.AddDays(1.0));
  }
}
