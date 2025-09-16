// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.OrganizationLedgerLinkMaintBase`2
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.Common.Extensions;
using PX.Objects.CS;
using PX.Objects.GL.DAC;
using System;
using System.Collections;
using System.ComponentModel;

#nullable disable
namespace PX.Objects.GL;

public abstract class OrganizationLedgerLinkMaintBase<TGraph, TPrimaryDAC> : PXGraphExtension<TGraph>
  where TGraph : PXGraph
  where TPrimaryDAC : class, IBqlTable, new()
{
  public PXAction<TPrimaryDAC> DeleteOrganizationLedgerLink;

  public abstract PXSelectBase<OrganizationLedgerLink> OrganizationLedgerLinkSelect { get; }

  public abstract PXSelectBase<PX.Objects.GL.DAC.Organization> OrganizationViewBase { get; }

  public abstract PXSelectBase<Ledger> LedgerViewBase { get; }

  protected PXCache<OrganizationLedgerLink> LinkCache
  {
    get
    {
      return (PXCache<OrganizationLedgerLink>) ((PXSelectBase) this.OrganizationLedgerLinkSelect).Cache;
    }
  }

  protected abstract Type VisibleField { get; }

  public virtual void Initialize()
  {
    ((PXGraphExtension) this).Initialize();
    PXAction<TPrimaryDAC> organizationLedgerLink = this.DeleteOrganizationLedgerLink;
    OrganizationLedgerLinkMaintBase<TGraph, TPrimaryDAC> ledgerLinkMaintBase = this;
    // ISSUE: virtual method pointer
    PXFieldSelecting pxFieldSelecting = new PXFieldSelecting((object) ledgerLinkMaintBase, __vmethodptr(ledgerLinkMaintBase, DeleteButtonFieldSelectingHandler));
    ((PXAction) organizationLedgerLink).StateSelectingEvents += pxFieldSelecting;
  }

  protected virtual void OrganizationLedgerLink_RowDeleting(PXCache cache, PXRowDeletingEventArgs e)
  {
    OrganizationLedgerLink row = e.Row as OrganizationLedgerLink;
    PXSetPropertyException propertyException = this.CanBeLinkDeleted(row);
    if (propertyException == null)
      return;
    cache.RaiseExceptionHandling(this.VisibleField.Name, (object) row, cache.GetValueExt((object) row, this.VisibleField.Name), (Exception) propertyException);
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void OrganizationLedgerLink_RowPersisting(
    PXCache cache,
    PXRowPersistingEventArgs e)
  {
    OrganizationLedgerLink row = e.Row as OrganizationLedgerLink;
    if (e.Operation != 3)
      return;
    PXSetPropertyException propertyException = this.CanBeLinkDeleted(row);
    if (propertyException != null)
      throw propertyException;
  }

  protected virtual void OrganizationLedgerLink_RowSelected(PXCache cache, PXRowSelectedEventArgs e)
  {
    if (!(e.Row is OrganizationLedgerLink row))
      return;
    PXCache pxCache1 = cache;
    OrganizationLedgerLink organizationLedgerLink1 = row;
    int? nullable = row.OrganizationID;
    int num1 = !nullable.HasValue ? 1 : 0;
    PXUIFieldAttribute.SetEnabled<OrganizationLedgerLink.organizationID>(pxCache1, (object) organizationLedgerLink1, num1 != 0);
    PXCache pxCache2 = cache;
    OrganizationLedgerLink organizationLedgerLink2 = row;
    nullable = row.LedgerID;
    int num2 = !nullable.HasValue ? 1 : 0;
    PXUIFieldAttribute.SetEnabled<OrganizationLedgerLink.ledgerID>(pxCache2, (object) organizationLedgerLink2, num2 != 0);
  }

  protected virtual void OrganizationLedgerLink_RowInserting(
    PXCache cache,
    PXRowInsertingEventArgs e)
  {
    if (!(e.Row is OrganizationLedgerLink row))
      return;
    Ledger ledgerById = GeneralLedgerMaint.FindLedgerByID((PXGraph) this.Base, row.LedgerID, false);
    if (!(ledgerById.BalanceType == "A"))
      return;
    this.CheckActualLedgerCanBeAssigned(ledgerById, row.OrganizationID.SingleToArray<int?>());
  }

  protected virtual void OrganizationLedgerLink_RowInserted(PXCache cache, PXRowInsertedEventArgs e)
  {
    if (!(e.Row is OrganizationLedgerLink row))
      return;
    Ledger ledgerById = GeneralLedgerMaint.FindLedgerByID((PXGraph) this.Base, row.LedgerID, false);
    GraphHelper.SmartSetStatus(((PXSelectBase) this.LedgerViewBase).Cache, (object) ledgerById, (PXEntryStatus) 1, (PXEntryStatus) 0);
    if (!(ledgerById.BalanceType == "A"))
      return;
    PX.Objects.GL.DAC.Organization updatingOrganization = this.GetUpdatingOrganization(row.OrganizationID);
    if (updatingOrganization.ActualLedgerID.HasValue)
      return;
    PX.Objects.GL.DAC.Organization copy = PXCache<PX.Objects.GL.DAC.Organization>.CreateCopy(updatingOrganization);
    copy.ActualLedgerID = row.LedgerID;
    this.OrganizationViewBase.Update(copy);
  }

  protected virtual void OrganizationLedgerLink_RowDeleted(PXCache cache, PXRowDeletedEventArgs e)
  {
    if (!(e.Row is OrganizationLedgerLink row))
      return;
    Ledger ledgerById = GeneralLedgerMaint.FindLedgerByID((PXGraph) this.Base, row.LedgerID, false);
    PX.Objects.GL.DAC.Organization updatingOrganization = this.GetUpdatingOrganization(row.OrganizationID);
    PXEntryStatus status = ((PXSelectBase) this.OrganizationViewBase).Cache.GetStatus((object) updatingOrganization);
    if (!(ledgerById.BalanceType == "A") || status == 3 || status == 4)
      return;
    PX.Objects.GL.DAC.Organization copy = PXCache<PX.Objects.GL.DAC.Organization>.CreateCopy(updatingOrganization);
    copy.ActualLedgerID = new int?();
    this.OrganizationViewBase.Update(copy);
  }

  protected virtual void DeleteButtonFieldSelectingHandler(
    PXCache sender,
    PXFieldSelectingEventArgs e)
  {
    e.ReturnState = (object) PXButtonState.CreateDefaultState<TPrimaryDAC>(e.ReturnState);
    ((PXFieldState) e.ReturnState).Enabled = ((PXSelectBase) this.OrganizationLedgerLinkSelect).AllowDelete;
  }

  [PXButton(ImageKey = "RecordDel", ImageSet = "main")]
  [PXUIField]
  public virtual IEnumerable deleteOrganizationLedgerLink(PXAdapter adapter)
  {
    if ((((PXCache) this.LinkCache).Current is OrganizationLedgerLink current ? (!current.OrganizationID.HasValue ? 1 : 0) : 1) != 0 || !current.LedgerID.HasValue)
      return adapter.Get();
    Ledger ledgerById = GeneralLedgerMaint.FindLedgerByID((PXGraph) this.Base, current.LedgerID);
    if (ledgerById != null && ledgerById.BalanceType == "A")
      this.LinkCache.Delete(current);
    else if (GLUtility.RelatedGLHistoryExists((PXGraph) this.Base, current.LedgerID, current.OrganizationID))
    {
      PX.Objects.GL.DAC.Organization organizationById = OrganizationMaint.FindOrganizationByID((PXGraph) this.Base, current.OrganizationID);
      if (this.OrganizationLedgerLinkSelect.Ask(PXMessages.LocalizeFormatNoPrefix("At least one General Ledger transaction has been posted to the {0} ledger. Are you sure you want to detach the ledger from the {1} company?", new object[2]
      {
        (object) ((PXCache) this.LinkCache).GetValueExt<OrganizationLedgerLink.ledgerID>((object) current).ToString().Trim(),
        (object) organizationById.OrganizationCD.Trim()
      }), (MessageButtons) 4) == 6)
        this.LinkCache.Delete(current);
    }
    else
      this.LinkCache.Delete(current);
    return adapter.Get();
  }

  public virtual void CheckActualLedgerCanBeAssigned(Ledger ledger, int?[] organizationIDs)
  {
    foreach (PX.Objects.GL.DAC.Organization organizationById in OrganizationMaint.FindOrganizationByIDs((PXGraph) this.Base, organizationIDs, false))
    {
      int? actualLedgerId = organizationById.ActualLedgerID;
      if (actualLedgerId.HasValue)
      {
        actualLedgerId = organizationById.ActualLedgerID;
        int? ledgerId = ledger.LedgerID;
        if (!(actualLedgerId.GetValueOrDefault() == ledgerId.GetValueOrDefault() & actualLedgerId.HasValue == ledgerId.HasValue))
        {
          Ledger ledgerById = GeneralLedgerMaint.FindLedgerByID((PXGraph) this.Base, organizationById.ActualLedgerID, false);
          throw new PXSetPropertyException("The {0} ledger cannot be associated with the {1} company because the {2} actual ledger has been already associated with this company.", new object[3]
          {
            (object) ledger.LedgerCD,
            (object) organizationById.OrganizationCD.Trim(),
            (object) ledgerById.LedgerCD
          });
        }
      }
      if (organizationById.BaseCuryID != ledger.BaseCuryID)
        throw new PXSetPropertyException("The base currency of the {0} company does not match the {1} ledger currency.", new object[2]
        {
          (object) organizationById.OrganizationCD.Trim(),
          (object) ledger.LedgerCD
        });
    }
  }

  public virtual void SetActualLedgerIDNullInRelatedCompanies(Ledger ledger, int?[] organizationIDs)
  {
    foreach (PX.Objects.GL.DAC.Organization organizationById in OrganizationMaint.FindOrganizationByIDs((PXGraph) this.Base, organizationIDs, false))
    {
      PX.Objects.GL.DAC.Organization copy = PXCache<PX.Objects.GL.DAC.Organization>.CreateCopy(organizationById);
      copy.ActualLedgerID = new int?();
      this.OrganizationViewBase.Update(copy);
    }
  }

  protected abstract PX.Objects.GL.DAC.Organization GetUpdatingOrganization(int? organizationID);

  protected virtual PXSetPropertyException CanBeLinkDeleted(OrganizationLedgerLink link)
  {
    Ledger ledgerById = GeneralLedgerMaint.FindLedgerByID((PXGraph) this.Base, link.LedgerID);
    if (ledgerById == null || !(ledgerById.BalanceType == "A") || !GLUtility.RelatedGLHistoryExists((PXGraph) this.Base, link.LedgerID, link.OrganizationID))
      return (PXSetPropertyException) null;
    PX.Objects.GL.DAC.Organization organizationById = OrganizationMaint.FindOrganizationByID((PXGraph) this.Base, link.OrganizationID);
    return new PXSetPropertyException("The relation between the {0} ledger and the {1} company cannot be removed because at least one General Ledger transaction has been posted to the related branch and ledger.", (PXErrorLevel) 5, new object[2]
    {
      ((PXCache) this.LinkCache).GetValueExt<OrganizationLedgerLink.ledgerID>((object) link),
      (object) organizationById.OrganizationCD.Trim()
    });
  }
}
