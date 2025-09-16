// Decompiled with JetBrains decompiler
// Type: PX.SM.IncomingMailProtocolsAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.SM;

/// <exclude />
public class IncomingMailProtocolsAttribute : PXIntListAttribute
{
  public const int _POP3 = 0;
  public const int _IMAP = 1;
  public const int _EXCHANGE = 2;

  public IncomingMailProtocolsAttribute()
    : base(new int[2]{ 0, 1 }, new string[2]
    {
      "POP3",
      "IMAP"
    })
  {
  }

  public class pop3 : BqlType<IBqlInt, int>.Constant<
  #nullable disable
  IncomingMailProtocolsAttribute.pop3>
  {
    public pop3()
      : base(0)
    {
    }
  }

  public class imap : BqlType<
  #nullable enable
  IBqlInt, int>.Constant<
  #nullable disable
  IncomingMailProtocolsAttribute.imap>
  {
    public imap()
      : base(1)
    {
    }
  }

  public class exchange : BqlType<
  #nullable enable
  IBqlInt, int>.Constant<
  #nullable disable
  IncomingMailProtocolsAttribute.imap>
  {
    public exchange()
      : base(2)
    {
    }
  }
}
