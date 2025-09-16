// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.AdjustingBranch
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.GL;

[Serializable]
public class AdjustingBranch : Branch
{
  public new abstract class branchID : BqlType<IBqlInt, int>.Field<
  #nullable disable
  AdjustingBranch.branchID>
  {
  }

  public new abstract class organizationID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    AdjustingBranch.organizationID>
  {
  }
}
