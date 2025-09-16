// Decompiled with JetBrains decompiler
// Type: PX.Objects.CS.BranchValidator
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.Common.Extensions;
using PX.Objects.CR;
using PX.Objects.CT;
using PX.Objects.EP;
using PX.Objects.FA;
using PX.Objects.GL;
using PX.Objects.IN;
using PX.Objects.PM;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.CS;

public class BranchValidator
{
  protected int EntityCountInErrorMessage = 10;
  protected readonly PXGraph Graph;

  public BranchValidator(PXGraph graph) => this.Graph = graph;

  public virtual void CanBeBranchesDeletedSeparately(IReadOnlyCollection<PX.Objects.GL.Branch> branches)
  {
    foreach (PX.Objects.GL.Branch branch in (IEnumerable<PX.Objects.GL.Branch>) branches)
    {
      this.Graph.Caches[typeof (PX.Objects.GL.Branch)].ClearQueryCacheObsolete();
      PX.Objects.GL.DAC.Organization organizationById = OrganizationMaint.FindOrganizationByID(this.Graph, branch.OrganizationID);
      if (organizationById != null && organizationById.OrganizationType == "WithoutBranches")
        throw new PXException("The {0} branch cannot be deleted because it belongs to the {1} company of the Without Branches type.", new object[2]
        {
          (object) branch.BranchCD.Trim(),
          (object) organizationById.OrganizationCD.Trim()
        });
    }
    this.CanBeBranchesDeleted(branches);
  }

  public virtual void CanBeBranchesDeleted(
    IReadOnlyCollection<PX.Objects.GL.Branch> branches,
    bool isOrganizationWithoutBranchesDeletion = false)
  {
    int?[] array1 = branches.Select<PX.Objects.GL.Branch, int?>((Func<PX.Objects.GL.Branch, int?>) (b => b.BAccountID)).ToArray<int?>();
    int?[] array2 = branches.Select<PX.Objects.GL.Branch, int?>((Func<PX.Objects.GL.Branch, int?>) (b => b.BranchID)).ToArray<int?>();
    using (new PXReadBranchRestrictedScope())
    {
      this.CheckRelatedCashAccountsDontExist(array2);
      this.CheckRelatedEmployeesDoNotExist(array1);
      this.CheckRelatedGLHistoryDoesNotExist(array2);
      this.CheckRelatedGLTranDoesNotExist(array2);
      string exceptionMessage1;
      string exceptionMessage2;
      if (isOrganizationWithoutBranchesDeletion)
      {
        exceptionMessage1 = "The company cannot be deleted because its branch has been specified as an in-transit branch. To delete the company, on the Inventory Preferences (IN101000) form, specify a branch of another company in the In-Transit Branch box.";
        exceptionMessage2 = "The {0} company cannot be deleted because the following related fixed assets exist: {1}.";
      }
      else
      {
        exceptionMessage1 = "The branch cannot be deleted because it has been specified as an in-transit branch. To delete the branch, on the Inventory Preferences (IN101000) form, specify another branch in the In-Transit Branch box.";
        exceptionMessage2 = "The {0} branch cannot be deleted because the following related fixed assets exist: {1}.";
      }
      this.CheckRelatedWarehousesDontExist(array2, exceptionMessage1);
      this.CheckRelatedFixedAssetsDontExist(array2, exceptionMessage2);
      this.CheckRelatedProformaDoesNotExist(array2, "The {0} branch cannot be deleted because it is used in the following pro forma invoices: {1}.");
      this.CheckProjectOrTemplateDoesNotExist(array2);
      if (!isOrganizationWithoutBranchesDeletion)
        this.CheckRelatedBillingRulesDontExist(array2, "The {0} branch cannot be deleted because it is used in the following billing rules: {1}.");
      this.CheckExtendedBranch(array1);
    }
  }

  public virtual void CheckRelatedGLTranDoesNotExist(int?[] branchIDs)
  {
    if (branchIDs == null || ((IEnumerable<int?>) branchIDs).IsEmpty<int?>())
      return;
    PX.Objects.GL.GLTran glTran = PXResultset<PX.Objects.GL.GLTran>.op_Implicit(PXSelectBase<PX.Objects.GL.GLTran, PXSelectReadonly<PX.Objects.GL.GLTran, Where<PX.Objects.GL.GLTran.branchID, In<Required<PX.Objects.GL.GLTran.branchID>>>>.Config>.SelectSingleBound(this.Graph, (object[]) null, new object[1]
    {
      (object) branchIDs
    }));
    if (glTran != null)
      throw new PXException("The {0} branch or branches cannot be deleted because the related {1} transaction has been posted.", new object[2]
      {
        (object) BranchMaint.FindBranchByID(this.Graph, glTran.BranchID).BranchCD.Trim(),
        (object) ((object) glTran).ToString()
      });
  }

  public virtual void CheckRelatedCashAccountsDontExist(int?[] branchIDs)
  {
    if (branchIDs == null || ((IEnumerable<int?>) branchIDs).IsEmpty<int?>())
      return;
    PX.Objects.CA.CashAccount[] array = GraphHelper.RowCast<PX.Objects.CA.CashAccount>((IEnumerable) PXSelectBase<PX.Objects.CA.CashAccount, PXSelectReadonly<PX.Objects.CA.CashAccount, Where<PX.Objects.CA.CashAccount.branchID, In<Required<PX.Objects.CA.CashAccount.branchID>>, And<PX.Objects.CA.CashAccount.restrictVisibilityWithBranch, Equal<boolTrue>>>>.Config>.SelectWindowed(this.Graph, 0, this.EntityCountInErrorMessage + 1, new object[1]
    {
      (object) branchIDs
    })).ToArray<PX.Objects.CA.CashAccount>();
    if (((IEnumerable<PX.Objects.CA.CashAccount>) array).Any<PX.Objects.CA.CashAccount>())
      throw new PXException("The {0} branch or branches cannot be deleted because the following related cash accounts exist: {1}.", new object[2]
      {
        (object) ((ICollection<string>) ((IEnumerable<PX.Objects.GL.Branch>) BranchMaint.FindBranchesByID(this.Graph, branchIDs).ToArray<PX.Objects.GL.Branch>()).Select<PX.Objects.GL.Branch, string>((Func<PX.Objects.GL.Branch, string>) (b => b.BranchCD.Trim())).ToArray<string>()).JoinIntoStringForMessage<string>(),
        (object) ((ICollection<string>) ((IEnumerable<PX.Objects.CA.CashAccount>) array).Select<PX.Objects.CA.CashAccount, string>((Func<PX.Objects.CA.CashAccount, string>) (a => a.CashAccountCD.Trim())).ToArray<string>()).JoinIntoStringForMessage<string>(this.EntityCountInErrorMessage)
      });
  }

  public virtual void CheckRelatedEmployeesDoNotExist(int?[] branchBAccountIDs)
  {
    if (branchBAccountIDs == null || ((IEnumerable<int?>) branchBAccountIDs).IsEmpty<int?>())
      return;
    EPEmployee[] array = GraphHelper.RowCast<EPEmployee>((IEnumerable) PXSelectBase<EPEmployee, PXSelectReadonly<EPEmployee, Where<EPEmployee.parentBAccountID, In<Required<EPEmployee.parentBAccountID>>>>.Config>.SelectWindowed(this.Graph, 0, this.EntityCountInErrorMessage + 1, new object[1]
    {
      (object) branchBAccountIDs
    })).ToArray<EPEmployee>();
    if (((IEnumerable<EPEmployee>) array).Any<EPEmployee>())
      throw new PXException("The {0} branch or branches cannot be deleted because the following employees are assigned to the branches: {1}.", new object[2]
      {
        (object) ((ICollection<string>) GraphHelper.RowCast<PX.Objects.GL.Branch>((IEnumerable) PXSelectBase<PX.Objects.GL.Branch, PXSelectReadonly<PX.Objects.GL.Branch, Where<PX.Objects.GL.Branch.bAccountID, In<Required<PX.Objects.GL.Branch.bAccountID>>>>.Config>.Select(this.Graph, new object[1]
        {
          (object) ((IEnumerable<EPEmployee>) array).Take<EPEmployee>(this.EntityCountInErrorMessage).Select<EPEmployee, int?>((Func<EPEmployee, int?>) (e => e.ParentBAccountID)).ToArray<int?>()
        })).Select<PX.Objects.GL.Branch, string>((Func<PX.Objects.GL.Branch, string>) (b => b.BranchCD.Trim())).ToArray<string>()).JoinIntoStringForMessage<string>(),
        (object) ((ICollection<string>) ((IEnumerable<EPEmployee>) array).Select<EPEmployee, string>((Func<EPEmployee, string>) (e => e.AcctCD.Trim())).ToArray<string>()).JoinIntoStringForMessage<string>(this.EntityCountInErrorMessage)
      });
  }

  public virtual void CheckRelatedGLHistoryDoesNotExist(int?[] branchIDs)
  {
    if (branchIDs == null || ((IEnumerable<int?>) branchIDs).IsEmpty<int?>())
      return;
    GLHistory toBranchGlHistory = GLUtility.GetRelatedToBranchGLHistory(this.Graph, branchIDs);
    if (toBranchGlHistory == null)
      return;
    PX.Objects.GL.Branch branchById = BranchMaint.FindBranchByID(this.Graph, toBranchGlHistory.BranchID);
    if (branchById != null)
      throw new PXException("Branches {0} can not be deleted because related posted GL transaction exist.", new object[1]
      {
        (object) branchById.BranchCD.Trim()
      });
  }

  public virtual void CheckProjectOrTemplateDoesNotExist(int?[] branchIDs)
  {
    if (branchIDs == null || ((IEnumerable<int?>) branchIDs).IsEmpty<int?>())
      return;
    PMProject[] array = GraphHelper.RowCast<PMProject>((IEnumerable) PXSelectBase<PMProject, PXSelectReadonly<PMProject, Where<PMProject.defaultBranchID, In<Required<PMProject.defaultBranchID>>, And<Where<PMProject.baseType, Equal<CTPRType.project>, Or<PMProject.baseType, Equal<CTPRType.projectTemplate>>>>>>.Config>.SelectWindowed(this.Graph, 0, this.EntityCountInErrorMessage + 1, new object[1]
    {
      (object) branchIDs
    })).ToArray<PMProject>();
    if (((IEnumerable<PMProject>) array).Any<PMProject>())
      throw new PXException("The {0} branch cannot be deleted because it is used in the following projects or project templates: {1}.", new object[2]
      {
        (object) ((ICollection<string>) ((IEnumerable<PX.Objects.GL.Branch>) BranchMaint.FindBranchesByID(this.Graph, branchIDs).ToArray<PX.Objects.GL.Branch>()).Select<PX.Objects.GL.Branch, string>((Func<PX.Objects.GL.Branch, string>) (b => b.BranchCD.Trim())).ToArray<string>()).JoinIntoStringForMessage<string>(),
        (object) ((ICollection<string>) ((IEnumerable<PMProject>) array).Select<PMProject, string>((Func<PMProject, string>) (a => a.ContractCD.Trim())).ToArray<string>()).JoinIntoStringForMessage<string>(this.EntityCountInErrorMessage)
      });
  }

  public virtual void CheckRelatedProformaDoesNotExist(int?[] branchIDs, string message)
  {
    if (branchIDs == null || ((IEnumerable<int?>) branchIDs).IsEmpty<int?>())
      return;
    List<PMProforma> list = GraphHelper.RowCast<PMProforma>((IEnumerable) PXSelectBase<PMProforma, PXSelectReadonly<PMProforma, Where<PMProforma.status, NotEqual<ProformaStatus.closed>, And<Where<PMProforma.branchID, In<Required<PMProforma.branchID>>>>>>.Config>.SelectWindowed(this.Graph, 0, this.EntityCountInErrorMessage + 1, new object[1]
    {
      (object) branchIDs
    })).Select<PMProforma, PMProforma>((Func<PMProforma, PMProforma>) (_ => _)).ToList<PMProforma>();
    if (list.Any<PMProforma>())
      throw new PXSetPropertyException(message, new object[2]
      {
        (object) BranchMaint.FindBranchByID(this.Graph, (int?) branchIDs?[0]).BranchCD.Trim(),
        (object) string.Join(", ", list.Select<PMProforma, string>((Func<PMProforma, string>) (x => x.RefNbr)).ToArray<string>())
      });
  }

  public virtual void ValidateActiveField(
    int?[] branchIDs,
    bool? newValue,
    PX.Objects.GL.DAC.Organization organization)
  {
    this.ValidateActiveField(branchIDs, newValue, organization, false);
  }

  public virtual void ValidateActiveField(
    int?[] branchIDs,
    bool? newValue,
    PX.Objects.GL.DAC.Organization organization,
    bool skipActivateValidation = false)
  {
    if (!newValue.GetValueOrDefault())
    {
      using (new PXReadBranchRestrictedScope())
      {
        string exceptionMessage1;
        string exceptionMessage2;
        if (organization?.OrganizationType == "WithoutBranches")
        {
          exceptionMessage1 = "The {0} company cannot be set as inactive because the following related warehouses exist: {1}.";
          exceptionMessage2 = "The {0} company cannot be set as inactive because the following related fixed assets exist: {1}.";
        }
        else
        {
          exceptionMessage1 = "The {0} branch of the company cannot be set as inactive because the following related warehouses exist: {1}.";
          exceptionMessage2 = "The {0} branch of the company cannot be set as inactive because the following related fixed assets exist: {1}.";
        }
        this.CheckRelatedWarehousesDontExist(branchIDs, exceptionMessage1);
        this.CheckRelatedFixedAssetsDontExist(branchIDs, exceptionMessage2);
        if (organization?.OrganizationType == "Balancing" || organization?.OrganizationType == "NotBalancing")
          this.CheckRelatedBillingRulesDontExist(branchIDs, "The {0} branch cannot be deactivated because it is used in the following billing rules: {1}.");
        this.CheckRelatedProformaDoesNotExist(branchIDs, "The {0} branch cannot be deactivated because it is used in the following pro forma invoices: {1}.");
      }
    }
    if (newValue.GetValueOrDefault() && organization != null && !organization.Active.GetValueOrDefault() && !skipActivateValidation)
      throw new PXSetPropertyException("The branch cannot be activated because its parent company is inactive.");
  }

  public virtual void CheckRelatedFixedAssetsDontExist(int?[] branchIDs, string exceptionMessage)
  {
    if (branchIDs == null || ((IEnumerable<int?>) branchIDs).IsEmpty<int?>())
      return;
    FixedAsset[] array = GraphHelper.RowCast<FixedAsset>((IEnumerable) PXSelectBase<FixedAsset, PXSelectReadonly<FixedAsset, Where<FixedAsset.branchID, In<Required<FixedAsset.branchID>>, And<FixedAsset.status, NotEqual<FixedAssetStatus.reversed>, And<FixedAsset.status, NotEqual<FixedAssetStatus.disposed>>>>>.Config>.SelectWindowed(this.Graph, 0, this.EntityCountInErrorMessage + 1, new object[1]
    {
      (object) branchIDs
    })).ToArray<FixedAsset>();
    if (((IEnumerable<FixedAsset>) array).Any<FixedAsset>())
    {
      IEnumerable<PX.Objects.GL.Branch> branchesById = BranchMaint.FindBranchesByID(this.Graph, ((IEnumerable<FixedAsset>) array).Take<FixedAsset>(this.EntityCountInErrorMessage).Select<FixedAsset, int?>((Func<FixedAsset, int?>) (a => a.BranchID)).ToArray<int?>());
      throw new PXSetPropertyException(exceptionMessage, new object[2]
      {
        (object) ((ICollection<string>) branchesById.Select<PX.Objects.GL.Branch, string>((Func<PX.Objects.GL.Branch, string>) (b => b.BranchCD.Trim())).ToArray<string>()).JoinIntoStringForMessage<string>(),
        (object) ((ICollection<string>) ((IEnumerable<FixedAsset>) array).Select<FixedAsset, string>((Func<FixedAsset, string>) (s => s.AssetCD.Trim())).ToArray<string>()).JoinIntoStringForMessage<string>(this.EntityCountInErrorMessage)
      });
    }
  }

  public virtual void CheckRelatedWarehousesDontExist(int?[] branchIDs, string exceptionMessage)
  {
    if (branchIDs == null || ((IEnumerable<int?>) branchIDs).IsEmpty<int?>())
      return;
    INSite[] array = GraphHelper.RowCast<INSite>((IEnumerable) PXSelectBase<INSite, PXSelectReadonly<INSite, Where<INSite.branchID, In<Required<INSite.branchID>>, And<INSite.active, Equal<True>>>>.Config>.SelectWindowed(this.Graph, 0, this.EntityCountInErrorMessage + 1, new object[1]
    {
      (object) branchIDs
    })).ToArray<INSite>();
    if (((IEnumerable<INSite>) array).Any<INSite>())
    {
      IEnumerable<PX.Objects.GL.Branch> branchesById = BranchMaint.FindBranchesByID(this.Graph, ((IEnumerable<INSite>) array).Take<INSite>(this.EntityCountInErrorMessage).Select<INSite, int?>((Func<INSite, int?>) (a => a.BranchID)).ToArray<int?>());
      throw new PXSetPropertyException(exceptionMessage, new object[2]
      {
        (object) ((ICollection<string>) branchesById.Select<PX.Objects.GL.Branch, string>((Func<PX.Objects.GL.Branch, string>) (b => b.BranchCD.Trim())).ToArray<string>()).JoinIntoStringForMessage<string>(),
        (object) ((ICollection<string>) ((IEnumerable<INSite>) array).Select<INSite, string>((Func<INSite, string>) (s => s.SiteCD.Trim())).ToArray<string>()).JoinIntoStringForMessage<string>(this.EntityCountInErrorMessage)
      });
    }
  }

  public virtual void CheckRelatedBillingRulesDontExist(int?[] branchIDs, string exceptionMessage)
  {
    if (branchIDs == null || ((IEnumerable<int?>) branchIDs).IsEmpty<int?>())
      return;
    PMBillingRule[] array = GraphHelper.RowCast<PMBillingRule>((IEnumerable) PXSelectBase<PMBillingRule, PXSelectReadonly<PMBillingRule, Where<PMBillingRule.targetBranchID, In<Required<PMBillingRule.targetBranchID>>>>.Config>.SelectWindowed(this.Graph, 0, this.EntityCountInErrorMessage + 1, new object[1]
    {
      (object) branchIDs
    })).ToArray<PMBillingRule>();
    if (((IEnumerable<PMBillingRule>) array).Any<PMBillingRule>())
    {
      IEnumerable<PX.Objects.GL.Branch> branchesById = BranchMaint.FindBranchesByID(this.Graph, ((IEnumerable<PMBillingRule>) array).Take<PMBillingRule>(this.EntityCountInErrorMessage).Select<PMBillingRule, int?>((Func<PMBillingRule, int?>) (a => a.TargetBranchID)).ToArray<int?>());
      throw new PXSetPropertyException(exceptionMessage, new object[2]
      {
        (object) ((ICollection<string>) branchesById.Select<PX.Objects.GL.Branch, string>((Func<PX.Objects.GL.Branch, string>) (b => b.BranchCD.Trim())).ToArray<string>()).JoinIntoStringForMessage<string>(),
        (object) ((ICollection<string>) ((IEnumerable<PMBillingRule>) array).Select<PMBillingRule, string>((Func<PMBillingRule, string>) (s => s.BillingID.Trim())).ToArray<string>()).JoinIntoStringForMessage<string>(this.EntityCountInErrorMessage)
      });
    }
  }

  public virtual void CheckExtendedBranch(int?[] branchBAccountIDs)
  {
    if (branchBAccountIDs == null || ((IEnumerable<int?>) branchBAccountIDs).IsEmpty<int?>())
      return;
    PX.Objects.CR.BAccount baccount = PXResultset<PX.Objects.CR.BAccount>.op_Implicit(PXSelectBase<PX.Objects.CR.BAccount, PXViewOf<PX.Objects.CR.BAccount>.BasedOn<SelectFromBase<PX.Objects.CR.BAccount, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.CR.BAccount.bAccountID, In<P.AsInt>>>>, And<BqlOperand<PX.Objects.CR.BAccount.isBranch, IBqlBool>.IsEqual<True>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.CR.BAccount.type, Equal<BAccountType.customerType>>>>>.Or<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.CR.BAccount.type, Equal<BAccountType.vendorType>>>>>.Or<BqlOperand<PX.Objects.CR.BAccount.type, IBqlString>.IsEqual<BAccountType.combinedType>>>>>>.ReadOnly.Config>.SelectSingleBound(this.Graph, (object[]) null, new object[1]
    {
      (object) branchBAccountIDs
    }));
    if (baccount != null)
      throw new PXSetPropertyException("The {0} entity cannot be deleted because it has been extended to customer or vendor.", new object[1]
      {
        (object) baccount.AcctCD.Trim()
      });
  }
}
