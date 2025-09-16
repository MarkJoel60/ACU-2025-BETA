// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.ContractEx
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data.BQL;
using PX.Objects.CT;

#nullable enable
namespace PX.Objects.EP;

public class ContractEx : Contract
{
  public new abstract class contractID : BqlType<IBqlInt, int>.Field<
  #nullable disable
  ContractEx.contractID>
  {
  }

  public new abstract class contractCD : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ContractEx.contractCD>
  {
  }
}
