// Decompiled with JetBrains decompiler
// Type: PX.Objects.CS.CountryMaint
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;
using System.ComponentModel;

#nullable disable
namespace PX.Objects.CS;

public class CountryMaint : PXGraph<CountryMaint, PX.Objects.CS.Country>
{
  public PXSelect<PX.Objects.CS.Country> Country;
  public PXSelect<State, Where<State.countryID, Equal<Current<PX.Objects.CS.Country.countryID>>>, OrderBy<Asc<State.stateID>>> CountryStates;

  public CountryMaint()
  {
    PXUIFieldAttribute.SetVisible<PX.Objects.CS.Country.languageID>(((PXSelectBase) this.Country).Cache, (object) null, PXDBLocalizableStringAttribute.HasMultipleLocales);
  }

  [PXDBString(2, IsKey = true, IsFixed = true)]
  [PXMergeAttributes]
  protected virtual void Country_CountryID_CacheAttached(PXCache sender)
  {
  }

  [PXCustomizeBaseAttribute(typeof (PXUIFieldAttribute), "DisplayName", "Country Validation Mode")]
  public virtual void _(
    Events.CacheAttached<PX.Objects.CS.Country.countryValidationMethod> e)
  {
  }

  [PXCustomizeBaseAttribute(typeof (PXUIFieldAttribute), "DisplayName", "Country Validation Regexp")]
  public virtual void _(Events.CacheAttached<PX.Objects.CS.Country.countryRegexp> e)
  {
  }

  [PXCustomizeBaseAttribute(typeof (PXUIFieldAttribute), "DisplayName", "State Validation Mode")]
  public virtual void _(
    Events.CacheAttached<PX.Objects.CS.Country.stateValidationMethod> e)
  {
  }

  [PXCustomizeBaseAttribute(typeof (PXUIFieldAttribute), "DisplayName", "Postal Code Validation Regexp")]
  public virtual void _(Events.CacheAttached<PX.Objects.CS.Country.zipCodeRegexp> e)
  {
  }

  [PXCustomizeBaseAttribute(typeof (PXUIFieldAttribute), "DisplayName", "State Validation Regexp")]
  public virtual void _(Events.CacheAttached<State.stateRegexp> e)
  {
  }

  protected virtual void Country_RowPersisting(PXCache cache, PXRowPersistingEventArgs e)
  {
    if ((e.Operation & 3) != 2 && (e.Operation & 3) != 1)
      return;
    PX.Objects.CS.Country row = e.Row as PX.Objects.CS.Country;
    if (string.IsNullOrEmpty(row.Description) || string.IsNullOrEmpty(row.Description.Trim()))
      cache.RaiseExceptionHandling("Description", e.Row, (object) row.Description, (Exception) new PXSetPropertyException("Country Name cannot be empty."));
    if (!string.IsNullOrEmpty(row.CountryID) && !string.IsNullOrEmpty(row.CountryID.Trim()))
      return;
    cache.RaiseExceptionHandling("CountryID", e.Row, (object) row.CountryID, (Exception) new PXSetPropertyException("Country ID cannot be empty."));
  }

  protected virtual void Country_RowPersisted(PXCache cache, PXRowPersistedEventArgs e)
  {
    if (e.TranStatus != 1)
      return;
    PXZipValidationAttribute.Clear<PX.Objects.CS.Country>();
  }

  private bool IsKeyExists(State aState)
  {
    foreach (PXResult<State> pxResult in ((PXSelectBase<State>) this.CountryStates).Select(Array.Empty<object>()))
    {
      if (PXResult<State>.op_Implicit(pxResult).StateID == aState.StateID)
        return true;
    }
    return false;
  }

  protected virtual void Country_CountryID_FieldVerifying(
    PXCache cache,
    PXFieldVerifyingEventArgs e)
  {
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void State_RowUpdating(PXCache cache, PXRowUpdatingEventArgs e)
  {
    this.ValidateState(e.Row as State);
  }

  protected virtual void State_RowInserting(PXCache cache, PXRowInsertingEventArgs e)
  {
    State row = e.Row as State;
    if (this.ValidateState(row) && this.IsKeyExists(row))
      throw new PXException("State/Region ID must be unique.");
  }

  private bool ValidateState(State state)
  {
    if (string.IsNullOrEmpty(state.StateID) || string.IsNullOrEmpty(state.StateID.Trim()))
      throw new PXException("State/Region ID cannot be empty.");
    return true;
  }
}
