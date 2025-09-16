// Decompiled with JetBrains decompiler
// Type: PX.Data.PXAutomationMenuAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.BQL;

#nullable enable
namespace PX.Data;

public class PXAutomationMenuAttribute
{
  public const 
  #nullable disable
  string Undefined = "<SELECT>";

  /// <exclude />
  public class undefinded : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    PXAutomationMenuAttribute.undefinded>
  {
    public undefinded()
      : base("<SELECT>")
    {
    }
  }
}
