// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.SOOrderProcessSelected
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data.BQL;
using PX.TM;
using System;

#nullable enable
namespace PX.Objects.SO;

[OwnedFilter.Projection(typeof (SOProcessFilter), typeof (SOOrder.workgroupID), typeof (SOOrder.ownerID))]
[Serializable]
public class SOOrderProcessSelected : SOOrder
{
  public new abstract class status : BqlType<IBqlString, string>.Field<
  #nullable disable
  SOOrderProcessSelected.status>
  {
  }

  public new abstract class behavior : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOOrderProcessSelected.behavior>
  {
  }

  public new abstract class orderType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOOrderProcessSelected.orderType>
  {
  }
}
