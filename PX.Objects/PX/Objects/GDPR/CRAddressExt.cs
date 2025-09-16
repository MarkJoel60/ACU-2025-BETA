// Decompiled with JetBrains decompiler
// Type: PX.Objects.GDPR.CRAddressExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.CR;
using PX.Objects.CR.Standalone;
using System;

#nullable enable
namespace PX.Objects.GDPR;

[PXPersonalDataTable(typeof (SelectFromBase<CRAddress, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<CROpportunityRevision>.On<BqlOperand<CROpportunityRevision.opportunityAddressID, IBqlInt>.IsEqual<CRAddress.addressID>>>>.Where<BqlOperand<CROpportunityRevision.opportunityContactID, IBqlInt>.IsEqual<BqlField<CRContact.contactID, IBqlInt>.FromCurrent>>))]
[PXPersonalDataTable(typeof (SelectFromBase<CRAddress, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<CROpportunityRevision>.On<BqlOperand<CROpportunityRevision.shipAddressID, IBqlInt>.IsEqual<CRAddress.addressID>>>>.Where<BqlOperand<CROpportunityRevision.shipContactID, IBqlInt>.IsEqual<BqlField<CRContact.contactID, IBqlInt>.FromCurrent>>))]
[PXPersonalDataTable(typeof (SelectFromBase<CRAddress, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<CROpportunityRevision>.On<BqlOperand<CROpportunityRevision.billAddressID, IBqlInt>.IsEqual<CRAddress.addressID>>>>.Where<BqlOperand<CROpportunityRevision.billContactID, IBqlInt>.IsEqual<BqlField<CRContact.contactID, IBqlInt>.FromCurrent>>))]
[Serializable]
public sealed class CRAddressExt : PXCacheExtension<
#nullable disable
CRAddress>, IPseudonymizable
{
  [PXPseudonymizationStatusField]
  public int? PseudonymizationStatus { get; set; }

  public abstract class pseudonymizationStatus : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    CRAddressExt.pseudonymizationStatus>
  {
  }
}
