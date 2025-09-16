// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.PMCommitmentType
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System.Diagnostics.CodeAnalysis;

#nullable enable
namespace PX.Objects.PM;

[ExcludeFromCodeCoverage]
public static class PMCommitmentType
{
  public const 
  #nullable disable
  string Internal = "I";
  public const string External = "E";

  public class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(new string[2]{ "I", "E" }, new string[2]
      {
        "Internal",
        "External"
      })
    {
    }
  }

  public class internalType : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  PMCommitmentType.internalType>
  {
    public internalType()
      : base("I")
    {
    }
  }

  public class externalType : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  PMCommitmentType.externalType>
  {
    public externalType()
      : base("E")
    {
    }
  }
}
