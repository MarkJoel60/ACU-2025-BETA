// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.ReversingChangeOrder
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;
using System.Diagnostics.CodeAnalysis;

#nullable enable
namespace PX.Objects.PM;

/// <summary>Contains reversing change order data.</summary>
[ExcludeFromCodeCoverage]
[PXCacheName("Reversing Change Order")]
[PXHidden]
[PXBreakInheritance]
[Serializable]
public class ReversingChangeOrder : PMChangeOrder
{
  public new abstract class refNbr : BqlType<IBqlString, string>.Field<
  #nullable disable
  ReversingChangeOrder.refNbr>
  {
  }

  public new abstract class origRefNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ReversingChangeOrder.origRefNbr>
  {
  }
}
