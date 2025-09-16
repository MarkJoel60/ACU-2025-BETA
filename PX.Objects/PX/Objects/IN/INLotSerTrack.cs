// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.INLotSerTrack
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.IN;

public static class INLotSerTrack
{
  public const 
  #nullable disable
  string NotNumbered = "N";
  public const string LotNumbered = "L";
  public const string SerialNumbered = "S";

  public static bool HasFlags(this INLotSerTrack.Mode mode, INLotSerTrack.Mode flags)
  {
    return (mode & flags) == flags;
  }

  [Flags]
  public enum Mode
  {
    None = 0,
    Create = 1,
    Issue = 2,
    Manual = 4,
  }

  public class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(new Tuple<string, string>[3]
      {
        PXStringListAttribute.Pair("N", "Not Tracked"),
        PXStringListAttribute.Pair("L", "Track Lot Numbers"),
        PXStringListAttribute.Pair("S", "Track Serial Numbers")
      })
    {
    }
  }

  public class notNumbered : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  INLotSerTrack.notNumbered>
  {
    public notNumbered()
      : base("N")
    {
    }
  }

  public class lotNumbered : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  INLotSerTrack.lotNumbered>
  {
    public lotNumbered()
      : base("L")
    {
    }
  }

  public class serialNumbered : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  INLotSerTrack.serialNumbered>
  {
    public serialNumbered()
      : base("S")
    {
    }
  }
}
