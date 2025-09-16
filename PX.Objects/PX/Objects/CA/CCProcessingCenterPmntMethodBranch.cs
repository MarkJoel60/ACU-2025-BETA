// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.CCProcessingCenterPmntMethodBranch
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.GL;
using System;

#nullable enable
namespace PX.Objects.CA;

/// <summary>
/// Represents a mapping row for Payment Method, Branch, and Processing Center
/// </summary>
[PXCacheName("Overrides By Branch")]
public class CCProcessingCenterPmntMethodBranch : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  /// <summary>
  /// This processing center will be used for payments created with <see cref="P:PX.Objects.CA.CCProcessingCenterPmntMethodBranch.PaymentMethodID" /> payment method for <see cref="P:PX.Objects.CA.CCProcessingCenterPmntMethodBranch.BranchID" /> branch&gt;.
  /// </summary>
  [PXDBString(10, IsUnicode = true, InputMask = ">aaaaaaaaaa")]
  [PXDefault]
  [PXSelector(typeof (Search<CCProcessingCenter.processingCenterID, Where<CCProcessingCenter.isActive, Equal<True>>>))]
  [PXParent(typeof (CCProcessingCenterPmntMethodBranch.FK.ProcessingCenter))]
  [PXUIField(DisplayName = "Default Processing Center")]
  public virtual 
  #nullable disable
  string ProcessingCenterID { get; set; }

  /// <summary>Identifier of the branch</summary>
  [Branch(null, null, true, true, true)]
  [PXUIField(DisplayName = "Branch")]
  public virtual int? BranchID { get; set; }

  /// <summary>Identifier of the processing center</summary>
  [PXDBString(10, IsUnicode = true, IsKey = true)]
  [PXDBDefault(typeof (PaymentMethod.paymentMethodID))]
  [PXParent(typeof (CCProcessingCenterPmntMethodBranch.FK.PaymentMethod))]
  [PXParent(typeof (CCProcessingCenterPmntMethodBranch.FK.ProcessingCenterPmntMethod))]
  public virtual string PaymentMethodID { get; set; }

  [PXDBCreatedByID]
  public virtual Guid? CreatedByID { get; set; }

  [PXDBCreatedByScreenID]
  public virtual string CreatedByScreenID { get; set; }

  [PXDBCreatedDateTime]
  public virtual DateTime? CreatedDateTime { get; set; }

  [PXDBLastModifiedByID]
  public virtual Guid? LastModifiedByID { get; set; }

  [PXDBLastModifiedByScreenID]
  public virtual string LastModifiedByScreenID { get; set; }

  [PXDBLastModifiedDateTime]
  public virtual DateTime? LastModifiedDateTime { get; set; }

  [PXDBTimestamp]
  public virtual byte[] tstamp { get; set; }

  public class PK : 
    PrimaryKeyOf<CCProcessingCenterPmntMethodBranch>.By<CCProcessingCenterPmntMethodBranch.paymentMethodID, CCProcessingCenterPmntMethodBranch.branchID>
  {
    public static CCProcessingCenterPmntMethodBranch Find(
      PXGraph graph,
      string paymentMethodID,
      int? branchId)
    {
      return (CCProcessingCenterPmntMethodBranch) PrimaryKeyOf<CCProcessingCenterPmntMethodBranch>.By<CCProcessingCenterPmntMethodBranch.paymentMethodID, CCProcessingCenterPmntMethodBranch.branchID>.FindBy(graph, (object) paymentMethodID, (object) branchId, (PKFindOptions) 0);
    }
  }

  public static class FK
  {
    public class ProcessingCenterPmntMethod : 
      PrimaryKeyOf<CCProcessingCenterPmntMethod>.By<CCProcessingCenterPmntMethod.processingCenterID, CCProcessingCenterPmntMethod.paymentMethodID>.ForeignKeyOf<CCProcessingCenterPmntMethodBranch>.By<CCProcessingCenterPmntMethodBranch.processingCenterID, CCProcessingCenterPmntMethodBranch.paymentMethodID>
    {
    }

    public class ProcessingCenter : 
      PrimaryKeyOf<CCProcessingCenter>.By<CCProcessingCenter.processingCenterID>.ForeignKeyOf<CCProcessingCenterPmntMethodBranch>.By<CCProcessingCenterPmntMethodBranch.processingCenterID>
    {
    }

    public class PaymentMethod : 
      PrimaryKeyOf<PaymentMethod>.By<PaymentMethod.paymentMethodID>.ForeignKeyOf<CCProcessingCenterPmntMethodBranch>.By<CCProcessingCenterPmntMethodBranch.paymentMethodID>
    {
    }

    public class Branch : 
      PrimaryKeyOf<PX.Objects.GL.Branch>.By<PX.Objects.GL.Branch.branchID>.ForeignKeyOf<CCProcessingCenterPmntMethodBranch>.By<CCProcessingCenterPmntMethodBranch.branchID>
    {
    }
  }

  public abstract class processingCenterID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CCProcessingCenterPmntMethodBranch.processingCenterID>
  {
  }

  public abstract class branchID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    CCProcessingCenterPmntMethodBranch.branchID>
  {
  }

  public abstract class paymentMethodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CCProcessingCenterPmntMethodBranch.paymentMethodID>
  {
  }

  public abstract class createdByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    CCProcessingCenterPmntMethodBranch.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CCProcessingCenterPmntMethodBranch.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CCProcessingCenterPmntMethodBranch.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    CCProcessingCenterPmntMethodBranch.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CCProcessingCenterPmntMethodBranch.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CCProcessingCenterPmntMethodBranch.lastModifiedDateTime>
  {
  }

  public abstract class Tstamp : 
    BqlType<
    #nullable enable
    IBqlByteArray, byte[]>.Field<
    #nullable disable
    CCProcessingCenterPmntMethodBranch.Tstamp>
  {
  }
}
