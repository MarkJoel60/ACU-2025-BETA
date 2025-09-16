// Decompiled with JetBrains decompiler
// Type: PX.CS.AttributeControlType
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.CS;

public static class AttributeControlType
{
  public const int Text = 1;
  public const int Combo = 2;
  public const int Lookup = 3;
  public const int CheckBox = 4;
  public const int Datetime = 5;
  public const int MultiSelectCombo = 6;
  public const int GISelector = 7;
  public const int Number = 8;

  public class text : BqlType<IBqlInt, int>.Constant<
  #nullable disable
  AttributeControlType.text>
  {
    public text()
      : base(1)
    {
    }
  }

  public class combo : BqlType<
  #nullable enable
  IBqlInt, int>.Constant<
  #nullable disable
  AttributeControlType.combo>
  {
    public combo()
      : base(2)
    {
    }
  }

  public class lookup : BqlType<
  #nullable enable
  IBqlInt, int>.Constant<
  #nullable disable
  AttributeControlType.lookup>
  {
    public lookup()
      : base(3)
    {
    }
  }

  public class checkBox : BqlType<
  #nullable enable
  IBqlInt, int>.Constant<
  #nullable disable
  AttributeControlType.checkBox>
  {
    public checkBox()
      : base(4)
    {
    }
  }

  public class datetime : BqlType<
  #nullable enable
  IBqlInt, int>.Constant<
  #nullable disable
  AttributeControlType.datetime>
  {
    public datetime()
      : base(5)
    {
    }
  }

  public class multiSelectCombo : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Constant<
    #nullable disable
    AttributeControlType.multiSelectCombo>
  {
    public multiSelectCombo()
      : base(6)
    {
    }
  }

  public class giSelector : BqlType<
  #nullable enable
  IBqlInt, int>.Constant<
  #nullable disable
  AttributeControlType.giSelector>
  {
    public giSelector()
      : base(7)
    {
    }
  }

  public class number : BqlType<
  #nullable enable
  IBqlInt, int>.Constant<
  #nullable disable
  AttributeControlType.number>
  {
    public number()
      : base(8)
    {
    }
  }

  [PXLocalizable]
  public static class DisplayNames
  {
    public const string Text = "Text";
    public const string Combo = "Combo";
    public const string Lookup = "Lookup";
    public const string CheckBox = "Checkbox";
    public const string Datetime = "Datetime";
    public const string MultiSelectCombo = "Multi Select Combo";
    public const string GISelector = "Selector";
    public const string Number = "Number";
  }

  public class ListAttribute : PXIntListAttribute
  {
    public ListAttribute()
      : base((1, "Text"), (8, "Number"), (2, "Combo"), (6, "Multi Select Combo"), (4, "Checkbox"), (5, "Datetime"), (7, "Selector"))
    {
    }
  }
}
