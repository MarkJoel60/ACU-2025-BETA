// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.GLSetupMaint
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.EP;
using PX.Objects.PM;
using System;
using System.Collections;
using System.Linq;

#nullable disable
namespace PX.Objects.GL;

public class GLSetupMaint : PXGraph<GLSetupMaint>
{
  public PXSelect<GLSetup> GLSetupRecord;
  public PXSave<GLSetup> Save;
  public PXCancel<GLSetup> Cancel;
  public PXSetup<Company> company;
  public PXSelect<GLSetupApproval> SetupApproval;
  public PXAction<GLSetup> viewAssignmentMap;

  public GLSetupMaint()
  {
    if (string.IsNullOrEmpty(((PXSelectBase<Company>) this.company).Current.BaseCuryID))
      throw new PXSetupNotEnteredException("The required configuration data is not entered on the {0} form.", typeof (Company), new object[1]
      {
        (object) PXMessages.LocalizeNoPrefix("Companies")
      });
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable ViewAssignmentMap(PXAdapter adapter)
  {
    if (((PXSelectBase<GLSetupApproval>) this.SetupApproval).Current != null)
    {
      EPAssignmentMap epAssignmentMap = PXResult<EPAssignmentMap>.op_Implicit(((IQueryable<PXResult<EPAssignmentMap>>) PXSelectBase<EPAssignmentMap, PXSelect<EPAssignmentMap, Where<EPAssignmentMap.assignmentMapID, Equal<Required<EPAssignmentMap.assignmentMapID>>>>.Config>.Select((PXGraph) this, new object[1]
      {
        (object) ((PXSelectBase<GLSetupApproval>) this.SetupApproval).Current.AssignmentMapID
      })).First<PXResult<EPAssignmentMap>>());
      int? nullable = epAssignmentMap.MapType;
      PXGraph instance;
      if (nullable.GetValueOrDefault() == 2)
      {
        instance = (PXGraph) PXGraph.CreateInstance<EPApprovalMapMaint>();
      }
      else
      {
        nullable = epAssignmentMap.MapType;
        if (nullable.GetValueOrDefault() == 1)
        {
          instance = (PXGraph) PXGraph.CreateInstance<EPAssignmentMapMaint>();
        }
        else
        {
          nullable = epAssignmentMap.MapType;
          int num1 = 0;
          if (nullable.GetValueOrDefault() == num1 & nullable.HasValue)
          {
            nullable = epAssignmentMap.AssignmentMapID;
            int num2 = 0;
            if (nullable.GetValueOrDefault() > num2 & nullable.HasValue)
            {
              instance = (PXGraph) PXGraph.CreateInstance<EPAssignmentMaint>();
              goto label_9;
            }
          }
          instance = (PXGraph) PXGraph.CreateInstance<EPAssignmentAndApprovalMapEnq>();
        }
      }
label_9:
      PXRedirectHelper.TryRedirect(instance, (object) epAssignmentMap, (PXRedirectHelper.WindowMode) 3);
    }
    return adapter.Get();
  }

  protected virtual void GLSetup_BegFiscalYear_FieldDefaulting(
    PXCache cache,
    PXFieldDefaultingEventArgs e)
  {
    e.NewValue = (object) ((PXGraph) this).Accessinfo.BusinessDate;
  }

  protected virtual void GLSetup_RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    GLSetup glSetup = PXResultset<GLSetup>.op_Implicit(PXSelectBase<GLSetup, PXSelectReadonly<GLSetup>.Config>.Select((PXGraph) this, Array.Empty<object>()));
    GLSetup row = (GLSetup) e.Row;
    short? coaOrder;
    int? nullable1;
    if (glSetup != null)
    {
      coaOrder = glSetup.COAOrder;
      int? nullable2 = coaOrder.HasValue ? new int?((int) coaOrder.GetValueOrDefault()) : new int?();
      coaOrder = row.COAOrder;
      nullable1 = coaOrder.HasValue ? new int?((int) coaOrder.GetValueOrDefault()) : new int?();
      if (nullable2.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable2.HasValue == nullable1.HasValue)
        return;
    }
    coaOrder = row.COAOrder;
    nullable1 = coaOrder.HasValue ? new int?((int) coaOrder.GetValueOrDefault()) : new int?();
    int num = 4;
    if (!(nullable1.GetValueOrDefault() < num & nullable1.HasValue))
      return;
    for (short index1 = 0; index1 < (short) 4; ++index1)
    {
      PXDataFieldParam[] pxDataFieldParamArray1 = new PXDataFieldParam[2];
      string[] coaOrderOptions1 = AccountType.COAOrderOptions;
      coaOrder = row.COAOrder;
      int index2 = (int) coaOrder.Value;
      pxDataFieldParamArray1[0] = (PXDataFieldParam) new PXDataFieldAssign("COAOrder", (object) Convert.ToInt32(coaOrderOptions1[index2].Substring((int) index1, 1)));
      pxDataFieldParamArray1[1] = (PXDataFieldParam) new PXDataFieldRestrict("Type", (object) AccountType.Literal(index1));
      PXDatabase.Update<Account>(pxDataFieldParamArray1);
      PXDataFieldParam[] pxDataFieldParamArray2 = new PXDataFieldParam[2];
      string name = typeof (PMAccountGroup.sortOrder).Name;
      string[] coaOrderOptions2 = AccountType.COAOrderOptions;
      coaOrder = row.COAOrder;
      int index3 = (int) coaOrder.Value;
      // ISSUE: variable of a boxed type
      __Boxed<int> int32 = (ValueType) Convert.ToInt32(coaOrderOptions2[index3].Substring((int) index1, 1));
      pxDataFieldParamArray2[0] = (PXDataFieldParam) new PXDataFieldAssign(name, (object) int32);
      pxDataFieldParamArray2[1] = (PXDataFieldParam) new PXDataFieldRestrict(typeof (PMAccountGroup.type).Name, (object) AccountType.Literal(index1));
      PXDatabase.Update<PMAccountGroup>(pxDataFieldParamArray2);
    }
  }

  protected virtual void GLSetup_RowSelected(PXCache cache, PXRowSelectedEventArgs e)
  {
    if (e.Row == null)
      return;
    GLSetup row = e.Row as GLSetup;
    bool flag1 = GLUtility.IsAccountHistoryExist((PXGraph) this, row.YtdNetIncAccountID);
    PXUIFieldAttribute.SetEnabled<GLSetup.ytdNetIncAccountID>(((PXSelectBase) this.GLSetupRecord).Cache, (object) row, !flag1);
    bool flag2 = GLUtility.IsAccountHistoryExist((PXGraph) this, row.RetEarnAccountID);
    PXUIFieldAttribute.SetEnabled<GLSetup.retEarnAccountID>(((PXSelectBase) this.GLSetupRecord).Cache, (object) row, !flag2);
  }

  protected virtual void GLSetup_AutoReleaseReclassBatch_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    if (!(bool) e.NewValue)
      return;
    foreach (PXResult<GLSetupApproval> pxResult in ((PXSelectBase<GLSetupApproval>) this.SetupApproval).Select(Array.Empty<object>()))
    {
      GLSetupApproval glSetupApproval = PXResult<GLSetupApproval>.op_Implicit(pxResult);
      if (glSetupApproval.BatchType == "RCL" && glSetupApproval.IsActive.GetValueOrDefault())
        throw new PXSetPropertyException("The Automatically Release Reclassification Batches check box cannot be selected if an active approval map is set up for the Reclassification batch type.");
    }
  }

  protected virtual void GLSetupApproval_RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    GLSetupApproval row = (GLSetupApproval) e.Row;
    if (row.IsActive.GetValueOrDefault() && row.BatchType == "RCL" && ((PXSelectBase<GLSetup>) this.GLSetupRecord).Current.AutoReleaseReclassBatch.GetValueOrDefault())
    {
      ((PXSelectBase) this.GLSetupRecord).Cache.RaiseExceptionHandling<GLSetup.autoReleaseReclassBatch>((object) ((PXSelectBase<GLSetup>) this.GLSetupRecord).Current, (object) ((PXSelectBase<GLSetup>) this.GLSetupRecord).Current.AutoReleaseReclassBatch, (Exception) new PXSetPropertyException("The Automatically Release Reclassification Batches check box cannot be selected if an active approval map is set up for the Reclassification batch type.", (PXErrorLevel) 4));
      throw new PXSetPropertyException("The Automatically Release Reclassification Batches check box cannot be selected if an active approval map is set up for the Reclassification batch type.");
    }
  }

  protected virtual void GLSetup_YtdNetIncAccountID_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    GLSetup row = (GLSetup) e.Row;
    if (row == null || e.NewValue == null)
      return;
    Account account1 = PXResultset<Account>.op_Implicit(PXSelectBase<Account, PXSelect<Account, Where<Account.accountID, Equal<Required<GLSetup.ytdNetIncAccountID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      e.NewValue
    }));
    int? newValue = (int?) e.NewValue;
    int? retEarnAccountId = row.RetEarnAccountID;
    if (newValue.GetValueOrDefault() == retEarnAccountId.GetValueOrDefault() & newValue.HasValue == retEarnAccountId.HasValue)
    {
      Account account2 = PXResultset<Account>.op_Implicit(PXSelectBase<Account, PXSelect<Account, Where<Account.accountID, Equal<Current<GLSetup.ytdNetIncAccountID>>>>.Config>.SelectSingleBound((PXGraph) this, new object[1]
      {
        (object) row
      }, Array.Empty<object>()));
      Account account3 = PXResultset<Account>.op_Implicit(PXSelectBase<Account, PXSelect<Account, Where<Account.accountID, Equal<Current<GLSetup.retEarnAccountID>>>>.Config>.SelectSingleBound((PXGraph) this, new object[1]
      {
        (object) row
      }, Array.Empty<object>()));
      e.NewValue = account2 == null ? (object) (string) null : (object) account2.AccountCD;
      throw new PXSetPropertyException("Incorrect value. The value to be entered must not be equal to {0}.", new object[1]
      {
        (object) account3.AccountCD
      });
    }
    if (account1.GLConsolAccountCD != null)
      throw new PXSetPropertyException("The account cannot be specified as the YTD Net Income account because a consolidation account is specified for this account on the Chart of Accounts (GL202500) form.");
    if (GLUtility.IsAccountHistoryExist((PXGraph) this, (int?) e.NewValue) || GLUtility.IsAccountGLTranExist((PXGraph) this, (int?) e.NewValue))
    {
      e.NewValue = account1 == null ? (object) (string) null : (object) account1.AccountCD;
      throw new PXSetPropertyException("You cannot select this GL account, because it is used in transactions. Select another account.");
    }
  }

  protected virtual void GLSetup_RetEarnAccountID_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    GLSetup row = (GLSetup) e.Row;
    if (row == null)
      return;
    if (e.NewValue != null)
    {
      int? newValue = (int?) e.NewValue;
      int? ytdNetIncAccountId = row.YtdNetIncAccountID;
      if (newValue.GetValueOrDefault() == ytdNetIncAccountId.GetValueOrDefault() & newValue.HasValue == ytdNetIncAccountId.HasValue)
      {
        Account account1 = PXResultset<Account>.op_Implicit(PXSelectBase<Account, PXSelect<Account, Where<Account.accountID, Equal<Current<GLSetup.ytdNetIncAccountID>>>>.Config>.SelectSingleBound((PXGraph) this, new object[1]
        {
          (object) row
        }, Array.Empty<object>()));
        Account account2 = PXResultset<Account>.op_Implicit(PXSelectBase<Account, PXSelect<Account, Where<Account.accountID, Equal<Current<GLSetup.retEarnAccountID>>>>.Config>.SelectSingleBound((PXGraph) this, new object[1]
        {
          (object) row
        }, Array.Empty<object>()));
        e.NewValue = account2 == null ? (object) (string) null : (object) account2.AccountCD;
        throw new PXSetPropertyException("Incorrect value. The value to be entered must not be equal to {0}.", new object[1]
        {
          (object) account1.AccountCD
        });
      }
    }
    if (e.NewValue == null || !GLUtility.IsAccountHistoryExist((PXGraph) this, (int?) e.NewValue))
      return;
    sender.RaiseExceptionHandling<GLSetup.retEarnAccountID>(e.Row, (object) null, (Exception) new PXSetPropertyException("You cannot select this GL account, because it is used in transactions. Select another account.", (PXErrorLevel) 2));
  }

  public virtual void Persist() => ((PXGraph) this).Persist();
}
