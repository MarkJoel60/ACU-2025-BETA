// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.SOPackageType
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.SO;

public class SOPackageType
{
  public const 
  #nullable disable
  string Auto = "A";
  public const string Manual = "M";

  public class auto : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  SOPackageType.auto>
  {
    public auto()
      : base("A")
    {
    }
  }

  public class manual : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  SOPackageType.manual>
  {
    public manual()
      : base("M")
    {
    }
  }

  [PXLocalizable]
  public abstract class DisplayNames
  {
    public const string Auto = "Auto";
    public const string Manual = "Manual";
  }

  public class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(new Tuple<string, string>[2]
      {
        PXStringListAttribute.Pair("A", "Auto"),
        PXStringListAttribute.Pair("M", "Manual")
      })
    {
    }
  }

  public class ForFiltering : SOPackageType
  {
    public const string Both = "B";

    public class both : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    SOPackageType.ForFiltering.both>
    {
      public both()
        : base("B")
      {
      }
    }

    public new class ListAttribute : PXStringListAttribute
    {
      public ListAttribute()
        : base(new Tuple<string, string>[3]
        {
          PXStringListAttribute.Pair("B", "Auto and Manual"),
          PXStringListAttribute.Pair("A", "Auto"),
          PXStringListAttribute.Pair("M", "Manual")
        })
      {
      }
    }

    [PXLocalizable]
    public new abstract class DisplayNames : SOPackageType.DisplayNames
    {
      public const string Both = "Auto and Manual";
    }
  }
}
