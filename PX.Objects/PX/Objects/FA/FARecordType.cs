// Decompiled with JetBrains decompiler
// Type: PX.Objects.FA.FARecordType
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CS;
using System;

#nullable enable
namespace PX.Objects.FA;

public class FARecordType
{
  public const 
  #nullable disable
  string ClassType = "C";
  public const string AssetType = "A";
  public const string ElementType = "E";
  public const string BothType = "B";

  public class CustomListAttribute(string[] AllowedValues, string[] AllowedLabels) : 
    PXStringListAttribute(AllowedValues, AllowedLabels)
  {
    public string[] AllowedValues => this._AllowedValues;

    public string[] AllowedLabels => this._AllowedLabels;
  }

  public class MethodListAttribute : FARecordType.CustomListAttribute
  {
    public MethodListAttribute()
      : base(new string[3]{ "C", "A", "B" }, new string[3]
      {
        "Class",
        "Asset",
        "Both"
      })
    {
    }
  }

  public class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(new string[3]{ "C", "A", "E" }, new string[3]
      {
        "Class",
        "Asset",
        "Component"
      })
    {
    }
  }

  public class NumberingAttribute : AutoNumberAttribute
  {
    public NumberingAttribute()
      : base(typeof (FixedAsset.recordType), typeof (FixedAsset.createdDateTime), new string[3]
      {
        "C",
        "A",
        "E"
      }, new Type[3]
      {
        null,
        typeof (Search<FASetup.assetNumberingID>),
        typeof (Search<FASetup.assetNumberingID>)
      })
    {
      this.NullMode = AutoNumberAttribute.NullNumberingMode.UserNumbering;
    }
  }

  public class classType : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  FARecordType.classType>
  {
    public classType()
      : base("C")
    {
    }
  }

  public class assetType : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  FARecordType.assetType>
  {
    public assetType()
      : base("A")
    {
    }
  }

  public class elementType : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  FARecordType.elementType>
  {
    public elementType()
      : base("E")
    {
    }
  }

  public class bothType : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  FARecordType.bothType>
  {
    public bothType()
      : base("B")
    {
    }
  }
}
