// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.ARDunningLetterProcess
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.CM;
using PX.Objects.Common;
using PX.Objects.CS;
using PX.Objects.GL;
using PX.Objects.GL.Attributes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace PX.Objects.AR;

public class ARDunningLetterProcess : PXGraph<
#nullable disable
ARDunningLetterProcess>
{
  public PXFilter<ARDunningLetterProcess.ARDunningLetterRecordsParameters> Filter;
  public PXCancel<ARDunningLetterProcess.ARDunningLetterRecordsParameters> Cancel;
  [PXFilterable(new Type[] {})]
  [PXVirtualDAC]
  public PXFilteredProcessing<ARDunningLetterProcess.ARDunningLetterList, ARDunningLetterProcess.ARDunningLetterRecordsParameters> DunningLetterList;
  public PXSetup<ARSetup> arsetup;
  private readonly Dictionary<string, int> MaxDunningLevels = new Dictionary<string, int>();
  private readonly int DunningLetterProcessTypeARSetup;
  [PXViewName("DunningLetter")]
  public PXSelect<ARDunningLetter> docs;
  [PXViewName("DunningLetterDetail")]
  public PXSelect<ARDunningLetterDetail, Where<ARDunningLetterDetail.dunningLetterID, Equal<Required<ARDunningLetter.dunningLetterID>>>> docsDet;

  public ARDunningLetterProcess()
  {
    ((PXSelectBase) this.DunningLetterList).Cache.AllowDelete = false;
    ((PXSelectBase) this.DunningLetterList).Cache.AllowInsert = false;
    ((PXSelectBase) this.DunningLetterList).Cache.AllowUpdate = true;
    this.DunningLetterProcessTypeARSetup = PXResultset<ARSetup>.op_Implicit(((PXSelectBase<ARSetup>) this.arsetup).Select(Array.Empty<object>())).DunningLetterProcessType.Value;
    bool flag = this.DunningLetterProcessTypeARSetup == 0;
    ARSetup arSetup = PXResultset<ARSetup>.op_Implicit(((PXSelectBase<ARSetup>) this.arsetup).Select(Array.Empty<object>()));
    int num = ((PXSelectBase<ARSetup>) this.arsetup).Current.DunningFeeInventoryID.HasValue ? 1 : 0;
    PXUIFieldAttribute.SetVisible<ARDunningLetterProcess.ARDunningLetterRecordsParameters.includeType>(((PXSelectBase) this.Filter).Cache, (object) null, !flag);
    PXUIFieldAttribute.SetVisible<ARDunningLetterProcess.ARDunningLetterRecordsParameters.levelFrom>(((PXSelectBase) this.Filter).Cache, (object) null, !flag);
    PXUIFieldAttribute.SetVisible<ARDunningLetterProcess.ARDunningLetterRecordsParameters.levelTo>(((PXSelectBase) this.Filter).Cache, (object) null, !flag);
    foreach (PXResult<ARDunningCustomerClass> pxResult in PXSelectBase<ARDunningCustomerClass, PXViewOf<ARDunningCustomerClass>.BasedOn<SelectFromBase<ARDunningCustomerClass, TypeArrayOf<IFbqlJoin>.Empty>.Aggregate<To<GroupBy<ARDunningCustomerClass.customerClassID>, Max<ARDunningCustomerClass.dunningLetterLevel>>>>.Config>.SelectMultiBound((PXGraph) this, (object[]) null, (object[]) null))
    {
      ARDunningCustomerClass dunningCustomerClass = PXResult<ARDunningCustomerClass>.op_Implicit(pxResult);
      this.MaxDunningLevels.Add(dunningCustomerClass.CustomerClassID, dunningCustomerClass.DunningLetterLevel.GetValueOrDefault());
    }
    // ISSUE: method pointer
    ((PXProcessingBase<ARDunningLetterProcess.ARDunningLetterList>) this.DunningLetterList).SetProcessDelegate(new PXProcessingBase<ARDunningLetterProcess.ARDunningLetterList>.ProcessListDelegate((object) this, __methodptr(\u003C\u002Ector\u003Eb__12_0)));
    PXUIFieldAttribute.SetEnabled(((PXSelectBase) this.DunningLetterList).Cache, (string) null, false);
    PXUIFieldAttribute.SetEnabled<ARDunningLetterProcess.ARDunningLetterList.selected>(((PXSelectBase) this.DunningLetterList).Cache, (object) null, true);
    ((PXProcessingBase<ARDunningLetterProcess.ARDunningLetterList>) this.DunningLetterList).SetSelected<ARDunningLetterProcess.ARDunningLetterList.selected>();
    PXUIFieldAttribute.SetVisible<ARDunningLetterProcess.ARDunningLetterRecordsParameters.orgBAccountID>(((PXSelectBase) this.Filter).Cache, (object) null, arSetup.PrepareDunningLetters == "B");
    PXUIFieldAttribute.SetVisible<ARDunningLetterProcess.ARDunningLetterRecordsParameters.organizationID>(((PXSelectBase) this.Filter).Cache, (object) null, arSetup.PrepareDunningLetters == "C");
    PXUIFieldAttribute.SetEnabled<ARDunningLetterProcess.ARDunningLetterRecordsParameters.organizationID>(((PXSelectBase) this.Filter).Cache, (object) null, arSetup.PrepareDunningLetters == "C");
  }

  protected virtual void ARDunningLetterRecordsParameters_RowSelected(
    PXCache sender,
    PXRowSelectedEventArgs e)
  {
    ARDunningLetterProcess.ARDunningLetterRecordsParameters row = (ARDunningLetterProcess.ARDunningLetterRecordsParameters) e.Row;
    if (row == null)
      return;
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: method pointer
    ((PXProcessingBase<ARDunningLetterProcess.ARDunningLetterList>) this.DunningLetterList).SetProcessDelegate(new PXProcessingBase<ARDunningLetterProcess.ARDunningLetterList>.ProcessListDelegate((object) new ARDunningLetterProcess.\u003C\u003Ec__DisplayClass13_0()
    {
      filter = (ARDunningLetterProcess.ARDunningLetterRecordsParameters) ((PXSelectBase) this.Filter).Cache.CreateCopy((object) row)
    }, __methodptr(\u003CARDunningLetterRecordsParameters_RowSelected\u003Eb__0)));
    int? includeType = row.IncludeType;
    int num = 0;
    bool flag = includeType.GetValueOrDefault() == num & includeType.HasValue;
    PXUIFieldAttribute.SetEnabled<ARDunningLetterProcess.ARDunningLetterRecordsParameters.levelFrom>(sender, (object) null, !flag);
    PXUIFieldAttribute.SetEnabled<ARDunningLetterProcess.ARDunningLetterRecordsParameters.levelTo>(sender, (object) null, !flag);
    PXUIFieldAttribute.SetVisible<ARDunningLetterProcess.ARDunningLetterRecordsParameters.levelFrom>(sender, (object) null, !flag);
    PXUIFieldAttribute.SetVisible<ARDunningLetterProcess.ARDunningLetterRecordsParameters.levelTo>(sender, (object) null, !flag);
  }

  protected virtual void ARDunningLetterRecordsParameters_RowUpdated(
    PXCache sender,
    PXRowUpdatedEventArgs e)
  {
    ((PXSelectBase) this.DunningLetterList).Cache.Clear();
  }

  protected virtual void _(
    PX.Data.Events.RowUpdating<ARDunningLetterProcess.ARDunningLetterRecordsParameters> e)
  {
    if (this.DunningLetterProcessTypeARSetup != 0 || !((PXSelectBase<ARSetup>) this.arsetup).Current.DunningFeeInventoryID.HasValue || !((PXSelectBase) this.DunningLetterList).Cache.IsDirty)
      return;
    e.Cancel = true;
    if (((PXSelectBase<ARDunningLetterProcess.ARDunningLetterList>) this.DunningLetterList).Ask("Warning", "The changes in the dunning fee will be lost. Continue?", (MessageButtons) 4) != 6)
      return;
    e.Cancel = false;
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<ARDunningLetterProcess.ARDunningLetterRecordsParameters, ARDunningLetterProcess.ARDunningLetterRecordsParameters.customerClassID> e)
  {
    BqlCommand command = (BqlCommand) new SelectFromBase<ARDunningCustomerClass, TypeArrayOf<IFbqlJoin>.Empty>.AggregateTo<Max<ARDunningCustomerClass.dunningLetterLevel>>();
    if (e.NewValue != null)
      command = command.WhereAnd<Where<BqlOperand<ARDunningCustomerClass.customerClassID, IBqlString>.IsEqual<P.AsString>>>();
    ARDunningCustomerClass dunningCustomerClass = command.Select<ARDunningCustomerClass>((PXGraph) this, e.NewValue).FirstOrDefault<ARDunningCustomerClass>();
    e.Row.LevelTo = dunningCustomerClass.DunningLetterLevel;
  }

  protected virtual IEnumerable dunningLetterList()
  {
    ARDunningLetterProcess.ARDunningLetterRecordsParameters current = ((PXSelectBase<ARDunningLetterProcess.ARDunningLetterRecordsParameters>) this.Filter).Current;
    if (current != null && current.DocDate.HasValue)
    {
      foreach (ARDunningLetterProcess.ARDunningLetterList prepare in this.PrepareList())
      {
        prepare.Selected = (((PXSelectBase<ARDunningLetterProcess.ARDunningLetterList>) this.DunningLetterList).Locate(prepare) ?? prepare).Selected;
        GraphHelper.Hold(((PXSelectBase) this.DunningLetterList).Cache, (object) prepare);
        yield return (object) prepare;
      }
    }
  }

  private IEnumerable<ARDunningLetterProcess.ARDunningLetterList> PrepareList()
  {
    return (IEnumerable<ARDunningLetterProcess.ARDunningLetterList>) this.ComposeResult(this.GetData());
  }

  protected virtual IEnumerable<PXResult<Customer>> GetData()
  {
    ARSetup current1 = ((PXSelectBase<ARSetup>) this.arsetup).Current;
    bool flag1 = current1.PrepareDunningLetters == "B";
    bool flag2 = current1.PrepareDunningLetters == "C";
    ARDunningLetterProcess.ARDunningLetterRecordsParameters current2 = ((PXSelectBase<ARDunningLetterProcess.ARDunningLetterRecordsParameters>) this.Filter).Current;
    if (this.DunningLetterProcessTypeARSetup == 0)
    {
      PXSelectJoinGroupBy<Customer, InnerJoin<ARDunningLetterProcess.ARInvoiceWithDL, On<ARDunningLetterProcess.ARInvoiceWithDL.customerID, Equal<Customer.bAccountID>, And<ARDunningLetterProcess.ARInvoiceWithDL.dueDate, Less<Required<ARDunningLetterProcess.ARDunningLetterRecordsParameters.docDate>>>>, InnerJoin<CustomerAlias, On<Customer.sharedCreditCustomerID, Equal<CustomerAlias.bAccountID>>, LeftJoin<ARDunningCustomerClass, On<CustomerAlias.customerClassID, Equal<ARDunningCustomerClass.customerClassID>, And<Where<BqlOperand<ARDunningLetterProcess.ARInvoiceWithDL.dunningLetterLevel, IBqlInt>.Add<int1>, Equal<ARDunningCustomerClass.dunningLetterLevel>, Or<Where<ARDunningLetterProcess.ARInvoiceWithDL.dunningLetterLevel, IsNull, And<ARDunningCustomerClass.dunningLetterLevel, Equal<int1>>>>>>>, LeftJoin<ARDunningCustomerClassAlias, On<CustomerAlias.customerClassID, Equal<ARDunningCustomerClassAlias.customerClassID>, And<ARDunningLetterProcess.ARInvoiceWithDL.dunningLetterLevel, Equal<ARDunningCustomerClassAlias.dunningLetterLevel>>>, LeftJoin<ARBalances, On<ARBalances.customerID, Equal<Customer.sharedCreditCustomerID>, And<ARBalances.branchID, Equal<ARDunningLetterProcess.ARInvoiceWithDL.branchID>, And<ARBalances.customerLocationID, Equal<ARDunningLetterProcess.ARInvoiceWithDL.customerLocationID>>>>>>>>>, Where2<Where<Customer.printDunningLetters, Equal<True>, Or<Customer.mailDunningLetters, Equal<True>>>, And2<Where<ARDunningLetterProcess.ARInvoiceWithDL.dLReleased, Equal<True>, Or<ARDunningLetterProcess.ARInvoiceWithDL.dLReleased, IsNull>>, And2<Match<Current<AccessInfo.userName>>, And2<Where<Customer.customerClassID, Equal<Required<ARDunningLetterProcess.ARDunningLetterRecordsParameters.customerClassID>>, Or<Required<ARDunningLetterProcess.ARDunningLetterRecordsParameters.customerClassID>, IsNull>>, And<Where<ARDunningLetterProcess.ARInvoiceWithDL.dunningLetterLevel, IsNotNull, Or<ARDunningCustomerClass.dunningLetterLevel, IsNotNull>>>>>>>, Aggregate<GroupBy<Customer.sharedCreditCustomerID, GroupBy<ARDunningLetterProcess.ARInvoiceWithDL.branchID, Min<ARDunningLetterProcess.ARInvoiceWithDL.dueDate, Sum<ARDunningLetterProcess.ARInvoiceWithDL.docBal, Count<ARDunningLetterProcess.ARInvoiceWithDL.refNbr>>>>>>> selectJoinGroupBy = new PXSelectJoinGroupBy<Customer, InnerJoin<ARDunningLetterProcess.ARInvoiceWithDL, On<ARDunningLetterProcess.ARInvoiceWithDL.customerID, Equal<Customer.bAccountID>, And<ARDunningLetterProcess.ARInvoiceWithDL.dueDate, Less<Required<ARDunningLetterProcess.ARDunningLetterRecordsParameters.docDate>>>>, InnerJoin<CustomerAlias, On<Customer.sharedCreditCustomerID, Equal<CustomerAlias.bAccountID>>, LeftJoin<ARDunningCustomerClass, On<CustomerAlias.customerClassID, Equal<ARDunningCustomerClass.customerClassID>, And<Where<BqlOperand<ARDunningLetterProcess.ARInvoiceWithDL.dunningLetterLevel, IBqlInt>.Add<int1>, Equal<ARDunningCustomerClass.dunningLetterLevel>, Or<Where<ARDunningLetterProcess.ARInvoiceWithDL.dunningLetterLevel, IsNull, And<ARDunningCustomerClass.dunningLetterLevel, Equal<int1>>>>>>>, LeftJoin<ARDunningCustomerClassAlias, On<CustomerAlias.customerClassID, Equal<ARDunningCustomerClassAlias.customerClassID>, And<ARDunningLetterProcess.ARInvoiceWithDL.dunningLetterLevel, Equal<ARDunningCustomerClassAlias.dunningLetterLevel>>>, LeftJoin<ARBalances, On<ARBalances.customerID, Equal<Customer.sharedCreditCustomerID>, And<ARBalances.branchID, Equal<ARDunningLetterProcess.ARInvoiceWithDL.branchID>, And<ARBalances.customerLocationID, Equal<ARDunningLetterProcess.ARInvoiceWithDL.customerLocationID>>>>>>>>>, Where2<Where<Customer.printDunningLetters, Equal<True>, Or<Customer.mailDunningLetters, Equal<True>>>, And2<Where<ARDunningLetterProcess.ARInvoiceWithDL.dLReleased, Equal<True>, Or<ARDunningLetterProcess.ARInvoiceWithDL.dLReleased, IsNull>>, And2<Match<Current<AccessInfo.userName>>, And2<Where<Customer.customerClassID, Equal<Required<ARDunningLetterProcess.ARDunningLetterRecordsParameters.customerClassID>>, Or<Required<ARDunningLetterProcess.ARDunningLetterRecordsParameters.customerClassID>, IsNull>>, And<Where<ARDunningLetterProcess.ARInvoiceWithDL.dunningLetterLevel, IsNotNull, Or<ARDunningCustomerClass.dunningLetterLevel, IsNotNull>>>>>>>, Aggregate<GroupBy<Customer.sharedCreditCustomerID, GroupBy<ARDunningLetterProcess.ARInvoiceWithDL.branchID, Min<ARDunningLetterProcess.ARInvoiceWithDL.dueDate, Sum<ARDunningLetterProcess.ARInvoiceWithDL.docBal, Count<ARDunningLetterProcess.ARInvoiceWithDL.refNbr>>>>>>>((PXGraph) this);
      List<object> objectList = new List<object>()
      {
        (object) current2.DocDate,
        (object) current2.CustomerClassID,
        (object) current2.CustomerClassID
      };
      int? nullable;
      if (flag1)
      {
        nullable = current2.OrgBAccountID;
        if (nullable.HasValue)
        {
          ((PXSelectBase<Customer>) selectJoinGroupBy).WhereAnd<Where<ARDunningLetterProcess.ARInvoiceWithDL.branchID, Inside<Required<ARDunningLetterProcess.ARDunningLetterRecordsParameters.orgBAccountID>>>>();
          objectList.Add((object) current2.OrgBAccountID);
          goto label_7;
        }
      }
      if (flag2)
      {
        nullable = current2.OrganizationID;
        if (nullable.HasValue)
        {
          PXAccess.MasterCollection.Organization organizationById = PXAccess.GetOrganizationByID(current2.OrganizationID);
          current2.OrgBAccountID = ((PXAccess.Organization) organizationById).BAccountID;
          ((PXSelectBase<Customer>) selectJoinGroupBy).WhereAnd<Where<ARDunningLetterProcess.ARInvoiceWithDL.branchID, Inside<Required<ARDunningLetterProcess.ARDunningLetterRecordsParameters.orgBAccountID>>>>();
          objectList.Add((object) ((PXAccess.Organization) organizationById).BAccountID);
        }
      }
label_7:
      return (IEnumerable<PXResult<Customer>>) ((IEnumerable<PXResult<Customer>>) ((PXSelectBase<Customer>) selectJoinGroupBy).Select(objectList.ToArray())).ToList<PXResult<Customer>>();
    }
    if (this.DunningLetterProcessTypeARSetup != 1)
      throw new NotImplementedException();
    PXSelectJoinGroupBy<Customer, InnerJoin<ARDunningLetterProcess.ARInvoiceWithDL, On<ARDunningLetterProcess.ARInvoiceWithDL.customerID, Equal<Customer.bAccountID>, And<ARDunningLetterProcess.ARInvoiceWithDL.revoked, Equal<False>>>, InnerJoin<CustomerAlias, On<Customer.sharedCreditCustomerID, Equal<CustomerAlias.bAccountID>>, LeftJoin<ARDunningCustomerClass, On<CustomerAlias.customerClassID, Equal<ARDunningCustomerClass.customerClassID>, And<Where<BqlOperand<ARDunningLetterProcess.ARInvoiceWithDL.dunningLetterLevel, IBqlInt>.Add<int1>, Equal<ARDunningCustomerClass.dunningLetterLevel>, Or<Where<ARDunningLetterProcess.ARInvoiceWithDL.dunningLetterLevel, IsNull, And<ARDunningCustomerClass.dunningLetterLevel, Equal<int1>>>>>>>, LeftJoin<ARDunningCustomerClassAlias, On<CustomerAlias.customerClassID, Equal<ARDunningCustomerClassAlias.customerClassID>, And<ARDunningLetterProcess.ARInvoiceWithDL.dunningLetterLevel, Equal<ARDunningCustomerClassAlias.dunningLetterLevel>>>, LeftJoin<ARBalances, On<ARBalances.customerID, Equal<Customer.sharedCreditCustomerID>, And<ARBalances.branchID, Equal<ARDunningLetterProcess.ARInvoiceWithDL.branchID>, And<ARBalances.customerLocationID, Equal<ARDunningLetterProcess.ARInvoiceWithDL.customerLocationID>>>>>>>>>, Where2<Where<Customer.printDunningLetters, Equal<True>, Or<Customer.mailDunningLetters, Equal<True>>>, And2<Where<ARDunningLetterProcess.ARInvoiceWithDL.dLReleased, Equal<True>, Or<ARDunningLetterProcess.ARInvoiceWithDL.dLReleased, IsNull>>, And2<Match<Current<AccessInfo.userName>>, And<Add<ARDunningLetterProcess.ARInvoiceWithDL.dueDate, ARDunningCustomerClass.dueDays>, Less<Required<ARDunningLetterProcess.ARDunningLetterRecordsParameters.docDate>>, And<Where<ARDunningLetterProcess.ARInvoiceWithDL.dunningLetterLevel, IsNotNull, Or<ARDunningCustomerClass.dunningLetterLevel, IsNotNull>>>>>>>, Aggregate<GroupBy<ARDunningLetterProcess.ARInvoiceWithDL.dunningLetterLevel, GroupBy<Customer.sharedCreditCustomerID, GroupBy<ARDunningLetterProcess.ARInvoiceWithDL.branchID, Min<ARDunningLetterProcess.ARInvoiceWithDL.dueDate, Sum<ARDunningLetterProcess.ARInvoiceWithDL.docBal, Count<ARDunningLetterProcess.ARInvoiceWithDL.refNbr>>>>>>>> selectJoinGroupBy1 = new PXSelectJoinGroupBy<Customer, InnerJoin<ARDunningLetterProcess.ARInvoiceWithDL, On<ARDunningLetterProcess.ARInvoiceWithDL.customerID, Equal<Customer.bAccountID>, And<ARDunningLetterProcess.ARInvoiceWithDL.revoked, Equal<False>>>, InnerJoin<CustomerAlias, On<Customer.sharedCreditCustomerID, Equal<CustomerAlias.bAccountID>>, LeftJoin<ARDunningCustomerClass, On<CustomerAlias.customerClassID, Equal<ARDunningCustomerClass.customerClassID>, And<Where<BqlOperand<ARDunningLetterProcess.ARInvoiceWithDL.dunningLetterLevel, IBqlInt>.Add<int1>, Equal<ARDunningCustomerClass.dunningLetterLevel>, Or<Where<ARDunningLetterProcess.ARInvoiceWithDL.dunningLetterLevel, IsNull, And<ARDunningCustomerClass.dunningLetterLevel, Equal<int1>>>>>>>, LeftJoin<ARDunningCustomerClassAlias, On<CustomerAlias.customerClassID, Equal<ARDunningCustomerClassAlias.customerClassID>, And<ARDunningLetterProcess.ARInvoiceWithDL.dunningLetterLevel, Equal<ARDunningCustomerClassAlias.dunningLetterLevel>>>, LeftJoin<ARBalances, On<ARBalances.customerID, Equal<Customer.sharedCreditCustomerID>, And<ARBalances.branchID, Equal<ARDunningLetterProcess.ARInvoiceWithDL.branchID>, And<ARBalances.customerLocationID, Equal<ARDunningLetterProcess.ARInvoiceWithDL.customerLocationID>>>>>>>>>, Where2<Where<Customer.printDunningLetters, Equal<True>, Or<Customer.mailDunningLetters, Equal<True>>>, And2<Where<ARDunningLetterProcess.ARInvoiceWithDL.dLReleased, Equal<True>, Or<ARDunningLetterProcess.ARInvoiceWithDL.dLReleased, IsNull>>, And2<Match<Current<AccessInfo.userName>>, And<Add<ARDunningLetterProcess.ARInvoiceWithDL.dueDate, ARDunningCustomerClass.dueDays>, Less<Required<ARDunningLetterProcess.ARDunningLetterRecordsParameters.docDate>>, And<Where<ARDunningLetterProcess.ARInvoiceWithDL.dunningLetterLevel, IsNotNull, Or<ARDunningCustomerClass.dunningLetterLevel, IsNotNull>>>>>>>, Aggregate<GroupBy<ARDunningLetterProcess.ARInvoiceWithDL.dunningLetterLevel, GroupBy<Customer.sharedCreditCustomerID, GroupBy<ARDunningLetterProcess.ARInvoiceWithDL.branchID, Min<ARDunningLetterProcess.ARInvoiceWithDL.dueDate, Sum<ARDunningLetterProcess.ARInvoiceWithDL.docBal, Count<ARDunningLetterProcess.ARInvoiceWithDL.refNbr>>>>>>>>((PXGraph) this);
    List<object> objectList1 = new List<object>()
    {
      (object) current2.DocDate
    };
    int? nullable1;
    if (flag1)
    {
      nullable1 = current2.OrgBAccountID;
      if (nullable1.HasValue)
      {
        ((PXSelectBase<Customer>) selectJoinGroupBy1).WhereAnd<Where<ARDunningLetterProcess.ARInvoiceWithDL.branchID, Inside<Required<ARDunningLetterProcess.ARDunningLetterRecordsParameters.orgBAccountID>>>>();
        objectList1.Add((object) current2.OrgBAccountID);
        goto label_18;
      }
    }
    if (flag2)
    {
      nullable1 = current2.OrganizationID;
      if (nullable1.HasValue)
      {
        PXAccess.MasterCollection.Organization organizationById = PXAccess.GetOrganizationByID(current2.OrganizationID);
        current2.OrgBAccountID = ((PXAccess.Organization) organizationById).BAccountID;
        ((PXSelectBase<Customer>) selectJoinGroupBy1).WhereAnd<Where<ARDunningLetterProcess.ARInvoiceWithDL.branchID, Inside<Required<ARDunningLetterProcess.ARDunningLetterRecordsParameters.orgBAccountID>>>>();
        List<object> objectList2 = objectList1;
        int? nullable2;
        if (organizationById == null)
        {
          nullable1 = new int?();
          nullable2 = nullable1;
        }
        else
          nullable2 = ((PXAccess.Organization) organizationById).BAccountID;
        // ISSUE: variable of a boxed type
        __Boxed<int?> local = (ValueType) nullable2;
        objectList2.Add((object) local);
      }
    }
label_18:
    if (current2.CustomerClassID != null)
    {
      ((PXSelectBase<Customer>) selectJoinGroupBy1).WhereAnd<Where<Customer.customerClassID, Equal<Required<ARDunningLetterProcess.ARDunningLetterRecordsParameters.customerClassID>>>>();
      objectList1.Add((object) current2.CustomerClassID);
    }
    nullable1 = current2.IncludeType;
    if (nullable1.GetValueOrDefault() == 1)
    {
      ((PXSelectBase<Customer>) selectJoinGroupBy1).WhereAnd<Where2<Where<ARDunningLetterProcess.ARInvoiceWithDL.dunningLetterLevel, GreaterEqual<Required<ARDunningLetterProcess.ARDunningLetterRecordsParameters.levelFrom>>, And<ARDunningLetterProcess.ARInvoiceWithDL.dunningLetterLevel, LessEqual<Required<ARDunningLetterProcess.ARDunningLetterRecordsParameters.levelTo>>>>, Or<Where<ARDunningLetterProcess.ARInvoiceWithDL.dunningLetterLevel, IsNull, And<Required<ARDunningLetterProcess.ARDunningLetterRecordsParameters.levelFrom>, Less<int1>>>>>>();
      List<object> objectList3 = objectList1;
      nullable1 = current2.LevelFrom;
      // ISSUE: variable of a boxed type
      __Boxed<int?> local1 = (ValueType) (nullable1.HasValue ? new int?(nullable1.GetValueOrDefault() - 1) : new int?());
      objectList3.Add((object) local1);
      List<object> objectList4 = objectList1;
      nullable1 = current2.LevelTo;
      // ISSUE: variable of a boxed type
      __Boxed<int?> local2 = (ValueType) (nullable1.HasValue ? new int?(nullable1.GetValueOrDefault() - 1) : new int?());
      objectList4.Add((object) local2);
      List<object> objectList5 = objectList1;
      nullable1 = current2.LevelFrom;
      // ISSUE: variable of a boxed type
      __Boxed<int?> local3 = (ValueType) (nullable1.HasValue ? new int?(nullable1.GetValueOrDefault() - 1) : new int?());
      objectList5.Add((object) local3);
    }
    List<PXResult<Customer>> pxResultList = new List<PXResult<Customer>>();
    return (IEnumerable<PXResult<Customer>>) ((IEnumerable<PXResult<Customer>>) ((PXSelectBase<Customer>) selectJoinGroupBy1).Select(objectList1.ToArray())).ToList<PXResult<Customer>>();
  }

  protected virtual List<ARDunningLetterProcess.ARDunningLetterList> ComposeResult(
    IEnumerable<PXResult<Customer>> rows)
  {
    DateTime? docDate = ((PXSelectBase<ARDunningLetterProcess.ARDunningLetterRecordsParameters>) this.Filter).Current.DocDate;
    BqlCommand bqlCommand = (BqlCommand) new SelectFromBase<Customer, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<ARRegister>.On<BqlOperand<ARRegister.customerID, IBqlInt>.IsEqual<Customer.bAccountID>>>, FbqlJoins.Left<PX.Objects.AR.Standalone.ARInvoice>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.AR.Standalone.ARInvoice.docType, Equal<ARRegister.docType>>>>>.And<BqlOperand<PX.Objects.AR.Standalone.ARInvoice.refNbr, IBqlString>.IsEqual<ARRegister.refNbr>>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Customer.sharedCreditCustomerID, In<P.AsInt>>>>, And<BqlOperand<ARRegister.released, IBqlBool>.IsEqual<True>>>, And<BqlOperand<ARRegister.openDoc, IBqlBool>.IsEqual<True>>>, And<BqlOperand<ARRegister.voided, IBqlBool>.IsEqual<False>>>, And<BqlOperand<ARRegister.pendingPPD, IBqlBool>.IsNotEqual<True>>>>.And<BqlOperand<ARRegister.docDate, IBqlDateTime>.IsLessEqual<P.AsDateTime>>>.AggregateTo<GroupBy<Customer.sharedCreditCustomerID>, Count>();
    BqlCommand command = ((PXSelectBase<ARDunningLetterProcess.ARDunningLetterRecordsParameters>) this.Filter).Current.AddOpenPaymentsAndCreditMemos.GetValueOrDefault() ? bqlCommand.WhereAnd<Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ARRegister.docType, Equal<ARDocType.invoice>>>>, Or<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ARRegister.docType, Equal<ARDocType.prepaymentInvoice>>>>, And<BqlOperand<Current<ARDunningLetterProcess.ARDunningLetterRecordsParameters.addUnpaidPPI>, IBqlBool>.IsEqual<True>>>>.And<BqlOperand<ARRegister.pendingPayment, IBqlBool>.IsEqual<True>>>>, Or<BqlOperand<ARRegister.docType, IBqlString>.IsEqual<ARDocType.finCharge>>>, Or<BqlOperand<ARRegister.docType, IBqlString>.IsEqual<ARDocType.debitMemo>>>, Or<BqlOperand<ARRegister.docType, IBqlString>.IsEqual<ARDocType.payment>>>, Or<BqlOperand<ARRegister.docType, IBqlString>.IsEqual<ARDocType.creditMemo>>>, Or<BqlOperand<ARRegister.docType, IBqlString>.IsEqual<ARDocType.prepayment>>>>.Or<BqlOperand<ARRegister.docType, IBqlString>.IsEqual<ARDocType.refund>>>>() : bqlCommand.WhereAnd<Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ARRegister.docType, Equal<ARDocType.invoice>>>>, Or<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ARRegister.docType, Equal<ARDocType.prepaymentInvoice>>>>, And<BqlOperand<Current<ARDunningLetterProcess.ARDunningLetterRecordsParameters.addUnpaidPPI>, IBqlBool>.IsEqual<True>>>>.And<BqlOperand<ARRegister.pendingPayment, IBqlBool>.IsEqual<True>>>>, Or<BqlOperand<ARRegister.docType, IBqlString>.IsEqual<ARDocType.finCharge>>>>.Or<BqlOperand<ARRegister.docType, IBqlString>.IsEqual<ARDocType.debitMemo>>>>();
    if (this.DunningLetterProcessTypeARSetup == 1)
      command = command.WhereAnd<Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.AR.Standalone.ARInvoice.revoked, Equal<False>>>>>.Or<BqlOperand<PX.Objects.AR.Standalone.ARInvoice.revoked, IBqlBool>.IsNull>>>();
    int?[] array = rows.Select<PXResult<Customer>, int?>((Func<PXResult<Customer>, int?>) (_ => PXResult<Customer>.op_Implicit(_).SharedCreditCustomerID)).Distinct<int?>().ToArray<int?>();
    Dictionary<int?, int?> dictionary = command.CreateView((PXGraph) this).SelectMulti(new object[2]
    {
      (object) array,
      (object) docDate
    }).Cast<PXResult<Customer>>().ToDictionary<PXResult<Customer>, int?, int?>((Func<PXResult<Customer>, int?>) (k => ((PXResult) k).GetItem<Customer>().SharedCreditCustomerID), (Func<PXResult<Customer>, int?>) (v => ((PXResult) v).RowCount));
    List<ARDunningLetterProcess.ARDunningLetterList> dunningLetterListList = new List<ARDunningLetterProcess.ARDunningLetterList>();
    foreach (PXResult<Customer, ARDunningLetterProcess.ARInvoiceWithDL, CustomerAlias, ARDunningCustomerClass, ARDunningCustomerClassAlias, ARBalances> row in rows)
    {
      Customer customer = PXResult<Customer, ARDunningLetterProcess.ARInvoiceWithDL, CustomerAlias, ARDunningCustomerClass, ARDunningCustomerClassAlias, ARBalances>.op_Implicit(row);
      CustomerAlias customerAlias = PXResult<Customer, ARDunningLetterProcess.ARInvoiceWithDL, CustomerAlias, ARDunningCustomerClass, ARDunningCustomerClassAlias, ARBalances>.op_Implicit(row);
      ARDunningLetterProcess.ARInvoiceWithDL arInvoiceWithDl = PXResult<Customer, ARDunningLetterProcess.ARInvoiceWithDL, CustomerAlias, ARDunningCustomerClass, ARDunningCustomerClassAlias, ARBalances>.op_Implicit(row);
      ARBalances arBalances = PXResult<Customer, ARDunningLetterProcess.ARInvoiceWithDL, CustomerAlias, ARDunningCustomerClass, ARDunningCustomerClassAlias, ARBalances>.op_Implicit(row);
      ARDunningCustomerClass dunningCustomerClass = PXResult<Customer, ARDunningLetterProcess.ARInvoiceWithDL, CustomerAlias, ARDunningCustomerClass, ARDunningCustomerClassAlias, ARBalances>.op_Implicit(row);
      ARDunningCustomerClassAlias customerClassAlias = PXResult<Customer, ARDunningLetterProcess.ARInvoiceWithDL, CustomerAlias, ARDunningCustomerClass, ARDunningCustomerClassAlias, ARBalances>.op_Implicit(row);
      if (arInvoiceWithDl != null)
      {
        int? nullable1 = arInvoiceWithDl.DunningLetterLevel;
        int valueOrDefault = nullable1.GetValueOrDefault();
        int num1;
        if (!this.MaxDunningLevels.TryGetValue(customerAlias.CustomerClassID, out num1) || valueOrDefault != num1)
        {
          nullable1 = dunningCustomerClass.DueDays;
          DateTime dateTime1;
          DateTime? nullable2;
          if (nullable1.HasValue)
          {
            dateTime1 = arInvoiceWithDl.DueDate.Value;
            ref DateTime local = ref dateTime1;
            nullable1 = dunningCustomerClass.DueDays;
            double num2 = (double) nullable1.Value;
            DateTime dateTime2 = local.AddDays(num2);
            nullable2 = docDate;
            if ((nullable2.HasValue ? (dateTime2 >= nullable2.GetValueOrDefault() ? 1 : 0) : 0) != 0)
              continue;
          }
          nullable1 = dunningCustomerClass.DueDays;
          if (nullable1.HasValue)
          {
            nullable1 = customerClassAlias.DueDays;
            if (nullable1.HasValue && valueOrDefault > 0)
            {
              dateTime1 = arInvoiceWithDl.DunningLetterDate.Value;
              ref DateTime local = ref dateTime1;
              nullable1 = dunningCustomerClass.DueDays;
              int num3 = nullable1.Value;
              nullable1 = customerClassAlias.DueDays;
              int num4 = nullable1.Value;
              double num5 = (double) (num3 - num4);
              DateTime dateTime3 = local.AddDays(num5);
              nullable2 = docDate;
              if ((nullable2.HasValue ? (dateTime3 >= nullable2.GetValueOrDefault() ? 1 : 0) : 0) != 0)
                continue;
            }
          }
          PXAccess.MasterCollection.Branch branch = PXAccess.GetBranch(arInvoiceWithDl.BranchID);
          ARDunningLetterProcess.ARDunningLetterList dunningLetterList1 = new ARDunningLetterProcess.ARDunningLetterList();
          dunningLetterList1.BAccountID = customer.SharedCreditCustomerID;
          ARDunningLetterProcess.ARDunningLetterList dunningLetterList2 = dunningLetterList1;
          int? nullable3;
          if (branch == null)
          {
            nullable1 = new int?();
            nullable3 = nullable1;
          }
          else
          {
            PXAccess.MasterCollection.Organization organization = branch.Organization;
            if (organization == null)
            {
              nullable1 = new int?();
              nullable3 = nullable1;
            }
            else
              nullable3 = ((PXAccess.Organization) organization).OrganizationID;
          }
          dunningLetterList2.OrganizationID = nullable3;
          ARDunningLetterProcess.ARDunningLetterList dunningLetterList3 = dunningLetterList1;
          int? nullable4;
          if (branch == null)
          {
            nullable1 = new int?();
            nullable4 = nullable1;
          }
          else
            nullable4 = new int?(branch.BranchID);
          dunningLetterList3.BranchID = nullable4;
          dunningLetterList1.CustomerClassID = customer.CustomerClassID;
          dunningLetterList1.DocBal = arInvoiceWithDl.DocBal;
          dunningLetterList1.DueDate = arInvoiceWithDl.DueDate;
          ARDunningLetterProcess.ARDunningLetterList dunningLetterList4 = dunningLetterList1;
          nullable1 = dunningCustomerClass.DaysToSettle;
          int? nullable5 = new int?(nullable1.GetValueOrDefault());
          dunningLetterList4.DueDays = nullable5;
          dunningLetterList1.DunningLetterLevel = new int?(valueOrDefault + 1);
          dunningLetterList1.LastDunningLetterDate = arInvoiceWithDl.DunningLetterDate;
          dunningLetterList1.NumberOfOverdueDocuments = ((PXResult) row).RowCount;
          dunningLetterList1.CuryID = branch?.BaseCuryID;
          ARDunningLetterProcess.ARDunningLetterList dunningLetterList5 = dunningLetterList1;
          Decimal? nullable6;
          if (!PXAccess.FeatureInstalled<FeaturesSet.parentChildAccount>())
            nullable6 = arBalances.CurrentBal;
          else
            nullable6 = ((IEnumerable<PXResult<ARBalances>>) PXSelectBase<ARBalances, PXSelectJoinGroupBy<ARBalances, InnerJoin<Customer, On<Customer.bAccountID, Equal<ARBalances.customerID>>>, Where<ARBalances.branchID, Equal<Required<ARBalances.branchID>>, And<Customer.sharedCreditCustomerID, Equal<Required<Customer.sharedCreditCustomerID>>>>, Aggregate<GroupBy<ARBalances.customerID, Sum<ARBalances.currentBal>>>>.Config>.Select((PXGraph) this, new object[2]
            {
              (object) dunningLetterList1.BranchID,
              (object) dunningLetterList1.BAccountID
            })).AsEnumerable<PXResult<ARBalances>>().Sum<PXResult<ARBalances>>((Func<PXResult<ARBalances>, Decimal?>) (cons => PXResult<ARBalances>.op_Implicit(cons).CurrentBal));
          dunningLetterList5.OrigDocAmt = nullable6;
          int? nullable7;
          dunningLetterList1.NumberOfDocuments = dictionary.TryGetValue(dunningLetterList1.BAccountID, out nullable7) ? nullable7 : new int?(0);
          dunningLetterList1.NoteID = customer.NoteID;
          dunningLetterListList.Add(dunningLetterList1);
        }
      }
    }
    return dunningLetterListList;
  }

  private static void DunningLetterProc(
    List<ARDunningLetterProcess.ARDunningLetterList> list,
    ARDunningLetterProcess.ARDunningLetterRecordsParameters filter)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    ARDunningLetterProcess.\u003C\u003Ec__DisplayClass21_0 cDisplayClass210 = new ARDunningLetterProcess.\u003C\u003Ec__DisplayClass21_0()
    {
      list = list,
      filter = filter,
      graph = PXGraph.CreateInstance<ARDunningLetterProcess>()
    };
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    cDisplayClass210.orgDLBranches = GraphHelper.RowCast<PX.Objects.GL.DAC.Organization>((IEnumerable) PXSelectBase<PX.Objects.GL.DAC.Organization, PXSelect<PX.Objects.GL.DAC.Organization>.Config>.Select((PXGraph) cDisplayClass210.graph, Array.Empty<object>())).ToDictionary<PX.Objects.GL.DAC.Organization, int?, int?>((Func<PX.Objects.GL.DAC.Organization, int?>) (_ => _.OrganizationID), (Func<PX.Objects.GL.DAC.Organization, int?>) (_ => _.DunningFeeBranchID));
    // ISSUE: reference to a compiler-generated field
    // ISSUE: method pointer
    PXLongOperation.StartOperation((PXGraph) cDisplayClass210.graph, new PXToggleAsyncDelegate((object) cDisplayClass210, __methodptr(\u003CDunningLetterProc\u003Eb__2)));
  }

  [Obsolete("This item has been deprecated and will be removed in Acumatica ERP 2024 R1.")]
  public static ARDunningLetter CreateDunningLetter(
    DunningLetterMassProcess graph,
    int? bAccountID,
    int? branchID,
    int? organizationID,
    DateTime? docDate,
    int? dueDays,
    List<int> includedLevels,
    bool includeNonOverdue,
    bool processByCutomer,
    string consolidationSettings,
    List<int> dueDaysByLevel,
    bool addOpenPaymentsAndCreditMemos = false)
  {
    ARDunningLetterProcess instance = PXGraph.CreateInstance<ARDunningLetterProcess>();
    ARDunningLetterProcess.ARDunningLetterRecordsParameters filter = ((PXSelectBase<ARDunningLetterProcess.ARDunningLetterRecordsParameters>) instance.Filter).Insert(new ARDunningLetterProcess.ARDunningLetterRecordsParameters()
    {
      DocDate = docDate,
      IncludeNonOverdueDunning = new bool?(includeNonOverdue),
      AddOpenPaymentsAndCreditMemos = new bool?(addOpenPaymentsAndCreditMemos)
    });
    return instance.CreateDunningLetter(bAccountID, branchID, organizationID, dueDays, includedLevels, processByCutomer, consolidationSettings, dueDaysByLevel, filter);
  }

  protected virtual ARDunningLetter CreateDunningLetter(
    int? bAccountID,
    int? branchID,
    int? organizationID,
    int? dueDays,
    List<int> includedLevels,
    bool processByCutomer,
    string consolidationSettings,
    List<int> dueDaysByLevel,
    ARDunningLetterProcess.ARDunningLetterRecordsParameters filter)
  {
    DateTime? docDate = filter.DocDate;
    bool valueOrDefault1 = filter.IncludeNonOverdueDunning.GetValueOrDefault();
    bool valueOrDefault2 = filter.AddOpenPaymentsAndCreditMemos.GetValueOrDefault();
    ((PXGraph) this).Clear();
    ((PXSelectBase<ARDunningLetterProcess.ARDunningLetterRecordsParameters>) this.Filter).Current = filter;
    int count = dueDaysByLevel.Count;
    ARDunningLetter doc = ((PXSelectBase<ARDunningLetter>) this.docs).Insert(ARDunningLetterProcess.CreateDunningLetterHeader((PXGraph) this, bAccountID, branchID, docDate, dueDays, consolidationSettings));
    foreach (PXResult<ARDunningLetterProcess.ARInvoiceWithDL> invoice in ARDunningLetterProcess.GetInvoiceList((PXGraph) this, bAccountID, branchID, organizationID, docDate, includedLevels, valueOrDefault1, processByCutomer, consolidationSettings, dueDaysByLevel))
    {
      ARDunningLetterDetail dunningLetterDetail = ARDunningLetterProcess.CreateDunningLetterDetail(docDate, processByCutomer, PXResult<ARDunningLetterProcess.ARInvoiceWithDL>.op_Implicit(invoice), dueDaysByLevel);
      ARDunningLetter arDunningLetter = doc;
      int? dunningLetterLevel = doc.DunningLetterLevel;
      int valueOrDefault3 = dunningLetterLevel.GetValueOrDefault();
      dunningLetterLevel = dunningLetterDetail.DunningLetterLevel;
      int valueOrDefault4 = dunningLetterLevel.GetValueOrDefault();
      int? nullable = new int?(Math.Max(valueOrDefault3, valueOrDefault4));
      arDunningLetter.DunningLetterLevel = nullable;
      ((PXSelectBase<ARDunningLetterDetail>) this.docsDet).Insert(dunningLetterDetail);
    }
    ARDunningLetter arDunningLetter1 = doc;
    int? dunningLetterLevel1 = doc.DunningLetterLevel;
    int num1 = count;
    bool? nullable1 = new bool?(dunningLetterLevel1.GetValueOrDefault() == num1 & dunningLetterLevel1.HasValue);
    arDunningLetter1.LastLevel = nullable1;
    ARDunningCustomerClass dunningCustomerClass = PXResultset<ARDunningCustomerClass>.op_Implicit(PXSelectBase<ARDunningCustomerClass, PXViewOf<ARDunningCustomerClass>.BasedOn<SelectFromBase<ARDunningCustomerClass, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<Customer>.On<BqlOperand<ARDunningCustomerClass.customerClassID, IBqlString>.IsEqual<Customer.customerClassID>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Customer.bAccountID, Equal<P.AsInt>>>>>.And<BqlOperand<ARDunningCustomerClass.dunningLetterLevel, IBqlInt>.IsEqual<P.AsInt>>>>.Config>.SelectMultiBound((PXGraph) this, (object[]) null, new object[2]
    {
      (object) doc.BAccountID,
      (object) doc.DunningLetterLevel
    }));
    Decimal? dunningFee = dunningCustomerClass.DunningFee;
    if (dunningFee.HasValue)
    {
      dunningFee = dunningCustomerClass.DunningFee;
      Decimal num2 = 0M;
      if (!(dunningFee.GetValueOrDefault() == num2 & dunningFee.HasValue))
        doc.DunningFee = dunningCustomerClass.DunningFee;
    }
    if (valueOrDefault2)
    {
      foreach (PXResult<ARPayment, Customer> payment in ARDunningLetterProcess.GetPaymentList((PXGraph) this, bAccountID, branchID, organizationID, docDate, processByCutomer, consolidationSettings))
        ((PXSelectBase<ARDunningLetterDetail>) this.docsDet).Insert(ARDunningLetterProcess.CreateDunningLetterDetailForPayment(payment, docDate));
    }
    ARDunningLetter dunningLetter = this.ExpandDunningLetter(doc, bAccountID, branchID, organizationID, dueDays, includedLevels, processByCutomer, consolidationSettings, dueDaysByLevel, filter);
    ((PXSelectBase<ARDunningLetter>) this.docs).Update(dunningLetter);
    ((PXGraph) this).Actions.PressSave();
    return dunningLetter;
  }

  protected virtual ARDunningLetter ExpandDunningLetter(
    ARDunningLetter doc,
    int? bAccountID,
    int? branchID,
    int? organizationID,
    int? dueDays,
    List<int> includedLevels,
    bool processByCutomer,
    string consolidationSettings,
    List<int> dueDaysByLevel,
    ARDunningLetterProcess.ARDunningLetterRecordsParameters filter)
  {
    return doc;
  }

  private static List<PXResult<ARDunningLetterProcess.ARInvoiceWithDL>> GetInvoiceList(
    PXGraph graph,
    int? bAccountID,
    int? branchID,
    int? organizationID,
    DateTime? docDate,
    List<int> includedLevels,
    bool includeNonOverdue,
    bool processByCutomer,
    string consolidationSettings,
    List<int> dueDaysByLevel)
  {
    if (processByCutomer)
    {
      PXSelectJoinGroupBy<ARDunningLetterProcess.ARInvoiceWithDL, InnerJoin<PX.Objects.GL.Branch, On<PX.Objects.GL.Branch.branchID, Equal<ARDunningLetterProcess.ARInvoiceWithDL.branchID>>>, Where<ARDunningLetterProcess.ARInvoiceWithDL.sharedCreditCustomerID, Equal<Required<ARDunningLetterProcess.ARInvoiceWithDL.customerID>>, And<ARDunningLetterProcess.ARInvoiceWithDL.docDate, LessEqual<Required<ARDunningLetterProcess.ARInvoiceWithDL.docDate>>>>, Aggregate<GroupBy<ARDunningLetterProcess.ARInvoiceWithDL.released, GroupBy<ARDunningLetterProcess.ARInvoiceWithDL.refNbr, GroupBy<ARDunningLetterProcess.ARInvoiceWithDL.docType>>>>> selectJoinGroupBy = new PXSelectJoinGroupBy<ARDunningLetterProcess.ARInvoiceWithDL, InnerJoin<PX.Objects.GL.Branch, On<PX.Objects.GL.Branch.branchID, Equal<ARDunningLetterProcess.ARInvoiceWithDL.branchID>>>, Where<ARDunningLetterProcess.ARInvoiceWithDL.sharedCreditCustomerID, Equal<Required<ARDunningLetterProcess.ARInvoiceWithDL.customerID>>, And<ARDunningLetterProcess.ARInvoiceWithDL.docDate, LessEqual<Required<ARDunningLetterProcess.ARInvoiceWithDL.docDate>>>>, Aggregate<GroupBy<ARDunningLetterProcess.ARInvoiceWithDL.released, GroupBy<ARDunningLetterProcess.ARInvoiceWithDL.refNbr, GroupBy<ARDunningLetterProcess.ARInvoiceWithDL.docType>>>>>(graph);
      if (!includeNonOverdue)
        ((PXSelectBase<ARDunningLetterProcess.ARInvoiceWithDL>) selectJoinGroupBy).WhereAnd<Where<ARDunningLetterProcess.ARInvoiceWithDL.dueDate, Less<Required<ARInvoice.dueDate>>>>();
      else
        ((PXSelectBase<ARDunningLetterProcess.ARInvoiceWithDL>) selectJoinGroupBy).WhereAnd<Where<Required<ARInvoice.dueDate>, IsNotNull>>();
      int? nullable = new int?();
      switch (consolidationSettings)
      {
        case "B":
          ((PXSelectBase<ARDunningLetterProcess.ARInvoiceWithDL>) selectJoinGroupBy).WhereAnd<Where<ARDunningLetterProcess.ARInvoiceWithDL.branchID, Equal<Required<ARDunningLetterProcess.ARInvoiceWithDL.branchID>>>>();
          nullable = branchID;
          break;
        case "C":
          ((PXSelectBase<ARDunningLetterProcess.ARInvoiceWithDL>) selectJoinGroupBy).WhereAnd<Where<PX.Objects.GL.Branch.organizationID, Equal<Required<PX.Objects.GL.Branch.organizationID>>>>();
          nullable = organizationID;
          break;
      }
      return ((IEnumerable<PXResult<ARDunningLetterProcess.ARInvoiceWithDL>>) ((PXSelectBase<ARDunningLetterProcess.ARInvoiceWithDL>) selectJoinGroupBy).Select(new object[4]
      {
        (object) bAccountID,
        (object) docDate,
        (object) docDate,
        (object) nullable
      })).ToList<PXResult<ARDunningLetterProcess.ARInvoiceWithDL>>();
    }
    List<PXResult<ARDunningLetterProcess.ARInvoiceWithDL>> first = new List<PXResult<ARDunningLetterProcess.ARInvoiceWithDL>>();
    foreach (int includedLevel in includedLevels)
    {
      PXSelectJoinGroupBy<ARDunningLetterProcess.ARInvoiceWithDL, InnerJoin<PX.Objects.GL.Branch, On<PX.Objects.GL.Branch.branchID, Equal<ARDunningLetterProcess.ARInvoiceWithDL.branchID>>>, Where<ARDunningLetterProcess.ARInvoiceWithDL.sharedCreditCustomerID, Equal<Required<ARDunningLetterProcess.ARInvoiceWithDL.customerID>>, And<ARDunningLetterProcess.ARInvoiceWithDL.revoked, Equal<False>, And<ARDunningLetterProcess.ARInvoiceWithDL.dueDate, Less<Required<ARInvoice.dueDate>>, And<ARDunningLetterProcess.ARInvoiceWithDL.docDate, LessEqual<Required<ARDunningLetterProcess.ARInvoiceWithDL.docDate>>, And<Where<ARDunningLetterProcess.ARInvoiceWithDL.dunningLetterLevel, Equal<Required<ARDunningLetter.dunningLetterLevel>>, Or<Where<ARDunningLetterProcess.ARInvoiceWithDL.dunningLetterLevel, IsNull, And<Required<ARDunningLetter.dunningLetterLevel>, Equal<int0>>>>>>>>>>, Aggregate<GroupBy<ARDunningLetterProcess.ARInvoiceWithDL.dunningLetterLevel, GroupBy<ARDunningLetterProcess.ARInvoiceWithDL.released, GroupBy<ARDunningLetterProcess.ARInvoiceWithDL.refNbr, GroupBy<ARDunningLetterProcess.ARInvoiceWithDL.docType>>>>>> selectJoinGroupBy1 = new PXSelectJoinGroupBy<ARDunningLetterProcess.ARInvoiceWithDL, InnerJoin<PX.Objects.GL.Branch, On<PX.Objects.GL.Branch.branchID, Equal<ARDunningLetterProcess.ARInvoiceWithDL.branchID>>>, Where<ARDunningLetterProcess.ARInvoiceWithDL.sharedCreditCustomerID, Equal<Required<ARDunningLetterProcess.ARInvoiceWithDL.customerID>>, And<ARDunningLetterProcess.ARInvoiceWithDL.revoked, Equal<False>, And<ARDunningLetterProcess.ARInvoiceWithDL.dueDate, Less<Required<ARInvoice.dueDate>>, And<ARDunningLetterProcess.ARInvoiceWithDL.docDate, LessEqual<Required<ARDunningLetterProcess.ARInvoiceWithDL.docDate>>, And<Where<ARDunningLetterProcess.ARInvoiceWithDL.dunningLetterLevel, Equal<Required<ARDunningLetter.dunningLetterLevel>>, Or<Where<ARDunningLetterProcess.ARInvoiceWithDL.dunningLetterLevel, IsNull, And<Required<ARDunningLetter.dunningLetterLevel>, Equal<int0>>>>>>>>>>, Aggregate<GroupBy<ARDunningLetterProcess.ARInvoiceWithDL.dunningLetterLevel, GroupBy<ARDunningLetterProcess.ARInvoiceWithDL.released, GroupBy<ARDunningLetterProcess.ARInvoiceWithDL.refNbr, GroupBy<ARDunningLetterProcess.ARInvoiceWithDL.docType>>>>>>(graph);
      int? nullable = new int?();
      switch (consolidationSettings)
      {
        case "B":
          ((PXSelectBase<ARDunningLetterProcess.ARInvoiceWithDL>) selectJoinGroupBy1).WhereAnd<Where<ARDunningLetterProcess.ARInvoiceWithDL.branchID, Equal<Required<ARDunningLetterProcess.ARInvoiceWithDL.branchID>>>>();
          nullable = branchID;
          break;
        case "C":
          ((PXSelectBase<ARDunningLetterProcess.ARInvoiceWithDL>) selectJoinGroupBy1).WhereAnd<Where<PX.Objects.GL.Branch.organizationID, Equal<Required<PX.Objects.GL.Branch.organizationID>>>>();
          nullable = organizationID;
          break;
      }
      first = first.Concat<PXResult<ARDunningLetterProcess.ARInvoiceWithDL>>((IEnumerable<PXResult<ARDunningLetterProcess.ARInvoiceWithDL>>) ((PXSelectBase<ARDunningLetterProcess.ARInvoiceWithDL>) selectJoinGroupBy1).Select(new object[6]
      {
        (object) bAccountID,
        (object) docDate.Value.AddDays((double) (-1 * dueDaysByLevel[includedLevel - 1])),
        (object) docDate,
        (object) (includedLevel - 1),
        (object) (includedLevel - 1),
        (object) nullable
      })).ToList<PXResult<ARDunningLetterProcess.ARInvoiceWithDL>>();
      if (includedLevel == 1 & includeNonOverdue)
      {
        PXSelectJoinGroupBy<ARDunningLetterProcess.ARInvoiceWithDL, InnerJoin<PX.Objects.GL.Branch, On<PX.Objects.GL.Branch.branchID, Equal<ARDunningLetterProcess.ARInvoiceWithDL.branchID>>>, Where<ARDunningLetterProcess.ARInvoiceWithDL.sharedCreditCustomerID, Equal<Required<ARDunningLetterProcess.ARInvoiceWithDL.customerID>>, And<ARDunningLetterProcess.ARInvoiceWithDL.revoked, Equal<False>, And2<Where<ARDunningLetterProcess.ARInvoiceWithDL.dunningLetterLevel, IsNull, Or<ARDunningLetterProcess.ARInvoiceWithDL.dunningLetterLevel, Equal<int0>>>, And<ARDunningLetterProcess.ARInvoiceWithDL.dueDate, Less<Required<ARDunningLetterProcess.ARInvoiceWithDL.docDate>>, And<ARDunningLetterProcess.ARInvoiceWithDL.dueDate, GreaterEqual<Required<ARDunningLetterProcess.ARInvoiceWithDL.docDate>>, And<ARDunningLetterProcess.ARInvoiceWithDL.docDate, LessEqual<Required<ARDunningLetterProcess.ARInvoiceWithDL.docDate>>>>>>>>, Aggregate<GroupBy<ARDunningLetterProcess.ARInvoiceWithDL.dunningLetterLevel, GroupBy<ARDunningLetterProcess.ARInvoiceWithDL.released, GroupBy<ARDunningLetterProcess.ARInvoiceWithDL.refNbr, GroupBy<ARDunningLetterProcess.ARInvoiceWithDL.docType>>>>>> selectJoinGroupBy2 = new PXSelectJoinGroupBy<ARDunningLetterProcess.ARInvoiceWithDL, InnerJoin<PX.Objects.GL.Branch, On<PX.Objects.GL.Branch.branchID, Equal<ARDunningLetterProcess.ARInvoiceWithDL.branchID>>>, Where<ARDunningLetterProcess.ARInvoiceWithDL.sharedCreditCustomerID, Equal<Required<ARDunningLetterProcess.ARInvoiceWithDL.customerID>>, And<ARDunningLetterProcess.ARInvoiceWithDL.revoked, Equal<False>, And2<Where<ARDunningLetterProcess.ARInvoiceWithDL.dunningLetterLevel, IsNull, Or<ARDunningLetterProcess.ARInvoiceWithDL.dunningLetterLevel, Equal<int0>>>, And<ARDunningLetterProcess.ARInvoiceWithDL.dueDate, Less<Required<ARDunningLetterProcess.ARInvoiceWithDL.docDate>>, And<ARDunningLetterProcess.ARInvoiceWithDL.dueDate, GreaterEqual<Required<ARDunningLetterProcess.ARInvoiceWithDL.docDate>>, And<ARDunningLetterProcess.ARInvoiceWithDL.docDate, LessEqual<Required<ARDunningLetterProcess.ARInvoiceWithDL.docDate>>>>>>>>, Aggregate<GroupBy<ARDunningLetterProcess.ARInvoiceWithDL.dunningLetterLevel, GroupBy<ARDunningLetterProcess.ARInvoiceWithDL.released, GroupBy<ARDunningLetterProcess.ARInvoiceWithDL.refNbr, GroupBy<ARDunningLetterProcess.ARInvoiceWithDL.docType>>>>>>(graph);
        switch (consolidationSettings)
        {
          case "B":
            ((PXSelectBase<ARDunningLetterProcess.ARInvoiceWithDL>) selectJoinGroupBy2).WhereAnd<Where<ARDunningLetterProcess.ARInvoiceWithDL.branchID, Equal<Required<ARDunningLetterProcess.ARInvoiceWithDL.branchID>>>>();
            break;
          case "C":
            ((PXSelectBase<ARDunningLetterProcess.ARInvoiceWithDL>) selectJoinGroupBy2).WhereAnd<Where<PX.Objects.GL.Branch.organizationID, Equal<Required<PX.Objects.GL.Branch.organizationID>>>>();
            break;
        }
        first = first.Concat<PXResult<ARDunningLetterProcess.ARInvoiceWithDL>>((IEnumerable<PXResult<ARDunningLetterProcess.ARInvoiceWithDL>>) ((PXSelectBase<ARDunningLetterProcess.ARInvoiceWithDL>) selectJoinGroupBy2).Select(new object[5]
        {
          (object) bAccountID,
          (object) docDate,
          (object) docDate.Value.AddDays((double) (-1 * dueDaysByLevel[0])),
          (object) docDate,
          (object) nullable
        })).ToList<PXResult<ARDunningLetterProcess.ARInvoiceWithDL>>();
      }
    }
    if (includeNonOverdue)
    {
      PXSelectJoinGroupBy<ARDunningLetterProcess.ARInvoiceWithDL, InnerJoin<PX.Objects.GL.Branch, On<PX.Objects.GL.Branch.branchID, Equal<ARDunningLetterProcess.ARInvoiceWithDL.branchID>>>, Where<ARDunningLetterProcess.ARInvoiceWithDL.sharedCreditCustomerID, Equal<Required<ARDunningLetterProcess.ARInvoiceWithDL.customerID>>, And<ARDunningLetterProcess.ARInvoiceWithDL.revoked, Equal<False>, And2<Where<ARDunningLetterProcess.ARInvoiceWithDL.dunningLetterLevel, IsNull, Or<ARDunningLetterProcess.ARInvoiceWithDL.dunningLetterLevel, Equal<int0>>>, And<ARDunningLetterProcess.ARInvoiceWithDL.dueDate, GreaterEqual<Required<ARDunningLetterProcess.ARInvoiceWithDL.docDate>>, And<ARDunningLetterProcess.ARInvoiceWithDL.docDate, LessEqual<Required<ARDunningLetterProcess.ARInvoiceWithDL.docDate>>>>>>>, Aggregate<GroupBy<ARDunningLetterProcess.ARInvoiceWithDL.dunningLetterLevel, GroupBy<ARDunningLetterProcess.ARInvoiceWithDL.released, GroupBy<ARDunningLetterProcess.ARInvoiceWithDL.refNbr, GroupBy<ARDunningLetterProcess.ARInvoiceWithDL.docType>>>>>> selectJoinGroupBy = new PXSelectJoinGroupBy<ARDunningLetterProcess.ARInvoiceWithDL, InnerJoin<PX.Objects.GL.Branch, On<PX.Objects.GL.Branch.branchID, Equal<ARDunningLetterProcess.ARInvoiceWithDL.branchID>>>, Where<ARDunningLetterProcess.ARInvoiceWithDL.sharedCreditCustomerID, Equal<Required<ARDunningLetterProcess.ARInvoiceWithDL.customerID>>, And<ARDunningLetterProcess.ARInvoiceWithDL.revoked, Equal<False>, And2<Where<ARDunningLetterProcess.ARInvoiceWithDL.dunningLetterLevel, IsNull, Or<ARDunningLetterProcess.ARInvoiceWithDL.dunningLetterLevel, Equal<int0>>>, And<ARDunningLetterProcess.ARInvoiceWithDL.dueDate, GreaterEqual<Required<ARDunningLetterProcess.ARInvoiceWithDL.docDate>>, And<ARDunningLetterProcess.ARInvoiceWithDL.docDate, LessEqual<Required<ARDunningLetterProcess.ARInvoiceWithDL.docDate>>>>>>>, Aggregate<GroupBy<ARDunningLetterProcess.ARInvoiceWithDL.dunningLetterLevel, GroupBy<ARDunningLetterProcess.ARInvoiceWithDL.released, GroupBy<ARDunningLetterProcess.ARInvoiceWithDL.refNbr, GroupBy<ARDunningLetterProcess.ARInvoiceWithDL.docType>>>>>>(graph);
      int? nullable = new int?();
      switch (consolidationSettings)
      {
        case "B":
          ((PXSelectBase<ARDunningLetterProcess.ARInvoiceWithDL>) selectJoinGroupBy).WhereAnd<Where<ARDunningLetterProcess.ARInvoiceWithDL.branchID, Equal<Required<ARDunningLetterProcess.ARInvoiceWithDL.branchID>>>>();
          nullable = branchID;
          break;
        case "C":
          ((PXSelectBase<ARDunningLetterProcess.ARInvoiceWithDL>) selectJoinGroupBy).WhereAnd<Where<PX.Objects.GL.Branch.organizationID, Equal<Required<PX.Objects.GL.Branch.organizationID>>>>();
          nullable = organizationID;
          break;
      }
      first = first.Concat<PXResult<ARDunningLetterProcess.ARInvoiceWithDL>>((IEnumerable<PXResult<ARDunningLetterProcess.ARInvoiceWithDL>>) ((PXSelectBase<ARDunningLetterProcess.ARInvoiceWithDL>) selectJoinGroupBy).Select(new object[4]
      {
        (object) bAccountID,
        (object) docDate,
        (object) docDate,
        (object) nullable
      })).ToList<PXResult<ARDunningLetterProcess.ARInvoiceWithDL>>();
    }
    return first;
  }

  private static List<PXResult<ARPayment, Customer>> GetPaymentList(
    PXGraph graph,
    int? bAccountID,
    int? branchID,
    int? organizationID,
    DateTime? docDate,
    bool processByCutomer,
    string consolidationSettings)
  {
    BqlCommand command = (BqlCommand) new SelectFromBase<ARPayment, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<Customer>.On<BqlOperand<Customer.bAccountID, IBqlInt>.IsEqual<ARPayment.customerID>>>, FbqlJoins.Inner<PX.Objects.GL.Branch>.On<BqlOperand<PX.Objects.GL.Branch.branchID, IBqlInt>.IsEqual<ARPayment.branchID>>>, FbqlJoins.Left<PX.Objects.AR.Standalone.ARInvoice>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.AR.Standalone.ARInvoice.docType, Equal<ARPayment.docType>>>>>.And<BqlOperand<PX.Objects.AR.Standalone.ARInvoice.refNbr, IBqlString>.IsEqual<ARPayment.refNbr>>>>>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ARPayment.released, Equal<True>>>>, And<BqlOperand<ARPayment.openDoc, IBqlBool>.IsEqual<True>>>, And<BqlOperand<ARPayment.voided, IBqlBool>.IsEqual<False>>>, And<BqlOperand<ARRegister.pendingPPD, IBqlBool>.IsNotEqual<True>>>, And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ARPayment.docType, Equal<ARDocType.payment>>>>, Or<BqlOperand<ARPayment.docType, IBqlString>.IsEqual<ARDocType.creditMemo>>>, Or<BqlOperand<ARPayment.docType, IBqlString>.IsEqual<ARDocType.prepayment>>>>.Or<BqlOperand<ARPayment.docType, IBqlString>.IsEqual<ARDocType.refund>>>>, And<BqlOperand<Customer.sharedCreditCustomerID, IBqlInt>.IsEqual<P.AsInt>>>>.And<BqlOperand<ARPayment.docDate, IBqlDateTime>.IsLessEqual<P.AsDateTime>>>();
    int? nullable = new int?();
    switch (consolidationSettings)
    {
      case "B":
        command = command.WhereAnd<Where<BqlOperand<ARPayment.branchID, IBqlInt>.IsEqual<P.AsInt>>>();
        nullable = branchID;
        break;
      case "C":
        command = command.WhereAnd<Where<BqlOperand<PX.Objects.GL.Branch.organizationID, IBqlInt>.IsEqual<P.AsInt>>>();
        nullable = organizationID;
        break;
    }
    if (!processByCutomer)
      command = command.WhereAnd<Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.AR.Standalone.ARInvoice.revoked, Equal<False>>>>>.Or<BqlOperand<PX.Objects.AR.Standalone.ARInvoice.revoked, IBqlBool>.IsNull>>>();
    return command.CreateView(graph).SelectMulti(new object[3]
    {
      (object) bAccountID,
      (object) docDate,
      (object) nullable
    }).Cast<PXResult<ARPayment, Customer>>().ToList<PXResult<ARPayment, Customer>>();
  }

  private static ARDunningLetter CreateDunningLetterHeader(
    PXGraph graph,
    int? bAccountID,
    int? branchID,
    DateTime? docDate,
    int? dueDays,
    string consolidationSettings)
  {
    ARDunningLetter dunningLetterHeader = new ARDunningLetter();
    dunningLetterHeader.BAccountID = bAccountID;
    dunningLetterHeader.BranchID = branchID;
    dunningLetterHeader.DunningLetterDate = docDate;
    dunningLetterHeader.Deadline = new DateTime?(docDate.Value.AddDays((double) dueDays.Value));
    dunningLetterHeader.ConsolidationSettings = consolidationSettings;
    dunningLetterHeader.Released = new bool?(false);
    dunningLetterHeader.Printed = new bool?(false);
    dunningLetterHeader.Emailed = new bool?(false);
    dunningLetterHeader.LastLevel = new bool?(false);
    Customer customer = PXResultset<Customer>.op_Implicit(PXSelectBase<Customer, PXSelect<Customer, Where<Customer.bAccountID, Equal<Required<Customer.bAccountID>>>>.Config>.Select(graph, new object[1]
    {
      (object) bAccountID
    }));
    ARDunningLetter arDunningLetter1 = dunningLetterHeader;
    bool? printDunningLetters = customer.PrintDunningLetters;
    bool flag1 = false;
    bool? nullable1 = new bool?(printDunningLetters.GetValueOrDefault() == flag1 & printDunningLetters.HasValue);
    arDunningLetter1.DontPrint = nullable1;
    ARDunningLetter arDunningLetter2 = dunningLetterHeader;
    bool? mailDunningLetters = customer.MailDunningLetters;
    bool flag2 = false;
    bool? nullable2 = new bool?(mailDunningLetters.GetValueOrDefault() == flag2 & mailDunningLetters.HasValue);
    arDunningLetter2.DontEmail = nullable2;
    return dunningLetterHeader;
  }

  private static ARDunningLetterDetail CreateDunningLetterDetail(
    DateTime? docDate,
    bool processByCutomer,
    ARDunningLetterProcess.ARInvoiceWithDL invoice,
    List<int> dueDaysByLevel)
  {
    ARDunningLetterDetail dunningLetterDetail1 = new ARDunningLetterDetail();
    dunningLetterDetail1.CuryOrigDocAmt = invoice.CuryOrigDocAmt;
    dunningLetterDetail1.CuryDocBal = invoice.CuryDocBal;
    dunningLetterDetail1.CuryID = invoice.CuryID;
    dunningLetterDetail1.OrigDocAmt = invoice.OrigDocAmt;
    dunningLetterDetail1.DocBal = invoice.DocBal;
    dunningLetterDetail1.DueDate = invoice.DueDate;
    dunningLetterDetail1.DocType = invoice.DocType;
    dunningLetterDetail1.RefNbr = invoice.RefNbr;
    dunningLetterDetail1.BAccountID = invoice.CustomerID;
    dunningLetterDetail1.DunningLetterBAccountID = invoice.SharedCreditCustomerID;
    dunningLetterDetail1.DocDate = invoice.DocDate;
    ARDunningLetterDetail dunningLetterDetail2 = dunningLetterDetail1;
    DateTime? dueDate = invoice.DueDate;
    DateTime? nullable1 = docDate;
    bool? nullable2 = new bool?(dueDate.HasValue & nullable1.HasValue && dueDate.GetValueOrDefault() < nullable1.GetValueOrDefault());
    dunningLetterDetail2.Overdue = nullable2;
    DateTime? nullable3;
    if (processByCutomer)
    {
      nullable1 = invoice.DueDate;
      nullable3 = docDate;
      if ((nullable1.HasValue & nullable3.HasValue ? (nullable1.GetValueOrDefault() >= nullable3.GetValueOrDefault() ? 1 : 0) : 0) != 0)
        goto label_3;
    }
    nullable1 = invoice.DueDate;
    DateTime dateTime = nullable1.Value.AddDays((double) dueDaysByLevel[invoice.DunningLetterLevel.GetValueOrDefault()]);
    nullable3 = docDate;
    if ((nullable3.HasValue ? (dateTime >= nullable3.GetValueOrDefault() ? 1 : 0) : 0) == 0)
    {
      dunningLetterDetail1.DunningLetterLevel = new int?(invoice.DunningLetterLevel.GetValueOrDefault() + 1);
      goto label_5;
    }
label_3:
    dunningLetterDetail1.DunningLetterLevel = new int?(0);
label_5:
    return dunningLetterDetail1;
  }

  private static ARDunningLetterDetail CreateDunningLetterDetailForPayment(
    PXResult<ARPayment, Customer> paymentDoc,
    DateTime? docDate)
  {
    ARDunningLetterDetail detailForPayment = new ARDunningLetterDetail();
    ARPayment arPayment = PXResult<ARPayment, Customer>.op_Implicit(paymentDoc);
    Customer customer = PXResult<ARPayment, Customer>.op_Implicit(paymentDoc);
    detailForPayment.CuryOrigDocAmt = arPayment.CuryOrigDocAmt;
    detailForPayment.CuryDocBal = arPayment.CuryDocBal;
    detailForPayment.CuryID = arPayment.CuryID;
    detailForPayment.OrigDocAmt = arPayment.OrigDocAmt;
    detailForPayment.DocBal = arPayment.DocBal;
    detailForPayment.DocType = arPayment.DocType;
    detailForPayment.RefNbr = arPayment.RefNbr;
    detailForPayment.BAccountID = arPayment.CustomerID;
    detailForPayment.DunningLetterBAccountID = customer.SharedCreditCustomerID;
    detailForPayment.DocDate = arPayment.DocDate;
    detailForPayment.Overdue = new bool?(false);
    detailForPayment.DunningLetterLevel = new int?(0);
    if (arPayment.DocType == "CRM")
    {
      detailForPayment.DueDate = arPayment.DueDate;
      ARDunningLetterDetail dunningLetterDetail = detailForPayment;
      DateTime? dueDate = arPayment.DueDate;
      DateTime? nullable1 = docDate;
      bool? nullable2 = new bool?(dueDate.HasValue & nullable1.HasValue && dueDate.GetValueOrDefault() < nullable1.GetValueOrDefault());
      dunningLetterDetail.Overdue = nullable2;
    }
    return detailForPayment;
  }

  [PXProjection(typeof (Select5<PX.Objects.AR.Standalone.ARInvoice, InnerJoin<ARRegister, On<PX.Objects.AR.Standalone.ARInvoice.docType, Equal<ARRegister.docType>, And<PX.Objects.AR.Standalone.ARInvoice.refNbr, Equal<ARRegister.refNbr>, And<ARRegister.released, Equal<True>, And<ARRegister.openDoc, Equal<True>, And<ARRegister.voided, Equal<False>, And<ARRegister.pendingPPD, NotEqual<True>, And<Where<ARRegister.docType, Equal<ARDocType.invoice>, Or2<Where<ARRegister.docType, Equal<ARDocType.prepaymentInvoice>, And<CurrentValue<ARDunningLetterProcess.ARDunningLetterRecordsParameters.addUnpaidPPI>, Equal<True>, And<ARRegister.pendingPayment, Equal<True>>>>, Or<ARRegister.docType, Equal<ARDocType.finCharge>, Or<ARRegister.docType, Equal<ARDocType.debitMemo>>>>>>>>>>>>, InnerJoin<Customer, On<Customer.bAccountID, Equal<ARRegister.customerID>>, LeftJoin<ARDunningLetterDetail, On<ARDunningLetterDetail.dunningLetterBAccountID, Equal<Customer.sharedCreditCustomerID>, And<ARDunningLetterDetail.docType, Equal<ARRegister.docType>, And<ARDunningLetterDetail.refNbr, Equal<ARRegister.refNbr>, And<ARDunningLetterDetail.voided, Equal<False>>>>>, LeftJoin<ARDunningLetter, On<ARDunningLetter.dunningLetterID, Equal<ARDunningLetterDetail.dunningLetterID>, And<ARDunningLetter.voided, Equal<False>>>>>>>, Aggregate<GroupBy<ARRegister.refNbr, GroupBy<ARRegister.docType, Min<ARDunningLetterDetail.released>>>>>))]
  [Serializable]
  public class ARInvoiceWithDL : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    [PXDBInt(BqlField = typeof (ARRegister.customerID))]
    public virtual int? CustomerID { get; set; }

    [PXDBInt(BqlField = typeof (Customer.sharedCreditCustomerID))]
    public virtual int? SharedCreditCustomerID { get; set; }

    [PXDBInt(BqlField = typeof (ARRegister.branchID))]
    public virtual int? BranchID { get; set; }

    [PXDBInt(BqlField = typeof (ARRegister.customerLocationID))]
    public virtual int? CustomerLocationID { get; set; }

    [PXDBDecimal(BqlField = typeof (ARRegister.docBal))]
    public virtual Decimal? DocBal { get; set; }

    [PXDBDate(BqlField = typeof (ARRegister.dueDate))]
    public virtual DateTime? DueDate { get; set; }

    [PXDBBool(BqlField = typeof (ARRegister.released))]
    public virtual bool? Released { get; set; }

    [PXDBBool(BqlField = typeof (ARRegister.openDoc))]
    public virtual bool? OpenDoc { get; set; }

    [PXDBBool(BqlField = typeof (ARRegister.voided))]
    public virtual bool? Voided { get; set; }

    [PXDBBool(BqlField = typeof (PX.Objects.AR.Standalone.ARInvoice.revoked))]
    public virtual bool? Revoked { get; set; }

    [PXDBString(3, IsKey = true, IsFixed = true, BqlField = typeof (ARRegister.docType))]
    public virtual string DocType { get; set; }

    [PXDBString(15, IsUnicode = true, IsKey = true, BqlField = typeof (ARRegister.refNbr))]
    public virtual string RefNbr { get; set; }

    [PXDBInt(BqlField = typeof (ARDunningLetterDetail.dunningLetterLevel))]
    public virtual int? DunningLetterLevel { get; set; }

    [PXDBDate(BqlField = typeof (ARDunningLetter.dunningLetterDate))]
    public virtual DateTime? DunningLetterDate { get; set; }

    [PXDBDate(BqlField = typeof (ARRegister.docDate))]
    public virtual DateTime? DocDate { get; set; }

    [PXDBString(5, IsUnicode = true, BqlField = typeof (ARRegister.curyID))]
    public virtual string CuryID { get; set; }

    [PXDBDecimal(BqlField = typeof (ARRegister.curyOrigDocAmt))]
    public virtual Decimal? CuryOrigDocAmt { get; set; }

    [PXDBDecimal(BqlField = typeof (ARRegister.origDocAmt))]
    public virtual Decimal? OrigDocAmt { get; set; }

    [PXDBDecimal(BqlField = typeof (ARRegister.curyDocBal))]
    public virtual Decimal? CuryDocBal { get; set; }

    [PXDBBool(BqlField = typeof (ARDunningLetterDetail.released))]
    public virtual bool? DLReleased { get; set; }

    public abstract class customerID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      ARDunningLetterProcess.ARInvoiceWithDL.customerID>
    {
    }

    public abstract class sharedCreditCustomerID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      ARDunningLetterProcess.ARInvoiceWithDL.sharedCreditCustomerID>
    {
    }

    public abstract class branchID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      ARDunningLetterProcess.ARInvoiceWithDL.branchID>
    {
    }

    public abstract class customerLocationID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      ARDunningLetterProcess.ARInvoiceWithDL.customerLocationID>
    {
    }

    public abstract class docBal : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARDunningLetterProcess.ARInvoiceWithDL.docBal>
    {
    }

    public abstract class dueDate : 
      BqlType<
      #nullable enable
      IBqlDateTime, DateTime>.Field<
      #nullable disable
      ARDunningLetterProcess.ARInvoiceWithDL.dueDate>
    {
    }

    public abstract class released : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      ARDunningLetterProcess.ARInvoiceWithDL.released>
    {
    }

    public abstract class openDoc : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      ARDunningLetterProcess.ARInvoiceWithDL.openDoc>
    {
    }

    public abstract class voided : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      ARDunningLetterProcess.ARInvoiceWithDL.voided>
    {
    }

    public abstract class revoked : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      ARDunningLetterProcess.ARInvoiceWithDL.revoked>
    {
    }

    public abstract class docType : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ARDunningLetterProcess.ARInvoiceWithDL.docType>
    {
    }

    public abstract class refNbr : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ARDunningLetterProcess.ARInvoiceWithDL.refNbr>
    {
    }

    public abstract class dunningLetterLevel : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      ARDunningLetterProcess.ARInvoiceWithDL.dunningLetterLevel>
    {
    }

    public abstract class dunningLetterDate : 
      BqlType<
      #nullable enable
      IBqlDateTime, DateTime>.Field<
      #nullable disable
      ARDunningLetterProcess.ARInvoiceWithDL.dunningLetterDate>
    {
    }

    public abstract class docDate : 
      BqlType<
      #nullable enable
      IBqlDateTime, DateTime>.Field<
      #nullable disable
      ARDunningLetterProcess.ARInvoiceWithDL.docDate>
    {
    }

    public abstract class curyID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ARDunningLetterProcess.ARInvoiceWithDL.curyID>
    {
    }

    public abstract class curyOrigDocAmt : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARDunningLetterProcess.ARInvoiceWithDL.curyOrigDocAmt>
    {
    }

    public abstract class origDocAmt : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARDunningLetterProcess.ARInvoiceWithDL.origDocAmt>
    {
    }

    public abstract class curyDocBal : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARDunningLetterProcess.ARInvoiceWithDL.curyDocBal>
    {
    }

    public abstract class dLReleased : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      ARDunningLetterProcess.ARInvoiceWithDL.dLReleased>
    {
    }
  }

  protected class IncludeTypes
  {
    public const int IncludeAll = 0;
    public const int IncludeLevels = 1;

    public class ListAttribute : PXIntListAttribute
    {
      public ListAttribute()
        : base(new int[2]{ 0, 1 }, new string[2]
        {
          "All Overdue Documents",
          "Dunning Letter Level"
        })
      {
      }
    }
  }

  [Serializable]
  public class ARDunningLetterRecordsParameters : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    [Organization(true, Required = false)]
    public int? OrganizationID { get; set; }

    [BranchOfOrganization(typeof (ARDunningLetterProcess.ARDunningLetterRecordsParameters.organizationID), true, null, null, Required = false)]
    public int? BranchID { get; set; }

    [OrganizationTree(typeof (ARDunningLetterProcess.ARDunningLetterRecordsParameters.organizationID), typeof (ARDunningLetterProcess.ARDunningLetterRecordsParameters.branchID), null, true)]
    public int? OrgBAccountID { get; set; }

    [PXDBString(10, IsUnicode = true)]
    [PXUIField]
    [PXSelector(typeof (CustomerClass.customerClassID))]
    public virtual string CustomerClassID { get; set; }

    [PXDate]
    [PXDefault(typeof (AccessInfo.businessDate))]
    [PXUIField]
    public virtual DateTime? DocDate { get; set; }

    [PXDBBool]
    [PXDefault(typeof (Search<ARSetup.includeNonOverdueDunning>))]
    [PXUIField]
    public virtual bool? IncludeNonOverdueDunning { get; set; }

    [PXDBBool]
    [PXDefault(typeof (Search<ARSetup.addOpenPaymentsAndCreditMemos>))]
    [PXUIField]
    public virtual bool? AddOpenPaymentsAndCreditMemos { get; set; }

    /// <summary>
    /// Specifies, if Prepayment INvoices should be add to Dunning Process
    /// </summary>
    [PXDBBool]
    [PXDefault(typeof (Search<ARSetup.addUnpaidPPI>))]
    [PXUIField]
    public virtual bool? AddUnpaidPPI { get; set; }

    [PXInt]
    [PXDefault(0)]
    [PXUIField]
    [ARDunningLetterProcess.IncludeTypes.List]
    public virtual int? IncludeType { get; set; }

    [PXInt]
    [PXDefault(1)]
    [PXUIField(DisplayName = "From", Enabled = false)]
    public virtual int? LevelFrom { get; set; }

    [PXInt]
    [PXDefault(typeof (Search<ARDunningCustomerClass.dunningLetterLevel, Where<True, Equal<True>>, OrderBy<Desc<ARDunningCustomerClass.dunningLetterLevel>>>))]
    [PXUIField(DisplayName = "To", Enabled = false)]
    public virtual int? LevelTo { get; set; }

    public abstract class organizationID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      ARDunningLetterProcess.ARDunningLetterRecordsParameters.organizationID>
    {
    }

    public abstract class branchID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      ARDunningLetterProcess.ARDunningLetterRecordsParameters.branchID>
    {
    }

    public abstract class orgBAccountID : IBqlField, IBqlOperand
    {
    }

    public abstract class customerClassID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ARDunningLetterProcess.ARDunningLetterRecordsParameters.customerClassID>
    {
    }

    public abstract class docDate : 
      BqlType<
      #nullable enable
      IBqlDateTime, DateTime>.Field<
      #nullable disable
      ARDunningLetterProcess.ARDunningLetterRecordsParameters.docDate>
    {
    }

    public abstract class includeNonOverdueDunning : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      ARDunningLetterProcess.ARDunningLetterRecordsParameters.includeNonOverdueDunning>
    {
    }

    public abstract class addOpenPaymentsAndCreditMemos : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      ARDunningLetterProcess.ARDunningLetterRecordsParameters.addOpenPaymentsAndCreditMemos>
    {
    }

    public abstract class addUnpaidPPI : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      ARDunningLetterProcess.ARDunningLetterRecordsParameters.addUnpaidPPI>
    {
    }

    public abstract class includeType : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      ARDunningLetterProcess.ARDunningLetterRecordsParameters.includeType>
    {
    }

    public abstract class levelFrom : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      ARDunningLetterProcess.ARDunningLetterRecordsParameters.levelFrom>
    {
    }

    public abstract class levelTo : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      ARDunningLetterProcess.ARDunningLetterRecordsParameters.levelTo>
    {
    }
  }

  [Serializable]
  public class ARDunningLetterList : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    [PXBool]
    [PXDefault(false)]
    [PXUIField(DisplayName = "Selected")]
    public virtual bool? Selected { get; set; }

    [PXDBString(10, IsUnicode = true)]
    [PXUIField]
    public virtual string CustomerClassID { get; set; }

    [Organization(true, IsKey = true)]
    public int? OrganizationID { get; set; }

    [Branch(null, null, true, true, true, IsKey = true)]
    public virtual int? BranchID { get; set; }

    [PXDBInt(IsKey = true)]
    [PXDefault]
    [Customer(DescriptionField = typeof (Customer.acctName))]
    [PXUIField(DisplayName = "Customer")]
    public virtual int? BAccountID { get; set; }

    [PXDBDate(IsKey = true)]
    [PXDefault]
    [PXUIField(DisplayName = "Earliest Due Date")]
    public virtual DateTime? DueDate { get; set; }

    [PXInt]
    [PXDefault(0)]
    [PXUIField(DisplayName = "Number of Documents")]
    public virtual int? NumberOfDocuments { get; set; }

    [PXInt]
    [PXDefault(0)]
    [PXUIField(DisplayName = "Number of Overdue Documents")]
    public virtual int? NumberOfOverdueDocuments { get; set; }

    [PXDBBaseCuryMaxPrecision]
    [PXUIField(DisplayName = "Customer Balance")]
    public virtual Decimal? OrigDocAmt { get; set; }

    [PXDBBaseCuryMaxPrecision]
    [PXUIField(DisplayName = "Overdue Balance")]
    public virtual Decimal? DocBal { get; set; }

    [PXInt]
    [PXDefault(0)]
    [PXUIField(DisplayName = "Dunning Letter Level")]
    public virtual int? DunningLetterLevel { get; set; }

    [PXDBDate(IsKey = true)]
    [PXDefault]
    [PXUIField(DisplayName = "Last Dunning Letter Date")]
    public virtual DateTime? LastDunningLetterDate { get; set; }

    [PXDBInt]
    [PXDefault]
    [PXUIField(DisplayName = "Due Days")]
    public virtual int? DueDays { get; set; }

    [PXDBString(5, IsUnicode = true)]
    [PXUIField(DisplayName = "Currency")]
    [PXSelector(typeof (PX.Objects.CM.Currency.curyID))]
    public virtual string CuryID { get; set; }

    [PXNote]
    public virtual Guid? NoteID { get; set; }

    public abstract class selected : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      ARDunningLetterProcess.ARDunningLetterList.selected>
    {
    }

    public abstract class customerClassID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ARDunningLetterProcess.ARDunningLetterList.customerClassID>
    {
    }

    public abstract class organizationID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      ARDunningLetterProcess.ARDunningLetterList.organizationID>
    {
    }

    public abstract class branchID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      ARDunningLetterProcess.ARDunningLetterList.branchID>
    {
    }

    public abstract class bAccountID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      ARDunningLetterProcess.ARDunningLetterList.bAccountID>
    {
    }

    public abstract class dueDate : 
      BqlType<
      #nullable enable
      IBqlDateTime, DateTime>.Field<
      #nullable disable
      ARDunningLetterProcess.ARDunningLetterList.dueDate>
    {
    }

    public abstract class numberOfDocuments : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      ARDunningLetterProcess.ARDunningLetterList.numberOfDocuments>
    {
    }

    public abstract class numberOfOverdueDocuments : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      ARDunningLetterProcess.ARDunningLetterList.numberOfOverdueDocuments>
    {
    }

    public abstract class origDocAmt : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARDunningLetterProcess.ARDunningLetterList.origDocAmt>
    {
    }

    public abstract class docBal : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARDunningLetterProcess.ARDunningLetterList.docBal>
    {
    }

    public abstract class dunningLetterLevel : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      ARDunningLetterProcess.ARDunningLetterList.dunningLetterLevel>
    {
    }

    public abstract class lastDunningLetterDate : 
      BqlType<
      #nullable enable
      IBqlDateTime, DateTime>.Field<
      #nullable disable
      ARDunningLetterProcess.ARDunningLetterList.lastDunningLetterDate>
    {
    }

    public abstract class dueDays : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      ARDunningLetterProcess.ARDunningLetterList.dueDays>
    {
    }

    public abstract class curyID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ARDunningLetterProcess.ARDunningLetterList.curyID>
    {
    }

    public abstract class noteID : 
      BqlType<
      #nullable enable
      IBqlGuid, Guid>.Field<
      #nullable disable
      ARDunningLetterProcess.ARDunningLetterList.noteID>
    {
    }
  }
}
