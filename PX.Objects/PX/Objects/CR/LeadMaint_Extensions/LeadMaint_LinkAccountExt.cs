// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.LeadMaint_Extensions.LeadMaint_LinkAccountExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CR.Extensions;
using PX.Objects.CR.Extensions.CRCreateActions;
using PX.Objects.CR.Wizard;
using System;

#nullable enable
namespace PX.Objects.CR.LeadMaint_Extensions;

/// <summary>
/// An extension that you can use to link <see cref="T:PX.Objects.CR.BAccount" /> with the current <see cref="T:PX.Objects.CR.CRLead" />.
/// </summary>
public class LeadMaint_LinkAccountExt : PXGraphExtension<
#nullable disable
LeadMaint>
{
  public PXFilter<LeadMaint_LinkAccountExt.LinkAccountFilter> LinkAccount;
  public PXFilter<LeadMaint_LinkAccountExt.LinkAccountFilter> LinkAccountWithoutContact;

  protected virtual void _(Events.RowUpdated<CRLead> e, PXRowUpdated del)
  {
    PreventRecursionCall.Execute((Action) (() =>
    {
      try
      {
        bool flag = false;
        if (e.Row != null && this.ShouldProcess(((Events.Event<PXRowUpdatedEventArgs, Events.RowUpdated<CRLead>>) e).Cache, e.Row, e.OldRow) && !((PXGraph) this.Base).UnattendedMode && !((PXGraph) this.Base).IsCopyPasteContext)
        {
          this.ProcessFirstStep(((Events.Event<PXRowUpdatedEventArgs, Events.RowUpdated<CRLead>>) e).Cache, e.Row, e.OldRow);
          flag = true;
        }
        try
        {
          del?.Invoke(((Events.Event<PXRowUpdatedEventArgs, Events.RowUpdated<CRLead>>) e).Cache, ((Events.Event<PXRowUpdatedEventArgs, Events.RowUpdated<CRLead>>) e).Args);
        }
        catch (CRWizardAbortException ex)
        {
          this.AbortCurrentValueChange();
        }
        if (!flag)
          return;
        this.EnsurePopupPanelWillBeShown();
      }
      catch (PXDialogRequiredException ex)
      {
        this.StoreChangedData(e.OldRow, e.Row.RefContactID, e.Row.BAccountID);
        throw;
      }
    }), nameof (_), "C:\\build\\code_repo\\WebSites\\Pure\\PX.Objects\\CR\\Graphs\\LeadMaint\\LeadMaint_LinkAccountExt.cs", 203);
  }

  protected virtual void _(
    Events.RowUpdated<LeadMaint_LinkAccountExt.LinkAccountFilter> e)
  {
    ((Events.Event<PXRowUpdatedEventArgs, Events.RowUpdated<LeadMaint_LinkAccountExt.LinkAccountFilter>>) e).Cache.IsDirty = false;
  }

  protected virtual void _(
    Events.FieldDefaulting<ContactFilter, ContactFilter.fullName> e,
    PXFieldDefaulting del)
  {
    if (e.Row != null)
    {
      int? newBaccountId = ((PXSelectBase<LeadMaint_LinkAccountExt.LinkAccountFilter>) this.LinkAccount).Current.NewBAccountID;
      if (newBaccountId.HasValue && newBaccountId.GetValueOrDefault() != 0)
      {
        BAccount baccount = BAccount.PK.Find((PXGraph) this.Base, ((PXSelectBase<LeadMaint_LinkAccountExt.LinkAccountFilter>) this.LinkAccount).Current.NewBAccountID);
        if (baccount != null)
        {
          ((Events.FieldDefaultingBase<Events.FieldDefaulting<ContactFilter, ContactFilter.fullName>, ContactFilter, object>) e).NewValue = (object) baccount.AcctName;
          return;
        }
      }
    }
    del.Invoke(((Events.Event<PXFieldDefaultingEventArgs, Events.FieldDefaulting<ContactFilter, ContactFilter.fullName>>) e).Cache, ((Events.Event<PXFieldDefaultingEventArgs, Events.FieldDefaulting<ContactFilter, ContactFilter.fullName>>) e).Args);
  }

  /// <summary>
  /// Throws <see cref="T:PX.Data.PXDialogRequiredException" />
  /// </summary>
  public virtual void ProcessFirstStep(PXCache cache, CRLead row, CRLead oldRow)
  {
    using (new WizardScope())
    {
      switch ((int) this.AskOnValueChange())
      {
        case 0:
          this.AskOnValueChange();
          break;
        case 1:
        case 6:
          try
          {
            int? linkAccountOption = ((PXSelectBase<LeadMaint_LinkAccountExt.LinkAccountFilter>) this.LinkAccount).Current.LinkAccountOption;
            if (!linkAccountOption.HasValue)
              break;
            switch (linkAccountOption.GetValueOrDefault())
            {
              case 0:
                this.ProcessSecondStep(cache, row, oldRow);
                return;
              case 1:
                try
                {
                  ((PXGraph) this.Base).GetExtension<LeadMaint_LinkContactExt>().SelectEntityForLinkAsk();
                }
                catch (CRWizardBackException ex)
                {
                  ((PXSelectBase) this.LinkAccount).View.Answer = (WebDialogResult) 0;
                  throw;
                }
                oldRow.RefContactID = row.RefContactID;
                return;
              case 2:
                LeadMaint.CreateContactFromLeadGraphExt extension = ((PXGraph) this.Base).GetExtension<LeadMaint.CreateContactFromLeadGraphExt>();
                if (((PXSelectBase) extension.PopupValidator.Filter).View.Answer == null)
                {
                  bool? insideCreateContact = ((PXSelectBase<LeadMaint_LinkAccountExt.LinkAccountFilter>) this.LinkAccount).Current.InsideCreateContact;
                  if (insideCreateContact.HasValue && insideCreateContact.GetValueOrDefault())
                    return;
                  ((PXSelectBase) this.Base.Lead).Cache.SetValue<CRLead.refContactID>((object) row, (object) ((PXSelectBase<LeadMaint_LinkAccountExt.LinkAccountFilter>) this.LinkAccount).Current.OldContactID);
                  ((PXSelectBase) this.Base.Lead).Cache.SetValue<CRLead.bAccountID>((object) row, (object) ((PXSelectBase<LeadMaint_LinkAccountExt.LinkAccountFilter>) this.LinkAccount).Current.OldBAccountID);
                  ((PXSelectBase<LeadMaint_LinkAccountExt.LinkAccountFilter>) this.LinkAccount).Current.InsideCreateContact = new bool?(true);
                }
                try
                {
                  ((PXAction) extension.CreateContact).Press();
                }
                catch (PXDialogRequiredException ex)
                {
                  ((PXSelectBase) this.LinkAccount).View.Answer = (WebDialogResult) 6;
                  throw;
                }
                catch (CRWizardException ex)
                {
                  ((PXSelectBase<LeadMaint_LinkAccountExt.LinkAccountFilter>) this.LinkAccount).Current.InsideCreateContact = new bool?(false);
                  throw;
                }
                catch (PXOuterException ex)
                {
                  ((PXSelectBase<LeadMaint_LinkAccountExt.LinkAccountFilter>) this.LinkAccount).Current.InsideCreateContact = new bool?(false);
                  throw;
                }
                finally
                {
                  if (((PXSelectBase) extension.PopupValidator.Filter).View.Answer == null)
                  {
                    ((PXSelectBase) this.Base.Lead).Cache.SetValue<CRLead.refContactID>((object) row, (object) ((PXSelectBase<LeadMaint_LinkAccountExt.LinkAccountFilter>) this.LinkAccount).Current.NewContactID);
                    ((PXSelectBase) this.Base.Lead).Cache.SetValue<CRLead.bAccountID>((object) row, (object) ((PXSelectBase<LeadMaint_LinkAccountExt.LinkAccountFilter>) this.LinkAccount).Current.NewBAccountID);
                  }
                }
                ((PXSelectBase<LeadMaint_LinkAccountExt.LinkAccountFilter>) this.LinkAccount).Current.InsideCreateContact = new bool?(false);
                row.OverrideRefContact = new bool?(false);
                return;
              default:
                return;
            }
          }
          catch (CRWizardBackException ex)
          {
            ((PXSelectBase) this.LinkAccount).View.Answer = (WebDialogResult) 0;
            goto case 0;
          }
          catch (CRWizardAbortException ex)
          {
            goto case 3;
          }
          catch (PXBaseRedirectException ex)
          {
            throw;
          }
          catch (Exception ex)
          {
            PXTrace.WriteError(ex);
            throw new CRWrappedRedirectException(ex);
          }
        case 3:
          this.AbortValueChange(row);
          break;
      }
    }
  }

  /// <summary>
  /// Throws <see cref="T:PX.Data.PXDialogRequiredException" />, <see cref="T:PX.Objects.CR.Wizard.CRWizardBackException" />, <see cref="T:PX.Objects.CR.Wizard.CRWizardAbortException" />
  /// </summary>
  public virtual void ProcessSecondStep(PXCache cache, CRLead row, CRLead oldRow)
  {
    WebDialogResult webDialogResult = ((PXSelectBase<LeadMaint_LinkAccountExt.LinkAccountFilter>) this.LinkAccountWithoutContact.WithActionIfNoAnswerFor<PXFilter<LeadMaint_LinkAccountExt.LinkAccountFilter>>(true, (Action) (() =>
    {
      if (((PXSelectBase<LeadMaint_LinkAccountExt.LinkAccountFilter>) this.LinkAccountWithoutContact).Current == null)
        return;
      ((PXSelectBase<LeadMaint_LinkAccountExt.LinkAccountFilter>) this.LinkAccountWithoutContact).Current.WithoutContactOption = new int?(0);
    })).WithAnswerForImport<PXFilter<LeadMaint_LinkAccountExt.LinkAccountFilter>>((WebDialogResult) 6).WithAnswerForMobile<PXFilter<LeadMaint_LinkAccountExt.LinkAccountFilter>>((WebDialogResult) 6).WithAnswerForCbApi<PXFilter<LeadMaint_LinkAccountExt.LinkAccountFilter>>((WebDialogResult) 6)).AskExt();
    if (webDialogResult == 3)
      throw new CRWizardAbortException();
    if (webDialogResult != 6)
    {
      if (webDialogResult == 72)
      {
        this.ClearAnswers();
        throw new CRWizardBackException();
      }
    }
    else
    {
      this.ProcessLinkAccountWithoutContact();
      this.ResetStoredFilterValues();
    }
  }

  public virtual void ProcessLinkAccountWithoutContact()
  {
    LeadMaint.LeadBAccountSharedAddressOverrideGraphExt extension1 = ((PXGraph) this.Base).GetExtension<LeadMaint.LeadBAccountSharedAddressOverrideGraphExt>();
    LeadMaint.UpdateRelatedContactInfoFromLeadGraphExt extension2 = ((PXGraph) this.Base).GetExtension<LeadMaint.UpdateRelatedContactInfoFromLeadGraphExt>();
    bool flag = ((PXSelectBase<LeadMaint_LinkAccountExt.LinkAccountFilter>) this.LinkAccount).Current.WithoutContactOption.GetValueOrDefault() != 1;
    CRLead current = ((PXSelectBase<CRLead>) this.Base.Lead).Current;
    CRLead crLead1 = current;
    CRLead crLead2 = current;
    bool? nullable1 = new bool?(flag);
    bool? nullable2 = nullable1;
    crLead2.OverrideAddress = nullable2;
    bool? nullable3 = nullable1;
    crLead1.OverrideRefContact = nullable3;
    CRLead newRow = current;
    int? oldBaccountId = ((PXSelectBase<LeadMaint_LinkAccountExt.LinkAccountFilter>) this.LinkAccount).Current.OldBAccountID;
    extension1.UpdateRelatedOnBAccountIDChange(newRow, oldBaccountId);
    if (current.OverrideRefContact.GetValueOrDefault())
      return;
    BAccount baccount = BAccount.PK.Find((PXGraph) this.Base, current.BAccountID);
    Contact parent1 = (Contact) PrimaryKeyOf<Contact>.By<Contact.contactID>.ForeignKeyOf<BAccount>.By<BAccount.defContactID>.FindParent((PXGraph) this.Base, (BAccount.defContactID) baccount, (PKFindOptions) 0);
    Address parent2 = (Address) PrimaryKeyOf<Address>.By<Address.addressID>.ForeignKeyOf<BAccount>.By<BAccount.defAddressID>.FindParent((PXGraph) this.Base, (BAccount.defAddressID) baccount, (PKFindOptions) 0);
    extension2.UpdateFieldsValuesWithoutPersist((PXResult) new PXResult<Contact, Address>(parent1, parent2), (PXResult) new PXResult<Contact, Address>((Contact) current, ((PXSelectBase<Address>) this.Base.AddressCurrent).SelectSingle(Array.Empty<object>())));
  }

  protected virtual void ClearAnswers()
  {
    ((PXSelectBase) this.LinkAccount).View.Answer = (WebDialogResult) 0;
    ((PXSelectBase) this.LinkAccountWithoutContact).View.Answer = (WebDialogResult) 0;
  }

  protected virtual bool ShouldProcess(PXCache cache, CRLead row, CRLead oldRow)
  {
    bool flag = ((PXGraph) this.Base).IsImport || ((PXGraph) this.Base).IsMobile || ((PXGraph) this.Base).IsContractBasedAPI;
    if (flag && this.ValueChanged(cache, row, oldRow))
      return true;
    if (flag)
      return false;
    return ((PXSelectBase) this.LinkAccount).View.Answer != null || this.ValueChanged(cache, row, oldRow);
  }

  protected virtual bool ValueChanged(PXCache cache, CRLead row, CRLead oldRow)
  {
    if (row.RefContactID.HasValue || !row.BAccountID.HasValue)
      return false;
    int? baccountId1 = row.BAccountID;
    int? baccountId2 = oldRow.BAccountID;
    return !(baccountId1.GetValueOrDefault() == baccountId2.GetValueOrDefault() & baccountId1.HasValue == baccountId2.HasValue);
  }

  public virtual void StoreChangedData(CRLead lead, int? newContactID, int? newBAccountID)
  {
    if (((PXSelectBase<LeadMaint_LinkAccountExt.LinkAccountFilter>) this.LinkAccount).Current == null)
    {
      ((PXSelectBase) this.LinkAccount).Cache.Clear();
      ((PXSelectBase<LeadMaint_LinkAccountExt.LinkAccountFilter>) this.LinkAccount).Current = ((PXSelectBase<LeadMaint_LinkAccountExt.LinkAccountFilter>) this.LinkAccount).Insert();
    }
    int? oldBaccountId = ((PXSelectBase<LeadMaint_LinkAccountExt.LinkAccountFilter>) this.LinkAccount).Current.OldBAccountID;
    int num = 0;
    if (!(oldBaccountId.GetValueOrDefault() == num & oldBaccountId.HasValue))
      return;
    ((PXSelectBase<LeadMaint_LinkAccountExt.LinkAccountFilter>) this.LinkAccount).Current.OldContactID = lead.RefContactID;
    ((PXSelectBase<LeadMaint_LinkAccountExt.LinkAccountFilter>) this.LinkAccount).Current.NewContactID = newContactID;
    ((PXSelectBase<LeadMaint_LinkAccountExt.LinkAccountFilter>) this.LinkAccount).Current.OldBAccountID = lead.BAccountID;
    ((PXSelectBase<LeadMaint_LinkAccountExt.LinkAccountFilter>) this.LinkAccount).Current.NewBAccountID = newBAccountID;
  }

  public virtual void ResetStoredFilterValues()
  {
    if (((PXSelectBase<LeadMaint_LinkAccountExt.LinkAccountFilter>) this.LinkAccount).Current == null)
      return;
    LeadMaint_LinkAccountExt.LinkAccountFilter current = ((PXSelectBase<LeadMaint_LinkAccountExt.LinkAccountFilter>) this.LinkAccount).Current;
    current.OldContactID = new int?(0);
    current.NewContactID = new int?(0);
    current.OldBAccountID = new int?(0);
    current.NewBAccountID = new int?(0);
    current.IsWizardOpen = new bool?(false);
    current.InsideCreateContact = new bool?(false);
  }

  public virtual WebDialogResult AskOnValueChange()
  {
    ((PXSelectBase<LeadMaint_LinkAccountExt.LinkAccountFilter>) this.LinkAccount).Current.IsWizardOpen = new bool?(true);
    return ((PXSelectBase<LeadMaint_LinkAccountExt.LinkAccountFilter>) this.LinkAccount.WithActionIfNoAnswerFor<PXFilter<LeadMaint_LinkAccountExt.LinkAccountFilter>>(((PXGraph) this.Base).IsImport || ((PXGraph) this.Base).IsMobile || ((PXGraph) this.Base).IsContractBasedAPI, (Action) (() => ((PXSelectBase<LeadMaint_LinkAccountExt.LinkAccountFilter>) this.LinkAccount).Current.LinkAccountOption = new int?(0))).WithAnswerForImport<PXFilter<LeadMaint_LinkAccountExt.LinkAccountFilter>>((WebDialogResult) 6).WithAnswerForMobile<PXFilter<LeadMaint_LinkAccountExt.LinkAccountFilter>>((WebDialogResult) 6).WithAnswerForCbApi<PXFilter<LeadMaint_LinkAccountExt.LinkAccountFilter>>((WebDialogResult) 6)).AskExt();
  }

  public virtual void AbortCurrentValueChange()
  {
    this.AbortValueChange(((PXSelectBase<CRLead>) this.Base.Lead).Current);
  }

  public virtual void AbortValueChange(CRLead lead)
  {
    CRLead crLead1 = lead;
    int? oldContactId = ((PXSelectBase<LeadMaint_LinkAccountExt.LinkAccountFilter>) this.LinkAccount).Current.OldContactID;
    int num1 = 0;
    int? nullable1 = !(oldContactId.GetValueOrDefault() == num1 & oldContactId.HasValue) ? ((PXSelectBase<LeadMaint_LinkAccountExt.LinkAccountFilter>) this.LinkAccount).Current.OldContactID : new int?();
    crLead1.RefContactID = nullable1;
    CRLead crLead2 = lead;
    int? oldBaccountId = ((PXSelectBase<LeadMaint_LinkAccountExt.LinkAccountFilter>) this.LinkAccount).Current.OldBAccountID;
    int num2 = 0;
    int? nullable2 = !(oldBaccountId.GetValueOrDefault() == num2 & oldBaccountId.HasValue) ? ((PXSelectBase<LeadMaint_LinkAccountExt.LinkAccountFilter>) this.LinkAccount).Current.OldBAccountID : new int?();
    crLead2.BAccountID = nullable2;
    ((PXSelectBase) this.LinkAccount).View.Answer = (WebDialogResult) 0;
    this.ResetStoredFilterValues();
    this.LinkAccount.Reset();
  }

  public virtual bool IsWizardOpen()
  {
    return ((PXSelectBase<LeadMaint_LinkAccountExt.LinkAccountFilter>) this.LinkAccount).Current.IsWizardOpen ?? false;
  }

  public virtual void EnsurePopupPanelWillBeShown()
  {
    if (!string.IsNullOrEmpty(PopupNoteManager.Message))
      return;
    object baccountId = (object) ((PXSelectBase<CRLead>) this.Base.Lead).Current.BAccountID;
    ((PXSelectBase) this.Base.Lead).Cache.RaiseFieldVerifying<CRLead.bAccountID>((object) ((PXSelectBase<CRLead>) this.Base.Lead).Current, ref baccountId);
  }

  /// <summary>
  /// The filter that is used for linking <see cref="T:PX.Objects.CR.BAccount" /> with the current <see cref="T:PX.Objects.CR.CRLead" />.
  /// </summary>
  [PXHidden]
  public class LinkAccountFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    public const int NotSetValue = 0;

    /// <summary>
    /// The field that specifies how <see cref="T:PX.Objects.CR.BAccount" /> is linked with the current <see cref="T:PX.Objects.CR.CRLead" />:
    /// with <see cref="T:PX.Objects.CR.BAccount" />'s <see cref="T:PX.Objects.CR.Contact" />, without <see cref="T:PX.Objects.CR.Contact" />,
    /// or with a new <see cref="T:PX.Objects.CR.Contact" />.
    /// </summary>
    /// <value>
    /// The field can have one of the values described in <see cref="T:PX.Objects.CR.LeadMaint_Extensions.LeadMaint_LinkAccountExt.LinkAccountFilter.linkAccountOption.List" />.
    /// </value>
    [PXInt]
    [PXUIField]
    [PXUnboundDefault(0)]
    [LeadMaint_LinkAccountExt.LinkAccountFilter.linkAccountOption.List]
    public int? LinkAccountOption { get; set; }

    /// <summary>
    /// The field that specifies whether the <see cref="T:PX.Objects.CR.BAccount" /> settings override the <see cref="T:PX.Objects.CR.CRLead" /> settings.
    /// </summary>
    /// <value>
    /// The field can have one of the values described in <see cref="T:PX.Objects.CR.LeadMaint_Extensions.LeadMaint_LinkAccountExt.LinkAccountFilter.withoutContactOption.List" />.
    /// </value>
    [PXInt]
    [PXUIField]
    [PXUnboundDefault(0)]
    [LeadMaint_LinkAccountExt.LinkAccountFilter.withoutContactOption.List]
    public int? WithoutContactOption { get; set; }

    /// <summary>
    /// The ID of <see cref="T:PX.Objects.CR.Contact" /> that should be linked with the current <see cref="T:PX.Objects.CR.CRLead" />
    /// along with <see cref="T:PX.Objects.CR.BAccount" />.
    /// </summary>
    /// <value>
    /// The value of this field corresponds to the <see cref="P:PX.Objects.CR.Contact.ContactID" />.
    /// </value>
    [PXInt]
    [PXUnboundDefault(0)]
    public int? NewContactID { get; set; }

    /// <summary>
    /// The ID of <see cref="T:PX.Objects.CR.Contact" /> that was linked with the current <see cref="T:PX.Objects.CR.CRLead" />
    /// before the smart panel was opened.
    /// </summary>
    /// <value>
    /// The value of this field corresponds to the <see cref="P:PX.Objects.CR.Contact.ContactID" />.
    /// </value>
    [PXInt]
    [PXUnboundDefault(0)]
    public int? OldContactID { get; set; }

    /// <summary>
    /// The ID of <see cref="T:PX.Objects.CR.BAccount" /> that should be linked with the current <see cref="T:PX.Objects.CR.CRLead" />.
    /// </summary>
    /// <value>
    /// The value of this field corresponds to the <see cref="P:PX.Objects.CR.BAccount.BAccountID" />.
    /// </value>
    [PXInt]
    [PXUnboundDefault(0)]
    public int? NewBAccountID { get; set; }

    /// <summary>
    /// The ID of <see cref="T:PX.Objects.CR.BAccount" /> that was linked with the current <see cref="T:PX.Objects.CR.CRLead" />
    /// before the smart panel was opened.
    /// </summary>
    /// <value>
    /// The value of this field corresponds to the <see cref="P:PX.Objects.CR.BAccount.BAccountID" />.
    /// </value>
    [PXInt]
    [PXUnboundDefault(0)]
    public int? OldBAccountID { get; set; }

    /// <summary>
    /// The service field that specifies that the link account wizard is currently open.
    /// </summary>
    /// <remarks>
    /// This field is required for proper work of the Back and Cancel buttons.
    /// </remarks>
    [PXBool]
    [PXUnboundDefault(false)]
    public bool? IsWizardOpen { get; set; }

    /// <summary>
    /// The service field that shows that the Create Contact dialog is currently open.
    /// </summary>
    /// <remarks>
    /// This field is required for proper navigation between the main screen of the wizard
    /// and the Create Contact panel (see <see cref="T:PX.Objects.CR.Extensions.CRCreateActions.CRCreateContactAction`2" />).
    /// </remarks>
    [PXBool]
    [PXUnboundDefault(false)]
    public bool? InsideCreateContact { get; set; }

    public abstract class linkAccountOption : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      LeadMaint_LinkAccountExt.LinkAccountFilter.linkAccountOption>
    {
      public const int LinkAccount = 0;
      public const int SelectContact = 1;
      public const int CreateContact = 2;

      public class List : PXIntListAttribute
      {
        public List()
          : base(new (int, string)[3]
          {
            (0, "Associate the Lead with an Account"),
            (1, "Associate the Lead with an Account and a Contact"),
            (2, "Associate the Lead with an Account and a New Contact")
          })
        {
        }
      }
    }

    public abstract class withoutContactOption : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      LeadMaint_LinkAccountExt.LinkAccountFilter.withoutContactOption>
    {
      public const int Link = 0;
      public const int LinkAndReplace = 1;

      public class List : PXIntListAttribute
      {
        public List()
          : base(new (int, string)[2]
          {
            (0, "Do Not Update the Lead Settings"),
            (1, "Replace the Lead Settings with the Account Settings")
          })
        {
        }
      }
    }

    public abstract class newContactID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      LeadMaint_LinkAccountExt.LinkAccountFilter.newContactID>
    {
    }

    public abstract class oldContactID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      LeadMaint_LinkAccountExt.LinkAccountFilter.oldContactID>
    {
    }

    public abstract class newBAccountID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      LeadMaint_LinkAccountExt.LinkAccountFilter.newBAccountID>
    {
    }

    public abstract class oldBAccountID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      LeadMaint_LinkAccountExt.LinkAccountFilter.oldBAccountID>
    {
    }

    public abstract class isWizardOpen : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      LeadMaint_LinkAccountExt.LinkAccountFilter.isWizardOpen>
    {
    }

    public abstract class insideCreateContact : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      LeadMaint_LinkAccountExt.LinkAccountFilter.insideCreateContact>
    {
    }
  }
}
