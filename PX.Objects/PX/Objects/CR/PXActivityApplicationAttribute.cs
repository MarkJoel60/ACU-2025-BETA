// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.PXActivityApplicationAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.Objects.CR;

public class PXActivityApplicationAttribute : PXIntListAttribute
{
  public const int Portal = 0;
  public const int Backend = 1;
  public const int System = 2;

  public PXActivityApplicationAttribute()
    : base(new (int, string)[3]
    {
      (1, "ERP Users"),
      (0, "Portal Users"),
      (2, nameof (System))
    })
  {
  }

  public class portal : BqlType<IBqlInt, int>.Constant<
  #nullable disable
  PXActivityApplicationAttribute.portal>
  {
    public portal()
      : base(0)
    {
    }
  }

  public class backend : BqlType<
  #nullable enable
  IBqlInt, int>.Constant<
  #nullable disable
  PXActivityApplicationAttribute.backend>
  {
    public backend()
      : base(1)
    {
    }
  }

  public class system : BqlType<
  #nullable enable
  IBqlInt, int>.Constant<
  #nullable disable
  PXActivityApplicationAttribute.system>
  {
    public system()
      : base(2)
    {
    }
  }
}
