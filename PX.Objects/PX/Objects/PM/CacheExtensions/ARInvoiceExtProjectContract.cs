// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.CacheExtensions.ARInvoiceExtProjectContract
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.AR;
using PX.Objects.CS;
using PX.Objects.CT;

#nullable enable
namespace PX.Objects.PM.CacheExtensions;

/// <summary>
/// Extension that activates Project-related functionality for the <see cref="T:PX.Objects.AR.ARInvoice" /> when Project accounting module is enabled.
/// </summary>
public sealed class ARInvoiceExtProjectContract : PXCacheExtension<
#nullable disable
ARInvoice>
{
  /// <summary>
  /// Specifies (if set to <see langword="true" />) that the extension is active.
  /// </summary>
  public static bool IsActive()
  {
    return PXAccess.FeatureInstalled<FeaturesSet.contractManagement>() && PXAccess.FeatureInstalled<FeaturesSet.projectAccounting>();
  }

  /// <inheritdoc cref="P:PX.Objects.AR.ARInvoice.ProjectID" />
  [PXMergeAttributes]
  [PXRemoveBaseAttribute(typeof (NonProjectBaseAttribute))]
  [ActiveProjectOrContractBase(typeof (ARInvoice.customerID))]
  [PXRestrictor(typeof (Where<PMProject.visibleInAR, Equal<True>, Or<PMProject.nonProject, Equal<True>>>), "The '{0}' project is invisible in the module.", new System.Type[] {typeof (PMProject.contractCD)})]
  [PXRestrictor(typeof (Where<PMSetup.visibleInAR, Equal<True>, Or<PMProject.nonProject, Equal<True>, Or<PMProject.baseType, Equal<CTPRType.contract>, Or<PMProject.contractID, Equal<Current<ARInvoice.projectID>>>>>>), "The '{0}' project is invisible in the module.", new System.Type[] {typeof (PMProject.contractCD)})]
  [PXRemoveBaseAttribute(typeof (ProjectDefaultAttribute))]
  [ProjectDefault("AR", typeof (Search<PX.Objects.CR.Location.cDefProjectID, Where<PX.Objects.CR.Location.bAccountID, Equal<Current<ARInvoice.customerID>>, And<PX.Objects.CR.Location.locationID, Equal<Current<ARInvoice.customerLocationID>>>>>))]
  public int? ProjectID { get; set; }

  /// <inheritdoc cref="P:PX.Objects.AR.ARInvoice.ProjectID" />
  public abstract class projectID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    ARInvoiceExtProjectContract.projectID>
  {
  }
}
