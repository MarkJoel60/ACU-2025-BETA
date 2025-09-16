// Decompiled with JetBrains decompiler
// Type: PX.CS.RMDataSource
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;

#nullable enable
namespace PX.CS;

[PXCacheName("Data Source")]
public class RMDataSource : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected int? _DataSourceID;
  protected 
  #nullable disable
  string _Expand;
  protected string _RowDescription;
  protected short? _AmountType;

  [PXDBIdentity(IsKey = true)]
  [PXUIField(Visible = false)]
  [PXReferentialIntegrityCheck]
  public virtual int? DataSourceID
  {
    get => this._DataSourceID;
    set => this._DataSourceID = value;
  }

  [PXDBString(1)]
  [PXUIField(DisplayName = "Expand", Required = false)]
  [PXDefault("N")]
  public virtual string Expand
  {
    get => this._Expand;
    set => this._Expand = value;
  }

  [PXDBString(2)]
  [PXUIField(DisplayName = "Row Description", Required = false)]
  [PXDefault("N")]
  public virtual string RowDescription
  {
    get => this._RowDescription;
    set => this._RowDescription = value;
  }

  [PXDBShort]
  [PXDefault(0)]
  [PXUIField(DisplayName = "Amount Type")]
  public virtual short? AmountType
  {
    get => this._AmountType;
    set => this._AmountType = value;
  }

  public class PK : PrimaryKeyOf<RMDataSource>.By<RMDataSource.dataSourceID>
  {
    public static RMDataSource Find(PXGraph graph, int? dataSourceID, PKFindOptions options = PKFindOptions.None)
    {
      return PrimaryKeyOf<RMDataSource>.By<RMDataSource.dataSourceID>.FindBy(graph, (object) dataSourceID, options);
    }
  }

  public abstract class dataSourceID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  RMDataSource.dataSourceID>
  {
  }

  public abstract class expand : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  RMDataSource.expand>
  {
  }

  public abstract class rowDescription : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    RMDataSource.rowDescription>
  {
  }

  public abstract class amountType : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  RMDataSource.amountType>
  {
  }
}
