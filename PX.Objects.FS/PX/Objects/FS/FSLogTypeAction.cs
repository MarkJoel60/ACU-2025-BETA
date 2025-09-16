// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.FSLogTypeAction
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.FS;

public class FSLogTypeAction
{
  public class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(new Tuple<string, string>[4]
      {
        PXStringListAttribute.Pair("SE", "Service"),
        PXStringListAttribute.Pair("TR", "Travel"),
        PXStringListAttribute.Pair("SA", "Staff and Service (If Any)"),
        PXStringListAttribute.Pair("SB", "Services and Assigned Staff (If Any)")
      })
    {
    }
  }

  public class STListAttribute : PXStringListAttribute
  {
    public STListAttribute()
      : base(new Tuple<string, string>[2]
      {
        PXStringListAttribute.Pair("SE", "Service"),
        PXStringListAttribute.Pair("TR", "Travel")
      })
    {
    }
  }

  public class Service : BqlType<IBqlString, string>.Constant<
  #nullable disable
  FSLogTypeAction.Service>
  {
    public Service()
      : base("SE")
    {
    }
  }

  public class Travel : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  FSLogTypeAction.Travel>
  {
    public Travel()
      : base("TR")
    {
    }
  }

  public class StaffAssignment : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    FSLogTypeAction.StaffAssignment>
  {
    public StaffAssignment()
      : base("SA")
    {
    }
  }

  public class SrvBasedOnAssignment : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    FSLogTypeAction.SrvBasedOnAssignment>
  {
    public SrvBasedOnAssignment()
      : base("SB")
    {
    }
  }
}
