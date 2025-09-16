// Decompiled with JetBrains decompiler
// Type: PX.SM.MailConnectionPorts
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.BQL;

#nullable enable
namespace PX.SM;

public class MailConnectionPorts
{
  public class Incoming
  {
    public const int Pop3TlsIncomingPort = 995;
    public const int Pop3NonSecureIncomingPort = 110;
    public const int ImapTlsIncomingPort = 993;
    public const int ImapNonSecureIncomingPort = 143;

    public class pop3NonSecureIncomingPort : 
      BqlType<IBqlInt, int>.Constant<
      #nullable disable
      MailConnectionPorts.Incoming.pop3NonSecureIncomingPort>
    {
      public pop3NonSecureIncomingPort()
        : base(110)
      {
      }
    }

    public class pop3TlsIncomingPort : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Constant<
      #nullable disable
      MailConnectionPorts.Incoming.pop3TlsIncomingPort>
    {
      public pop3TlsIncomingPort()
        : base(995)
      {
      }
    }

    public class imapTlsIncomingPort : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Constant<
      #nullable disable
      MailConnectionPorts.Incoming.imapTlsIncomingPort>
    {
      public imapTlsIncomingPort()
        : base(993)
      {
      }
    }

    public class imapNonSecureIncomingPort : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Constant<
      #nullable disable
      MailConnectionPorts.Incoming.imapNonSecureIncomingPort>
    {
      public imapNonSecureIncomingPort()
        : base(143)
      {
      }
    }
  }

  public class Outgoing
  {
    public const int SmtpNoneSecureOutgoingPort = 25;
    public const int SmtpImplicitTlsOutgoingPort = 465;
    public const int SmtpExplicitTlsOutgoingPort = 587;

    public class smtpNoneSecureOutgoingPort : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Constant<
      #nullable disable
      MailConnectionPorts.Outgoing.smtpNoneSecureOutgoingPort>
    {
      public smtpNoneSecureOutgoingPort()
        : base(25)
      {
      }
    }

    public class smtpImplicitTlsOutgoingPort : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Constant<
      #nullable disable
      MailConnectionPorts.Outgoing.smtpExplicitTlsOutgoingPort>
    {
      public smtpImplicitTlsOutgoingPort()
        : base(465)
      {
      }
    }

    public class smtpExplicitTlsOutgoingPort : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Constant<
      #nullable disable
      MailConnectionPorts.Outgoing.smtpExplicitTlsOutgoingPort>
    {
      public smtpExplicitTlsOutgoingPort()
        : base(587)
      {
      }
    }
  }
}
