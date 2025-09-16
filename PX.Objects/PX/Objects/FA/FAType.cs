// Decompiled with JetBrains decompiler
// Type: PX.Objects.FA.FAType
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using System;

#nullable enable
namespace PX.Objects.FA;

[PXPrimaryGraph(typeof (AssetTypeMaint))]
[PXCacheName("FA Type")]
[Serializable]
public class FAType : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected 
  #nullable disable
  string _AssetTypeID;
  protected string _Description;
  protected bool? _IsTangible;
  protected bool? _Depreciable;

  [PXDBString(10, IsUnicode = true, IsKey = true, InputMask = ">CCCCCCCCCC")]
  [PXDefault]
  [PXUIField]
  [PXSelector(typeof (Search<FAType.assetTypeID>))]
  public virtual string AssetTypeID
  {
    get => this._AssetTypeID;
    set => this._AssetTypeID = value;
  }

  [PXDBString(60, IsUnicode = true)]
  [PXUIField]
  public virtual string Description
  {
    get => this._Description;
    set => this._Description = value;
  }

  [PXDBBool]
  [PXDefault(true)]
  [PXUIField]
  public virtual bool? IsTangible
  {
    get => this._IsTangible;
    set => this._IsTangible = value;
  }

  [PXDBBool]
  [PXDefault(true)]
  [PXUIField]
  public virtual bool? Depreciable
  {
    get => this._Depreciable;
    set => this._Depreciable = value;
  }

  public class PK : PrimaryKeyOf<FAType>.By<FAType.assetTypeID>
  {
    public static FAType Find(PXGraph graph, string assetTypeID, PKFindOptions options = 0)
    {
      return (FAType) PrimaryKeyOf<FAType>.By<FAType.assetTypeID>.FindBy(graph, (object) assetTypeID, options);
    }
  }

  public abstract class assetTypeID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FAType.assetTypeID>
  {
  }

  public abstract class description : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FAType.description>
  {
  }

  public abstract class isTangible : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FAType.isTangible>
  {
  }

  public abstract class depreciable : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FAType.depreciable>
  {
  }
}
