// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.DuplicateFilter
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.AR;

[Serializable]
public class DuplicateFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  /// <summary>
  /// The field to enter a new reference number of a document to avoid duplicates
  /// </summary>
  [PXString(15, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC")]
  [PXUIField]
  public virtual 
  #nullable disable
  string RefNbr { get; set; }

  /// <summary>
  /// The field to store a warning message that a document with a certain type and number already exists
  /// </summary>
  [PXString]
  public virtual string Label { get; set; }

  public abstract class refNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  DuplicateFilter.refNbr>
  {
  }

  public abstract class label : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  DuplicateFilter.label>
  {
  }
}
