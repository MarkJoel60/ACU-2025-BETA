// Decompiled with JetBrains decompiler
// Type: PX.Objects.Common.DataIntegrity.InconsistencyCode
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data.BQL;
using System.Collections.Generic;

#nullable enable
namespace PX.Objects.Common.DataIntegrity;

public class InconsistencyCode : ILabelProvider
{
  public const 
  #nullable disable
  string UnreleasedDocumentHasGlTransactions = "UNRELDOCHASGL";
  public const string ReleasedDocumentHasNoGlTransactions = "RELDOCHASNOGL";
  public const string BatchTotalNotEqualToTransactionTotal = "BATCHTRANTOTALMISMATCH";
  public const string ReleasedDocumentHasUnreleasedApplications = "RELDOCUNRELADJUST";
  public const string UnreleasedDocumentHasReleasedApplications = "UNRELDOCRELADJUST";
  public const string DocumentTotalsWrongPrecision = "DOCTOTALSPRECISION";
  public const string DocumentNegativeBalance = "DOCNEGATIVEBALANCE";

  public IEnumerable<ValueLabelPair> ValueLabelPairs
  {
    get
    {
      return (IEnumerable<ValueLabelPair>) new ValueLabelList()
      {
        {
          "UNRELDOCHASGL",
          "A General Ledger batch already exists for the document that has not been released."
        },
        {
          "RELDOCHASNOGL",
          "A General Ledger batch has not been generated for the released document."
        },
        {
          "BATCHTRANTOTALMISMATCH",
          "The total of the General Ledger batch is not equal to the sum of its transactions’ amounts."
        },
        {
          "RELDOCUNRELADJUST",
          "The document has been released but some of its applications have not been released."
        },
        {
          "UNRELDOCRELADJUST",
          "The document has not been released but some of its applications have been released."
        },
        {
          "DOCTOTALSPRECISION",
          "At least one of the totals of the document has a greater precision than the decimal precision specified in the currency settings."
        },
        {
          "DOCNEGATIVEBALANCE",
          "The document has obtained a negative balance."
        }
      };
    }
  }

  public class unreleasedDocumentHasGlTransactions : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    InconsistencyCode.unreleasedDocumentHasGlTransactions>
  {
    public unreleasedDocumentHasGlTransactions()
      : base("UNRELDOCHASGL")
    {
    }
  }

  public class releasedDocumentHasNoGlTransactions : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    InconsistencyCode.releasedDocumentHasNoGlTransactions>
  {
    public releasedDocumentHasNoGlTransactions()
      : base("RELDOCHASNOGL")
    {
    }
  }

  public class batchTotalNotEqualToTransactionTotal : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    InconsistencyCode.batchTotalNotEqualToTransactionTotal>
  {
    public batchTotalNotEqualToTransactionTotal()
      : base("BATCHTRANTOTALMISMATCH")
    {
    }
  }

  public class releasedDocumentHasUnreleasedApplications : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    InconsistencyCode.releasedDocumentHasUnreleasedApplications>
  {
    public releasedDocumentHasUnreleasedApplications()
      : base("RELDOCUNRELADJUST")
    {
    }
  }

  public class unreleasedDocumentHasReleasedApplications : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    InconsistencyCode.unreleasedDocumentHasReleasedApplications>
  {
    public unreleasedDocumentHasReleasedApplications()
      : base("UNRELDOCRELADJUST")
    {
    }
  }

  public class documentTotalsWrongPrecision : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    InconsistencyCode.documentTotalsWrongPrecision>
  {
    public documentTotalsWrongPrecision()
      : base("DOCTOTALSPRECISION")
    {
    }
  }

  public class documentNegativeBalance : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    InconsistencyCode.documentNegativeBalance>
  {
    public documentNegativeBalance()
      : base("DOCNEGATIVEBALANCE")
    {
    }
  }
}
