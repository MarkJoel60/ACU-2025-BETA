// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.INSiteTo
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data.BQL;
using PX.Objects.IN;
using System;

#nullable enable
namespace PX.Objects.GL;

[Serializable]
public class INSiteTo : INSite
{
  public new abstract class siteID : BqlType<IBqlInt, int>.Field<
  #nullable disable
  INSiteTo.siteID>
  {
  }

  public new abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INSiteTo.branchID>
  {
  }
}
