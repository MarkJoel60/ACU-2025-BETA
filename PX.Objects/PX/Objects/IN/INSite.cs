// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.INSite
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Data.EP;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CS;
using PX.Objects.GL;
using PX.SM;
using System;

#nullable enable
namespace PX.Objects.IN;

[PXPrimaryGraph(new System.Type[] {typeof (INSiteMaint)}, new System.Type[] {typeof (SelectFromBase<INSite, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<INSite.siteID, IBqlInt>.IsEqual<BqlField<INSite.siteID, IBqlInt>.FromCurrent>>)})]
[PXCacheName]
public class INSite : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage, IIncludable, IRestricted
{
  public const 
  #nullable disable
  string Main = "MAIN";

  [PXDBForeignIdentity(typeof (INCostSite))]
  [PXReferentialIntegrityCheck]
  public virtual int? SiteID { get; set; }

  [PXRestrictor(typeof (Where<BqlOperand<INSite.siteID, IBqlInt>.IsNotEqual<SiteAnyAttribute.transitSiteID>>), "The warehouse cannot be selected; it is used for transit.", new System.Type[] {})]
  [SiteRaw(true, IsKey = true)]
  [PXDefault]
  [PXFieldDescription]
  public virtual string SiteCD { get; set; }

  [PXDBString(60, IsUnicode = true)]
  [PXUIField]
  [PXFieldDescription]
  public virtual string Descr { get; set; }

  [SubAccount]
  public virtual int? ReasonCodeSubID { get; set; }

  [Account]
  [PXForeignReference(typeof (INSite.FK.SalesAccount))]
  public virtual int? SalesAcctID { get; set; }

  [SubAccount(typeof (INSite.salesAcctID))]
  [PXForeignReference(typeof (INSite.FK.SalesSubaccount))]
  public virtual int? SalesSubID { get; set; }

  [PXDefault]
  [Account]
  [PXForeignReference(typeof (INSite.FK.InventoryAccount))]
  public virtual int? InvtAcctID { get; set; }

  [PXDefault]
  [SubAccount(typeof (INSite.invtAcctID))]
  [PXForeignReference(typeof (INSite.FK.InventorySubaccount))]
  public virtual int? InvtSubID { get; set; }

  [Account]
  [PXForeignReference(typeof (INSite.FK.COGSAccount))]
  public virtual int? COGSAcctID { get; set; }

  [SubAccount(typeof (INSite.cOGSAcctID))]
  [PXForeignReference(typeof (INSite.FK.COGSSubaccount))]
  public virtual int? COGSSubID { get; set; }

  [Account]
  [PXForeignReference(typeof (INSite.FK.StandardCostRevaluationAccount))]
  public virtual int? StdCstRevAcctID { get; set; }

  [SubAccount(typeof (INSite.stdCstRevAcctID))]
  [PXForeignReference(typeof (INSite.FK.StandardCostRevaluationSubaccount))]
  public virtual int? StdCstRevSubID { get; set; }

  [Account]
  [PXForeignReference(typeof (INSite.FK.StandardCostVarianceAccount))]
  public virtual int? StdCstVarAcctID { get; set; }

  [SubAccount(typeof (INSite.stdCstVarAcctID))]
  [PXForeignReference(typeof (INSite.FK.StandardCostVarianceSubaccount))]
  public virtual int? StdCstVarSubID { get; set; }

  [Account]
  [PXForeignReference(typeof (INSite.FK.PPVAccount))]
  public virtual int? PPVAcctID { get; set; }

  [SubAccount(typeof (INSite.pPVAcctID))]
  [PXForeignReference(typeof (INSite.FK.PPVSubaccount))]
  public virtual int? PPVSubID { get; set; }

  [Account]
  [PXForeignReference(typeof (INSite.FK.POAccrualAccount))]
  public virtual int? POAccrualAcctID { get; set; }

  [SubAccount(typeof (INSite.pOAccrualAcctID))]
  [PXForeignReference(typeof (INSite.FK.POAccrualSubaccount))]
  public virtual int? POAccrualSubID { get; set; }

  [Account]
  [PXForeignReference(typeof (INSite.FK.LandedCostVarianceAccount))]
  public virtual int? LCVarianceAcctID { get; set; }

  [SubAccount(typeof (INSite.lCVarianceAcctID))]
  [PXForeignReference(typeof (INSite.FK.LandedCostVarianceSubaccount))]
  public virtual int? LCVarianceSubID { get; set; }

  [PXRestrictor(typeof (Where<BqlOperand<INLocation.active, IBqlBool>.IsEqual<True>>), "Location is not Active.", new System.Type[] {})]
  [Location(typeof (INSite.siteID), DisplayName = "Receiving Location", DescriptionField = typeof (INLocation.descr), DirtyRead = true)]
  public virtual int? ReceiptLocationID { get; set; }

  [PXBool]
  public virtual bool? ReceiptLocationIDOverride { get; set; }

  [PXRestrictor(typeof (Where<BqlOperand<INLocation.active, IBqlBool>.IsEqual<True>>), "Location is not Active.", new System.Type[] {})]
  [Location(typeof (INSite.siteID), DisplayName = "Shipping Location", DescriptionField = typeof (INLocation.descr), DirtyRead = true)]
  public virtual int? ShipLocationID { get; set; }

  [PXBool]
  public virtual bool? ShipLocationIDOverride { get; set; }

  [PXRestrictor(typeof (Where<BqlOperand<INLocation.active, IBqlBool>.IsEqual<True>>), "Location is not Active.", new System.Type[] {})]
  [Location(typeof (INSite.siteID), DisplayName = "Drop-Ship Location", DescriptionField = typeof (INLocation.descr), DirtyRead = true)]
  public virtual int? DropShipLocationID { get; set; }

  [PXRestrictor(typeof (Where<BqlOperand<INLocation.active, IBqlBool>.IsEqual<True>>), "Location is not Active.", new System.Type[] {})]
  [Location(typeof (INSite.siteID), DisplayName = "RMA Location", DescriptionField = typeof (INLocation.descr), DirtyRead = true)]
  public virtual int? ReturnLocationID { get; set; }

  [PXDBString(1, IsFixed = true)]
  [PXUIField(DisplayName = "Location Entry")]
  [INLocationValid.List]
  [PXDefault("V")]
  public virtual string LocationValid { get; set; }

  [Branch(null, null, true, true, true)]
  public virtual int? BranchID { get; set; }

  [PXDBInt]
  [PXFormula(typeof (Selector<INSite.branchID, PX.Objects.GL.Branch.bAccountID>))]
  [PXDefault]
  public virtual int? BAccountID { get; set; }

  [PXDBString(5, IsUnicode = true)]
  [PXFormula(typeof (Selector<INSite.branchID, PX.Objects.GL.Branch.baseCuryID>))]
  [PXUIField(DisplayName = "Base Currency ID", Enabled = false)]
  public virtual string BaseCuryID { get; set; }

  [PXDBInt]
  [PXDBChildIdentity(typeof (PX.Objects.CR.Address.addressID))]
  [PXForeignReference(typeof (INSite.FK.Address))]
  public virtual int? AddressID { get; set; }

  [PXDBInt]
  [PXDBChildIdentity(typeof (PX.Objects.CR.Contact.contactID))]
  [PXForeignReference(typeof (INSite.FK.Contact))]
  public virtual int? ContactID { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "Building ID")]
  [PXForeignReference(typeof (INSite.FK.Building))]
  [PXSelector(typeof (SearchFor<INSiteBuilding.buildingID>.Where<BqlOperand<INSiteBuilding.branchID, IBqlInt>.IsEqual<BqlField<INSite.branchID, IBqlInt>.FromCurrent>>), SubstituteKey = typeof (INSiteBuilding.buildingCD), DescriptionField = typeof (INSiteBuilding.descr))]
  public virtual int? BuildingID { get; set; }

  [PXNote(DescriptionField = typeof (INSite.siteCD), Selector = typeof (INSite.siteCD), FieldList = new System.Type[] {typeof (INSite.siteCD), typeof (INSite.descr), typeof (INSite.replenishmentClassID)})]
  public virtual Guid? NoteID { get; set; }

  [PXDBString(10, IsUnicode = true, InputMask = ">aaaaaaaaaa")]
  [PXUIField]
  [PXSelector(typeof (Search<INReplenishmentClass.replenishmentClassID>), DescriptionField = typeof (INReplenishmentClass.descr))]
  public virtual string ReplenishmentClassID { get; set; }

  [PXDBString(1, IsFixed = true)]
  [PXDefault("A")]
  [INSite.avgDefaultCost.List]
  [PXUIField(DisplayName = "Average Default Cost")]
  public virtual string AvgDefaultCost { get; set; }

  [PXDBString(1, IsFixed = true)]
  [PXDefault("A")]
  [INSite.avgDefaultCost.List]
  [PXUIField(DisplayName = "FIFO Default Cost")]
  public virtual string FIFODefaultCost { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Override Inventory Account/Sub.")]
  public virtual bool? OverrideInvtAccSub { get; set; }

  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Active")]
  public virtual bool? Active { get; set; }

  [PXDBCreatedByID]
  public virtual Guid? CreatedByID { get; set; }

  [PXDBCreatedByScreenID]
  public virtual string CreatedByScreenID { get; set; }

  [PXDBCreatedDateTime]
  [PXUIField(DisplayName = "Created On", Enabled = false, IsReadOnly = true)]
  public virtual DateTime? CreatedDateTime { get; set; }

  [PXDBLastModifiedByID]
  public virtual Guid? LastModifiedByID { get; set; }

  [PXDBLastModifiedByScreenID]
  public virtual string LastModifiedByScreenID { get; set; }

  [PXDBLastModifiedDateTime]
  [PXUIField(DisplayName = "Last Modified On", Enabled = false, IsReadOnly = true)]
  public virtual DateTime? LastModifiedDateTime { get; set; }

  [PXDBTimestamp]
  public virtual byte[] tstamp { get; set; }

  [PXDBGroupMask]
  public virtual byte[] GroupMask { get; set; }

  [PXBool]
  [PXUIField(DisplayName = "Included")]
  [PXUnboundDefault(false)]
  public virtual bool? Included { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Use Item Default Location for Picking")]
  public virtual bool? UseItemDefaultLocationForPicking { get; set; }

  [Location(typeof (INSite.siteID), DisplayName = "Non-Stock Location", DescriptionField = typeof (INLocation.descr), DirtyRead = true)]
  [PXRestrictor(typeof (Where<BqlOperand<INLocation.active, IBqlBool>.IsEqual<True>>), "Location is not Active.", new System.Type[] {})]
  public virtual int? NonStockPickingLocationID { get; set; }

  [PXDBString(50, IsUnicode = true)]
  [PXUIField(DisplayName = "Carrier Facility")]
  public virtual string CarrierFacility { get; set; }

  [PXInt]
  [Obsolete("The field is preserved for the support of the legacy Default Endpoints only. Last endpoint that uses this field: 20.200.001. This field will be deleted once the 20.200.001 endpoint become obsolete.")]
  public virtual int? DiscAcctID
  {
    get => new int?();
    set
    {
    }
  }

  [PXInt]
  [Obsolete("The field is preserved for the support of the legacy Default Endpoints only. Last endpoint that uses this field: 20.200.001. This field will be deleted once the 20.200.001 endpoint become obsolete.")]
  public virtual int? DiscSubID
  {
    get => new int?();
    set
    {
    }
  }

  [PXInt]
  [Obsolete("The field is preserved for the support of the legacy Default Endpoints only. Last endpoint that uses this field: 20.200.001. This field will be deleted once the 20.200.001 endpoint become obsolete.")]
  public virtual int? FreightAcctID
  {
    get => new int?();
    set
    {
    }
  }

  [PXInt]
  [Obsolete("The field is preserved for the support of the legacy Default Endpoints only. Last endpoint that uses this field: 20.200.001. This field will be deleted once the 20.200.001 endpoint become obsolete.")]
  public virtual int? FreightSubID
  {
    get => new int?();
    set
    {
    }
  }

  [PXInt]
  [Obsolete("The field is preserved for the support of the legacy Default Endpoints only. Last endpoint that uses this field: 20.200.001. This field will be deleted once the 20.200.001 endpoint become obsolete.")]
  public virtual int? MiscAcctID
  {
    get => new int?();
    set
    {
    }
  }

  [PXInt]
  [Obsolete("The field is preserved for the support of the legacy Default Endpoints only. Last endpoint that uses this field: 20.200.001. This field will be deleted once the 20.200.001 endpoint become obsolete.")]
  public virtual int? MiscSubID
  {
    get => new int?();
    set
    {
    }
  }

  public class PK : PrimaryKeyOf<INSite>.By<INSite.siteID>.Dirty
  {
    public static INSite Find(PXGraph graph, int? siteID, PKFindOptions options = 0)
    {
      return (INSite) PrimaryKeyOf<INSite>.By<INSite.siteID>.Dirty.FindBy(graph, (object) siteID, options != null || siteID.GetValueOrDefault() > 0 ? options : (PKFindOptions) (object) 1);
    }
  }

  public class UK : PrimaryKeyOf<INSite>.By<INSite.siteCD>
  {
    public static INSite Find(PXGraph graph, string siteCD, PKFindOptions options = 0)
    {
      return (INSite) PrimaryKeyOf<INSite>.By<INSite.siteCD>.FindBy(graph, (object) siteCD, options);
    }
  }

  public static class FK
  {
    public class InventoryAccount : 
      PrimaryKeyOf<PX.Objects.GL.Account>.By<PX.Objects.GL.Account.accountID>.ForeignKeyOf<INSite>.By<INSite.invtAcctID>
    {
    }

    public class InventorySubaccount : 
      PrimaryKeyOf<PX.Objects.GL.Sub>.By<PX.Objects.GL.Sub.subID>.ForeignKeyOf<INSite>.By<INSite.invtSubID>
    {
    }

    public class COGSAccount : 
      PrimaryKeyOf<PX.Objects.GL.Account>.By<PX.Objects.GL.Account.accountID>.ForeignKeyOf<INSite>.By<INSite.cOGSAcctID>
    {
    }

    public class COGSSubaccount : 
      PrimaryKeyOf<PX.Objects.GL.Sub>.By<PX.Objects.GL.Sub.subID>.ForeignKeyOf<INSite>.By<INSite.cOGSSubID>
    {
    }

    public class SalesAccount : 
      PrimaryKeyOf<PX.Objects.GL.Account>.By<PX.Objects.GL.Account.accountID>.ForeignKeyOf<INSite>.By<INSite.salesAcctID>
    {
    }

    public class SalesSubaccount : 
      PrimaryKeyOf<PX.Objects.GL.Sub>.By<PX.Objects.GL.Sub.subID>.ForeignKeyOf<INSite>.By<INSite.salesSubID>
    {
    }

    public class StandardCostRevaluationAccount : 
      PrimaryKeyOf<PX.Objects.GL.Account>.By<PX.Objects.GL.Account.accountID>.ForeignKeyOf<INSite>.By<INSite.stdCstRevAcctID>
    {
    }

    public class StandardCostRevaluationSubaccount : 
      PrimaryKeyOf<PX.Objects.GL.Sub>.By<PX.Objects.GL.Sub.subID>.ForeignKeyOf<INSite>.By<INSite.stdCstRevSubID>
    {
    }

    public class PPVAccount : 
      PrimaryKeyOf<PX.Objects.GL.Account>.By<PX.Objects.GL.Account.accountID>.ForeignKeyOf<INSite>.By<INSite.pPVAcctID>
    {
    }

    public class PPVSubaccount : 
      PrimaryKeyOf<PX.Objects.GL.Sub>.By<PX.Objects.GL.Sub.subID>.ForeignKeyOf<INSite>.By<INSite.pPVSubID>
    {
    }

    public class POAccrualAccount : 
      PrimaryKeyOf<PX.Objects.GL.Account>.By<PX.Objects.GL.Account.accountID>.ForeignKeyOf<INSite>.By<INSite.pOAccrualAcctID>
    {
    }

    public class POAccrualSubaccount : 
      PrimaryKeyOf<PX.Objects.GL.Sub>.By<PX.Objects.GL.Sub.subID>.ForeignKeyOf<INSite>.By<INSite.pOAccrualSubID>
    {
    }

    public class StandardCostVarianceAccount : 
      PrimaryKeyOf<PX.Objects.GL.Account>.By<PX.Objects.GL.Account.accountID>.ForeignKeyOf<INSite>.By<INSite.stdCstVarAcctID>
    {
    }

    public class StandardCostVarianceSubaccount : 
      PrimaryKeyOf<PX.Objects.GL.Sub>.By<PX.Objects.GL.Sub.subID>.ForeignKeyOf<INSite>.By<INSite.stdCstVarSubID>
    {
    }

    public class LandedCostVarianceAccount : 
      PrimaryKeyOf<PX.Objects.GL.Account>.By<PX.Objects.GL.Account.accountID>.ForeignKeyOf<INSite>.By<INSite.lCVarianceAcctID>
    {
    }

    public class LandedCostVarianceSubaccount : 
      PrimaryKeyOf<PX.Objects.GL.Sub>.By<PX.Objects.GL.Sub.subID>.ForeignKeyOf<INSite>.By<INSite.lCVarianceSubID>
    {
    }

    public class ReasonCodeSubaccount : 
      PrimaryKeyOf<PX.Objects.GL.Sub>.By<PX.Objects.GL.Sub.subID>.ForeignKeyOf<INSite>.By<INSite.reasonCodeSubID>
    {
    }

    public class ReceiptLocation : 
      PrimaryKeyOf<INLocation>.By<INLocation.locationID>.ForeignKeyOf<INSite>.By<INSite.receiptLocationID>
    {
    }

    public class ShipLocation : 
      PrimaryKeyOf<INLocation>.By<INLocation.locationID>.ForeignKeyOf<INSite>.By<INSite.shipLocationID>
    {
    }

    public class ReturnLocation : 
      PrimaryKeyOf<INLocation>.By<INLocation.locationID>.ForeignKeyOf<INSite>.By<INSite.returnLocationID>
    {
    }

    public class DropShipLocation : 
      PrimaryKeyOf<INLocation>.By<INLocation.locationID>.ForeignKeyOf<INSite>.By<INSite.dropShipLocationID>
    {
    }

    public class Branch : 
      PrimaryKeyOf<PX.Objects.GL.Branch>.By<PX.Objects.GL.Branch.branchID>.ForeignKeyOf<INSite>.By<INSite.branchID>
    {
    }

    public class ReplenishmentClass : 
      PrimaryKeyOf<INReplenishmentClass>.By<INReplenishmentClass.replenishmentClassID>.ForeignKeyOf<INSite>.By<INSite.replenishmentClassID>
    {
    }

    public class Address : 
      PrimaryKeyOf<PX.Objects.CR.Address>.By<PX.Objects.CR.Address.addressID>.ForeignKeyOf<INSite>.By<INSite.addressID>
    {
    }

    public class Contact : 
      PrimaryKeyOf<PX.Objects.CR.Contact>.By<PX.Objects.CR.Contact.contactID>.ForeignKeyOf<INSite>.By<INSite.contactID>
    {
    }

    public class Building : 
      PrimaryKeyOf<INSiteBuilding>.By<INSiteBuilding.buildingID>.ForeignKeyOf<INSite>.By<INSite.buildingID>
    {
    }
  }

  public abstract class siteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INSite.siteID>
  {
  }

  public abstract class siteCD : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INSite.siteCD>
  {
  }

  public abstract class descr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INSite.descr>
  {
  }

  public abstract class reasonCodeSubID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INSite.reasonCodeSubID>
  {
  }

  public abstract class salesAcctID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INSite.salesAcctID>
  {
  }

  public abstract class salesSubID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INSite.salesSubID>
  {
  }

  public abstract class invtAcctID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INSite.invtAcctID>
  {
  }

  public abstract class invtSubID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INSite.invtSubID>
  {
  }

  public abstract class cOGSAcctID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INSite.cOGSAcctID>
  {
  }

  public abstract class cOGSSubID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INSite.cOGSSubID>
  {
  }

  public abstract class stdCstRevAcctID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INSite.stdCstRevAcctID>
  {
  }

  public abstract class stdCstRevSubID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INSite.stdCstRevSubID>
  {
  }

  public abstract class stdCstVarAcctID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INSite.stdCstVarAcctID>
  {
  }

  public abstract class stdCstVarSubID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INSite.stdCstVarSubID>
  {
  }

  public abstract class pPVAcctID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INSite.pPVAcctID>
  {
  }

  public abstract class pPVSubID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INSite.pPVSubID>
  {
  }

  public abstract class pOAccrualAcctID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INSite.pOAccrualAcctID>
  {
  }

  public abstract class pOAccrualSubID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INSite.pOAccrualSubID>
  {
  }

  public abstract class lCVarianceAcctID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INSite.lCVarianceAcctID>
  {
  }

  public abstract class lCVarianceSubID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INSite.lCVarianceSubID>
  {
  }

  public abstract class receiptLocationID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INSite.receiptLocationID>
  {
  }

  public abstract class receiptLocationIDOverride : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    INSite.receiptLocationIDOverride>
  {
  }

  public abstract class shipLocationID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INSite.shipLocationID>
  {
  }

  public abstract class shipLocationIDOverride : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    INSite.shipLocationIDOverride>
  {
  }

  public abstract class dropShipLocationID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INSite.dropShipLocationID>
  {
  }

  public abstract class returnLocationID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INSite.returnLocationID>
  {
  }

  public abstract class locationValid : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INSite.locationValid>
  {
  }

  public abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INSite.branchID>
  {
    public class PreventEditIfSOExists : 
      EditPreventor<TypeArrayOf<IBqlField>.FilledWith<INSite.branchID>>.On<INSiteMaint>.IfExists<SelectFromBase<INCostStatus, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
      #nullable enable
      INCostStatus.siteID, 
      #nullable disable
      Equal<BqlField<
      #nullable enable
      INSite.siteID, IBqlInt>.FromCurrent>>>>>.And<
      #nullable disable
      BqlOperand<
      #nullable enable
      INCostStatus.qtyOnHand, IBqlDecimal>.IsNotEqual<
      #nullable disable
      decimal0>>>>
    {
      protected virtual string CreateEditPreventingReason(
        GetEditPreventingReasonArgs arg,
        object firstPreventingEntity,
        string fieldName,
        string currentTableName,
        string foreignTableName)
      {
        return PXMessages.Localize("The branch cannot be changed because the warehouse has items in stock.");
      }
    }

    public class PreventEditIfHistoryExists : 
      EditPreventor<TypeArrayOf<IBqlField>.FilledWith<INSite.branchID>>.On<INSiteMaint>.IfExists<SelectFromBase<INItemStats, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<
      #nullable enable
      INItemStats.siteID, IBqlInt>.IsEqual<
      #nullable disable
      BqlField<
      #nullable enable
      INSite.siteID, IBqlInt>.FromCurrent>>>
    {
      public static bool IsActive()
      {
        return PXAccess.FeatureInstalled<FeaturesSet.multipleBaseCurrencies>();
      }

      protected virtual 
      #nullable disable
      string CreateEditPreventingReason(
        GetEditPreventingReasonArgs arg,
        object firstPreventingEntity,
        string fieldName,
        string currentTableName,
        string foreignTableName)
      {
        INSite row = arg.Row as INSite;
        PX.Objects.GL.Branch branch = PX.Objects.GL.Branch.PK.Find((PXGraph) ((PXGraphExtension<INSiteMaint>) this).Base, arg.NewValue as int?);
        if (row?.BaseCuryID == branch.BaseCuryID)
          return (string) null;
        return PXMessages.LocalizeFormat("Cannot change the branch as the transactions history of the {0} warehouse has transactions in the {1} currency.", new object[2]
        {
          (object) row?.SiteCD,
          (object) row?.BaseCuryID
        });
      }
    }

    public class ExtensionSort : 
      SortExtensionsBy<TypeArrayOf<PXGraphExtension<INSiteMaint>>.FilledWith<INSite.branchID.PreventEditIfHistoryExists, INSite.branchID.PreventEditIfSOExists>>
    {
    }
  }

  public abstract class bAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INSite.bAccountID>
  {
  }

  public abstract class baseCuryID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INSite.baseCuryID>
  {
  }

  public abstract class addressID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INSite.addressID>
  {
  }

  public abstract class contactID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INSite.contactID>
  {
  }

  public abstract class buildingID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INSite.buildingID>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  INSite.noteID>
  {
  }

  public abstract class replenishmentClassID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INSite.replenishmentClassID>
  {
  }

  public abstract class avgDefaultCost : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INSite.avgDefaultCost>
  {
    public const string AverageCost = "A";
    public const string LastCost = "L";

    public class ListAttribute : PXStringListAttribute
    {
      public ListAttribute()
        : base(new Tuple<string, string>[2]
        {
          PXStringListAttribute.Pair("A", "Average"),
          PXStringListAttribute.Pair("L", "Last")
        })
      {
      }
    }

    public class averageCost : 
      BqlType<
      #nullable enable
      IBqlString, string>.Constant<
      #nullable disable
      INSite.avgDefaultCost.averageCost>
    {
      public averageCost()
        : base("A")
      {
      }
    }

    public class lastCost : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    INSite.avgDefaultCost.lastCost>
    {
      public lastCost()
        : base("L")
      {
      }
    }
  }

  public abstract class fIFODefaultCost : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INSite.fIFODefaultCost>
  {
  }

  public abstract class overrideInvtAccSub : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  INSite.overrideInvtAccSub>
  {
  }

  public abstract class active : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  INSite.active>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  INSite.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INSite.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    INSite.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  INSite.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INSite.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    INSite.lastModifiedDateTime>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  INSite.Tstamp>
  {
  }

  public abstract class groupMask : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  INSite.groupMask>
  {
  }

  public abstract class included : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  INSite.included>
  {
  }

  public abstract class useItemDefaultLocationForPicking : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    INSite.useItemDefaultLocationForPicking>
  {
  }

  public abstract class nonStockPickingLocationID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    INSite.nonStockPickingLocationID>
  {
  }

  public abstract class carrierFacility : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INSite.carrierFacility>
  {
  }

  [Obsolete("The field is preserved for the support of the legacy Default Endpoints only. Last endpoint that uses this field: 20.200.001. This field will be deleted once the 20.200.001 endpoint become obsolete.")]
  public abstract class discAcctID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INSite.discAcctID>
  {
  }

  [Obsolete("The field is preserved for the support of the legacy Default Endpoints only. Last endpoint that uses this field: 20.200.001. This field will be deleted once the 20.200.001 endpoint become obsolete.")]
  public abstract class discSubID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INSite.discSubID>
  {
  }

  [Obsolete("The field is preserved for the support of the legacy Default Endpoints only. Last endpoint that uses this field: 20.200.001. This field will be deleted once the 20.200.001 endpoint become obsolete.")]
  public abstract class freightAcctID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INSite.freightAcctID>
  {
  }

  [Obsolete("The field is preserved for the support of the legacy Default Endpoints only. Last endpoint that uses this field: 20.200.001. This field will be deleted once the 20.200.001 endpoint become obsolete.")]
  public abstract class freightSubID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INSite.freightSubID>
  {
  }

  [Obsolete("The field is preserved for the support of the legacy Default Endpoints only. Last endpoint that uses this field: 20.200.001. This field will be deleted once the 20.200.001 endpoint become obsolete.")]
  public abstract class miscAcctID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INSite.miscAcctID>
  {
  }

  [Obsolete("The field is preserved for the support of the legacy Default Endpoints only. Last endpoint that uses this field: 20.200.001. This field will be deleted once the 20.200.001 endpoint become obsolete.")]
  public abstract class miscSubID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INSite.miscSubID>
  {
  }

  public class main : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  INSite.main>
  {
    public main()
      : base("MAIN")
    {
    }
  }
}
