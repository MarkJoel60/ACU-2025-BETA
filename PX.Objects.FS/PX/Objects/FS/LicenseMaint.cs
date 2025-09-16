// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.LicenseMaint
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.FS;

public class LicenseMaint : PXGraph<LicenseMaint, FSLicense>
{
  [PXHidden]
  public PXSelect<BAccountStaffMember> DummyView_BAccountStaffMember;
  [PXHidden]
  public PXSelect<BAccountSelectorBase> DummyView_BAccountSelectorBase;
  [PXHidden]
  public PXSelect<PX.Objects.CR.Contact> Contacts;
  [PXHidden]
  public PXSetup<FSSetup> SetupRecord;
  public PXSelect<FSLicense> LicenseRecords;

  [PXMergeAttributes]
  [PXSelector(typeof (FSLicense.refNbr))]
  protected virtual void _(PX.Data.Events.CacheAttached<FSLicense.refNbr> e)
  {
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<FSLicense, FSLicense.expirationDate> e)
  {
    FSLicense row = e.Row;
    if (row == null)
      return;
    DateTime? handlingDateTime = SharedFunctions.TryParseHandlingDateTime(((PX.Data.Events.Event<PXFieldVerifyingEventArgs, PX.Data.Events.FieldVerifying<FSLicense, FSLicense.expirationDate>>) e).Cache, ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<FSLicense, FSLicense.expirationDate>, FSLicense, object>) e).NewValue);
    if (!handlingDateTime.HasValue)
      return;
    DateTime? nullable = handlingDateTime;
    DateTime? issueDate = row.IssueDate;
    if ((nullable.HasValue & issueDate.HasValue ? (nullable.GetValueOrDefault() < issueDate.GetValueOrDefault() ? 1 : 0) : 0) == 0)
      return;
    issueDate = row.IssueDate;
    if (!issueDate.HasValue)
      return;
    ((PX.Data.Events.Event<PXFieldVerifyingEventArgs, PX.Data.Events.FieldVerifying<FSLicense, FSLicense.expirationDate>>) e).Cache.RaiseExceptionHandling<FSLicense.expirationDate>((object) row, (object) null, (Exception) new PXSetPropertyException("The issue date must be earlier than the expiration date."));
    ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<FSLicense, FSLicense.expirationDate>>) e).Cancel = true;
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<FSLicense, FSLicense.issueDate> e)
  {
    FSLicense row = e.Row;
    if (row == null)
      return;
    DateTime? handlingDateTime = SharedFunctions.TryParseHandlingDateTime(((PX.Data.Events.Event<PXFieldVerifyingEventArgs, PX.Data.Events.FieldVerifying<FSLicense, FSLicense.issueDate>>) e).Cache, ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<FSLicense, FSLicense.issueDate>, FSLicense, object>) e).NewValue);
    if (!handlingDateTime.HasValue || !row.ExpirationDate.HasValue)
      return;
    DateTime? expirationDate = row.ExpirationDate;
    DateTime? nullable = handlingDateTime;
    if ((expirationDate.HasValue & nullable.HasValue ? (expirationDate.GetValueOrDefault() < nullable.GetValueOrDefault() ? 1 : 0) : 0) == 0)
      return;
    ((PX.Data.Events.Event<PXFieldVerifyingEventArgs, PX.Data.Events.FieldVerifying<FSLicense, FSLicense.issueDate>>) e).Cache.RaiseExceptionHandling<FSLicense.issueDate>((object) row, (object) null, (Exception) new PXSetPropertyException("The issue date must be earlier than the expiration date."));
    ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<FSLicense, FSLicense.issueDate>>) e).Cancel = true;
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<FSLicense, FSLicense.neverExpires> e)
  {
    FSLicense row = e.Row;
    if (row == null || !row.NeverExpires.GetValueOrDefault())
      return;
    row.ExpirationDate = new DateTime?();
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<FSLicense, FSLicense.licenseTypeID> e)
  {
    FSLicense row = e.Row;
    if (row == null || !row.LicenseTypeID.HasValue)
      return;
    FSLicenseType fsLicenseType = PXResultset<FSLicenseType>.op_Implicit(PXSelectBase<FSLicenseType, PXSelect<FSLicenseType, Where<FSLicenseType.licenseTypeID, Equal<Required<FSLicenseType.licenseTypeID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) row.LicenseTypeID
    }));
    if (fsLicenseType == null)
      return;
    row.Descr = fsLicenseType.Descr;
  }

  protected virtual void _(PX.Data.Events.RowSelecting<FSLicense> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowSelected<FSLicense> e)
  {
    FSLicense row = e.Row;
    if (row == null)
      return;
    PXCache cache1 = ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<FSLicense>>) e).Cache;
    FSLicense fsLicense1 = row;
    bool? neverExpires = row.NeverExpires;
    int num1;
    if (!neverExpires.HasValue)
    {
      num1 = 1;
    }
    else
    {
      neverExpires = row.NeverExpires;
      num1 = (neverExpires.HasValue ? new bool?(!neverExpires.GetValueOrDefault()) : new bool?()).Value ? 1 : 0;
    }
    PXUIFieldAttribute.SetEnabled<FSLicense.expirationDate>(cache1, (object) fsLicense1, num1 != 0);
    PXCache cache2 = ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<FSLicense>>) e).Cache;
    FSLicense fsLicense2 = row;
    neverExpires = row.NeverExpires;
    int num2;
    if (neverExpires.HasValue)
    {
      neverExpires = row.NeverExpires;
      if (neverExpires.GetValueOrDefault())
      {
        num2 = 2;
        goto label_8;
      }
    }
    num2 = 1;
label_8:
    PXDefaultAttribute.SetPersistingCheck<FSLicense.expirationDate>(cache2, (object) fsLicense2, (PXPersistingCheck) num2);
  }

  protected virtual void _(PX.Data.Events.RowInserting<FSLicense> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowInserted<FSLicense> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowUpdating<FSLicense> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowUpdated<FSLicense> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowDeleting<FSLicense> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowDeleted<FSLicense> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowPersisting<FSLicense> e)
  {
    if (e.Row == null)
      return;
    FSLicense row = e.Row;
    if (!string.IsNullOrEmpty(PXResultset<FSSetup>.op_Implicit(((PXSelectBase<FSSetup>) this.SetupRecord).Select(Array.Empty<object>())).LicenseNumberingID))
      return;
    ((PXSelectBase) this.LicenseRecords).Cache.RaiseExceptionHandling<FSLicense.refNbr>((object) row, (object) row.RefNbr, (Exception) new PXSetPropertyException("The license numbering sequence has not been specified. Specify it in the License Numbering Sequence box on the {0} form.", (PXErrorLevel) 4, new object[1]
    {
      (object) DACHelper.GetDisplayName(typeof (FSSetup))
    }));
  }

  protected virtual void _(PX.Data.Events.RowPersisted<FSLicense> e)
  {
  }
}
