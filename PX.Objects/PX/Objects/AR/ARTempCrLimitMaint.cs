// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.ARTempCrLimitMaint
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;
using System.Collections;

#nullable disable
namespace PX.Objects.AR;

[PXHidden]
[Obsolete("This graph is not used anymore and will be removed in Acumatica ERP 8.0.")]
public class ARTempCrLimitMaint : PXGraph<ARTempCrLimitMaint>
{
  public PXSave<ARTempCrLimitFilter> Save;
  public PXAction<ARTempCrLimitFilter> cancel;
  public PXFilter<ARTempCrLimitFilter> Filter;
  public PXSelect<ARTempCreditLimit, Where<ARTempCreditLimit.customerID, Equal<Current<ARTempCrLimitFilter.customerID>>>> ARTempCreditLimitRecord;

  [PXUIField]
  [PXCancelButton]
  protected virtual IEnumerable Cancel(PXAdapter adapter)
  {
    ((PXSelectBase) this.ARTempCreditLimitRecord).Cache.Clear();
    ((PXGraph) this).TimeStamp = (byte[]) null;
    return adapter.Get();
  }

  protected virtual void ARTempCreditLimit_StartDate_FieldVerifying(
    PXCache cache,
    PXFieldVerifyingEventArgs e)
  {
    ARTempCreditLimit row = (ARTempCreditLimit) e.Row;
    DateTime? newValue = (DateTime?) e.NewValue;
    DateTime? nullable1;
    if (newValue.HasValue)
    {
      DateTime? nullable2 = row.EndDate;
      if (nullable2.HasValue)
      {
        nullable2 = newValue;
        nullable1 = row.EndDate;
        if ((nullable2.HasValue & nullable1.HasValue ? (nullable2.GetValueOrDefault() > nullable1.GetValueOrDefault() ? 1 : 0) : 0) != 0)
          throw new PXSetPropertyException("Start date must be less or equal to the end date.");
      }
    }
    if (!newValue.HasValue)
      return;
    ARTempCreditLimit arTempCreditLimit1 = PXResultset<ARTempCreditLimit>.op_Implicit(PXSelectBase<ARTempCreditLimit, PXSelect<ARTempCreditLimit, Where<ARTempCreditLimit.customerID, Equal<Required<ARTempCreditLimit.customerID>>, And<ARTempCreditLimit.lineID, NotEqual<Required<ARTempCreditLimit.lineID>>, And<ARTempCreditLimit.startDate, LessEqual<Required<ARTempCreditLimit.startDate>>, And<ARTempCreditLimit.endDate, GreaterEqual<Required<ARTempCreditLimit.endDate>>>>>>>.Config>.Select((PXGraph) this, new object[4]
    {
      (object) row.CustomerID,
      (object) row.LineID,
      (object) newValue,
      (object) newValue
    }));
    int? customerId;
    if (arTempCreditLimit1 != null)
    {
      customerId = arTempCreditLimit1.CustomerID;
      if (customerId.HasValue)
      {
        nullable1 = arTempCreditLimit1.StartDate;
        if (nullable1.HasValue)
        {
          nullable1 = arTempCreditLimit1.EndDate;
          if (nullable1.HasValue)
            throw new PXSetPropertyException("Credit limit for this customer has already been exceeded.");
        }
      }
    }
    nullable1 = row.EndDate;
    if (!nullable1.HasValue)
      return;
    ARTempCreditLimit arTempCreditLimit2 = PXResultset<ARTempCreditLimit>.op_Implicit(PXSelectBase<ARTempCreditLimit, PXSelect<ARTempCreditLimit, Where<ARTempCreditLimit.customerID, Equal<Required<ARTempCreditLimit.customerID>>, And<ARTempCreditLimit.lineID, NotEqual<Required<ARTempCreditLimit.lineID>>, And<ARTempCreditLimit.startDate, Between<Required<ARTempCreditLimit.startDate>, Required<ARTempCreditLimit.startDate>>, And<ARTempCreditLimit.endDate, Between<Required<ARTempCreditLimit.endDate>, Required<ARTempCreditLimit.endDate>>>>>>>.Config>.Select((PXGraph) this, new object[6]
    {
      (object) row.CustomerID,
      (object) row.LineID,
      (object) newValue,
      (object) row.EndDate,
      (object) newValue,
      (object) row.EndDate
    }));
    if (arTempCreditLimit2 == null)
      return;
    customerId = arTempCreditLimit2.CustomerID;
    if (!customerId.HasValue)
      return;
    nullable1 = arTempCreditLimit2.StartDate;
    if (!nullable1.HasValue)
      return;
    nullable1 = arTempCreditLimit2.EndDate;
    if (nullable1.HasValue)
      throw new PXSetPropertyException("Credit limit for this customer has already been exceeded.");
  }

  protected virtual void ARTempCreditLimit_EndDate_FieldVerifying(
    PXCache cache,
    PXFieldVerifyingEventArgs e)
  {
    ARTempCreditLimit row = (ARTempCreditLimit) e.Row;
    DateTime? newValue = (DateTime?) e.NewValue;
    DateTime? nullable1;
    if (newValue.HasValue)
    {
      DateTime? nullable2 = row.StartDate;
      if (nullable2.HasValue)
      {
        nullable2 = newValue;
        nullable1 = row.StartDate;
        if ((nullable2.HasValue & nullable1.HasValue ? (nullable2.GetValueOrDefault() < nullable1.GetValueOrDefault() ? 1 : 0) : 0) != 0)
          throw new PXSetPropertyException("Start date must be less or equal to the end date.");
      }
    }
    if (!newValue.HasValue)
      return;
    ARTempCreditLimit arTempCreditLimit1 = PXResultset<ARTempCreditLimit>.op_Implicit(PXSelectBase<ARTempCreditLimit, PXSelect<ARTempCreditLimit, Where<ARTempCreditLimit.customerID, Equal<Required<ARTempCreditLimit.customerID>>, And<ARTempCreditLimit.lineID, NotEqual<Required<ARTempCreditLimit.lineID>>, And<ARTempCreditLimit.startDate, LessEqual<Required<ARTempCreditLimit.startDate>>, And<ARTempCreditLimit.endDate, GreaterEqual<Required<ARTempCreditLimit.endDate>>>>>>>.Config>.Select((PXGraph) this, new object[4]
    {
      (object) row.CustomerID,
      (object) row.LineID,
      (object) newValue,
      (object) newValue
    }));
    int? customerId;
    if (arTempCreditLimit1 != null)
    {
      customerId = arTempCreditLimit1.CustomerID;
      if (customerId.HasValue)
      {
        nullable1 = arTempCreditLimit1.StartDate;
        if (nullable1.HasValue)
        {
          nullable1 = arTempCreditLimit1.EndDate;
          if (nullable1.HasValue)
            throw new PXSetPropertyException("Credit limit for this customer has already been exceeded.");
        }
      }
    }
    nullable1 = row.StartDate;
    if (!nullable1.HasValue)
      return;
    ARTempCreditLimit arTempCreditLimit2 = PXResultset<ARTempCreditLimit>.op_Implicit(PXSelectBase<ARTempCreditLimit, PXSelect<ARTempCreditLimit, Where<ARTempCreditLimit.customerID, Equal<Required<ARTempCreditLimit.customerID>>, And<ARTempCreditLimit.lineID, NotEqual<Required<ARTempCreditLimit.lineID>>, And<ARTempCreditLimit.startDate, Between<Required<ARTempCreditLimit.startDate>, Required<ARTempCreditLimit.startDate>>, And<ARTempCreditLimit.endDate, Between<Required<ARTempCreditLimit.endDate>, Required<ARTempCreditLimit.endDate>>>>>>>.Config>.Select((PXGraph) this, new object[6]
    {
      (object) row.CustomerID,
      (object) row.LineID,
      (object) row.StartDate,
      (object) newValue,
      (object) row.StartDate,
      (object) newValue
    }));
    if (arTempCreditLimit2 == null)
      return;
    customerId = arTempCreditLimit2.CustomerID;
    if (!customerId.HasValue)
      return;
    nullable1 = arTempCreditLimit2.StartDate;
    if (!nullable1.HasValue)
      return;
    nullable1 = arTempCreditLimit2.EndDate;
    if (nullable1.HasValue)
      throw new PXSetPropertyException("Credit limit for this customer has already been exceeded.");
  }
}
