// Decompiled with JetBrains decompiler
// Type: PX.SM.LicenseFeature
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.SM;

[Serializable]
public class LicenseFeature : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXString(IsKey = true)]
  public 
  #nullable disable
  string Id { get; set; }

  [PXString]
  [PXUIField(DisplayName = "Feature Name", Enabled = false)]
  public string Name { get; set; }

  [PXBool]
  [PXUIField(DisplayName = "Activated", Enabled = false)]
  public bool? Enabled { get; set; }

  [PXString]
  [PXUIField(DisplayName = "Feature Description", Enabled = false, Visible = false)]
  public string Description { get; set; }

  [PXBool]
  [PXUIField(DisplayName = "Feature Visibility", Enabled = false, Visible = false)]
  public bool? Visible { get; set; }

  public abstract class id : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  LicenseFeature.id>
  {
  }

  public abstract class name : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  LicenseFeature.name>
  {
  }

  public abstract class enabled : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  LicenseFeature.enabled>
  {
  }

  public abstract class description : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  LicenseFeature.description>
  {
  }

  public abstract class visible : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  LicenseFeature.visible>
  {
  }
}
