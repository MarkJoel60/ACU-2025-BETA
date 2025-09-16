// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.CROpportunityProductLineTypeAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.Objects.CR;

public class CROpportunityProductLineTypeAttribute : PXStringListAttribute
{
  public const 
  #nullable disable
  string Distribution = "D";
  public const string ScopeOfWork = "SOW";

  public CROpportunityProductLineTypeAttribute()
    : base(new string[2]{ "D", "SOW" }, new string[2]
    {
      nameof (Distribution),
      "Estimation"
    })
  {
  }

  public sealed class distribution : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    CROpportunityProductLineTypeAttribute.distribution>
  {
    public distribution()
      : base("D")
    {
    }
  }

  public sealed class scopeOfWork : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    CROpportunityProductLineTypeAttribute.scopeOfWork>
  {
    public scopeOfWork()
      : base("SOW")
    {
    }
  }
}
