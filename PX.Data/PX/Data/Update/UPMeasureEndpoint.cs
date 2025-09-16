// Decompiled with JetBrains decompiler
// Type: PX.Data.Update.UPMeasureEndpoint
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Data.Update;

[Serializable]
public class UPMeasureEndpoint : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXUIField(DisplayName = "Session ID")]
  [PXDBString(16 /*0x10*/, IsKey = true)]
  [PXDefault]
  [PXSelector(typeof (Search<UPMeasureEndpoint.endpointID>))]
  public virtual 
  #nullable disable
  string EndpointID { get; set; }

  [PXUIField(DisplayName = "Destination URL")]
  [PXDBString(256 /*0x0100*/)]
  [PXDefault]
  public virtual string Url { get; set; }

  [PXUIField(DisplayName = "User Name")]
  [PXDBString(256 /*0x0100*/)]
  [PXDefault]
  public virtual string Login { get; set; }

  [PXUIField(DisplayName = "Password")]
  [PXDBString(256 /*0x0100*/)]
  [PXDefault]
  public virtual string Password { get; set; }

  [PXUIField(DisplayName = "Form Name")]
  [PXDBString(8, InputMask = "CC.CC.CC.CC")]
  [PXDefault]
  public virtual string Screen { get; set; }

  [PXUIField(DisplayName = "Operation")]
  [PXDBString(64 /*0x40*/)]
  [PXDefault]
  [PXStringList(new string[] {null}, new string[] {""}, ExclusiveValues = false)]
  public virtual string Action { get; set; }

  [PXUIField(DisplayName = "Measurements")]
  [PXDBInt]
  [PXDefault(100)]
  public virtual int? Count { get; set; }

  public abstract class endpointID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  UPMeasureEndpoint.endpointID>
  {
  }

  public abstract class url : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  UPMeasureEndpoint.url>
  {
  }

  public abstract class login : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  UPMeasureEndpoint.login>
  {
  }

  public abstract class password : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  UPMeasureEndpoint.password>
  {
  }

  public abstract class screen : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  UPMeasureEndpoint.screen>
  {
  }

  public abstract class action : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  UPMeasureEndpoint.action>
  {
  }

  public abstract class count : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  UPMeasureEndpoint.count>
  {
  }
}
