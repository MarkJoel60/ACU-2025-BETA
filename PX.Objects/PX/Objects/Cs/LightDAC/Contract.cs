// Decompiled with JetBrains decompiler
// Type: PX.Objects.CS.LightDAC.Contract
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CT;

#nullable enable
namespace PX.Objects.CS.LightDAC;

/// <summary>the small version of  <see cref="T:PX.Objects.PM.PMProject" /></summary>
[PXHidden]
public class Contract : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBIdentity]
  public virtual int? ContractID { get; set; }

  [PXDBString(1, IsFixed = true, IsKey = true)]
  [CTPRType.List]
  public virtual 
  #nullable disable
  string BaseType { get; set; }

  [PXDimension("PROJECT")]
  [PXDBString(IsUnicode = true, IsKey = true, InputMask = "")]
  public virtual string ContractCD { get; set; }

  [PXDBLocalizableString(255 /*0xFF*/, IsUnicode = true)]
  public virtual string Description { get; set; }

  [PXDBBool]
  public virtual bool? Hold { get; set; }

  [PXDBBool]
  public virtual bool? IsActive { get; set; }

  [PXDBBool]
  public virtual bool? NonProject { get; set; }

  public abstract class contractID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Contract.contractID>
  {
  }

  public abstract class baseType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Contract.baseType>
  {
  }

  public abstract class contractCD : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Contract.contractCD>
  {
  }

  public abstract class description : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Contract.description>
  {
  }

  public abstract class hold : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  Contract.hold>
  {
  }

  public abstract class isActive : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  Contract.isActive>
  {
  }

  public abstract class nonProject : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  Contract.nonProject>
  {
  }
}
