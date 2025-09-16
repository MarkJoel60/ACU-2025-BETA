// Decompiled with JetBrains decompiler
// Type: PX.SM.PXEmailSyncOperation
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.SM;

public static class PXEmailSyncOperation
{
  public const 
  #nullable disable
  string NoneCode = "None";
  public const string ContactsCode = "Contacts";
  public const string EmailsCode = "Emails";
  public const string TasksCode = "Tasks";
  public const string EventsCode = "Events";

  public static string Parse(PXEmailSyncOperation.Operations direct)
  {
    switch (direct)
    {
      case PXEmailSyncOperation.Operations.None:
        return "None";
      case PXEmailSyncOperation.Operations.Contacts:
        return "Contacts";
      case PXEmailSyncOperation.Operations.Events:
        return "Events";
      case PXEmailSyncOperation.Operations.Tasks:
        return "Tasks";
      default:
        throw new PXException("Unknown sync operation.");
    }
  }

  public static PXEmailSyncOperation.Operations Parse(string code)
  {
    switch (code)
    {
      case "None":
        return PXEmailSyncOperation.Operations.None;
      case "Contacts":
        return PXEmailSyncOperation.Operations.Contacts;
      case "Events":
        return PXEmailSyncOperation.Operations.Events;
      case "Tasks":
        return PXEmailSyncOperation.Operations.Tasks;
      default:
        throw new PXException("Unknown sync operation.");
    }
  }

  [Flags]
  public enum Operations
  {
    None = 0,
    Emails = 1,
    Contacts = 2,
    Events = Contacts | Emails, // 0x00000003
    Tasks = 4,
  }

  public class none : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  PXEmailSyncOperation.none>
  {
    public none()
      : base("None")
    {
    }
  }

  public class contacts : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  PXEmailSyncOperation.contacts>
  {
    public contacts()
      : base("Contacts")
    {
    }
  }

  public class emails : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  PXEmailSyncOperation.emails>
  {
    public emails()
      : base("Emails")
    {
    }
  }

  public class tasks : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  PXEmailSyncOperation.tasks>
  {
    public tasks()
      : base("Tasks")
    {
    }
  }

  public class events : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  PXEmailSyncOperation.events>
  {
    public events()
      : base("Events")
    {
    }
  }

  public class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(new string[5]
      {
        "None",
        "Contacts",
        "Emails",
        "Tasks",
        "Events"
      }, new string[5]
      {
        "<Select Operation>",
        "Sync Contact",
        "Sync Emails",
        "Sync Tasks",
        "Sync Events"
      })
    {
    }
  }
}
