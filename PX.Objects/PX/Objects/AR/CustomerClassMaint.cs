// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.CustomerClassMaint
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Data.Descriptor;
using PX.Objects.CA;
using PX.Objects.Common;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.SM;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

#nullable disable
namespace PX.Objects.AR;

public class CustomerClassMaint : PXGraph<CustomerClassMaint, CustomerClass>
{
  public PXSelect<CustomerClass> CustomerClassRecord;
  public PXSelect<CustomerClass, Where<CustomerClass.customerClassID, Equal<Current<CustomerClass.customerClassID>>>> CurCustomerClassRecord;
  [PXViewName("Attributes")]
  public CSAttributeGroupList<CustomerClass, Customer> Mapping;
  public CRClassNotificationSourceList<CustomerClass.customerClassID, ARNotificationSource.customer> NotificationSources;
  public PXSelect<NotificationRecipient, Where<NotificationRecipient.refNoteID, IsNull, And<NotificationRecipient.sourceID, Equal<Optional<NotificationSource.sourceID>>>>> NotificationRecipients;
  public PXSelect<Customer, Where<Customer.customerClassID, Equal<Current<CustomerClass.customerClassID>>>> Customers;
  public PXSelect<ARDunningCustomerClass, Where<ARDunningCustomerClass.customerClassID, Equal<Current<CustomerClass.customerClassID>>>> DunningSetup;
  public PXSetup<PX.Objects.AR.ARSetup> ARSetup;
  public PXMenuAction<CustomerClass> ActionsMenu;
  public PXAction<CustomerClass> resetGroup;
  public PXSelect<Neighbour> Neighbours;
  public PXSetup<PX.Objects.GL.Company> Company;

  [InjectDependency]
  internal IBAccountRestrictionHelper BAccountRestrictionHelper { get; set; }

  [PXSelector(typeof (Search<NotificationSetup.setupID, Where<NotificationSetup.sourceCD, Equal<ARNotificationSource.customer>>>))]
  [PXMergeAttributes]
  protected virtual void NotificationSource_SetupID_CacheAttached(PXCache sender)
  {
  }

  [PXDefault(typeof (CustomerClass.customerClassID))]
  [PXParent(typeof (Select2<CustomerClass, InnerJoin<NotificationSetup, On<NotificationSetup.setupID, Equal<Current<NotificationSource.setupID>>>>, Where<CustomerClass.customerClassID, Equal<Current<NotificationSource.classID>>>>))]
  [PXMergeAttributes]
  protected virtual void NotificationSource_ClassID_CacheAttached(PXCache sender)
  {
  }

  [PXSelector(typeof (Search<SiteMap.screenID, Where2<Where<SiteMap.url, Like<PX.Objects.Common.urlReports>, Or<SiteMap.url, Like<urlReportsInNewUi>>>, And<Where<SiteMap.screenID, Like<PXModule.ar_>, Or<SiteMap.screenID, Like<PXModule.so_>, Or<SiteMap.screenID, Like<PXModule.cr_>>>>>>, OrderBy<Asc<SiteMap.screenID>>>), new System.Type[] {typeof (SiteMap.screenID), typeof (SiteMap.title)}, Headers = new string[] {"Report ID", "Report Name"}, DescriptionField = typeof (SiteMap.title))]
  [PXMergeAttributes]
  protected virtual void NotificationSource_ReportID_CacheAttached(PXCache sender)
  {
  }

  [PXDefault]
  [CustomerContactType.ClassList]
  [PXCheckDistinct(new System.Type[] {typeof (NotificationRecipient.contactID)}, Where = typeof (Where<NotificationRecipient.refNoteID, IsNull, And<NotificationRecipient.sourceID, Equal<Current<NotificationRecipient.sourceID>>>>))]
  [PXMergeAttributes]
  protected virtual void NotificationRecipient_ContactType_CacheAttached(PXCache sender)
  {
  }

  [PXProcessButton(IsLockedOnToolbar = true)]
  [PXUIField(DisplayName = "Include Customers in Restriction Group")]
  protected virtual IEnumerable ResetGroup(PXAdapter adapter)
  {
    if (((PXSelectBase<CustomerClass>) this.CustomerClassRecord).Ask("Warning", "All customers of the class will be included in the group specified in the Default Restriction Group box and excluded from the group to which they currently belong. Do you want to proceed?", (MessageButtons) 1) == 1)
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      CustomerClassMaint.\u003C\u003Ec__DisplayClass18_0 cDisplayClass180 = new CustomerClassMaint.\u003C\u003Ec__DisplayClass18_0();
      ((PXAction) this.Save).Press();
      // ISSUE: reference to a compiler-generated field
      cDisplayClass180.classID = ((PXSelectBase<CustomerClass>) this.CustomerClassRecord).Current.CustomerClassID;
      // ISSUE: method pointer
      PXLongOperation.StartOperation((PXGraph) this, new PXToggleAsyncDelegate((object) cDisplayClass180, __methodptr(\u003CResetGroup\u003Eb__0)));
    }
    return adapter.Get();
  }

  protected static void Reset(string classID)
  {
    CustomerClassMaint instance = PXGraph.CreateInstance<CustomerClassMaint>();
    ((PXSelectBase<CustomerClass>) instance.CustomerClassRecord).Current = PXResultset<CustomerClass>.op_Implicit(((PXSelectBase<CustomerClass>) instance.CustomerClassRecord).Search<CustomerClass.customerClassID>((object) classID, Array.Empty<object>()));
    if (((PXSelectBase<CustomerClass>) instance.CustomerClassRecord).Current == null)
      return;
    foreach (PXResult<Customer> pxResult in ((PXSelectBase<Customer>) instance.Customers).Select(Array.Empty<object>()))
    {
      Customer customer = PXResult<Customer>.op_Implicit(pxResult);
      customer.GroupMask = ((PXSelectBase<CustomerClass>) instance.CustomerClassRecord).Current.GroupMask;
      ((PXSelectBase) instance.Customers).Cache.SetStatus((object) customer, (PXEntryStatus) 1);
    }
    ((PXAction) instance.Save).Press();
  }

  public virtual void Persist()
  {
    if (((PXSelectBase<CustomerClass>) this.CustomerClassRecord).Current != null)
    {
      SingleGroupAttribute.PopulateNeighbours<CustomerClass.groupMask>((PXSelectBase) this.CustomerClassRecord, (PXSelectBase<Neighbour>) this.Neighbours, typeof (Users), typeof (Users));
      SingleGroupAttribute.PopulateNeighbours<CustomerClass.groupMask>((PXSelectBase) this.CustomerClassRecord, (PXSelectBase<Neighbour>) this.Neighbours, typeof (Customer), typeof (Customer));
      SingleGroupAttribute.PopulateNeighbours<CustomerClass.groupMask>((PXSelectBase) this.CustomerClassRecord, (PXSelectBase<Neighbour>) this.Neighbours, typeof (CustomerClass), typeof (CustomerClass));
      SingleGroupAttribute.PopulateNeighbours<CustomerClass.groupMask>((PXSelectBase) this.CustomerClassRecord, (PXSelectBase<Neighbour>) this.Neighbours, typeof (Users), typeof (Customer));
      SingleGroupAttribute.PopulateNeighbours<CustomerClass.groupMask>((PXSelectBase) this.CustomerClassRecord, (PXSelectBase<Neighbour>) this.Neighbours, typeof (Customer), typeof (Users));
      SingleGroupAttribute.PopulateNeighbours<CustomerClass.groupMask>((PXSelectBase) this.CustomerClassRecord, (PXSelectBase<Neighbour>) this.Neighbours, typeof (Users), typeof (CustomerClass));
      SingleGroupAttribute.PopulateNeighbours<CustomerClass.groupMask>((PXSelectBase) this.CustomerClassRecord, (PXSelectBase<Neighbour>) this.Neighbours, typeof (CustomerClass), typeof (Users));
      SingleGroupAttribute.PopulateNeighbours<CustomerClass.groupMask>((PXSelectBase) this.CustomerClassRecord, (PXSelectBase<Neighbour>) this.Neighbours, typeof (CustomerClass), typeof (Customer));
      SingleGroupAttribute.PopulateNeighbours<CustomerClass.groupMask>((PXSelectBase) this.CustomerClassRecord, (PXSelectBase<Neighbour>) this.Neighbours, typeof (Customer), typeof (CustomerClass));
    }
    using (PXTransactionScope transactionScope = new PXTransactionScope())
    {
      this.BAccountRestrictionHelper.Persist();
      ((PXGraph) this).Persist();
      GroupHelper.Clear();
      transactionScope.Complete();
    }
  }

  public virtual void CustomerClass_RowSelected(PXCache cache, PXRowSelectedEventArgs e)
  {
    CustomerClass row = (CustomerClass) e.Row;
    if (row == null)
      return;
    ((PXSelectBase) this.NotificationRecipients).Cache.AllowInsert = PXResultset<NotificationSource>.op_Implicit(((PXSelectBase<NotificationSource>) this.NotificationSources).Select(new object[1]
    {
      (object) row.CustomerClassID
    })) != null;
    PXUIFieldAttribute.SetEnabled<CustomerClass.creditLimit>(cache, (object) row, row.CreditRule == "C" || row.CreditRule == "B");
    PXUIFieldAttribute.SetEnabled<CustomerClass.overLimitAmount>(cache, (object) row, row.CreditRule == "C" || row.CreditRule == "B");
    PXUIFieldAttribute.SetEnabled<CustomerClass.creditDaysPastDue>(cache, (object) row, row.CreditRule == "D" || row.CreditRule == "B");
    PXCache pxCache1 = cache;
    CustomerClass customerClass1 = row;
    bool? nullable = row.SmallBalanceAllow;
    int num1 = nullable.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetEnabled<CustomerClass.smallBalanceLimit>(pxCache1, (object) customerClass1, num1 != 0);
    PXCache pxCache2 = cache;
    CustomerClass customerClass2 = row;
    nullable = row.FinChargeApply;
    int num2 = nullable.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetEnabled<CustomerClass.finChargeID>(pxCache2, (object) customerClass2, num2 != 0);
    bool flag = PXAccess.FeatureInstalled<FeaturesSet.multicurrency>();
    PXUIFieldAttribute.SetVisible<CustomerClass.curyID>(cache, (object) null, flag);
    PXUIFieldAttribute.SetVisible<CustomerClass.curyRateTypeID>(cache, (object) null, flag);
    PXUIFieldAttribute.SetVisible<CustomerClass.printCuryStatements>(cache, (object) null, flag);
    PXUIFieldAttribute.SetVisible<CustomerClass.allowOverrideCury>(cache, (object) null, flag);
    PXUIFieldAttribute.SetVisible<CustomerClass.allowOverrideRate>(cache, (object) null, flag);
    PXUIFieldAttribute.SetVisible<CustomerClass.unrealizedGainAcctID>(cache, (object) null, flag);
    PXUIFieldAttribute.SetVisible<CustomerClass.unrealizedGainSubID>(cache, (object) null, flag);
    PXUIFieldAttribute.SetVisible<CustomerClass.unrealizedLossAcctID>(cache, (object) null, flag);
    PXUIFieldAttribute.SetVisible<CustomerClass.unrealizedLossSubID>(cache, (object) null, flag);
  }

  public virtual void CustomerClass_RowDeleting(PXCache cache, PXRowDeletingEventArgs e)
  {
    CustomerClass row = (CustomerClass) e.Row;
    if (row == null)
      return;
    PX.Objects.AR.ARSetup arSetup = PXResultset<PX.Objects.AR.ARSetup>.op_Implicit(PXSelectBase<PX.Objects.AR.ARSetup, PXSelect<PX.Objects.AR.ARSetup>.Config>.Select((PXGraph) this, Array.Empty<object>()));
    if (arSetup != null && row.CustomerClassID == arSetup.DfltCustomerClassID)
      throw new PXException("This Customer Class can not be deleted because it is used in Accounts Receivable Preferences.");
  }

  protected virtual void _(PX.Data.Events.RowUpdated<CustomerClass> e)
  {
    if (!((PXGraph) this).IsCopyPasteContext)
      return;
    ((PXSelectBase) this.DunningSetup).Cache.Clear();
  }

  protected virtual void CustomerClass_CuryID_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    if (PXAccess.FeatureInstalled<FeaturesSet.multicurrency>())
      return;
    e.NewValue = (object) null;
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void CustomerClass_CuryRateTypeID_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    if (PXAccess.FeatureInstalled<FeaturesSet.multicurrency>())
      return;
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void CustomerClass_CuryRateTypeID_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    if (PXAccess.FeatureInstalled<FeaturesSet.multicurrency>())
      return;
    ((CancelEventArgs) e).Cancel = true;
  }

  public virtual void CustomerClass_RowPersisting(PXCache cache, PXRowPersistingEventArgs e)
  {
    CustomerClass row = (CustomerClass) e.Row;
    int num;
    if (!(PXSelectorAttribute.Select<CustomerClass.shipVia>(cache, (object) row) is Carrier carrier))
    {
      num = 0;
    }
    else
    {
      bool? isActive = carrier.IsActive;
      bool flag = false;
      num = isActive.GetValueOrDefault() == flag & isActive.HasValue ? 1 : 0;
    }
    if (num != 0)
      cache.RaiseExceptionHandling<CustomerClass.shipVia>((object) row, (object) row.ShipVia, (Exception) new PXSetPropertyException((IBqlTable) row, "The Ship Via code is not active.", (PXErrorLevel) 2));
    bool? nullable = row.FinChargeApply;
    if (nullable.GetValueOrDefault() && row.FinChargeID == null)
    {
      if (cache.RaiseExceptionHandling<CustomerClass.finChargeID>(e.Row, (object) null, (Exception) new PXSetPropertyException("'{0}' cannot be empty.", new object[1]
      {
        (object) "[finChargeID]"
      })))
        throw new PXRowPersistingException(typeof (CustomerClass.finChargeID).Name, (object) null, "'{0}' cannot be empty.", new object[1]
        {
          (object) typeof (CustomerClass.finChargeID).Name
        });
    }
    if (row == null)
      return;
    nullable = row.RequireAvalaraCustomerUsageType;
    if (nullable.GetValueOrDefault() && row.AvalaraCustomerUsageType == "0")
      throw new PXRowPersistingException(typeof (CustomerClass.avalaraCustomerUsageType).Name, (object) row.AvalaraCustomerUsageType, "Select the entity usage type other than Default.");
  }

  public virtual void CustomerClass_StatementCycleId_FieldUpdated(
    PXCache cache,
    PXFieldUpdatedEventArgs e)
  {
    CustomerClass row = (CustomerClass) e.Row;
    if (row == null || row.StatementCycleId == null)
      return;
    PX.Objects.AR.ARSetup arSetup = PXResultset<PX.Objects.AR.ARSetup>.op_Implicit(PXSelectBase<PX.Objects.AR.ARSetup, PXSelect<PX.Objects.AR.ARSetup>.Config>.Select((PXGraph) this, Array.Empty<object>()));
    if (arSetup == null || !arSetup.DefFinChargeFromCycle.GetValueOrDefault())
      return;
    ARStatementCycle arStatementCycle = PXResultset<ARStatementCycle>.op_Implicit(PXSelectBase<ARStatementCycle, PXSelect<ARStatementCycle, Where<ARStatementCycle.statementCycleId, Equal<Required<ARStatementCycle.statementCycleId>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) row.StatementCycleId
    }));
    if (arStatementCycle == null || arStatementCycle.FinChargeID == null)
      return;
    row.FinChargeID = arStatementCycle.FinChargeID;
    ((PXSelectBase) this.CustomerClassRecord).Cache.RaiseFieldUpdated<CustomerClass.finChargeID>((object) row, (object) null);
  }

  public virtual void CustomerClass_CreditRule_FieldUpdated(
    PXCache cache,
    PXFieldUpdatedEventArgs e)
  {
    CustomerClass row = (CustomerClass) e.Row;
    if (row.CreditRule == "C" || row.CreditRule == "N")
      row.CreditDaysPastDue = new short?((short) 0);
    if (!(row.CreditRule == "D") && !(row.CreditRule == "N"))
      return;
    row.CreditLimit = new Decimal?(0M);
  }

  public virtual void CustomerClass_SmallBalanceAllow_FieldUpdated(
    PXCache cache,
    PXFieldUpdatedEventArgs e)
  {
    ((CustomerClass) e.Row).SmallBalanceLimit = new Decimal?(0M);
  }

  public virtual void CustomerClass_FinChargeApply_FieldUpdated(
    PXCache cache,
    PXFieldUpdatedEventArgs e)
  {
    CustomerClass row = (CustomerClass) e.Row;
    if (row.FinChargeApply.GetValueOrDefault())
      return;
    row.FinChargeID = (string) null;
  }

  protected virtual void _(PX.Data.Events.RowInserted<CustomerClass> e)
  {
    if (e.Row.CustomerClassID == null)
      return;
    foreach (PXResult<ARDunningSetup> pxResult in PXSelectBase<ARDunningSetup, PXViewOf<ARDunningSetup>.BasedOn<SelectFromBase<ARDunningSetup, TypeArrayOf<IFbqlJoin>.Empty>>.Config>.SelectMultiBound((PXGraph) this, (object[]) null, (object[]) null))
    {
      ARDunningSetup arDunningSetup = PXResult<ARDunningSetup>.op_Implicit(pxResult);
      ARDunningCustomerClass dunningCustomerClass = ((PXSelectBase) this.DunningSetup).Cache.Insert((object) new ARDunningCustomerClass()
      {
        DunningLetterLevel = arDunningSetup.DunningLetterLevel,
        CustomerClassID = ((PXSelectBase<CustomerClass>) this.CustomerClassRecord).Current.CustomerClassID
      }) as ARDunningCustomerClass;
      dunningCustomerClass.DueDays = arDunningSetup.DueDays;
      dunningCustomerClass.DaysToSettle = arDunningSetup.DaysToSettle;
      dunningCustomerClass.Descr = arDunningSetup.Descr;
      dunningCustomerClass.DunningFee = arDunningSetup.DunningFee;
      ((PXSelectBase) this.DunningSetup).Cache.Update((object) dunningCustomerClass);
    }
  }

  protected virtual void _(PX.Data.Events.RowDeleting<ARDunningCustomerClass> e)
  {
    if (e.Row == null || ((PXCache) GraphHelper.Caches<CustomerClass>((PXGraph) this)).Deleted.Cast<CustomerClass>().Any<CustomerClass>((Func<CustomerClass, bool>) (c => c.CustomerClassID == e.Row.CustomerClassID)) || GraphHelper.Caches<CustomerClass>((PXGraph) this).GetStatus(((PXSelectBase<CustomerClass>) this.CustomerClassRecord).Current) == 4)
      return;
    int num1 = 0;
    foreach (PXResult<ARDunningCustomerClass> pxResult in PXSelectBase<ARDunningCustomerClass, PXViewOf<ARDunningCustomerClass>.BasedOn<SelectFromBase<ARDunningCustomerClass, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<ARDunningCustomerClass.customerClassID, IBqlString>.IsEqual<P.AsString>>>.Config>.SelectMultiBound((PXGraph) this, (object[]) null, new object[1]
    {
      (object) e.Row.CustomerClassID
    }))
    {
      int num2 = PXResult<ARDunningCustomerClass>.op_Implicit(pxResult).DunningLetterLevel.Value;
      num1 = num1 < num2 ? num2 : num1;
    }
    if (e.Row.DunningLetterLevel.Value < num1)
      throw new PXException("Only last row can be deleted");
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<ARDunningCustomerClass, ARDunningCustomerClass.dueDays> e)
  {
    if (e.Row == null)
      return;
    int num1 = e.Row.DunningLetterLevel.Value;
    int int32 = Convert.ToInt32(((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<ARDunningCustomerClass, ARDunningCustomerClass.dueDays>, ARDunningCustomerClass, object>) e).NewValue);
    if (num1 == 1 && int32 <= 0)
      throw new PXSetPropertyException("This value MUST exceed {0}", (PXErrorLevel) 0);
    int num2 = 0;
    int num3 = 0;
    foreach (PXResult<ARDunningCustomerClass> pxResult in PXSelectBase<ARDunningCustomerClass, PXSelect<ARDunningCustomerClass, Where<ARDunningCustomerClass.customerClassID, Equal<Required<ARDunningCustomerClass.customerClassID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) e.Row.CustomerClassID
    }))
    {
      ARDunningCustomerClass dunningCustomerClass = PXResult<ARDunningCustomerClass>.op_Implicit(pxResult);
      int? nullable = dunningCustomerClass.DunningLetterLevel;
      if (nullable.Value == num1 - 1)
      {
        nullable = dunningCustomerClass.DueDays;
        num3 = nullable.Value;
      }
      nullable = dunningCustomerClass.DunningLetterLevel;
      if (nullable.Value == num1 + 1)
      {
        nullable = dunningCustomerClass.DueDays;
        num2 = nullable.Value;
      }
    }
    if (int32 <= num3)
      throw new PXSetPropertyException("This value MUST exceed {0}", new object[1]
      {
        (object) num3
      });
    if (int32 >= num2 && num2 > 0)
      throw new PXSetPropertyException("This value can not exceed {0}", new object[1]
      {
        (object) num2
      });
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<ARDunningCustomerClass.dunningFee> e)
  {
    if (((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<ARDunningCustomerClass.dunningFee>, object, object>) e).NewValue != null && (Decimal) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<ARDunningCustomerClass.dunningFee>, object, object>) e).NewValue != 0M && !((PXSelectBase<PX.Objects.AR.ARSetup>) this.ARSetup).Current.DunningFeeInventoryID.HasValue)
      throw new PXSetPropertyException("To charge a dunning letter fee, select an item in the Dunning Fee Item box (Dunning tab) of the Accounts Receivable Preferences (AR101000) form.");
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<ARDunningCustomerClass, ARDunningCustomerClass.dueDays> e)
  {
    ARDunningCustomerClass row = e.Row;
    if ((row != null ? (row.DunningLetterLevel.HasValue ? 1 : 0) : 0) == 0)
      return;
    int? nullable = e.Row.DunningLetterLevel;
    int num1 = nullable.Value;
    if (num1 == 1)
    {
      ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<ARDunningCustomerClass, ARDunningCustomerClass.dueDays>, ARDunningCustomerClass, object>) e).NewValue = (object) 30;
    }
    else
    {
      int num2 = 0;
      foreach (PXResult<ARDunningCustomerClass> pxResult in PXSelectBase<ARDunningCustomerClass, PXSelect<ARDunningCustomerClass, Where<ARDunningCustomerClass.customerClassID, Equal<Current<ARDunningCustomerClass.customerClassID>>>>.Config>.Select((PXGraph) this, Array.Empty<object>()))
      {
        ARDunningCustomerClass dunningCustomerClass = PXResult<ARDunningCustomerClass>.op_Implicit(pxResult);
        nullable = dunningCustomerClass.DunningLetterLevel;
        if (nullable.Value == num1 - 1)
        {
          int num3 = num2;
          nullable = dunningCustomerClass.DueDays;
          int num4 = nullable.Value;
          num2 = num3 + num4;
        }
        nullable = dunningCustomerClass.DunningLetterLevel;
        if (nullable.Value == 1 && num1 > 1)
        {
          int num5 = num2;
          nullable = dunningCustomerClass.DueDays;
          int num6 = nullable.Value;
          num2 = num5 + num6;
        }
      }
      ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<ARDunningCustomerClass, ARDunningCustomerClass.dueDays>, ARDunningCustomerClass, object>) e).NewValue = (object) num2;
    }
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<ARDunningCustomerClass.dunningLetterLevel> e)
  {
    List<ARDunningCustomerClass> list = GraphHelper.RowCast<ARDunningCustomerClass>((IEnumerable) PXSelectBase<ARDunningCustomerClass, PXSelect<ARDunningCustomerClass, Where<ARDunningCustomerClass.customerClassID, Equal<Current<ARDunningCustomerClass.customerClassID>>>>.Config>.Select((PXGraph) this, Array.Empty<object>())).ToList<ARDunningCustomerClass>();
    PX.Data.Events.FieldDefaulting<ARDunningCustomerClass.dunningLetterLevel> fieldDefaulting = e;
    int? nullable;
    if (!list.Any<ARDunningCustomerClass>())
    {
      nullable = new int?(1);
    }
    else
    {
      int? dunningLetterLevel = list.OrderByDescending<ARDunningCustomerClass, int?>((Func<ARDunningCustomerClass, int?>) (_ => _.DunningLetterLevel)).First<ARDunningCustomerClass>().DunningLetterLevel;
      nullable = dunningLetterLevel.HasValue ? new int?(dunningLetterLevel.GetValueOrDefault() + 1) : new int?();
    }
    // ISSUE: variable of a boxed type
    __Boxed<int?> local = (ValueType) nullable;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<ARDunningCustomerClass.dunningLetterLevel>, object, object>) fieldDefaulting).NewValue = (object) local;
  }

  [PXMergeAttributes]
  [PXDefault(typeof (CustomerClass.customerClassID))]
  [PXParent(typeof (Select<CustomerClass, Where<CustomerClass.customerClassID, Equal<Current<ARDunningCustomerClass.customerClassID>>>>))]
  protected virtual void _(
    PX.Data.Events.CacheAttached<ARDunningCustomerClass.customerClassID> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowSelected<ARDunningCustomerClass> e)
  {
    if (e.Row == null)
      return;
    ARDunningCustomerClass dunningCustomerClass = PXResultset<ARDunningCustomerClass>.op_Implicit(PXSelectBase<ARDunningCustomerClass, PXViewOf<ARDunningCustomerClass>.BasedOn<SelectFromBase<ARDunningCustomerClass, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ARDunningCustomerClass.customerClassID, Equal<P.AsString>>>>>.And<BqlOperand<ARDunningCustomerClass.dunningLetterLevel, IBqlInt>.IsGreater<P.AsInt>>>.Order<PX.Data.BQL.Fluent.By<BqlField<ARDunningCustomerClass.dunningLetterLevel, IBqlInt>.Asc>>>.Config>.SelectSingleBound((PXGraph) this, (object[]) null, new object[2]
    {
      (object) e.Row.CustomerClassID,
      (object) e.Row.DunningLetterLevel
    }));
    bool flag = true;
    if (dunningCustomerClass != null)
    {
      int? nullable = dunningCustomerClass.DueDays;
      if (nullable.HasValue)
      {
        nullable = e.Row.DueDays;
        if (nullable.HasValue)
        {
          nullable = e.Row.DaysToSettle;
          if (nullable.HasValue)
          {
            nullable = e.Row.DueDays;
            int num1 = nullable.Value;
            nullable = e.Row.DaysToSettle;
            int num2 = nullable.Value;
            int num3 = num1 + num2;
            nullable = dunningCustomerClass.DueDays;
            int valueOrDefault = nullable.GetValueOrDefault();
            if (num3 > valueOrDefault & nullable.HasValue)
            {
              string displayName1 = PXUIFieldAttribute.GetDisplayName<ARDunningCustomerClass.dueDays>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<ARDunningCustomerClass>>) e).Cache);
              string displayName2 = PXUIFieldAttribute.GetDisplayName<ARDunningCustomerClass.daysToSettle>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<ARDunningCustomerClass>>) e).Cache);
              ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<ARDunningCustomerClass>>) e).Cache.RaiseExceptionHandling<ARDunningCustomerClass.daysToSettle>((object) e.Row, (object) e.Row.DaysToSettle, (Exception) new PXSetPropertyException("'{0}'+'{1}' should not exceed the '{0}' of the next level Dunning Letter.", (PXErrorLevel) 2, new object[2]
              {
                (object) displayName1,
                (object) displayName2
              }));
              flag = false;
            }
          }
        }
      }
    }
    if (!flag)
      return;
    ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<ARDunningCustomerClass>>) e).Cache.RaiseExceptionHandling<ARDunningCustomerClass.daysToSettle>((object) e.Row, (object) e.Row.DaysToSettle, (Exception) null);
  }

  public CustomerClassMaint()
  {
    PXUIFieldAttribute.SetVisible<CustomerClass.cOGSAcctID>(((PXSelectBase) this.CustomerClassRecord).Cache, (object) null, false);
    PXUIFieldAttribute.SetVisible<CustomerClass.cOGSSubID>(((PXSelectBase) this.CustomerClassRecord).Cache, (object) null, false);
    PXUIFieldAttribute.SetVisible<CustomerClass.miscAcctID>(((PXSelectBase) this.CustomerClassRecord).Cache, (object) null, false);
    PXUIFieldAttribute.SetVisible<CustomerClass.miscSubID>(((PXSelectBase) this.CustomerClassRecord).Cache, (object) null, false);
    PXUIFieldAttribute.SetVisible<CustomerClass.localeName>(((PXSelectBase) this.CustomerClassRecord).Cache, (object) null, PXDBLocalizableStringAttribute.HasMultipleLocales);
  }
}
