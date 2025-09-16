// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.GLTranType
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data.BQL;
using PX.Objects.Common;
using System.Collections.Generic;

#nullable enable
namespace PX.Objects.GL;

public class GLTranType : ILabelProvider
{
  protected static readonly 
  #nullable disable
  IEnumerable<ValueLabelPair> ValueLabelList = (IEnumerable<ValueLabelPair>) new PX.Objects.Common.ValueLabelList()
  {
    {
      "GLE",
      "GL Entry"
    }
  };
  public const string GLEntry = "GLE";

  public IEnumerable<ValueLabelPair> ValueLabelPairs => GLTranType.ValueLabelList;

  public class gLEntry : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  GLTranType.gLEntry>
  {
    public gLEntry()
      : base("GLE")
    {
    }
  }
}
