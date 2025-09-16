// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.AppointmentBoxComponentField
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.FS;

[PXBreakInheritance]
[PXProjection(typeof (Select<FSCalendarComponentField, Where<FSCalendarComponentField.componentType, Equal<ListField_ComponentType.appointmentBox>>>), Persistent = true)]
[Serializable]
public class AppointmentBoxComponentField : FSCalendarComponentField
{
  [PXDefault("AP")]
  [PXDBString(2, IsFixed = true, IsKey = true)]
  [PXUIField(DisplayName = "Component Type", Enabled = false)]
  [ListField_ComponentType.List]
  public override 
  #nullable disable
  string ComponentType { get; set; }

  [PXDBInt]
  [PXDefault(2147483647 /*0x7FFFFFFF*/)]
  [PXUIField(Visible = false, Enabled = false)]
  public override int? SortOrder { get; set; }

  [PXDefault("PX.Objects.FS.FSAppointment")]
  [PXDBString(InputMask = "", IsUnicode = false, IsKey = true)]
  [PXUIField]
  [PXStringList(new string[] {null}, new string[] {""}, ExclusiveValues = false)]
  public override string ObjectName { get; set; }

  [PXDefault]
  [PXDBString(InputMask = "", IsUnicode = false, IsKey = true)]
  [PXUIField(DisplayName = "Field Name")]
  [PXStringList(new string[] {null}, new string[] {""}, ExclusiveValues = false)]
  public override string FieldName { get; set; }

  public new abstract class componentType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AppointmentBoxComponentField.componentType>
  {
    public abstract class Values : ListField_ComponentType
    {
    }
  }

  public new abstract class sortOrder : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    AppointmentBoxComponentField.sortOrder>
  {
  }

  public new abstract class objectName : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AppointmentBoxComponentField.objectName>
  {
  }

  public new abstract class fieldName : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AppointmentBoxComponentField.fieldName>
  {
  }
}
