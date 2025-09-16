// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.DAC.OrganizationLedgerLink
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.GL.Attributes;
using System;

#nullable enable
namespace PX.Objects.GL.DAC;

/// <summary>
/// A many-to-many relation link between <see cref="T:PX.Objects.GL.DAC.Organization" /> and <see cref="T:PX.Objects.GL.Ledger" /> records.
/// </summary>
[Serializable]
public class OrganizationLedgerLink : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXParent(typeof (Select<Organization, Where<Organization.organizationID, Equal<Current<OrganizationLedgerLink.organizationID>>>>))]
  [Organization(true, typeof (Search<Organization.organizationID, Where<BqlOperand<Organization.organizationType, IBqlString>.IsNotEqual<OrganizationTypes.group>>>), null, IsKey = true, FieldClass = null)]
  public virtual int? OrganizationID { get; set; }

  /// <summary>
  /// The reference to the associated <see cref="T:PX.Objects.GL.Ledger" /> record.
  /// </summary>
  [PXDBInt(IsKey = true)]
  [PXUIField(DisplayName = "Ledger")]
  [PXSelector(typeof (Search<PX.Objects.GL.Ledger.ledgerID>), SubstituteKey = typeof (PX.Objects.GL.Ledger.ledgerCD), DescriptionField = typeof (PX.Objects.GL.Ledger.descr), DirtyRead = true)]
  [PXDBDefault(typeof (PX.Objects.GL.Ledger.ledgerID))]
  public virtual int? LedgerID { get; set; }

  public class PK : 
    PrimaryKeyOf<
    #nullable disable
    OrganizationLedgerLink>.By<OrganizationLedgerLink.organizationID, OrganizationLedgerLink.ledgerID>
  {
    public static OrganizationLedgerLink Find(
      PXGraph graph,
      int? organizationID,
      int? ledgerID,
      PKFindOptions options = 0)
    {
      return (OrganizationLedgerLink) PrimaryKeyOf<OrganizationLedgerLink>.By<OrganizationLedgerLink.organizationID, OrganizationLedgerLink.ledgerID>.FindBy(graph, (object) organizationID, (object) ledgerID, options);
    }
  }

  public static class FK
  {
    public class Organization : 
      PrimaryKeyOf<Organization>.By<Organization.organizationID>.ForeignKeyOf<OrganizationLedgerLink>.By<OrganizationLedgerLink.organizationID>
    {
    }

    public class Ledger : 
      PrimaryKeyOf<PX.Objects.GL.Ledger>.By<PX.Objects.GL.Ledger.ledgerID>.ForeignKeyOf<OrganizationLedgerLink>.By<OrganizationLedgerLink.ledgerID>
    {
    }
  }

  public abstract class organizationID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    OrganizationLedgerLink.organizationID>
  {
  }

  public abstract class ledgerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  OrganizationLedgerLink.ledgerID>
  {
  }
}
