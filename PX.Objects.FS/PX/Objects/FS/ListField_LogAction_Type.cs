// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.ListField_LogAction_Type
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.FS;

public abstract class ListField_LogAction_Type : ListField_Log_ItemType
{
  public const 
  #nullable disable
  string ServBasedAssignment = "SB";

  public new class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(new Tuple<string, string>[5]
      {
        PXStringListAttribute.Pair("TR", "Travel"),
        PXStringListAttribute.Pair("SA", "Staff and Service (If Any)"),
        PXStringListAttribute.Pair("SB", "Services and Assigned Staff (If Any)"),
        PXStringListAttribute.Pair("SE", "Service"),
        PXStringListAttribute.Pair("NS", "Non-Stock")
      })
    {
    }
  }

  public class serviceBasedAssignment : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    ListField_LogAction_Type.serviceBasedAssignment>
  {
    public serviceBasedAssignment()
      : base("SB")
    {
    }
  }
}
