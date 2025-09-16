// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.MailStatusListAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.CR;

public class MailStatusListAttribute : PXStringListAttribute
{
  public const 
  #nullable disable
  string Draft = "DR";
  public const string PreProcess = "PP";
  public const string InProcess = "IP";
  public const string Scheduled = "SC";
  public const string Processed = "PD";
  public const string Failed = "FL";
  public const string Canceled = "CL";
  public const string Deleted = "DL";
  [Obsolete("This object is obsolete and will be removed. Rewrite your code without this object or contact your partner for assistance.")]
  public const string Archived = "AR";

  public MailStatusListAttribute()
    : base(new string[8]
    {
      "DR",
      "PP",
      "IP",
      "SC",
      "PD",
      "CL",
      "FL",
      "DL"
    }, new string[8]
    {
      nameof (Draft),
      "Pending Processing",
      "Processing",
      nameof (Scheduled),
      nameof (Processed),
      nameof (Canceled),
      nameof (Failed),
      nameof (Deleted)
    })
  {
  }

  public class draft : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  MailStatusListAttribute.draft>
  {
    public draft()
      : base("DR")
    {
    }
  }

  public class preProcess : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  MailStatusListAttribute.preProcess>
  {
    public preProcess()
      : base("PP")
    {
    }
  }

  public class inProcess : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  MailStatusListAttribute.inProcess>
  {
    public inProcess()
      : base("IP")
    {
    }
  }

  public class processed : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  MailStatusListAttribute.processed>
  {
    public processed()
      : base("PD")
    {
    }
  }

  public class canceled : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  MailStatusListAttribute.canceled>
  {
    public canceled()
      : base("CL")
    {
    }
  }

  public class failed : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  MailStatusListAttribute.failed>
  {
    public failed()
      : base("FL")
    {
    }
  }

  public class deleted : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  MailStatusListAttribute.deleted>
  {
    public deleted()
      : base("DL")
    {
    }
  }
}
