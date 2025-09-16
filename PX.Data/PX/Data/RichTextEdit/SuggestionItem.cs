// Decompiled with JetBrains decompiler
// Type: PX.Data.RichTextEdit.SuggestionItem
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Data.RichTextEdit;

[Serializable]
public class SuggestionItem : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXString]
  [PXUIField(DisplayName = "Type", Visible = false)]
  public SuggestionItem.TypeEnum ItemType { get; set; }

  [PXString]
  [PXUIField(DisplayName = "Link", Visible = false)]
  public 
  #nullable disable
  string Link { get; set; }

  [PXString]
  [PXUIField(DisplayName = "Name", Visible = true)]
  public string Caption { get; set; }

  public SuggestionItem(SuggestionItem.TypeEnum type, string link, string caption)
  {
    this.ItemType = type;
    this.Caption = caption;
    this.Link = link;
  }

  public SuggestionItem()
  {
  }

  [Serializable]
  public enum TypeEnum
  {
    Image = 1,
    File = 2,
    Article = 3,
    Screen = 4,
  }

  public abstract class type : IBqlField, IBqlOperand
  {
  }

  public abstract class link : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SuggestionItem.link>
  {
  }

  public abstract class caption : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SuggestionItem.caption>
  {
  }

  public abstract class suggestionTypes
  {
    public class file : BqlType<
    #nullable enable
    IBqlInt, int>.Constant<
    #nullable disable
    SuggestionItem.suggestionTypes.file>
    {
      public file()
        : base(2)
      {
      }
    }

    public class image : BqlType<
    #nullable enable
    IBqlInt, int>.Constant<
    #nullable disable
    SuggestionItem.suggestionTypes.image>
    {
      public image()
        : base(1)
      {
      }
    }

    public class article : BqlType<
    #nullable enable
    IBqlInt, int>.Constant<
    #nullable disable
    SuggestionItem.suggestionTypes.article>
    {
      public article()
        : base(3)
      {
      }
    }

    public class screen : BqlType<
    #nullable enable
    IBqlInt, int>.Constant<
    #nullable disable
    SuggestionItem.suggestionTypes.screen>
    {
      public screen()
        : base(4)
      {
      }
    }
  }
}
