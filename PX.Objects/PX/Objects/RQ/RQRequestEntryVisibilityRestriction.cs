// Decompiled with JetBrains decompiler
// Type: PX.Objects.RQ.RQRequestEntryVisibilityRestriction
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.AR;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.PO;

#nullable disable
namespace PX.Objects.RQ;

public class RQRequestEntryVisibilityRestriction : PXGraphExtension<RQRequestEntry>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.visibilityRestriction>();

  [PXMergeAttributes]
  [PXSelector(typeof (Search2<BAccount2.bAccountID, LeftJoin<PX.Objects.AP.Vendor, On<PX.Objects.AP.Vendor.bAccountID, Equal<BAccount2.bAccountID>, And<PX.Objects.AP.Vendor.type, NotEqual<BAccountType.employeeType>, And<PX.Objects.AP.Vendor.vOrgBAccountID, RestrictByUserBranches<Current<AccessInfo.userName>>>>>, LeftJoin<Customer, On<Customer.bAccountID, Equal<BAccount2.bAccountID>>, LeftJoin<PX.Objects.GL.Branch, On<PX.Objects.GL.Branch.bAccountID, Equal<BAccount2.bAccountID>>>>>, Where<PX.Objects.AP.Vendor.bAccountID, IsNotNull, And<Optional<RQRequest.shipDestType>, Equal<POShippingDestination.vendor>, Or<Where<PX.Objects.GL.Branch.bAccountID, IsNotNull, And<Optional<RQRequest.shipDestType>, Equal<POShippingDestination.company>, Or<Where<Customer.bAccountID, IsNotNull, And<Optional<RQRequest.shipDestType>, Equal<POShippingDestination.customer>>>>>>>>>>), new System.Type[] {typeof (PX.Objects.CR.BAccount.acctCD), typeof (PX.Objects.CR.BAccount.acctName), typeof (PX.Objects.CR.BAccount.type), typeof (PX.Objects.CR.BAccount.acctReferenceNbr), typeof (PX.Objects.CR.BAccount.parentBAccountID)}, SubstituteKey = typeof (PX.Objects.CR.BAccount.acctCD), DescriptionField = typeof (PX.Objects.CR.BAccount.acctName))]
  public virtual void RQRequest_ShipToBAccountID_CacheAttached(PXCache sender)
  {
  }
}
