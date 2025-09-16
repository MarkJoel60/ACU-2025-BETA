// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.ARSPCommissionYear
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using System;

#nullable enable
namespace PX.Objects.AR;

/// <summary>
/// Represents a year in which salesperson commissions are
/// calculated and to which commission periods (<see cref="T:PX.Objects.AR.ARSPCommissionPeriod" />) belong. The records of
/// this type are created during the Calculate Commissions
/// (AR505500) process, which corresponds to the <see cref="T:PX.Objects.AR.ARSPCommissionProcess" /> graph.
/// </summary>
[PXCacheName("AR Salesperson Commission Year")]
[Serializable]
public class ARSPCommissionYear : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected 
  #nullable disable
  string _Year;
  protected bool? _Filed;
  protected byte[] _tstamp;

  [PXDBString(4, IsKey = true, IsFixed = true)]
  [PXDefault]
  public virtual string Year
  {
    get => this._Year;
    set => this._Year = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? Filed
  {
    get => this._Filed;
    set => this._Filed = value;
  }

  [PXDBTimestamp]
  public virtual byte[] tstamp
  {
    get => this._tstamp;
    set => this._tstamp = value;
  }

  /// <exclude />
  public class PK : PrimaryKeyOf<ARSPCommissionYear>.By<ARSPCommissionYear.year>
  {
    public static ARSPCommissionYear Find(PXGraph graph, string year, PKFindOptions options = 0)
    {
      return (ARSPCommissionYear) PrimaryKeyOf<ARSPCommissionYear>.By<ARSPCommissionYear.year>.FindBy(graph, (object) year, options);
    }
  }

  public abstract class year : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARSPCommissionYear.year>
  {
  }

  public abstract class filed : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARSPCommissionYear.filed>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  ARSPCommissionYear.Tstamp>
  {
  }
}
