// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.ContractExEx
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data.BQL;
using PX.Objects.CT;
using System;

#nullable enable
namespace PX.Objects.EP;

public class ContractExEx : Contract
{
  public new abstract class contractID : BqlType<IBqlInt, int>.Field<
  #nullable disable
  ContractExEx.contractID>
  {
  }

  public new abstract class baseType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ContractExEx.baseType>
  {
  }

  public new abstract class contractCD : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ContractExEx.contractCD>
  {
  }

  [Obsolete("This property has been deprecated and will be removed in Acumatica ERP 2019R2.")]
  public new abstract class isTemplate : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ContractExEx.isTemplate>
  {
  }

  public new abstract class description : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ContractExEx.description>
  {
  }
}
