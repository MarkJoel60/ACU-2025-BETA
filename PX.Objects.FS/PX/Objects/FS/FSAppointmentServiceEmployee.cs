// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.FSAppointmentServiceEmployee
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using System;

#nullable enable
namespace PX.Objects.FS;

[PXBreakInheritance]
[PXProjection(typeof (Select<FSAppointmentDet>), Persistent = false)]
[Serializable]
public class FSAppointmentServiceEmployee : FSAppointmentDet
{
  [PXDBString(4, IsFixed = true)]
  [PXUIField]
  public override 
  #nullable disable
  string LineRef { get; set; }

  public new class PK : 
    PrimaryKeyOf<FSAppointmentServiceEmployee>.By<FSAppointmentDet.srvOrdType, FSAppointmentDet.refNbr, FSAppointmentDet.lineNbr>
  {
    public static FSAppointmentServiceEmployee Find(
      PXGraph graph,
      string srvOrdType,
      string refNbr,
      int? lineNbr,
      PKFindOptions options = 0)
    {
      return (FSAppointmentServiceEmployee) PrimaryKeyOf<FSAppointmentServiceEmployee>.By<FSAppointmentDet.srvOrdType, FSAppointmentDet.refNbr, FSAppointmentDet.lineNbr>.FindBy(graph, (object) srvOrdType, (object) refNbr, (object) lineNbr, options);
    }
  }

  public new abstract class appointmentID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSAppointmentServiceEmployee.appointmentID>
  {
  }

  public new abstract class sODetID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSAppointmentServiceEmployee.sODetID>
  {
  }

  public new abstract class lineType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSAppointmentServiceEmployee.lineType>
  {
  }

  public new abstract class lineRef : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSAppointmentServiceEmployee.lineRef>
  {
  }

  public new abstract class inventoryID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSAppointmentServiceEmployee.inventoryID>
  {
  }
}
