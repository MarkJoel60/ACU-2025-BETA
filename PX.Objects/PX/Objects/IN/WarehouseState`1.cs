// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.WarehouseState`1
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.BarcodeProcessing;
using PX.Common;
using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.Objects.IN;

public abstract class WarehouseState<TScanBasis> : EntityState<
#nullable disable
TScanBasis, INSite> where TScanBasis : PXGraphExtension, IBarcodeDrivenStateMachine
{
  public const string Value = "SITE";

  public virtual string Code => "SITE";

  protected virtual string StatePrompt => "Scan the warehouse.";

  protected abstract int? SiteID { get; set; }

  protected abstract bool UseDefaultWarehouse { get; }

  protected virtual int? DefaultSiteID
  {
    get => UserPreferenceExt.GetDefaultSite(((ScanComponent<TScanBasis>) this).Basis.Graph);
  }

  protected virtual bool IsStateSkippable() => this.SiteID.HasValue;

  protected virtual void OnTakingOver()
  {
    if (!((ScanState<TScanBasis>) this).IsActive || ((ScanState<TScanBasis>) this).IsSkippable || !this.UseDefaultWarehouse || !this.DefaultSiteID.HasValue)
      return;
    INSite inSite = INSite.PK.Find(((ScanComponent<TScanBasis>) this).Basis.Graph, this.DefaultSiteID);
    if (inSite == null)
      return;
    ((ScanState<TScanBasis>) this).Process(inSite.SiteCD);
  }

  protected virtual INSite GetByBarcode(string barcode)
  {
    return INSite.UK.Find(((ScanComponent<TScanBasis>) this).Basis.Graph, barcode);
  }

  protected virtual void ReportMissing(string barcode)
  {
    ((ScanComponent<TScanBasis>) this).Basis.Reporter.Error("{0} warehouse not found.", new object[1]
    {
      (object) barcode
    });
  }

  protected virtual void Apply(INSite site) => this.SiteID = site.SiteID;

  protected virtual void ClearState() => this.SiteID = new int?();

  protected virtual void ReportSuccess(INSite site)
  {
    ((ScanComponent<TScanBasis>) this).Basis.Reporter.Info("{0} warehouse selected.", new object[1]
    {
      (object) site.SiteCD
    });
  }

  public class value : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  WarehouseState<TScanBasis>.value>
  {
    public value()
      : base("SITE")
    {
    }
  }

  [PXLocalizable]
  public abstract class Msg
  {
    public const string Prompt = "Scan the warehouse.";
    public const string Ready = "{0} warehouse selected.";
    public const string Missing = "{0} warehouse not found.";
  }
}
