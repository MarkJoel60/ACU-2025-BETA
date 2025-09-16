// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.CRDefaultOwnerAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.Objects.CR;

/// <exclude />
public class CRDefaultOwnerAttribute : PXStringListAttribute
{
  public const 
  #nullable disable
  string DoNotChange = "N";
  public const string Creator = "C";
  public const string AssignmentMap = "A";
  public const string Source = "S";

  public CRDefaultOwnerAttribute()
    : base(new string[4]{ "N", "C", "A", "S" }, new string[4]
    {
      "Do Not Change",
      nameof (Creator),
      "Assignment map",
      "From source entity"
    })
  {
  }

  public class creator : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  CRDefaultOwnerAttribute.creator>
  {
    public creator()
      : base("C")
    {
    }
  }

  public class assignmentMap : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    CRDefaultOwnerAttribute.assignmentMap>
  {
    public assignmentMap()
      : base("A")
    {
    }
  }

  public class source : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  CRDefaultOwnerAttribute.source>
  {
    public source()
      : base("S")
    {
    }
  }
}
