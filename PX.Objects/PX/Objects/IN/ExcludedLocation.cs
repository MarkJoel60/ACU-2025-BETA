// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.ExcludedLocation
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CS;
using System;

#nullable enable
namespace PX.Objects.IN;

[Serializable]
public class ExcludedLocation : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXInt(IsKey = true)]
  [PXUIField(DisplayName = "Location ID")]
  [PXDimensionSelector("INLOCATION", typeof (Search<INLocation.locationID, Where<INLocation.siteID, Equal<Current<PIGeneratorSettings.siteID>>, And<INLocation.active, Equal<boolTrue>>>>), typeof (INLocation.locationCD), DescriptionField = typeof (INLocation.descr))]
  public virtual int? LocationID { get; set; }

  [PXString(60, IsUnicode = true)]
  [PXUIField(DisplayName = "Description", Enabled = false)]
  public virtual 
  #nullable disable
  string Descr { get; set; }

  public abstract class locationID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ExcludedLocation.locationID>
  {
  }

  public abstract class descr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ExcludedLocation.descr>
  {
  }
}
