// Decompiled with JetBrains decompiler
// Type: PX.SM.MailConnectionEncryption
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.SM;

public class MailConnectionEncryption
{
  public const int None = 0;
  public const int ImplicitTls = 1;
  public const int ExplicitTls = 2;

  public class none : BqlType<IBqlInt, int>.Constant<
  #nullable disable
  MailConnectionEncryption.none>
  {
    public none()
      : base(0)
    {
    }
  }

  public class implicitTls : BqlType<
  #nullable enable
  IBqlInt, int>.Constant<
  #nullable disable
  MailConnectionEncryption.implicitTls>
  {
    public implicitTls()
      : base(1)
    {
    }
  }

  public class explicitTls : BqlType<
  #nullable enable
  IBqlInt, int>.Constant<
  #nullable disable
  MailConnectionEncryption.explicitTls>
  {
    public explicitTls()
      : base(2)
    {
    }
  }

  public class IncomingAttribute : PXIntListAttribute
  {
    public IncomingAttribute()
      : base((0, "None"), (1, "Implicit TLS"))
    {
    }
  }

  public class OutgoingAttribute : PXIntListAttribute
  {
    public OutgoingAttribute()
      : base((0, "None"), (1, "Implicit TLS"), (2, "Explicit TLS"))
    {
    }
  }
}
