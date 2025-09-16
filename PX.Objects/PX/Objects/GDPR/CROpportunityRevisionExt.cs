// Decompiled with JetBrains decompiler
// Type: PX.Objects.GDPR.CROpportunityRevisionExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CR;
using PX.Objects.CR.Standalone;

#nullable enable
namespace PX.Objects.GDPR;

[PXPersonalDataTable(typeof (Select<CROpportunityRevision, Where<CROpportunityRevision.opportunityContactID, Equal<Current<CRContact.contactID>>>>))]
[PXPersonalDataTable(typeof (Select<CROpportunityRevision, Where<CROpportunityRevision.shipContactID, Equal<Current<CRContact.contactID>>>>))]
[PXPersonalDataTable(typeof (Select<CROpportunityRevision, Where<CROpportunityRevision.billContactID, Equal<Current<CRContact.contactID>>>>))]
public sealed class CROpportunityRevisionExt : PXCacheExtension<
#nullable disable
CROpportunityRevision>
{
  [PXPersonalDataTableAnchor]
  [PXMergeAttributes]
  public string OpportunityID
  {
    get => this.Base.OpportunityID;
    set => this.Base.OpportunityID = value;
  }

  [PXPseudonymizationStatusField]
  public int? PseudonymizationStatus { get; set; }

  public abstract class pseudonymizationStatus : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    CROpportunityRevisionExt.pseudonymizationStatus>
  {
  }
}
