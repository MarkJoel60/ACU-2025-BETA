// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.FSRouteContractScheduleFSServiceContract
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CS;
using System;

#nullable enable
namespace PX.Objects.FS;

/// <exclude />
///     // Acuminator disable once PX1094 NoPXHiddenOrPXCacheNameOnDac - legacy code
[PXProjection(typeof (Select2<FSRouteContractSchedule, InnerJoin<FSServiceContract, On<FSRouteContractSchedule.entityID, Equal<FSServiceContract.serviceContractID>, And<FSRouteContractSchedule.entityType, Equal<ListField_Schedule_EntityType.Contract>>>, InnerJoin<PX.Objects.AR.Customer, On<PX.Objects.AR.Customer.bAccountID, Equal<FSServiceContract.customerID>>, LeftJoin<PX.Objects.CR.Location, On<PX.Objects.CR.Location.locationID, Equal<FSServiceContract.customerLocationID>>>>>, Where<FSServiceContract.recordType, Equal<ListField_RecordType_ContractSchedule.RouteServiceContract>, And<FSServiceContract.status, Equal<ListField_Status_ServiceContract.Active>>>>))]
[PXGroupMask(typeof (InnerJoin<PX.Objects.AR.Customer, On<PX.Objects.AR.Customer.bAccountID, Equal<FSRouteContractSchedule.customerID>, And<Match<PX.Objects.AR.Customer, Current<AccessInfo.userName>>>>>))]
[Serializable]
public class FSRouteContractScheduleFSServiceContract : FSRouteContractSchedule
{
  [LocationID(typeof (Where<PX.Objects.CR.Location.bAccountID, Equal<Optional<FSRouteContractSchedule.customerID>>, And<PX.Objects.CR.Location.isActive, Equal<True>, And<MatchWithBranch<PX.Objects.CR.Location.cBranchID>>>>))]
  public override int? CustomerLocationID { get; set; }

  [PXDBString(15, IsKey = true, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC", BqlField = typeof (FSServiceContract.refNbr))]
  [PXUIField]
  [PXSelector(typeof (Search<FSServiceContract.refNbr, Where<FSServiceContract.recordType, Equal<ListField_RecordType_ContractSchedule.RouteServiceContract>>, OrderBy<Desc<FSServiceContract.refNbr>>>))]
  [AutoNumber(typeof (Search<FSSetup.serviceContractNumberingID>), typeof (AccessInfo.businessDate))]
  public virtual 
  #nullable disable
  string ServiceContractRefNbr { get; set; }

  [PXDBString(15, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC", BqlField = typeof (FSServiceContract.customerContractNbr))]
  [PXUIField(DisplayName = "Customer Contract Nbr.")]
  public virtual string CustomerContractNbr { get; set; }

  [PXDBString(255 /*0xFF*/, IsUnicode = true, BqlField = typeof (FSServiceContract.docDesc))]
  [PXUIField(DisplayName = "Description")]
  public virtual string DocDesc { get; set; }

  [PXDBString(15, IsUnicode = true, IsKey = true, InputMask = ">CCCCCCCCCCCCCCC")]
  [PXUIField]
  [PXSelector(typeof (Search<FSRouteContractSchedule.refNbr, Where<FSRouteContractSchedule.entityID, Equal<Current<FSRouteContractSchedule.entityID>>, And<FSRouteContractSchedule.entityType, Equal<ListField_Schedule_EntityType.Contract>>>, OrderBy<Desc<FSRouteContractSchedule.refNbr>>>), CacheGlobal = true)]
  public override string RefNbr { get; set; }

  [PXString]
  public override string FormCaptionDescription { get; set; }

  public new abstract class customerLocationID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSRouteContractScheduleFSServiceContract.customerLocationID>
  {
  }

  public abstract class serviceContractRefNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSRouteContractScheduleFSServiceContract.serviceContractRefNbr>
  {
  }

  public abstract class customerContractNbr : IBqlField, IBqlOperand
  {
  }

  public abstract class docDesc : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSRouteContractScheduleFSServiceContract.docDesc>
  {
  }
}
