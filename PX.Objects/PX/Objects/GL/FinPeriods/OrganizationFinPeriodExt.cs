// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.FinPeriods.OrganizationFinPeriodExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.GL.Attributes;
using PX.Objects.GL.FinPeriods.TableDefinition;

#nullable enable
namespace PX.Objects.GL.FinPeriods;

[PXProjection(typeof (Select5<FinPeriod, LeftJoin<FinPeriod2, On<FinPeriod2.finPeriodID, Less<FinPeriod.finPeriodID>>>, Aggregate<Max<FinPeriod2.finPeriodID, GroupBy<FinPeriod.organizationID, GroupBy<FinPeriod.finPeriodID, GroupBy<FinPeriod.masterFinPeriodID>>>>>>))]
[PXCacheName("Last FinPeriod")]
public class OrganizationFinPeriodExt : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [Organization(true, IsKey = true, BqlTable = typeof (FinPeriod))]
  public virtual int? OrganizationID { get; set; }

  /// <summary>
  /// Key field.
  /// Unique identifier of the Financial Period.
  /// </summary>
  /// 
  ///             Consists of the year and the number of the period in the year. For more information see <see cref="T:PX.Objects.GL.FinPeriodIDAttribute" />
  /// .
  [PXDBString(6, IsKey = true, IsFixed = true, BqlTable = typeof (FinPeriod))]
  [FinPeriodIDFormatting]
  [PXDefault]
  [PXUIField]
  public virtual 
  #nullable disable
  string FinPeriodID { get; set; }

  [PX.Objects.GL.FinPeriodID(null, null, null, null, null, null, true, false, null, null, null, true, true, BqlTable = typeof (FinPeriod))]
  [PXDefault]
  [PXUIField]
  public virtual string MasterFinPeriodID { get; set; }

  [PXDBString(6, IsKey = true, IsFixed = true, BqlField = typeof (FinPeriod2.finPeriodID))]
  [FinPeriodIDFormatting]
  public virtual string PrevFinPeriodID { get; set; }

  public abstract class organizationID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    OrganizationFinPeriodExt.organizationID>
  {
  }

  public abstract class finPeriodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    OrganizationFinPeriodExt.finPeriodID>
  {
  }

  public abstract class masterFinPeriodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    OrganizationFinPeriodExt.masterFinPeriodID>
  {
  }

  public abstract class prevFinPeriodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    OrganizationFinPeriodExt.prevFinPeriodID>
  {
  }
}
