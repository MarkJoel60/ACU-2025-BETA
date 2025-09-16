// Decompiled with JetBrains decompiler
// Type: PX.Objects.CN.Compliance.PM.DAC.LienWaiverRecipient
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CN.Common.DAC;
using PX.Objects.EP;
using PX.Objects.PM;
using System;

#nullable enable
namespace PX.Objects.CN.Compliance.PM.DAC;

[PXCacheName("Lien Waiver Recipient")]
[Serializable]
public class LienWaiverRecipient : BaseCache, IBqlTable, IBqlTableSystemDataStorage
{
  [PXBool]
  [PXDefault(false)]
  [PXUIField]
  public virtual bool? Selected { get; set; }

  [PXDBIdentity]
  public virtual int? LienWaiverRecipientId { get; set; }

  [PXDBInt(IsKey = true)]
  [PXDBDefault(typeof (PMProject.contractID))]
  [PXParent(typeof (LienWaiverRecipient.FK.Project))]
  public virtual int? ProjectId { get; set; }

  [PXDBString(10, IsUnicode = true, IsKey = true)]
  [PXDefault]
  [PXUIField(DisplayName = "Vendor Class")]
  [PXSelector(typeof (SearchFor<PX.Objects.AP.VendorClass.vendorClassID>.In<SelectFromBase<PX.Objects.AP.VendorClass, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Left<EPEmployeeClass>.On<BqlOperand<EPEmployeeClass.vendorClassID, IBqlString>.IsEqual<PX.Objects.AP.VendorClass.vendorClassID>>>>.Where<BqlOperand<EPEmployeeClass.vendorClassID, IBqlString>.IsNull>>))]
  [PXParent(typeof (LienWaiverRecipient.FK.VendorClass))]
  public virtual 
  #nullable disable
  string VendorClassId { get; set; }

  [PXDBDecimal(MinValue = 0.0)]
  [PXDefault]
  [PXUIField(DisplayName = "Minimum Commitment Amount", Required = true)]
  public virtual Decimal? MinimumCommitmentAmount { get; set; }

  public class PK : PrimaryKeyOf<LienWaiverRecipient>.By<LienWaiverRecipient.lienWaiverRecipientId>
  {
    public static LienWaiverRecipient Find(
      PXGraph graph,
      int? lienWaiverRecipientId,
      PKFindOptions options = 0)
    {
      return (LienWaiverRecipient) PrimaryKeyOf<LienWaiverRecipient>.By<LienWaiverRecipient.lienWaiverRecipientId>.FindBy(graph, (object) lienWaiverRecipientId, options);
    }
  }

  public static class FK
  {
    public class Project : 
      PrimaryKeyOf<PMProject>.By<PMProject.contractID>.ForeignKeyOf<LienWaiverRecipient>.By<LienWaiverRecipient.projectId>
    {
    }

    public class VendorClass : 
      PrimaryKeyOf<PX.Objects.AP.VendorClass>.By<PX.Objects.AP.VendorClass.vendorClassID>.ForeignKeyOf<LienWaiverRecipient>.By<LienWaiverRecipient.vendorClassId>
    {
    }
  }

  public abstract class selected : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  LienWaiverRecipient.selected>
  {
  }

  public abstract class lienWaiverRecipientId : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    LienWaiverRecipient.lienWaiverRecipientId>
  {
  }

  public abstract class projectId : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  LienWaiverRecipient.projectId>
  {
  }

  public abstract class vendorClassId : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    LienWaiverRecipient.vendorClassId>
  {
  }

  public abstract class minimumCommitmentAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    LienWaiverRecipient.minimumCommitmentAmount>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  LienWaiverRecipient.noteID>
  {
  }

  public abstract class tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  LienWaiverRecipient.tstamp>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  LienWaiverRecipient.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    LienWaiverRecipient.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    LienWaiverRecipient.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    LienWaiverRecipient.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    LienWaiverRecipient.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    LienWaiverRecipient.lastModifiedDateTime>
  {
  }
}
