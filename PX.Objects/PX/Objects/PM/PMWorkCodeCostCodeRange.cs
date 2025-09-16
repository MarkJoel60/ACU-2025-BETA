// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.PMWorkCodeCostCodeRange
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using System;

#nullable enable
namespace PX.Objects.PM;

[PXCacheName("Workers' Compensation Code Cost Code Range")]
[Serializable]
public class PMWorkCodeCostCodeRange : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBString(15, IsKey = true)]
  [PXDBDefault(typeof (PMWorkCode.workCodeID))]
  [PXParent(typeof (PMWorkCodeCostCodeRange.FK.WorkCode))]
  public 
  #nullable disable
  string WorkCodeID { get; set; }

  [PXDBInt(IsKey = true)]
  [PXLineNbr(typeof (PMWorkCode))]
  public int? LineNbr { get; set; }

  [PXRestrictor(typeof (Where<PMCostCode.isActive, Equal<True>>), "The {0} cost code is inactive.", new Type[] {typeof (PMCostCode.costCodeCD)})]
  [PXDimensionSelector("COSTCODE", typeof (PMCostCode.costCodeCD))]
  [PXDBString(IsUnicode = true)]
  [PXUIField(DisplayName = "Cost Code From")]
  [CheckWorkCodeCostCodeRange]
  [PXForeignReference(typeof (PMWorkCodeCostCodeRange.FK.CostCodeFrom))]
  public virtual string CostCodeFrom { get; set; }

  [PXRestrictor(typeof (Where<PMCostCode.isActive, Equal<True>>), "The {0} cost code is inactive.", new Type[] {typeof (PMCostCode.costCodeCD)})]
  [PXDimensionSelector("COSTCODE", typeof (PMCostCode.costCodeCD))]
  [PXDBString(IsUnicode = true)]
  [PXUIField(DisplayName = "Cost Code To")]
  [PXForeignReference(typeof (PMWorkCodeCostCodeRange.FK.CostCodeTo))]
  public virtual string CostCodeTo { get; set; }

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

  public class PK : 
    PrimaryKeyOf<PMWorkCodeCostCodeRange>.By<PMWorkCodeCostCodeRange.workCodeID, PMWorkCodeCostCodeRange.lineNbr>
  {
    public static PMWorkCodeCostCodeRange Find(
      PXGraph graph,
      string workCodeID,
      int? lineNbr,
      PKFindOptions options = 0)
    {
      return (PMWorkCodeCostCodeRange) PrimaryKeyOf<PMWorkCodeCostCodeRange>.By<PMWorkCodeCostCodeRange.workCodeID, PMWorkCodeCostCodeRange.lineNbr>.FindBy(graph, (object) workCodeID, (object) lineNbr, options);
    }
  }

  public static class FK
  {
    public class WorkCode : 
      PrimaryKeyOf<PMWorkCode>.By<PMWorkCode.workCodeID>.ForeignKeyOf<PMWorkCodeCostCodeRange>.By<PMWorkCodeCostCodeRange.workCodeID>
    {
    }

    public class CostCodeFrom : 
      PrimaryKeyOf<PMCostCode>.By<PMCostCode.costCodeID>.ForeignKeyOf<PMWorkCodeCostCodeRange>.By<PMWorkCodeCostCodeRange.costCodeFrom>
    {
    }

    public class CostCodeTo : 
      PrimaryKeyOf<PMCostCode>.By<PMCostCode.costCodeID>.ForeignKeyOf<PMWorkCodeCostCodeRange>.By<PMWorkCodeCostCodeRange.costCodeTo>
    {
    }
  }

  public abstract class workCodeID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMWorkCodeCostCodeRange.workCodeID>
  {
  }

  public abstract class lineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMWorkCodeCostCodeRange.lineNbr>
  {
  }

  public abstract class costCodeFrom : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMWorkCodeCostCodeRange.costCodeFrom>
  {
  }

  public abstract class costCodeTo : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMWorkCodeCostCodeRange.costCodeTo>
  {
  }

  public abstract class createdByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    PMWorkCodeCostCodeRange.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMWorkCodeCostCodeRange.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    PMWorkCodeCostCodeRange.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    PMWorkCodeCostCodeRange.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMWorkCodeCostCodeRange.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    PMWorkCodeCostCodeRange.lastModifiedDateTime>
  {
  }
}
