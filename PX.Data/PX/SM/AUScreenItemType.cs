// Decompiled with JetBrains decompiler
// Type: PX.SM.AUScreenItemType
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.BQL;

#nullable enable
namespace PX.SM;

public class AUScreenItemType
{
  public const 
  #nullable disable
  string Action = "A";
  public const string Report = "R";
  public const string Inquiry = "I";
  public const string Popup = "P";
  public const string Condition = "C";
  public const string PopupField = "PF";
  public const string AutomationFlow = "AF";
  public const string AutomationStep = "AS";
  public const string Form = "F";
  public const string FieldForm = "FF";
  public const string EventConditional = "EC";
  public const string EventUnconditional = "EU";
  public const string EventSubscriber = "ES";

  public class action : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  AUScreenItemType.action>
  {
    public action()
      : base("A")
    {
    }
  }

  public class report : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  AUScreenItemType.report>
  {
    public report()
      : base("R")
    {
    }
  }

  public class inquiry : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  AUScreenItemType.inquiry>
  {
    public inquiry()
      : base("I")
    {
    }
  }

  public class popup : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  AUScreenItemType.popup>
  {
    public popup()
      : base("P")
    {
    }
  }

  public class condition : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  AUScreenItemType.condition>
  {
    public condition()
      : base("C")
    {
    }
  }

  public class popupField : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  AUScreenItemType.popupField>
  {
    public popupField()
      : base("PF")
    {
    }
  }

  public class automationFlow : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  AUScreenItemType.automationFlow>
  {
    public automationFlow()
      : base("AF")
    {
    }
  }

  public class automationStep : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  AUScreenItemType.automationStep>
  {
    public automationStep()
      : base("AS")
    {
    }
  }

  public class form : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  AUScreenItemType.form>
  {
    public form()
      : base("F")
    {
    }
  }

  public class fieldForm : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  AUScreenItemType.fieldForm>
  {
    public fieldForm()
      : base("FF")
    {
    }
  }

  public class eventsConditional : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    AUScreenItemType.eventsConditional>
  {
    public eventsConditional()
      : base("EC")
    {
    }
  }

  public class eventsUnconditional : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    AUScreenItemType.eventsUnconditional>
  {
    public eventsUnconditional()
      : base("EU")
    {
    }
  }

  public class eventSubscriber : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    AUScreenItemType.eventSubscriber>
  {
    public eventSubscriber()
      : base("ES")
    {
    }
  }
}
