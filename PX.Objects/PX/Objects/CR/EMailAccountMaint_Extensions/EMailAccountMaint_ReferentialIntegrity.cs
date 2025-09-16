// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.EMailAccountMaint_Extensions.EMailAccountMaint_ReferentialIntegrity
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.SM;
using System;

#nullable enable
namespace PX.Objects.CR.EMailAccountMaint_Extensions;

public class EMailAccountMaint_ReferentialIntegrity : PXGraphExtension<
#nullable disable
EMailAccountMaint>
{
  [PXHidden]
  [PXCopyPasteHiddenView]
  public FbqlSelect<SelectFromBase<CRContactClass, TypeArrayOf<IFbqlJoin>.Empty>, CRContactClass>.View ContactClass;
  [PXHidden]
  [PXCopyPasteHiddenView]
  public FbqlSelect<SelectFromBase<CRCaseClass, TypeArrayOf<IFbqlJoin>.Empty>, CRCaseClass>.View CaseClass;
  [PXHidden]
  [PXCopyPasteHiddenView]
  public FbqlSelect<SelectFromBase<CRCustomerClass, TypeArrayOf<IFbqlJoin>.Empty>, CRCustomerClass>.View CustomerClass;
  [PXHidden]
  [PXCopyPasteHiddenView]
  public FbqlSelect<SelectFromBase<CRLeadClass, TypeArrayOf<IFbqlJoin>.Empty>, CRLeadClass>.View LeadClass;
  [PXHidden]
  [PXCopyPasteHiddenView]
  public FbqlSelect<SelectFromBase<CROpportunityClass, TypeArrayOf<IFbqlJoin>.Empty>, CROpportunityClass>.View OpportunityClass;
  [PXHidden]
  [PXCopyPasteHiddenView]
  public FbqlSelect<SelectFromBase<UserPreferences, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<
  #nullable enable
  UserPreferences.defaultEMailAccountID, IBqlInt>.IsEqual<
  #nullable disable
  BqlField<
  #nullable enable
  EMailAccount.emailAccountID, IBqlInt>.FromCurrent>>, 
  #nullable disable
  UserPreferences>.View UserPreference;

  protected virtual void _(Events.RowDeleted<EMailAccount> e)
  {
    if (e.Row == null)
      return;
    PreferencesEmail preferencesEmail1 = ((PXSelectBase<PreferencesEmail>) this.Base.Preferences).SelectSingle(Array.Empty<object>());
    int? nullable1;
    if (preferencesEmail1 != null)
    {
      int? emailAccountId = e.Row.EmailAccountID;
      nullable1 = preferencesEmail1.DefaultEMailAccountID;
      if (emailAccountId.GetValueOrDefault() == nullable1.GetValueOrDefault() & emailAccountId.HasValue == nullable1.HasValue)
      {
        PreferencesEmail preferencesEmail2 = preferencesEmail1;
        nullable1 = new int?();
        int? nullable2 = nullable1;
        preferencesEmail2.DefaultEMailAccountID = nullable2;
        ((PXSelectBase<PreferencesEmail>) this.Base.Preferences).Update(preferencesEmail1);
      }
    }
    foreach (PXResult<UserPreferences> pxResult in ((PXSelectBase<UserPreferences>) this.UserPreference).Select(Array.Empty<object>()))
    {
      UserPreferences userPreferences1 = PXResult<UserPreferences>.op_Implicit(pxResult);
      UserPreferences userPreferences2 = userPreferences1;
      nullable1 = new int?();
      int? nullable3 = nullable1;
      userPreferences2.DefaultEMailAccountID = nullable3;
      ((PXSelectBase<UserPreferences>) this.UserPreference).Update(userPreferences1);
    }
  }
}
