// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.CRCaseMaint
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.AR;
using PX.Objects.CR.CRCaseMaint_Extensions;
using PX.Objects.CS;
using PX.Objects.CT;
using PX.Objects.EP;
using PX.Objects.GL;
using PX.Objects.PM;
using PX.SM;
using PX.TM;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;

#nullable disable
namespace PX.Objects.CR;

public class CRCaseMaint : PXGraph<CRCaseMaint, CRCase>
{
  [PXHidden]
  public PXSelect<BAccount> bAccountBasic;
  [PXHidden]
  public PXSelect<BAccountR> bAccountRBasic;
  [PXHidden]
  public PXSelect<Contact> Contacts;
  [PXHidden]
  public PXSelect<PX.Objects.CT.Contract> Contracts;
  [PXHidden]
  [PXCheckCurrent]
  public PXSetup<Company> company;
  [PXHidden]
  [PXCheckCurrent]
  public PXSetup<CRSetup> Setup;
  [PXViewName("Case")]
  public PXSelectJoin<CRCase, LeftJoin<BAccount, On<BAccount.bAccountID, Equal<CRCase.customerID>>>, Where<BAccount.bAccountID, IsNull, Or<Match<BAccount, Current<AccessInfo.userName>>>>> Case;
  [PXHidden]
  [PXCopyPasteHiddenFields(new System.Type[] {typeof (CRCase.description), typeof (CRCase.solutionActivityNoteID)})]
  public PXSelect<CRCase, Where<CRCase.caseCD, Equal<Current<CRCase.caseCD>>>> CaseCurrent;
  [PXCopyPasteHiddenView]
  public PXSelect<CRActivityStatistics, Where<CRActivityStatistics.noteID, Equal<Current<CRCase.noteID>>>> CaseActivityStatistics;
  [PXHidden]
  public PXSetup<CRCaseClass, Where<CRCaseClass.caseClassID, Equal<Optional<CRCase.caseClassID>>>> Class;
  [PXViewName("Answers")]
  public CRAttributeList<CRCase> Answers;
  [PXHidden]
  public PXSelect<CRActivity> ActivitiesSelect;
  [PXViewName("Related Cases")]
  [PXViewDetailsButton(typeof (CRCase), typeof (Select<CRCase, Where<CRCase.caseCD, Equal<Current<CRCaseReference.childCaseCD>>>>))]
  public PXSelectJoin<CRCaseReference, LeftJoin<CRCaseRelated, On<CRCase.caseCD, Equal<CRCaseReference.childCaseCD>>>> CaseRefs;
  [PXHidden]
  public PXSelect<CRCaseRelated, Where<CRCase.caseCD, Equal<Current<CRCaseReference.childCaseCD>>>> CaseRelated;
  [PXCopyPasteHiddenView]
  [PXViewName("Owner User")]
  public PXSelectReadonly2<Users, InnerJoin<Contact, On<Contact.userID, Equal<Users.pKID>>>, Where<Contact.contactID, Equal<Current<CRCase.ownerID>>>> OwnerUser;
  public PXSave<CRCase> Save;
  public PXCancel<CRCase> Cancel;
  public PXInsert<CRCase> Insert;
  public PXCopyPasteAction<CRCase> CopyPaste;
  public PXDelete<CRCase> Delete;
  public PXFirst<CRCase> First;
  public PXPrevious<CRCase> Previous;
  public PXNext<CRCase> Next;
  public PXLast<CRCase> Last;
  public PXAction<CRCase> release;
  public PXMenuAction<CRCase> Action;
  public PXMenuInquiry<CRCase> Inquiry;
  public PXAction<CRCase> takeCase;
  public PXAction<CRCase> assign;
  public PXAction<CRCase> viewInvoice;
  public PXAction<CRCase> addNewContact;
  public PXAction<CRCase> Open;
  public PXAction<CRCase> Close;
  public PXAction<CRCase> PendingCustomer;

  public CRCaseMaint()
  {
    if (string.IsNullOrEmpty(((PXSelectBase<CRSetup>) this.Setup).Current.CaseNumberingID))
      throw new PXSetPropertyException("Numbering ID is not configured in the CR setup.", new object[1]
      {
        (object) "Customer Management Preferences"
      });
    PXUIFieldAttribute.SetRequired<CRCase.caseClassID>(((PXSelectBase) this.Case).Cache, true);
    GraphHelper.EnsureCachePersistence((PXGraph) this, typeof (CRPMTimeActivity));
    PXCache cach = ((PXGraph) this).Caches[typeof (BAccount)];
    PXUIFieldAttribute.SetDisplayName<BAccount.acctCD>(cach, "Business Account");
    PXUIFieldAttribute.SetDisplayName<BAccount.acctName>(cach, "Business Account Name");
  }

  protected virtual IEnumerable caseRefs()
  {
    CRCaseMaint crCaseMaint1 = this;
    string currentCaseCd = ((PXSelectBase<CRCase>) crCaseMaint1.Case).Current.With<CRCase, string>((Func<CRCase, string>) (_ => _.CaseCD));
    if (currentCaseCd != null)
    {
      HybridDictionary ht = new HybridDictionary();
      CRCaseMaint crCaseMaint2 = crCaseMaint1;
      object[] objArray1 = new object[1]
      {
        (object) currentCaseCd
      };
      foreach (PXResult<CRCaseReference> pxResult in PXSelectBase<CRCaseReference, PXSelect<CRCaseReference, Where<CRCaseReference.parentCaseCD, Equal<Required<CRCaseReference.parentCaseCD>>>>.Config>.Select((PXGraph) crCaseMaint2, objArray1))
      {
        CRCaseReference crCaseReference = PXResult<CRCaseReference>.op_Implicit(pxResult);
        string str = crCaseReference.ChildCaseCD ?? string.Empty;
        if (!ht.Contains((object) str))
        {
          ht.Add((object) str, (object) crCaseReference);
          CRCaseRelated crCaseRelated = crCaseMaint1.SelectCase((object) str);
          if (crCaseRelated != null)
            yield return (object) new PXResult<CRCaseReference, CRCaseRelated>(crCaseReference, crCaseRelated);
        }
      }
      PXCache cache = ((PXSelectBase) crCaseMaint1.CaseRefs).Cache;
      bool oldIsDirty = cache.IsDirty;
      CRCaseMaint crCaseMaint3 = crCaseMaint1;
      object[] objArray2 = new object[1]
      {
        (object) currentCaseCd
      };
      foreach (PXResult<CRCaseReference> pxResult in PXSelectBase<CRCaseReference, PXSelect<CRCaseReference, Where<CRCaseReference.childCaseCD, Equal<Required<CRCaseReference.childCaseCD>>>>.Config>.Select((PXGraph) crCaseMaint3, objArray2))
      {
        CRCaseReference crCaseReference1 = PXResult<CRCaseReference>.op_Implicit(pxResult);
        string str = crCaseReference1.ParentCaseCD ?? string.Empty;
        if (!ht.Contains((object) str))
        {
          CRCaseRelated crCaseRelated = crCaseMaint1.SelectCase((object) str);
          if (crCaseRelated != null)
          {
            ht.Add((object) str, (object) crCaseReference1);
            cache.Delete((object) crCaseReference1);
            CRCaseReference instance = (CRCaseReference) cache.CreateInstance();
            instance.ParentCaseCD = currentCaseCd;
            instance.ChildCaseCD = str;
            switch (crCaseReference1.RelationType)
            {
              case "C":
                instance.RelationType = "P";
                break;
              case "D":
                instance.RelationType = "D";
                break;
              case "R":
                instance.RelationType = "R";
                break;
              default:
                instance.RelationType = "C";
                break;
            }
            CRCaseReference crCaseReference2 = (CRCaseReference) cache.Insert((object) instance);
            cache.IsDirty = oldIsDirty;
            yield return (object) new PXResult<CRCaseReference, CRCaseRelated>(crCaseReference2, crCaseRelated);
          }
        }
      }
    }
  }

  [PXUIField]
  [PXProcessButton]
  public virtual IEnumerable Release(PXAdapter adapter)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    CRCaseMaint.\u003C\u003Ec__DisplayClass27_0 cDisplayClass270 = new CRCaseMaint.\u003C\u003Ec__DisplayClass27_0();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass270.list = new List<CRCase>(adapter.Get<CRCase>());
    ((PXAction) this.Save).Press();
    // ISSUE: method pointer
    PXLongOperation.StartOperation((PXGraph) this, new PXToggleAsyncDelegate((object) cDisplayClass270, __methodptr(\u003CRelease\u003Eb__0)));
    return adapter.Get();
  }

  [PXUIField]
  [PXButton(PopupVisible = true)]
  public virtual IEnumerable TakeCase(PXAdapter adapter)
  {
    CRCaseMaint graph = this;
    foreach (CRCase crCase1 in adapter.Get<CRCase>())
    {
      CRCase crCase2 = (CRCase) ((PXSelectBase) graph.Case).Cache.CreateCopy((object) crCase1);
      crCase2.OwnerID = EmployeeMaint.GetCurrentOwnerID((PXGraph) graph);
      int? nullable1 = crCase2.WorkgroupID;
      if (nullable1.HasValue)
      {
        if (PXResultset<EPCompanyTreeMember>.op_Implicit(PXSelectBase<EPCompanyTreeMember, PXSelect<EPCompanyTreeMember, Where<EPCompanyTreeMember.contactID, Equal<Current<AccessInfo.contactID>>, And<EPCompanyTreeMember.workGroupID, Equal<Required<CRCase.workgroupID>>>>>.Config>.Select((PXGraph) graph, new object[1]
        {
          (object) crCase2.WorkgroupID
        })) == null)
        {
          CRCase crCase3 = crCase2;
          nullable1 = new int?();
          int? nullable2 = nullable1;
          crCase3.WorkgroupID = nullable2;
        }
      }
      nullable1 = crCase2.OwnerID;
      int? nullable3 = ((PXSelectBase<CRCase>) graph.Case).Current.OwnerID;
      if (nullable1.GetValueOrDefault() == nullable3.GetValueOrDefault() & nullable1.HasValue == nullable3.HasValue)
      {
        nullable3 = crCase2.WorkgroupID;
        nullable1 = ((PXSelectBase<CRCase>) graph.Case).Current.WorkgroupID;
        if (nullable3.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable3.HasValue == nullable1.HasValue)
          goto label_8;
      }
      crCase2 = ((PXSelectBase<CRCase>) graph.Case).Update(crCase2);
label_8:
      if (((PXGraph) graph).IsContractBasedAPI)
        ((PXAction) graph.Save).Press();
      yield return (object) crCase2;
    }
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable Assign(PXAdapter adapter)
  {
    if (!((PXSelectBase<CRSetup>) this.Setup).Current.DefaultCaseAssignmentMapID.HasValue)
      throw new PXSetPropertyException("Default Case Assignment Map is not entered in CR setup", new object[1]
      {
        (object) "Customer Management Preferences"
      });
    new EPAssignmentProcessor<CRCase>((PXGraph) this).Assign(((PXSelectBase<CRCase>) this.CaseCurrent).Current, ((PXSelectBase<CRSetup>) this.Setup).Current.DefaultCaseAssignmentMapID);
    ((PXSelectBase<CRCase>) this.CaseCurrent).Update(((PXSelectBase<CRCase>) this.CaseCurrent).Current);
    if (((PXGraph) this).IsContractBasedAPI)
      ((PXAction) this.Save).Press();
    return adapter.Get();
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable ViewInvoice(PXAdapter adapter)
  {
    if (((PXSelectBase<CRCase>) this.CaseCurrent).Current != null && !string.IsNullOrEmpty(((PXSelectBase<CRCase>) this.CaseCurrent).Current.ARRefNbr))
    {
      ARInvoiceEntry instance = PXGraph.CreateInstance<ARInvoiceEntry>();
      ((PXGraph) instance).Clear();
      ((PXSelectBase<PX.Objects.AR.ARInvoice>) instance.Document).Current = PXResultset<PX.Objects.AR.ARInvoice>.op_Implicit(((PXSelectBase<PX.Objects.AR.ARInvoice>) instance.Document).Search<PX.Objects.AR.ARInvoice.refNbr>((object) ((PXSelectBase<CRCase>) this.CaseCurrent).Current.ARRefNbr, Array.Empty<object>()));
      throw new PXRedirectRequiredException((PXGraph) instance, nameof (ViewInvoice));
    }
    if (((PXSelectBase<CRCase>) this.CaseCurrent).Current != null && ((PXSelectBase<CRCase>) this.CaseCurrent).Current.ContractID.HasValue)
    {
      PMTran pmTran = PXResultset<PMTran>.op_Implicit(PXSelectBase<PMTran, PXSelect<PMTran, Where<PMTran.origRefID, Equal<Current<CRCase.noteID>>>>.Config>.Select((PXGraph) this, Array.Empty<object>()));
      if (pmTran != null && !string.IsNullOrEmpty(pmTran.ARRefNbr))
      {
        ARInvoiceEntry instance = PXGraph.CreateInstance<ARInvoiceEntry>();
        ((PXGraph) instance).Clear();
        ((PXSelectBase<PX.Objects.AR.ARInvoice>) instance.Document).Current = PXResultset<PX.Objects.AR.ARInvoice>.op_Implicit(((PXSelectBase<PX.Objects.AR.ARInvoice>) instance.Document).Search<PX.Objects.AR.ARInvoice.refNbr>((object) pmTran.ARRefNbr, Array.Empty<object>()));
        throw new PXRedirectRequiredException((PXGraph) instance, nameof (ViewInvoice));
      }
    }
    return adapter.Get();
  }

  [PXUIField]
  [PXButton(DisplayOnMainToolbar = false)]
  public virtual IEnumerable AddNewContact(PXAdapter adapter)
  {
    if (((PXSelectBase<CRCase>) this.Case).Current != null && ((PXSelectBase<CRCase>) this.Case).Current.CustomerID.HasValue)
    {
      ContactMaint instance = PXGraph.CreateInstance<ContactMaint>();
      ((PXGraph) instance).Clear();
      Contact contact = ((PXSelectBase<Contact>) instance.Contact).Insert();
      contact.BAccountID = ((PXSelectBase<CRCase>) this.Case).Current.CustomerID;
      if (PXResultset<CRContactClass>.op_Implicit(PXSelectBase<CRContactClass, PXSelect<CRContactClass, Where<CRContactClass.classID, Equal<Current<Contact.classID>>>>.Config>.SelectSingleBound((PXGraph) this, new object[1]
      {
        (object) contact
      }, Array.Empty<object>()))?.DefaultOwner == "S")
      {
        contact.WorkgroupID = ((PXSelectBase<CRCase>) this.Case).Current.WorkgroupID;
        contact.OwnerID = ((PXSelectBase<CRCase>) this.Case).Current.OwnerID;
      }
      ((PXSelectBase<Contact>) instance.Contact).Update(contact);
      PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, true, "Contact");
      ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 3;
      throw requiredException;
    }
    return adapter.Get();
  }

  [PXButton]
  [PXUIField(DisplayName = "Open")]
  public virtual IEnumerable open(PXAdapter adapter) => adapter.Get();

  [PXButton]
  [PXUIField(DisplayName = "Close")]
  public virtual IEnumerable close(PXAdapter adapter) => adapter.Get();

  [PXButton]
  [PXUIField(DisplayName = "Pending Customer")]
  public virtual IEnumerable pendingCustomer(PXAdapter adapter) => adapter.Get();

  [CRMBAccount(new System.Type[] {typeof (BAccountType.prospectType), typeof (BAccountType.customerType), typeof (BAccountType.combinedType), typeof (BAccountType.vendorType)}, null, null, null)]
  [PXMergeAttributes]
  protected virtual void _(PX.Data.Events.CacheAttached<Contact.bAccountID> e)
  {
  }

  [PopupMessage]
  [PXRestrictor(typeof (Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Current<CRCaseClass.requireCustomer>, Equal<False>>>>>.Or<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Current<CRCaseClass.requireVendor>, Equal<True>>>>, Or<BqlOperand<BAccount.type, IBqlString>.IsEqual<BAccountType.customerType>>>>.Or<BqlOperand<BAccount.type, IBqlString>.IsEqual<BAccountType.combinedType>>>>), "An account of the Customer type is required for this case.", new System.Type[] {typeof (BAccount.acctCD)})]
  [PXRestrictor(typeof (Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Current<CRCaseClass.requireVendor>, Equal<False>>>>>.Or<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Current<CRCaseClass.requireCustomer>, Equal<True>>>>, Or<BqlOperand<BAccount.type, IBqlString>.IsEqual<BAccountType.vendorType>>>>.Or<BqlOperand<BAccount.type, IBqlString>.IsEqual<BAccountType.combinedType>>>>), "An account of the Vendor type is required for this case.", new System.Type[] {typeof (BAccount.acctCD)})]
  [PXRestrictor(typeof (Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Current<CRCaseClass.requireVendor>, Equal<False>>>>>.Or<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Current<CRCaseClass.requireCustomer>, Equal<False>>>>>.Or<BqlOperand<BAccount.type, IBqlString>.IsEqual<BAccountType.combinedType>>>>), "An account of the Customer & Vendor type is required for this case. ", new System.Type[] {typeof (BAccount.acctCD)})]
  [PXRestrictorWithErase(typeof (Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Current<CRCase.caseClassID>, IsNull>>>, Or<BqlOperand<Current<CRCaseClass.allowEmployeeAsContact>, IBqlBool>.IsEqual<True>>>, Or<BqlOperand<BAccount.type, IBqlString>.IsNull>>>.Or<BqlOperand<BAccount.type, IBqlString>.IsNotEqual<BAccountType.branchType>>>), "You cannot select the {0} branch because the {1} case class does not allow selecting branches and employees for cases of the class.", new System.Type[] {typeof (BAccount.acctName), typeof (Current<CRCaseClass.caseClassID>)})]
  [PXMergeAttributes]
  protected virtual void _(PX.Data.Events.CacheAttached<CRCase.customerID> e)
  {
  }

  [PXRestrictor(typeof (Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Current<CRCaseClass.requireCustomer>, Equal<False>>>>>.Or<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Current<CRCaseClass.requireVendor>, Equal<True>>>>, Or<BqlOperand<BAccount.type, IBqlString>.IsEqual<BAccountType.customerType>>>>.Or<BqlOperand<BAccount.type, IBqlString>.IsEqual<BAccountType.combinedType>>>>), "An account of the Customer type is required for this case.", new System.Type[] {typeof (BAccount.acctCD)})]
  [PXRestrictor(typeof (Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Current<CRCaseClass.requireVendor>, Equal<False>>>>>.Or<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Current<CRCaseClass.requireCustomer>, Equal<True>>>>, Or<BqlOperand<BAccount.type, IBqlString>.IsEqual<BAccountType.vendorType>>>>.Or<BqlOperand<BAccount.type, IBqlString>.IsEqual<BAccountType.combinedType>>>>), "An account of the Vendor type is required for this case.", new System.Type[] {typeof (BAccount.acctCD)})]
  [PXRestrictor(typeof (Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Current<CRCaseClass.requireVendor>, Equal<False>>>>>.Or<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Current<CRCaseClass.requireCustomer>, Equal<False>>>>>.Or<BqlOperand<BAccount.type, IBqlString>.IsEqual<BAccountType.combinedType>>>>), "An account of the Customer & Vendor type is required for this case. ", new System.Type[] {typeof (BAccount.acctCD)})]
  [PXRestrictorWithErase(typeof (Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Current<CRCase.caseClassID>, IsNull>>>, Or<BqlOperand<Current<CRCaseClass.allowEmployeeAsContact>, IBqlBool>.IsEqual<True>>>, Or<BqlOperand<BAccount.type, IBqlString>.IsNull>>>.Or<BqlOperand<BAccount.isBranch, IBqlBool>.IsNotEqual<True>>>), "You cannot select the {0} employee because the {1} case class does not allow selecting an employee as a contact for cases of the class.", new System.Type[] {typeof (Contact.displayName), typeof (Current<CRCaseClass.caseClassID>)})]
  [PXMergeAttributes]
  protected virtual void _(PX.Data.Events.CacheAttached<CRCase.contactID> e)
  {
  }

  [CRCaseBillableTime]
  [PXUIField(DisplayName = "Billable Time", Enabled = false)]
  [PXMergeAttributes]
  protected virtual void _(PX.Data.Events.CacheAttached<CRCase.timeBillable> e)
  {
  }

  [PXDBInt]
  [PXUIField(DisplayName = "Contract")]
  [PXSelector(typeof (Search2<PX.Objects.CT.Contract.contractID, LeftJoin<ContractBillingSchedule, On<PX.Objects.CT.Contract.contractID, Equal<ContractBillingSchedule.contractID>>>, Where<PX.Objects.CT.Contract.baseType, Equal<CTPRType.contract>, And<Where<Current<CRCase.customerID>, IsNull, Or2<Where<PX.Objects.CT.Contract.customerID, Equal<Current<CRCase.customerID>>, And<Current<CRCase.locationID>, IsNull>>, Or2<Where<ContractBillingSchedule.accountID, Equal<Current<CRCase.customerID>>, And<Current<CRCase.locationID>, IsNull>>, Or2<Where<PX.Objects.CT.Contract.customerID, Equal<Current<CRCase.customerID>>, And<PX.Objects.CT.Contract.locationID, Equal<Current<CRCase.locationID>>>>, Or<Where<ContractBillingSchedule.accountID, Equal<Current<CRCase.customerID>>, And<ContractBillingSchedule.locationID, Equal<Current<CRCase.locationID>>>>>>>>>>>, OrderBy<Desc<PX.Objects.CT.Contract.contractCD>>>), DescriptionField = typeof (PX.Objects.CT.Contract.description), SubstituteKey = typeof (PX.Objects.CT.Contract.contractCD), Filterable = true)]
  [PXRestrictor(typeof (Where<PX.Objects.CT.Contract.status, Equal<PX.Objects.CT.Contract.status.active>, Or<PX.Objects.CT.Contract.status, Equal<PX.Objects.CT.Contract.status.inUpgrade>>>), "Contract is not active.", new System.Type[] {})]
  [PXRestrictor(typeof (Where<Current<AccessInfo.businessDate>, LessEqual<PX.Objects.CT.Contract.graceDate>, Or<PX.Objects.CT.Contract.expireDate, IsNull>>), "Contract has expired.", new System.Type[] {})]
  [PXRestrictor(typeof (Where<Current<AccessInfo.businessDate>, GreaterEqual<PX.Objects.CT.Contract.startDate>>), "Contract activation date is in future. This contract can only be used starting from {0}", new System.Type[] {typeof (PX.Objects.CT.Contract.startDate)})]
  [PXFormula(typeof (Default<CRCase.customerID>))]
  [PXDefault]
  protected virtual void _(PX.Data.Events.CacheAttached<CRCase.contractID> e)
  {
  }

  [PXMergeAttributes]
  [PXDBDatetimeScalar(typeof (Search<CRActivityStatistics.lastActivityDate, Where<CRActivityStatistics.noteID, Equal<CRCase.noteID>>>), PreserveTime = true, UseTimeZone = true)]
  protected virtual void _(PX.Data.Events.CacheAttached<CRCase.lastActivity> e)
  {
  }

  [PXDBString(15, IsKey = true)]
  [PXDBDefault(typeof (CRCase.caseCD))]
  [PXUIField(Visible = false)]
  protected virtual void _(
    PX.Data.Events.CacheAttached<CRCaseReference.parentCaseCD> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowSelected<CRCase> e)
  {
    CRCase row1 = e.Row;
    if (row1 == null)
      return;
    CRCaseClass parent = (CRCaseClass) PrimaryKeyOf<CRCaseClass>.By<CRCaseClass.caseClassID>.ForeignKeyOf<CRCase>.By<CRCase.caseClassID>.FindParent((PXGraph) this, (CRCase.caseClassID) e.Row, (PKFindOptions) 0);
    bool flag1 = false;
    bool flag2 = false;
    BAccount baccount = BAccount.PK.Find((PXGraph) this, e.Row.CustomerID);
    bool flag3 = baccount?.Type == "CU" || baccount?.Type == "VC";
    int? nullable1;
    if (parent != null)
    {
      flag2 = !parent.AllowOverrideBillable.GetValueOrDefault();
      nullable1 = parent.PerItemBilling;
      flag1 = nullable1.GetValueOrDefault() == 1;
    }
    if (!row1.IsBillable.GetValueOrDefault())
      row1.ManualBillableTimes = new bool?(false);
    if (flag1 & flag2)
      row1.IsBillable = new bool?(false);
    bool flag4 = !row1.Released.GetValueOrDefault();
    bool? nullable2;
    if (flag4)
    {
      PXUIFieldAttribute.SetEnabled<CRCase.manualBillableTimes>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<CRCase>>) e).Cache, (object) row1, row1.IsBillable.GetValueOrDefault() & flag3);
      PXUIFieldAttribute.SetEnabled<CRCase.isBillable>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<CRCase>>) e).Cache, (object) row1, ((flag1 ? 0 : (!flag2 ? 1 : 0)) & (flag3 ? 1 : 0)) != 0);
      nullable2 = row1.IsBillable;
      int num;
      if (nullable2.GetValueOrDefault())
      {
        nullable2 = row1.ManualBillableTimes;
        num = nullable2.GetValueOrDefault() ? 1 : 0;
      }
      else
        num = 0;
      bool flag5 = num != 0;
      PXUIFieldAttribute.SetEnabled<CRCase.timeBillable>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<CRCase>>) e).Cache, (object) row1, flag5);
      PXUIFieldAttribute.SetEnabled<CRCase.overtimeBillable>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<CRCase>>) e).Cache, (object) row1, flag5);
    }
    else
      PXUIFieldAttribute.SetEnabled(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<CRCase>>) e).Cache, (object) row1, false);
    PXUIFieldAttribute.SetEnabled<CRCase.caseCD>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<CRCase>>) e).Cache, (object) row1, true);
    this.RecalcDetails(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<CRCase>>) e).Cache, row1);
    PXAction<CRCase> release = this.release;
    int num1;
    if (flag4)
    {
      nullable2 = row1.IsBillable;
      if (nullable2.GetValueOrDefault())
      {
        num1 = !flag1 ? 1 : 0;
        goto label_17;
      }
    }
    num1 = 0;
label_17:
    ((PXAction) release).SetEnabled(num1 != 0);
    Guid? currentEmployeeId = EmployeeMaint.GetCurrentEmployeeID((PXGraph) this);
    PXAction<CRCase> takeCase = this.takeCase;
    int? nullable3;
    int num2;
    if (currentEmployeeId.HasValue)
    {
      nullable1 = row1.OwnerID;
      nullable3 = EmployeeMaint.GetCurrentOwnerID((PXGraph) this);
      num2 = !(nullable1.GetValueOrDefault() == nullable3.GetValueOrDefault() & nullable1.HasValue == nullable3.HasValue) ? 1 : 0;
    }
    else
      num2 = 0;
    int num3 = flag4 ? 1 : 0;
    int num4 = num2 & num3;
    ((PXAction) takeCase).SetEnabled(num4 != 0);
    PXCache cache1 = ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<CRCase>>) e).Cache;
    nullable2 = row1.IsBillable;
    int num5;
    if (!nullable2.GetValueOrDefault())
    {
      if (parent != null)
      {
        nullable2 = parent.RequireCustomer;
        if (nullable2.GetValueOrDefault())
          goto label_26;
      }
      if (parent != null)
      {
        nullable2 = parent.RequireVendor;
        num5 = nullable2.GetValueOrDefault() ? 1 : 0;
        goto label_27;
      }
      num5 = 0;
      goto label_27;
    }
label_26:
    num5 = 1;
label_27:
    PXUIFieldAttribute.SetRequired<CRCase.customerID>(cache1, num5 != 0);
    PXCache cache2 = ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<CRCase>>) e).Cache;
    int num6;
    if (parent != null && PXAccess.FeatureInstalled<FeaturesSet.contractManagement>())
    {
      nullable2 = parent.RequireContract;
      num6 = nullable2.GetValueOrDefault() ? 1 : 0;
    }
    else
      num6 = 0;
    PXUIFieldAttribute.SetRequired<CRCase.contractID>(cache2, num6 != 0);
    PXCache cache3 = ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<CRCase>>) e).Cache;
    int num7;
    if (parent != null)
    {
      nullable2 = parent.RequireContact;
      num7 = nullable2.GetValueOrDefault() ? 1 : 0;
    }
    else
      num7 = 0;
    PXUIFieldAttribute.SetRequired<CRCase.contactID>(cache3, num7 != 0);
    PXCache cache4 = ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<CRCase>>) e).Cache;
    CRCase row2 = e.Row;
    nullable3 = e.Row.ContactID;
    int num8 = !nullable3.HasValue || baccount == null ? 1 : (baccount.Type != "OR" ? 1 : 0);
    PXUIFieldAttribute.SetEnabled<CRCase.customerID>(cache4, (object) row2, num8 != 0);
  }

  protected virtual void _(PX.Data.Events.RowPersisting<CRCase> e)
  {
    CRCase row = e.Row;
    CRCaseClass crCaseClass = PXSelectorAttribute.Select<CRCase.caseClassID>(((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<CRCase>>) e).Cache, (object) e.Row) as CRCaseClass;
    PXCache cache1 = ((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<CRCase>>) e).Cache;
    CRCase crCase1 = row;
    bool? nullable;
    int num1;
    if (!row.IsBillable.GetValueOrDefault())
    {
      if (crCaseClass != null)
      {
        nullable = crCaseClass.RequireCustomer;
        if (nullable.GetValueOrDefault())
          goto label_6;
      }
      if (crCaseClass != null)
      {
        nullable = crCaseClass.RequireVendor;
        if (nullable.GetValueOrDefault())
          goto label_6;
      }
      num1 = 2;
      goto label_7;
    }
label_6:
    num1 = 0;
label_7:
    PXDefaultAttribute.SetPersistingCheck<CRCase.customerID>(cache1, (object) crCase1, (PXPersistingCheck) num1);
    PXCache cache2 = ((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<CRCase>>) e).Cache;
    CRCase crCase2 = row;
    int num2;
    if (crCaseClass != null && PXAccess.FeatureInstalled<FeaturesSet.contractManagement>())
    {
      nullable = crCaseClass.RequireContract;
      if (nullable.GetValueOrDefault())
      {
        num2 = 0;
        goto label_11;
      }
    }
    num2 = 2;
label_11:
    PXDefaultAttribute.SetPersistingCheck<CRCase.contractID>(cache2, (object) crCase2, (PXPersistingCheck) num2);
    PXCache cache3 = ((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<CRCase>>) e).Cache;
    CRCase crCase3 = row;
    int num3;
    if (crCaseClass != null)
    {
      nullable = crCaseClass.RequireContact;
      if (nullable.GetValueOrDefault())
      {
        num3 = 0;
        goto label_15;
      }
    }
    num3 = 2;
label_15:
    PXDefaultAttribute.SetPersistingCheck<CRCase.contactID>(cache3, (object) crCase3, (PXPersistingCheck) num3);
    if (!(PXSelectorAttribute.Select<CRCase.customerID>(((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<CRCase>>) e).Cache, (object) e.Row, (object) e.Row.CustomerID) is BAccount baccount) || !(baccount.Type == "CP") || crCaseClass == null)
      return;
    nullable = crCaseClass.AllowEmployeeAsContact;
    bool flag = false;
    if (nullable.GetValueOrDefault() == flag & nullable.HasValue)
      throw new PXException("The {0} case class cannot be changed because this case class is associated with at least one case.");
  }

  protected virtual void _(PX.Data.Events.RowDeleted<CRCase> e)
  {
    CRCase row = e.Row;
    string caseCd = row?.CaseCD;
    if (row == null || string.IsNullOrEmpty(caseCd))
      return;
    PXCache cache = ((PXSelectBase) this.CaseRefs).Cache;
    HybridDictionary hybridDictionary = new HybridDictionary();
    foreach (PXResult<CRCaseReference> pxResult in PXSelectBase<CRCaseReference, PXSelect<CRCaseReference, Where<CRCaseReference.parentCaseCD, Equal<Required<CRCaseReference.parentCaseCD>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) caseCd
    }))
    {
      CRCaseReference crCaseReference = PXResult<CRCaseReference>.op_Implicit(pxResult);
      string key = crCaseReference.ChildCaseCD ?? string.Empty;
      if (!hybridDictionary.Contains((object) key))
      {
        hybridDictionary.Add((object) key, (object) crCaseReference);
        cache.Delete((object) crCaseReference);
      }
    }
    foreach (PXResult<CRCaseReference> pxResult in PXSelectBase<CRCaseReference, PXSelect<CRCaseReference, Where<CRCaseReference.childCaseCD, Equal<Required<CRCaseReference.childCaseCD>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) caseCd
    }))
    {
      CRCaseReference crCaseReference = PXResult<CRCaseReference>.op_Implicit(pxResult);
      string key = crCaseReference.ParentCaseCD ?? string.Empty;
      if (!hybridDictionary.Contains((object) key))
      {
        hybridDictionary.Add((object) key, (object) crCaseReference);
        cache.Delete((object) crCaseReference);
      }
    }
  }

  protected virtual void _(PX.Data.Events.RowUpdated<CRCase> e)
  {
    CRCase row = e.Row;
    CRCase oldRow = e.OldRow;
    if (row == null || oldRow == null)
      return;
    if (!row.OwnerID.HasValue)
    {
      row.AssignDate = new DateTime?();
    }
    else
    {
      if (oldRow.OwnerID.HasValue)
        return;
      row.AssignDate = new DateTime?(PXTimeZoneInfo.Now);
    }
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<CRCase, CRCase.contractID> e)
  {
    CRCase row = e.Row;
    if (row == null || !row.CustomerID.HasValue)
      return;
    List<object> objectList = PXSelectorAttribute.SelectAll<CRCase.contractID>(((PX.Data.Events.Event<PXFieldDefaultingEventArgs, PX.Data.Events.FieldDefaulting<CRCase, CRCase.contractID>>) e).Cache, (object) e.Row);
    if (objectList.Exists((Predicate<object>) (contract =>
    {
      int? contractId1 = PXResult.Unwrap<PX.Objects.CT.Contract>(contract).ContractID;
      int? contractId2 = row.ContractID;
      return contractId1.GetValueOrDefault() == contractId2.GetValueOrDefault() & contractId1.HasValue == contractId2.HasValue;
    })))
      ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<CRCase, CRCase.contractID>, CRCase, object>) e).NewValue = (object) row.ContractID;
    else if (objectList.Count == 1)
      ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<CRCase, CRCase.contractID>, CRCase, object>) e).NewValue = (object) PXResult.Unwrap<PX.Objects.CT.Contract>(objectList[0]).ContractID;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<CRCase, CRCase.contractID>>) e).Cancel = true;
  }

  protected virtual void _(PX.Data.Events.FieldUpdating<CRCase, CRCase.contractID> e)
  {
    CRCase row1 = e.Row;
    PX.Objects.CT.Contract contract = PXResult.Unwrap<PX.Objects.CT.Contract>(PXSelectorAttribute.Select<CRCase.contractID>(((PX.Data.Events.Event<PXFieldUpdatingEventArgs, PX.Data.Events.FieldUpdating<CRCase, CRCase.contractID>>) e).Cache, (object) e.Row, ((PX.Data.Events.FieldUpdatingBase<PX.Data.Events.FieldUpdating<CRCase, CRCase.contractID>>) e).NewValue));
    if (row1 == null || contract == null)
      return;
    DateTime? businessDate1 = ((PXGraph) this).Accessinfo.BusinessDate;
    if (!businessDate1.HasValue)
      return;
    PX.Objects.CT.Contract row2 = contract;
    businessDate1 = ((PXGraph) this).Accessinfo.BusinessDate;
    DateTime businessDate2 = businessDate1.Value;
    int num;
    ref int local = ref num;
    if (!ContractMaint.IsInGracePeriod(row2, businessDate2, out local))
      return;
    ((PX.Data.Events.Event<PXFieldUpdatingEventArgs, PX.Data.Events.FieldUpdating<CRCase, CRCase.contractID>>) e).Cache.RaiseExceptionHandling<CRCase.contractID>((object) row1, ((PX.Data.Events.FieldUpdatingBase<PX.Data.Events.FieldUpdating<CRCase, CRCase.contractID>>) e).NewValue, (Exception) new PXSetPropertyException("Selected Contract is on the grace period. {0} day(s) left before the expiration.", (PXErrorLevel) 2, new object[1]
    {
      (object) num
    }));
  }

  protected virtual void _(PX.Data.Events.FieldUpdated<CRCase, CRCase.caseClassID> e)
  {
    if (e.Row == null || ((PX.Data.Events.FieldUpdatedBase<PX.Data.Events.FieldUpdated<CRCase, CRCase.caseClassID>, CRCase, object>) e).OldValue == null || !(PXSelectorAttribute.Select<CRCase.caseClassID>(((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<CRCase, CRCase.caseClassID>>) e).Cache, (object) e.Row, ((PX.Data.Events.FieldUpdatedBase<PX.Data.Events.FieldUpdated<CRCase, CRCase.caseClassID>, CRCase, object>) e).OldValue) is CRCaseClass crCaseClass) || !crCaseClass.AllowEmployeeAsContact.GetValueOrDefault() || e.Row.ContactID.HasValue)
      return;
    e.Row.CustomerID = new int?();
  }

  protected virtual void _(PX.Data.Events.RowInserted<CRCaseReference> e)
  {
    CRCaseReference row = e.Row;
    if (row == null || row.ChildCaseCD == null || row.ParentCaseCD == null)
      return;
    CRCaseReference crCaseReference = PXResultset<CRCaseReference>.op_Implicit(PXSelectBase<CRCaseReference, PXSelect<CRCaseReference, Where<CRCaseReference.parentCaseCD, Equal<Required<CRCaseReference.parentCaseCD>>, And<CRCaseReference.childCaseCD, Equal<Required<CRCaseReference.childCaseCD>>>>>.Config>.Select((PXGraph) this, new object[2]
    {
      (object) row.ChildCaseCD,
      (object) row.ParentCaseCD
    }));
    if (crCaseReference == null)
      return;
    ((PX.Data.Events.Event<PXRowInsertedEventArgs, PX.Data.Events.RowInserted<CRCaseReference>>) e).Cache.Delete((object) crCaseReference);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdating<CRCaseReference, CRCaseReference.relationType> e)
  {
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<CRCaseReference, CRCaseReference.childCaseCD> e)
  {
    CRCaseReference row = e.Row;
    if (row != null && ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<CRCaseReference, CRCaseReference.childCaseCD>, CRCaseReference, object>) e).NewValue != null && object.Equals((object) row.ParentCaseCD, ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<CRCaseReference, CRCaseReference.childCaseCD>, CRCaseReference, object>) e).NewValue))
    {
      ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<CRCaseReference, CRCaseReference.childCaseCD>>) e).Cancel = true;
      throw new PXSetPropertyException("Case cannot depend upon itself.");
    }
  }

  private CRCaseRelated SelectCase(object caseCd)
  {
    if (caseCd == null)
      return (CRCaseRelated) null;
    return PXResultset<CRCaseRelated>.op_Implicit(PXSelectBase<CRCaseRelated, PXSelect<CRCaseRelated, Where<CRCase.caseCD, Equal<Required<CRCase.caseCD>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      caseCd
    }));
  }

  protected virtual void ReleaseCase(CRCase item)
  {
    RegisterEntry instance1 = (RegisterEntry) PXGraph.CreateInstance(typeof (RegisterEntry));
    PXSelect<EPActivityApprove, Where<PMTimeActivity.refNoteID, Equal<Required<PMTimeActivity.refNoteID>>>> pxSelect = new PXSelect<EPActivityApprove, Where<PMTimeActivity.refNoteID, Equal<Required<PMTimeActivity.refNoteID>>>>((PXGraph) this);
    List<EPActivityApprove> activities = new List<EPActivityApprove>();
    object[] objArray = new object[1]
    {
      (object) item.NoteID
    };
    foreach (PXResult<EPActivityApprove> pxResult in ((PXSelectBase<EPActivityApprove>) pxSelect).Select(objArray))
    {
      EPActivityApprove epActivityApprove = PXResult<EPActivityApprove>.op_Implicit(pxResult);
      activities.Add(epActivityApprove);
      int? nullable = epActivityApprove.TimeSpent;
      if (nullable.GetValueOrDefault() == 0)
      {
        nullable = epActivityApprove.TimeBillable;
        if (nullable.GetValueOrDefault() == 0)
          continue;
      }
      nullable = epActivityApprove.ApproverID;
      if (nullable.HasValue && epActivityApprove.ApprovalStatus != "CD" && epActivityApprove.ApprovalStatus != "CL")
        throw new PXException("One or more activities that require Project Manager's approval is not approved. Case can be released only when all activities are approved.");
    }
    bool activityAdded = false;
    if (item.ContractID.HasValue)
    {
      using (PXTransactionScope transactionScope = new PXTransactionScope())
      {
        this.RecordContractUsage(item);
        ((PXGraph) this).TimeStamp = EmployeeActivitiesRelease.RecordCostTrans(instance1, activities, out activityAdded) ? ((PXGraph) instance1).TimeStamp : throw new PXException("The system failed to record cost transactions to the Project module.");
        item.Released = new bool?(true);
        string resolution = item.Resolution;
        ((PXSelectBase<CRCase>) this.Case).Update(item);
        item.Resolution = resolution;
        ((PXAction) this.Save).Press();
        transactionScope.Complete();
      }
    }
    else
    {
      PX.Objects.AR.Customer customer = PXResultset<PX.Objects.AR.Customer>.op_Implicit(PXSelectBase<PX.Objects.AR.Customer, PXSelect<PX.Objects.AR.Customer, Where<PX.Objects.AR.Customer.bAccountID, Equal<Required<PX.Objects.AR.Customer.bAccountID>>>>.Config>.Select((PXGraph) this, new object[1]
      {
        (object) item.CustomerID
      }));
      if (customer == null)
        throw new PXException("The customer was not found. A customer is required for the case to be billed.");
      using (PXTransactionScope transactionScope = new PXTransactionScope())
      {
        ARInvoiceEntry instance2 = PXGraph.CreateInstance<ARInvoiceEntry>();
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        // ISSUE: method pointer
        ((PXGraph) instance2).FieldVerifying.AddHandler<PX.Objects.AR.ARInvoice.projectID>(CRCaseMaint.\u003C\u003Ec.\u003C\u003E9__62_0 ?? (CRCaseMaint.\u003C\u003Ec.\u003C\u003E9__62_0 = new PXFieldVerifying((object) CRCaseMaint.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003CReleaseCase\u003Eb__62_0))));
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        // ISSUE: method pointer
        ((PXGraph) instance2).FieldVerifying.AddHandler<PX.Objects.AR.ARTran.projectID>(CRCaseMaint.\u003C\u003Ec.\u003C\u003E9__62_1 ?? (CRCaseMaint.\u003C\u003Ec.\u003C\u003E9__62_1 = new PXFieldVerifying((object) CRCaseMaint.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003CReleaseCase\u003Eb__62_1))));
        PX.Objects.AR.ARInvoice instance3 = (PX.Objects.AR.ARInvoice) ((PXGraph) instance2).Caches[typeof (PX.Objects.AR.ARInvoice)].CreateInstance();
        instance3.DocType = "INV";
        PX.Objects.AR.ARInvoice arInvoice1 = (PX.Objects.AR.ARInvoice) ((PXGraph) instance2).Caches[typeof (PX.Objects.AR.ARInvoice)].Insert((object) instance3);
        PX.Objects.AR.ARInvoice copy1 = (PX.Objects.AR.ARInvoice) ((PXSelectBase) instance2.Document).Cache.CreateCopy((object) ((PXSelectBase<PX.Objects.AR.ARInvoice>) instance2.Document).Current);
        copy1.CustomerID = item.CustomerID;
        copy1.CustomerLocationID = item.LocationID;
        copy1.DocDate = ((PXGraph) this).Accessinfo.BusinessDate;
        copy1.DocDesc = item.Subject;
        PX.Objects.AR.ARInvoice arInvoice2 = ((PXSelectBase<PX.Objects.AR.ARInvoice>) instance2.Document).Update(copy1);
        ((PXGraph) instance2).Caches[typeof (PX.Objects.AR.ARInvoice)].SetValueExt<PX.Objects.AR.ARInvoice.hold>((object) arInvoice2, (object) false);
        ((PXSelectBase<PX.Objects.AR.Customer>) instance2.customer).Current.CreditRule = customer.CreditRule;
        PX.Objects.AR.ARInvoice arInvoice3 = ((PXSelectBase<PX.Objects.AR.ARInvoice>) instance2.Document).Update(arInvoice2);
        foreach (PX.Objects.AR.ARTran arTran in this.GenerateARTrans(item))
          ((PXSelectBase<PX.Objects.AR.ARTran>) instance2.Transactions).Insert(arTran);
        PX.Objects.AR.ARInvoice copy2 = (PX.Objects.AR.ARInvoice) ((PXGraph) instance2).Caches[typeof (PX.Objects.AR.ARInvoice)].CreateCopy((object) arInvoice3);
        arInvoice3.CuryOrigDocAmt = arInvoice3.CuryDocBal;
        arInvoice3.OrigDocAmt = arInvoice3.DocBal;
        ((PXGraph) instance2).Caches[typeof (PX.Objects.AR.ARInvoice)].RaiseRowUpdated((object) arInvoice3, (object) copy2);
        ((PXGraph) instance2).Caches[typeof (PX.Objects.AR.ARInvoice)].SetValue<PX.Objects.AR.ARInvoice.curyOrigDocAmt>((object) arInvoice3, (object) arInvoice3.CuryDocBal);
        ((PXGraph) instance2).Actions.PressSave();
        item.Released = new bool?(true);
        item.ARRefNbr = ((PXSelectBase<PX.Objects.AR.ARInvoice>) instance2.Document).Current.RefNbr;
        string resolution = item.Resolution;
        ((PXSelectBase<CRCase>) this.Case).Update(item);
        item.Resolution = resolution;
        ((PXAction) this.Save).Press();
        if (!EmployeeActivitiesRelease.RecordCostTrans(instance1, activities, out activityAdded))
          throw new PXException("The system failed to record cost transactions to the Project module.");
        transactionScope.Complete();
      }
    }
    if (!activityAdded)
      return;
    EPSetup epSetup = PXResultset<EPSetup>.op_Implicit(PXSelectBase<EPSetup, PXSelect<EPSetup>.Config>.Select((PXGraph) instance1, Array.Empty<object>()));
    if (epSetup == null || !epSetup.AutomaticReleasePM.GetValueOrDefault())
      return;
    RegisterRelease.Release(((PXSelectBase<PMRegister>) instance1.Document).Current);
  }

  protected virtual void RecordContractUsage(CRCase item)
  {
    RegisterEntry instance = PXGraph.CreateInstance<RegisterEntry>();
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: method pointer
    ((PXGraph) instance).FieldVerifying.AddHandler<PMTran.projectID>(CRCaseMaint.\u003C\u003Ec.\u003C\u003E9__63_0 ?? (CRCaseMaint.\u003C\u003Ec.\u003C\u003E9__63_0 = new PXFieldVerifying((object) CRCaseMaint.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003CRecordContractUsage\u003Eb__63_0))));
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: method pointer
    ((PXGraph) instance).FieldVerifying.AddHandler<PMTran.inventoryID>(CRCaseMaint.\u003C\u003Ec.\u003C\u003E9__63_1 ?? (CRCaseMaint.\u003C\u003Ec.\u003C\u003E9__63_1 = new PXFieldVerifying((object) CRCaseMaint.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003CRecordContractUsage\u003Eb__63_1))));
    ((PXSelectBase) instance.Document).Cache.Insert();
    ((PXSelectBase<PMRegister>) instance.Document).Current.Description = item.Subject;
    ((PXSelectBase<PMRegister>) instance.Document).Current.Released = new bool?(true);
    ((PXSelectBase<PMRegister>) instance.Document).Current.Status = "R";
    GraphHelper.EnsureCachePersistence((PXGraph) instance, typeof (CRPMTimeActivity));
    foreach (PMTran pmTran in this.GeneratePMTrans(item))
    {
      ((PXSelectBase<PMTran>) instance.Transactions).Insert(pmTran);
      UsageMaint.AddUsage(((PXSelectBase) instance.ContractDetails).Cache, pmTran.ProjectID, pmTran.InventoryID, pmTran.BillableQty.GetValueOrDefault(), pmTran.UOM);
    }
    item.Released = new bool?(true);
    string resolution = item.Resolution;
    ((PXSelectBase<CRCase>) this.Case).Update(item);
    item.Resolution = resolution;
    ((PXAction) instance.Save).Press();
  }

  public virtual object GetValueExt(string viewName, object data, string fieldName)
  {
    object valueExt = ((PXGraph) this).GetValueExt(viewName, data, fieldName);
    if (string.Equals(viewName, "CaseCurrent", StringComparison.OrdinalIgnoreCase) && string.Equals(fieldName, "CustomerID", StringComparison.OrdinalIgnoreCase) && valueExt is PXFieldState && !string.IsNullOrEmpty(((PXFieldState) valueExt).Error))
      ((PXFieldState) valueExt).Error = (string) null;
    return valueExt;
  }

  protected virtual List<PMTran> GeneratePMTrans(CRCase @case)
  {
    PX.Objects.CT.Contract contract = PXResultset<PX.Objects.CT.Contract>.op_Implicit(PXSelectBase<PX.Objects.CT.Contract, PXSelect<PX.Objects.CT.Contract, Where<PX.Objects.CT.Contract.contractID, Equal<Required<PX.Objects.CT.Contract.contractID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) @case.ContractID
    }));
    CRCaseClass crCaseClass = PXResultset<CRCaseClass>.op_Implicit(PXSelectBase<CRCaseClass, PXSelect<CRCaseClass, Where<CRCaseClass.caseClassID, Equal<Required<CRCaseClass.caseClassID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) @case.CaseClassID
    }));
    List<PMTran> pmTrans = new List<PMTran>();
    DateTime? nullable1 = ((PXGraph) this).Accessinfo.BusinessDate;
    DateTime dateTime1 = nullable1 ?? @case.ReportedOnDateTime.Value;
    DateTime dateTime2 = dateTime1.Add(new TimeSpan(0, @case.TimeBillable.GetValueOrDefault(), 0));
    PXResultset<CRPMTimeActivity> pxResultset = PXSelectBase<CRPMTimeActivity, PXViewOf<CRPMTimeActivity>.BasedOn<SelectFromBase<CRPMTimeActivity, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<CRPMTimeActivity.refNoteID, Equal<P.AsGuid>>>>>.And<BqlOperand<CRPMTimeActivity.type, IBqlString>.IsNotEqual<EPActivityType.type.systemMessage>>>.Order<By<BqlField<CRPMTimeActivity.createdDateTime, IBqlDateTime>.Desc>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) @case.NoteID
    });
    if (pxResultset.Count > 0)
    {
      nullable1 = PXResult<CRPMTimeActivity>.op_Implicit(pxResultset[0]).StartDate;
      dateTime1 = nullable1.Value;
      dateTime2 = dateTime1;
    }
    PXCache pxCache = (PXCache) null;
    foreach (PXResult<CRPMTimeActivity> pxResult in pxResultset)
    {
      CRPMTimeActivity crpmTimeActivity = PXResult<CRPMTimeActivity>.op_Implicit(pxResult);
      if (pxCache == null)
        pxCache = ((PXGraph) this).Caches[((object) crpmTimeActivity).GetType()];
      if (crpmTimeActivity.ClassID.GetValueOrDefault() == 2 && crpmTimeActivity.IsBillable.GetValueOrDefault())
      {
        nullable1 = crpmTimeActivity.StartDate;
        if (nullable1.HasValue)
        {
          nullable1 = crpmTimeActivity.StartDate;
          if (nullable1.Value < dateTime1)
          {
            nullable1 = crpmTimeActivity.StartDate;
            dateTime1 = nullable1.Value;
          }
        }
        nullable1 = crpmTimeActivity.EndDate;
        if (nullable1.HasValue)
        {
          nullable1 = crpmTimeActivity.EndDate;
          if (nullable1.Value > dateTime2)
          {
            nullable1 = crpmTimeActivity.EndDate;
            dateTime2 = nullable1.Value;
          }
        }
        crpmTimeActivity.Billed = new bool?(true);
      }
      crpmTimeActivity.Released = new bool?(true);
      pxCache.Update((object) crpmTimeActivity);
    }
    int? nullable2 = contract.CaseItemID;
    if (nullable2.HasValue)
    {
      PX.Objects.IN.InventoryItem inventoryItem = PXResultset<PX.Objects.IN.InventoryItem>.op_Implicit(PXSelectBase<PX.Objects.IN.InventoryItem, PXSelect<PX.Objects.IN.InventoryItem, Where<PX.Objects.IN.InventoryItem.inventoryID, Equal<Required<PX.Objects.IN.InventoryItem.inventoryID>>>>.Config>.Select((PXGraph) this, new object[1]
      {
        (object) contract.CaseItemID
      }));
      pmTrans.Add(new PMTran()
      {
        ProjectID = contract.ContractID,
        InventoryID = contract.CaseItemID,
        AccountGroupID = contract.ContractAccountGroup,
        OrigRefID = @case.NoteID,
        BAccountID = @case.CustomerID,
        LocationID = @case.LocationID,
        Description = @case.Subject,
        StartDate = new DateTime?(dateTime1),
        EndDate = new DateTime?(dateTime2),
        Date = new DateTime?(dateTime2),
        Qty = new Decimal?((Decimal) 1),
        BillableQty = new Decimal?((Decimal) 1),
        UOM = inventoryItem.SalesUnit,
        Released = new bool?(true),
        Allocated = new bool?(true),
        BillingID = contract.BillingID,
        IsQtyOnly = new bool?(true),
        CaseCD = @case.CaseCD
      });
    }
    nullable2 = crCaseClass.LabourItemID;
    if (nullable2.HasValue)
    {
      nullable2 = @case.TimeBillable;
      int num1 = nullable2.GetValueOrDefault();
      nullable2 = crCaseClass.OvertimeItemID;
      if (nullable2.HasValue)
      {
        int num2 = num1;
        nullable2 = @case.OvertimeBillable;
        int valueOrDefault = nullable2.GetValueOrDefault();
        num1 = num2 - valueOrDefault;
      }
      if (num1 > 0)
      {
        nullable2 = crCaseClass.PerItemBilling;
        int num3 = 0;
        if (nullable2.GetValueOrDefault() == num3 & nullable2.HasValue)
        {
          nullable2 = crCaseClass.RoundingInMinutes;
          int num4 = 1;
          if (nullable2.GetValueOrDefault() > num4 & nullable2.HasValue)
          {
            int int32 = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(num1) / Convert.ToDecimal((object) crCaseClass.RoundingInMinutes)));
            nullable2 = crCaseClass.RoundingInMinutes;
            int valueOrDefault = nullable2.GetValueOrDefault();
            num1 = int32 * valueOrDefault;
          }
        }
        nullable2 = crCaseClass.PerItemBilling;
        int num5 = 0;
        if (nullable2.GetValueOrDefault() == num5 & nullable2.HasValue)
        {
          nullable2 = crCaseClass.MinBillTimeInMinutes;
          int num6 = 0;
          if (nullable2.GetValueOrDefault() > num6 & nullable2.HasValue)
          {
            int val1 = num1;
            nullable2 = crCaseClass.MinBillTimeInMinutes;
            int val2 = nullable2.Value;
            num1 = Math.Max(val1, val2);
          }
        }
        PX.Objects.IN.InventoryItem inventoryItem = PXResultset<PX.Objects.IN.InventoryItem>.op_Implicit(PXSelectBase<PX.Objects.IN.InventoryItem, PXSelect<PX.Objects.IN.InventoryItem, Where<PX.Objects.IN.InventoryItem.inventoryID, Equal<Required<PX.Objects.IN.InventoryItem.inventoryID>>>>.Config>.Select((PXGraph) this, new object[1]
        {
          (object) crCaseClass.LabourItemID
        }));
        PMTran pmTran = new PMTran()
        {
          ProjectID = contract.ContractID,
          InventoryID = crCaseClass.LabourItemID,
          AccountGroupID = contract.ContractAccountGroup,
          OrigRefID = @case.NoteID,
          BAccountID = @case.CustomerID,
          LocationID = @case.LocationID,
          Description = @case.Subject,
          StartDate = new DateTime?(dateTime1),
          EndDate = new DateTime?(dateTime2),
          Date = new DateTime?(dateTime2),
          UOM = inventoryItem.SalesUnit,
          Qty = new Decimal?(Convert.ToDecimal(TimeSpan.FromMinutes((double) num1).TotalHours))
        };
        pmTran.BillableQty = pmTran.Qty;
        pmTran.Released = new bool?(true);
        pmTran.Allocated = new bool?(true);
        pmTran.BillingID = contract.BillingID;
        pmTran.IsQtyOnly = new bool?(true);
        pmTran.CaseCD = @case.CaseCD;
        pmTrans.Add(pmTran);
      }
    }
    nullable2 = crCaseClass.OvertimeItemID;
    if (nullable2.HasValue)
    {
      PX.Objects.IN.InventoryItem inventoryItem = PXResultset<PX.Objects.IN.InventoryItem>.op_Implicit(PXSelectBase<PX.Objects.IN.InventoryItem, PXSelect<PX.Objects.IN.InventoryItem, Where<PX.Objects.IN.InventoryItem.inventoryID, Equal<Required<PX.Objects.IN.InventoryItem.inventoryID>>>>.Config>.Select((PXGraph) this, new object[1]
      {
        (object) crCaseClass.OvertimeItemID
      }));
      nullable2 = @case.OvertimeBillable;
      int num7 = nullable2.GetValueOrDefault();
      if (num7 > 0)
      {
        nullable2 = crCaseClass.PerItemBilling;
        int num8 = 0;
        if (nullable2.GetValueOrDefault() == num8 & nullable2.HasValue)
        {
          nullable2 = crCaseClass.RoundingInMinutes;
          int num9 = 1;
          if (nullable2.GetValueOrDefault() > num9 & nullable2.HasValue)
          {
            int int32 = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(num7) / Convert.ToDecimal((object) crCaseClass.RoundingInMinutes)));
            nullable2 = crCaseClass.RoundingInMinutes;
            int valueOrDefault = nullable2.GetValueOrDefault();
            num7 = int32 * valueOrDefault;
          }
        }
        PMTran pmTran = new PMTran()
        {
          ProjectID = contract.ContractID,
          InventoryID = crCaseClass.OvertimeItemID,
          AccountGroupID = contract.ContractAccountGroup,
          OrigRefID = @case.NoteID,
          BAccountID = @case.CustomerID,
          LocationID = @case.LocationID,
          Description = @case.Subject,
          StartDate = new DateTime?(dateTime1),
          EndDate = new DateTime?(dateTime2),
          Date = new DateTime?(dateTime2),
          Qty = new Decimal?(Convert.ToDecimal(TimeSpan.FromMinutes((double) num7).TotalHours))
        };
        pmTran.BillableQty = pmTran.Qty;
        pmTran.UOM = inventoryItem.SalesUnit;
        pmTran.Released = new bool?(true);
        pmTran.Allocated = new bool?(true);
        pmTran.BillingID = contract.BillingID;
        pmTran.IsQtyOnly = new bool?(true);
        pmTran.CaseCD = @case.CaseCD;
        pmTrans.Add(pmTran);
      }
    }
    return pmTrans;
  }

  protected virtual List<PX.Objects.AR.ARTran> GenerateARTrans(CRCase c)
  {
    CRCaseClass crCaseClass = PXResultset<CRCaseClass>.op_Implicit(PXSelectBase<CRCaseClass, PXSelect<CRCaseClass, Where<CRCaseClass.caseClassID, Equal<Required<CRCaseClass.caseClassID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) c.CaseClassID
    }));
    List<PX.Objects.AR.ARTran> arTrans = new List<PX.Objects.AR.ARTran>();
    DateTime dateTime1 = c.ReportedOnDateTime.Value;
    DateTime dateTime2 = dateTime1.Add(new TimeSpan(0, c.TimeBillable.GetValueOrDefault(), 0));
    PXResultset<CRPMTimeActivity> pxResultset = PXSelectBase<CRPMTimeActivity, PXSelect<CRPMTimeActivity, Where<CRPMTimeActivity.refNoteID, Equal<Required<CRPMTimeActivity.refNoteID>>>, OrderBy<Desc<CRPMTimeActivity.createdDateTime>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) c.NoteID
    });
    if (pxResultset.Count > 0)
    {
      dateTime1 = PXResult<CRPMTimeActivity>.op_Implicit(pxResultset[0]).StartDate.Value;
      dateTime2 = dateTime1;
    }
    PXCache pxCache = (PXCache) null;
    foreach (PXResult<CRPMTimeActivity> pxResult in pxResultset)
    {
      CRPMTimeActivity crpmTimeActivity = PXResult<CRPMTimeActivity>.op_Implicit(pxResult);
      if (pxCache == null)
        pxCache = ((PXGraph) this).Caches[((object) crpmTimeActivity).GetType()];
      pxCache.Current = (object) crpmTimeActivity;
      if (crpmTimeActivity.ClassID.GetValueOrDefault() == 2 && crpmTimeActivity.IsBillable.GetValueOrDefault())
      {
        DateTime? nullable = crpmTimeActivity.StartDate;
        if (nullable.HasValue)
        {
          nullable = crpmTimeActivity.StartDate;
          if (nullable.Value < dateTime1)
          {
            nullable = crpmTimeActivity.StartDate;
            dateTime1 = nullable.Value;
          }
        }
        nullable = crpmTimeActivity.EndDate;
        if (nullable.HasValue)
        {
          nullable = crpmTimeActivity.EndDate;
          if (nullable.Value > dateTime2)
          {
            nullable = crpmTimeActivity.EndDate;
            dateTime2 = nullable.Value;
          }
        }
        crpmTimeActivity.Billed = new bool?(true);
        pxCache.Update((object) crpmTimeActivity);
      }
    }
    int? nullable1 = crCaseClass.LabourItemID;
    if (nullable1.HasValue)
    {
      nullable1 = c.TimeBillable;
      int num1 = nullable1.GetValueOrDefault();
      nullable1 = crCaseClass.OvertimeItemID;
      if (nullable1.HasValue)
      {
        int num2 = num1;
        nullable1 = c.OvertimeBillable;
        int valueOrDefault = nullable1.GetValueOrDefault();
        num1 = num2 - valueOrDefault;
      }
      if (num1 > 0)
      {
        nullable1 = crCaseClass.RoundingInMinutes;
        int num3 = 1;
        if (nullable1.GetValueOrDefault() > num3 & nullable1.HasValue)
        {
          int int32 = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(num1) / Convert.ToDecimal((object) crCaseClass.RoundingInMinutes)));
          nullable1 = crCaseClass.RoundingInMinutes;
          int valueOrDefault = nullable1.GetValueOrDefault();
          num1 = int32 * valueOrDefault;
        }
        nullable1 = crCaseClass.MinBillTimeInMinutes;
        int num4 = 0;
        if (nullable1.GetValueOrDefault() > num4 & nullable1.HasValue)
        {
          int val1 = num1;
          nullable1 = crCaseClass.MinBillTimeInMinutes;
          int val2 = nullable1.Value;
          num1 = Math.Max(val1, val2);
        }
        PX.Objects.IN.InventoryItem inventoryItem = PXResultset<PX.Objects.IN.InventoryItem>.op_Implicit(PXSelectBase<PX.Objects.IN.InventoryItem, PXSelect<PX.Objects.IN.InventoryItem, Where<PX.Objects.IN.InventoryItem.inventoryID, Equal<Required<PX.Objects.IN.InventoryItem.inventoryID>>>>.Config>.Select((PXGraph) this, new object[1]
        {
          (object) crCaseClass.LabourItemID
        }));
        arTrans.Add(new PX.Objects.AR.ARTran()
        {
          InventoryID = crCaseClass.LabourItemID,
          TranDesc = c.Subject,
          UOM = inventoryItem.SalesUnit,
          Qty = new Decimal?(Convert.ToDecimal(TimeSpan.FromMinutes((double) num1).TotalHours)),
          CaseCD = c.CaseCD,
          ManualPrice = new bool?(false)
        });
      }
    }
    nullable1 = crCaseClass.OvertimeItemID;
    if (nullable1.HasValue)
    {
      PX.Objects.IN.InventoryItem inventoryItem = PXResultset<PX.Objects.IN.InventoryItem>.op_Implicit(PXSelectBase<PX.Objects.IN.InventoryItem, PXSelect<PX.Objects.IN.InventoryItem, Where<PX.Objects.IN.InventoryItem.inventoryID, Equal<Required<PX.Objects.IN.InventoryItem.inventoryID>>>>.Config>.Select((PXGraph) this, new object[1]
      {
        (object) crCaseClass.OvertimeItemID
      }));
      nullable1 = c.OvertimeBillable;
      int num5 = nullable1.GetValueOrDefault();
      if (num5 > 0)
      {
        nullable1 = crCaseClass.RoundingInMinutes;
        int num6 = 1;
        if (nullable1.GetValueOrDefault() > num6 & nullable1.HasValue)
        {
          int int32 = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(num5) / Convert.ToDecimal((object) crCaseClass.RoundingInMinutes)));
          nullable1 = crCaseClass.RoundingInMinutes;
          int valueOrDefault = nullable1.GetValueOrDefault();
          num5 = int32 * valueOrDefault;
        }
        arTrans.Add(new PX.Objects.AR.ARTran()
        {
          InventoryID = crCaseClass.OvertimeItemID,
          TranDesc = c.Subject,
          UOM = inventoryItem.SalesUnit,
          Qty = new Decimal?(Convert.ToDecimal(TimeSpan.FromMinutes((double) num5).TotalHours)),
          CaseCD = c.CaseCD,
          ManualPrice = new bool?(true)
        });
      }
    }
    return arTrans;
  }

  private void RecalcDetails(PXCache sender, CRCase row)
  {
    using (new PXConnectionScope())
    {
      CRCase crCase = row;
      if (!row.InitResponse.HasValue)
        crCase = (CRCase) sender.Locate((object) row);
      CRPMTimeActivity crpmTimeActivity = PXResult<CRPMTimeActivity, CRReminder>.op_Implicit((PXResult<CRPMTimeActivity, CRReminder>) new PXView((PXGraph) this, true, ((PXSelectBase) ((PXGraph) this).GetExtension<CRCaseMaint_ActivityDetailsExt>().Activities).View.BqlSelect.WhereNew<Where<CRActivity.refNoteID, Equal<Current<CRCase.noteID>>, And2<Where<CRActivity.isPrivate, IsNull, Or<CRActivity.isPrivate, Equal<False>>>, And<CRActivity.ownerID, IsNotNull, And2<Where<CRActivity.incoming, IsNull, Or<CRActivity.incoming, Equal<False>>>, And<Where<CRActivity.isExternal, IsNull, Or<CRActivity.isExternal, Equal<False>>>>>>>>>().OrderByNew<OrderBy<Asc<CRPMTimeActivity.startDate>>>()).SelectSingleBound(new object[1]
      {
        (object) crCase
      }, Array.Empty<object>()));
      if (crpmTimeActivity == null)
        return;
      DateTime? nullable = crpmTimeActivity.StartDate;
      if (!nullable.HasValue)
        return;
      TimeSpan timeSpan1;
      ref TimeSpan local1 = ref timeSpan1;
      nullable = row.ReportedOnDateTime;
      long ticks1 = nullable.Value.Ticks;
      local1 = new TimeSpan(ticks1);
      TimeSpan timeSpan2;
      ref TimeSpan local2 = ref timeSpan2;
      nullable = crpmTimeActivity.StartDate;
      long ticks2 = nullable.Value.Ticks;
      local2 = new TimeSpan(ticks2);
      sender.SetValue<CRCase.initResponse>((object) row, (object) ((int) timeSpan2.TotalMinutes - (int) timeSpan1.TotalMinutes));
      sender.RaiseFieldUpdated<CRCase.initResponse>((object) row, (object) null);
    }
  }

  private bool VerifyField<TField>(object row, object newValue) where TField : IBqlField
  {
    if (row == null)
      return true;
    bool flag = false;
    PXCache cach = ((PXGraph) this).Caches[row.GetType()];
    try
    {
      flag = cach.RaiseFieldVerifying<TField>(row, ref newValue);
    }
    catch (StackOverflowException ex)
    {
      throw;
    }
    catch (OutOfMemoryException ex)
    {
      throw;
    }
    catch (Exception ex)
    {
    }
    return flag;
  }

  private void CheckBillingSettings(CRCase @case)
  {
    CRCaseClass crCaseClass = PXResultset<CRPMTimeActivity>.op_Implicit(PXSelectBase<CRPMTimeActivity, PXSelectReadonly<CRPMTimeActivity, Where<CRPMTimeActivity.isBillable, Equal<True>, And2<Where<CRPMTimeActivity.uistatus, IsNull, Or<CRPMTimeActivity.uistatus, Equal<ActivityStatusListAttribute.open>>>, And<Where<CRPMTimeActivity.refNoteID, Equal<Current<CRCase.noteID>>>>>>>.Config>.SelectSingleBound((PXGraph) this, new object[1]
    {
      (object) @case
    }, Array.Empty<object>())) == null ? PXResultset<CRCaseClass>.op_Implicit(PXSelectBase<CRCaseClass, PXSelect<CRCaseClass, Where<CRCaseClass.caseClassID, Equal<Required<CRCaseClass.caseClassID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) @case.CaseClassID
    })) : throw new PXException("Case has On-Hold billable activities and cannot be closed.");
    int? nullable = crCaseClass.PerItemBilling;
    if (nullable.GetValueOrDefault() == 1)
      throw new PXException("Case is configured to bill by Activity. You can release only activities.");
    if (!@case.IsBillable.GetValueOrDefault())
      return;
    nullable = @case.ContractID;
    if (!nullable.HasValue)
    {
      nullable = crCaseClass.LabourItemID;
      if (!nullable.HasValue)
        throw new PXException("Cases of the given class cannot be billed. The 'Labor Item' and 'Overtime Class ID' must be specified in the Case Class if you want to bill for the hours. If you want to bill for the 'number of cases' set the Case Count Item in the Contract Template.");
      PX.Objects.IN.InventoryItem inventoryItem = PXResultset<PX.Objects.IN.InventoryItem>.op_Implicit(PXSelectBase<PX.Objects.IN.InventoryItem, PXSelect<PX.Objects.IN.InventoryItem, Where<PX.Objects.IN.InventoryItem.inventoryID, Equal<Required<PX.Objects.IN.InventoryItem.inventoryID>>>>.Config>.Select((PXGraph) this, new object[1]
      {
        (object) crCaseClass.LabourItemID
      }));
      if (inventoryItem == null)
        return;
      nullable = inventoryItem.SalesAcctID;
      if (!nullable.HasValue)
        throw new PXException("Sales Account is not configured for the Labour Item.");
    }
    else
    {
      PX.Objects.CT.Contract contract = PXResultset<PX.Objects.CT.Contract>.op_Implicit(PXSelectBase<PX.Objects.CT.Contract, PXSelect<PX.Objects.CT.Contract, Where<PX.Objects.CT.Contract.contractID, Equal<Current<CRCase.contractID>>>>.Config>.SelectSingleBound((PXGraph) this, new object[1]
      {
        (object) @case
      }, Array.Empty<object>()));
      nullable = crCaseClass.LabourItemID;
      if (nullable.HasValue)
        return;
      nullable = contract.CaseItemID;
      if (!nullable.HasValue)
        throw new PXException("Cases of the given class cannot be billed. The 'Labor Item' and 'Overtime Class ID' must be specified in the Case Class if you want to bill for the hours. If you want to bill for the 'number of cases' set the Case Count Item in the Contract Template.");
    }
  }
}
