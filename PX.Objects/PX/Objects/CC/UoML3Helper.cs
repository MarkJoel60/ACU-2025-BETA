// Decompiled with JetBrains decompiler
// Type: PX.Objects.CC.UoML3Helper
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.CCProcessingUtility;
using PX.Data;
using PX.Data.BQL;
using System;
using System.Collections.Generic;

#nullable enable
namespace PX.Objects.CC;

/// <summary>Helper for Level 3 codes for units of measure.</summary>
public static class UoML3Helper
{
  /// <summary>Return Level 3 Codes for selector.</summary>
  public static 
  #nullable disable
  IEnumerable<UoML3Helper.UoML3Code> GetCodes()
  {
    List<UoML3Helper.UoML3Code> codes = new List<UoML3Helper.UoML3Code>();
    foreach ((string, string) l3Code in UnitOfMeasureL3Codes.L3Codes)
      codes.Add(new UoML3Helper.UoML3Code()
      {
        L3Code = l3Code.Item1,
        Description = l3Code.Item2
      });
    return (IEnumerable<UoML3Helper.UoML3Code>) codes;
  }

  /// <summary>Unit of Measure Level 3 Data code.</summary>
  [PXCacheName("Level 3 Code Unit of Measure")]
  public class UoML3Code : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    /// <summary>The unit of measure.</summary>
    [PXUIField(DisplayName = "Level 3 Unit ID")]
    [PXString(3, IsKey = true)]
    [PXSelectorByMethod(typeof (UoML3Helper), "GetCodes", typeof (Search<UoML3Helper.UoML3Code.l3Code>), new Type[] {typeof (UoML3Helper.UoML3Code.l3Code), typeof (UoML3Helper.UoML3Code.description)}, DescriptionField = typeof (UoML3Helper.UoML3Code.description))]
    public virtual string L3Code { get; set; }

    /// <summary>The description of Level 3 code.</summary>
    [PXUIField(DisplayName = "Description")]
    [PXString(256 /*0x0100*/)]
    public virtual string Description { get; set; }

    public abstract class l3Code : BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    UoML3Helper.UoML3Code.l3Code>
    {
    }

    public abstract class description : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      UoML3Helper.UoML3Code.description>
    {
    }
  }
}
