// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.FinPeriods.OrganizationFinPeriodMin
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

[PXProjection(typeof (Select4<FinPeriod, Where<FinPeriod.masterFinPeriodID, GreaterEqual<CurrentValue<GLHistoryFilter.finPeriodID>>>, Aggregate<Min<FinPeriod.finPeriodID, GroupBy<FinPeriod.organizationID>>>>))]
[PXCacheName("Min FinPeriod Current")]
public class OrganizationFinPeriodMin : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
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

  public abstract class organizationID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    OrganizationFinPeriodMin.organizationID>
  {
  }

  public abstract class finPeriodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    OrganizationFinPeriodMin.finPeriodID>
  {
  }
}
