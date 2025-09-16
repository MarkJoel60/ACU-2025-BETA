// Decompiled with JetBrains decompiler
// Type: PX.Objects.TX.TaxPluginMapping
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
namespace PX.Objects.TX;

[PXCacheName("Tax Plug-in Mapping")]
[Serializable]
public class TaxPluginMapping : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected 
  #nullable disable
  string _TaxPluginID;
  protected int? _BranchID;
  protected string _CompanyCode;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected DateTime? _LastModifiedDateTime;
  protected byte[] _tstamp;

  [PXUIField(DisplayName = "Tax Plug-in")]
  [PXSelector(typeof (TaxPlugin.taxPluginID))]
  [PXParent(typeof (Select<TaxPlugin, Where<TaxPlugin.taxPluginID, Equal<Current<TaxPluginMapping.taxPluginID>>>>))]
  [PXDBString(15, IsUnicode = true, IsKey = true)]
  [PXDefault(typeof (TaxPlugin.taxPluginID))]
  public virtual string TaxPluginID
  {
    get => this._TaxPluginID;
    set => this._TaxPluginID = value;
  }

  [Branch(null, null, true, true, true, IsKey = true)]
  public virtual int? BranchID
  {
    get => this._BranchID;
    set => this._BranchID = value;
  }

  [PXDBString(50, IsUnicode = true)]
  [PXUIField(DisplayName = "Company Code")]
  public virtual string CompanyCode
  {
    get => this._CompanyCode;
    set => this._CompanyCode = value;
  }

  /// <summary>The company ID of the external tax provider.</summary>
  [PXDBString(15, IsUnicode = true)]
  public virtual string ExternalCompanyID { get; set; }

  [PXDBCreatedByID]
  public virtual Guid? CreatedByID
  {
    get => this._CreatedByID;
    set => this._CreatedByID = value;
  }

  [PXDBCreatedByScreenID]
  public virtual string CreatedByScreenID
  {
    get => this._CreatedByScreenID;
    set => this._CreatedByScreenID = value;
  }

  [PXDBCreatedDateTime]
  public virtual DateTime? CreatedDateTime
  {
    get => this._CreatedDateTime;
    set => this._CreatedDateTime = value;
  }

  [PXDBLastModifiedByID]
  public virtual Guid? LastModifiedByID
  {
    get => this._LastModifiedByID;
    set => this._LastModifiedByID = value;
  }

  [PXDBLastModifiedByScreenID]
  public virtual string LastModifiedByScreenID
  {
    get => this._LastModifiedByScreenID;
    set => this._LastModifiedByScreenID = value;
  }

  [PXDBLastModifiedDateTime]
  public virtual DateTime? LastModifiedDateTime
  {
    get => this._LastModifiedDateTime;
    set => this._LastModifiedDateTime = value;
  }

  [PXDBTimestamp]
  public virtual byte[] tstamp
  {
    get => this._tstamp;
    set => this._tstamp = value;
  }

  public class PK : 
    PrimaryKeyOf<TaxPluginMapping>.By<TaxPluginMapping.taxPluginID, TaxPluginMapping.branchID>
  {
    public static TaxPluginMapping Find(
      PXGraph graph,
      string taxPluginID,
      string branchID,
      PKFindOptions options = 0)
    {
      return (TaxPluginMapping) PrimaryKeyOf<TaxPluginMapping>.By<TaxPluginMapping.taxPluginID, TaxPluginMapping.branchID>.FindBy(graph, (object) taxPluginID, (object) branchID, options);
    }
  }

  public static class FK
  {
    public class TaxPlugin : 
      PrimaryKeyOf<TaxPlugin>.By<TaxPlugin.taxPluginID>.ForeignKeyOf<TaxPluginMapping>.By<TaxPluginMapping.taxPluginID>
    {
    }

    public class Branch : 
      PrimaryKeyOf<PX.Objects.GL.Branch>.By<PX.Objects.GL.Branch.branchID>.ForeignKeyOf<TaxPluginMapping>.By<TaxPluginMapping.branchID>
    {
    }
  }

  public abstract class taxPluginID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  TaxPluginMapping.taxPluginID>
  {
  }

  public abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  TaxPluginMapping.branchID>
  {
  }

  public abstract class companyCode : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  TaxPluginMapping.companyCode>
  {
  }

  public abstract class externalCompanyID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    TaxPluginMapping.externalCompanyID>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  TaxPluginMapping.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    TaxPluginMapping.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    TaxPluginMapping.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    TaxPluginMapping.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    TaxPluginMapping.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    TaxPluginMapping.lastModifiedDateTime>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  TaxPluginMapping.Tstamp>
  {
  }
}
