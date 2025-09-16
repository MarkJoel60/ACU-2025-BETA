// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.CreateOnEntryAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.Objects.CR;

public class CreateOnEntryAttribute : PXStringListAttribute
{
  public const 
  #nullable disable
  string Block = "B";
  public const string Allow = "A";
  public const string Warn = "W";

  public CreateOnEntryAttribute(bool stricted = true)
  {
    string[] strArray1;
    if (!stricted)
      strArray1 = new string[2]{ "A", "W" };
    else
      strArray1 = new string[3]{ "B", "A", "W" };
    string[] strArray2;
    if (!stricted)
      strArray2 = new string[2]
      {
        nameof (Allow),
        nameof (Warn)
      };
    else
      strArray2 = new string[3]
      {
        nameof (Block),
        nameof (Allow),
        nameof (Warn)
      };
    // ISSUE: explicit constructor call
    base.\u002Ector(strArray1, strArray2);
  }

  public sealed class block : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  CreateOnEntryAttribute.block>
  {
    public block()
      : base("B")
    {
    }
  }

  public sealed class allow : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  CreateOnEntryAttribute.allow>
  {
    public allow()
      : base("A")
    {
    }
  }

  public sealed class warn : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  CreateOnEntryAttribute.warn>
  {
    public warn()
      : base("W")
    {
    }
  }
}
