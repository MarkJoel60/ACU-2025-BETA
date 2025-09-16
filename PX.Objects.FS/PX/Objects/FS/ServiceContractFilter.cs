// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.ServiceContractFilter
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

[Serializable]
public class ServiceContractFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXInt]
  [PXUIField]
  [FSSelectorCustomer]
  public virtual int? CustomerID { get; set; }

  [PXString(15, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC")]
  [PXUIField(DisplayName = "Service Contract ID")]
  [FSSelectorContractRefNbrAttribute(typeof (ListField_RecordType_ContractSchedule.ServiceContract))]
  public virtual 
  #nullable disable
  string RefNbr { get; set; }

  [PXInt]
  [PXUIField(DisplayName = "Branch")]
  [PXSelector(typeof (PX.Objects.GL.Branch.branchID), SubstituteKey = typeof (PX.Objects.GL.Branch.branchCD), DescriptionField = typeof (PX.Objects.GL.Branch.acctName))]
  public virtual int? BranchID { get; set; }

  [PXInt]
  [PXDefault(typeof (Search<FSBranchLocation.branchLocationID, Where<FSBranchLocation.branchID, Equal<Current<ServiceContractFilter.branchID>>>>))]
  [PXUIField(DisplayName = "Branch Location")]
  [PXSelector(typeof (Search<FSBranchLocation.branchLocationID, Where<FSBranchLocation.branchID, Equal<Current<ServiceContractFilter.branchID>>>>), SubstituteKey = typeof (FSBranchLocation.branchLocationCD), DescriptionField = typeof (FSBranchLocation.descr))]
  public virtual int? BranchLocationID { get; set; }

  [LocationID(typeof (Where<PX.Objects.CR.Location.bAccountID, Equal<Current<ServiceContractFilter.customerID>>>), DescriptionField = typeof (PX.Objects.CR.Location.descr), DisplayName = "Location")]
  public virtual int? CustomerLocationID { get; set; }

  [PXDateAndTime(UseTimeZone = false)]
  [PXDefault(typeof (AccessInfo.businessDate))]
  [PXUIField]
  public virtual DateTime? ToDate { get; set; }

  [PXInt]
  public virtual int? ScheduleID { get; set; }

  [PXString]
  [PXDefault("CS")]
  [ListField_ActionType_ProcessServiceContracts.ListAtrribute]
  [PXUIField(DisplayName = "Action")]
  public virtual string ActionType { get; set; }

  public abstract class customerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ServiceContractFilter.customerID>
  {
  }

  public abstract class refNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ServiceContractFilter.refNbr>
  {
  }

  public abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ServiceContractFilter.branchID>
  {
  }

  public abstract class branchLocationID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    ServiceContractFilter.branchLocationID>
  {
  }

  public abstract class customerLocationID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    ServiceContractFilter.customerLocationID>
  {
  }

  public abstract class toDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  ServiceContractFilter.toDate>
  {
  }

  public abstract class scheduleID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ServiceContractFilter.scheduleID>
  {
  }

  public abstract class actionType : ListField_ActionType_ProcessServiceContracts
  {
  }
}
