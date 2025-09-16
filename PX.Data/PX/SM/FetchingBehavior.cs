// Decompiled with JetBrains decompiler
// Type: PX.SM.FetchingBehavior
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.SM;

public class FetchingBehavior
{
  public const int MarkEmailOnServerAsRead = 0;
  public const int LeaveEmailOnServerUntouched = 1;
  public const int DeleteEmailOnServer = 2;

  public class ListAttribute : PXIntListAttribute
  {
    public ListAttribute()
      : base(new int[3]{ 0, 1, 2 }, new string[3]
      {
        "Mark Email on Server as Read",
        "Leave Email on Server Untouched",
        "Delete Email on Server"
      })
    {
    }
  }

  public class markEmailOnServerAsRead : 
    BqlType<IBqlInt, int>.Constant<
    #nullable disable
    FetchingBehavior.markEmailOnServerAsRead>
  {
    public markEmailOnServerAsRead()
      : base(0)
    {
    }
  }

  public class leaveEmailOnServerUntouched : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Constant<
    #nullable disable
    FetchingBehavior.leaveEmailOnServerUntouched>
  {
    public leaveEmailOnServerUntouched()
      : base(1)
    {
    }
  }

  public class deleteEmailOnServer : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Constant<
    #nullable disable
    FetchingBehavior.deleteEmailOnServer>
  {
    public deleteEmailOnServer()
      : base(2)
    {
    }
  }
}
