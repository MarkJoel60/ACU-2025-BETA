// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.APSetupMaint
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Caching;
using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Metadata;
using PX.Objects.CM;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.EP;
using PX.Objects.GL;
using System;
using System.Collections;
using System.Linq;

#nullable enable
namespace PX.Objects.AP;

public class APSetupMaint : PXGraph<
#nullable disable
APSetupMaint>
{
  public PXSave<APSetup> Save;
  public PXCancel<APSetup> Cancel;
  public PXSelect<APSetup> Setup;
  public PXSelect<APSetupApproval> SetupApproval;
  public PXSelectOrderBy<AP1099Box, PX.Data.OrderBy<BqlField<
  #nullable enable
  AP1099Box.boxCD, IBqlString>.Asc>> Boxes1099;
  public 
  #nullable disable
  PXSelect<PX.Objects.GL.Account, Where<PX.Objects.GL.Account.accountID, Equal<Required<PX.Objects.GL.Account.accountID>>>> Account_AccountID;
  public PXSelect<PX.Objects.GL.Account, Where<PX.Objects.GL.Account.box1099, Equal<Required<PX.Objects.GL.Account.box1099>>>> Account_Box1099;
  public CRNotificationSetupList<APNotification> Notifications;
  public PXSelect<NotificationSetupRecipient, Where<NotificationSetupRecipient.setupID, Equal<Current<APNotification.setupID>>>> Recipients;
  public PXAction<APSetup> viewAssignmentMap;
  public CMSetupSelect CMSetup;
  public PXSetup<PX.Objects.GL.Company> Company;
  public PXSetup<PX.Objects.GL.GLSetup> GLSetup;

  [PXDBString(10)]
  [PXDefault]
  [VendorContactType.ClassList]
  [PXUIField(DisplayName = "Contact Type")]
  [PXCheckDistinct(new System.Type[] {typeof (NotificationSetupRecipient.contactID)}, Where = typeof (Where<NotificationSetupRecipient.setupID, Equal<Current<NotificationSetupRecipient.setupID>>>))]
  public virtual void NotificationSetupRecipient_ContactType_CacheAttached(PXCache sender)
  {
  }

  [PXDBInt]
  [PXUIField(DisplayName = "Contact ID")]
  [PXNotificationContactSelector(typeof (NotificationSetupRecipient.contactType), typeof (Search2<PX.Objects.CR.Contact.contactID, LeftJoin<PX.Objects.EP.EPEmployee, On<PX.Objects.EP.EPEmployee.parentBAccountID, Equal<PX.Objects.CR.Contact.bAccountID>, And<PX.Objects.EP.EPEmployee.defContactID, Equal<PX.Objects.CR.Contact.contactID>>>>, Where<Current<NotificationSetupRecipient.contactType>, Equal<NotificationContactType.employee>, And<PX.Objects.EP.EPEmployee.acctCD, PX.Data.IsNotNull>>>))]
  public virtual void NotificationSetupRecipient_ContactID_CacheAttached(PXCache sender)
  {
  }

  [Box1099NumberSelector]
  [PXMergeAttributes(Method = MergeMethod.Append)]
  public virtual void AP1099Box_BoxNbr_CacheAttached(PXCache sender)
  {
  }

  [PXUIField(MapEnableRights = PXCacheRights.Select, MapViewRights = PXCacheRights.Select)]
  public virtual IEnumerable ViewAssignmentMap(PXAdapter adapter)
  {
    if (this.SetupApproval.Current != null)
    {
      EPAssignmentMap row = (EPAssignmentMap) PXSelectBase<EPAssignmentMap, PXSelect<EPAssignmentMap, Where<EPAssignmentMap.assignmentMapID, Equal<Required<EPAssignmentMap.assignmentMapID>>>>.Config>.Select((PXGraph) this, (object) this.SetupApproval.Current.AssignmentMapID).First<PXResult<EPAssignmentMap>>();
      int? nullable = row.MapType;
      PXGraph instance;
      if (nullable.GetValueOrDefault() == 2)
      {
        instance = (PXGraph) PXGraph.CreateInstance<EPApprovalMapMaint>();
      }
      else
      {
        nullable = row.MapType;
        if (nullable.GetValueOrDefault() == 1)
        {
          instance = (PXGraph) PXGraph.CreateInstance<EPAssignmentMapMaint>();
        }
        else
        {
          nullable = row.MapType;
          int num1 = 0;
          if (nullable.GetValueOrDefault() == num1 & nullable.HasValue)
          {
            nullable = row.AssignmentMapID;
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
      PXRedirectHelper.TryRedirect(instance, (object) row, PXRedirectHelper.WindowMode.NewWindow);
    }
    return adapter.Get();
  }

  public APSetupMaint()
  {
    PX.Objects.GL.GLSetup current = this.GLSetup.Current;
    this.Boxes1099.Cache.AllowDelete = false;
    this.Boxes1099.Cache.AllowInsert = false;
    this.Boxes1099.Cache.AllowUpdate = true;
    PXUIFieldAttribute.SetVisible<APSetup.applyQuantityDiscountBy>(this.Setup.Cache, (object) null, PXAccess.FeatureInstalled<PX.Objects.CS.FeaturesSet.vendorDiscounts>() && PXAccess.FeatureInstalled<PX.Objects.CS.FeaturesSet.multipleUnitMeasure>());
  }

  protected IEnumerable boxes1099()
  {
    APSetupMaint graph = this;
    foreach (PXResult<AP1099Box> pxResult in PXSelectBase<AP1099Box, PXViewOf<AP1099Box>.BasedOn<SelectFromBase<AP1099Box, TypeArrayOf<IFbqlJoin>.Empty>.Order<By<BqlField<AP1099Box.boxCD, IBqlString>.Asc>>>.Config>.Select((PXGraph) graph))
    {
      AP1099Box ap1099Box = (AP1099Box) pxResult;
      if (ap1099Box.OldAccountID.HasValue)
      {
        yield return (object) ap1099Box;
      }
      else
      {
        PX.Objects.GL.Account account = (PX.Objects.GL.Account) graph.Account_Box1099.Select((object) ap1099Box.BoxNbr);
        if (account != null)
        {
          ap1099Box.AccountID = (int?) account?.AccountID;
          ap1099Box.OldAccountID = (int?) account?.AccountID;
        }
        yield return (object) ap1099Box;
      }
    }
  }

  protected virtual void AP1099Box_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    AP1099Box row = (AP1099Box) e.Row;
    if (row == null || row.OldAccountID.HasValue)
      return;
    PX.Objects.GL.Account account = (PX.Objects.GL.Account) this.Account_Box1099.Select((object) row.BoxNbr);
    if (account == null)
      return;
    row.AccountID = account.AccountID;
    row.OldAccountID = account.AccountID;
  }

  protected virtual void AP1099Box_RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    if ((e.Operation & PXDBOperation.Delete) != PXDBOperation.Update)
    {
      e.Cancel = true;
    }
    else
    {
      foreach (AP1099Box ap1099Box in this.Boxes1099.Cache.Updated)
      {
        int? nullable1 = ap1099Box.OldAccountID;
        int? nullable2;
        if (nullable1.HasValue)
        {
          nullable1 = ap1099Box.AccountID;
          if (nullable1.HasValue)
          {
            nullable1 = ap1099Box.OldAccountID;
            nullable2 = ap1099Box.AccountID;
            if (nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue)
              goto label_9;
          }
          PX.Objects.GL.Account account = (PX.Objects.GL.Account) this.Account_AccountID.Select((object) ap1099Box.OldAccountID);
          if (account != null)
          {
            account.Box1099 = new short?();
            this.Account_AccountID.Cache.Update((object) account);
          }
        }
label_9:
        nullable2 = ap1099Box.AccountID;
        if (nullable2.HasValue)
        {
          nullable2 = ap1099Box.OldAccountID;
          if (nullable2.HasValue)
          {
            nullable2 = ap1099Box.OldAccountID;
            nullable1 = ap1099Box.AccountID;
            if (nullable2.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable2.HasValue == nullable1.HasValue)
              continue;
          }
          PX.Objects.GL.Account account = (PX.Objects.GL.Account) this.Account_AccountID.Select((object) ap1099Box.AccountID);
          if (account != null)
          {
            account.Box1099 = ap1099Box.BoxNbr;
            this.Account_AccountID.Cache.Update((object) account);
          }
        }
      }
    }
  }

  protected virtual void APSetup_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    APSetup row = (APSetup) e.Row;
    if (e.Row == null)
      return;
    PXUIFieldAttribute.SetEnabled<APSetup.invoicePrecision>(sender, (object) row, row.InvoiceRounding != "N");
    PXUIFieldAttribute.SetEnabled<APSetup.numberOfMonths>(sender, (object) row, row.RetentionType == "F");
    this.VerifyInvoiceRounding(sender, row);
    if (!PXAccess.FeatureInstalled<PX.Objects.CS.FeaturesSet.distributionModule>() || PXAccess.FeatureInstalled<PX.Objects.CS.FeaturesSet.inventory>())
      return;
    PXDefaultAttribute.SetDefault<APSetup.vendorPriceUpdate>(sender, (object) "P");
    PXStringListAttribute.SetList<APSetup.vendorPriceUpdate>(sender, (object) null, new string[3]
    {
      "N",
      "P",
      "B"
    }, new string[3]
    {
      "None",
      "On PO Entry",
      "On AP Bill Release"
    });
  }

  protected virtual void APSetup_RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    if (!(e.Row is APSetup row) || row == null || sender.ObjectsEqual<APSetup.retentionType>(e.Row, e.OldRow) && sender.ObjectsEqual<APSetup.numberOfMonths>(e.Row, e.OldRow))
      return;
    if (row.RetentionType == "L")
      sender.RaiseExceptionHandling<APSetup.retentionType>(e.Row, (object) ((APSetup) e.Row).RetentionType, (Exception) new PXSetPropertyException("The system retains the last price and the current price for each item.", PXErrorLevel.Warning));
    if (!(row.RetentionType == "F"))
      return;
    int? numberOfMonths = row.NumberOfMonths;
    int num1 = 0;
    if (!(numberOfMonths.GetValueOrDefault() == num1 & numberOfMonths.HasValue))
      sender.RaiseExceptionHandling<APSetup.retentionType>(e.Row, (object) ((APSetup) e.Row).RetentionType, (Exception) new PXSetPropertyException("The system retains changes of the price records during {0} months.", PXErrorLevel.Warning, new object[1]
      {
        (object) row.NumberOfMonths
      }));
    numberOfMonths = row.NumberOfMonths;
    int num2 = 0;
    if (!(numberOfMonths.GetValueOrDefault() == num2 & numberOfMonths.HasValue))
      return;
    sender.RaiseExceptionHandling<APSetup.retentionType>(e.Row, (object) ((APSetup) e.Row).RetentionType, (Exception) new PXSetPropertyException("The system retains changes of the price records for an unlimited period.", PXErrorLevel.Warning, new object[1]
    {
      (object) row.NumberOfMonths
    }));
  }

  protected virtual void _(PX.Data.Events.RowPersisting<APSetup> e)
  {
    APSetup row = e.Row;
    if (row == null || PXAccess.FeatureInstalled<PX.Objects.CS.FeaturesSet.vATRecognitionOnPrepaymentsAP>())
      return;
    PXDefaultAttribute.SetPersistingCheck<APSetup.prepaymentInvoiceNumberingID>(e.Cache, (object) row, PXPersistingCheck.Nothing);
  }

  private void VerifyInvoiceRounding(PXCache sender, APSetup row)
  {
    bool flag = false;
    if (row.InvoiceRounding != "N" && !PXAccess.FeatureInstalled<PX.Objects.CS.FeaturesSet.multipleBaseCurrencies>())
    {
      Decimal? roundingLimit = CurrencyCollection.GetCurrency(this.Company.Current.BaseCuryID).RoundingLimit;
      Decimal num = 0M;
      if (roundingLimit.GetValueOrDefault() == num & roundingLimit.HasValue)
      {
        flag = true;
        sender.RaiseExceptionHandling<APSetup.invoiceRounding>((object) row, (object) null, (Exception) new PXSetPropertyException("To use this rounding rule, specify Rounding Limit for the base currency on the Currencies (CM202000) form.", PXErrorLevel.Warning));
      }
    }
    if (flag)
      return;
    sender.RaiseExceptionHandling<APSetup.invoiceRounding>((object) row, (object) null, (Exception) null);
  }

  protected virtual void APSetup_MigrationMode_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    if (!(e.Row is APSetup row))
      return;
    bool? oldValue = (bool?) e.OldValue;
    bool? migrationMode = row.MigrationMode;
    if (migrationMode.GetValueOrDefault() && !oldValue.GetValueOrDefault())
    {
      if ((PX.Objects.GL.GLTran) PXSelectBase<PX.Objects.GL.GLTran, PXSelect<PX.Objects.GL.GLTran, Where<PX.Objects.GL.GLTran.module, Equal<BatchModule.moduleAP>>>.Config>.SelectSingleBound((PXGraph) this, (object[]) null) == null)
        return;
      sender.RaiseExceptionHandling<APSetup.migrationMode>((object) row, (object) row.MigrationMode, (Exception) new PXSetPropertyException("The General Ledger batches generated in the module exist in the system.", PXErrorLevel.Warning));
    }
    else
    {
      migrationMode = row.MigrationMode;
      if (migrationMode.GetValueOrDefault() || !oldValue.GetValueOrDefault() || (APRegister) PXSelectBase<APRegister, PXSelect<APRegister, Where<APRegister.released, NotEqual<True>, And<APRegister.isMigratedRecord, Equal<True>>>>.Config>.SelectSingleBound((PXGraph) this, (object[]) null) == null)
        return;
      sender.RaiseExceptionHandling<APSetup.migrationMode>((object) row, (object) row.MigrationMode, (Exception) new PXSetPropertyException("Migrated documents that have not been released exist in the system.", PXErrorLevel.Warning));
    }
  }

  protected virtual void APSetupApproval_DocType_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    if (!(e.Row is APSetupApproval row))
      return;
    sender.SetDefaultExt<APSetupApproval.assignmentMapID>((object) row);
    sender.SetDefaultExt<APSetupApproval.assignmentNotificationID>((object) row);
  }

  public override void Persist()
  {
    base.Persist();
    this.PageCacheControl.InvalidateCache();
    this.ScreenInfoCacheControl.InvalidateCache();
  }

  [InjectDependency]
  protected ICacheControl<PageCache> PageCacheControl { get; set; }

  [InjectDependency]
  protected IScreenInfoCacheControl ScreenInfoCacheControl { get; set; }
}
