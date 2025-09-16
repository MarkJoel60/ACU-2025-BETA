// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.CustomerProcessingCenterID
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.AR;
using System;

#nullable enable
namespace PX.Objects.CA;

[PXCacheName("Customer Processing Center ID")]
[Serializable]
public class CustomerProcessingCenterID : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected DateTime? _CreatedDateTime;

  [PXDBIdentity(IsKey = true)]
  public virtual int? InstanceID { get; set; }

  [Customer(DescriptionField = typeof (PX.Objects.AR.Customer.acctName))]
  [PXParent(typeof (Select<PX.Objects.AR.Customer, Where<PX.Objects.AR.Customer.bAccountID, Equal<Current<CustomerProcessingCenterID.bAccountID>>>>))]
  public virtual int? BAccountID { get; set; }

  [PXDBString(10, IsUnicode = true)]
  [PXDefault]
  [PXParent(typeof (Select<CCProcessingCenter, Where<CCProcessingCenter.processingCenterID, Equal<Current<CustomerProcessingCenterID.cCProcessingCenterID>>>>))]
  [PXUIField(DisplayName = "Proc. Center ID")]
  public virtual 
  #nullable disable
  string CCProcessingCenterID { get; set; }

  [PXDBString(1024 /*0x0400*/, IsUnicode = true)]
  [PXDBDefault]
  [PXUIField(DisplayName = "Customer CCPID")]
  public virtual string CustomerCCPID { get; set; }

  [PXDBCreatedDateTime]
  public virtual DateTime? CreatedDateTime
  {
    get => this._CreatedDateTime;
    set => this._CreatedDateTime = value;
  }

  public class PK : 
    PrimaryKeyOf<CustomerProcessingCenterID>.By<CustomerProcessingCenterID.instanceID>
  {
    public static CustomerProcessingCenterID Find(
      PXGraph graph,
      int? instanceID,
      PKFindOptions options = 0)
    {
      return (CustomerProcessingCenterID) PrimaryKeyOf<CustomerProcessingCenterID>.By<CustomerProcessingCenterID.instanceID>.FindBy(graph, (object) instanceID, options);
    }
  }

  public static class FK
  {
    public class Customer : 
      PrimaryKeyOf<PX.Objects.AR.Customer>.By<PX.Objects.AR.Customer.bAccountID>.ForeignKeyOf<CustomerProcessingCenterID>.By<CustomerProcessingCenterID.bAccountID>
    {
    }

    public class ProcessingCenter : 
      PrimaryKeyOf<CCProcessingCenter>.By<CCProcessingCenter.processingCenterID>.ForeignKeyOf<CustomerProcessingCenterID>.By<CustomerProcessingCenterID.cCProcessingCenterID>
    {
    }
  }

  public abstract class instanceID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    CustomerProcessingCenterID.instanceID>
  {
  }

  public abstract class bAccountID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    CustomerProcessingCenterID.bAccountID>
  {
  }

  public abstract class cCProcessingCenterID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CustomerProcessingCenterID.cCProcessingCenterID>
  {
  }

  public abstract class customerCCPID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CustomerProcessingCenterID.customerCCPID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CustomerProcessingCenterID.createdDateTime>
  {
  }
}
