// Decompiled with JetBrains decompiler
// Type: PX.Objects.CS.SeparateBAccountMaint
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CR;
using PX.Objects.CR.Extensions.Relational;
using PX.Objects.GL;

#nullable disable
namespace PX.Objects.CS;

public class SeparateBAccountMaint : PXGraph<SeparateBAccountMaint, PX.Objects.CR.BAccount>
{
  [PXViewName("Customer")]
  public PXSelect<PX.Objects.CR.BAccount, Where<Match<Current<AccessInfo.userName>>>> BAccount;
  [PXHidden]
  public PXSelect<PX.Objects.CR.Standalone.Location> BaseLocations;
  [PXHidden]
  public PXSelect<PX.Objects.CR.Address> AddressDummy;
  [PXHidden]
  public PXSelect<Contact> ContactDummy;
  public PXSetup<Company> Commpany;

  [PXDimensionSelector("BRANCH", typeof (PX.Objects.CR.BAccount.acctCD), typeof (PX.Objects.CR.BAccount.acctCD), DescriptionField = typeof (PX.Objects.CR.BAccount.acctName))]
  [PXMergeAttributes]
  protected virtual void _(PX.Data.Events.CacheAttached<PX.Objects.CR.BAccount.acctCD> e)
  {
  }

  /// <exclude />
  public class SeparateDefSharedContactOverrideGraphExt : 
    SharedChildOverrideGraphExt<SeparateBAccountMaint, SeparateBAccountMaint.SeparateDefSharedContactOverrideGraphExt>
  {
    public override bool ViewHasADelegate => true;

    protected override CRParentChild<SeparateBAccountMaint, SeparateBAccountMaint.SeparateDefSharedContactOverrideGraphExt>.DocumentMapping GetDocumentMapping()
    {
      return new CRParentChild<SeparateBAccountMaint, SeparateBAccountMaint.SeparateDefSharedContactOverrideGraphExt>.DocumentMapping(typeof (PX.Objects.CR.Standalone.Location))
      {
        RelatedID = typeof (PX.Objects.CR.Standalone.Location.bAccountID),
        ChildID = typeof (PX.Objects.CR.Standalone.Location.defContactID),
        IsOverrideRelated = typeof (PX.Objects.CR.Standalone.Location.overrideContact)
      };
    }

    protected override SharedChildOverrideGraphExt<SeparateBAccountMaint, SeparateBAccountMaint.SeparateDefSharedContactOverrideGraphExt>.RelatedMapping GetRelatedMapping()
    {
      return new SharedChildOverrideGraphExt<SeparateBAccountMaint, SeparateBAccountMaint.SeparateDefSharedContactOverrideGraphExt>.RelatedMapping(typeof (PX.Objects.CR.BAccount))
      {
        RelatedID = typeof (PX.Objects.CR.BAccount.bAccountID),
        ChildID = typeof (PX.Objects.CR.BAccount.defContactID)
      };
    }

    protected override CRParentChild<SeparateBAccountMaint, SeparateBAccountMaint.SeparateDefSharedContactOverrideGraphExt>.ChildMapping GetChildMapping()
    {
      return new CRParentChild<SeparateBAccountMaint, SeparateBAccountMaint.SeparateDefSharedContactOverrideGraphExt>.ChildMapping(typeof (Contact))
      {
        ChildID = typeof (Contact.contactID),
        RelatedID = typeof (Contact.bAccountID)
      };
    }
  }

  /// <exclude />
  public class SeparateDefSharedAddressOverrideGraphExt : 
    SharedChildOverrideGraphExt<SeparateBAccountMaint, SeparateBAccountMaint.SeparateDefSharedAddressOverrideGraphExt>
  {
    public override bool ViewHasADelegate => true;

    protected override CRParentChild<SeparateBAccountMaint, SeparateBAccountMaint.SeparateDefSharedAddressOverrideGraphExt>.DocumentMapping GetDocumentMapping()
    {
      return new CRParentChild<SeparateBAccountMaint, SeparateBAccountMaint.SeparateDefSharedAddressOverrideGraphExt>.DocumentMapping(typeof (PX.Objects.CR.Standalone.Location))
      {
        RelatedID = typeof (PX.Objects.CR.Standalone.Location.bAccountID),
        ChildID = typeof (PX.Objects.CR.Standalone.Location.defAddressID),
        IsOverrideRelated = typeof (PX.Objects.CR.Standalone.Location.overrideAddress)
      };
    }

    protected override SharedChildOverrideGraphExt<SeparateBAccountMaint, SeparateBAccountMaint.SeparateDefSharedAddressOverrideGraphExt>.RelatedMapping GetRelatedMapping()
    {
      return new SharedChildOverrideGraphExt<SeparateBAccountMaint, SeparateBAccountMaint.SeparateDefSharedAddressOverrideGraphExt>.RelatedMapping(typeof (PX.Objects.CR.BAccount))
      {
        RelatedID = typeof (PX.Objects.CR.BAccount.bAccountID),
        ChildID = typeof (PX.Objects.CR.BAccount.defAddressID)
      };
    }

    protected override CRParentChild<SeparateBAccountMaint, SeparateBAccountMaint.SeparateDefSharedAddressOverrideGraphExt>.ChildMapping GetChildMapping()
    {
      return new CRParentChild<SeparateBAccountMaint, SeparateBAccountMaint.SeparateDefSharedAddressOverrideGraphExt>.ChildMapping(typeof (PX.Objects.CR.Address))
      {
        ChildID = typeof (PX.Objects.CR.Address.addressID),
        RelatedID = typeof (PX.Objects.CR.Address.bAccountID)
      };
    }
  }

  /// <exclude />
  public class DefContactAddressExt : 
    PX.Objects.CR.Extensions.DefContactAddressExt<SeparateBAccountMaint, PX.Objects.CR.BAccount, PX.Objects.CR.BAccount.acctName>
  {
  }

  /// <exclude />
  public class DefLocationExt : 
    PX.Objects.CR.Extensions.DefLocationExt<SeparateBAccountMaint, SeparateBAccountMaint.DefContactAddressExt, SeparateBAccountMaint.LocationDetailsExt, PX.Objects.CR.BAccount, PX.Objects.CR.BAccount.bAccountID, PX.Objects.CR.BAccount.defLocationID>.WithUIExtension
  {
  }

  /// <exclude />
  public class LocationDetailsExt : 
    PX.Objects.CR.Extensions.LocationDetailsExt<SeparateBAccountMaint, PX.Objects.CR.BAccount, PX.Objects.CR.BAccount.bAccountID>
  {
  }
}
