// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.ListField_ComponentType
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.FS;

public abstract class ListField_ComponentType
{
  public const 
  #nullable disable
  string AppointmentBox = "AP";
  public const string ServiceOrderGrid = "SO";
  public const string UnassignedAppGrid = "UA";

  public class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(new Tuple<string, string>[2]
      {
        PXStringListAttribute.Pair("AP", "AP"),
        PXStringListAttribute.Pair("SO", "SO")
      })
    {
    }
  }

  public class appointmentBox : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    ListField_ComponentType.appointmentBox>
  {
    public appointmentBox()
      : base("AP")
    {
    }
  }

  public class serviceOrderGrid : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    ListField_ComponentType.serviceOrderGrid>
  {
    public serviceOrderGrid()
      : base("SO")
    {
    }
  }

  public class unassignedAppGrid : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    ListField_ComponentType.unassignedAppGrid>
  {
    public unassignedAppGrid()
      : base("UA")
    {
    }
  }
}
