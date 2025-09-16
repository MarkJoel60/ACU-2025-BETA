// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.Standalone.Branch2
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.Objects.GL.Standalone;

[PXHidden]
public class Branch2 : Branch
{
  public new abstract class branchID : BqlType<IBqlInt, int>.Field<
  #nullable disable
  Branch2.branchID>
  {
  }

  public new abstract class bAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Branch2.bAccountID>
  {
  }
}
