// Decompiled with JetBrains decompiler
// Type: PX.Objects.CM.CMReportTranType
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.Common;
using System;
using System.Collections.Generic;

#nullable enable
namespace PX.Objects.CM;

[Serializable]
public class CMReportTranType : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXString(3, IsFixed = true)]
  [LabelList(typeof (CMReportTranType.tranType))]
  public virtual 
  #nullable disable
  string TranType { get; set; }

  public class tranType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CMReportTranType.tranType>,
    ILabelProvider
  {
    public const string Revalue = "REV";

    public IEnumerable<ValueLabelPair> ValueLabelPairs
    {
      get
      {
        return (IEnumerable<ValueLabelPair>) new ValueLabelList()
        {
          {
            "REV",
            "Revalue"
          }
        };
      }
    }

    public class revalue : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    CMReportTranType.tranType.revalue>
    {
      public revalue()
        : base("REV")
      {
      }
    }
  }
}
