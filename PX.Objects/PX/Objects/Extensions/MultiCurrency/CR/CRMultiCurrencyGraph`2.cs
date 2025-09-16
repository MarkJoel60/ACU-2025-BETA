// Decompiled with JetBrains decompiler
// Type: PX.Objects.Extensions.MultiCurrency.CR.CRMultiCurrencyGraph`2
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.AR;
using PX.Objects.CR;

#nullable disable
namespace PX.Objects.Extensions.MultiCurrency.CR;

public abstract class CRMultiCurrencyGraph<TGraph, TPrimary> : MultiCurrencyGraph<TGraph, TPrimary>
  where TGraph : PXGraph
  where TPrimary : class, IBqlTable, new()
{
  public PXSelect<CRSetup> crSetup;

  protected override string Module => "CR";

  protected override MultiCurrencyGraph<TGraph, TPrimary>.CurySourceMapping GetCurySourceMapping()
  {
    return new MultiCurrencyGraph<TGraph, TPrimary>.CurySourceMapping(typeof (Customer));
  }

  public override void Initialize()
  {
    base.Initialize();
    if (!(this.BAccountField != (System.Type) null) || this.DetailsView == null)
      return;
    this.Base.FieldVerifying.AddHandler(typeof (TPrimary), this.BAccountField.Name, new PXFieldVerifying(this.BAccountID__FieldVerifying));
  }

  protected virtual System.Type BAccountField => (System.Type) null;

  protected virtual PX.Data.PXView DetailsView => (PX.Data.PXView) null;

  protected virtual PX.Objects.CR.BAccount GetRelatedBAccount() => (PX.Objects.CR.BAccount) null;

  protected virtual PX.Objects.Extensions.MultiCurrency.CurySource GetFallbackMapping()
  {
    CRSetup crSetup = this.crSetup.SelectSingle();
    PX.Objects.CR.BAccount relatedBaccount = this.GetRelatedBAccount();
    return new PX.Objects.Extensions.MultiCurrency.CurySource()
    {
      CuryID = relatedBaccount?.CuryID,
      AllowOverrideCury = (bool?) relatedBaccount?.AllowOverrideCury,
      CuryRateTypeID = crSetup?.DefaultRateTypeID,
      AllowOverrideRate = (bool?) crSetup?.AllowOverrideRate
    };
  }

  protected override PX.Objects.Extensions.MultiCurrency.CurySource CurrentSourceSelect()
  {
    PX.Objects.Extensions.MultiCurrency.CurySource curySource = base.CurrentSourceSelect();
    PX.Objects.Extensions.MultiCurrency.CurySource fallbackMapping = this.GetFallbackMapping();
    return new PX.Objects.Extensions.MultiCurrency.CurySource()
    {
      CuryID = curySource?.CuryID ?? fallbackMapping?.CuryID,
      AllowOverrideCury = (bool?) ((bool?) curySource?.AllowOverrideCury ?? fallbackMapping?.AllowOverrideCury),
      CuryRateTypeID = curySource?.CuryRateTypeID ?? fallbackMapping?.CuryRateTypeID,
      AllowOverrideRate = (bool?) ((bool?) curySource?.AllowOverrideRate ?? fallbackMapping?.AllowOverrideRate)
    };
  }

  protected virtual void BAccountID__FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs args)
  {
    object objB = sender.GetValue(args.Row, this.BAccountField.Name);
    if (object.Equals(args.NewValue, objB) || !this.HasDetailRecords())
      return;
    PX.Objects.CR.BAccount baccount = PX.Objects.CR.BAccount.PK.Find((PXGraph) this.Base, args.NewValue as int?);
    if (baccount == null)
      return;
    string str = this.Documents.Cache.GetValue<PX.Objects.Extensions.MultiCurrency.Document.curyID>(args.Row) as string;
    if (!baccount.AllowOverrideCury.GetValueOrDefault() && baccount.CuryID != str && !string.IsNullOrEmpty(baccount.CuryID))
      throw new PXSetPropertyException("The business account cannot be changed because the currency of the account differs from the currency of the document and the Enable Currency Override check box is cleared on the Business Accounts (CR303000) form.")
      {
        ErrorValue = (object) baccount.AcctCD
      };
  }

  protected override void _(
    PX.Data.Events.FieldUpdated<PX.Objects.Extensions.MultiCurrency.Document, PX.Objects.Extensions.MultiCurrency.Document.bAccountID> e)
  {
    if (!e.ExternalCall && e.Row?.CuryID != null)
      return;
    this.SourceFieldUpdated<PX.Objects.Extensions.MultiCurrency.Document.curyInfoID, PX.Objects.Extensions.MultiCurrency.Document.curyID, PX.Objects.Extensions.MultiCurrency.Document.documentDate>(e.Cache, (IBqlTable) e.Row, !this.HasDetailRecords());
  }

  public virtual bool HasDetailRecords()
  {
    if (this.DetailsView == null)
      return false;
    PXCache cach = this.Base.Caches[typeof (TPrimary)];
    if (cach.Current == null)
      return false;
    return cach.GetStatus(cach.Current) == PXEntryStatus.Inserted ? this.DetailsView.Cache.IsDirty : this.DetailsView.SelectSingle() != null;
  }
}
